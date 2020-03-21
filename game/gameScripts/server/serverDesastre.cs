// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverDesastre.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 8 de abril de 2008 17:04
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function jogo::sortearDesastre(%this){
	%resultado1 = dadoMM(1, 100, 0);
	//echo("RESULT DESASTRE, JOGO(" @ %this.num @ "): " @ %resultado1 @ " de " @ %this.planeta.desastres); 
	if(%resultado1 <= %this.planeta.desastres){
		%resultado2 = dadoMM(1, %this.planeta.areasDeDesastre.getCount(), 0);
		%areaName = %this.planeta.areasDeDesastre.getObject(%resultado2 - 1).getName();
		echo("DESASTRE EM " @ %areaName);
		%eval = "%areaNoJogo = %this." @ %areaName @ ";";
		eval(%eval);
		%this.desastre(%areaNoJogo, %areaNoJogo.myDesastre);	
	}
}

function jogo::desastre(%this, %onde, %tipo, %mega){
	if(!%this.guloksDespertaram || (%this.guloksDespertaram && %onde.myName !$= "UNG_PaGu03")){
		%this.desastres++;
		for(%i = 0; %i < %this.playersAtivos; %i++){
			%client = %this.simPlayers.getObject(%i).client;
			commandToClient(%client, 'desastre', %onde.myName, %tipo, %mega);
		}
		if(%this.observadorOn){
			commandToClient(%this.observador, 'desastre', %onde.myName, %tipo, %mega);
		}
		%this.killAll(%onde);
	}
}


function jogo::killAll(%this, %onde, %autor){
	
		if(%autor $= ""){
			%autor = "DESASTRE";
		}
		while(%onde.myPos3List.getCount() > 0){
			%onde.myPos3List.getObject(0).kill(%autor);	
		}
		if(%onde.terreno $= "terra"){
			while(%onde.myPos4List.getCount() > 0){
				%onde.myPos4List.getObject(0).kill(%autor);	
			}
		}
		for(%i = 0; %i < 5; %i++){ //mata até mesmo uam rainha com 5 vermes em cima dela, ou um líder com escudo2; mata tudo;
			if(%onde.pos2Flag !$= "nada"){
				%onde.pos2Quem.kill(%autor);
			}
			if(%onde.pos1Flag !$= "nada"){
				%onde.pos1Quem.kill(%autor);
			}
		}
		if(%onde.pos0Flag){
			if(!%onde.pos0Quem.grandeMatriarca){
				%onde.dono.mySimAreas.remove(%onde);
				%onde.pos0Quem.safeDelete();	
				%onde.pos0Flag = false; //marca na área que ela não tem mais base
				%onde.pos0Quem = 0; //marca que a base não é mais o <quem> da pos0 da área
			}
		}
		%onde.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
	
		%this.verificarGruposGlobal();
		%this.verificarObjetivosGlobal();
		%this.verificarPlayersMortos();
	
}

function jogo::sortearMegaDesastre(%this){
	%fator = mFloor(5 / %this.disparosOrbitais);
	if(%fator < 1){
		%fator = 1;	
	}
	%resultadoMEGA1 = dadoMM(1, %fator, 0);
	//echo("RESULT MEGA-DESASTRE, JOGO(" @ %this.num @ "), %fator(" @ %fator @ "): " @ %resultadoMEGA1); 
	if(%resultadoMEGA1 == 1){
		for(%i = 0; %i < 3; %i++){
			%resultado2 = dadoMM(1, %this.planeta.areasPool.getCount(), 0);
			%areaName = %this.planeta.areasPool.getObject(%resultado2 - 1).getName();
			echo("DESASTRE EM " @ %areaName);
			%eval = "%areaNoJogo = %this." @ %areaName @ ";";
			eval(%eval);
			if(%areaNoJogo.ilha $= ""){
				if(%areaNoJogo.oceano $= ""){
					if(!%this.guloksDespertaram || (%this.guloksDespertaram && %areaNoJogo.myName !$= "UNG_PaGu03")){
						%this.desastre(%areaNoJogo, "furacao");
					}
				}
			}
		}
	}
}

function jogo::verificarPlayersMortos(%this){
	for(%i = 0; %i < %this.playersAtivos; %i++){
		%player = %this.simPlayers.getObject(%i);
		if(!%player.taMorto){
			if(%player.mySimAreas.getCount() < 1 && %player != %this.aiPlayer){
				serverPlayerMorteNatural(%player);
			}
		}
	}
}


