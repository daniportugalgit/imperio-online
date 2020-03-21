// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientKeyBind.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sábado, 8 de março de 2008 20:04
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function loadDev_keyBind(){
	moveMap.bindCmd(keyboard, "alt l", "dev_login();", "doNothing();");	
	moveMap.bindCmd(keyboard, "escape", "clientPressEsc();", "doNothing();");
	moveMap.bindCmd(keyboard, "alt k", "clientToggleKickBtns();", "doNothing();");
}

function Universo_cls::onLevelLoaded(%this, %scenegraph){ //quando o level é carregado, executa esta função uma vez
	//moveMap.bindCmd(keyboard, "LEFT", "cameraLeft();", "doNothing();");
    //moveMap.bindCmd(keyboard, "RIGHT", "cameraRight();", "doNothing();");
    moveMap.bindCmd(keyboard, "UP", "tryToSetNormalZoom();", "doNothing();");
    moveMap.bindCmd(keyboard, "DOWN", "setMouseZoom();", "doNothing();");
	//moveMap.bindCmd(keyboard, "DOWN", "toggleConsole();", "doNothing();");
	
	
	
	//teclas especiais:
	moveMap.bindCmd(keyboard, "LSHIFT", "ligarShift();", "desligarShift();");
	moveMap.bindCmd(keyboard, "DELETE", "doNothing();", "clientRenderUnidadeQuestion();");
	moveMap.bindCmd(keyboard, "shift DELETE", "clientRenderUnidadeQuestion();", "doNothing();");
	moveMap.bindCmd(keyboard, "ctrl a", "clientMostrarAcademiaDados();", "clientApagarAcademiaDados();");
	moveMap.bindCmd(keyboard, "ctrl UP", "tryToSetNormalZoom();", "doNothing();");
	moveMap.bindCmd(keyboard, "ctrl DOWN", "setMouseZoom();", "doNothing();");
	GlobalActionMap.bind(keyboard, "ctrl z", clientAskUndo); //impede que o client use o ctrl+z no chat - não impede naum! :(
}


function writeXmlToFile( %file ){
    %xml = new ScriptObject() { class = "XML"; };
	
	%xml.beginWrite( expandFilename("~/gameScripts/client/" @ %file @ ".xml") );
		%xml.writeClassBegin( "TorqueGameConfiguration" );
			%xml.writeField( "Company", $Game::CompanyName );
			%xml.writeField( "GameName", $Game::ProductName );
			%xml.writeField( "Resolution", $Game::Resolution );
			%xml.writeField( "FullScreen", $Game::FullScreen );
			%xml.writeField( "CommonVer", $Game::CommonVersion );
			%xml.writeField( "ConsoleKey", $Game::ConsoleBind );
			%xml.writeField( "ScreenShotKey", $Game::ScreenshotBind );
			%xml.writeField( "FullscreenKey", $Game::FullscreenBind );
			%xml.writeField( "UsesNetwork", $Game::UsesNetwork );
			%xml.writeField( "UsesAudio", $Game::UsesAudio );
			%xml.writeField( "DefaultScene", $Game::DefaultScene );
		%xml.writeClassEnd();
	%xml.endWrite();
   	
	echo("XML == " @ %xml);
	// Delete the object
	%xml.delete();
}


