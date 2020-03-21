// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverPk_jogo.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quinta-feira, 19 de fevereiro de 2009 1:37
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function pk_jogo::herdarCaracteristicasDaSala(%this)
{
	%this.blind = %this.jogoPai.sala.blind;
	%this.apostaMax = %this.jogoPai.sala.apostaMax; //em blinds
	%this.fichasIniciaisGlobal = %this.jogoPai.sala.fichasIniciaisGlobal;
	%this.setFichasForAll();
	%this.pagarBlindInicial();
	%this.zerarTodasAsPersonas();
}

function pk_jogo::setFichasForAll(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		echo(%player.persona.nome @ " entrou na sala com " @ %player.persona.pk_fichas @ " fichas.");
		%player.pk_fichas = %this.fichasIniciaisGlobal;
		%player.persona.pk_fichas -= %this.fichasIniciaisGlobal;
		echo(%player.persona.nome @ " pagou o investimento, ficando com " @ %player.persona.pk_fichas @ " fichas.");
	}	
}

function pk_jogo::pagarBlindInicial(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		%player.pk_fichas -= %this.blind;
	}
	
	%this.pot = %this.blind * %this.jogoPai.playersAtivos;
}



function pk_jogo::initCartas(%this)
{
	if(isObject(%this.simCartas))
		return;
		
	%this.copiarSimCartas();		
}

function pk_jogo::copiarSimCartas(%this)
{
	%this.simCartas = new SimSet();
	
	for(%i = 0; %i < %this.parent.simCartasBP.getCount(); %i++)
	{
		%carta = %this.parent.simCartasBP.getObject(%i);
		%this.simCartas.add(%carta);
	}
}

function pk_jogo::sortearCarta(%this)
{		
	%x = dado(%this.simCartas.getCount(), -1);
	%carta = %this.simCartas.getObject(%x);
	
	return %carta;
}

function pk_jogo::darCartaAoPlayer(%this, %player, %carta)
{
	if(!isObject(%player.pk_simCartas))
		%player.pk_simCartas = new SimSet();
		
	%this.simCartas.remove(%carta);
	%player.pk_simCartas.add(%carta);
	%this.sendTech(%player, %carta);
	
	%this.verificarClearForceRiver();
	echo("POKER - Carta dada para " @ %player.persona.nome @ ": " @ %carta.desc);
}

function pk_jogo::avaliarMao(%this, %player)
{
	%this.ordenarMaoPorNaipe(%player);
	
	if(%this.getQuadra(%player))
	{
		%player.jogoFeito = "Quadra";
		%player.pk_jogoFeitoProClient = "Quadra";
		%player.pk_pontos = 30;
		%player.quadra++;
		return;
	}
	
	if(%this.getFullHouse(%player))
	{
		%player.jogoFeito = "Full House";
		%player.pk_jogoFeitoProClient = "FullHouse";
		%player.pk_pontos = 20;
		%player.fullHouse++;
		return;
	}
	
	if(%this.getCores(%player))
	{
		%player.jogoFeito = "Cores";
		%player.pk_jogoFeitoProClient = "Cores";
		%player.pk_pontos = 15;
		%player.cores++;
		return;
	}
	
	if(%this.getTrinca(%player))
	{
		%player.jogoFeito = "Trinca";
		%player.pk_jogoFeitoProClient = "Trinca";
		%player.pk_pontos = 10;
		%player.trinca++;
		return;
	}
	
	if(%this.getDoisPares(%player))
	{
		%player.jogoFeito = "Dois Pares";
		%player.pk_jogoFeitoProClient = "DoisPares";
		%player.pk_pontos = 5;
		%player.doisPares++;
		return;
	}
	
	if(%this.getPar(%player))
	{
		%player.jogoFeito = "Um Par";
		%player.pk_jogoFeitoProClient = "UmPar";
		%player.pk_pontos = 2;
		%player.umPar++;
		return;
	}
}

function pk_jogo::ordenarMaoPorNaipe(%this, %player)
{
	for (%i = 0; %i < %player.pk_simCartas.getCount(); %i++)
	{
		for (%j = 1; %j < %player.pk_simCartas.getCount() - %i; %j++)
		{
			if(%player.pk_simCartas.getObject(%j-1).num > %player.pk_simCartas.getObject(%j).num)
				%player.pk_simCartas.reorderChild(%player.pk_simCartas.getObject(%j), %player.pk_simCartas.getObject(%j-1));
		}
	}
}

function pk_jogo::getQuadra(%this, %player)
{
	for (%i = 0; %i < %player.pk_simCartas.getCount(); %i++)
	{
		%carta[%i] = %player.pk_simCartas.getObject(%i);
	}
		
	if(%carta[0].num == %carta[1].num && %carta[0].num == %carta[2].num	&& %carta[0].num == %carta[3].num)
		return true;
		
	if(%carta[1].num == %carta[2].num && %carta[1].num == %carta[3].num	&& %carta[1].num == %carta[4].num)
		return true;
		
	return false;
}

function pk_jogo::getFullHouse(%this, %player)
{
	if(!%this.getTrinca(%player))
		return false;
		
	for (%i = 0; %i < %player.pk_simCartas.getCount(); %i++)
	{
		%carta[%i] = %player.pk_simCartas.getObject(%i);
	}
	
	if((%carta[0].num != %carta[2].num) && (%carta[0].num == %carta[1].num))
		return true;
	
	if((%carta[2].num != %carta[4].num) && (%carta[3].num == %carta[4].num))
		return true;
	
	return false;
}

function pk_jogo::getCores(%this, %player)
{
	for (%i = 0; %i < %player.pk_simCartas.getCount(); %i++)
	{
		%carta1 = %player.pk_simCartas.getObject(%i);
		for (%j = 0; %j < %player.pk_simCartas.getCount(); %j++)
		{
			%carta2 = %player.pk_simCartas.getObject(%j);
			if(%carta1 != %carta2)
			{
				if(%carta1.num == %carta2.num)
					return false;
			}
		}
	}
			
	return true;
}

function pk_jogo::getTrinca(%this, %player)
{
	for (%i = 0; %i < %player.pk_simCartas.getCount(); %i++)
	{
		%carta[%i] = %player.pk_simCartas.getObject(%i);
	}
		
	if(%carta[0].num == %carta[1].num && %carta[0].num == %carta[2].num)
		return true;
		
	if(%carta[1].num == %carta[2].num && %carta[1].num == %carta[3].num)
		return true;
		
	if(%carta[2].num == %carta[3].num && %carta[2].num == %carta[4].num)
		return true;
		
	return false;
}

function pk_jogo::getDoisPares(%this, %player)
{
	for (%i = 0; %i < %player.pk_simCartas.getCount(); %i++)
	{
		%carta[%i] = %player.pk_simCartas.getObject(%i);
	}	
	
	if((%carta[0].num != %carta[1].num) && (%carta[1].num != %carta[2].num))
		return false;
	
	if((%carta[2].num != %carta[3].num) && (%carta[3].num != %carta[4].num))
		return false;
		
	return true;
}

function pk_jogo::getPar(%this, %player)
{
	for (%i = 0; %i < %player.pk_simCartas.getCount(); %i++)
	{
		%carta[%i] = %player.pk_simCartas.getObject(%i);
	}	
	
	if((%carta[0].num == %carta[1].num) || (%carta[1].num == %carta[2].num) || (%carta[2].num == %carta[3].num) || (%carta[3].num == %carta[4].num))
		return true;
				
	return false;
}

function pk_jogo::flop(%this)
{
	%carta = %this.sortearCarta();
	%this.darCartaAMesa(%carta);
	%this.flops++;
}

function pk_jogo::darCartaAMesa(%this, %carta)
{
	if(!isObject(%this.simMesa))
		%this.simMesa = new Simset();
		
	%this.simMesa.add(%carta);
	%this.simCartas.remove(%carta);
	
	for(%i = 0; %i < %this.jogoPai.simPlayers.getCount(); %i++)
	{
		%this.jogoPai.simPlayers.getObject(%i).pk_simCartas.add(%carta);	
	}
	
	%this.zerarRodadaDeApostas();
	%this.CTCdarCartaAMesa(%carta);
	%this.sendTechForAll(%carta);
	//%this.iniciarRodadaDeApostas();
}

function pk_jogo::CTCdarCartaAMesa(%this, %carta)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'adicionarCartaPokerAMesa', %carta.id);
		
	if(%this.observadorOn)
		commandToClient(%this.jogoPai.observador, 'adicionarCartaPokerAMesa', %carta.id);	
}

function serverCmdSortearCartaPoker(%client, %cartaClicada)
{
	%player = %client.player;
	%pk_jogo = %player.pk_jogo;
	
	%carta = %pk_jogo.sortearCarta();
	%pk_jogo.darCartaAoPlayer(%player, %carta);
	%pk_jogo.CTCdarCartaAoPlayer(%player, %carta, %cartaClicada);
	
	%pk_jogo.verificarFlop();
}

function pk_jogo::CTCdarCartaAoPlayer(%this, %player, %carta, %cartaClicada)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%tempPlayer = %this.jogoPai.simPlayers.getObject(%i);
		if(%player != %tempPlayer)
			commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'apagarCartaPoker', %cartaClicada);
	}
		
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'apagarCartaPoker', %cartaClicada);
		
	commandToClient(%player.client, 'receberCartaPoker', %carta.id, %cartaClicada);
}

function pk_jogo::CTCcriarPk_jogo(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'criarPk_jogo', %this.blind, %this.apostaMax, %this.fichasIniciaisGlobal);
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'criarPk_jogo', %this.blind, %this.apostaMax, %this.fichasIniciaisGlobal);
}

function pk_jogo::verificarFlop(%this)
{
	if(%this.getTodosTemPeloMenosDuasCartas())
	{
		if(!isObject(%this.simMesa))
		{
			%this.CTCesperandoFlop();
			serverSheduleFlop(%this, 3000);
			return;
		}
		
		if(%this.simMesa.getCount() == 1)
		{
			%this.CTCesperandoTurn();
			serverSheduleFlop(%this, 3000);
			return;
		}	
	}
	
	%this.jogoPai.finalizarPkturno();
}

function pk_jogo::verificarClearForceRiver(%this)
{
	if(%this.getTodosTemCincoCartas())
		%this.CTCclearForceRiver();
}

function pk_jogo::getTodosTemCincoCartas(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		
		if(!isObject(%player.pk_simCartas) && !%player.taMorto)
			return false;
		
		if(%player.pk_simCartas.getCount() != 5 && !%player.taMorto)
			return false;
	}
	
	return true;
}

function pk_jogo::CTCclearForceRiver(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'ClearForceRiver');
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'ClearForceRiver');	
}


function pk_jogo::CTCesperandoFlop(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'forceEsperandoFlop');
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'forceEsperandoFlop');
}

function pk_jogo::CTCesperandoTurn(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'forceEsperandoTurn');
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'forceEsperandoTurn');
}

function pk_jogo::verificarRiver(%this)
{
	if(%this.jogoPai.rodada != 3)
		return;
	
	if(!%this.getTodosTemPeloMenosDuasCartas())
		return;
	
	if(%this.simMesa.getCount() != 2)
		return;
	
	%ultimoPlayerVivo = %this.jogoPai.getUltimoPlayerVivo();
	if(%ultimoPlayerVivo.pk_simCartas.getCount() != 4)
		return; 
	
	%this.river();
}


function pk_jogo::getTodosTemPeloMenosDuasCartas(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		
		if(!isObject(%player.pk_simCartas) && !%player.taMorto)
			return false;
		
		if(%player.pk_simCartas.getCount() < 2 && !%player.taMorto)
			return false;
	}
	
	return true;
}

function serverSheduleFlop(%pk_jogo, %time)
{
	schedule(%time, 0, "serverPk_jogoFlop", %pk_jogo);	
}

function serverPk_jogoFlop(%pk_jogo)
{
	%pk_jogo.flop();
	
	if(%pk_jogo.flops >= 2)
		return;
	
	%pk_jogo.jogoPai.finalizarPkturno(); //só tem que passar a vez no primeiro flop, não no turn;
}


function pk_jogo::firstStart(%this)
{
	%this.CTCpk_firstStart();	
}

function pk_jogo::CTCpk_firstStart(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'pk_firstStart');
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'pk_firstStart');	
}

function pk_jogo::darMesa(%this)
{
	%this.jogoPai.jogadorDaVez.pk_apostaAtual = "MESA";
	%this.CTCdeuMesa();
	serverSheduleFinalizarPkTurno(%this.jogoPai, 1000);
}

function pk_jogo::CTCdeuMesa(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'pk_deuMesa');
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'pk_deuMesa');		
}


function serverSheduleFinalizarPkTurno(%jogo, %time)
{
	schedule(%time, 0, "serverFinalizarPkTurno", %jogo);	
}

function serverFinalizarPkTurno(%jogo)
{
	%jogo.finalizarPkTurno();	
}

function serverCmdPk_darMesa(%client)
{
	%pk_jogo = %client.player.pk_jogo;
	%pk_jogo.darMesa();
}

function pk_jogo::getApostasIguais(%this)
{
	if(!%this.rodadaCompleta)
		return false;
		
	if(!%this.getTodosTemPeloMenosDuasCartas())
		return false;
		
	if(%this.simMesa.getCount() < 1)
		return false;
	
	if(%this.getFullCircle())
		return true;
	
	return false;
}

function pk_jogo::getFullCircle(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		
		if(%player.pk_apostaAtual $= "" && !%player.pk_fugiu)
		{
			echo("not full circle: " @ %player.persona.nome @ " tem como aposta atual NADA.");
			return false;
		}
				
		for(%j = 0; %j < %this.jogoPai.playersAtivos; %j++)
		{
			%tempPlayer = %this.jogoPai.simPlayers.getObject(%j);
			if(%player.pk_apostaAtual !$= %tempPlayer.pk_apostaAtual && !%player.pk_fugiu && !%tempPlayer.pk_fugiu)
			{
				echo("not full circle: " @ %player.persona.nome @ " e " @ %tempPlayer.persona.nome @ " não fugiram e têm apostas diferentes!");
				return false;
			}
		}
	}
	
	echo("FULL CIRCLE!");
	return true;
}

function pk_jogo::setPk_rodadaCompleta(%this, %val)
{
	%this.rodadaCompleta = %val;	
}

function pk_jogo::river(%this)
{
	%this.zerarRodadaDeApostas();
	%this.CTCpk_river();	
}

function pk_jogo::zerarRodadaDeApostas(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		%player.pk_apostaAtual = "";
	}
	%this.CTCzerarPkLastAction();
}

function pk_jogo::CTCzerarPkLastAction(%this)
{	
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'zerarPkLastAction');
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'zerarPkLastAction');		
}

function pk_jogo::CTCpk_river(%this)
{	
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'pk_river');
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'pk_river');		
}

/////////////////
//apostas:

function serverCmdPk_apostar(%client, %blinds)
{
	%pk_jogo = %client.persona.player.pk_jogo;
	%pk_jogo.apostar(%blinds);
	serverSheduleFinalizarPkTurno(%pk_jogo.jogoPai, 1000);
}	

function pk_jogo::apostar(%this, %blinds)
{
	%apostador = %this.jogoPai.jogadorDaVez;
	%aposta = %this.blind * %blinds;
	%apostador.pk_apostaAtual += %aposta;
	%apostador.pk_fichas -= %aposta;
	%this.apostaAtual = %apostador.pk_apostaAtual;
	%this.pot += %aposta;
	
	%this.CTCpk_aposta(%aposta);
}


function pk_jogo::CTCpk_aposta(%this, %aposta)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'pk_aposta', %aposta);
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'pk_aposta', %aposta);			
}


//pagar:
function serverCmdPk_pagarAposta(%client)
{
	%pk_jogo = %client.persona.player.pk_jogo;
	%pk_jogo.pagarAposta();
	serverSheduleFinalizarPkTurno(%pk_jogo.jogoPai, 1000);
}	
function pk_jogo::pagarAposta(%this)
{
	%apostador = %this.jogoPai.jogadorDaVez;
	%aposta = %this.apostaAtual - %apostador.pk_apostaAtual;
	%apostador.pk_apostaAtual = %this.apostaAtual;
	%apostador.pk_fichas -= %aposta;
	%this.pot += %aposta;
		
	%this.CTCpk_pagarAposta(%aposta);
}

function pk_jogo::CTCpk_pagarAposta(%this, %aposta)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'pk_pagarAposta', %aposta);
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'pk_pagarAposta', %aposta);			
}


/////////////
function pk_jogo::CTCforceInativarPkBtns(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'forceInativarPkBtns', %aposta);
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'forceInativarPkBtns', %aposta);
}

//////////////////////
//Fugir:
function serverCmdPk_fugir(%client)
{
	%pk_jogo = %client.persona.player.pk_jogo;
	%pk_jogo.fugir(%client.persona.player);
}

function pk_jogo::fugir(%this, %player)
{
	%player.pk_fugiu = true;
	serverCmdRenderTodos(%player.client);
	%this.CTCplayerFugiu(%player);
}

function pk_jogo::CTCplayerFugiu(%this, %player)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'playerFugiu', %player.id);
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'playerFugiu', %player.id);	
}

function serverPokerFimSolitario(%jogo)
{
	echo(">>>>>>>>>>>>>>>>>>>>>>>>>>>>serverPokerFimSolitario<<<<<<<<<<<<<<<<<<<<<<<<<<<<");	
	%jogo.pk_jogo.execFimSolitario();
}



	
function pk_jogo::execFimSolitario(%this)
{
	%primeiroPlayerVivo = %this.jogoPai.getPrimeiroPlayerVivo();
		
	%this.jogoPai.setGanhadorNormalPlayer(%primeiroPlayerVivo);
	%primeiroPlayerVivo.ganhouOJogo = true;
	%this.mandarPaiFinalizarJogo();
	
	%this.CTCfimSolitario(%this.pot, %primeiroPlayerVivo);
}

function pk_jogo::mandarPaiFinalizarJogo(%this)
{
	%jogoPai = %this.jogoPai;
	%jogoPai.verificacoesFinais();
	%jogoPai.terminado = true; 
	%jogoPai.setDataFim();	
	%jogoPai.setTempoDeJogo();
	%jogoPai.calcularPontosGlobal();	
	%jogoPai.setSimVencedores();	
	%jogoPai.setNewStatsForAll();
	%this.setPkFichasBatidaForaAll();
	%this.setPowerPlayForAll();
	
	%jogoPai.setFimDeJogoString(%jogoPai.getPrimeiroPlayerVivo());
	%jogoPai.assassino = %jogoPai.getAssassino();
	%jogoPai.resetarPersonasProntas();
	
	%jogoPai.sala.jogoTAXOid = "";
	enviarTAXOResultado(%jogoPai);
	serverWriteURLToFile(serverCriarURL(%jogoPai));
	
	%jogoPai.recalcularGraduacaoForAll();
	%jogoPai.setPartidaEncerrada();
	
	if(isObject(%jogoPai.sala.banList))
		%jogoPai.sala.banList.clear();
	
	serverAtualizarAtrioParaTodos();	
}
	
	
function pk_jogo::returnFichasNaoGastasForAll(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		echo(%player.persona.nome @ " acabou a partida com " @ %player.pk_fichas @ " fichas não-apostadas.");
		%player.persona.pk_fichas += %player.pk_fichas;
		%player.persona.lastPkFichasAReceber = %player.pk_fichas;
		echo(%player.persona.nome @ " recebeu de volta suas fichas, ficando com " @ %player.persona.pk_fichas @ " fichas.");
	}
}

function pk_jogo::CTCfimSolitario(%this, %pot, %ganhador)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'pk_fimSolitario', %pot, %ganhador.id);
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'pk_fimSolitario', %pot, %ganhador.id);
}


////////////

function pk_jogo::setPowerPlayForAll(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		if(%this.getPowerPlay(%player))
		{
			%player.powerPlay = true;
			%player.partidaPerfeita = true;
		}
	}
}

function pk_jogo::setPkFichasBatidaForaAll(%this)
{
	%this.returnFichasNaoGastasForAll();
	%potIndividual = mFloor(%this.pot / %this.jogoPai.vencedores.getCount());
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		if(%player.ganhouOJogo)
		{
			echo(%player.persona.nome @ " ganhou o pot de " @ %potIndividual @ " quando tinha " @ %player.persona.pk_fichas);
			%player.persona.pk_fichas += %potIndividual;
			%player.persona.lastPkFichasAReceber += %potIndividual;
			%player.pk_fichas += %potIndividual;
			echo(%player.persona.nome @ " venceu a partida e levou o pot de " @ %potIndividual @ ", ficando com um total de " @ %player.persona.pk_fichas @ " fichas.");
		}
	}
	%this.TAXOdarFichasForAll();
}


function pk_jogo::getPowerPlay(%this, %player)
{
	if(%player.totalDePontos <= 0)
		return false;
		
	if(!%player.ganhouOJogo)
		return false;
	
	%maiorPk_jogo = %this.getMaiorPk_jogo();
	if(%player.pk_pontos >= %maiorPk_jogo)
		return false;
		
	return true;
}

function pk_jogo::getMaiorPk_jogo(%this)
{
	%this.maiorPk_jogo = 0;
	
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		if(%player.pk_pontos > %this.maiorPk_jogo)
		{
			%this.maiorPk_jogo = %player.pk_pontos;
		}
	}	

	
	return %this.maiorPk_jogo;
}
//////////////////////////
//tecnologias:

function pk_jogo::sendTechForAll(%this, %carta)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		%this.sendTech(%player, %carta);
	}
}

function pk_jogo::sendTech(%this, %player, %carta)
{
	if(%carta.pesquisaId $= "especial")
	{
		%this.darTecnologiaEspecialAoPlayer(%player, %carta.id);
		return;	
	}
	
	%this.darTecnologiaAoPlayer(%player, %carta.pesquisaId);
}



function pk_jogo::darTecnologiaAoPlayer(%this, %player, %pesClassId)
{
	%eval = "%player.persona." @ %pesClassId @ " = 3;";
	eval(%eval);
	echo("TECNOLOGIA " @ %pesClassId @ " DADA AO PLAYER " @ %player.persona.nome);
	if(%pesClassId $= "aca_c_1")
		%player.prospeccao = 3;
		
	if(%pesClassId $= "aca_v_5")
		%player.airDrops = 2;
		
	%this.CTCdarTecnologia(%player, %pesClassId);
}

function pk_jogo::CTCdarTecnologia(%this, %player, %pesClassId)
{
	commandToClient(%player.client, 'darTecnologia', %pesClassId);		
}

function pk_jogo::darTecnologiaEspecialAoPlayer(%this, %player, %cartaId)
{
	switch(%cartaId)
	{
		case 4: %this.darLiderAoPlayer(%player);
		case 8: %this.darLiderAoPlayer(%player);
		case 12: %this.darVelocistaAoPlayer(%player);
		case 17: %this.darColecionadorAoPlayer(%player);
		case 18: %this.darMarinheiroAoPlayer(%player);
		case 19: %this.darEngenheiroAoPlayer(%player);
		case 20: %this.darMagnataAoPlayer(%player);
	}
}

function pk_jogo::darLiderAoPlayer(%this, %player)
{
	%player.persona.aca_a_1++;	
	%player.pk_lideresDisponiveis++;
	%this.CTCdarLiderAoPlayer(%player);
}

function pk_jogo::CTCdarLiderAoPlayer(%this, %player)
{
	commandToClient(%player.client, 'darLider');	
}

function pk_jogo::darVelocistaAoPlayer(%this, %player)
{
	%player.velocista = true;
	%this.jogoPai.setMovimentosDaVez();
	%this.CTCAtualizarMovimentosDoPrimeiroPlayerVivo(); 
}

function pk_jogo::CTCAtualizarMovimentosDoPrimeiroPlayerVivo()
{
	%primeiroPlayerVivo = %this.jogoPai.getPrimeiroPlayerVivo();
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
		commandToClient(%this.jogoPai.simPlayers.getObject(%i).client, 'atualizarMovimentosDoPrimeiroPlayerVivo', %primeiroPlayerVivo.movimentos);
	
	if(%this.jogoPai.observadorOn)
		commandToClient(%this.jogoPai.observador, 'atualizarMovimentosDoPrimeiroPlayerVivo', %primeiroPlayerVivo.movimentos);	
}

function pk_jogo::darColecionadorAoPlayer(%this, %player)
{
	%player.colecionador = true;
	%player.minerios+=1;
	%player.petroleos+=1;
	%player.uranios+=1;
	%this.CTCdarColecionador(%player);
}

function pk_jogo::CTCdarColecionador(%this, %player)
{
	commandToClient(%player.client, 'darColecionador');	
}

function pk_jogo::darEngenheiroAoPlayer(%this, %player)
{
	%player.engenheiro = true;
	%this.CTCdarEngenheiro(%player);
}

function pk_jogo::CTCdarEngenheiro(%this, %player)
{
	commandToClient(%player.client, 'darEngenheiro');	
}

function pk_jogo::darMarinheiroAoPlayer(%this, %player)
{
	%player.marinheiro = true;
	%this.CTCdarMarinheiro(%player);
}

function pk_jogo::CTCdarMarinheiro(%this, %player)
{
	commandToClient(%player.client, 'darMarinheiro');	
}

function pk_jogo::darMagnataAoPlayer(%this, %player)
{
	%player.magnata = true;
	%player.imperiais += 20;
	%this.CTCdarMagnata(%player);
}

function pk_jogo::CTCdarMagnata(%this, %player)
{
	commandToClient(%player.client, 'darMagnata');	
}


////
// A função de zerar personas deveria já fazer uma cópia da persona original, guardando-a para depois. 
function pk_jogo::zerarTodasAsPersonas(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		%player.persona.setAcadBKP();
		%this.zerarPersona(%player);
	}
}


//zerar uma persona:
function pk_jogo::zerarPersona(%this, %player)
{
	%persona = %player.persona;
	if(%persona.especie $= "humano"){
		//soldados:
		%persona.aca_s_d_min = 1;
		%persona.aca_s_d_max = 6;
		%persona.aca_s_a_min = 1;
		%persona.aca_s_a_max = 6;
		//tanques:
		%persona.aca_t_d_min = 1;
		%persona.aca_t_d_max = 12;
		%persona.aca_t_a_min = 1;
		%persona.aca_t_a_max = 12;
		//navios:
		%persona.aca_n_d_min = 1;
		%persona.aca_n_d_max = 12;
		%persona.aca_n_a_min = 1;
		%persona.aca_n_a_max = 12;
		//líder1:
		%persona.aca_ldr_1_d_min = 1;
		%persona.aca_ldr_1_d_max = 12;
		%persona.aca_ldr_1_a_min = 1;
		%persona.aca_ldr_1_a_max = 12;
		%persona.aca_ldr_1_h1 = 2;
		%persona.aca_ldr_1_h2 = 2;
		%persona.aca_ldr_1_h3 = 2;
		%persona.aca_ldr_1_h4 = 2;
		//líder2:
		%persona.aca_ldr_2_d_min = 1;
		%persona.aca_ldr_2_d_max = 12;
		%persona.aca_ldr_2_a_min = 1;
		%persona.aca_ldr_2_a_max = 12;
		%persona.aca_ldr_2_h1 = 2;
		%persona.aca_ldr_2_h2 = 2;
		%persona.aca_ldr_2_h3 = 2;
		%persona.aca_ldr_2_h4 = 2;
		//líder3:
		%persona.aca_ldr_3_d_min = 1;
		%persona.aca_ldr_3_d_max = 12;
		%persona.aca_ldr_3_a_min = 1;
		%persona.aca_ldr_3_a_max = 12;
		%persona.aca_ldr_3_h1 = 2;
		%persona.aca_ldr_3_h2 = 2;
		%persona.aca_ldr_3_h3 = 2;
		%persona.aca_ldr_3_h4 = 2;
		
		//visionário
		%persona.aca_v_1 = 2;
		%persona.aca_v_2 = 0;
		%persona.aca_v_3 = 0;
		%persona.aca_v_4 = 0;
		%persona.aca_v_5 = 0;
		%persona.aca_v_6 = 0;
		//arebatador:
		%persona.aca_a_1 = 0;
		%persona.aca_a_2 = 0;
		//comerciante:
		%persona.aca_c_1 = 0;
		//diplomata:
		%persona.aca_d_1 = 3;
		//intel:
		%persona.aca_i_1 = 0;
		%persona.aca_i_2 = 0;
		%persona.aca_i_3 = 0;
	} else if(%persona.especie $= "gulok"){
		//vermes:
		%persona.aca_s_d_min = 1;
		%persona.aca_s_d_max = 8;
		%persona.aca_s_a_min = 1;
		%persona.aca_s_a_max = 8;
		//Rainhas:
		%persona.aca_t_d_min = 1;
		%persona.aca_t_d_max = 15;
		%persona.aca_t_a_min = 1;
		%persona.aca_t_a_max = 15;
		//Cefaloks:
		%persona.aca_n_d_min = 1;
		%persona.aca_n_d_max = 14;
		%persona.aca_n_a_min = 1;
		%persona.aca_n_a_max = 14;
		//Zangão1:
		%persona.aca_ldr_1_d_min = 1;
		%persona.aca_ldr_1_d_max = 14;
		%persona.aca_ldr_1_a_min = 1;
		%persona.aca_ldr_1_a_max = 14;
		%persona.aca_ldr_1_h1 = 2;
		%persona.aca_ldr_1_h2 = 2;
		%persona.aca_ldr_1_h3 = 2;
		%persona.aca_ldr_1_h4 = 2;
		//Zangão2:
		%persona.aca_ldr_2_d_min = 1;
		%persona.aca_ldr_2_d_max = 14;
		%persona.aca_ldr_2_a_min = 1;
		%persona.aca_ldr_2_a_max = 14;
		%persona.aca_ldr_2_h1 = 2;
		%persona.aca_ldr_2_h2 = 2;
		%persona.aca_ldr_2_h3 = 2;
		%persona.aca_ldr_2_h4 = 2;
		//Dragnal2:
		%persona.aca_ldr_3_d_min = 15;
		%persona.aca_ldr_3_d_max = 30;
		%persona.aca_ldr_3_a_min = 15;
		%persona.aca_ldr_3_a_max = 30;
		%persona.aca_ldr_3_h1 = 2;
		%persona.aca_ldr_3_h2 = 2;
		%persona.aca_ldr_3_h3 = 2;
		%persona.aca_ldr_3_h4 = 2;
		
		//visionário
		%persona.aca_v_1 = 0;
		%persona.aca_v_2 = 0;
		%persona.aca_v_3 = 0;
		%persona.aca_v_4 = 0;
		%persona.aca_v_5 = 0;
		%persona.aca_v_6 = 0;
		//arebatador:
		%persona.aca_a_1 = 0;
		%persona.aca_a_2 = 0;
		//comerciante:
		%persona.aca_c_1 = 0;
		//diplomata:
		%persona.aca_d_1 = 0;
		//intel:
		%persona.aca_i_1 = 0;
		%persona.aca_i_2 = 0;
		%persona.aca_i_3 = 0;
	}
	
	//Pesquisa Em Andamento:
	%persona.aca_pea_id = 0;
	%persona.aca_pea_min = 0;
	%persona.aca_pea_pet = 0;
	%persona.aca_pea_ura = 0;
	%persona.aca_pea_ldr = 0;
		
	//marca que a persona tem dados de academia:
	%persona.aca_tenhoDados = true;
	
	//pesquisas Avançadas:
	%persona.aca_av_1 = 0;
	%persona.aca_av_2 = 0;
	%persona.aca_av_3 = 0;
	%persona.aca_av_4 = 0;
	
	%persona.aca_pln_1 = 0;
	
	%persona.aca_art_1 = 1; //Geo-Canhão
	%persona.aca_art_2 = 1; //Nexus Temporal
	
	echo("Persona " @ %persona.nome @ " teve a academia zerada com sucesso!");
}


//Pagamento de fichas:

function pk_jogo::taxoPagarFichasIniciais(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		%custoDaSala = %this.fichasIniciaisGlobal * -1;
		$poker_handler.taxoPk_fichas(%player.persona, %custoDaSala, 0, 0); //params(%persona, %fichas, %creditos, %omnis)
	}
}

function pk_jogo::TAXOdarFichasForAll(%this)
{
	for(%i = 0; %i < %this.jogoPai.playersAtivos; %i++)
	{
		%player = %this.jogoPai.simPlayers.getObject(%i);
		commandToClient(%player.client, 'ReceberPkFichas', %player.persona.lastPkFichasAReceber);
		%this.taxoReceberFichasDePoker(%player);
	}
}
function pk_jogo::taxoReceberFichasDePoker(%this, %player)
{
	if(%player.pk_fichas <= 0)
		return;
		
	$poker_handler.taxoPk_fichas(%player.persona, %player.persona.lastPkFichasAReceber, 0, 0); //params(%persona, %fichas, %creditos, %omnis)
	%player.persona.lastPkFichasAReceber = 0;
}


///////

