// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverGM.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 10 de março de 2008 20:13
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function serverCmdGMAskGive(%client, %praQuem, %imp, %min, %pet, %ura){
	echo("**GM**> Give: %praQuem = " @ %praQuem @ ", %imp = " @ %imp @ ", %min = " @ %min @ ", %pet = " @ %pet @ ", %ura = " @ %ura);
	%user = %client.user;
	%jogo = %client.player.jogo;
	if(%user.nome $= "Dani"){
		if(%praQuem $= "mim"){
			%player = %client.player;
		} else {
			%eval = "%player = %jogo." @ %praQuem @ ";";
			eval(%eval);		
		}
		%player.imperiais += %imp;
		%player.minerios += %min;
		%player.petroleos += %pet;
		%player.uranios += %ura;
	}
	commandToClient(%player.client, 'atualizarGrana', %player.imperiais, %player.minerios, %player.petroleos, %player.uranios);
}

function serverCmdGMAskDesastre(%client, %tipo, %onde){
	%user = %client.user;
	if(%client.player $= "OBSERVADOR"){
		echo("ATENÇÃO: OBSERVADOR PEDIU UM DESASTRE: JOGO(" @ %jogo.num @ ")");
		%jogo = %client.jogo;
	} else {
		%jogo = %client.player.jogo;
	}
	if(%user.nome $= "Dani"){
		if(isObject(%jogo)){
			%eval = "%myArea = %jogo." @ %onde @ ";";
			eval(%eval);
				
			%jogo.desastre(%myArea, %tipo);
		}
	}
}

function serverCmdSetPrizeNight(){
	$serverPrizeNight = true;	
	$serverPrizeUsers = new SimSet();
	
	for(%i = 0; %i < $serverSimUSERS.getCount(); %i++){
		%user = $serverSimUSERS.getObject(%i);
		$serverPrizeUsers.add(%user);
		%eval = "$user" @ %user.nome @ "PrizeCountInicial = " @ %user.persona.TAXOpontos @ ";";
		eval(%eval);
	}
}

function serverCmdFinalizarPrizeNight(%client){
	%myPrizeNightResultString = "inicio";
	for(%i = 0; %i < $serverPrizeUsers.getCount(); %i++){
		%user = $serverPrizeUsers.getObject(%i);
		%eval = "%myCountInicial = $user" @ %user.nome @ "PrizeCountInicial;";
		eval(%eval);
		%myNightCount = %user.persona.TAXOpontos - %myCountInicial;
		%myPrizeNightResultString = %myPrizeNightResultString SPC %user.persona.nome SPC %myNightCount;
	}
	
	commandToClient(%client, 'resultPrizeNight', %myPrizeNightResultString);	
}

function serverCmdComprarCom100Omnis(%client, %username, %pesquisaId){
	%eval = "%user = $user" @ %username @ ";";
	eval(%eval);
	%persona = %user.persona;
	%user.TAXOomnis += 100;
	serverCmda_comprar(%user.client, %pesquisaId, %liderNum);
}

function serverCmdGmMakeGulok(%client, %userName){
	if(%client.persona.user == $userDani){
		%eval = "%user = $user" @ %userName @ ";";
		eval(%eval);
		
		%user.persona.gulok = true;
		
		commandToClient(%user.persona.client, 'GmMakeGulok');
	}
}

function serverCmdGMsalaPoker(%client, %blind, %apostaMax, %fichasIniciaisGlobal)
{
	//if(%client.persona.user != $userDani)
	//	return;
	
	%sala = %client.persona.sala;
	%sala.setTipoDeJogo("poker");
	
	if(%blind !$= ""){
		%sala.blind = %blind;
	} else {
		%sala.blind = 1;	
	}
	
	if(%apostaMax !$= ""){
		%sala.apostaMax = %apostaMax;
	} else {
		%sala.apostaMax = 6;	
	}
	
	if(%fichasIniciaisGlobal !$= ""){
		%sala.fichasIniciaisGlobal = %fichasIniciaisGlobal;
	} else {
		%sala.fichasIniciaisGlobal = 20;
	}
	
	%sala.CTCalterarTipoDeJogo("poker");
}

function serverCmdGMgiveTaxoPk_fichas(%userNome, %qtd)
{
	echo(%userNome @ " -> " @ %qtd @ " fichas");
	%eval = "$user" @ %userNome @ ".persona.pk_fichas = " @ %qtd @ ";";
	eval(%eval);
}

function serverGetUserPorUsername(%username)
{
	%eval = "%user = $user" @ %username @ ";";
	eval(%eval);
	return %user;
}