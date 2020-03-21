// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverMovimentacao.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 30 de outubro de 2007 20:29
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================



//agora a função de movimentação:
function serverCmdMovimentar(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	
	if(%posDeOrigem $= "pos2" || %posDeOrigem $= "pos1" || %posDeOrigem $= "pos0"){
		%eval = "%unit = " @ %areaDeOrigemNoJogo @ "." @ %posDeOrigem @ "Quem;";
		eval(%eval);
	}else if(%posDeOrigem $= "pos3"){
		%pos3Count = %areaDeOrigemNoJogo.myPos3List.getCount();
		for(%i = 0; %i < %pos3Count; %i++){
			%tempUnit = %areaDeOrigemNoJogo.myPos3List.getObject(%i);
			if(%tempUnit.dono == %jogadorDaVez){
				//echo("Unidade encontrada");
				%unit = %areaDeOrigemNoJogo.myPos3List.getObject(%i);
				if(isObject(%unit.myTransporte)){
					if(%unit.myTransporte.getCount() > 0){
						%i = %pos3Count;
					}
				}
			}
		}
	}else if (%posDeOrigem $= "pos4"){
		if(%areaDeOrigemNoJogo.dono !$= "MISTA" && %areaDeOrigemNoJogo.dono !$= "COMPARTILHADA"){
			%unit = %areaDeOrigemNoJogo.myPos4List.getObject(0);
		} else {
			%pos4Count = %areaDeOrigemNoJogo.myPos4List.getCount();
			for(%i = 0; %i < %pos4Count; %i++){
				%tempUnit = %areaDeOrigemNoJogo.myPos4List.getObject(%i);
				if(%tempUnit.dono == %jogadorDaVez){
					//echo("Unidade encontrada");
					%unit = %areaDeOrigemNoJogo.myPos4List.getObject(%i);
					if(isObject(%unit.myTransporte)){
						if(%unit.myTransporte.getCount() > 0){
							%i = %pos4Count;
						}
					}
				}
			}
		}
	}
	
	
	
	if(%unit.class !$= "lider" && %unit.class !$= "zangao"){
		%comumSemJetPack = true;
	} else {
		if(%unit.JPagora < 1){
			%liderSemJetPack = true;
		} else {
			%liderComJetPack = true;
		}
	}
	
	if(%jogadorDaVez.movimentos > 0 || %liderComJetPack == true || %jogadorDaVez == %jogo.aiPlayer){
		if(%jogadorDaVez.client == %client || %jogadorDaVez == %jogo.aiPlayer){ //SEGURANÇA: server verifica se o client realmente tem movimentos e se é a vez dele
			if(%areaAlvo.myName !$= %areaDeOrigem.myName){ //não pode movimentar pra onde vc já está!
				for(%i = 0; %i < %jogo.playersAtivos; %i++){
					commandToClient(%jogo.simPlayers.getObject(%i).client, 'moverUnidade', %areaDeOrigem, %posDeOrigem, %areaAlvo); 
				}
				if(%jogo.observadorOn){
					commandToClient(%jogo.observador, 'moverUnidade', %areaDeOrigem, %posDeOrigem, %areaAlvo); 
				}
				
				serverRemoverUnidade(%jogo, %unit, %unit.onde);
				%areaAlvoNoJogo.positionUnit(%unit);
				if(%comumSemJetPack){
					serverCriarUndo(%jogo, %unit, %areaDeOrigemNoJogo, false);
					%jogadorDaVez.movimentos -= 1;
				} else {
					if(%liderSemJetPack){
						serverCriarUndo(%jogo, %unit, %areaDeOrigemNoJogo, false);
						%jogadorDaVez.movimentos -= 1;
					} else if(%liderComJetPack){
						serverCriarUndo(%jogo, %unit, %areaDeOrigemNoJogo, true);
						%unit.JPagora--;
					}
				}
				%areaAlvoNoJogo.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso, atualiza posReservaTxt
				
				
				//echo("verificando grupos e objetivos:");
				serverVerificarGruposX(%jogadorDaVez);
				//echo("1");
				serverVerificarObjetivos(%jogadorDaVez);
				//echo("grupos e objetivos verificados");
			}	
		}
	} else {
		commandToClient(%client, 'movimentosAcabaram');	
	}
}




function serverRemoverUnidade(%jogo, %unit, %area){
	//echo("serverRemoverUnidade, passo1");
	//echo("JOGO(" @ %jogo.num @ "), %unit.class = " @ %unit.class @ ", %unit.dono.persona.nome = " @ %unit.dono.persona.nome @ ", %unit.pos = " @ %unit.pos @ ", %area = " @ %area);
	%eval = "%areaNoJogo = %jogo." @ %area.myName @ ";";
	eval(%eval);
	//echo("serverRemoverUnidade, passo2");
	if(%unit.pos $= "pos0"){
		%areaNoJogo.pos0Flag = false;
		%areaNoJogo.pos0Quem = 0;
		%areaNoJogo.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
		
	} else if (%unit.pos $= "pos1"){
		%areaNoJogo.pos1Flag = "nada";
		serverReporUnidade(%jogo, %area, "pos1");
		//echo("serverRemoverUnidade, passo3");
	} else if (%unit.pos $= "pos2"){
		%areaNoJogo.pos2Flag = "nada";
		serverReporUnidade(%jogo, %area, "pos2");
		//echo("serverRemoverUnidade, passo4");
	} else {
		if (%unit.pos $= "pos3"){
			%areaNoJogo.myPos3List.remove(%unit);
			%areaNoJogo.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
			
			//echo("serverRemoverUnidade, passo5");
		} else if (%unit.pos $= "pos4"){
			%areaNoJogo.myPos4List.remove(%unit);
			%areaNoJogo.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
			
			//echo("serverRemoverUnidade, passo6");
		} else {
			echo("!!!!ALERTA!!!! %unit.pos não estava marcada !!!!ALERTA!!!!");	
		}
	} 
}



//as funções serverReporUnidade e serverRemoverUnidade não têm chamadas para os clients, são executadas apenas no server mesmo, pois têm correlatas no client; estas são chamadas pela função clientCmdMoverUnidade
function serverReporUnidade(%jogo, %area, %pos){
	%estoque = 0; //zera a variável de estoque
	%eval = "%areaNoJogo = %jogo." @ %area.myName @ ";";
	eval(%eval);
	//primeiro verifica se existem unidades no estoque:
	if(isObject(%areaNoJogo.myPos4List)){
		if (%areaNoJogo.myPos4List.getCount() >=  1){ //se existe alguém na pos4
			%unidade = %areaNoJogo.myPos4List.getObject(0); //os tanques têm preferência, %unidade pssa a ser a unidade repositora
			if(%unidade.class !$= "ovo"){
				%areaNoJogo.myPos4List.remove(%unidade); //remove a unidade repositora da sua posição anterior
				%estoque = 1; //marca que existe uma unidade no estoque
			} else if (%areaNoJogo.myPos3List.getCount() >=  1){ //se não houver ninguém na pos4, e houver alguém na pos3
				%unidade = %areaNoJogo.myPos3List.getObject(0); //pega a primeira unidade da pos3 e marca como a %unidade
				%areaNoJogo.myPos3List.remove(%unidade); //remove ela da pos3
				%estoque = 1; //marca que existe uma unidade no estoque
			}
		} else if (%areaNoJogo.myPos3List.getCount() >=  1){ //se não houver ninguém na pos4, e houver alguém na pos3
			%unidade = %areaNoJogo.myPos3List.getObject(0); //pega a primeira unidade da pos3 e marca como a %unidade
			%areaNoJogo.myPos3List.remove(%unidade); //remove ela da pos3
			%estoque = 1; //marca que existe uma unidade no estoque
		}
	} else {
		if (%areaNoJogo.myPos3List.getCount() >=  1){ //se não houver ninguém na pos4, e houver alguém na pos3
			%unidade = %areaNoJogo.myPos3List.getObject(0); //pega a primeira unidade da pos3 e marca como a %unidade
			%areaNoJogo.myPos3List.remove(%unidade); //remove ela da pos3
			%estoque = 1; //marca que existe uma unidade no estoque
		}
	}
	
	if (%estoque == 1){ //se existir alguma unidade no estoque, então
		switch$(%pos){ //verifica qual é posição a ser reposta
			case "pos1": //se for a pos1,
			%unidade.pos = "pos1"; //marca na unidade sua nova posição
			%areaNoJogo.pos1Flag = %unidade.class; //marca na área a Flag para a classe da unidade repositora
			%areaNoJogo.pos1Quem = %unidade; //marca na área a nova unidade que está naquela posição
			
      	  case "pos2": //se for a pos2, proceder da mesma maneira, só que mandando pra pos2
			%unidade.pos = "pos2";
			%areaNoJogo.pos2Flag = %unidade.class;
			%areaNoJogo.pos2Quem = %unidade;
		}
	} else { //se não houver ninguém no estoque, então
		if (%areaNoJogo.pos1Flag $= "nada" && %areaNoJogo.pos2Flag !$= "nada"){
			%areaNoJogo.pos1Quem = %areaNoJogo.pos2Quem;
			%areaNoJogo.pos1Flag = %areaNoJogo.pos1Quem.class;
			%areaNoJogo.pos1Quem.pos = "pos1";
			%areaNoJogo.pos2Quem = "nada";
			%areaNoJogo.pos2Flag = "nada";
		}
	}
	%areaNoJogo.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
}




//função unificada para o embarque de qq posição:
function serverCmdEmbarcar(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo, %posDoNavio, %undo, %incorporar, %carregar){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem.myName @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo.myName @ ";";
	eval(%eval);


	
	if(%posDeOrigem $= "pos3"){
		%pos3Count = %areaDeOrigemNoJogo.myPos3List.getCount();
		for(%i = 0; %i < %pos3Count; %i++){
			%tempUnit = %areaDeOrigemNoJogo.myPos3List.getObject(%i);
			if(%tempUnit.dono == %jogadorDaVez){
				//echo("Unidade visitante encontrada");
				%unidade = %areaDeOrigemNoJogo.myPos3List.getObject(%i);
			}
		}	
	} else {
		%eval = "%unidade = %areaDeOrigemNoJogo." @ %posDeOrigem @ "Quem;";
		eval(%eval);
	}
	
	if(%posDoNavio $= "pos3"){
		%pos3Count = %areaAlvoNoJogo.myPos3List.getCount();
		for(%i = 0; %i < %pos3Count; %i++){
			%tempNavio = %areaAlvoNoJogo.myPos3List.getObject(%i);
			if(%tempNavio.dono == %jogadorDaVez){
				//echo("Navio encontrado");
				%navio = %areaAlvoNoJogo.myPos3List.getObject(%i);
				if(isObject(%navio.myTransporte)){
					if(%navio.myTransporte.getCount() > 0){
						%i = %pos3Count;
					}	
				}
			}
		}
	} else {
		%eval = "%navio = %areaAlvoNoJogo." @ %posDoNavio @ "Quem;";
		eval(%eval);
	}
		
	if(%jogadorDaVez.client == %client){ //SEGURANÇA: só se for o jogador da vez.
		if (%navio.transporteFlag == false){
			%navio.myTransporte = new SimSet();
			%navio.transporteFlag = true;
		}	
		
		if(%jogo.semPesquisas){
			%maxCarga = 2;
		} else {
			%maxCarga = 2 + %navio.dono.persona.aca_v_2;
		}
		if(%navio.class $= "cefalok"){
			%maxCarga = 5;	
		} else if(%navio.class $= "rainha"){
			if(%navio.dono.persona.aca_v_3 == 1){
				%maxCarga = 1;	
			} else if(%navio.dono.persona.aca_v_3 == 2){
				%maxCarga = 2;	
			} else if(%navio.dono.persona.aca_v_3 == 3){
				%maxCarga = 3;	
			} else {
				%maxCarga = 0;	
			}
		} else if(%navio.class $= "zangao"){
			%eval = "%myPesq = %navio.dono.persona.aca_ldr_" @ %navio.liderNum @ "_h1;";
			eval(%eval);
			%maxCarga = %myPesq;
		}
		if(%navio.myTransporte.getCount() < %maxCarga){ 
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'embarcarUnidade', %areaDeOrigem, %posDeOrigem, %areaAlvo, %posDoNavio, %undo, %incorporar, %carregar); 
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'embarcarUnidade', %areaDeOrigem, %posDeOrigem, %areaAlvo, %posDoNavio, %undo, %incorporar, %carregar); 
			}
			%navio.myTransporte.add(%unidade);
			serverRemoverUnidade(%jogo, %unidade, %unidade.onde);
			%unidade.onde = %navio;
		} else {
			//retornar para o client: ("Não há espaço neste navio. Embarque NÃO efetuado.");
			if(%incorporar){
				commandToClient(%client, 'clientMsgNaoHaEspacoRainha');
			} else {
				commandToClient(%client, 'clientMsgNaoHaEspaco');
			}
		}
		
		if(%undo){
			%jogadorDaVez.movimentos += 1; //devolve um movimento pro truta que desembarcou sem querer
		} else {
			serverCriarUndo(%jogo, %unidade, %areaDeOrigemNoJogo, true, "embarque");
		}
	}
	
}
///////////////



//////////

function serverCmdDesembarcar(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo, %tipo, %corDeQuem, %unDo, %liderNum, %deZangao){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	
	echo("serverCmdDesembarcar::deZangao == " @ %deZangao @ "; Undo == " @ %undo @ ";liderNum == " @ %lidernum);
	
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem.myName @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo.myName @ ";";
	eval(%eval);
	if(%posDeOrigem $= "pos3"){
		%pos3Count = %areaDeOrigemNoJogo.myPos3List.getCount();
		for(%i = 0; %i < %pos3Count; %i++){
			%tempNavio = %areaDeOrigemNoJogo.myPos3List.getObject(%i);
			if(%tempNavio.dono == %jogadorDaVez){
				//echo("Unidade encontrada");
				%navio = %areaDeOrigemNoJogo.myPos3List.getObject(%i);
				if(isObject(%navio.myTransporte)){
					if(%navio.myTransporte.getCount() > 0){
						%i = %pos3Count;
					}
				}
			}
		}
	} else {
		%eval = "%navio = %areaDeOrigemNoJogo." @ %posDeOrigem @ "Quem;";
		eval(%eval);
	}
	
		
	if(%jogadorDaVez.client == %client){ //SEGURANÇA
		if(%jogadorDaVez.movimentos > 0 || %undo == true || %deZangao == true){
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'desembarcarUnidade', %areaDeOrigem, %posDeOrigem, %areaAlvo, %tipo, %corDeQuem, %undo, "", %deZangao); 
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'desembarcarUnidade', %areaDeOrigem, %posDeOrigem, %areaAlvo, %tipo, %corDeQuem, %undo, "", %deZangao); 
			}
				
			%jogo.desembarcando = true;
			
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
					//echo("DESEMBARCAR: classe  e cor da unidade não especificadas. Pegando a de índice(0)... feito.");
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
			
			serverDesembarcar(%jogo, %unit, %areaAlvo, %deZangao);
			%navio.myTransporte.remove(%unit);
			%jogo.desembarcando = false;
			if(%undo){
				%jogadorDaVez.movimentos += 1;	
			} else {
				if(!%deZangao){
					serverCriarUndo(%jogo, %unit, %navio, false, "desembarque");
				}
			}
		} else {
			commandToClient(%client, 'movimentosAcabaram');	
		}
	}
}



function serverDesembarcar(%jogo, %unit, %areaAlvo, %deZangao){
	%jogadorDaVez = %jogo.jogadorDaVez;
	%eval = "%areaAlvoNoJogo = %jogo." SPC %areaAlvo.myName @ ";";
	eval(%eval);
	
	/////
	%areaAlvoNoJogo.positionUnit(%unit);
	////
	if(%deZangao != true){
		%jogadorDaVez.movimentos -= 1;
	}
	%areaAlvoNoJogo.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
	
	serverVerificarGruposX(%jogadorDaVez);
	serverVerificarObjetivos(%jogadorDaVez);
}

///////////
//GULOK:

function serverCmdRainhaCapturarBase(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	%eval = "%unit = " @ %areaDeOrigemNoJogo @ ".pos0Quem;";
	eval(%eval);
		
	if(%jogadorDaVez.movimentos > 0){
		if(%jogadorDaVez.client == %client || %jogadorDaVez == %jogo.aiPlayer){ //SEGURANÇA: server verifica se o client realmente tem movimentos e se é a vez dele
			//primeior verifica se o invasor pode pilhar:
			if(%jogadorDaVez.persona.aca_i_2 > 0){
				if(%jogadorDaVez.minhasPilhagens < %jogadorDaVez.persona.aca_i_2){
					if(!%jogo.semPesquisas)
						serverVerificarPilhagem(%areaAlvoNoJogo, %jogadorDaVez);
				} else {
					echo("2");	
				}
			} else {
				echo("1");	
			}
		
			%areaAlvoNoJogo.pos0Quem.dono.mySimBases.remove(%areaAlvoNoJogo.pos0Quem); //remove a base do dono antigo
			if(%areaAlvoNoJogo.pos0Quem.dono.mySimAreas.isMember(%areaAlvoNoJogo)){ 
				%areaAlvoNoJogo.pos0Quem.dono.mySimAreas.remove(%areaAlvoNoJogo); //remove a área do dono antigo
				if(%areaAlvoNoJogo.pos0Quem.dono.mySimAreas.getCount() < 1){
					serverPlayerKill(%areaAlvoNoJogo.pos0Quem.dono, %areaDeOrigemNoJogo.pos0Quem.dono);
				}
			}
			%areaAlvoNoJogo.pos0Quem.safeDelete(); //deleta a base antiga
			%areaAlvoNoJogo.pos0Quem = 0; //marca na área que não tem ninguém na pos0
			%areaAlvoNoJogo.pos0Flag = false; //marca na área que não tem ninguém na pos0
			
			serverRemoverUnidade(%jogo, %unit, %unit.onde); //remove a rainha da área anterior
			%areaAlvoNoJogo.positionUnit(%unit); //coloca a Rainha na nova área
			%jogadorDaVez.movimentos -= 1; //retira um movimento do jogadorDaVez
			if(%areaAlvoNoJogo.terreno $= "terra"){
				for(%i = 0; %i < 3; %i++){
					%unit.spawnUnit("ovo");
					%jogadorDaVez.imperiais += 3;
				}
			} else {
				for(%i = 0; %i < 2; %i++){
					%unit.spawnUnit("cefalok");
					%jogadorDaVez.imperiais += 4;
				}
			}
			
			
			//verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso, atualiza posReservaTxt:
			%areaAlvoNoJogo.resolverMyStatus();
			
			
			
			
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'rainhaCapturarBase', %areaDeOrigem, %posDeOrigem, %areaAlvo); 
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'rainhaCapturarBase', %areaDeOrigem, %posDeOrigem, %areaAlvo); 
			}
							
			serverVerificarGruposX(%jogadorDaVez);
			serverVerificarObjetivos(%jogadorDaVez);
		}
	} else {
		commandToClient(%client, 'movimentosAcabaram');	
	}

}


