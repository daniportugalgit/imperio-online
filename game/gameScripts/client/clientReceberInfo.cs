// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientReceberInfo.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 25 de janeiro de 2008 17:09
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

//recebe a info do server:
function clientCmdSetNewInfo(%infoNum, %faro, %pilhar){
	%info = clientFindInfo(%infoNum);
	$mySelf.mySimInfo.add(%info);
	
	clientMarkNewMission(%infoNum, %faro, %pilhar); //marca a missão corretamente
	clientAtualizarEstatisticas();
}
/////////////
//Marca na tela  uma info recebida:
function clientMarkNewMission(%infoNum, %faro, %pilhar){
	%info = clientFindInfo(%infoNum);
	%eval = "%areaDaMissao =" SPC %info.area @ ";";
	eval(%eval);
	
	if(%info.bonusPt > 0){
		%newAlvo = missaoMarkerBase.createCopy();	
		%newAlvo.setPosition(%areaDaMissao.pos0);
		//efeito de partículas:
		%fireFX = new t2dParticleEffect() { scenegraph = %newAlvo.scenegraph; };
		%fireFX.loadEffect("~/data/particles/baseFind.eff");
		%fireFX.mount(%newAlvo);
		%fireFX.setEffectLifeMode("KILL", 1);
		%fireFX.playEffect();
		
		if(%areaDaMissao.dono == $mySelf && %areaDaMissao.pos0Flag == true){
			%newAlvo.setAutoRotation(-15);
			%newAlvo.setFrame(2); 
			%newAlvo.setBlendColor($mySelf.corR, $mySelf.corG, $mySelf.corB, 0.7);
		} else {
			%newAlvo.setFrame(1); 
			%newAlvo.setBlendColor(1, 1, 1, 1);
		}
	} else {
		if (%info.bonusM > 0){
			%newAlvo = missaoMarkerMinerio.createCopy();	
			%fireFX_img = "~/data/particles/minerioFind.eff";
			%sound = "achouMinerio";
		} else if (%info.bonusP > 0){
			%newAlvo = missaoMarkerPetroleo.createCopy();	
			%fireFX_img = "~/data/particles/petroleoFind.eff";
			%sound = "achouPetroleo";
		} else if (%info.bonusU > 0){
			%newAlvo = missaoMarkerUranio.createCopy();	
			%fireFX_img = "~/data/particles/uranioFind.eff";
			%sound = "achouUranio";
		}
		
		%newAlvo.setPosition(%areaDaMissao.pos1);
		%newAlvo.setBlendColor(1, 1, 1, 1);
		//efeito de partículas:
		%fireFX = new t2dParticleEffect() { scenegraph = %newAlvo.scenegraph; };
		%fireFX.loadEffect(%fireFX_img);
		%fireFX.mount(%newAlvo);
		%fireFX.setEffectLifeMode("KILL", 1);
		%fireFX.playEffect();
		
		if(%areaDaMissao.dono == $mySelf){
			if($mySelf.mySimExpl.isMember(%info)){
				%newAlvo.setFrame(1); 
			} else {
				%newAlvo.setFrame(2); 
			}
			
			%newAlvo.setAutoRotation(-15);
		}
		
		if(!$noSound){
			if(!$semSomDasMissoes){
				alxPlay(%sound);
			}
		}
	}
	%info.myMark = %newAlvo;
	%newAlvo.myInfo = %info;
	$missionMarksPool.add(%newAlvo);
	%newAlvo.setLayer(3); //deixa na frente de outras missoes que jah estavam neste local, e atrás da unidades, que estão nas layers 1 e 0;
	
	//se for uma missão vinda de faro extremo, coloca o efeito de texto do faro extremo:
	if(%faro){
		clientFXtxt(%info.myMark, "faroExtremo");	
	}
	
	if(%pilhar){
		clientFXtxt(%info.myMark, "pilhar");	
	}
	
	
	//if($normalZoomOn){
	//	schedule(1000, 0, "setUniZoom", %areaDaMissao, %areaDaMissao, 2000);	
	//}
}

























