// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientCamera.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 29 de janeiro de 2008 21:11
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

//seta a câmera:
$zoomFactor = 1; //nível do zoom (1 = sem zoom; mais do que 1 significa zoom-in)
sceneWindow2D.setCurrentCameraZoom($zoomFactor); //tira o zoom, se estiver em zoom-in


function cameraLeft(){
	if ($zoomFactor > 1){
		if(!sceneWindow2D.getIsCameraMoving()){
			if(getWord(sceneWindow2D.getCurrentCameraPosition(), 0)>-1){
				sceneWindow2D.setTargetCameraPosition(getWord(sceneWindow2D.getCurrentCameraPosition(), 0)-9 SPC "0");
				sceneWindow2D.startCameraMove(0.4);
			}
		}			
   	}
}


function cameraRight(){
	if ($zoomFactor > 1){
		if(!sceneWindow2D.getIsCameraMoving()){
			if(getWord(sceneWindow2D.getCurrentCameraPosition(), 0)<9){
				sceneWindow2D.setTargetCameraPosition(getWord(sceneWindow2D.getCurrentCameraPosition(), 0)+9 SPC "0");
				sceneWindow2D.startCameraMove(0.4);
			}
		}
	}
}

function zoomOut(){
	if ($zoomFactor > 1){
		if(!sceneWindow2D.getIsCameraMoving()){
			sceneWindow2D.setCurrentCameraPosition("0 0");
			sceneWindow2D.setTargetCameraZoom(sceneWindow2D.getCurrentCameraZoom()-0.01);
			sceneWindow2D.startCameraMove(0.4);
			$zoomFactor = 1;
		}
	}
}
function zoomIn(){
	if ($zoomFactor == 1){
		if(!sceneWindow2D.getIsCameraMoving()){
			sceneWindow2D.setTargetCameraZoom(sceneWindow2D.getCurrentCameraZoom()+0.01);
			sceneWindow2D.startCameraMove(0.4);
			$zoomFactor = 1.01;
		}
	}
}

function doNothing(){
	//echo("doingNothing");
}


//isso é para as visitas de áreas, mas deixei aki junto com os keyBind
function ligarShift(){
	$shiftOn = true;
}

function desligarShift(){
	$shiftOn = false;
}

function ligarCtrlReal(){
	$ctrlRealOn = true;
}

function desligarCtrlReal(){
	$ctrlRealOn = false;
}


function setNormalZoom(%wheel){
	sceneWindow2D.setTargetCameraArea("-50 -37.5 49 36.5");
	sceneWindow2D.setTargetCameraZoom(1);
	if(%wheel){
		sceneWindow2d.startCameraMove(0.4);
	} else {
		sceneWindow2d.startCameraMove(0.5);
	}
	$normalZoomOn = true;
	$settingCamPitch = false;
}

function setObjetivosZoom(){
	sceneWindow2D.setTargetCameraArea("-69 -23.8 30 50.2");
	sceneWindow2D.setTargetCameraZoom(0.7);
	sceneWindow2d.startCameraMove(0.5);
}

function clientCameraShake(%magnitude, %tempo){
	sceneWindow2D.startCameraShake(%magnitude, %tempo);
}


function setUniZoom(%area1, %area2, %tempo)
{
	if($vendoPoker)
		return;
		
	if($estouNoTutorial)
		return;
		
	if($dandoZoom)
		return;
		
	if($jogadorDaVez == $aiPlayer)
		return;
	
	$normalZoomOn = false;
	setZoomTimer();
	cancel($zoomschedule);
	%unit1PosX = firstWord(%area1.pos1);
	%unit1PosY = getWord(%area1.pos1, 1);
	%unit2PosX = firstWord(%area2.pos1);
	%unit2PosY = getWord(%area2.pos1, 1);
	
	%unitPosX = (%unit1PosX + %unit2PosX) / 2;
	%unitPosY = (%unit1PosY + %unit2PosY) / 2;
			
	%upperLeftX = %unitPosX - 33.33;
	%upperLeftY = %unitPosY - 25;
	
	%lowerRightX = %unitPosX + 32.66;
	%lowerRightY = %unitPosY + 24.33;
	
	if(%upperLeftX < -50){
		%sobraUpperLeftX = %upperLeftX + 50;
		%upperLeftX = -50;
		%lowerRightX -= %sobraUpperLeftX;
	}
	if(%upperLeftY < -37.5){
		%sobraUpperLeftY = %upperLeftY + 37.5;
		%upperLeftY = -37.5;
		%lowerRightY -= %sobraUpperLeftY;
	}
	if(%lowerRightX > 49){
		%sobraLowerRightX = %lowerRightX - 49;
		%lowerRightX = 49;
		%upperLeftX -= %sobraLowerRightX;
	}
	if(%lowerRightY > 29.5){
		%sobraLowerRightY = %lowerRightY - 29.5;
		%lowerRightY = 29.5;
		%upperLeftY -= %sobraLowerRightY;
	}
	sceneWindow2D.setTargetCameraArea(%upperLeftX SPC %upperLeftY SPC %lowerRightX SPC %lowerRightY);

	sceneWindow2d.startCameraMove(0.5);
	echo("ZoomIn: " @ %upperLeftX SPC %upperLeftY SPC %lowerRightX SPC %lowerRightY);
	$zoomschedule = schedule(%tempo, 0, "setNormalZoom");
}

function setMouseZoom(%wheel){
	if($vendoPoker)
		return;
		
	if($estouNoTutorial)
		return;
		
	if($dandoZoom)
		return;
	
	
	$normalZoomOn = false;
	$mouseZoomOn = true;
	$settingCamPitch = false;
	
	cancel($zoomschedule);
		
	%unitPosX = firstWord(sceneWindow2D.getMousePosition());
	%unitPosY = getWord(sceneWindow2D.getMousePosition(), 1);
			
	%upperLeftX = %unitPosX - 33.33;
	%upperLeftY = %unitPosY - 25;
	
	%lowerRightX = %unitPosX + 32.66;
	%lowerRightY = %unitPosY + 24.33;
	
	if(%upperLeftX < -50){
		%sobraUpperLeftX = %upperLeftX + 50;
		%upperLeftX = -50;
		%lowerRightX -= %sobraUpperLeftX;
	}
	if(%upperLeftY < -37.5){
		%sobraUpperLeftY = %upperLeftY + 37.5;
		%upperLeftY = -37.5;
		%lowerRightY -= %sobraUpperLeftY;
	}
	if(%lowerRightX > 49){
		%sobraLowerRightX = %lowerRightX - 49;
		%lowerRightX = 49;
		%upperLeftX -= %sobraLowerRightX;
	}
	if(%lowerRightY > 29.5){
		%sobraLowerRightY = %lowerRightY - 29.5;
		%lowerRightY = 29.5;
		%upperLeftY -= %sobraLowerRightY;
	}
	sceneWindow2D.setTargetCameraArea(%upperLeftX SPC %upperLeftY SPC %lowerRightX SPC %lowerRightY);
	
	if(%wheel)
	{
		sceneWindow2d.startCameraMove(0.4);
		return;
	}
	
	sceneWindow2d.startCameraMove(0.5);
}

function setZoomTimer(){
	$dandoZoom = true;
	$zoomOKTimer = schedule (0.5, 0, "finishZoomTimer");
}

function finishZoomTimer(){
	cancel($zoomOKTimer);
	$dandoZoom = false;	
}

function tryToSetNormalZoom(){
	if($vendoPoker)
		return;
		
	if($rodadaAtual <= 0)
		return;
		
	setNormalZoom();
}



////////////
//Zoom no mouse:

function pitch(%val){
	if(%val > 0){
		%camPitch = 1;
	} else if(%val < 0 && $camPitch > 0){
		%camPitch = -1;
	}
	if(!$settingCamPitch){
		$camPitchSchedule = schedule(200, 0, "setMouseWheelZoom", %camPitch);
		$settingCamPitch = true;
	}
}

moveMap.bind(mouse, zaxis, pitch);

function setMouseWheelZoom(%camPitch){
	if($vendoPoker)
		return;
		
	if($rodadaAtual <= 0)
		return;
	
	cancel($camPitchSchedule);
	
	if(%camPitch > 0)
	{
		setMouseZoom(true);
		return;
	}
		
	setNormalZoom(true);
}


