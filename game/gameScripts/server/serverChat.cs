// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverChat.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 6 de fevereiro de 2008 2:27
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdupdateChatText(%client, %text)
{
	%jogo = serverGetJogoClient(%client);
   	serverCTCupdateChatText(%jogo, %client.persona.nome, %text);
}

function serverGetJogoClient(%client)
{
	%jogo = %client.player.jogo;
	if(!isObject(%jogo))
		%jogo = %client.jogo; //se for o observador, não há um player dele, marca o jogo diretamente no client;	
		
	return %jogo;
}

function serverGetPlayerPorId(%jogo, %id)
{
	%eval = "%player = %jogo." @ %id @ ";";
	eval(%eval);
	
	return %player;
}

function serverCTCupdateChatText(%jogo, %clientName, %text){
  	for(%i=0;%i<%jogo.playersAtivos;%i++){
		%client = %jogo.simPlayers.getObject(%i).client;
		commandToClient(%client, 'updateChatText', %clientName, %text);      
   	}
	
	if(%jogo.observadorOn)
		commandToClient(%jogo.observador, 'updateChatText', %clientName, %text);      
}

function serverCmdPrivateChatText(%client, %text, %receptorId){
	%jogo = serverGetJogoClient(%client);
	
	%playerReceptor = serverGetPlayerPorId(%jogo, %receptorId);
	serverCTCPrivateChatText(%client, %client.persona.nome, %text, %playerReceptor.client);
}

function serverCTCPrivateChatText(%client, %clientName, %text, %receptorClient){
	echo(%clientName @ " diz para " @ %receptorClient.persona.nome @ " >> " @ %text);
	
	%jogo = serverGetJogoClient(%client);
		
  	commandToClient(%receptorClient, 'updatePrivateChatText', %clientName, %text, %receptorClient.persona.nome);      
    commandToClient(%client, 'updatePrivateChatText', %clientName, %text, %receptorClient.persona.nome);  
	
	if(%jogo.observadorOn && %clientName !$= %jogo.observador.persona.nome)
		commandToClient(%jogo.observador, 'updatePrivateChatText', %clientName, %text, %receptorClient.persona.nome);   
}


//chat da sala:
function serverCmdupdateSalaChatText(%client, %text){
	%sala = %client.persona.sala;
   	updateSalaChatText(%sala, %client.persona.nome, %text);
}

function updateSalaChatText(%sala, %clientName, %text){
  	for(%i=0;%i<%sala.simPersonas.getCount();%i++){
		%client = %sala.simPersonas.getObject(%i).client;
		commandToClient(%client, 'updateSalaChatText', %clientName, %text);      
   	}
	if(%sala.observadorOn){
		commandToClient(%sala.observador, 'updateSalaChatText', %clientName, %text);     
	}
}

//buzina:
function serverCmdBuzinaSala(%client){
	%sala = %client.persona.sala;
	%nome = %client.persona.nome;
	
	for(%i=0;%i<%sala.simPersonas.getCount();%i++){
		%client = %sala.simPersonas.getObject(%i).client;
		commandToClient(%client, 'buzinaSala', %nome);      
   	}
	if(%sala.observadorOn){
		commandToClient(%sala.observador, 'buzinaSala', %nome);     
	}	
}

//chat do atrio:
function serverCmdupdateAtrioChatText(%client, %text){
	updateAtrioChatText(%client.persona.nome, %text);
}

function updateAtrioChatText(%clientName, %text){
	%personasNoAtrio = $personasNoAtrio.getCount();
  	for(%i = 0; %i < %personasNoAtrio; %i++){
		%persona = $personasNoAtrio.getObject(%i);
		commandToClient(%persona.client, 'updateAtrioChatText', %clientName, %text);      
   	}
}