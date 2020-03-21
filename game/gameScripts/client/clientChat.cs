// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientChat.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 6 de fevereiro de 2008 3:11
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function clientCmdUpdateChatText(%clientName, %text){
    %string = %clientName @ ": " @ %text;
    chatVectorText.pushBackLine(%string, 0);
	if(!$vendoChat){
		chatMsgPic.setVisible(true); //liga um mostrador discreto
		alxPlay ( chatPing );
	}
}

function clientCmdUpdatePrivateChatText(%clientName, %text, %receptorNome){
	%string = %clientName @ ">>" @ %receptorNome @ ": " @ %text;
	chatVectorText.pushBackLine(%string, 0);
	if(!$vendoChat){
		piscarChatMark(); //pisca o mostrador
		alxPlay ( chatPingPrive );
	}
}

function piscarChatMark(){
	cancel($piscarChatMarkSchedule); //cancela um pedido anterior de piscar
	$chatMarkPiscando = 0; //zera o número de piscadas
	clientPiscarChatMark(); //chama a função de piscar
}

function clientPiscarChatMark(){ 
	if($chatMarkPiscando < 8){
		$piscarChatMarkSchedule = schedule(500, 0, "clientPiscarChatMark");
		
		if($chatMarkPiscarOn){
			chatMsgPic.setVisible(false);
			$chatMarkPiscarOn = false;
		} else {
			chatMsgPic.setVisible(true);
			$chatMarkPiscarOn = true;
		}
		$chatMarkPiscando++;
	} else {
		$chatMarkPiscando = 0;
		if(!$vendoChat){
			chatMsgPic.setVisible(true);
		}
	}
}




//////////////////////////////////////
//ChatGui:
function initJogoChatGui(){
	if(isObject(chatVectorText)){
		chatMessageText.detach();
		chatVectorText.delete();
	}
	
	new MessageVector(chatVectorText){};
	chatMessageText.attach(chatVectorText);
	
	chatVectorText.clear();
	echo("Chat initialized!");
}

function jogoChatGuiSend(){
	%text = chatEditText.getValue();
	if(%text !$= ""){
		if($chatTodosOn){
			commandToServer('updateChatText', %text);
			chatEditText.setValue("");
		} else {
			for(%i = 1; %i < 6; %i++){
				%eval = "%myBtnOn = $chat" @ %i @ "On;";
				eval(%eval);
				%eval = "%myBtnPlayerId = $chat" @ %i @ "PlayerId;";
				eval(%eval);
								
				if(%myBtnOn){
					echo("sending private text to " @ %myBtnPlayerId);
					commandToServer('privateChatText', %text, %myBtnPlayerId);
					chatEditText.setValue("");
					%i = 6;
				}
			}
		}
	}
}

function apagarChatBtns(){
	clientClearChatBtns(); //des-seleciona qq botão
	for(%i = 1; %i < 6; %i++){
		%eval = "%myChatBtn = chat" @ %i @ "Btn;";
		eval(%eval);
		%myChatBtn.setVisible(false);
	}	
}

function ligarChatTodosBtn(){
	clientClearChatBtns();
	$chatTodosOn = true;
	chatTodos_Btn.setStateOn(true);
}

function ligarChatPlayerBtn(%num){
	clientClearChatBtns();
	%eval = "%myChatBtn = chat" @ %num @ "Btn;";
	eval(%eval);
	
	switch$ (%num){
		case "1": $chat1On = true;
		case "2": $chat2On = true;
		case "3": $chat3On = true;
		case "4": $chat4On = true;
		case "5": $chat5On = true;
	}
	
	%myChatBtn.setStateOn(true);
}

function clientClearChatBtns(){
	$chatTodosOn = false;
	$chat1On = false;
	$chat2On = false;
	$chat3On = false;
	$chat4On = false;
	$chat5On = false;
	chatTodos_Btn.setStateOn(false);
	for(%i = 1; %i < 6; %i++){
		%eval = "%myChatBtn = chat" @ %i @ "Btn;";
		eval(%eval);
			
		%myChatBtn.setStateOn(false);
	}
}


//chatBnt:
function clientJogoChatToggle(){
	if($vendoChat){
		if($estouNoTutorial){
			if($tut_campanha.passo.key $= "chatBtnClick"){
				clientApagarChat();
				tut_verificarObjetivo(false, "chatBtnClick");	
			}
		} else {
			clientApagarChat();
		}
	} else {
		clientChamarChat();
	}
}


function clientChamarChat(){
	clientDesligarInvestirRecursos(); //apaga o hud de investir recursos
	clientFecharIntelGui(); //apaga o hud de intel
	clientFecharPropTab(); //fecha a tab de propostas
	chatGuifundo.setVisible(true);
	mainGuiChatGlobal_btn.setStateOn(false);
	$vendoChat = true;
	cancel($piscarChatMarkSchedule); //cancela o piscar atual, caso estivesse ligado
	chatMsgPic.setVisible(false); //apaga o mostrador (chatMark)	
}

function clientApagarChat(){
	chatGuifundo.setVisible(false);
	mainGuiChatGlobal_btn.setStateOn(true);
	$vendoChat = false;
	if($estouNoTutorial){
		tut_verificarObjetivo(false, "chatBtnClick");	
	}
}



///////////////////////////////////
//Chat da sala:
//ChatGui:
function initSalaChatGui(){
	if(isObject(salaChatVectorText)){
		salaChatMessageText.detach();
		salaChatVectorText.delete();
	}
	
	new MessageVector(salaChatVectorText){};
	salaChatMessageText.attach(salaChatVectorText);
	
	salaChatVectorText.clear();
	echo("SalaChat initialized!");
}

function salaChatGuiSend(){
	%text = salaChatEditText.getValue();
	if(%text !$= ""){
		commandToServer('updateSalaChatText', %text);
		salaChatEditText.setValue("");
	}
}

function clientCmdUpdateSalaChatText(%clientName, %text){
    %string = %clientName @ ": " @ %text;
    salaChatVectorText.pushBackLine(%string, 0);
}

//Buzina:
function clientAskBuzina(){
	if(!$buzinaBlocked){
		$buzinaBlocked = true;
		buzinaSala_btn.setActive(false);
		schedule(30000, 0, "unblockBuzina");
		commandToServer('buzinaSala');
	} else {
		echo("A Buzina só pode ser tocada de 30 em 30 segundos.");	
	}
}

function clientCmdBuzinaSala(%nome){
	%string = ">>> " @ %nome @ " grita: VAMOS JOGAAAR!!! <<<";
	salaChatVectorText.pushBackLine(%string, 0);
	if(!$noSound)
		alxPlay( buzinaSala );
	
	clientPiscarSalaInsideChat();
	
	echo("$pref::Input::mouseEnabled = " @ $pref::Input::mouseEnabled);
	if(!$pref::Input::mouseEnabled){
		echo("Buzina Acionada quando o Torque estava em segundo planooooo!!!!!");	
		//aki deveria entrar a função que faz a janelinha minimizada chamar atenção.
	}
}

function unblockBuzina(){
	$buzinaBlocked = false;
	buzinaSala_btn.setActive(true);
}

function clientResetSalaInsideChatPiscando()
{
	cancel($salainsideChatShedule);
	clientUnlightSalaInsideChat();	
	$piscarSalaInsideChatCount = 0;
}

function clientPiscarSalaInsideChat()
{
	cancel($salainsideChatShedule);
	
	if($piscarSalaInsideChatCount == 3)
	{
		clientResetSalaInsideChatPiscando();
		return;	
	}
	
	if($salaInsideChatPiscando){
		clientUnlightSalaInsideChat();
	} else {
		clientHiLightSalaInsideChat();	
	}
	
	$piscarSalaInsideChatCount++;
	$salainsideChatShedule = schedule(500, 0, "clientPiscarSalaInsideChat");
}

function clientHiLightSalaInsideChat()
{
	salaInside_chatPiscando_img.setvisible(true);
	$salaInsideChatPiscando = true;
}

function clientUnlightSalaInsideChat()
{
	salaInside_chatPiscando_img.setvisible(false);
	$salaInsideChatPiscando = false;
}

////////////////
///////////////////////////////////
//Chat do atrio:
function initAtrioChatGui(){
	if(isObject(atrioChatVectorText)){
		atrioChatMessageText.detach();
		atrioChatVectorText.delete();
	}
	
	new MessageVector(atrioChatVectorText){};
	atrioChatMessageText.attach(atrioChatVectorText);
	
	atrioChatVectorText.clear();
	echo("AtrioChat initialized!");
}

function atrioChatGuiSend(){
	%text = atrioChatEditText.getValue();
	if(%text !$= ""){
		commandToServer('updateAtrioChatText', %text);
		atrioChatEditText.setValue("");
	}
}

function clientCmdUpdateAtrioChatText(%clientName, %text){
    %string = %clientName @ ": " @ %text;
    atrioChatVectorText.pushBackLine(%string, 0);
}
