// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverVerificarPlayerStatus.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  domingo, 3 de fevereiro de 2008 22:29
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverVerificarGruposX(%player){
	%jogo = %player.jogo;
	
	for(%i = 0; %i < %jogo.gruposNoJogo.getCount(); %i++){
		%grupo = %jogo.gruposNoJogo.getObject(%i);
		%areasMinhas = 0;
		
		for (%j = 0; %j < %grupo.simAreas.getCount(); %j++){
			%area = %grupo.simAreas.getObject(%j);
			if(%area.dono == %player){
				%areasMinhas++;
			} else {
				if(%area.dono $= "COMPARTILHADA"){
					if(%area.dono1 == %player || %area.dono2 == %player){
						%areasMinhas++;	
					}
				}
			}
		}
		
		if(%areasMinhas == %grupo.simAreas.getCount()){
			%eval = "%jogo." @ %player.id @ "." @ %grupo.nome @ " = true;";
			eval(%eval);		
		} else {
			%eval = "%jogo." @ %player.id @ "." @ %grupo.nome @ " = false;";
			eval(%eval);	
		}
	}
}




/////////////////////////////////////
function jogo::verificarGruposGlobal(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		serverVerificarGruposX(%player);
	}
}

/////////////////////////////////
function serverVerificarObjetivos(%player)
{
	%player.jogo.verificarObjetivosPlayer(%player);
}

function jogo::verificarObjetivosPlayer(%this, %player)
{
	%this.setAreasCountPlayer(%player);
	%this.setImperioPlayer(%player);
	%this.checkObjetivoCompletoPlayer(%player, 1);
	%this.checkObjetivoCompletoPlayer(%player, 2);
}

function jogo::setAreasCountPlayer(%this, %player)
{
	for (%i = 0; %i < %player.mySimAreas.getCount(); %i++)
	{
		%area = %player.mySimAreas.getObject(%i);
	
		if (%area.terreno $= "mar"){
			%baias += 1;
		} else if(%area.terreno $= "terra"){
			%territorios += 1;
		}
	}	
	%player.baias = %baias;
	%player.territorios = %territorios;
}

function jogo::setImperioPlayer(%this, %player)
{
	%bases = %this.getBasesPlayer(%player);
	
	if(%this.emDuplas && %bases > 14)
	{
		%player.imperio = true;
		return;
	}
	
	if(%bases > 9)
	{
		%player.imperio = true; 
		return;
	}
	
	%player.imperio = false; //se chegou aki o cara não tem império
}

function jogo::getBasesPlayer(%this, %player)
{	
	return %player.mySimBases.getCount();
}

function jogo::getBasesMaritimasPlayer(%this, %player)
{
	for (%i = 0; %i < %player.mySimBases.getCount(); %i++)
	{
		%area = %player.mySimBases.getObject(%i).onde;
		if (%area.pos0Flag == 1 && %area.terreno $= "mar")
			%basesMaritimas += 1;
	}
	return %basesMaritimas;
}

function jogo::checkObjetivoCompletoPlayer(%this, %player, %num)
{
	if(%this.getObjetivoCompleto(%player, %player.mySimObj.getObject(%num-1))){
		%this.setObjetivoCompleto(%player, %num);
		%this.CTCligarBaterBtn(%player);
	} else {
		%this.setObjetivoIncompleto(%player, %num);
		%this.decideDesligarBaterBtnPlayer(%player);
	}
}

function jogo::decideDesligarBaterBtnPlayer(%this, %player)
{
	if(%player.obj1Completo || %player.obj2Completo)
		return;
		
	%this.CTCdesligarBaterBtn(%player);	
}

function jogo::getObjetivoCompleto(%this, %player, %objetivo)
{
	if (%objetivo.grupo $= "0")
		return %this.getObjetivoLivreCompleto(%player, %objetivo);
	
	return %this.getObjetivoFechadoCompleto(%player, %objetivo);
}

//"Objetivo Livre" é um que não exige que você conquiste Grupos para bater
function jogo::getObjetivoLivreCompleto(%this, %player, %objetivo)
{
	if(%player.petroleos >= %objetivo.petroleos && %player.baias >= %objetivo.baias && %player.territorios >= %objetivo.territorios)
		return true;
		
	return false;
}

//"Objetivo Fechado" é um que exige que você conquiste pelo menos 1 Grupo para bater
function jogo::getObjetivoFechadoCompleto(%this, %player, %objetivo)
{
	%eval = "%playerGrupo = %player." @ %objetivo.grupo @ ";";
	eval(%eval);
	
	if(%playerGrupo == true && %player.minerios >= %objetivo.minerios && %player.petroleos >= %objetivo.petroleos && %player.uranios >= %objetivo.uranios && %player.baias >= %objetivo.baias && %player.territorios >= %objetivo.territorios)
		return true;
		
	return false;
}

function jogo::setObjetivoCompleto(%this, %player, %num)
{
	%eval = "%player.obj" @ %num @ "Completo = true;";
	eval(%eval);
}

function jogo::setObjetivoIncompleto(%this, %player, %num)
{
	%eval = "%player.obj" @ %num @ "Completo = false;";
	eval(%eval);
}

function jogo::CTCligarBaterBtn(%this, %player)
{
	if(%player == %this.jogadorDaVez)
		commandToClient(%player.client, 'ligarBaterBtn');		
}

function jogo::CTCdesligarBaterBtn(%this, %player)
{
	commandToClient(%player.client, 'desligarBaterBtn');		
}

function jogo::verificarObjetivosGlobal(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%player = %this.simPlayers.getObject(%i);
		%this.verificarObjetivosPlayer(%player);	
	}
}

function jogo::calcularCompletude(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%this.calcularCompletudePlayer(%player);
	}
}

function jogo::calcularCompletudePlayer(%this, %player)
{
	%this.setAreasCountPlayer(%player);
	%imperioPercent = %this.getImperioPercentPlayer(%player);
	%objetivo1Percent = %this.getObjetivoPercent(%player, %player.mySimObj.getObject(0));
	%objetivo2Percent = %this.getObjetivoPercent(%player, %player.mySimObj.getObject(1));
	
	%this.setNormalizedPercents(%player, %imperioPercent, %objetivo1Percent, %objetivo2Percent);	
}

function jogo::getImperioPercentPlayer(%this, %player)
{	
	%bases = %this.getBasesPlayer(%player);
	if(%this.emDuplas)
		return mFloor((%bases / 15) * 100);	
	
	return mFloor((%bases / 10) * 100);	
}

function jogo::getObjetivoPercent(%this, %player, %objetivo)
{
	if(%objetivo.grupo $= "0")
		return %this.getObjetivoLivrePercent(%player, %objetivo);
	
	return %this.getObjetivoFechadoPercent(%player, %objetivo);
}

//"Objetivo Livre" é um que não exige que você conquiste Grupos para bater
function jogo::getObjetivoLivrePercent(%this, %player, %objetivo)
{		
	if(%objetivo.petroleos > 0)
		return mFloor((%player.petroleos / %objetivo.petroleos) * 100);
	
	if(%objetivo.baias > 0)
		return mFloor((%player.baias / %objetivo.baias) * 100);	
	
	if(%objetivo.territorios > 0)
		return mFloor((%player.territorios / %objetivo.territorios) * 100);
}

//"Objetivo Fechado" é um que exige que você conquiste pelo menos 1 Grupo para bater
function jogo::getObjetivoFechadoPercent(%this, %player, %objetivo)
{
	%eval = "%playerGrupo = %player." @ %objetivo.grupo @ ";";
	eval(%eval);
		
	if (%playerGrupo == true)
		%objetivoPercent = 50;
		
	if(%objetivo.minerios > 0){
		%objetivoPercentT = mFloor((%player.minerios / %objetivo.minerios) * 50);
	} else if(%objetivo.petroleos > 0){
		%objetivoPercentT = mFloor((%player.petroleos / %objetivo.petroleos) * 50);
	} else if(%objetivo.uranios > 0){
		%objetivoPercentT = mFloor((%player.uranios / %objetivo.uranios) * 50);
	} else if(%objetivo.baias > 0){
		%objetivoPercentT = mFloor((%player.baias / %objetivo.baias) * 50);	
	}
	
	if(%objetivoPercentT > 50)
		%objetivoPercentT = 50;	
	
	%objetivoPercent += %objetivoPercentT;
	
	return %objetivoPercent;
}

function jogo::setNormalizedPercents(%this, %player, %imperioPercent, %objetivo1Percent, %objetivo2Percent)
{
	if(%imperioPercent > 100){
		%imperioPercent = 100;	
	}
	if(%objetivo1Percent > 100){
		%objetivo1Percent = 100;	
	}
	if(%objetivo2Percent > 100){
		%objetivo2Percent = 100;	
	}
			
	%player.imperioPercent = %imperioPercent;
	%player.objetivo1Percent = %objetivo1Percent;
	%player.objetivo2Percent = %objetivo2Percent;	
}