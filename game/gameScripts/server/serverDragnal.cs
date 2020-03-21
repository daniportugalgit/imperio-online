// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverDragnal.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 14 de dezembro de 2008 20:14
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdDragnalAtacar(%client, %areaNome, %irmaos, %fronteirasAtingidas, %fronteirasNomes){
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	
	if(%persona.aca_av_1 >= 2){
		%myMax = 2;
	} else {
		%myMax = 1;	
	}
	
	if(%persona.aca_i_3 > 0){
		if(%persona.player.dragnalAtks < %myMax){
			if(%jogo.primeiraRodada == false){
				%jogo.dragnalAtk(%client.player, %areaNome, %irmaos, %fronteirasAtingidas, %fronteirasNomes); 	
			}
		}
	}
}

function jogo::dragnalAtk(%this, %autor, %onde, %irmaos, %fronteirasAtingidas, %fronteirasNomes){
	%persona = %autor.persona;
	%eval = "%areaNoJogo = %this." @ %onde @ ";";
	eval(%eval);
	
	if(%persona.aca_av_1 == 3){
		%custo = 1;	
	} else {
		%custo = 2;
	}
	
	%autor.imperiais -= %custo;
	%autor.atacou = 1;
	
	%ataques = %persona.aca_i_3 + (%persona.aca_ldr_3_h3 * 2);
	
	for(%i = 0; %i < %ataques; %i++){
		if(isObject(%areaNoJogo.pos1Quem)){
			%areaNoJogo.pos1Quem.kill(%autor);	
		} else if(isObject(%areaNoJogo.pos2Quem)){
			%areaNoJogo.pos2Quem.kill(%autor);	
		} else if(isObject(%areaNoJogo.pos0Quem && %areaNoJogo.pos0Quem.class $= "rainha")){
			if(!%areaNoJogo.pos0Quem.grandeMatriarca){
				%areaNoJogo.pos0Quem.kill(%autor);	
			}
		} else {
			%ataques = %i;	
		}
	}
	
	if(%irmaos > 0){
		for(%i = 1; %i < %fronteirasAtingidas; %i++){
			%myWord = getWord(%fronteirasNomes, %i);
			%eval = "%altAreaAlvo = %this." @ %myWord @ ";";
			eval(%eval);
			if(isObject(%altAreaAlvo.pos1Quem)){
				%altAreaAlvo.pos1Quem.kill(%autor);	
			} else if(isObject(%altAreaAlvo.pos2Quem)){
				%altAreaAlvo.pos2Quem.kill(%autor);	
			} else if(isObject(%altAreaAlvo.pos0Quem && %altAreaAlvo.pos0Quem.class $= "rainha")){
				if(!%altAreaAlvo.pos0Quem.grandeMatriarca){
					%altAreaAlvo.pos0Quem.kill(%autor);	
				}
			}
		}	
	}
	
	
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%client = %this.simPlayers.getObject(%i).client;
		commandToClient(%client, 'DragnalAtacar', %onde, %ataques, %irmaos, %fronteirasAtingidas, %fronteirasNomes);
	}
	if(%this.observadorOn){
		commandToClient(%this.observador, 'DragnalAtacar', %onde, %ataques, %irmaos, %fronteirasAtingidas, %fronteirasNomes);
	}
	
}

function serverCmdDragnalEntregar(%client, %onde){
	echo("DRAGNAL ENTREGAR EM: " @ %onde);
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	%player = %client.player;
	
	%custo = 2;
	if(%persona.aca_av_1 > 2){
		%custo = 1;	
	}
	%myMax = 1;
	if(%persona.aca_av_1 > 0){
		%myMax = 2;	
	}
	%ovos = %persona.aca_ldr_3_h1;
	
	if(%player == %jogo.jogadorDaVez){
		if(%player.dragnalEntregas < %myMax && %player.imperiais >= %custo){
			%player.dragnalEntregas++;
			%player.imperiais -= %custo;
			%eval = "%areaNoJogo = %jogo." @ %onde @ ";";
			eval(%eval);
			
			for(%i = 0; %i < %ovos; %i++){
				%newUnit = ovo_BP.createCopy();
				%newUnit.isSelectable = 1;
				%newUnit.isMoveable = 0;
				%newUnit.dono = %player;
				%newUnit.gulok = true;
				%player.mySimUnits.add(%newUnit); //adiciona a unit no simset de unidades do player;
				%player.mySimOvos.add(%newUnit);	
				%areaNoJogo.positionUnit(%newUnit);	
			}
			
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				%jogador = %jogo.simPlayers.getObject(%i);
				commandToClient(%jogador.client, 'dragnalEntregar', %onde, %ovos);
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'dragnalEntregar', %onde, %ovos);
			}
		}
	}
}

function serverCmdDragnalSoprar(%client, %onde){
	echo("DRAGNAL SOPRAR EM: " @ %onde);
	%jogo = %client.player.jogo;
	%persona = %client.persona;
	%player = %client.player;
	
	%custo = 2;
	if(%persona.aca_av_1 > 2){
		%custo = 1;	
	}
	%myMax = 1;
	if(%persona.aca_av_1 > 0){
		%myMax = 2;	
	}
	%ovos = %persona.aca_ldr_3_h2;
	
	if(%player == %jogo.jogadorDaVez){
		if(%player.dragnalSopradas < %myMax && %player.imperiais >= %custo){
			%player.dragnalSopradas++;
			%player.imperiais -= %custo;
			%eval = "%areaNoJogo = %jogo." @ %onde @ ";";
			eval(%eval);
			
			if(%ovos > %areaNoJogo.myPos4List.getCount()){
				%ovos = %areaNoJogo.myPos4List.getCount();	
			}
			
			for(%i = 0; %i < %ovos; %i++){
				%ovo = %areaNoJogo.myPos4List.getObject(0);
				%ovo.eclodir();
			}
			
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				%jogador = %jogo.simPlayers.getObject(%i);
				commandToClient(%jogador.client, 'dragnalSoprar', %onde, %ovos);
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'dragnalSoprar', %onde, %ovos);
			}
		}
	}
}