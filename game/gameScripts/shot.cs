// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\shot.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quinta-feira, 2 de agosto de 2007 6:47
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function shot::createCopy(%this){
      %copy = new t2dStaticSprite(){
         scenegraph = $strategyScene;
         class = %this.class;   
         imageMap = %this.imageMap;
         size = %this.size;
         position = %this.position;
         CollisionActiveReceive = %this.CollisionActiveReceive;
         CollisionActiveSend = %this.CollisionActiveSend;
         CollisionCallback = %this.CollisionCallback;
         CollisionResponseMode = %this.CollisionResponseMode;
         CollisionDetectionMode = %this.CollisionDetectionMode;
         CollisionPolyList = %this.CollisionPolyList;
         CollisionPhysicsReceive = %this.CollisionPhysicsReceive;
         CollisionPhysicsSend = %this.CollisionPhysicsSend;
         WorldLimitCallback = %this.WorldLimitCallback;
         
	};
   %copy.mySpeed = %this.mySpeed;
   return %copy;
}


function shot::moveToLoc(%this, %worldPos)
{
	%speed = %this.mySpeed; //velocidade da BluePrint do projétil;

	%orient = angleBetween(%this.getPosition(), %worldPos); //descobre a orientação correta do objeto com relação ao seu alvo
	%this.setRotation(%orient); //orienta o objeto de acordo com a orientação obtida acima
	
	if(%this.getPosition() !$= %worldPos){    // "se o objeto não estiver na posição-alvo"
		%this.moveTo(%worldPos, %speed, true, true); // manda o objeto andar até o mouse e parar quando chegar lá
	} else {   //"senão", ou seja, "se o objeto já estiver na posição-alvo"
		%this.onPositionTarget(); //chama a função que diz o que fazer quando chegar lá
	}
}


function shot::onPositionTarget(%this){
	if(!isObject(%this.areaAntiga.myCruzarFronteiras)){
		%this.cruzando = false;
	} else {
		%this.cruzando = %this.areaAntiga.myCruzarFronteiras.isMember(%this.areaAlvo);
	}
	if(%this.cruzando){
		%this.setPosition(%this.areaAlvo.transPos);
		%this.areaAlvo = -1;
		%targetPos = %this.lastEnemy.getPosition();
		%this.cruzando = false;
		%this.moveToLoc(%targetPos);
		//anular o comando de zoom:
		setNormalZoom(true); //o parametro true faz ficar 1 décimo de segundo mais rápido
	} else {
		%unidadeAlvo = %this.lastEnemy;
		if(!%this.miss){
			%unidadeAlvo.kill(%this.dono); //matar e dizer quem matou
		}
		%this.effect.dismount();
		%this.effect.stopEffect(1, 1);
		%this.safeDelete(); //destruir o míssil
	}
}

///////////////////////////

function universalCreateShot(%shooter, %inimigo, %projetil, %FX, %sound, %result, %miss, %missPos){
  	// Posiciona o projétil:
	%projetil.setPosition( %shooter.getPosition() );
	%projetil.setWorldLimit( kill, "-50 -38 88 38" );
				
	// Fire FX.
	%fireFX = new t2dParticleEffect() { scenegraph = %shooter.scenegraph; };
	%fireFX.loadEffect("~/data/particles/" @ %FX @ ".eff");
	%fireFX.setLayer( %shooter.getLayer() );
	%fireFX.mount( %projetil );
	
	if(%shooter.gulok && %shooter.class !$= "dragnal"){
		switch$ (%shooter.dono.myColor){
			case "vermelho":
			%red = 0.8;
			%green = 0;
			%blue = 0;
			
			case "laranja":
			%red = 0.8;
			%green = 0.6;
			%blue = 0;
			
			case "amarelo":
			%red = 0.9;
			%green = 0.9;
			%blue = 0;
			
			case "verde":
			%red = 0;
			%green = 0.8;
			%blue = 0;
			
			case "azul":
			%red = 0;
			%green = 1;
			%blue = 1;
			
			case "indigo":
			%red = 0;
			%green = 0;
			%blue = 1;
			
			case "roxo":
			%red = 0.5;
			%green = 0;
			%blue = 0.8;
		}
		
		%myEmitterCount = %fireFX.getEmitterCount();
	
		for(%i = 0; %i < %myEmitterCount; %i++){
			%myTempEmitter = %fireFX.getEmitterObject(%i);
			%myTempEmitter.selectGraph("red_life");
			%myTempEmitter.clearDataKeys();
			%myTempEmitter.addDataKey(0, %red);
			
			%myTempEmitter.selectGraph("green_life");
			%myTempEmitter.clearDataKeys();
			%myTempEmitter.addDataKey(0, %green);
			
			%myTempEmitter.selectGraph("blue_life");
			%myTempEmitter.clearDataKeys();
			%myTempEmitter.addDataKey(0, %blue);
			
			%myTempEmitter.setIntenseParticles(true);
		}	
	}
	
	%fireFX.playEffect();
	
	%projetil.lastEnemy = %inimigo;
	%projetil.areaAntiga = %shooter.onde;
	%projetil.areaAlvo = %inimigo.onde;		
	
	if(%miss){
		%projetil.miss = true;
	}
		
	if(!isObject(%projetil.areaAntiga.myCruzarFronteiras)){
		if(%miss){
			%projetil.action("moveToLoc", %missPos);
		} else {
			%projetil.action("moveToLoc", %inimigo.getPosition());
		}
	} else {
		if(%projetil.areaAntiga.myCruzarFronteiras.isMember(%projetil.areaAlvo)){
			%projetil.action("moveToLoc", %projetil.areaAntiga.transPos);
		} else {
			if(%miss){
				%projetil.action("moveToLoc", %missPos);
			} else {
				%projetil.action("moveToLoc", %inimigo.getPosition());
			}
		}
	}	
	
		
	%projetil.effect = %fireFX;
		
	//para tocar o áudio do tiro:
 	//alxPlay( %sound );	
}