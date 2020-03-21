// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverCreditos.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 30 de novembro de 2007 21:47
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
$bonusHorarioNobre = 2;

function jogo::calcularCreditosGlobal(%this, %quemBateu)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%this.calcularCreditosPlayer(%player, %quemBateu);
		%this.echoCreditos(%player);
	}
	
	%this.verificarAchievements(%quemBateu);
	%this.setBonusHorarioNobre();
	
	%this.setCreditosDistribuidos();	
}

function jogo::calcularCreditosPlayer(%this, %player, %quemBateu)
{
	%this.zerarCreditosPlayer(%player);
	
	if(%player.taMorto)
		return;
		
	if(%this.poker)
		return;
	
	%this.setCreditosBatida(%player, %quemBateu);
	%this.setCreditosVitoria(%player);
	%this.setCreditosVisionario(%player);
	%this.setCreditosArrebatador(%player);
	%this.setCreditosComerciante(%player);
	%this.setCreditosDiplomata(%player);	
}

function jogo::zerarCreditosPlayer(%this, %player)
{
	%player.creditosAgora = 0;
	%player.cBateu = 0;
	%player.cVencedor = 0;
	%player.cVisionario = 0;
	%player.cArrebatador = 0;
	%player.cComerciante = 0;
	%player.cDiplomata = 0;
}

function jogo::getBonusDiplomatico(%this)
{
	if (%this.playersAtivos > 4)
		return 3;
	
	return 2;
}

function jogo::getImperiosGlobal(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		if(%player.imperio)
			%imperiosFeitos++;	
	}
	return %imperiosFeitos;
}

function jogo::setPenalidadeSuicidio(%this, %player)
{
	%player.creditosAgora = -1;	
}

function jogo::setCreditosBatida(%this, %player, %quemBateuNome)
{
	if(%player.persona.nome !$= %quemBateuNome)
		return;
	
	%player.creditosAgora += 1;
	%player.cBateu = 1;
}

function jogo::setCreditosVitoria(%this, %player)
{
	if(!%player.ganhouOJogo)
		return;
		
	
	if(%this.handicap){
		%this.setCreditosVitoriaComHandicap(%player);
		return;
	}
			
	%this.setCreditosVitoriaNormal(%player);
}

function jogo::setCreditosVitoriaComHandicap(%this, %player)
{
	%ordemDeJogada = %this.getPlayerOrderIndex(%player);
	if(%ordemDeJogada == 0 || %this.rodada > 9)
	{
		%player.creditosAgora += 1;
		%player.cVencedor = 1;
		return;
	}
	
	switch (%ordemDeJogada)
	{
		case 1:
			%player.creditosAgora += 2;
			%player.cVencedor = 2;
			
		case 2:
			%player.creditosAgora += 3;
			%player.cVencedor = 3;
			
		case 3:
			%player.creditosAgora += 4;
			%player.cVencedor = 4;
			
		case 4:
			%player.creditosAgora += 5;
			%player.cVencedor = 5;
	}
}

function jogo::getPlayerOrderIndex(%this, %player)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%tempPlayer = %this.simPlayers.getObject(%i);	
		if(%tempPlayer == %player)
			return %i;
	}
}

function jogo::setCreditosVitoriaNormal(%this, %player)
{
	%player.creditosAgora += 3;
	%player.cVencedor = 3;		
}

function jogo::setCreditosVisionario(%this, %player)
{
	if(!%player.imperio)
		return;
	
	%imperiosFeitos = %this.getImperiosGlobal();
	%modImp = %imperiosFeitos - 1;
	
	if(%modImp > 2)
		%modImp = 2;	
	
	%player.creditosAgora += 3 - %modImp;
	%player.cVisionario = 3 - %modImp;	
}

function jogo::setCreditosArrebatador(%this, %player)
{
	if(!(%player.obj1Completo && %player.obj2Completo))
		return;
		
	%player.arrebatadorAgora = 1;
	%player.creditosAgora += 3; 
	%player.cArrebatador = 3;
}

function jogo::setCreditosComerciante(%this, %player)
{
	%this.setComercianteAgora(%player);
	if(%player.comercianteAgora)
	{
		%player.creditosAgora += 2;
		%player.cComerciante = 2;	
	}
}

function jogo::setComercianteAgora(%this, %player)
{
	if(%player.decretouMoratoria > 0)
	{
		%player.comercianteAgora = 0;
		return;	
	}
	
	if(%player.negInGame <= 0 && %player.mySimExpl.getCount() <= 0)
	{
		%player.comercianteAgora = 0;
		return;
	}
	
	%player.comercianteAgora = 1;
}

function jogo::setComercianteAgoraForAll(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.GetObject(%i);
		%this.setComercianteAgora(%player);
	}
}

function jogo::setCreditosDiplomata(%this, %player)
{
	if(%player.atacou > 0)
		return;
		
	if(%player.ganhouOJogo)
	{
		%this.setCreditosDiplomataVitoria(%player);
		return;
	}
	
	%this.setCreditosDiplomataNormal(%player);
}

function jogo::setCreditosDiplomataVitoria(%this, %player)
{
	%bonusDiplomatico = %this.getBonusDiplomatico();
	%player.creditosAgora = %player.creditosAgora * %bonusDiplomatico;
	%player.cDiplomata = %bonusDiplomatico;
	%player.vitoriaDiplomatica = true;	
}

function jogo::setCreditosDiplomataNormal(%this, %player)
{
	%player.creditosAgora += 2;
	%player.cDiplomata = 2; 	
}

function jogo::setCreditosDistribuidos(%this)
{
	%this.creditosDistribuidos = 0;
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.GetObject(%i);
		%this.creditosDistribuidos += %player.creditosAgora;
	}	
}

function jogo::verificarAchievements(%this, %quemBateu)
{
	if(%this.poker)
		return;
	
	if(%this.playersAtivos < 3)
		return;	
	
	if(%this.rodada > 9)
	{
		%this.setCreditosPartidaLenta();
		return;
	}
	
	%this.setCreditosQuebraDeSaque();
	
	if(%this.getAssassino())
	{
		%this.setCreditosAssassino();
		return;
	}
	
	%this.setCreditosPartidaPerfeita();
}

function jogo::setCreditosPartidaLenta(%this)
{
	echo("PARTIDA LENTA!");
	%this.partidaLenta = true;
	%mod = %this.getModPartidaLenta();
	
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%player.creditosAgora -= %mod;
		if(%player.creditosAgora < 0)
			%player.creditosAgora = 0;	
	}
}

function jogo::getModPartidaLenta(%this)
{
	%mod = %this.rodada - 9;
	%mod *= 15; //cada rodada a partir da décima subtrai 15 créditos de todos os participantes
	if(%mod < 0)
		%mod = 0;	
	
	return %mod;
}

function jogo::setCreditosQuebraDeSaque(%this)
{
	%ultimoPlayer = %this.getUltimoPlayer();
	
	if(!%ultimoPlayer.ganhouOJogo)
		return;
	
	echo("QUEBRA DE SAQUE --> " @ %ultimoPlayer.persona.nome);
	%ultimoPlayer.creditosAgora += 3;	
}

function jogo::getAssassino(%this)
{
	for(%j = 0; %j < %this.playersAtivos; %j++)
	{
		%player = %this.simPlayers.getObject(%j);
		if(%player.assassino)
			return true;
	}	
	
	return false;
}

function jogo::setCreditosAssassino(%this)
{
	for(%j = 0; %j < %this.playersAtivos; %j++)
	{
		%player = %this.simPlayers.getObject(%j);
		if(%player.assassino)
		{
			echo("ASSASSINO --> " @ %player.persona.nome);
			%player.creditosAgora += 3;
		}
	}	
}

function jogo::setCreditosPartidaPerfeita(%this)
{
	echo(">>>Verificando Partida Perfeita...<<<");
	if(%this.emDuplas || %this.handicap)
	{
		echo("Jogo em Duplas ou com Handicap, partida imperfeita.");
		return;
	}
		
	if(!%this.getQuemBateuVenceu())
	{
		echo("Quem venceu não bateu, partida imperfeita.");
		return;
	}
	
	if(%this.vencedores.getCount() != 1)
	{
		echo("Mais de um vencedor, partida imperfeita.");
		return;
	}
	
	if(%this.getImperiosGlobal() != 1)
	{
		echo("Mais de um Império construído, partida imperfeita.");
		return;
	}
	
	%vencedor = %this.vencedores.getObject(0);
	
	if(!%vencedor.imperio || %vencedor.cArrebatador <= 0 || !%vencedor.comercianteAgora || !%vencedor.vitoriaDiplomatica)
	{
		echo("Vencedor não fez Império, ou não fez Arrebatador, ou não fez Comerciante, ou não foi Diplomata: partida imperfeita.");
		return;	
	}
	
	
	echo("PARTIDA PERFEITA!: " @ %vencedor.persona.nome);
	%vencedor.creditosAgora += 5;
	%vencedor.partidaPerfeita = true;
}

function jogo::setSimVencedores(%this)
{
	%this.vencedores = new SimSet();
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		if(%player.ganhouOJogo)
			%this.vencedores.add(%player);
	}
}

function jogo::getQuemBateuVenceu(%this)
{
	for(%i = 0; %i < %this.vencedores.getCount(); %i++)
	{
		%player = %this.vencedores.getObject(%i);
		if(%player.cBateu)
			return true;
	}
	
	return false;
}

function jogo::setBonusHorarioNobre(%this)
{
	if(!%this.horarioNobre)
	{
		trace("!-> Jogo fora do horário nobre: não há bônus.");
		return;
	}
	
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%player.creditosAgora += $bonusHorarioNobre;
	}
	
	%this.CTCbonusHorarioNobre();
	echo("!-> JOGO " @ %this.num @ " FOI INICIADO EM HORÁRIO NOBRE: +" @ $bonusHorarioNobre @ "CRÉDITOS PARA CADA PARTICIPANTE!");
}

function jogo::CTCbonusHorarioNobre(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
		commandToClient(%this.simPlayers.getObject(%i).client, 'bonusHorarioNobre');
}

function jogo::echoCreditos(%this, %player){
	echo("JOGO(" @ %this.num @ "), " @ %player.id @ " (" @ %player.persona.nome @ ") CRÉDITOS:--------------------");
			
	echo("Batida = " @ %player.cBateu);
	echo("Vitória = " @ %player.ganhouOJogo @ " -> " @ %player.cVencedor);
	echo("Visionário = " @ %player.imperio @ " -> " @ %player.cVisionario);
	echo("Arrebatador = " @ %player.arrebatadorAgora @ " -> " @ %player.cArrebatador);
	echo("Comerciante = " @ mFloor(%player.comercianteAgora) @ " -> " @ %player.cComerciante);
	
	if(%player.vitoriaDiplomatica){
		echo("Diplomata = x" @ %player.cDiplomata);
	} else {
		echo("Diplomata = " @ %player.cDiplomata);
	}
		
	echo("TOTAL DE CRÉDITOS (" @ %player.persona.nome @ ") = " @ %player.creditosAgora @ " ------------------------");
}