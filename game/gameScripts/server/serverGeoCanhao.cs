// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverGeoCanhao.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 23 de novembro de 2008 18:55
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdGeoDisparo(%client, %fronteirasValidas, %fronteirasNomes){
	%persona = %client.persona;
	%player = %client.player;
	%jogo = %player.jogo;
	%player.atacou = true;
	echo("clientAskGeoDisparo: " @ %fronteirasValidas SPC %fronteirasNomes);
	
	//primeiro veirifica se a persona tem a planetas2:
	if(%persona.aca_v_6 >= 2){
		%player.geoDisparos = 1; //marca que o truta jah fez o disparo da partida
			
		//agora verifica o tipo de desastre:
		if(%persona.aca_art_1 < 6){
			%tipo = "vulcao";
		} else {
			%tipo = "furacao";
		}
		
		//agora pega cada área:
		for(%i = 0; %i < %fronteirasValidas; %i++){
			%myWord = getWord(%fronteirasNomes, %i);
			%eval = "%area[%i] = %jogo." @ %myWord @ ";";
			eval(%eval);
		}
				
		//primeiro mata todos na área:
		%jogo.killAll(%area[0], %player);
		
		//agora pega cada fronteira
		for(%i = 1; %i < %fronteirasValidas + 1; %i++){
			%jogo.killAll(%area[%i], %player);	
		}
		
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			%player = %jogo.simPlayers.getObject(%i);
			commandtoClient(%player.client, 'geoDisparo', %fronteirasValidas, %fronteirasNomes, %tipo); 
		}
		if(%jogo.observadorOn){
			commandtoClient(%jogo.observador, 'geoDisparo', %fronteirasValidas, %fronteirasNomes, %tipo); 
		}
		%persona.aca_art_1 = 0;
	}
}

function serverCmdGeoCanhao_investir(%client){
	%persona = %client.persona;
	%player = %client.player;
	%jogo = %player.jogo;	
	
	if(!%jogo.terminado){
		%player.minerios -= 1;
		%player.petroleos -= 1;
		%player.uranios -= 1;
		
		%persona.aca_art_1++;
		echo("art_1 = " @ %persona.aca_art_1);
		%player.geoInvest++;
		commandToClient(%client, 'geoCanhao_investir');		
	}
}
