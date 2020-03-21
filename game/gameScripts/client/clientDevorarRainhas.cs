// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientDevorarRainhas.cs
// Copyright          :  
// Author             :  admin
// Created on         :  sábado, 13 de dezembro de 2008 17:49
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskDevorarRainha(){
	%zangao = foco.getObject(0);
	if($myPersona.aca_ldr_1_h4 > 0){
		if(%zangao.onde.pos0Flag && %zangao.onde.pos0Quem.class $= "rainha"){
			commandToServer('devorarRainha', %zangao.onde.getName(), %zangao.pos); 	
		}
	}
}

function clientCmdDevorarRainha(%areaNome, %pos){
	%eval = "%area = " @ %areaNome @ ";";
	eval(%eval);
	%eval = "%zangao = %area." @ %pos @ "Quem;";
	eval(%eval);
	%rainha = %area.pos0Quem;	
	
	alxPlay( canibalizar );
	
	clientFXtxt(%zangao, "devorarRainha");
	
	//efeito especial permanente:
	if(!isObject(%zangao.myComeuRainhaFx)){
		%zangaoComeuRainhaFX = new t2dParticleEffect(){scenegraph = %zangao.scenegraph;};
		%zangaoComeuRainhaFX.loadEffect("~/data/particles/ComeuRainhaFX.eff");
		%zangaoComeuRainhaFX.mount(%zangao);
		%zangaoComeuRainhaFX.playEffect();
		%zangao.myComeuRainhaFx = %zangaoComeuRainhaFX;
	}
	
	%zangao.myRainhasDevoradas++;
	
	schedule(1000, 0, "clientKill", %rainha);
	schedule(1500, 0, "atualizarBotoesDeCompra");
}

