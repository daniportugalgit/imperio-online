// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverPropostas.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 28 de janeiro de 2008 20:07
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdEnviarPropostaDeTroca(%client, %infoNum, %receptorId, %b_doar){
	%jogo = %client.player.jogo;
	if(!%jogo.terminado){
		%eval = "%doadorNoJogo = %jogo." @ %client.player.id @ ";"; //quem está doando
		eval(%eval);
		%eval = "%receptorNoJogo = %jogo." @ %receptorId @ ";"; //quem está recebendo a carta
		eval(%eval);
			
		if(%receptorNoJogo == %doadorNoJogo.myDupla || %b_doar == true){
			serverCmdDoarMissao(%client, %infoNum, %client.player.id, %receptorId);
		} else {
			//agora enviar o comando para os clients interessados:
			commandToClient(%doadorNoJogo.client, 'enviarProposta', %infoNum, %receptorNoJogo.id);
			commandToClient(%receptorNoJogo.client, 'receberProposta', %infoNum, %doadorNoJogo.id);
		}
	}
}

function serverCmdEnviarRespostaDeProposta(%client, %numDaInfoAntiga, %infoNum, %parceiroId){
	%jogo = %client.player.jogo;
	if(!%jogo.terminado){
		%eval = "%respondente = %jogo." @ %client.player.id @ ";";
		eval(%eval);
		%eval = "%perguntante = %jogo." @ %parceiroId @ ";";
		eval(%eval);
		
		commandToClient(%respondente.client, 'respostaEnviada', %numDaInfoAntiga, %infoNum, %parceiroId);
		commandToClient(%perguntante.client, 'respostaRecebida', %numDaInfoAntiga, %infoNum, %client.player.id);
	}
}


function serverCmdEfetuarTroca(%client, %numDaInfo1, %numDaInfo2, %parceiroId){
	%jogo = %client.player.jogo; //pega o jogo
	
	if(!%jogo.terminado){
		//pega os donos de cada info:
		%donoDaInfo1 = %client.player;
		%eval = "%donoDaInfo2 = %jogo." @ %parceiroId @ ";";
		eval(%eval);
		
		//pega as $infos:
		%info1 = %jogo.findInfo(%numDaInfo1);
		%info2 = %jogo.findInfo(%numDaInfo2);
			
		//pega as infosNoJogo:
		%eval = "%info1NoJogo = %jogo.info" @ %numDaInfo1 @ ";";
		eval(%eval);
		%eval = "%info2NoJogo = %jogo.info" @ %numDaInfo2 @ ";";
		eval(%eval);
		
		//troca as missões:
		%donoDaInfo1.mySimInfo.remove(%info1); 
		%donoDaInfo2.mySimInfo.add(%info1);
		%donoDaInfo2.mySimInfo.remove(%info2); 
		%donoDaInfo1.mySimInfo.add(%info2); 
		%info1NoJogo.dono = %donoDaInfo2; //atualiza no server que é o novo dono da carta
		%info2NoJogo.dono = %donoDaInfo1; //atualiza no server que é o novo dono da carta
		%donoDaInfo1.negInGame++;
		%donoDaInfo2.negInGame++;
		echo("%donoDaInfo1.negInGame = " @ %donoDaInfo1.negInGame);
		echo("%donoDaInfo2.negInGame = " @ %donoDaInfo2.negInGame);
		
		commandToClient(%donoDaInfo1.client, 'efetuarTroca', %numDaInfo1, %numDaInfo2, %donoDaInfo2.id);
		commandToClient(%donoDaInfo2.client, 'efetuarTroca', %numDaInfo1, %numDaInfo2, %donoDaInfo1.id);
	}
}


function serverCmdNegarTroca(%client, %numDaInfo1, %numDaInfo2, %parceiroId){
	%jogo = %client.player.jogo; //pega o jogo
	if(!%jogo.terminado){
		//pega os donos de cada info:
		%donoDaInfo1 = %client.player;
		%eval = "%donoDaInfo2 = %jogo." @ %parceiroId @ ";";
		eval(%eval);
				
		commandToClient(%donoDaInfo1.client, 'negarTroca', %numDaInfo1, %numDaInfo2, %donoDaInfo2.id);
		commandToClient(%donoDaInfo2.client, 'negarTroca', %numDaInfo1, %numDaInfo2, %donoDaInfo1.id);
	}
}

function serverCmdCancelarTroca(%client, %numDaInfo1, %numDaInfo2, %parceiroId){
	%jogo = %client.player.jogo; //pega o jogo
	if(!%jogo.terminado){
		//pega os donos de cada info:
		%donoDaInfo1 = %client.player;
		%eval = "%donoDaInfo2 = %jogo." @ %parceiroId @ ";";
		eval(%eval);
				
		commandToClient(%donoDaInfo1.client, 'cancelarTroca', %numDaInfo1, %numDaInfo2);
		commandToClient(%donoDaInfo2.client, 'cancelarTroca', %numDaInfo1, %numDaInfo2);
	}
}


















