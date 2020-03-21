// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientAI_bot.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quinta-feira, 18 de junho de 2009 13:09
//
// Editor             :  Codeweaver v. 1.2.3430.42233
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function ai_bot::setGruposConquistados(%this)
{
	//limpa o simSet de Grupos Conquistados:
	%this.simGruposConquistados.clear();
	
	//pega o planeta:
	%planeta = clientGetPlaneta($planetaAtual);
	
	//verifica cada grupo:
	for(%i = 0; %i < %planeta.grupos.getCount(); %i++)
	{
		%grupo = %planeta.grupos.getObject(%i);
		%areasConquistadas = 0;
				
		for (%j = 0; %j < %grupo.numDeAreas; %j++)
		{
			%area = %grupo.simAreas.getObject(%j);
			if(%area.dono != 0)
				%areasConquistadas++;
		}
		
		if(%areasConquistadas == %grupo.simAreas.getCount())
			%this.simGruposConquistados.add(%grupo);
	}
}

function ai_bot::getGrupoDisponivel(%this, %grupo)
{
	if(%this.simGruposConquistados.isMember(%grupo))
		return false;
		
	return true;
}

function ai_bot::executarJogadaVelha(%num)
{
	switch(%num)
	{
		case 1:	ai_bot.executarJogada1();
	}
}

//jogada velha 1a: Nascido nos EUA, pegar australia:
function ai_bot::executarJogada1(%this)
{
	%this.criarTransporte();
	//move o barco pro oceano pacífico ocidental
	//move o barco pro oceano pacífico oriental
	//se não houver ninguém em bMarDeJava, move o barco pra bMarDeJava
		//senão, se não houver ninguém em bGrandeBaiaAustraliana, move o barco pra bGrandeBaiaAustraliana
	//desembarca um soldado em perth
	//desembarca um soldado em sydney
}

function ai_bot::criarTransporte(%this)
{
	//constrói as unidades necessárias (2 soldados e 1 navio):
	schedule(1000, 0, "clientAskSpawnUnit", %this.myMainBaseTerrestre.onde, "soldado", true); 
	schedule(1500, 0, "clientAskSpawnUnit", %this.myMainBaseTerrestre.onde, "soldado", true);
	schedule(2000, 0, "clientAskSpawnUnit", %this.myMainBaseMaritima.onde, "navio", true);
	
	//Embarca os soldados no navio:
	%soldado1 = %this.getSoldadoNaArea(%this.myMainBaseTerrestre.onde);
	schedule(3000, 0, "clientAskEmbarcar", %this.myMainBaseTerrestre.onde, %soldado1.pos, %this.myMainBaseMaritima.onde, "pos1", false); 
	%soldado2 = %this.getSoldadoNaArea(%this.myMainBaseTerrestre.onde);
	schedule(4000, 0, "clientAskEmbarcar", %this.myMainBaseTerrestre.onde, %soldado2.pos, %this.myMainBaseMaritima.onde, "pos1", false); 
}

function ai_bot::getSoldadoNaArea(%this, %area)
{
	if(%area.pos1Flag $= "soldado")
		return %area.pos1Quem;
		
	if(%area.pos2Flag $= "soldado")
		return %area.pos2Quem;
		
	if(%area.myPos3List.getCount > 0)
		return %area.myPos3list.getObject(0);
}