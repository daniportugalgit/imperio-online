// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverCanibalizar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quinta-feira, 11 de dezembro de 2008 5:39
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdCanibalizar(%client, %areaNome, %pos){
	%jogo = %client.player.jogo;
	%eval = "%areaNoJogo = %jogo." @ %areaNome @ ";";
	eval(%eval);
	%eval = "%zangao = %areaNoJogo." @ %pos @ "Quem;";
	eval(%eval);
	
	%vermesPool = new SimSet();
	if(%areaNoJogo.pos1Quem.class $= "verme"){
		%vermesPool.add(%areaNoJogo.pos1Quem);
	}
	if(%areaNoJogo.pos2Quem.class $= "verme"){
		%vermesPool.add(%areaNoJogo.pos2Quem);
	}
	for(%i = 0; %i < %areaNoJogo.myPos3List.getCount(); %i++){
		%vermesPool.add(%areaNoJogo.myPos3List.getObject(%i));
	}
	
	%vermesCount = %vermesPool.getCount();
	if(%zangao.dono.persona.aca_ldr_1_h3 == 1){
		%zangao.myBonusCanibalMax = %vermesCount;	
	} else if(%zangao.dono.persona.aca_ldr_1_h3 == 2){
		%zangao.myBonusCanibalMax = %vermesCount * 2;	
	} else if(%zangao.dono.persona.aca_ldr_1_h3 == 3){
		%zangao.myBonusCanibalMax = %vermesCount * 3;	
	}
	
	for(%i = 0; %i < %vermesPool.getCount(); %i++){
		%verme = %vermesPool.getObject(0);
		serverRemoverUnidade(%jogo, %verme, %verme.onde);
		%verme.safeDelete();
	}
	
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		%client = %jogo.simPlayers.getObject(%i).client;
		commandToClient(%client, 'canibalizar', %areaNome, %pos);
	}
	if(%this.observadorOn){
		commandToClient(%jogo.observador, 'canibalizar', %areaNome, %pos);
	}
}