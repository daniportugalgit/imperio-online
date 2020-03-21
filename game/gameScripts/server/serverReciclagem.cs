// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverReciclagem.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quinta-feira, 27 de março de 2008 16:41
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function serverCmdReciclar(%client, %onde){
	//echo("serverCmdReciclar(" @ %onde @ ")");
	%jogo = %client.player.jogo;
	%eval = "%areaNoJogo = %jogo." @ %onde @ ";";
	eval(%eval);
	%unit = %areaNoJogo.pos0Quem;
	%dono = %unit.dono;
	%ondeInGame = %unit.onde;
	
	%lvl = %dono.persona.aca_v_3;
	%unit.myLvlRetorno = %lvl;
	
	if(%lvl == 1 || %lvl == 2){
		%unit.reciclando = true;
	} else if(%lvl == 3){
		serverFinalizarReciclagem(%ondeInGame);
	} else if(%lvl == 0){
		echo("**CHEATER??? Persona: " @ %dono.persona.nome @ "; User: " @ %dono.persona.user @ "; ERRO: não possuia tecnologia de reciclagem, mas o client não bloqueou o pedido.");	
	}
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		%player = %jogo.simPlayers.getObject(%i);
		commandToClient(%player.client, 'reciclar', %onde, %lvl);
	}
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'reciclar', %onde, %lvl);
	}
}

function serverFinalizarReciclagem(%ondeInGame){
	//echo("serverFinalizarReciclagem(" @ %ondeInGame.myName @ ")");
	%unit = %ondeInGame.pos0Quem;
	%dono = %unit.dono; //pega o dono da estrutura
	%jogo = %unit.dono.jogo;
		
	//remove do simSet de bases:
	%dono.mySimBases.remove(%unit); 
	
	//remove da Área:
	%ondeInGame.pos0Quem = "nada";
	%ondeInGame.pos0Flag = false;
	%ondeInGame.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
	
			
	//devolve parte dos imperiais:
	%retorno = serverGetRetornoDeReciclagem(%unit);
	%dono.imperiais += %retorno;
	
	
	%jogo.verificarGruposGlobal();
	%jogo.verificarObjetivosGlobal();
	
	if(!%jogo.semPesquisas){
		if(%jogo.jogadorDaVez.oculto){
			if(%unit.refinaria){
				%myUnit = "REFINARIA";	
			} else {
				%myUnit = "BASE";	
			}
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				%persona = %jogo.simPlayers.getObject(%i).persona;
				if(%persona.especie $= "gulok"){
					if((%persona.aca_i_1 == 2 && %jogo.jogadorDaVez.persona.aca_i_1 < 3) || %persona.aca_i_1 == 3){
						if(%persona.nome !$= %jogo.jogadorDaVez.persona.nome){
							commandToClient(%persona.client, 'i_intelOculto', %jogo.jogadorDaVez.persona.nome, "reciclou", %myUnit);	
						}
					}
				}
			}
		}
	}
	
	//apaga a peça:
	%unit.safeDelete(); 
	
	//suicidio se o truta reciclou a última base/refinaria:
	if(%dono.mySimAreas.getCount() <= 0){
		serverKillPlayer(%dono.persona);
	}
}

function serverGetRetornoDeReciclagem(%unit)
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
function jogo::verificarReciclagens(%this){
	%jogadorDaVez = %this.jogadorDaVez;
	//echo("jogo::verificarReciclagens::BASES: " @ %jogadorDaVez.mySimBases.getCount() @ ")");
	%basesCount = %jogadorDaVez.mySimBases.getCount();
	for(%j = 0; %j < %basesCount; %j++){
		for(%i = 0; %i < %jogadorDaVez.mySimBases.getCount(); %i++){
			%estrutura = %jogadorDaVez.mySimBases.getObject(%i);
			if(%estrutura.reciclando){
				//echo("->Estrutura reciclando em " @ %estrutura.onde.myName);
				serverFinalizarReciclagem(%estrutura.onde);	
			} else {
				//echo("-<Estrutura NÃO reciclando em " @ %estrutura.onde.myName);	
			}
		}
	}
}
