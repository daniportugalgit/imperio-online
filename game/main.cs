//---------------------------------------------------------------------------------------------
// Torque Game Builder
// Copyright (C) GarageGames.com, Inc.
//---------------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------------
// initializeProject
// Perform game initialization here.
//---------------------------------------------------------------------------------------------

function initializeProject(){
    $splash = 1; //número de splash screens:
	exec("~/gamescripts/guiProfiles.cs");
    exec("~/gui/splash.gui");    //carrega o gui de splashScreens            
    exec("./gameScripts/splash.cs");  //carrega o código das SplashScreens
	exec("~/gui/genericSplash.gui");  //carrega a tela de "Processando";
	exec("~/gui/mainScreenGui.gui");

	//carrega a splashScreen antes de carregar o resto jogo:
    $defaultScene = "game/data/levels/terra.t2d";
    schedule(200, 0, "loadSplash");
	//schedule(800, 0, "alxPlay", "tema_2");
	
	//schedule(1000, 0, "execGuis");
	//schedule(2000, 0, "execGameScripts");
}

function execGuis(){
	// Load up the in game gui.
   exec("~/gui/objetivosGuii.gui");
   exec("~/gui/escolhaDeCores.gui");
   exec("~/gui/aguardandoObjGui.gui");  
   exec("~/gui/baterGui.gui");
   exec("~/gui/clientStartGui.gui");
   exec("~/gui/doarGui.gui");
   exec("~/gui/atrioGui.gui");
   exec("~/gui/loggedInGui.gui");
   exec("~/gui/newSalaInsideGui.gui");
   exec("~/gui/aguardeMsgBoxGui.gui");
   exec("~/gui/renderTodosMsgBoxGui.gui");
   exec("~/gui/serverComDotGui.gui");
   exec("~/gui/escolhaDeComandantesGui.gui");
   exec("~/gui/codinomeJahExisteGui.gui");
   exec("~/gui/confirmarDeletarMsgBoxGui.gui");
   exec("~/gui/academiaGui.gui");
   exec("~/gui/configAcademiaMsgBoxGui.gui");
   exec("~/gui/pesquisaCompletaMsgBoxGui.gui");
   exec("~/gui/pesquisaInexistenteMsgBoxGui.gui");
   exec("~/gui/MsgBoxOKPadraoGui.gui");
   exec("~/gui/doacaoEfetuadaGui.gui");
   exec("~/gui/sortearOrdemGui.gui");
   exec("~/gui/diplomaciaQGui.gui");
   exec("~/gui/emboscadaQGui.gui");
   exec("~/gui/TUTmsgGui.gui");
   exec("~/gui/tutHudGui.gui");
   exec("~/gui/carregandoGruposGui.gui");
   exec("~/gui/tutSelectGui.gui");
   exec("~/gui/tut2Gui.gui");
   exec("~/gui/invDefGui.gui");
   exec("~/gui/criarDuplasGui.gui");
   exec("~/gui/confirmarCompraGui.gui");
   exec("~/gui/capturaQGui.gui");
   exec("~/gui/pokerGui.gui");
   exec("~/gui/pk_sorteioGui.gui");
   exec("~/gui/confirmarKickGui.gui");
   exec("~/gui/tiposDeJogoGui.gui");
   exec("~/gui/pk_fimSolitarioGui.gui");
   exec("~/gui/sincronizandoGui.gui");
   exec("~/gui/confirmarFugirGui.gui");
   exec("~/gui/msgBoxOKTGui.gui");
   exec("~/gui/msgBoxOKT3Gui.gui");
}

function execGameScripts(){
	// Exec game scripts.
   exec("./gameScripts/game.cs");
}

//---------------------------------------------------------------------------------------------
// shutdownProject
// Clean up your game objects here.
//---------------------------------------------------------------------------------------------
function shutdownProject()
{
   endGame();
}

//---------------------------------------------------------------------------------------------
// setupKeybinds
// Bind keys to actions here..
//---------------------------------------------------------------------------------------------
function setupKeybinds()
{
   new ActionMap(moveMap);
   //moveMap.bind("keyboard", "a", "doAction", "Action Description");
   //moveMap.bind("keyboard", "ENTER", "jogoChatGuiSend();", "SendChat;");
}
