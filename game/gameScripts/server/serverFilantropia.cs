// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverFilantropia.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sábado, 29 de março de 2008 22:40
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdFilantropia(%client, %praQuemId, %anonima){
	echo("serverCmdFilantropia:: %praQuemId = " @ %praQuemId @ "; %anonima = " @ %anonima);
	%player = %client.player;
	%jogo = %player.jogo;
	%eval = "%receptor = %jogo." @ %praQuemId @ ";";
	eval(%eval);
	if(%player.filantropiasEfetuadas < %player.persona.aca_i_2){
		%jogo.efetuarDoacaoFilantropica(%player, %receptor, %anonima);
	}
}

function jogo::efetuarDoacaoFilantropica(%this, %doador, %receptor, %anonima){
	%doador.filantropiasEfetuadas++;
	%doador.minerios--;
	%doador.petroleos--;
	%doador.uranios--;
	
	%receptor.minerios++;
	%receptor.petroleos++;
	%receptor.uranios++;
	
	commandToClient(%doador.client, 'filantropia', "doar", %receptor.persona.nome, %anonima);
	commandToClient(%receptor.client, 'filantropia', "receber", %doador.persona.nome, %anonima);
}