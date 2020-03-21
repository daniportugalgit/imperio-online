// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverNexus.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 23 de novembro de 2008 6:47
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdAlquimia(%client, %recursoNum){
	%persona = %client.persona;
	%player = %client.player;
	%jogo = %player.jogo;
		
	//if(%persona.especie $= "humano" && %persona.aca_v_6 != 3)
		//return;
		
	%player.imperiais -= 3;
	%result = dado(100, 0);
	echo("RESULT: " @ %result);
	%lvl = mFloor(%persona.aca_art_2 / 50);
	if(%result <= 80 - (%lvl * 20)){
		%recurso = "minerios";
		%player.minerios += 1;
		
	} else {
		if(%recursoNum == 1){
			%recurso = "petroleos";
			%player.petroleos += 1;	
			
		} else if(%recursoNum == 2){
			%recurso = "uranios";
			%player.uranios += 1;
			
		}
	}
	
	commandToClient(%client, 'alquimia', %recurso);
	serverVerificarObjetivos(%player);
	
}

function serverCmdNexus_investir(%client){
	%persona = %client.persona;
	%player = %client.player;
	%jogo = %player.jogo;	
	
	if(!%jogo.terminado){
		%player.minerios -= 1;
		%player.petroleos -= 1;
		%player.uranios -= 1;
		
		%persona.aca_art_2++;
		echo("art_2 = " @ %persona.aca_art_1);
		%player.nexusInvest++;
		commandToClient(%client, 'nexus_investir');		
	}
}

