// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientIncorporar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  sábado, 6 de dezembro de 2008 18:03
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskIncorporar(){
	%rainha = foco.getObject(0);
	if(%rainha.onde.pos1Quem.class $= "verme"){
		%posDeOrigem = "pos1";	
	} else if(%rainha.onde.pos2Quem.class $= "verme"){
		%posDeOrigem = "pos2";	
	} else {
		%posDeOrigem = "pos3";	
	}
	clientAskEmbarcar(%rainha.onde, %posDeOrigem, %rainha.onde, "pos0", false, true);	
}

function clientIncorporarEffect(%rainha){
	%incorporarFXtxt = new t2dParticleEffect(){scenegraph = %rainha.scenegraph;};
	%incorporarFXtxt.loadEffect("~/data/particles/incorporarFX_txt.eff");
	%rainhaPosX = firstWord(%rainha.getPosition());
	%rainhaPosY = getWord(%rainha.getPosition(), 1);
	%myPosY = %rainhaPosY - 2;
	%incorporarFXtxt.setPosition(%rainhaPosX SPC %myPosY);
	%incorporarFXtxt.setEffectLifeMode("KILL", 3);
	%incorporarFXtxt.playEffect();
}