// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientCriarSala.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 26 de dezembro de 2007 11:52
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
$primeiroJogo = true;

//Função que coloca em objetos os dados dos planetas:
function clientInitDadosPlanetas(){
	if(!isObject($planetaTerra)){
		$planetaTerra = new ScriptObject(){
			terra = 41;
			mar = 32;
			minerio = 4;
			petroleo = 1;
			uranio = 4;
			desastres = 1;
			lotacao = 4;
			desc = "O PLANETA TERRA SE CARACTERIZA PRINCIPALMENTE PELA PRESENÇA DE OCEANOS E PELA ESCASSEZ DE PETRÓLEO.";
		};
	}
	if(!isObject($planetaUngart)){
		$planetaUngart = new ScriptObject(){
			terra = 36;
			mar = 39;
			minerio = 5;
			petroleo = 3;
			uranio = 5;
			desastres = 14;
			lotacao = 5;
			//desc = "PLANETA CARACTERIZADO PELO COMPLEXO SISTEMA DE CANAIS MARÍTIMOS E PELO CLIMA INSTÁVEL.";
			desc = "UNGART POSSUI RECURSOS ABUNDANTES, DESASTRES FREQUENTES E UM COMPLEXO SISTEMA DE CANAIS MARÍTIMOS.";
		};
	}
	if(!isObject($planetaTeluria)){
		$planetaTeluria = new ScriptObject(){
			terra = 31;
			mar = 34;
			minerio = 5;
			petroleo = 3;
			uranio = 4;
			reliquias = 1;
			artefatos = 1;
			desastres = 10;
			lotacao = 4;
			desc = "PLANETA CARACTERIZADO POR UM ARTEFATO E UMA RELÍQUIA DE TECNOLOGIA ALIENÍGENA. OS RECURSOS SÃO ABUNDANTES.";
		};
	}
}
clientInitDadosPlanetas();

//sair do átrio:
function clientAskSairDoAtrio(){
	//troca os dialogs do Canvas:
	Canvas.popDialog(academiaGui);
	Canvas.pushDialog(loggedInGui);	
	$vendoAcademia = false; 
	$primeiraAtrio = false;
	//envia o pedido pro server:
	commandToServer('sairDoAtrio');
	
	//popula a persona:
	clientPopularDados("loggedIn"); //passa os dados para a tela de LoggedIn (para o caso de o usuário tenha gasto créditos/omnis)
}



//salas na tela:
function initSalasNaTela(){
	$salasNaTela = new SimSet();
	
	for(%i = 1; %i < 13; %i++){
		%eval = "$sala" @ %i @ " = new ScriptObject(){};";
		eval(%eval);
	}
}

clientReStartPlayers();
function clientAskCriarSala(){
	clientPopTipoDeSalaGui();
	clientPopAtrioPokerConfigGui();
	clientPushAguardeMsgBox();
	commandToServer('criarSala');	
}
function clientAskCriarSalaPoker(%blind){
	clientPopTipoDeSalaGui();
	clientPopAtrioPokerConfigGui();
	clientPushAguardeMsgBox();
	$myLastSeenBlind = %blind;
	commandToServer('criarSalaDePoker', %blind);	
}

function clientTentarCriarSala()
{
	clientPopTipoDeSalaGui();
	clientPopAtrioPokerConfigGui();
	
	//if($myPersona.taxoVitorias < 25 || $myPersona.especie !$= "humano")
	//{
		clientAskCriarSala();
		return;
	//}
	
	//clientPushTipoDeSalaGui();
}

function clientPushTipoDeSalaGui()
{
	atrio_tipoSalaGui.setVisible(true);	
}
function clientPopTipoDeSalaGui()
{
	atrio_tipoSalaGui.setVisible(false);	
}
function clientPushAtrioPokerConfigGui()
{
	atrio_poker_configGui.setvisible(true);	
	$myLastChosenPkBlind = 1;
	atrio_PokerBlindInput_txt.text = $myLastChosenPkBlind;
	atrio_blind_setaLeft.setActive(false);
	atrio_blind_setaRight.setActive(false);
	
	if($myPersona.pk_fichas >= 40)
		atrio_blind_setaRight.setActive(true);	
}
function clientBlindConfigSetaRight()
{
	%blind = atrio_PokerBlindInput_txt.text;
	%blind++;
	atrio_PokerBlindInput_txt.text = %blind;
	$myLastChosenPkBlind = %blind;
	verifyBlindConfigGuiSetas(%blind);
}
function clientBlindConfigSetaLeft()
{
	%blind = atrio_PokerBlindInput_txt.text;
	%blind--;
	atrio_PokerBlindInput_txt.text = %blind;
	$myLastChosenPkBlind = %blind;
	verifyBlindConfigGuiSetas(%blind);
}
function verifyBlindConfigGuiSetas(%blind)
{
	atrio_blind_setaRight.setActive(true);
	atrio_blind_setaLeft.setActive(true);
	
	if($myPersona.pk_fichas < (%blind+1) * 20 || %blind == 5)
		atrio_blind_setaRight.setActive(false);	
	
	if(%blind == 1)
		atrio_blind_setaLeft.setActive(false);	
}

function clientPopAtrioPokerConfigGui()
{
	atrio_poker_configGui.setvisible(false);
	atrio_pokerImperial_btn.setStateOn(false);
}

function clientTentarCriarSalaDePoker()
{
	%blind = $myLastChosenPkBlind;
	clientAskCriarSalaPoker(%blind);
}


function clientAskSairDaSala(){
	//marca que o cara já entrou numa sala e está saindo, os gauges precisam ser re-calculados com novos dados dependendo da resolução de tela:
	$primeiraSalaInside = false;
	$primeiraAtrio = false;
	commandToServer('sairDaSala');	
}

function zerarPersonasImgs(){
	//múltiplos jogos sem resetar o client:
	salaPersona1_img.setVisible(false); 
	salaPersona2_img.setVisible(false); 
	salaPersona3_img.setVisible(false); 
	salaPersona4_img.setVisible(false); 
	salaPersona5_img.setVisible(false); 
	salaPersona6_img.setVisible(false); 
	
	p1Fator_img.setVisible(false);
	p2Fator_img.setVisible(false);
	p3Fator_img.setVisible(false);
	p4Fator_img.setVisible(false);
	p5Fator_img.setVisible(false);
	p6Fator_img.setVisible(false);
}

function clientCmdCriarSala(%num){
	clientResetPiscarMyProntoBtn();
	clientPopAguardeMsgBox(); //devolve o controle ao usuário
	Canvas.popDialog(atrioGui); //apaga o átrio
	Canvas.pushDialog(newSalaInsideGui); //chama o gui de dentro da sala
	zerarPersonasImgs(); //apaga as personas do último jogo;
	salaInsideSalaNum_txt.text = %num;
	
	salaPersona1_img.setVisible(true); //liga a primeira persona, que é a deste client;
	$salaEmQueEstouTipoDeJogo = "Classico";
	clientPopularDadosCriandoSala(); //popula os dados da primeira persona com os deste client;
	iniciarJogo_btn.setActive(false); //torna inativo o botão de iniciarJogo;
	$PlayersNaSalaEmQueEstou = 1;
	salaInsideTipoDeJogo_img.bitmap = "~/data/images/classico_img.png";
	$salaEmQueEstouPlaneta = "Terra";
	salaInsidePlaneta_img.bitmap = "~/data/images/planetaTerra_img.png";
	salaInsideTurno_img.bitmap = "~/data/images/100segundos_img";
	$primeiraSalaInside = false;
	clientCmdAlterarLotacao(4);
	initSalaChatGui();
	
	if($buzinaBlocked){
		buzinaSala_btn.setActive(false);
	} else {
		buzinaSala_btn.setActive(true);
	}
	
	clientSetDadosPlaneta("Terra");
	
	clientApagarDuplasMarks();
	clientApagarPlanetas();
	clientApagarTiposDeJogo();
	clientApagarLotacao();
	clientApagarTurno();
	clientClearSalaInsideDifIcons();
	clientPopKickBtns();
	salaInsideDifFacilima_icon.setVisible(true);
	clientSetSalaInsideLotacaoTab(4, $myLastSeenBlind);
}

function clientSetDadosPlaneta(%planeta){
	echo("setDadosPlaneta: " @ %planeta);
	%percent = "%";
	%eval = "%myPlaneta = $planeta" @ %planeta @ ";";
	eval(%eval);
	salaInsideTerra_txt.text = %myPlaneta.terra;
	salaInsideMar_txt.text = %myPlaneta.mar;
	salaInsideMinerio_txt.text = %myPlaneta.minerio;
	salaInsidePetroleo_txt.text = %myPlaneta.petroleo;
	salaInsideUranio_txt.text = %myPlaneta.uranio;
	salaInsideDesastres_txt.text = %myPlaneta.desastres @ %percent;
	
	salaInside_planetaDesc_txt.setText("<just:center>" @ %myPlaneta.desc @ "\n\n");
		
	if(%planeta $= "Terra"){
		salaInsideTabuleiro_img.setVisible(false);	
	} else {
		salaInsideTabuleiro_img.setVisible(true);
		salaInsideTabuleiro_img.bitmap = "~/data/images/salaInsideTab" @ %planeta;
	}
}


function clientCalcularSalaInsideDif(){
	clientCalcularMyPersonaDif();
	clientClearSalaInsideDifIcons();
	
	//clacula a média de vitórias da sala:	
	%numDePersonas = $PlayersNaSalaEmQueEstou;
	if(%numDePersonas > 1){
		%mediaVit = $salaEmQueEstouDif; //- $myPersona.myDif;
				
		//calcula os limites da dificuldade:
		if($myPersona.taxoVitorias < 100){
			%myDifLimit = 50;
		} else if($myPersona.taxoVitorias >= 100 && $myPersona.taxoVitorias < 200){
			%myDifLimit = 55;
		} else {
			%myDifLimit = 60;
		}
		%myDifLimitNeg = %myDifLimit * -1;
		
		//calcula a dificuldade:
		%dificuldadeNum = $myPersona.myDif - %mediaVit;
		echo("DificuldadeNum = " @ %dificuldadeNum);
		echo("myDifLimit = " @ %myDifLimit);
		if((%dificuldadeNum / 2) > %myDifLimit){
			%dificuldade = "Facilima";
		} else if(%dificuldadeNum > %myDifLimit){
			%dificuldade = "Facil";
		} else if (%dificuldadeNum < %myDifLimitNeg){
			if((%dificuldadeNum / 2) < %myDifLimitNeg){
				%dificuldade = "Dificilima";	
			} else {
				%dificuldade = "Dificil";
			}
		} else {
			%dificuldade = "Competitiva";
		}
		
		//se for um jogo sem pesquisas, a sala é sempre sem pesquisas:
		if($salaEmQueEstouTipoDeJogo $= "semPesquisas"){
			%dificuldade = "Competitiva";
		}
	} else {
		%dificuldade = "Facilima";
	}
	
	//Marca a dificuldade na sala:
	%eval = "salaInsideDif" @ %dificuldade @ "_icon.setVisible(true);";
	eval(%eval);
}

function clientAskGetSalaDif(){
	commandToServer('GetSalaDif');
}

function clientCmdGetSalaDif(%dif){
	clientCalcularMyPersonaDif();
	%dif -= $myPersona.myDif;
	%dif /= ($PlayersNaSalaEmQueEstou - 1);
	echo("SALA EM QUE ESTOU DIF: " @ %dif);
	$salaEmQueEstouDif = %dif;
	clientCalcularSalaInsideDif();
}


function clientCalcularMyPersonaDif(){
	if($myPersona.especie $= "humano"){
		%mediaVit += $myPersona.taxoVitorias / 10;
		%mediaVit += $myPersona.aca_v_1 * ($myPersona.aca_v_1 * 2);
		%mediaVit += $myPersona.aca_v_2 * 2;
		%mediaVit += $myPersona.aca_v_3 * ($myPersona.aca_v_3 * 3);
		%mediaVit += $myPersona.aca_v_4 * 3;
		%mediaVit += $myPersona.aca_v_5 * 3;
		%mediaVit += $myPersona.aca_v_6 * 3; //Planetas
		%mediaVit += $myPersona.aca_a_1 * 2; //Líderes
		%mediaVit += $myPersona.aca_a_2 * 3;
		%mediaVit += $myPersona.aca_i_1 * 2;
		%mediaVit += $myPersona.aca_i_2 * 15;
		%mediaVit += $myPersona.aca_i_3 * 3;
		%mediaVit += $myPersona.aca_c_1 * 2;
		%mediaVit += $myPersona.aca_d_1 * 2;
		%mediaVit += $myPersona.aca_t_d_max / 3;
		%mediaVit += $myPersona.aca_t_a_max / 3;
		%mediaVit += $myPersona.aca_n_d_max / 3;
		%mediaVit += $myPersona.aca_n_a_max / 3;
		%mediaVit += $myPersona.aca_ldr_1_h1 * 2;
		%mediaVit += $myPersona.aca_ldr_2_h1 * 2;
		%mediaVit += $myPersona.aca_ldr_1_h2 * ($myPersona.aca_ldr_1_h2 * 4);
		%mediaVit += $myPersona.aca_ldr_2_h2 * ($myPersona.aca_ldr_2_h2 * 4);
		%mediaVit += $myPersona.aca_ldr_1_h3 * 2;
		%mediaVit += $myPersona.aca_ldr_2_h3 * 2;
		%mediaVit += $myPersona.aca_ldr_1_h4 * 2;
		%mediaVit += $myPersona.aca_ldr_2_h4 * 2;	
		
		//bônus por sinergia com prospecção (filantropia, almirante e reciclagem):
		if($myPersona.aca_c_1 > 0){
			%mediaVit += $myPersona.aca_i_2 * $myPersona.aca_c_1;
			%mediaVit += $myPersona.aca_i_3 * $myPersona.aca_c_1;
			%mediaVit += $myPersona.aca_v_3 * $myPersona.aca_c_1;
		}
		
		//bônus por sinergia de almirante com filantropia:
		if($myPersona.aca_i_2 > 0){
			%mediaVit += $myPersona.aca_i_3 * $myPersona.aca_i_2;
		}
		
		//pesquisas avançadas:
		%mediaVit += $myPersona.aca_av_1 * 3;	 //Carapaça
		%mediaVit += $myPersona.aca_av_2 * 3;	 //Mira Eletrônica
		%mediaVit += $myPersona.aca_av_3 * 3;	 //Ocultar
		%mediaVit += $myPersona.aca_av_4 * ($myPersona.aca_av_4 * 3);	 //Satélite
		
		%mediaVit -= 16; //base dos humanos
	} else if($myPersona.especie $= "gulok"){
		%mediaVit += 76; //base dos guloks
		%mediaVit += $myPersona.taxoVitorias / 10;
		%mediaVit += $myPersona.aca_v_1 * 3; //Metabolismo
		%mediaVit += $myPersona.aca_v_2 * 3; //Instinto Materno
		%mediaVit += $myPersona.aca_v_3 * 3; //Incorporar
		%mediaVit += $myPersona.aca_v_4 * 3; //Submergir
		%mediaVit += $myPersona.aca_v_5 * 15; //Crisálida
		%mediaVit += $myPersona.aca_v_6 * 5; //Matriarca
		%mediaVit += $myPersona.aca_a_1 * 2; //Exoesqueleto
		%mediaVit += $myPersona.aca_a_2 * 3; //Horda
		%mediaVit += $myPersona.aca_i_1 * 2; //Espionagem
		%mediaVit += $myPersona.aca_i_2 * 2; //Pilhar
		%mediaVit += $myPersona.aca_i_3 * 3; //Dragnal
		%mediaVit += $myPersona.aca_c_1 * 2; //Faro Extremo
		%mediaVit += $myPersona.aca_d_1 * 2; //Fertilidade
		%mediaVit += $myPersona.aca_t_d_max / 3; //Rainhas
		%mediaVit += $myPersona.aca_t_a_max / 3;
		%mediaVit += $myPersona.aca_n_d_max / 3; //Cefaloks
		%mediaVit += $myPersona.aca_n_a_max / 3;
		%mediaVit += $myPersona.aca_s_d_max / 3; //Vermes
		%mediaVit += $myPersona.aca_s_a_max / 3;
		%mediaVit += $myPersona.aca_ldr_1_h1 * ($myPersona.aca_ldr_1_h1 * 3); //Asas
		%mediaVit += $myPersona.aca_ldr_2_h1 * ($myPersona.aca_ldr_2_h1 * 3);
		%mediaVit += $myPersona.aca_ldr_1_h2 * 3; //Carregar
		%mediaVit += $myPersona.aca_ldr_2_h2 * 3;
		%mediaVit += $myPersona.aca_ldr_1_h3 * 3; //Canibalizar
		%mediaVit += $myPersona.aca_ldr_2_h3 * 3; //Metamorfose
		%mediaVit += $myPersona.aca_ldr_1_h4 * 3; //Devorar Rainhas
		%mediaVit += $myPersona.aca_ldr_2_h4 * 3; //Cortejar
		%mediaVit += $myPersona.aca_ldr_3_h1 * ($myPersona.aca_ldr_3_h1 * 3); //Entregar
		%mediaVit += $myPersona.aca_ldr_3_h2 * ($myPersona.aca_ldr_3_h2 * 3); //Sopro
		%mediaVit += $myPersona.aca_ldr_3_h3 * ($myPersona.aca_ldr_3_h3 * 3); //Fúria
		%mediaVit += $myPersona.aca_ldr_3_h4 * ($myPersona.aca_ldr_3_h4 * 3); //Covil
		
		//bônus por sinergia com Instinto Materno (horda, cortejar e entregar):
		if($myPersona.aca_v_2 > 0){
			%mediaVit += $myPersona.aca_v_2 * $myPersona.aca_a_2;
			%mediaVit += $myPersona.aca_v_2 * $myPersona.aca_ldr_2_h4;
			%mediaVit += $myPersona.aca_v_2 * $myPersona.aca_ldr_3_h1;
		}
		
		//bônus por sinergia de crisálida com matriarca:
		if($myPersona.aca_v_6 > 0){
			%mediaVit += $myPersona.aca_v_5 * $myPersona.aca_v_6;
		}
				
		//pesquisas avançadas:
		%mediaVit += $myPersona.aca_av_1 * $myPersona.aca_ldr_3_h1 * $myPersona.aca_ldr_3_h2 * $myPersona.aca_i_3;	 //Especializar
		%mediaVit += $myPersona.aca_av_2 * 3;	 //Vírus Gulok
		%mediaVit += $myPersona.aca_av_3 * 3;	 //Expulsar
		%mediaVit += $myPersona.aca_av_4 * ($myPersona.aca_av_4 * 3);	 //Evolução Avançada
	}
	
	
	$myPersona.myDif = mFloor(%mediaVit);
	//echo("$myPersona.myDif = " @ %mediaVit);
}

function clientAskPlayerPronto(%num){
	if($myNumNaSala $= %num){
		if($estouProntoProJogo){
			//cancela o pronto:
			commandToServer('setPlayerPronto', %num, false);
		} else {
			//torna pronto:
			commandToServer('setPlayerPronto', %num, true);	
		}
	} else {
		%eval = "%playerPronto = $player" @ %num @ "Pronto;";
		eval(%eval);
		if(%playerPronto){
			%eval = "p" @ %num @ "Pronto_btn.setStateOn(true);";
			eval(%eval);
		} else {
			%eval = "p" @ %num @ "Pronto_btn.setStateOn(false);";
			eval(%eval);
		}
	}
}

function clientCmdSetPlayerPronto(%num, %param){
	%eval = "p" @ %num @ "Pronto_btn.setStateOn(%param);";
	eval(%eval);
	
	%eval = "%mySalaInsideNome_txt = salaInsideNome" @ %num @ "_txt;";
	eval(%eval);
	
	if(%mySalaInsideNome_txt.text $= $myPersona.nome)
		$estouProntoProJogo = %param;
	
	%eval = "$player" @ %num @ "Pronto = %param;";
	eval(%eval);
	
	clientVerificarPiscarProntoBtn();	
	
	if(%param == true){
		if(!$noSound)
			alxPlay(pronto);	
	}
	
	clientVerifyIniciarJogo($PlayersNaSalaEmQueEstou); //verifica se pode ligar o IniciarJogo_btn
}

function clientVerificarPiscarProntoBtn()
{
	if($estouProntoProJogo)
	{
		clientResetPiscarMyProntoBtn();
		return;
	}
		
	for (%i = 1; %i < $PlayersNaSalaEmQueEstou+1; %i++)
	{
		%eval = "%playerPronto = $player" @ %i @ "Pronto;";
		eval(%eval);
		
		if(%playerPronto == true)
		{
			%prontos++;
			echo("Player " @ %i @ " Pronto");
		}
	}
	
	if(%prontos != $PlayersNaSalaEmQueEstou-1 || $PlayersNaSalaEmQueEstou <= 1)
	{
		clientResetPiscarMyProntoBtn();	
		return;
	}
		
	clientPiscarMyProntoBtn();
}

function clientResetPiscarMyProntoBtn()
{
	cancel($schedulePiscarProntoBtn);
	%myProntoBtn =  clientGetMyProntoBtn();
	if(isObject(%myProntoBtn))
		%myProntoBtn.setBitmap("game/data/images/pronto_btn");
}

function clientGetMyProntoBtn()
{
	for (%i = 1; %i < $PlayersNaSalaEmQueEstou+1; %i++)
	{
		%eval = "%salaInsideNome_txt = salaInsideNome" @ %i @ "_txt;";
		eval(%eval);
		
		if(%salaInsideNome_txt.text $= $myPersona.nome)
		{
			%eval = "%myProntoBtn = p" @ %i @ "Pronto_btn;";
			eval(%eval);
			
			return %myProntoBtn;
		}
	}
}

function clientPiscarMyProntoBtn()
{
	cancel($schedulePiscarProntoBtn);
	
	if($myProntoBtnHilighted)
	{
		clientUnlightMyProntoBtn();
		$schedulePiscarProntoBtn = schedule(500, 0, "clientPiscarMyProntoBtn");
		return;	
	}
	
	clientHilightMyProntoBtn();
	$schedulePiscarProntoBtn = schedule(500, 0, "clientPiscarMyProntoBtn");
}

function clientHilightMyProntoBtn()
{
	%myProntoBtn =  clientGetMyProntoBtn();
	%myProntoBtn.setBitmap("game/data/images/prontoBranco_btn");
	$myProntoBtnHilighted = true;
}

function clientUnlightMyProntoBtn()
{
	%myProntoBtn =  clientGetMyProntoBtn();
	%myProntoBtn.setBitmap("game/data/images/pronto_btn");
	$myProntoBtnHilighted = false;
}

function clientClearProntoBtns(%special){
	$estouProntoProJogo = false;
	for(%i = 1; %i < 7; %i++){
		%eval = "p" @ %i @ "Pronto_btn.setVisible(false);";	
		eval(%eval);
		%eval = "p" @ %i @ "Pronto_btn.setStateOn(false);";	
		eval(%eval);	
		if(%special){
			%eval = "$player" @ %i @ "Pronto = false;";
			eval(%eval);	
		}
	}
}


//
function clientPiscarIniciarJogoBtn()
{
	cancel($scheduleIniciarJogoBtn);
	
	if($iniciarJogoBtnHilighted)
	{
		clientUnlightIniciarJogoBtn();
		$scheduleIniciarJogoBtn = schedule(500, 0, "clientPiscarIniciarJogoBtn");
		return;	
	}
	
	clientHilightIniciarJogoBtn();
	$scheduleIniciarJogoBtn = schedule(500, 0, "clientPiscarIniciarJogoBtn");
}

function clientHilightIniciarJogoBtn()
{
	iniciarJogo_btn.setBitmap("game/data/images/iniciarJogoBranco_btn");
	$iniciarJogoBtnHilighted = true;
}

function clientUnlightIniciarJogoBtn()
{
	iniciarJogo_btn.setBitmap("game/data/images/iniciarJogo_btn");
	$iniciarJogoBtnHilighted = false;
}

function clientResetIniciarJogoBtn()
{
	cancel($scheduleIniciarJogoBtn);
	
	if($PlayersNaSalaEmQueEstou <= 2)
	{
		iniciarJogo_btn.setBitmap("game/data/images/iniciarJogoTreino_btn");
		return;	
	}
	
	iniciarJogo_btn.setBitmap("game/data/images/iniciarJogo_btn");
}

function clientVerificarPiscarIniciarJogoBtn()
{
	
	if($PlayersNaSalaEmQueEstou <= 2)
	{
		clientResetIniciarJogoBtn();
		return;
	}
	if($PlayersNaSalaEmQueEstou == 4 && $salaEmQueEstouTipoDeJogo $= "emDuplas")
	{
		clientResetIniciarJogoBtn();
		return;
	}
	
	
	%podeIr = clientVerifyPlayersProntos();	
	%souDonoDaSala = getSouDonoDaSala();
	
	if(%podeIr && %souDonoDaSala)
	{
		clientPiscarIniciarJogoBtn();
		return;
	}
	
	clientResetIniciarJogoBtn();
}

function getSouDonoDaSala()
{
	if(salaInsideNome1_txt.text $= $myPersona.nome)
		return true;
		
	return false;
}


//


function clientPopularDadosCriandoSala(){
	clientClearProntoBtns(true);
	if($myPersona.especie $= "gulok"){
		salaPersona1_img.bitmap = "game/data/images/persona_gulok";	
	} else {
		salaPersona1_img.bitmap = "game/data/images/salapersona";	
	}
	salaInsideNome1_txt.text = $myPersona.nome;
	salaInsideVitorias1_txt.text = $myPersona.TAXOvitorias;
	salaInsidePontos1_txt.text = $myPersona.TAXOpontos;
	salaInsideVisionario1_txt.text = $myPersona.TAXOvisionario;
	salaInsideArrebatador1_txt.text = $myPersona.TAXOarrebatador;
	if($myPersona.myComerciante !$= "-1"){
		salaInsideComerciante1_txt.text = $myPersona.myComerciante @ "%";
	} else {
		salaInsideComerciante1_txt.text = "??";
	}
	if($myPersona.myDiplomata !$= "-1"){
		salaInsideDiplomata1_txt.text = $myPersona.myDiplomata @ "%";
	} else {
		salaInsideDiplomata1_txt.text = "??";
	}
	
	clientCalcularGauge("vitorias", 1, $myPersona.TAXOvitorias, -1, "salaInside", $myPersona.nome, $myPersona.especie);
	clientCalcularGauge("pontos", 1, $myPersona.TAXOpontos, -1, "salaInside", $myPersona.nome, $myPersona.especie);
	clientCalcularGauge("visionario", 1, $myPersona.TAXOvisionario, -1, "salaInside", $myPersona.nome, $myPersona.especie);
	clientCalcularGauge("arrebatador", 1, $myPersona.TAXOarrebatador, -1, "salaInside", $myPersona.nome, $myPersona.especie);
	clientCalcularGauge("comerciante", 1, $myPersona.myComerciante, $myPersona.TAXOvitorias, "salaInside", $myPersona.nome, $myPersona.especie);
	clientCalcularGauge("diplomata", 1, $myPersona.myDiplomata, $myPersona.TAXOvitorias, "salaInside", $myPersona.nome, $myPersona.especie);
	
	p1Pronto_btn.setVisible(true);
	
	clientShowFator(1, $myPersona.myDif, $myPersona.pk_fichas); //mostrar meu fator imperial no player 1 da sala
	
	$myNumNaSala = 1;
}

function clientShowFator(%pos, %fator, %fichas)
{
	%eval = "p" @ %pos @ "Fator_img.setVisible(true);";
	eval(%eval);
	
	if($salaEmQueEstouTipoDeJogo $= "poker")
	{	
		%tooltipTxt = "Total de fichas de Poker Imperial";
		%eval = "salaInsideP" @ %pos @ "_fator_txt.text = " @ %fichas @ ";";
		eval(%eval);
		%eval = "salaInsideP" @ %pos @ "_fator_txt.ToolTip = %tooltipTxt;";
		eval(%eval);
		return;	
	}
	
	%tooltipTxt = "A força desta Persona, com base nas pesquisas desenvolvidas. Diferenças superiores a 100 são dificílimas/facílimas.";
	%eval = "salaInsideP" @ %pos @ "_fator_txt.text = " @ %fator @ ";";
	eval(%eval);
	%eval = "salaInsideP" @ %pos @ "_fator_txt.ToolTip = %tooltipTxt;";
	eval(%eval);
}

function clientAskEntrarNaSala(%posDaSala){
	clientPushAguardeMsgBox(); //IMPEDE Q O CLIENT ENTRE DUAS VEZES COM DUPLO-CLIQUE
	$PlayersNaSalaEmQueEstou = 0;
	%sala = $salasNaTela.getObject(%posDaSala - 1); //pega a sala correta conforme a posição na tela;
	
	if(%sala.poker && $myPersona.taxoVitorias < 25)
	{
		clientMsgBoxOKT("IMPOSSÍVEL ENTRAR", "O POKER IMPERIAL EXIGE PELO MENOS 25 VITÓRIAS.");	
		clientPopAguardeMsgBox();
		return;
	}
	if(%sala.poker && %sala.blind * 20 > $myPersona.pk_fichas)
	{
		clientMsgBoxOKT("FICHAS INSUFICIENTES", "VOCÊ NÃO POSSUI FICHAS PARA ENTRAR NESTA SALA.");	
		clientPopAguardeMsgBox();
		return;
	}
	if(%sala.poker && $myPersona.especie $= "gulok")
	{
		clientMsgBoxOKT("IMPOSSÍVEL ENTRAR", "APENAS HUMANOS PODEM JOGAR POKER IMPERIAL.");	
		clientPopAguardeMsgBox();
		return;
	}
	
	$myLastSeenBlind = %sala.blind;
	commandToServer('entrarNaSala', %sala.num); //pede pra entrar
}

//pro client não entrar se a sala ainda não tiver idJogo:
function clientCmdSalaSendoConfigurada()
{
	clientMsgBoxOKT("CONFIGURANDO SALA", "TENTE NOVAMENTE DENTRO DE ALGUNS SEGUNDOS.");	
	clientPopAguardeMsgBox();
}

function clientCmdIncluirPlayerNaSala(%novaPersonaStats){
	echo("Inclui uma persona");
	clientPopKickBtns();
	%proximoPlayer = $PlayersNaSalaEmQueEstou + 1;
	
	%eval = "salaPersona" @ %proximoPlayer @ "_img.setVisible(true);";
	eval(%eval);
	
	echo("%novaPersonaStats = " @ %novaPersonaStats);
	clientPopularDadosNoPlayer(%proximoPlayer, %novaPersonaStats);
	$PlayersNaSalaEmQueEstou++;
	clientVerificarPiscarProntoBtn();
	%p1Nome = salaInsideNome1_txt.text;
	if($myPersona.nome $= %p1Nome){
		clientVerifyIniciarJogo($PlayersNaSalaEmQueEstou);
	} else {
		iniciarJogo_btn.setActive(false);	
	}
	
	clientAskGetSalaDif(); //recalcula a dificuldade da sala;
}

function clientPopularDadosNoPlayer(%playerNum, %dados, %special){
	//echo("POPULANDO DADOS NO PLAYER:");
	//echo("%playerNum = " @ %playerNum);
	//echo("%dados = " @ %dados);
	%nome = getWord(%dados, 0);
	%TAXOvitorias = getWord(%dados, 1);
	%TAXOpontos = getWord(%dados, 2);
	%TAXOvisionario = getWord(%dados, 3);
	%TAXOarrebatador = getWord(%dados, 4);
	%TAXOcomerciante = getWord(%dados, 5);
	%TAXOdiplomata = getWord(%dados, 6);
	%graduacaoNome = getWord(%dados, 7);
	%pronto = getWord(%dados, 8);
	%especie = getWord(%dados, 9);
	%fatorImperial = getWord(%dados, 10);
	%pk_fichas = getWord(%dados, 11);
	
	%myImg = "salaPersona" @ %playerNum @"_img";
	if(%especie $= "gulok"){
		%bitmap = "game/data/images/persona_gulok";	
	} else {
		%bitmap = "game/data/images/salapersona";	
	}
	%myImg.bitmap = %bitmap;
	
	
	%percent = "%";
	%indefinido = "??";
	%eval = "salaInsideNome" @ %playerNum @ "_txt.text = %nome;";
	eval(%eval);
	%eval = "salaInsideVitorias" @ %playerNum @ "_txt.text = %TAXOvitorias;";
	eval(%eval);
	%eval = "salaInsidePontos" @ %playerNum @ "_txt.text = %TAXOpontos;";
	eval(%eval);
	%eval = "salaInsideVisionario" @ %playerNum @ "_txt.text = %TAXOvisionario;";
	eval(%eval);
	%eval = "salaInsideArrebatador" @ %playerNum @ "_txt.text = %TAXOarrebatador;";
	eval(%eval);
	if(%TAXOcomerciante !$= "-1"){
		%eval = "salaInsideComerciante" @ %playerNum @ "_txt.text = %TAXOcomerciante @ %percent;";
		eval(%eval);
	} else {
		%eval = "salaInsideComerciante" @ %playerNum @ "_txt.text = %indefinido;";
		eval(%eval);	
	}
	if(%TAXODiplomata !$= "-1"){
		%eval = "salaInsideDiplomata" @ %playerNum @ "_txt.text = %TAXOdiplomata @ %percent;";
		eval(%eval);
	} else {
		%eval = "salaInsideDiplomata" @ %playerNum @ "_txt.text = %indefinido;";
		eval(%eval);	
	}
	
	
	//mostra o btn de pronto do jogador em questão:
	%eval = "p" @ %playerNum @ "Pronto_btn.setVisible(true);";
	eval(%eval);
	%eval = "%myPlayerPronto = $player" @ %playerNum @ "Pronto;";
	eval(%eval);
	if(%myPlayerPronto || %pronto){
		clientCmdSetPlayerPronto(%playerNum, true);
	} else {
		clientCmdSetPlayerPronto(%playerNum, false);
	}
	
	//mostra o fatorImperial da persona:
	clientShowFator(%playerNum, %fatorImperial, %pk_fichas);
			
	//seta os gauges:
	if(%special){
		clientCalcularGauge("vitorias", %playerNum, %TAXOvitorias, -1, "salaInside", %nome, %especie);
		clientCalcularGauge("pontos", %playerNum, %TAXOpontos, -1, "salaInside", %nome, %especie);
		clientCalcularGauge("visionario", %playerNum, %TAXOvisionario, -1, "salaInside", %nome, %especie);
		clientCalcularGauge("arrebatador", %playerNum, %TAXOarrebatador, -1, "salaInside", %nome, %especie);
		clientCalcularGauge("comerciante", %playerNum, %TAXOcomerciante, %TAXOvitorias, "salaInside", %nome, %especie);
		clientCalcularGauge("diplomata", %playerNum, %TAXOdiplomata, %TAXOvitorias, "salaInside", %nome, %especie);
	} else {
		clientGaugeGradual(50, "vitorias", %playerNum, %TAXOvitorias, -1, "salaInside", %nome, %especie);
		clientGaugeGradual(50, "pontos", %playerNum, %TAXOpontos, -1, "salaInside", %nome, %especie);
		clientGaugeGradual(50, "visionario", %playerNum, %TAXOvisionario, -1, "salaInside", %nome, %especie);
		clientGaugeGradual(50, "arrebatador", %playerNum, %TAXOarrebatador, -1, "salaInside", %nome, %especie);
		clientGaugeGradual(50, "comerciante", %playerNum, %TAXOcomerciante, %TAXOvitorias, "salaInside", %nome, %especie);
		clientGaugeGradual(50, "diplomata", %playerNum, %TAXOdiplomata, %TAXOvitorias, "salaInside", %nome, %especie);
	}
}

function clientGaugeGradual(%vezes, %gauge, %playerNum, %dado, %vit, %onde, %nome, %especie){
	for(%i = 0; %i < %vezes; %i++){
		schedule((50 * (%i + 1)), 0, "clientCalcularGauge", %gauge, %playerNum, ((%dado / %vezes) * (%i + 1)), %vit, %onde, %nome, %especie);
	}
}

function clientCalcularGauge(%oQue, %playerNum, %dado, %vit, %onde, %nome, %especie){
	//antes de qualquer coisa, confirma que a persona ainda está neste número:
	%eval = "%playerName = " @ %onde @ "Nome" @ %playerNum @ "_txt.text;";
	eval(%eval);
	if(%nome $= %playerName){
		//antes de mais nada, estabelece os valores corretos conforme a resolução de tela:
		%eval = "%myIf = $primeira" @ %onde @ ";";
		eval(%eval);
		if(%myIf){
			//echo("RES::DEFAULT(1024x768)");
			%myX = 141;
			%myY = 23;
		} else {
			/*
			%myWindowResX = sceneWindow2d.getWindowExtents();
			%myWindowResX = getWord(%myWindowResX, 2);
			//echo("myWindowResX = " @ %myWindowResX);
			if (%myWindowResX == 800){
				%myX = 110;
				%myY = 18;
			} else if (%myWindowResX == 933){
				%myX = 128;
				%myY = 21;
			} else if (%myWindowResX == 1024){
				%myX = 141;
				%myY = 23;
			} else if (%myWindowResX == 1400){
				%myX = 193;
				%myY = 31;
			}
			*/
			%myWindowResX = sceneWindow2d.getWindowExtents();
			$myWindowResX = getWord(%myWindowResX, 2);
			%myX = calcularNaRes(141, $myWindowResX);
			%myY = calcularNaRes(23, $myWindowResX);
		}
				
		if(%vit $= "-1"){
			switch$(%oQue){
				case "vitorias":
				%limiteAzul = 3; //Vira cadete, abre algumas pesquisas
				%limiteVerde = 10; //+1 Imp
				%limiteDourado = 25; //+2 Imp
				%limiteVermelho = 50; //+3 Imp
				%limiteRoxo = 100; //+4 Imp
				%limitePreto = 150; //+5 Imp
				%limitePretoAzul = 200; //+6 Imp
				%limitePretoVerde = 300; //+8 Imp
				%limitePretoDourado = 400; //+10 Imp
				%limitePretoVermelho = 500; //+15 Imp, Bônus de Economia x2
				%limitePretoRoxo = 650; //+20 Imp, Bônus de Economia x2 
				%limiteBranco = 800; //+25 Imp, Bônus de Economia x2 
				clientSetGradIcon(%playerNum, %dado, %onde, %especie); //passa as vitórias como parâmetro
				
				case "pontos":
				%limiteAzul = 60; //+5 Créditos
				%limiteVerde = 200; //+10 Créditos
				%limitedourado = 500; //+15 Créditos
				%limiteVermelho = 1000; //+20 Créditos
				%limiteRoxo = 2000; //+25 Créditos
				%limitePreto = 4500; //+30 Créditos
				%limitePretoAzul = 6000; //+35 Créditos
				%limitePretoVerde = 9000; //+40 Créditos
				%limitePretoDourado = 12000; //+45 Créditos
				%limitePretoVermelho = 15000; //+50 Créditos
				%limitePretoRoxo = 20000; //+60 Crédtios
				%limiteBranco = 25000; //+75 Créditos
							
				case "visionario":
				%limiteAzul = 1;
				%limiteVerde = 3;
				%limitedourado = 6;
				%limiteVermelho = 10;
				%limiteRoxo = 20;
				%limitePreto = 30;
				%limitePretoAzul = 40;
				%limitePretoVerde = 60;
				%limitePretoDourado = 80;
				%limitePretoVermelho = 100;
				%limitePretoRoxo = 130;
				%limiteBranco = 160;
				
				case "arrebatador":
				%limiteAzul = 1;
				%limiteVerde = 3;
				%limitedourado = 6;
				%limiteVermelho = 10;
				%limiteRoxo = 20;
				%limitePreto = 30;
				%limitePretoAzul = 40;
				%limitePretoVerde = 60;
				%limitePretoDourado = 80;
				%limitePretoVermelho = 100;
				%limitePretoRoxo = 130;
				%limiteBranco = 160;
			}
			
			//genérica para ligar gauges:
			%TAXOdaVez = %dado;
			%eval = "%gauge1DaVez = " @ %onde @ %oQue @ "G1_" @ %playerNum @ ";";
			eval(%eval);
			%eval = "%gauge2DaVez = " @ %onde @ %oQue @ "G2_" @ %playerNum @ ";";
			eval(%eval);
			%gauge1DaVez.setVisible(false);	
			%gauge2DaVez.setVisible(false);	
			
			//liga os Gauges corretos:
			if(%TAXOdaVez > 0){
				%gauge1DaVez.setVisible(true);	
			}
			if(%TAXOdaVez > %limiteAzul){
				%gauge2DaVez.setVisible(true);	
				%ext = %myX SPC %myY;
				%gauge1DaVez.extent = %ext;
			} 
			
			//seta o tamanho dos gauges:
			if(%TAXOdaVez <= %limiteAzul){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkAzul" @ "_" @ %especie; 
				%extX = mFloor((%TAXOdaVez / %limiteAzul) * %myX);
				%ext = %extX SPC %myY;
				%gauge1DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limiteAzul && %TAXOdaVez <= %limiteVerde){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkAzul" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkVerde" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez - %limiteAzul) / (%limiteVerde - %limiteAzul)) * %myX);
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limiteVerde && %TAXOdaVez <= %limitedourado){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkVerde" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkdourado" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez  - %limiteVerde)/ (%limitedourado - %limiteVerde)) * %myX);
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limitedourado && %TAXOdaVez <= %limiteVermelho){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkdourado" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkVermelho" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez - %limitedourado) / (%limiteVermelho - %limitedourado)) * %myX);
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limiteVermelho && %TAXOdaVez <= %limiteRoxo){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkVermelho" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkRoxo" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez - %limiteVermelho) / (%limiteRoxo - %limiteVermelho)) * %myX);
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limiteRoxo && %TAXOdaVez <= %limitePreto){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkRoxo" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkPreto" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez - %limiteRoxo) / (%limitePreto - %limiteRoxo)) * %myX);
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limitePreto && %TAXOdaVez <= %limitePretoAzul){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkPreto" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkPretoAzul" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez - %limitePreto) / (%limitePretoAzul - %limitePreto)) * %myX);
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limitePretoAzul && %TAXOdaVez <= %limitePretoVerde){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkPretoAzul" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkPretoVerde" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez - %limitePretoAzul) / (%limitePretoVerde - %limitePretoAzul)) * %myX);
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limitePretoVerde && %TAXOdaVez <= %limitePretoDourado){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkPretoVerde" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkPretoDourado" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez - %limitePretoVerde) / (%limitePretoDourado - %limitePretoVerde)) * %myX);
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limitePretoDourado && %TAXOdaVez <= %limitePretoVermelho){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkPretoDourado" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkPretoVermelho" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez - %limitePretoDourado) / (%limitePretoVermelho - %limitePretoDourado)) * %myX);
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limitePretoVermelho && %TAXOdaVez <= %limitePretoRoxo){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkPretoVermelho" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkPretoRoxo" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez - %limitePretoVermelho) / (%limitePretoRoxo - %limitePretoVermelho)) * %myX);
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			} else if(%TAXOdaVez > %limitePretoRoxo){
				%gauge1DaVez.bitmap = "~/data/images/personaMarkPretoRoxo" @ "_" @ %especie; 
				%gauge2DaVez.bitmap = "~/data/images/personaMarkBranco" @ "_" @ %especie; 
				%extX = mFloor(((%TAXOdaVez - %limitePretoRoxo) / (%limiteBranco - %limitePretoRoxo)) * %myX);
				if(%extX > %myX){
					%extX = %myX;		
				}
				%ext = %extX SPC %myY;
				%gauge2DaVez.extent = %ext;
			}
		} else { //abaixo é para Comerciante e Diplomata:
			%eval = "%gaugeDaVez = " @ %onde @ %oQue @ "G1_" @ %playerNum @ ";";
			eval(%eval);
			if(%vit <= 3){
				%eval = "%gaugeVit = " @ %onde @ "VitoriasG1_" @ %playerNum @ ";";
				eval(%eval);
			} else {
				%eval = "%gaugeVit = " @ %onde @ "VitoriasG2_" @ %playerNum @ ";";
				eval(%eval);
			}
			
			if(%dado > 0){
				%gaugeDaVez.setVisible(true);	
				%gaugeDaVez.bitmap = %gaugeVit.bitmap; //deixa o gauge com a mesma cor do gauge das vitórias;
			
				%extX = mFloor((%dado / 100) * %myX);
				if(%extX > %myX){
					%extX = %myX;		
				}
				if(%extX < 0){
					%extX = 0;		
				}
				%ext = %extX SPC %myY;
				%gaugeDaVez.extent = %ext;
			} else {
				%gaugeDaVez.setVisible(false);	
			}
		}
	}
}

function clientSetGradIcon(%playerNum, %dado, %onde, %especie){
	%g0 = 0; //bolinha cinza (Recruta)
	
	//Chapéu cinza (Cadete):
	%g1 = 1; 
	%g2 = 2;
	%g3 = 3;
	
	//Chapéu azul pqno:
	%g4 = 4;
	%g5 = 5;
	%g6 = 6;
	
	//Chapéu verde pqno (Aspirante):
	%g7 = 7;
	%g8 = 8;
	%g9 = 10;
	
	//Chapéu amarelo pqno:
	%g10 = 12;
	%g11 = 14;
	%g12 = 16;
	
	//Chapéu vermelho pqno (Sargento):
	%g13 = 19;
	%g14 = 22;
	%g15 = 25;
	
	//Chapéu roxo pqno:
	%g16 = 28;
	%g17 = 30;
	%g18 = 33;
	
	//Chapéu azul gordo:
	%g19 = 36;
	%g20 = 39;
	%g21 = 42;
	
	//Chapéu verde gordo (Tenente):
	%g22 = 45;
	%g23 = 48;
	%g24 = 50; 
	
	//Chapéu amarelo gordo:
	%g25 = 53;
	%g26 = 56;
	%g27 = 59;
	
	//Chapéu vermelho gordo:	
	%g28 = 62;
	%g29 = 65;
	%g30 = 68;
	
	//Chapéu roxo gordo:
	%g31 = 71;
	%g32 = 74;
	%g33 = 77;
	
	//Chapéu preto (Capitão):
	%g34 = 80;
	%g35 = 84;
	%g36 = 88; 
	%g37 = 92;
	%g38 = 96;
	%g39 = 100;
	
	//Estrelas Azuis:
	%g40 = 105;
	%g41 = 110; 
	%g42 = 115; 
	
	//Estrelas Verdes:
	%g43 = 120;
	%g44 = 126;
	%g45 = 132;
	
	//Estrelas Amarelas:
	%g46 = 138;
	%g47 = 144;
	%g48 = 150;
	
	//Estrelas Vermelhas:
	%g49 = 156;
	%g50 = 162;
	%g51 = 168;
	
	//Estrelas Roxas:
	%g52 = 175;
	%g53 = 182;
	%g54 = 190;
			
	//Estrela Nova:
	%g55 = 200;
	
	//montagem azul:
	%g56 = 210;
	%g57 = 220;
	%g58 = 230;
	%g59 = 240;
	%g60 = 250;
	
	//montagem verde:
	%g61 = 260;
	%g62 = 270;
	%g63 = 280;
	%g64 = 290;
	%g65 = 300;
	
	//montagem amarela:
	%g66 = 320;
	%g67 = 340;
	%g68 = 360;
	%g69 = 380;
	%g70 = 400;
	
	//montagem vermelha:
	%g71 = 420;
	%g72 = 440;
	%g73 = 460;
	%g74 = 480;
	%g75 = 500;
	
	//montagem roxa:
	%g76 = 520;
	%g77 = 540;
	%g78 = 560;
	%g79 = 580;
	%g80 = 600;
	
	//montagem branca (final):
	%g81 = 650;
	%g82 = 680;
	%g83 = 720;
	%g84 = 760;
	%g85 = 800;
	
	%numDeGrads = 86; //número de graduações
		
	for(%i = 0; %i < %numDeGrads; %i++){
		%eval = "%myG1 = %g" @ %i @ ";";
		eval(%eval);
		%eval = "%myG2 = %g" @ %i + 1 @ ";";
		eval(%eval);
		
		if(%dado >= %g85){
			%gradIconNum = 85; 
		} else {
			if(%dado >= %myG1 && %dado < %myG2){
				%gradIconNum = %i; //pega o número da grad
				%i = %numDeGrads; //sai do loop
			}
		}
	}
	
	%eval = "%image = " @ %onde @ "grad" @ %playerNum @ ";";
	eval(%eval);
	%bitmap = "~/data/images/grad" @ %gradIconNum; 
	if(%especie $= "gulok")
		%bitmap = "~/data/images/gradGuloks/grad" @ %gradIconNum; 
		
	%image.bitmap = %bitmap;
}


function clientCmdEntrarNaSala(%playersOn, %statsP1, %statsP2, %statsP3, %statsP4, %statsP5, %statsP6, %salaNum, %planeta, %tipoDeJogo, %turno, %lotacao, %duplasInfo){
	clientResetPiscarMyProntoBtn();
	clientApagarDuplasMarks();
	clientPopKickBtns();
	clientClearProntoBtns(true); //limpa os btns de pronto, pois pode ter vindo de outra sala;
	clientPopAguardeMsgBox(); //devolve o controle ao usuário
	Canvas.popDialog(atrioGui); //apaga o átrio
	Canvas.pushDialog(newSalaInsideGui); //chama o gui de dentro da sala
	zerarPersonasImgs(); //apaga as personas do último jogo;
	iniciarJogo_btn.setActive(false); //torna inativo o botão de iniciarJogo;
	salaInsideSalaNum_txt.text = %salaNum;
	salaInsideTurno_img.bitmap = "~/data/images/" @ %turno @ "segundos_img";
	salaInsideTipoDeJogo_img.bitmap = "~/data/images/" @ %tipoDeJogo @ "_img";
	salaInsideLotacao_img.bitmap = "~/data/images/" @ %lotacao @ "jogadores_img";
	salaInsidePlaneta_img.bitmap = "~/data/images/planeta" @ %planeta @ "_img";
	
	$salaEmQueEstouTipoDeJogo = %tipoDeJogo;
	$salaEmQueEstouPlaneta = %planeta;	
		
	echo("STATS RECEBIDOS (P1) = " @ %statsP1);
	echo("STATS RECEBIDOS (P2) = " @ %statsP2);
	echo("STATS RECEBIDOS (P3) = " @ %statsP3);
	echo("STATS RECEBIDOS (P4) = " @ %statsP4);
	echo("STATS RECEBIDOS (P5) = " @ %statsP5);
	echo("STATS RECEBIDOS (P6) = " @ %statsP6);
		
	for(%i = 0; %i < %playersOn; %i++){
		%eval = "%statsCorretos = %statsP" @ %i + 1 @ ";";
		eval(%eval);
		%nome = getWord(%statsCorretos, 0);
		%TAXOvitorias = getWord(%statsCorretos, 1);
		%TAXOpontos = getWord(%statsCorretos, 2);
		%TAXOvisionario = getWord(%statsCorretos, 3);
		%TAXOarrebatador = getWord(%statsCorretos, 4);
		%TAXOcomerciante = getWord(%statsCorretos, 5);
		%TAXOdiplomata = getWord(%statsCorretos, 6);
		%graduacaoNome = getWord(%statsCorretos, 7);
		%pronto = getWord(%statsCorretos, 8);
		%especie = getWord(%statsCorretos, 9);
		%fatorImperial = getWord(%statsCorretos, 10);
		%pk_fichas = getWord(%statsCorretos, 11);
		
		%eval = "salaPersona" @ %i + 1 @ "_img.setVisible(true);";
		eval(%eval);
				
		%stats = %nome SPC %TAXOvitorias SPC %TAXOpontos SPC %TAXOvisionario SPC %TAXOarrebatador SPC %TAXOcomerciante SPC %TAXOdiplomata SPC %graduacaoNome SPC %pronto SPC %especie SPC %fatorImperial SPC %pk_fichas;
		clientPopularDadosNoPlayer(%i + 1, %stats); //popula os dados no player correto;
	}
	echo("entrei na sala!");
		
	$myNumNaSala = %playersOn;
	$PlayersNaSalaEmQueEstou = %playersOn;
	$primeiraSalaInside = false;
	initSalaChatGui();
	escolhaDeTipoDeJogoGui.setVisible(false);
	escolhaDeLotacaoGui.setVisible(false);
	escolhaDeTurnoGui.setVisible(false);
	clientSetDadosPlaneta(%planeta);
	
	if($buzinaBlocked){
		buzinaSala_btn.setActive(false);
	} else {
		buzinaSala_btn.setActive(true);
	}
	
	clientAskGetSalaDif(); //recalcula a dificuldade da sala;
	
	//pega as duplas, caso seja um jogo de duplas:
	if(%tipoDeJogo $= "emDuplas"){
		%p1Dupla = firstWord(%duplasInfo);
		%p2Dupla = getWord(%duplasInfo, 1);
		%p3Dupla = getWord(%duplasInfo, 2);
		%p4Dupla = getWord(%duplasInfo, 3);
		%p5Dupla = getWord(%duplasInfo, 4);
		%p6Dupla = getWord(%duplasInfo, 5);
		clientCmdSetarDuplas(%p1Dupla, %p2Dupla, %p3Dupla, %p4Dupla, %p5Dupla, %p6Dupla);	
	}
	clientVerificarPiscarProntoBtn();
	clientSetSalaInsideLotacaoTab(%lotacao, $myLastSeenBlind);
}

function clientSetSalaInsideLotacaoTab(%lotacao, %blind)
{
	if($salaEmQueEstouTipoDeJogo $= "poker")
	{
		salaInside_lotacao_btn.text = $TXT_blind;
		salaInsideLotacao_img.bitmap = "~/data/images/blind_" @ %blind @ "_img";
		return;	
	}
	
	salaInsideLotacao_img.bitmap = "~/data/images/" @ %lotacao @ "jogadores_img";
	salaInside_lotacao_btn.text = $TXT_lotacao;
}

function clientAskIniciarJogo(){
	clientPushAguardeMsgBox();
	commandToServer('iniciarJogo');	
}

function clientAlterarAtrioPagina(%maisOuMenos){
	if(%maisOuMenos $= "mais"){
		$atrioPagina++;
	} else {
		$atrioPagina--;
	}
	commandToServer('alterarAtrioPagina', $atrioPagina);
	clientVerifyAtrioPaginas();
}

function clientVerifyAtrioPaginas(){
	if($atrioPagina > 1){
		atrioSetaEsq.setActive(true);	
	} else {
		atrioSetaEsq.setActive(false);	
	}
	
	
	%numDeSalas = $totalDeSalas;
	
	
	if(%numDeSalas > 6){
		if($atrioPagina < %numDeSalas / 6){
			atrioSetaDir.setActive(true);	
		} else {
			atrioSetaDir.setActive(false);	
		}
	} else {
		atrioSetaDir.setActive(false);	
	}
	
	atrio_pagina_txt.text = $atrioPagina @ "  /  " @ mFloor($totalDeSalas / 6.1) + 1;
}

function clientCmdAtualizarNumDePersonasNoAtrio(%num){
	atrioPersonasNoChat_txt.text = "( " @ %num @ " Personas )";  //marca o número de personas no chat	
}

$atrioPagina = 1;

function clientCmdPopularAtrio(%salasString, %vindoDeOnde, %personaDados, %numDePersonasNoChat)
{
	$souObservador = false; 
	clientResetPiscarMyProntoBtn();
	clientCalcularMyPersonaDif();
	clientPopAguardeMsgBox(); //devolve o controle ao usuário
	clientZerarAtrio(); //zera o átrio
	atrioPersonasNoChat_txt.text = "( " @ %numDePersonasNoChat @ " Personas )";  //marca o número de personas no chat
		
	if(%vindoDeOnde $= "sala")
	{
		canvas.popDialog(newSalaInsideGui);
		canvas.pushDialog(atrioGui);
		$PlayersNaSalaEmQueEstou = 0;
		clientPopularDados("Atrio", %personaDados);
		initAtrioChatGui();
	}
	
	if(!$jahRecebiMsgInicial)
	{
		atrioChatVectorText.pushBackLine("GM-BOT >> Não tem contra quem jogar?", 0);		
		atrioChatVectorText.pushBackLine("  É porque ainda estamos em fase ALPHA!", 0);		
		atrioChatVectorText.pushBackLine("  Novos jogadores se cadastram todos os dias.", 0);		
		atrioChatVectorText.pushBackLine("  Permaneça neste chat para ouvir um", 0);		
		atrioChatVectorText.pushBackLine("  aviso sonoro assim que uma nova sala for criada", 0);		
		atrioChatVectorText.pushBackLine("  (mesmo que você minimize o Império).", 0);
		atrioChatVectorText.pushBackLine("", 0);
		atrioChatVectorText.pushBackLine("GM-BOT >> Fique atento ao horário nobre", 0);
		atrioChatVectorText.pushBackLine("do Projeto Império: todos os dias entre", 0);
		atrioChatVectorText.pushBackLine("21:00 e 23:59 todos os jogadores ganham", 0);
		atrioChatVectorText.pushBackLine("um bônus de +2 Créditos por partida!", 0);
		$jahRecebiMsgInicial = true;
	}
	
	
	%numDeSalas = getWord(%salasString, 0);	
	$totalDeSalas = %numDeSalas;
	%start = 1 + (($atrioPagina - 1) * 6);
	
	for(%i = 1; %i < %numDeSalas + 1; %i++)
	{
		%num = %i - (6 * ($atrioPagina - 1));	
		%wordPos = 1 + (12 * (%num - 1));
		if(!isObject(%sala[%i]))
			%sala[%i] = new ScriptObject(){};	
				
		%sala[%i].num = getWord(%salasString, %wordPos);
		%sala[%i].numDePlayers = getWord(%salasString, %wordPos + 1);
		%sala[%i].emJogo = getWord(%salasString, %wordPos + 2);
		%sala[%i].lotacao = getWord(%salasString, %wordPos + 3);
		%sala[%i].planeta = getWord(%salasString, %wordPos + 4);
		%sala[%i].emDuplas = getWord(%salasString, %wordPos + 5);
		%sala[%i].semPesquisas = getWord(%salasString, %wordPos + 6);
		%sala[%i].handicap = getWord(%salasString, %wordPos + 7);
		%sala[%i].set = getWord(%salasString, %wordPos + 8);
		%sala[%i].poker = getWord(%salasString, %wordPos + 9);
		%sala[%i].mediaVit = getWord(%salasString, %wordPos + 10);
		%sala[%i].blind = getWord(%salasString, %wordPos + 11);
	}
		
	for(%i = %start; %i < %start + 6; %i++)
	{
		%sala = %sala[%i];
		if(isObject(%sala)){
			$salasNaTela.add(%sala);
			echo("adicionando sala ao Átrio: SALA " @ %i);
			%num = %i - (6 * ($atrioPagina - 1));	
						
			clientSalaMostrarNaTela(%num);
			clientSalaSetarNumero(%sala, %num);
			clientSalaSetarLotacao(%sala, %num);
			clientSalaSetarEntrarBtn(%sala, %num);
			clientSalaSetarTipoDeJogo(%sala, %num);
			clientSalaSetarPlaneta(%sala, %num);
			clientSalaSetarDificuldade(%sala, %num);
		}
	}
	
	echo("$salasAbertas = " @ $salasAbertas @ "; $lastSalasAbertas = " @ $lastSalasAbertas);
	if($salasAbertas > $lastSalasAbertas && $salasAbertas > 0)
		clientPingNovaSalaAberta();
	
	clientVerifyAtrioPaginas();
}

function clientPingNovaSalaAberta()
{
	%localTime = getWord(getLocalTime(), 1);
	%exp = explode(%localTime, ":");
	%hora = %exp.get[0];
	%minutos = %exp.get[1];
	%finalTime = %hora @ ":" @ %minutos;
	//clientCmdUpdateAtrioChatText("GM-BOT", "Nova sala aberta às " @ %finalTime @ ".");
	alxPlay(novaSalaAberta);
}
function clientClearSalaDifIcons(%salaNum){
	%eval = "atrioSala" @ %salaNum @ "DifFacilima_icon.setVisible(false);";
	eval(%eval);
	%eval = "atrioSala" @ %salaNum @ "DifFacil_icon.setVisible(false);";
	eval(%eval);	
	%eval = "atrioSala" @ %salaNum @ "DifCompetitiva_icon.setVisible(false);";
	eval(%eval);	
	%eval = "atrioSala" @ %salaNum @ "DifDificil_icon.setVisible(false);";
	eval(%eval);
	%eval = "atrioSala" @ %salaNum @ "DifDificilima_icon.setVisible(false);";
	eval(%eval);	
}

function clientClearSalaInsideDifIcons(){
	salaInsideDifFacilima_icon.setVisible(false);
	salaInsideDifFacil_icon.setVisible(false);
	salaInsideDifCompetitiva_icon.setVisible(false);
	salaInsideDifDificil_icon.setVisible(false);
	salaInsideDifDificilima_icon.setVisible(false);
}
	

function clientZerarAtrio(){
	echo("ZERANDO ATRIO");
	
	$lastSalasAbertas = $salasAbertas;
	$salasAbertas = 0;
	
	for(%i = 1; %i < 7; %i++){
		%eval = "atrioSala" @ %i @ "_img.setVisible(false);";	
		eval(%eval);
	}
	if(isObject($salasNaTela)){
		$salasNaTela.clear();	
	}
	for(%i = 1; %i < 7; %i++){
		%eval = "%sala = $sala" @ %i @ ";";
		eval(%eval);
		clientClearSalaDifIcons(%i);
		%sala.num = "no";
		%sala.numDePlayers = "no";
		%sala.ocupada = false;
		%sala.lotacao = "no";
		%sala.planeta = "no";
		%sala.emDuplas = false;
	}
}



function clientCmdRebuildSalaComDados(%playersAtivos, %statsP1, %statsP2, %statsP3, %statsP4, %statsP5, %statsP6){
	echo("STATS RECEBIDOS (P1) = " @ %statsP1);
	echo("STATS RECEBIDOS (P2) = " @ %statsP2);
	echo("STATS RECEBIDOS (P3) = " @ %statsP3);
	echo("STATS RECEBIDOS (P4) = " @ %statsP4);
	echo("STATS RECEBIDOS (P5) = " @ %statsP5);
	echo("STATS RECEBIDOS (P6) = " @ %statsP6);
	
	clientClearProntoBtns(true); //limpa os btns de pronto e as variáveis
	clientSalaInsideClear();
		
	for(%i = 0; %i < %playersAtivos; %i++){
		%eval = "%statsCorretos = %statsP" @ %i + 1 @ ";";
		eval(%eval);
		%nome = getWord(%statsCorretos, 0);
		%TAXOvitorias = getWord(%statsCorretos, 1);
		%TAXOpontos = getWord(%statsCorretos, 2);
		%TAXOvisionario = getWord(%statsCorretos, 3);
		%TAXOarrebatador = getWord(%statsCorretos, 4);
		%TAXOcomerciante = getWord(%statsCorretos, 5);
		%TAXOdiplomata = getWord(%statsCorretos, 6);
		%graduacaoNome = getWord(%statsCorretos, 7);
		%pronto = getWord(%statsCorretos, 8);
		%especie = getWord(%statsCorretos, 9);
		%fatorImperial = getWord(%statsCorretos, 10);
		%pk_fichas = getWord(%statsCorretos, 11);
		
		%eval = "salaPersona" @ %i + 1 @ "_img.setVisible(true);";
		eval(%eval);
				
		%stats = %nome SPC %TAXOvitorias SPC %TAXOpontos SPC %TAXOvisionario SPC %TAXOarrebatador SPC %TAXOcomerciante SPC %TAXOdiplomata SPC %graduacaoNome SPC %pronto SPC %especie SPC %fatorImperial SPC %pk_fichas;
		if(%nome $= $myPersona.nome){
			$myNumNaSala = %i + 1;	
		}
		clientPopularDadosNoPlayer(%i + 1, %stats, true); //popula os dados no player correto;
	}
			
	$PlayersNaSalaEmQueEstou = %playersAtivos; //soma eu ao número de players na sala;
	/////////////////////////////////////////////////////
	///////////////////////////////////////////////////
	//botão de iniciar jogo:
	iniciarJogo_btn.setBitmap("game/data/images/iniciarJogo_btn");
	%p1Nome = getWord(%statsP1, 0);
	if($myPersona.nome $= %p1Nome){
		clientVerifyIniciarJogo(%playersAtivos);
	} else {
		iniciarJogo_btn.setActive(false);
	}
	escolhaDeTipoDeJogoGui.setVisible(false);
	escolhaDeLotacaoGui.setVisible(false);
	escolhaDeTurnoGui.setVisible(false);
	
	clientAskGetSalaDif(); //recalcula a dificuldade da sala;
	
	if(!$vendoBaterGui){
		Canvas.popDialog(baterGui); //fecha o baterGui, caso ele esteja aberto;
	}
}
/*
function clientVerifyIniciarJogo(%playersAtivos){
	clientSetIniciarJogoBitmapPorPlayersAtivos(%playersAtivos);
	
	clientVerificarPiscarIniciarJogoBtn();
		
	if($myPersona.nome !$=  salaInsideNome1_txt.text || %playersAtivos <= 1){
		iniciarJogo_btn.setActive(false);
		return;
	}
	
	%podeIr = clientVerifyPlayersProntos();
	if(!%podeIr)
	{
		iniciarJogo_btn.setActive(false);
		return;	
	}
		
	if($salaEmQueEstouTipoDeJogo !$= "emDuplas")
	{
		if($salaEmQueEstouTipoDeJogo $= "poker")
		{
			if(%playersAtivos > 2)
			{
				iniciarJogo_btn.setActive(true);
				return;	
			}
			else
			{
				iniciarJogo_btn.setActive(false);
				return;	
			}
		}
		iniciarJogo_btn.setActive(true);
		return;
	}
	
	clientSetIniciarJogoBitmapPorPlayersAtivosEmDuplas(%playersAtivos);
	if(%playersAtivos == 4 || %playersAtivos == 6){
		iniciarJogo_btn.setActive(true);
		return;
	} 
					
	iniciarJogo_btn.setActive(false);
}
*/
//
function clientVerifyIniciarJogo(%playersAtivos){
	clientSetIniciarJogoBitmapPorPlayersAtivos(%playersAtivos);
	clientVerificarPiscarIniciarJogoBtn();
	iniciarJogo_btn.setActive(false);
		
	if($myPersona.nome !$=  salaInsideNome1_txt.text)
		return;
		
	if(%playersAtivos < 2)
		return;
		
	%podeIr = clientVerifyPlayersProntos();
	if(!%podeIr)
		return;	
		
	if($salaEmQueEstouTipoDeJogo !$= "emDuplas")
	{
		if($salaEmQueEstouTipoDeJogo $= "poker")
		{
			if(%playersAtivos > 2)
				iniciarJogo_btn.setActive(true);
			
			return;	
		}
		
		iniciarJogo_btn.setActive(true);
		return;
	}
	
	//se chegou aki, o jogo é em Duplas:	
	clientSetIniciarJogoBitmapPorPlayersAtivosEmDuplas(%playersAtivos);
	if(%playersAtivos == 4 || %playersAtivos == 6)
	{
		iniciarJogo_btn.setActive(true);
		return;
	} 
}

//

function clientSetIniciarJogoBitmapPorPlayersAtivos(%playersAtivos)
{
	if(%playersAtivos < 3)
	{
		iniciarJogo_btn.setBitmap("game/data/images/iniciarJogoTreino_btn");
		return;
	}
	
	iniciarJogo_btn.setBitmap("game/data/images/iniciarJogo_btn");	
}

function clientSetIniciarJogoBitmapPorPlayersAtivosEmDuplas(%playersAtivos)
{
	if(%playersAtivos == 4)
	{
		iniciarJogo_btn.setBitmap("game/data/images/iniciarJogoTreino_btn");
		return;
	} 
	
	if(%playersAtivos == 6)
		iniciarJogo_btn.setBitmap("game/data/images/iniciarJogo_btn");
}

function clientVerifyPlayersProntos(){
	for(%i = 1; %i < $PlayersNaSalaEmQueEstou+1; %i++){
		%eval = "%tempPlayer = $player" @ %i @ "Pronto;";
		eval(%eval);
		
		if(%tempPlayer){
			echo("Player " @ %i @ ": pronto!");
			%count++;	
		}
	}
	if(%count == $PlayersNaSalaEmQueEstou){
		%prontos = true;
		echo("TODOS PRONTOS");
	} else {
		%prontos = false;	
	}
	
	return (%prontos);
}

function clientSalaInsideClear(){
	echo("clientSalaInsideClear ACIONADO!");
	clientClearProntoBtns(); //limpa os btns de pronto
	%zero = "0";
	for(%i = 1; %i < 7; %i++){
		%eval = "salaPersona" @ %i @ "_img.setVisible(false);";
		eval(%eval);
		%eval = "p" @ %i @ "Fator_img.setVisible(false);";
		eval(%eval);
	}
	escolhaDeTipoDeJogoGui.setVisible(false);
	escolhaDeLotacaoGui.setVisible(false);
	escolhaDeTurnoGui.setVisible(false);
	
	clientPopKickBtns();
}

function clientAskVoltarPraSala(){
	$vendoBaterGui = false;
	if(!$estouNoTutorial){
		if($primeiroJogo){
			$primeiroJogo = false;
		}
		commandToServer('rebuildSalaComDados');
	} else {
		tut_fecharJogoTutorial();
		Canvas.popDialog(baterGui);
	}
	clientUnloadGame();
	clientFecharMsgBoxJogoInvalido();
	clientPopPk_fimSolitarioGui();
}



////////////////////////
//Observador:
function clientCmdEntrarNaSalaComoObservador(%playersOn, %statsP1, %statsP2, %statsP3, %statsP4, %statsP5, %statsP6, %salaNum, %planeta, %tipoDeJogo, %turno, %lotacao){
	clientPopAguardeMsgBox(); //devolve o controle ao usuário
	$souObservador = true; 
	Canvas.popDialog(atrioGui); //apaga o átrio
	Canvas.pushDialog(newSalaInsideGui); //chama o gui de dentro da sala
	zerarPersonasImgs(); //apaga as personas do último jogo;
	iniciarJogo_btn.setActive(false); //torna inativo o botão de iniciarJogo;
	salaInsideSalaNum_txt.text = %salaNum;
	salaInsideTurno_img.bitmap = "~/data/images/" @ %turno @ "segundos_img";
	salaInsideTipoDeJogo_img.bitmap = "~/data/images/" @ %tipoDeJogo @ "_img";
	salaInsideLotacao_img.bitmap = "~/data/images/" @ %lotacao @ "jogadores_img";
	salaInsidePlaneta_img.bitmap = "~/data/images/planeta" @ %planeta @ "_img";
	
	echo("STATS RECEBIDOS (P1) = " @ %statsP1);
	echo("STATS RECEBIDOS (P2) = " @ %statsP2);
	echo("STATS RECEBIDOS (P3) = " @ %statsP3);
	echo("STATS RECEBIDOS (P4) = " @ %statsP4);
	echo("STATS RECEBIDOS (P5) = " @ %statsP5);
	echo("STATS RECEBIDOS (P6) = " @ %statsP6);
		
	for(%i = 0; %i < %playersOn; %i++){
		%eval = "%statsCorretos = %statsP" @ %i + 1 @ ";";
		eval(%eval);
		%nome = getWord(%statsCorretos, 0);
		%TAXOvitorias = getWord(%statsCorretos, 1);
		%TAXOpontos = getWord(%statsCorretos, 2);
		%TAXOvisionario = getWord(%statsCorretos, 3);
		%TAXOarrebatador = getWord(%statsCorretos, 4);
		%TAXOcomerciante = getWord(%statsCorretos, 5);
		%TAXOdiplomata = getWord(%statsCorretos, 6);
		%graduacaoNome = getWord(%statsCorretos, 7);
		%pronto = getWord(%statsCorretos, 8);
		%especie = getWord(%statsCorretos, 9);
		%fatorImperial = getWord(%statsCorretos, 10);
		%pk_fichas = getWord(%statsCorretos, 11);
		
		%eval = "salaPersona" @ %i + 1 @ "_img.setVisible(true);";
		eval(%eval);
				
		%stats = %nome SPC %TAXOvitorias SPC %TAXOpontos SPC %TAXOvisionario SPC %TAXOarrebatador SPC %TAXOcomerciante SPC %TAXOdiplomata SPC %graduacaoNome SPC %pronto SPC %especie SPC %fatorImperial SPC %pk_fichas;
		clientPopularDadosNoPlayer(%i + 1, %stats); //popula os dados no player correto;
	}
		
	$PlayersNaSalaEmQueEstou = %playersOn; //soma eu ao número de players na sala;
	$salaEmQueEstouTipoDeJogo = %tipoDeJogo;
	$salaEmQueEstouPlaneta = %planeta;
	$primeiraSalaInside = false;
	initSalaChatGui();
	
	clientAskGetSalaDif(); //recalcula a dificuldade da sala;
}



//////////////////
//toggle btns da sala:
function clientToggleSalaPlanetas(){
	if($vendoSalaPlanetas){
		clientApagarPlanetas();
	} else {
		if($salaEmQueEstouTipoDeJogo !$= "poker")
			clientMostrarPlanetas();
	}
}
function clientMostrarPlanetas(){
	if(salaInsideNome1_txt.text $= $myPersona.nome){
		if($myPersona.aca_v_6 > 0 || $myPersona.especie $= "gulok"){
			//NarSul:
			if($myPersona.aca_pln_1 !$= "1"){
				planetaBtnTeluria.setActive(false);
			} else {
				planetaBtnTeluria.setActive(true);
			}
			//Telúria:
			if($myPersona.aca_pln_2 !$= "1"){
				planetaBtnNarSul.setActive(false);
			} else {
				planetaBtnNarSul.setActive(true);
			}
			//Ungart:
			if($myPersona.aca_v_6 > 0 || $myPersona.especie $= "gulok"){
				planetaBtnUngart.setActive(true);
			} else {
				planetaBtnUngart.setActive(false);
			}
			//Terra:
			planetaBtnTerra.setActive(true);
			
			escolhaDePlanetaGui.setVisible(true);
			$vendoSalaPlanetas = true;	
		} else {
			clientPushPesquisaInexistenteMsgBox();
		}
	}
}
function clientApagarPlanetas(){
	escolhaDePlanetaGui.setVisible(false);
	$vendoSalaPlanetas = false;
}
function clientPesquisaInexistenteOkBtnClick(){
	clientPopPesquisaInexistenteMsgBox();	
}
function clientPopPesquisaInexistenteMsgBox(){
	canvas.popDialog(msgBoxPesquisaInexistenteGui);
}
function clientPushPesquisaInexistenteMsgBox()
{
	clientMsgBoxOKT("PESQUISA INEXISTENTE", "PESQUISE NOVOS PLANETAS NA ACADEMIA IMPERIAL.");	
}
//
function clientToggleSalaTiposDeJogo(){
	if($vendoSalaTiposDeJogo){
		clientApagarTiposDeJogo();
	} else {
		if($salaEmQueEstouTipoDeJogo !$= "poker")
			clientMostrarTiposDeJogo();
	}
}
function clientMostrarTiposDeJogo(){
	if(salaInsideNome1_txt.text $= $myPersona.nome){
		canvas.pushDialog(TiposDeJogoGui);
		$vendoSalaTiposDeJogo = true;
	}
}
function clientApagarTiposDeJogo(){
	canvas.popDialog(TiposDeJogoGui);
	$vendoSalaTiposDeJogo = false;
}
//
function clientToggleSalaTurno(){
	if($vendoSalaTurno){
		clientApagarTurno();
	} else {
		clientMostrarTurno();
	}
}
function clientMostrarTurno(){
	if(salaInsideNome1_txt.text $= $myPersona.nome){
		escolhaDeTurnoGui.setVisible(true);
		$vendoSalaTurno = true;
	}
}
function clientApagarTurno(){
	escolhaDeTurnoGui.setVisible(false);
	$vendoSalaTurno = false;
}
//
function clientToggleSalaLotacao(){
	if($vendoSalaLotacao){
		clientApagarLotacao();
	} else {
		clientMostrarLotacao();
	}
}
function clientMostrarLotacao(){
	if($salaEmQueEstouTipoDeJogo $= "poker")
		return;
		
	if(salaInsideNome1_txt.text !$= $myPersona.nome)
		return;
	
	escolhaDeLotacaoGui.setVisible(true);
	$vendoSalaLotacao = true;
	
	lotacaoBtn3.setActive(true);
	lotacaoBtn4.setActive(true);
	lotacaoBtn5.setActive(true);	
	lotacaoBtn6.setActive(true);
	
	if($salaEmQueEstouTipoDeJogo $= "emDuplas"){
		lotacaoBtn3.setActive(false);
		lotacaoBtn4.setActive(false);
		lotacaoBtn5.setActive(false);	
	}
	
	if($salaEmQueEstouPlaneta $= "Terra" || $salaEmQueEstouPlaneta $= "Teluria"){
		if($salaEmQueEstouTipoDeJogo !$= "emDuplas"){
			lotacaoBtn5.setActive(false);
			lotacaoBtn6.setActive(false);
		}
	} else if($salaEmQueEstouPlaneta $= "Ungart"){
		if($salaEmQueEstouTipoDeJogo !$= "emDuplas"){
			lotacaoBtn6.setActive(false);
		}
	}
}
function clientApagarLotacao(){
	escolhaDeLotacaoGui.setVisible(false);
	$vendoSalaLotacao = false;
}


//////////////////
//PLANETAS
//%planetas: "Terra";
function clientAskAlterarPlaneta(%planeta){
	commandToServer('alterarPlaneta', %planeta);	
}
function clientCmdAlterarPlaneta(%planeta){
	salaInsidePlaneta_img.bitmap = "~/data/images/planeta" @ %planeta @ "_img";
	$salaEmQueEstouPlaneta = %planeta;
	clientSetDadosPlaneta(%planeta);
	clientApagarPlanetas();
	clientSetLotacaoPorPlaneta();
}

//////////////////
//TIPOS DE JOGO
//tipos: "emDuplas"; "classico"; "semPesquisas"; "handicap"
function clientAskAlterarTipoDeJogo(%tipo){
	commandToServer('alterarTipoDeJogo', %tipo);
	
	if(%tipo $= "emDuplas")
		callCriarDuplasGui();
}

function clientCmdAlterarTipoDeJogo(%tipo){
	salaInsideTipoDeJogo_img.bitmap = "~/data/images/" @ %tipo @ "_img.png";
	clientApagarTiposDeJogo();
	$salaEmQueEstouTipoDeJogo = %tipo;
	clientSetLotacaoPorTipoDeJogo();
	clientVerifyIniciarJogo($PlayersNaSalaEmQueEstou);
	clientCalcularSalaInsideDif(); //recalcula a dificuldade da sala;
	
	if($myPersona.nome $=  salaInsideNome1_txt.text)
		clientShowFator(1, $myPersona.myDif, $myPersona.pk_fichas);
}


function clientCmdForceHandicap()
{
	echo("clientCmd->FORCEHANDICAP!");
	salaInsideTipoDeJogo_img.bitmap = "~/data/images/handicap_img.png";
	$salaEmQueEstouTipoDeJogo = "handicap";	
	
	clientPushForceHandicapImg();
}

function clientPushForceHandicapImg()
{
	forceHandicap_img.setvisible(true);
	schedule(6000, 0, "clientPopForceHandicapImg");
}

function clientPopForceHandicapImg()
{
	forceHandicap_img.setvisible(false);
}
function clientSetLotacaoPorTipoDeJogo()
{
	if($salaEmQueEstouTipoDeJogo $= "emDuplas")
	{
		//clientCmdAlterarLotacao(6);
		clientSetSalaInsideLotacaoTab(6, $myLastSeenBlind);
		return;	
	}
	
	clientApagarDuplasMarks();
	clientSetLotacaoPorPlaneta();
	return;
}

function clientSetLotacaoPorPlaneta()
{
	if($salaEmQueEstouTipoDeJogo $= "emDuplas")
	{
		//clientCmdAlterarLotacao(6);
		clientSetSalaInsideLotacaoTab(6, $myLastSeenBlind);
		return;		
	}
	
	if($salaEmQueEstouPlaneta $= "Ungart")
	{
		//clientCmdAlterarLotacao(5);
		clientSetSalaInsideLotacaoTab(5, $myLastSeenBlind);
		return;
	}
	
	clientSetSalaInsideLotacaoTab(4, $myLastSeenBlind);
	//clientCmdAlterarLotacao(4);
}

////////////////
//TURNO
//%tempos: 60; 75, 100; 120;
function clientAskAlterarTurno(%tempo){
	commandToServer('alterarTurno', %tempo);	
}

function clientCmdAlterarTurno(%tempo){
	salaInsideTurno_img.bitmap = "~/data/images/" @ %tempo @ "segundos_img";
	clientApagarTurno();
}

////////////////
//LOTAÇÃO
//%lotacaos: "3"; "4"; "5"; "6";
function clientAskAlterarLotacao(%lotacao){
	commandToServer('alterarLotacao', %lotacao);	
}

function clientCmdAlterarLotacao(%lotacao){
	echo("clientCmdAlterarLotacao(" @ %lotacao @ ")");
	salaInsideLotacao_img.bitmap = "~/data/images/" @ %lotacao @ "jogadores_img";
	clientApagarLotacao();
}




/////////////////////
//Setar Duplas:
function criarDuplasAplicarBtnClick(){
	%p1 = criarDuplasP1_txt.text;
	%p2 = criarDuplasP2_txt.text;
	%p3 = criarDuplasP3_txt.text;
	%p4 = criarDuplasP4_txt.text;
	%p5 = criarDuplasP5_txt.text;
	%p6 = criarDuplasP6_txt.text;
	
	clientAskCriarDuplas(%p1, %p2, %p3, %p4, %p5, %p6);
}

function criarDuplasCancelarBtnClick(){
	apagarCriarDuplasGui();
}

function callCriarDuplasGui(){
	zerarCriarDuplas();
	canvas.pushDialog(criarDuplasGui);	
	verifyCriarDuplasAllSetas();
}

function apagarCriarDuplasGui(){
	canvas.popDialog(criarDuplasGui);	
}

function zerarCriarDuplas(){
	for(%i = 1; %i < 7; %i++){
		%eval = "%myTxt = criarDuplasP" @ %i @ "_txt;";
		eval(%eval);
		%novoValor = mFloor((%i + 1) / 2);
		%myTxt.setText(%novoValor);
		
		//Seta os nomes que já estão na sala:
		%eval = "%espaco = criarDuplasNome" @ %i @ "_txt;";
		eval(%eval);
		
		if($playersNaSalaEmQueEstou >= %i){
			%eval = "criarDuplasNome" @ %i @ "_txt.text = salaInsideNome" @ %i @ "_txt.text;";
			eval(%eval);
		} else {
			%espaco.text = "Jogador" SPC %i;
		}
	}	
}

function criarDuplasArrowRight(%num){
	%eval = "%myTxt = criarDuplasP" @ %num @ "_txt;";
	eval(%eval);
	%valorAtual = %myTxt.text;
	%novoValor = %valorAtual++;
	%myTxt.text = %novoValor;
	verifyCriarDuplasSeta(%num, %novoValor);
}

function criarDuplasArrowLeft(%num){
	%eval = "%myTxt = criarDuplasP" @ %num @ "_txt;";
	eval(%eval);
	%valorAtual = %myTxt.text;
	%novoValor = %valorAtual--;
	%myTxt.text = %novoValor;
	verifyCriarDuplasSeta(%num, %novoValor);
}

function verifyCriarDuplasSeta(%num, %novoValor){
	%eval = "%mySetaRight = criarDuplasArrowRight" @ %num @ ";";
	eval(%eval);
	%eval = "%mySetaLeft = criarDuplasArrowLeft" @ %num @ ";";
	eval(%eval);
	
	//Right:
	if(%novoValor > 2){
		%mySetaRight.setActive(false);	
	} else {
		%mySetaRight.setActive(true);	
	}
		
	//Left:
	if(%novoValor < 2){
		%mySetaLeft.setActive(false);	
	} else {
		%mySetaLeft.setActive(true);	
	}
	verifyCriarDuplasValidade();
}

function verifyCriarDuplasAllSetas(){
	for(%i = 1; %i < 7; %i++){
		%eval = "%myTxt = criarDuplasP" @ %i @ "_txt;";
		eval(%eval);
		verifyCriarDuplasSeta(%i, %myTxt.text);
		echo("myTxt: " @ %myTxt.text);
	}
}

function clientAskCriarDuplas(%p1, %p2, %p3, %p4, %p5, %p6){
	commandToServer('criarDuplas', %p1, %p2, %p3, %p4, %p5, %p6);
	apagarCriarDuplasGui();
}

function verifyCriarDuplasValidade(){
	%p[1] = criarDuplasP1_txt.text;
	%p[2] = criarDuplasP2_txt.text;
	%p[3] = criarDuplasP3_txt.text;
	%p[4] = criarDuplasP4_txt.text;
	%p[5] = criarDuplasP5_txt.text;
	%p[6] = criarDuplasP6_txt.text;
	
	%um = 0;
	%dois = 0;
	%tres = 0;
	
	for(%i = 1; %i < 7; %i++){
		if(%p[%i] == 1){
			%um++;
		} else if(%p[%i] == 2){
			%dois++;
		} else if(%p[%i] == 3){
			%tres++;
		}
	}
	
	if(%um == 2 & %dois == 2 && %tres == 2){
		criarDuplasAplicarBtn.setActive(true);	
	} else {
		criarDuplasAplicarBtn.setActive(false);	
	}
}

function clientCmdSetarDuplas(%p1, %p2, %p3, %p4, %p5, %p6){
	for(%i = 1; %i < 7; %i++){
		%eval = "%mySalaInsidePImage = salaInsideP" @ %i @ "Dupla_img;";
		eval(%eval);
		%eval = "%myP = %p" @ %i @ ";";
		eval(%eval);
		
		%mySalaInsidePImage.setVisible(true);
		%mySalaInsidePImage.bitmap = "game/data/images/duplaMark" @ %myP;
	}
	//abaixo as linhas como são cruas, foram modificadas pra entrarem aki em cima
	//salaInsideP1Dupla_img.setVisible(true);
	//salaInsideP1Dupla_img.bitmap = "game/data/images/dupla" @ %p1 @ "_img";
}

function clientApagarDuplasMarks(){
	for(%i = 1; %i < 7; %i++){
		%eval = "%mySalaInsidePImage = salaInsideP" @ %i @ "Dupla_img;";
		eval(%eval);
		%mySalaInsidePImage.setVisible(false);
	}
}


/////////////////
//Buscar Jogador:
function buscaCancelarBtnClick(){
	msgBoxBuscarPersona.setVisible(false);	
}

function buscarPersonaCallGui(){
	msgBoxBuscarPersona.setVisible(true);	
}

function clientBuscarPersona(%nomeDaPersona){
	clientPushAguardeMsgBox();
	commandToServer('buscarPersona', %nomeDaPersona);	
}

function clientCmdPersonaBuscada(%nomeDaPersona, %online, %onde){
	echo("Persona online = " @ %online @ "; Persona onde = " @ %onde @ ";");
	msgBoxBuscarPersona.setVisible(false);	//apaga a msgBox de busca
	clientPushPersonaBuscadaMsgBox(%nomeDaPersona, %online, %onde); //liga a msg box de resposta
	clientPopAguardeMsgBox();
}

function clientPushPersonaBuscadaMsgBox(%nomeDaPersona, %online, %onde){
	clientMsgBoxOk("buscaCompleta", false, false, true);
	if(%online){
		msgBoxOk_txt3.text = %nomeDaPersona @ " está ONLINE";
		if(%onde $= "Chat"){
			msgBoxOk_txt4.text = "no CHAT GLOBAL";
		} else if(%onde $= "Academia"){
			msgBoxOk_txt4.text = "na ACADEMIA IMPERIAL";
		} else {
			msgBoxOk_txt4.text = "na SALA " @ %onde;
		}
	} else {
		msgBoxOk_txt3.text = "OFFLINE";
		msgBoxOk_txt4.text = %nomeDaPersona @ " não está online.";
	}
}


///////////////////
//BanList, kick:

function clientKikarBtnClick(%num)
{
	$lastKickNum = %num;
	
	clientPushConfirmarKickGui();
}

function clientAskKick()
{
	clientPopKickBtns();
	clientPopConfirmarKickGui();
	commandToServer('kick', $lastKickNum);
}

function clientPushConfirmarKickGui()
{
	canvas.pushDialog(confirmarKickGui);	
}

function clientPopConfirmarKickGui()
{
	canvas.popDialog(confirmarKickGui);	
}

function clientCancelKick()
{
	clientPopConfirmarKickGui();
	clientPopKickBtns();
}

function clientCmdVoceFoiKikado(%salasString, %numDePersonasNoChat)
{
	clientCmdPopularAtrio(%salasString, "sala", "", %numDePersonasNoChat);	
	clientMsgBoxOKT("VOCÊ FOI EXPULSO", "E SÓ PODERÁ VOLTAR PARA ESTA SALA NO PRÓXIMO JOGO.");	
}
function clientCmdVoceFoiKikadoPorFaltaDeFichas(%salasString, %numDePersonasNoChat)
{
	clientCmdPopularAtrio(%salasString, "sala", "", %numDePersonasNoChat);	
	clientMsgBoxOKT("VOCÊ SAIU DA SALA", "POR NÃO POSSUIR FICHAS PARA CONTINUAR.");	
}

function clientCmdImpossivelEntrarNaSalaBanido()
{
	clientMsgBoxOKT("VOCÊ FOI EXPULSO", "E SÓ PODERÁ VOLTAR PARA ESTA SALA NO PRÓXIMO JOGO.");	
	clientPopAguardeMsgBox();
}

function clientPopKickBtns()
{
	for(%i = 1; %i < 6; %i++)
	{
		%eval = "%closeBtn = salaInside_personaClose_btn_" @ %i @ ";";
		eval(%eval);
		
		%closeBtn.setVisible(false);
	}
	
	$vendoKickBtns = false;
}

function clientPushKickBtns()
{
	for(%i = 1; %i < $playersNaSalaEmQueEstou; %i++)
	{
		%eval = "%closeBtn = salaInside_personaClose_btn_" @ %i @ ";";
		eval(%eval);
		
		%closeBtn.setVisible(true);
	}
	
	$vendoKickBtns = true;
}

function clientToggleKickBtns()
{
	if($vendoKickBtns || $myPersona.nome !$= salaInsideNome1_txt.text)
	{
		clientPopKickBtns();
		return;
	}
		
	clientPushKickBtns();
}

function clientCmdLotacaoEstourou()
{
	clientMsgBoxOKT("IMPOSSÍVEL INICIAR", "HÁ MAIS JOGADORES DO QUE O PLANETA ESCOLHIDO SUPORTA.");	
}

//////////////////////
//Info da situação da sala:
function clientAskSalaInfoAtual(%posDaSala)
{
	%sala = $salasNaTela.getObject(%posDaSala - 1);
	clientPushAguardeMsgBox();
	commandToServer('getSalaInfoAtual', %sala.num);
}

function clientCmdSalaInfoAtual(%infoString)
{
	echo(%infoString);
	%numDePlayers = firstWord(%infoString);
	%rodada = getWord(%infoString, 1);
	
	clientClearSalaInfoAtualGui();
	
	for(%i = 0; %i < %numDePlayers; %i++)
	{
		%playerNome[%i] = getWord(%infoString, 2 + %i);	
	}
	
	atrioSalaInfoAtualRodada_txt.text = %rodada;
	for(%i = 0; %i < %numDePlayers; %i++)
	{
		%eval = "atrioSalaInfoAtualPlayerNome" @ %i+1 @ "_txt.text = %playerNome[%i];";
		eval(%eval);
		%eval = "atrioSalaInfoAtualPlayerNome" @ %i+1 @ "_txt.setVisible(true);";
		eval(%eval);
		%eval = "salaInfoNome" @ %i+1 @ "_img.setVisible(true);";
		eval(%eval);
	}
	clientPopAguardeMsgBox();
	atrioSalaInfoAtualGui.setVisible(true);
}

function clientPopSalaInfoAtualGui()
{
	atrioSalaInfoAtualGui.setVisible(false);
}

function clientClearSalaInfoAtualGui()
{
	for(%i = 0; %i < 6; %i++)
	{
		%eval = "atrioSalaInfoAtualPlayerNome" @ %i+1 @ "_txt.setVisible(false);";
		eval(%eval);
		%eval = "salaInfoNome" @ %i+1 @ "_img.setVisible(false);";
		eval(%eval);
	}	
}