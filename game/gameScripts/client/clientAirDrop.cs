// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientAirDrop.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 28 de março de 2008 21:15
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskAirDrop(){
	if($jogadorDaVez != $mySelf)
		return;
		
	if($mySelf.AirDrops <= 0)
		return;
	
	if(Foco.getObject(0).onde.terreno !$= "terra")
	{
		clientMsg("airDropSohNaTerra", 3000);
		return;
	}
		
	%custo = clientGetAirDropCusto();	
	if($mySelf.imperiais < %custo)
		return;
		
	%rodadaEmQuePosso = clientGetAirDropRodada();
	if($rodadaAtual < %rodadaEmQuePosso)
		return;
	
	clientPushServerComDot();
	$mySelf.imperiais -= %custo;
	$mySelf.AirDrops--;
	clientSetAirDropBtnPorAirDropsDisponiveis();
	atualizarImperiaisGui();
	commandToServer('airDrop', Foco.getObject(0).onde.getName());	
}

function clientGetAirDropCusto()
{
	if($myPersona.aca_v_5 > 2)
		return 4;	

	return 5;	
}
function clientGetAirDropRodada()
{
	if($myPersona.aca_v_5 > 2)
		return 2;	

	return 3;	
}
function clientSetAirDropBtnPorAirDropsDisponiveis()
{
	if($mySelf.AirDrops > 0)
	{
		airDropHud_btn.setActive(true);	
		return;
	}
	
	airDropHud_btn.setActive(false);	
}


function clientCmdAirDrop(%onde){
	atualizarBotoesDeCompra(); //inativa o botão de airDrop se não houver dinheiro para um segundo
	clientPopServerComDot();
	%nave = nave_BP.createCopy();
	%nave.dono = $jogadorDaVez;
	%nave.viajarIda(%onde);
	
	if(!$noSound)
		alxPlay(airDrop);
}

function nave::createCopy(%this){
      %copy = new t2dStaticSprite(){
         scenegraph = %this.sceneGraph;
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
         WorldLimitMax = %this.WorldLimitMax;
         WorldLimitMin = %this.WorldLimitMin;
         WorldLimitMode = %this.WorldLimitMode;
    };
   %copy.setLayer(0);
   %copy.dono = $jogadorDaVez;
   return %copy;
}


function nave::viajarIda(%this, %areaAlvo){
	%this.myOriginalSizeX = %this.getSizeX();
	//%this.setMyImage(); //isso era pra ter naves de cada cor, mas talvez fique melhor coim todas da mesma cor. 
	%this.areaAlvo = %areaAlvo;
	%speed = 15;
	
	%orient = angleBetween(%this.getPosition(), %areaAlvo.pos0);
	%this.setRotation(%orient);
	
	//turbinas:
	%turbina1 = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%turbina1.loadEffect("~/data/particles/airDropJet.eff");
	%turbina1.setEffectLifeMode("INFINITE");
	%turbina1.mount(%this, 0, -0.5, 0, 1, 0, 0, 0); //o quarto número é 1[bool], quer dizer pra herdar a rotação do objeto-base
	%turbina1.playEffect();
	%this.myTurbina1 = %turbina1;
	
	//turbinas:
	%turbina2 = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%turbina2.loadEffect("~/data/particles/airDropJet.eff");
	%turbina2.setEffectLifeMode("INFINITE");
	%turbina2.mount(%this, 0, 0.5, 0, 1, 0, 0, 0); //o quarto número é 1[bool], quer dizer pra herdar a rotação do objeto-base
	%turbina2.playEffect();
	%this.myTurbina2 = %turbina2;
	
   	// manda o objeto andar, parar quando chegar lá e chamar o callback onPositionTarget:
   	%this.moveTo(%areaAlvo.pos0, %speed, true, true); 
}

function nave::onPositionTarget(%this){
	clientNaveDescerNoTabuleiro(%this);
}

function clientNaveDescerNoTabuleiro(%nave){
	%nave.myTurbina1.setVisible(false);
	%nave.myTurbina2.setVisible(false);
	%mySizeX = %nave.getSizeX();
	%mySizeY = %nave.getSizeY();
	if(%mySizeX > %nave.myOriginalSizeX / 1.5){
		%nave.setSizeX(%mySizeX / 1.1); 
		%nave.setSizeY(%mySizeY / 1.1);
		schedule(200, 0, "clientNaveDescerNoTabuleiro", %nave);
	} else {
		%nave.droparTanques();
		clientNaveSubirDoTabuleiro(%nave);
	}
}
function clientNaveSubirDoTabuleiro(%nave){
	%nave.myTurbina1.setVisible(false);
	%nave.myTurbina2.setVisible(false);
	%mySizeX = %nave.getSizeX();
	%mySizeY = %nave.getSizeY();
	if(%mySizeX < %nave.myOriginalSizeX){
		%nave.setSizeX(%mySizeX * 1.1); 
		%nave.setSizeY(%mySizeY * 1.1);
		schedule(100, 0, "clientNaveSubirDoTabuleiro", %nave);
	} else {
		%nave.myTurbina1.setVisible(true);
		%nave.myTurbina2.setVisible(true);
		%nave.moveToLoc(nave_BP.getPosition());
		schedule(10000, 0, "delNave", %nave);
	}
}

function nave::droparTanques(%this){
	%eval = "%areaAlvo = " @ %this.areaAlvo @ ";";
	eval(%eval);
	echo("DROPANDO TANQUES:: AreaAlvo = " @ %this.areaAlvo);
	
	for(%i = 0; %i < 3; %i++){
		if($novasPecas){
			if($myVideoPrefX > 800){
				%newUnit = tanque_BP_neo.createCopy();
			} else {
				%newUnit = tanque_BP.createCopy();
			}
		} else {
			%newUnit = tanque_BP.createCopy();
		}
		%newUnit.onde = %areaAlvo;
		%newUnit.setPosition(%newUnit.onde.pos0);
		%newUnit.isSelectable = 1;
		%newUnit.isMoveable = 1;
		%newUnit.dono = %this.dono;
		%newUnit.setMyImage();
		%newUnit.dono.mySimUnits.add(%newUnit); //adiciona a unit no simset de unidades do player;	
		
		%newUnit.onde.positionUnit(%newUnit);
		
		%newUnit.onde.resolverMyStatus();
	}
}

function nave::moveToLoc(%this, %worldPos){
	%speed = 15; 

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar e parar quando chegar lá:
   	%this.moveTo(%worldPos, %speed, true); 
}


function nave::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	%myImage = "nave" @ %corDoMeuDono @ "ImageMap";
		
	%this.setImageMap(%myImage);
}

function delNave(%nave){
	%nave.myTurbina1.safeDelete();
	%nave.myTurbina2.safeDelete();
	%nave.safeDelete();	
}