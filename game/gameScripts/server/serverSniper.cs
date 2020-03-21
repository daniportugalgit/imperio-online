// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverSniper.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 31 de março de 2008 14:41
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdSniper(%client, %areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo){
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	%eval = "%lider = %areaDeOrigemNoJogo." @ %posDeOrigem @ "Quem;";
	eval(%eval);
	%eval = "%unitAlvo = %areaAlvoNoJogo." @ %posAlvo @ "Quem;";
	eval(%eval);
		
	if(%lider.mySnipers > 0){
		%lider.mySnipers--;
		%lider.dono.atacou = 1;
		%lider.fire(%unitAlvo);
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			commandToClient(%jogo.simPlayers.getObject(%i).client, 'sniper', %areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo); 
		}
		if(%jogo.observadorOn){
			commandToClient(%jogo.observador, 'sniper', %areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo); 
		}
	} else {
		echo("**CHEATER?? Usuário " @ %client.user.nome @ ", Persona " @ %persona.nome @ ": Não tinha Sniper para usar, mas o client não bloqueou o pedido!");	
		commandToClient(%client, 'popServerComDot'); 
	}
}