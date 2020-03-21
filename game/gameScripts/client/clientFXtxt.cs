// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientFXtxt.cs
// Copyright          :  
// Author             :  admin
// Created on         :  segunda-feira, 8 de dezembro de 2008 3:17
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientFXtxt(%unit, %txt){
	%tempFXtxt = new t2dParticleEffect(){scenegraph = %unit.scenegraph;};
	%tempFXtxt.loadEffect("~/data/particles/" @ %txt @ "FX_txt.eff");
	%unitPosX = firstWord(%unit.getPosition());
	%unitPosY = getWord(%unit.getPosition(), 1);
	%myPosY = %unitPosY - 2;
	%tempFXtxt.setPosition(%unitPosX SPC %myPosY);
	%tempFXtxt.setEffectLifeMode("KILL", 3);
	%tempFXtxt.playEffect();
}