// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverCortejar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 14 de dezembro de 2008 16:11
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdCortejar(%client, %areaNome, %pos){
	%jogo = %client.player.jogo;
	%eval = "%areaNoJogo = %jogo." @ %areaNome @ ";";
	eval(%eval);
	%eval = "%zangao = %areaNoJogo." @ %pos @ "Quem;";
	eval(%eval);
	%rainha = %areaNoJogo.pos0Quem;
	
	%zangao.dono.cortejos++;
		
	%rainha.cortejada = true;
		
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		%client = %jogo.simPlayers.getObject(%i).client;
		commandToClient(%client, 'cortejar', %areaNome, %pos);
	}
	if(%this.observadorOn){
		commandToClient(%jogo.observador, 'cortejar', %areaNome, %pos);
	}
}

function serverRemoverCortejadas(%jogo){
	for(%i = 0; %i < %jogo.jogadorDaVez.mySimBases.getcount(); %i++){
		%base = %jogo.jogadorDaVez.mySimBases.getObject(0);
		%base.cortejada = false;
	}
}