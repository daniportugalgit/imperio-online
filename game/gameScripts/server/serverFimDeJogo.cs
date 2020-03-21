// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverFimDeJogo.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sábado, 5 de janeiro de 2008 16:10
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function jogo::getSouJogoTreino(%this)
{
	if(%this.forceTreino)
		return true;
			
	if(%this.playersAtivos < 3)
		return true;
	
	if(%this.emDuplas && %this.playersAtivos == 4)
		return true;
		
	return false;
}


//bater:
function serverCmdBater(%client, %quemBateu, %assassino)
{
	%jogo = %client.player.jogo;
	
	if(%jogo.terminado)
		return;
	
	%jogo.bater(%quemBateu, %assassino);
}

function jogo::bater(%this, %quemBateu)
{
	%this.terminado = true; 
		
	%this.verificacoesFinais();
	
	%this.setDataFim();	
	%this.setTempoDeJogo();
		
	if(%this.guloksDespertaram)
	{
		%this.removerAiPlayer();
		%this.addObjGulok();
	}
		
	%this.calcularPontosGlobal();	
	%this.setGanhadores();
	%this.setSimVencedores();		
	%this.calcularCreditosGlobal(%quemBateu); 
	
	%souJogoTreino = %this.getSouJogoTreino();
		
	if(isObject(%this.pk_jogo))
	{
		%this.pk_jogo.setPkFichasBatidaForaAll();
		%this.pk_jogo.setPowerPlayForAll();
		%this.setComercianteAgoraForAll();
		%this.setArrebatadorAgoraForAll();
	}
	%this.setFimDeJogoString(%quemBateu);
	%this.assassino = %this.getAssassino();
	%this.resetarPersonasProntas();
	%this.echoResultado();
	%this.CTCfinal();
	
	if(%souJogoTreino){
		%this.zerarCreditosAgoraForAll();
	} else {
		%this.setNewStatsForAll();
	}
	
	if(!%souJogoTreino && %this.tempoDeJogo > 1) //TODO: Se for de poker, não importa o tempo! sempre é válido!
	{
		%this.sala.jogoTAXOid = "";
		enviarTAXOResultado(%this);
	}
		
	serverWriteURLToFile(serverCriarURL(%this)); //só pra ter alguma referência depois
	
	%this.recalcularGraduacaoForAll();
	%this.verificarTAXOinvestimentos();
	%this.setPartidaEncerrada();
	//%this.CTCpowerPlays();
	
	if(isObject(%this.sala.banList))
		%this.sala.banList.clear();
	
	if(%this.horarioNobre)
		%this.CTCbonusHorarioNobre();
		
	serverAtualizarAtrioParaTodos();
}

function jogo::verificarTAXOinvestimentos(%this)
{
	%this.verificarInvestimentosForAll();
	%this.verificarReliquiaInvest();
	%this.verificarArtefatoInvest();	
}

function jogo::verificacoesFinais(%this)
{
	%this.verificarGruposGlobal();
	%this.verificarObjetivosGlobal();
	%this.calcularCompletude(); //pega as porcentagens de completude dos objetivos e do império	
}

function jogo::setDataFim(%this)
{
	%this.dataFim = getLocalTime();
	%this.gameTimeFim = getRealTime();
}

function jogo::setTempoDeJogo(%this)
{
	%this.tempoDeJogo = mFloor((%this.gameTimeFim - %this.gameTimeInicio) /1000 /60);
}

function jogo::removerAiPlayer(%this)
{
	%this.playersAtivos--;
	%this.simPlayers.remove(%this.simPlayers.getObject(%this.playersAtivos));
}

function jogo::addObjGulok(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%player.mySimObj.add($obj_guloks);
		if(%player.objEspecial == 50){
			%player.objetivo1Percent = 100;
			%player.obj1Completo = true;
		} else {
			%player.objetivo1Percent = 0;	
		}
	}	
}

function jogo::pagarObj1Player(%this, %player)
{
	if(!%player.obj1Completo)
		return;
			
	%player.minerios -= %player.mySimObj.getObject(0).minerios;
	%player.petroleos -= %player.mySimObj.getObject(0).petroleos;
	%player.uranios -= %player.mySimObj.getObject(0).uranios;	
}

function jogo::pagarObj2Player(%this, %player)
{
	if(!%player.obj2Completo)
		return;
	
	%objetivo1 = %player.mySimObj.getObject(0);
	%objetivo2 = %player.mySimObj.getObject(1);
	
	if (!%player.obj1Completo || !%this.getObjetivosRecursosCongruentes(%player))
	{
		%player.minerios -= %objetivo2.minerios;
		%player.petroleos -= %objetivo2.petroleos;
		%player.uranios -= %objetivo2.uranios;
		return;
	}
	
	//Se chegou aki é porque os dois objetivos são congruentes!
	if(%objetivo1.minerios > 0 && %objetivo1.minerios >= %objetivo2.minerios)
		return; //já descontou tudo que precisava	
		
	if(%objetivo1.petroleos > 0 && %objetivo1.petroleos >= %objetivo2.petroleos)
		return; //já descontou o que precisava descontar
	
	if(%objetivo1.uranios > 0 && %objetivo1.uranios >= %objetivo2.uranios)
		return; //já descontou tudo que precisava	
		
	if(%objetivo1.petroleos > 0 && %objetivo1.petroleos < %objetivo2.petroleos)
		%player.petroleos -= (%objetivo2.petroleos - %objetivo1.petroleos); //desconta o resto
}

function jogo::getObjetivosRecursosCongruentes(%this, %player)
{
	if(%player.mySimObj.getObject(0).minerios > 0 && %player.mySimObj.getObject(1).minerios > 0)
		return true;
		
	if(%player.mySimObj.getObject(0).petroleos > 0 && %player.mySimObj.getObject(1).petroleos > 0)
		return true;
		
	if(%player.mySimObj.getObject(0).uranios > 0 && %player.mySimObj.getObject(1).uranios > 0)
		return true;
		
	return false;
}

function jogo::pagarRecursosDeObjetivos(%this, %player)
{
	%this.pagarObj1Player(%player);
	%this.pagarObj2Player(%player);
}

function jogo::darPontosPorObjetivosCumpridos(%this, %player)
{
	if(%player.obj1Completo)
	{
		if(%this.guloksDespertaram)
		{
			%player.obj1Pt = %player.objEspecial;
		}
		else
		{
			%player.obj1Pt = 5;
		}
	}
		
	if(%player.obj2Completo)
		%player.obj2Pt = 5;
}

function jogo::darPontosPorRecursosRestantes(%this, %player)
{
	%existeConjunto = %this.getPossuiConjunto(%player);
	
	while(%existeConjunto)
	{
		%this.converterConjuntoEmPontos(%player);		
		%existeConjunto = %this.getPossuiConjunto(%player);
	}
		
	%player.recursosPt += mFloor(%player.uranios/3);
	%player.recursosPt += mFloor(%player.petroleos/4);
	%player.recursosPt += mFloor(%player.minerios/5);
}

function jogo::getPossuiConjunto(%this, %player)
{
	if (%player.uranios > 0 && %player.petroleos > 0 && %player.minerios > 0)
		return true;
		
	return false;	
}

function jogo::converterConjuntoEmPontos(%this, %player)
{
	%player.recursosPt += 1;
	%player.uranios -= 1;
	%player.petroleos -= 1;
	%player.minerios -= 1;	
}

function jogo::darPontosPorMissoes(%this, %player)
{
	for (%i = 0; %i < %player.mySimInfo.getcount(); %i++)
	{
		%missao = %player.mySimInfo.getObject(%i);
		%this.darPontosPorUmaMissao(%player, %missao);
	}	
}

function jogo::darPontosPorUmaMissao(%this, %player, %missao)
{
	if (%missao.tipo !$= "build")
		return;
	
	%areaDaMissao = %this.getAreaDaMissao(%missao);
	
	if (!%player.mySimAreas.isMember(%areaDaMissao))
		return;
		
	if (%areaDaMissao.pos0Flag == 1)
		%player.missoesPt += 2;	
}

function jogo::darPontosPorFilantropias(%this, %player)
{
	%player.missoesPt += 3 * %player.filantropiasEfetuadas;
}

function jogo::darPontosPorImperio(%this, %player)
{
	%this.setImperioPlayer(%player);
	
	%imperiosFeitos = %this.getImperiosGlobal();
	%valorDoImperio = mFloor(10 / %imperiosFeitos);
		
	if(%player.imperio)
		%player.imperioPt = %valorDoImperio;
		//%player.imperioPt = 10;
}

function jogo::darPontosPorAlmirante(%this, %player)
{
	if(%player.persona.aca_i_3 < 1)
		return;
		
	%basesMaritimas = %this.getBasesMaritimasPlayer(%player);
	if(%basesMaritimas >= 5)
		%player.missoesPt += 3 + %player.persona.aca_i_3;
}

function jogo::darPontosPorCrisalidas(%this, %player)
{
	if(!isObject(%player.mySimCrisalidas))
		return;
	
	if(%player.mySimCrisalidas.getcount() <= 0)
		return;
			
	%player.missoesPt += (%player.mySimCrisalidas.getCount() * 3);
}

function jogo::darPontosPorMatriarcas(%this, %player)
{
	if(!isObject(%player.mySimMatriarcas))
		return;
		
	if(%player.mySimMatriarcas.getcount() <= 0)
		return;
		
	%player.missoesPt += (%player.mySimMatriarcas.getcount() * 5); 	
}

function jogo::darPontosPorDestruirGrandeMatriarca(%this, %player)
{
	if(!%this.guloksDespertaram)
		return;
		
	if(%player.objEspecial <= 0)
		return;
	
	%player.missoesPt += %player.objEspecial;
}

function jogo::montarDescricoesDeObjetivos(%this, %player)
{
	if (%player.mySimObj.getObject(0).desc2 $= ""){
		%player.obj1Desc = %player.mySimObj.getObject(0).desc1;
	} else {
		%player.obj1Desc = %player.mySimObj.getObject(0).desc1 @ "+" @ %player.mySimObj.getObject(0).desc2;
	}
		
	if (%player.mySimObj.getObject(1).desc2 $= ""){
		%player.obj2Desc = %player.mySimObj.getObject(1).desc1;
	} else {
		%player.obj2Desc = %player.mySimObj.getObject(1).desc1 @ "+" @ %player.mySimObj.getObject(1).desc2;
	}
}

function jogo::setNegAgoraPlayer(%this, %player)
{
	for(%i = 0; %i < %player.mySimExpl.getCount(); %i++)
	{
		%tempInfo = %player.mySimExpl.getObject(%i);
		if(%player.mySimInfo.isMember(%tempInfo))
			%player.negAgora++;		
	}
	
	%player.negAgora = %player.negAgora + %player.negInGame;
}

function jogo::setTotalDePontosPlayer(%this, %player)
{
	if(%this.poker && !%player.pk_fugiu)
	{
		%this.pk_jogo.avaliarMao(%player);
		%player.totalDePontos = %player.obj1Pt + %player.obj2Pt + %player.missoesPt + %player.recursosPt + %player.ImperioPt + %player.pk_pontos;
		return;	
	}
	
	%player.totalDePontos = %player.obj1Pt + %player.obj2Pt + %player.missoesPt + %player.recursosPt + %player.ImperioPt;
}

function jogo::setGanhadores(%this)
{
	if(%this.emDuplas)
	{
		%this.setGanhadoresEmDuplas();
		return;
	}
	
	%this.setGanhadoresNormal();
}

function jogo::setGanhadoresEmDuplas(%this)
{
	%this.setTotalDePontosPorDuplas();
	for(%i = 0; %i < %this.simDuplas.getCount(); %i++)
	{
		%dupla = %this.simDuplas.getObject(%i);	
		%this.setGanhadoraDupla(%dupla);
	}
}

function jogo::setTotalDePontosPorDuplas(%this)
{
	for(%i = 0; %i < %this.simDuplas.getCount(); %i++)
	{
		%dupla = %this.simDuplas.getObject(%i);
		%dupla.totalDePontos = %dupla.getObject(0).totalDePontos + %dupla.getObject(1).totalDePontos;
	}
}

function jogo::getGanhadoraDupla(%this, %dupla)
{
	if(%dupla.totalDePontos <= 0)
		return false;
	
	for(%i = 0; %i < %this.simDuplas.getCount(); %i++)
	{
		%tempDupla = %this.simDuplas.getObject(%i);
		if(%tempDupla != %dupla)
		{
			if(%dupla.totalDePontos < %tempDupla.totalDePontos)	
				return false;
		}
	}
	
	return true;
}

function jogo::setGanhadoraDupla(%this, %dupla)
{
	if(%this.getGanhadoraDupla(%dupla))
	{
		%dupla.getObject(0).ganhouOJogo = true;
		%dupla.getObject(1).ganhouOJogo = true;
		%dupla.ganhouOJogo = true;
		return;
	}
	
	%dupla.getObject(0).ganhouOJogo = false;
	%dupla.getObject(1).ganhouOJogo = false;
	%dupla.ganhouOJogo = false;
}

function jogo::setGanhadoresNormal(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);	
		%this.setGanhadorNormalPlayer(%player);
	}
}	

function jogo::getGanhadorNormalPlayer(%this, %player)
{
	if(%player.totalDePontos <= 0)
		return false;
	
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%tempPlayer = %this.simPlayers.getObject(%i);
		if(%tempPlayer != %player)
		{
			if(%player.totalDePontos < %tempPlayer.totalDePontos)	
				return false;
		}
	}
	
	return true;
}

function jogo::setGanhadorNormalPlayer(%this, %player)
{
	if(%this.getGanhadorNormalPlayer(%player))
	{
		%player.ganhouOJogo = true;
		return;
	}
	
	%player.ganhouOJogo = false;
}

function jogo::calcularPontosGlobal(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
					
		%this.pagarRecursosDeObjetivos(%player);
		%this.darPontosPorObjetivosCumpridos(%player);
		%this.darPontosPorRecursosRestantes(%player);
		%this.darPontosPorMissoes(%player);
		%this.darPontosPorFilantropias(%player);	
		%this.darPontosPorImperio(%player);
		%this.darPontosPorAlmirante(%player);
		%this.darPontosPorCrisalidas(%player);
		%this.darPontosPorMatriarcas(%player);
		
		%this.montarDescricoesDeObjetivos(%player);
		
		%this.setNegAgoraPlayer(%player);	
		
		%this.setTotalDePontosPlayer(%player);
	}	
}

function jogo::setNewStatsForAll(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%this.setNewStatsForPlayer(%player);		
	}	
}

function jogo::setNewStatsForPlayer(%this, %player)
{
	if(%this.tempoDeJogo <= 1)
		return;
		
	%player.persona.TAXOjogos += 1;
	%player.persona.TAXOvitorias += %player.ganhouOJogo;
	%player.persona.TAXOpontos += %player.totalDePontos;
	%player.persona.TAXOvisionario += %player.imperio;
	%player.persona.TAXOarrebatador += %player.arrebatadorAgora;
	%player.persona.TAXOcomerciante += %player.comercianteAgora;
	%player.persona.TAXOatacou += %player.atacou;
	%player.persona.TAXOcreditos += %player.creditosAgora;
	%player.persona.pk_power_plays += %player.partidaPerfeita;
	
	if(%this.poker)
		%player.persona.pk_vitorias += %player.ganhouOJogo;
	
	%player.persona.myComerciante = mFloor((%player.persona.TAXOcomerciante / %player.persona.TAXOjogos) * 100);
	%player.persona.myDiplomata = mFloor(((%player.persona.TAXOjogos - %player.persona.TAXOatacou) / %player.persona.TAXOjogos) * 100);
}

function jogo::zerarCreditosAgoraForAll(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%player = %this.simPlayers.getObject(%i);
		%player.creditosAgora = 0;	
	}
	%this.creditosDistribuidos = 0;		
}

function jogo::setFimDeJogoString(%this, %quemBateu)
{
	%this.dadosDoJogo = %this.playersAtivos SPC %quemBateu SPC %this.tempoDeJogo SPC %this.desastres SPC %this.creditosDistribuidos;
		
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		if(%this.poker){
			%this.setFimDeJogoPlayerPokerString(%player);		
		} else {
			%this.setFimDeJogoPlayerString(%player);	
		}
	}	
}

function jogo::setFimDeJogoPlayerString(%this, %player)
{
	%player.dadosDoJogo = %player.persona.nome;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.obj1Desc;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.obj1Pt;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.obj2Desc;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.obj2Pt;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.imperioPt;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.missoesPt;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.recursosPt;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.totalDePontos;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.creditosAgora;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.cVencedor;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.cVisionario;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.cArrebatador;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.cComerciante;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.cDiplomata;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.objetivo1Percent;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.objetivo2Percent;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.imperioPercent;
}

function jogo::setFimDeJogoPlayerPokerString(%this, %player)
{
	%player.dadosDoJogo = %player.persona.nome;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.obj1Desc;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.obj1Pt;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.obj2Desc;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.obj2Pt;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.imperioPt;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.missoesPt;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.recursosPt;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.totalDePontos;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.pk_fichas;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.ganhouOJogo;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.imperio;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.arrebatadorAgora;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.partidaPerfeita;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.pk_jogoFeitoProClient;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.objetivo1Percent;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.objetivo2Percent;
	%player.dadosDoJogo = %player.dadosDoJogo SPC %player.imperioPercent;
}

function jogo::setArrebatadorAgoraForAll(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		if(%player.obj1Completo && %player.obj2Completo)
			%player.arrebatadorAgora = true;
		else
			%player.arrebatadorAgora = false;
	}
}


function jogo::resetarPersonasProntas(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%player.persona.pronto = false;
	}
}

function jogo::CTCfinal(%this)
{
	if(%this.tempoDeJogo <= 1)
	{
		%this.jogoInvalido = true;
		%this.CTCfimDeJogoInvalido();
		return;	
	}
	
	%this.CTCfimDeJogo();
}

function jogo::CTCfimDeJogo(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
		commandToClient(%this.simPlayers.getObject(%i).client, 'fimDeJogo', %this.dadosDoJogo, 1, %this.player1.dadosDoJogo, %this.player2.dadosDoJogo, %this.player3.dadosDoJogo, %this.player4.dadosDoJogo, %this.player5.dadosDoJogo, %this.player6.dadosDoJogo, false, %this.assassino);
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'fimDeJogo', %this.dadosDoJogo, 1, %this.player1.dadosDoJogo, %this.player2.dadosDoJogo, %this.player3.dadosDoJogo, %this.player4.dadosDoJogo, %this.player5.dadosDoJogo, %this.player6.dadosDoJogo, false, %this.assassino);
}

function jogo::verificarInvestimentosForAll(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%this.verificarInvestimentosPlayer(%player);
	}	
}

function jogo::verificarInvestimentosPlayer(%this, %player)
{
	if(%player.persona.aca_pea_id $= "0" || %player.persona.aca_pea_id $= "")
		return;
	
	if(%player.mineriosInvestidos <= 0 && %player.petroleosInvestidos <= 0 && %player.uraniosInvestidos <= 0)
		return;
		
	%myServerPesq = criarServerPesq();
	%myServerPesq.url = "/torque/academia/investir?idPersona=" @ %player.persona.TAXOid @ "&min=" @ %player.mineriosInvestidos @  "&pet=" @ %player.petroleosInvestidos @  "&ura=" @ %player.uraniosInvestidos @ "&creditos=0&idPesqTorque=" @ %myServerPesq.num;
	$filas_handler.newFilaObj("investir", %myServerPesq.url, 2, %myServerPesq, %player.persona);			
}

function jogo::echoResultado(%this)
{
	echo("JOGO " @ %this.num @ " FINALIZADO EM " @ %this.tempoDeJogo @ " minutos!");
	for(%i = 0; %i < %this.vencedores.getCount(); %i++)
	{
		echo("Vencedor = " @ %this.vencedores.getObject(%i).persona.nome @ " -> " @ %this.vencedores.getObject(%i).totalDePontos @ " pontos.");
	}
}

function jogo::CTCfimDeJogoInvalido(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
		commandToClient(%this.simPlayers.getObject(%i).client, 'fimDeJogo', %this.dadosDoJogo, 0);
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'fimDeJogo', %this.dadosDoJogo, 0);
}

function jogo::setPartidaEncerrada(%this)
{
	%this.partidaEncerrada = true;
	%this.firstStart = false;
	
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%persona = %this.simPlayers.getObject(%i).persona;
		%persona.inGame = false;
	}
	
	if(%this.getSouJogoTreino() || (%this.tempoDeJogo <= 1) || %this.jogoInvalido)
		%this.sala.ocupada = false; //isso tem que ser feito somente quando recebe o proximo taxoid, ou se for jogo-treino, ou se for inválido
}

function jogo::recalcularGraduacaoForAll(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%persona = %this.simPlayers.getObject(%i).persona;
		%persona.setPatente();
		%persona.setPorcentagens();
	}	
}

//função funciona apenas para o Nexus Alquímico!
function jogo::verificarReliquiaInvest(%this){
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%player = %this.simPlayers.getObject(%i);
		if(%player.nexusInvest > 0){
			%myServerPesq = criarServerPesq();
			
			
			%myServerPesq = criarServerPesq();
			%myServerPesq.url = "/torque/academia/investir_artefato?idPersona=" @ %player.persona.TAXOid @ "&pesq=aca_art_2&qtde_cjs=" @ %player.nexusInvest @ "&idPesqTorque=" @ %myServerPesq.num;
			$filas_handler.newFilaObj("investir", %myServerPesq.url, 2, %myServerPesq, %player.persona);	
		}
	}
}
function jogo::verificarArtefatoInvest(%this){
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%player = %this.simPlayers.getObject(%i);
		if(%player.geoDisparos > 0){
			%myServerPesq = criarServerPesq();
			%myServerPesq.url = "/torque/academia/usar_artefato?idPersona=" @ %player.persona.TAXOid @ "&pesq=aca_art_1&idPesqTorque=" @ %myServerPesq.num;
			$filas_handler.newFilaObj("usar_artefato", %myServerPesq.url, 2, %myServerPesq, %player.persona);
		} else {
			if(%player.geoInvest > 0){
				%myServerPesq = criarServerPesq();
				%myServerPesq.url = "/torque/academia/investir_artefato?idPersona=" @ %player.persona.TAXOid @ "&pesq=aca_art_1&qtde_cjs=" @ %player.geoInvest @ "&idPesqTorque=" @ %myServerPesq.num;
				$filas_handler.newFilaObj("investir", %myServerPesq.url, 2, %myServerPesq, %player.persona);
			}
		}	
	}
}

function jogo::CTCpowerPlays(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);	
		if(%player.partidaPerfeita)
			commandToClient(%player.client, 'registrarPartidaPerfeita');
	}
}	


