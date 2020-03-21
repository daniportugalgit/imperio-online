// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverGulokAppear.cs
// Copyright          :  
// Author             :  admin
// Created on         :  sábado, 20 de dezembro de 2008 9:40
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

/////////////////
//Surgimento dos guloks:
function jogo::addAIPlayer(%this, %nome, %quemDespertou){
	createPlayer(%this);
	%playerCount = %this.simPlayers.getCount();
	%newPlayer = %this.simPlayers.getObject(%playercount - 1);
	%eval = "%newPlayer.id = player" @ %playerCount @ ";";
	eval(%eval);
	
	if(%playerCount == 3){
		%newPlayer.id = "player3";	
	} else if(%playerCount == 4){
		%newPlayer.id = "player4";	
	} else  if(%playerCount == 5){
		%newPlayer.id = "player5";	
	} else if(%playerCount == 6){
		%newPlayer.id = "player6";	
	}
	%newPlayer.nome = %nome;
	%newPlayer.aiPlayer = true;
	%this.playersAtivos++;
	%this.aiPlayer = %newPlayer;
	%newPlayer.imperiais = 5999;
	%newPlayer.jogo = %this;
	
	%this.guloksDespertaram = true;
	%this.playerQueDespertouGuloks = %quemDespertou;
	
	%this.killAll(%this.UNG_PaGu03);
	%this.killAll(%this.UNG_PaGu01);
	%this.killAll(%this.UNG_PaGu02);
	%this.killAll(%this.UNG_VaGu02);
	%this.killAll(%this.UNG_VaGu01);
	%this.killAll(%this.UNG_b19);
	%this.killAll(%this.UNG_b18);
	%this.killAll(%this.UNG_b22);
	%this.killAll(%this.UNG_b23);
	%this.killAll(%this.UNG_b25);
	%this.killAll(%this.UNG_b38);
	%this.killAll(%this.UNG_b39);
	%this.killAll(%this.UNG_ChOc02);
	%this.killAll(%this.UNG_ChOc01);
	
	for(%i = 0; %i < %this.playersAtivos-1; %i++){
		%player = %this.simPlayers.getObject(%i);
		commandToClient(%player.client, 'gulokArmaggedon');	
		%player.mySimObj.clear(); //limpa os objetivos de cada jogador;
	}
	if(%jogo.observadorOn){
		commandToClient(%this.observador, 'gulokArmaggedon');	
	}
		
	if(%this.player1.myColor !$= "vermelho" && %this.player2.myColor !$= "vermelho" && %this.player3.myColor !$= "vermelho" && %this.player4.myColor !$= "vermelho" && %this.player5.myColor !$= "vermelho"){
		%this.setColor($vermelho, %newPlayer);
	} else if(%this.player1.myColor !$= "laranja" && %this.player2.myColor !$= "laranja" && %this.player3.myColor !$= "laranja" && %this.player4.myColor !$= "laranja" && %this.player5.myColor !$= "laranja"){
		%this.setColor($laranja, %newPlayer);
	} else if(%this.player1.myColor !$= "roxo" && %this.player2.myColor !$= "roxo" && %this.player3.myColor !$= "roxo" && %this.player4.myColor !$= "roxo" && %this.player5.myColor !$= "roxo"){
		%this.setColor($roxo, %newPlayer);
	} else if(%this.player1.myColor !$= "verde" && %this.player2.myColor !$= "verde" && %this.player3.myColor !$= "verde" && %this.player4.myColor !$= "verde" && %this.player5.myColor !$= "verde"){
		%this.setColor($verde, %newPlayer);
	} else if(%this.player1.myColor !$= "azul" && %this.player2.myColor !$= "azul" && %this.player3.myColor !$= "azul" && %this.player4.myColor !$= "azul" && %this.player5.myColor !$= "azul"){
		%this.setColor($azul, %newPlayer);
	} else {
		%this.setColor($indigo, %newPlayer);
	}
	
	%this.createPlayerOnClients(%newPlayer);
		
	schedule(5000, 0, "serverJogoMakeGulokAI", %this);
}

function jogo::createPlayerOnClients(%this, %newPlayer){
	%eval = "%corEscolhida = $" @ %newPlayer.myColor @ ";";
	eval(%eval);
	for(%i = 0; %i < %this.playersAtivos; %i++){
		commandToClient(%this.simPlayers.getObject(%i).client, 'setColor', %corEscolhida, %newPlayer.id, "vermelho", true);
		commandToClient(%this.simPlayers.getObject(%i).client, 'SetGULOKAiPlayer');
	}
	if(%jogo.observadorOn){
		commandToClient(%this.observador, 'setColor', %corEscolhida, %newPlayer.id, "vermelho", true);
		commandToClient(%this.observador, 'SetGULOKAiPlayer');
	}
}

function serverJogoMakeGulokAI(%jogo){
	%jogo.makeGulokAI();
}

function jogo::makeGulokAI(%this){
	serverCriarPersonaAiGulok(%this);
	%this.setGuloksVars(); //prepara os clients para receberem as variáveis deste player;
	
	%this.elegerAiManager(); //pega um client que não esteja morto
	%this.aiStep = 0;
	%this.askAIdirections();
}

function serverJogoElegerAiManager(%jogo){
	%jogo.elegerAIManager();
}

function jogo::elegerAIManager(%this){
	for(%i = 0; %i < %this.simPlayers.getcount(); %i++){
		%player = %this.simPlayers.getObject(%i);
		if(!%player.taMorto){
			%antigoAiManager = %this.aiManager;
			echo("Elegendo Ai Manager(jogo" @ %this.num @ "): " @ %player.id); 
			commandToClient(%player.client, 'setAiManager');
			%this.aiManager = %player;
			if(%this.aiManager != %antigoAiManager){
				if(%this.jogadorDaVez == %this.aiPlayer){
					%this.askAIDirections();	
				}
			}
			return;
		}
	}
}

function jogo::askAIdirections(%this){
	if(!%this.terminado){
		commandToClient(%this.aiManager.client, 'askAIdirections', %this.aiStep);
		%this.aiStep++;
	}
}

function serverCompletarObjetivoGulok(%player){
	%player.objEspecial = 50;
	serverCmdBater(%player.client, %player.persona.nome);
	
	if(%player != %player.jogo.playerQueDespertouGuloks)
		return;
	
	if(%player.persona.user.TAXOconhece_g)
		return;
	
	%player.persona.user.TAXOconhece_g = 1;
	%myServerPesq = criarServerPesq();
	%myServerPesq.url = "/torque/conhece_g/" @ %player.persona.user.nome @ "?idPesqTorque=" @ %myServerPesq.num;
	$filas_handler.newFilaObj("abrir_guloks", %myServerPesq.url, 2, %myServerPesq, %player.persona);
	commandToClient(%player.client, 'descobrirGuloks');
}

function serverCmdGulokAIdecretarMoratorias(%client){
	%jogo = %client.player.jogo;
	%aiPlayer = %jogo.aiPlayer;
	echo("## AI: serverCmdGulokAIdecretarMoratorias: jogo(" @ %jogo.num @ "), " @ %jogo.aiPlayer.id);
	for(%i = 0; %i < %aiPlayer.mySimExpl.getCount(); %i++){
		%info = %aiPlayer.mySimExpl.getObject(%i);
		echo("## AI: INFO " @ %info.num @ " FOUND!");
		schedule(5000 + (%i * 1000), 0, "serverCmdEmbargar", %client, %info.num);
	}
}