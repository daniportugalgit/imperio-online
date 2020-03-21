// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverVenderRecursos.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  domingo, 3 de fevereiro de 2008 22:14
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function jogo::espalharNoticia(%this, %quemVendeu, %vendeuOQue){
	%nomeDeQuemVendeu = %quemVendeu.persona.nome;
	
	if(!%this.semPesquisas){
		for(%i = 0; %i < %this.playersAtivos; %i++){
			%persona = %this.simPlayers.getObject(%i).persona;
			if((%persona.aca_i_1 > 1 && %quemVendeu.persona.aca_i_1 < 3) || (%persona.aca_i_1 == 3 && %quemVendeu.persona.aca_i_1 == 3) || (%persona.aca_i_1 > 0 && %persona.especie $= "gulok" && %quemVendeu.persona.aca_i_1 < 3)){
				if(%persona.nome !$= %nomeDeQuemVendeu){
					commandToClient(%persona.client, 'i_intelVendeu', %nomeDeQuemVendeu, %vendeuOQue);	
				}
			}
		}
	}
	if(%this.observadorOn){
		commandToClient(%this.observador, 'i_intelVendeu', %nomeDeQuemVendeu, %vendeuOQue);	
	}
}


function serverCmdVenderUranio(%client, %player){
	%jogo = %client.player.jogo;
	%eval = "%vendedorNoJogo = %jogo." @ %player @ ";";
	eval(%eval);
	
	if(%vendedorNoJogo.uranios > 2){
		%vendedorNoJogo.uranios -= 3;
		%vendedorNoJogo.imperiais += 10;
		//echo("3 Uranios vendidos por 10 Imperiais (" @ %player @ "), JOGO " @ %jogo.num);
		commandToClient(%vendedorNoJogo.client, 'venderUranio');	
		serverClearUndo(%vendedorNoJogo);
		
		%jogo.espalharNoticia(%vendedorNoJogo, "Ura");
	} else {
		commandToClient(%vendedorNoJogo.client, 'recursosInsuficientes');
	}
	
	serverVerificarObjetivos(%vendedorNoJogo);
}


function serverCmdVenderPetroleo(%client, %player){
	%jogo = %client.player.jogo;
	%eval = "%vendedorNoJogo = %jogo." @ %player @ ";";
	eval(%eval);
	
	if(%vendedorNoJogo.petroleos > 3){
		%vendedorNoJogo.petroleos -= 4;
		%vendedorNoJogo.imperiais += 10;
		//echo("4 Petroleos vendidos por 10 Imperiais (" @ %player @ "), JOGO " @ %jogo.num);
		commandToClient(%vendedorNoJogo.client, 'venderPetroleo');
		serverClearUndo(%vendedorNoJogo);
		
		%jogo.espalharNoticia(%vendedorNoJogo, "Pet");
	} else {
		commandToClient(%vendedorNoJogo.client, 'recursosInsuficientes');
	}
	
	serverVerificarObjetivos(%vendedorNoJogo);
}

function serverCmdVenderMinerio(%client, %player){
	%jogo = %client.player.jogo;
	%eval = "%vendedorNoJogo = %jogo." @ %player @ ";";
	eval(%eval);
	
	if(%vendedorNoJogo.minerios > 4){
		%vendedorNoJogo.minerios -= 5;
		%vendedorNoJogo.imperiais += 10;
		//echo("5 Minerios vendidos por 10 Imperiais (" @ %player @ "), JOGO " @ %jogo.num);
		commandToClient(%vendedorNoJogo.client, 'venderMinerio');
		serverClearUndo(%vendedorNoJogo);
		
		%jogo.espalharNoticia(%vendedorNoJogo, "Min");
	} else {
		commandToClient(%vendedorNoJogo.client, 'recursosInsuficientes');
	}
	
	serverVerificarObjetivos(%vendedorNoJogo);
}

function serverCmdVenderConjunto(%client, %player){
	%jogo = %client.player.jogo;
	%eval = "%vendedorNoJogo = %jogo." @ %player @ ";";
	eval(%eval);	
	
	if(%vendedorNoJogo.minerios > 0 && %vendedorNoJogo.petroleos > 0 && %vendedorNoJogo.uranios > 0){
		%vendedorNoJogo.minerios -= 1;
		%vendedorNoJogo.petroleos -= 1;
		%vendedorNoJogo.uranios -= 1;
		%vendedorNoJogo.imperiais += 10;
		//echo("1 conjunto de Recursos vendido por 10 Imperiais (" @ %player @ "), JOGO " @ %jogo.num);
		commandToClient(%vendedorNoJogo.client, 'venderConjunto');
		serverClearUndo(%vendedorNoJogo);
		
		%jogo.espalharNoticia(%vendedorNoJogo, "Cj");
	} else {
		commandToClient(%vendedorNoJogo.client, 'recursosInsuficientes');
	}
	
	serverVerificarObjetivos(%vendedorNoJogo);
}
