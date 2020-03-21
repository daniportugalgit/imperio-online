// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientIntel.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 25 de março de 2008 12:57
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientSetIntel(){
	$mySelf.prospeccao = $myPersona.aca_c_1;
	
	if($myPersona.aca_v_1 > 0 && $tipoDeJogo !$= "semPesquisas" && $myPersona.especie $= "humano"){
		//ligar o ícone de intel:
		intelHudIcon.setActive(true);
		i_prospeccao_btn.setVisible(true);		
		i_filantropia_btn.setVisible(true);		
		i_almirante_btn.setVisible(true);	
		if($myPersona.aca_c_1 > 0 && ($myPersona.myComerciante > 89 || $salaEmQueEstouTipoDeJogo $= "poker")){
			i_prospeccao_btn.setActive(true);	
		} else {
			i_prospeccao_btn.setActive(false);		
		}
		if($myPersona.aca_i_2 > 0){
			i_filantropia_btn.setActive(true);	
			clientSetFilantropia();
		} else {
			i_filantropia_btn.setActive(false);		
		}
		if($myPersona.aca_i_3 > 0){
			i_almirante_btn.setActive(true);	
		} else {
			i_almirante_btn.setActive(false);		
		}
		if($planetaAtual $= "Terra"){
			i_minerios_btn.setActive(true);	
			i_petroleos_btn.setActive(true);	
			i_uranios_btn.setActive(true);	
		} else {
			if($myPersona.aca_v_1 > 1){	
				i_minerios_btn.setActive(true);	
				i_petroleos_btn.setActive(true);	
				i_uranios_btn.setActive(true);		
			} else {
				i_minerios_btn.setActive(false);	
				i_petroleos_btn.setActive(false);	
				i_uranios_btn.setActive(false);	
			}
		}
		
		i_mais_btn.setActive(false); //por enquanto só há duas missões especiais, não precisa do btn mais, ele fica inativo;
	} else if($myPersona.especie $= "gulok"){
		intelHudIcon.setActive(true);
		i_prospeccao_btn.setVisible(false);		
		i_filantropia_btn.setVisible(false);		
		i_almirante_btn.setVisible(false);		
		
		i_minerios_btn.setActive(true);	
		i_petroleos_btn.setActive(true);	
		i_uranios_btn.setActive(true);	
		
		i_mais_btn.setActive(false);
	} else {
		//desligar o ícone de intel:
		intelHudIcon.setActive(false);
	}
}

function clientIntelHudBtnClick(){
	if($intelHudOn){
		clientFecharIntelGui();
	} else {
		clientAbrirIntelGui();
	}
}

function clientFecharIntelGui(){
	intelTab.setVisible(false);
	intelHudIcon.setStateOn(false);
	$intelHudOn = false;
	i_apagarMinerios();
	i_apagarPetroleos();
	i_apagarUranios();
	clientFecharFilantropiaGui();
	clientApagarAlmiranteTab();
}

function clientAbrirIntelGui(){
	intelTab.setVisible(true);
	clientFecharFilantropiaGui();
	clientApagarAlmiranteTab();
	clientApagarChat();
	clientFecharPropTab();
	intelHudIcon.setStateOn(true);
	$intelHudOn = true;
}


//Para ver todas as missões de determinado tipo:
$myIntelMarkersMineriosOn = false;
$myIntelMarkersPetroleosOn = false;
$myIntelMarkersUraniosOn = false;

function i_mostrarMinerios(){
	if($planetaAtual $= "Ungart"){
		%limit = 86;
	} else {
		%limit = 79;
	}
	for (%i = 1; %i < %limit; %i++){
		%info = clientFindInfo(%i);
		if (%info.bonusM > 0){
			%eval = "%areaDaMissao =" SPC %info.area @ ";";
			eval(%eval);	
		
			%newAlvo = missaoMarkerMinerio.clone();	
			%newAlvo.setPosition(%areaDaMissao.pos1);
			%name = "markerMinerio" @ %i;
			%newAlvo.setName(%name);
			%newAlvo.setLayer(0);
		}
	}
	$myIntelMarkersMineriosOn = true;	
	i_minerios_btn.setStateOn(true);
}

function i_apagarMinerios(){
	if($planetaAtual $= "Ungart"){
		%limit = 86;
	} else {
		%limit = 79;
	}
	for (%i = 1; %i < %limit; %i++){
		%info = clientFindInfo(%i);
		if (%info.bonusM > 0){
			%eval = "%mission = markerMinerio" @ %i @ ";";
			eval(%eval);
			
			if(isObject(%mission)){
				%mission.safeDelete();
			}
		}
	}
	$myIntelMarkersMineriosOn = false;
	i_minerios_btn.setStateOn(false);
}

function i_mostrarPetroleos(){
	if($planetaAtual $= "Ungart"){
		%limit = 86;
	} else {
		%limit = 79;
	}
	for (%i = 1; %i < %limit; %i++){
		%info = clientFindInfo(%i);
		if (%info.bonusP > 0){
			%eval = "%areaDaMissao =" SPC %info.area @ ";";
			eval(%eval);	
			
			%newAlvo = missaoMarkerPetroleo.clone();	
			%newAlvo.setPosition(%areaDaMissao.pos1);
			%name = "markerPetroleo" @ %i;
			%newAlvo.setName(%name);
			%newAlvo.setLayer(0);
		}
	}
	$myIntelMarkersPetroleosOn = true;
	i_petroleos_btn.setStateOn(true);
}

function i_apagarPetroleos(){
	if($planetaAtual $= "Ungart"){
		%limit = 86;
	} else {
		%limit = 79;
	}
	for (%i = 1; %i < %limit; %i++){
		%info = clientFindInfo(%i);
		if (%info.bonusP > 0){
			%eval = "%mission = markerPetroleo" @ %i @ ";";
			eval(%eval);
			
			if(isObject(%mission)){
				%mission.safeDelete();
			}
		}
	}
	$myIntelMarkersPetroleosOn = false;
	i_petroleos_btn.setStateOn(false);
}

function i_mostrarUranios(){
	if($planetaAtual $= "Ungart"){
		%limit = 86;
	} else {
		%limit = 79;
	}
	for (%i = 1; %i < %limit; %i++){
		%info = clientFindInfo(%i);
		if (%info.bonusU > 0){
			%eval = "%areaDaMissao =" SPC %info.area @ ";";
			eval(%eval);	
		
			%newAlvo = missaoMarkerUranio.clone();	
			%newAlvo.setPosition(%areaDaMissao.pos1);
			//%newAlvo.setAutoRotation(-15);
			%name = "markerUranio" @ %i;
			%newAlvo.setName(%name);
			%newAlvo.setLayer(0);
		}
	}	
	$myIntelMarkersUraniosOn = true;
	i_uranios_btn.setStateOn(true);
}

function i_apagarUranios(){
	if($planetaAtual $= "Ungart"){
		%limit = 86;
	} else {
		%limit = 79;
	}
	for (%i = 1; %i < %limit; %i++){
		%info = clientFindInfo(%i);
		if (%info.bonusU > 0){
			%eval = "%mission = markerUranio" @ %i @ ";";
			eval(%eval);
			
			if(isObject(%mission)){
				%mission.safeDelete();
			}
		}
	}
	$myIntelMarkersUraniosOn = false;
	i_uranios_btn.setStateOn(false);
}

function toggleIntelMinerios(){
	if($myIntelMarkersMineriosOn){
		i_apagarMinerios();
	} else {
		i_mostrarMinerios();
		i_apagarPetroleos();
		i_apagarUranios();
	}
}

function toggleIntelPetroleos(%terreno){
	if($myIntelMarkersPetroleosOn){
		i_apagarPetroleos();
	} else {
		i_apagarMinerios();
		i_mostrarPetroleos();
		i_apagarUranios();
	}		
}

function toggleIntelUranios(%terreno){
	if($myIntelMarkersUraniosOn){
		i_apagarUranios();
	} else {
		i_apagarMinerios();
		i_apagarPetroleos();
		i_mostrarUranios();
	}		
}

function clientCmdi_intelVendeu(%QuemVendeu, %vendeuOQue){
	i_intelVendeuGui.setVisible(true);
	i_intelVendeuQuem_txt.text = %quemVendeu @ " vendeu Recursos";
	
	i_intelVendeuGui.bitmap = "game/data/images/academia/i_vendeu" @ %vendeuOQue;
	
	alxPlay(intelVendeu);
	
	cancel($i_vendeuSchedule);
	$i_vendeuSchedule = schedule(4000, 0, "i_apagarIntelVendeu");
}

function clientCmdi_intelOculto(%QuemFez, %fezOQue, %comOQue){
	i_intelVendeuGui.setVisible(true);
	i_intelVendeuQuem_txt.text = %QuemFez @ " " @ %fezOQue @ " uma " @ %comOQue;
	
	i_intelVendeuGui.bitmap = "game/data/images/academia/i_blank";
	
	alxPlay(intelVendeu);
	
	cancel($i_vendeuSchedule);
	$i_vendeuSchedule = schedule(4000, 0, "i_apagarIntelVendeu");
}

function i_apagarIntelVendeu(){
	i_intelVendeuGui.setVisible(false);
}

function clienti_prospeccao_BtnClick(){
	if($mySelf.prospeccao > 0 && $mySelf.imperiais > 1){
		$mySelf.prospeccao--;
		$mySelf.imperiais -= 2;
		atualizarImperiaisGui();
		commandToServer('i_prospeccao');
		if($mySelf.prospeccao < 1){
			i_prospeccao_btn.setActive(false);	
		}
	}
}