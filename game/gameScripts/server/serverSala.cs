// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverSala.cs
// Copyright          :  
// Author             :  admin
// Created on         :  segunda-feira, 2 de fevereiro de 2009 0:08
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function sala::getDono(%this)
{
	return %this.simPersonas.getObject(0);
}


function sala::removerPersona(%this, %persona)
{
	%persona.sala = "no";
	%persona.pronto = false;
	%this.simPersonas.remove(%persona);
	%persona.lastJogoTaxoId = %this.jogoTaxoId;
	
	if (!%persona.fechouTorque)
	{
		$personasNoAtrio.add(%persona);
		
		if(%this.poker && isObject(%persona.acadBKP))
			%persona.resetAcadFromBKP();
	}
		
	if (%this.simPersonas.getCount() >= 1)
	{
		%this.dificuldade = false;
		
		%this.rebuildSalaComDados(%persona);
		%this.resetarPlaneta();
		return;
	}
		
	serverDestruirSala(%this);
}

function sala::kickarPersona(%this, %num)
{
	%persona = %this.simPersonas.getObject(%num);
	
	%this.removerPersona(%persona);
	
	%this.CTCvoceFoiKikado(%persona);
	
	updateSalaChatText(%this, "GM-BOT", ">>> A persona " @ %persona.nome @ " foi expulsa da sala por " @ %this.simPersonas.getObject(0).nome @ "! <<<");
	
	if(!isObject(%this.banList))
		%this.banList = new SimSet();
		
	%this.banList.add(%persona);
}

function sala::kickarPersonaPorFaltaDeFichas(%this, %num)
{
	%persona = %this.simPersonas.getObject(%num);
	
	%this.removerPersona(%persona);
	
	%this.CTCvoceFoiKikadoPorFaltaDeFichas(%persona);
	
	updateSalaChatText(%this, "GM-BOT", "A persona " @ %persona.nome @ " foi expulsa da sala por não ter fichas para continuar!");
}

function sala::CTCvoceFoiKikado(%this, %persona)
{
	commandToClient(%persona.client, 'voceFoiKikado', serverGetAtrioSalasString(), $personasNoAtrio.getCount());	
}
function sala::CTCvoceFoiKikadoPorFaltaDeFichas(%this, %persona)
{
	commandToClient(%persona.client, 'voceFoiKikadoPorFaltaDeFichas', serverGetAtrioSalasString(), $personasNoAtrio.getCount());	
}

function sala::resetarPlaneta(%this)
{
	%donoDaSala = %this.getDono();
		
	if(!%donoDaSala.temUngart() && %this.planeta.nome $= "Ungart")
	{
		serverCmdAlterarPlaneta(%donoDaSala.client, "Terra");
		return;
	}
	
	if(!%donoDaSala.temTeluria() && %this.planeta.nome $= "Teluria")
	{
		serverCmdAlterarPlaneta(%donoDaSala.client, "Terra");
		return;
	}
}

function sala::rebuildSalaComDados(%this, %persona)
{
	%count = %this.simPersonas.getCount();
	
	for (%i = 0; %i < %count; %i++)
	{
		%personaTemp = %this.simPersonas.getObject(%i);
		if (%persona != %personaTemp)
		{
			%personaTemp.rebuildSalaComDados();
		}
	}
	
	if(%this.observadorOn && isObject(%this.observador))
	{
		%this.observador.persona.rebuildSalaComDados();
	}
}

function sala::rebuildSalaComDadosPrivate(%this, %client)
{
	%client.persona.rebuildSalaComDados();
}

function sala::getLotada(%this)
{
	if(%this.simPersonas.getCount() >= %this.lotacao)
		return true;
		
	return false;
}

function sala::CTClotacaoEstourou(%this, %client)
{
	commandToClient(%client, 'lotacaoEstourou');	
}


function sala::adicionarPersona(%this, %persona){
	if (%persona.offLine)
		return;
		
	%persona.pronto = false;
	%this.simPersonas.add(%persona);
	%persona.sala = %this;
	
	$personasNoAtrio.remove(%persona);
	%this.dificuldade = false;
}

function sala::setNoObservador(%this)
{	
	%this.observador = "no";
	%this.observadorOn = false;
}

function sala::getDificuldadeRelativa(%this)
{
	return mFloor(%this.getDificuldade() / %this.simPersonas.getCount());
}

function sala::getDificuldade(%this)
{
	if (!%this.dificuldade){
		%numDePersonas = %this.simPersonas.getCount();
		%mediaVit = 0;
		for(%i = 0; %i < %numDePersonas; %i++){
			%persona = %this.simPersonas.getObject(%i);
			serverCalcularPersonaDif(%persona);
			%mediaVit += %persona.mediaVit;	
		}
		
		%this.dificuldade = %mediaVit;
	}

	return %this.dificuldade;
}

function sala::getPersona(%this, %i){
	if(isObject(%this.simPersonas.getObject(%i)))
		return %this.simPersonas.getObject(%i);
		
	return "";
}

function sala::getPersonaStatsPlus(%this, %i){
	if (!isObject(%this.getPersona(%i))){
		return "";
	}

	return %this.getPersona(%i).getStatsPlus();
}

function sala::getPersonaStats(%this, %i){
	if (!isObject(%this.getPersona(%i))){
		return "";
	}
		
	return %this.getPersona(%i).getStats();
}

function sala::checkBan(%this, %persona)
{
	if(!isObject(%this.banList))
		return false;
	
	if(%this.banList.isMember(%persona))
		return true;
	
	return false;
}

function sala::zerarTiposDeJogo(%this)
{
	%this.semPesquisas = false;
	%this.emDuplas = false;
	%this.handicap = false;
	%this.poker = false;
	%this.set = false;	
}

function sala::setTipoDeJogo(%this, %tipo)
{
	%this.zerarTiposDeJogo();
	%this.tipoDeJogo = %tipo;
	
	switch$(%tipo)
	{
		case "emDuplas": %this.emDuplas = true;
		case "semPesquisas": %this.semPesquisas = true;
		case "handicap": %this.handicap = true;
		case "set": %this.set = true;
		case "poker": %this.poker = true;
	}
	%this.resetLotacaoPorPlaneta();
}

function sala::resetLotacaoPorPlaneta(%this)
{
	if(%this.emDuplas)
	{
		%this.lotacao = 6;
		return;		
	}
	
	%this.lotacao = %this.planeta.lotacao;
}

function sala::CTCalterarTipoDeJogo(%this, %tipo)
{
	for(%i = 0; %i < %this.simPersonas.getCount(); %i++)
		commandToClient(%this.simPersonas.getObject(%i).client, 'alterarTipoDeJogo', %tipo);
	
	if(isObject(%this.observador))
		commandToClient(%this.observador, 'alterarTipoDeJogo', %tipo);
}

function sala::CTCalterarTurno(%this, %tempo)
{
	for(%i = 0; %i < %this.simPersonas.getCount(); %i++)
		commandToClient(%this.simPersonas.getObject(%i).client, 'alterarTurno', %tempo);
	
	if(isObject(%this.observador))
		commandToClient(%this.observador, 'alterarTurno', %tempo);
}

function sala::CTCalterarLotacao(%this, %lotacao)
{
	for(%i = 0; %i < %this.simPersonas.getCount(); %i++)
		commandToClient(%this.simPersonas.getObject(%i).client, 'alterarLotacao', %lotacao);
	
	if(isObject(%this.observador))
		commandToClient(%this.observador, 'alterarLotacao', %lotacao);
}

function sala::CTCalterarPlaneta(%this, %planetaNome)
{
	for(%i = 0; %i < %this.simPersonas.getCount(); %i++)
		commandToClient(%this.simPersonas.getObject(%i).client, 'alterarPlaneta', %planetaNome);

	if(isObject(%this.observador))
		commandToClient(%this.observador, 'alterarPlaneta', %planetaNome);
}

///////////////////
//InfoAtual:
function sala::setInfoAtual(%this)
{
	%this.myInfoAtual = %this.simPersonas.getCount(); //primeira palavra é o número de jogadores.
	
	%this.myInfoAtual = %this.myInfoAtual SPC %this.getRodadaAtual(); //segunda palavra é a rodada
	
	for(%i = 0; %i < %this.simPersonas.getCount(); %i++)
	{
		%this.myInfoAtual = %this.myInfoAtual SPC %this.simPersonas.getObject(%i).nome;	
	}
}

function sala::CTCsalaInfoAtual(%this, %client)
{
	commandToClient(%client, 'salaInfoAtual', %this.myInfoAtual);
}

function sala::getRodadaAtual(%this)
{
	if(!isObject(%this.jogo))
		return "";
		
	if(%this.jogo.terminado)
		return "";
		
	return %this.jogo.rodada;
}

