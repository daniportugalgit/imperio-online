// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientCanibalizar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quinta-feira, 11 de dezembro de 2008 5:18
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientVerifyVermes(%area){
	%vermes = 0;
	if(%area.dono !$= "MISTA"){
		%vermes = 0;
		if(%area.pos1Flag $= "verme"){
			%vermes++;	
		}
		if(%area.pos2Flag $= "verme"){
			%vermes++;	
		}
		if(%area.myPos3List.getcount() > 0){
			%vermes += %area.myPos3List.getcount();
		}
				
	}
	return %vermes;
}

function clientAskCanibalizar(){
	%zangao = foco.getObject(0);
	if($myPersona.aca_ldr_1_h3 > 0){
		if($mySelf.canibalismos < %zangao.myCanibalizar){
			%vermes = clientVerifyVermes(%zangao.onde);
			if(%vermes > 1){
				clientPushServercomDot();
				commandToServer('canibalizar', %zangao.onde.getName(), %zangao.pos); 	
			}
		}
	}
}

function clientCmdCanibalizar(%areaNome, %pos){
	%eval = "%area = " @ %areaNome @ ";";
	eval(%eval);
	%eval = "%zangao = %area." @ %pos @ "Quem;";
	eval(%eval);
	
	if(%zangao.dono == $mySelf){
		$mySelf.canibalismos++;
	}
	
	%vermesPool = new SimSet();
	if(%area.pos1Quem.class $= "verme"){
		%vermesPool.add(%area.pos1Quem);
	}
	if(%area.pos2Quem.class $= "verme"){
		%vermesPool.add(%area.pos2Quem);
	}
	for(%i = 0; %i < %area.myPos3List.getCount(); %i++){
		%vermesPool.add(%area.myPos3List.getObject(%i));
	}
	
	for(%i = 0; %i < %vermesPool.getCount(); %i++){
		%verme = %vermesPool.getObject(%i);
		schedule(500 + (500 * %i), 0, "clientKill", %verme);
	}
	schedule(1000 + (500 * %i), 0, "clientPopServerComDot");
	
	alxPlay( canibalizar );
	
	clientFXtxt(%zangao, "canibalizar");
	
	//efeito especial instantâneo:
	%instantFX = new t2dParticleEffect(){scenegraph = %zangao.scenegraph;};
	%instantFX.loadEffect("~/data/particles/canibalizarInstantFX.eff");
	%instantFX.mount(%zangao);
	%instantFX.playEffect();	
		
	//efeito especial permanente:
	%zangaoCanibalizarFX = new t2dParticleEffect(){scenegraph = %zangao.scenegraph;};
	%zangaoCanibalizarFX.loadEffect("~/data/particles/zangao" @ %zangao.dono.myColor @ "CanibalizarFX.eff");
	%zangaoCanibalizarFX.mount(%zangao);
	%zangaoCanibalizarFX.playEffect();
	%zangao.myCanibalizarFx = %zangaoCanibalizarFX;
	
	atualizarBotoesDeCompra();
	hud_canibalizar_btn.setActive(false); //a área perde vermes gradualmente...
	hud_carregar_btn.setActive(false);
}

function clientKill(%unit){
	%unit.kill();	
}

function desligarBonusCanibais(){
	for(%i = 1; %i < 7; %i++){
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		if(isObject(%player.mySimLideres)){
			for(%j = 0; %j < %player.mySimLideres.getCount(); %j++){
				%lider = %player.mySimLideres.getObject(%j);
				%lider.myBonusCanibalMax = 0;	
				%lider.myBonusCanibalMin = 0;
				if(isObject(%lider.myCanibalizarFX)){
					%lider.myCanibalizarFX.setEffectLifeMode("KILL", 1);	
				}
			}
		}
	}
}



















