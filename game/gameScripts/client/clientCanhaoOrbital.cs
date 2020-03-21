// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientCanhaoOrbital.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 9 de abril de 2008 21:37
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function clientAskCanhaoOrbital(%onde){
	echo("pedindo canhão orbital");
	$disparoOrbitalON = false;
	if($myPersona.aca_a_2 > 0){
		if($mySelf.disparosOrbitais == 0){
			if($rodadaAtual >= $mySelf.reqCanhao){
				canhaoOrbital_btn.setVisible(false);
				clientPushServercomDot();
				
				$mySelf.uranios -= (4 - $myPersona.aca_a_2);
				
				$mySelf.disparosOrbitais++;
				atualizarRecursosGui();
				commandToServer('disparoOrbital', %onde.getName());	
			}
		}
	}
}

function clientCmdCanhaoOrbital(%onde){
	clientClearDisparoMark();
	$jogadorDaVez.myDiplomataHud.bitmap = "~/data/images/playerHudAtaque.png"; //marca que quem atacou não é mais diplomata
	$jogadorDaVez.atacou = true;
	echo("efetuando disparo orbital");
	%tiroCopy = tiroOrbital_BP.createCopy();
	
	%tiroEffect = new t2dParticleEffect(){scenegraph = %tiroCopy.scenegraph;};
	%tiroEffect.loadEffect("~/data/particles/rastroOrbital.eff");
	%tiroEffect.mount(%tiroCopy);
	%tiroEffect.playEffect();
	
	%tiroCopy.myEffect = %tiroEffect;
	%tiroCopy.myAlvo = %onde;
	if(%onde.ilha){
		%tiroCopy.moveToLoc(%onde.pos1);	
	} else {
		%tiroCopy.moveToLoc(%onde.pos0);
	}
}

function clientCanhaoOrbitalExplodir(%onde){
	%desastreMark = new t2dParticleEffect(){scenegraph = %onde.scenegraph;};
	%desastreMark.loadEffect("~/data/particles/desastreMark.eff");
	if(%onde.ilha){
		%desastreMark.setPosition(%onde.pos1);
	} else {
		%desastreMark.setPosition(%onde.pos0);
	}
	%desastreMark.playEffect();
	
	%CORBeffect = new t2dParticleEffect(){scenegraph = %onde.scenegraph;};
	%CORBeffect.loadEffect("~/data/particles/canhaoOrbital.eff");
	if(%onde.ilha){
		%CORBeffect.setPosition(%onde.pos1);
	} else {
		%CORBeffect.setPosition(%onde.pos0);
	}
	%CORBeffect.setEffectLifeMode("KILL", 5);
	%CORBeffect.playEffect();
	//alxPlay( somDoCanhaoOrbital );
	clientCameraShake(4, 4);	
	schedule(2000, 0, "clientKillAll", %onde);
	clientMsg("canhaoOrbital", 5000); //mostra a msg de Canhão Orbital
	schedule(1500, 0, "setUniZoom", %onde, %onde, 3000);	
	clientPopServercomDot();
}


function tiroOrbital::moveToLoc(%this, %worldPos){
	%speed = 50;

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true, true); 
}


function tiroOrbital::onPositionTarget(%this){
	%this.myEffect.dismount();
	%this.myEffect.stopEffect(1, 1);
	clientCanhaoOrbitalExplodir(%this.myAlvo);
	%this.safeDelete(); //destruir o míssil
}

function tiroOrbital::createCopy(%this){
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
   return %copy;
}


function clientToggleCanhaoOrbitalON(){
	if($disparoOrbitalON){
		clientCancelarCanhao();
	} else {
		$disparoOrbitalON = true;
		clientMsgCanhaoOrbital();
		canhaoOrbital_Btn.setStateOn(true);
	}
}


function clientCancelarCanhao(){
	$disparoOrbitalON = false;
	$geoDisparoON = false;
	$dragnalAtkON = false;
	$virusON = false;
	clientMsgGuiZerar();	
	canhaoOrbital_Btn.setStateOn(false);
	hud_dragAtk_btn.setStateOn(false);
	hud_virus_btn.setStateOn(false);
	msgGui.setVisible(false);
	clientClearDisparoMark();
}

function clientConfirmarDisparo(%tipo, %area){
	switch$ (%tipo){
		case "orbital":
			clientAskCanhaoOrbital(%area);
			
		case "geo":
			clientAskGeoDisparo(%area);
		
		case "dragnalAtk":
			clientAskDragnalAtacar(%area);
			
		case "virus":
			clientAskVirusGulok(%area);
	}
}