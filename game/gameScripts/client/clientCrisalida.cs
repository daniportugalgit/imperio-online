// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientCrisalida.cs
// Copyright          :  
// Author             :  admin
// Created on         :  segunda-feira, 8 de dezembro de 2008 3:03
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskCrisalida(){
	%rainha = foco.getObject(0);
	if(isObject(%rainha)){
		if(%rainha.onde.terreno $= "terra"){
			if($myPersona.aca_v_5 > 0){
				%myMax = $myPersona.aca_v_5;
				if(%myMax > 2){
					%myMax = 2;
					%myCusto = 8;
				} else {
					%myCusto = 10;	
				}
				if($myPersona.aca_av_4 > 0){
					%myCusto = 7;	
				}
				if(isObject($mySelf.mySimCrisalidas)){
					%myCount = $mySelf.mySimCrisalidas.getCount();
				} else {
					%myCount = 0;
				}
				if(%myCount < %myMax){
					if($mySelf.imperiais >= %myCusto){
						commandToServer('crisalida', %rainha.onde.getName());
					}
				}
			}
		}
	}
}

function clientCmdCrisalida(%areaNome){
	%eval = "%area = " @ %areaNome @ ";";
	eval(%eval);
	%rainha = %area.pos0Quem;
	%rainha.crisalida = true;
	%rainha.isMoveable = false;
	if(!isObject(%rainha.dono.mySimCrisalidas)){
		%rainha.dono.mySimCrisalidas = new SimSet();
	}
	%rainha.dono.mySimCrisalidas.add(%rainha);
	
	playSound("crisalida_nascer", 1, true);
	
	//efeito de texto:
	clientFXtxt(%rainha, "crisalida");
		
	//efeito especial permanente:
	%crisalidaPermaFX = new t2dParticleEffect(){scenegraph = %rainha.scenegraph;};
	%crisalidaPermaFX.loadEffect("~/data/particles/crisalida" @ %rainha.dono.myColor @ ".eff");
	%crisalidaPermaFX.mount(%rainha);
	%crisalidaPermaFX.playEffect();
	%rainha.myCrisalidaFx = %crisalidaPermaFX;
	
	%rainha.setRotation(0);
	%rainha.setMyImage();
	if($myPersona.aca_av_4 < 2){
		%rainha.estahVerde = true; //ainda não pode evoluir pra matriarca, só na próxima rodada.
	}
	
	%custo = 10;
	if($myPersona.aca_v_5 == 3){
		%custo = 8;	
	}
	if($myPersona.aca_av_4 > 0){
		%custo = 7;	
	}
	if($mySelf == $jogadorDaVez)
	{
		$mySelf.imperiais -= %custo;
		atualizarImperiaisGui();
	}
	
	%rainha.onde.resolverMyStatus();
	clientAtualizarEstatisticas();
	atualizarBotoesDeCompra();
	
	schedule(3000, 0, "clientPushCrisalidaDetectada", %area);
}

//função para não deixar uma crisálida evoluir no turno em que entra em jogo:
function verificarCrisalidasVerdes(){
	if(isObject($mySelf.mySimCrisalidas)){
		for(%i = 0; %i < $mySelf.mySimCrisalidas.getcount(); %i++){
			%crisalida = $mySelf.mySimCrisalidas.getObject(%i);
			%crisalida.estahVerde = false;	
		}
	}
}

function rainha::setMyFX(%this){
	%this.myCrisalidaFx.setEffectLifeMode("KILL", 1);
	%this.setMyImage();
	
	schedule(2000, 0, "clientPutCrisalidaFX", %this);
}

function clientPutCrisalidaFX(%rainha){
	%crisalidaPermaFX = new t2dParticleEffect(){scenegraph = %rainha.scenegraph;};
	%crisalidaPermaFX.loadEffect("~/data/particles/crisalida" @ %rainha.dono.myColor @ ".eff");
	%crisalidaPermaFX.mount(%rainha);
	%crisalidaPermaFX.playEffect();
	%rainha.myCrisalidaFx = %crisalidaPermaFX;
}


function clientPushCrisalidaDetectada(%area)
{
	if($estouNoTutorial)
		return;
		
	if($myPersona.especie $= "gulok")
		return;
		
	clientResetTutTips();
		
	%img1 = "tutCrisalidaIcon";
	%img2 = "tutCrisalidaIcon";

	inGame_arrowOnUnit(%area.pos0Quem, "CRISÁLIDA GULOK", "Crisálida detectada!", "Capture e mantenha até", "o fim da partida para", "receber +3 pontos.", "", "", "", %img1, %img2, true); //o último param é presença de um closeBtn
	$acordoTipSchedule = schedule(6500, 0, "tut_clearTips");
}

function clientResetTutTips()
{
	cancel($acordoTipSchedule);
	
	tut_clearTips();
	
	if($tutMod_arrowSoldadoX $= "")
		tut_pegarResolucao();		
}


function inGame_arrowOnUnit(%obj, %title, %linha1, %linha2, %linha3, %linha4, %linha1b, %linha2b, %linha3b, %img1, %img2, %closeBtn){
	tut_arrowClearLinhas();
			
	//seta para seleção:
	%newPosX = FirstWord(%obj.getPosition());
	%newPosY = GetWord(%obj.getPosition(), 1);
	
	%quadrante = clientGetQuadrante(%newPosX, %newPosY);
	%myArrow = clientGetTutArrow(%quadrante);
	
	%orient = angleBetween(%myArrow.myDefaultPos, %obj.getPosition());
	
	%myArrow.setRotation(%orient);
	%myArrow.setPosition(%newPosX, %newPosY);	
	%setaGUIPOS = SceneWindow2d.getWindowPoint(%newPosX, %newPosY);
	%newPosX = FirstWord(%setaGUIPOS);
	%newPosY = GetWord(%setaGUIPOS, 1);
		
	%myArrow.setVisible(true);
		
	tutMiniGuiTitle_txt.text = %title;
	tutMiniGuiLinha1_txt.text = %linha1;
	tutMiniGuiLinha2_txt.text = %linha2;
	tutMiniGuiLinha3_txt.text = %linha3;
	tutMiniGuiLinha4_txt.text = %linha4;
	tutMiniGuiLinha1b_txt.text = %linha1b;
	tutMiniGuiLinha2b_txt.text = %linha2b;
	tutMiniGuiLinha3b_txt.text = %linha3b;
		
	if(%myArrow.myDefaultPos $= tutArrowAustralia.myDefaultPos){
		tutMiniHud.setPosition(%newPosX+$tutMod_hudAustraliaX, %newPosY+$tutMod_hudAustraliaY);
	} else {
		tutMiniHud.setPosition(%newPosX+$tutMod_hudAmNorteX, %newPosY+$tutMod_hudAmNorteY);
	}
	
	if(%closeBtn){
		tutMiniHud_closeBtn.setVisible(true);	
	} else {
		tutMiniHud_closeBtn.setVisible(false);	
	}
	
	tutMiniHud.setVisible(true);
	
	$tutIconAceso = false; // começa mostrando o btn que deve ser pressionado, a segunda imagem. 
	tut_piscarIcon(%img1, %img2);
}

function clientGetQuadrante(%posX, %posY)
{
	if(%posX <= 0){
		%quadX = "left";
	} else {
		%quadX = "right";	
	}
	
	if(%posY <= -11){
		%quadY = "up";
	} else {
		%quadY = "down";	
	}
	
	if(%quadX $= "left" && %quadY $= "up")
		return 1;
		
	if(%quadX $= "right" && %quadY $= "up")
		return 2;
	
	if(%quadX $= "right" && %quadY $= "down")
		return 3;
		
	if(%quadX $= "left" && %quadY $= "down")
		return 4;
}	

function clientGetTutArrow(%quadrante)
{
	if(%quadrante == 2 || %quadrante == 3)
		%myArrow = tutArrowAustralia;	
		
	if(%quadrante == 1 || %quadrante == 4)
		%myArrow = tutArrowAmNorte;	
	
	return %myArrow;
}