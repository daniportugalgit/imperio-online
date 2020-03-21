// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverMatriarca.cs
// Copyright          :  
// Author             :  admin
// Created on         :  segunda-feira, 8 de dezembro de 2008 20:50
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdMatriarca(%client, %areaNome){
	%jogo = %client.player.jogo;
	%eval = "%areaNoJogo = %jogo." @ %areaNome @ ";";
	eval(%eval);
	%rainha = %areaNoJogo.pos0Quem;
	%persona = %rainha.dono.persona;
	%rainha.matriarca = true;	
	%rainha.crisalida = false;
	%rainha.isMoveable = false;
	if(!isObject(%rainha.dono.mySimMatriarcas)){
		%rainha.dono.mySimMatriarcas = new SimSet();
	}
	%rainha.dono.mySimMatriarcas.add(%rainha);
	%rainha.dono.mySimCrisalidas.remove(%rainha);
	
	%custo = 10;
	if(%persona.aca_av_4 > 0){
		%custo = 8;	
	}
	
	%rainha.dono.imperiais -= %custo;
	%rainha.onde.resolverMyStatus();
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		%client = %jogo.simPlayers.getObject(%i).client;
		commandToClient(%client, 'matriarca', %areaNome);
	}
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'matriarca', %areaNome);
	}
}