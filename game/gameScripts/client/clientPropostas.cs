// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientPropostas.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 28 de janeiro de 2008 17:48
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
//TAB:
function clientPropTabHudBtnClick(){
	if($propTabOn){
		clientFecharPropTab();
	} else {
		clientAbrirPropTab();
	}
}

function clientFecharPropTab(){
	if(!$estouNoTutorial){
		propTab.setVisible(false);
		propHud_btnIcon.setStateOn(false);
		$propTabOn = false;
	}
}

function clientAbrirPropTab(){
	if($estouNoTutorial){
		if($tut_campanha.passo.key $= "propTabClick"){
			propTab.setVisible(true);
			clientFecharIntelGui();
			clientApagarChat();
			propHud_btnIcon.setStateOn(true);
			$propTabOn = true;
			propAlert.setVisible(false);
			tut_verificarObjetivo(false, "propTabClick");
		} else {
			propHud_btnIcon.setStateOn(false);
		}
	} else {
		propTab.setVisible(true);
		clientFecharIntelGui();
		clientApagarChat();
		clientDesligarInvestirRecursos();
		propHud_btnIcon.setStateOn(true);
		$propTabOn = true;
		propAlert.setVisible(false);
	}
}

//troca de cartas de pontos:
function clientInitPropostasPool(){
	//deletar propostasPool se ela já existia (se este não for o primeiro jogo):
	if(isObject($propostasPool)){
		while($propostasPool.getCount() > 0){
			%proposta = $propostasPool.getObject(0);
			%proposta.delete();
		}
		$propostasPool.delete();
	}
	
	//criar a propostasPool e cada uma das 12 propostas possíveis:
	$propostasPool = new SimSet();
}

function clientAskProporTroca(%infoNum, %parceiroId, %b_doar){
	echo("clientAskProporTroca ACIONADO...");
	%respondendo = false;
	if($propostasPool > 0){
		for(%i = 0; %i < $propostasPool.getCount(); %i++){
			%proposta = $propostasPool.getObject(%i);
			if(%proposta.expandida){
				%respondendo = true;
				%propostaExpandida = %proposta;
			}
		}
	}
	if(%respondendo == false){ //se o usuário NÃO estava respondendo uma proposta de propósito:
		//verificar se existe uma proposta que pode ser respondida:
		for(%i = 0; %i < $propostasPool.getCount(); %i++){
			%proposta = $propostasPool.getObject(%i);
			%info = clientFindInfo(%infoNum);
			if(%proposta.envieiOuRecebi $= "recebi"){
				if(%proposta.parceiroId $= %info.area.dono.id || %proposta.parceiroId $= %info.area.dono1.id || %proposta.parceiroId $= %info.area.dono2.id){
					if(!%proposta.haDuasCartas){
						%respondendo = true;
						%propostaExpandida = %proposta;
						%i = $propostasPool.getCount();
					}
				}
			}
		}
	}
	if(%respondendo){
		//envia a resposta pro server pra que seja enviada de volta pra cá e pro parceiro:
		%numDaInfoAntiga = %propostaExpandida.numDaInfo1;
		clientApagarPossibilidades();
		if(!$estouNoTutorial){
			commandToServer('enviarRespostaDeProposta', %numDaInfoAntiga, %infoNum, %parceiroId);
		} else {
			
		}
	} else {
		//envia a proposta pro server pra que seja enviada de volta pra cá e pro parceiro:
		clientDesligarExplMarkers();
		if(!$estouNoTutorial){
			commandToServer('enviarPropostaDeTroca', %infoNum, %parceiroId, %b_doar);	
		} else {
			clientCmdEnviarProposta(%infoNum, %parceiroId);
		}
	} 	
}

function clientCmdEnviarProposta(%infoNum, %receptorId){
	echo("clientCmdEnviarProposta ACIONADO!!!");
	//cria a proposta como um scriptObject e coloca na pool de propostas:
	%estaProposta = new ScriptObject(){};
	$propostasPool.add(%estaProposta);
	%estaProposta.on = true; // se está ligada
	%estaProposta.haDuasCartas = false; // liga quando houver duas cartas na proposta
	%estaProposta.envieiOuRecebi = "enviei"; // marca na proposta que fui eu quem enviou a primeira carta;
	%estaProposta.numDaInfo1 = %infoNum; // o número da info enviada
	%estaProposta.parceiroId = %receptorId; // guarda quem é o parceiro
	%info1 = clientFindInfo(%infoNum);
	%estaProposta.info1 = %info1;
	%info1.myMark.setBlendColor($mySelf.corR, $mySelf.corG, $mySelf.corB, 0.6);
	%info1.myMark.myOldBlendColor = %info1.myMark.getBlendColor();
	%info1.jahFoiOferecida = true;
		
	//propHud confirmando que a proposta foi enviada:
	%eval = "%receptor = $" @ %receptorId @ ";";
	eval(%eval);
	clientPropHud("propostaRecemEnviada", %receptor.nome); 
	
	//faz aparecer o quadradinho no canto esquerdo da tela:
	clientAtualizarPropostasGui();
	
	//impede que a seleção fique na base do local, se houver uma base lá:
	resetSelection();
}

function clientCmdReceberProposta(%infoNum, %doadorId){
	echo("clientCmdReceberProposta ACIONADO!!!");
	//cria a proposta como um scriptObject e coloca na pool de propostas:
	%estaProposta = new ScriptObject(){};
	$propostasPool.add(%estaProposta);
	%estaProposta.on = true; // se está ligada
	%estaProposta.haDuasCartas = false; // liga quando houver duas cartas na proposta
	%estaProposta.envieiOuRecebi = "recebi"; // marca na proposta que fui eu quem enviou a primeira carta;
	%estaProposta.numDaInfo1 = %infoNum; // o número da info enviada
	%estaProposta.parceiroId = %doadorId; // guarda quem é o parceiro
	%info1 = clientFindInfo(%infoNum);
	%estaProposta.info1 = %info1;
	
	//propHud confirmando que uma proposta foi recebida:
	%eval = "%doador = $" @ %doadorId @ ";";
	eval(%eval);
	clientPropHud("propostaRecemRecebida", %doador.nome); 
	
	alxPlay(propostaRecebida);
	
	//faz aparecer o quadradinho no canto esquerdo da tela:
	clientAtualizarPropostasGui();
}


function clientCmdRespostaRecebida(%numDaInfoAntiga, %numDaInfoNova, %receptorId){
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		if(%proposta.numDaInfo1 == %numDaInfoAntiga){
			%estaProposta = %proposta;	
		}
	}
	
	if(isObject(%estaProposta)){
		%estaProposta.haDuasCartas = true;
		%estaProposta.numDaInfo2 = %numDaInfoNova; // o número da info enviada
		%info2 = clientFindInfo(%estaProposta.numDaInfo2);
		%estaProposta.info2 = %info2;
		%eval = "%parceiro = $" @ %estaProposta.parceiroId @ ";";
		eval(%eval);
		%info2.myMark.setBlendColor(%parceiro.corR, %parceiro.corG, %parceiro.corB, 0.6);
		%info2.myMark.myOldBlendColor = %info2.myMark.getBlendColor();
	} else {
		echo("%estaProposta não é um objeto!");	
	}
		
	//faz piscar o quadradinho no canto esquerdo da tela:
	clientAtualizarPropostasGui();
}


function clientCmdRespostaEnviada(%numDaInfoAntiga, %numDaInfoNova, %doadorId){
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		if(%proposta.numDaInfo1 == %numDaInfoAntiga){
			%estaProposta = %proposta;	
		}
	}
	
	if(isObject(%estaProposta)){
		%estaProposta.haDuasCartas = true;
		%estaProposta.numDaInfo2 = %numDaInfoNova; // o número da info enviada
		%info2 = clientFindInfo(%estaProposta.numDaInfo2);
		%estaProposta.info2 = %info2;
		if(isObject(%info2.myExplMark)){
			%info2.myExplMark.safeDelete();
		}
		%info2.myMark.setBlendColor($mySelf.corR, $mySelf.corG, $mySelf.corB, 0.6);
		%info2.myMark.myOldBlendColor = %info2.myMark.getBlendColor();
		%info2.jahFoiOferecida = true;
	} else {
		echo("%estaProposta não é um objeto!");	
	}
		
	//faz ganhar um ícone de "respondido" no quadradinho da proposta(ainda não fiz):
	clientAtualizarPropostasGui();
	
	desExpandirPropostas();
	clientToggleExpandProposta(-1, false, %estaProposta); //expande a proposta
		
	//propHud confirmando que a resposta foi enviada:
	%eval = "%doador = $" @ %doadorId @ ";";
	eval(%eval);
	clientPropHud("respostaRecemEnviada", %doador.nome); 
	
	//impede que a seleção fique na base do local, se houver uma base lá:
	resetSelection();
}


function clientPropostasGuiClear(){
	//Novo:
	for(%i = 0; %i < 5; %i++){
		%eval = "%trocaBtn = propTab_enviado" @ %i + 1 @ "_btn;";	
		eval(%eval);
		%trocaBtn.setVisible(false);
		%eval = "%trocaBtn = propTab_recebido" @ %i + 1 @ "_btn;";	
		eval(%eval);
		%trocaBtn.setVisible(false);
	}
}

function clientAtualizarPropostasGui(){
	clientPropostasGuiClear(); 
	clientAtualizarEstatisticas();
	clientAtualizarPropostasERPool();
	
	%numDePropostasEnviadas = $propostaEnvieiPool.getCount();
	%numDePropostasRecebidas = $propostaRecebiPool.getCount();
	%recebidas = 0;
	%enviadas = 0;
	
	for(%i = 0; %i < %numDePropostasEnviadas; %i++){
		%estaProposta = $propostaEnvieiPool.getObject(%i);
		%info = clientFindInfo(%estaProposta.numDaInfo1);
		%eval = "%parceiro = $" @ %estaProposta.parceiroId @ ";";
		eval(%eval);
				
		//coloca a cor correta no infoMark:
		%info.myMark.setBlendColor($mySelf.corR, $mySelf.corG, $mySelf.corB, 0.6);
		
		//seta o frame correto do infoMark:
		if(isObject(%info.myMark)){
			%info.myMark.setFrame(0); //frame de negociação;
		}
		%enviadas++;
		if(%enviadas < 6 && $propTabPagAtual == 1){
			//Pega o botão:
			%eval = "%estaProposta.myBtn = propTab_enviado" @ %i + 1 @ "_btn;";
			eval(%eval);
			//coloca a cor correta no botão:
			%estaProposta.myBtn.setBitmap("game/data/images/propTab_" @ %parceiro.myColor @ "BTN");
			//liga o botão:
			%estaProposta.myBtn.setVisible(true);
					
			//faz piscar se tiver duas cartas:
			if(%estaProposta.haDuasCartas == true){
				%info = clientFindInfo(%estaProposta.numDaInfo2);
				if(isObject(%info.myMark)){
					%info.myMark.setFrame(0); //frame de negociação;
				}
				%estaProposta.myBtn.piscar = true;
				clientPiscarPropostaBtn(%estaProposta);
			} else {
				%estaProposta.myBtn.piscar = false;	
			}
		} else if(%enviadas > 5 && $propTabPagAtual == 2){
			//Pega o botão:
			%eval = "%estaProposta.myBtn = propTab_enviado" @ %i - 4 @ "_btn;";
			eval(%eval);
			//coloca a cor correta no botão:
			%estaProposta.myBtn.setBitmap("game/data/images/propTab_" @ %parceiro.myColor @ "BTN");
			//liga o botão:
			%estaProposta.myBtn.setVisible(true);
					
			//faz piscar se tiver duas cartas:
			if(%estaProposta.haDuasCartas == true){
				%info = clientFindInfo(%estaProposta.numDaInfo2);
				if(isObject(%info.myMark)){
					%info.myMark.setFrame(0); //frame de negociação;
				}
				%estaProposta.myBtn.piscar = true;
				clientPiscarPropostaBtn(%estaProposta);
			} else {
				%estaProposta.myBtn.piscar = false;	
			}
		}
		
		//se o parceiro morreu, cancela propostas com ele
		if(%parceiro.taMorto){
			clientExpandirProposta(%estaProposta);
			clientPropHudCancelar(); //cancela a proposta expandida	
		}
	}
	for(%i = 0; %i < %numDePropostasRecebidas; %i++){
		%estaProposta = $propostaRecebiPool.getObject(%i);
		%info = clientFindInfo(%estaProposta.numDaInfo1);
		%eval = "%parceiro = $" @ %estaProposta.parceiroId @ ";";
		eval(%eval);
		
		//seta o frame correto do infoMark:
		if(isObject(%info.myMark)){
			%info.myMark.setFrame(0); //frame de negociação;
		}
		
		%recebidas++;
		if(%recebidas < 6 && $propTabPagAtual == 1){
			//Pega o botão:
			%eval = "%estaProposta.myBtn = propTab_recebido" @ %i + 1 @ "_btn;";
			eval(%eval);
			//coloca a cor correta no botão:
			%estaProposta.myBtn.setBitmap("game/data/images/propTab_" @ %parceiro.myColor @ "BTN");
			//liga o botão:
			%estaProposta.myBtn.setVisible(true);
					
			//faz piscar se tiver duas cartas:
			if(%estaProposta.haDuasCartas == true){
				%info = clientFindInfo(%estaProposta.numDaInfo2);
				if(isObject(%info.myMark)){
					%info.myMark.setFrame(0); //frame de negociação;
				}
				//mostrar ícone de "respondido"	
			} else {
				%estaProposta.myBtn.piscar = false;	
			}
		} else if(%recebidas > 5 && $propTabPagAtual == 2){
			//Pega o botão:
			%eval = "%estaProposta.myBtn = propTab_recebido" @ %i - 4 @ "_btn;";
			eval(%eval);
			//coloca a cor correta no botão:
			%estaProposta.myBtn.setBitmap("game/data/images/propTab_" @ %parceiro.myColor @ "BTN");
			//liga o botão:
			%estaProposta.myBtn.setVisible(true);
					
			//faz piscar se tiver duas cartas:
			if(%estaProposta.haDuasCartas == true){
				%info = clientFindInfo(%estaProposta.numDaInfo2);
				if(isObject(%info.myMark)){
					%info.myMark.setFrame(0); //frame de negociação;
				}
				//mostrar ícone de "respondido"	
			} else {
				%estaProposta.myBtn.piscar = false;	
			}
		}
		
		//se o parceiro morreu, cancela propostas com ele
		if(%parceiro.taMorto){
			clientExpandirProposta(%estaProposta);
			clientPropHudCancelar(); //cancela a proposta expandida	
		}
	}
	if(%numDePropostasEnviadas > 5 || %numDePropostasRecebidas > 5){
		propTab_mais_btn.setActive(true);
	} else {
		$vendoPag1propTab = true;
		propTab_mais_btn.setBitmap("game/data/images/propMaisBTN");
		propTab_mais_btn.setActive(false);
	}
}

function clientPiscarPropostaBtn(%proposta){
	%btn = %proposta.myBtn;
	%eval = "%parceiro = $" @ %proposta.parceiroId @ ";";
	eval(%eval);
		
	if(%proposta.myBtn.piscar == true){
		cancel(%proposta.myPiscarSchedule);
		%proposta.myPiscarSchedule = schedule(500, 0, "clientPiscarPropostaBtn", %proposta);
					
		if(%proposta.myBtn.piscarOn){
			%btn.setBitmap("game/data/images/propTab_BrancoBTN");
			%proposta.myBtn.piscarOn = false;
			if(!$propTabOn){
				propAlert.setVisible(true);
			}
		} else {
			%btn.setBitmap("game/data/images/propTab_" @ %parceiro.myColor @ "BTN");
			%proposta.myBtn.piscarOn = true;
			if(!$propTabOn){
				propAlert.setVisible(false);
			}
		}
	}
}

function clientForcePropAlert(){
	if(!$propTabOn){
		cancel($forcePropAlertSchedule);
		if($forcePropAlertOn){
			propAlert.setVisible(false);
			$forcePropAlertOn = false;
		} else {
			propAlert.setVisible(true);
			$forcePropAlertOn = true;
		}
		$forcePropAlertSchedule = schedule(500, 0, "clientForcePropAlert");
	} else {
		propAlert.setVisible(false);
	}
}

function clientPropTab_mais_bntClick(){
	if($vendoPag1PropTab){
		clientSetPagPropTab(2);
	} else {
		clientSetPagPropTab(1);
	}
	clientAtualizarPropostasGui();
}

function clientSetPagPropTab(%num){
	desExpandirPropostas(); //desExpande qualquer outra proposta que esteja expandida
	$propTabPagAtual = %num;
	if(%num == 1){
		$vendoPag1propTab = true;
		propTab_mais_btn.setBitmap("game/data/images/propMaisBTN");
	} else if (%num == 2){
		$vendoPag1propTab = false;
		propTab_mais_btn.setBitmap("game/data/images/propMenosBTN");
	}
}

function clientPropostasHudClear(){
	//apaga os botões:
	//ATENÇÃO: ESTARIA AKI O BUG DOS BTNS SEREM APAGADOS DO NADA????? ***
	clientClearCentralButtonControl();
	
	//apaga o texto:
	propHud_txt.setVisible(false);
}

function desExpandirPropostas(){
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		if(%proposta.expandida){
			clientDesExpandirProposta(%proposta);
		}
	}
}

function clientProp_setarMarker(%proposta, %info, %primeiraOuSegunda){
	%eval = "%areaDaMissao =" SPC %info.area @ ";";
	eval(%eval);
	%eval = "%parceiro = $" @ %proposta.parceiroId @ ";";
	eval(%eval);
	if(!isObject(%info.myMark)){
		%newAlvo = missaoMarkerBase.createCopy();	
		%newAlvo.setPosition(%areaDaMissao.pos0);
		%info.myMark = %newAlvo;
		%newAlvo.myInfo = %info;
		if(!$missionMarksPool.isMember(%newAlvo)){
			$missionMarksPool.add(%newAlvo);
		}
	}
	%info.myMark.setFrame(0);  //a imagem de "Em Negociação"
	
	
	if(%proposta.envieiOuRecebi $= "enviei"){
		if(%primeiraOuSegunda == 1){
			%info.myMark.setBlendColor($mySelf.corR, $mySelf.corG, $mySelf.corB, 1);
		} else {
			%info.myMark.setBlendColor(%parceiro.corR, %parceiro.corG, %parceiro.corB, 1);
			clientPropHud("respostaRecebida"); //mostrar os botões de aceitar ou negar
		}
	} else {
		if(%primeiraOuSegunda == 1){
			%info.myMark.setBlendColor(%parceiro.corR, %parceiro.corG, %parceiro.corB, 1);
		} else {
			%info.myMark.setBlendColor($mySelf.corR, $mySelf.corG, $mySelf.corB, 1);
			clientPropHud("respostaEnviada"); //mostrar botão de cancelar
		}
	}
}

function clientAtualizarPropostasERPool(){
	if(isObject($propostaEnvieiPool)){
		$propostaEnvieiPool.clear();
	} else {
		$propostaEnvieiPool = new SimSet();
	}
	if(isObject($propostaRecebiPool)){
		$propostaRecebiPool.clear();
	} else {
		$propostaRecebiPool = new SimSet();
	}
	
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		if(%proposta.envieiOuRecebi $= "enviei"){
			$propostaEnvieiPool.add(%proposta);
		} else {
			$propostaRecebiPool.add(%proposta);
		}
	}
}

function clientToggleExpandProposta(%propostaNum, %enviadaOuRecebida, %propriaProposta){
	clientAtualizarPropostasERPool();
	if(%propostaNum !$= "-1"){
		if(%enviadaOuRecebida $= "env"){
			%myPool = $propostaEnvieiPool;
		} else {
			%myPool = $propostaRecebiPool;
		}
		if($propTabPagAtual == 1){
			%proposta = %myPool.getObject(%propostaNum - 1); //pega a proposta
		} else {
			%proposta = %myPool.getObject(%propostaNum + 4); //pega a proposta
		}
	} else {
		%proposta = %propriaProposta;	
	}
	
	if(%proposta.expandida){
		clientDesExpandirProposta(%proposta);
	} else {
		clientExpandirProposta(%proposta);
	}
}

function clientExpandirProposta(%proposta){
	desExpandirPropostas(); //desExpande qualquer outra proposta que esteja expandida
	clientDesligarExplMarkers();
	%proposta.myBtn.setStateOn(true);
	%info1 = clientFindInfo(%proposta.numDaInfo1);
	%eval = "%parceiro = $" @ %proposta.parceiroId @ ";";
	eval(%eval);
	%eval = "%areaDaMissao =" SPC %info1.area @ ";";
	eval(%eval);
		
	//se o marker ainda não existe, cria:
	clientProp_setarMarker(%proposta, %info1, 1);
	//manda o missionMarker1Piscar:
	$piscarBase1Mark = true;
	clientPiscarBase1Mark(%info1.myMark);
	
	//se houver uma segunda info, faz ela piscar tb e mostra o botão de aceitar ou negar caso vc tenha enviado a proposta em primeiro lugar:
	if(%proposta.haDuasCartas == true){
		%info2 = clientFindInfo(%proposta.numDaInfo2);
		%eval = "%areaDaMissao =" SPC %info2.area @ ";";
		eval(%eval);
		
		//se o marker ainda não existe, cria:
		clientProp_setarMarker(%proposta, %info2, 2);
		//manda o missionMarker2Piscar:
		$piscarBase2Mark = true;
		clientPiscarBase2Mark(%info2.myMark);
	} else { //se só tem uma carta:
		if(%proposta.envieiOuRecebi $= "recebi"){
			%possibilidades = clientMostrarPossibilidades(%parceiro);
			if(%possibilidades > 0){
				clientPropHud("haPossibilidades", %possibilidades);
			} else {
				clientPropHud("naoHaPossibilidades");
			}
		} else {
			clientPropHud("propostaEnviada");
		}
	}
	
	%proposta.expandida = true;
	
	clientSetDesExpandirProposta(%proposta, 10000);
}

function clientSetDesExpandirProposta(%proposta, %time){
	$desExpandirPropostaTimer = schedule(%time, 0, "clientDesExpandirProposta", %proposta);
}
	
	
function clientDesExpandirProposta(%proposta){
	cancel($desExpandirPropostaTimer);
	clientClearCentralButtonControl();
	%proposta.myBtn.setStateOn(false);
	%eval = "%parceiro = $" @ %proposta.parceiroId @ ";";
	eval(%eval);
	
	propHud.setVisible(false);
	$piscarBase1Mark = false;
	cancel($piscarBase1MarkSchedule);
	$piscarBase2Mark = false;
	cancel($piscarBase2MarkSchedule);
	clientApagarPossibilidades(%parceiro);
	
	%info1 = clientFindInfo(%proposta.numDaInfo1);
	%info1.myMark.setVisible(false);
	%info1.myMark.setBlendAlpha(0.6);
	if(%proposta.haDuasCartas){
		%info2 = clientFindInfo(%proposta.numDaInfo2);
		%info2.myMark.setVisible(false);
		%info2.myMark.setBlendAlpha(0.6);
	}
	
	%proposta.expandida = false;
}
	
function clientMostrarPossibilidades(%parceiro){
	for(%i = 0; %i < $mySelf.mySimInfo.getCount(); %i++){
		%info = $mySelf.mySimInfo.getObject(%i);
		if(%info.bonusPt > 0){
			if(%info.area.dono == %parceiro){
				if(!%info.jahFoiOferecida){
					if(%info.area.dono !$= $mySelf.id && %info.area.dono !$= "0" && %info.area.dono !$= "MISTA"){
						%possibilidades += 1;
						%explMark = explMarkerPontos.createCopy();	
						%explMark.setPosition(%info.area.pos0);
						if(isObject(%info.myExplMark)){
							%info.myExplMark.safeDelete();	
						}
						%info.myExplMark = %explMark;
						%explMark.setUseMouseEvents(true);
						%explMark.infoNum = %info.num;
						%explMark.area = %info.area;
						clientExplFx(%explMark); //dá um acordoPing pro client
					}
				}
			}
		}
	}
	if(%possibilidades > 0){
		$explMarkersOn = true;	
	}
	
	return %possibilidades;
}

function clientApagarPossibilidades(%parceiro){
	for(%i = 0; %i < $mySelf.mySimInfo.getCount(); %i++){
		%info = $mySelf.mySimInfo.getObject(%i);
		if(%info.bonusPt > 0){
			if(%info.area.dono == %parceiro){
				if(isObject(%info.myExplMark)){
					%info.myExplMark.safeDelete();
				}
			}
		}
	}
	$explMarkersOn = false;	
}


function clientExplFx(%mark){
	%markFX = new t2dParticleEffect() { scenegraph = %mark.scenegraph; };
	%markFX.loadEffect("~/data/particles/acordosPossiveisPing.eff");
	%markFX.setLayer( %mark.getLayer() );
	%markFX.mount( %mark );
	%markFX.playEffect(); //o efeito se mata sozinho depois;
}
////////////
//////
//clientProp:
//clientPropHud("respostaRecebida"); //mostrar os botões de aceitar ou negar
//*clientPropHud("respostaEnviada"); //mostrar botão de cancelar
//*clientPropHud("haPossibilidades", %possibilidades); //txt de possibilidades
//*clientPropHud("naoHaPossibilidades");
//*clientPropHud("propostaEnviada"); //btn de cancelar
//*clientPropHud("propostaRecebida", %doadorId); //apenas hud e txt
function clientPropHud(%tipo, %param){
	clientPropostasHudClear();
	propHud.setVisible(true);
	switch$(%tipo){
		case "naoHaPossibilidades":
		propHud.bitmap = "~/data/images/propHud_naoHaAcordos";
		
		case "haPossibilidades":
		propHud.bitmap = "~/data/images/propHud_acordosPossiveis";
		propHud_txt.setVisible(true);
		propHud_txt.text = %param;
		
		case "propostaEnviada":
		propHud.bitmap = "~/data/images/propHud_propEnviada";
		propHud_cancelarBtn.setVisible(true);
		
		case "propostaRecemEnviada":
		propHud.bitmap = "~/data/images/propHud_propRecemEnviada";
		propHud_txt.setVisible(true);
		propHud_txt.text = %param;
		setApagarPropHud(3000);
		
		case "propostaRecemRecebida":
		propRecebidaMsg.setVisible(true);
		propRecebidaMsg_txt.text = %param;
		propHud.bitmap = "~/data/images/propHud_propRecebida";
		propHud_txt.setVisible(true);
		propHud_txt.text = %param;
		setApagarPropHud(6000);
		
		case "respostaEnviada":
		propHud.bitmap = "~/data/images/propHud_respostaEnviada";
		propHud_cancelarBtn.setVisible(true);
		
		case "respostaRecemEnviada":
		propHud.bitmap = "~/data/images/propHud_respostaRecemEnviada";
		propHud_txt.setVisible(true);
		propHud_txt.text = %param;
		setApagarPropHud(3000);
		
		case "respostaRecebida":
		propHud.bitmap = "~/data/images/propHud_respostaRecebida";
		propHud_aceitarBtn.setVisible(true);
		propHud_negarBtn.setVisible(true);
		
		case "acordoEfetivado":
		propHud.bitmap = "~/data/images/propHud_acordoEfetivado";
		setApagarPropHud(3000);
		
		case "acordoNegado":
		propHud.bitmap = "~/data/images/propHud_acordoNegado";
		setApagarPropHud(3000);
		
		case "propostaCancelada":
		propHud.bitmap = "~/data/images/propHud_propCancelada";
		setApagarPropHud(3000);
	}
}


function setApagarPropHud(%tempo){
	cancel($apagarPropHudSchedule);
	$apagarPropHudSchedule = schedule(%tempo, 0, "clientApagarPropHud");
}

function clientApagarPropHud(){
	propHud.setVisible(false);	
	propRecebidaMsg.setVisible(false);
}











///////////
//pisca a info1
function clientPiscarBase1Mark(%infoMark){
	if($piscarBase1Mark == true){
		$piscarBase1MarkSchedule = schedule(500, 0, "clientPiscarBase1Mark", %infoMark);
		
		if($piscarBMark1On){
			%infoMark.setVisible(false);			
			$piscarBMark1On = false;
		} else {
			%infoMark.setVisible(true);	
			$piscarBMark1On = true;
		}
	}
}

//pisca info2
function clientPiscarBase2Mark(%infoMark){
	if($piscarBase2Mark == true){
		$piscarBase2MarkSchedule = schedule(500, 0, "clientPiscarBase2Mark", %infoMark);
		
		if($piscarBMark2On){
			%infoMark.setVisible(false);			
			$piscarBMark2On = false;
		} else {
			%infoMark.setVisible(true);	
			$piscarBMark2On = true;
		}
	}
}



//////////////
//btns:

function clientPropHudAceitar(){
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		if(%proposta.expandida){
			%propostaExpandida = %proposta;
			%i = $propostasPool.getCount();
		}
	}
	
	if(!$estouNoTutorial){
		commandToServer('efetuarTroca', %propostaExpandida.numDaInfo1, %propostaExpandida.numDaInfo2, %propostaExpandida.parceiroId);
		clientPushServerComDot(); //previne que a negociação seja feita duas vezes (previne qq atitude até uma resposta do server);
	} else {
		clientCmdEfetuarTroca(%propostaExpandida.numDaInfo1, %propostaExpandida.numDaInfo2, "player2"); //efetua a troca do tutorial com o player2
	}
}

function clientCmdEfetuarTroca(%numDaInfo1, %numDaInfo2, %parceiroId){
	//encontra a proposta em questão:
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		if(%proposta.numDaInfo1 == %numDaInfo1){
			%estaProposta = %proposta;
			%i = $propostasPool.getCount();
		}
	}
	
	//troca as cartas e atualiza estatísticas automaticamente:
	if(%estaProposta.envieiOuRecebi $= "enviei"){
		clientCmdPerderInfo(%numDaInfo1);
		clientCmdReceberInfo(%numDaInfo2);
	} else {
		clientCmdPerderInfo(%numDaInfo2);
		clientCmdReceberInfo(%numDaInfo1);
	}
	
	//mostra o hud de acordo efetivado:
	clientPropHud("acordoEfetivado");
	
	//deixa o botão desclicado:
	%estaProposta.myBtn.setStateOn(false);
	%estaProposta.info1.jahFoiOferecida = false;
	%estaProposta.info2.jahFoiOferecida = false;
	
	//deleta a proposta:
	$propostasPool.remove(%estaProposta);
	if(isObject(%estaProposta)){
		%estaProposta.delete();
	}
	
	//atualiza o propostasGui:
	clientAtualizarPropostasGui();
	
	//faz o propBtn parar de piscar:
	$piscarBase1Mark = false;
	$piscarBase2Mark = false;
	
	//marca no mue player que fiz uma troca (pra verificar comerciante):
	$mySelf.trocas++;
		
	//devolve o controle ao usuário:
	clientPopServerComDot(); 
}

function clientCmdPerderInfo(%infoNum){
	%info = clientFindInfo(%infoNum);
	$mySelf.mySimInfo.remove(%info);
	$missionMarksPool.remove(%info.myMark);
	%info.myMark.safeDelete();
}
	
function clientCmdReceberInfo(%infoNum){
	%info = clientFindInfo(%infoNum);
	$mySelf.mySimInfo.add(%info);
	if(isObject(%info.myMark)){
		%info.myMark.safeDelete();
	}
	clientMarkNewMission(%infoNum); //marca a missão corretamente
	clientAtualizarEstatisticas();	
}


//////
//Negar:
function clientPropHudNegar(){
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		if(%proposta.expandida){
			%propostaExpandida = %proposta;
			%i = $propostasPool.getCount();
		}
	}
	
	if(!$estouNoTutorial){
		commandToServer('negarTroca', %propostaExpandida.numDaInfo1, %propostaExpandida.numDaInfo2, %propostaExpandida.parceiroId);
		clientPushServerComDot(); //previne que a negociação seja feita duas vezes (previne qq atitude até uma resposta do server);
	}
}

function clientCmdNegarTroca(%numDaInfo1, %numDaInfo2, %parceiroId){
	//encontra a proposta em questão:
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		if(%proposta.numDaInfo1 == %numDaInfo1){
			%estaProposta = %proposta;
			%i = $propostasPool.getCount();
		}
	}
	
	//deleta o marker da info 2:
	%info2 = clientFindInfo(%numDaInfo2);
	if(isObject(%info2.myMark)){
		if(%estaProposta.envieiOuRecebi $= "enviei"){
			if($missionMarksPool.isMember(%info2.myMark)){
				$missionMarksPool.remove(%info2.myMark);
			}
			%info2.myMark.delete();	
		} else {
			%info2.myMark.setFrame(1); //carta não-negociada;
			%info2.myMark.setBlendColor(1, 1, 1, 1); //volta o blend normal
			%info2.myMark.myOldBlendColor = %info2.myMark.getBlendColor();
			%info2.myMark.setVisible(true);
		}
	}
	
	//joga fora a segunda carta:
	%estaProposta.numDainfo2 = "";
	%estaProposta.info2.jahFoiOferecida = false;
	%estaProposta.info2 = "";
	%estaProposta.haDuasCartas = false;
			
	//atualiza o propostasGui:
	clientAtualizarPropostasGui();
	
	//faz o propBtn parar de piscar:
	$piscarBase1Mark = false;
	$piscarBase2Mark = false;
	%estaProposta.myBtn.piscar = false;
	
	//desclica o botão da proposta:
	if(%estaProposta.envieiOuRecebi $= "enviei"){
		clientExpandProposta(-1, %estaProposta);
		%estaProposta.myBtn.setStateOn(false);
	}
	
	//mostra o hud de acordo negado:
	clientPropHud("acordoNegado");
		
	//devolve o controle ao usuário:
	clientPopServerComDot(); 
	toggleAcordos_btn.setStateOn(false);
}


//////
//Cancelar:
function clientPropHudCancelar(){
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		if(%proposta.expandida){
			%propostaExpandida = %proposta;
			%i = $propostasPool.getCount();
		}
	}
	
	if(!$estouNoTutorial){
		commandToServer('cancelarTroca', %propostaExpandida.numDaInfo1, %propostaExpandida.numDaInfo2, %propostaExpandida.parceiroId);
		clientPushServerComDot(); //previne que a negociação seja feita duas vezes (previne qq atitude até uma resposta do server);
	}
}

function clientCmdCancelarTroca(%numDaInfo1, %numDaInfo2){
	//encontra a proposta em questão:
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i);
		if(%proposta.numDaInfo1 == %numDaInfo1){
			%estaProposta = %proposta;
			%i = $propostasPool.getCount();
		}
	}
		
	//pega as infos:
	%info1 = clientFindInfo(%numDaInfo1);
	%info2 = clientFindInfo(%numDaInfo2);
				
	//desclica o botão da proposta:
	if(%estaProposta.expandida){
		clientExpandProposta(-1, %estaProposta);
		%estaProposta.myBtn.setStateOn(false);
	}
	
	//marca que as missões não foram oferecidas:
	%estaProposta.info1.jahFoiOferecida = false;
	if(isObject(%estaProposta.info2)){
		%estaProposta.info2.jahFoiOferecida = false;
	}
	
	if($mySelf.mySimInfo.isMember(%info1)){ //se a minha carta é a info1:
		if(isObject(%info1.myMark)){
			%info1.myMark.setFrame(1); //carta não-negociada;
			%info1.myMark.setBlendColor(1, 1, 1, 1); //volta o blend normal
			%info1.myMark.myOldBlendColor = %info1.myMark.getBlendColor();
			%info1.myMark.setVisible(true);
		}
		if(isObject(%info2.myMark)){
			if($missionMarksPool.isMember(%info2.myMark)){
				$missionMarksPool.remove(%info2.myMark);
			}
			%info2.myMark.safeDelete();	
		}
		//faz os markers e o btn pararem de piscar:
		$piscarBase1Mark = false;
		$piscarBase2Mark = false;
		%estaProposta.myBtn.piscar = false;
	} else {  //se a minha carta é a info2 ou se eu ainda não respondi:
		if(isObject(%info2.myMark)){
			%info2.myMark.setFrame(1); //carta não-negociada;
			%info2.myMark.setBlendColor(1, 1, 1, 1); //volta o blend normal
			%info2.myMark.myOldBlendColor = %info2.myMark.getBlendColor();
			%info2.myMark.setVisible(true);
		}
		if(isObject(%info1.myMark)){
			if($missionMarksPool.isMember(%info1.myMark)){
				$missionMarksPool.remove(%info1.myMark);
			}
			%info1.myMark.safeDelete();	
		}
		//faz os markers e o btn pararem de piscar:
		$piscarBase1Mark = false;
		$piscarBase2Mark = false;
		%estaProposta.myBtn.piscar = false;
	}
		
	//joga fora a proposta:
	$propostasPool.remove(%estaProposta);
	%estaProposta.delete();
				
	//atualiza o propostasGui:
	clientAtualizarPropostasGui();
		
	//mostra o hud de proposta cancelada:
	clientPropHud("propostaCancelada");
		
	//devolve o controle ao usuário:
	clientPopServerComDot(); 
	toggleAcordos_btn.setStateOn(false);
}




function clientVerifyProps(){
	//echo("Verificando Propostas...");
	for(%i = 0; %i < $propostasPool.getCount(); %i++){
		%proposta = $propostasPool.getObject(%i); //pega cada proposta da propostasPool
		if(%proposta.envieiOuRecebi $= "recebi"){ //se foi um proposta recebida:
			if(%proposta.haDuasCartas){
				%info2 = clientFindInfo(%proposta.numDaInfo2); //pega a info2
				if(%proposta.parceiroId !$= %info2.area.dono.id && %proposta.parceiroId !$= %info2.area.dono1.id && %proposta.parceiroId !$= %info2.area.dono2.id){ //se quem me ofereceu ainda for o dono da área onde tenho missão
					//Se o parceiro perdeu a área que propus, cancela a proposta:
					clientExpandirProposta(%proposta);
					clientPropHudCancelar(); //cancela a proposta expandida
				}
			}
		} else {
			//echo("Proposta Enviada Encontrada!");
			//enviei uma proposta
			%info1 = clientFindInfo(%proposta.numDaInfo1); //pega a info1
			if(%info1.area.dono.id !$= %proposta.parceiroId && %info1.area.dono1.id !$= %proposta.parceiroId && %info1.area.dono2.id !$= %proposta.parceiroId){
				//Se o parceiro perdeu a área que propus, cancela a proposta:
				clientExpandirProposta(%proposta);
				clientPropHudCancelar(); //cancela a proposta expandida
			}
		}
	}
}

/*
function clientVerifyAllProps(){
	for(%it = 0; %it < $mySelf.mySimInfo.getCount(); %it++){
		%info = $mySelf.mySimInfo.getObject(%i);
		%donoDaArea = %info.area.dono;
		if(%info.bonusPt > 0){
			if(%donoDaArea != $mySelf && %donoDaArea != 0 && %donoDaArea !$= "MISTA"){
				for(%i = 0; %i < $propostasPool.getCount(); %i++){
					%proposta = $propostasPool.getObject(%i);
					if(%proposta.envieiOuRecebi $= "recebi"){
						if(%proposta.parceiroId $= %donoDaArea.id){
							if(!%proposta.haDuasCartas){
								//mostrarFlecha:
								%flechaNum = %i + 1;
								%eval = "%flechaCorreta = flechaProp" @ %flechaNum @ ";";
								eval(%eval);
								%flechaCorreta.setVisible(true);
								%proposta.myFlecha = %flechaCorreta;
							}
						}
					}
				}
			}	
		}
	}
}
*/

