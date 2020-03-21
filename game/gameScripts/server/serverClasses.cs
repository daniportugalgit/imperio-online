// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverClasses.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 17 de outubro de 2007 10:02
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

// ============================================================
/////////////////////////////////////

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
 
   return %copy;
}

function base::spawnUnit(%this, %unit){
	%unitBP = %unit @ "_BP";
	%newUnit = %unitBP.createCopy();
	
	if(%this.dono.jogo.emDuplas){
		%newUnit.dono = %this.dono.jogo.jogadorDaVez;
	} else {
		%newUnit.dono = %this.dono;
	}
	
	%this.positionUnit(%newUnit);
	%newUnit.isMoveable = 1;
	%newUnit.dono.mySimUnits.add(%newUnit); //adiciona a unit no simset de unidades do player;
	if(%newUnit.class $= "lider"){
		%newUnit.dono.mySimLideres.add(%newUnit);	
	}
	
	return %newUnit;
}

function base::positionUnit(%this, %unit){
	if(%this.dono.marinheiro){
		%custoNavio = 2;
	} else {
		%custoNavio = 3;	
	}
	
	switch$(%unit.class){
		case "soldado": %this.dono.jogo.jogadorDaVez.imperiais -= 1;
		case "tanque": %this.dono.jogo.jogadorDaVez.imperiais -= 2;
		case "navio": %this.dono.jogo.jogadorDaVez.imperiais -= %custoNavio;
	}
	%this.onde.positionUnit(%unit);
	if(%this.dono.jogo.emDuplas){
		if(%this.dono != %unit.dono){
			%this.onde.resolverMyStatus();	
		}
	}
}
///////////


//função para usar imagens em vez de blend:
function base::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	%myImage = "base" @ %corDoMeuDono @ "ImageMap";
	
	%this.setImageMap(%myImage);
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
		 
    };
   
   return %copy;
}

function Soldado::kill(%this, %quemMatou){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	if(%this.dono.mySimAreas.getCount() <= 0){
		if(%quemMatou $= "DESASTRE"){
			serverPlayerMorteNatural(%this.dono);
		} else {
			serverPlayerKill(%this.dono, %quemMatou);
		}
	}
	%this.safeDelete();
}

function Soldado::moveToLoc(%this, %worldPos){
	%speed = 25; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}
	
/////////////Render:
function Soldado::render(%this){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	
	if(%this.dono.mySimAreas.getCount() <= 0){
		%jogo.playerSuicide(%this.dono);
	}
	%this.safeDelete();
}
////////////
	
	
function soldado::fire(%this, %inimigo){
  	%inimigo.kill(%this.dono);
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

///////////////////////////////////////////////
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
   
   return %copy;
}

	
function Tanque::moveToLoc(%this, %worldPos){
	%speed = 30; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}


function tanque::kill(%this, %quemMatou){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	if(%this.dono.mySimAreas.getCount() <= 0){
		if(%quemMatou $= "DESASTRE"){
			serverPlayerMorteNatural(%this.dono);
		} else {
			serverPlayerKill(%this.dono, %quemMatou);
		}
	}
	%this.safeDelete();
}

//////////////Render:
function tanque::render(%this){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	if(%this.dono.mySimAreas.getCount() <= 0){
		%jogo.playerSuicide(%this.dono);
	}
	%this.safeDelete();
}
/////////////


function tanque::fire(%this, %inimigo){
 	%inimigo.kill(%this.dono);
}

//função para usar imagens em vez de blend:
function tanque::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	%myImage = "tanque" @ %corDoMeuDono @ "ImageMap";
	
	%this.setImageMap(%myImage);
}




//Movimentação:
function tanque::moverPara(%this, %area, %pos){
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

//////////////////////////////
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
		 
		
    };
   
   return %copy;
}


function navio::moveToLoc(%this, %worldPos){
	%speed = 25;
	
	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);


   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}


function navio::kill(%this, %quemMatou){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	
	if(isObject(%this.myTransporte)){
		for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
			%this.myTransporte.getObject(%i).render();	
		}
	}
	
	if(%this.dono.mySimAreas.getCount() <= 0){
		if(%quemMatou $= "DESASTRE"){
			serverPlayerMorteNatural(%this.dono);
		} else {
			serverPlayerKill(%this.dono, %quemMatou);
		}
	}
	%this.safeDelete();
}

///////////////Render:
function navio::render(%this){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);	
	%this.dono.mySimUnits.remove(%this);
	
	if(isObject(%this.myTransporte)){
		for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
			%this.myTransporte.getObject(%i).render();	
		}
	}

	if(%this.dono.mySimAreas.getCount() <= 0){
		%jogo.playerSuicide(%this.dono);
	}
	%this.safeDelete();
}
////////////////


function navio::fire(%this, %inimigo){
 	%inimigo.kill(%this.dono);
}


//função para usar imagens em vez de blend:
function navio::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	%myImage = "navio" @ %corDoMeuDono @ "ImageMap";
	
	%this.setImageMap(%myImage);
}



//Movimentação:
function navio::moverPara(%this, %area, %pos){
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



///////////////////////////
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
		 
    };
    return %copy;
}


function lider::moveToLoc(%this, %worldPos){
	%speed = 25; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}


function lider::kill(%this, %quemMatou){
	%jogo = %this.dono.jogo;
	if(%this.escudoOn){
		if(%this.myEscudos < 2){
			%this.myEscudos = 0;	
			%this.escudoOn = false;
		} else {
			%this.myEscudos -= 1;	
		}
	    //para tocar um som:
	    //alxPlay( somDaExplosao );	
	} else {
		serverRemoverUnidade(%jogo, %this, %this.onde);
		%this.dono.mySimUnits.remove(%this);
		%this.dono.mySimLideres.remove(%this);	
		%this.dono.liderVive = 0;
		%jogo.verificarMoral(%this.dono);
		if(%this.dono.mySimAreas.getCount() <= 0){
			if(%quemMatou $= "DESASTRE"){
				serverPlayerMorteNatural(%this.dono);
			} else {
				serverPlayerKill(%this.dono, %quemMatou);
			}
		}
		%this.safeDelete();
	}
}

/////////////Render:
function lider::render(%this){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	%this.dono.mySimLideres.remove(%this);	
	%this.dono.liderVive = 0;
	%jogo.verificarMoral(%this.dono);
	%this.safeDelete();
	
	if(%this.dono.mySimAreas.getCount() <= 0){
		%jogo.playerSuicide(%this.dono);
	}
}
///////////////
	
	
function lider::fire(%this, %inimigo){
 	%inimigo.kill(%this.dono);
}


//função para usar imagens em vez de blend:
function lider::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	%myImage = "lider" @ %corDoMeuDono @ "ImageMap";
	
	%this.setImageMap(%myImage);
}


//criar escudo:
function lider::criarMeuEscudo(%this){
	%this.escudoOn = true;
}



//Movimentação:
function lider::moverPara(%this, %area, %pos){
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

//===========================
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
 
   return %copy;
}

function rainha::spawnUnit(%this, %unit){
	%unitBP = %unit @ "_BP";
	%newUnit = %unitBP.createCopy();
	%newUnit.gulok = true;
	
	if(%this.dono.jogo.emDuplas){
		%newUnit.dono = %this.dono.jogo.jogadorDaVez;
	} else {
		%newUnit.dono = %this.dono;
	}
	
	%this.positionUnit(%newUnit);
	if(%newUnit.class !$= "ovo"){
		%newUnit.isMoveable = 1;
	} else {
		%newUnit.isMoveable = 0;
	}
	%newUnit.dono.mySimUnits.add(%newUnit); //adiciona a unit no simset de unidades do player;
	if(%newUnit.class $= "zangao"){
		%newUnit.dono.mySimLideres.add(%newUnit);	
	} else if (%newUnit.class $= "ovo"){
		%newUnit.dono.mySimOvos.add(%newUnit);	
	}
	
	return %newUnit;
}

function rainha::positionUnit(%this, %unit){
	if(%this.cortejada || %this.matriarca){
		%custoOvo = 2;	
	} else {
		%custoOvo = 3;	
	}
	if(%this.dono != %this.dono.jogo.aiPlayer){
		switch$(%unit.class){
			case "ovo": %this.dono.jogo.jogadorDaVez.imperiais -= %custoOvo;
			case "cefalok": %this.dono.jogo.jogadorDaVez.imperiais -= 4;
		}
	}
	%this.onde.positionUnit(%unit);
	%unit.onde = %this.onde;
	if(%this.dono.jogo.emDuplas){
		if(%this.dono != %unit.dono){
			%this.onde.resolverMyStatus();	
		}
	}
}

//Movimentação:
function rainha::moverPara(%this, %area, %pos){
	%this.pos = "pos0";
	
	%area.pos0Flag = true;
	%area.pos0Quem = %this;
}


function rainha::kill(%this, %quemMatou){
	%jogo = %this.dono.jogo;
	if(isObject(%this.myTransporte)){
		if(%this.myTransporte.getcount() > 0){
			%verme = %this.myTransporte.getObject(0);
			%verme.dismount();
			%this.myTransporte.remove(%verme);
			%verme.safeDelete();
		} else {
			serverRemoverUnidade(%jogo, %this, %this.onde);
			%this.dono.mySimUnits.remove(%this);
			if(%this.dono.mySimAreas.getCount() <= 0){
				if(%quemMatou $= "DESASTRE"){
					serverPlayerMorteNatural(%this.dono);
				} else {
					serverPlayerKill(%this.dono, %quemMatou);
				}
			}
			%this.safeDelete();
		}
	} else {
		serverRemoverUnidade(%jogo, %this, %this.onde);
		%this.dono.mySimUnits.remove(%this);
		if(%this.dono.mySimAreas.getCount() <= 0){
			if(%quemMatou $= "DESASTRE"){
				serverPlayerMorteNatural(%this.dono);
			} else {
				serverPlayerKill(%this.dono, %quemMatou);
			}
		}
		
		if(%this.grandeMatriarca){
			schedule(3000, 0, "serverCompletarObjetivoGulok", %quemMatou);		
		}
		
		%this.safeDelete();
	}
}

function rainha::moveToLoc(%this, %worldPos){
	%speed = 25; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}
	
/////////////Render:
function rainha::render(%this){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	
	if(%this.dono.mySimAreas.getCount() <= 0){
		%jogo.playerSuicide(%this.dono);
	}
	%this.safeDelete();
}

////////////
function rainha::fire(%this, %inimigo){
  	%inimigo.kill(%this.dono);
}
//////////
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
		 
    };
   
   return %copy;
}

function verme::kill(%this, %quemMatou){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	if(%this.dono.mySimAreas.getCount() <= 0){
		if(%quemMatou $= "DESASTRE"){
			serverPlayerMorteNatural(%this.dono);
		} else {
			serverPlayerKill(%this.dono, %quemMatou);
		}
	}
	%this.safeDelete();
}

function verme::moveToLoc(%this, %worldPos){
	%speed = 25; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}
	
/////////////Render:
function verme::render(%this){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	
	if(%this.dono.mySimAreas.getCount() <= 0){
		%jogo.playerSuicide(%this.dono);
	}
	%this.safeDelete();
}

////////////
function verme::fire(%this, %inimigo){
  	%inimigo.kill(%this.dono);
}

//Movimentação:
function verme::moverPara(%this, %area, %pos){
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

function verme::evoluir(%this){
	serverCmdEvoluirEmRainha(%this.dono.client, %this.onde.myName, %this.pos);	
}

///////////
//Cefaloks:
//Navio:
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
		 
		
    };
   
   return %copy;
}


function cefalok::moveToLoc(%this, %worldPos){
	%speed = 25;
	
	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);


   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}


function cefalok::kill(%this, %quemMatou){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	
	if(isObject(%this.myTransporte)){
		for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
			%this.myTransporte.getObject(%i).render();	
		}
	}
	
	if(%this.dono.mySimAreas.getCount() <= 0){
		if(%quemMatou $= "DESASTRE"){
			serverPlayerMorteNatural(%this.dono);
		} else {
			serverPlayerKill(%this.dono, %quemMatou);
		}
	}
	%this.safeDelete();
}

///////////////Render:
function cefalok::render(%this){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);	
	%this.dono.mySimUnits.remove(%this);
	
	if(isObject(%this.myTransporte)){
		for(%i = 0; %i < %this.myTransporte.getCount(); %i++){
			%this.myTransporte.getObject(%i).render();	
		}
	}

	if(%this.dono.mySimAreas.getCount() <= 0){
		%jogo.playerSuicide(%this.dono);
	}
	%this.safeDelete();
}
////////////////


function cefalok::fire(%this, %inimigo){
 	%inimigo.kill(%this.dono);
}


//função para usar imagens em vez de blend:
function cefalok::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	%myImage = "cefalok" @ %corDoMeuDono @ "ImageMap";
	
	%this.setImageMap(%myImage);
}



//Movimentação:
function cefalok::moverPara(%this, %area, %pos){
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

////////////////////
//Ovos:
function ovo::createCopy(%this){
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
		dado = 0;
    };
 
   return %copy;
}
function ovo::spawnUnit(%this, %unit){
	%unitBP = %unit @ "_BP";
	%newUnit = %unitBP.createCopy();
	%newUnit.gulok = true;
	
	if(%this.dono.jogo.emDuplas){
		%newUnit.dono = %this.dono.jogo.jogadorDaVez;
	} else {
		%newUnit.dono = %this.dono;
	}
	
	%this.positionUnit(%newUnit);
	%newUnit.isMoveable = 1;
	%newUnit.dono.mySimUnits.add(%newUnit); //adiciona a unit no simset de unidades do player;
	if(%newUnit.class $= "lider"){
		%newUnit.dono.mySimLideres.add(%newUnit);	
	}
	
	return %newUnit;
}
function ovo::positionUnit(%this, %unit){
	%this.onde.positionUnit(%unit);
	if(%this.dono.jogo.emDuplas){
		if(%this.dono != %unit.dono){
			%this.onde.resolverMyStatus();	
		}
	}
}
function ovo::kill(%this, %quemMatou){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	%this.dono.mySimOvos.remove(%this);
		
	if(%this.dono.mySimAreas.getCount() <= 0){
		if(%quemMatou $= "DESASTRE"){
			serverPlayerMorteNatural(%this.dono);
		} else {
			serverPlayerKill(%this.dono, %quemMatou);
		}
	}
	%this.safeDelete();
}
function ovo::render(%this){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);	
	%this.dono.mySimUnits.remove(%this);
	%this.dono.mySimOvos.remove(%this);
	
	if(%this.dono.mySimAreas.getCount() <= 0){
		%jogo.playerSuicide(%this.dono);
	}
	%this.safeDelete();
}
function ovo::eclodir(%this){
	for(%i = 0; %i < 3; %i++){
		%this.spawnUnit("verme");	
	}
	
	echo("OVO ECLODIR ONDE: " @ %this.onde.myName);
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	%this.dono.mySimOvos.remove(%this);
	%this.onde.resolverMyStatus();
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
		 
    };
    return %copy;
}


function zangao::moveToLoc(%this, %worldPos){
	%speed = 25; //no futuro cada unidade pode ter a sua

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar até o mouse e parar quando chegar lá (true):
   	%this.moveTo(%worldPos, %speed, true); 
}


function zangao::kill(%this, %quemMatou){
	%jogo = %this.dono.jogo;
	if(isObject(%this.myTransporte)){
		if(%this.myTransporte.getcount() > 0){
			%verme = %this.myTransporte.getObject(0);
			%verme.dismount();
			%this.myTransporte.remove(%verme);
			%verme.safeDelete();
		} else {
			serverRemoverUnidade(%jogo, %this, %this.onde);
			%this.dono.mySimUnits.remove(%this);
			%this.dono.mySimLideres.remove(%this);	
			%this.dono.liderVive = 0;
			if(%this.dono.mySimAreas.getCount() <= 0){
				if(%quemMatou $= "DESASTRE"){
					serverPlayerMorteNatural(%this.dono);
				} else {
					serverPlayerKill(%this.dono, %quemMatou);
				}
			}
			%this.safeDelete();
		}
	} else {
		serverRemoverUnidade(%jogo, %this, %this.onde);
		%this.dono.mySimUnits.remove(%this);
		%this.dono.mySimLideres.remove(%this);	
		%this.dono.liderVive = 0;
		if(%this.dono.mySimAreas.getCount() <= 0){
			if(%quemMatou $= "DESASTRE"){
				serverPlayerMorteNatural(%this.dono);
			} else {
				serverPlayerKill(%this.dono, %quemMatou);
			}
		}
		%this.safeDelete();
	}
}

/////////////Render:
function zangao::render(%this){
	%jogo = %this.dono.jogo;
	serverRemoverUnidade(%jogo, %this, %this.onde);
	%this.dono.mySimUnits.remove(%this);
	%this.dono.mySimLideres.remove(%this);	
	%this.dono.liderVive = 0;
	
	if(%this.dono.mySimAreas.getCount() <= 0){
		%jogo.playerSuicide(%this.dono);
	}
	
	%this.safeDelete();
}
///////////////
	
	
function zangao::fire(%this, %inimigo){
 	%inimigo.kill(%this.dono);
}


//função para usar imagens em vez de blend:
function zangao::setMyImage(%this){
	%corDoMeuDono = %this.dono.myColor;
	%myImage = "zangao1" @ %corDoMeuDono @ "ImageMap";
	
	%this.setImageMap(%myImage);
}

//Movimentação:
function zangao::moverPara(%this, %area, %pos){
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