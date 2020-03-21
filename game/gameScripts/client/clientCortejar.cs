// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientCortejar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 14 de dezembro de 2008 8:33
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskCortejar(){
	%zangao = foco.getObject(0);	
	
	if($myPersona.aca_ldr_2_h4 > 0){
		if(isObject(%zangao.onde.pos0Quem)){
			if($mySelf.cortejos < $myPersona.aca_ldr_2_h4){
				commandToServer('cortejar', %zangao.onde.getName(), %zangao.pos);	
			} else {
				echo("Já cortejou tudo que podia!");	
			}
		}
	}
}

function clientCmdCortejar(%areaNome, %pos){
	%eval = "%area = " @ %areaNome @ ";";
	eval(%eval);
	%eval = "%zangao = %area." @ %pos @ "Quem;";
	eval(%eval);
	%rainha = %area.pos0Quem;
	
	if(%zangao.dono == $mySelf){
		$mySelf.cortejos++;
	}
	
	//efeito de texto:
	clientFXtxt(%rainha, "cortejar");
		
	//efeito especial permanente:
	%cortejarPermaFX = new t2dParticleEffect(){scenegraph = %rainha.scenegraph;};
	%cortejarPermaFX.loadEffect("~/data/particles/cortejarFX.eff");
	%cortejarPermaFX.mount(%rainha);
	%cortejarPermaFX.playEffect();
	%rainha.myCortejarFx = %cortejarPermaFX;
	
	
	%rainha.cortejada = true;
	clientClearUndo();
	atualizarBotoesDeCompra();
}

function clientRemoverCortejadas(){
	for(%p = 1; %p < 7; %p++){
		%eval = "%player = $player" @ %p @ ";";
		eval(%eval);
		for(%i = 0; %i < %player.mySimBases.getcount(); %i++){
			%base = %player.mySimBases.getObject(%i);
			%base.cortejada = false;
			if(isObject(%base.myCortejarFx)){
				%base.myCortejarFx.setEffectLifeMode("KILL", 1);
			}
		}
	}
}