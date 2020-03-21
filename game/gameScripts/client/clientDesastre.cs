// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientDesastre.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 8 de abril de 2008 17:47
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientCmdDesastre(%onde, %tipo, %mega){
	%explosao = new t2dParticleEffect(){scenegraph = %onde.scenegraph;};
	%explosao.loadEffect("~/data/particles/" @ %tipo @ ".eff");
	
	%desastreMark = new t2dParticleEffect(){scenegraph = %onde.scenegraph;};
	%desastreMark.loadEffect("~/data/particles/desastreMark.eff");
	%desastreMark.setPosition(%onde.pos0);
	%desastreMark.playEffect();
		
	if(%tipo $= "vulcao" || %tipo $= "gas"){
		clientCameraShake(0.6, 4);	
		%explosao.setPosition(%onde.pos0);
		%explosao.setEffectLifeMode("KILL", 5);
		//alxPlay( somDoDesastre );
	} else if(%tipo $= "furacao"){
		%furacaoCopy = furacao_BP.clone();
		%furacaoCopy.setPosition(%onde.pos0);
		%explosao.setEffectLifeMode("INFINITE");
		%explosao.mount(%furacaoCopy);
		clientCameraShake(0.3, 4);	
		schedule(4500, 0, "clientApagarFuracao", %furacaoCopy, %explosao);
	}
	%explosao.playEffect();
	schedule(1000, 0, "clientKillAll", %onde);
	
	clientMsg(%tipo, 4500); //mostra a msg de desatre
	
	/*
	if(%mega $= "" || %mega == false){
		schedule(1500, 0, "setUniZoom", %onde, %onde, 3000);	
	}
	*/
}

function clientKillAll(%onde){
	while(%onde.myPos3List.getCount() > 0){
		%onde.myPos3List.getObject(0).kill();	
	}
	if(%onde.terreno $= "terra"){
		if(!%onde.ilha){
			while(%onde.myPos4List.getCount() > 0){
				%onde.myPos4List.getObject(0).kill();	
			}
		}
	}
	for(%i = 0; %i < 5; %i++){ //mata até mesmo uam rainha com 5 vermes em cima dela, ou um líder com escudo2; mata tudo;
		if(%onde.pos2Flag !$= "nada"){
			%onde.pos2Quem.kill();
		}
		if(%onde.pos1Flag !$= "nada"){
			%onde.pos1Quem.kill();
		}
	}
	if(%onde.pos0Flag){
		if(!%onde.pos0Quem.grandeMatriarca){
			if(isObject(%onde.pos0Quem.myReciclarEffect)){
				%onde.pos0Quem.myReciclarEffect.safeDelete();
			}
			%onde.pos0Quem.safeDelete();	
			%onde.pos0Flag = false; //marca na área que ela não tem mais base
			%onde.pos0Quem = 0; //marca que a base não é mais o <quem> da pos0 da área
		}
	}
	
	%onde.resolverMyStatus(); //marca como vazia ou whatever needed	
	
	clientAtualizarEstatisticas();
	schedule(1500, 0, "clientAtualizarEstatisticas");
}

function clientApagarFuracao(%furacao, %explosao){
	%furacao.safeDelete();
	%explosao.stopEffect(1, 1);
}

function clientZoomNoDesastre(%pos){
	%posX = firstWord(%pos);
	%posY = getWord(%pos, 1);
		
	//X1, Y1, X2, Y2
	//-50 -37.5 49 36.5 = original area
	%targetX1 = %posX - 50;
	%targetX2 = %posX + 49;
	%targetY1 = %posY - 37.5;
	%targetY2 = %posY + 36.5;
	
	/*
	//tenta previnir a tela de mostrar partes pretas:
	if(%targetX1 < -50){
		%targetX1 = -50;	
	}
	if(%targetX2 > 49){
		%targetX2 = 49;	
	}
	if(%targetY1 < -37.5){
		%targetY1 = -37.5;	
	}
	if(%targetY2 > 36.5){
		%targetY2 = 36.5;	
	}
	*/
	
	%targetString = %targetX1 SPC %targetY1 SPC %targetX2 SPC %targetY2;
	
	sceneWindow2D.setTargetCameraArea(%targetString);
	sceneWindow2D.setTargetCameraZoom(2);
	sceneWindow2d.startCameraMove(1.0);
	
	cancel($backToNormalZoomschedule);
	$backToNormalZoom = schedule(5000, 0, "setNormalZoom");
}
