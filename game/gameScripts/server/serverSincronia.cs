// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverSincronia.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 5 de maio de 2009 16:57
//
// Editor             :  Codeweaver v. 1.2.3341.40715
//
// Description        :  Arquivo que concentra funções controladoras
//                    :  de sincronia entre clients e servidor
//                    :  
// ============================================================
/////////

function serverCmdConfirmarAtoFeito(%client, %playerId)
{
	%jogo = %client.player.jogo;
	%eval = "%jogo.lastActingPlayer = %jogo." @ %playerId @ ";";
	eval(%eval);
	%jogo.addAtoFinalizado(%client.player);
	echo("confirmando ato feito -> " @ %client.player.id @ "; -> SimLastAtoFinalizado getCount() == " @ %jogo.simLastAtoFinalizado.getCount());
}

function jogo::addAtoFinalizado(%this, %player)
{
	if(!isObject(%this.simLastAtoFinalizado))
		%this.simLastAtoFinalizado = new SimSet();
	
	if(!%this.simLastAtoFinalizado.isMember(%player))
		%this.simLastAtoFinalizado.add(%player);
	
	if(%this.getGoodToGo_act())
	{
		%this.simLastAtoFinalizado.clear();
		%this.CTCpopLastActServerComDot();
		%this.goodToGo_mov = true;
	}
}

function jogo::getGoodToGo_act(%this)
{
	%totalDeClients = %this.getTotalDeClients();
	if(%totalDeClients == %this.simLastAtoFinalizado.getCount())
		return true;
		
	return false;
}

function jogo::getTotalDeClients(%this)
{
	if(isObject(%this.observador))
		%clients++;	
		
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		if(isObject(%player.client) && !%player.persona.fechouTorque)
			%clients++;
	}
	return %clients;
}

function jogo::CTCpopLastActServerComDot(%this)
{
	commandToClient(%this.lastActingPlayer.client, 'popServerComDot');
}
