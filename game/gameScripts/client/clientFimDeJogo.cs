// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientFimDeJogo.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 2 de janeiro de 2008 14:14
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskBater(){
	if(!$estouNoTutorial){
		commandToServer('bater', $mySelf.nome);
	} else {
		if($tut_campanha.passo.objetivo $= "bater"){
			%dados = "2";
			%dados = %dados SPC $myPersona.nome;
			%dados = %dados SPC "?";
			%dados = %dados SPC 0;
			%dados = %dados SPC 0;
			
			%dadosP1 = $myPersona.nome;
			%dadosP1 = %dadosP1 SPC "Brasil+Urânio";
			%dadosP1 = %dadosP1 SPC "5";
			%dadosP1 = %dadosP1 SPC "Rússia+Mar";
			%dadosP1 = %dadosP1 SPC "0"; //objPt	
			%dadosP1 = %dadosP1 SPC "0"; //impérioPt	
			%dadosP1 = %dadosP1 SPC "6"; //missõesPt
			%dadosP1 = %dadosP1 SPC "1"; //recursosPt		
			%dadosP1 = %dadosP1 SPC "12"; //totalPt		
			
			%dadosP1 = %dadosP1 SPC "0"; //creditos	total / fichas total	
			%dadosP1 = %dadosP1 SPC "3"; //vitoria (creds)	
			%dadosP1 = %dadosP1 SPC "0"; //visionario (creds)	
			%dadosP1 = %dadosP1 SPC "0"; //arrebatador (creds)	
			%dadosP1 = %dadosP1 SPC "2"; //comerciante (creds)
			%dadosP1 = %dadosP1 SPC "2"; //diplomata (creds)	
			
			%dadosP1 = %dadosP1 SPC "100"; //ob1Percent	
			%dadosP1 = %dadosP1 SPC "20"; //ob2Percent	
			%dadosP1 = %dadosP1 SPC "40"; //imperioPercent	
			
			
			//Adversário:
			%dadosP2 = "Adversário";
			%dadosP2 = %dadosP2 SPC "Oriente+Minério";
			%dadosP2 = %dadosP2 SPC "0";
			%dadosP2 = %dadosP2 SPC "Rússia+Petróleo";
			%dadosP2 = %dadosP2 SPC "0"; //objPt	
			%dadosP2 = %dadosP2 SPC "0"; //impérioPt	
			%dadosP2 = %dadosP2 SPC "2"; //missõesPt
			%dadosP2 = %dadosP2 SPC "4"; //recursosPt		
			%dadosP2 = %dadosP2 SPC "6"; //totalPt		
			
			%dadosP2 = %dadosP2 SPC "0"; //creditos		
			%dadosP2 = %dadosP2 SPC "0"; //vitoria	
			%dadosP2 = %dadosP2 SPC "0"; //creditos	
			%dadosP2 = %dadosP2 SPC "0"; //creditos	
			%dadosP2 = %dadosP2 SPC "2"; //creditos	
			%dadosP2 = %dadosP2 SPC "2"; //creditos	
			
			%dadosP2 = %dadosP2 SPC "80"; //ob1Percent	
			%dadosP2 = %dadosP2 SPC "85"; //ob2Percent	
			%dadosP2 = %dadosP2 SPC "30"; //imperioPercent	
			
			clientCmdFimDeJogo(%dados, true, %dadosP1, %dadosP2, -1, -1, -1, -1, true);
			
			if($myPersona.taxoTutorial < 1){
				commandToServer('MarcarTaxoTutorial');
			}
		}
	}
}

function clientZerarBaterGui(){
	tut_apagarFinal();
	baterTab_esq.setVisible(false);
	baterTab_dir.setVisible(false);
	baterTabTxt_dir.setVisible(false);
	baterTabTxt_esq.setVisible(false);
	%xBitmap = "game/data/images/Xcreditos.png";
	for (%i = 1; %i < 7; %i++){
		%eval = "p" @ %i @ "Obj1Tick.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "Obj2Tick.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "ImperioTick.setVisible(false);";
		eval(%eval);
		%eval = "vitoriaMark" @ %i @ ".setVisible(false);";
		eval(%eval);		
		%eval = "p" @ %i @ "BaterPersonaGui.setVisible(false);";
		eval(%eval);
		//Créditos:
		%eval = "p" @ %i @ "cBatida.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "cVencedor.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "cVisionario.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "cArrebatador.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "cComerciante.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "cDiplomata.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "cBatidaX.setVisible(true);";
		eval(%eval);
		%eval = "p" @ %i @ "cVencedorX.setVisible(true);";
		eval(%eval);
		%eval = "p" @ %i @ "cVisionarioX.setVisible(true);";
		eval(%eval);
		%eval = "p" @ %i @ "cArrebatadorX.setVisible(true);";
		eval(%eval);
		%eval = "p" @ %i @ "cComercianteX.setVisible(true);";
		eval(%eval);
		%eval = "p" @ %i @ "cDiplomataX.setVisible(true);";
		eval(%eval);
		%eval = "p" @ %i @ "cBatidaX.bitmap = %xBitmap;";
		eval(%eval);
		%eval = "p" @ %i @ "cVencedorX.bitmap = %xBitmap;";
		eval(%eval);
		%eval = "p" @ %i @ "cVisionarioX.bitmap = %xBitmap;";
		eval(%eval);
		%eval = "p" @ %i @ "cArrebatadorX.bitmap = %xBitmap;";
		eval(%eval);
		%eval = "p" @ %i @ "cComercianteX.bitmap = %xBitmap;";
		eval(%eval);
		%eval = "p" @ %i @ "cDiplomataX.bitmap = %xBitmap;";
		eval(%eval);
		
		%eval = "p" @ %i @ "pokerGame_batida.setVisible(false);";
		eval(%eval);
		%eval = "vitoriaMark" @ %i @ "_pk.setVisible(false);";
		eval(%eval);	
	}
}


function clientCmdFimDeJogo(%dados, %valido, %dadosP1, %dadosP2, %dadosP3, %dadosP4, %dadosP5, %dadosP6, %tutorial, %assassino){
	//marca que o cara já jogou e está voltando pra sala, os gauges precisam ser re-calculados com novos dados dependendo da resolução de tela:
	$primeiraSalaInside = false;
	$inGame = false;
	cancel($pk_piscarPlayerShedule);
	PalcoTurnoTimer.pauseTimer(true);
	
	
	echo("DADOS RECEBIDOS PARA FIM DE PARTIDA:");
	echo(%dados);
	echo("%dadosP1: " @ %dadosP1);
	echo("%dadosP2: " @ %dadosP2);
	echo("%dadosP3: " @ %dadosP3);
	echo("%dadosP4: " @ %dadosP4);
	echo("%dadosP5: " @ %dadosP5);
	echo("%dadosP6: " @ %dadosP6);
		
	%numDePlayers = getWord(%dados, 0);
	%quemBateu = getWord(%dados, 1);
	%tempoDeJogo = getWord(%dados, 2);
	%desastres = getWord(%dados, 3);
	%creditosDistribuidos = getWord(%dados, 4);
	
	//first things first:
	clientZerarBaterGui(); //apaga qualquer batida de jogo anterior
	palcoTurnoTimer.setTimerOff(); //desliga o timer
	fimDeJogo_img.bitmap = "~/data/images/baterGui" @ %numDePlayers @ "p.jpg"; //seta a imagem de fundo conforme o número de players
	
	clientMarcarDadosBatida(%numDePlayers, %quemBateu, %dadosP1, %dadosP2, %dadosP3, %dadosP4, %dadosP5, %dadosP6);	
	
	clientVerificarObjetivoDescGulok();
	
	%vencedores = new SimSet();
		
	if(%valido > 0){
		//marcar a vitória:
		for(%i = 1; %i < %numDePlayers + 1; %i++){
			%eval = "%tp" @ %i @ " = getWord(%dadosP" @ %i @ ", 8);";	
			eval(%eval);
			echo("%tp1 = " @ %tp1 @ ", %tp2 = " @ %tp2 @ ", %tp3 = " @ %tp3 @ ", %tp4 = " @ %tp4 @ ", %tp5 = " @ %tp5 @ ", %tp6 = " @ %tp6);
		}
		
		if($jogoEmDuplas){
			if(%numDePlayers == 4){
				%dupla1Result = %tp1 + %tp3;
				%dupla2Result = %tp2 + %tp4;
				totalPtP1_txt.text = %tp1 SPC "/" SPC %dupla1Result;	
				totalPtP2_txt.text = %tp2 SPC "/" SPC %dupla2Result;	
				totalPtP3_txt.text = %tp3 SPC "/" SPC %dupla1Result;	
				totalPtP4_txt.text = %tp4 SPC "/" SPC %dupla2Result;	
				if(%dupla1Result >= %dupla2Result && %dupla1Result > 0){
					vitoriaMark1.setVisible(true);
					vitoriaMark3.setVisible(true);
					%vencedores.add($player1);
					%vencedores.add($player3);
				}
				if(%dupla2Result >= %dupla1Result && %dupla2Result > 0){
					vitoriaMark2.setVisible(true);
					vitoriaMark4.setVisible(true);
					%vencedores.add($player2);
					%vencedores.add($player4);
				}
			} else if(%numDePlayers == 6){
				%dupla1Result = %tp1 + %tp4;
				%dupla2Result = %tp2 + %tp5;
				%dupla3Result = %tp3 + %tp6;
				totalPtP1_txt.text = %tp1 SPC "/" SPC %dupla1Result;	
				totalPtP2_txt.text = %tp2 SPC "/" SPC %dupla2Result;	
				totalPtP3_txt.text = %tp3 SPC "/" SPC %dupla3Result;	
				totalPtP4_txt.text = %tp4 SPC "/" SPC %dupla1Result;	
				totalPtP5_txt.text = %tp5 SPC "/" SPC %dupla2Result;	
				totalPtP6_txt.text = %tp6 SPC "/" SPC %dupla3Result;	
				if(%dupla1Result >= %dupla2Result && %dupla1Result >= %dupla3Result && %dupla1Result > 0){
					vitoriaMark1.setVisible(true);
					vitoriaMark4.setVisible(true);
					%vencedores.add($player1);
					%vencedores.add($player4);
				}
				if(%dupla2Result >= %dupla1Result && %dupla2Result >= %dupla3Result && %dupla2Result > 0){
					vitoriaMark2.setVisible(true);
					vitoriaMark5.setVisible(true);
					%vencedores.add($player2);
					%vencedores.add($player5);
				}
				if(%dupla3Result >= %dupla1Result && %dupla3Result >= %dupla2Result && %dupla3Result > 0){
					vitoriaMark3.setVisible(true);
					vitoriaMark6.setVisible(true);
					%vencedores.add($player3);
					%vencedores.add($player6);
				}
			}
		} else {
			if(%tp1 >= %tp2 && %tp1 >= %tp3 && %tp1 >= %tp4 && %tp1 >= %tp5 && %tp1 >= %tp6 && %tp1 > 0){ //se empatarem em 0 pontos ninguém ganha!
				if($salaEmQueEstouTipoDeJogo $= "poker"){
					vitoriaMark1_pk.setVisible(true);
				} else {
					vitoriaMark1.setVisible(true);
				}
				%vencedores.add($player1);
			}
			if(%tp2 >= %tp1 && %tp2 >= %tp3 && %tp2 >= %tp4 && %tp2 >= %tp5 && %tp2 >= %tp6 && %tp2 > 0){
				if($salaEmQueEstouTipoDeJogo $= "poker"){
					vitoriaMark2_pk.setVisible(true);
				} else {
					vitoriaMark2.setVisible(true);
				}
				%vencedores.add($player2);
			}
			if(%tp3 >= %tp1 && %tp3 >= %tp2 && %tp3 >= %tp4 && %tp3 >= %tp5 && %tp3 >= %tp6 && %tp3 > 0){
				if($salaEmQueEstouTipoDeJogo $= "poker"){
					vitoriaMark3_pk.setVisible(true);
				} else {
					vitoriaMark3.setVisible(true);
				}
				%vencedores.add($player3);
			}
			if(%tp4 >= %tp1 && %tp4 >= %tp2 && %tp4 >= %tp3 && %tp4 >= %tp5 && %tp4 >= %tp6 && %tp4 > 0){
				if($salaEmQueEstouTipoDeJogo $= "poker"){
					vitoriaMark4_pk.setVisible(true);
				} else {
					vitoriaMark4.setVisible(true);
				}
				%vencedores.add($player4);
			}
			if(%tp5 >= %tp1 && %tp5 >= %tp2 && %tp5 >= %tp3 && %tp5 >= %tp4 && %tp5 >= %tp6 && %tp5 > 0){
				if($salaEmQueEstouTipoDeJogo $= "poker"){
					vitoriaMark5_pk.setVisible(true);
				} else {
					vitoriaMark5.setVisible(true);
				}
				%vencedores.add($player5);
			}	
			if(%tp6 >= %tp1 && %tp6 >= %tp2 && %tp6 >= %tp3 && %tp6 >= %tp4 && %tp6 >= %tp5 && %tp4 > 0){
				if($salaEmQueEstouTipoDeJogo $= "poker"){
					vitoriaMark6_pk.setVisible(true);
				} else {
					vitoriaMark6.setVisible(true);
				}
				%vencedores.add($player6);
			}
		}
	}
	
	//marca os dados da sala:
	%quemBateuVenceu = false;
	for(%i = 0; %i < %vencedores.getCount(); %i++){
		%player = %vencedores.getObject(%i);
		if(%player.nome $= %quemBateu){
			%quemBateuVenceu = true;
		}
	}
	clientSetBatidaText(%quemBateu, %quemBateuVenceu, %tempoDeJogo, %vencedores);
	baterGuiTempoDeJogo_txt.text = %tempoDeJogo;
	baterGuiDesastres_txt.text = %desastres;
	baterGuiCreditosDistribuidos_txt.text = %creditosDistribuidos;
	
	
	clientVerificarAchievements(%numDePlayers, %vencedores, $rodadaAtual, %quemBateuVenceu, %assassino);
	
		
	/////////////////////////////////////////////////////
	//agora que já pegou todas as informações, retornar para o usuário (chamando o gui):
	Canvas.pushDialog(newSalaInsideGui);
	initSalaChatGui(); //prepara pra receber comentários dos jogadores
	if(%valido > 0){
		Canvas.pushDialog(baterGui); //chama o gui da batida por cima do tabuleiro;
		$vendoBaterGui = true;
	} else {
		Canvas.popDialog(objetivosGuii);
		Canvas.popDialog(escolhaDeCores);
		Canvas.popDialog(aguardandoObjGui);
		clientAskVoltarPraSala();
		clientMsgBoxOKT("JOGO INVÁLIDO", "O SISTEMA NÃO ADMITE JOGOS CURTOS DEMAIS.");	
	}
	clientUnloadGame(); //deleta todas as peças, cartas e players e reseta todas as variáveis;
}

function clientSetBatidaText(%quemBateu, %quemBateuVenceu, %tempoDeJogo, %vencedores)
{
	if($estouNoTutorial)
	{
		quemBateuPt_txt.text = %quemBateu SPC "bateu!";
		return;
	}
	
	if(%quemBateuVenceu)
	{
		if(%vencedores.getCount() == 1)
		{
			quemBateuPt_txt.text = %quemBateu SPC "bateu em " @ %tempoDeJogo @ " minutos!";
			return;
		}
		
		//mais de um vencedor, quem bateu TAMBÉM venceu:
		%bateuStr = %quemBateu @ " bateu! VENCEDORES: " @ %vencedores.getObject(0).nome;
		for(%i = 1; %i < %vencedores.getCount(); %i++)
			%bateuStr = %bateuStr @ " e " @ %vencedores.getObject(%i).nome;
		
		%bateuStr = %bateuStr @ ".";
		quemBateuPt_txt.text = %bateuStr;
		return;
	}
	
	if(%quemBateu $= "Ninguém")
	{
		if(%vencedores.getCount() > 0)
		{	
			%bateuStr = %quemBateu @ " bateu! VENCEDORES: " @ %vencedores.getObject(0).nome;
			for(%i = 1; %i < %vencedores.getCount(); %i++)
				%bateuStr = %bateuStr @ " e " @ %vencedores.getObject(%i).nome;
			
			%bateuStr = %bateuStr @ ".";
			quemBateuPt_txt.text = %bateuStr;
			return;
		}
		
		quemBateuPt_txt.text = "Ninguém bateu! Ninguém venceu! Boa... :)";
		return;
	}
	
	if(%vencedores.getCount() == 1)
	{
		quemBateuPt_txt.text = %quemBateu SPC "bateu, mas " @ %vencedores.getObject(0).nome @ " venceu!"; 
		return;
	}
	
	%bateuStr = %quemBateu SPC "bateu, mas ";
	%bateuStr = %bateuStr @ %vencedores.getObject(0).nome;
	for(%i = 1; %i < %vencedores.getCount(); %i++)
		%bateuStr = %bateuStr @ " e " @ %vencedores.getObject(%i).nome;
	
	%bateuStr = %bateuStr @ " venceram!";
	quemBateuPt_txt.text = %bateuStr;
}



function clientMarcarDadosBatida(%numDePlayers, %quemBateu, %dadosP1, %dadosP2, %dadosP3, %dadosP4, %dadosP5, %dadosP6)
{
	if($salaEmQueEstouTipoDeJogo $= "poker")
	{
		clientMarcarDadosBatidaPoker(%numDePlayers, %quemBateu, %dadosP1, %dadosP2, %dadosP3, %dadosP4, %dadosP5, %dadosP6);
		return;	
	}
	
	clientMarcarDadosBatidaNormal(%numDePlayers, %quemBateu, %dadosP1, %dadosP2, %dadosP3, %dadosP4, %dadosP5, %dadosP6);
}

function clientMarcarDadosBatidaPoker(%numDePlayers, %quemBateu, %dadosP1, %dadosP2, %dadosP3, %dadosP4, %dadosP5, %dadosP6)
{
	%dados[1] = %dadosP1;
	%dados[2] = %dadosP2;
	%dados[3] = %dadosP3;
	%dados[4] = %dadosP4;
	%dados[5] = %dadosP5;
	%dados[6] = %dadosP6;
	
	if($primeiroJogo){
		%myX = 351;
		%myY = 149;
		%my79 = 79;
		%my158 = 158;
	} else {
		%myWindowResX = sceneWindow2d.getWindowExtents();
		$myWindowResX = getWord(%myWindowResX, 2);
		%myX = calcularNaRes(351, $myWindowResX);
		%myY = calcularNaRes(149, $myWindowResX);
		%my79 = calcularNaRes(79, $myWindowResX);
		%my158 = calcularNaRes(158, $myWindowResX);
	}
		
	%percent = "%";
	%xString = "x"; 
	
	%limiteDoLoop = %numDePlayers + 1;
	%myP1BaterGuiPosX = %myX - (%my79 * (%numDePlayers - 2));
	
	for(%i = 1; %i < %limiteDoLoop; %i++){
		%myBaterGuiPosX = %myP1BaterGuiPosX + (%my158 * (%i - 1));
		clientSetPersonaBaterGui(%i, %myBaterGuiPosX, %myY);
					
		//apaga os X indesejáveis:
		%eval = "p" @ %i @ "cBatidaX.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "cVencedorX.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "cVisionarioX.setVisible(false);";
		eval(%eval);
		
		//seta os valores básicos:
		%eval = "nomePtP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 0);";	
		eval(%eval);
		%eval = "obj1DescPtP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 1);";	
		eval(%eval);
		%eval = "obj2DescPtP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 3);";	
		eval(%eval);
		%eval = "missoesPtP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 6);";	
		eval(%eval);
		%eval = "recursosTP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 7);";	
		eval(%eval);
		%eval = "totalPtP" @ %i @ "_txt_pk.text = getWord(%dadosP" @ %i @ ", 8);";	//total de pontos
		eval(%eval);
		%eval = "p" @ %i @ "creditos.text = getWord(%dadosP" @ %i @ ", 9);";	//fichas
		eval(%eval);
		
		%vitoriaPlayer[%i] = getWord(%dados[%i], 10);
		%visionarioPlayer[%i] = getWord(%dados[%i], 11);
		%arrebatadorPlayer[%i] = getWord(%dados[%i], 12);
		%powerPlayPlayer[%i] = getWord(%dados[%i], 13); //cCOMERCIANTE NO JOGO NORMAL
		%jogoPokerPlayer[%i] = getWord(%dados[%i], 14); //cDIPLOMATA NO JOGO NORMAL
		
		clientPrintJogoPokerBatida(%jogoPokerPlayer[%i], %i);
		
		%eval = "p" @ %i @ "objetivo1Percent.text = getWord(%dadosP" @ %i @ ", 15) @ %percent;";	
		eval(%eval);
		%eval = "p" @ %i @ "objetivo2Percent.text = getWord(%dadosP" @ %i @ ", 16) @ %percent;";	
		eval(%eval);
		%eval = "p" @ %i @ "imperioPercent.text = getWord(%dadosP" @ %i @ ", 17) @ %percent;";	
		eval(%eval);
		//
		%eval = "%thisPnome = nomePtP" @ %i @ "_txt.text;";	
		eval(%eval);
		%eval = "%thisP15Pontos_x = p" @ %i @ "cArrebatadorX;";	
		eval(%eval);
		%eval = "%thisP5Acordos_x = p" @ %i @ "cComercianteX;";	
		eval(%eval);
		%eval = "%thisPDiplomata_x = p" @ %i @ "cDiplomataX;";	
		eval(%eval);
		
		//Marca a Vitória no tick de Arrebatador:
		if(%vitoriaPlayer[%i] !$= "0" && %vitoriaPlayer[%i] !$= "")
			%thisP15Pontos_x.bitmap = "game/data/images/tick2.png";	
				
		//Marca a batida no tick de comerciante:
		if(%quemBateu $= %thisPnome)
			%thisP5Acordos_x.bitmap = "game/data/images/tick2.png";
					
		//Marca a powerPlay no tick de diplomata:
		if(%powerPlayPlayer[%i] !$= "0" && %powerPlayPlayer[%i] !$= "")
			%thisPDiplomata_x.bitmap = "game/data/images/tick2.png";
						
		//seta os ticks grandes:
		%eval = "%myObj1Pt = getWord(%dadosP" @ %i @ ", 2);";	
		eval(%eval);
		%eval = "%myObj2Pt = getWord(%dadosP" @ %i @ ", 4);";	
		eval(%eval);
		%eval = "%myImperioPt = getWord(%dadosP" @ %i @ ", 5);";	
		eval(%eval);
		if(%myObj1Pt $= "5"){
			%eval = "p" @ %i @ "Obj1Tick.setVisible(true);";	
			eval(%eval);
		}
		if(%myObj2Pt $= "5"){
			%eval = "p" @ %i @ "Obj2Tick.setVisible(true);";	
			eval(%eval);
		}
		if(%myImperioPt > 0){
			%eval = "p" @ %i @ "ImperioTick.setVisible(true);";	
			eval(%eval);
		}
	}	
}

function clientPrintJogoPokerBatida(%jogoFeito, %i)
{
	%eval = "%myPlayerPokerGame_img = p" @ %i @ "pokerGame_batida;";
	eval(%eval);
	
	%myPlayerPokerGame_img.setVisible(true);
	%myPlayerPokerGame_img.bitmap = "game/data/images/pk/poker_aval_" @ %jogoFeito @ "_batida.png";
}


function clientMarcarDadosBatidaNormal(%numDePlayers, %quemBateu, %dadosP1, %dadosP2, %dadosP3, %dadosP4, %dadosP5, %dadosP6)
{
	if($primeiroJogo){
		%myX = 351;
		%myY = 149;
		%my79 = 79;
		%my158 = 158;
	} else {
		%myWindowResX = sceneWindow2d.getWindowExtents();
		$myWindowResX = getWord(%myWindowResX, 2);
		%myX = calcularNaRes(351, $myWindowResX);
		%myY = calcularNaRes(149, $myWindowResX);
		%my79 = calcularNaRes(79, $myWindowResX);
		%my158 = calcularNaRes(158, $myWindowResX);
	}
		
	%percent = "%";
	%xString = "x"; 
	
	%limiteDoLoop = %numDePlayers + 1;
	%myP1BaterGuiPosX = %myX - (%my79 * (%numDePlayers - 2));
	
	for(%i = 1; %i < %limiteDoLoop; %i++){
		%myBaterGuiPosX = %myP1BaterGuiPosX + (%my158 * (%i - 1));
		clientSetPersonaBaterGui(%i, %myBaterGuiPosX, %myY);
				
		
		//seta os valores básicos:
		%eval = "nomePtP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 0);";	
		eval(%eval);
		%eval = "obj1DescPtP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 1);";	
		eval(%eval);
		%eval = "obj2DescPtP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 3);";	
		eval(%eval);
		%eval = "missoesPtP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 6);";	
		eval(%eval);
		%eval = "recursosTP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 7);";	
		eval(%eval);
		%eval = "totalPtP" @ %i @ "_txt.text = getWord(%dadosP" @ %i @ ", 8);";	
		eval(%eval);
		%eval = "p" @ %i @ "creditos.text = getWord(%dadosP" @ %i @ ", 9);";	
		eval(%eval);
		%eval = "p" @ %i @ "cVencedor.text = getWord(%dadosP" @ %i @ ", 10);";	
		eval(%eval);
		%eval = "p" @ %i @ "cVisionario.text = getWord(%dadosP" @ %i @ ", 11);";	
		eval(%eval);
		%eval = "p" @ %i @ "cArrebatador.text = getWord(%dadosP" @ %i @ ", 12);";	
		eval(%eval);
		%eval = "p" @ %i @ "cComerciante.text = getWord(%dadosP" @ %i @ ", 13);";	
		eval(%eval);
		%eval = "p" @ %i @ "cDiplomata.text = getWord(%dadosP" @ %i @ ", 14);";	
		eval(%eval);
		%eval = "p" @ %i @ "objetivo1Percent.text = getWord(%dadosP" @ %i @ ", 15) @ %percent;";	
		eval(%eval);
		%eval = "p" @ %i @ "objetivo2Percent.text = getWord(%dadosP" @ %i @ ", 16) @ %percent;";	
		eval(%eval);
		%eval = "p" @ %i @ "imperioPercent.text = getWord(%dadosP" @ %i @ ", 17) @ %percent;";	
		eval(%eval);
		//
		%eval = "%thisPnome = nomePtP" @ %i @ "_txt.text;";	
		eval(%eval);
		%eval = "%thisPBatida_txt = p" @ %i @ "cBatida;";	
		eval(%eval);
		%eval = "%thisPVitoria_txt = p" @ %i @ "cVencedor;";	
		eval(%eval);
		%eval = "%thisPImperio_txt = p" @ %i @ "cVisionario;";	
		eval(%eval);
		%eval = "%thisP15Pontos_txt = p" @ %i @ "cArrebatador;";	
		eval(%eval);
		%eval = "%thisP5Acordos_txt = p" @ %i @ "cComerciante;";	
		eval(%eval);
		%eval = "%thisPDiplomata_txt = p" @ %i @ "cDiplomata;";	
		eval(%eval);
		%eval = "%thisPBatida_x = p" @ %i @ "cBatidaX;";	
		eval(%eval);
		%eval = "%thisPVitoria_x = p" @ %i @ "cVencedorX;";	
		eval(%eval);
		%eval = "%thisPImperio_x = p" @ %i @ "cVisionarioX;";	
		eval(%eval);
		%eval = "%thisP15Pontos_x = p" @ %i @ "cArrebatadorX;";	
		eval(%eval);
		%eval = "%thisP5Acordos_x = p" @ %i @ "cComercianteX;";	
		eval(%eval);
		%eval = "%thisPDiplomata_x = p" @ %i @ "cDiplomataX;";	
		eval(%eval);
		
		//mostra os ticks de créditos:
		if(%quemBateu $= %thisPnome){
			%thisPBatida_x.bitmap = "game/data/images/tick2.png";
			if($estouNoTutorial){
				tut_showFinal();
			}
		}
		if(%thisPVitoria_txt.text !$= "0" && %thisPVitoria_txt.text !$= ""){
			%thisPVitoria_x.bitmap = "game/data/images/tick2.png";	
		}
		if(%thisPImperio_txt.text !$= "0" && %thisPImperio_txt.text !$= ""){
			%imperiosFeitos++;
			%thisPImperio_x.bitmap = "game/data/images/tick2.png";
			%eval = "$player" @ %i @ ".visionario = true;";	
			eval(%eval);
		}
		if(%thisP15Pontos_txt.text !$= "0" && %thisP15Pontos_txt.text !$= ""){
			%thisP15Pontos_x.bitmap = "game/data/images/tick2.png";	
			%eval = "$player" @ %i @ ".arrebatador = true;";	
			eval(%eval);
		}
		if(%thisP5Acordos_txt.text !$= "0" && %thisP5Acordos_txt.text !$= ""){
			%thisP5Acordos_x.bitmap = "game/data/images/tick2.png";	
			%eval = "$player" @ %i @ ".comerciante = true;";	
			eval(%eval);
		}
		if(%thisPDiplomata_txt.text !$= "0" && %thisPDiplomata_txt.text !$= ""){
			%eval = "$player" @ %i @ ".diplomata = true;";	
			eval(%eval);
			if(%thisPVitoria_txt.text !$= "0" && %thisPVitoria_txt.text !$= ""){
				%xString = "x";
				%thisPDiplomata_x.setVisible(false);
				%thisPDiplomata_txt.setVisible(true);
				%eval = "p" @ %i @ "cDiplomata.text = %xString SPC getWord(%dadosP" @ %i @ ", 14);";	
				eval(%eval);
			} else {
				%thisPDiplomata_x.bitmap = "game/data/images/tick2.png";
			}
		}
				
				
		//seta os ticks grandes:
		%eval = "%myObj1Pt = getWord(%dadosP" @ %i @ ", 2);";	
		eval(%eval);
		%eval = "%myObj2Pt = getWord(%dadosP" @ %i @ ", 4);";	
		eval(%eval);
		%eval = "%myImperioPt = getWord(%dadosP" @ %i @ ", 5);";	
		eval(%eval);
		if(%myObj1Pt > 0){
			%eval = "p" @ %i @ "Obj1Tick.setVisible(true);";	
			eval(%eval);
		}
		if(%myObj2Pt > 0){
			%eval = "p" @ %i @ "Obj2Tick.setVisible(true);";	
			eval(%eval);
		}
		if(%myImperioPt > 0){
			%eval = "p" @ %i @ "ImperioTick.setVisible(true);";	
			eval(%eval);
		}
	}	
}

function clientSetPersonaBaterGui(%i, %myBaterGuiPosX, %myY)
{
	%eval = "%myBaterGui = p" @ %i @ "BaterPersonaGui;";	
	eval(%eval);
	%myBaterGui.setVisible(true);
	%myBaterGui.setPosition(%myBaterGuiPosX, %myY);
	%eval = "%myColor = $player" @ %i @ ".myColor;";	
	eval(%eval);
			
	clientSetPersonaBaterImg(%myBaterGui, %myColor);
	clientSetTotalDePontosTxt(%i);
}

function clientSetPersonaBaterImg(%myBaterGui, %myColor)
{
	if($salaEmQueEstouTipoDeJogo $= "poker")
	{
		%myBaterGui.bitmap = "~/data/images/pk/poker_baterPersona_" @ %myColor @ ".png";
		return;	
	}
	
	%myBaterGui.bitmap = "~/data/images/player" @ %myColor @ "bater.png";	
}

function clientSetTotalDePontosTxt(%i)
{
	if($salaEmQueEstouTipoDeJogo $= "poker")
	{
		%eval = "totalPtP" @ %i @ "_txt.setVisible(false);";
		eval(%eval);
		%eval = "totalPtP" @ %i @ "_txt_pk.setVisible(true);";
		eval(%eval);
		return;
	}
	
	%eval = "totalPtP" @ %i @ "_txt.setVisible(true);";
	eval(%eval);
	%eval = "totalPtP" @ %i @ "_txt_pk.setVisible(false);";
	eval(%eval);
}

function clientVerificarObjetivoDescGulok()
{
	if(obj1DescPtP1_txt.text !$= "Grande+Matriarca")
		return;
	
	obj1DescPtP1_txt.text = "Grande Matriarca";
	obj1DescPtP2_txt.text = "Grande Matriarca";
	obj1DescPtP3_txt.text = "Grande Matriarca";
	obj1DescPtP4_txt.text = "Grande Matriarca";
	obj1DescPtP5_txt.text = "Grande Matriarca";
	obj2DescPtP1_txt.text = "";
	obj2DescPtP2_txt.text = "";
	obj2DescPtP3_txt.text = "";
	obj2DescPtP4_txt.text = "";
	obj2DescPtP5_txt.text = "";	
}

function clientVerificarAchievements(%numDePlayers, %vencedores, %rodadaFinal, %quemBateuVenceu, %assassino){
	echo("VERIFICANDO ACHIEVEMENTS...");
	
	if(%numDePlayers < 3)
	{
		echo("Partida Imperfeita por ter menos de 3 jogadores");
		return;	
	}
	
	if($salaEmQueEstouTipoDeJogo $= "poker")
	{
		echo("Partida Imperfeita por ser um jogo de Poker Imperial");
		return;
	}
	
	//Partida Lenta:
	if(%rodadaFinal > 9){
		baterTab_esq.bitmap = "~/data/images/baterTab_lenta.png";
		baterTab_esq.setVisible(true);
		echo("Partida Lenta, nenhum achievement é possível!");
		return; //se a partida é lenta nenhum achievement é possível!
	}
	
	//Quebra de Saque -> se o último a jogar está entre os ganhadores:
	%eval = "%ultimoAJogar = $player" @ %numDePlayers @ ";";
	eval(%eval);
	for(%i = 0; %i < %vencedores.getCount(); %i++){
		%player = %vencedores.getObject(%i);
		if(%player.nome $= %ultimoAJogar.nome){
			%quebraDeSaque = true;
			baterTab_dir.bitmap = "~/data/images/baterTab_quebraSaque.png";
			baterTab_dir.setVisible(true);
			baterTabTxt_dir.setVisible(true);
			baterTabTxt_dir.text = %ultimoAJogar.nome;
			echo("----------------------> Vencedor jogou por último! QUEBRA DE SAQUE!");
		}
	}
	
	//Assassino:
	if(%assassino && %vencedores.getCount() > 0){
		baterTab_esq.bitmap = "~/data/images/baterTab_assassino.png";
		baterTab_esq.setVisible(true);
		baterTabTxt_esq.setVisible(true);
		baterTabTxt_esq.text = %vencedores.getObject(0).nome;
		echo("----------------------> Vencedor é assasino! Partida Imperfeita!");
		return; //se o cara é assassino não tem como ter feito uma partida perfeita!
	}
	
	//Partida Perfeita:
	if(%vencedores.getCount() > 1){
		echo("----------------------> Mais de um vencedor! Partida Imperfeita!");
		%imperfeita = true;	
	}
	%imperiosFeitos = clientGetImperiosFeitos(%numDePlayers);
	if(%imperiosFeitos != 1){
		echo("----------------------> Mais de um Império, ou nenhum! Partida Imperfeita!");
		%imperfeita = true;		
	}
	if(%quemBateuVenceu && !%imperfeita && ($salaEmQueEstouTipoDeJogo $= "classico" || $salaEmQueEstouTipoDeJogo $= "semPesquisas" || $salaEmQueEstouTipoDeJogo $= "set")){
		echo("----------------------> Quem bateu venceu!");
		//não foi partida lenta nem assassina
		//apenas um ganhou
		//apenas um fez imperio
		//jogo Clássico ou Sem Pesquisas
		%vencedor = %vencedores.getObject(0);
		if(%vencedor.visionario && %vencedor.arrebatador && %vencedor.comerciante && %vencedor.diplomata){
			echo("----------------------> PARTIDA PERFEITA!!!");
			baterTab_esq.bitmap = "~/data/images/baterTab_perfeita.png";
			baterTab_esq.setVisible(true);
			baterTabTxt_esq.setVisible(true);
			baterTabTxt_esq.text = %vencedores.getObject(0).nome;
		} else {
			echo("----------------------> Vencedor não fez Império, ou não fez Arrebatador, ou não fez Comerciante, ou não fez Diplomata! Partida Imperfeita!");	
		}
	}
}

function clientGetImperiosFeitos(%numDePlayers)
{
	for(%i = 0; %i < %numDePlayers; %i++)
	{
		%player = clientGetPlayerPorId("player" @ %i+1);
		if(clientGetImperio(%player))
			%imperiosFeitos++;	
	}
	return %imperiosFeitos;
}	


function clientUnloadGame(){
	$jogadorDaVez = "no";
	
	Canvas.popDialog(objetivosGuii);
	Canvas.popDialog(escolhaDeCores);
	Canvas.popDialog(aguardandoObjGui);
	
	//apagar markers de advesários, das propostas:
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		%info1 = clientFindInfo(%proposta.numDaInfo1);
		%info2 = clientFindInfo(%proposta.numDaInfo2);		
		if(isObject(%info1.myMark)){
			%info1.myMark.safeDelete();	
		}
		if(isObject(%info2.myMark)){
			%info2.myMark.safeDelete();	
		}
	}
	
	//apagando os ímbolos de acordos de exploração no acordosGui:
	clientExplGuiClear();
		
	//deletando os players:
	for(%i = 1; %i < 5; %i++){
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		
		%count = %player.mySimUnits.getCount();
		for(%j = 0; %j < %count; %j++){
			%unit = %player.mySimUnits.getObject(0);
			%player.mySimUnits.remove(%unit);
			if(isObject(%unit.myEscudo)){
				%unit.myEscudo.safeDelete();	
			}
			if(isObject(%unit)){
				%unit.safeDelete();
			}
		}
		%count = %player.mySimBases.getCount();
		for(%k = 0; %k < %count; %k++){ 
			%base = %player.mySimBases.getObject(0);
			%player.mySimBases.remove(%base); //remove a área do player;
			%base.safeDelete(); //deleta a peça
		}
		
		%player.mySimAreas.delete();
		%player.mySimInfo.delete();
		%player.mySimExpl.delete();
		%player.mySimObj.delete();
		%player.mySimUnits.delete();
		%player.mySimBases.delete();
		%player.delete();	
	}
		
	$statsAtuais = false; //para ligar os gauges dos objetivos;
	$desembarcando = false;
	$zoomFactor = 1;
	$shiftOn = false;
	$tenhoRecursos = 0;
	
	$explMarkersOn = false;
	$doarMarkersOn = false;
	$moratoriaMarkersOn = false;
	
	$infoMarkPiscando = 0;	
	$piscando = 0;	
	$piscarOn = false;

	cancel($atkGuiSchedule);
	cancel($piscarExplBtnSchedule);
	cancel($msgGuiSchedule);
	cancel($linha2Schedule);
	
	$rightClickDelay = false;
	$pingDelay = false;
	
	$pingTo = "nada";
	$lastPingBtn = "nada";
	$ultimoAtaqueFinalizado = true;
	$jogoEmDuplas = false;
	
	Foco.clear();
	
	clientReStartPlayers();
	
	//zera as poisções reservaTXT:
	for(%i = 0; %i < $areasDeTerra.getCount(); %i++){
		%area = $areasDeTerra.getObject(%i);
		%area.onLevelLoaded();
		clientAtualizarPosReservaTxt(%area);
	}
	
	
	//MainGui:
	movGui.bitmap = "~/data/images/5mov.png";
	msgGui.setVisible(false);
	atkGui.setVisible(false);
	clientExplGuiClear();
	bater_btn.setVisible(false);
	finalizarTurno_btn.setVisible(false);
	venderRecursos_btn.setActive(false);
	imperiais_txt.text = "10";
	minerios_txt.text = "0";
	petroleos_txt.text = "0";
	uranios_txt.text = "0";
	pontosHud_txt.text = "0";
	areasTerrestres_txt.text = "1";
	areasMaritimas_txt.text = "1";
	rendaImperiais_txt.text = "+0";
	rendaMinerios_txt.text = "+0";
	rendaPetroleos_txt.text = "+0";
	rendaUranios_txt.text = "+0";
	clientDesligarMoratoriaMarkers();
	clientDesligarExplMarkers();
	toggleAcordos_btn.setActive(false);
	embargar_btn.setActive(false);
	venderRecursos_btn.setActive(false);
	investirRecursos_btn.setActive(true);
	imperioMark_img.setVisible(false); 
	
	//doarGui:
	Canvas.popDialog(doarGui);
	
	//apagar os infoMarkers e explMarkers:
	for(%i = 0; %i < 78; %i++){
		%info = clientFindInfo(%i + 1);
		if(isObject(%info.myMark)){
			%info.myMark.safeDelete();	
		}
		if(isObject(%info.myExplMark)){
			%info.myExplMark.safeDelete();	
		}
		%info.jahFoiOferecida = false;
	}
	
	//apaga os markers de moratórias, caso um dos clients estivesse vendo na hora em que o jogo acabou:
	clientZerarMoratoriaMarks(); 
		
	//apagar as cartas de objetivos sorteados na escolha de objetivos:
	hudObjSorteado1_img.setVisible(false); 
	hudObjSorteado2_img.setVisible(false);	
	
	//caso o jogo tenha acabado em um ataque:
	clientApagarMorteMarks(); 
	
	//limpa as variáveis de players prontos:
	clientClearProntoBtns(true);
	
	//apaga a msgBox de sincronizando, caso o jogo tenha sido cancelado enquanto ela estava na tela
	clientPopSincronizandoMsgBox();
	
	//tira o server com dot, caso ele tenha surgido após um jogo em que ninguém bateu:
	schedule(3500, 0, "clientPopServerComDot");
}


function clientFecharMsgBoxJogoInvalido(){
	jogoInvalidoMsgBox.setVisible(false);
	jogoSair_btn.setActive(true);
}


function clientCmdRegistrarPartidaPerfeita()
{
	$myPersona.pk_power_plays += 1;	
}

function clientCmdBonusHorarioNobre()
{
	clientMsgBoxOKT("BONUS DE HORÁRIO NOBRE", "VOCÊ GANHOU +2 CRÉDITOS POR JOGAR ENTRE 21:00 e 23:59.");
}