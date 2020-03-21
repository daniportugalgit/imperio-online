//---------------------------------------------------------------------------------------------
// Torque Game Builder
// Copyright (C) GarageGames.com, Inc.
//---------------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------------
// startGame
// All game logic should be set up here. This will be called by the level builder when you
// select "Run Game" or by the startup process of your game to load the first level.
//---------------------------------------------------------------------------------------------
function startGame(%level)
{
   //Canvas.setContent(mainScreenGui);
   Canvas.setCursor(DefaultCursor);
   
   new ActionMap(moveMap);   
   moveMap.push();
   
   $enableDirectInput = true;
   activateDirectInput();
   enableJoystick();
   
   sceneWindow2D.loadLevel(%level);

	exec("./exec.cs"); //executa todos os arquivos
        Canvas.pushDialog(clientStartGui);
	if($IAmServer $= "0"){
		pushVideoOptionsMenu();
	} 
}

//---------------------------------------------------------------------------------------------
// endGame
// Game cleanup should be done here.
//---------------------------------------------------------------------------------------------
function endGame(){
   sceneWindow2D.endLevel();
   moveMap.pop();
   moveMap.delete();
   //flushTextureCache();
}

function fecharImperio(){
	canvas.popdialog(loggedInGui);
	canvas.popdialog(atrioGui);
	canvas.popdialog(newSalaInsideGui);
	canvas.popDialog(genericSplash);
	canvas.pushDialog(genericSplash);
	schedule(500, 0, "endGame");
	schedule(1500, 0, "quit");
}
