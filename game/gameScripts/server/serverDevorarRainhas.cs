// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverDevorarRainhas.cs
// Copyright          :  
// Author             :  admin
// Created on         :  sábado, 13 de dezembro de 2008 17:56
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdDevorarRainha(%client, %areaNome, %pos){
	%jogo = %client.player.jogo;
	%eval = "%areaNoJogo = %jogo." @ %areaNome @ ";";
	eval(%eval);
	%eval = "%zangao = %areaNoJogo." @ %pos @ "Quem;";
	eval(%eval);
	%rainha = %areaNoJogo.pos0Quem;
	
	%zangao.myBonusDevorar += 2 + %zangao.dono.persona.aca_ldr_1_h4;	
	%zangao.myRainhasDevoradas++;
		
	serverRemoverUnidade(%jogo, %rainha, %rainha.onde);
	%rainha.safeDelete();
		
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		%client = %jogo.simPlayers.getObject(%i).client;
		commandToClient(%client, 'devorarRainha', %areaNome, %pos);
	}
	if(%this.observadorOn){
		commandToClient(%jogo.observador, 'devorarRainha', %areaNome, %pos);
	}
}