// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientProducao.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 25 de janeiro de 2008 16:50
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function clientAskCriarBase(%onde){
	if(!$estouNoTutorial){
		%ondeClient = %onde.getName();
		commandToServer('criarBase', %ondeClient);	
	} else {
		clientCmdCriarBase(%onde, "player1");
	}
}

function clientCmdCriarBase(%onde, %praQuem, %lider, %L1Escudo, %L2Escudo, %L1JetPack, %L2JetPack, %forD, %L1Snipers, %L2Snipers, %L1Moral, %L2Moral){
	%eval = "%jogadorDaVez =" SPC "$" @ %praQuem @ ";";
	eval(%eval);
	%eval = "%ondeClient =" SPC %onde @";";
	eval(%eval);
	
	//if($novasPecas){
		//%newBase = base_BP_neo.createCopy();
	//} else {
		%newBase = base_BP.createCopy();
	//}
	if(%jogadorDaVez.oculto){
		if(%jogadorDaVez == $mySelf){
			%newBase.setBlendColor(1,1,1,0.6);
		} else {
			%newBase.setBlendColor(1,1,1,0);	
		}
		%newBase.oculta = true;
	}
	%newBase.setPosition(%onde.pos0);
	%newBase.isSelectable = 1;
	%newBase.onde = %ondeClient;
	%newBase.isMoveable = 0;
	%newBase.dono = %jogadorDaVez;
	%ondeClient.pos0Flag = true;
	%ondeClient.pos0Quem = %newBase;
	%ondeClient.dono = %jogadorDaVez;
	%jogadorDaVez.mySimAreas.add(%ondeClient);
	%newBase.setMyImage();
	
	
	if (%newBase.onde.terreno $= "terra"){
		if(%lider == 0 || %lider $= ""){
			%newBase.spawnUnit("soldado");
			%newBase.spawnUnit("soldado");
			%jogadorDaVez.imperiais += 2;
		} else {
			for(%i = 0; %i < %lider; %i++){
				%newBase.spawnUnit("lider");
				if(%i == 0){
					if(%L1Escudo > 0){
						%newBase.onde.pos1Quem.criarMeuEscudo();
						%newBase.onde.pos1Quem.myEscudos = %L1Escudo;
					}
					%newBase.onde.pos1Quem.liderNum = 1; //Sou o Líder 1
					if(%L1JetPack > 1){
						%newBase.onde.pos1Quem.JPBP = (%L1JetPack - 1);	
						%newBase.onde.pos1Quem.JPagora = (%L1JetPack - 1);	
						%newBase.onde.pos1Quem.anfibio = true;
					} else {
						%newBase.onde.pos1Quem.JPBP = %L1JetPack;	
						%newBase.onde.pos1Quem.JPagora = %L1JetPack;
					}
					%newBase.onde.pos1Quem.mySnipers = %L1Snipers;
					%newBase.onde.pos1Quem.myMoral = %L1Moral;
				} else {
					if(%L2Escudo > 0){
						%newBase.onde.pos2Quem.criarMeuEscudo();
						%newBase.onde.pos2Quem.myEscudos = %L2Escudo;
					}
					%newBase.onde.pos2Quem.liderNum = 2; //Sou o Líder 2
					%newBase.onde.pos2Quem.setMyImage();
					
					if(%L2JetPack > 1){
						%newBase.onde.pos2Quem.JPBP = (%L2JetPack - 1);
						%newBase.onde.pos2Quem.JPagora = (%L2JetPack - 1);
						%newBase.onde.pos2Quem.anfibio = true;
					} else {
						%newBase.onde.pos2Quem.JPBP = %L2JetPack; //JetPack BluePrint
						%newBase.onde.pos2Quem.JPagora = %L2JetPack;
					}
					%newBase.onde.pos2Quem.mySnipers = %L2Snipers;
					%newBase.onde.pos2Quem.myMoral = %L2Moral;
				}
			}
		}
		
		//agora completa com um soldado caso haja hapenas um líder:
		if(%lider == 1){
			%newBase.spawnUnit("soldado");
			%jogadorDaVez.imperiais++;
		}			
		
		//Força Diplomática:
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
		%newBase.spawnUnit("navio");
		%jogadorDaVez.imperiais += 3;
		
		//Força Diplomática 3:
		if(%forD == 3){
			%newBase.spawnUnit("navio");
			%jogadorDaVez.imperiais += 3;
		}
		clientResetAlvos();
	}
	%jogadorDaVez.mySimBases.add(%newBase);
	if(%jogadorDaVez == $mySelf){
		clientVerificarMoral();
	}
}

function clientAskConstruirBase(%onde){
	%ondeClient = %onde.getName();
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	
	if($mySelf.engenheiro){
		%custoBase = 7;	
	} else {
		%custoBase = 10;
	}
	
	if(%mySelf == $jogadorDaVez || $estouNoTutorial){
		if(%onde.pos0Flag == false){
			if(%mySelf.imperiais >= %custoBase){
				if(%onde.oceano != 1){ //se não for um oceano
					if(%onde.ilha != 1){ //se não for uma ilha
						if(!$estouNoTutorial){
							clientPushServerComDot();
							commandToServer('construirBase', %ondeClient);
						} else {
							if(($tut_campanha.passo.objetivo $= "produzir" && $tut_campanha.passo.unidade $= "base") || $jogadorDaVez == $player2){
								clientCmdConstruirBase(%ondeClient, $jogadorDaVez.id);
								tut_verificarObjetivo(true, "base");
							}
						}
					} else {
						clientMsg("baseEmIlha", 3000);
					}
				} else {
					clientMsg("baseEmOceano", 3000);
				}
			} else {
				clientMsg("imperiaisInsuficientes", 3000);
			}
		} else {
			clientMsg("umaBasePorArea", 3000);
		}
	}
}

function clientCmdConstruirBase(%onde, %praQuem){
	%eval = "%jogadorDaVez =" SPC "$" @ %praQuem @ ";";
	eval(%eval);
	%eval = "%jogadorDaVezAreas = $" @ %praQuem @ "Areas;";
	eval(%eval);
	%eval = "%ondeClient =" SPC %onde @";";
	eval(%eval);
	
	if(%jogadorDaVez.engenheiro){
		%custoBase = 7;	
	} else {
		%custoBase = 10;
	}
	
	%jogadorDaVez.imperiais -= %custoBase;
	%newBase = base_BP.createCopy();
	if(%jogadorDaVez.oculto){
		if(%jogadorDaVez == $mySelf){
			%newBase.setBlendColor(1,1,1,0.6);
		} else {
			%newBase.setBlendColor(1,1,1,0);	
		}
		%newBase.oculta = true;
	}
	%newBase.setPosition(%onde.pos0);
	%newBase.isSelectable = 1;
	%newBase.onde = %ondeClient;
	%newBase.isMoveable = 0;
	%newBase.dono = %jogadorDaVez;
	%ondeClient.pos0Flag = true;
	%ondeClient.pos0Quem = %newBase;
	%jogadorDaVez.unitCount++;
	//%newBase.setBlendColor(%jogadorDaVez.corR, %jogadorDaVez.corG, %jogadorDaVez.corB, %jogadorDaVez.corA);	
	%newBase.setMyImage();
	
	%jogadorDaVez.mySimBases.add(%newBase);
	
	if($jogadorDaVez != $mySelf){
		if(!$jogadorDaVez.oculto){
			ghostSelect(%newBase);
		}
	}
	
	//agora atualizar o HUD pra ver se o usuário ainda tem imperiais suficientes para construir mais uma base ou se o botão deve sumir:
	atualizarImperiaisGui();
	clientAtualizarEstatisticas();
	atualizarBotoesDeCompraHumanos();
	if($myPersona.aca_i_3 > 0){
		clientAtualizarAlmiranteTab();
	}
	
	clientClearUndo(); //bloqueia o undo
	clientAskConfirmarAtoFeito(%newBase.dono.id); //envia confirmação de que construiu a base, para sincronia
}

function clientAskSpawnUnit(%onde, %tipo, %AI){
	//echo("clientAskSpawnUnit => " @ %onde.getName() SPC %tipo);
	if($rodadaAtual > 0){
		%clientOnde = %onde.getName();
		%eval = "%mySelf =" SPC $mySelf @ ";";
		eval(%eval);
		%base = %onde.pos0Quem;
		
		if(%mySelf == $jogadorDaVez || $estouNoTutorial || $jogadorDaVez == $aiPlayer){
			switch$(%tipo){
				case "soldado":
				if(%mySelf.imperiais > 0){
					if(!$estouNoTutorial){
						clientPushServerComDot();
						commandToServer('spawnUnit', %clientOnde, %tipo);
					} else {
						if(($tut_campanha.passo.objetivo $= "produzir" && $tut_campanha.passo.unidade $= %tipo) || $jogadorDaVez == $player2){
							clientCmdSpawnUnit(%clientOnde, %tipo);
							tut_verificarObjetivo(true, %tipo);
						}
					}
				} else {
					clientMsg("imperiaisInsuficientes", 3000);
				}
				
				case "tanque":
				if(%mySelf.imperiais > 1){
					if(!$estouNoTutorial){
						clientPushServerComDot();
						commandToServer('spawnUnit', %clientOnde, %tipo);
					} else {
						if(($tut_campanha.passo.objetivo $= "produzir" && $tut_campanha.passo.unidade $= %tipo) || $jogadorDaVez == $player2){
							clientCmdSpawnUnit(%clientOnde, %tipo);	
							tut_verificarObjetivo(true, %tipo);
						}
					}
				} else {
					clientMsg("imperiaisInsuficientes", 3000);
					clientVerificarBtnsAtivos(Foco.getObject(0));
				}
				
				case "navio":
				if($mySelf.marinheiro){
					%custoNavio = 2;	
				} else {
					%custoNavio = 3;
				}
				if($mySelf.imperiais >= %custoNavio){
					if(!$estouNoTutorial){
						clientPushServerComDot();
						commandToServer('spawnUnit', %clientOnde, %tipo);
					} else {
						if(($tut_campanha.passo.objetivo $= "produzir" && $tut_campanha.passo.unidade $= %tipo) || $jogadorDaVez == $player2){
							clientCmdSpawnUnit(%clientOnde, %tipo);	
							tut_verificarObjetivo(true, %tipo);
						}
					}
				} else {
					clientMsg("imperiaisInsuficientes", 3000);
				}
				
				case "ovo":
				if(%base.cortejada || %base.matriarca){
					%custo = 2;
				} else {
					%custo = 3;
				}
				if(%mySelf.imperiais >= %custo || %AI){
					clientPushServerComDot();
					commandToServer('spawnUnit', %clientOnde, %tipo);
				} else {
					clientMsg("imperiaisInsuficientes", 3000);
				}
				
				case "cefalok":
				if(%mySelf.imperiais > 3 || %AI){
					clientPushServerComDot();
					commandToServer('spawnUnit', %clientOnde, %tipo);
				} else {
					clientMsg("imperiaisInsuficientes", 3000);
				}
				
				case "verme":
				clientPushServerComDot();
				commandToServer('spawnUnit', %clientOnde, %tipo);
			}
		} else {
			echo("ERRO1!");	
		}
	} else { //se ainda não é a primeira rodada:
		//echo("SELECIONE UM DOS LOCAIS MARCADOS PARA A CONSTRUÇÃO DE UMA BASE");
		echo("Impossível produzir unidades antes da primeira rodada!");
	}
}

function clientCmdSpawnUnit(%onde, %tipo){
	//echo("clientCmdSpawnUnit => " @ %onde.getName() SPC %tipo);
	%base = %onde.pos0Quem;
	ghostSelect(%base);
	%base.spawnUnit(%tipo);
	atualizarImperiaisGui();
	if($mySelf == $jogadorDaVez){
		clientVerificarBtnsAtivos(%base);	
	}
	if(%tipo $= "ovo"){
		schedule(1000, 0, "clientVerificarInstinto", %base);	
	}
	clientClearUndo();
	//clientAskConfirmarAtoFeito(%base.dono.id);
	clientPopServerComDot();
}

/////////////
//Refinarias:
function clientAskConstruirRefinaria(){
	%onde = Foco.getObject(0).onde;
	%ondeClient = %onde.getName();
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	
	if($myPersona.aca_v_4 > 2){
		%custoRefinaria = 4;	
	} else {
		%custoRefinaria = 5;	
	}
	
	if(%mySelf == $jogadorDaVez){
		if(%onde.pos0Flag == false){
			if(%mySelf.imperiais >= %custoRefinaria){
				if(%mySelf.mySimRefinarias.getCount() < $myPersona.aca_v_4 && %mySelf.mySimRefinarias.getCount() < 3){
					if(%onde.oceano != 1){ //se não for um oceano
						if(%onde.ilha != 1){ //se não for uma ilha
							if(%onde.terreno $= "terra"){
								clientPushServerComDot();
								commandToServer('construirRefinaria', %ondeClient);	
							} else {
								clientMsg("refinariaSohEmTerra", 3000);	//OK?
							}
						} else {
							clientMsg("baseEmIlha", 3000);
						}
					} else {
						clientMsg("baseEmOceano", 3000);
					}
				} else {
					//não pode ter mais de 3 refinarias, nem mais refinarias do qeu seu nível em refinarias...		
				}
			} else {
				clientMsg("imperiaisInsuficientes", 3000);
			}
		} else {
			clientMsg("umaBasePorArea", 3000);
		}
	}
}




function clientCmdConstruirRefinaria(%onde, %praQuem, %custoRefinaria){
	%eval = "%jogadorDaVez =" SPC "$" @ %praQuem @ ";";
	eval(%eval);
	%eval = "%jogadorDaVezAreas = $" @ %praQuem @ "Areas;";
	eval(%eval);
	%eval = "%ondeClient =" SPC %onde @";";
	eval(%eval);
	
	%jogadorDaVez.imperiais -= %custoRefinaria;
	%newBase = refinaria_BP.createCopy();
	if(%jogadorDaVez.oculto){
		if(%jogadorDaVez == $mySelf){
			%newBase.setBlendColor(1,1,1,0.6);
		} else {
			%newBase.setBlendColor(1,1,1,0);	
		}
		%newBase.oculta = true;
	}
	%newBase.refinaria = true;
	%newBase.setPosition(%onde.pos0);
	%newBase.isSelectable = 1;
	%newBase.onde = %ondeClient;
	%newBase.isMoveable = 0;
	%newBase.dono = %jogadorDaVez;
	%ondeClient.pos0Flag = true;
	%ondeClient.pos0Quem = %newBase;
	%ondeClient.dono = %jogadorDaVez;
	%jogadorDaVezAreas.add(%ondeClient);
	%jogadorDaVez.areaCount++;
	%jogadorDaVez.unitCount++;
	%newBase.setMyImage();
	
	%jogadorDaVez.mySimBases.add(%newBase);
	%jogadorDaVez.mySimRefinarias.add(%newBase);
	
	if($jogadorDaVez != $mySelf){
		if(!$jogadorDaVez.oculto){
			ghostSelect(%newBase);
		}
	}
	
	//agora atualizar o HUD:
	atualizarImperiaisGui();
	clientAtualizarEstatisticas();
	atualizarBotoesDeCompraHumanos();
	
	clientClearUndo(); //bloqueia o undo
	clientAskConfirmarAtoFeito(%newBase.dono.id);
}



//===========================
///GULOK:
function clientAskCriarRainha(%onde, %semLideres, %AI){
	%ondeClient = %onde.getName();
	commandToServer('criarRainha', %ondeClient, false, %semLideres, %AI);	
}


function clientCmdCriarRainha(%onde, %praQuem, %lider, %L1Asas, %L2Asas, %L1Canibalizar, %L2Metamorfose, %forD, %L1Carregar, %L2Carregar, %L1DevorarRainhas, %L2Cortejar, %semFilhos, %semLideres, %AI){
	%eval = "%jogadorDaVez =" SPC "$" @ %praQuem @ ";";
	eval(%eval);
	%eval = "%ondeClient =" SPC %onde @";";
	eval(%eval);
	
	%newRainha = rainha_BP.createCopy();
	%newRainha.gulok = true;
	%newRainha.setPosition(%onde.pos0);
	%newRainha.isSelectable = 1;
	%newRainha.onde = %ondeClient;
	%newRainha.isMoveable = 1;
	%newRainha.dono = %jogadorDaVez;
	%ondeClient.pos0Flag = true;
	%ondeClient.pos0Quem = %newRainha;
	%ondeClient.dono = %jogadorDaVez;
	%jogadorDaVez.mySimAreas.add(%ondeClient);
	%newRainha.setMyImage();
	%newRainha.pos = "pos0";
	
	if(!%semFilhos){
		if (%newRainha.onde.terreno $= "terra"){
			%newRainha.spawnUnit("ovo");
			%newRainha.spawnUnit("ovo");
			%jogadorDaVez.imperiais += 6;
			if(!%semLideres){
				for(%i = 0; %i < %lider; %i++){
					%newLider = %newRainha.spawnUnit("zangao");
					%newLider.liderNum = %i+1;
					%newLider.setMyImage();
					
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
			if(%forD > 0){
				%newRainha.spawnUnit("ovo");
				%jogadorDaVez.imperiais += 3;
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
			%newRainha.spawnUnit("cefalok");
			%jogadorDaVez.imperiais += 4;
			if(%AI){
				%newRainha.spawnUnit("cefalok");
				%jogadorDaVez.imperiais += 4;
			}
			clientResetAlvos();
		}
	}
	%jogadorDaVez.mySimBases.add(%newRainha);
}

function clientEvoluir(%unit, %especial){
	%custo = 10;	
	
	if($mySelf.aca_av_4 == 3)
	{
		%custo = 9;	
	}
	if(%especial == true){
		%custo = 0;	
	}
	
	if($mySelf.imperiais >= %custo){
		clientAskEvoluirEmRainha(%unit.onde, %unit.pos, %especial);
	} else {
		clientMsg("imperiaisInsuficientes", 3000);	
	}
}

function clientAskEvoluirEmRainha(%onde, %posDeOrigem, %especial){
	clientPushServerComDot();
	%ondeClient = %onde.getName();
	commandToServer('evoluirEmRainha', %ondeClient, %posDeOrigem, %especial);
}

function clientCmdEvoluirEmRainha(%onde, %posDeOrigem, %especial, %metamorfose, %evolucaoAvancada){
	%eval = "%ondeClient =" SPC %onde @";";
	eval(%eval);
	
	%unit = clientGetGameUnit(%onde, %posDeOrigem);
	%custo = 10;
	
	if(%evolucaoAvancada){
		%custo = 9;	
	}
	if(%especial == true){
		%custo = 0;	
	}
	if(%metamorfose > 0){
		%custo = 10 - (%metamorfose * 3);	
	}
	
			
	%unit.dono.imperiais -= %custo;
	%newRainha = rainha_BP.createCopy();
	%newRainha.gulok = true;
	%newRainha.setPosition(%unit.getPosition());
	%newRainha.isSelectable = 1;
	%newRainha.onde = %ondeClient;
	%newRainha.isMoveable = 1;
	%newRainha.dono = %unit.dono;
	%newRainha.setMyImage();
	
	//efeito de evolução pra rainhas:
	%explosao = new t2dParticleEffect(){scenegraph = %unit.scenegraph;};
	%explosao.loadEffect("~/data/particles/evoluirEmRainha.eff");
	%explosao.setEffectLifeMode("KILL", 1);
    %explosao.mount(%newRainha, 0, 0, 0, 0, 0, 0, 0); //monta o efeito na rainha
    %explosao.playEffect();
   
	playSound("rainha_nascer", 3, true);
	
	%unit.dono.mySimBases.add(%newRainha);
		
	ghostSelect(%newRainha);
	
	//agora manda a rainha pra pos0:
	%ondeClient.positionUnit(%newRainha);
	
	//e agora remove a peça inicial:
	clientRemoverUnidade(%unit, %ondeClient);
	%unit.dono.mySimUnits.remove(%unit);
	if(Foco.getObject(0) == %unit){
		resetSelection();
	}
	%unit.safeDelete();
	
	//agora atualizar o HUD:
	atualizarImperiaisGui();
	clientAtualizarEstatisticas();
	clientAskConfirmarAtoFeito(%newRainha.dono.id);
}
///////////
//eclosão dos ovos:
function clientVerificarOvos(){
	%ovosCount = $jogadorDaVez.mySimOvos.getCount();
	for(%i = 0; %i < %ovosCount; %i++){
		%ovos[%i] = $jogadorDaVez.mySimOvos.getObject(%i);
	}
	for(%i = 0; %i < %ovosCount; %i++){
		%ovo = %ovos[%i];
		%ovo.eclodir();
	}
}


//quando não constroi e tem que tirar o serverComDot do client:
function clientCmdPopServerComDot(){
	clientPopServerComDot();	
}

////////////
//Grande Matriarca:
function clientAskCriarGrandeMatriarca(%onde){
	%ondeClient = %onde.getName();
	commandToServer('criarGrandeMatriarca', %ondeClient);	
}

function clientCmdCriarGrandeMatriarca(%onde, %praQuem){
	%eval = "%playerGulok =" SPC "$" @ %praQuem @ ";";
	eval(%eval);
	%eval = "%ondeClient =" SPC %onde @";";
	eval(%eval);
	
	
	%newMatriarca = grandeMatriarca_BP.createCopy();
	%newMatriarca.gulok = true;
	%newMatriarca.setPosition(%onde.pos0);
	%newMatriarca.isSelectable = 1;
	%newMatriarca.onde = %ondeClient;
	%newMatriarca.isMoveable = 1;
	%newMatriarca.dono = %playerGulok;
	%ondeClient.pos0Flag = true;
	%ondeClient.pos0Quem = %newMatriarca;
	%ondeClient.dono = %playerGulok;
	%playerGulok.mySimAreas.add(%ondeClient);
	%newMatriarca.matriarca = true;
	%newMatriarca.grandeMatriarca = true;
	%newMatriarca.setMyImage();
	%newMatriarca.pos = "pos0";
	%playerGulok.mySimBases.add(%newMatriarca);
	if(!isObject(%playerGulok.mySimMatriarcas)){
		%playerGulok.mySimMatriarcas = new SimSet();	
	} else {
		%playerGulok.mySimMatriarcas.clear();
	}
	%playerGulok.mySimMatriarcas.add(%newMatriarca);
	
	playSound("matriarca_nascer", 2, true);
	
	//alxPlay( gulok_appear );
	
	//instantFX:
	%grandeMatriarcaIFX = new t2dParticleEffect(){scenegraph = %newMatriarca.scenegraph;};
	%grandeMatriarcaIFX.loadEffect("~/data/particles/grandeMatriarcaInstantFX.eff");
	%grandeMatriarcaIFX.mount(%newMatriarca, 0, 0, 0, 1, 0, 0, 0); //o quarto número é 1[bool], quer dizer pra herdar a rotação do objeto-base
	%grandeMatriarcaIFX.setEffectLifeMode("KILL", 3);
	%grandeMatriarcaIFX.playEffect();
	%newMatriarca.myIFX = %grandeMatriarcaIFX;
	
	//FX:
	%grandeMatriarcaFX = new t2dParticleEffect(){scenegraph = %newMatriarca.scenegraph;};
	%grandeMatriarcaFX.loadEffect("~/data/particles/grandeMatriarca" @ %playerGulok.myColor @ "FX.eff");
	%grandeMatriarcaFX.mount(%newMatriarca, 0, 0, 0, 1, 0, 0, 0); //o quarto número é 1[bool], quer dizer pra herdar a rotação do objeto-base
	%grandeMatriarcaFX.playEffect();
	%newMatriarca.myFX = %grandeMatriarcaFX;
}

