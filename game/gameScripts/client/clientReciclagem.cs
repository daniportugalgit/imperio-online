// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientReciclagem.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quinta-feira, 27 de março de 2008 16:42
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function clientAskReciclar(){
	%foco = Foco.getObject(0);
	%onde = %foco.onde.getName();
	if(isObject(%foco)){
		if($jogadorDaVez == $mySelf){ //só na minha vez de jogar
			if(!%foco.reciclando){
				commandToServer('reciclar', %onde);
			} else {
				if($myPersona.aca_v_3 == 3){
					commandToServer('reciclar', %onde);
				} else {
					clientMsg("reciclagemEmAndamento", 3000);	
				}
			}
		}
	}
}

function clientCmdReciclar(%onde, %lvl){
	echo("clientCmdReciclar(" @ %onde.getName() @ ", " @ %lvl @ ")");
	%unit = clientGetGameUnit(%onde, "pos0"); //pega a estrutura
	%dono = %unit.dono;
	%ondeInGame = %unit.onde;
	
	%unit.myLvlRetorno = %lvl;
	
	if(%lvl == 1 || %lvl == 2){
		%unit.reciclando = true;
		//colocar efeito de reciclagem:
		if(!%unit.oculta || (%unit.oculta && %unit.dono == $mySelf)){
			%reciclarEffect = new t2dParticleEffect(){scenegraph = %unit.scenegraph;};
			%reciclarEffect.loadEffect("~/data/particles/reciclando.eff");
			%reciclarEffect.setEffectLifeMode("INFINITE");
			%reciclarEffect.mount(%unit, 0, 0, 0, 0, 0, 0, 0); 
			%reciclarEffect.playEffect();
			%unit.myReciclarEffect = %reciclarEffect;
		}
	} else if(%lvl == 3){
		clientFinalizarReciclagem(%onde);
	} else if(%lvl == 0){
		echo("ERRO: jogador da vez não possui tecnologia de reciclagem. Ignorando pedido;");	
	}
	
	clientAtualizarEstatisticas();
}

function clientFinalizarReciclagem(%onde){
	echo("clientFinalizarReciclagem(" @ %onde.getName() @ ")");
	%unit = clientGetGameUnit(%onde, "pos0"); //pega a estrutura
	%dono = %unit.dono; //pega o dono da estrutura
	%ondeInGame = %unit.onde; //pega a área no client
	
	//remove do simSet de bases:
	%dono.mySimBases.remove(%unit); 
	
	//remove da Área:
	%ondeInGame.pos0Quem = "nada";
	%ondeInGame.pos0Flag = false;
	%ondeInGame.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
	
	//faz o efeito de reciclagem completa:
	if(!%unit.oculta || (%unit.oculta && %unit.dono == $mySelf)){
		%reciclagemCompleta = new t2dParticleEffect(){scenegraph = %unit.scenegraph;};
		%reciclagemCompleta.loadEffect("~/data/particles/reciclagemCompleta.eff");
		%reciclagemCompleta.setPosition(%ondeInGame.pos0);
		%reciclagemCompleta.playEffect();
	}
			
	//devolve parte dos imperiais:
	%retorno = clientGetRetornoDeReciclagem(%unit);
	%dono.imperiais += %retorno;
	
	
	//apaga a peça:
	if(isObject(%unit.myReciclarEffect)){
		%unit.myReciclarEffect.safeDelete(); 
	}
	%unit.safeDelete(); 
	
	//atualiza as estatísticas:
	clientAtualizarEstatisticas();
	atualizarImperiaisGui();
	atualizarBotoesDeCompraHumanos();
}

function clientGetRetornoDeReciclagem(%unit)
{
	if(%unit.refinaria)
	{
		if(%unit.myLvlRetorno == 1)
			return 3;
			
		return 4;
	}
	
	//bases comuns:
	if(%unit.myLvlRetorno == 1)
		return 5;
		
	if(%unit.dono.engenheiro)
		return 6;
	
	return 8;	
}

///////////
function clientVerificarReciclagens(){
	//echo("verificarReciclagens::BASES: " @ $jogadorDaVez.mySimBases.getCount() @ ")");
	%basesCount = $jogadorDaVez.mySimBases.getCount();
	for(%j = 0; %j < %basesCount; %j++){
		for(%i = 0; %i < $jogadorDaVez.mySimBases.getCount(); %i++){
			%estrutura = $jogadorDaVez.mySimBases.getObject(%i);
			if(%estrutura.reciclando){
				//echo("->Estrutura reciclando em " @ %estrutura.onde.getName());
				clientFinalizarReciclagem(%estrutura.onde);	
			} else {
				//echo("-<Estrutura NÃO reciclando em " @ %estrutura.onde.getName());		
			}
		}
	}
}
