// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientMovimentacao.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 30 de outubro de 2007 21:12
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientGetGameUnit(%area, %pos){
//se a unidade estiver na pos1 ou na pos2, é só pegar. Se estiver em uma das pos reserva, tem que verificar se a área é mista e, se for, pegar uma unidade do jogadorDaVez.
	//pega a unidade correta:
	if(%pos $= "pos2" || %pos $= "pos1" || %pos $= "pos0"){
		%eval = "%unit = " @ %area @ "." @ %pos @ "Quem;";
		eval(%eval);
	}else if(%pos $= "pos3"){
		if(%area.dono !$= "MISTA" && %area.dono !$= "COMPARTILHADA"){
			%unit = %area.myPos3List.getObject(0);
		} else {
			%pos3Count = %area.myPos3List.getCount();
			for(%i = 0; %i < %pos3Count; %i++){
				%tempUnit = %area.myPos3List.getObject(%i);
				if(%tempUnit.dono == $jogadorDaVez){
					%unit = %area.myPos3List.getObject(%i);
					//echo("Unidade encontrada, DONO: " @ %unit.dono);
					if(isObject(%unit.myTransporte)){
						if(%unit.myTransporte.getCount() > 0){
							%i = %pos3Count;
						}
					}
				}
			}
		}
	}else if (%pos $= "pos4"){
		if(%area.dono !$= "MISTA" && %area.dono !$= "COMPARTILHADA"){
			%unit = %area.myPos4List.getObject(0);
		} else {
			%pos4Count = %area.myPos4List.getCount();
			for(%i = 0; %i < %pos4Count; %i++){
				%tempUnit = %area.myPos4List.getObject(%i);
				if(%tempUnit.dono == $jogadorDaVez){
					%unit = %area.myPos4List.getObject(%i);
					//echo("Unidade encontrada, DONO: " @ %unit.dono);
					if(isObject(%unit.myTransporte)){
						if(%unit.myTransporte.getCount() > 0){
							%i = %pos4Count;
						}
					}
				}
			}
		}
	}
	return %unit;
}


function clientAskMovimentar(%areaDeOrigem, %posDeOrigem, %areaAlvo){
	%clientAreaDeOrigem = %areaDeOrigem.getName();
	%clientAreaAlvo = %areaAlvo.getName();
			
	if(!$estouNoTutorial){
		if($rodadaAtual > 0){
			%unit = clientGetGameUnit(%areaDeOrigem, %posDeOrigem);
			
			if(%unit.class $= "rainha" && clientGetAreaTemArtefatoOuReliquia(%areaAlvo))
				return;
			
			if($mySelf.movimentos > 0 || %unit.JPagora > 0 || $jogadorDaVez == $aiPlayer){
				clientPushServerComDot();
				
				if(%unit.class $= "rainha"){
					if(%areaAlvo.pos0Flag == true && %areaAlvo.pos0Quem.class $= "rainha"){
						echo("Cancelando comando de entrar com uma rainha onde já havia outra");
						clientPopServerComDot();
					} else {
						commandToServer('movimentar', %clientAreaDeOrigem, %posDeOrigem, %clientAreaAlvo);
					}
				} else {
					commandToServer('movimentar', %clientAreaDeOrigem, %posDeOrigem, %clientAreaAlvo);
				}
			} else {
				//clientMsg seus movimentos acabaram
				clientMsg("movimentosAcabaram", 3000);
			}
		} else { //ainda é antes da primeira rodada
			echo("Imposível movimentar antes da primeira rodada");
		}
	} else {
		if(%clientAreaAlvo $= $tut_campanha.passo.alvo.getName() || $jogadorDaVez == $player2){
			clientCmdMoverUnidade(%clientAreaDeOrigem, %posDeOrigem, %clientAreaAlvo);	
		}
	}
}

function clientGetAreaTemArtefatoOuReliquia(%area)
{
	if(%area.artefato || %area.reliquia)
		return true;
		
	return false;
}

function clientCmdMoverUnidade(%areaDeOrigem, %posDeOrigem, %areaAlvo){
	//echo("clientCmdMoverUnidade(" @ %areaDeOrigem SPC %posDeOrigem SPC %areaAlvo @ ")");
	%unit = clientGetGameUnit(%areaDeOrigem, %posDeOrigem); //pega a unidade que será movimentada
		
	ghostSelect(%unit); // se for um adversário, marcar a peça que está sendo movimentada
	
	clientRemoverUnidade(%unit, %unit.onde); //remove a peça da área em que está
	%areaAlvo.positionUnit(%unit); //coloca a peça na área-alvo
	
	if(%unit.class !$= "lider" && %unit.class !$= "zangao"){
		//echo("Movendo peça comum");
		$jogadorDaVez.movimentos -= 1;
		clientCriarUndo(%unit, %areaDeOrigem, false); //cria um Undo normal
	} else {
		if(%unit.JPagora < 1){
			//echo("Movendo Lider sem JetPack");
			$jogadorDaVez.movimentos -= 1;
			clientCriarUndo(%unit, %areaDeOrigem, false); //cria um Undo normal
		} else {
			//echo("Movendo Lider COM JetPack: movimento não foi descontado.");
			%unit.JPagora--;
			clientCriarUndo(%unit, %areaDeOrigem, true); //cria um Undo especial
			
			if(%unit.class $= "zangao"){
				%turbina = new t2dParticleEffect(){scenegraph = %unit.scenegraph;};
				%turbina.loadEffect("~/data/particles/zangaoJet.eff");
				%turbina.setEffectLifeMode("KILL", 0.5);
				%turbina.mount(%unit, 0, 0, 0, 1, 0, 0, 0); //o quarto número é 1[bool], quer dizer pra herdar a rotação do objeto-base
				%turbina.playEffect();
				//alxPlay( somDaExplosao );
			} else {
				%turbina = new t2dParticleEffect(){scenegraph = %unit.scenegraph;};
				%turbina.loadEffect("~/data/particles/liderJet.eff");
				%turbina.setEffectLifeMode("KILL", 0.5);
				%turbina.mount(%unit, 0, 0, 0, 1, 0, 0, 0); //o quarto número é 1[bool], quer dizer pra herdar a rotação do objeto-base
				%turbina.playEffect();
				//alxPlay( somDaExplosao );
			}
		}
	}
		
	%areaAlvo.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso	
	
	atualizarMovimentosGui(); //atualiza os movimentos e já separa os meus dos adversários por um cinza inativo para os últimos (todos os clients vêem os movimentos do jogadorDaVez);
	clientLigarFlechas(%areaAlvo); //tem que desligar e ligar quando seleciona outra peça;
	atualizarBotoesDeCompra();
	schedule(300, 0, "atualizarBotoesDeCompra"); //verifica se pode fazer emboscada
	clientPopServerComDot(); //devolve o controle do jogo ao client
	//clientAskConfirmarAtoFeito(%unit.dono.id);
	//echo("FIM DA clientCmdMoverUnidade(" @ %areaDeOrigem SPC %posDeOrigem SPC %areaAlvo @ ")");
}




function clientRemoverUnidade(%unit, %area){
	//se a unidade estivesse na pos1 ou na pos2, tem que ver se existem unidades repositoras. Do contrário, basta remover a peça.
	if(%unit.pos $= "pos0"){
		%area.pos0Flag = false;
		%area.pos0Quem = 0;
		%area.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
	} else if (%unit.pos $= "pos1"){
		%area.pos1Flag = "nada";
		clientReporUnidade(%area, "pos1");
	} else if (%unit.pos $= "pos2"){
		%area.pos2Flag = "nada";
		clientReporUnidade(%area, "pos2");
	} else {
		if (%unit.pos $= "pos3"){
			%area.myPos3List.remove(%unit);
			%area.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
		} else if (%unit.pos $= "pos4"){
			%area.myPos4List.remove(%unit);
			%area.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
		}
		clientAtualizarPosReservaTxt(%area);
	} 
}

function clientVerificarEstoque(%area){
	%estoque = 0; //zera a variável de estoque
	if(isObject(%area.myPos4List)){
		if (%area.myPos4List.getCount() >=  1){ //se existe alguém na pos4
			%unidade = %area.myPos4List.getObject(0); //os tanques têm preferência, %unidade pssa a ser a unidade repositora
			if(%unidade.class !$= "ovo"){
				%area.myPos4List.remove(%unidade); //remove a unidade repositora da sua posição anterior
				%estoque = 1; //marca que existe uma unidade no estoque
			} else if (%area.myPos3List.getCount() >=  1){ //se não houver ninguém na pos4, e houver alguém na pos3
				%unidade = %area.myPos3List.getObject(0); //pega a primeira unidade da pos3 e marca como a %unidade
				%area.myPos3List.remove(%unidade); //remove ela da pos3
				%estoque = 1; //marca que existe uma unidade no estoque
			}
		} else if (%area.myPos3List.getCount() >=  1){ //se não houver ninguém na pos4, e houver alguém na pos3
			%unidade = %area.myPos3List.getObject(0); //pega a primeira unidade da pos3 e marca como a %unidade
			%area.myPos3List.remove(%unidade); //remove ela da pos3
			%estoque = 1; //marca que existe uma unidade no estoque
		}
	} else if (%area.myPos3List.getCount() >=  1){ //se não houver ninguém na pos4, e houver alguém na pos3
		%unidade = %area.myPos3List.getObject(0); //pega a primeira unidade da pos3 e marca como a %unidade
		%area.myPos3List.remove(%unidade); //remove ela da pos3
		%estoque = 1; //marca que existe uma unidade no estoque
	}
	return %estoque SPC %unidade;
}
	
//função unificada de reposição de unidades (CLIENT SIDE):
function clientReporUnidade(%area, %pos){
	//primeiro verifica se existem unidades no estoque:
	%areaEstoque = clientVerificarEstoque(%area);
	%estoque = FirstWord(%areaEstoque);
	%unidade = getWord(%areaEstoque, 1);
	
	if (%estoque == 1){ //se existir alguma unidade no estoque, então
		switch$(%pos){ //verifica qual é posição a ser reposta
			case "pos1": //se for a pos1,
			%unidade.action("moveToLoc", %area.pos1); //manda a unidade repositora pra lá
			%unidade.pos = "pos1"; //marca na unidade sua nova posição
			%area.pos1Flag = %unidade.class; //marca na área a Flag para a classe da unidade repositora
			%area.pos1Quem = %unidade; //marca na área a nova unidade que está naquela posição
			
      	  case "pos2": //se for a pos2, proceder da mesma maneira, só que mandando pra pos2
			%unidade.action("moveToLoc", %area.pos2); 
			%unidade.pos = "pos2";
			%area.pos2Flag = %unidade.class;
			%area.pos2Quem = %unidade;
		}
	} else { //se não houver ninguém no estoque, então
		if (%area.pos1Flag $= "nada" && %area.pos2Flag !$= "nada"){ //se a pos1 tiver sido destruida e houver alguém na pos2, então
			%area.pos2Quem.action("moveToLoc", %area.pos1); //mandar quem está na pos2 para a pos1
			%area.pos1Quem = %area.pos2Quem; //marca na área quem está na pos1 agora
			%area.pos1Flag = %area.pos1Quem.class; //marca na área a Flag da posição
			%area.pos1Quem.pos = "pos1"; //marca na unidade sua nova posição
			%area.pos2Quem = "nada"; //marca na área que a pos2 está vazia
			%area.pos2Flag = "nada"; //marca na área a Flag da pos2
		} 
	}
	%area.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
}

function clientAskEmbarcar(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posDoNavio, %undo, %incorporar, %carregar){
	%clientAreaDeOrigem = %areaDeOrigem.getName();
	%clientAreaAlvo = %areaAlvo.getName();
	if(!$estouNoTutorial){
		if($rodadaAtual > 0){
			clientPushServerComDot();
			commandToServer('embarcar', %clientAreaDeOrigem, %posDeOrigem, %clientAreaAlvo, %posDoNavio, %undo, %incorporar, %carregar);
		} else {
			//ainda é antes da primeira rodada (segurança)
			echo("Imposível embarcar antes da primeira rodada");	
		}
	} else {
		%navio = clientGetGameUnit(%areaAlvo, %posDoNavio);
		if(%navio == $tut_campanha.passo.alvo || $jogadorDaVez == $player2){
			clientCmdEmbarcarUnidade(%clientAreaDeOrigem, %posDeOrigem, %clientAreaAlvo, %posDoNavio);
			tut_verificarObjetivo(false, %posDoNavio);
		}
	}
}

//função unificada para o embarque de qq posição:
function clientCmdEmbarcarUnidade(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posDoNavio, %undo, %incorporar, %carregar){
	//echo("clientCmdEmbarcarUnidade(" @ %areaDeOrigem SPC %posDeOrigem SPC %areaAlvo SPC %posDoNavio SPC %undo @ ")");
	//clientPopServerComDot();
		
	%unidade = clientGetGameUnit(%areaDeOrigem, %posDeOrigem); //pega a unidade que será movimentada
	%navio = clientGetGameUnit(%areaAlvo, %posDoNavio); //pega o navio
	
		
	if (%navio.transporteFlag == false){
		%navio.myTransporte = new SimSet();
		%navio.transporteFlag = true;
	}
	
	resetSelection();
	%navio.myTransporte.add(%unidade);
	clientRemoverUnidade(%unidade, %unidade.onde);
	%unidade.action("moveToLoc", %navio.getPosition());
	%unidade.onde = %navio;
	%unidade.setVisible(false);
	%navio.mark(%navio.myTransporte.getCount());
	%unidade.mount(%navio, 0, 0, 0, 0, 0, 0, 0); //monta o truta no navio
	
	clientAtualizarEstatisticas();
	if(%undo){
		$jogadorDaVez.movimentos += 1; //devolve um movimento pro truta que desembarcou sem querer
		atualizarMovimentosGui();
	} else {
		clientCriarUndo(%unidade, %areaDeOrigem, true, "embarque");
	}
	ghostSelect(%navio); //mostra pros outros clients que é o navio que está sendo manipulado pelo jogadorDaVez
	if(%incorporar){
		//clientIncorporarEffect(%navio);	
		clientFXtxt(%navio, "incorporar");
	}
	if(%carregar){
		//clientCarregarEffect(%navio);	
		if(%navio.liderNum == 1){
			clientFXtxt(%navio, "carregarPreto");
		} else {
			clientFXtxt(%navio, "carregarBranco");
		}
	}
	
	//clientAskConfirmarAtoFeito(%unidade.dono.id);
	clientPopServerComDot();
}







function verificarClassesDoNavio(%navio){
	%umaClasseNoNavio = true;
		
	if(%navio.myTransporte.getCount() > 1){
		for(%i = 0; %i < %navio.myTransporte.getCount(); %i++){
			%eval = "%unit" @ %i @ "Class = %navio.myTransporte.getObject(%i).class;";
			eval(%eval);
		}
		
		for(%i = 0; %i < %navio.myTransporte.getCount(); %i++){
			%iMaisUm = %i + 1;
			if(%iMaisUm >= %navio.myTransporte.getCount()){
				%iMaisUm = 0;	
			}
			%string = "if(";
			%string = %string @ "%unit" @ %i @ "Class !$= %unit" @ %iMaisUm @ "Class){";
			%string = %string @ "%umaClasseNoNavio = false;";
			%string = %string @ "}";
			eval(%string);
		}
	}
	
	return %umaClasseNoNavio;
}

function verificarCoresNoNavio(%navio){
	%umaCorNoNavio = true;
		
	if(%navio.myTransporte.getCount() > 1){
		for(%i = 0; %i < %navio.myTransporte.getCount(); %i++){
			%eval = "%unit" @ %i @ "Dono = %navio.myTransporte.getObject(%i).dono;";
			eval(%eval);
		}
		
		for(%i = 0; %i < %navio.myTransporte.getCount(); %i++){
			%iMaisUm = %i + 1;
			if(%iMaisUm >= %navio.myTransporte.getCount()){
				%iMaisUm = 0;	
			}
			%string = "if(";
			%string = %string @ "%unit" @ %i @ "Dono !$= %unit" @ %iMaisUm @ "Dono){";
			%string = %string @ "%umaCorNoNavio = false;";
			%string = %string @ "}";
			eval(%string);
		}
	}
	
	return %umaCorNoNavio;
}

function desembarcarLider_btnClick(){
	%eval = "%transporte = " @ $classD_onde @ "." @ $classD_pos @ ".quem;";
	eval(%eval);
	
	if(%transporte.gulok)
	{
		clientAskDesembarcar($classD_onde, $classD_pos, $classD_alvo, "zangao");
	}
	else
	{
		clientAskDesembarcar($classD_onde, $classD_pos, $classD_alvo, "lider");
	}
		
	clientDesligarQuemDesembarcaHud();
}

function desembarcarSoldado_btnClick(){
	%eval = "%transporte = " @ $classD_onde @ "." @ $classD_pos @ ".quem;";
	eval(%eval);
	
	if(%transporte.gulok)
	{
		clientAskDesembarcar($classD_onde, $classD_pos, $classD_alvo, "verme");
	}
	else
	{
		clientAskDesembarcar($classD_onde, $classD_pos, $classD_alvo, "soldado");
	}
		
	clientDesligarQuemDesembarcaHud();
}

function desembarcarMinhaCor_btnClick(){
	//echo("desembarcarMinhaCor_btn CLICK!");
	clientAskDesembarcar($classD_onde, $classD_pos, $classD_alvo, "", "minhaCor");
	clientDesligarQueCorDesembarcaHud();
}

function desembarcarVisitante_btnClick(){
	//echo("desembarcarVisitante_btn CLICK!");
	clientAskDesembarcar($classD_onde, $classD_pos, $classD_alvo, "", "visitante");
	clientDesligarQueCorDesembarcaHud();
}


function desembarcarCancelar(){
	clientDesligarQuemDesembarcaHud();
}

function clientAskDesembarcar(%areaDeOrigem, %posDeOrigem, %areaAlvo, %tipo, %corDeQuem, %deZangao){
	%clientAreaDeOrigem = %areaDeOrigem.getName();
	%clientAreaAlvo = %areaAlvo.getName();
	
	if(!$estouNoTutorial){
		clientPushServerComDot();
		commandToServer('desembarcar', %clientAreaDeOrigem, %posDeOrigem, %clientAreaAlvo, %tipo, %corDeQuem, false, "", %deZangao);
	} else {
		if(%clientAreaAlvo $= $tut_campanha.passo.alvo.getName() || $jogadorDaVez == $player2){
			clientCmdDesembarcarUnidade(%clientAreaDeOrigem, %posDeOrigem, %clientAreaAlvo, %tipo, %corDeQuem, false, "", %deZangao);
		}
	}
}

function clientCmdDesembarcarUnidade(%areaDeOrigem, %posDeOrigem, %areaAlvo, %tipo, %corDeQuem, %undo, %liderNum, %deZangao){
	//echo("clientCmdDesembarcarUnidade()...");
	//clientPopServerComDot();
		
	%eval = "%jogadorDaVez =" SPC $jogadorDaVez @ ";";
	eval(%eval);	
	%eval = "%areaDeOrigem =" SPC %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvo =" SPC %areaAlvo @ ";";
	eval(%eval);
	%navio = clientGetGameUnit(%areaDeOrigem, %posDeOrigem); //pega o navio
		
	%navio.unMark();
	$desembarcando = true;
	
	if(%tipo !$= ""){
		for(%i = 0; %i < %navio.myTransporte.getCount(); %i++){
			%tempUnit = %navio.myTransporte.getObject(%i);
			if(%tempUnit.class $= %tipo){
				if(%tempUnit.liderNum == 1){
					%unit = %tempUnit;
					%i = %navio.myTransporte.getCount(); //sai do loop
				} else {
					%unit = %tempUnit;
					//%i = %navio.myTransporte.getCount(); //sai do loop
				}
			}
		}
		if(!isObject(%unit)){
			echo("ERRO AO DESEMBARCAR: não encontrei a unidade da classe especificada (" @ %tipo @ "). Pegando a de índice(0)... feito.");
			%unit = %navio.myTransporte.getObject(0);
		}
	} else {
		if(%corDeQuem !$= ""){
			for(%i = 0; %i < %navio.myTransporte.getCount(); %i++){
				%tempUnit = %navio.myTransporte.getObject(%i);
				if(%corDeQuem $= "minhaCor"){
					if(%tempUnit.dono == %navio.dono){
						%unit = %tempUnit;
						%i = %navio.myTransporte.getCount(); //sai do loop
					}
				} else {
					if(%tempUnit.dono != %navio.dono){
						%unit = %tempUnit;
						%i = %navio.myTransporte.getCount(); //sai do loop
					}	
				}
			}
		} else {
			for(%i = 0; %i < %navio.myTransporte.getCount(); %i++){
				%tempUnit = %navio.myTransporte.getObject(%i);
				if(%tempUnit.liderNum == 1){
					%unit = %tempUnit;
					%i = %navio.myTransporte.getCount(); //sai do loop
				} else {
					%unit = %tempUnit;
				}
			}
		}
	}
		
	%unit.dismount();
	%unit.setPosition(%navio.getPosition());
	%unit.setVisible(true);
	clientDesembarcar(%unit, %areaAlvo, %undo, %deZangao);
	%navio.myTransporte.remove(%unit);
	%navio.reMark();
	$desembarcando = false;
	if(%undo){
		//$jogadorDaVez.movimentos += 1; //devolve um movimento pro truta que embarcou sem querer
		//atualizarMovimentosGui();
	} else {
		if(%deZangao){
			clientClearUndo();	
		} else {
			clientCriarUndo(%unit, %navio, false, "desembarque");
		}
	}
	atualizarBotoesDecompra();
	//clientAskConfirmarAtoFeito(%navio.dono.id);
	clientPopServerComDot();
}


function clientDesembarcar(%unit, %areaAlvo, %undo, %deZangao){
	//echo("clientDesembarcar()...");
	//estabelecendo o jogador da vez para a função:
	%eval = "%jogadorDaVez =" SPC $jogadorDaVez @ ";";
	eval(%eval);
	
	%areaAlvo.positionUnit(%unit);	
	%areaAlvo.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
	if(%undo != true && %deZangao != true){
		%jogadorDaVez.movimentos -= 1;
	} else {
		if(%deZangao $= "1"){
			//nada
		} else {
			clientShowUndoMark(); //mostra que um UnDo está sendo efetuado;	
		}
	}
	atualizarMovimentosGui();
	clientAtualizarEstatisticas();
}




/////////////////
//GULOK:

function clientAskRainhaCapturarBase(%areaDeOrigem, %posDeOrigem, %areaAlvo){
	%unit = clientGetGameUnit(%areaDeOrigem, %posDeOrigem);
	if($mySelf.movimentos > 0 || $jogadorDaVez == $aiPlayer){
		clientPushServerComDot();
		%clientAreaDeOrigem = %areaDeOrigem.getName();
		%clientAreaAlvo = %areaAlvo.getName();
		
		if($jogadorDaVez == $aiPlayer){
			if(%areaAlvo.pos0Quem.dono != $aiPlayer && isObject(%areaDeOrigem.pos0Quem)){
				commandToServer('rainhaCapturarBase', %clientAreaDeOrigem, %posDeOrigem, %clientAreaAlvo);
			}
		} else {
			commandToServer('rainhaCapturarBase', %clientAreaDeOrigem, %posDeOrigem, %clientAreaAlvo);
		}
	} else {
		//clientMsg seus movimentos acabaram
		clientMsg("movimentosAcabaram", 3000);
	}
}

function clientCmdRainhaCapturarBase(%areaDeOrigem, %posDeOrigem, %areaAlvo){
	echo("RAINHA CAPTURANDO BASE!");
	%unit = clientGetGameUnit(%areaDeOrigem, %posDeOrigem);
	ghostSelect(%unit);
		
	%areaAlvo.killBase();
	
	clientRemoverUnidade(%unit, %unit.onde);
	%areaAlvo.positionUnit(%unit);
	$jogadorDaVez.movimentos -= 1;
	
	%unit.parirVarios(); //bota ovos ou pare cefaloks, conforme o terreno da área em que está
	
	%areaAlvo.resolverMyStatus();
	
	atualizarMovimentosGui(); 
	clientLigarFlechas(%areaAlvo);
	atualizarBotoesDeCompra();
	clientClearUndo();
	clientPopServerComDot();
	schedule(300, 0, "atualizarBotoesDeCompra"); //verifica se pode fazer emboscada
}


////////////////////
//confirmação quando um zangão tenta capturar uma estrutura adversária:
function clientConfirmarCaptura(){
	clientPushServerComDot();
	clientPopCapturaQMsgBox();
	clientAskMovimentar($lastCapturaAreaDeOrigem, $lastCapturaPosDeOrigem, $lastCapturaAreaAlvo);
}

function clientPushCapturaQMsgBox(){
	canvas.pushDialog(capturaQGui);	
}

function clientPopCapturaQMsgBox(){
	canvas.popDialog(capturaQGui);	
}
	