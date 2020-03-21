// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientInvestirRecursos.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 24 de março de 2008 16:44
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

/////////Venda de recursos:
function toggleInvestirGui(){
	if($estouInvestindoInGame){
		clientDesligarInvestirRecursos();
	} else {
		clientLigarInvestirRecursos();	
	}
}

function clientLigarInvestirRecursos(){
	clientFecharIntelGui(); //fecha o gui de intel
	clientFecharPropTab();
	investirTab.setVisible(true);
	clientAtualizarPEATab();
	investirRecursos_btn.setStateOn(true);
	clientApagarChat();
	$estouInvestindoInGame = true;
}

function clientDesligarInvestirRecursos(){
	investirTab.setVisible(false);
	investirRecursos_btn.setStateOn(false);
	$estouInvestindoInGame = false;
}

function clientAskInvestirRecursos(%recurso){
	%eval = "%myRecurso = $mySelf." @ %recurso @ ";";
	eval(%eval);
	
	if(%myRecurso > 0){ //esta é a segunda verificação, o botão nem deveria ter ficado ativo caso não houvesse recursos pra investir
		clientPushServerComDot();
		commandToServer('a_investirRecurso', %recurso);
	} else {
		//o botão não poderia ter ficado ativo
	}
}

//server responde com isso clientCmd_aInvestirRecurso("minerios", true), por exemplo;
function clientCmda_InvestirRecurso(%recurso){
	%eval = "$mySelf." @ %recurso @ " -= 1;"; //subtrai da conta o recurso investido
	eval(%eval);
	
	alxPlay( investir );
		
	%pesquisa = clientFindPesquisaPorClassId($myPersona.aca_pea_id);
	
	//adiciona à pesquisa o recurso investido:
	if(%recurso $= "minerios"){
		$myPersona.aca_pea_min ++;
	} else if(%recurso $= "petroleos"){
		$myPersona.aca_pea_pet ++;
	} else if(%recurso $= "uranios"){
		$myPersona.aca_pea_ura ++;
	}	
	
	atualizarRecursosGui(); //retira os recursos investidos
	clientAtualizarPEATab(); //atualiza a pesquisa
	clientAtualizarEstatisticas(); //marca os pontos
	clientToggleRecursosBtns();
	clientPopServerComDot(); //devolve o controle pro usuário
}

///////////////////////////
function clientAtualizarPEATab(){
	if($myPersona.aca_pea_id !$= "0" && $myPersona.aca_pea_id !$= ""){
		%myPesquisa = clientFindPesquisaPorClassId($myPersona.aca_pea_id);
		
		///////////////////////
		//seta o tipo da pesquisa:
		if(%myPesquisa.tipo $= "visionario"){
			PEA_txt.text = "Visionário";
		} else if (%myPesquisa.tipo $= "lider"){
			PEA_txt.text = "Líder " @ $myPersona.aca_pea_ldr;
		} else if (%myPesquisa.tipo $= "avancadas"){
			PEA_txt.text = "Avançadas";
		} else if (%myPesquisa.tipo $= "dragnal"){
			PEA_txt.text = "Dragnal";
		} else if (%myPesquisa.tipo $= "zangao"){
			PEA_txt.text = "Zangão";
		} else {
			PEA_txt.text = %myPesquisa.tipo;	
		}
		
		//seta a pesquisa em si:		
		if(%myPesquisa.upgrade $= "ataqueMaximo"){
			%myTEXT = "Ataque Máximo";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "ataqueMinimo"){
			%myTEXT = "Ataque Mínimo";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "defesaMaxima"){
			%myTEXT = "Defesa Máxima";	
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "defesaMinima"){
			%myTEXT = "Defesa Mínima";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "prospeccao"){
			%myTEXT = "Prospecção";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "forcaD"){
			%myTEXT = "Força Diplomática";
			PEA_txt.text = %myTEXT;
		} else if(%myPesquisa.upgrade $= "canhaoOrbital"){
			%myTEXT = "Canhão Orbital";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "airDrop"){
			%myTEXT = "Air Drop";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "jetPack"){
			%myTEXT = "Jet Pack";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "lider"){
			%myTEXT = "Líder";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "carapaca"){
			%myTEXT = "Carapaça";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "mira"){
			%myTEXT = "Mira Eletrônica";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "satelite"){
			%myTEXT = "Satélite";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "crisalida"){
			%myTEXT = "Crisálida";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "furia"){
			%myTEXT = "Fúria";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "virus"){
			%myTEXT = "Vírus";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "evolucao"){
			%myTEXT = "Evolução";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "dragnal"){
			%myTEXT = "Dragnal";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else if(%myPesquisa.upgrade $= "devorarRainhas"){
			%myTEXT = "Devorar Rainhas";
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myTEXT;
		} else {
			PEA_txt.text = PEA_txt.text SPC ">>" SPC %myPesquisa.upgrade;
		}
		/////////////////////
		
		PEA_minTxt.text = $myPersona.aca_pea_min @ " / " @ %myPesquisa.cDevMin; 
		PEA_petTxt.text = $myPersona.aca_pea_pet @ " / " @ %myPesquisa.cDevPet; 
		PEA_uraTxt.text = $myPersona.aca_pea_ura @ " / " @ %myPesquisa.cDevUra; 
		
		if($myPersona.aca_pea_min >= %myPesquisa.cDevMin || $mySelf.minerios < 1){
			investirMinerio_btn.setActive(false);
		} else {
			investirMinerio_btn.setActive(true);
		}
		
		if($myPersona.aca_pea_pet >= %myPesquisa.cDevPet || $mySelf.petroleos < 1){
			investirPetroleo_btn.setActive(false);
		} else {
			investirPetroleo_btn.setActive(true);
		}
		
		if($myPersona.aca_pea_ura >= %myPesquisa.cDevUra || $mySelf.uranios < 1){
			investirUranio_btn.setActive(false);
		} else {
			investirUranio_btn.setActive(true);
		}
	} else {
		PEA_txt.text = "NÃO HÁ";
		
		PEA_minTxt.text = 0;
		PEA_petTxt.text = 0;
		PEA_uraTxt.text = 0;
		investirMinerio_btn.setActive(false);
		investirPetroleo_btn.setActive(false);
		investirUranio_btn.setActive(false);
	}
}