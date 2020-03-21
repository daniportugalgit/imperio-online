// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverSorteios.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  domingo, 28 de outubro de 2007 11:54
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  Sorteios iniciais: Objetivos, Escolha de cores e Grupos
//                    :  
//                    :  
// ============================================================
//serverCOMMANDS:

//Sorteio de objetivos 2.0 (a função retorna o Objetivo sorteado, para que ele seja passada pro cliente, além de marcar tudo no server):
function serverCmdSortearObjetivo(%client, %carta){
	%jogo = %client.player.jogo;
	echo("JOGO(" @ %jogo.num @ ")");

	if (%jogo.objPool.getCount() > 0){
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			commandToClient(%jogo.simPlayers.getObject(%i).client, 'apagarCartaObjetivo', %carta);
		}
		if(%jogo.observadorOn){
			commandToClient(%jogo.observador, 'apagarCartaObjetivo', %carta);
		}
		
		%maxObjIndice = %jogo.objPool.getCount();
		
		%sorteio = dado(%maxObjIndice, -1);
		if (!isObject(%jogo.objPool.getObject(%sorteio))){
			%sorteio = 0;
		}
		
		%objSorteado = %jogo.objPool.getObject(%sorteio);
		%jogo.jogadorDaVez.mySimObj.add(%objSorteado);
		
		%jogo.objPool.remove(%objSorteado);	
	
		echo("OBJ SORTEADO (Jogo " @ %jogo.num @ ", " @ %jogo.jogadorDaVez.id @ "): " @ %objSorteado.desc1 @ "+" @ %objSorteado.desc2);
						
		commandToClient(%jogo.jogadorDaVez.client, 'addObjetivo', %objSorteado.num);
		
		if(%jogo.jogadorDaVez.mySimObj.getCount() > 1){
			%jogo.passarAVezEscolhendoObjetivos();
		}
	} else {
		echo("Não há mais Objetivos no Jogo " @ %jogo.num);
	}
}
//////

function jogo::autoSortearObjetivo(%this){
	if (%this.objPool.getCount() > 0){
		for(%i = 0; %i < %this.playersAtivos; %i++){
			commandToClient(%this.simPlayers.getObject(%i).client, 'apagarCartaObjetivo', 1);
		}
		if(%jogo.observadorOn){
			commandToClient(%jogo.observador, 'apagarCartaObjetivo', 1);
		}
		
		%maxObjIndice = %this.objPool.getCount();
		
		%sorteio = dado(%maxObjIndice, -1);
		if (!isObject(%this.objPool.getObject(%sorteio))){
			%sorteio = 0;
		}
		
		%objSorteado = %this.objPool.getObject(%sorteio);
		%this.jogadorDaVez.mySimObj.add(%objSorteado);
		
		%this.objPool.remove(%objSorteado);	
	
		echo("OBJ SORTEADO (Jogo " @ %this.num @ ", " @ %this.jogadorDaVez.id @ "): " @ %objSorteado.desc1 @ "+" @ %objSorteado.desc2);
						
		commandToClient(%this.jogadorDaVez.client, 'addObjetivo', %objSorteado.num);
		
		if(%this.jogadorDaVez.mySimObj.getCount() > 1){
			%this.passarAVezEscolhendoObjetivos();
		}
	} else {
		echo("Não há mais Objetivos no Jogo " @ %this.num);
	}	
}

////////////////////////////////////////////////////////////////////////

function jogo::autoSortearGrupo(%this){	
	%resultado = dado(%this.gruposParaSorteioPool.getCount(), -1);
	%grupoSorteado = %this.gruposParaSorteioPool.getObject(%resultado);
	%this.gruposParaSorteioPool.remove(%grupoSorteado);
		
	%jogadorDaVez = %this.jogadorDaVez;
	
	%eval = "commandToClient(%jogadorDaVez.client, 'mark" @ %grupoNome @ "', %jogadorDaVez.corR, %jogadorDaVez.corG, %jogadorDaVez.corB, %jogadorDaVez.corA);";
	eval(%eval);
	%this.jogadorDaVez.grupoInicial = %grupoSorteado.nome;
}

//e se retirar do jogo um jogador que não sorteia suas coisas? Ele poderia simplesmente sair do jogo? Não, já está marcado como x players; Que fazer?
//Ué, naum daria pra remarcar o número de players? Simplesmente mata o truta!



//////////////////////////////////////////////
//////////////////////////////////////////////
//serverFUNCTIONS:
//GRUPOS:

function jogo::escolherGrupo(%this, %grupoNome, %forced){
	%jogadorDaVez = %this.jogadorDaVez;
	
	//%eval = "commandToClient(%jogadorDaVez.client, 'mark" @ %grupoNome @ "', %jogadorDaVez.corR, %jogadorDaVez.corG, %jogadorDaVez.corB, %jogadorDaVez.corA);";
	//eval(%eval);
	
	if(!%forced){
		commandToClient(%jogadorDaVez.client, 'markGrupo', %grupoNome, %jogadorDaVez.corR, %jogadorDaVez.corG, %jogadorDaVez.corB, %jogadorDaVez.corA);
	}
	
	%jogadorDaVez.grupoInicial = %grupoNome;
}



//Sorteio de Infos (a função retorna a info sorteada, para que ela seja passada pro cliente, além de marcar tudo no server):
function jogo::sortearInfo(%this){
	if(!%this.partidaEncerrada){
		%jogadorDaVez = %this.jogadorDaVez;
		if (%this.infoPool.getCount() > 0){
			%maxInfoIndice = %this.infoPool.getCount();
			
			%sorteio = dado(%maxInfoIndice, -1);
			if (!isObject(%this.infoPool.getObject(%sorteio))){
				%sorteio = 0;	
			}
			
			%infoSorteada = %this.infoPool.getObject(%sorteio);
			%jogadorDaVez.mySimInfo.add(%infoSorteada);
			%this.infoPool.remove(%infoSorteada);
			%eval = "%this.info" @ %infoSorteada.num @ " = new ScriptObject(){};";
			eval(%eval);
			%eval = "%infoScriptNojogo = %this.info" @ %infoSorteada.num @ ";";
			eval(%eval);
			%infoScriptNoJogo.dono = %jogadorDaVez;
			%infoScriptNoJogo.compartilhada = false;
			return %infoSorteada.num;
		} else {
			echo("Não há mais informações");
		}
	}
}



////////////////

function serverCmdPassarAVezAntesDoInicio(%client){
	%player = %client.player;
	%jogo = %player.jogo;
	
	if(%player.mySimObj.getCount() < 2){
		%jogo.autoSortearObjetivo();
		if(%player.mySimObj.getCount() < 2){
			%jogo.autoSortearObjetivo();
		}
	} else {
		if(%player.myColor !$= ""){
			//se ele ainda não escolheu carta de grupo, escolher um grupo e posicionar as bases:
			if(%jogo.jogadorDaVez.grupoInicial $= ""){
				serverCmdEscolherGrupo(%client, 1, true);
				if(%player.grupoInicial !$= "Europa"){
					%jogo.forcePosicionarBaseTerrestre();
				}
				%jogo.forcePosicionarBaseMaritima();
			} else {
				//se ele já escolheu carta de grupo e já posicionou uma base, posicionar uma base marítima:
				if(%player.basesIniciais > 0){
					%jogo.forcePosicionarBaseMaritima();
				} else {
					//se ele já escolheu carta de grupo, mas não posicionou nenhuma base, posicionar duas bases:
					if(%player.grupoInicial !$= "Europa"){
						%jogo.forcePosicionarBaseTerrestre();
					}
					%jogo.forcePosicionarBaseMaritima();
				}
			}
			//%jogo.passarAVezEscolhendoGrupos(); //o client deve passar a vez, ou o server passa a vez em 25 segundos (caso o client tenha caído);
		} else {
			//escolhe uma cor:
			if(%jogo.player1.myColor !$= "vermelho" && %jogo.player2.myColor !$= "vermelho" && %jogo.player3.myColor !$= "vermelho" && %jogo.player4.myColor !$= "vermelho" && %jogo.player5.myColor !$= "vermelho"){
				serverCmdEscolherCor(%client, "vermelho");
			} else if(%jogo.player1.myColor !$= "laranja" && %jogo.player2.myColor !$= "laranja" && %jogo.player3.myColor !$= "laranja" && %jogo.player4.myColor !$= "laranja" && %jogo.player5.myColor !$= "laranja"){
				serverCmdEscolherCor(%client, "laranja");
			} else if(%jogo.player1.myColor !$= "amarelo" && %jogo.player2.myColor !$= "amarelo" && %jogo.player3.myColor !$= "amarelo" && %jogo.player4.myColor !$= "amarelo" && %jogo.player5.myColor !$= "amarelo"){
				serverCmdEscolherCor(%client, "amarelo");
			} else if(%jogo.player1.myColor !$= "verde" && %jogo.player2.myColor !$= "verde" && %jogo.player3.myColor !$= "verde" && %jogo.player4.myColor !$= "verde" && %jogo.player5.myColor !$= "verde"){
				serverCmdEscolherCor(%client, "verde");
			} else if(%jogo.player1.myColor !$= "azul" && %jogo.player2.myColor !$= "azul" && %jogo.player3.myColor !$= "azul" && %jogo.player4.myColor !$= "azul" && %jogo.player5.myColor !$= "azul"){
				serverCmdEscolherCor(%client, "azul");
			} else {
				serverCmdEscolherCor(%client, "indigo");	
			}
		}
	}
}

function jogo::forcePosicionarBaseTerrestre(%this){
	%jogadorDaVez = %this.jogadorDaVez;
	echo("Nome do grupo inicial = " @ %jogadorDaVez.grupoInicial);
	%grupo = %this.findGrupoPorNome(%jogadorDaVez.grupoInicial);
	echo("GrupoInicial = " @ %grupo);
	//pegar a primeira área terrestre:
	for(%i = 0; %i < %grupo.simAreas.getCount(); %i++){
		%tempArea = %grupo.simAreas.getObject(%i);
		if(%tempArea.terreno $= "terra"){
			%areaTerrestre1 = %tempArea;
			%i = %grupo.simAreas.getCount(); //sai do loop;
			echo("Área Terrestre encontrada = " @ %areaTerrestre1.myName);
		}
	}
	
	serverCmdCriarBase(%jogadorDaVez.persona.client, %areaTerrestre1.myName);
}



//ATENÇÃO: A primeira área registrada em um grupo sempre precisa ter alguma fronteira marítima!!! [grupo.simAres.getObject(0)]
function jogo::forcePosicionarBaseMaritima(%this){
	%jogadorDaVez = %this.jogadorDaVez;
	
	%grupo = %this.findGrupoPorNome(%jogadorDaVez.grupoInicial);
	%areaTerrestre = %grupo.simAreas.getObject(0);
	
	commandToClient(%jogadorDaVez.client, 'serverGetFronteiraMaritima', %areaTerrestre.myName);
	$forceMaritimaSchedule = schedule(25000, 0, "serverCmdPassarAVezEscolhendoGrupos", %jogadorDaVez.client);
}



function jogo::findGrupoPorNome(%this, %nomeDoGrupo){
	for(%i = 0; %i < %this.gruposNoJogo.getCount(); %i++){
		%tempGrupo = %this.gruposNoJogo.getObject(%i);
		if(%tempGrupo.nome $= %nomeDoGrupo){
			%grupoEncontrado = %tempGrupo;
			%i = %this.gruposNoJogo.getCount(); //sai do loop;
			echo("GRUPO ENCONTRADO = " @ %grupoEncontrado);
		}
	}
	
	return %grupoEncontrado;
}



function jogo::anularEsteJogo(%this){
	%this.terminado = true; //marca que o jogo acabou de acabar!
	//primeiro double-check Status atual de todos os jogadores:
		
	%this.forceTreino = true;
			
	/////////////////////////////////////
	//cria a string que será enviada para cada client:
	%dadosDoJogo = %jogo.playersAtivos; // word 0
	%dadosDoJogo = %dadosDoJogo SPC "Ninguém"; // word 1
	%dadosDoJogo = %dadosDoJogo SPC "0"; // word 2
	%dadosDoJogo = %dadosDoJogo SPC "0"; // word 3
	%dadosDoJogo = %dadosDoJogo SPC "0"; // word 4
	
	%dadosP1 = -1;
	%dadosP2 = -1;
	%dadosP3 = -1;
	%dadosP4 = -1;
	%dadosP5 = -1;
	%dadosP6 = -1;
		
	//////////////////////////////////////
	for(%i = 0; %i < %this.playersAtivos; %i++){
		commandToClient(%this.simPlayers.getObject(%i).client, 'fimDeJogo', %dadosDoJogo, 0);
	}
	if(%this.observadorOn){
		commandToClient(%this.observador, 'fimDeJogo', %dadosDoJogo, 0);
	}
	echo("JOGO INVÁLIDO(" @ %this.num @ ") FINALIZADO, DADOS = " @ %dadosDoJogo);
	%this.partidaEncerrada = true;
		
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%persona = %this.simPlayers.getObject(%i).persona;
		%persona.inGame = false;
	}
	
	//libera a sala no átrio:
	%this.sala.ocupada = false;
	serverAtualizarAtrioParaTodos();
}

/////////////////////
//GoodToGo:

function serverCmdFinalizeiSorteioDeOrdemShow(%client)
{
	%jogo = %client.player.jogo;
	%jogo.addSorteioshowFinalizado(%client.player);
}

function jogo::addSorteioshowFinalizado(%this, %player)
{
	if(!isObject(%this.simSorteioShowFinalizado))
		%this.simSorteioShowFinalizado = new SimSet();
	
	if(!%this.simSorteioShowFinalizado.isMember(%player))
		%this.simSorteioShowFinalizado.add(%player);
	
	if(%this.getGoodToGo())
	{
		%this.CTCgoodToGo();
		%this.goodToGo = true;
	}
}

function jogo::getGoodToGo(%this)
{
	if(isObject(%this.observador))
		%observadorNum = 1;
		
	%totalDeClients = %this.playersAtivos + %observadorNum;
	if(%totalDeClients == %this.simSorteioShowFinalizado.getCount())
		return true;
		
	return false;
}

function jogo::CTCgoodToGo(%this)
{
	for(%i = 0; %i < %this.playersAtivos; %i++)
		commandToClient(%this.simPlayers.getObject(%i).client, 'goodToGo');
	
	if(%this.observadorOn)
		commandToClient(%this.observador, 'goodToGo');
}