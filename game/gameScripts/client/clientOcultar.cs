// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientOcultar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quarta-feira, 12 de novembro de 2008 23:47
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskOcultar(){
	echo("pedindo para ocultar estruturas...");
	if($myPersona.aca_av_3 > 0){
		if($rodadaAtual >= $mySelf.reqOcultar){
			if($mySelf.imperiais >= $mySelf.ocultarCusto){
				ocultarInGame_btn.setVisible(false);
				clientPushServercomDot();
				
				$mySelf.imperiais -= $mySelf.ocultarCusto;
				
				$mySelf.oculto = true;
				atualizarImperiaisGui();
				commandToServer('ocultar');	
			}
		}
	}
}

function clientCmdOcultar(%playerId){
	%eval = "%player = $" @ %playerId @ ";";
	eval(%eval);
	%player.oculto = true;
	echo("ocultando estruturas do jogador da vez");
	
	for(%i = 0; %i < %player.mySimBases.getCount(); %i++){
		%estrutura = %player.mySimBases.getObject(%i);
		clientOcultarEstrutura(%estrutura, %player);
	}
	clientPopServercomDot();
}

function clientVerifyOcultar(%unit, %lastDono){
	%newDono = %unit.dono;
	if(%lastDono.oculto){
		if(%newDono.oculto){
			if(%newDono == $mySelf || %newDono == $mySelf.myDupla){
				%unit.setBlendColor(1,1,1,0.6);	
				clientDesocultarFX(%unit);
			} else {
				%unit.setBlendColor(1,1,1,0);
				if(%lastDono == $mySelf || %newDono == $mySelf.myDupla){
					clientOcultarEstrutura(%unit, %newDono);	
				}
			}
		} else {
			%unit.setBlendColor(1,1,1,1);
			clientDesocultarFX(%unit);
			%unit.oculta = false;
		}
	} else {
		if(%newDono.oculto){
			clientOcultarEstrutura(%unit, %newDono);
		}
	}
}

function clientDesocultarFX(%unit){
	%ocultarEffect = new t2dParticleEffect(){scenegraph = %unit.scenegraph;};
	%ocultarEffect.loadEffect("~/data/particles/desocultando.eff");
	%ocultarEffect.setEffectLifeMode("KILL", 2);
	%ocultarEffect.mount(%unit, 0, 0, 0, 0, 0, 0, 0); 
	%ocultarEffect.playEffect();
}

function clientOcultarEstrutura(%estrutura, %player){
	%estrutura.oculta = true;
	if(%player != $mySelf && %player != $mySelf.myDupla){
		%estrutura.setBlendColor(1,1,1,0);
	} else {
		%estrutura.setBlendColor(1,1,1,0.6);
	}
	
	//mostra o efeito de ocultar:
	%ocultarEffect = new t2dParticleEffect(){scenegraph = %estrutura.scenegraph;};
	%ocultarEffect.loadEffect("~/data/particles/ocultando.eff");
	%ocultarEffect.setEffectLifeMode("KILL", 2);
	%ocultarEffect.mount(%estrutura, 0, 0, 0, 0, 0, 0, 0); 
	%ocultarEffect.playEffect();
}