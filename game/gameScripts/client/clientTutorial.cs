// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientTutorial.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 4 de julho de 2008 7:56
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  Tutorial do jogo
//                    :  
//                    :  
// ============================================================

//começa criando um vetor que guarda as páginas do tutorial:
function clientInitTutorial(){
	canvas.popDialog(tutSelectGui);
	TUT_intro.setVisible(true); //traz uma tela onde se pode colocar textos (pngs)
	if($myPersona.taxoVitorias > 25){
		tut_iniciarJogoTutorial_btn.setActive(false);
	} else {
		tut_iniciarJogoTutorial_btn.setActive(true);
	}
}

function clientInitTutorial2(){
	canvas.popDialog(tutSelectGui);
	canvas.pushDialog(tut2Gui);
}

function tut2VoltarBtnClick(){
	canvas.popDialog(tut2Gui);	
}

function clientTutorialIniciarJogo(){
	$estouNoTutorial = true; //marca que o client está no tutorial
	canvas.popDialog(loggedInGui); //apaga a tela de loggedIn
	canvas.popDialog(atrioGui); //apaga a tela do atrio, caso o jogador tenha recém vindo de lá
	clientCmdSetMyPlayer("player1", "Terra", 2, 0, 500); //seta os players e chama a escolha de objetivos
	clientCmdSorteioDeOrdemShow(2, $myPersona.nome, "Adversário"); //seta os nomes para o sorteio
	clientCmdSetJogadorDaVez("player1", 0);
	tut_pegarResolucao();
	commandToServer('entreiNoTutorial');
}

function tut_pegarResolucao(){
	%myWindowResX = sceneWindow2d.getWindowExtents();
	$myWindowResX = getWord(%myWindowResX, 2);
	echo("myWindowResX = " @ $myWindowResX);
	
	$tutMod_arrowSoldadoX = calcularNaRes(-15, $myWindowResX);
	$tutMod_arrowSoldadoY = calcularNaRes(-100, $myWindowResX);
	$tutMod_arrowTanqueX = calcularNaRes(-10, $myWindowResX);
	$tutMod_arrowTanqueY = calcularNaRes(-115, $myWindowResX);
	$tutMod_arrowNavioX = calcularNaRes(-15, $myWindowResX);
	$tutMod_arrowNavioY = calcularNaRes(-120, $myWindowResX);
	
	$tutMod_hudSoldadoX = calcularNaRes(-15, $myWindowResX);
	$tutMod_hudSoldadoY = calcularNaRes(-150, $myWindowResX);
	$tutMod_hudTanqueX = calcularNaRes(-15, $myWindowResX);
	$tutMod_hudTanqueY = calcularNaRes(-170, $myWindowResX);
	$tutMod_hudNavioX = calcularNaRes(-15, $myWindowResX);
	$tutMod_hudNavioY = calcularNaRes(-170, $myWindowResX);
	
	$tutMod_hudAustraliaX = calcularNaRes(-330, $myWindowResX);
	$tutMod_hudAustraliaY = calcularNaRes(-50, $myWindowResX);
	$tutMod_hudAmNorteX = calcularNaRes(80, $myWindowResX);
	$tutMod_hudAmNorteY = calcularNaRes(-30, $myWindowResX);
	
	$tutMod_hudBasico1X = calcularNaRes(-15, $myWindowResX);
	$tutMod_hudBasico1Y = calcularNaRes(-150, $myWindowResX);
	
	$tutMod_hudBasico2X = calcularNaRes(-30, $myWindowResX);
	$tutMod_hudBasico2Y = calcularNaRes(-150, $myWindowResX);
	
	$tutMod_hudBasico3X = calcularNaRes(-60, $myWindowResX);
	$tutMod_hudBasico3Y = calcularNaRes(-150, $myWindowResX);
	
	$tutMod_hudBasico4X = calcularNaRes(-350, $myWindowResX);
	$tutMod_hudBasico4Y = calcularNaRes(-50, $myWindowResX);
	
	$tutMod_hudLookX = calcularNaRes(-200, $myWindowResX);
	$tutMod_hudLookY = calcularNaRes(-150, $myWindowResX);
}

function TUTintroClose(){
	TUT_intro.setVisible(false);	
	//TUT2_intro.setVisible(false);	
}

function TUTarrowInicialSaoPaulo(){
	if(!saoPaulo.pos0Flag){
		%obj = saoPaulo;
		%title = "BASE INICIAL";
		%linha1b = "Clique nesta área para";
		%linha2b = "construir sua base";
		%linha3b = "terrestre inicial";
		%img1 = "tutMouseIcon";
		%img2 = "tutMouseIconNormal";
		tut_arrowOn(%obj, %title, %linha1, %linha2, %linha3, %linha4, %linha1b, %linha2b, %linha3b, %img1, %img2, true);
	}
}

function TUTarrowInicialbTodosOsSantos(){
	if(!bTodosSantos.pos0Flag){
		%obj = bTodosOsSantos;
		%title = "BASE INICIAL";
		%linha1b = "Clique nesta área para";
		%linha2b = "construir sua base";
		%linha3b = "marítima inicial";
		%img1 = "tutMouseIcon";
		%img2 = "tutMouseIconNormal";
		tut_arrowOn(%obj, %title, %linha1, %linha2, %linha3, %linha4, %linha1b, %linha2b, %linha3b, %img1, %img2, true);
	}
}


function TUTmsg(%filename, %pos, %inGame){
	if(%pos !$= ""){
		switch$(%pos){
			case "left":
			tut_fundoLeft.setVisible(true);
			tut_fundo.setVisible(false);
			tut_fundoRight.setVisible(false);
			TUTmsgTXTleft.bitmap = "~/data/images/tutorial/tut_" @ %filename @ ".png"; //coloca o texto correto
			
			case "center":
			tut_fundo.setVisible(true);
			tut_fundoLeft.setVisible(false);
			tut_fundoRight.setVisible(false);
			TUTmsgTXT.bitmap = "~/data/images/tutorial/tut_" @ %filename @ ".png"; //coloca o texto correto
			
			case "right":
			tut_fundoRight.setVisible(true);
			tut_fundoLeft.setVisible(false);
			tut_fundo.setVisible(false);
			TUTmsgTXTright.bitmap = "~/data/images/tutorial/tut_" @ %filename @ ".png"; //coloca o texto correto
		}
	} else {
		tut_fundo.setVisible(true);
		tut_fundoLeft.setVisible(false);
		tut_fundoRight.setVisible(false);
		TUTmsgTXT.bitmap = "~/data/images/tutorial/tut_" @ %filename @ ".png"; //coloca o texto correto
	}
	
	if(%inGame){
		tut_ok_btn.setVisible(false);
		tut_inGame_ok_btn.setVisible(true);
	} else {
		tut_ok_btn.setVisible(true);
		tut_inGame_ok_btn.setVisible(false);
	}
	
	canvas.pushDialog(TUTmsgGui); //traz o fundo da msgBox
}

function TUTinGameMsgClose(){
	canvas.popDialog(TUTmsgGui);
}

function TUTmsgClose(){
	canvas.popDialog(TUTmsgGui);
	if($estouNoTutorial){
		if($rodadaAtual == 0){
			if($escolhendoGrupo){
				if($grupoStartCount == 0){
					TUTarrowInicialSaoPaulo(); 
				} else {
					TUTarrowInicialbTodosOsSantos(); 	
				}
			}
		} else if($primeiraRodada){
			if(!$jahInicieiTutObjetivos){
				tut_initObjetivos();
			}
		}
	}
}

function tutIniciarNachina(){
	clientCmdCriarBase("pequim", "player2");	
	clientCmdCriarBase("bMarDaChina", "player2");
	schedule(2000, 0, "clientCmdSetJogadorDaVez", "player1", 0);
	schedule(2002, 0, "clientCmdIniciarPartida", 2);
	schedule(2004, 0, "clientCmdInicializarMeuTurno", 5, 10, 0, 0, 0);
	schedule(2008, 0, "TUTmsg", "movimentacao", "right");
}

function TUTaiJogar(%rodada){
	//chama a função correta de acordo com a rodada:
	%eval = "TUTaiRodada" @ %rodada @ "();";
	eval(%eval);
}

function tut_initArrow(){
	tutArrow.myDefaultPos = tutArrow.getPosition();	
	tutArrowAustralia.myDefaultPos = tutArrowAustralia.getPosition();	
	tutArrowAmNorte.myDefaultPos = tutArrowAmNorte.getPosition();	
}

function tut_arrowClearLinhas(){
	tutMiniGuiLinha1_txt.text = "";
	tutMiniGuiLinha2_txt.text = "";	
	tutMiniGuiLinha3_txt.text = "";	
	tutMiniGuiLinha4_txt.text = "";	
	
	tutMiniGuiLinha1b_txt.text = "";	
	tutMiniGuiLinha2b_txt.text = "";	
	tutMiniGuiLinha3b_txt.text = "";	
}

function tut_arrowOn(%obj, %title, %linha1, %linha2, %linha3, %linha4, %linha1b, %linha2b, %linha3b, %img1, %img2, %inicial, %closeBtn){
	tut_arrowClearLinhas();
	if($tut_campanha.passo.objetivo $= "produzir" || ($tut_campanha.passo.objetivo $= "look" && !$tut_campanha.passo.propostaArrow)|| ($tut_campanha.passo.objetivo $= "btnClick" && $tut_campanha.passo.key !$= "enviarExplClick")){
		%myArrow = tutArrowGui;	
		%palcoPosX = FirstWord(%obj.getPosition());
		%palcoPosY = getWord(%obj.getPosition(), 1);
		%newGuiPos = SceneWindow2d.getWindowPoint(%palcoPosX, %palcoPosY);
		%newPosX = FirstWord(%newGuiPos);
		%newPosY = GetWord(%newGuiPos, 1);
		if($tut_campanha.passo.unidade $= "soldado"){
			%myArrow.setPosition(%newPosX+$tutMod_arrowSoldadoX, %newPosY+$tutMod_arrowSoldadoY);	
		} else if($tut_campanha.passo.unidade $= "tanque" || $tut_campanha.passo.unidade $= "base"){
			%myArrow.setPosition(%newPosX+$tutMod_arrowTanqueX, %newPosY+$tutMod_arrowTanqueY);	
		} else if($tut_campanha.passo.unidade $= "navio"){
			%myArrow.setPosition(%newPosX+$tutMod_arrowNavioX, %newPosY+$tutMod_arrowNavioY);	
		} else {
			%myArrow.setPosition(%newPosX+$tutMod_arrowSoldadoX, %newPosY+$tutMod_arrowSoldadoY);
		}
	} else {
		if(%obj.class $= "area"){
			if(%obj.pos0 !$= ""){
				//seta para conquista, ataque e missão:
				if($tut_campanha.passo.bonus $= "recurso" || %closeBtn !$= ""){
					%newPosX = FirstWord(%obj.pos1);
					%newPosY = GetWord(%obj.pos1, 1);
				} else {
					%newPosX = FirstWord(%obj.pos0);
					%newPosY = GetWord(%obj.pos0, 1);
				}
				echo("Obj.posX = " @ %newPosX @ "; Obj.posY = " @ %newPosY);
				if(%closeBtn !$= ""){
					if(%newPosY < -20 && %newPosX < 18){
						%myArrow = tutArrowAmNorte;	
					} else if(%newPosX > 18){
						%myArrow = tutArrowAustralia;	
					} else {
						%myArrow = tutArrow;
					}
				} else {
					if(%newPosX > 20){
						%myArrow = tutArrowAustralia;	
					} else if(%newPosX < -20 && %newPosY < -20){
						%myArrow = tutArrowAmNorte;	
					} else {
						%myArrow = tutArrow;
					}
				}
				%orient = angleBetween(%myArrow.myDefaultPos, %obj.pos0);
			} else {
				if(%closeBtn !$= ""){
					//é uma missão em uma ilha
					%newPosX = FirstWord(%obj.pos1);
					%newPosY = GetWord(%obj.pos1, 1);
					echo("Obj.posX = " @ %newPosX @ "; Obj.posY = " @ %newPosY);
					if(%newPosY < -20 && %newPosX < 18){
						%myArrow = tutArrowAmNorte;	
					} else if(%newPosX > 18){
						%myArrow = tutArrowAustralia;	
					} else {
						%myArrow = tutArrow;
					}
				} else {
					//seta para os btns de produção de unidades:
					%myArrow = tutArrow;	
					%orient = angleBetween(%myArrow.myDefaultPos, %obj.pos1);
					%newPosX = FirstWord(%obj.pos1);
					%newPosY = GetWord(%obj.pos1, 1);
				}
			}
		} else {
			//seta para seleção:
			%newPosX = FirstWord(%obj.getPosition());
			%newPosY = GetWord(%obj.getPosition(), 1);
			//if(%newPosX > 20 && %newPosY > -2){
			if(%newPosX > 20){				
				%myArrow = tutArrowAustralia;	
			} else if(%newPosX < -20 && %newPosY < -20){
				%myArrow = tutArrowAmNorte;	
			} else {
				%myArrow = tutArrow;
			}
			%orient = angleBetween(%myArrow.myDefaultPos, %obj.getPosition());
		}
		%myArrow.setRotation(%orient);
		%myArrow.setPosition(%newPosX, %newPosY);	
		%setaGUIPOS = SceneWindow2d.getWindowPoint(%newPosX, %newPosY);
		%newPosX = FirstWord(%setaGUIPOS);
		%newPosY = GetWord(%setaGUIPOS, 1);
	}
	
	echo("%myArrow = " @ %myArrow);
	echo("%newPosX = " @ %newPosX @ "; %newPosY = " @ %newPosY);
	
	%myArrow.setVisible(true);
		
	tutMiniGuiTitle_txt.text = %title;
	tutMiniGuiLinha1_txt.text = %linha1;
	tutMiniGuiLinha2_txt.text = %linha2;
	tutMiniGuiLinha3_txt.text = %linha3;
	tutMiniGuiLinha4_txt.text = %linha4;
	tutMiniGuiLinha1b_txt.text = %linha1b;
	tutMiniGuiLinha2b_txt.text = %linha2b;
	tutMiniGuiLinha3b_txt.text = %linha3b;
	
	if(%inicial){
		%newPosY += 20; 
	}
	if(%myArrow.myDefaultPos $= tutArrowAustralia.myDefaultPos){
		echo("%myArrow.myDefaultPos == tutArrowAustralia.myDefaultPos");
		tutMiniHud.setPosition(%newPosX+$tutMod_hudAustraliaX, %newPosY+$tutMod_hudAustraliaY);
	} else if(%myArrow.myDefaultPos $= tutArrowAmNorte.myDefaultPos){
		echo("%myArrow.myDefaultPos == tutArrowAmNorte.myDefaultPos");
		tutMiniHud.setPosition(%newPosX+$tutMod_hudAmNorteX, %newPosY+$tutMod_hudAmNorteY);
	} else {
		if($tut_campanha.passo.objetivo $= "produzir" || ($tut_campanha.passo.objetivo $= "look" && !$tut_campanha.passo.propostaArrow) || $tut_campanha.passo.objetivo $= "btnClick"){
			if($tut_campanha.passo.unidade $= "soldado"){
				tutMiniHud.setPosition(%newPosX+$tutMod_hudSoldadoX, %newPosY+$tutMod_hudSoldadoY);		
			} else if($tut_campanha.passo.unidade $= "tanque" || $tut_campanha.passo.unidade $= "base"){
				tutMiniHud.setPosition(%newPosX+$tutMod_hudTanqueX, %newPosY+$tutMod_hudTanqueY);		
			} else if($tut_campanha.passo.unidade $= "navio"){
				tutMiniHud.setPosition(%newPosX+$tutMod_hudNavioX, %newPosY+$tutMod_hudNavioY);		
			} else {
				tutMiniHud.setPosition(%newPosX+$tutMod_hudLookX, %newPosY+$tutMod_hudLookY);	
			}
		} else {
			echo("%myArrow.myDefaultPos != tutArrowAustralia.myDefaultPos");
			if(%orient < 0){
				tutMiniHud.setPosition(%newPosX+$tutMod_hudBasico1X, %newPosY+$tutMod_hudBasico1Y);		
			} else if (%orient < 100 && %orient > 80){
				tutMiniHud.setPosition(%newPosX+$tutMod_hudBasico2X, %newPosY+$tutMod_hudBasico2Y);	
			} else if (%orient < 80 && %orient > 70){
				tutMiniHud.setPosition(%newPosX+$tutMod_hudBasico3X, %newPosY+$tutMod_hudBasico3Y);	
			} else {
				echo("ELSE ELSE!");
				if(%closeBtn !$= ""){
					tutMiniHud.setPosition(%newPosX+$tutMod_hudBasico3X, %newPosY+$tutMod_hudBasico3Y);	
				} else {
					tutMiniHud.setPosition(%newPosX+$tutMod_hudBasico4X, %newPosY+$tutMod_hudBasico4Y);
				}
			}
		}
	}
	
	if(%closeBtn){
		tutMiniHud_closeBtn.setVisible(true);	
	} else {
		tutMiniHud_closeBtn.setVisible(false);	
	}
	tutMiniHud.setVisible(true);
	
	$tutIconAceso = false; // começa mostrando o btn que deve ser pressionado, a segunda imagem. 
	tut_piscarIcon(%img1, %img2);
}

function tut_piscarIcon(%img1, %img2){
	cancel($tutIconSchedule);
	if($tutIconAceso){
		tutMiniGuiIcon.bitmap = "~/data/images/tutorial/" @ %img1 @ ".png";
		$tutIconAceso = false;
	} else {
		tutMiniGuiIcon.bitmap = "~/data/images/tutorial/" @ %img2 @ ".png";
		$tutIconAceso = true;	
	}
	$tutIconSchedule = schedule(800, 0, "tut_piscarIcon", %img1, %img2);
}

///Apaga o tutMiniHud e as setas
function tut_clearTips(){
	cancel($tutIconSchedule);
	tutArrow.setVisible(false);
	tutArrowAustralia.setVisible(false);
	tutArrowAmNorte.setVisible(false);
	tutArrowGui.setVisible(false);
	tutMiniHud.setVisible(false);
	
	//limpa as variáveis de produção:
	$tut_producaoSoldado = 0;
	$tut_producaoTanque = 0;
	$tut_producaoNavio = 0;
	$tut_producaoBase = 0;
}

function tut_initObjetivos(){
	if(isObject($tut_objetivo)){
		$tut_objetivo.delete();	
	}
	if(isObject($tut_campanha)){
		$tut_campanha.delete();	
	}
	$tut_objetivo =	new ScriptObject(){};
	$tut_campanha =	new ScriptObject(){};
	if(isObject($tut_campanha.myPassos)){
		$tut_campanha.myPassos.clear();	
	} else {
		$tut_campanha.myPassos = new SimSet();
	}
	$tut_campanha.passo = 0;
	
	$tut_campanha.passo1 = new ScriptObject(){
		num = 1;
		objetivo = "selecionar";
		alvo = $myTUTAreaTerrestreInicial.pos1Quem;
		titulo = "SELECIONAR";
		linha2 = "Clique para selecionar";
		linha3 = "este soldado.";
		icon1 = "tutMouseIcon";
		icon2 = "tutMouseIconNormal";
		override = true;
	};
	$tut_campanha.myPassos.add($tut_campanha.passo1);
	
	//seta as missões dessa campanha de tutorial:
	$tut_campanha.infoNum1 = 57; //base no Mar de Java
	$tut_campanha.infoNum2 = 43; //base em Kinshasa
	$tut_campanha.infoNum3 = 28; //petróleo na Baía de Baffin
	$tut_campanha.infoNum4 = 9; //minério em Vancouver
	$tut_campanha.infoNum5 = 40; //urânio em Pequim
	$tut_campanha.infoNum6 = 64; //base na Comunidade Européia
	
	$jahInicieiTutObjetivos = true;
	tut_completarObjetivo(); //para iniciar com o primeiro passo
}

function tut_execPasso(%passoNum){
	switch(%passoNum){
		case 2:
		$tut_campanha.passo2 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = manaus;
			linha1b = "Clique com o botão direito";
			linha2b = "nesta área para mover o";
			linha3b = "soldado selecionado.";
			override = true;
		};
		if($myTUTAreaTerrestreInicial.getName() $= "manaus"){
			$tut_campanha.passo2.alvo = saoPaulo;
		}
		
		case 3:
		$tut_campanha.passo3 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos0Quem;
		};
		
		case 4:
		$tut_campanha.passo4 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaTerrestreInicial;
			unidade = "soldado";
			qtd = 3;
			linha1b = "Clique para produzir um soldado.";
			linha2b = "Cada soldado custa 1 Imperial.";
			linha3b = "Produza 3 soldados.";
			override = true;
		};
		
		case 5:
		$tut_campanha.passo5 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = manaus.pos1Quem; 
		};
		
		case 6:
		$tut_campanha.passo6 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = venezuela;
			linha1b = "Clique com o botão direito";
			linha2b = "nesta área para mover o";
			linha3b = "soldado selecionado.";
			override = true;
		};
		
		case 7:
		$tut_campanha.passo7 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos1Quem; 
		};
		
		case 8:
		$tut_campanha.passo8 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = manaus;
		};
		if($myTUTAreaTerrestreInicial.getName() $= "manaus"){
			$tut_campanha.passo8.alvo = bolivia;
		}
		
		case 9:
		$tut_campanha.passo9 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = saoPaulo.pos1Quem;
		};
		
		case 10:
		$tut_campanha.passo10 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = argentina;
		};
		
		case 11:
		$tut_campanha.passo11 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos1Quem; 
			linha2b = "Agora, selecione este soldado.";
			override = true;
		};
		
		case 12:
		$tut_campanha.passo12 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = bolivia;
		};
		if($myTUTAreaTerrestreInicial.getName() $= "manaus"){
			$tut_campanha.passo12.alvo = saoPaulo;
		}
		
		case 13:
		$tut_campanha.passo13 = new ScriptObject(){
			objetivo = "finalizarJogada";
		};
		
		case 14:
		$tut_campanha.passo14 = new ScriptObject(){
			objetivo = "missaoRecebida";
			alvo = bMarDeJava;
			bonus = "pontos";
			override = true;
			titulo = "MISSÃO RECEBIDA";
			linha1 = "Esta missão lhe dará +2 pontos";
			linha2 = "por construir uma base nesta";
			linha3 = "área. Mais tarde você aprenderá";
			linha4 = "a construir bases.";
		};
		
		////////////////
		//Segunda rodada:
		case 15:
		$tut_campanha.passo15 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos0Quem; 
		};
		
		case 16:
		$tut_campanha.passo16 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaMaritimaInicial;
			unidade = "navio";
			qtd = 1;
			linha1b = "Clique para produzir um navio.";
			linha2b = "Cada navio custa 3 Imperiais.";
			linha3b = "Produza 1 navio.";
			override = true;
		};
		
		case 17:
		$tut_campanha.passo17 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos0Quem; 
		};
		
		case 18:
		$tut_campanha.passo18 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaTerrestreInicial;
			unidade = "soldado";
			qtd = 2;
		};
		
		case 19:
		$tut_campanha.passo19 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos1Quem; 
		};
		
		case 20:
		$tut_campanha.passo20 = new ScriptObject(){
			objetivo = "embarcar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
			linha2 = "Clique com o botão direito";
			linha3 = "para embarcar neste navio.";
			override = true;
		};
		
		case 21:
		$tut_campanha.passo21 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos1Quem; 
		};
		
		case 22:
		$tut_campanha.passo22 = new ScriptObject(){
			objetivo = "embarcar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
			linha1b = "Embarque neste navio.";
			linha2b = "Cada navio carrega até";
			linha3b = "2 soldados.";
			override = true;
		};
		
		case 23:
		$tut_campanha.passo23 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
			linha2b = "Selecione o navio carregado.";
			override = true;
		};
		
		case 24:
		$tut_campanha.passo24 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = oceanoAtlanticoSul;
			linha1b = "Clique com o botão direito";
			linha2b = "neste oceano para mover o";
			linha3b = "navio selecionado.";
			override = true;
		};
		
		case 25:
		$tut_campanha.passo25 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = oceanoIndico;
			linha1 = "Movimente o navio para";
			linha2 = "este oceano.";
			linha3 = "Oceanos podem ser ocupados,";
			linha4 = "mas não conquistados.";
			override = true;
		};
		
		case 26:
		$tut_campanha.passo26 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = bMarDeJava;
		};
		
		case 27:
		$tut_campanha.passo27 = new ScriptObject(){
			objetivo = "desembarcar";
			alvo = perth;
			linha2 = "Clique com o botão direito";
			linha3 = "para desembarcar um soldado.";
			override = true;
		};
		
		case 28:
		$tut_campanha.passo28 = new ScriptObject(){
			objetivo = "desembarcar";
			alvo = sydney;
			linha1 = "Desembarque o soldado restante";
			linha2 = "para conquistar a Austrália.";
			linha3 = "A Austrália lhe fornece";
			linha4 = "+1 Urânio por rodada.";
			override = true;
		};
		
		case 29:
		$tut_campanha.passo29 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_proximaRodada;
			titulo = "SUA RENDA";
			linha2 = "Aqui você pode ver qual será";
			linha3 = "sua renda na próxima rodada.";
			icon1 = "tutRecursosIcon";
			icon2 = "tutRecursosIcon";
		};
		
		case 30:
		$tut_campanha.passo30 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_meusRecursos;
			titulo = "SEU SALDO";
			linha1 = "Quantos Imperiais, Minérios,";
			linha2 = "Petróleos e Urânios você possui.";
			linha3 = "Os Recursos podem ser vendidos";
			linha4 = "ou convertidos em pontos.";
			icon1 = "tutRecursosIcon";
			icon2 = "tutRecursosIcon";
		};
		
		case 31:
		$tut_campanha.passo31 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_venderRecursos;
			titulo = "VENDER RECURSOS";
			linha1 = "Quando você possuir Recursos";
			linha2 = "suficientes este botão ficará ativo.";
			linha3 = "O Banco compra apenas certas";
			linha4 = "quantidades de Recursos.";
			icon1 = "tutRecursosIcon";
			icon2 = "tutRecursosIcon";
		};
		
		case 32:
		$tut_campanha.passo32 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_meusRecursos;
			titulo = "VENDENDO AO BANCO";
			linha1 = "O Banco paga 10 Imperiais por:";
			linha2 = "3 Urânios OU";
			linha3 = "4 Petróleos OU";
			linha4 = "5 Minérios.";
			icon1 = "tutRecursosIcon";
			icon2 = "tutRecursosIcon";
		};
		
		case 33:
		$tut_campanha.passo33 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_meusRecursos;
			titulo = "CONJUNTO";
			linha1 = "1 Minério + 1 Petróleo + 1 Urânio";
			linha2 = "formam um Conjunto de Recursos.";
			linha3 = "O Banco também paga 10 Imperiais";
			linha4 = "por 1 Conjunto de Recursos.";
			icon1 = "tutRecursosIcon";
			icon2 = "tutRecursosIcon";
		};
		
		case 34:
		$tut_campanha.passo34 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_meusRecursos;
			titulo = "RECURSOS E PONTOS";
			linha1 = "No fim do jogo, os Recursos que";
			linha2 = "não foram vendidos nem usados";
			linha3 = "nos objetivos são convertidos";
			linha4 = "automaticamente em pontos.";
			icon1 = "tutRecursosIcon";
			icon2 = "tutRecursosIcon";
		};
		
		case 35:
		$tut_campanha.passo35 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_meusRecursos;
			titulo = "VALOR DOS RECURSOS";
			linha1 = "Na mesma proporção, 5 Minérios";
			linha2 = "valem 1 ponto, 4 Petróleos valem";
			linha3 = "1 ponto, 3 Urânios valem 1 ponto";
			linha4 = "e 1 Conjunto vale 1 ponto.";
			icon1 = "tutRecursosIcon";
			icon2 = "tutRecursosIcon";
		};
		
		case 36:
		$tut_campanha.passo36 = new ScriptObject(){
			objetivo = "finalizarJogada";
		};
		
		case 37:
		$tut_campanha.passo37 = new ScriptObject(){
			objetivo = "missaoRecebida";
			alvo = kinshasa;
			bonus = "pontos";
			override = true;
			titulo = "MISSÃO RECEBIDA";
			linha1 = "Mais uma missão de construção:";
			linha2 = "ela lhe dará +2 pontos por";
			linha3 = "construir uma base aqui.";
			linha4 = "Bases custam 10 Imperiais.";
		};
		
		////////////////
		//Terceira Rodada:
		case 38:
		$tut_campanha.passo38 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos0Quem; 
		};
		
		case 39:
		$tut_campanha.passo39 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaTerrestreInicial;
			unidade = "soldado";
			qtd = 2;
		};
		
		case 40:
		$tut_campanha.passo40 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos1Quem; 
		};
		
		case 41:
		$tut_campanha.passo41 = new ScriptObject(){
			objetivo = "embarcar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
		};
		
		case 42:
		$tut_campanha.passo42 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos1Quem; 
		};
		
		case 43:
		$tut_campanha.passo43 = new ScriptObject(){
			objetivo = "embarcar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
		};
		
		case 44:
		$tut_campanha.passo44 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
		};
		
		case 45:
		$tut_campanha.passo45 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = oceanoAtlanticoSul;
		};
		
		case 46:
		$tut_campanha.passo46 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = bDeLuanda;
		};
		
		case 47:
		$tut_campanha.passo47 = new ScriptObject(){
			objetivo = "desembarcar";
			alvo = cidadeDoCabo;
			linha2 = "Clique com o botão direito";
			linha3 = "para desembarcar um soldado.";
			override = true;
		};
		
		case 48:
		$tut_campanha.passo48 = new ScriptObject(){
			objetivo = "desembarcar";
			alvo = kinshasa;
		};
		
		case 49:
		$tut_campanha.passo49 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos0Quem; 
		};
		
		case 50:
		$tut_campanha.passo50 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaMaritimaInicial;
			unidade = "navio";
			qtd = 1;
			linha1b = "Bases desprotegidas podem";
			linha2b = "ser capturadas. Proteja sua";
			linha3b = "base produzindo 1 navio.";
			override = true;
		};
		
		case 51:
		$tut_campanha.passo51 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos0Quem; 
		};
		
		case 52:
		$tut_campanha.passo52 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaTerrestreInicial;
			unidade = "tanque";
			qtd = 1;
			linha1 = "Tanques são mais fortes";
			linha2 = "e resistentes que soldados.";
			linha3 = "Proteja melhor esta base";
			linha4 = "produzindo 1 tanque.";
			override = true;
		};
		
		case 53:
		$tut_campanha.passo53 = new ScriptObject(){
			objetivo = "finalizarJogada";
		};
		
		case 54:
		$tut_campanha.passo54 = new ScriptObject(){
			objetivo = "missaoRecebida";
			alvo = bDeBaffin;
			bonus = "recurso";
			recurso = "petroleo";
			override = true;
			titulo = "MISSÃO DE CONQUISTA";
			linha1 = "Você descobriu petróleo!";
			linha2 = "Basta conquistar esta área";
			linha3 = "para começar a receber";
			linha4 = "+1 Petróleo por rodada.";
		};
		
		////////////////
		//Quarta Rodada:
		case 55:
		$tut_campanha.passo55 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos0Quem;
		};
		
		case 56:
		$tut_campanha.passo56 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaTerrestreInicial;
			unidade = "soldado";
			qtd = 2;
		};
		
		case 57:
		$tut_campanha.passo57 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.myPos3List.getObject(0); 
		};
		
		case 58:
		$tut_campanha.passo58 = new ScriptObject(){
			objetivo = "embarcar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
		};
		
		case 59:
		$tut_campanha.passo59 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.myPos3List.getObject(0); 
		};
		
		case 60:
		$tut_campanha.passo60 = new ScriptObject(){
			objetivo = "embarcar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
		};
		
		case 61:
		$tut_campanha.passo61 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
		};
		
		case 62:
		$tut_campanha.passo62 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = bDeDakar;
		};
				
		case 63:
		$tut_campanha.passo63 = new ScriptObject(){
			objetivo = "desembarcar";
			alvo = marrakesh;
		};
		
		case 64:
		$tut_campanha.passo64 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = marrakesh.pos1Quem; 
		};
		
		case 65:
		$tut_campanha.passo65 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = cairo;
		};	
		
		case 66:
		$tut_campanha.passo66 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = bDeDakar.pos1Quem;
		};
		
		case 67:
		$tut_campanha.passo67 = new ScriptObject(){
			objetivo = "desembarcar";
			alvo = marrakesh;
			linha1 = "Desembarque o soldado";
			linha2 = "nesta área.";
			linha3 = "A Árfrica lhe confere";
			linha4 = "+1 Urânio por rodada.";
			override = true;
		};
			
		case 68:
		$tut_campanha.passo68 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos0Quem;
		};		
		
		case 69:
		$tut_campanha.passo69 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaMaritimaInicial;
			unidade = "navio";
			qtd = 1;
			linha2 = "Proteja sua base";
			linha3 = "produzindo 1 navio.";
			override = true;
		};		
				
		case 70:
		$tut_campanha.passo70 = new ScriptObject(){
			objetivo = "finalizarJogada";
		};
		
		case 71:
		$tut_campanha.passo71 = new ScriptObject(){
			objetivo = "missaoRecebida";
			alvo = vancouver;
			bonus = "recurso";
			recurso = "minerio";
			override = true;
			titulo = "MISSÃO DE CONQUISTA";
			linha1 = "Você descobriu minério!";
			linha2 = "Basta conquistar esta área";
			linha3 = "para começar a receber";
			linha4 = "+1 Minério por rodada.";
		};
		
		//////////////////
		//Quinta Rodada:
		case 72:
		$tut_campanha.passo72 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos0Quem;
		};
	
		case 73:
		$tut_campanha.passo73 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaTerrestreInicial;
			unidade = "soldado";
			qtd = 2;
			linha1b = "Produza 2 soldados.";
			linha2b = "Eles serão levados para";
			linha3b = "o Canadá.";
			override = true;
		};
		
		case 74:
		$tut_campanha.passo74 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.myPos3List.getObject(0);
		};
		
		case 75:
		$tut_campanha.passo75 = new ScriptObject(){
			objetivo = "embarcar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
		};
		
		case 76:
		$tut_campanha.passo76 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.myPos3List.getObject(0);
		};
		
		case 77:
		$tut_campanha.passo77 = new ScriptObject(){
			objetivo = "embarcar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
		};
		
		case 78:
		$tut_campanha.passo78 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem;
		};
		
		case 79:
		$tut_campanha.passo79 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = oceanoAtlanticoNorte;
		};	
		
		case 80:
		$tut_campanha.passo80 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = bDeHudson;
		};	
		
		case 81:
		$tut_campanha.passo81 = new ScriptObject(){
			objetivo = "desembarcar";
			alvo = toronto;
		};
		
		case 82:
		$tut_campanha.passo82 = new ScriptObject(){
			objetivo = "desembarcar";
			alvo = montreal;
		};
				
		case 83:
		$tut_campanha.passo83 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = bDeBaffin;
			linha1 = "Movimente o navio para";
			linha2 = "esta área. Você ativará a";
			linha3 = "missão e passará a receber";
			linha4 = "+1 Petróleo por rodada.";
			override = true;
		};	
		
		case 84:
		$tut_campanha.passo84 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos0Quem; 
		};
		
		case 85:
		$tut_campanha.passo85 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaMaritimaInicial;
			unidade = "navio";
			qtd = 1;
		};
		
		case 86:
		$tut_campanha.passo86 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos0Quem;
		};	
		
		case 87:
		$tut_campanha.passo87 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaTerrestreInicial;
			unidade = "tanque";
			qtd = 1;
			linha1b = "Produza mais 1 tanque.";
			linha2b = "Com 2 tanques sua base fica";
			linha3b = "mais protegida.";
			override = true;
		};
		
		case 88:
		$tut_campanha.passo88 = new ScriptObject(){
			objetivo = "finalizarJogada";
		};
		
		case 89:
		$tut_campanha.passo89 = new ScriptObject(){
			objetivo = "missaoRecebida";
			alvo = pequim;
			bonus = "recurso";
			recurso = "uranio";
			override = true;
			titulo = "MISSÃO DE CONQUISTA";
			linha1 = "Você descobriu urânio!";
			linha2 = "É possível negociar missões";
			linha3 = "de conquista, fazendo acordos";
			linha4 = "de exploração.";
		};
		
		//////////////////////
		//Sexta rodada:
		case 90:
		$tut_campanha.passo90 = new ScriptObject(){
			objetivo = "btnClick";
			alvo = dummy_acordosPossiveis;
			key = "acordosPossiveisClick";
			override = true;
			titulo = "FAZENDO ACORDOS";
			linha2 = "Clique aqui para visualizar";
			linha3 = "os acordos possíveis.";
		};
		
		case 91:
		$tut_campanha.passo91 = new ScriptObject(){
			objetivo = "btnClick";
			alvo = pequim.pos1Quem;
			key = "enviarExplClick";
			override = true;
			titulo = "ENVIAR ACORDO";
			linha2 = "Clique aqui para enviar";
			linha3 = "a missão ao Adversário.";
		};
		
		case 92:
		$tut_campanha.passo92 = new ScriptObject(){
			objetivo = "look";
			alvo = pequim.pos1Quem;
			titulo = "ACORDO EFETUADO";
			linha1 = "Você enviou a missão e está";
			linha2 = "compartilhando este Urânio.";
			linha3 = "A cada duas rodadas você";
			linha4 = "receberá +1 Urânio.";
			icon1 = "tutUranioIcon";
			icon2 = "tutUranioIcon";
		};
		
		case 93:
		$tut_campanha.passo93 = new ScriptObject(){
			objetivo = "btnClick";
			alvo = dummy_chatBtn;
			key = "chatBtnClick";
			override = true;
			titulo = "VER ACORDOS";
			linha1b = "Clique aqui para fechar";
			linha2b = "o chat e abrir a janela.";
			linha3b = "de acordos efetuados.";
		};
		
		case 94:
		$tut_campanha.passo94 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_primeiroAcordoEnviado;
			titulo = "ACORDOS";
			linha1b = "Nesta janela você visualiza";
			linha2b = "todos os acordos de exploração";
			linha3b = "feitos durante a partida.";
			icon1 = "tutTresAcordosIcon";
			icon2 = "tutTresAcordosIcon";
		};
				
		case 95:
		$tut_campanha.passo95 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos0Quem; 
		};
		
		case 96:
		$tut_campanha.passo96 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaMaritimaInicial;
			unidade = "navio";
			qtd = 1;
		};
		
		case 97:
		$tut_campanha.passo97 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.pos0Quem; 
		};
		
		case 98:
		$tut_campanha.passo98 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaTerrestreInicial;
			unidade = "soldado";
			qtd = 2;
		};
		
		case 99:
		$tut_campanha.passo99 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.myPos3List.getObject(0); 
		};
		
		case 100:
		$tut_campanha.passo100 = new ScriptObject(){
			objetivo = "embarcar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
		};
		
		case 101:
		$tut_campanha.passo101 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaTerrestreInicial.myPos3List.getObject(0); 
		};
		
		case 102:
		$tut_campanha.passo102 = new ScriptObject(){
			objetivo = "embarcar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem; 
		};
		
		case 103:
		$tut_campanha.passo103 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos1Quem;
		};
		
		case 104:
		$tut_campanha.passo104 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = oceanoAtlanticoNorte;
		};	
		
		case 105:
		$tut_campanha.passo105 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = bDeHudson;
		};	
		
		case 106:
		$tut_campanha.passo106 = new ScriptObject(){
			objetivo = "desembarcar";
			alvo = bakerlake;
		};
		
		case 107:
		$tut_campanha.passo107 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = bakerlake.pos1Quem;
		};
		
		case 108:
		$tut_campanha.passo108 = new ScriptObject(){
			objetivo = "conquistar";
			alvo = vancouver;
			override = true; 
			linha1 = "Movimente o soldado para";
			linha2 = "esta área. Você ativará a";
			linha3 = "missão e passará a receber";
			linha4 = "+1 Minério por rodada.";
		};	
		
		case 109:
		$tut_campanha.passo109 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = bDeHudson.pos1Quem;
		};
		
		case 110:
		$tut_campanha.passo110 = new ScriptObject(){
			objetivo = "desembarcar";
			alvo = bakerlake;
			linha1 = "Desembarque um soldado para";
			linha2 = "conquistar o Canadá.";
			linha3 = "Você passará a receber";
			linha4 = "+1 Urânio por rodada.";
			override = true; 
		};
		
		case 111:
		$tut_campanha.passo111 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = $myTUTAreaMaritimaInicial.pos0Quem;
		};
		
		case 112:
		$tut_campanha.passo112 = new ScriptObject(){
			objetivo = "produzir";
			alvo = $myTUTAreaMaritimaInicial;
			unidade = "navio";
			qtd = 1;
		};
		
		case 113:
		$tut_campanha.passo113 = new ScriptObject(){
			objetivo = "finalizarJogada";
		};
		
		case 114:
		$tut_campanha.passo114 = new ScriptObject(){
			objetivo = "missaoRecebida";
			alvo = comunidadeEuropeia;
			bonus = "pontos";
			override = true;
			titulo = "MISSÃO RECEBIDA";
			linha1 = "Missões de construção também";
			linha2 = "podem ser negociadas.";
			linha3 = "Normalmente, é melhor negociar";
			linha4 = "do que entrar em guerra.";
		};
		
		////////////
		//Última Rodada (7ª):
		case 115:
		$tut_campanha.passo115 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_meuObj1;
			titulo = "OBJETIVO COMPLETO";
			linha1 = "Parabéns!!";
			linha2 = "Você completou um dos seus";
			linha3 = "objetivos e já pode bater!";
			linha4 = "Mas antes...";
			icon1 = "tutBrasilIcon";
			icon2 = "tutBrasilIcon";
		};
		
		case 116:
		$tut_campanha.passo116 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = kinshasa.pos1Quem;
		};
		
		case 117:
		$tut_campanha.passo117 = new ScriptObject(){
			objetivo = "produzir";
			alvo = kinshasa;
			unidade = "base";
			qtd = 1;
			override = true;
			titulo = "CONSTRUIR BASE";
			linha1 = "Clique aqui para construir";
			linha2 = "uma base nesta área.";
			linha3 = "Você ativará a missão e";
			linha4 = "receberá 2 pontos.";
		};
		
		case 118:
		$tut_campanha.passo118 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_meusRecursos;
			titulo = "SITUAÇÃO";
			linha1 = "Bases custam 10 Imperiais,";
			linha2 = "mas você possui apenas 4.";
			linha3 = "Seu objetivo exige 8 urânios,";
			linha4 = "e você já possui 9.";
			icon1 = "tutRecursosIcon";
			icon2 = "tutRecursosIcon";
		};
		
		case 119:
		$tut_campanha.passo119 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_meusRecursos;
			titulo = "CONJUNTO";
			linha1 = "Vendendo 1 minério +";
			linha2 = "1 petróleo + 1 urânio";
			linha3 = "ao Banco você recebe";
			linha4 = "10 Imperiais.";
			icon1 = "tutRecursosIcon";
			icon2 = "tutRecursosIcon";
		};
		
		case 120:
		$tut_campanha.passo120 = new ScriptObject(){
			objetivo = "btnClick";
			alvo = dummy_venderRecursos;
			key = "venderRecursosClick";
			override = true;
			titulo = "VENDA RECURSOS";
			linha2 = "Clique aqui para abrir o";
			linha3 = "menu de venda de Recursos.";
		};
		
		case 121:
		$tut_campanha.passo121 = new ScriptObject(){
			objetivo = "btnClick";
			alvo = dummy_venderConjunto;
			key = "venderConjuntoClick";
			override = true;
			titulo = "VENDER RECURSOS";
			linha1b = "Clique aqui para vender";
			linha2b = "ao Banco 1 Conjunto de";
			linha3b = "Recursos por 10 Imperiais.";
		};
		
		case 122:
		$tut_campanha.passo122 = new ScriptObject(){
			objetivo = "selecionar";
			alvo = bMarDeJava.pos1Quem;
		};
		
		case 123:
		$tut_campanha.passo123 = new ScriptObject(){
			objetivo = "produzir";
			alvo = bMarDeJava;
			unidade = "base";
			qtd = 1;
			override = true;
			titulo = "CONSTRUIR BASE";
			linha1 = "Clique aqui para construir";
			linha2 = "uma base nesta área.";
			linha3 = "Você ativará a missão e";
			linha4 = "receberá 2 pontos.";
		};
		
		case 124:
		$tut_campanha.passo124 = new ScriptObject(){
			objetivo = "btnClick";
			alvo = dummy_acordosPossiveis;
			key = "acordosPossiveisClick";
			override = true;
			titulo = "FAZENDO ACORDOS";
			linha2 = "Clique aqui para visualizar";
			linha3 = "os acordos possíveis.";
		};
		
		case 125:
		$tut_campanha.passo125 = new ScriptObject(){
			objetivo = "btnClick";
			alvo = comunidadeEuropeia.pos0Quem;
			key = "enviarExplClick";
			override = true;
			titulo = "ENVIAR PROPOSTA";
			linha2 = "Clique aqui para enviar";
			linha3 = "uma proposta de troca.";
			icon1 = "tutPropostaIcon";
			icon2 = "tutPropostaIcon";
		};
		
		case 126:
		$tut_campanha.passo126 = new ScriptObject(){
			objetivo = "btnClick";
			alvo = dummy_propTab;
			key = "propTabClick";
			titulo = "PROPOSTAS";
			linha1 = "Você enviou a proposta.";
			linha2 = "Clique aqui para visualizar";
			linha3 = "todas as propostas enviadas";
			linha4 = "e recebidas.";
			icon1 = "tutPropostaIcon";
			icon2 = "tutPropostaIcon";
			command = "clientForcePropAlert";
		};
		
		case 127:
		$tut_campanha.passo127 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_primeiraProposta;
			titulo = "PROPOSTA ENVIADA";
			linha2 = "Clique aqui para visualizar";
			linha3 = "a proposta enviada.";
			icon1 = "tutPropostaIcon";
			icon2 = "tutPropostaIcon";
		};
		
		case 128:
		$tut_campanha.passo128 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_primeiraProposta;
			titulo = "PROPOSTAS";
			linha1 = "Quando seu adversário tiver";
			linha2 = "uma missão de construção em";
			linha3 = "uma área que pertence a você,";
			linha4 = "ele enviará uma resposta.";
			icon1 = "tutPropostaIcon";
			icon2 = "tutPropostaIcon";
		};
		
		case 129:
		$tut_campanha.passo129 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_primeiraProposta;
			titulo = "PROPOSTAS";
			linha1 = "Enquanto não houver resposta,";
			linha2 = "a missão não é enviada ao";
			linha3 = "adversário. A proposta pode ser";
			linha4 = "cancelada a qualquer momento.";
			icon1 = "tutPropostaIcon";
			icon2 = "tutPropostaIcon";
		};
		
		case 130:
		$tut_campanha.passo130 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_primeiraProposta;
			titulo = "RESPOSTA RECEBIDA";
			linha1 = "Clique aqui para abrir esta";
			linha2 = "proposta e ver a resposta.";
			linha3 = "Clique em ACEITAR para";
			linha4 = "efetuar a troca.";
			command = "TUTaiEnviarResposta";
			icon1 = "tutPropostaIcon";
			icon2 = "tutPropostaIcon";
		};
		
		case 131:
		$tut_campanha.passo131 = new ScriptObject(){
			objetivo = "look";
			alvo = saoPaulo.pos0Quem;
			titulo = "TROCA EFETUADA";
			linha1 = "Você trocou a missão com";
			linha2 = "seu adversário. Nem é preciso";
			linha3 = "construir uma base aqui, pois";
			linha4 = "você já possui uma.";
			icon1 = "tutPropostaIcon";
			icon2 = "tutPropostaIcon";
		};
		
		case 132:
		$tut_campanha.passo132 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_pontos;
			titulo = "SEUS PONTOS";
			linha1 = "Neste momento, você possui";
			linha2 = "esta quantidade de pontos.";
			linha3 = "Deve ser o suficiente para";
			linha4 = "vencer a partida.";
			icon1 = "tut12PtIcon";
			icon2 = "tut12PtIcon";
		};
		
		case 133:
		$tut_campanha.passo133 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_pontos;
			titulo = "PONTOS";
			linha1 = "São 6 pontos pelas missões";
			linha2 = "de construção.";
			linha3 = "Mais 5 pontos por ter";
			linha4 = "completado um objetivo.";
			icon1 = "tut12PtIcon";
			icon2 = "tut12PtIcon";
		};
		
		case 134:
		$tut_campanha.passo134 = new ScriptObject(){
			objetivo = "look";
			alvo = dummy_pontos;
			titulo = "RECURSOS";
			linha1 = "Seu 12º ponto é fruto";
			linha2 = "dos recursos que não foram";
			linha3 = "vendidos nem usados no";
			linha4 = "seu objetivo.";
			icon1 = "tut12PtIcon";
			icon2 = "tut12PtIcon";
		};
		
		case 135:
		$tut_campanha.passo135 = new ScriptObject(){
			objetivo = "bater";
			linha1 = "Você aprendeu as regras";
			linha2 = "básicas do jogo.";
			linha3 = "Clique aqui para BATER,";
			linha4 = "finalizando a partida.";
			override = true;
		};
	}
	%eval = "%myPasso = $tut_campanha.passo" @ %passoNum @ ";";
	eval(%eval);
	
	%myPasso.num = %passoNum;
	
	$tut_campanha.myPassos.add(%myPasso);
	
	if(%myPasso.objetivo $= "conquistar"){
		%myPasso.titulo = "MOVER";
		%myPasso.icon1 = "tutMouseIcon";
		%myPasso.icon2 = "tutMouseIconDir";	
		if(!%myPasso.override){
			%myObjClass = Foco.getObject(0).class;
			if(%myObjClass $= "Soldado"){
				%myObjClass = "soldado";	
			}
			if(%myPasso.alvo.oceano != 1){
				if(%myPasso.alvo.terreno $= "terra"){
					%myPasso.linha2 = "Movimente o " @ %myObjClass @ " para";
					%myPasso.linha3 = "esta área.";
				} else {
					%myPasso.linha2 = "Movimente o " @ %myObjClass @ " para";
					%myPasso.linha3 = "esta área marítima.";
				}
			} else {
				%myPasso.linha2 = "Movimente o " @ %myObjClass @ " para";
				%myPasso.linha3 = "este oceano.";
			}
		}
	} else if(%myPasso.objetivo $= "selecionar"){
		%myPasso.titulo = "SELECIONAR";
		%myPasso.icon1 = "tutMouseIcon";
		%myPasso.icon2 = "tutMouseIconNormal";
		if(!%myPasso.override){
			if(%myPasso.alvo.class $= "base"){
				%myPasso.linha2b = "Selecione esta base.";	
			} else {
				%myClass = %myPasso.alvo.class;
				if(%myClass $= "Soldado"){
					%myClass = "soldado";	
				} 
				%myPasso.linha2b = "Selecione este " @ %myClass @ ".";
			}
		}
	} else if(%myPasso.objetivo $= "produzir"){
		%myPasso.titulo = "PRODUZIR";
		%myPasso.icon1 = "tutMouseIcon";
		%myPasso.icon2 = "tutMouseIconNormal";
		if(!%myPasso.override){
			if(%myPasso.qtd == 1){
				%myPlural = "";	
			} else {
				%myPlural = "s";	
			}
			%myPasso.linha2b = "Produza " @ %myPasso.qtd SPC %myPasso.unidade @ %myPlural @ ".";	
		}
	} else if(%myPasso.objetivo $= "embarcar"){
		%myPasso.titulo = "EMBARCAR";
		%myPasso.icon1 = "tutMouseIcon";
		%myPasso.icon2 = "tutMouseIconDir";
		if(!%myPasso.override){
			%myPasso.linha2b = "Embarque neste navio.";	
		}
	}  else if(%myPasso.objetivo $= "desembarcar"){
		%myPasso.titulo = "DESEMBARCAR";
		%myPasso.icon1 = "tutMouseIcon";
		%myPasso.icon2 = "tutMouseIconDir";
		if(!%myPasso.override){
			%myPasso.linha2b = "Desembarque nesta área.";	
		}
	} else if(%myPasso.objetivo $= "finalizarJogada"){
		%myPasso.titulo = "FINALIZAR JOGADA";
		%myPasso.icon1 = "tutMouseIcon";
		%myPasso.icon2 = "tutMouseIconNormal";
		%myPasso.linha1 = "";
		%myPasso.linha2 = "Clique para finalizar";
		%myPasso.linha3 = "sua jogada.";
		%myPasso.linha4 = "";
		%myPasso.alvo = dummy_finalizarJogadaBTN;
	} else if(%myPasso.objetivo $= "missaoRecebida"){
		%myPasso.titulo = "MISSÃO RECEBIDA";
		%myPasso.timedMsg = true;
		%myPasso.time = 10000; //ms
		if(%myPasso.bonus $= "pontos"){
			%myPasso.icon1 = "tutConstruaIcon";
			%myPasso.icon2 = "tutConstruaIcon";
			if(!%myPasso.override){
				%myPasso.linha1 = "Missão de Construção:";
				%myPasso.linha2 = "Construa uma base nesta área";
				%myPasso.linha3 = "para receber +2 pontos ao";
				%myPasso.linha4 = "final da partida.";
			}
		} else {
			%myPasso.icon1 = "tut" @ %myPasso.recurso @ "Icon";
			%myPasso.icon2 = "tut" @ %myPasso.recurso @ "Icon";
			if(!%myPasso.override){
				%myPasso.linha1 = "Missão de Conquista:";
				%myPasso.linha2 = "Conquiste esta área para";
				%myPasso.linha3 = "receber +1 " @ %myPasso.recurso; 
				%myPasso.linha4 = "por rodada.";
			}
		}
	} else if(%myPasso.objetivo $= "bater"){
		%myPasso.titulo = "BATER";
		%myPasso.icon1 = "tutMouseIcon";
		%myPasso.icon2 = "tutMouseIconNormal";
		%myPasso.alvo = dummy_baterBTN;
		if(!%myPasso.override){
			%myPasso.linha1 = "Parabéns! Você completou um";
			%myPasso.linha2 = "dos seus objetivos!";
			%myPasso.linha3 = "Clique aqui para BATER,";
			%myPasso.linha4 = "finalizando o jogo.";
		}
	} else if(%myPasso.objetivo $= "look"){
		//%myPasso.icon1 = "tutLookIcon";
		//%myPasso.icon2 = "tutLookIconHi";
		%myPasso.timedMsg = true;
		%myPasso.time = 10000; //ms	
	} else if (%myPasso.objetivo $= "btnClick"){
		%myPasso.icon1 = "tutMouseIcon";
		%myPasso.icon2 = "tutMouseIconNormal";
	}
}

function tut_marcarObjetivo(%objetivo, %alvo, %unidade, %qtd, %key){
	$tut_objetivo.objetivo = %objetivo;
	$tut_objetivo.alvo = %alvo;
	$tut_objetivo.unidade = %unidade;
	$tut_objetivo.qtd = %qtd;
	$tut_objetivo.key = %key;
}

function tut_verificarObjetivo(%producao, %unitClass){
	if($tut_objetivo.objetivo $= "selecionar"){
		if(Foco.getObject(0) == $tut_objetivo.alvo){
			tut_completarObjetivo();	
		}
	} else if ($tut_objetivo.objetivo $= "conquistar" || $tut_objetivo.objetivo $= "desembarcar"){
		if($mySelf.mySimAreas.isMember($tut_objetivo.alvo)){ //|| ($tut_objetivo.alvo.oceano == 1 && $tut_objetivo.alvo.pos1Quem.dono == $mySelf)){
			tut_completarObjetivo();	
		}
	}  else if ($tut_objetivo.objetivo $= "embarcar"){
		if(%unitClass $= $tut_objetivo.alvo.pos){
			tut_completarObjetivo();	
		}
	} else if ($tut_objetivo.objetivo $= "produzir"){
		if(%producao){
			if(%unitClass $= $tut_objetivo.unidade){
				%eval = "$tut_producao" @ %unitClass @ "++;";
				eval(%eval);
				%eval = "%myProducao = $tut_producao" @ %unitClass @ ";";
				eval(%eval);
				if(%myProducao >= $tut_objetivo.qtd){
					tut_completarObjetivo();		
				}
			}
		}
	} else if ($tut_objetivo.objetivo $= "finalizarJogada"){
		if(%unitClass $= "finalizarJogada"){
			tut_completarObjetivo();	
		}
	} else if ($tut_objetivo.objetivo $= "dummy"){
		tut_completarObjetivo();	
	} else if ($tut_objetivo.objetivo $= "btnClick"){
		if(%unitClass $= $tut_objetivo.key){
			tut_completarObjetivo();
		}
	}
}

function tut_completarObjetivo(){
	tut_clearTips();
	if($tut_campanha.passo == 0){
		$tut_campanha.passo = $tut_campanha.passo1;
	} else {
		tut_execPasso($tut_campanha.passo.num+1);
		%eval = "$tut_campanha.passo = $tut_campanha.passo" @ $tut_campanha.passo.num+1 @ ";";	
		eval(%eval);
	}
	echo("tut_Objetivo Completo! Passando para o próximo (" @ $tut_campanha.passo.num @ ") tut_Objetivo.");
		
	%novoObjetivo = $tut_campanha.passo.objetivo;
	if($tut_campanha.passo.objetivo $= "produzir"){
		if($tut_campanha.passo.unidade $= "base"){
			%novoAlvo = dummy_tanqueBTN;
		} else {
			%eval = "%novoAlvo = dummy_" @ $tut_campanha.passo.unidade @ "BTN;";
			eval(%eval);
		}
	} else {
		%novoAlvo = $tut_campanha.passo.alvo;
	}
	%novaUnidade = $tut_campanha.passo.unidade;
	%novaQtd = $tut_campanha.passo.qtd;
		
	tut_marcarObjetivo(%novoObjetivo, %novoAlvo, %novaUnidade, %novaQtd, $tut_campanha.passo.key);
	if($tut_campanha.passo.num == 1){
		%myTime = 1000;
	} else {
		%myTime = 800;	
	}
	if(isObject($tut_campanha.passo)){
		if($mySelf == $jogadorDaVez || $tut_campanha.passo.objetivo $= "missaoRecebida"){
			schedule(%myTime, 0, "tut_arrowOn", %novoAlvo, $tut_campanha.passo.titulo, $tut_campanha.passo.linha1, $tut_campanha.passo.linha2, $tut_campanha.passo.linha3, $tut_campanha.passo.linha4, $tut_campanha.passo.linha1b, $tut_campanha.passo.linha2b, $tut_campanha.passo.linha3b, $tut_campanha.passo.icon1, $tut_campanha.passo.icon2);
			if($tut_campanha.passo.timedMsg){
				schedule($tut_campanha.passo.time, 0, "tut_completarObjetivo");	
			}
			//se houver um comando a ser executado, executa:
			if($tut_campanha.passo.command !$= ""){
				%eval = $tut_campanha.passo.command @ "();";
				eval(%eval);
			}
		}
	} else {
		echo("FIM DOS OBJETIVOS DE TUTORIAL");
		tut_clearTips();
	}
}

function tut_fecharJogoTutorial(){
	for(%i = 0; %i < $tut_campanha.myPassos.getCount(); %i++){
		$tut_campanha.myPassos.getObject(0).delete();	
	}
	$tut_campanha.delete();
	$estouNoTutorial = false;
	$primeiraRodada = false;
	$jahInicieiTutObjetivos = false;
	
	//apaga dialogs que poderia estar abertos:
	Canvas.popDialog(objetivosGuii);
	Canvas.popDialog(escolhaDeCores);
	Canvas.popDialog(aguardandoObjGui);
	
	clientUnloadGame();
	
	canvas.pushDialog(loggedInGui);
	TUTmsgClose();
	TUTintroClose();
	tut_clearTips();
	
	if($myPersona.TAXOvitorias > 0){
		loggedInAcademia_btn.setActive(true);	
	} else {
		loggedInAcademia_btn.setActive(false);	
	}
	clientPopServerComDot();
	commandToServer('saiDoTutorial');
}

function tut_showFinal(){
	tut_bater1_img.setVisible(true);	
	tut_bater2_img.setVisible(true);
	clientPopServerComDot();
}
function tut_apagarFinal(){
	tut_bater1_img.setVisible(false);	
	tut_bater2_img.setVisible(false);	
}

function clientCmdMarcarTaxoTutorial(){
	if($myPersona.taxoTutorial < 1){
		$myPersona.taxoCreditos += 5;	
	}
	$myPersona.taxoTutorial = 1;
	loggedInCreditos_txt.text = $myPersona.TAXOcreditos;
}