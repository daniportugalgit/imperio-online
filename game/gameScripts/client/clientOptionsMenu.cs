// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientOptionsMenu.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 4 de janeiro de 2008 16:23
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

//não é uma função pra abrir uma tela, e sim para setar as preferências. Se não houver preferências, abre a tela:
function pushVideoOptionsMenu(){
	if(!isFile("game/data/files/myVideoPrefs.sav")){
		optionsTelaMenu.setVisible(true);
		//Vídeo:
		//seta pra 800x600 inicialmente e grava isso no arquivo:
		$videoRadio = 1;
		optionsTelaNormal_rdb.setStateOn(true);
		
		//Áudio:
		//seta pra normal inicialmente e grava isso no arquivo:
		$audioRadio = 1;
		optionsAudio1_rdb.setStateOn(true);
		
		//aplica as opções de ambos:
		clientApplyVideoPref();
	} else {
		//não abre a tela, seta as preferências:
		%file = new FileObject();
		%file.openForRead("game/data/files/myVideoPrefs.sav"); 
		$myVideoPrefX = %file.readLine();
		$myVideoPrefY = %file.readLine();
		$myAudioPref = %file.readLine();
		$audioRadio = $myAudioPref;
		setarAudio();
		optionsTelaMenu.setVisible(false);	
		
		%myWindowResX = sceneWindow2d.getWindowExtents();
		%resolucaoXAtual = getWord(%myWindowResX, 2);
				
		if(%resolucaoXAtual !$= $myVideoPrefX){
			echo("Re-Setting Resolution");
			setScreenMode($myVideoPrefX, $myVideoPrefY, 32, false);
			//setRes($myVideoPrefX, $myVideoPrefY);
			%windowRes = $myVideoPrefX SPC $myVideoPrefY SPC "32";
			$Game::Resolution = %windowRes;
		}
		%file.close();
		%file.delete();
		
		//se a tela estiver em 800x600, traz a msg de que a resolução está muito baixa:
		%myWindowResX = sceneWindow2d.getWindowExtents();
		%resolucaoXAtual = getWord(%myWindowResX, 2);
		if(%resolucaoXAtual == 800){
			clientPushBaixaResMsg();	
		}
	}
}

function clientPushBaixaResMsg()
{
	clientMsgBoxOKT("RESOLUÇÃO BAIXA!", "CLIQUE EM OPÇÕES CASO QUEIRA AUMENTA-LA.");
}

function clientSetVideoRadio(%num){
	$videoRadio = %num;
}

function clientSetAudioRadio(%num){
	$audioRadio = %num;
}

function setarAudio(){
	switch$ ($audioRadio){
		case "1": //normal
		$noSound = false;
		$semSomDasMissoes = false;
		$myAudioPref = 1;
				
		case "2": //semSomDasMissoes
		$noSound = false;
		$semSomDasMissoes = true;
		$myAudioPref = 2;
				
		case "3": //noSound
		$noSound = true;
		$semSomDasMissoes = false;
		$myAudioPref = 3;
	}	
}

//aplica as opções de vídeo e de áudio simultaneamente:
function clientApplyVideoPref(){
	%myWindowResX = sceneWindow2d.getWindowExtents();
	%resolucaoXAntiga = getWord(%myWindowResX, 2);
		
	switch$ ($videoRadio){
		case "1":
		setScreenMode(800, 600, 32, false);
		$myVideoPrefX = 800;
		$myVideoPrefY = 600;
				
		case "2":
		$myVideoPrefX = 1024;
		$myVideoPrefY = 768;
		setScreenMode(1024, 768, 32, false);
				
		case "3":
		setScreenMode(1280, 960, 32, false);
		$myVideoPrefX = 1280;
		$myVideoPrefY = 960;
	}
	
	setarAudio();
	
	%file = new FileObject();
	%file.openForWrite("game/data/files/myVideoPrefs.sav"); 
	%file.writeLine($myVideoPrefX); 
	%file.writeLine($myVideoPrefY); 
	%file.writeLine($myAudioPref); 
	%file.close(); //fecha o myVideoPrefs.sav
	%file.delete(); //deleta o arquivo da memória RAM, não do HD;
	
	%windowRes = $myVideoPrefX SPC $myVideoPrefY SPC "32";
	$Game::Resolution = %windowRes;
	
	clientFecharOptionsMenu();
	
	if(%resolucaoXAntiga !$= $myVideoPrefX)
	{
		clientMsgBoxOKT3("TAMANHO ALTERADO", "É RECOMENDADO FECHAR E ABRIR O JOGO NOVAMENTE.");
	}
}


function clientCallOpcoesMenu(){
	optionsTelaMenu.setVisible(true);
	%myWindowResX = sceneWindow2d.getWindowExtents();
	%myWindowResX = getWord(%myWindowResX, 2);
	
	switch$ (%myWindowResX){
		case "800":
		optionsTelaMenor_rdb.setStateOn(true);
		clientSetVideoRadio(1);
		
		case "1024":
		optionsTelaNormal_rdb.setStateOn(true);
		clientSetVideoRadio(2);
		
		case "1280":
		optionsTelaGrande_rdb.setStateOn(true);
		clientSetVideoRadio(3);
	}
	
	switch$ ($myAudioPref){
		case "1":
		optionsAudio1_rdb.setStateOn(true);
		clientSetAudioRadio(1);
		
		case "2":
		optionsAudio2_rdb.setStateOn(true);
		clientSetAudioRadio(2);
		
		case "3":
		optionsAudio3_rdb.setStateOn(true);
		clientSetAudioRadio(3);
	}
}

function calcularNaRes(%valorOriginal, %resolucaoDestino){
	%x = mFloor(%valorOriginal * %resolucaoDestino) / 1024;
	return %x;
}

function clientFecharOptionsMenu(){
	optionsTelaMenu.setVisible(false);	
}

function clientPressEsc()
{
	if($vendoPoker)
	{
		clientPokerEsc();
		return;
	}
		
	if($vendoCommSelect)
		clientAskDesconectar();
}



////////////////


