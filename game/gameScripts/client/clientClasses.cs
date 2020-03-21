// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientClasses.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 17 de outubro de 2007 9:53
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  Aki estão as classes do client (são as mesmas no server,
//                    :  mas com gráficos)
//                    :  
// ============================================================
/////////////////////////////////////
$novasPecas = true;
//////
//SOM:
function playSound(%soundName, %variacoes, %alwaysPlay){
	//para tocar um som:
	if($noSound)
		return;
		
	if($mySelf == $jogadorDaVez || %alwaysPlay){
		%result = dado(%variacoes, 0);
		
		%eval = "alxPlay( " @ %soundName @ %result @ " );";
		eval(%eval);
	}	
}




///////////////////////

function base::createCopy(%this){
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
         WorldLimitMax = %this.WorldLimitMax;
         WorldLimitMin = %this.WorldLimitMin;
         WorldLimitMode = %this.WorldLimitMode;
    };
   %copy.setUseMouseEvents(true);
	%copy.setLayer(%this.getLayer());
   return %copy;
}





function base::spawnUnit(%this, %unit){
	echo("base::spawnUnit() => " @ %this.onde SPC %unit);
	if($novasPecas){
		if(%unit $= "tanque" || %unit $= "navio"){
			if($myVideoPrefX > 800){
				%unitBP = %unit @ "_BP_neo";
			} else {
				%unitBP = %unit @ "_BP";
			}
		} else {
			%unitBP = %unit @ "_BP";
		}
	} else {
		%unitBP = %unit @ "_BP";
	}
	%newUnit = %unitBP.createCopy();
	
	
	if($jogoEmDuplas){
		%newUnit.dono = $jogadorDaVez;
	} else {
		%newUnit.dono = %this.dono;
	}
	
	%newUnit.setMyImage();	
	%this.positionUnit(%newUnit);
	%newUnit.isSelectable = 1;
	%newUnit.isMoveable = 1;
	
	%newUnit.dono.mySimUnits.add(%newUnit); //adiciona a unit no simset de unidades do player;
	if(%newUnit.class $= "lider"){
		%newUnit.dono.mySimLideres.add(%newUnit);
	}
	
	if(%newUnit.class $= "navio"){
		playSound("navioNascer", 3); //%soundName, %variacoes	
	} else if (%newUnit.class $= "tanque"){
		playSound("tanqueMover", 5); //%soundName, %variacoes	
	}
	
	return %newUnit;
}

function base::positionUnit(%this, %unit){
	echo("base::positionUnit() => " @ %this.onde SPC %unit.class);
	%unit.onde = %this.onde;
	%areaLocal = %unit.onde;
	%unit.setUseMouseEvents(true);
	
	%unit.setPosition(%areaLocal.pos0); //coloca a unidade no meio da Base
	%areaLocal.positionUnit(%unit); //movimenta ela pra posição correta e marca os dados corretos
	
	if(%this.dono.marinheiro){
		%custoNavio = 2;	
	} else {
		%custoNavio = 3;
	}
	
	//desconta a grana:
	switch$(%unit.class){
		case "soldado":	$jogadorDaVez.imperiais -= 1;
		case "tanque":	$jogadorDaVez.imperiais -= 2;
		case "navio":	$jogadorDaVez.imperiais -= %custoNavio;
	}
	
	if($jogoEmDuplas){
		%areaLocal.resolverMyStatus();	
	}
	
	clientAtualizarPosReservaTxt(%areaLocal); //atualiza o texto das posições reserva
}
	
	
	
	
//testes de mouse over:

function base::onMouseEnter(%this, %modifier, %worldPos){
	if($jogadorDaVez == $mySelf){
		if(!%this.oculta || %this.dono == $mySelf || %this.dono == $mySelf.myDupla){
			$semiSelectionBase.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta nesta base
		}
	}
	if(%this.reciclando){
		$reciclandoTXT.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta o texto de reciclando nesta base
	}
}

function base::onMouseLeave(%this, %modifier, %worldPos)
{
   %eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	
	if($jogadorDaVez.id $= %mySelf.id){
		$semiSelectionBase.dismount(); //dismonta a selectionMark
		$semiSelectionBase.setPosition("-280 -280"); //fora da tela
	}
	if(%this.reciclando){
		$reciclandoTXT.dismount(); //dismonta o txt de reciclando
		$reciclandoTXT.setPosition("-280 -280"); //fora da tela
	}
}




//função para usar imagens em vez de blend:
function base::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	
	if(%this.refinaria){
		%myImage = "refinaria" @ %corDoMeuDono @ "ImageMap";
	} else {
		%myImage = "base" @ %corDoMeuDono @ "ImageMap";
		//FX:
		if(!%this.dono.oculto || %this.dono == $mySelf){
			%criarBaseFX = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
			%criarBaseFX.loadEffect("~/data/particles/base" @ %corDoMeuDono @ "FX.eff");
			%criarBaseFX.mount(%this);
			%criarBaseFX.setEffectLifeMode("KILL", 2);
			%criarBaseFX.playEffect();	
		}
	}
	
	
	%this.setImageMap(%myImage);
}


//hud:
function base::callMyHud(%this){
	clientZerarUnitHud();
	unitHud.setVisible(true);
	
	if(%this.refinaria){
		unitHud.bitmap = "~/data/images/refinariaHud";
	} else {
		unitHud.bitmap = "~/data/images/baseHud";
	}
}


////////////////////////////////////////////////
//Soldado:

function Soldado::createCopy(%this){
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
		 
		 dado = 6;
		 embarcavel = true;
    };
   %copy.setLayer(%this.getLayer());
   return %copy;
}


function Soldado::explodir(%this){
	%explosao = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%explosao.loadEffect("~/data/particles/enemyExplosion.eff");
	%explosao.setEffectLifeMode("KILL", 1);
    %explosao.setPosition(%this.getPosition());
    %explosao.playEffect();
         
    //para tocar um som:
	if(!$noSound){
		alxPlay( explosao5 );
	}
}	


function Soldado::moveToLoc(%this, %worldPos){
	%speed = 25; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}



function Soldado::kill(%this){
	%this.explodir();
	clientRemoverUnidade(%this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	if(Foco.getObject(0) == %this){
		resetSelection();
	}
	%this.safeDelete();
}

/////////////Render:
function Soldado::render(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	clientRemoverUnidade(%this, %this.onde);
	resetSelection();
	%this.safeDelete();
}
///////////////


	
	
	
function soldado::fire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroSoldado = soldadoTiro;
	%projetil = %tiroSoldado.createCopy();
	
	%orient = angleBetween(%this.getPosition(), %inimigo.getPosition());
	%this.setRotation(%orient);
		
	universalCreateShot(%this, %inimigo, %projetil, "soldierShot1", "", %result);
}

function soldado::missFire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroSoldado = soldadoTiro;
	%projetil = %tiroSoldado.createCopy();
	
	%inimigoMissPos = clientFindMissPos(%inimigo);
	
	%orient = angleBetween(%this.getPosition(), %inimigoMissPos);
	%this.setRotation(%orient);
		
	universalCreateShot(%this, %inimigo, %projetil, "soldierShot1", "", %result, true, %inimigoMissPos);
}




//testes de mouse over:

function soldado::onMouseEnter(%this, %modifier, %worldPos){
	if($jogadorDaVez.id $= $mySelf.id){
		$semiSelectionSoldado.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta neste truta
	}
}

function soldado::onMouseLeave(%this, %modifier, %worldPos){
 	if($jogadorDaVez.id $= $mySelf.id){
		$semiSelectionSoldado.dismount(); //dismonta a selectionMark
		$semiSelectionSoldado.setPosition("-280 -280"); //fora da tela
	}
}



//função para usar imagens em vez de blend:
function Soldado::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	
	%myImage = "soldado" @ %corDoMeuDono @ "ImageMap";
			
	%this.setImageMap(%myImage);
}


//Movimentação:
function Soldado::moverPara(%this, %area, %pos){
	%eval = "%areaPos = " @ %area @ "." @ %pos @ ";";
	eval(%eval);
	
	%this.action("moveToLoc", %areaPos);
	%this.pos = %pos;
	
	if(%pos $= "pos1" || %pos $= "pos2"){
		%eval = "%area." @ %pos @ "Flag = %this.class;";
		eval(%eval);
		%eval = "%area." @ %pos @ "Quem = %this;";
		eval(%eval);
	} else {
		%eval = "%area.my" @ %pos @ "List.add(%this);";
		eval(%eval);
	}
}


function soldado::callMyHud(%this){
	clientZerarUnitHud();
	unitHud.setVisible(true);
	
	unitHud.bitmap = "~/data/images/soldadoHud";
	unitHudDefesa_txt.setVisible(true);
	unitHudAtaque_txt.setVisible(true);
	
	%bonusDef = $mySelf.moralDefesa + ($myPersona.aca_av_1 * 2);
	%bonusAtk = $mySelf.moralAtaque + ($myPersona.aca_av_2 * 2);
		
	unitHudDefesa_txt.text = ($myPersona.aca_s_d_min + $mySelf.moralDefesa) @ "  a  " @ ($myPersona.aca_s_d_max + %bonusDef);
	unitHudAtaque_txt.text = ($myPersona.aca_s_a_min + $mySelf.moralAtaque) @ "  a  " @ ($myPersona.aca_s_a_max + %bonusAtk);
}


//////////////////////////////
//////////////////////////////////////////////////
//Tanque:

function tanque::createCopy(%this){
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
         WorldLimitMax = %this.WorldLimitMax;
         WorldLimitMin = %this.WorldLimitMin;
         WorldLimitMode = %this.WorldLimitMode;
		
		
		 dado = 12;
    };
   %copy.setLayer(%this.getLayer());
   return %copy;
}

function tanque::explodir(%this){
	%explosao = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%explosao.loadEffect("~/data/particles/enemyExplosion2.eff");
	%explosao.setEffectLifeMode("KILL", 1);
    %explosao.setPosition(%this.getPosition());
    %explosao.playEffect();
	
	clientCameraShake(1, 0.5);
         
    //para tocar um som:
	if(!$noSound){
		%result = dado(5,0);
		
		switch$ (%result){
			case "1":
			alxPlay( explosao4 );
			
			case "2":
			alxPlay( explosao6 );
			
			case "3":
			alxPlay( explosao7 );
			
			case "4":
			alxPlay( explosao8 );
			
			case "5":
			alxPlay( explosao9 );
		}
		
	}
}	
	
	




function Tanque::moveToLoc(%this, %worldPos){
	%speed = 30; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	//echo("Tanque Orient: " @ %orient);
	%this.setRotation(%orient);
	if($novasPecas){
		%this.setMyOrientImage(%orient);
	}

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}

function Tanque::setMyOrientImage(%this, %orient){
	if(%orient >= -112.5 && %orient <= -67.5){
		%myFrame = 0;
	} else if(%orient >= -67.5 && %orient <= -22.5){
		%myFrame = 1;
	} else if(%orient >= -22.5 && %orient <= 22.5){
		%myFrame = 2;
	} else if(%orient >= 22.5 && %orient <= 67.5){
		%myFrame = 3;
	} else if(%orient >= 67.5 || %orient <= -247.5){
		%myFrame = 4;
	} else if(%orient >= -247.5 && %orient <= -202.5){
		%myFrame = 5;
	} else if(%orient >= -202.5 && %orient <= -157.5){
		%myFrame = 6;
	} else if(%orient >= -157.5 && %orient <= -112.5){
		%myFrame = 7;
	}
	%this.setFrame(%myFrame);
	echo("Frame Selected = " @ %myFrame);
}

function tanque::kill(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	clientRemoverUnidade(%this, %this.onde);
	if(Foco.getObject(0) == %this){
		resetSelection();
	}
	%this.safeDelete();
}

///////////Render:
function tanque::render(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	clientRemoverUnidade(%this, %this.onde);
	resetSelection();
	%this.safeDelete();
}

///////////


function tanque::fire(%this, %inimigo, %result){
    // Criar o projétil:
	%tiroTanque = tanqueTiro;
	%projetil = %tiroTanque.createCopy();
	
	%orient = angleBetween(%this.getPosition(), %inimigo.getPosition());
	%this.setRotation(%orient);
		
	universalCreateShot(%this, %inimigo, %projetil, "tankShot1", "", %result);
}

function tanque::missFire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroTanque = tanqueTiro;
	%projetil = %tiroTanque.createCopy();
	
	%inimigoMissPos = clientFindMissPos(%inimigo);
	
	%orient = angleBetween(%this.getPosition(), %inimigoMissPos);
	%this.setRotation(%orient);
		
	universalCreateShot(%this, %inimigo, %projetil, "tankShot1", "", %result, true, %inimigoMissPos);
}
//testes de mouse over:

function tanque::onMouseEnter(%this, %modifier, %worldPos){
	if($jogadorDaVez.id $= $mySelf.id){
		$semiSelectionTanque.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta neste truta
	}
}

function tanque::onMouseLeave(%this, %modifier, %worldPos){
	if($jogadorDaVez.id $= $mySelf.id){
		$semiSelectionTanque.dismount(); //dismonta a selectionMark
		$semiSelectionTanque.setPosition("-280 -280"); //fora da tela
	}
}




//função para usar imagens em vez de blend:
function tanque::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	if($myVideoPrefX < 1024){
		%myImage = "tanque" @ %corDoMeuDono @ "SMImageMap";
	} else {
		if($novasPecas){
			%myImage = "tanque" @ %corDoMeuDono @ "AnimImageMap";
		} else {
			%myImage = "tanque" @ %corDoMeuDono @ "ImageMap";
		}
	}
	
	%this.setImageMap(%myImage);
}


//Movimentação:
function tanque::moverPara(%this, %area, %pos){
	//pega a posição da área:
	%eval = "%areaPos = " @ %area @ "." @ %pos @ ";";
	eval(%eval);
	
	//move a peça:
	%this.action("moveToLoc", %areaPos);
	%this.pos = %pos; //marca na peça onde ela está
	
	//marca na área a nova unidade:
	if(%pos $= "pos1" || %pos $= "pos2"){
		%eval = "%area." @ %pos @ "Flag = %this.class;";
		eval(%eval);
		%eval = "%area." @ %pos @ "Quem = %this;";
		eval(%eval);
	} else {
		%eval = "%area.my" @ %pos @ "List.add(%this);";
		eval(%eval);
	}
}


function tanque::callMyHud(%this){
	clientZerarUnitHud();
	unitHud.setVisible(true);
	
	unitHud.bitmap = "~/data/images/tanqueHud";
	unitHudDefesa_txt.setVisible(true);
	unitHudAtaque_txt.setVisible(true);
	
	%bonusDef = $mySelf.moralDefesa + ($myPersona.aca_av_1 * 2);
	%bonusAtk = $mySelf.moralAtaque + ($myPersona.aca_av_2 * 2);
	
	unitHudDefesa_txt.text = ($myPersona.aca_t_d_min + $mySelf.moralDefesa) @ "  a  " @ ($myPersona.aca_t_d_max + %bonusDef);
	unitHudAtaque_txt.text = ($myPersona.aca_t_a_min + $mySelf.moralAtaque) @ "  a  " @ ($myPersona.aca_t_a_max + %bonusAtk);
}



//////////////////////////////////////////
////////////////////////////////////////////////
//Navio:
function navio::createCopy(%this){
      %copy = new t2dStaticSprite(){
         scenegraph = $strategyScene;
         class = %this.class;   
         imageMap = %this.imageMap;
         size = %this.size;
		 rotation = %this.rotation;
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
		
		
		
		 dado = 12;
		 transporte = true;
		
    };
   
   %copy.setLayer(%this.getLayer());
   return %copy;
}


function navio::explodir(%this){
	%explosao = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%explosao.loadEffect("~/data/particles/customExplosion.eff");
	%explosao.setEffectLifeMode("KILL", 1);
    %explosao.setPosition(%this.getPosition());
    %explosao.playEffect();
         
	clientCameraShake(1.5, 0.5);
	
    //para tocar um som:
	if(!$noSound){
		alxPlay( explosao3 );
	}
}	


function navio::moveToLoc(%this, %worldPos){
	%speed = 25;
	
	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);
	if($novasPecas){
		%this.setMyOrientImage(%orient);
	}

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}

function navio::setMyOrientImage(%this, %orient){
	if(%orient >= -112.5 && %orient <= -67.5){
		%myFrame = 0;
	} else if(%orient >= -67.5 && %orient <= -22.5){
		%myFrame = 1;
	} else if(%orient >= -22.5 && %orient <= 22.5){
		%myFrame = 2;
	} else if(%orient >= 22.5 && %orient <= 67.5){
		%myFrame = 3;
	} else if(%orient >= 67.5 || %orient <= -247.5){
		%myFrame = 4;
	} else if(%orient >= -247.5 && %orient <= -202.5){
		%myFrame = 5;
	} else if(%orient >= -202.5 && %orient <= -157.5){
		%myFrame = 6;
	} else if(%orient >= -157.5 && %orient <= -112.5){
		%myFrame = 7;
	}
	%this.setFrame(%myFrame);
	echo("Frame Selected = " @ %myFrame);
}


function navio::kill(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	if(isObject(%this.myTransporte)){
		for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
			%this.myTransporte.getObject(%i).render();	
		}
	}
	clientRemoverUnidade(%this, %this.onde);
	if(Foco.getObject(0) == %this){
		resetSelection();
	}
	%this.safeDelete();
}

///////////////Render
function navio::render(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	clientRemoverUnidade(%this, %this.onde);
	resetSelection();
	%this.safeDelete();
}
///////////



function navio::fire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroNavio = navioTiro;
	%projetil = %tiroNavio.createCopy();
	
	//%orient = angleBetween(%this.getPosition(), %inimigo.getPosition());
	//%this.setRotation(%orient);	
		
	universalCreateShot(%this, %inimigo, %projetil, "torpedo", "", %result);	
}

function navio::missFire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroNavio = navioTiro;
	%projetil = %tiroNavio.createCopy();
	
	%inimigoMissPos = clientFindMissPos(%inimigo);
	
	%orient = angleBetween(%this.getPosition(), %inimigoMissPos);
	%this.setRotation(%orient);
		
	universalCreateShot(%this, %inimigo, %projetil, "torpedo", "", %result, true, %inimigoMissPos);
}


function navio::mark(%this, %mark){
	%eval = "%embarcante = %this.myTransporte.getObject(" @ %mark - 1 @ ");";
	eval(%eval);
	%eval = "%newMark = embarque" @ %mark @ ".createCopy();";
	eval(%eval);
	
	%newMark.mount(%this);
	%newMark.setBlendColor(%embarcante.dono.corR, %embarcante.dono.corG, %embarcante.dono.corB, %embarcante.dono.corA);
	
	%eval = "%this.myMark" @ %mark @ " = %newMark;";
	eval(%eval);
}

function navio::unMark(%this){
	for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
		%eval = "%myMark = %this.myMark" @ %i+1 @ ";";
		eval(%eval);
		%myMark.dismount();
		%myMark.safeDelete();
		%myMark = "nada";
	}
}

function navio::reMark(%this){
	for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
		%eval = "%embarcante = %this.myTransporte.getObject(" @ %i @ ");";
		eval(%eval);
		%eval = "%newMark = embarque" @ %i+1 @ ".createCopy();";
		eval(%eval);
		
		%newMark.mount(%this);
		%newMark.setBlendColor(%embarcante.dono.corR, %embarcante.dono.corG, %embarcante.dono.corB, %embarcante.dono.corA);
		
		%eval = "%this.myMark" @ %i+1 @ " = %newMark;";
		eval(%eval);
	}
}



function navio::onMouseEnter(%this, %modifier, %worldPos){
	if($jogadorDaVez.id $= $mySelf.id){
		$semiSelectionNavio.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta neste truta
	}
}

function navio::onMouseLeave(%this, %modifier, %worldPos){
	if($jogadorDaVez.id $= $mySelf.id){
		$semiSelectionNavio.dismount(); //dismonta a selectionMark
		$semiSelectionNavio.setPosition("-280 -280"); //fora da tela
	}
}




//função para usar imagens em vez de blend:
function navio::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	if($myVideoPrefX < 1024){
		%myImage = "navio" @ %corDoMeuDono @ "SMImageMap";
	} else {
		if($novasPecas){
			%myImage = "navio" @ %corDoMeuDono @ "AnimImageMap";
		} else {
			%myImage = "navio" @ %corDoMeuDono @ "ImageMap";
		}
	}
	
	%this.setImageMap(%myImage);
}



function navio::cruzarToLoc(%this, %worldPos){
	%speed = 25;
	
	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);
	if($novasPecas){
		%this.setMyOrientImage(%orient);
	}

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true, true); 
}

function navio::onPositionTarget(%this){
	%this.setPosition(%this.cruzarTarget.transPos);
	%this.moveToLoc(%this.targetPos);
	%this.cruzarTarget = -1;
	%this.targetPos = -1;
}

//função para manter o navio selecionado quando é um navio que está na pos 3 e cruza o oceano:
function navio::reselectMySelf(%this){
	if (%this.dono == $jogadorDaVez){
		echo("Reselecting");
		Foco.getObject(0).setLayer(1); //devolve o antigo foco pra layer comum das peças;
		Foco.clear(); //limpa o Foco (só pode ter uma coisa em foco por vez)
		Foco.add(%this); //atualiza o Foco
		%myMark = getMyMark(%this); //pega a marca de seleção conforme o tipo de unidade
		%myMark.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta no foco
		%myMark.setAutoRotation(50);
		clientSingleSelection(); //chama esta função no arquivo mainGui.cs, colocando o novo foco em prmeiro plano	
	}	
}


//Movimentação:
function navio::moverPara(%this, %area, %pos){
	%eval = "%areaPos = " @ %area @ "." @ %pos @ ";";
	eval(%eval);
		
	if(!isObject(%this.areaAntiga.myCruzarFronteiras)){
		%this.action("moveToLoc", %areaPos);
	} else {
		%cruzar = %this.areaAntiga.myCruzarFronteiras.isMember(%area);
		if(%cruzar){
			%this.cruzarTarget = %area;
			%this.targetPos = %areaPos;
			%this.action("cruzarToLoc", %this.areaAntiga.transPos);
			%this.reselectMySelf();
		} else {
			%this.action("moveToLoc", %areaPos);	
		}
		$cruzarDe = "nada";
	}
	
	%this.pos = %pos;
	
	if(%pos $= "pos1" || %pos $= "pos2"){
		%eval = "%area." @ %pos @ "Flag = %this.class;";
		eval(%eval);
		%eval = "%area." @ %pos @ "Quem = %this;";
		eval(%eval);
	} else {
		%eval = "%area.my" @ %pos @ "List.add(%this);";
		eval(%eval);
	}
}


function navio::callMyHud(%this){
	clientZerarUnitHud();
	unitHud.setVisible(true);
	
	unitHud.bitmap = "~/data/images/navioHud";
	unitHudDefesa_txt.setVisible(true);
	unitHudAtaque_txt.setVisible(true);
	
	%bonusDef = $mySelf.moralDefesa + ($myPersona.aca_av_1 * 2);
	%bonusAtk = $mySelf.moralAtaque + ($myPersona.aca_av_2 * 2);
	
	unitHudDefesa_txt.text = ($myPersona.aca_n_d_min + $mySelf.moralDefesa) @ "  a  " @ ($myPersona.aca_n_d_max + %bonusDef);
	unitHudAtaque_txt.text = ($myPersona.aca_n_a_min + $mySelf.moralAtaque) @ "  a  " @ ($myPersona.aca_n_a_max + %bonusAtk);
}


//////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
////////////////////////////////////////
//ExplMarkers:
//testes de mouse over:
function explMarker::onMouseEnter(%this, %modifier, %worldPos){
	%this.setFrame(1);
}

function explMarker::onMouseLeave(%this, %modifier, %worldPos){
	%this.setFrame(0);
}

function explMarker::createCopy(%this){
      %copy = new t2dStaticSprite(){
         scenegraph = $strategyScene;
         class = %this.class;   
         imageMap = %this.imageMap;
         size = %this.size;
         position = %this.position;
		 layer = %this.layer;
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
   %copy.setUseMouseEvents(true);
   %copy.tipo = %this.tipo;
   return %copy;
}

///////////////
//Markers das missões:
//mouse over:
function mMarker::onMouseEnter(%this, %modifier, %worldPos){
	%this.myOldBlendColor = %this.getBlendColor();
	%this.setBlendColor(1, 1, 1, 0.5);
}

function mMarker::onMouseLeave(%this, %modifier, %worldPos){
	%r = getWord(%this.myOldBlendColor, 0);
	%g = getWord(%this.myOldBlendColor, 1);
	%b = getWord(%this.myOldBlendColor, 2);
	%a = getWord(%this.myOldBlendColor, 3);
	
	%this.setBlendColor(%r, %g, %b, %a);
}

function mMarker::createCopy(%this){
      %copy = new t2dStaticSprite(){
         scenegraph = $strategyScene;
         class = %this.class;   
         imageMap = %this.imageMap;
         size = %this.size;
         position = %this.position;
		 layer = %this.layer;
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
   %copy.setUseMouseEvents(true);
   return %copy;
}



////////////////////////////////////
/////
//Markers dos navios:
function marker::createCopy(%this){
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
         WorldLimitMax = %this.WorldLimitMax;
         WorldLimitMin = %this.WorldLimitMin;
         WorldLimitMode = %this.WorldLimitMode;
		 
    };
   
   return %copy;
}


////////////////////////////////////////////////
//////////////////////////////
//Líder:
function lider::createCopy(%this){
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
		 
		 dado = 12;
		 embarcavel = true;
    };
	%copy.setLayer(%this.getLayer());
    return %copy;
}


function lider::explodir(%this){
	%explosao = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%explosao.loadEffect("~/data/particles/liderExplosion.eff");
	%explosao.setEffectLifeMode("KILL", 1);
    %explosao.setPosition(%this.getPosition());
    %explosao.playEffect();
        
	clientCameraShake(1.5, 0.7);		

    //para tocar um som:
	if(!$noSound){
		alxPlay( explosao1 );
	}
}	


function lider::moveToLoc(%this, %worldPos){
	%speed = 25; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}


function lider::kill(%this){
	if(%this.escudoOn){
		%explosao = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
		%explosao.loadEffect("~/data/particles/escudoGenericoExp.eff"); //aki poderia ter um efeito especial para cada cor. (faíscas seriam loucas)!
		%explosao.setEffectLifeMode("KILL", 1);
		%explosao.setPosition(%this.getPosition());
		%explosao.playEffect();
				
		if(%this.myEscudos < 2){
			%this.myEscudo.safeDelete();
			%this.myEscudos = 0;	
			%this.escudoOn = false;
			//som da perda do escudo:
			alxPlay( escudoPerdido );	
		} else {
			%this.myEscudos -= 1;
			//som do escudo danificado:
			//alxPlay( somDaExplosao );	
		}
		clientFinalizarAtaque(); //isso tem que ter pq esta função é chamada no area.resolverMyStatus(), que não é chamada aki!
		clientFinalizarAiAtaque();
	} else {
		%eval = "%donoDoMorto = $" @ %this.dono.id @ ";";
		eval(%eval);
		%eval = "%donoDoMortoAreas = $"@%donoDoMorto.id@"Areas;";
		eval(%eval);
		
		%this.explodir();
		%this.dono.mySimUnits.remove(%this);
		%this.dono.mySimLideres.remove(%this);	
		clientRemoverUnidade(%this, %this.onde);
		if(%this.dono == $mySelf){
			clientVerificarMoral();
		}
		if(Foco.getObject(0) == %this){
			resetSelection();
		}
		%this.safeDelete();
		//clientMsgSeuLiderMorreu();
	}
}

/////////////Render:
function lider::render(%this){
	%eval = "%donoDoMorto = $" @ %this.dono.id @ ";";
	eval(%eval);
	%eval = "%donoDoMortoAreas = $"@%donoDoMorto.id@"Areas;";
	eval(%eval);
	
	%this.explodir();
	if(%this.escudoOn){
		%this.myEscudo.safeDelete();	
	}
	%this.dono.mySimUnits.remove(%this);
	%this.dono.mySimLideres.remove(%this);	
	if(%this.dono == $mySelf){
		clientVerificarMoral();
	}
	clientRemoverUnidade(%this, %this.onde);
	resetSelection();
	%this.safeDelete();
}
///////////////


	
	
	
function lider::fire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroLider = soldadoTiro;
	%projetil = %tiroLider.createCopy();
	
	%orient = angleBetween(%this.getPosition(), %inimigo.getPosition());
	%this.setRotation(%orient);	
		
	universalCreateShot(%this, %inimigo, %projetil, "liderShot", "", %result);	
}

function lider::sniperFire(%this, %inimigo){
  	// Criar o projétil:
	%tiroLider = soldadoTiro;
	%projetil = %tiroLider.createCopy();
	
	universalCreateShot(%this, %inimigo, %projetil, "liderSniperShot", "");
}

function lider::missFire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroLider = soldadoTiro;
	%projetil = %tiroLider.createCopy();
	
	%inimigoMissPos = clientFindMissPos(%inimigo);
	
	%orient = angleBetween(%this.getPosition(), %inimigoMissPos);
	%this.setRotation(%orient);
		
	universalCreateShot(%this, %inimigo, %projetil, "liderShot", "", %result, true, %inimigoMissPos);
}



//testes de mouse over:

function lider::onMouseEnter(%this, %modifier, %worldPos){
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	
	if($jogadorDaVez.id $= %mySelf.id){
		$semiSelectionSoldado.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta neste truta
	}
}

function lider::onMouseLeave(%this, %modifier, %worldPos)
{
   %eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	
	if($jogadorDaVez.id $= %mySelf.id){
		$semiSelectionSoldado.dismount(); //dismonta a selectionMark
		$semiSelectionSoldado.setPosition("-280 -280"); //fora da tela
	}
}



//função para usar imagens em vez de blend:
function lider::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	%myImage = "lider" @ %this.liderNum @ %corDoMeuDono @ "ImageMap";	
	%this.setImageMap(%myImage);
}



//criar escudo:
function lider::criarMeuEscudo(%this){
	%escudo = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%nomeDoEscudo = "~/data/particles/liderShield" @ %this.dono.myColor @ ".eff";
	%escudo.loadEffect(%nomeDoEscudo);
	%escudo.setLayer(%this.getLayer());
    %escudo.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta o escudo no líder
    %escudo.playEffect();
	
	%this.myEscudo = %escudo;
	%this.escudoOn = true;
}


function lider::callMyHud(%this){
	clientZerarUnitHud();
	unitHud.setVisible(true);
	
	unitHud.bitmap = "~/data/images/lider" @ %this.liderNum @ "Hud";
	unitHudDefesaL_txt.setVisible(true);
	unitHudAtaqueL_txt.setVisible(true);
	
	%eval = "%defMin = $myPersona.aca_ldr_" @ %this.liderNum @ "_d_min;";
	eval(%eval);
	%eval = "%defMax = $myPersona.aca_ldr_" @ %this.liderNum @ "_d_max;";
	eval(%eval);
	%eval = "%atkMin = $myPersona.aca_ldr_" @ %this.liderNum @ "_a_min;";
	eval(%eval);
	%eval = "%atkMax = $myPersona.aca_ldr_" @ %this.liderNum @ "_a_max;";
	eval(%eval);
	%eval = "%h1 = $myPersona.aca_ldr_" @ %this.liderNum @ "_h1;";
	eval(%eval);
	%eval = "%h2 = $myPersona.aca_ldr_" @ %this.liderNum @ "_h2;";
	eval(%eval);
	%eval = "%h3 = $myPersona.aca_ldr_" @ %this.liderNum @ "_h3;";
	eval(%eval);
	%eval = "%h4 = $myPersona.aca_ldr_" @ %this.liderNum @ "_h4;";
	eval(%eval);
	
	%bonusDef = $mySelf.moralDefesa + ($myPersona.aca_av_1 * 2);
	%bonusAtk = $mySelf.moralAtaque + ($myPersona.aca_av_2 * 2);
	
	unitHudDefesaL_txt.text = (%defMin + $mySelf.moralDefesa) @ "  a  " @ (%defMax + %bonusDef);
	unitHudAtaqueL_txt.text = (%atkMin + $mySelf.moralAtaque) @ "  a  " @ (%atkMax + %bonusAtk);
		
	if(%h1 > 0){
		escHudMicon.setVisible(true);
		escHudMicon.bitmap = "~/data/images/academia/amicon_escudo" @ %h1;
	}
	if(%h2 > 0){
		jpkHudMicon.setVisible(true);
		jpkHudMicon.bitmap = "~/data/images/academia/amicon_jetPack" @ %h2;
	}
	if(%h3 > 0){
		snpHudMicon.setVisible(true);
		snpHudMicon.bitmap = "~/data/images/academia/amicon_sniper" @ %h3;
	}
	if(%h4 > 0){
		mrlHudMicon.setVisible(true);
		mrlHudMicon.bitmap = "~/data/images/academia/amicon_moral" @ %h4;
	}
	
}


////////////////
//lider cruzando oceanos:
function lider::cruzarToLoc(%this, %worldPos){
	%speed = 25;
	
	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);


   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true, true); 
}

function lider::onPositionTarget(%this){
	%this.setPosition(%this.cruzarTarget.transPos);
	%this.moveToLoc(%this.targetPos);
	%this.cruzarTarget = -1;
	%this.targetPos = -1;
}

//função para manter o lider selecionado quando é um lider que está na pos 3 e cruza o oceano:
function lider::reselectMySelf(%this){
	if (%this.dono == $jogadorDaVez){
		echo("Reselecting");
		Foco.getObject(0).setLayer(1); //devolve o antigo foco pra layer comum das peças;
		Foco.clear(); //limpa o Foco (só pode ter uma coisa em foco por vez)
		Foco.add(%this); //atualiza o Foco
		%myMark = getMyMark(%this); //pega a marca de seleção conforme o tipo de unidade
		%myMark.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta no foco
		%myMark.setAutoRotation(50);
		clientSingleSelection(); //chama esta função no arquivo mainGui.cs, colocando o novo foco em prmeiro plano	
	}	
}


//Movimentação:
function lider::moverPara(%this, %area, %pos){
	%eval = "%areaPos = " @ %area @ "." @ %pos @ ";";
	eval(%eval);
		
	if(!isObject(%this.areaAntiga.myCruzarFronteiras)){
		%this.action("moveToLoc", %areaPos);
	} else {
		%cruzar = %this.areaAntiga.myCruzarFronteiras.isMember(%area);
		if(%cruzar){
			%this.cruzarTarget = %area;
			%this.targetPos = %areaPos;
			%this.action("cruzarToLoc", %this.areaAntiga.transPos);
			%this.reselectMySelf();
		} else {
			%this.action("moveToLoc", %areaPos);	
		}
		$cruzarDe = "nada";
	}
	
	%this.pos = %pos;
	
	if(%pos $= "pos1" || %pos $= "pos2"){
		%eval = "%area." @ %pos @ "Flag = %this.class;";
		eval(%eval);
		%eval = "%area." @ %pos @ "Quem = %this;";
		eval(%eval);
	} else {
		%eval = "%area.my" @ %pos @ "List.add(%this);";
		eval(%eval);
	}
}




//=================================
//GULOK:

function rainha::createCopy(%this){
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
         WorldLimitMax = %this.WorldLimitMax;
         WorldLimitMin = %this.WorldLimitMin;
         WorldLimitMode = %this.WorldLimitMode;
		 dado = 15;
    };
   %copy.setUseMouseEvents(true);
	%copy.setLayer(%this.getLayer());
   return %copy;
}

function rainha::spawnUnit(%this, %unit){
	echo("rainha::spawnUnit() => " @ %this.onde SPC %unit);
	%unitBP = %unit @ "_BP";
	%newUnit = %unitBP.createCopy();
	%newUnit.gulok = true;
	
	if(%newUnit.class $= "cefalok"){
		%explosao = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
		%explosao.loadEffect("~/data/particles/ovoEclodir" @ %this.dono.myColor @ ".eff");
		%explosao.setEffectLifeMode("KILL", 1);
		%explosao.mount(%newUnit, 0, 0, 0, 0, 0, 0, 0);
		%explosao.playEffect();	
	}
	
	%this.positionUnit(%newUnit);
	%newUnit.isSelectable = 1;
	if(%newUnit.class !$= "ovo"){
		%newUnit.isMoveable = 1;
	} else {
		%newUnit.isMoveable = 0;
	}
	if($jogoEmDuplas){
		%newUnit.dono = $jogadorDaVez;
		%this.onde.resolverMyStatus();	
	} else {
		%newUnit.dono = %this.dono;
	}
	%newUnit.setMyImage();
	%newUnit.dono.mySimUnits.add(%newUnit); //adiciona a unit no simset de unidades do player;
	if(%newUnit.class $= "zangao"){
		%newUnit.dono.mySimLideres.add(%newUnit);
	} else if(%newUnit.class $= "ovo"){
		%newUnit.dono.mySimOvos.add(%newUnit);
	} else if(%newUnit.class $= "cefalok"){
		playSound("cefalok_nascer", 3); //%soundName, %variacoes	
	}
	
	return %newUnit;
}

function rainha::positionUnit(%this, %unit){
	echo("base::positionUnit() => " @ %this.onde SPC %unit.class);
	%unit.onde = %this.onde;
	%areaLocal = %unit.onde;
	%unit.setUseMouseEvents(true);
	
	if(%unit.class !$= "ovo"){
		%unit.setPosition(%areaLocal.pos0); //coloca a unidade no meio da Base
	} else {
		%unit.setPosition(%areaLocal.pos4);
		//chama o FX de criação de ovo:
		%explosao = new t2dParticleEffect(){scenegraph = %unit.scenegraph;};
		%explosao.loadEffect("~/data/particles/ovoBotado.eff");
		%explosao.setEffectLifeMode("KILL", 1);
		%explosao.setPosition(%unit.getPosition());
		%explosao.playEffect();
		
	}
	%areaLocal.positionUnit(%unit); //movimenta ela pra posição correta e marca os dados corretos
	
	//desconta a grana:
	if(%this.cortejada || %this.matriarca){
		%custoOvo = 2;	
	} else {
		%custoOvo = 3;	
	}
	if(%this.dono != $aiPlayer){
		switch$(%unit.class){
			case "ovo":	$jogadorDaVez.imperiais -= %custoOvo;
			case "cefalok":	$jogadorDaVez.imperiais -= 4;
		}
	}
	
	clientAtualizarPosReservaTxt(%areaLocal); //atualiza o texto das posições reserva
}

function rainha::parirVarios(%this)
{
	if(%this.onde.terreno $= "terra"){
		for(%i = 0; %i < 3; %i++){
			%this.spawnUnit("ovo");	
			%this.dono.imperiais += 3;	
		}
	} else {
		for(%i = 0; %i < 2; %i++){
			%this.spawnUnit("cefalok");	
			%this.dono.imperiais += 4;	
		}
	}
	
}
		
function rainha::onMouseEnter(%this, %modifier, %worldPos){
	if($jogadorDaVez == $mySelf){
		$semiSelectionBase.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta nesta base
	}
}

function rainha::onMouseLeave(%this, %modifier, %worldPos){
  	if($jogadorDaVez == $mySelf){
		$semiSelectionBase.dismount(); //dismonta a selectionMark
		$semiSelectionBase.setPosition("-280 -280"); //fora da tela
	}
}

function rainha::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
		
	if(%this.matriarca){
		if(%this.grandeMatriarca){
			%myImage = "grandeMatriarca" @ %corDoMeuDono @ "ImageMap";
		} else {
			%myImage = "matriarca" @ %corDoMeuDono @ "ImageMap";
		}
	} else if(%this.crisalida){
		%myImage = "crisalida" @ %corDoMeuDono @ "ImageMap";
	} else {
		%myImage = "rainha" @ %corDoMeuDono @ "ImageMap";
	}
		
	%this.setImageMap(%myImage);
}

//hud:
function rainha::callMyHud(%this){
	clientZerarUnitHud();
	unitHud.setVisible(true);
	
	if(%this.matriarca){
		unitHud.bitmap = "~/data/images/matriarcaHud";
	} else if(%this.mineradora){
		unitHud.bitmap = "~/data/images/mineradoraHud";
	} else {
		unitHud.bitmap = "~/data/images/rainhaHud";
	}
}

function rainha::moveToLoc(%this, %worldPos){
	%speed = 30; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}



function rainha::kill(%this){
	if(%this.myTransporte.getcount() > 0){
		%this.unMark();
		%verme = %this.myTransporte.getObject(0);
		%verme.dismount();
		%this.myTransporte.remove(%verme);
		%verme.explodir();
		%this.reMark();
		clientFinalizarAtaque();
		clientFinalizarAiAtaque();
	} else {
		%this.explodir();
		clientRemoverUnidade(%this, %this.onde);
		%this.dono.mySimUnits.remove(%this);
		if(Foco.getObject(0) == %this){
			resetSelection();
		}
		%this.safeDelete();
		clientAtualizarEstatisticas();
	}
}

function rainha::render(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	clientRemoverUnidade(%this, %this.onde);
	resetSelection();
	%this.safeDelete();
}
	
function rainha::fire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiro = soldadoTiro;
	%projetil = %tiro.createCopy();
	
	if(!%this.grandeMatriarca){
		%orient = angleBetween(%this.getPosition(), %inimigo.getPosition());
		%this.setRotation(%orient);	
	}
		
	universalCreateShot(%this, %inimigo, %projetil, "rainhaShot", "", %result);
}

function rainha::missFire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroLider = soldadoTiro;
	%projetil = %tiroLider.createCopy();
	
	%inimigoMissPos = clientFindMissPos(%inimigo);
	
	if(!%this.grandeMatriarca){
		%orient = angleBetween(%this.getPosition(), %inimigoMissPos);
		%this.setRotation(%orient);	
	}
		
	universalCreateShot(%this, %inimigo, %projetil, "rainhaShot", "", %result, true, %inimigoMissPos);
}

function rainha::explodir(%this){
	explosaoGulok(%this);
    
	if(%this.matriarca){
		playSound("matriarca_morrer", 3, true);
	} else {
		playSound("rainha_morrer", 3, true);
	}
		
	clientCameraShake(1.5, 1);
      //para tocar um som:
   //alxPlay( somDaExplosao );
}	

function rainha::cruzarToLoc(%this, %worldPos){
	%speed = 15;
	
	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);


   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true, true); 
}

function rainha::onPositionTarget(%this){
	%this.setPosition(%this.cruzarTarget.transPos);
	%this.moveToLoc(%this.targetPos);
	%this.cruzarTarget = -1;
	%this.targetPos = -1;
}

//função para manter o navio selecionado quando é um navio que está na pos 3 e cruza o oceano:
function rainha::reselectMySelf(%this){
	if (%this.dono == $jogadorDaVez){
		echo("Reselecting");
		Foco.getObject(0).setLayer(1); //devolve o antigo foco pra layer comum das peças;
		Foco.clear(); //limpa o Foco (só pode ter uma coisa em foco por vez)
		Foco.add(%this); //atualiza o Foco
		%myMark = getMyMark(%this); //pega a marca de seleção conforme o tipo de unidade
		%myMark.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta no foco
		%myMark.setAutoRotation(50);
		clientSingleSelection(); //chama esta função no arquivo mainGui.cs, colocando o novo foco em prmeiro plano	
	}	
}


//Movimentação:
function rainha::moverPara(%this, %area, %pos){
	%eval = "%areaPos = " @ %area @ "." @ %pos @ ";";
	eval(%eval);
		
	if(!isObject(%this.areaAntiga.myCruzarFronteiras)){
		%this.action("moveToLoc", %areaPos);
	} else {
		%cruzar = %this.areaAntiga.myCruzarFronteiras.isMember(%area);
		if(%cruzar){
			%this.cruzarTarget = %area;
			%this.targetPos = %areaPos;
			%this.action("cruzarToLoc", %this.areaAntiga.transPos);
			%this.reselectMySelf();
		} else {
			%this.action("moveToLoc", %areaPos);	
		}
		$cruzarDe = "nada";
	}
	
	%this.pos = %pos;
	
	%area.pos0Flag = true;
	%area.pos0Quem = %this;
}

//incorporar:
function rainha::mark(%this, %mark){
	%eval = "%embarcante = %this.myTransporte.getObject(" @ %mark - 1 @ ");";
	eval(%eval);
	%eval = "%newMark = embarque" @ %mark @ ".createCopy();";
	eval(%eval);
	
	%newMark.mount(%this);
	//%newMark.setBlendColor(%embarcante.dono.corR, %embarcante.dono.corG, %embarcante.dono.corB, %embarcante.dono.corA);
	%newMark.setBlendColor(1, 1, 1, 0.8);
	
	%eval = "%this.myMark" @ %mark @ " = %newMark;";
	eval(%eval);
}

function rainha::unMark(%this){
	for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
		%eval = "%myMark = %this.myMark" @ %i+1 @ ";";
		eval(%eval);
		%myMark.dismount();
		%myMark.safeDelete();
		%myMark = "nada";
	}
}

function rainha::reMark(%this){
	for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
		%eval = "%embarcante = %this.myTransporte.getObject(" @ %i @ ");";
		eval(%eval);
		%eval = "%newMark = embarque" @ %i+1 @ ".createCopy();";
		eval(%eval);
		
		%newMark.mount(%this);
		//%newMark.setBlendColor(%embarcante.dono.corR, %embarcante.dono.corG, %embarcante.dono.corB, %embarcante.dono.corA);
		%newMark.setBlendColor(1, 1, 1, 0.8);
		
		%eval = "%this.myMark" @ %i+1 @ " = %newMark;";
		eval(%eval);
	}
}



////////////////////////////////////////////////
//Verme:
function verme::createCopy(%this){
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
		 
		 dado = 8;
		 embarcavel = true;
    };
   %copy.setLayer(%this.getLayer());
   return %copy;
}

function verme::explodir(%this){
	explosaoGulok(%this);
    
	playSound("gulok_morrer", 6, true);
      //para tocar um som:
   //alxPlay( somDaExplosao );
}	

function verme::moveToLoc(%this, %worldPos){
	%speed = 25; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}

function verme::kill(%this){
	%this.explodir();
	clientRemoverUnidade(%this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	if(Foco.getObject(0) == %this){
		resetSelection();
	}
	%this.safeDelete();
}

function verme::render(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	clientRemoverUnidade(%this, %this.onde);
	resetSelection();
	%this.safeDelete();
}
	
function verme::fire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiro = soldadoTiro;
	%projetil = %tiro.createCopy();
	
	%orient = angleBetween(%this.getPosition(), %inimigo.getPosition());
	%this.setRotation(%orient);	
		
	universalCreateShot(%this, %inimigo, %projetil, "vermeShot", "", %result);
}

function verme::missFire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroLider = soldadoTiro;
	%projetil = %tiroLider.createCopy();
	
	%inimigoMissPos = clientFindMissPos(%inimigo);
	
	%orient = angleBetween(%this.getPosition(), %inimigoMissPos);
	%this.setRotation(%orient);	
		
	universalCreateShot(%this, %inimigo, %projetil, "vermeShot", "", %result, true, %inimigoMissPos);
}

function verme::onMouseEnter(%this, %modifier, %worldPos){
	if($jogadorDaVez == $mySelf){
		$semiSelectionSoldado.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta neste truta
	}
}

function verme::onMouseLeave(%this, %modifier, %worldPos){
 	if($jogadorDaVez == $mySelf){
		$semiSelectionSoldado.dismount(); //dismonta a selectionMark
		$semiSelectionSoldado.setPosition("-280 -280"); //fora da tela
	}
}

function verme::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	
	%myImage = "verme" @ %corDoMeuDono @ "ImageMap";
			
	%this.setImageMap(%myImage);
}

//Movimentação:
function verme::moverPara(%this, %area, %pos){
	%eval = "%areaPos = " @ %area @ "." @ %pos @ ";";
	eval(%eval);
	
	%this.action("moveToLoc", %areaPos);
	%this.pos = %pos;
	
	if(%pos $= "pos1" || %pos $= "pos2"){
		%eval = "%area." @ %pos @ "Flag = %this.class;";
		eval(%eval);
		%eval = "%area." @ %pos @ "Quem = %this;";
		eval(%eval);
	} else {
		%eval = "%area.my" @ %pos @ "List.add(%this);";
		eval(%eval);
	}
}

function verme::callMyHud(%this){
	clientZerarUnitHud();
	unitHud.setVisible(true);
	
	unitHud.bitmap = "~/data/images/vermeHud";
	unitHudDefesa_txt.setVisible(true);
	unitHudAtaque_txt.setVisible(true);
	
	%vermes = clientVerifyHorda(%this.onde);
	%bonusMax = 0;
	if(%vermes >= 4){
		%bonusMax = (mFloor(%vermes / 4)) * %this.onde.dono.horda;	
	}
	
	unitHudDefesa_txt.text = ($myPersona.aca_s_d_min + $mySelf.moralDefesa + $myPersona.aca_a_1) @ "  a  " @ ($myPersona.aca_s_d_max + $mySelf.moralDefesa + $myPersona.aca_a_1 + %bonusMax);
	unitHudAtaque_txt.text = ($myPersona.aca_s_a_min + $mySelf.moralAtaque) @ "  a  " @ ($myPersona.aca_s_a_max + $mySelf.moralAtaque + %bonusMax);
}

function verme::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
		
	if(%this.dono.exoEsqueleto > 0){
		%myImage = "verme" @ %corDoMeuDono @ "BocarraImageMap";
	} else {
		%myImage = "verme" @ %corDoMeuDono @ "ImageMap";
	}
	%this.setImageMap(%myImage);
	
	schedule(1000, 0, "setUnitImage", %this);
}


//////////
//Cefalok:
function cefalok::createCopy(%this){
      %copy = new t2dStaticSprite(){
         scenegraph = $strategyScene;
         class = %this.class;   
         imageMap = %this.imageMap;
         size = %this.size;
		 rotation = %this.rotation;
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
		
		
		
		 dado = 14;
		 transporte = true;
		
    };
   
   %copy.setLayer(%this.getLayer());
   return %copy;
}


function cefalok::explodir(%this){
	explosaoGulok(%this);
    
	playSound("gulok_morrer", 6, true);	
		
	clientCameraShake(1.5, 0.5);
	
      //para tocar um som:
   //alxPlay( somDaExplosao );
}	


function cefalok::moveToLoc(%this, %worldPos){
	%speed = 25;
	
	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);


   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}




function cefalok::kill(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	if(isObject(%this.myTransporte)){
		for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
			%this.myTransporte.getObject(%i).render();	
		}
	}
	clientRemoverUnidade(%this, %this.onde);
	if(Foco.getObject(0) == %this){
		resetSelection();
	}
	%this.safeDelete();
}

///////////////Render
function cefalok::render(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	clientRemoverUnidade(%this, %this.onde);
	resetSelection();
	%this.safeDelete();
}
///////////



function cefalok::fire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroNavio = soldadoTiro;
	%projetil = %tiroNavio.createCopy();
	
	%orient = angleBetween(%this.getPosition(), %inimigo.getPosition());
	%this.setRotation(%orient);	
		
	universalCreateShot(%this, %inimigo, %projetil, "vermeShot", "", %result);	
}

function cefalok::missFire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroLider = soldadoTiro;
	%projetil = %tiroLider.createCopy();
	
	%inimigoMissPos = clientFindMissPos(%inimigo);
	
	%orient = angleBetween(%this.getPosition(), %inimigoMissPos);
	%this.setRotation(%orient);	
		
	universalCreateShot(%this, %inimigo, %projetil, "vermeShot", "", %result, true, %inimigoMissPos);
}


function cefalok::mark(%this, %mark){
	%eval = "%embarcante = %this.myTransporte.getObject(" @ %mark - 1 @ ");";
	eval(%eval);
	%eval = "%newMark = embarque" @ %mark @ ".createCopy();";
	eval(%eval);
	
	%newMark.mount(%this);
	%newMark.setBlendColor(%embarcante.dono.corR, %embarcante.dono.corG, %embarcante.dono.corB, %embarcante.dono.corA);
	
	%eval = "%this.myMark" @ %mark @ " = %newMark;";
	eval(%eval);
}

function cefalok::unMark(%this){
	for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
		%eval = "%myMark = %this.myMark" @ %i+1 @ ";";
		eval(%eval);
		%myMark.dismount();
		%myMark.safeDelete();
		%myMark = "nada";
	}
}

function cefalok::reMark(%this){
	for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
		%eval = "%embarcante = %this.myTransporte.getObject(" @ %i @ ");";
		eval(%eval);
		%eval = "%newMark = embarque" @ %i+1 @ ".createCopy();";
		eval(%eval);
		
		%newMark.mount(%this);
		%newMark.setBlendColor(%embarcante.dono.corR, %embarcante.dono.corG, %embarcante.dono.corB, %embarcante.dono.corA);
		
		%eval = "%this.myMark" @ %i+1 @ " = %newMark;";
		eval(%eval);
	}
}



function cefalok::onMouseEnter(%this, %modifier, %worldPos){
	if($jogadorDaVez.id $= $mySelf.id){
		$semiSelectionNavio.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta neste truta
	}
}

function cefalok::onMouseLeave(%this, %modifier, %worldPos){
	if($jogadorDaVez.id $= $mySelf.id){
		$semiSelectionNavio.dismount(); //dismonta a selectionMark
		$semiSelectionNavio.setPosition("-280 -280"); //fora da tela
	}
}




//função para usar imagens em vez de blend:
function cefalok::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	
//	if($myVideoPrefX < 1024){
//		%myImage = "cefalok" @ %corDoMeuDono @ "SMImageMap";	
//	} else {
		%myImage = "cefalok" @ %corDoMeuDono @ "ImageMap";
//	}
	
	%this.setImageMap(%myImage);
}

function cefalok::cruzarToLoc(%this, %worldPos){
	%speed = 25;
	
	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);


   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true, true); 
}

function cefalok::onPositionTarget(%this){
	%this.setPosition(%this.cruzarTarget.transPos);
	%this.moveToLoc(%this.targetPos);
	%this.cruzarTarget = -1;
	%this.targetPos = -1;
}

//função para manter o navio selecionado quando é um navio que está na pos 3 e cruza o oceano:
function cefalok::reselectMySelf(%this){
	if (%this.dono == $jogadorDaVez){
		echo("Reselecting");
		Foco.getObject(0).setLayer(1); //devolve o antigo foco pra layer comum das peças;
		Foco.clear(); //limpa o Foco (só pode ter uma coisa em foco por vez)
		Foco.add(%this); //atualiza o Foco
		%myMark = getMyMark(%this); //pega a marca de seleção conforme o tipo de unidade
		%myMark.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta no foco
		%myMark.setAutoRotation(50);
		clientSingleSelection(); //chama esta função no arquivo mainGui.cs, colocando o novo foco em prmeiro plano	
	}	
}


//Movimentação:
function cefalok::moverPara(%this, %area, %pos){
	%eval = "%areaPos = " @ %area @ "." @ %pos @ ";";
	eval(%eval);
		
	if(!isObject(%this.areaAntiga.myCruzarFronteiras)){
		%this.action("moveToLoc", %areaPos);
	} else {
		%cruzar = %this.areaAntiga.myCruzarFronteiras.isMember(%area);
		if(%cruzar){
			%this.cruzarTarget = %area;
			%this.targetPos = %areaPos;
			%this.action("cruzarToLoc", %this.areaAntiga.transPos);
			%this.reselectMySelf();
		} else {
			%this.action("moveToLoc", %areaPos);	
		}
		$cruzarDe = "nada";
	}
	
	%this.pos = %pos;
	
	if(%pos $= "pos1" || %pos $= "pos2"){
		%eval = "%area." @ %pos @ "Flag = %this.class;";
		eval(%eval);
		%eval = "%area." @ %pos @ "Quem = %this;";
		eval(%eval);
	} else {
		%eval = "%area.my" @ %pos @ "List.add(%this);";
		eval(%eval);
	}
}


function cefalok::callMyHud(%this){
	clientZerarUnitHud();
	unitHud.setVisible(true);
	
	unitHud.bitmap = "~/data/images/cefalokHud";
	unitHudDefesa_txt.setVisible(true);
	unitHudAtaque_txt.setVisible(true);
	
	unitHudDefesa_txt.text = ($myPersona.aca_n_d_min + $mySelf.moralDefesa) @ "  a  " @ ($myPersona.aca_n_d_max + $mySelf.moralDefesa);
	unitHudAtaque_txt.text = ($myPersona.aca_n_a_min + $mySelf.moralAtaque) @ "  a  " @ ($myPersona.aca_n_a_max + $mySelf.moralAtaque);
}


///////////////
//Ovos:
function ovo::createCopy(%this){
      %copy = new t2dStaticSprite(){
         scenegraph = $strategyScene;
         class = %this.class;   
         imageMap = %this.imageMap;
         size = %this.size;
		 rotation = %this.rotation;
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
		
		
		
		 dado = 0;
		 
		
    };
   
   %copy.setLayer(%this.getLayer());
   return %copy;
}
function ovo::spawnUnit(%this, %unit){
	echo("ovo::spawnUnit() => " @ %this.onde SPC %unit);
	%unitBP = %unit @ "_BP";
	%newUnit = %unitBP.createCopy();
	%newUnit.gulok = true;
	
	%explosao = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%explosao.loadEffect("~/data/particles/ovoEclodir" @ %this.dono.myColor @ ".eff");
	%explosao.setEffectLifeMode("KILL", 1);
    %explosao.mount(%newUnit, 0, 0, 0, 0, 0, 0, 0);
    %explosao.playEffect();
	
	%this.positionUnit(%newUnit);
	%newUnit.isSelectable = 1;
	if(%newUnit.class !$= "ovo"){
		%newUnit.isMoveable = 1;
	} else {
		%newUnit.isMoveable = 0;
	}
	if($jogoEmDuplas){
		%newUnit.dono = $jogadorDaVez;
		%this.onde.resolverMyStatus();	
	} else {
		%newUnit.dono = %this.dono;
	}
	%newUnit.setMyImage();
	%newUnit.dono.mySimUnits.add(%newUnit); //adiciona a unit no simset de unidades do player;
	if(%newUnit.class $= "lider"){
		%newUnit.dono.mySimLideres.add(%newUnit);
	}
	
	return %newUnit;
}
function ovo::positionUnit(%this, %unit){
	echo("ovo::positionUnit() => " @ %this.onde SPC %unit.class);
	%unit.onde = %this.onde;
	%areaLocal = %unit.onde;
	%unit.setUseMouseEvents(true);
	
	%unit.setPosition(%areaLocal.pos4);
	%areaLocal.positionUnit(%unit); //movimenta ela pra posição correta e marca os dados corretos
	
	clientAtualizarPosReservaTxt(%areaLocal); //atualiza o texto das posições reserva
}
function ovo::eclodir(%this){
	for(%i = 0; %i < 3; %i++){
		%this.spawnUnit("verme");	
	}
	%explosao = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%explosao.loadEffect("~/data/particles/ovoEclodir" @ %this.dono.myColor @ ".eff");
	%explosao.setEffectLifeMode("KILL", 1);
    %explosao.setPosition(%this.getPosition());
    %explosao.playEffect();
         
    playSound("ovo_eclodir", 5, true);

	//agora deleta o ovo:
	%this.dono.mySimUnits.remove(%this);
	clientRemoverUnidade(%this, %this.onde);
	if(Foco.getObject(0) == %this){
		resetSelection();
	}
	
	%this.dono.mySimOvos.remove(%this);
	%this.onde.resolverMyStatus();
	schedule(1000, 0, "clientVerifyHorda", %this.onde);
	%this.safeDelete();
}	
function ovo::explodir(%this){
	%explosao = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%explosao.loadEffect("~/data/particles/ovoExplosion.eff");
	%explosao.setEffectLifeMode("KILL", 1);
    %explosao.setPosition(%this.getPosition());
    %explosao.playEffect();
         
      //para tocar um som:
   //alxPlay( somDaExplosao );
}	
function ovo::kill(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	%this.dono.mySimOvos.remove(%this);
	clientRemoverUnidade(%this, %this.onde);
	if(Foco.getObject(0) == %this){
		resetSelection();
	}
	%this.safeDelete();
}
function ovo::render(%this){
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	%this.dono.mySimOvos.remove(%this);
	clientRemoverUnidade(%this, %this.onde);
	resetSelection();
	%this.safeDelete();
}
function ovo::moverPara(%this, %area, %pos){
	%eval = "%areaPos = " @ %area @ "." @ %pos @ ";";
	eval(%eval);
		
	%this.pos = %pos;
	
	if(%pos $= "pos1" || %pos $= "pos2"){
		%eval = "%area." @ %pos @ "Flag = %this.class;";
		eval(%eval);
		%eval = "%area." @ %pos @ "Quem = %this;";
		eval(%eval);
	} else {
		%eval = "%area.my" @ %pos @ "List.add(%this);";
		eval(%eval);
	}
}
function ovo::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	
	%myImage = "ovo" @ %corDoMeuDono @ "ImageMap";

	%this.setImageMap(%myImage);
}

//ZANGÃO:
function zangao::createCopy(%this){
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
		 
		 dado = 14;
		 embarcavel = true;
    };
	%copy.setLayer(%this.getLayer());
    return %copy;
}


function zangao::explodir(%this){
	explosaoGulok(%this);
        
	clientCameraShake(1.5, 0.7);		

    //para tocar um som:
	playSound("gulok_morrer", 6, true);
}	


function zangao::moveToLoc(%this, %worldPos){
	%speed = 25; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);
	
   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}


function zangao::kill(%this){
	if(%this.myTransporte.getcount() > 0){
		%this.unMark();
		%verme = %this.myTransporte.getObject(0);
		%verme.dismount();
		%this.myTransporte.remove(%verme);
		%verme.explodir();
		%this.reMark();
		clientFinalizarAtaque();
		clientFinalizarAiAtaque();
	} else {
		%eval = "%donoDoMorto = $" @ %this.dono.id @ ";";
		eval(%eval);
		%eval = "%donoDoMortoAreas = $"@%donoDoMorto.id@"Areas;";
		eval(%eval);
		
		%this.explodir();
		%this.dono.mySimUnits.remove(%this);
		%this.dono.mySimLideres.remove(%this);	
		clientRemoverUnidade(%this, %this.onde);
		
		if(Foco.getObject(0) == %this){
			resetSelection();
		}
		%this.safeDelete();
		//clientMsgSeuLiderMorreu();
	}
}

/////////////Render:
function zangao::render(%this){
	%eval = "%donoDoMorto = $" @ %this.dono.id @ ";";
	eval(%eval);
	%eval = "%donoDoMortoAreas = $"@%donoDoMorto.id@"Areas;";
	eval(%eval);
	
	%this.explodir();
	%this.dono.mySimUnits.remove(%this);
	%this.dono.mySimLideres.remove(%this);	
	clientRemoverUnidade(%this, %this.onde);
	resetSelection();
	%this.safeDelete();
}
///////////////


	
	
	
function zangao::fire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroLider = soldadoTiro;
	%projetil = %tiroLider.createCopy();
	
	%orient = angleBetween(%this.getPosition(), %inimigo.getPosition());
	%this.setRotation(%orient);	
		
	universalCreateShot(%this, %inimigo, %projetil, "zangaoShot", "", %result);	
}

function zangao::missFire(%this, %inimigo, %result){
  	// Criar o projétil:
	%tiroLider = soldadoTiro;
	%projetil = %tiroLider.createCopy();
	
	%inimigoMissPos = clientFindMissPos(%inimigo);
	
	%orient = angleBetween(%this.getPosition(), %inimigoMissPos);
	%this.setRotation(%orient);	
		
	universalCreateShot(%this, %inimigo, %projetil, "zangaoShot", "", %result, true, %inimigoMissPos);
}


//testes de mouse over:

function zangao::onMouseEnter(%this, %modifier, %worldPos){
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	
	if($jogadorDaVez.id $= %mySelf.id){
		$semiSelectionTanque.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta neste truta
	}
}

function zangao::onMouseLeave(%this, %modifier, %worldPos)
{
   %eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	
	if($jogadorDaVez.id $= %mySelf.id){
		$semiSelectionTanque.dismount(); //dismonta a selectionMark
		$semiSelectionTanque.setPosition("-280 -280"); //fora da tela
	}
}



//função para usar imagens em vez de blend:
function zangao::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	if(%this.JPBP > 0){
		%myImage = "zangao" @ %this.liderNum @ %corDoMeuDono @ "AsaImageMap";	
	} else {
		%myImage = "zangao" @ %this.liderNum @ %corDoMeuDono @ "ImageMap";	
	}
	%this.setImageMap(%myImage);
	
	schedule(1000, 0, "setUnitImage", %this);
}

function setUnitImage(%unit){
	%unit.setMyImage();	
}


//incorporar:
function zangao::mark(%this, %mark){
	%eval = "%embarcante = %this.myTransporte.getObject(" @ %mark - 1 @ ");";
	eval(%eval);
	%eval = "%newMark = embarque" @ %mark @ ".createCopy();";
	eval(%eval);
	
	%newMark.mount(%this);
	//%newMark.setBlendColor(%embarcante.dono.corR, %embarcante.dono.corG, %embarcante.dono.corB, %embarcante.dono.corA);
	%newMark.setBlendColor(1, 1, 1, 0.8);
	
	%eval = "%this.myMark" @ %mark @ " = %newMark;";
	eval(%eval);
}

function zangao::unMark(%this){
	for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
		%eval = "%myMark = %this.myMark" @ %i+1 @ ";";
		eval(%eval);
		%myMark.dismount();
		%myMark.safeDelete();
		%myMark = "nada";
	}
}

function zangao::reMark(%this){
	for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
		%eval = "%embarcante = %this.myTransporte.getObject(" @ %i @ ");";
		eval(%eval);
		%eval = "%newMark = embarque" @ %i+1 @ ".createCopy();";
		eval(%eval);
		
		%newMark.mount(%this);
		//%newMark.setBlendColor(%embarcante.dono.corR, %embarcante.dono.corG, %embarcante.dono.corB, %embarcante.dono.corA);
		%newMark.setBlendColor(1, 1, 1, 0.8);
		
		%eval = "%this.myMark" @ %i+1 @ " = %newMark;";
		eval(%eval);
	}
}



/*
function zangao::callMyHud(%this){
	clientZerarUnitHud();
	unitHud.setVisible(true);
	
	unitHud.bitmap = "~/data/images/lider" @ %this.liderNum @ "Hud";
	unitHudDefesaL_txt.setVisible(true);
	unitHudAtaqueL_txt.setVisible(true);
	
	if(%this.liderNum == 1){
		unitHudDefesaL_txt.text = ($myPersona.a_L1DMn + $mySelf.moralDefesa) @ "  a  " @ ($myPersona.a_L1DMx + $mySelf.moralDefesa);
		unitHudAtaqueL_txt.text = ($myPersona.a_L1AMn + $mySelf.moralAtaque) @ "  a  " @ ($myPersona.a_L1AMx + $mySelf.moralAtaque);
		if($myPersona.a_L1Esc > 0){
			escHudMicon.setVisible(true);
			escHudMicon.bitmap = "~/data/images/academia/amicon_escudo" @ $myPersona.a_L1Esc;
		}
		if($myPersona.a_L1JPk > 0){
			jpkHudMicon.setVisible(true);
			jpkHudMicon.bitmap = "~/data/images/academia/amicon_jetPack" @ $myPersona.a_L1JPk;
		}
		if($myPersona.a_L1Snp > 0){
			snpHudMicon.setVisible(true);
			snpHudMicon.bitmap = "~/data/images/academia/amicon_sniper" @ $myPersona.a_L1Snp;
		}
		if($myPersona.a_L1Mrl > 0){
			mrlHudMicon.setVisible(true);
			mrlHudMicon.bitmap = "~/data/images/academia/amicon_moral" @ $myPersona.a_L1Mrl;
		}
	} else if(%this.liderNum == 2){
		unitHudDefesaL_txt.text = ($myPersona.a_L2DMn + $mySelf.moralDefesa) @ "  a  " @ ($myPersona.a_L2DMx + $mySelf.moralDefesa);
		unitHudAtaqueL_txt.text = ($myPersona.a_L2AMn + $mySelf.moralAtaque) @ "  a  " @ ($myPersona.a_L2AMx + $mySelf.moralAtaque);
		if($myPersona.a_L2Esc > 0){
			escHudMicon.setVisible(true);
			escHudMicon.bitmap = "~/data/images/academia/amicon_escudo" @ $myPersona.a_L2Esc;
		}
		if($myPersona.a_L2JPk > 0){
			jpkHudMicon.setVisible(true);
			jpkHudMicon.bitmap = "~/data/images/academia/amicon_jetPack" @ $myPersona.a_L2JPk;
		}
		if($myPersona.a_L2Snp > 0){
			snpHudMicon.setVisible(true);
			snpHudMicon.bitmap = "~/data/images/academia/amicon_sniper" @ $myPersona.a_L2Snp;
		}
		if($myPersona.a_L2Mrl > 0){
			mrlHudMicon.setVisible(true);
			mrlHudMicon.bitmap = "~/data/images/academia/amicon_moral" @ $myPersona.a_L2Mrl;
		}
	} else if(%this.liderNum == 3){
		unitHudDefesaL_txt.text = ($myPersona.a_L3DMn + $mySelf.moralDefesa) @ "  a  " @ ($myPersona.a_L3DMx + $mySelf.moralDefesa);
		unitHudAtaqueL_txt.text = ($myPersona.a_L3AMn + $mySelf.moralAtaque) @ "  a  " @ ($myPersona.a_L3AMx + $mySelf.moralAtaque);
		if($myPersona.a_L3Esc > 0){
			escHudMicon.setVisible(true);
			escHudMicon.bitmap = "~/data/images/academia/amicon_escudo" @ $myPersona.a_L3Esc;
		}
		if($myPersona.a_L3JPk > 0){
			jpkHudMicon.setVisible(true);
			jpkHudMicon.bitmap = "~/data/images/academia/amicon_jetPack" @ $myPersona.a_L3JPk;
		}
		if($myPersona.a_L3Snp > 0){
			snpHudMicon.setVisible(true);
			snpHudMicon.bitmap = "~/data/images/academia/amicon_sniper" @ $myPersona.a_L3Snp;
		}
		if($myPersona.a_L3Mrl > 0){
			mrlHudMicon.setVisible(true);
			mrlHudMicon.bitmap = "~/data/images/academia/amicon_moral" @ $myPersona.a_L3Mrl;
		}
	}
}
*/

////////////////
//lider cruzando oceanos:
function zangao::cruzarToLoc(%this, %worldPos){
	%speed = 25;
	
	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);


   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true, true); 
}

function zangao::onPositionTarget(%this){
	%this.setPosition(%this.cruzarTarget.transPos);
	%this.moveToLoc(%this.targetPos);
	%this.cruzarTarget = -1;
	%this.targetPos = -1;
}

//função para manter o lider selecionado quando é um lider que está na pos 3 e cruza o oceano:
function zangao::reselectMySelf(%this){
	if (%this.dono == $jogadorDaVez){
		Foco.getObject(0).setLayer(1); //devolve o antigo foco pra layer comum das peças;
		Foco.clear(); //limpa o Foco (só pode ter uma coisa em foco por vez)
		Foco.add(%this); //atualiza o Foco
		%myMark = getMyMark(%this); //pega a marca de seleção conforme o tipo de unidade
		%myMark.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta no foco
		%myMark.setAutoRotation(50);
		clientSingleSelection(); //chama esta função no arquivo mainGui.cs, colocando o novo foco em prmeiro plano	
	}	
}


//Movimentação:
function zangao::moverPara(%this, %area, %pos){
	%eval = "%areaPos = " @ %area @ "." @ %pos @ ";";
	eval(%eval);
		
	if(!isObject(%this.areaAntiga.myCruzarFronteiras)){
		%this.action("moveToLoc", %areaPos);
	} else {
		%cruzar = %this.areaAntiga.myCruzarFronteiras.isMember(%area);
		if(%cruzar){
			%this.cruzarTarget = %area;
			%this.targetPos = %areaPos;
			%this.action("cruzarToLoc", %this.areaAntiga.transPos);
			%this.reselectMySelf();
		} else {
			%this.action("moveToLoc", %areaPos);	
		}
		$cruzarDe = "nada";
	}
	
	%this.pos = %pos;
	
	if(%pos $= "pos1" || %pos $= "pos2"){
		%eval = "%area." @ %pos @ "Flag = %this.class;";
		eval(%eval);
		%eval = "%area." @ %pos @ "Quem = %this;";
		eval(%eval);
	} else {
		%eval = "%area.my" @ %pos @ "List.add(%this);";
		eval(%eval);
	}
}



/////////////////
//explosão Gulok:
function explosaoGulok(%objeto){
	%explosao = new t2dParticleEffect(){scenegraph = $strategyScene;};
	%explosao.loadEffect("~/data/particles/" @ %objeto.class @ "Explosion.eff");
	%explosao.setEffectLifeMode("KILL", 1);
    %explosao.setPosition(%objeto.getPosition());
	
	switch$ (%objeto.dono.myColor){
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
	
	%myEmitterCount = %explosao.getEmitterCount();
	
	for(%i = 0; %i < %myEmitterCount; %i++){
		%myTempEmitter = %explosao.getEmitterObject(%i);
		%myTempEmitter.selectGraph("red_life");
		%myTempEmitter.clearDataKeys();
		%myTempEmitter.addDataKey(0, %red);
		
		%myTempEmitter.selectGraph("green_life");
		%myTempEmitter.clearDataKeys();
		%myTempEmitter.addDataKey(0, %green);
		
		%myTempEmitter.selectGraph("blue_life");
		%myTempEmitter.clearDataKeys();
		%myTempEmitter.addDataKey(0, %blue);
		
		%myTempEmitter.setIntenseParticles(false);
	}	
	
    %explosao.playEffect();
}




//////////////////
//Teste de peças 3D:
/*
function tanque::createCopy(%this){
      %copy = new t2dShape3D(){
         scenegraph = $strategyScene;
         class = %this.class;   
         shape = %this.shape;
         size = %this.size;
		 shapeRotation = %this.shapeRotation;
		 shapeScale = %this.shapeScale;
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
		
		
		 dado = 12;
    };
   %copy.setLayer(%this.getLayer());
   return %copy;
}

function tanque::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	%myShape = "game/data/shapes/tanque" @ %corDoMeuDono @ ".dts";
		
	%this.setShape(%myShape);
}
*/


/////////
//Relíquias e Artefatos:
function reliquia::onMouseEnter(%this, %modifier, %worldPos){
	if($jogadorDaVez == $mySelf){
		//if(%this.onde.dono == $mySelf || %this.onde.dono == $mySelf.myDupla){
			$semiSelectionReliquia.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta nesta base
		//}
	}
}

function reliquia::onMouseLeave(%this, %modifier, %worldPos){
   	if($jogadorDaVez == $mySelf){
		$semiSelectionReliquia.dismount(); //dismonta a selectionMark
		$semiSelectionReliquia.setPosition("-280 -280"); //fora da tela
	}
}

function reliquia::callMyHud(%this){
	%myName = %this.getName();
	echo("chamando relíquia hud: " @ %myName);
	
	switch$ (%myName){
		case "nexusAlquimico":
			clientNexusCallHud();
	}
}

function clientApagarRelArtTabs(){
	nexusTab.setVisible(false);	
	geoCanhaoTab.setVisible(false);	
}


function artefato::onMouseEnter(%this, %modifier, %worldPos){
	if($jogadorDaVez == $mySelf){
		//if(%this.onde.dono == $mySelf || %this.onde.dono == $mySelf.myDupla){
			$semiSelectionReliquia.mount(%this, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta nesta base
		//}
	}
}

function artefato::onMouseLeave(%this, %modifier, %worldPos){
   	if($jogadorDaVez == $mySelf){
		$semiSelectionReliquia.dismount(); //dismonta a selectionMark
		$semiSelectionReliquia.setPosition("-280 -280"); //fora da tela
	}
}

function artefato::callMyHud(%this){
	%myName = %this.getName();
	echo("chamando artefato hud: " @ %myName);
	
	switch$ (%myName){
		case "geoCanhao":
			clientGeoCanhaoCallHud();
		
	}
}

function clientFindMissPos(%unit){
	%inimigoPosX = firstWord(%unit.getPosition());
	%inimigoPosY = getWord(%unit.getPosition(), 1);
	
	%randomMissX = dadoReal(-15, 15) / 10; //dado de -8 a 8;
	%randomMissY = dadoReal(15, 15) / 10; //dado de -8 a 8;
	
	if(%randomMissX > -0.7 && %randomMissX < 0){
		%randomMissX = -0.7;
	} else if(%randomMissX < 0.7 && %randomMissX > 0){
		%randomMissX = 0.7;
	}
	if(%randomMissY > -0.7 && %randomMissY < 0){
		%randomMissY = -0.7;
	} else if(%randomMissY < 0.7 && %randomMissY > 0){
		%randomMissY = 0.7;
	}
		
	%inimigoMissPos = %inimigoPosX + %randomMissX SPC %inimigoPosY + %randomMissY;
	
	return %inimigoMissPos;
}