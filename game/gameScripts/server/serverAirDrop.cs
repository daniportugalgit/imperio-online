// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverAirDrop.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 28 de março de 2008 23:40
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdAirDrop(%client, %onde){
	echo("AIRDROP EM: " @ %onde);
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	%player = %client.player;
	
	%custo = 5;
	if(%persona.aca_v_5 > 2){
		%custo = 4;	
	}
	
	if(%player == %jogo.jogadorDaVez){
		if(%player.airDrops > 0 && %player.imperiais >= %custo){
			%player.airDrops--;
			%player.imperiais -= %custo;
			%eval = "%areaNoJogo = %jogo." @ %onde @ ";";
			eval(%eval);
			
			for(%i = 0; %i < 3; %i++){
				%newUnit = tanque_BP.createCopy();
				%newUnit.isSelectable = 1;
				%newUnit.isMoveable = 1;
				%newUnit.dono = %player;
				%player.mySimUnits.add(%newUnit); //adiciona a unit no simset de unidades do player;	
				%areaNoJogo.positionUnit(%newUnit);	
			}
			
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				%jogador = %jogo.simPlayers.getObject(%i);
				commandToClient(%jogador.client, 'airDrop', %onde);
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'airDrop', %onde);
			}
		}
	}
}