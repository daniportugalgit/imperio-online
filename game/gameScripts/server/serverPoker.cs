// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverPoker.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quinta-feira, 19 de fevereiro de 2009 1:32
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
	
	echo("Carta de Poker " @ %carta.desc @ " criada com sucesso!");
}

function pk_handler::criarTodasAsCartas(%this){
	%this.criarCarta(1, 1, "Vermelho", "CANHÃO ORBITAL", "aca_a_2", "Você pode fazer um disparo orbital a partir da 3ª rodada.", "Todos podem fazer um disparo orbital a partir da 3ª rodada.");
	%this.criarCarta(2, 1, "Vermelho", "MIRA ELETRÔNICA", "aca_av_2", "Todas as suas unidades têm +6 em ataque mínimo e máximo.", "Todas as unidades têm +6 em ataque mínimo e máximo.");
	%this.criarCarta(3, 1, "Vermelho", "CARAPAÇA", "aca_av_1", "Todas as suas unidades têm +6 em defesa mínima e máxima.", "Todas as unidades têm +6 em defesa mínima e máxima.");
	%this.criarCarta(4, 1, "Vermelho", "LÍDER BRAVO", "especial", "Você pode convocar um líder anfíbio com habilidades no nível 2.", "Todos podem convocar um líder anfíbio com habilidades no nível 2.");
			
	%this.criarCarta(5, 2, "Amarelo", "OCULTAR", "aca_av_3", "Você pode tornar invisíveis suas bases e refinarias.", "Todos podem tornar bases e refinarias invisíveis aos adversários.");
	%this.criarCarta(6, 2, "Amarelo", "RECICLAGEM", "aca_v_3", "Você pode reciclar bases e refinarias instantaneamente.", "Todos podem reciclar bases e refinarias instantaneamente.");
	%this.criarCarta(7, 2, "Amarelo", "REFINARIA", "aca_v_4", "Você pode construir duas refinarias, cada uma por 4 imperiais.", "Todos podem construir duas refinarias, cada uma por 4 imperiais.");
	%this.criarCarta(8, 2, "Amarelo", "LIDER ALPHA", "especial", "Você pode convocar um líder anfíbio com habilidades no nível 2.", "Todos podem convocar um líder anfíbio com habilidades no nível 2.");
		
	%this.criarCarta(9, 3, "Verde", "SATÉLITE", "aca_av_4", "Você recebe 70% mais Imperiais por rodada.", "Todos recebem 70% mais imperiais por rodada.");
	%this.criarCarta(10, 3, "Verde", "AIR DROP", "aca_v_5", "Você pode enviar tanques para áreas terrestres sob seu domínio.", "Todos podem enviar tanques para áreas terrestres que possuam.");	
	%this.criarCarta(11, 3, "Verde", "TRANSPORTE", "aca_v_2", "Seus navios transportam até 5 soldados ou líderes.", "Todos os navios transportam até 5 soldados ou líderes.");
	%this.criarCarta(12, 3, "Verde", "VELOCISTA", "especial", "Você pode fazer 6 movimentos por rodada.", "Todos podem fazer 6 movimentos por rodada."); 
	
	%this.criarCarta(13, 4, "Azul", "ESPIONAGEM", "aca_i_1", "Você fica sabendo quando um adversário vende recursos ao Banco.", "Todos ficam sabendo quando alguém vende recursos ao Banco.");
	%this.criarCarta(14, 4, "Azul", "FILANTROPIA", "aca_i_2", "Você pode fazer até 3 doações filantrópicas nesta partida.", "Todos podem fazer até 3 doações filantrópicas nesta partida.");
	%this.criarCarta(15, 4, "Azul", "ALMIRANTE", "aca_i_3", "Você ganhará 6 pontos se possuir 5 ou mais bases no mar.", "Cada jogador ganhará 6 pontos se possuir 5 ou mais bases no mar.");
	%this.criarCarta(16, 4, "Azul", "PROSPECÇÃO", "aca_c_1", "Você pode comprar até 3 missões nesta partida.", "Todos podem comprar até 3 missões nesta partida.");
	
	%this.criarCarta(17, 5, "Roxo", "COLECIONADOR", "especial", "Você ganhou 1 conjunto de recursos.", "Todos ganharam 1 conjunto de recursos.");
	%this.criarCarta(18, 5, "Roxo", "MARINHEIRO", "especial", "Você pode construir navios por apenas 2 imperiais.", "Todos podem construir navios por apenas 2 imperiais.");
	%this.criarCarta(19, 5, "Roxo", "ENGENHEIRO", "especial", "Você pode construir bases por apenas 7 imperiais.", "Todos podem construir bases por apenas 7 imperiais."); 
	%this.criarCarta(20, 5, "Roxo", "MAGNATA", "especial", "Você ganhou 20 Imperiais.", "Todos ganharam 20 Imperiais."); 
}

function pk_handler::criarCartasExtra(%this){
	%this.criarCarta(21, 6, "Prata", "Imperador", "especial", "Você ganhou 15 Imperiais e seu bônus de economia duplicou.");
	%this.criarCarta(22, 6, "Prata", "Artífice", "especial", "Você pode usar o Artefato Alienígena deste planeta.");
	%this.criarCarta(23, 6, "Prata", "Guardião", "especial", "Você pode usar a Relíquia Alienígena deste planeta.");
	%this.criarCarta(24, 6, "Prata", "Comerciante", "especial", "Você recebeu 5 missões."); 
}

function pk_handler::setPk_jogo(%this, %jogo)
{
	%pk_jogo = %this.criarJogo();
	%pk_jogo.jogoPai = %jogo;
	%jogo.pk_jogo = %pk_jogo;
	%pk_jogo.herdarCaracteristicasDaSala();
		
		
	for(%i = 0; %i < %jogo.playersAtivos; %i++)
	{
		%player = %jogo.simPlayers.getObject(%i);
		%player.pk_jogo = %pk_jogo;
	}
	
	%pk_jogo.CTCcriarPk_jogo();
	%pk_jogo.TAXOpagarFichasIniciais();
}

function pk_handler::criarJogo(%this)
{
	$pokerJogosNum++;
	%pk_jogo = newSimObj("pk_jogo", $pokerJogosNum);
	%pk_jogo.parent = %this;
	%pk_jogo.initCartas();
	return %pk_jogo;
}

//Compra, venda, ganho e perda de fichas:
function pk_handler::comprarOuVenderFichas(%this, %persona, %fichas, %creditos, %omnis)
{
	%persona.pk_fichas += %fichas; //tem que vir negativo pra ser subtraído
	%persona.taxoCreditos += %creditos; //tem que vir negativo pra ser subtraído
	%persona.user.taxoOmnis += %omnis; //tem que vir negativo pra ser subtraído
	%this.taxoPk_fichas(%persona, %fichas, %creditos, %omnis);
	
	commandToClient(%persona.client, 'ReceberPkFichas', %fichas, %creditos, %omnis);
}

function pk_handler::taxoPk_fichas(%this, %persona, %fichas, %creditos, %omnis)
{
	%myServerPesq = criarServerPesq();
	%myServerPesq.url = "/torque/academia/pk_fichas?idPersona=" @ %persona.taxoId @ "&pk_fichas=" @ %fichas @ "&creditos=" @ %creditos @ "&omnis=" @ %omnis @ "&idPesqTorque=" @ %myServerPesq.num;
	$filas_handler.newFilaObj("pk_fichas", %myServerPesq.url, 2, %myServerPesq, %persona);	
}


////////////


initPokerSys();































////////////////////////////
//Testes:

function pk_handler::clearCartas(%this, %numDePlayers)
{
	for(%i = 1; %i < %numDePlayers + 1; %i++)
	{
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		
		if(isObject(%player.pk_simCartas))
			%player.pk_simCartas.clear();
	}
}



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
Trinca_________ 12.308 (12,3%) -> 10%  -> 10 Pontos
Cores__________  6.731 (6,7%)  ->  7%  -> 15 Pontos
Full House_____  3.101 (3,1%)  ->  3%  -> 20 Pontos
Quadra_________    491 (0,5%)  -> 0,5% -> 30 Pontos


*/
