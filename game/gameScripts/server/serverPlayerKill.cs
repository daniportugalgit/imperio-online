// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverPlayerKill.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 30 de outubro de 2007 2:21
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function serverCmdRenderUnidade(%client, %area, %pos)
{
	%player = %client.player;
	%jogo = %player.jogo;
	%areaNoJogo = %jogo.getAreaFromClient(%area);
	%unit = %jogo.getPlayerUnitNaPosX(%areaNoJogo, %pos, %player);
		
	if(%player != %unit.dono)
		return;
		
	%unit.render();
	%jogo.CTCrenderUnidade(%area, %pos, %player);
	%jogo.verificarGruposGlobal();
	%jogo.verificarObjetivosGlobal();
}

function jogo::getAreaFromClient(%this, %areaNome)
{
	%eval = "%areaNoJogo = %this." SPC %areaNome @ ";";
	eval(%eval);	
	
	return %areaNoJogo;
}

function jogo::CTCrenderUnidade(%this, %areaNome, %pos, %player)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
		commandToClient(%this.simPlayers.getObject(%i).client, 'renderUnidade', %areaNome, %pos, %player.id);
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'renderUnidade', %areaNome, %pos, %player.id);
}

function jogo::getPlayerUnitNaPosX(%this, %area, %pos, %player)
{
	if(%pos $= "pos0" || %pos $= "pos1" || %pos $= "pos2")
	{
		%unit = %this.getUnitEmPosBasica(%area, %pos);
		return %unit;
	}
	
	%unit = %this.getUnitEmPosReserva(%area, %pos, %player);
	return %unit;
}

function jogo::getUnitEmPosBasica(%this, %area, %pos)
{
	if(%pos !$= "pos0" && %pos !$= "pos1" && %pos !$= "pos2")
		return;
		
	%eval = "%unit = %area." @ %pos @ "Quem;";
	eval(%eval);
	
	return %unit;
}

function jogo::getUnitEmPosReserva(%this, %area, %pos, %player)
{
	%simReserva = %this.getAreaSimReserva(%area, %pos);
	
	if(%area.dono !$= "MISTA" && %area.dono !$= "COMPARTILHADA")
		return %simReserva.getObject(0);
		
	for(%i = 0; %i < %simReserva.getCount(); %i++)
	{
		if(%simReserva.getObject(%i).dono == %player)
			return %simReserva.getObject(%i);
	}
}

function jogo::getAreaSimReserva(%this, %area, %pos)
{
	%eval = "%simReserva = %area.my" @ %pos @ "List;";
	eval(%eval);
	
	return %simReserva;
}

function serverPlayerKill(%quemMorreu, %quemMatou)
{
	%jogo = %quemMorreu.jogo;
	%jogo.killPlayer(%quemMorreu, %quemMatou);
}

function jogo::killPlayer(%this, %quemMorreu, %quemMatou)
{
	%quemMatou.qtsMatou += 1;
		
	%this.roubarGrana(%quemMatou, %quemMorreu);
	%this.roubarTodasAsCartas(%quemMatou, %quemMorreu);
	%this.matarPlayerSimExpl(%quemMorreu);
	%this.setPlayerMorto(%quemMorreu);
	%this.CTCatualizarGranaPorMorte(%quemMatou, %quemMorreu);				
	%this.CTCmsgPlayerMorreu(%quemMatou, %quemMorreu);
	%this.killAllPlayerUnits(%quemMorreu); //caso tenha sobrado alguma nos oceanos
	%this.CTCkillUnidadesRestantes(%quemMorreu);
	%this.verificarObjetivosGlobal();
	%this.verificarBatidaCompulsoria(%quemMatou);
	%this.verificarPassarAVez(%quemMorreu);	
}

function jogo::CTCkillUnidadesRestantes(%this, %player)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
		commandToClient(%this.simPlayers.getObject(%i).client, 'KillUnidadesRestantes', %player.id);
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'KillUnidadesRestantes', %player.id);
}

function jogo::roubarGrana(%this, %playerLadrao, %playerRoubado)
{
	%playerLadrao.imperiais += %playerRoubado.imperiais;	
	%playerLadrao.minerios += %playerRoubado.minerios;	
	%playerLadrao.petroleos += %playerRoubado.petroleos;	
	%playerLadrao.uranios += %playerRoubado.uranios;	
	
	%this.zerarGranaDoPlayer(%playerRoubado);
}

function jogo::zerarGranaDoPlayer(%this, %player)
{
	%player.imperiais = 0;	
	%player.minerios = 0;	
	%player.petroleos = 0;	
	%player.uranios = 0;			
}

function jogo::roubarTodasAsCartas(%this, %playerLadrao, %playerRoubado)
{
	%count = %playerRoubado.mySimInfo.getCount();
	
	for(%i=0; %i < %count; %i++)
	{
		%info = %playerRoubado.mySimInfo.getObject(0);
		%playerRoubado.mySimInfo.remove(%info); 
		%playerLadrao.mySimInfo.add(%info); 
		%infoNoJogo = %this.findInfo(%info.num);
				
		%infoNoJogo.compartilhada = false;
		for(%j = 0; %j < %this.playersAtivos; %j++)
		{
			%player = %this.simPlayers.getObject(%j);
			if(%player.mySimExpl.isMember(%info))
				%player.mySimExpl.remove(%info);
		}
						
		%this.CTCrouboDeInfo(%playerLadrao, %playerRoubado, %info);
	}
}

function jogo::CTCrouboDeInfo(%this, %playerLadrao, %playerRoubado, %info)
{
	commandToClient(%playerLadrao.client, 'roubarInfo', %info.num);
	commandToClient(%playerRoubado.client, 'perderInfo', %info.num);	
}

function jogo::matarPlayerSimExpl(%this, %player)
{
	for(%i=0; %i < %player.mySimExpl.getCount(); %i++){
		%info = %player.mySimExpl.getObject(0);
		%this.findInfo(%info.num);
		%infoNoJogo.compartilhada = false;
		%player.mySimExpl.remove(%info);
	}		
}

function jogo::setPlayerMorto(%this, %player)
{
	if(%player.taMorto)
		return;
	
	%player.taMorto = true;	
	%this.playersMortos += 1;
	%player.mySimInfo.clear();	
	%player.mySimExpl.clear();
	%player.mySimAreas.clear();
	%player.filantropiasEfetuadas = 0;
}

function jogo::CTCatualizarGranaPorMorte(%this, %quemMatou, %quemMorreu)
{
	commandToClient(%quemMatou.client, 'atualizarGrana', %quemMatou.imperiais, %quemMatou.minerios, %quemMatou.petroleos, %quemMatou.uranios);
	commandToClient(%quemMorreu.client, 'atualizarGrana', 0, 0, 0, 0);	
}

function jogo::CTCmsgPlayerMorreu(%this, %quemMatou, %quemMorreu)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		if(%this.simPlayers.getObject(%i) != %quemMatou && %this.simPlayers.getObject(%i) != %quemMorreu)
			commandToClient(%this.simPlayers.getObject(%i).client, 'clientMsgPlayerMorreu', %quemMorreu.persona.nome, %quemMatou.persona.nome);
		
		if(%this.observadorOn)
			commandToClient(%this.observador, 'clientMsgPlayerMorreu', %quemMorreu.persona.nome, %quemMatou.persona.nome);
	}
	
	commandToClient(%quemMatou.client, 'clientMsgMatouPlayer', %quemMorreu.persona.nome);
	commandToClient(%quemMorreu.client, 'clientMsgMorri', %quemMatou.persona.nome);
}

function jogo::verificarBatidaCompulsoria(%this, %quemMatou)
{
	if(%this.emDuplas)
	{
		%this.verificarBatidaCompulsoriaEmDuplas(%quemMatou);
		return;
	}
	
	%this.verificarBatidaCompulsoriaNormal(%quemMatou);
}

function jogo::verificarBatidaCompulsoriaEmDuplas(%this, %quemMatou)
{
	if(%this.playersMortos <= %this.playersAtivos - 3)
		return;

	%quemMatou.assassino = true;
	
	if(%quemMatou.myDupla.taMorto == false)
	{
		schedule(3000, 0, "serverCmdBater", %quemMatou.client, "Ninguém", true);
		return;
	}
	
	if(%this.playersMortos <= %this.playersAtivos - 2)
		return;
		
	schedule(3000, 0, "serverCmdBater", %quemMatou.client, "Ninguém", true); 
}

function jogo::verificarBatidaCompulsoriaNormal(%this, %quemMatou)
{
	if(%this.playersMortos <= %this.playersAtivos - 2)
		return;
	
	if(%quemMatou == %this.aiPlayer)
	{
		%firstClientOnline = %this.getFirstClientOnline();
		schedule(3000, 0, "serverCmdBater", %firstClientOnline, "Ninguém");
		return;
	}
		
	%quemMatou.assassino = true;
	schedule(3000, 0, "serverCmdBater", %quemMatou.client, "Ninguém", true);
}
///////
function jogo::verificarBatidaCompulsoriaPorSuicidio(%this)
{
	if(%this.emDuplas)
	{
		%this.verificarBatidaCompulsoriaEmDuplasPorSuicidio();
		return;
	}
	
	%this.verificarBatidaCompulsoriaNormalPorSuicidio();
}

function jogo::verificarBatidaCompulsoriaEmDuplasPorSuicidio(%this)
{
	if(%this.playersMortos <= %this.playersAtivos - 3)
		return;
	
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		if(!%player.taMorto && !%player.myDupla.taMorto)
		{
			schedule(3000, 0, "serverCmdBater", %player.client, "Ninguém");
			return;
		}
	}
	
	if(%this.playersMortos <= %this.playersAtivos - 2)
		return;
	
	%primeiroPlayerVivo = %this.getPrimeiroPlayerVivo();
	schedule(3000, 0, "serverCmdBater", %primeiroPlayerVivo.client, "Ninguém"); 
}

function jogo::verificarBatidaCompulsoriaNormalPorSuicidio(%this)
{
	if(%this.playersMortos <= %this.playersAtivos - 2)
		return;
		
	if(%this.poker)
	{
		schedule(3000, 0, "serverPokerFimSolitario", %this);
		return;	
	}
	
	%firstClientOnline = %this.getFirstClientOnline();
	schedule(3000, 0, "serverCmdBater", %firstClientOnline, "Ninguém");
}
/////


function jogo::getFirstClientOnline(%this)
{
	if(isObject(%this.player1.client))
		return %this.player1.client;
		
	if(isObject(%this.player2.client))
		return %this.player2.client;
	
	if(isObject(%this.player3.client))
		return %this.player3.client;
	
	if(isObject(%this.player4.client))
		return %this.player4.client;
	
	if(isObject(%this.player5.client))
		return %this.player5.client;
		
	if(isObject(%this.player6.client))
		return %this.player6.client;		
}



function jogo::verificarPassarAVez(%this, %quemMorreu)
{
	if(%this.terminado)
		return;
		
	if(%this.poker)
		return;
	
	if(!%this.partidaIniciada){
		echo("Jogador morreu antes do início da partida! ANULANDO JOGO " @ %this.num);
		%this.anularEsteJogo();
		return;
	}	
		
	if(%this.jogadorDaVez != %quemMorreu)
		return;
		
	%this.setJogadorDaVezParaTodos();
	%this.inicializarTurno();
}

function jogo::verificarPassarAVezPorMorteNoPoker(%this, %quemMorreu)
{
	if(%this.terminado)
		return;
		
	if(!%this.poker)
		return;
		
	%quemMorreu.pk_fugiu = true;
	%this.pk_jogo.CTCplayerFugiu(%quemMorreu);
	
	if(%this.jogadorDaVez != %quemMorreu)
		return;
	
	if(%this.inPoker)
	{
		if(%quemMorreu == %this.ultimoPlayerVivo)
		{
			%this.finalizarPkTurno(true);
			return;
		}
		if(%quemMorreu == %this.primeiroPlayerVivo)
		{
			%this.needPokerUpdate = true;
			%this.finalizarPkTurno();
			return;	
		}
				
		%this.finalizarPkTurno();
		return;
	}
	
	%this.passarAVezPorStatus(%this.getPlayerStartStatus(%quemMorreu));
	
	/*
	if(%quemMorreu == %this.ultimoPlayerVivo)
		%this.setJogadorDaVezParaTodos(true);	
	else
		%this.setJogadorDaVezParaTodos();	//aki tem que entrar a verificação de se o jogo já começou, se está na escolha de objetivos, cores, etc
	
	%this.inicializarTurno();
	*/
}

function jogo::playerSuicide(%this, %player)
{
	if(%player.taMorto)
		return;
	
	%this.matarPlayerSimExpl(%player);
	%this.setUltimoPlayerVivo();
	%this.setPrimeiroPlayerVivo();
	%this.setPlayerMorto(%player);
	%this.zerarGranaDoPlayer(%player);
	%this.CTCzerarGrana(%player);				
	%this.CTCmsgPlayerSuicidou(%player.persona.nome);
	%this.setSuicideTaxoMark();
	%this.verificarObjetivosGlobal();
	%this.verificarBatidaCompulsoriaPorSuicidio();
	if(%this.poker)
	{
		%this.verificarPassarAVezPorMorteNoPoker(%player);
		return;
	}
	%this.verificarPassarAVez(%player);
}

function jogo::setUltimoPlayerVivo(%this)
{
	%this.ultimoPlayerVivo = %this.getUltimoPlayerVivo();	
}

function jogo::setPrimeiroPlayerVivo(%this)
{
	%this.primeiroPlayerVivo = %this.getPrimeiroPlayerVivo();	
}

function jogo::CTCmsgPlayerSuicidou(%this, %nomeDoMorto)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
		commandToClient(%this.simPlayers.getObject(%i).client, 'clientMsgPlayerSuicidou', %nomeDoMorto);
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'clientMsgPlayerSuicidou', %nomeDoMorto);
}

function jogo::CTCzerarGrana(%this, %player)
{
	commandToClient(%player.pesona.client, 'atualizarGrana', 0, 0, 0, 0);	
}

function jogo::setSuicideTaxoMark(%this, %player)
{
	if(%player.persona.fechouTorque)
	{
		%player.suicidouSe = "s"; //se suicidou
		return;
	}
	
	%player.suicidouSe = "r"; //se rendeu	
}


function serverRenderTodos(%player, %naSuaDaVez){
	%jogo = %player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	
	%jogo.killAllBasesDoPlayer(%player);
	%jogo.killAllPlayerUnits(%player);
		
	if(%player.mySimUnits.getCount() > 0)
	{
		schedule(300, 0, "serverRenderTodos", %player); //se existirem, chama novamente a função até que todas tenham sido rendidas;
		return;
	} 
		
	if(%player.mySimAreas.getCount < 1 && %player.taMorto == false) 
		%jogo.playerSuicide(%player); //se o cara tinha bases, pode não ter dado o playerSuicide
		
	%jogo.CTCrenderTodos(%player);
	
	if(!%naSuaDaVez == true || %jogo.partidaEncerrada)
		return;
		
	
	//%jogo.setJogadorDaVezParaTodos();
	//%jogo.inicializarTurno();
}

function jogo::killAllPlayerUnits(%this, %player)
{
	for(%i = 0; %i < %player.mySimUnits.getCount(); %i++)
	{
		%unit = %player.mySimUnits.getObject(0);
		%unit.render();
	}	
}

function jogo::killBaseNaArea(%this, %area)
{
	if(!%area.pos0Flag)
		return;
	
	%area.pos0Quem.safeDelete(); //deleta a peça
	%area.pos0Flag = false; //marca na área que ela não tem mais base
	%area.pos0Quem = 0; //marca que a base não é mais o <quem> da pos0 da área
	%area.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
}

function jogo::killAllBasesDoPlayer(%this, %player)
{
	%basesCount = %player.mySimBases.getCount();
	if(%basesCount <= 0)
		return;
	
	for(%j = 0; %j < %basesCount; %j++)
	{ 
		%base = %player.mySimBases.getObject(0); //o índice é sempre 0 pq as áreas são removidas conforme as bases são destruídas.
		%this.killBaseNaArea(%base.onde);
	}	
}

function jogo::CTCrenderTodos(%this, %quemMorreu)
{
	for(%h = 0; %h < %this.playersAtivos; %h++)
		commandToClient(%this.simPlayers.getObject(%h).client, 'renderTodos', %quemMorreu.id);
		
	if(%this.observadorOn)
		commandToClient(%this.observador, 'renderTodos', %quemMorreu.id);
}

//agora quando o client pede:
function serverCmdRenderTodos(%client){
	%jogo = %client.persona.jogo;
	%player = %client.persona.player;
		
	if(%player == %jogo.jogadorDaVez)
	{
		serverRenderTodos(%player, true);	
		return;
	}
	
	serverRenderTodos(%player);
}

//onDrop:
function serverKillPlayer(%persona){
	%jogo = %persona.jogo;
	%nome = %persona.nome;
	%player = %persona.player;
	%player.pk_fugiu = true;
	echo("EXECUTANDO serverKillPlayer(" @ %nome @ "), JOGO " @ %jogo.num @ ";");
	if(%jogo.jogadorDaVez == %persona.player){
		serverRenderTodos(%player, true);
	} else {
		serverRenderTodos(%player, false);
	}
}

function serverMatarPersona(%persona){
	serverRemoverPersonaDoAtrio(%persona);
	serverMarcarPersonaOffline(%persona);
	serverVerificarTirarPersonaDaSala(%persona);
		
	echo("Persona > " @ %persona.nome @ " < marcada como Offline.");
}

function serverRemoverPersonaDoAtrio(%persona)
{
	if(!$personasNoAtrio.isMember(%persona))
		return;
	
	$personasNoAtrio.remove(%persona);
	serverAtualizarPersonasNoAtrioParaTodos();
}

function serverMarcarPersonaOffline(%persona)
{
	%persona.offLine = true;
	
	if(!$persona.inGame)
		return;
	
	%jogo = %persona.jogo;
	%jogo.ChatGroup.remove(%client);
	removeChatClient(%jogo, %client.persona.nome);	
	%persona.inGame = false;
}

function serverVerificarTirarPersonaDaSala(%persona)
{
	if(!isObject(%persona))
		return;
		
	if(%persona.sala $= "no")
		return;
	
	if(%persona.jogo.firstStart && !%persona.jogo.partidaIniciada && !%persona.jogo.poker)
		%persona.jogo.anularEsteJogo(); //anula o jogo caso ele ainda não tenha começado
	
	if(%persona.jogo.partidaIniciada && !%persona.jogo.partidaEncerrada)
	{
		echo("-> NÃO remover Persona " @ %persona.nome @ " da sala, pois a partida está em andamento.");
		serverTirarDaSala(%persona); //tira a persona da sala, mas não manda nada pro taxo (persona saiu no meio do jogo);
		return;
	}
		
	echo("-> Remover Persona" @ %persona.nome @ " da sala, pois não está em jogo");
	serverTirarDaSalaComTaxo(%persona); //tira a persona da sala;	
}

//re-declarando uma função do common/server/clientConnection.cs:
function GameConnection::onDrop(%client, %reason){
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	%persona.fechouTorque = true;
	%persona.player.suicidouSe = "s"; //se suicidou
	
	echo("Client Dropped: " @ %client @ ", JOGO(" @ %jogo.num @ "), PERSONA " @ %persona.nome);
	$Server::PlayerCount--;
		
	if(!isObject(%persona))
		return;
		
	serverRemoverPersonaDoJogo(%persona, %jogo);
	serverMatarPersona(%persona);
}

function serverRemoverPersonaDoJogo(%persona, %jogo)
{
	if(!isObject(%persona))
		return;
		
	if(!isObject(%jogo))
		return;
		
	if(%jogo.partidaEncerrada)
		return;
	
	if(!%jogo.partidaIniciada && !%jogo.poker) //isso não adianta, pq parte está na serverPlayerKill, a saber, verificar se precisa mudar de turno.
		return;
		
	serverKillPlayer(%persona);
	
	if(%jogo.guloksDespertaram)
		schedule(6000, 0, "serverJogoElegerAiManager", %jogo);
}




function serverPlayerMorteNatural(%quemMorreu){
	%jogo = %quemMorreu.jogo;
	%quemMorreu.taMorto = true;	//pular este jogador nas rodadas futuras:
	%jogo.playersMortos += 1; //marca que tem um a menos
	
	%quemMorreu.imperiais = 0;	
	%quemMorreu.minerios = 0;	
	%quemMorreu.petroleos = 0;	
	%quemMorreu.uranios = 0;	
	commandToClient(%quemMorreu.client, 'atualizarGrana', 0, 0, 0, 0);
	
	//clientMsgs:
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		commandToClient(%jogo.simPlayers.getObject(%i).client, 'clientMsgPlayerMorteNatural', %quemMorreu.persona.nome);
	}
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'clientMsgPlayerMorteNatural', %quemMorreu.persona.nome);
	}
	//para finalizar, verifica se sobrou só um participante no jogo. Em caso positivo, fazer a batida compulsória:
	if(%jogo.playersMortos > %jogo.playersAtivos - 2){ //$playersAtivos será sempre no mínimo 2; Se $playersMortos for igual a 1, num jogo de dois acaba a partida. Num de três, não. 
		//verifica quem restou vivo:
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			%player = %jogo.simPlayers.getObject(%i);
					
			if(%player.taMorto == false){
				%quemSobreviveu = %player;	
			}
		}
		schedule(3000, 0, "serverCmdBater", %quemSobreviveu.client, "Ninguém");
	}
	
	//se não cabaou o jogo, verifica se tem que passar a vez:
	if(%jogo.jogadorDaVez == %quemMorreu){
		echo("Estava na vez de quem se matou! Passando a vez...feito!");
		%jogo.setJogadorDaVezParaTodos();
		%jogo.inicializarTurno();
	}
}

