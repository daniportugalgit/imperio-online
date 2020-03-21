// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverProducao.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  domingo, 3 de fevereiro de 2008 22:12
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
///////////////////
//função que unifica as funções de criarBase no mar e na terra:
function serverCmdCriarBase(%client, %onde){
	%player = %client.player;
	if(%player.persona.gulok){
		serverCmdCriarRainha(%client, %onde);
	} else {
		serverCmdCriarBaseHumanos(%client, %onde);
	}
	%player.basesIniciais++;
	cancel($forceMaritimaSchedule); //o client respondeu, não precisa assumir o controle
}

function serverCmdCriarBaseHumanos(%client, %onde){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	
	%eval = "%ondeNoJogo = %jogo." @ %onde @ ";";
	eval(%eval);
	
	%newBase = base_BP.createCopy();
	%newBase.onde = %ondeNoJogo;
	%newBase.isMoveable = 0;
	%newBase.dono = %jogadorDaVez;
	%ondeNoJogo.pos0Flag = true;
	%ondeNoJogo.pos0Quem = %newBase;
	%ondeNoJogo.dono = %jogadorDaVez;
	%jogadorDaVez.mySimAreas.add(%ondeNoJogo);
	%jogadorDaVez.mySimBases.add(%newBase);
	
	//verifica se é um jogo sem pesquisas:
	if(%jogo.semPesquisas){
		%lider = 0;
		%L1Escudo = 0;
		%L2Escudo = 0;
		%L1JetPack = 0;
		%L2JetPack = 0;
		%L1Snipers = 0;
		%L2Snipers = 0;
		%L1Moral = 0;
		%L2Moral = 0;
		%forD = 0;
	} else {
		%lider = %newBase.dono.persona.aca_a_1;
		if(%lider == 3){
			%lider = 2;	
		}
		
		%L1Escudo = %newBase.dono.persona.aca_ldr_1_h1;
		%L2Escudo = %newBase.dono.persona.aca_ldr_2_h1;
		%L1JetPack = %newBase.dono.persona.aca_ldr_1_h2;
		%L2JetPack = %newBase.dono.persona.aca_ldr_2_h2;
		if(%newBase.dono.persona.aca_ldr_1_h3 > 0){
			%L1Snipers = %newBase.dono.persona.aca_ldr_1_h3 + 1;
		} else {
			%L1Snipers = 0;
		}
		if(%newBase.dono.persona.aca_ldr_2_h3 > 0){
			%L2Snipers = %newBase.dono.persona.aca_ldr_2_h3 + 1;
		} else {
			%L2Snipers = 0;
		}
		%L1Moral = %newBase.dono.persona.aca_ldr_1_h4;
		%L2Moral = %newBase.dono.persona.aca_ldr_2_h4;
		%forD = %newBase.dono.persona.aca_d_1;
	}
						
						
	if (%newBase.onde.terreno $= "terra"){
		if(%lider == 0 || %lider $= ""){
			%newBase.spawnUnit("soldado");
			%newBase.spawnUnit("soldado");
			%jogadorDaVez.imperiais += 2;
		} else {
			for(%i = 0; %i < %lider; %i++){
				%newLider = %newBase.spawnUnit("lider");
				%newLider.liderNum = %i+1;
				
				if(%i == 0){
					if(%L1Escudo > 0){
						%newLider.criarMeuEscudo();
						%newLider.myEscudos = %L1Escudo;
					}
					if(%L1JetPack > 1){
						%newLider.JPBP = (%L1JetPack - 1);	
						%newLider.JPagora = (%L1JetPack - 1);	
						%newLider.anfibio = true;
					} else {
						%newLider.JPBP = %L1JetPack; //JetPack BluePrint
						%newLider.JPagora = %L1JetPack;
					}
					%newLider.mySnipers = %L1Snipers;
					%newLider.myMoral = %L1Moral;
				} else {
					if(%L2Escudo > 0){
						%newLider.criarMeuEscudo();
						%newLider.myEscudos = %L2Escudo;
					}
					if(%L2JetPack > 1){
						%newLider.JPBP = (%L2JetPack - 1);	
						%newLider.JPagora = (%L2JetPack - 1);	
						%newLider.anfibio = true;
					} else {
						%newLider.JPBP = %L2JetPack; //JetPack BluePrint
						%newLider.JPagora = %L2JetPack;
					}
					%newLider.mySnipers = %L2Snipers;
					%newLider.myMoral = %L2Moral;
				}
			}
			%jogo.verificarMoral(%newLider.dono);
		}
		
		//agora completa com um soldado caso haja hapenas um líder:
		if(%lider == 1){
			%newBase.spawnUnit("soldado");
			%jogadorDaVez.imperiais++;
		}		
		
		//Força Diplomática:
		if(%newBase.dono.persona.myDiplomata > 69){
			if(%forD == 1){
				%newBase.spawnUnit("soldado");
				%newBase.spawnUnit("soldado");
				%jogadorDaVez.imperiais += 2;
			} else if(%forD == 2){
				%newBase.spawnUnit("tanque");	
				%newBase.spawnUnit("tanque");	
				%jogadorDaVez.imperiais += 4;
			} else if(%forD == 3){
				%newBase.spawnUnit("tanque");	
				%newBase.spawnUnit("tanque");
				%jogadorDaVez.imperiais += 4;
			}
		} else {
			%forD = 0;	
		}
	} else {
		%newBase.spawnUnit("navio");
		%jogadorDaVez.imperiais += 3;
		
		if(%newBase.dono.persona.myDiplomata > 69){
			if(%forD == 3){
				%newBase.spawnUnit("navio");
				%jogadorDaVez.imperiais += 3;
			}	
		} else {
			%forD = 0;	
		}
	}
	
		
	//
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		commandToClient(%jogo.simPlayers.getObject(%i).client, 'criarBase', %onde, %jogadorDaVez.id, %lider, %L1Escudo, %L2Escudo, %L1JetPack, %L2JetPack, %forD, %L1Snipers, %L2Snipers, %L1Moral, %L2Moral);
	}
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'criarBase', %onde, %jogadorDaVez.id, %lider, %L1Escudo, %L2Escudo, %L1JetPack, %L2JetPack, %forD, %L1Snipers, %L2Snipers, %L1Moral, %L2Moral);
	}
}

function serverCmdConstruirBase(%client, %onde){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;

	%eval = "%ondeNoJogo = %jogo." @ %onde @ ";";
	eval(%eval);
	
	if(%jogadorDaVez.client != %client)
		return;
	
	if(!%jogadorDaVez.engenheiro && %jogadorDaVez.imperiais < 10)
		return;
	
	if(%jogadorDaVez.imperiais < 7)
		return;
	
	if(%jogadorDaVez.engenheiro){
		%jogadorDaVez.imperiais -= 7;	
	} else {
		%jogadorDaVez.imperiais -= 10;
	}
	
	%newBase = base_BP.createCopy();
	%newBase.onde = %ondeNoJogo;
	%newBase.isMoveable = 0;
	%newBase.dono = %jogadorDaVez;
	%ondeNoJogo.pos0Flag = true;
	%ondeNoJogo.pos0Quem = %newBase;
	%jogadorDaVez.mySimBases.add(%newBase);
		
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		commandToClient(%jogo.simPlayers.getObject(%i).client, 'construirBase', %onde, %jogadorDaVez.id);
	}
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'construirBase', %onde, %jogadorDaVez.id);
	}
	
	//verificar se elguém pode espionar um oculto:
	if(!%jogo.semPesquisas){
		if(%jogadorDaVez.oculto){
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				%persona = %jogo.simPlayers.getObject(%i).persona;
				if(%persona.especie $= "gulok"){
					if((%persona.aca_i_1 == 2 && %jogadorDaVez.persona.aca_i_1 < 3) || %persona.aca_i_1 == 3){
						if(%persona.nome !$= %jogadorDaVez.persona.nome){
							commandToClient(%persona.client, 'i_intelOculto', %jogadorDaVez.persona.nome, "construiu", "BASE");	
						}
					}
				}
			}
		}
	}
	
	serverClearUndo(%jogadorDaVez);
}

function serverCmdSpawnUnit(%client, %onde, %tipo){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	%eval = "%ondeNoJogo = %jogo." @ %onde @ ";";
	eval(%eval);
	
	if(isObject(%ondeNoJogo.pos0Quem)){
		if(%ondeNoJogo.pos0Quem.cortejada || %ondeNoJogo.pos0Quem.matriarca){
			%custoOvo = 2;	
		} else {
			%custoOvo = 3;
		}
	}
	
	if(%jogadorDaVez.marinheiro){
		%custoNavio = 2;	
	} else {
		%custoNavio = 3;
	}
	
	
	%podeComprar = false;
	if(%jogadorDaVez.client == %client || %jogadorDaVez == %jogo.aiPlayer){ //SEGURANÇA 1 -> se for a vez do client
		if(%tipo $= "soldado" && %jogadorDaVez.imperiais > 0){
			%podeComprar = true;
		} else if (%tipo $= "tanque" && %jogadorDaVez.imperiais > 1){
			%podeComprar = true;
		} else if (%tipo $= "navio" && %jogadorDaVez.imperiais > 2){
			%podeComprar = true;
		} else if (%tipo $= "navio" && %jogadorDaVez.imperiais >= %custoNavio){
			%podeComprar = true;
		} else if (%tipo $= "ovo" && %jogadorDaVez.imperiais >= %custoOvo){
			%podeComprar = true;
		} else if (%tipo $= "cefalok" && %jogadorDaVez.imperiais > 3){
			%podeComprar = true;
		} else if (%tipo $= "verme"){
			%podeComprar = true;
		}
		
		if(%podeComprar == true){
			%base = %ondeNoJogo.pos0Quem;
			%base.spawnUnit(%tipo);
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'spawnUnit', %onde, %tipo);
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'spawnUnit', %onde, %tipo);
			}
			serverClearUndo(%jogadorDaVez);
		} else {
			echo("ERRO2: sem imperiais para efetuar compra de unidades!");
			commandToClient(%client, 'clientMsg', "imperiaisInsuficientes", true);
		}
	} else {
		//não constroi nada, mas devolve o controle pro usuário lah no client:
		commandToClient(%client, 'popServerComDot');
	}
}


/////////////
//Refinarias:
function serverCmdConstruirRefinaria(%client, %onde){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;

	%eval = "%ondeNoJogo = %jogo." @ %onde @ ";";
	eval(%eval);
	
	if(%jogadorDaVez.persona.aca_v_4 > 2){
		%custoRefinaria = 4;	
	} else {
		%custoRefinaria = 5;	
	}
	
	if(%jogadorDaVez.imperiais >= %custoRefinaria && %jogadorDaVez.client == %client){
		if(%jogadorDaVez.mySimRefinarias.getCount() < %jogadorDaVez.persona.aca_v_4 && %jogadorDaVez.mySimRefinarias.getCount() < 3){
			%jogadorDaVez.imperiais -= %custoRefinaria;
			%newBase = refinaria_BP.createCopy();
			%newBase.refinaria = true;
			%newBase.onde = %ondeNoJogo;
			%newBase.isMoveable = 0;
			%newBase.dono = %jogadorDaVez;
			%ondeNoJogo.pos0Flag = true;
			%ondeNoJogo.pos0Quem = %newBase;
			//%ondeNoJogo.dono = %jogadorDaVez;
			%jogadorDaVez.mySimBases.add(%newBase);
			%jogadorDaVez.mySimRefinarias.add(%newBase);
				
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'construirRefinaria', %onde, %jogadorDaVez.id, %custoRefinaria);
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'construirRefinaria', %onde, %jogadorDaVez.id, %custoRefinaria);
			}
			
			//verificar se elguém pode espionar um oculto:
			if(!%jogo.semPesquisas){
				if(%jogadorDaVez.oculto){
					for(%i = 0; %i < %jogo.playersAtivos; %i++){
						%persona = %jogo.simPlayers.getObject(%i).persona;
						if(%persona.especie $= "gulok"){
							if((%persona.aca_i_1 == 2 && %jogadorDaVez.persona.aca_i_1 < 3) || %persona.aca_i_1 == 3){
								if(%persona.nome !$= %jogadorDaVez.persona.nome){
									commandToClient(%persona.client, 'i_intelOculto', %jogadorDaVez.persona.nome, "construiu", "REFINARIA");	
								}
							}
						}
					}
				}
			}
			
			serverClearUndo(%jogadorDaVez);
		} else {
			echo("Já possui duas refinarias, comando ignorado.");	
		}
	} else {
		echo("Existe um CHEATER dizendo que tem mais Imperiais do que realmente tem, ou jogando fora da sua vez:" SPC %jogadorDaVez.id);
		echo("JOGO: " @ %jogo.num @ ", PERSONA: " @ %client.persona.nome);
	}
}



//====================================
///GULOK:
function serverCmdCriarRainha(%client, %onde, %semFilhos, %semLideres, %AI){
	%jogo = %client.player.jogo;
	if(!%AI){
		%jogadorDaVez = %jogo.jogadorDaVez;
	} else {
		%jogadorDaVez = %jogo.aiPlayer;	
	}
	
	%eval = "%ondeNoJogo = %jogo." @ %onde @ ";";
	eval(%eval);
	
	%newRainha = rainha_BP.createCopy();
	%newRainha.gulok = true;
	%newRainha.onde = %ondeNoJogo;
	%newRainha.isMoveable = 1;
	%newRainha.dono = %jogadorDaVez;
	%ondeNoJogo.pos0Flag = true;
	%ondeNoJogo.pos0Quem = %newRainha;
	%ondeNoJogo.dono = %jogadorDaVez;
	%jogadorDaVez.mySimAreas.add(%ondeNoJogo);
	%jogadorDaVez.mySimBases.add(%newRainha);
	%newRainha.pos = "pos0";
	
	if(!%semFilhos){
		%lider = 2;
			
		//verifica se é um jogo sem pesquisas:
		if(%jogo.semPesquisas){
			%L1Carregar = 0;
			%L2Carregar = 0;
			%L1Asas = 0;
			%L2Asas = 0;
			%L1Devorar = 0;
			%L2Devorar = 0;
			%L1Cortejar = 0;
			%L2Cortejar = 0;
			%forD = 0;
		} else {
			%L1Carregar = %newRainha.dono.persona.aca_ldr_1_h1;
			%L2Carregar = %newRainha.dono.persona.aca_ldr_2_h1;
			%L1Asas = %newRainha.dono.persona.aca_ldr_1_h2;
			%L2Asas = %newRainha.dono.persona.aca_ldr_2_h2;
			%L1Canibalizar = %newRainha.dono.persona.aca_ldr_1_h3;
			%L2Metamorfose = %newRainha.dono.persona.aca_ldr_2_h3;
			%L1DevorarRainhas = %newRainha.dono.persona.aca_ldr_1_h4;
			%L2Cortejar = %newRainha.dono.persona.aca_ldr_2_h4;
			%forD = %newRainha.dono.persona.aca_d_1;
		}
							
							
		if (%newRainha.onde.terreno $= "terra"){
			%newRainha.spawnUnit("ovo");
			%newRainha.spawnUnit("ovo");
			%jogadorDaVez.imperiais += 6;
			
			if(!%semLideres){
				for(%i = 0; %i < %lider; %i++){
					%newLider = %newRainha.spawnUnit("zangao");
					%newLider.liderNum = %i+1;
					
					if(%i == 0){
						%newLider.myCarregar = %L1Carregar;
						if(%L1Asas > 1){
							%newLider.JPBP = (%L1Asas - 1);	
							%newLider.JPagora = (%L1Asas - 1);	
							%newLider.anfibio = true;
						} else {
							%newLider.JPBP = %L1Asas; //JetPack BluePrint
							%newLider.JPagora = %L1Asas;
						}									
						%newLider.myCanibalizar = %L1Canibalizar;
						%newLider.myDevorarRainhas = %L1DevorarRainhas;
					} else {
						%newLider.myCarregar = %L2Carregar;
						if(%L2Asas > 1){
							%newLider.JPBP = (%L2Asas - 1);	
							%newLider.JPagora = (%L2Asas - 1);	
							%newLider.anfibio = true;
						} else {
							%newLider.JPBP = %L2Asas; //JetPack BluePrint
							%newLider.JPagora = %L2Asas;
						}	
						%newLider.myMetamorfose = %L2Metamorfose;
						%newLider.myCortejar = %L2Cortejar;
					}
				}
			}
					
			//Força Diplomática:
			if(%newRainha.dono.persona.myDiplomata > 69){
				if(%forD > 0){
					%newRainha.spawnUnit("ovo");
					%jogadorDaVez.imperiais += 3;
				} else {
					%forD = 0;		
				}
				if(%forD > 1){
					%newRainha.spawnUnit("ovo");
					%jogadorDaVez.imperiais += 3;
				}
				if(%forD > 2){
					%newRainha.spawnUnit("ovo");
					%jogadorDaVez.imperiais += 3;
				}	
			} else {
				%forD = 0;	
			}
		} else {
			%newRainha.spawnUnit("cefalok");
			%jogadorDaVez.imperiais += 4;
			if(%AI){
				%newRainha.spawnUnit("cefalok");
				%jogadorDaVez.imperiais += 4;
			}
		}
	}
	
		
	//
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		commandToClient(%jogo.simPlayers.getObject(%i).client, 'criarRainha', %onde, %jogadorDaVez.id, %lider, %L1Asas, %L2Asas, %L1Canibalizar, %L2Metamorfose, %forD, %L1Carregar, %L2Carregar, %L1DevorarRainhas, %L2Cortejar, %semFilhos, %semLideres, %AI);
	}
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'criarRainha', %onde, %jogadorDaVez.id, %lider, %L1Asas, %L2Asas, %L1Canibalizar, %L2Metamorfose, %forD, %L1Carregar, %L2Carregar, %L1DevorarRainhas, %L2Cortejar, %semFilhos, %semLideres, %AI);
	}
}

//
function serverCmdEvoluirEmRainha(%client, %ondeClient, %posDeOrigem, %especial, %metamorfose){
	%jogo = %client.player.jogo;
	%player = %client.player;
	
	//se for uma ação de AI dar um override na marcação de player, pra garantir os donos corretos das peças
	if(%jogo.jogadorDaVez == %jogo.aiPlayer){
		%player = %jogo.aiPlayer;	
	}
	
	%eval = "%ondeNoJogo = %jogo." @ %ondeClient @ ";";
	eval(%eval);
	
	%newRainha = rainha_BP.createCopy();
	%newRainha.gulok = true;
	%newRainha.onde = %ondeNoJogo;
	%newRainha.isMoveable = 1;
	%newRainha.dono = %player;
	%player.mySimBases.add(%newRainha);
			
	%unit = %jogo.findMyUnit(%ondeClient, %posDeOrigem, %client);
	
	%evolucaoAvancada = false;
	
	%custo = 10;
	
	if(%player.persona.aca_av_4 == 3){
		%custo = 9;
		%evolucaoAvancada = true;
	}
	if(%especial == true){
		%custo = 0;
	}
	if(%metamorfose > 0){
		%custo = 10 - (%metamorfose * 3);	
	}
	
	%player.imperiais -= %custo;
		
	//agora manda a rainha pra pos0:
	%ondeNoJogo.positionUnit(%newRainha);
	
	//e agora remove a peça inicial:
	serverRemoverUnidade(%jogo, %unit, %unit.onde);
	%unit.dono.mySimUnits.remove(%unit);
	%unit.safeDelete();
	
	//envia o comando pros clients:
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		commandToClient(%jogo.simPlayers.getObject(%i).client, 'evoluirEmRainha', %ondeClient, %posDeOrigem, %especial, %metamorfose, %evolucaoAvancada);
	}
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'evoluirEmRainha', %ondeClient, %posDeOrigem, %especial, %metamorfose, %evolucaoAvancada);
	}
	
	//verifica objetivos global, pode ter acontecido por uma captura de base:
	%jogo.verificarGruposGlobal();
	%jogo.verificarObjetivosGlobal();
}
///////////
//eclosão dos ovos:
function jogo::verificarOvos(%this){
	%jogadorDaVez = %this.jogadorDaVez;
	%ovosCount = %jogadorDaVez.mySimOvos.getCount();
	for(%i = 0; %i < %jogadorDaVez.mySimOvos.getCount(); %i++){
		%ovos[%i] = %jogadorDaVez.mySimOvos.getObject(%i);
	}
	for(%i = 0; %i < %ovosCount; %i++){
		%ovo = %ovos[%i];
		%ovo.eclodir();
	}
}


////////////////
//Grande Matriarca:
function serverCmdCriarGrandeMatriarca(%client, %onde){
	%jogo = %client.player.jogo;
	%playerGulok = %jogo.aiPlayer;
	
	%eval = "%ondeNoJogo = %jogo." @ %onde @ ";";
	eval(%eval);
	
	%newMatriarca = rainha_BP.createCopy();
	%newMatriarca.gulok = true;
	%newMatriarca.onde = %ondeNoJogo;
	%newMatriarca.isMoveable = 0;
	%newMatriarca.dono = %playerGulok;
	%ondeNoJogo.pos0Flag = true;
	%ondeNoJogo.pos0Quem = %newMatriarca;
	%ondeNoJogo.dono = %playerGulok;
	%playerGulok.mySimAreas.add(%ondeNoJogo);
	%playerGulok.mySimBases.add(%newMatriarca);
	%newMatriarca.matriarca = true;
	%newMatriarca.grandeMatriarca = true;
	
	
	if(!isObject(%playerGulok.mySimMatriarcas)){
		%playerGulok.mySimMatriarcas = new SimSet();
	} else {
		%playerGulok.mySimMatriarcas.clear();
	}
	%playerGulok.mySimMatriarcas.add(%newMatriarca);
	%newMatriarca.pos = "pos0";
			
	//
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		commandToClient(%jogo.simPlayers.getObject(%i).client, 'criarGrandeMatriarca', %onde, %playerGulok.id);
	}
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'criarGrandeMatriarca', %onde, %playerGulok.id);
	}
}