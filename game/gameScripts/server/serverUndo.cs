// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverUndo.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 15 de fevereiro de 2008 19:25
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function serverCriarUndo(%jogo, %unit, %areaDeOrigem, %especial, %embarque){
	%eval = "%player = %jogo." @ %unit.dono.id @ ";";
	eval(%eval);
	%playerUndo = %player.clientUNDO;
			
	if(!isObject(%playerUndo)){
		%eval = "%jogo." @ %unit.dono.id @ ".clientUNDO = new ScriptObject(){};";
		eval(%eval);
		%eval = "%playerUndo = %jogo." @ %unit.dono.id @ ".clientUNDO;";
		eval(%eval);
	} else {
		serverClearUndo(%unit.dono.id);
	}
	
	if(!%player.blockundo){
		%playerUndo.unit = %unit;
		%playerUndo.areaDeOrigem = %areaDeOrigem;
		%playerUndo.especial = %especial; //true or false;
		%playerUndo.embarque = %embarque; //"embarque" ou "desembarque" ou nada;
	} else {
		//echo("unDo Blocked! unBlocking unDo.");
		%player.blockundo = false;
	}
}

function serverClearUndo(%player){
	//echo("serverClearUndo();");
	%player.clientUNDO.unit = "no";
	%player.clientUNDO.areaDeOrigem = "no";
	%player.clientUNDO.especial = "no";
	%player.clientUNDO.embarque = "no";
}

function serverBlockUndo(%player){
	%player.blockundo = true;
}

function serverCmdUndo(%client){
	%player = %client.player;
	%jogo = %client.player.jogo;
		
	%unit = %player.clientUNDO.unit;
	%areaDeOrigem = %player.clientUNDO.areaDeOrigem;
	%especial = %player.clientUNDO.especial;
	%embarque = %player.clientUNDO.embarque;
		
	if(isObject(%unit) && isObject(%areaDeOrigem)){
		if(%embarque $= "embarque"){
			//unit.onde == %navio
			if(%navio.dono == %unit.dono){
				%corDeQuem = "minhaCor";	
			} else {
				%corDeQuem = "outro";	
			}
			serverCmdDesembarcar(%unit.dono.client, %unit.onde.onde.myName, %unit.onde.pos, %areaDeOrigem.myName, %unit.class, %corDeQuem, true);
			%areaDeOrigem.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
			
		} else if(%embarque $= "desembarque"){
			//serverCmdEmbarcar(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo, %posDoNavio, %undo)
			serverCmdEmbarcar(%unit.dono.client, %unit.onde.myName, %unit.pos, %areaDeOrigem.onde.myName, %areaDeOrigem.pos, true); //areaDeOrigem == próprio navio
		} else {
			serverRemoverUnidade(%jogo, %unit, %unit.onde);
			%areaDeOrigem.positionUnit(%unit);
			%areaDeOrigem.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
			
			if(%especial == false){
				%jogo.jogadorDaVez.movimentos += 1;
			} else {
				%unit.JPagora++;	
			}
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'doUndo'); 
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'doUndo'); 
			}
		}
		
		serverClearUndo(%player); //apenas um Undo fica gravado;
		serverVerificarGruposX(%player); //verifica se o player ainda mantém os grupos
		serverVerificarObjetivos(%player); //verifica se o player ainda tem os objetivos
		
	} else {
	 	//echo("UNDO IMPOSSÍVEL: Não há ação para ser desfeita!");	
	} 
}