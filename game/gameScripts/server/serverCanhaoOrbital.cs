// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverCanhaoOrbital.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 9 de abril de 2008 21:37
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdDisparoOrbital(%client, %onde){
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	
	if(%persona.aca_a_2 > 0){
		if(%persona.player.disparosOrbitais == 0){
			if(%jogo.primeiraRodada == false){
				%jogo.canhaoOrbital(%client.player, %onde); 	
			}
		}
	}
}

function jogo::canhaoOrbital(%this, %autor, %onde){
	%this.disparosOrbitais++;
	
	%autor.uranios -= (4 - %autor.persona.aca_a_2);
	
	%autor.atacou = 1;
	%eval = "%areaNoJogo = %this." @ %onde @ ";";
	eval(%eval);
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%client = %this.simPlayers.getObject(%i).client;
		commandToClient(%client, 'canhaoOrbital', %areaNoJogo.myName);
	}
	if(%this.observadorOn){
		commandToClient(%this.observador, 'canhaoOrbital', %areaNoJogo.myName);
	}
	%this.killAll(%areaNoJogo, %autor);
	
	if(%onde $= "UNG_PaGu03"){
		if(!%this.emDuplas && %this.playersAtivos > 2){
			%result = dado(2, 0);
			if(%result == 1){
				schedule(4000, 0, "jogoAddGulokAI", %this, "Gulok", %autor); //50% de chance de acordar os guloks
			}
		}
	}
}

function jogoAddGulokAI(%jogo, %nome, %quemDespertou){
	%jogo.addAiPlayer(%nome, %quemDespertou);
}