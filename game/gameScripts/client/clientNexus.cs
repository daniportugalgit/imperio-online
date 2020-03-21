// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientNexus.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 23 de novembro de 2008 4:50
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientNexusCallHud(){
	nexusTab.setVisible(true);
	if(($myPersona.aca_v_6 == 3 && $myPersona.especie $= "humano") || $myPersona.especie $= "gulok"){
		//echo("Configurando hud do Nexus Alquímico");	
		nexus_tab_ajuda_btn.setActive(true);
		%myLvl = mFloor($myPersona.aca_art_2 / 50);
		nexus_tab_pesquisa_txt.text = $myPersona.aca_art_2 @ " / 150";
		
		nexus_tab_nivel_txt.text = %myLvl @ " / 3";
		
		%myMinPercent = 80 - (%myLvl * 20);
		%myPetUraPercent = 20 + (%myLvl * 20);
		nexus_tab_porcentagem_pet_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_alquimia_percent_" @ %myMinPercent @ %myPetUraPercent;
		nexus_tab_porcentagem_ura_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_alquimia_percent_" @ %myMinPercent @ %myPetUraPercent;
		nexus_tab_travado_img.setVisible(false);
		
		clientNexusCalcularGauge();
		clientVerifyNexusBtns();
	} else {
		//echo("Pesquisa Inexistente: Planetas Nível 3");
		nexus_tab_ajuda_btn.setActive(false);
		nexus_tab_travado_img.setVisible(true);
		nexus_tab_investir_btn.setActive(false);
		nexus_tab_pesquisa_txt.text = "0 / 150";
		nexus_tab_nivel_txt.text = "0 / 3";
		clientNexusCalcularGauge();
	}	
}

function clientVerifyNexusBtns(){
	clientNexusVerificarInvestir();
	if($mySelf.imperiais >= 3 && $mySelf.alquimias == 0){
		nexus_tab_alquimia_pet_btn.setActive(true);
		nexus_tab_alquimia_ura_btn.setActive(true);
	} else {
		nexus_tab_alquimia_pet_btn.setActive(false);
		nexus_tab_alquimia_ura_btn.setActive(false);
	}
}

function clientNexusCalcularGauge(){
	%lvl = mFloor($myPersona.aca_art_2 / 50);
	
	nexus_tab_nivel_1_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_tab_nivel_1_normal";
	nexus_tab_nivel_2_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_tab_nivel_2_normal";
	nexus_tab_nivel_3_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_tab_nivel_3_normal";
	
	//níveis:
	if(%lvl > 0){
		nexus_tab_nivel_1_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_tab_nivel_1_ativo"; 
		if(%lvl > 1){
			nexus_tab_nivel_2_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_tab_nivel_2_ativo"; 
			if(%lvl > 2){
				nexus_tab_nivel_3_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_tab_nivel_3_ativo"; 
			}
		}
	}
	
	//barras:
	nexus_fillBarras(%lvl);
}

function nexus_fillBarras(%lvl){
	if($primeiroJogoDaSessao){
		//RES::DEFAULT(1024x768):
		%myX = 67;
		%myY = 11;
	} else {
		%myWindowResX = sceneWindow2d.getWindowExtents();
		$myWindowResX = getWord(%myWindowResX, 2);
		%myX = calcularNaRes(67, $myWindowResX);
		%myY = calcularNaRes(11, $myWindowResX);
	}
	
	%myInvest = $myPersona.aca_art_2;
	
	nexus_tab_barra_1_img.setVisible(true);	
	nexus_tab_barra_2_img.setVisible(false);
	nexus_tab_barra_3_img.setVisible(false);
	if(%myInvest > 50){
		%myInvestTemp = 50;
	} else {
		%myInvestTemp = %myInvest;
	}
	%extX = mFloor((%myInvestTemp / 50) * %myX);
	%ext = %extX SPC %myY;
	nexus_tab_barra_1_img.extent = %ext;	
		
	if(%lvl >= 1){
		nexus_tab_barra_2_img.setVisible(true);
		if(%myInvest >= 100){
			%myInvestTemp = 50;
		} else {
			%myInvestTemp = %myInvest - 50;
		}
		%extX = mFloor((%myInvestTemp / 50) * %myX);
		%ext = %extX SPC %myY;
		nexus_tab_barra_2_img.extent = %ext;
	}
	if(%lvl >= 2){
		nexus_tab_barra_3_img.setVisible(true);
		if(%myInvest >= 150){
			%myInvestTemp = 50;
		} else {
			%myInvestTemp = %myInvest - 100;
		}
		%extX = mFloor((%myInvestTemp / 50) * %myX);
		%ext = %extX SPC %myY;
		nexus_tab_barra_3_img.extent = %ext;
	}
}

function clientNexusVerificarInvestir(){
	if($mySelf.minerios > 0 && $mySelf.petroleos > 0 && $mySelf.uranios > 0){
		if($myPersona.aca_art_2 >= 150){
			nexus_tab_investir_btn.setActive(false);
		} else {
			if($numDePlayersNestaPartida > 2 && !$jogoEmDuplas){
				nexus_tab_investir_btn.setActive(true);		
			} else if($numDePlayersNestaPartida == 6 && $jogoEmDuplas){
				nexus_tab_investir_btn.setActive(true);	
			} else {
				nexus_tab_investir_btn.setActive(false);
			}
		}
	} else {
		nexus_tab_investir_btn.setActive(false);		
	}
}

function clientAskAlquimia(%recursoNum){
	if($mySelf.imperiais >= 3){
		clientPushServerComDot();
		commandToServer('alquimia', %recursoNum);
	}
}

function clientCmdAlquimia(%recurso){
	$mySelf.imperiais -= 3;	
	if(%recurso $= "minerios"){
		$mySelf.minerios += 1;
		%fireFX_img = "~/data/particles/minerioFind.eff";
		%sound = "achouMinerio";
	} else if(%recurso $= "petroleos"){
		$mySelf.petroleos += 1;
		%fireFX_img = "~/data/particles/petroleoFind.eff";
		%sound = "achouPetroleo";
	} else if(%recurso $= "uranios"){
		$mySelf.uranios += 1;
		%fireFX_img = "~/data/particles/uranioFind.eff";
		%sound = "achouUranio";
	}
	
	%fireFX = new t2dParticleEffect() { scenegraph = $nexus.scenegraph; };
	%fireFX.loadEffect(%fireFX_img);
	%fireFX.mount(nexus_hp);
	%fireFX.setEffectLifeMode("KILL", 1);
	%fireFX.playEffect();
	
	if(!$noSound){
		if(!$semSomDasMissoes){
			alxPlay(%sound);
		}
	}
	
	atualizarRecursosGui();
	atualizarImperiaisGui();
	clientAtualizarEstatisticas();
	$mySelf.alquimias++;
	clientVerifyNexusBtns();
	clientPopServerComDot();
}

function clientAskInvestirNexus(){
	if($mySelf.minerios > 0 && $mySelf.petroleos > 0 && $mySelf.uranios > 0){ //esta é a segunda verificação, o botão nem deveria ter ficado ativo caso não houvesse recursos pra investir
		if($myPersona.aca_art_2 < 150){
			clientPushServerComDot();
			commandToServer('nexus_investir');
		}
	} else {
		//o botão não poderia ter ficado ativo
	}
}

//server responde com isso clientCmd_aInvestirRecurso("minerios", true), por exemplo;
function clientCmdNexus_investir(){
	$mySelf.minerios -= 1;
	$mySelf.petroleos -= 1;
	$mySelf.uranios -= 1;
		
	alxPlay( investir );
		
	$myPersona.aca_art_2++;
		
	atualizarRecursosGui(); //retira os recursos investidos
	clientAtualizarPEATab(); //atualiza a pesquisa
	clientAtualizarEstatisticas(); //marca os pontos
	clientToggleRecursosBtns(); //verifica os btns de venda de recursos
	clientNexusCallHud(); //recalcula o hud do nexus
	clientPopServerComDot(); //devolve o controle pro usuário
}