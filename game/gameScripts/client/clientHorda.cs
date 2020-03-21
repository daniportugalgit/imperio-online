// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientHorda.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quarta-feira, 10 de dezembro de 2008 4:39
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientVerifyHorda(%area){
	%vermes = clientVerifyVermes(%area);
	if(%area.dono.horda > 0){
		if(%vermes >= 4){
			if(!isObject(%area.myHordaMark)){
				%pos1X = firstWord(%area.pos1);
				%pos1Y = getWord(%area.pos1, 1);
				%pos2X = firstWord(%area.pos2);
				%pos2Y = getWord(%area.pos2, 1);
				%posX = (%pos1X + %pos2X) / 2;
				%posY = (%pos1Y + %pos2Y) / 2;
				
				%hordaMark = new t2dParticleEffect(){scenegraph = %area.scenegraph;};
				%hordaMark.loadEffect("~/data/particles/hordaFX.eff");
				%hordaMark.setPosition(%posX SPC %posY);
				%hordaMark.setSize("4 4");
				%hordaMark.playEffect();	
				%area.myHordaMark = %hordaMark;
			}
		} else {
			if(isObject(%area.myHordaMark)){
				%area.myHordaMark.setEffectLifeMode("KILL", 1);
			}
		}
	} else {
		if(isObject(%area.myHordaMark)){
			%area.myHordaMark.setEffectLifeMode("KILL", 1);
		}
	}
	return %vermes;
}