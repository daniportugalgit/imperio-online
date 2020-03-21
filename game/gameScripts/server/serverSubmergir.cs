// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverSubmergir.cs
// Copyright          :  
// Author             :  admin
// Created on         :  sábado, 6 de dezembro de 2008 19:23
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdSubmergir(%client, %areaNome){
	%jogo = %client.player.jogo;
	%eval = "%areaNoJogo = %jogo." @ %areaNome @ ";";
	eval(%eval);
	%rainha = %areaNoJogo.pos0Quem;
	
	%rainha.submersa = true;
	%rainha.dono.imperiais -= (4 - %rainha.dono.persona.aca_v_4);
	
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		%client = %jogo.simPlayers.getObject(%i).client;
		commandToClient(%client, 'submergir', %areaNome);
	}
	if(%this.observadorOn){
		commandToClient(%jogo.observador, 'submergir', %areaNome);
	}
}

function serverCmdEmergir(%client, %areaNome){
	%jogo = %client.player.jogo;
	%eval = "%areaNoJogo = %jogo." @ %areaNome @ ";";
	eval(%eval);
	%rainha = %areaNoJogo.pos0Quem;
	
	%rainha.submersa = false;
	
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		%client = %jogo.simPlayers.getObject(%i).client;
		commandToClient(%client, 'emergir', %areaNome);
	}
	if(%this.observadorOn){
		commandToClient(%jogo.observador, 'emergir', %areaNome);
	}
}