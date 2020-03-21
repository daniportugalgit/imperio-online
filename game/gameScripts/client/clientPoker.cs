// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientPoker.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 18 de novembro de 2008 14:03
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function initPokerSys()
{
	$pokerSysNum++;

	$poker_handler = newSimObj("pk_handler", $pokerSysNum);
	$poker_handler.initSimCartas();
	$poker_handler.createClientActions();
}

function pk_handler::initSimCartas(%this)
{
	if(!isObject(%this.simCartasBP))
	{
		%this.simCartasBP = new SimSet();
		%this.criarTodasAsCartas();
	}	
}

function pk_handler::criarCarta(%this, %id, %num, %naipe, %nome, %pesquisaId, %toolTip, %toolTipMesa){
	$pokerCartaBPNum++;
	%carta = newSimObj("pk_carta", $pokerCartaBPNum);	
	%carta.id = %id;
	%carta.num = %num;
	%carta.naipe = %naipe;
	%carta.nome = %nome;
	%carta.pesquisaId = %pesquisaId;
	%carta.desc = %nome SPC %naipe;
	%carta.toolTip = %toolTip;
	%carta.toolTipMesa = %toolTipMesa;
	
	%this.simCartasBP.add(%carta);
	
	//echo("Carta de Poker " @ %carta.desc @ " criada com sucesso!");
}

function pk_handler::criarTodasAsCartas(%this){
	%this.criarCarta(1, 1, "Vermelho", "CANHÃO ORBITAL", "aca_a_2", "Você pode fazer um disparo orbital a partir da 3ª rodada.", "Todos podem fazer um disparo orbital a partir da 3ª rodada.");
	%this.criarCarta(2, 1, "Vermelho", "MIRA ELETRÔNICA", "aca_av_2", "Todas as suas unidades têm +6 em ataque mínimo e máximo.", "Todas as unidades têm +6 em ataque mínimo e máximo.");
	%this.criarCarta(3, 1, "Vermelho", "CARAPAÇA", "aca_av_1", "Todas as suas unidades têm +6 em defesa mínima e máxima.", "Todas as unidades têm +6 em defesa mínima e máxima.");
	%this.criarCarta(4, 1, "Vermelho", "LÍDER BRAVO", "aca_a_1", "Você pode convocar um líder anfíbio com habilidades no nível 2.", "Todos podem convocar um líder anfíbio com habilidades no nível 2.");
			
	%this.criarCarta(5, 2, "Amarelo", "OCULTAR", "aca_av_3", "Você pode tornar invisíveis suas bases e refinarias.", "Todos podem tornar bases e refinarias invisíveis aos adversários.");
	%this.criarCarta(6, 2, "Amarelo", "RECICLAGEM", "aca_v_3", "Você pode reciclar bases e refinarias instantaneamente.", "Todos podem reciclar bases e refinarias instantaneamente.");
	%this.criarCarta(7, 2, "Amarelo", "REFINARIA", "aca_v_4", "Você pode construir duas refinarias, cada uma por 4 imperiais.", "Todos podem construir duas refinarias, cada uma por 4 imperiais.");
	%this.criarCarta(8, 2, "Amarelo", "LIDER ALPHA", "aca_a_1", "Você pode convocar um líder anfíbio com habilidades no nível 2.", "Todos podem convocar um líder anfíbio com habilidades no nível 2.");
		
	%this.criarCarta(9, 3, "Verde", "SATÉLITE", "aca_av_4", "Você recebe 70% mais imperiais por rodada.", "Todos recebem 70% mais imperiais por rodada.");
	%this.criarCarta(10, 3, "Verde", "AIR DROP", "aca_v_5", "Você pode fazer Air Drops a partir da segunda rodada.", "Todos podem fazer Air Drops a partir da segunda rodada.");	
	%this.criarCarta(11, 3, "Verde", "TRANSPORTE", "aca_v_2", "Seus navios transportam até 5 soldados ou líderes.", "Todos os navios transportam até 5 soldados ou líderes.");
	%this.criarCarta(12, 3, "Verde", "VELOCISTA", "especial", "Você pode fazer 6 movimentos por rodada.", "Todos podem fazer 6 movimentos por rodada."); 
	
	%this.criarCarta(13, 4, "Azul", "ESPIONAGEM", "aca_i_1", "Você fica sabendo quando um adversário vende recursos ao Banco.", "Todos ficam sabendo quando alguém vende recursos ao Banco.");
	%this.criarCarta(14, 4, "Azul", "FILANTROPIA", "aca_i_2", "Você pode fazer até 3 doações filantrópicas nesta partida.", "Todos podem fazer até 3 doações filantrópicas nesta partida.");
	%this.criarCarta(15, 4, "Azul", "ALMIRANTE", "aca_i_3", "Você ganhará 6 pontos se possuir 5 ou mais bases no mar.", "Cada jogador ganhará 6 pontos se possuir 5 ou mais bases no mar.");
	%this.criarCarta(16, 4, "Azul", "PROSPECÇÃO", "aca_c_1", "Você pode comprar até 3 missões nesta partida.", "Todos podem comprar até 3 missões nesta partida.");
	
	%this.criarCarta(17, 5, "Roxo", "COLECIONADOR", "especial", "Você ganhou 1 conjunto de recursos.", "Todos ganharam 1 conjunto de recursos.");
	%this.criarCarta(18, 5, "Roxo", "MARINHEIRO", "especial", "Você pode construir navios por apenas 2 imperiais.", "Todos podem construir navios por apenas 2 imperiais.");
	%this.criarCarta(19, 5, "Roxo", "ENGENHEIRO", "especial", "Você pode construir bases por apenas 7 imperiais.", "Todos podem construir bases por apenas 7 imperiais."); 
	%this.criarCarta(20, 5, "Roxo", "MAGNATA", "especial", "Você ganhou 20 imperiais.", "Todos ganharam 20 imperiais."); 
}

function pk_handler::criarCartasSuperPoker(%this){
	%this.criarCarta(1, 1, "Vermelho", "AIR DROP", "aca_v_5", "Você pode fazer Air Drops a partir da segunda rodada.", "Todos podem fazer Air Drops a partir da segunda rodada.");
	%this.criarCarta(2, 1, "Vermelho", "CANHÃO ORBITAL", "aca_a_2", "Você pode fazer um disparo orbital a partir da 3ª rodada.", "Todos podem fazer um disparo orbital a partir da 3ª rodada.");
	%this.criarCarta(3, 1, "Vermelho", "MIRA ELETRÔNICA", "aca_av_2", "Todas as suas unidades têm +6 em ataque mínimo e máximo.", "Todas as unidades têm +6 em ataque mínimo e máximo.");
	%this.criarCarta(4, 1, "Vermelho", "CARAPAÇA", "aca_av_1", "Todas as suas unidades têm +6 em defesa mínima e máxima.", "Todas as unidades têm +6 em defesa mínima e máxima.");
	%this.criarCarta(5, 1, "Vermelho", "LÍDER BRAVO", "aca_a_1", "Você pode convocar um líder anfíbio com habilidades no nível 2.", "Todos podem convocar um líder anfíbio com habilidades no nível 2.");
		
	%this.criarCarta(6, 2, "Amarelo", "SATÉLITE", "aca_av_4", "Você recebe 70% mais Imperiais por rodada.", "Todos recebem 70% mais imperiais por rodada.");
	%this.criarCarta(7, 2, "Amarelo", "OCULTAR", "aca_av_3", "Você pode tornar invisíveis suas bases e refinarias.", "Todos podem tornar bases e refinarias invisíveis aos adversários.");
	%this.criarCarta(8, 2, "Amarelo", "RECICLAGEM", "aca_v_3", "Você pode reciclar bases e refinarias instantaneamente.", "Todos podem reciclar bases e refinarias instantaneamente.");
	%this.criarCarta(9, 2, "Amarelo", "REFINARIA", "aca_v_4", "Você pode construir duas refinarias, cada uma por 4 imperiais.", "Todos podem construir duas refinarias, cada uma por 4 imperiais.");
	%this.criarCarta(10, 2, "Amarelo", "LIDER ALPHA", "aca_a_1", "Você pode convocar um líder anfíbio com habilidades no nível 2.", "Todos podem convocar um líder anfíbio com habilidades no nível 2.");
		
	%this.criarCarta(11, 3, "Verde", "TRANSPORTE", "aca_v_2", "Seus navios transportam até 5 soldados ou líderes.", "Todos os navios transportam até 5 soldados ou líderes.");
	%this.criarCarta(12, 3, "Verde", "ESPIONAGEM", "aca_i_1", "Você fica sabendo quando um adversário vende recursos ao Banco.", "Todos ficam sabendo quando alguém vende recursos ao Banco.");
	%this.criarCarta(13, 3, "Verde", "FILANTROPIA", "aca_i_2", "Você pode fazer até 3 doações filantrópicas nesta partida.", "Todos podem fazer até 3 doações filantrópicas nesta partida.");
	%this.criarCarta(14, 3, "Verde", "ALMIRANTE", "aca_i_3", "Você ganhará 6 pontos se possuir 5 ou mais bases no mar.", "Cada jogador ganhará 6 pontos se possuir 5 ou mais bases no mar.");
	%this.criarCarta(15, 3, "Verde", "LIDER DELTA", "aca_a_1", "Você pode convocar um líder anfíbio com habilidades no nível 2.", "Todos podem convocar um líder anfíbio com habilidades no nível 2.");
		
	%this.criarCarta(16, 4, "Azul", "IMPERADOR", "especial", "Você ganhou 15 Imperiais e seu bônus de economia duplicou.", "Todos ganharam 15 Imperiais; bônus de economia vale 2 imperiais.");
	%this.criarCarta(17, 4, "Azul", "COLECIONADOR", "especial", "Você ganhou 1 conjunto de recursos.", "Todos ganharam 1 conjunto de recursos.");
	%this.criarCarta(18, 4, "Azul", "MARINHEIRO", "especial", "Você pode construir navios por apenas 2 imperiais.", "Todos podem construir navios por apenas 2 imperiais.");
	%this.criarCarta(19, 4, "Azul", "ENGENHEIRO", "especial", "Você pode construir bases por apenas 7 imperiais.", "Todos podem construir bases por apenas 7 imperiais."); 
	%this.criarCarta(20, 4, "Azul", "MAGNATA", "especial", "Você ganhou 20 Imperiais.", "Todos ganharam 20 Imperiais."); 
	
	%this.criarCarta(21, 5, "Roxo", "MONOPOLISTA", "especial", "Conquiste 50% dos Grupos deste planeta para receber +10 pontos.", "O jogador que conquistar 50% dos Grupos deste planeta receberá +10 pontos."); 
	%this.criarCarta(22, 5, "Roxo", "ECONOMISTA", "especial", "Cada Grupo que você possuir lhe dará 3 imperiais extra por rodada.", "Cada Grupo conquistado dá ao seu dono 3 imperiais extra por rodada.");
	%this.criarCarta(23, 5, "Roxo", "VELOCISTA", "especial", "Você pode fazer 6 movimentos por rodada.", "Todos podem fazer 6 movimentos por rodada."); 
	%this.criarCarta(24, 5, "Roxo", "MISSIONISTA", "especial", "Você recebeu 5 missões.", "Cada jogador recebeu 5 missões.");
	%this.criarCarta(25, 5, "Roxo", "OPORTUNISTA", "especial", "Cada um dos seus Objetivos vale 8 pontos, em vez de apenas 5.", "Para todos os jogadores, cada Objetivo completo vale 8 pontos.");
}

function pk_handler::criarJogo(%this)
{
	$pokerJogosNum++;
	%jogo = newSimObj("pk_jogo", $pokerJogosNum);
	%jogo.parent = %this;
	%jogo.initCartas();
	return %jogo;
}

function pk_handler::createClientActions(%this)
{
	%this.setClientActionsSimSet();
		
	%this.createClientAction(0, "sortear uma carta para si");
	%this.createClientAction(1, "dar Mesa ou fazer uma aposta");
	%this.createClientAction(2, "fugir, pagar ou aumentar a aposta");
	%this.createClientAction(3, "fugir ou pagar a aposta");
	%this.createClientAction(4, "FLOP: Sorteando uma carta para a mesa...");
	%this.createClientAction(5, "TURN: Sorteando uma carta para a mesa...");
	%this.createClientAction(6, "[CASO NÃO CALCULADO]");
}

function pk_handler::setClientActionsSimSet(%this)
{
	if(isObject(%this.clientActionsSimSet))
	{
		%this.clientActionsSimSet.clear();
		return;
	}
		
	%this.clientActionsSimSet = new SimSet();
}

function pk_handler::createClientAction(%this, %num, %txt)
{
	%act = new ScriptObject(){};
	%act.num = %num;
	%act.txt = %txt;
	
	%this.clientActionsSimSet.add(%act);
}


function clientCmdCriarPk_jogo(%blind, %apostaMax, %fichasIniciaisGlobal)
{
	%pk_jogo = $poker_handler.criarJogo();	
	%pk_jogo.clearHud();
	%pk_jogo.clearMaoUnicaHud();
	$poker_handler.clearCartas($playersNesteJogo);
	$poker_handler.clearPkPontos($playersNesteJogo);
	$poker_handler.clearPkMaoUnica($playersNesteJogo);
	%pk_jogo.resetCartasSorteio();
	%pk_jogo.resetPkBtns();
	%pk_jogo.inativarPkBtns();
	%pk_jogo.clearAval();
	
	%pk_jogo.blind = %blind;
	%pk_jogo.apostaMax = %apostaMax;
	%pk_jogo.fichasIniciaisGlobal = %fichasIniciaisGlobal;
	%pk_jogo.allInIndividual = %apostaMax * %blind * 3;
	%pk_jogo.setFichasForAll();
	%pk_jogo.zerarPot();
	%pk_jogo.pagarBlindInicial();
			
	%pk_jogo.addPlayer($mySelf);
	%pk_jogo.setMyAcadBKP();
	%pk_jogo.zerarMyPersona();
	
	$vendoPoker = false;
}

function pk_jogo::setFichasForAll(%this)
{
	$myPersona.pk_fichas -= %this.fichasIniciaisGlobal;
	clientAtualizarFichasDePokerTXTs();
	for(%i = 0; %i < $numDePlayersNestaPartida; %i++)
	{
		%player = %this.getPlayerPorOrdem(%i+1);
		%player.pk_fichas = %this.fichasIniciaisGlobal;
	}	
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
		
	if(!isObject(%player.pk_simMao))
		%player.pk_simMao = new SimSet();
		
	%this.simCartas.remove(%carta);
	%player.pk_simCartas.add(%carta);
	%player.pk_simMao.add(%carta);
	
	//echo("POKER - Carta dada para " @ %player.nome @ ": " @ %carta.desc);
}

function pk_jogo::avaliarMao(%this, %player)
{
	%this.ordenarMaoPorNaipe(%player);
	
	if(%this.getQuadra(%player))
	{
		%player.jogoFeito = "Quadra";
		%player.pk_pontos = 30;
		%player.quadra++;
		return;
	}
	
	if(%this.getFullHouse(%player))
	{
		%player.jogoFeito = "Full House";
		%player.pk_pontos = 20;
		%player.fullHouse++;
		return;
	}
	
	if(%this.getCores(%player))
	{
		%player.jogoFeito = "Cores";
		%player.pk_pontos = 15;
		%player.cores++;
		return;
	}
	
	if(%this.getTrinca(%player))
	{
		%player.jogoFeito = "Trinca";
		%player.pk_pontos = 10;
		%player.trinca++;
		return;
	}
	
	if(%this.getDoisPares(%player))
	{
		%player.jogoFeito = "Dois Pares";
		%player.pk_pontos = 5;
		%player.doisPares++;
		return;
	}
	
	if(%this.getPar(%player))
	{
		%player.jogoFeito = "Um Par";
		%player.pk_pontos = 2;
		%player.umPar++;
		return;
	}
	
	%player.jogoFeito = "Nenhum";
	%player.pk_pontos = 0;
}

function pk_jogo::ordenarMaoPorNaipe(%this, %player)
{
	%this.setPlayerSimCartasPorOrdem(%player);
	
	for (%i = 0; %i < %player.pk_simCartas.getCount(); %i++)
	{
		for (%j = 1; %j < %player.pk_simCartas.getCount() - %i; %j++)
		{
			if(%player.pk_simCartasPorOrdem.getObject(%j-1).num > %player.pk_simCartasPorOrdem.getObject(%j).num)
				%player.pk_simCartasPorOrdem.reorderChild(%player.pk_simCartasPorOrdem.getObject(%j), %player.pk_simCartasPorOrdem.getObject(%j-1));
		}
	}
}

function pk_jogo::resetPlayerSimCartasPorOrdem(%this, %player)
{
	if(isObject(%player.pk_simCartasPorOrdem))
	{
		%player.pk_simCartasPorOrdem.clear();
		return;	
	}
		
	%player.pk_simCartasPorOrdem = new SimSet();
}

function pk_jogo::setPlayerSimCartasPorOrdem(%this, %player)
{
	%this.resetPlayerSimCartasPorOrdem(%player);
	
	for(%i = 0; %i < %player.pk_simCartas.getCount(); %i++)
	{
		%carta = %player.pk_simCartas.getObject(%i);
		%player.pk_simCartasPorOrdem.add(%carta);	
	}
}


function pk_jogo::getQuadra(%this, %player)
{
	for (%i = 0; %i < %player.pk_simCartasPorOrdem.getCount(); %i++)
	{
		%carta[%i] = %player.pk_simCartasPorOrdem.getObject(%i);
	}
	
	if(!isObject(%carta[3]))
		return false;
		
	if(%carta[0].num == %carta[1].num && %carta[0].num == %carta[2].num	&& %carta[0].num == %carta[3].num)
		return true;
	
	if(!isObject(%carta[4]))
		return false;	
		
	if(%carta[1].num == %carta[2].num && %carta[1].num == %carta[3].num	&& %carta[1].num == %carta[4].num)
		return true;
		
	return false;
}

function pk_jogo::getFullHouse(%this, %player)
{
	if(!%this.getTrinca(%player))
		return false;
			
	for (%i = 0; %i < %player.pk_simCartasPorOrdem.getCount(); %i++)
	{
		%carta[%i] = %player.pk_simCartasPorOrdem.getObject(%i);
	}
	
	if(!isObject(%carta[4]))
		return false;			
			
	if((%carta[0].num != %carta[2].num) && (%carta[0].num == %carta[1].num))
		return true;
	
	if((%carta[2].num != %carta[4].num) && (%carta[3].num == %carta[4].num))
		return true;
	
	return false;
}

function pk_jogo::getCores(%this, %player)
{
	if(!isObject(%player.pk_simCartas.getObject(4)))
		return false;	
	
	for (%i = 0; %i < %player.pk_simCartasPorOrdem.getCount(); %i++)
	{
		%carta1 = %player.pk_simCartasPorOrdem.getObject(%i);
		for (%j = 0; %j < %player.pk_simCartasPorOrdem.getCount(); %j++)
		{
			%carta2 = %player.pk_simCartasPorOrdem.getObject(%j);
			if(%carta1 != %carta2)
			{
				if(%carta1.num == %carta2.num || !isObject(%carta1) || !isObject(%carta2))
					return false;
			}
		}
	}
			
	return true;
}

function pk_jogo::getTrinca(%this, %player)
{
	for (%i = 0; %i < %player.pk_simCartasPorOrdem.getCount(); %i++)
	{
		%carta[%i] = %player.pk_simCartasPorOrdem.getObject(%i);
	}
	
	if(!isObject(%carta[2]))
		return false;	
		
	if(%carta[0].num == %carta[1].num && %carta[0].num == %carta[2].num)
		return true;
	
	if(!isObject(%carta[3]))
		return false;	
		
	if(%carta[1].num == %carta[2].num && %carta[1].num == %carta[3].num)
		return true;
	
	if(!isObject(%carta[4]))
		return false;	
		
	if(%carta[2].num == %carta[3].num && %carta[2].num == %carta[4].num)
		return true;
		
	return false;
}

function pk_jogo::getDoisPares(%this, %player)
{
	for (%i = 0; %i < %player.pk_simCartasPorOrdem.getCount(); %i++)
	{
		%carta[%i] = %player.pk_simCartasPorOrdem.getObject(%i);
	}	
	
	if((!isObject(%carta[3])))
		return false;
	
	if((%carta[0].num != %carta[1].num) && (%carta[1].num != %carta[2].num))
		return false;
		
	if((%carta[0].num == %carta[1].num) && (%carta[2].num == %carta[3].num))
		return true;
		
	if((%carta[0].num == %carta[2].num) && (%carta[1].num == %carta[3].num))
		return true;
		
	if((%carta[0].num == %carta[3].num) && (%carta[1].num == %carta[2].num))
		return true;
	
	if(!isObject(%carta[4]))
		return false;
		
	if((%carta[2].num != %carta[3].num) && (%carta[3].num != %carta[4].num))
		return false;
		
	return true;
}

function pk_jogo::getPar(%this, %player)
{
	for (%i = 0; %i < %player.pk_simCartasPorOrdem.getCount(); %i++)
	{
		%carta[%i] = %player.pk_simCartasPorOrdem.getObject(%i);
	}	
	
	if((!isObject(%carta[0])))
		return false;
	
	for(%i = 1; %i < 5; %i++)
	{
		if((!isObject(%carta[%i]))){
			return false;	
		} else {
			if(%carta[%i-1].num == %carta[%i].num)
				return true;	
		}
	}
	
	return false;
}

function pk_jogo::addPlayer(%this, %player)
{
	if(!isObject(%this.simPlayers))
		%this.simPlayers = new Simset();
		
	%this.simPlayers.add(%player);
	%player.pk_jogo = %this;
}


function pk_jogo::darCartaAMesa(%this, %carta)
{
	if(!isObject(%this.simMesa))
		%this.simMesa = new Simset();
		
	%this.simMesa.add(%carta);
	%this.simCartas.remove(%carta);
	
	for(%i = 0; %i < %this.simPlayers.getCount(); %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%player.pk_simCartas.add(%carta);	
	}
	
	clientCmdForceInPokerPlay();
	PalcoTurnoTimer.setPokerTimer();
	//echo("POKER - Carta sorteada para a mesa: " @ %carta.desc);
}

function pk_jogo::flop(%this)
{
	%carta = %this.sortearCarta();
	%this.darCartaAMesa(%carta);
	%this.flops++;
	clientCmdZerarPkLastAction();
	%this.popularMesaHud();
}


function clientCmdAdicionarCartaPokerAMesa(%cartaId)
{
	%carta = $mySelf.pk_jogo.getCartaPorId(%cartaId);
	$mySelf.pk_jogo.darCartaAMesa(%carta);
	$mySelf.pk_jogo.avaliarMao($mySelf);
	$mySelf.pk_jogo.printAvalMao();
	$mySelf.pk_jogo.printTodasAsCartas();
	clientCmdZerarPkLastAction();
	$mySelf.pk_jogo.zerarApostaAtual();
	$mySelf.pk_jogo.popularMesaHud();
	$mySelf.pk_jogo.setPk_btns();
}

function pk_jogo::getCartaPorId(%this, %cartaId)
{
	for(%i = 0; %i < %this.parent.simCartasBP.getCount(); %i++)
	{
		%carta = %this.simCartas.getObject(%i);
		if(%carta.id == %cartaId)
			return %carta;
	}
}

function clientCmdReceberCartaPoker(%cartaId, %cartaNum)
{
	clientCmdApagarCartaPoker(%cartaNum);
	%carta = $mySelf.pk_jogo.getCartaPorId(%cartaId);
	$mySelf.pk_jogo.darCartaAoPlayer($mySelf, %carta);
	$mySelf.pk_jogo.avaliarMao($mySelf);
	$mySelf.pk_jogo.printAvalMao();
	$mySelf.pk_jogo.avaliarMaoUnica($mySelf);
	$mySelf.pk_jogo.printMaoUnica();
	$mySelf.pk_jogo.printTodasAsCartas();
	$mySelf.pk_jogo.setPk_btns();
}

function pk_jogo::printAvalMao(%this)
{
	%this.clearAval();
	switch ($mySelf.pk_pontos)
	{
		case 0: pk_aval_nenhum.setVisible(true);
		case 2: pk_aval_umPar.setVisible(true);
		case 5: pk_aval_doisPares.setVisible(true);
		case 10: pk_aval_trinca.setVisible(true);
		case 15: pk_aval_cores.setVisible(true);
		case 20: pk_aval_fullHouse.setVisible(true);
		case 30: pk_aval_quadra.setVisible(true);
	}
}

function pk_jogo::clearAval(%this)
{
	pk_aval_nenhum.setVisible(false);
	pk_aval_umPar.setVisible(false);
	pk_aval_doisPares.setVisible(false);
	pk_aval_trinca.setVisible(false);
	pk_aval_cores.setVisible(false);
	pk_aval_fullHouse.setVisible(false);
	pk_aval_quadra.setVisible(false);	
}

function pk_jogo::printTodasAsCartas(%this)
{
	%this.printCartasDaMesa();
	%this.printCartasDaMao();
}

function pk_jogo::printCartasDaMesa(%this)
{
	if(!isObject(%this.simMesa))
		return;
	
	for(%i = 0; %i < %this.simMesa.getCount(); %i++)
	{
		%carta = %this.simMesa.getObject(%i);
		%this.printCartaFull(%carta, %i, true);
	}
}

function pk_jogo::printCartaFull(%this, %carta, %pos, %mesaDesc)
{
	%this.printCarta(%carta, %pos);
	%this.printCartaDescription(%carta, %pos, %mesaDesc);	
}

function pk_jogo::printCarta(%this, %carta, %pos)
{
	%cartaNoHud = %this.getCartaNoHud(%pos);
	%cartaNoHud.bitmap = "game/data/images/pk/pk_carta_" @ %carta.id;
	%cartaNoHud.setVisible(true);
}

function pk_jogo::getCartaNoHud(%this, %pos)
{
	%eval = "%cartaNoHud = pk_cartaHud_" @ %pos @ ";";	
	eval(%eval);
	return %cartaNoHud;	
}

function pk_jogo::printCartaDescription(%this, %carta, %pos, %mesaDesc)
{
	%descriptionNoHud = %this.getDescriptionNoHud(%pos);
	if(%mesaDesc){
		%descriptionNoHud.text = %carta.nome @ " >>> " @ %carta.toolTipMesa;
	} else {
		%descriptionNoHud.text = %carta.nome @ " >>> " @ %carta.toolTip;
	}
	%descriptionNoHud.setVisible(true);
	
	%descriptionToolTipNoHud = %this.getDescriptionToolTipNoHud(%pos);
	%descriptionToolTipNoHud.bitmap = "game/data/images/pk/poker_toolTip_" @ %carta.num;
	%descriptionToolTipNoHud.setVisible(true);
}

function pk_jogo::getDescriptionNoHud(%this, %pos)
{
	%eval = "%descriptionNoHud = pk_descHud_" @ %pos @ "_txt;";	
	eval(%eval);
	return %descriptionNoHud;	
}

function pk_jogo::getDescriptionToolTipNoHud(%this, %pos)
{
	%eval = "%descriptionToolTipNoHud = pk_toolTip_" @ %pos @ ";";	
	eval(%eval);
	return %descriptionToolTipNoHud;	
}

function pk_jogo::printCartasDaMao(%this)
{
	for(%i = 0; %i < $mySelf.pk_simMao.getCount(); %i++)
	{
		%carta = $mySelf.pk_simMao.getObject(%i);
		%this.printCartaFull(%carta, %i+2);
	}
}



function pk_jogo::avaliarMaoUnica(%this, %player)
{
	%carta1 = %player.pk_simMao.getObject(0);
	%carta2 = %player.pk_simMao.getObject(1);
		
	if(%carta1.num != %carta2.num)
	{
		%this.avaliarMaoColorida(%player);
		return;
	}
	
	%this.avaliarParesIniciais(%player);
	%this.avaliarTrincasUnicas(%player);
}

function pk_jogo::avaliarParesIniciais(%this, %player)
{
	%carta1 = %player.pk_simMao.getObject(0);
	%carta2 = %player.pk_simMao.getObject(1);
	
	if(%carta1.num != %carta2.num)
		return;
	
	
	if(%this.verificarParUnico(1, 2, %player))
	{
		%player.maoUnica = "Assassino";
		return;
	}
	
	if(%this.verificarParUnico(1, 3, %player))
	{
		%player.maoUnica = "Fortaleza";
		return;
	}
	
	if(%this.verificarParUnico(2, 3, %player))
	{
		%player.maoUnica = "Tropa de Elite";
		return;
	}
	
	if(%this.verificarParUnico(2, 4, %player))
	{
		%player.maoUnica = "Rambo";
		return;
	}
	
	if(%this.verificarParUnico(3, 4, %player))
	{
		%player.maoUnica = "Fortaleza";
		return;
	}
		
	if(%this.verificarParUnico(5, 6, %player))
	{
		%player.maoUnica = "Ninja";
		return;
	}
	
	if(%this.verificarParUnico(5, 7, %player))
	{
		%player.maoUnica = "Minerador Furtivo";
		return;
	}
		
	if(%this.verificarParUnico(6, 7, %player))
	{
		%player.maoUnica = "Versátil";
		return;
	}
	
	if(%this.verificarParUnico(9, 12, %player))
	{
		%player.maoUnica = "Colonizador";
		return;
	}
	
	if(%this.verificarParUnico(10, 12, %player))
	{
		%player.maoUnica = "Colonizador";
		return;
	}
	
	if(%this.verificarParUnico(11, 12, %player))
	{
		%player.maoUnica = "Colonizador";
		return;
	}
	
	if(%this.verificarParUnico(13, 14, %player))
	{
		%player.maoUnica = "Grande Irmão";
		return;
	}
	
	if(%this.verificarParUnico(13, 16, %player))
	{
		%player.maoUnica = "Informante";
		return;
	}
	
	if(%this.verificarParUnico(14, 15, %player))
	{
		%player.maoUnica = "Autossuficiente";
		return;
	}
	
	if(%this.verificarParUnico(14, 16, %player))
	{
		%player.maoUnica = "Amigável";
		return;
	}
	
	if(%this.verificarParUnico(17, 20, %player))
	{
		%player.maoUnica = "Podre de Rico";
		return;
	}	
		
	if(%this.verificarParUnico(18, 19, %player))
	{
		%player.maoUnica = "Especialista";
		return;
	}
	
	if(%this.verificarParUnico(18, 20, %player))
	{
		%player.maoUnica = "Mestre dos Mares";
		return;
	}
	
	if(%this.verificarParUnico(19, 20, %player))
	{
		%player.maoUnica = "Construtor";
		return;
	}
}

function pk_jogo::avaliarTrincasUnicas(%this, %player)
{
	if(%this.verificarTrincaColorida(1, 2, 3, %player))
	{
		%player.maoUnica = "Senhor das Armas";	
		%player.maoUnicaTier = 2;
		return;
	}
	
	if(%this.verificarTrincaColorida(1, 2, 4, %player))
	{
		%player.maoUnica = "Exterminador";	
		%player.maoUnicaTier = 2;
		return;
	}
	
	if(%this.verificarTrincaColorida(1, 3, 4, %player))
	{
		%player.maoUnica = "Grande Fortaleza";	
		%player.maoUnicaTier = 2;
		return;
	}
	
	if(%this.verificarTrincaColorida(2, 3, 4, %player))
	{
		%player.maoUnica = "Chuck Norris";	
		%player.maoUnicaTier = 2;
		return;
	}
	
	if(%this.verificarTrincaColorida(5, 6, 7, %player))
	{
		%player.maoUnica = "Dançarino das Sombras";	
		%player.maoUnicaTier = 2;
		return;
	}
	
	if(%this.verificarTrincaColorida(5, 6, 8, %player))
	{
		%player.maoUnica = "Ronin";	
		%player.maoUnicaTier = 2;
		return;
	}
	
	if(%this.verificarTrincaColorida(6, 7, 8, %player))
	{
		%player.maoUnica = "Trinca Logística";	
		%player.maoUnicaTier = 2;
		return;
	}
		
	if(%this.verificarTrincaColorida(9, 10, 12, %player))
	{
		%player.maoUnica = "Formiga Atômica";	
		%player.maoUnicaTier = 2;
		return;
	}
	
	if(%this.verificarTrincaColorida(10, 11, 12, %player))
	{
		%player.maoUnica = "Formiga Atômica";	
		%player.maoUnicaTier = 2;
		return;
	}
	
	if(%this.verificarTrincaColorida(13, 14, 15, %player))
	{
		%player.maoUnica = "Rei no Castelo";	
		%player.maoUnicaTier = 2;
		return;
	}
	
	if(%this.verificarTrincaColorida(14, 15, 16, %player))
	{
		%player.maoUnica = "Rei no Castelo";	
		%player.maoUnicaTier = 2;
		return;
	}
	
	if(%this.verificarTrincaColorida(13, 14, 16, %player))
	{
		%player.maoUnica = "Grande Pai";	
		%player.maoUnicaTier = 2;
		return;
	}
		
	if(%this.verificarTrincaColorida(17, 18, 19, %player))
	{
		%player.maoUnica = "MacGyver";	
		%player.maoUnicaTier = 2;
		return;
	}	
	
	if(%this.verificarTrincaColorida(17, 18, 20, %player))
	{
		%player.maoUnica = "Navegador Independente";	
		%player.maoUnicaTier = 2;
		return;
	}	
	
	if(%this.verificarTrincaColorida(17, 19, 20, %player))
	{
		%player.maoUnica = "Senhor Feudal";	
		%player.maoUnicaTier = 2;
		return;
	}	
	
	if(%this.verificarTrincaColorida(18, 19, 20, %player))
	{
		%player.maoUnica = "MacGyver";	
		%player.maoUnicaTier = 2;
		return;
	}	
}


function pk_jogo::avaliarMaoColorida(%this, %player)
{
	%carta1 = %player.pk_simMao.getObject(0);
	%carta2 = %player.pk_simMao.getObject(1);
	%carta3 = %player.pk_simMao.getObject(2);
	
	if((%carta1.id == 11 && %carta2.id == 13) || (%carta1.id == 13 && %carta2.id == 11))
	{
		%player.maoUnica = "Pior mão inicial: Milícia";	
		return;
	}
	
	if(%this.verificarTrincaColorida(2, 5, 13, %player))
	{
		%player.maoUnica = "Cavalo de Tróia";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(2, 6, 10, %player))
	{
		%player.maoUnica = "Assaltante";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(2, 8, 13, %player))
	{
		%player.maoUnica = "007";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(2, 8, 18, %player))
	{
		%player.maoUnica = "Posseidon";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(2, 9, 18, %player))
	{
		%player.maoUnica = "Grande Frota Naval";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(2, 12, 20, %player))
	{
		%player.maoUnica = "Atropelador";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(3, 5, 12, %player))
	{
		%player.maoUnica = "Muralha Invisível";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(3, 9, 14, %player))
	{
		%player.maoUnica = "Governo Próspero";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(3, 12, 18, %player))
	{
		%player.maoUnica = "Grande Explorador";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(3, 14, 17, %player))
	{
		%player.maoUnica = "Autônomo";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(4, 5, 13, %player))
	{
		%player.maoUnica = "Agente Secreto";
		%player.maoUnicaTier = 2;
		return;	
	}	
	
	if(%this.verificarTrincaColorida(4, 8, 12, %player))
	{
		%player.maoUnica = "Velocirraptor";
		%player.maoUnicaTier = 2;
		return;	
	}
		
	if(%this.verificarTrincaColorida(4, 5, 14, %player))
	{
		%player.maoUnica = "Monge";
		%player.maoUnicaTier = 2;
		return;	
	}
			
	if(%this.verificarTrincaColorida(4, 8, 18, %player))
	{
		%player.maoUnica = "Marines";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(4, 15, 18, %player))
	{
		%player.maoUnica = "Frota Naval";
		%player.maoUnicaTier = 2;
		return;	
	}
		
	if(%this.verificarTrincaColorida(5, 9, 15, %player))
	{
		%player.maoUnica = "Almirante Furtivo";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(5, 15, 19, %player))
	{
		%player.maoUnica = "Almirante Furtivo";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(6, 11, 18, %player))
	{
		%player.maoUnica = "Golfinho";
		%player.maoUnicaTier = 2;
		return;	
	}	
		
	if(%this.verificarTrincaColorida(6, 12, 19, %player))
	{
		%player.maoUnica = "Malabarista";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(6, 15, 18, %player))
	{
		%player.maoUnica = "Cardume";
		%player.maoUnicaTier = 2;
		return;	
	}	
		
	if(%this.verificarTrincaColorida(8, 15, 18, %player))
	{
		%player.maoUnica = "Frota Naval";
		%player.maoUnicaTier = 2;
		return;	
	}
	
	if(%this.verificarTrincaColorida(9, 16, 20, %player))
	{
		%player.maoUnica = "Investidor";
		%player.maoUnicaTier = 2;
		return;	
	}
}

function pk_jogo::verificarParUnico(%this, %id1, %id2, %player)
{
	%carta1 = %player.pk_simMao.getObject(0);
	%carta2 = %player.pk_simMao.getObject(1);
		
	if((%carta1.id == %id1 || %carta1.id == %id2) && (%carta2.id == %id1 || %carta2.id == %id2))
	{
		return true;	
	}
	
	return false;
}

function pk_jogo::verificarTrincaColorida(%this, %id1, %id2, %id3, %player)
{
	%carta1 = %player.pk_simMao.getObject(0);
	%carta2 = %player.pk_simMao.getObject(1);
	%carta3 = %player.pk_simMao.getObject(2);
	
	if((%carta1.id == %id1 || %carta1.id == %id2 || %carta1.id == %id3) && (%carta2.id == %id1 || %carta2.id == %id2 || %carta2.id == %id3) && (%carta3.id == %id1 || %carta3.id == %id2 || %carta3.id == %id3))
	{
		return true;	
	}
	
	return false;
}


function pk_jogo::printMaoUnica(%this)
{
	%this.clearMaoUnicaHud();
	
	if($mySelf.maoUnica $= "")
		return;
		
	if($mySelf.maoUnicaTier == 2)
	{
		pk_maoUnica_decal_2.setVisible(true);	
		pk_maoUnica_txt_2.setVisible(true);	
		pk_maoUnica_txt_2.text = ">>>>>  " @ $mySelf.maoUnica @ "  <<<<<";
		return;
	}
	
	pk_maoUnica_decal_1.setVisible(true);
	pk_maoUnica_txt_1.setVisible(true);	
	pk_maoUnica_txt_1.text = ">>>>>  " @ $mySelf.maoUnica @ "  <<<<<";
}

function pk_jogo::clearMaoUnicaHud(%this)
{
	pk_maoUnica_decal_1.setVisible(false);	
	pk_maoUnica_decal_2.setVisible(false);	
	pk_maoUnica_txt_1.setVisible(false);	
	pk_maoUnica_txt_2.setVisible(false);	
}

function pk_jogo::clearHud(%this)
{
	for(%i = 0; %i < 5; %i++)
	{
		%descriptionNoHud = %this.getDescriptionNoHud(%i);
		%descriptionNoHud.setVisible(false);
		%descriptionToolTipNoHud = %this.getDescriptionToolTipNoHud(%i);
		%descriptionToolTipNoHud.setVisible(false);
		%cartaNoHud = %this.getCartaNoHud(%i);
		%cartaNoHud.setVisible(false);
	}
}

function pk_jogo::atualizarPagarBtn(%this)
{
	%myApostaAtual = $mySelf.pk_apostaAtual;
	if(firstWord($mySelf.pk_lastAction) $= "Aposta:")
	{
		%myApostaAtual = getWord($mySelf.pk_lastAction, 1);	
	}
	pk_pagar_btn.setText("              " @ %this.apostaAtual - %myApostaAtual);
}

function clientCmdAtualizarApostaAtual(%val)
{
	$mySelf.pk_jogo.apostaAtual = %val;
	$mySelf.pk_jogo.atualizarPagarBtn();
}

function pk_jogo::zerarApostaAtual(%this)
{
	echo("ZERANDO APOSTA ATUAL!!!");
	%this.apostaAtual = "";
	for(%i = 0; %i < $numDePlayersNestaPartida; %i++)
	{
		%player = %this.getPlayerPorOrdem(%i+1);
		%player.pk_apostaAtual = "";
	}
}

initPokerSys();

///////////////////////


function clientAskSortearPkCarta(%num)
{
	commandToServer('SortearCartaPoker', %num);	
	clientPopPk_sorteioGui();
}

function clientCmdApagarCartaPoker(%num)
{
	%eval =	"%carta = pk_cartaSorteio_" @ %num @ ";";
	eval(%eval);
	
	%carta.setVisible(false);
}

function pk_jogo::resetCartasSorteio(%this)
{
	for(%i = 1; %i < 21; %i++)
	{
		%eval =	"%carta = pk_cartaSorteio_" @ %i @ ";";
		eval(%eval);
	
		%carta.setVisible(true);	
	}
}

function clientPushPokerGui()
{
	schedule(500, 0, "setObjetivosZoom");	
	$vendoPoker = true;
	pokerImperial_btn.setStateOn(true);
	$mySelf.pk_jogo.popularMesaHud();
	canvas.pushDialog(pokerGui);
	$mySelf.pk_jogo.verificarEsperandoFlop();
	$mySelf.pk_jogo.verificarEsperandoTurn();
	$mySelf.pk_jogo.setPk_btns();
	$mySelf.pk_jogo.atualizarGMBOT();
}
function clientPopPokerGui()
{
	canvas.popDialog(pokerGui);
	setNormalZoom();
	$vendoPoker = false;
	pokerImperial_btn.setStateOn(false);
	clientAtualizarEstatisticas();
}

function clientPushPk_sorteioGui()
{
	canvas.pushDialog(pk_sorteioGui);
	//clientPlayMyTurnSound();
}
function clientPopPk_sorteioGui()
{
	canvas.popDialog(pk_sorteioGui);	
}

function clientCmdInicializarPkTurno()
{
	$mySelf.pk_jogo.popularMesaHud();
	$mySelf.pk_jogo.inativarPkBtns();
	$mySelf.pk_jogo.atualizarGMBOT();
}

function pk_jogo::setPk_btns(%this)
{
	%this.verificarEsperandoTurn();
	if(%this.notInPokerPlay)
	{
		%this.inativarPkBtns();
		return;	
	}
	if($mySelf != $jogadorDaVez)
	{
		%this.inativarPkBtns();
		return;	
	}
	if($mySelf.pk_simCartas.getCount() < 3)
	{
		%this.inativarPkBtns();
		return;	
	}
	if(%this.esperandoFlop)
	{
		%this.inativarPkBtns();
		return;		
	}	
	if(%this.esperandoTurn)
	{
		%this.inativarPkBtns();
		return;		
	}
	
	%this.resetPkBtns();
}

function pk_jogo::setEsperandoTurn(%this, %val)
{
	%this.esperandoTurn = %val;	
}

function pk_jogo::verificarEsperandoTurn(%this)
{
	%this.setEsperandoTurn(%this.getEsperandoTurn());
}

function pk_jogo::getEsperandoTurn(%this)
{
	if(!isObject(%this.simMesa))
		return false;
			
	if($rodadaAtual != 1)
		return false;
			
	if(!%this.forceEsperandoTurn)
		return false;
		
	%lastPlayer = %this.getUltimoJogador();
	if($jogadorDaVez != %lastPlayer)
		return false;
			
	return true;
}

function pk_jogo::setEsperandoFlop(%this, %val)
{
	%this.esperandoFlop = %val;	
}

function pk_jogo::verificarEsperandoFlop(%this)
{
	%this.setEsperandoFlop(%this.getEsperandoFlop());
}

function pk_jogo::getEsperandoFlop(%this)
{
	if(isObject(%this.simMesa))
		return false;
	
	if($rodadaAtual > 0)
		return false;
			
	if(!%this.forceEsperandoFlop)
		return false;
		
	%lastPlayer = %this.getUltimoJogador();
	if($jogadorDaVez != %lastPlayer)
		return false;
		
	return true;
}

function clientCmdForceEsperandoFlop()
{
	$mySelf.pk_jogo.forceEsperandoFlop = true;	
	$mySelf.pk_jogo.verificarEsperandoFlop();
	$mySelf.pk_jogo.atualizarGMBOT();
}

function clientCmdForceEsperandoTurn()
{
	$mySelf.pk_jogo.notInPokerPlay = false;
	$mySelf.pk_jogo.forceEsperandoTurn = true;
	$mySelf.pk_jogo.verificarEsperandoTurn();
	$mySelf.pk_jogo.atualizarGMBOT();
}


function pk_jogo::getUltimoJogador(%this)
{
	%eval = "%ultimoPlayer = $player" @ $numDePlayersNestaPartida @ ";";
	eval(%eval);
	
	return %ultimoPlayer;
}

function pk_jogo::inativarPkBtns(%this)
{
	pk_aumentar_btn.setActive(false);	
	pk_pagar_btn.setActive(false);
	pk_pagar_btn.setVisible(false);
	pk_fugir_btn.setActive(false);
	pk_mesa_btn.setActive(false);
	pk_mesa_btn.setVisible(true);
	pk_apostar_btn.setActive(false);
}

function pk_jogo::resetPkBtns(%this)
{
	if($mySelf.pk_apostaAtual == %this.apostaAtual || %this.apostaAtual $= "")
	{
		//está na minha vez, posso apostar ou dar mesa, ninguém antes de mim apostou.
		pk_aumentar_btn.setVisible(false);	
		pk_pagar_btn.setVisible(false);	
		
		pk_mesa_btn.setVisible(true);
		pk_apostar_btn.setVisible(true);
		
		pk_mesa_btn.setActive(true);
		pk_apostar_btn.setActive(true);
		
		pk_fugir_btn.setActive(false);
		
		if(%this.getAlgumAllIn() && %this.apostaAtual $= "")
		{
			pk_apostar_btn.setActive(false);
		}
		
		return;
	}
	
	if($mySelf.pk_apostaAtual < $mySelf.pk_jogo.apostaAtual)
	{
		//está na minha vez, posso pagar, aumentar, ou fugir.
		pk_aumentar_btn.setVisible(true);	
		pk_pagar_btn.setVisible(true);	
		
		pk_mesa_btn.setVisible(false);
		pk_apostar_btn.setVisible(false);
		
		%this.setAumentarBtn();
				
		pk_pagar_btn.setActive(true);	
		
		%this.atualizarPagarBtn();
		
		pk_fugir_btn.setActive(true);	
	}
}

function pk_jogo::setAumentarBtn(%this)
{
	if(%this.getAlgumAllIn())
	{
		pk_aumentar_btn.setActive(false);
		return;
	}
	
	if(%this.apostaAtual < (%this.blind * %this.apostaMax))
	{
		pk_aumentar_btn.setActive(true);
		return;
	}
	
	if($rodadaAtual >= 3)
	{
		pk_aumentar_btn.setActive(true);
		return;	
	}
	
	pk_aumentar_btn.setActive(false);
}

function pk_jogo::apostar(%this, %blinds)
{
	%aposta = %blinds * %this.blind;
	
	if($mySelf.pk_fichas < %aposta)
		return;
	
	commandToServer('pk_apostar', %blinds);
}

function pk_jogo::darMesa(%this)
{	
	%this.inativarPkBtns();
	commandToServer('pk_darMesa');
}

function pk_jogo::fugir(%this)
{	
	%this.inativarPkBtns();
	clientPopMsgBoxConfirmarFugir();
	commandToServer('pk_fugir');
}

function pk_jogo::setPokerImperialBtn(%this)
{
	investirRecursos_btn.setVisible(false);
	pokerImperial_btn.setVisible(true);
}

function clientUnsetPokerImperialBtn()
{
	investirRecursos_btn.setVisible(true);
	pokerImperial_btn.setVisible(false);
}

function clientPokerEsc()
{
	if(!$mySelf.pk_jogo.notInPokerPlay)
		return;
	
	clientPopPokerGui();
}


function clientCmdVoltarDoPokerProJogo()
{
	clientPopPokerGui();
	clientCmdForceNotInPokerPlay();
	palcoTurnoTimer.iniciarTimer();
}

function clientCmdVoltarDoJogoProPoker()
{
	clientPushPokerGui();
	$forcePokerGui = true;
	schedule(2500, 0, "clientZerarForcePokerGui");
}

function clientZerarForcePokerGui()
{
	$forcePokerGui = false;
}

function clientCmdPk_firstStart()
{
	clientPushPokerGui();
	
	if($mySelf != $jogadorDavez)
	{
		$mySelf.pk_jogo.inativarPkBtns();
		return;
	}
	
	clientPushPk_sorteioGui();
}	

function clientCmdPk_river()
{
	clientCmdZerarPkLastAction();
	$mySelf.pk_jogo.zerarApostaAtual();
	$mySelf.pk_jogo.popularMesaHud();
	$mySelf.pk_jogo.setPk_btns();
	$mySelf.pk_jogo.notInPokerPlay = false;
	$mySelf.pk_jogo.forceRiver = true;
	palcoTurnoTimer.iniciarTimer();
	$mySelf.pk_jogo.atualizarGMBOT();
	
	
	schedule(500, 0, "clientPushPokerGui");
	
	if($mySelf != $jogadorDaVez)
		return;
	
	schedule(1000, 0, "clientPushPk_sorteioGui");
}	

function clientCmdClearForceRiver()
{
	$mySelf.pk_jogo.forceRiver = false;
}

function clientCmdInicializarMyPkTurno()
{
	$mySelf.pk_jogo.popularMesaHud();
	$mySelf.pk_jogo.resetPkBtns();
	$mySelf.pk_jogo.atualizarGMBOT();
	clientPushPokerGui();
	clientPlayMyTurnSound();
			
	if(!isObject($mySelf.pk_simCartas))
	{
		clientPushPk_sorteioGui();
		return;
	}
	
	if($mySelf.pk_simCartas.getCount() == 5)
		return;
	
	if($mySelf.pk_simCartas.getCount() == 0 || $mySelf.pk_simCartas.getCount() == 1)
	{
		clientPushPk_sorteioGui();
		return;
	}
}

function clientCmdInicializarPkTurno()
{
	$mySelf.pk_jogo.popularMesaHud();
	$mySelf.pk_jogo.inativarPkBtns();
	$mySelf.pk_jogo.piscarJogadorDaVez();
	clientCmdForceInPokerPlay();
	clientPushPokerGui();
}

function pk_jogo::showJogadorDaVez(%this)
{
	%this.clearMesaPlayersAtivos();
	%eval = "pk_playerAtivo_" @ $jogadorDaVez.id @ ".setvisible(true);";
	eval(%eval);
	%this.mostrandoJogadorDaVez = true;
}

function pk_jogo::clearMesaPlayersAtivos(%this)
{
	pk_playerAtivo_player1.setvisible(false);
	pk_playerAtivo_player2.setvisible(false);
	pk_playerAtivo_player3.setvisible(false);
	pk_playerAtivo_player4.setvisible(false);
	%this.mostrandoJogadorDaVez = false;
}

function clientClearMesaPlayersAtivos(%pk_jogo)
{
	%pk_jogo.clearMesaPlayersAtivos();
}

function clientShowJogadorDaVez(%pk_jogo)
{
	%pk_jogo.showJogadorDaVez();
}

function pk_jogo::piscarJogadorDaVez(%this)
{
	if(%this.esperandoFlop || %this.esperandoTurn)
	{
		cancel($pk_piscarPlayerShedule);
		$pk_piscarPlayerShedule = schedule(500, 0, "clientPiscarPkJogadorDaVez", %this);
		clientClearMesaPlayersAtivos(%this);
		%this.atualizarGMBOT();
		return;		
	}
	
	if(%this.mostrandoJogadorDaVez)
	{
		cancel($pk_piscarPlayerShedule);
		$pk_piscarPlayerShedule = schedule(500, 0, "clientPiscarPkJogadorDaVez", %this);
		clientClearMesaPlayersAtivos(%this);
		return;
	}
	
	cancel($pk_piscarPlayerShedule);
	$pk_piscarPlayerShedule = schedule(800, 0, "clientPiscarPkJogadorDaVez", %this);
	clientShowJogadorDaVez(%this);
}

function clientPiscarPkJogadorDaVez(%pk_jogo)
{
	%pk_jogo.piscarJogadorDaVez();	
}

function clientCmdPk_deuMesa()
{
	$jogadorDaVez.pk_lastAction = "MESA";
	%txt = $mySelf.pk_jogo.getLastActionTxtPorPlayer($jogadorDaVez);
	%txt.text = "MESA";
}

function pk_jogo::getLastActionTxtPorPlayer(%this, %player)
{
	%eval = "%txt = pk_gui_playerLastAction_" @ %player.num @ ";";
	eval(%eval);
	
	return %txt;
}

function pk_jogo::getNomeTxtPorPlayer(%this, %player)
{
	%eval = "%txt = pk_gui_playerNome_" @ %player.num @ ";";
	eval(%eval);
	
	return %txt;
}

function pk_jogo::getFichasTxtPorPlayer(%this, %player)
{
	%eval = "%txt = pk_gui_playerFichas_" @ %player.num @ ";";
	eval(%eval);
	
	return %txt;
}

function pk_jogo::getPlayerPorOrdem(%this, %i)
{
	%eval = "%player = $player" @ %i @ ";";
	eval(%eval);
	
	return %player;
}

function pk_jogo::popularMesaHud(%this)
{
	%this.clearMesaHud();
	
	for(%i = 0; %i < $numDePlayersNestaPartida; %i++)
	{
		%player = %this.getPlayerPorOrdem(%i+1);
		%lastActionTxt = %this.getLastActionTxtPorPlayer(%player);
		%nomeTxt = %this.getNomeTxtPorPlayer(%player);
		%fichasTxt = %this.getFichasTxtPorPlayer(%player);
		
		if(%player.pk_fugiu){
			%lastActionTxt.text = "FUGIU";
		} else {
			%lastActionTxt.text = %player.pk_lastAction;	
		}
		
		%nomeTxt.text = %player.nome;
		
		if(%player.pk_fichas > 0){
			%fichasTxt.text = %player.pk_fichas;
		} else {
			%fichasTxt.text = "ALL-IN";
		}
	}
}




function pk_jogo::clearMesaHud()
{
	for(%i = 0; %i < 4; %i++)
	{
		%eval = "%lastActionTxt = pk_gui_playerLastAction_" @ %i+1 @ ";";
		eval(%eval);
		%eval = "%nomeTxt = pk_gui_playerNome_" @ %i+1 @ ";";
		eval(%eval);
		%eval = "%fichasTxt = pk_gui_playerFichas_" @ %i+1 @ ";";
		eval(%eval);
				
		%lastActionTxt.text = "";
		%nomeTxt.text = "";
		%fichasTxt.text = "";
	}	
}


function clientCmdZerarPkLastAction()
{
	for(%i = 0; %i < $numDePlayersNestaPartida; %i++)
	{
		%player = $mySelf.pk_jogo.getPlayerPorOrdem(%i+1);
		%player.pk_lastAction = "";
	}	
}



//////////////////
//apostas:

function clientPkApostasBtnClick()
{
	$mySelf.pk_jogo.pushApostasFundo();	
}

function pk_jogo::pushApostasFundo(%this)
{
	%this.setApostasBtns();
	pk_gui_aposta_fundo.setvisible(true);	
}

function pk_jogo::popApostasFundo(%this)
{
	pk_gui_aposta_fundo.setvisible(false);	
}

function pk_jogo::setApostasBtns(%this)
{
	
	for(%i = 1; %i < 8; %i++)
	{
		%this.setApostaTxt(%i);
		%this.setApostaBtn(%i);
	}
	
	%this.setAllInBtn();
}

function pk_jogo::setApostaBtn(%this, %i)
{
	%eval = "%apostaBtn = pk_gui_aposta_btn_" @ %i @ ";";
	eval(%eval);
	
	%eval = "%apostaTxt = pk_gui_aposta_txt_" @ %i @ ";";
	eval(%eval);
	
	if(%apostaTxt.text > %this.apostaAtual && (%apostaTxt.text / %this.blind) <= %this.apostaMax)
	{
		%apostaBtn.setActive(true);
		return;
	}
	
	%apostaBtn.setActive(false);	
}

function pk_jogo::setApostaTxt(%this, %i)
{
	%eval = "%apostaTxt = pk_gui_aposta_txt_" @ %i @ ";";
	eval(%eval);
	
	switch(%i)
	{
		case 1: %apostaTxt.text = 1 * %this.blind;
		case 2: %apostaTxt.text = 2* %this.blind;
		case 3: %apostaTxt.text = 3 * %this.blind;
		case 4: %apostaTxt.text = 4 * %this.blind;
		case 5: %apostaTxt.text = 5 * %this.blind;
		case 6: %apostaTxt.text = 6 * %this.blind;
		case 7: %apostaTxt.text = $mySelf.pk_fichas;
	}	
}

function pk_jogo::setAllInBtn(%this)
{
	%allInVal = $mySelf.pk_fichas;
	%algumAllIn = %this.getAlgumAllIn();
	
	if($rodadaAtual > 2 && !%algumAllIn)
	{
		pk_gui_aposta_btn_7.setActive(true);
		return;
	}
	
	pk_gui_aposta_btn_7.setActive(false);
}

function pk_jogo::getAlgumAllIn(%this)
{
	for(%i = 0; %i < $numDePlayersNestaPartida; %i++)
	{
		%player = %this.getPlayerPorOrdem(%i+1);
		if(%player.pk_fichas == 0)
			return true;
	}
	
	return false;
}

function clientAskApostar(%blinds)
{
	$mySelf.pk_jogo.popApostasFundo();
	$mySelf.pk_jogo.inativarPkBtns();
	clientPushAguardeMsgBox();
	if(%blinds $= "ALLIN"){
		%blinds = $mySelf.pk_fichas / $mySelf.pk_jogo.blind;	
	} else {
		%aumento = (%blinds * $mySelf.pk_jogo.blind) - $mySelf.pk_apostaAtual;
		%blinds = %aumento / $mySelf.pk_jogo.blind;
	}
	commandToServer('pk_apostar', %blinds);	
}

function clientCmdPk_aposta(%val)
{
	$jogadorDaVez.pk_apostaAtual = %val + $jogadorDaVez.pk_apostaAtual;
	$jogadorDaVez.pk_lastAction = "Aposta: " @ $jogadorDaVez.pk_apostaAtual;
	$jogadorDaVez.pk_apostaAtualEmBlinds = (%val + $jogadorDaVez.pk_apostaAtual) / $mySelf.pk_jogo.blind;
	%lastActionTxt = $mySelf.pk_jogo.getLastActionTxtPorPlayer($jogadorDaVez);
	%lastActionTxt.text = $jogadorDaVez.pk_lastAction;
	
	$jogadorDaVez.pk_fichas -= %val;
	$mySelf.pk_jogo.atualizarPot(%val);
	
	%fichasTxt = $mySelf.pk_jogo.getFichasTxtPorPlayer($jogadorDaVez);
	%fichasTxt.text = $jogadorDaVez.pk_fichas;
	clientCmdAtualizarApostaAtual($jogadorDaVez.pk_apostaAtual);
	clientPopAguardeMsgBox();
	
	if($jogadorDaVez.pk_fichas == 0)
		%fichasTxt.text = "ALL-IN";
}

function clientPkPagarBtnClick()
{
	clientPushAguardeMsgBox();
	$mySelf.pk_jogo.inativarPkBtns();	
	commandToServer('pk_pagarAposta');
}

function clientCmdPk_pagarAposta(%val)
{
	$jogadorDaVez.pk_lastAction = "Aposta: " @ $mySelf.pk_jogo.apostaAtual;
	%lastActionTxt = $mySelf.pk_jogo.getLastActionTxtPorPlayer($jogadorDaVez);
	%lastActionTxt.text = $jogadorDaVez.pk_lastAction;
	
	$jogadorDaVez.pk_fichas -= %val;
	$mySelf.pk_jogo.atualizarPot(%val);
	
	%fichasTxt = $mySelf.pk_jogo.getFichasTxtPorPlayer($jogadorDaVez);
	%fichasTxt.text = $jogadorDaVez.pk_fichas;
	clientPopAguardeMsgBox();
	
	if($jogadorDaVez.pk_fichas == 0)
		%fichasTxt.text = "ALL-IN";
}

function pk_jogo::atualizarPot(%this, %val)
{
	%this.pot += %val;
	pk_gui_pot_txt.text = %this.pot;
}

function pk_jogo::zerarPot(%this, %val)
{
	%this.pot = 0;
	pk_gui_pot_txt.text = "0";
}

function pk_jogo::pagarBlindInicial(%this)
{
	for(%i = 0; %i < $numDePlayersNestaPartida; %i++)
	{
		%player = %this.getPlayerPorOrdem(%i+1);
		%player.pk_fichas -= %this.blind;
	}
	
	%pot = %this.blind * $playersNaSalaEmQueEstou;
	%this.atualizarPot(%pot);
	%this.popularMesaHud();
}




function clientCmdForceInPokerPlay()
{
	$mySelf.pk_jogo.notInPokerPlay = false;	
}

function clientCmdForceNotInPokerPlay()
{
	$mySelf.pk_jogo.notInPokerPlay = true;	
}


//////////////////
//Poker GMBOT:



function pk_jogo::atualizarGMBOT(%this)
{
	if(%this.notInPokerPlay)
	{
		pk_gui_GMBOT_txt.text = "Pressione a tecla ESC para voltar para o jogo.";
		return;
	}
		
	%this.printGMBOT();
}

function pk_jogo::printGMBOT(%this)
{
	%txt = %this.getGMBOTaction();
	pk_gui_GMBOT_txt.setVisible(true);
	
	if(%txt.num == 4 || %txt.num == 5)
	{
		pk_gui_GMBOT_txt.text = %txt.txt;	
		return;
	}
	
	pk_gui_GMBOT_txt.text = $jogadorDaVez.nome @ " tem " @ palcoTurnoTimer.turnoTimeLeft @ " segundos para " @ %txt.txt @ ".";
	pk_sorteioGui_timer_txt.text = "Tempo restante: " @ palcoTurnoTimer.turnoTimeLeft;
}

function pk_jogo::getGMBOTaction(%this)
{
	if(%this.esperandoFlop)
	{
		%action = %this.parent.clientActionsSimSet.getObject(4); //%string = "FLOP: Sorteando uma carta para a mesa...";
		return %action;	
	}
	
	if(%this.esperandoTurn)
	{
		%action = %this.parent.clientActionsSimSet.getObject(5); //%string = "TURN: Sorteando uma carta para a mesa...";
		return %action;	
	}
	
	if(!isObject(%this.simMesa))
	{
		%action = %this.parent.clientActionsSimSet.getObject(0); //%string = "sortear uma carta para si";
		return %action;
	}
	
	if(%this.forceRiver)
	{
		%action = %this.parent.clientActionsSimSet.getObject(0); //%string = "sortear uma carta para si";
		return %action;
	}
		
	if(%this.apostaAtual $= "" || %this.apostaAtual $= "MESA")
	{
		%action = %this.parent.clientActionsSimSet.getObject(1); //%string = "dar Mesa ou fazer uma aposta";
		return %action;
	}
	
	if(!%this.getPodeAumentarAposta())
	{
		%action = %this.parent.clientActionsSimSet.getObject(3); //%string = "fugir ou pagar a aposta";
		return %action;	
	}
	
	%action = %this.parent.clientActionsSimSet.getObject(2); //%string = "fugir, pagar ou aumentar a aposta";
	return %action;
}




function pk_jogo::getPodeAumentarAposta(%this)
{
	if(%this.getAlgumAllIn())
	{
		return false;
	}
	
	if(%this.apostaAtual < (%this.blind * %this.apostaMax))
	{
		return true;
	}
	
	if($rodadaAtual >= 3)
	{
		return true;
	}
	
	return false;
}

//////////////////////////////////////////////////////
//Fugir
function clientPushMsgBoxConfirmarFugir()
{
	canvas.pushDialog(confirmarFugirGui);
}

function clientPopMsgBoxConfirmarFugir()
{
	canvas.popDialog(confirmarFugirGui);	
}


function clientGetPlayerPorId(%id)
{
	%eval = "%player = $" @ %id @ ";";
	eval(%eval);
	return %player;
}


function clientCmdPlayerFugiu(%playerId)
{
	%player = clientGetPlayerPorId(%playerId);
	%player.pk_lastAction = "FUGIU";
	%txt = $mySelf.pk_jogo.getLastActionTxtPorPlayer(%player);
	%txt.text = "FUGIU";
	%player.pk_fugiu = true;
}

function clientCmdPk_FimSolitario(%pot, %ganhadorId)
{
	%ganhador = clientGetPlayerPorId(%ganhadorId);
	clientPushPk_fimSolitarioGui();
	pk_fimSolitario_nome_txt.text = %ganhador.nome;
	pk_fimSolitario_pot_txt.text = %pot;
	
	$primeiraSalaInside = false;
	$inGame = false;
	cancel($pk_piscarPlayerShedule);
	
	palcoTurnoTimer.setTimerOff();
	
	clientPopTelasNoFimDoJogo();
	initSalaChatGui();
}

function clientPopTelasNoFimDoJogo()
{
	Canvas.popDialog(objetivosGuii);
	Canvas.popDialog(escolhaDeCores);
	Canvas.popDialog(aguardandoObjGui);
	clientPopPokerGui();
	clientPopPk_sorteioGui();
}

function clientPushPk_fimSolitarioGui()
{
	canvas.pushDialog(pk_fimSolitarioGui);	
}

function clientPopPk_fimSolitarioGui()
{
	canvas.popDialog(pk_fimSolitarioGui);	
}

function clientPk_askVoltarPraSala()
{
	clientUnloadGame();
	Canvas.pushDialog(newSalaInsideGui);
	clientAskVoltarPraSala();
}



//////////////////////////////////////
//Tecnologias:
function clientCmdDarTecnologia(%pesClassId)
{
	%eval = "$myPersona." @ %pesClassId @ " = 3;";
	eval(%eval);
	%primeiroPlayerVivo = clientGetPrimeiroPlayerVivo();
	
	switch$ (%pesClassId)
	{
		case "aca_c_1": clientSetIntel();
		case "aca_i_2": clientSetFilantropia(); clientSetIntel();
		case "aca_i_3": clientSetIntel();
		case "aca_a_2": $mySelf.reqCanhao = 3;
		case "aca_av_3": $mySelf.ocultarCusto = 1; $mySelf.reqOcultar = 1; if($mySelf == %primeiroPlayerVivo){verificarOcultarBtn();}
		case "aca_v_5": $mySelf.airDrops = 2; airDropHud_btn.setActive(true);
	}
	echo("RECEBI A TECNOLOGIA " @ %pesClassId);
}

function clientGetPrimeiroPlayerVivo()
{
	for(%i = 1; %i < 7; %i++)
	{
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		if(!%player.taMorto)
			return %player;
	}
}

function clientCmdDarColecionador()
{
	$mySelf.colecionador = true;
	$mySelf.minerios+=1;	
	$mySelf.petroleos+=1;
	$mySelf.uranios+=1;
	atualizarRecursosGui();
}

function clientCmdDarEngenheiro()
{
	$mySelf.engenheiro = true;
	clientSetBasesBtnImg();
}

function clientCmdDarMarinheiro()
{
	$mySelf.marinheiro = true;
	clientSetNaviosBtnImg();
}

function clientCmdDarMagnata()
{
	$mySelf.magnata = true;
	$mySelf.imperiais += 20;	
	imperiais_txt.text = $mySelf.imperiais;
}

function clientCmdDarLider()
{
	$myPersona.aca_a_1++;
	$mySelf.lideresDisponiveis++;
}

function pk_jogo::zerarMyPersona(%this)
{
	if($myPersona.especie $= "humano"){
		//soldados:
		$myPersona.aca_s_d_min = 1;
		$myPersona.aca_s_d_max = 6;
		$myPersona.aca_s_a_min = 1;
		$myPersona.aca_s_a_max = 6;
		//tanques:
		$myPersona.aca_t_d_min = 1;
		$myPersona.aca_t_d_max = 12;
		$myPersona.aca_t_a_min = 1;
		$myPersona.aca_t_a_max = 12;
		//navios:
		$myPersona.aca_n_d_min = 1;
		$myPersona.aca_n_d_max = 12;
		$myPersona.aca_n_a_min = 1;
		$myPersona.aca_n_a_max = 12;
		//líder1:
		$myPersona.aca_ldr_1_d_min = 1;
		$myPersona.aca_ldr_1_d_max = 12;
		$myPersona.aca_ldr_1_a_min = 1;
		$myPersona.aca_ldr_1_a_max = 12;
		$myPersona.aca_ldr_1_h1 = 2;
		$myPersona.aca_ldr_1_h2 = 2;
		$myPersona.aca_ldr_1_h3 = 2;
		$myPersona.aca_ldr_1_h4 = 2;
		//líder2:
		$myPersona.aca_ldr_2_d_min = 1;
		$myPersona.aca_ldr_2_d_max = 12;
		$myPersona.aca_ldr_2_a_min = 1;
		$myPersona.aca_ldr_2_a_max = 12;
		$myPersona.aca_ldr_2_h1 = 2;
		$myPersona.aca_ldr_2_h2 = 2;
		$myPersona.aca_ldr_2_h3 = 2;
		$myPersona.aca_ldr_2_h4 = 2;
		//líder3:
		$myPersona.aca_ldr_3_d_min = 1;
		$myPersona.aca_ldr_3_d_max = 12;
		$myPersona.aca_ldr_3_a_min = 1;
		$myPersona.aca_ldr_3_a_max = 12;
		$myPersona.aca_ldr_3_h1 = 2;
		$myPersona.aca_ldr_3_h2 = 2;
		$myPersona.aca_ldr_3_h3 = 2;
		$myPersona.aca_ldr_3_h4 = 2;
		
		//visionário
		$myPersona.aca_v_1 = 2;
		$myPersona.aca_v_2 = 0;
		$myPersona.aca_v_3 = 0;
		$myPersona.aca_v_4 = 0;
		$myPersona.aca_v_5 = 0;
		$myPersona.aca_v_6 = 0;
		//arebatador:
		$myPersona.aca_a_1 = 0;
		$myPersona.aca_a_2 = 0;
		//comerciante:
		$myPersona.aca_c_1 = 0;
		//diplomata:
		$myPersona.aca_d_1 = 3;
		//intel:
		$myPersona.aca_i_1 = 0;
		$myPersona.aca_i_2 = 0;
		$myPersona.aca_i_3 = 0;
	} else if($myPersona.especie $= "gulok"){
		//vermes:
		$myPersona.aca_s_d_min = 1;
		$myPersona.aca_s_d_max = 8;
		$myPersona.aca_s_a_min = 1;
		$myPersona.aca_s_a_max = 8;
		//Rainhas:
		$myPersona.aca_t_d_min = 1;
		$myPersona.aca_t_d_max = 15;
		$myPersona.aca_t_a_min = 1;
		$myPersona.aca_t_a_max = 15;
		//Cefaloks:
		$myPersona.aca_n_d_min = 1;
		$myPersona.aca_n_d_max = 14;
		$myPersona.aca_n_a_min = 1;
		$myPersona.aca_n_a_max = 14;
		//Zangão1:
		$myPersona.aca_ldr_1_d_min = 1;
		$myPersona.aca_ldr_1_d_max = 14;
		$myPersona.aca_ldr_1_a_min = 1;
		$myPersona.aca_ldr_1_a_max = 14;
		$myPersona.aca_ldr_1_h1 = 2;
		$myPersona.aca_ldr_1_h2 = 2;
		$myPersona.aca_ldr_1_h3 = 2;
		$myPersona.aca_ldr_1_h4 = 2;
		//Zangão2:
		$myPersona.aca_ldr_2_d_min = 1;
		$myPersona.aca_ldr_2_d_max = 14;
		$myPersona.aca_ldr_2_a_min = 1;
		$myPersona.aca_ldr_2_a_max = 14;
		$myPersona.aca_ldr_2_h1 = 2;
		$myPersona.aca_ldr_2_h2 = 2;
		$myPersona.aca_ldr_2_h3 = 2;
		$myPersona.aca_ldr_2_h4 = 2;
		//Dragnal2:
		$myPersona.aca_ldr_3_d_min = 15;
		$myPersona.aca_ldr_3_d_max = 30;
		$myPersona.aca_ldr_3_a_min = 15;
		$myPersona.aca_ldr_3_a_max = 30;
		$myPersona.aca_ldr_3_h1 = 2;
		$myPersona.aca_ldr_3_h2 = 2;
		$myPersona.aca_ldr_3_h3 = 2;
		$myPersona.aca_ldr_3_h4 = 2;
		
		//visionário
		$myPersona.aca_v_1 = 0;
		$myPersona.aca_v_2 = 0;
		$myPersona.aca_v_3 = 0;
		$myPersona.aca_v_4 = 0;
		$myPersona.aca_v_5 = 0;
		$myPersona.aca_v_6 = 0;
		//arebatador:
		$myPersona.aca_a_1 = 0;
		$myPersona.aca_a_2 = 0;
		//comerciante:
		$myPersona.aca_c_1 = 0;
		//diplomata:
		$myPersona.aca_d_1 = 0;
		//intel:
		$myPersona.aca_i_1 = 0;
		$myPersona.aca_i_2 = 0;
		$myPersona.aca_i_3 = 0;
	}
	
	//Pesquisa Em Andamento:
	$myPersona.aca_pea_id = 0;
	$myPersona.aca_pea_min = 0;
	$myPersona.aca_pea_pet = 0;
	$myPersona.aca_pea_ura = 0;
	$myPersona.aca_pea_ldr = 0;
		
	//marca que a persona tem dados de academia:
	$myPersona.aca_tenhoDados = true;
	
	//pesquisas Avançadas:
	$myPersona.aca_av_1 = 0;
	$myPersona.aca_av_2 = 0;
	$myPersona.aca_av_3 = 0;
	$myPersona.aca_av_4 = 0;
	
	$myPersona.aca_pln_1 = 0;
	
	$myPersona.aca_art_1 = 1; //Geo-Canhão
	$myPersona.aca_art_2 = 1; //Nexus Temporal	
}

function pk_jogo::setMyAcadBKP(%this)
{
	if(isObject($myPersona.acadBKP))
		return;
	
	$myPersona.acadBKP = new ScriptObject(){};
	
	$myPersona.acadBKP.aca_s_d_min = $myPersona.aca_s_d_min;
	$myPersona.acadBKP.aca_s_d_max = $myPersona.aca_s_d_max;
	$myPersona.acadBKP.aca_s_a_min = $myPersona.aca_s_a_min;
	$myPersona.acadBKP.aca_s_a_max = $myPersona.aca_s_a_max;
	//tanques:
	$myPersona.acadBKP.aca_t_d_min = $myPersona.aca_t_d_min;
	$myPersona.acadBKP.aca_t_d_max = $myPersona.aca_t_d_max;
	$myPersona.acadBKP.aca_t_a_min = $myPersona.aca_t_a_min;
	$myPersona.acadBKP.aca_t_a_max = $myPersona.aca_t_a_max;
	//navios:
	$myPersona.acadBKP.aca_n_d_min = $myPersona.aca_n_d_min;
	$myPersona.acadBKP.aca_n_d_max = $myPersona.aca_n_d_max;
	$myPersona.acadBKP.aca_n_a_min = $myPersona.aca_n_a_min;
	$myPersona.acadBKP.aca_n_a_max = $myPersona.aca_n_a_max;
	//líder1:
	$myPersona.acadBKP.aca_ldr_1_d_min = $myPersona.aca_ldr_1_d_min;
	$myPersona.acadBKP.aca_ldr_1_d_max = $myPersona.aca_ldr_1_d_max;
	$myPersona.acadBKP.aca_ldr_1_a_min = $myPersona.aca_ldr_1_a_min;
	$myPersona.acadBKP.aca_ldr_1_a_max = $myPersona.aca_ldr_1_a_max;
	$myPersona.acadBKP.aca_ldr_1_h1 = $myPersona.aca_ldr_1_h1;
	$myPersona.acadBKP.aca_ldr_1_h2 = $myPersona.aca_ldr_1_h2;
	$myPersona.acadBKP.aca_ldr_1_h3 = $myPersona.aca_ldr_1_h3;
	$myPersona.acadBKP.aca_ldr_1_h4 = $myPersona.aca_ldr_1_h4;
	//líder2:
	$myPersona.acadBKP.aca_ldr_2_d_min = $myPersona.aca_ldr_2_d_min;
	$myPersona.acadBKP.aca_ldr_2_d_max = $myPersona.aca_ldr_2_d_max;
	$myPersona.acadBKP.aca_ldr_2_a_min = $myPersona.aca_ldr_2_a_min;
	$myPersona.acadBKP.aca_ldr_2_a_max = $myPersona.aca_ldr_2_a_max;
	$myPersona.acadBKP.aca_ldr_2_h1 = $myPersona.aca_ldr_2_h1;
	$myPersona.acadBKP.aca_ldr_2_h2 = $myPersona.aca_ldr_2_h2;
	$myPersona.acadBKP.aca_ldr_2_h3 = $myPersona.aca_ldr_2_h3;
	$myPersona.acadBKP.aca_ldr_2_h4 = $myPersona.aca_ldr_2_h4;
	//líder3:
	$myPersona.acadBKP.aca_ldr_3_d_min = $myPersona.aca_ldr_3_d_min;
	$myPersona.acadBKP.aca_ldr_3_d_max = $myPersona.aca_ldr_3_d_max;
	$myPersona.acadBKP.aca_ldr_3_a_min = $myPersona.aca_ldr_3_a_min;
	$myPersona.acadBKP.aca_ldr_3_a_max = $myPersona.aca_ldr_3_a_max;
	$myPersona.acadBKP.aca_ldr_3_h1 = $myPersona.aca_ldr_3_h1;
	$myPersona.acadBKP.aca_ldr_3_h2 = $myPersona.aca_ldr_3_h2;
	$myPersona.acadBKP.aca_ldr_3_h3 = $myPersona.aca_ldr_3_h3;
	$myPersona.acadBKP.aca_ldr_3_h4 = $myPersona.aca_ldr_3_h4;
	
	//visionário
	$myPersona.acadBKP.aca_v_1 = $myPersona.aca_v_1;
	$myPersona.acadBKP.aca_v_2 = $myPersona.aca_v_2;
	$myPersona.acadBKP.aca_v_3 = $myPersona.aca_v_3;
	$myPersona.acadBKP.aca_v_4 = $myPersona.aca_v_4;
	$myPersona.acadBKP.aca_v_5 = $myPersona.aca_v_5;
	$myPersona.acadBKP.aca_v_6 = $myPersona.aca_v_6;
	//arebatador:
	$myPersona.acadBKP.aca_a_1 = $myPersona.aca_a_1;
	$myPersona.acadBKP.aca_a_2 = $myPersona.aca_a_2;
	//comerciante:
	$myPersona.acadBKP.aca_c_1 = $myPersona.aca_c_1;
	//diplomata:
	$myPersona.acadBKP.aca_d_1 = $myPersona.aca_d_1;
	//intel:
	$myPersona.acadBKP.aca_i_1 = $myPersona.aca_i_1;
	$myPersona.acadBKP.aca_i_2 = $myPersona.aca_i_2;
	$myPersona.acadBKP.aca_i_3 = $myPersona.aca_i_3;
	
	$myPersona.acadBKP.aca_pea_id = $myPersona.aca_pea_id;
	$myPersona.acadBKP.aca_pea_min = $myPersona.aca_pea_min;
	$myPersona.acadBKP.aca_pea_pet = $myPersona.aca_pea_pet;
	$myPersona.acadBKP.aca_pea_ura = $myPersona.aca_pea_ura;
	$myPersona.acadBKP.aca_pea_ldr = $myPersona.aca_pea_ldr;
		
	//pesquisas Avançadas:
	$myPersona.acadBKP.aca_av_1 = $myPersona.aca_av_1;
	$myPersona.acadBKP.aca_av_2 = $myPersona.aca_av_2;
	$myPersona.acadBKP.aca_av_3 = $myPersona.aca_av_3;
	$myPersona.acadBKP.aca_av_4 = $myPersona.aca_av_4;
	
	$myPersona.acadBKP.aca_pln_1 = $myPersona.aca_pln_1;
	
	$myPersona.acadBKP.aca_art_1 = $myPersona.aca_art_1; 
	$myPersona.acadBKP.aca_art_2 = $myPersona.aca_art_2;
	
	//marca que a persona tem dados de academia no backup:
	$myPersona.acadBKP.aca_tenhoDados = true;
	
	echo("BKP de academia criado com sucesso para a Persona " @ $myPersona.nome);
}

function clientCmdResetAcadFromBKP()
{
	if(!isObject($myPersona.acadBKP))
	{	
		echo("*** ERRO: BKP da academia não está disponível para ser carregado!");
		return;
	}
			
	$myPersona.aca_s_d_min = $myPersona.acadBKP.aca_s_d_min;
	$myPersona.aca_s_d_max = $myPersona.acadBKP.aca_s_d_max;
	$myPersona.aca_s_a_min = $myPersona.acadBKP.aca_s_a_min;
	$myPersona.aca_s_a_max = $myPersona.acadBKP.aca_s_a_max;
	//tanques:
	$myPersona.aca_t_d_min = $myPersona.acadBKP.aca_t_d_min;
	$myPersona.aca_t_d_max = $myPersona.acadBKP.aca_t_d_max;
	$myPersona.aca_t_a_min = $myPersona.acadBKP.aca_t_a_min;
	$myPersona.aca_t_a_max = $myPersona.acadBKP.aca_t_a_max;
	//navios:
	$myPersona.aca_n_d_min = $myPersona.acadBKP.aca_n_d_min;
	$myPersona.aca_n_d_max = $myPersona.acadBKP.aca_n_d_max;
	$myPersona.aca_n_a_min = $myPersona.acadBKP.aca_n_a_min;
	$myPersona.aca_n_a_max = $myPersona.acadBKP.aca_n_a_max;
	//líder1:
	$myPersona.aca_ldr_1_d_min = $myPersona.acadBKP.aca_ldr_1_d_min;
	$myPersona.aca_ldr_1_d_max = $myPersona.acadBKP.aca_ldr_1_d_max;
	$myPersona.aca_ldr_1_a_min = $myPersona.acadBKP.aca_ldr_1_a_min;
	$myPersona.aca_ldr_1_a_max = $myPersona.acadBKP.aca_ldr_1_a_max;
	$myPersona.aca_ldr_1_h1 = $myPersona.acadBKP.aca_ldr_1_h1;
	$myPersona.aca_ldr_1_h2 = $myPersona.acadBKP.aca_ldr_1_h2;
	$myPersona.aca_ldr_1_h3 = $myPersona.acadBKP.aca_ldr_1_h3;
	$myPersona.aca_ldr_1_h4 = $myPersona.acadBKP.aca_ldr_1_h4;
	//líder2:
	$myPersona.aca_ldr_2_d_min = $myPersona.acadBKP.aca_ldr_2_d_min;
	$myPersona.aca_ldr_2_d_max = $myPersona.acadBKP.aca_ldr_2_d_max;
	$myPersona.aca_ldr_2_a_min = $myPersona.acadBKP.aca_ldr_2_a_min;
	$myPersona.aca_ldr_2_a_max = $myPersona.acadBKP.aca_ldr_2_a_max;
	$myPersona.aca_ldr_2_h1 = $myPersona.acadBKP.aca_ldr_2_h1;
	$myPersona.aca_ldr_2_h2 = $myPersona.acadBKP.aca_ldr_2_h2;
	$myPersona.aca_ldr_2_h3 = $myPersona.acadBKP.aca_ldr_2_h3;
	$myPersona.aca_ldr_2_h4 = $myPersona.acadBKP.aca_ldr_2_h4;
	//líder3:
	$myPersona.aca_ldr_3_d_min = $myPersona.acadBKP.aca_ldr_3_d_min;
	$myPersona.aca_ldr_3_d_max = $myPersona.acadBKP.aca_ldr_3_d_max;
	$myPersona.aca_ldr_3_a_min = $myPersona.acadBKP.aca_ldr_3_a_min;
	$myPersona.aca_ldr_3_a_max = $myPersona.acadBKP.aca_ldr_3_a_max;
	$myPersona.aca_ldr_3_h1 = $myPersona.acadBKP.aca_ldr_3_h1;
	$myPersona.aca_ldr_3_h2 = $myPersona.acadBKP.aca_ldr_3_h2;
	$myPersona.aca_ldr_3_h3 = $myPersona.acadBKP.aca_ldr_3_h3;
	$myPersona.aca_ldr_3_h4 = $myPersona.acadBKP.aca_ldr_3_h4;
	
	//visionário
	$myPersona.aca_v_1 = $myPersona.acadBKP.aca_v_1;
	$myPersona.aca_v_2 = $myPersona.acadBKP.aca_v_2;
	$myPersona.aca_v_3 = $myPersona.acadBKP.aca_v_3;
	$myPersona.aca_v_4 = $myPersona.acadBKP.aca_v_4;
	$myPersona.aca_v_5 = $myPersona.acadBKP.aca_v_5;
	$myPersona.aca_v_6 = $myPersona.acadBKP.aca_v_6;
	//arebatador:
	$myPersona.aca_a_1 = $myPersona.acadBKP.aca_a_1;
	$myPersona.aca_a_2 = $myPersona.acadBKP.aca_a_2;
	//comerciante:
	$myPersona.aca_c_1 = $myPersona.acadBKP.aca_c_1;
	//diplomata:
	$myPersona.aca_d_1 = $myPersona.acadBKP.aca_d_1;
	//intel:
	$myPersona.aca_i_1 = $myPersona.acadBKP.aca_i_1;
	$myPersona.aca_i_2 = $myPersona.acadBKP.aca_i_2;
	$myPersona.aca_i_3 = $myPersona.acadBKP.aca_i_3;
	
	$myPersona.aca_pea_id = $myPersona.acadBKP.aca_pea_id;
	$myPersona.aca_pea_min = $myPersona.acadBKP.aca_pea_min;
	$myPersona.aca_pea_pet = $myPersona.acadBKP.aca_pea_pet;
	$myPersona.aca_pea_ura = $myPersona.acadBKP.aca_pea_ura;
	$myPersona.aca_pea_ldr = $myPersona.acadBKP.aca_pea_ldr;
		
	//pesquisas Avançadas:
	$myPersona.aca_av_1 = $myPersona.acadBKP.aca_av_1;
	$myPersona.aca_av_2 = $myPersona.acadBKP.aca_av_2;
	$myPersona.aca_av_3 = $myPersona.acadBKP.aca_av_3;
	$myPersona.aca_av_4 = $myPersona.acadBKP.aca_av_4;
	
	$myPersona.aca_pln_1 = $myPersona.acadBKP.aca_pln_1;
	
	$myPersona.aca_art_1 = $myPersona.acadBKP.aca_art_1; 
	$myPersona.aca_art_2 = $myPersona.acadBKP.aca_art_2;
	
	//marca que a persona tem dados de academia:
	$myPersona.aca_tenhoDados = true;
	clientDelMyAcadBKP(); //deleta o BKP pra persona poder usar a academia e depois o sistema fazer um novo BKP atualizado.
	echo("BKP foi usado com sucesso para resetar os dados de academia da Persona " @ $myPersona.nome);
}

function clientDelMyAcadBKP()
{
	if(!isObject($myPersona.acadBKP))
		return;
		
	$myPersona.acadBKP.delete();	
	echo("BKP de academia deletado para Persona " @ $myPersona.nome);
}

///////////////////////////
//Check/Fold/Sorteio automático:

function clientPk_checkFoldSorteio()
{
	$mySelf.pk_jogo.inativarPkBtns();
	$mySelf.pk_jogo.checkFoldSorteio();	
}



function pk_jogo::checkFoldSorteio(%this)
{
	if(!isObject($mySelf.pk_simCartas))
	{
		%this.askSorteioAutomatico();
		return;
	}
	
	if($mySelf.pk_simCartas.getCount() == 0 || $mySelf.pk_simCartas.getCount() == 1)
	{
		%this.askSorteioAutomatico();
		return;
	}
	
	if($mySelf.pk_simCartas.getCount() == 3) 
	{
		%this.askCheckFoldAutomatico();
		return;
	}
	
	if($mySelf.pk_simCartas.getCount() == 4 && $rodadaAtual == 2) 
	{
		%this.askCheckFoldAutomatico();
		return;
	}
	
	if($mySelf.pk_simCartas.getCount() == 4 && $rodadaAtual == 3) 
	{
		%this.askSorteioAutomatico();
		return;
	}
	
	if($mySelf.pk_simCartas.getCount() == 5)
	{
		%this.askCheckFoldAutomatico();
		return;
	}
	
	echo("BUG -> situação não levada em conta!");
}

function pk_jogo::askSorteioAutomatico(%this)
{
	echo("SORTEIO AUTOMÁTICO DE POKER: Rodada Atual = " @ $rodadaAtual);
	%num = dado(20, 0);
	clientAskSortearPkCarta(%num);	
}

function pk_jogo::askCheckFoldAutomatico(%this)
{
	echo("CHECK/FOLD AUTOMÁTICO DE POKER: Rodada Atual = " @ $rodadaAtual);
	if($mySelf.pk_apostaAtual == %this.apostaAtual || %this.apostaAtual $= "")
	{
		%this.darMesa();
		return;
	}
	
	%this.fugir();
}












////////////////////////////
//Testes:

function pk_jogo::contarCartasDoNaipe(%this, %naipe, %player)
{
	for(%i = 0; %i < %player.pk_simCartas.getCount(); %i++)
	{
		%carta = %player.pk_simCartas.getObject(%i);
		if(%carta.naipe $= %naipe)
			%count++;
	}
	return %count;
}

function pk_handler::simularJogo(%this, %numDePlayers, %preFlop)
{
	canvas.pushDialog(pokerGui);
	
	%pk_jogo = %this.criarJogo();
	
	%pk_jogo.clearHud();
	%this.clearCartas(%numDePlayers);
	%this.clearPkPontos(%numDePlayers);
	%this.clearPkMaoUnica(%numDePlayers);
		
	%pk_jogo.addPlayer($player1);
	$mySelf = $player1;
	$player1.nome = "Loki";
	
	if(!%preFlop){
		for(%i = 0; %i < 3; %i++)
		{
			%carta = %pk_jogo.sortearCarta();
			clientCmdReceberCartaPoker(%carta.id);
		}
		
		for(%i = 0; %i < 2; %i++)
		{
			%carta = %pk_jogo.sortearCarta();
			clientCmdAdicionarCartaPokerAMesa(%carta.id);
		}
	} else {
		for(%i = 0; %i < 2; %i++)
		{
			%carta = %pk_jogo.sortearCarta();
			clientCmdReceberCartaPoker(%carta.id);
		}
	}
	
	%pk_jogo.avaliarMao($player1);
	%pk_jogo.printTodasAsCartas();
	%pk_jogo.avaliarMaoUnica($player1);
	%pk_jogo.printMaoUnica();
	echo("POKER >> JOGO FEITO: " @ $player1.jogoFeito);
	//%pk_jogo.delete();
}

function pk_handler::clearCartas(%this, %numDePlayers)
{
	for(%i = 1; %i < %numDePlayers + 1; %i++)
	{
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		
		if(isObject(%player.pk_simCartas))
			%player.pk_simCartas.clear();
			
		if(isObject(%player.pk_simMao))
			%player.pk_simMao.clear();
	}
}

function pk_handler::clearPkPontos(%this, %numDePlayers)
{
	for(%i = 1; %i < %numDePlayers + 1; %i++)
	{
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		
		%player.pk_pontos = 0;
	}
}

function pk_handler::clearPkMaoUnica(%this, %numDePlayers)
{
	for(%i = 1; %i < %numDePlayers + 1; %i++)
	{
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		
		%player.maoUnica = "";
		%player.maoUnicaTier = 0;
	}
}


function pk_handler::simularVariosJogos(%this, %numDeJogos)
{
	for(%i = 0; %i < %numDeJogos; %i++)
		schedule(%i * 10, 0, "simularJogo");
		
	schedule(%numDeJogos * 10, 0, "echoTest", %numDeJogos);
}

function simularJogo()
{
	$poker_handler.simularJogo(1);	
}

function echoTest(%numDeJogos)
{
	echo(">>>>>>>>>>>>>>>>>>>>>>>>>>POKER: SIMULAÇÃO DE " @ %numDeJogos @ " JOGOS<<<<<<<<<<<<<<<<<<<<<");
	echo("Um Par_______" @ $player1.umPar);
	echo("Dois Pares___" @ $player1.doisPares);
	echo("Trinca_______" @ $player1.trinca);
	echo("Cores________" @ $player1.cores);
	echo("Full House___" @ $player1.fullHouse);
	echo("Quadra_______" @ $player1.quadra);
	echo(">>>>>>>>>>>>>>>>>>>>>>>>>>FIM DA SIMULAÇÃO<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");	
}



//Poker Tab:
function clientAjuda_poker()
{
	ajudaPoker_titulo_txt.text = "POKER IMPERIAL";
	//loggedIn_pk_ajuda_txtML.setText("<just:center>O POKER IMPERIAL É UMA <color:FDFDD8>UNIÃO ENTRE IMPÉRIO E POKER, <color:FFFFFF>ONDE SÃO APOSTADAS FICHAS ESPECIAIS.\n\nCADA PERSONA RECEBE <color:FDFDD8>150 FICHAS INICIAIS COMO CORTESIA. <color:FFFFFF>QUANDO ESTAS TERMINAREM, É POSSÍVEL COMPRAR MAIS FICHAS COM <color:FDFDD8>CRÉDITOS OU OMNIS. <color:FFFFFF>\n\nAS FICHAS TAMBÉM PODEM SER <color:FDFDD8>REVENDIDAS POR CRÉDITOS <color:FFFFFF>(A PARTIR DE 1000 FICHAS).\n\nCADA CARTA DE POKER REPRESENTA UMA <color:FDFDD8>TECNOLOGIA DA ACADEMIA IMPERIAL, <color:FFFFFF>QUE É ADQUIRIDA TEMPORARIAMENTE QUANDO VOCÊ RECEBE A CARTA.\n\nÉ ALTAMENTE RECOMENDADO ASSISTIR AO VÍDEO-TUTORIAL NO SITE <color:FDFDD8>WWW.PROJETOIMPERIO.COM <color:FFFFFF>ANTES DE JOGAR SUA PRIMEIRA PARTIDA DE POKER IMPERIAL.\n\n");
	//loggedIn_pk_ajuda_txtML.setText("<just:center>O POKER IMPERIAL É UMA <color:FDFDD8>UNIÃO ENTRE IMPÉRIO E POKER, <color:FFFFFF>ONDE SÃO APOSTADAS FICHAS ESPECIAIS.\n\nCADA PERSONA RECEBE <color:FDFDD8>150 FICHAS INICIAIS COMO CORTESIA. <color:FFFFFF>QUANDO ESTAS TERMINAREM, É POSSÍVEL COMPRAR MAIS FICHAS COM <color:FDFDD8>CRÉDITOS OU OMNIS. <color:FFFFFF>\n\nCADA CARTA DE POKER REPRESENTA UMA <color:FDFDD8>TECNOLOGIA DA ACADEMIA IMPERIAL, <color:FFFFFF>QUE É ADQUIRIDA TEMPORARIAMENTE QUANDO VOCÊ RECEBE A CARTA.\n\nO POKER IMPERIAL SÓ PODE SER JOGADO POR <color:FDFDD8>PERSONAS HUMANAS COM PELO MENOS 25 VITÓRIAS.\n\nATENÇÃO: ESTE MODO DE JOGO ESTÁ EM TESTES PRELIMINARES.\n <color:FFFFFF>É PROVÁVEL QUE OCORRAM BUGS.\nJOGUE APENAS SE ESTIVER DISPOSTO A AJUDAR.  =)\n\n");
	loggedIn_pk_ajuda_txtML.setText("<just:center>O POKER IMPERIAL É UMA <color:FDFDD8>UNIÃO ENTRE IMPÉRIO E POKER, <color:FFFFFF>ONDE SÃO APOSTADAS FICHAS ESPECIAIS.\n\nCADA PERSONA RECEBE <color:FDFDD8>150 FICHAS INICIAIS COMO CORTESIA. <color:FFFFFF>QUANDO ESTAS TERMINAREM, É POSSÍVEL COMPRAR MAIS FICHAS COM <color:FDFDD8>CRÉDITOS OU OMNIS. <color:FFFFFF>\n\nCADA CARTA DE POKER REPRESENTA UMA <color:FDFDD8>TECNOLOGIA DA ACADEMIA IMPERIAL, <color:FFFFFF>QUE É ADQUIRIDA TEMPORARIAMENTE QUANDO VOCÊ RECEBE A CARTA.\n\nO POKER IMPERIAL SÓ PODE SER JOGADO POR <color:FDFDD8>PERSONAS HUMANAS COM PELO MENOS 25 VITÓRIAS.\n\n <color:FFFFFF>ESTE TIPO DE JOGO ESTÁ EM DESENVOLVIMENTO E AINDA NÃO PODE SER JOGADO. ESTARÁ DISPONÍVEL EM BREVE.\n <color:FDFDD8>PROGRESSO: 80%\n\n");
	ajuda_poker_tab.setVisible(true);		
}
function clientPopAjudaPokerTab()
{
	ajuda_poker_tab.setVisible(false);		
}

////////////////
//recebendo fichas do server no fim de cada jogo:
function clientCmdReceberPkFichas(%fichas, %creditos, %omnis)
{
	$myPersona.pk_fichas += %fichas;
	$myPersona.TAXOcreditos += %creditos;
	$myPersona.TAXOomnis += %omnis;
	clientAtualizarFichasDePokerTXTs();
}

function clientAtualizarFichasDePokerTXTs()
{
	loggedIn_pk_fichas_txt.text = $myPersona.pk_fichas;
	loggedInCreditos_txt.text = $myPersona.TAXOcreditos;
	loggedInOmnis_txt.text = $myPersona.TAXOomnis;
}










//////////////////////////////

/*
Em 10.000 jogos:
Um Par_________ 4951 (49,5%)
Dois Pares_____ 2796 (28,0%)
Trinca_________ 1196 (12,0%)
Cores__________ 686  (6,9%)
Full House_____ 323  (3,2%)
Quadra_________ 48   (0,5%)

Total em 30.000 jogos:
Um Par_________ 14828 (49,4%)
Dois Pares_____ 8324  (27,7%)
Trinca_________ 3723  (12,4%)
Cores__________ 2036  (6,8%)
Full House_____ 947   (3,1%)
Quadra_________ 142   (0,5%)

Total em 60.000 jogos:
Um Par_________ 29728 (49,5%)
Dois Pares_____ 16624 (27,7%)
Trinca_________ 7411  (12,3%)
Cores__________ 4029  (6,7%)
Full House_____ 1908  (3,2%)
Quadra_________ 300   (0,5%)

TOTAL EM 100.000 JOGOS:
Um Par_________ 49.469 (49,5%) -> 50%  -> 2 Pontos
Dois Pares_____ 27.900 (27,9%) -> 30%  -> 5 Pontos
Trinca_________ 12.308 (12,3%) -> 12%  -> 10 Pontos
Cores__________  6.731 (6,7%)  ->  7%  -> 15 Pontos
Full House_____  3.101 (3,1%)  ->  3%  -> 20 Pontos
Quadra_________    491 (0,5%)  -> 0,5% -> 30 Pontos
*/