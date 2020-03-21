// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientInstinto.cs
// Copyright          :  
// Author             :  admin
// Created on         :  sábado, 6 de dezembro de 2008 14:53
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function clientVerificarInstinto(%rainha){
	%intinto = 0;
	if(%rainha.dono.instinto && $tipoDeJogo !$= "semPesquisas"){
		for(%i = 0; %i < %rainha.onde.myPos4List.getCount(); %i++){
			%unit = %rainha.onde.myPos4List.getObject(%i);
			if(%unit.class $= "ovo" && %unit.dono == %rainha.dono){
				%instinto++;	
			}
		}
		if(%instinto > 0){
			if(!isObject(%rainha.myInstintoMark)){	
				%instintoMark = new t2dParticleEffect(){scenegraph = %rainha.scenegraph;};
				%instintoMark.loadEffect("~/data/particles/instintoFX.eff");
				%instintoMark.mount(%rainha);
				%instintoMark.playEffect();	
				%rainha.myInstintoMark = %instintoMark;
			}
		} else {
			if(isObject(%rainha.myInstintoMark)){
				%rainha.myInstintoMark.setEffectLifeMode("KILL", 1);
			}
		}
	}
}