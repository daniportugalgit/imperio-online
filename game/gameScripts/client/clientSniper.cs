// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientSniper.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 31 de março de 2008 14:41
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskSniper(%areaDeOrigem, %posDeOrigem, %areaAlvo){
	%lider = clientGetGameUnit(%areaDeOrigem, %posDeOrigem);
	if($mySelf == $jogadorDaVez){
		if(%lider.mySnipers > 0){
			%lider.mySnipers--;
			if(%areaAlvo.pos0Quem.gulok){
				if(!%areaAlvo.pos0Quem.submersa){
					clientPushServerComDot();
					commandToServer('sniper', %areaDeOrigem.getName(), %posDeOrigem, %areaAlvo.getName(), "pos0");	
				} else {
					desligarSniperShot();
					clientMsg("sniperEmSubmersa", 3000);	
				}
			} else {
				clientPushServerComDot();
				if(%areaAlvo.pos1Quem.class $= "soldado" && (%areaAlvo.pos2Quem.class $= "tanque" || %areaAlvo.pos2Quem.class $= "lider")){
					commandToServer('sniper', %areaDeOrigem.getName(), %posDeOrigem, %areaAlvo.getName(), "pos2");	
				} else {
					commandToServer('sniper', %areaDeOrigem.getName(), %posDeOrigem, %areaAlvo.getName(), "pos1");	
				}	
			}
		} else {
			desligarSniperShot();
			clientAskAtacar(%areaDeOrigem.getName(), %areaAlvo.getName());
			echo("Seus tiros de Sniper acabaram."); //na verdade, o botão nem fica ativo se não pode mais atirar;
		}
	}
}

function clientCmdSniper(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo){
	desligarSniperShot();
	%lider = clientGetGameUnit(%areaDeOrigem, %posDeOrigem);
	
	%lider.dono.myDiplomataHud.bitmap = "~/data/images/playerHudAtaque.png"; //marca que quem atacou não é mais diplomata
	%lider.dono.atacou = true;
	
	//FX:
	%sniperFX = new t2dParticleEffect(){scenegraph = %lider.scenegraph;};
	%sniperFX.loadEffect("~/data/particles/sniperTxt.eff");
	%sniperFX.mount(%lider);
	%sniperFX.playEffect();
	
	%unitAlvo = clientGetGameUnit(%areaAlvo, %posAlvo);
	if($mySelf == $jogadorDaVez){
		atualizarBotoesDeCompra();	
	}
	%lider.sniperFire(%unitAlvo);
	clientPopServerComDot();
}


function ligarSniperShot(){
	sniper_btn.setStateOn(true);
	$SNIPERSHOT = true;	
}

function desligarSniperShot(){
	sniper_btn.setStateOn(false);
	$SNIPERSHOT = false;	
}

function toggleSniperBtn(){
	if($SNIPERSHOT){
		desligarSniperShot();
	} else {
		ligarSniperShot();
	}
}