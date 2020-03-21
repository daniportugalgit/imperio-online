// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverEmboscada.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 10 de março de 2008 13:58
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function jogo::findMyUnit(%this, %area, %pos, %client){ //o parâmetro %client é opcional para áreas NÃO-MISTAS, e obrigatório para áreas MISTAS;
	%eval = "%areaDeOrigemNoJogo = %this." @ %area @ ";";
	eval(%eval);
		
	if(%pos $= "pos2" || %pos $= "pos1" || %pos $= "pos0"){
		%eval = "%unit = " @ %areaDeOrigemNoJogo @ "." @ %pos @ "Quem;";
		eval(%eval);
	}else if(%pos $= "pos3"){
		if(%areaDeOrigemNoJogo.dono !$= "MISTA"){
			%unit = %areaDeOrigemNoJogo.myPos3List.getObject(0);
		} else {
			%pos3Count = %areaDeOrigemNoJogo.myPos3List.getCount();
			for(%i = 0; %i < %pos3Count; %i++){
				%tempUnit = %areaDeOrigemNoJogo.myPos3List.getObject(%i);
				if(%tempUnit.dono == %client.player){
					%unit = %areaDeOrigemNoJogo.myPos3List.getObject(%i);
					echo("Unidade encontrada, DONO: " @ %unit.dono);
					%i = %pos3Count;
				}
			}
		}
	}else if (%pos $= "pos4"){
		if(%areaDeOrigemNoJogo.dono !$= "MISTA"){
			%unit = %areaDeOrigemNoJogo.myPos4List.getObject(0);
		} else {
			%pos4Count = %areaDeOrigemNoJogo.myPos4List.getCount();
			for(%i = 0; %i < %pos4Count; %i++){
				%tempUnit = %areaDeOrigemNoJogo.myPos4List.getObject(%i);
				if(%tempUnit.dono == %client.player){
					%unit = %areaDeOrigemNoJogo.myPos4List.getObject(%i);
					echo("Unidade encontrada, DONO: " @ %unit.dono);
					%i = %pos4Count;
				}
			}
		}
	}
	return %unit;
}

function jogo::findUnitInimiga(%this, %area, %pos, %client){ //o parâmetro %client é referente a quem está pedindo o comando, não ao inimigo;
	%eval = "%areaDeOrigemNoJogo = %this." @ %area @ ";";
	eval(%eval);
	
	if(%pos $= "pos2" || %pos $= "pos1" || %pos $= "pos0"){
		%eval = "%tempUnit = " @ %areaDeOrigemNoJogo @ "." @ %pos @ "Quem;";
		eval(%eval);
		if(%tempUnit.dono != %client.player){
			%eval = "%unit = " @ %areaDeOrigemNoJogo @ "." @ %pos @ "Quem;";
			eval(%eval);
		}
	} else if(%pos $= "pos3"){
		if(%areaDeOrigemNoJogo.dono !$= "MISTA"){
			%unit = %areaDeOrigemNoJogo.myPos3List.getObject(0);
		} else {
			%pos3Count = %areaDeOrigemNoJogo.myPos3List.getCount();
			for(%i = 0; %i < %pos3Count; %i++){
				%tempUnit = %areaDeOrigemNoJogo.myPos3List.getObject(%i);
				if(%tempUnit.dono != %client.player){
					%unit = %areaDeOrigemNoJogo.myPos3List.getObject(%i);
					echo("Unidade Inimiga encontrada, DONO: " @ %unit.dono.persona.nome);
					%i = %pos3Count;
				}
			}
		}
	} else if (%pos $= "pos4"){
		if(%areaDeOrigemNoJogo.dono !$= "MISTA"){
			%unit = %areaDeOrigemNoJogo.myPos4List.getObject(0);
		} else {
			%pos4Count = %areaDeOrigemNoJogo.myPos4List.getCount();
			for(%i = 0; %i < %pos4Count; %i++){
				%tempUnit = %areaDeOrigemNoJogo.myPos4List.getObject(%i);
				if(%tempUnit.dono != %client.player){
					%unit = %areaDeOrigemNoJogo.myPos4List.getObject(%i);
					echo("Unidade encontrada, DONO: " @ %unit.dono.persona.nome);
					%i = %pos4Count;
				}
			}
		}
	}
	return %unit;
}

function jogo::findAreaNoJogo(%this, %nomeDaArea){
	%eval = "%areaNoJogo = %this." @ %nomeDaArea @ ";";
	eval(%eval);
	
	return %areaNoJogo;
}

function jogo::emboscada(%this, %client, %areaName, %posDeOrigem){
	%areaNoJogo = %this.findAreaNoJogo(%areaName);
	%unitAtacante = %this.findMyUnit(%areaName, %posDeOrigem, %client);
	%unitAtacante.dono.atacou = 1;
	%unitDefensora = %this.findUnitInimigaEmboscada(%areaNoJogo, %client.player);	
	
	%unitDefensoraPos = %unitDefensora.pos;
	
	%atk = a_dado(%unitAtacante, "Atk");
	%def = a_dado(%unitDefensora, "Def");
		
	echo("EMBOSCADA: Atk(" @ %atk @ ") vs Def(" @ %def @ ")");
	if (%atk > %def){
		%unitAtacante.fire(%unitDefensora); //atualiza no server
	} else {
		%unitDefensora.fire(%unitAtacante);
	}
		
	for(%i = 0; %i < %this.playersAtivos; %i++){
		commandToClient(%this.simPlayers.getObject(%i).client, 'executarEmboscada', %areaName, %posDeOrigem, %unitDefensoraPos, %atk, %def); 
	}
	if(%this.observadorOn){
		commandToClient(%this.observador, 'executarEmboscada', %areaName, %posDeOrigem, %unitDefensoraPos, %atk, %def); 
	}
}

function jogo::findUnitInimigaEmboscada(%this, %area, %player){ //o parâmetro %player é referente a quem está pedindo o comando, não ao inimigo;
	%tempUnit = %area.pos0Quem;
	if(%tempUnit.dono != %player && isObject(%tempUnit) && %tempUnit.gulok == true){
		%unidadeInimiga = %area.pos0Quem;	
	} else {
		%tempUnit = %area.pos1Quem;	
		if(%tempUnit.dono != %player){
			%unidadeInimiga = %area.pos1Quem;	
		} else {
			%tempUnit = %area.pos2Quem;	
			if(%tempUnit.dono != %player){
				%unidadeInimiga = %area.pos2Quem;	
			} else {
				%pos3Count = %area.myPos3List.getCount();
				for(%i = 0; %i < %pos3Count; %i++){
					%tempUnit = %area.myPos3List.getObject(%i);
					if(%tempUnit.dono != %player){
						%unidadeInimiga = %area.myPos3List.getObject(%i);
						%i = %pos3Count;
					}
				}	
			}
			if(!isObject(%unidadeInimiga)){
				%pos4Count = %area.myPos4List.getCount();
				for(%i = 0; %i < %pos4Count; %i++){
					%tempUnit = %area.myPos4List.getObject(%i);
					if(%tempUnit.dono != %player){
						%unidadeInimiga = %area.myPos4List.getObject(%i);
						%i = %pos4Count;
					}
				}	
			}
		}
	}
	
	echo("EMBOSCADA: unidade inimiga encontrada na " @ %unidadeInimiga.pos @ " de " @ %unidadeInimiga.onde.getName() @ " (" @ %unidadeInimiga.class @ " do " @ %unidadeInimiga.dono.id @ " - " @ %unidadeInimiga.dono.persona.nome @ ")");
	return %unidadeInimiga;
}





function serverCmdEmboscada(%client, %areaName, %posDeOrigem){
	%jogo = %client.player.jogo;
	%jogo.emboscada(%client, %areaName, %posDeOrigem);
}




