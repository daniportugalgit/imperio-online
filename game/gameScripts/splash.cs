// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\splash.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quinta-feira, 8 de janeiro de 2009 19:26
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

// Load splash screen
function loadSplash()
{
    // Load game
    if ($splash == 0)
    {
        resetCanvas();
		Canvas.setContent(mainScreenGui);
		canvas.pushDialog(genericSplash);
		execGuis();
		execGameScripts();
		schedule(500, 0, "startGame", $defaultScene);
		
		//tem que apagar o geric splash, senao ele fica sobre o mapa na hora de escolher objetivos
    }
   
    // Display splash screen
    else 
    {
        splashGUI.bitmap = "~/data/splash/logo" @ $splash @ ".png";
        canvas.popDialog(splashGUI);
        canvas.pushDialog(splashGUI);
        schedule(100, 0, checkSplash);
        $splash--;
    }

}

// Check to see if splash screen should be loaded
function checkSplash()
{
   if (splashGUI.done)
      loadSplash();
    else
      schedule(100, 0, checkSplash);
}