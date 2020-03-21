// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientMatriarca.cs
// Copyright          :  
// Author             :  admin
// Created on         :  segunda-feira, 8 de dezembro de 2008 20:43
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskMatriarca(){
	%rainha = foco.getObject(0);
	if(isObject(%rainha)){
		if($myPersona.aca_v_6 > 0){
			%myCusto = 10;
			if($myPersona.aca_av_4 > 0){
				%myCusto = 8;	
			}
			if(isObject($mySelf.mySimMatriarcas)){
				%myCount = $mySelf.mySimMatriarcas.getCount();
			} else {
				%myCount = 0;
			}
			if(%myCount < 1){
				if($mySelf.imperiais >= %myCusto){
					if(!%rainha.estahVerde){
						commandToServer('matriarca', %rainha.onde.getName());
					} else {
						//btn não deveria ficar ativo	
					}
				}
			}
		}
	}	
}

function clientCmdMatriarca(%areaNome){
	%eval = "%area = " @ %areaNome @ ";";
	eval(%eval);
	%rainha = %area.pos0Quem;
	%rainha.matriarca = true;
	%rainha.crisalida = false;
	%rainha.isMoveable = false;
	if(!isObject(%rainha.dono.mySimMatriarcas)){
		%rainha.dono.mySimMatriarcas = new SimSet();
	}
	%rainha.dono.mySimMatriarcas.add(%rainha);
	%rainha.dono.mySimCrisalidas.remove(%rainha);
	
	playSound("matriarca_nascer", 2, true);
	
	//efeito de texto:
	clientFXtxt(%rainha, "matriarca");
	
	//mata o efeito anterior, de crisálida:
	%rainha.myCrisalidaFx.setEffectLifeMode("KILL", 1);
	
	//efeito especial instantâneo:
	%instantFX = new t2dParticleEffect(){scenegraph = %rainha.scenegraph;};
	%instantFX.loadEffect("~/data/particles/matriarcaInstantFX.eff");
	%instantFX.mount(%rainha);
	%instantFX.setEffectLifeMode("KILL", 1);
	%instantFX.playEffect();	
		
	//efeito especial permanente:
	%matriarcaPermaFX = new t2dParticleEffect(){scenegraph = %rainha.scenegraph;};
	%matriarcaPermaFX.loadEffect("~/data/particles/matriarca" @ %rainha.dono.myColor @ "FX.eff");
	%matriarcaPermaFX.mount(%rainha);
	%matriarcaPermaFX.playEffect();
	%rainha.myMatriarcaFx = %matriarcaPermaFX;
	
	%rainha.setRotation(0);
	%rainha.setMyImage();
	
	%custo = 10;
	if($myPersona.aca_av_4 > 0){
		%custo = 8;	
	}
	if($mySelf == $jogadorDaVez)
	{
		$mySelf.imperiais -= %custo;
		atualizarImperiaisGui();
	}
	%rainha.onde.resolverMyStatus();
	clientAtualizarEstatisticas();
	atualizarBotoesDeCompra();
	schedule(3000, 0, "clientPushMatriarcaDetectada", %area);
}


function clientPushMatriarcaDetectada(%area)
{
	if($estouNoTutorial)
		return;
		
	if(%area.pos0Quem.dono == $mySelf)
		return;
			
	clientResetTutTips();
		
	%img1 = "tutMatriarcaIcon";
	%img2 = "tutMatriarcaIcon";
	inGame_arrowOnUnit(%area.pos0Quem, "MATRIARCA GULOK", "Esta Matriarca vale 5 pontos", "para seu dono e representa uma", "severa ameaça para nós.", "Destrua-a se puder.", "", "", "", %img1, %img2, true); //o último param é presença de um closeBtn
	$acordoTipSchedule = schedule(6500, 0, "tut_clearTips");
}