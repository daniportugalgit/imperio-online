// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverIntel.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quinta-feira, 27 de março de 2008 0:13
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdi_prospeccao(%client){
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	if(%persona.player.prospeccao > 0){
		%persona.player.prospeccao--;
		%sorteioDeInfo = %jogo.prospeccao(%persona.player);
		commandToClient(%client, 'setNewInfo', %sorteioDeInfo);
	}
}

function jogo::prospeccao(%this, %player){
	if(!%this.partidaEncerrada){
		if (%this.infoPool.getCount() > 0){
			%maxInfoIndice = %this.infoPool.getCount();
			
			%sorteio = dado(%maxInfoIndice, -1);
			if (!isObject(%this.infoPool.getObject(%sorteio))){
				%sorteio = 0;	
			}
			
			%infoSorteada = %this.infoPool.getObject(%sorteio);
			%player.mySimInfo.add(%infoSorteada);
			%this.infoPool.remove(%infoSorteada);
			%eval = "%this.info" @ %infoSorteada.num @ " = new ScriptObject(){};";
			eval(%eval);
			%eval = "%infoScriptNojogo = %this.info" @ %infoSorteada.num @ ";";
			eval(%eval);
			%infoScriptNoJogo.dono = %player;
			%infoScriptNoJogo.compartilhada = false;
			%player.imperiais -= 2;
			return %infoSorteada.num;
		} else {
			echo("Não há mais informações");
		}
	}
}