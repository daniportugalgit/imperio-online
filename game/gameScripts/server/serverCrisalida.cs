// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverCrisalida.cs
// Copyright          :  
// Author             :  admin
// Created on         :  segunda-feira, 8 de dezembro de 2008 3:51
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdCrisalida(%client, %areaNome){
	%jogo = %client.player.jogo;
	%eval = "%areaNoJogo = %jogo." @ %areaNome @ ";";
	eval(%eval);
	%rainha = %areaNoJogo.pos0Quem;
	%persona = %rainha.dono.persona;
	
	%rainha.crisalida = true;
	%rainha.isMoveable = false;
	if(!isObject(%rainha.dono.mySimCrisalidas)){
		%rainha.dono.mySimCrisalidas = new SimSet();
	}
	%rainha.dono.mySimCrisalidas.add(%rainha);
	
	%custo = 10;
	if(%persona.aca_v_5 == 3){
		%custo = 8;	
	}
	if(%persona.aca_av_4 > 0){
		%custo = 7;	
	}
	
	%rainha.dono.imperiais -= %custo;
	%rainha.onde.resolverMyStatus();
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		%client = %jogo.simPlayers.getObject(%i).client;
		commandToClient(%client, 'crisalida', %areaNome);
	}
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'crisalida', %areaNome);
	}
}