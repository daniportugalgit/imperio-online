// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientFilantropia.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sábado, 29 de março de 2008 22:25
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
//seta o simSet de filantropias efetuadas nesta partida:
function clientSetFilantropia(){
	if(isObject($mySelf.simFilantropiasEfetuadas)){
		$mySelf.simFilantropiasEfetuadas.clear();
	} else {
		$mySelf.simFilantropiasEfetuadas = new SimSet();
	}
}

function clientAskFilantropia(%num){
	%playerId = $mySimAdversarios.getObject(%num - 1).id;
	%adversarioQueRecebe = $mySimAdversarios.getObject(%num - 1);
	if($shiftOn){
		%anonima = true;	
	} else {
		%anonima = false;
	}
	
	if($mySelf.minerios > 0 && $mySelf.petroleos > 0 && $mySelf.uranios > 0){
		if($mySelf.filantropiasEfetuadas < $myPersona.aca_i_2){
			echo("ASK FILANTROPIA:: %num = " @ %num @ "; %playerId = " @ %playerId @ "; %anonima = " @ %anonima);
			clientPushServerComDot();
			$mySelf.simFilantropiasEfetuadas.add(%adversarioQueRecebe);
			commandToServer('filantropia', %playerId, %anonima);	
		}
	} else {
		//clientMsg "Recursos Insuficientes";	
	}
}

function clientCmdFilantropia(%doarOuReceber, %parceiroNome, %anonima){
	echo("clientCmdFilantropia(" @ %doarOuReceber @ ", " @ %parceiroNome @ ", " @ %anonima @ ")");
	if(%doarOuReceber $= "doar"){
		$mySelf.minerios--;
		$mySelf.petroleos--;
		$mySelf.uranios--;
		$mySelf.filantropiasEfetuadas++;
		if($mySelf.filantropiasEfetuadas == $myPersona.aca_i_2){
			i_filantropia_btn.setActive(false);
		}
		clientFecharIntelGui();
		clientFecharFilantropiaGui();
		if(%anonima){
			clientMsgBoxOKT3("DOAÇÃO ENVIADA", "DOAÇÃO FILANTRÓPICA ANÔNIMA ENVIADA PARA " @ strupr(%parceiroNome));	
		} else {
			clientMsgBoxOKT("DOAÇÃO ENVIADA", "DOAÇÃO FILANTRÓPICA ENVIADA PARA " @ strupr(%parceiroNome));	
		}
		msgBoxOk_txt1.setVisible(true);
		msgBoxOk_txt1.text = %parceiroNome;
		echo("Adversários que já receberam minhas doações (" @ $mySelf.simFilantropiasEfetuadas.getCount() @ ")");
		clientPopServerComDot();
	} else {
		$mySelf.minerios++;
		$mySelf.petroleos++;
		$mySelf.uranios++;
		if(%anonima){
			//clientCmdClientMsgDoacaoRecebida("[ Anônimo ]", 0, 1, 1, 1);
			clientMsgBoxOKT("DOAÇÃO RECEBIDA", "(ANÔNIMO) LHE DOOU UM CONJUNTO DE RECURSOS.");	
		} else {
			//clientCmdClientMsgDoacaoRecebida(%parceiroNome, 0, 1, 1, 1);
			clientMsgBoxOKT("DOAÇÃO RECEBIDA", strupr(%parceiroNome) @ " LHE DOOU UM CONJUNTO DE RECURSOS.");	
		}
	}	
	atualizarRecursosGui();
	clientAtualizarEstatisticas();
}

function clientPopularFilantropiaGui(%numDePlayers){
	clientApagarFilantropiaBtns(); //começa apagando todos os botões
	
	%mySimAdversarios = clientPegarMeusAdversarios(%numDePlayers);
	
	%count = %mySimAdversarios.getCount();
	for(%i = 0; %i < %count; %i++){
		%player = %mySimAdversarios.getObject(%i);
		//aki tem, no futuro, que verificar se o player não é minha dupla
		%eval = "%myFilBtn = fil_" @ %i+1 @ "Btn;";
		eval(%eval);
		%myFilBtn.setVisible(true);
		%eval = "%myFilCor = fil_" @ %i+1 @ "Cor;";
		eval(%eval);
		%myFilCor.setVisible(true);
		%myFilCor.setBitmap("game/data/images/fil_" @ %player.myColor);
		%eval = "%myFilTxt = fil_" @ %i+1 @ "Txt;";
		eval(%eval);
		%myFilTxt.setVisible(true);
		%myFilTxt.text = %player.nome;
	}
}

function clienti_filantropia_btnClick(){
	clientAbrirFilantropiaGui();
}

function clientAbrirFilantropiaGui(){
	filantropiaTab.setVisible(true); //abre o gui
	clientVerificarFilantropia();
}

function clientVerificarFilantropia(){
	if($mySelf.filantropiasEfetuadas < $myPersona.aca_i_2){
		if ($mySelf.minerios < 1 || $mySelf.petroleos < 1 || $mySelf.uranios < 1){
			fil_1Btn.setActive(false);
			fil_2Btn.setActive(false);
			fil_3Btn.setActive(false);
			fil_4Btn.setActive(false);
		} else {
			fil_1Btn.setActive(true);
			fil_2Btn.setActive(true);
			fil_3Btn.setActive(true);
			fil_4Btn.setActive(true);
			for(%i = 0; %i < $mySelf.simFilantropiasEfetuadas.getcount(); %i++){
				%player = $mySelf.simFilantropiasEfetuadas.getObject(%i);
				%eval = "fil_" @ %player.advNum @ "Btn.setActive(false);";
				eval(%eval);
			}
		}
	} else {
		fil_1Btn.setActive(false);
		fil_2Btn.setActive(false);
		fil_3Btn.setActive(false);
		fil_4Btn.setActive(false);
	}
}

function clientFecharFilantropiaGui(){
	filantropiaTab.setVisible(false); //abre o gui	
}

function clientApagarFilantropiaBtns(){
	for(%i = 1; %i < 5; %i++){
		%eval = "%myFilBtn = fil_" @ %i @ "Btn;";
		eval(%eval);
		%myFilBtn.setVisible(false);
		%eval = "%myFilCor = fil_" @ %i @ "Cor;";
		eval(%eval);
		%myFilCor.setVisible(false);
		%eval = "%myFilTxt = fil_" @ %i @ "Txt;";
		eval(%eval);
		%myFilTxt.setVisible(false);
	}		
}


function clientPegarMeusAdversarios(%numDePlayers){
	//prepara um SimSet de adversários:
	if(isObject($mySimAdversarios)){
		$mySimAdversarios.clear();	
	} else {
		$mySimAdversarios = new SimSet();
	}
	%advNum = 1;
	for(%i = 1; %i < %numDePlayers + 1; %i++){
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		if(%player.id !$= $mySelf.id){
			if(%player != $mySelf.myDupla){
				if(!%player.taMorto){
					$mySimAdversarios.add(%player);
					%player.advNum = %advNum;
					%advNum++;
				}
			}
		}
	}
		
	return $mySimAdversarios;
}
