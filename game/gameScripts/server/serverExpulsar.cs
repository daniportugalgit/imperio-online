// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverExpulsar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 16 de dezembro de 2008 9:47
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdExpulsar(%client, %areaNome){
	%jogo = %client.player.jogo;
	%eval = "%areaNoJogo = %jogo." @ %areaNome @ ";";
	eval(%eval);
	%rainha = %areaNoJogo.pos0Quem;
	%persona = %rainha.dono.persona;
	
	%custo = 3 - %persona.aca_av_3;	
	
	if(%persona.aca_av_3 > 0){
		if(%persona.aca_av_3 == 3){
			%todos = false;
		} else {
			%todos = true;	
		}
	}
	
	if(%todos){
		%transporteCount = %rainha.myTransporte.getCount();
		for(%i = 0; %i < %transporteCount; %i++){
			//schedule(500 + %i * 1000, 0, "serverCmdDesembarcar", %client, %areaNome, "pos0", %areaNome, "verme", "minhaCor", false, "", true);
			//serverCmdDesembarcar(%client, %areaNome, "pos0", %areaNome, "verme", "minhaCor", false, "", true);
			serverDesembarcar(%jogo, %rainha.myTransporte.getObject(0), %areaNoJogo, true);
			%rainha.myTransporte.remove(%rainha.myTransporte.getObject(0));
		}
	} else {
		serverDesembarcar(%jogo, %rainha.myTransporte.getObject(0), %areaNoJogo, true);
		%rainha.myTransporte.remove(%rainha.myTransporte.getObject(0));
	}
	
	%persona.player.imperiais -= %custo;
	
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		%client = %jogo.simPlayers.getObject(%i).client;
		commandToClient(%client, 'expulsar', %areaNome, %todos);
	}
	if(%this.observadorOn){
		commandToClient(%jogo.observador, 'expulsar', %areaNome, %todos);
	}
}