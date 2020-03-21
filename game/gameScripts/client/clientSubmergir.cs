// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientSubmergir.cs
// Copyright          :  
// Author             :  admin
// Created on         :  sábado, 6 de dezembro de 2008 19:18
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function clientAskSubmergir(){
	%rainha = foco.getObject(0);
	if(%rainha.submersa){
		commandToServer('emergir', %rainha.onde.getName());
	} else {
		if($myPersona.aca_v_4 > 0 && $mySelf.imperiais >= (4 - $myPersona.aca_v_4)){
			commandToServer('submergir', %rainha.onde.getName());	
		}
	}
}

function clientCmdSubmergir(%areaNome){
	%eval = "%area = " @ %areaNome @ ";";
	eval(%eval);
	%rainha = %area.pos0Quem;
	%rainha.submersa = true;
	
	%submergirFX = new t2dParticleEffect(){scenegraph = %rainha.scenegraph;};
	%submergirFX.loadEffect("~/data/particles/submergirFX.eff");
	%submergirFX.mount(%rainha);
	%submergirFX.setEffectLifeMode("KILL", 1);
	%submergirFX.playEffect();
	
	//efeito de texto:
	clientFXtxt(%rainha, "submergir");
	
	%rainha.setBlendColor(1, 1, 1, 0.7);
	
	if(%rainha.dono == $mySelf){
		$mySelf.imperiais -= (4 - $myPersona.aca_v_4);	
		atualizarImperiaisGui();
		clientClearUndo();
	}
}

function clientAskEmergir(){
	%rainha = foco.getObject(0);
	commandToServer('emergir', %rainha.onde.getName());	
}

function clientCmdEmergir(%areaNome){
	%eval = "%area = " @ %areaNome @ ";";
	eval(%eval);
	%rainha = %area.pos0Quem;
	%rainha.submersa = false;
	
	%emergirFX = new t2dParticleEffect(){scenegraph = %rainha.scenegraph;};
	%emergirFX.loadEffect("~/data/particles/emergirFX.eff");
	%emergirFX.mount(%rainha);
	%emergirFX.setEffectLifeMode("KILL", 1);
	%emergirFX.playEffect();
	
	//efeito de texto:
	clientFXtxt(%rainha, "emergir");
	
	%rainha.setBlendColor(1, 1, 1, 1);
	atualizarBotoesDeCompra();
	clientClearUndo();
}