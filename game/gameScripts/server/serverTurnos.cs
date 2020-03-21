// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverTurnos.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quinta-feira, 27 de dezembro de 2007 10:51
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
//talvez passar como parâmetro nesta função se ela foi decorrente de morte ou de escolha do player.
//se foi de morte, pegar quem morreu e ver se era o últimoPlayerVivo, em vez de simplesmente pegar o últimoPlayerVivo.
function jogo::setJogadorDaVezParaTodos(%this, %ultimoPlayerMorreu)
{
	if(isObject(%this.pk_jogo))
		%this.pk_jogo.setPk_rodadaCompleta(false);
	
	%ultimoPlayerVivo = %this.getUltimoPlayerVivo();
	%playerQueAcabouDePassarAVez = %this.jogadorDaVez;
	
	%proximoPlayer = %this.getProximoPlayer();
	%this.jogadorDaVez = %proximoPlayer;
		
	if((%ultimoPlayerVivo == %playerQueAcabouDePassarAVez || %ultimoPlayerMorreu) && !%this.inPoker)
		%this.incrementarRodada();	
	
	if(isObject(%this.pk_jogo))
	{
		if(%this.pk_jogo.getFullCircle() && %this.inPoker)
			%this.pk_jogo.setPk_rodadaCompleta(true);
	}
		
	%this.CTCsetJogadorDaVezParaTodos();
	
	if(%this.poker && %this.rodada == 3)
		%this.pk_jogo.verificarRiver();
}



function jogo::CTCsetJogadorDaVezParaTodos(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		commandToClient(%this.simPlayers.getObject(%i).client, 'setJogadorDaVez', %this.jogadorDaVez.id, %this.rodada);
	}
	
	if(%this.observadorOn)
	{
		commandToClient(%this.observador, 'setJogadorDaVez', %this.jogadorDaVez.id, %this.rodada);
	}	
}

function jogo::getProximoPlayer(%this)
{
	%jogadorDaVez = %this.jogadorDaVez;
	
	for(%i = 1; %i <= %this.playersAtivos; %i++)
	{
		%proximoIndex[%i] = %jogadorDaVez.num + %i - 1;
		if(%proximoIndex[%i] >= %this.playersAtivos)
			%proximoIndex[%i] -= %this.playersAtivos;
	}
	
	for(%i = 1; %i <= %this.playersAtivos; %i++)
	{
		if(!%this.simPlayers.getObject(%proximoIndex[%i]).taMorto)
		{
			%proximoPlayer = %this.simPlayers.getObject(%proximoIndex[%i]);
			return %proximoPlayer;
		}
	}
}

function jogo::getPrimeiroPlayerVivo(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{	
		%player = %this.simPlayers.getObject(%i);
		if(!%player.taMorto)
			return %player;	
	}
}

function jogo::getUltimoPlayerVivo(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{	
		%player = %this.simPlayers.getObject(%i);
		if(!%player.taMorto)
			%ultimoVivo = %player;	
	}
	
	return %ultimoVivo;
}


function jogo::incrementarRodada(%this)
{
	if(%this.partidaIniciada)
	{
		%this.primeiraRodada = false;
		%this.rodada++;
		echo("JOGO(" @ %this.num @ "): " @ %this.rodada @ "ª Rodada Iniciada");	
	}
	
	%this.checkPokerTurn();
	
	%this.checkGmBot();
	
	if(%this.estourouLimiteDeRodadas())
		%this.batidaCompulsoria();
}

function jogo::checkPokerTurn(%this)
{
	if(!isObject(%this.pk_jogo))
		return;
		
	if(%this.rodada != 2)
		return;
	
	if(%this.simPlayers.getObject(%this.playersAtivos-1).pk_simCartas.getCount() >= 4)
		return;
		
	%this.CTCvoltarDoJogoProPoker();
	%this.pk_jogo.verificarFlop();
}

function jogo::CTCvoltarDoJogoProPoker(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
		commandToClient(%this.simPlayers.getObject(%i).client, 'voltarDoJogoProPoker');
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'voltarDoJogoProPoker');
}

function jogo::estourouLimiteDeRodadas(%this)
{
	if(%this.rodada > 10)
		return true;
			
	return false;
}

function jogo::batidaCompulsoria(%this)
{
	serverCmdBater(%this.getPrimeiroPlayerVivo().client, "Ninguém");
}

function jogo::checkGmBot(%this)
{
	if(%this.rodada < 10)
		return;
		
	if(%this.playersAtivos < 3)
		return;
		
	if($salaEmQueEstouTipoDeJogo $= "emDuplas" && %this.playersAtivos == 4)
		return;
	
	if($salaEmQueEstouTipoDeJogo $= "poker")
		return;
		
	serverGmBot(%this, 1); //msg de jogos demorados
}

function jogo::inicializarTurno(%this)
{
	%this.checkDesastres();
		
	serverClearUndo(%this.jogadorDaVez);
	
	%this.finalizarPendencias(); //reseta jetPacks, finaliza reciclagens, eclode ovos...
		
	%this.verificarMissoesDaVez();
	
	serverVerificarGruposX(%this.jogadorDaVez);
	
	%this.setImperioDaVez();
			
	%this.setMovimentosDaVez();
	
	%this.setImperiaisDaVez();
	
	%this.setRecursosDaVez();
	
	%this.CTCinicializarTurno();
			
	serverVerificarObjetivos(%this.jogadorDaVez);
		
	if(%this.jogadorDaVez == %this.aiPlayer)
		%this.askAIdirections();
}

function jogo::checkDesastres(%this)
{
	if(%this.primeiraRodada)
		return;
	
	%this.sortearDesastre();
	
	if(%this.disparosOrbitais > 0)
		%this.sortearMegaDesastre();	
}

//devolve o jetPack pra todos os líderes do jogador da vez:
function jogo::resetJetPacks(%this)
{
	for(%i = 0; %i < %this.jogadorDaVez.mySimLideres.getCount(); %i++)
	{
		%lider = %this.jogadorDaVez.mySimLideres.getObject(%i);
		%lider.JPagora = %lider.JPBP;
	}	
}

function jogo::finalizarPendencias(%this)
{	
	%this.resetJetPacks();
	%this.verificarReciclagens();
	%this.verificarOvos();
}

function jogo::verificarMissoesDaVez(%this)
{
	for (%i = 0; %i < %this.jogadorDaVez.mySimInfo.getCount(); %i++)
	{
		%missao = %this.jogadorDaVez.mySimInfo.getObject(%i);
		%this.verificarMissao(%missao);
	}	
}

function jogo::verificarMissao(%this, %missao)
{
	if(%missao.tipo !$= "recurso")
		return;
		
	%area = %this.getAreaDaMissao(%missao);
	%missaoNoJogo = %this.getMissaoNoJogo(%missao);
	%jogadorDaVez = %this.jogadorDaVez;
	
	%this.setStatusDaMissaoNoJogo(%missao);
	
	if(%missaoNoJogo.ativaFlag)
		%this.extrairRecursoDaMissao(%missao);
}

function jogo::getAreaDaMissao(%this, %missao)
{
	%eval = "%areaNoJogo = %this." @ %missao.Area @ ";";
	eval(%eval);
	return %areaNoJogo;
}

function jogo::getMissaoNoJogo(%this, %missao)
{
	%eval = "%missaoNoJogo = %this.info" @ %missao.num @ ";";
	eval(%eval);
	return %missaoNoJogo;
}

function jogo::setStatusDaMissaoNoJogo(%this, %missao)
{
	if (%this.jogadorDaVez.mySimAreas.isMember(%this.getAreaDaMissao(%missao)))
	{
		%this.getMissaoNoJogo(%missao).ativaFlag = true;
		return;
	}
	
	%this.getMissaoNoJogo(%missao).ativaFlag = false;
}

function jogo::extrairRecursoDaMissao(%this, %missao)
{
	%missaoNoJogo = %this.getMissaoNoJogo(%missao);
	
	if(!%missaoNoJogo.compartilhada)
	{
		%this.jogadorDaVez.minerios += %missao.bonusM;
		%this.jogadorDaVez.petroleos += %missao.bonusP;
		%this.jogadorDaVez.uranios += %missao.bonusU;	
		return;
	}
			
	%this.dividirRecursoDaMissao(%missao);
}

function jogo::dividirRecursoDaMissao(%this, %missao)
{
	%missaoNoJogo = %this.getMissaoNoJogo(%missao);
	
	if(%missaoNoJogo.vezDeQuemDeu){
		%missaoNoJogo.quemDeu.minerios += %missao.bonusM;
		%missaoNoJogo.quemDeu.petroleos += %missao.bonusP;
		%missaoNoJogo.quemDeu.uranios += %missao.bonusU;
		commandToClient(%missaoNoJogo.quemDeu.client, 'receberPagamentoExpl', %missao.bonusM, %missao.bonusP, %missao.bonusU);
	} else {
		%missaoNoJogo.quemExplora.minerios += %missao.bonusM;
		%missaoNoJogo.quemExplora.petroleos += %missao.bonusP;
		%missaoNoJogo.quemExplora.uranios += %missao.bonusU;
	}
	serverToggleVezDeGanhar(%missao, %this);
}

function jogo::setImperioDaVez(%this)
{
	%this.setImperioPlayer(%this.jogadorDaVez);
}

function jogo::setMovimentosDaVez(%this)
{
	if(%this.jogadorDaVez.imperio)
	{
		%this.jogadorDaVez.movimentos = 10;
		return;
	}
			
	%this.jogadorDaVez.movimentos = 5 + %this.jogadorDaVez.persona.patente.movBonus;
	if(%this.jogadorDaVez.velocista)
		%this.jogadorDaVez.movimentos += 1;
}


function jogo::setImperiaisDaVez(%this)
{
	if(%this.primeiraRodada)
	{
		%this.setImperiaisDaVezPrimeiraRodada();
		return;
	}
		
	%imperiaisPorAreas = mFloor(%this.jogadorDaVez.mySimAreas.getCount()/2);
	%this.jogadorDaVez.imperiais += %imperiaisPorAreas;
	
	if(%this.jogadorDaVez.persona.aca_av_4 > 0 && %this.jogadorDaVez.persona.especie $= "humano")
		%this.setBonusImperiaisDaVezPorSatelite(%imperiaisPorAreas);
}

function jogo::setImperiaisDaVezPrimeiraRodada(%this)
{
	if(%this.semPesquisas)
		return;
	
	if(%this.poker)
		return;
		
	%this.setBonusImperiaisDaVezPorPlaneta();
	
	if(%this.handicap)
		%this.setBonusImperiaisDaVezPorHandicap();
		
	%this.jogadorDaVez.imperiais += %this.jogadorDaVez.persona.patente.impBonus;	
}

function jogo::setBonusImperiaisDaVezPorPlaneta(%this)
{
	if(%this.planeta.nome $= "Ungart")
	{
		if(%this.jogadorDaVez.persona.aca_v_6 > 0 && %this.jogadorDaVez.persona.especie $= "humano")
		{
			%this.jogadorDaVez.imperiais += 3;	
		}
		return;
	}
	
	if(%this.planeta.nome $= "Teluria")
	{
		if(%this.jogadorDaVez.persona.aca_pln_1 > 0)
		{
			%this.jogadorDaVez.imperiais += 3;	
		}
	}		
}

function jogo::setBonusImperiaisDaVezPorHandicap(%this)
{
	%this.jogadorDaVez.imperiais += %this.jogadorDaVez.persona.bonusDifImp;
	%this.jogadorDaVez.minerios += %this.jogadorDaVez.persona.bonusDifRec;
	%this.jogadorDaVez.petroleos += %this.jogadorDaVez.persona.bonusDifRec;
	%this.jogadorDaVez.uranios += %this.jogadorDaVez.persona.bonusDifRec;	
}

function jogo::setBonusImperiaisDaVezPorSatelite(%this, %imperiaisPorAreas)
{
	switch (%this.jogadorDaVez.persona.aca_av_4)
	{
		case 1: %mod = 0.3;	
		case 2: %mod = 0.5;
		case 3: %mod = 0.7;
	}
	%imperiaisPorSatelite = mFloor(%imperiaisPorAreas * %mod); 
	%this.jogadorDaVez.imperiais += %imperiaisPorSatelite;
}

function jogo::setRecursosDaVez(%this)
{
	%recursosDeGrupos = serverPegarRecursosDeGrupos(%this.jogadorDaVez);
	%this.jogadorDaVez.minerios += FirstWord(%recursosDeGrupos);
	%this.jogadorDaVez.petroleos += getWord(%recursosDeGrupos, 1);
	%this.jogadorDaVez.uranios += getWord(%recursosDeGrupos, 2);
	
	%this.setBonusRecursosDaVezPorRefinaria();
	
	%this.setBonusRecursosDaVezPorMatriarca();
}

function jogo::setBonusRecursosDaVezPorRefinaria(%this)
{
	for(%i = 0; %i < %this.jogadorDaVez.mySimRefinarias.getCount(); %i++)
	{
		%this.jogadorDaVez.minerios++;	
	}		
}

function jogo::setBonusRecursosDaVezPorMatriarca(%this)
{
	if(!isObject(%this.jogadorDaVez.mySimMatriarcas))
		return;
		
	if(%this.jogadorDaVez.mySimMatriarcas.getCount() <= 0)
		return;
	
	if(%this.jogadorDaVez.persona.aca_v_1 > 1)	
		%this.jogadorDaVez.minerios += %this.jogadorDaVez.persona.aca_v_1 - 1;
}

function jogo::CTCinicializarTurno(%this)
{
	commandToClient(%this.jogadorDaVez.client, 'inicializarMeuTurno', %this.jogadorDaVez.movimentos, %this.jogadorDaVez.imperiais, %this.jogadorDaVez.minerios, %this.jogadorDaVez.petroleos, %this.jogadorDaVez.uranios);
		
	for(%i = 0; %i < %this.playersAtivos; %i++){
		if(%this.simPlayers.getObject(%i).client != %this.jogadorDaVez.client){
			commandToClient(%this.simPlayers.getObject(%i).client, 'setJogadorDaVezMovimentos', %this.jogadorDaVez.movimentos);	
		}
	}
	if(%this.observadorOn){
		commandToClient(%this.observador, 'setJogadorDaVezMovimentos', %this.jogadorDaVez.movimentos);	
	}	
}
	
function serverPegarRecursosDeGrupos(%player)
{
	%jogo = %player.jogo;
	
	for(%i = 0; %i < %jogo.gruposNoJogo.getCount(); %i++)
	{
		%grupo = %jogo.gruposNoJogo.getObject(%i);
		%eval = "%myVar = %player." @ %grupo.nome @ ";";
		eval(%eval);
		
		if(%myVar == 1)
		{
			if(%grupo.recurso $= "minerio"){
				%minerio++;
			} else if(%grupo.recurso $= "petroleo"){
				%petroleo++;
			} else if(%grupo.recurso $= "uranio"){
				%uranio++;
			}
		}
	}
	
	%return = %minerio SPC %petroleo SPC %uranio;
	return %return;
}

////////////////
function serverCmdFinalizarTurno(%client, %turnoTimeLeft){
	%jogo = %client.player.jogo;
		
	if(%jogo.jogadorDaVez.client != %client)
		return;
	
	%jogo.finalizarTurno(%turnoTimeLeft);
}

function jogo::finalizarTurno(%this, %turnoTimeLeft)
{
	%persona = %this.jogadorDaVez.persona;
	%this.zerarBonusCanibalDaVez();
		
	if(!%this.jogadorDaVez.taMorto)
	{
		%this.setImperioPlayer(%this.jogadorDaVez);
		
		%this.sortearNewInfo();
				
		if(%this.jogadorDaVez.imperio)
			%this.sortearNewInfo();
				
		if(%this.getFaroDaVez())
			%this.sortearNewInfo();
			
		%this.setBonusEconomia(%turnoTimeLeft);
	}
		
	serverRemoverCortejadas(%this); //tira os bônus de cortejo das rainhas do jogadorDaVez;
	%this.clearAllViruses(); //limpa as áreas com vírus
	
	%this.setJogadorDaVezParaTodos();
	serverClearUndo(%client.player);
	%this.inicializarTurno();
}

function jogo::sortearNewInfo(%this)
{
	%sorteioDeInfo = %this.sortearInfo();
	%this.CTCsetNewInfo(%sorteioDeInfo);
}

function jogo::CTCsetNewInfo(%this, %infoNum)
{
	commandToClient(%this.jogadorDaVez.client, 'setNewInfo', %infoNum);	
}


function jogo::getFaroDaVez(%this)
{
	%persona = %this.jogadorDaVez.persona;
	
	if(%persona.especie !$= "gulok")
		return false;
		
	if(%persona.aca_c_1 <= 0)
		return false;
		
	if(%this.rodada <= 0)
		return false;
		
	if(%this.rodada > %persona.aca_c_1)
		return false;
		
	if(%persona.myComerciante < 90)
		return false;
		
	return true;
}

function jogo::zerarBonusCanibalDaVez(%this)
{
	if(%this.jogadorDaVez.persona.especie !$= "gulok")
		return;
		
	for(%i = 0; %i < %this.jogadorDaVez.mySimLideres.getCount(); %i++){
		%this.jogadorDaVez.mySimLideres.getObject(%i).myBonusCanibalMax = 0;
		%this.jogadorDaVez.mySimLideres.getObject(%i).myBonusCanibalMin = 0;
	}	
}

function jogo::setBonusEconomia(%this, %turnoTimeLeft)
{
	%tempoGasto = %this.turnoTime - %turnoTimeLeft;
	
	if(%tempoGasto > 30)
		return;
	
	%this.setBonusEconomiaBasico();	
	
	if(%this.jogadorDaVez.persona.especie $= "gulok")
		%this.setBonusEconomiaMetabolismo();
			
	%this.CTCbonusEconomia();
}

function jogo::setBonusEconomiaBasico(%this)
{
	if(%this.jogadorDaVez.persona.patente.num >= 10){
		%this.jogadorDaVez.imperiais += 2;
	} else {
		%this.jogadorDaVez.imperiais++;	
	}	
}

function jogo::setBonusEconomiaMetabolismo(%this)
{
	if(%this.jogadorDaVez.persona.especie !$= "gulok")
		return;
		
	if(%this.jogadorDaVez.persona.aca_v_1 > 0)
		%this.jogadorDaVez.imperiais++;	
}

function jogo::CTCbonusEconomia(%this)
{
	commandToClient(%this.jogadorDaVez.client, 'bonusEconomia', %this.jogadorDaVez.persona.patente.num);	
}

function serverCmdAiPassarAVez(%client){
	%jogo = %client.player.jogo;
	%jogo.setJogadorDaVezParaTodos();
	%jogo.inicializarTurno();
}


function jogo::passarAVezEscolhendoObjetivos(%this)
{
	%this.setJogadorDaVezParaTodos();
	
	if(%this.jogadorDaVez == %this.getPrimeiroPlayerVivo())
	{
		%this.CTCpushCoresGui();
		return;
	}
	
	%this.CTCpushPopAguardandoObjGui();	//apaga ou mostra o gui de "aguardando"
}

//passa da escolha de objetivos pra escolha de cores:
function jogo::CTCpushCoresGui(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		commandToClient(%player.client, 'pushCoresGui');
	}
	if(%this.observadorOn)
	{
		commandToClient(%this.observador, 'pushCoresGui');
	}
}

function jogo::CTCpushPopAguardandoObjGui(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		if(%player == %this.jogadorDaVez){
			commandToClient(%player.client, 'popAguardandoObjGui'); //apaga o GUI de aguardo pro próximo jogador		
		} else {
			commandToClient(%player.client, 'pushAguardandoObjGui'); //mostra novamente o GUI de aguardo pra quem jogou
		}
	}	
}

//COR:
function serverCmdEscolherCor(%client, %cor){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	
	%eval = "%corEscolhida = $" @ %cor @ ";";
	eval(%eval);
	%eval = "%corClicada =" SPC %cor @ "_btn;";
	eval(%eval);
	
	%jogo.setColor(%corEscolhida, %jogadorDaVez);
	
	%jogo.CTCsetColor(%corEscolhida, %corClicada);
	
	%jogo.passarAVezEscolhendoCores();
}

function jogo::CTCsetColor(%this, %corEscolhida, %corClicada)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		commandToClient(%this.simPlayers.getObject(%i).client, 'setColor', %corEscolhida, %this.jogadorDaVez.id, %corClicada);
	}
	if(%this.observadorOn)
	{
		commandToClient(%this.observador, 'setColor', %corEscolhida, %this.jogadorDaVez.id, %corClicada);
	}
}

function jogo::passarAVezEscolhendoCores(%this)
{
	%this.setJogadorDaVezParaTodos();
	
	if(%this.jogadorDaVez == %this.player1)
		%this.CTCpopCoresGui();
	
	%this.CTCpopAguardandoDaVez();
}	

//Passa pra escolha de grupos:
function jogo::CTCpopCoresGui(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		commandToClient(%player.client, 'popCoresGui', %this.player1.persona.nome, %this.player2.persona.nome, %this.player3.persona.nome, %this.player4.persona.nome, %this.player5.persona.nome, %this.player6.persona.nome, %this.player1.persona.patente.nome, %this.player2.persona.patente.nome, %this.player3.persona.patente.nome, %this.player4.persona.patente.nome, %this.player5.persona.patente.nome, %this.player6.persona.patente.nome); 
	}
	if(%this.observadorOn)
	{
		commandToClient(%this.observador, 'popCoresGui', %this.player1.persona.nome, %this.player2.persona.nome, %this.player3.persona.nome, %this.player4.persona.nome, %this.player5.persona.nome, %this.player6.persona.nome, %this.player1.persona.patente.nome, %this.player2.persona.patente.nome, %this.player3.persona.patente.nome, %this.player4.persona.patente.nome, %this.player5.persona.patente.nome, %this.player6.persona.patente.nome);
	}
}

function jogo::CTCpopAguardandoDaVez(%this)
{
	commandToClient(%this.jogadorDaVez.client, 'popAguardandoObjGui');
}

//GRUPO:
function serverCmdEscolherGrupo(%client, %carta)
{
	%jogo = %client.player.jogo;
	
	%jogo.CTCapagarCartaDeGrupo(%carta);
		
	if(%jogo.emDuplas){
		%grupoSorteado = %jogo.getGrupoSorteadoEmDuplas(%client.player);
	} else {
		%grupoSorteado = %jogo.getGrupoSorteadoNormal();	
	}
	
	%jogo.setGrupoSorteado(%client.player, %grupoSorteado);	
}

function jogo::CTCapagarCartaDeGrupo(%this, %carta)
{
	for(%i = 0; %i < %this.playersAtivos; %i++){
		commandToClient(%this.simPlayers.getObject(%i).client, 'apagarCartaGrupo', %carta);
	}
	if(%this.observadorOn){
		commandToClient(%this.observador, 'apagarCartaGrupo', %carta);
	}	
}

function jogo::getGrupoSorteadoEmDuplas(%this, %player)
{
	if(%this.playersAtivos < 5 || %player.id $= "player1" || %player.id $= "player2" || %player.id $= "player3")
	{
		%resultado = dado(%this.gruposParaSorteioPool.getCount(), -1);
		%grupoSorteado = %this.gruposParaSorteioPool.getObject(%resultado);
		return %grupoSorteado;
	}
	
	%resultado = dado(%this.gruposDeDuplasPool.getCount(), -1);
	%grupoSorteado = %this.gruposDeDuplasPool.getObject(%resultado);
	return %grupoSorteado;
}

function jogo::getGrupoSorteadoNormal(%this)
{
	%resultado = dado(%this.gruposParaSorteioPool.getCount(), -1);
	%grupoSorteado = %this.gruposParaSorteioPool.getObject(%resultado);
	return %grupoSorteado;
}

function jogo::setGrupoSorteado(%this, %player, %grupoSorteado)
{
	if(%this.gruposParaSorteioPool.isMember(%grupoSorteado))
		%this.gruposParaSorteioPool.remove(%grupoSorteado);
		
	if(%this.emDuplas)
	{
		if(%this.gruposDeDuplasPool.isMember(%grupoSorteado))
			%this.gruposDeDuplasPool.remove(%grupoSorteado);
	}
		
	%player.grupoInicial = %grupoSorteado.nome;
	%this.escolherGrupo(%grupoSorteado.nome);
}

function serverCmdPassarAVezEscolhendoGrupos(%client){
	%jogo = %client.player.jogo;	
	%jogo.passarAVezEscolhendoGrupos();
}

function jogo::passarAVezEscolhendoGrupos(%this)
{
	%this.setJogadorDaVezParaTodos();
	
	if(%this.jogadorDaVez == %this.player1)
	{
		if(%this.poker)
		{
			%this.inicializarPkTurno();
			return;	
		}
		
		%this.iniciarPartida();
		return;
	}
	
	if(%this.emDuplas && %this.playersAtivos > 4 && %this.jogadorDaVez == %this.player4)
		%this.CTCreloadGruposGui();
		
	%this.CTCpushPopAguardandoObjGui();
}	

function jogo::CTCreloadGruposGui(%this)
{
	commandToClient(%this.player4.client, 'reloadGruposGui'); //apaga o GUI de aguardo pro próximo jogador		
	commandToClient(%this.player5.client, 'reloadGruposGui'); //apaga o GUI de aguardo pro próximo jogador	
	commandToClient(%this.player6.client, 'reloadGruposGui'); //apaga o GUI de aguardo pro próximo jogador
}


////////////

function jogo::inicializarPkTurno(%this)
{
	%this.inPoker = true;
	%this.CTCinicializarPkTurno();	
}

function jogo::CTCinicializarPkTurno(%this)
{
	commandToClient(%this.jogadorDaVez.client, 'inicializarMyPkTurno');
		
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		if(%this.simPlayers.getObject(%i).client != %this.jogadorDaVez.client)
			commandToClient(%this.simPlayers.getObject(%i).client, 'inicializarPkTurno');	
	}
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'inicializarPkTurno');	
}

function jogo::finalizarPkTurno(%this)
{
	%this.setJogadorDaVezParaTodos();
		
	if(%this.pk_jogo.getFullCircle())
	{
		%primeiroPlayerVivo = %this.getPrimeiroPlayerVivo();
		%this.jogadorDaVez = %primeiroPlayerVivo;
		%this.voltarDoPokerProJogo();
		%this.CTCsetJogadorDaVezParaTodos();
				
		if(%this.pk_jogo.simMesa.getCount() == 1)
			%this.iniciarPartida();
			
		return;
	}
	
	%this.inicializarPkTurno();
}


function jogo::voltarDoPokerProJogo(%this)
{
	%this.inPoker = false;
	%this.CTCvoltarDoPokerProJogo();
	
	if(%this.needPokerUpdate)
	{
		%this.inicializarTurno();
		%this.needPokerUpdate = false;	
	}
}

function jogo::CTCvoltarDoPokerProJogo(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
		commandToClient(%this.simPlayers.getObject(%i).client, 'voltarDoPokerProJogo');	
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'voltarDoPokerProJogo');	
}


function jogo::getPlayerStartStatus(%this, %player)
{
	if(%player.mySimObj.getcount() == 0)
		return 0;
	
	if(%player.mySimObj.getcount() == 1)
		return 1;
		
	if(%player.mySimObj.getcount() == 2)
	{
		if(%player.mycolor $= "")
			return 2;
			
		if(%player.grupoInicial $= "")
			return 3;
			
		return 4;
	}
	
	return 5;
}

function jogo::passarAVezPorStatus(%this, %status)
{
	switch (%status)
	{
		case 0: %this.passarAVezEscolhendoObjetivos();
		case 1: %this.passarAVezEscolhendoObjetivos();
		case 2: %this.passarAVezEscolhendoCores();
		case 3: %this.passarAVezEscolhendoGrupos();
		
		case 4:
			if(%quemMorreu == %this.ultimoPlayerVivo)
				%this.setJogadorDaVezParaTodos(true);	
			else
				%this.setJogadorDaVezParaTodos();
			
			%this.inicializarTurno();
		
		case 5: echo("*ERRO -> passar a vez por morte antes do início: possibilidade não levada em conta!");
	}
}
