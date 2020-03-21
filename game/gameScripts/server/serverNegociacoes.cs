// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverNegociacoes.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  domingo, 3 de fevereiro de 2008 22:08
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

///////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////
// NEGOCIAÇÕES:
///////////////
function localizarUnidadePedinte(%jogo, %areaDeOrigem, %posDeOrigem){
	if(%posDeOrigem $= "pos1" || %posDeOrigem $= "pos2" || %posDeOrigem $= "pos0"){
		%eval = "%unidadePedinte = %jogo." @ %areaDeOrigem @ "." @ %posDeOrigem @ "Quem;";
		eval(%eval);
	} else { //se não for da pos1 ou 2, verifica as unidades até achar uma que tenha o client como dono
		if(%posDeOrigem $= "pos3"){
			%eval = "%pos3Count = %jogo." @ %areaDeOrigem @ ".myPos3List.getCount();";
			eval(%eval);
			for(%i = 0; %i < %pos3Count; %i++){
				%eval = "%tempUnit = %jogo." @ %areaDeOrigem @ ".myPos3List.getObject(%i);";
				eval(%eval);
				
				if(%tempUnit.dono == %jogo.jogadorDaVez){
					%unidadePedinte = %tempUnit;
					%i = %pos3Count;
				}
			}	
		} else if(%posDeOrigem $= "pos4"){
			%eval = "%pos4Count = %jogo." @ %areaDeOrigem @ ".myPos4List.getCount();";
			eval(%eval);
			for(%i = 0; %i < %pos4Count; %i++){
				%eval = "%tempUnit = %jogo." @ %areaDeOrigem @ ".myPos4List.getObject(%i);";
				eval(%eval);
				
				if(%tempUnit.dono == %jogo.jogadorDaVez){
					%unidadePedinte = %tempUnit;
					%i = %pos4Count;
				}
			}	
		}
	}
	return %unidadePedinte;
}

function serverCmdAskPermissaoParaVisitar(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo){
	%jogo = %client.player.jogo;
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	%unidadePedinte = localizarUnidadePedinte(%jogo, %areaDeOrigem, %posDeOrigem);
		
	//função clientCmdPushPerguntaVisita(%quemPede, %areaDeOrigem, %posDeOrigem, %areaAlvo);
	if(%areaAlvoNoJogo.dono $= "COMPARTILHADA"){
		commandToClient(%areaAlvoNoJogo.dono1.client, 'pushPerguntaVisita', %unidadePedinte.dono.id, %areaDeOrigem, %posDeOrigem, %areaAlvo);
	} else {
		commandToClient(%areaAlvoNoJogo.dono.client, 'pushPerguntaVisita', %unidadePedinte.dono.id, %areaDeOrigem, %posDeOrigem, %areaAlvo);
	}
}

function serverCmdVisitarArea(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo){
	%jogo = %client.player.jogo;
	%unidadePedinte = localizarUnidadePedinte(%jogo, %areaDeOrigem, %posDeOrigem);
		
	commandToClient(%client, 'msgGuiVisitaAutorizada'); //atualiza o msgGui de quem deu passagem
	commandToClient(%unidadePedinte.dono.client, 'msgGuiVisitaAutorizada'); //atualiza o msgGui de quem pediu passagem
	serverCmdMovimentar(%unidadePedinte.dono.client, %areaDeOrigem, %posDeOrigem, %areaAlvo);	
}

function serverCmdVisitaNegada(%client, %areaDeOrigem, %posDeOrigem){
	%jogo = %client.player.jogo;
	%unidadePedinte = localizarUnidadePedinte(%jogo, %areaDeOrigem, %posDeOrigem);
			
	commandToClient(%unidadePedinte.dono.client, 'receberPedidoNegado');
	commandToClient(%client, 'apagarMsgGui');
}

function serverCmdCancelarPergunta(%client, %areaAlvo){
	echo("CANCELANDO PERGUNTA- Área-Alvo = " @ %areaAlvo);
	%jogo = %client.player.jogo;
	
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	
	if(%areaAlvoNoJogo.dono $= "COMPARTILHADA"){
		%quemNaoRespondeu = %areaAlvoNoJogo.dono1;
	} else {
		%quemNaoRespondeu = %areaAlvoNoJogo.dono;
	}
		
	commandToClient(%quemNaoRespondeu.client, 'apagarMsgGui');
	commandToClient(%client, 'naoHouveResposta');
}







//Em negociação:
function serverCmdAskPermissaoParaEmbarcar(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo, %navioPos){
	%jogo = %client.player.jogo;
	%unidadePedinte = localizarUnidadePedinte(%jogo, %areaDeOrigem, %posDeOrigem);
	%navio = localizarUnidadePedinte(%jogo, %areaAlvo, %navioPos);
		
	//função clientCmdPushPerguntaVisita(%quemPede.id, %areaDeOrigem, %posDeOrigem, %areaAlvo);
	commandToClient(%navio.dono.client, 'pushPerguntaEmbarque', %unidadePedinte.dono.id, %areaDeOrigem, %posDeOrigem, %areaAlvo, %navioPos);
}

function serverCmdAskPermissaoParaDesembarcar(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo){
	%jogo = %client.player.jogo;
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);	
	%unidadePedinte = localizarUnidadePedinte(%jogo, %areaDeOrigem, %posDeOrigem);
	
	//função clientCmdPushPerguntaVisita(%quemPede.id, %areaDeOrigem, %posDeOrigem, %areaAlvo);
	if(%areaAlvoNoJogo.dono $= "COMPARTILHADA"){
		commandToClient(%areaAlvoNoJogo.dono1.client, 'pushPerguntaDesembarque', %unidadePedinte.dono.id, %areaDeOrigem, %posDeOrigem, %areaAlvo);
	} else {
		commandToClient(%areaAlvoNoJogo.dono.client, 'pushPerguntaDesembarque', %unidadePedinte.dono.id, %areaDeOrigem, %posDeOrigem, %areaAlvo);
	}
}

////////////////
function serverCmdEmbarcarVisitando(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo, %navioPos){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	%unidadePedinte = localizarUnidadePedinte(%jogo, %areaDeOrigem, %posDeOrigem);
	%navio = localizarUnidadePedinte(%jogo, %areaAlvo, %navioPos);
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
		
	if(%jogadorDaVez.client == %unidadePedinte.dono.client){ //SEGURANÇA
		commandToClient(%client, 'msgGuiEmbarqueAutorizado'); //atualiza o msgGui de quem deu passagem
		commandToClient(%unidadePedinte.dono.client, 'msgGuiEmbarqueAutorizado'); //atualiza o msgGui de quem pediu passagem
				
		if (!%navio.transporteFlag){
			%navio.myTransporte = new SimSet();
			%navio.transporteFlag = true;
		}	
			
		if(%navio.myTransporte.getCount() < 2){ //if(%navio.myTransporte.getCount() < %navio.dono.persona.TAXOnavioCarga){
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'embarcarVisitando', %areaDeOrigem, %posDeOrigem, %areaAlvo, %navioPos); 
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'embarcarVisitando', %areaDeOrigem, %posDeOrigem, %areaAlvo, %navioPos); 
			}
			%navio.myTransporte.add(%unidadePedinte);
			serverRemoverUnidade(%jogo, %unidadePedinte, %unidadePedinte.onde);
			%unidadePedinte.onde = "navio";
		} else {
			commandToClient(%client, 'clientMsgNaoHaEspaco'); //retornar para o client: ("Não há espaço neste navio. Embarque NÃO efetuado.");
		}
		serverClearUndo(%jogadorDaVez);
	} 
}

//
function serverCmdDesembarcarVisitando(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	%unidadePedinte = localizarUnidadePedinte(%jogo, %areaDeOrigem, %posDeOrigem);
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	
	%navio = %unidadePedinte;
	
	if(%jogadorDaVez.movimentos > 0 && %jogadorDaVez.client == %unidadePedinte.dono.client){ //SEGURANÇA
		commandToClient(%client, 'msgGuiDesembarqueAutorizado'); //atualiza o msgGui de quem deu passagem
		commandToClient(%navio.dono.client, 'msgGuiDesembarqueAutorizado'); //atualiza o msgGui de quem pediu passagem
	
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			commandToClient(%jogo.simPlayers.getObject(%i).client, 'desembarcarVisitando', %areaDeOrigem, %posDeOrigem, %areaAlvo); 
		}
		if(%jogo.observadorOn){
			commandToClient(%jogo.observador, 'desembarcarVisitando', %areaDeOrigem, %posDeOrigem, %areaAlvo); 
		}

		%jogo.desembarcando = true;
			
			%soldado = %navio.myTransporte.getObject(0);
			if(%areaAlvoNoJogo.desprotegida == true){ //cancela a visita e domina a base
				%eval = "%jogadorInvadido = %jogo." @ %areaAlvoNoJogo.dono.id @ ";"; //garante q o sistema reconheça o jogador invadido
				eval(%eval);
				%jogadorInvadido.mySimAreas.remove(%areaAlvoNoJogo); //remove a Área-alvo do jogador invadido
				%areaAlvoNoJogo.pos0Quem.dono = %jogadorDaVez; //marca na Base capturada seu novo dono
				if(%jogadorInvadido.mySimAreas.getCount() < 1){
					serverPlayerKill(%jogadorInvadido, %jogadorDaVez);
				}
				serverDesembarcar(%soldado, %navio, %areaAlvo);
			} else {
				serverDesembarcarVisitando(%jogo, %soldado, %navio, %areaAlvoNoJogo);
			}		
			%navio.myTransporte.remove(%soldado);
			
		%jogo.desembarcando = false;
	}
}


function serverDesembarcarVisitando(%jogo, %unit, %navio, %areaAlvo){
	%jogadorDaVez = %jogo.jogadorDaVez;
	%jogadorVisitado = %areaAlvo.dono;
		
	%areaAlvo.positionUnit(%unit);
	if (%areaAlvo.myName !$= "oceanoIndico" && %areaAlvo.myName !$= "oceanoAtlanticoSul" && %areaAlvo.myName !$= "oceanoArtico" && %areaAlvo.myName !$= "oceanoAtlanticoNorte" && %areaAlvo.myName !$= "oceanoPacificoOriental" && %areaAlvo.myName !$= "oceanoPacificoOcidental"){
		%jogadorVisitado.mySimAreas.remove(%areaAlvo);
	}

	%areaAlvo.dono = "MISTA";
	serverVerificarGruposX(%jogadorDaVez);
	serverVerificarGruposX(%jogadorVisitado);
	%jogadorDaVez.movimentos -= 1;
	serverVerificarObjetivos(%jogadorDaVez);
	serverVerificarObjetivos(%jogadorVisitado);
}




function serverCmdDoarMissao(%client, %infoNum, %doador, %receptor){
	%jogo = %client.player.jogo;
	
	%eval = "%doador = %jogo." @ %doador @ ";"; //quem está doando
	eval(%eval);
	%eval = "%receptor = %jogo." @ %receptor @ ";"; //quem está recebendo a carta
	eval(%eval);
	
	%info = %jogo.findInfo(%infoNum); //estabelece qual missão está sendo doada (conforme o planeta, a info é diferente) <<objeto global, BluePrint>>
	%eval = "%infoNoJogo = %jogo.info" @ %infoNum @ ";"; //estabelece qual missão está sendo doada
	eval(%eval);
		
	//organizar no server:
	%doador.mySimInfo.remove(%info);
	%receptor.mySimInfo.add(%info);
	%infoNoJogo.dono = %receptor;//atualiza no server que é o novo dono da carta
	echo("Missão doada do" SPC %doador.id SPC "para o" SPC %receptor.id);
	
	//agora enviar o comando para os clients interessados:
	commandToClient(%doador.client, 'doarMissao', %infoNum);
	commandToClient(%receptor.client, 'receberInfoDoada', %infoNum, %doador.persona.nome);
}




function serverCmdEnviarAcordoExpl(%client, %infoNum, %doador, %receptor){
	%jogo = %client.player.jogo;
	
	%eval = "%doadorNoJogo = %jogo." @ %doador @ ";"; //quem está doando
	eval(%eval);
	%eval = "%receptorNoJogo = %jogo." @ %receptor @ ";"; //quem está recebendo a carta
	eval(%eval);
	
	%info = %jogo.findInfo(%infoNum); //estabelece qual missão está sendo negociada (conforme o planeta, a info é diferente) <<objeto global, BluePrint>>
	%eval = "%infoNoJogo = %jogo.info" @ %infoNum @ ";"; //estabelece qual missão está sendo negociada <<objeto do jogo, editável>>
	eval(%eval);
	
	if(%receptorNoJogo == %doadorNoJogo.myDupla){
		//se o receptor é meu companheiro de dupla eu faço uma doação, não um acordo expl;
		serverCmdDoarMissao(%client, %infoNum, %doador, %receptor);
	} else {
		//organizar no server:
		%doadorNoJogo.mySimInfo.remove(%info); //retira a missão de quem deu
		%receptorNoJogo.mySimInfo.add(%info); //dá a missão a quem recebeu
		%infoNoJogo.dono = %receptorNoJogo; //atualiza no server que é o novo dono da carta
		
		%doadorNoJogo.mySimExpl.add(%info); //guarda no simset de negociacoes de quem doou
		%receptorNoJogo.mySimExpl.add(%info); //guarda no simset de negociacoes de quem recebeu
		%doadorNoJogo.negInGame++;
		%receptorNoJogo.negInGame++;
		
		%infoNoJogo.compartilhada = true; //o server não precisa criar botões, precisa saber apenas que a missão é compartilhada, quais são as partes e quem ganha agora:
		%infoNoJogo.quemDeu = %doadorNoJogo;
		%infoNoJogo.quemExplora = %receptorNoJogo;
		%infoNoJogo.vezDeQuemDeu = true; //de ganhar o recursos, pois quem descobriu a missão começa ganhando.
		//echo("Acordo de exploração entre" SPC %doadorNoJogo.id SPC "(doador) e" SPC %receptorNoJogo.id SPC "(receptor)");
		
		//agora enviar o comando para os clients interessados:
		commandToClient(%doadorNoJogo.client, 'acordoExpl', "doar", %infoNum, %receptorNoJogo.id);
		commandToClient(%receptorNoJogo.client, 'acordoExpl', "receber", %infoNum, %doadorNoJogo.id);
	}
}

function serverToggleVezDeGanhar(%missao, %jogo){
	%infoNum = %missao.num; //para passar pro client corretamente;
	%eval = "%missaoNoJogo = %jogo.info" @ %infoNum @ ";";
	eval(%eval);
	
	if(%missaoNoJogo.vezDeQuemDeu == true){
		%missaoNoJogo.vezDeQuemDeu = false;
	} else {
		%missaoNoJogo.vezDeQuemDeu = true;
	}
	
	commandToClient(%missaoNoJogo.quemDeu.client, 'toggleVezDeGanhar', %infoNum);
	commandToClient(%missaoNoJogo.quemExplora.client, 'toggleVezDeGanhar', %infoNum);
}

/////////////
//Moratória:
//commandToServer('embargar', %infoNum);
function serverCmdEmbargar(%client, %infoNum, %silent){
	%jogo = %client.player.jogo;
	if(!%jogo.terminado){
		%info = %jogo.findInfo(%infoNum); //estabelece qual missão está sendo embargada (conforme o planeta, a info é diferente) <<objeto global, BluePrint>>
		%eval = "%infoNoJogo = %jogo.info" @ %infoNum @ ";"; //estabelece qual missão está sendo embargada
		eval(%eval);
		
		%quemEmbarga = %infoNoJogo.quemExplora;
		%quemEhEmbargado = %infoNoJogo.QuemDeu;
		
		if(!%silent){
			%quemEmbarga.decretouMoratoria = 1;
		}
		
		%quemEmbarga.mySimExpl.remove(%info); //remove do simset de negociacoes de quem embargou
		%quemEhEmbargado.mySimExpl.remove(%info); //remove do simset de negociacoes de quem foi embargado
		%infoNoJogo.compartilhada = false;
		
		commandToClient(%quemEmbarga.client, 'moratoria', %infoNum, "Decretar", %silent);
		commandToClient(%quemEhEmbargado.client, 'moratoria', %infoNum, "Sofrer", %silent);
	} else {
		echo("executando serverCmdEmbargar, mas jogo já foi terminado. Comando anulado.");	
	}
}



/////////////////
//DoarRenda:

//commandToServer('doarRenda', %imperiais, %minerios, %petroleos, %uranios, %meuId, $atualReceptor.id);
function serverCmdDoarRenda(%client, %imperiais, %minerios, %petroleos, %uranios, %doadorId, %receptorId){
	%jogo = %client.player.jogo;
	%eval = "%doador = %jogo." @ %doadorId @ ";";
	eval(%eval);
	%eval = "%receptor = %jogo." @ %receptorId @ ";";
	eval(%eval);
	
	if(%doador.imperiais >= %imperiais && %doador.minerios >= %minerios && %doador.petroleos >= %petroleos && %doador.uranios >= %uranios){
		//doar
		%doador.imperiais -= %imperiais;
		%doador.minerios -= %minerios;
		%doador.petroleos -= %petroleos;
		%doador.uranios -= %uranios;
		
		%receptor.imperiais += %imperiais;
		%receptor.minerios += %minerios;
		%receptor.petroleos += %petroleos;
		%receptor.uranios += %uranios;
		
		commandToClient(%doador.client, 'atualizarGrana', %doador.imperiais, %doador.minerios, %doador.petroleos, %doador.uranios);
		commandToClient(%receptor.client, 'atualizarGrana', %receptor.imperiais, %receptor.minerios, %receptor.petroleos, %receptor.uranios);
		commandToClient(%doador.client, 'clientMsgDoacaoEfetuada', %receptor.persona.nome);
		commandToClient(%receptor.client, 'clientMsgDoacaoRecebida', %doador.persona.nome, %imperiais, %minerios, %petroleos, %uranios);
		%jogo.verificarObjetivosGlobal();
	} else {
		//devolver clientMsg de que não há renda;
		//só chegaria aki se o client estiver roubando, pois as setas ficam inativas quando vc não pode selecionar dterminado valor para doação.
	}
}




///////////////////////////
//Ping:
function serverCmdSendPing(%client, %idDoPingante, %paraQuemId, %onde)
{
	%jogo = serverGetJogoClient(%client);
	
	%pingante = serverGetPlayerPorId(%jogo, %idDoPingante);
	
	%pingado = serverGetPlayerPorId(%jogo, %paraQuemId);
	
	serverCTCping(%jogo, %pingante, %pingado, %onde);
}

function serverCTCping(%jogo, %pingante, %pingado, %onde)
{
	if(isObject(%pingante.client)){
		commandToClient(%pingante.client, 'ping', "pingar", %onde, %pingado.id);
	} else {
		commandToClient(%jogo.observador, 'ping', "pingar", %onde, %pingado.id);
	}
			
	commandToClient(%pingado.client, 'ping', "receberPing", %onde, %pingante.id);
}



