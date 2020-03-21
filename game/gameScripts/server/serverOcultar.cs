// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverOcultar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quarta-feira, 12 de novembro de 2008 23:47
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdOcultar(%client){
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	%player = %persona.player;
	%player.oculto = true;
	
	if(%persona.aca_av_3 > 0){
		%player.imperiais -= %player.ocultarCusto;
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			%client = %jogo.simPlayers.getObject(%i).client;
			commandToClient(%client, 'ocultar', %player.id);
		}
		if(%this.observadorOn){
			commandToClient(%jogo.observador, 'ocultar', %player.id);
		}
	}
}