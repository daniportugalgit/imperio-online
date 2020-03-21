// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverTerceiroLider.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 1 de abril de 2008 1:28
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdChamarTerceiroLider(%client, %onde){
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	%player = %client.player;
	
	if(%persona.aca_a_1 > 2){
		if(%player.mySimLideres.getCount() < 2){
			if(%player.terceiroLiderOn == false){
				%jogo.chamarTerceiroLider(%onde);		
			}
		}
	}
}



function jogo::chamarTerceiroLider(%this, %onde){
	%eval = "%areaNoJogo = %this." @ %onde @ ";";
	eval(%eval);
	
	%base = %areaNoJogo.pos0Quem;
	
	%newLider = %base.spawnUnit("lider");
	%newLiderEscudo = %base.dono.persona.aca_ldr_3_h1;
	%newLiderJetPack = %base.dono.persona.aca_ldr_3_h2;
	%newLiderSnipers = %base.dono.persona.aca_ldr_3_h3 * 2;
	%newLiderMoral = %base.dono.persona.aca_ldr_3_h4;
	%newLider.liderNum = 3;
	
	if(%newLiderEscudo > 0){
		%newLider.criarMeuEscudo();
		%newLider.myEscudos = %newLiderEscudo;
	}
	
	if(%newLiderJetPack > 1){
		%newLider.JPBP = %newLiderJetPack - 1;
		%newLider.JPagora = %newLiderJetPack - 1;
		%newLider.anfibio = true;
	} else {
		%newLider.JPBP = %newLiderJetPack;
		%newLider.JPagora = %newLiderJetPack;
	}
	%newLider.mySnipers = %newLiderSnipers;
	%newLider.myMoral = %newLiderMoral;
	
	%newLider.dono.terceiroLiderOn = true;
	%this.verificarMoral(%newLider.dono);

	for(%i = 0; %i < %this.playersAtivos; %i++){
		%player = %this.simPlayers.getObject(%i);
		commandToClient(%player.client, 'chamarTerceiroLider', %onde, %newLiderEscudo, %newLiderJetPack, %newLiderSnipers, %newLiderMoral);
	}
	if(%this.observadorOn){
		commandToClient(%this.observador, 'chamarTerceiroLider', %onde, %newLiderEscudo, %newLiderJetPack, %newLiderSnipers, %newLiderMoral);
	}
}


function serverCmdConvocarPkLider(%client, %onde){
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	%player = %client.player;
	
	if(%player.pk_lideresDisponiveis < 1)
		return;
		
	%jogo.convocarPkLider(%onde);
}



function jogo::convocarPkLider(%this, %onde){
	%eval = "%areaNoJogo = %this." @ %onde @ ";";
	eval(%eval);
	
	%base = %areaNoJogo.pos0Quem;
	
	%newLider = %base.spawnUnit("lider");
	%newLider.liderNum = %base.dono.mySimLideres.getCount();
	
	%newLider.criarMeuEscudo();
	%newLider.myEscudos = 2;
	
	
	%newLider.JPBP = 1;
	%newLider.JPagora = 1;
	%newLider.anfibio = true;
	%newLider.mySnipers = 3;
	%newLider.myMoral = 2;
		
	%this.verificarMoral(%newLider.dono);

	for(%i = 0; %i < %this.playersAtivos; %i++){
		%player = %this.simPlayers.getObject(%i);
		commandToClient(%player.client, 'convocarPkLider', %onde);
	}
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'convocarPkLider', %onde);
}
