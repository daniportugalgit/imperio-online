// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverVirus.cs
// Copyright          :  
// Author             :  admin
// Created on         :  segunda-feira, 15 de dezembro de 2008 18:14
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function serverCmdDispararVirus(%client, %matriarcaAreaNome, %fronteirasAtingidas, %fronteirasNomes, %AI){
	%jogo = %client.player.jogo;
	if(%AI){
		%player = %jogo.aiPlayer;
		%persona = %player.persona;
	} else {
		%persona = %client.persona;
		%player = %client.player;
	}
	
	
	
	echo("serverCmdDispararVirus: " @ %matriarcaAreaNome SPC %fronteirasAtingidas SPC %fronteirasNomes);
	
	//primeiro veirifica se a persona tem vírus:
	if(%persona.aca_av_2 > 0){
		%player.virusDisparados = 1; //marca que o truta jah fez o disparo da partida
				
		//agora pega cada área:
		for(%i = 0; %i < %fronteirasAtingidas; %i++){
			%myWord = getWord(%fronteirasNomes, %i);
			%eval = "%area[%i] = %jogo." @ %myWord @ ";";
			eval(%eval);
		}
				
		//primeiro solta o vírus na área-alvo:
		serverVirus(%jogo, %area[0]);
		
		//agora pega cada fronteira
		for(%i = 1; %i < %fronteirasAtingidas + 1; %i++){
			serverVirus(%jogo, %area[%i]);	
		}
		
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			%player = %jogo.simPlayers.getObject(%i);
			commandtoClient(%player.client, 'dispararVirus', %matriarcaAreaNome, %fronteirasAtingidas, %fronteirasNomes); 
		}
		if(%jogo.observadorOn){
			commandtoClient(%jogo.observador, 'dispararVirus', %matriarcaAreaNome, %fronteirasAtingidas, %fronteirasNomes); 
		}
	}
}

function serverVirus(%jogo, %area){
	if(!isObject(%jogo.simAreasComVirus)){
		%jogo.simAreasComVirus = new SimSet();
	}
	%jogo.simAreasComVirus.add(%area);	
}

function jogo::clearAllViruses(%this){
	if(isObject(%this.simAreasComVirus)){
		for(%i = 0; %i < %this.simAreasComVirus.getcount(); %i++){
			%area = %this.simAreasComVirus.getObject(0);
			%this.simAreasComVirus.remove(%area);
		}
	}
}
