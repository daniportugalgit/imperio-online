// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverJogo.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sábado, 22 de dezembro de 2007 22:09
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
$jogoNumUniversal = 0; //zera a variável que marca quantos jogos foram feitos nesta sessão do server;

function serverCmdIniciarJogo(%client)
{
	if(%client.persona.offLine)
		return;
	
	%sala = %client.persona.sala;
	if(%sala.simPersonas.getCount() > %sala.planeta.lotacao && !%sala.emDuplas)
	{
		%sala.CTClotacaoEstourou(%client);
		return;
	}
	
	if(%sala.jogoTAXOid !$= "" || %sala.simPersonas.getCount() == 2 || (%sala.simPersonas.getCount() == 4 && %sala.emDuplas)){
		%sala.ocupada = true;
		serverAtualizarAtrioParaTodos();
		serverCriarJogoObj(%sala); 
	} else {
		commandToClient(%client, 'aguardeTAXOid');	
	}
}

function serverCriarJogoObj(%sala){
	%classe = "jogo";
	%eval = "$jogo" @ $jogoNumUniversal @ "= new ScriptObject(){";
	%eval = %eval @ "class = " @ %classe @ ";";
	%eval = %eval @ "taxoId = " @ %sala.jogoTAXOid @ ";";
	%eval = %eval @ "};";
	eval(%eval);
	%eval = "$jogo" @ $jogoNumUniversal @ ".inicializar(" @ %sala @ ");";
	eval(%eval);
}

function jogo::inicializar(%this, %sala){
	%this.num = $jogoNumUniversal; 
	%this.sala = %sala;
	%sala.jogo = %this;
			
	%this.zerarVariaveisBasicas();
	%this.herdarCaracteristicasDaSala();
	
	%this.setAreas();
	%this.setObjetivosPool();
	%this.setInfoPool();
	%this.setGruposPool();
	%this.setGruposParaSorteio();
	%this.setGruposNoJogo(); 
					
	%this.sortearOrdem();
	
	if(%this.poker)
		$poker_handler.setPk_jogo(%this);
	
	%this.jogadorDaVez = %this.player1; 
	
	$jogoNumUniversal++;
	
	echo("Jogo(" @ %this.num @ ") Criado!");
}

function jogo::herdarCaracteristicasDaSala(%this)
{
	%this.playersAtivos = %this.sala.simPersonas.getCount();
	%this.setSimPlayers();
	
	%this.setSimPersonasOnline();
	
	%this.planeta = %this.sala.planeta; 
	%this.turnoTime = %this.sala.turno; 
	
	%this.emDuplas = %this.sala.emDuplas;
	%this.semPesquisas = %this.sala.semPesquisas;
	%this.handicap = %this.sala.handicap;	
	%this.poker = %this.sala.poker;
	%this.set = %this.sala.set;
	
	%this.observadorOn = %this.sala.observadorOn;
	%this.observador = %this.sala.observador;
	
	if(%this.getForceHandicap())
		%this.setForceHandicap();
	
	if(%this.sala.observadorOn)
		%this.observador.jogo = %this;
}

function jogo::getForceHandicap(%this)
{
	if(%this.emDuplas)
		return false;
		
	if(%this.semPesquisas)
		return false;
		
	if(%this.poker)
		return false;
		
	//if(%this.set) //
	//	return false;
		
	if(%this.handicap)
		return false;
		
	//se chegou aki, é jogo Clássico ou em SET:
	%this.ordenarPersonasOnlinePorFatorImperial();
	%maiorFator = %this.personasOnline.getObject(%this.playersAtivos-1).mediaVit;
	%menorFator = %this.personasOnline.getObject(0).mediaVit;
	
	if(%maiorFator - 100 < %menorFator)
		return false;
	
	echo("FORCEHANDICAP!");
	return true;
}

function jogo::setForceHandicap(%this)
{
	%this.handicap = true;
	%this.sala.tipoDeJogo = "handicap";
	%this.CTCforceHandicap();
}

function jogo::CTCforceHandicap(%this)
{
	for(%i = 0; %i < %this.sala.simPersonas.getCount(); %i++)
		commandToClient(%this.sala.simPersonas.getObject(%i).client, 'forceHandicap');
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'forceHandicap');
}

function jogo::zerarVariaveisBasicas(%this)
{
	%this.playersMortos = 0;
	%this.rodada = 0;
	%this.desastres = 0;
	%this.disparosOrbitais = 0;
}


function jogo::setAreas(%this)
{
	%numeroDeAreas = %this.planeta.numDeAreas;
	%this.areasPool = new SimSet();
	for(%i = 0; %i < %numeroDeAreas; %i++)
	{
		%area = %this.planeta.areasPool.getObject(%i);
		%eval = "%this." @ %area.getName() @ " = %area.createCopy(%this);";
		eval(%eval);
		%eval = "%this.areasPool.add(%this." @ %area.getName() @ ");";
		eval(%eval);
		
		//força a marcação dos oceanos (???) TODO:
		if(%area.getName() $= "oceanoPacificoOcidental" || %area.getName() $= "oceanoPacificoOriental" || %area.getName() $= "oceanoArtico" || %area.getName() $= "oceanoAtlanticoNorte" || %area.getName() $= "oceanoAtlanticoSul" || %area.getName() $= "oceanoIndico")
		{
			%eval = "%this." @ %area.getName() @ ".oceano = 1;";
			eval(%eval);
		}
	}	
}

function jogo::setObjetivosPool(%this)
{
	%this.objPool = new SimSet();
	
	for(%i = 0; %i < %this.planeta.objPool.getCount(); %i++)
	{
		%this.objPool.add(%this.planeta.objPool.getObject(%i));
	}	
}

function jogo::setInfoPool(%this)
{
	%this.infoPool = new SimSet();
	
	for(%i = 0; %i < %this.planeta.infoPool.getCount(); %i++)
	{
		%this.infoPool.add(%this.planeta.infoPool.getObject(%i));
	}	
}

function jogo::setGruposPool(%this)
{
	%this.gruposPool = new SimSet();
	%numDeGrupos = %this.planeta.grupos.getCount();
	
	for(%i = 0; %i < %numDeGrupos; %i++)
	{
		%this.gruposPool.add(%this.planeta.grupos.getObject(%i));
	}	
}

function jogo::setGruposParaSorteio(%this)
{
	%this.gruposParaSorteioPool = new SimSet();
	%numDeGruposParaSorteio = %this.planeta.gruposParaSorteio.getCount();
	for(%i = 0; %i < %numDeGruposParaSorteio; %i++)
	{
		%this.gruposParaSorteioPool.add(%this.planeta.gruposParaSorteio.getObject(%i));
	}
	
	if(%this.emDuplas)
	{
		%this.gruposDeDuplasPool = new SimSet();
		%numDeGruposDeDuplas = %this.planeta.gruposDeDuplas.getCount();
		for(%i = 0; %i < %numDeGruposDeDuplas; %i++)
		{
			%this.gruposDeDuplasPool.add(%this.planeta.gruposDeDuplas.getObject(%i));
		}
	}	
}

function jogo::setSimPlayers(%this)
{
	%this.simPlayers = new SimSet();
	
	for(%i = 0; %i < %this.sala.simPersonas.getCount(); %i++)
	{
		createPlayer(%this);
	}
}


function jogo::setGruposNoJogo(%this){
	%this.gruposNoJogo = new SimSet();
	for(%i = 0; %i < %this.planeta.grupos.getCount(); %i++){
		%grupo = %this.gruposPool.getObject(%i);
		%eval = "%this." @ %grupo.nome @ " = new ScriptObject(){};";
		eval(%eval);
		%eval = "%this.gruposNoJogo.add(%this." @ %grupo.nome @ ");";
		eval(%eval);
		%eval = "%this." @ %grupo.nome @ ".simAreas = new SimSet();";
		eval(%eval);
		%eval = "%this." @ %grupo.nome @ ".nome = %grupo.nome;";
		eval(%eval);
		%eval = "%this." @ %grupo.nome @ ".recurso = %grupo.recurso;";
		eval(%eval);
		for(%j = 1; %j < %grupo.numDeAreas + 1; %j++){
			%areaNome = "%grupo.area" @ %j @ "Nome";
			%eval = "%areaNome = " @ %areaNome @ ";";
			eval(%eval);
			%eval = "%areaNoJogo = %this." @ %areaNome @ ";";
			eval(%eval);
			%eval = "%this." @ %grupo.nome @ ".simAreas.add(%areaNoJogo);";
			eval(%eval);
		}
	}
}

function jogo::sortearOrdem(%this){
	%this.efetuarSorteioDeOrdem();
	%this.setPersonasNosPlayers();
	%this.CTCsetMyPlayer();
	%this.setDuplasNosPlayers();
	%this.setObservador();
	%this.resetStatusDaPartida();
	%this.setSetFactorForAll();
	
	if(%this.poker)
		%this.setBlindFactorForAll();
			
	%this.CTCsorteioDeOrdemShow();
}

function jogo::efetuarSorteioDeOrdem(%this)
{
	if(%this.emDuplas)
	{
		%this.sortearOrdemEmDuplas();
		return;		
	}
	
	if(%this.poker)
	{
		%this.sortearOrdemPorBlindFactor();
		return;		
	}
	
	if(%this.set)
	{
		%this.sortearOrdemPorSetFactor();
		return;		
	}
	
	if(!%this.handicap)
	{
		%this.sortearOrdemNormal();
		return;
	}
	
	%this.ordenarPersonasOnlinePorFatorImperial();
	%maiorFator = %this.personasOnline.getObject(%this.playersAtivos-1).mediaVit;
	%menorFator = %this.personasOnline.getObject(0).mediaVit;
	
	if(%maiorFator-50 > %menorFator)
	{
		%this.darBonusPorHandicap();
		%this.randomizarOrdemCrescente();
		return;
	}
	
	%this.sortearOrdemNormal();	
}

function jogo::setSimPersonasOnline(%this)
{
	if(isObject(%this.personasOnline)){
		%this.personasOnline.clear();
	} else {
		%this.personasOnline = new SimSet();
	}
	
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%persona = %this.sala.simPersonas.getObject(%i);
		if(%persona.offLine == false){
			%this.personasOnline.add(%persona);
		} else {
			%this.sala.simPersonas.remove(%persona);
		}
	}
}

function jogo::sortearOrdemNormal(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%sorteio = dado(%this.personasOnline.getCount(), -1);
		%this.sorteioPersona[%i] = %this.personasOnline.getObject(%sorteio);
		%this.personasOnline.remove(%this.sorteioPersona[%i]);
	}		
}

function jogo::sortearOrdemEmDuplas(%this)
{
	%this.marcarTempDuplas();
	
	for(%i = 0; %i < %this.playersAtivos / 2; %i++)
	{
		%sorteio = dado(%this.personasOnline.getCount(), -1);
		%this.sorteioPersona[%i] = %this.personasOnline.getObject(%sorteio);
		%this.sorteioPersona[%i+(%this.playersAtivos / 2)] = %this.sorteioPersona[%i].myTempDupla;
		%this.personasOnline.remove(%this.sorteioPersona[%i]);
		%this.personasOnline.remove(%this.sorteioPersona[%i].myTempDupla);
	}	
}

function jogo::sortearOrdemPorBlindFactor(%this)
{
	%this.ordenarPersonasOnlinePorBlindFactor();
	for(%i = 0; %i < %this.playersAtivos; %i++)
		%this.sorteioPersona[%i] = %this.personasOnline.getObject(%i);
}

function jogo::ordenarPersonasOnlinePorFatorImperial(%this)
{
	for (%i = 0; %i < %this.personasOnline.getCount(); %i++)
	{
		for (%j = 1; %j < %this.personasOnline.getCount() - %i; %j++)
		{
			if(%this.personasOnline.getObject(%j-1).mediaVit > %this.personasOnline.getObject(%j).mediaVit)
				%this.personasOnline.reorderChild(%this.personasOnline.getObject(%j), %this.personasOnline.getObject(%j-1));
		}
	}	
}

function jogo::ordenarPersonasOnlinePorBlindFactor(%this)
{
	%this.ordenarPersonasOnlineAleatoriamente();
	
	for (%i = 0; %i < %this.playersAtivos; %i++)
	{
		for (%j = 1; %j < %this.playersAtivos - %i; %j++)
		{
			if(%this.personasOnline.getObject(%j-1).blindFactor < %this.personasOnline.getObject(%j).blindFactor)
				%this.personasOnline.reorderChild(%this.personasOnline.getObject(%j), %this.personasOnline.getObject(%j-1));
		}
	}
}

function jogo::sortearOrdemPorSetFactor(%this)
{
	%this.ordenarPersonasOnlinePorSetFactor();
	for(%i = 0; %i < %this.playersAtivos; %i++)
		%this.sorteioPersona[%i] = %this.personasOnline.getObject(%i);
}

function jogo::ordenarPersonasOnlinePorSetFactor(%this)
{
	%this.ordenarPersonasOnlineAleatoriamente();
	
	for (%i = 0; %i < %this.playersAtivos; %i++)
	{
		for (%j = 1; %j < %this.playersAtivos - %i; %j++)
		{
			if(%this.personasOnline.getObject(%j-1).setFactor < %this.personasOnline.getObject(%j).setFactor)
				%this.personasOnline.reorderChild(%this.personasOnline.getObject(%j), %this.personasOnline.getObject(%j-1));
		}
	}
}

function jogo::ordenarPersonasOnlineAleatoriamente(%this)
{
	%tempSimSet = new SimSet();
	for (%i = 0; %i < %this.playersAtivos; %i++)
	{
		%persona = %this.personasOnline.getObject(%i);
		%tempSimSet.add(%persona);
	}
	
	for (%i = 0; %i < %this.playersAtivos; %i++)
	{
		%sorteio = dado(%tempSimSet.getCount(), -1);
		%persona = %tempSimSet.getObject(%sorteio);
		%persona.tempRandomFactor = %i;
		%tempSimSet.remove(%persona);
	}
		
	for (%i = 0; %i < %this.playersAtivos; %i++)
	{
		for (%j = 1; %j < %this.playersAtivos - %i; %j++)
		{
			if(%this.personasOnline.getObject(%j-1).tempRandomFactor < %this.personasOnline.getObject(%j).tempRandomFactor)
				%this.personasOnline.reorderChild(%this.personasOnline.getObject(%j), %this.personasOnline.getObject(%j-1));
		}
	}	
}

function jogo::randomizarOrdemCrescente(%this)
{
	for(%i = 4; %i > 0; %i--)
	{
		if(%this.playersAtivos > %i)
		{
			%randomSorteio = dado(3, 0);
			if(%randomSorteio == 3)
				%this.personasOnline.reorderChild(%this.personasOnline.getObject(%i), %this.personasOnline.getObject(%i-1));
		}	
	}
		
	for (%i = 0; %i < %this.playersAtivos; %i++){
		%this.sorteioPersona[%i] = %this.personasOnline.getObject(%i);
	}	
}

function jogo::darBonusPorHandicap(%this)
{
	%personasCount = %this.personasOnline.getCount();
	%maiorFator = %this.personasOnline.getObject(%personasCount-1).mediaVit;
	%maisForteEhGulok = %this.getMaisForteEhGulok();
	
	for (%i = 0; %i < %personasCount; %i++)
	{
		%persona = %this.personasOnline.getObject(%i);
		%persona.bonusDifImp = 0;
		%persona.bonusDifRec = 0;
		
		%difPraMim = %maiorFator - %persona.mediaVit;
		
		if(%difPraMim > 17){
			%persona.bonusDifImp = mFloor(%difPraMim / 8);
		} else {
			%persona.bonusDifImp = 0;
		}
		
		if(%persona.especie $= "humano" && %maisForteEhGulok && %persona.mediaVit <= 20)
		{
			%persona.bonusDifRec = 1;
		}	
	}		
}

function jogo::getMaisForteEhGulok(%this)
{
	%personaMaisForte = %this.personasOnline.getObject(%this.personasOnline.getCount() -1);
	if(%personaMaisForte.especie $= "gulok")
		return true;
		
	return false;
}

function jogo::setPersonasNosPlayers(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%player.persona = %this.sorteioPersona[%i];
		%player.persona.player = %player;
		%player.persona.jogo = %this;
		%player.persona.inGame = true;
		%player.client = %this.sorteioPersona[%i].client;
		%player.client.player = %player;
				
		%this.setBonusInicialPlanetario(%player);
				
		if(%this.handicap)
			%player.impBonus += %player.persona.bonusDifImp;
	}	
}

function jogo::setBonusInicialPlanetario(%this, %player)
{
	if(%this.planeta.nome $= "Teluria" && %player.persona.aca_pln_1 > 0)
	{
		%player.impBonus = %player.persona.patente.impBonus + 3;
		return;
	}
	
	if(%this.planeta.nome $= "Ungart" && %player.persona.aca_v_6 > 0 && %player.persona.especie $= "humano")
	{
		%player.impBonus = %player.persona.patente.impBonus + 3;
		return;
	}
	
	%player.impBonus = %player.persona.patente.impBonus;	
}

function jogo::CTCsetMyPlayer(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		commandToClient(%player.client, 'setMyPlayer', %player.id, %this.planeta.nome, %this.playersAtivos, %player.impBonus, %this.turnoTime);	
	}
}

function jogo::setDuplasNosPlayers(%this)
{
	if(!%this.emDuplas)
		return;
		
	if(%this.playersAtivos == 4){
		%this.player1.myDupla = %this.player3;	
		%this.player2.myDupla = %this.player4;	
		%this.player3.myDupla = %this.player1;	
		%this.player4.myDupla = %this.player2;
	} else if(%this.playersAtivos == 6){
		%this.player1.myDupla = %this.player4;	
		%this.player2.myDupla = %this.player5;	
		%this.player3.myDupla = %this.player6;	
		%this.player4.myDupla = %this.player1;
		%this.player5.myDupla = %this.player2;
		%this.player6.myDupla = %this.player3;
	}
	
	%this.setSimDuplas();
	%this.CTCsetMyDupla();
}

function jogo::setSimDuplas(%this)
{
	%this.simDuplas = new SimSet();
	%this.simDupla1 = new SimSet();
	%this.simDupla2 = new SimSet();
	
	if(%this.playersAtivos == 4)
	{
		%this.simDupla1.add(%this.player1);
		%this.simDupla1.add(%this.player3);
		%this.simDupla2.add(%this.player2);
		%this.simDupla2.add(%this.player4);
		%this.simDuplas.add(%this.simDupla1);
		%this.simDuplas.add(%this.simDupla2);
		return;
	}
	
	%this.simDupla3 = new SimSet();
	
	%this.simDupla1.add(%this.player1);
	%this.simDupla1.add(%this.player4);
	%this.simDupla2.add(%this.player2);
	%this.simDupla2.add(%this.player5);
	%this.simDupla3.add(%this.player3);
	%this.simDupla3.add(%this.player6);
	%this.simDuplas.add(%this.simDupla1);
	%this.simDuplas.add(%this.simDupla2);
	%this.simDuplas.add(%this.simDupla3);
}

function jogo::CTCsetMyDupla(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		commandToClient(%player.client, 'setMyDupla', %this.playersAtivos);	
	}	
}

function jogo::setObservador(%this)
{
	if(!%this.observadorOn)
		return;
	
	serverCriarObservador(%this);
	
	%this.observador.player = %this.playerObservador;	
	%this.observador.jogo = %this;
	%this.observador.player.client = %this.observador;
	%this.CTCsetObservadorPlayer();
}

function jogo::CTCsetObservadorPlayer(%this)
{
	commandToClient(%this.observador, 'setMyPlayer', "OBSERVADOR", %this.planeta.nome, %this.playersAtivos);	
}

function jogo::resetStatusDaPartida(%this)
{
	%this.partidaIniciada = false;
	%this.partidaEncerrada = false;
	%this.firstStart = true;	
}

function jogo::CTCsorteioDeOrdemShow(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
	{
		%client = %this.simPlayers.getObject(%i).client;
		commandToClient(%client, 'SorteioDeOrdemShow', %this.playersAtivos, %this.player1.persona.nome, %this.player2.persona.nome, %this.player3.persona.nome, %this.player4.persona.nome, %this.player5.persona.nome, %this.player6.persona.nome);
	}
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'SorteioDeOrdemShow', %this.playersAtivos, %this.player1.persona.nome, %this.player2.persona.nome, %this.player3.persona.nome, %this.player4.persona.nome, %this.player5.persona.nome, %this.player6.persona.nome);
}

function jogo::marcarTempDuplas(%this){
	for(%i = 1; %i < 7; %i++){
		%persona[%i] = %this.sala.simPersonas.getObject(%i-1);
		%eval = "%personaDuplaNum[%i] = %this.sala.p" @ %i @ "Dupla;";
		eval(%eval);
	}
	
		
	for(%i = 1; %i < 7; %i++){
		for(%j = 1; %j < 7; %j++){
			if(%personaDuplaNum[%i] == %personaDuplaNum[%j]){
				if(%i != %j){
					%persona[%i].myTempDupla = %persona[%j];
				}
			}
		}
		
	}
}

function jogo::organizarPersonasPorFator(%this){
	//cria o simSet de personas:
	%personasOnline = new SimSet();
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%persona = %this.sala.simPersonas.getObject(%i);
		if(%persona.offLine == false){
			%personasOnline.add(%persona);
		} else {
			%this.sala.simPersonas.remove(%persona);
		}
	}
	%personaCount = %personasOnline.getCount();
	echo("Ordem Inicial:");
	%personasOnline.listObjects();
	
	for (%i = 0; %i < %personaCount; %i++){
   		for (%j = 1; %j < %personaCount - %i; %j++){
      		if(%personasOnline.getObject(%j-1).mediaVit > %personasOnline.getObject(%j).mediaVit){
        		%personasOnline.reorderChild(%personasOnline.getObject(%j), %personasOnline.getObject(%j-1));
				echo("Re-ordenando: " @ %personasOnline.getObject(%j).nome @ " tem fator maior que " @ %personasOnline.getObject(%j-1).nome);
			}
		}
	}
	
	echo("Ordem Final:");
	%personasOnline.listObjects();
}

function jogo::setColor(%this, %color, %player){
	%player.corR = getWord(%color, 0);
	%player.corG = getWord(%color, 1);
	%player.corB = getWord(%color, 2);
	%player.corA = getWord(%color, 3);	
	%player.myColor = getWord(%color, 4);
}

function jogo::iniciarPartida(%this){
	
				
	//atualiza o jogador da vez em cada client 
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%player = %this.simPlayers.getObject(%i);
		%player.prospeccao = %player.persona.aca_c_1;
		%player.airDrops = %player.persona.aca_v_5;
		%player.filantropiaPossivel = %player.persona.aca_i_2;
		%player.filantropiasEfetuadas = 0;
		%player.terceiroLiderOn = false;
		%player.disparosOrbitais = 0;
		%player.comercianteAgora = 1;
		%player.geoDisparos = 0;
		if(%player.aca_av_3 > 0){
			%player.reqOcultar = 7 - (%player.aca_av_3 * 2);
			%player.ocultarCusto = 7 - (%player.aca_av_3 * 2);
		}
		commandToClient(%player.client, 'iniciarPartida', %this.playersAtivos);		
	}
	if(%this.observadorOn){
		commandToClient(%this.observador, 'iniciarPartida', %this.playersAtivos);	
	}

	%this.partidaIniciada = true;
	%this.primeiraRodada = true;
	%this.rodada = 1;
	%this.dataInicio = getLocalTime();
	%this.gameTimeInicio = getRealTime();
	%this.setHorarioNobre();
	if(%this.playersAtivos > 2 || (%this.emDuplas == true && %this.playersAtivos == 6))
	{
		%this.criarURLinicial();
		$filas_handler.newFilaObj("criar_jogo", %this.iniciarUrl, 1, %this);
	}
	%this.inicializarTurno();
	%this.setGuloksVars();
}

function jogo::setHorarioNobre(%this)
{
	%exp = getWord(%this.dataInicio, 1);
	%exp = explode(%horarioInicial, ":");
	%this.horaInicial = %exp.get[0];
	trace("%this.horaInicial == " + %this.horaInicial);
	if(%this.horaInicial > 20)
	{
		%this.horarioNobre = true;
		trace("!-> Iniciando jogo em HORÁRIO NOBRE.");
	}
}

function jogo::criarURLinicial(%this)
{
	for (%i = 0; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%idsPersona = %idsPersona @ %player.persona.TAXOid @ ";";
	}

	%newDataInicio1 = firstWord(%this.dataInicio);
	%newDataInicio2 = getWord(%this.dataInicio, 1);
	%newDataInicio = %newDataInicio1 @ "%20" @ %newDataInicio2;	
	%this.newDataInicio = %newDataInicio;
	%URL = "/torque/jogo/iniciar?idJogo=" @ %this.taxoId @ "&idsPersona=" @ %idsPersona @ "&sala=sala%20" @ %this.sala.num @ "&data_inicio=" @ %newDataInicio @ "&idJogoTorque=" @ %this.num @ "&tipo_jogo=" @ %this.getTipoDeJogoNum();
	%this.iniciarUrl = %URL;
}

function jogo::getTipoDeJogoNum(%this)
{
	if(%this.handicap)
		return 2;
		
	if(%this.set)
		return 3;
		
	if(%this.semPesquisas)
		return 4;
		
	if(%this.emDuplas)
		return 5;
		
	if(%this.poker)
		return 6;
	
	return 1; //clássico
}

function jogo::setGuloksVars(%this){
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%player = %this.simPlayers.getObject(%i);
		if(%player.persona.especie $= "gulok"){
			if(%player.persona.aca_v_2 > 0){
				%player.instinto = true;
			} else {
				%player.instinto = false;	
			}
			%player.horda = %player.persona.aca_a_2;
			%player.exoEsqueleto = %player.persona.aca_a_1;
		} else {
			%player.instinto = false;
			%player.horda = 0;
			%player.exoEsqueleto = 0;
		}
		for(%j = 0; %j < %this.playersAtivos; %j++){
			%tempplayer = %this.simPlayers.getObject(%j);
			commandToClient(%tempplayer.client, 'setGuloksVars', %player.id, %player.instinto, %player.horda, %player.exoEsqueleto);		
		}
		if(%this.observadorOn){
			commandToClient(%this.observador, 'setGuloksVars', %player.id, %player.instinto, %player.horda, %player.exoEsqueleto);	
		}
	}
}

function jogo::killMe(%this){
	%this.simPlayers.delete();
	
	//deletar as Áreas:
	%numeroDeAreas = $areasDeTerra.getCount();
	for(%i = 0; %i < %numeroDeAreas; %i++){
		%area = $areasDeTerra.getObject(%i);
		
		%eval = "%this." @ %area.getName() @ ".myFronteiras.delete();";
		eval(%eval); //deleta o simFronteiras
		%eval = "%this." @ %area.getName() @ ".delete();";
		eval(%eval); //deleta a área
	}
	
	%this.objPool.delete();
	%this.infoPool.delete();
	%this.gruposPool.delete();
	
	%this.sala.jogo = "no";
	%this.delete();
}

function jogo::verificarMeuRegistro(%this){
	if(%this.registrado){
		%this.safeDelete();
	} else {
		//grava num arquivo que será enviado posteriormente:
		serverWriteURLToBugFile(serverCriarURL(%jogo));
	}
}

function jogo::getUltimoPlayer(%this){
	//pega o último player:
	%ultimoPlayer = %this.simPlayers.getObject(%this.playersAtivos-1);
	
	return %ultimoPlayer;
}




function jogo::setBlindFactorForAll(%this)
{
	%player1 = %this.simPlayers.getObject(0);
	%player1.persona.blindFactor = 1;
	
	for(%i = 1; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%player.persona.blindFactor = %this.playersAtivos - (%i - 1);
	}
}

function jogo::setSetFactorForAll(%this)
{
	%player1 = %this.simPlayers.getObject(0);
	%player1.persona.setFactor = 1;
	
	for(%i = 1; %i < %this.playersAtivos; %i++)
	{
		%player = %this.simPlayers.getObject(%i);
		%player.persona.setFactor = %this.playersAtivos - (%i - 1);
	}
}

