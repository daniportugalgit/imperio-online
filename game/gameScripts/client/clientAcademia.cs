// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientAcademia.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 19 de março de 2008 16:58
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientClearAcademia(){
	//Líderes:
	for(%i = 1; %i < 4; %i++){
		%eval = "a_lider" @ %i @ "On_tab.setVisible(false);";
		eval(%eval);
			
		%eval = "%myEscudoTab = a_lider" @ %i @ "EscudoOn_tab;";
		eval(%eval);
		%eval = "%myEscudoMicon = a_lider" @ %i @ "Escudo_micon;";
		eval(%eval);
		%eval = "%myJetPackTab = a_lider" @ %i @ "JetPackOn_tab;";
		eval(%eval);
		%eval = "%myJetPackMicon = a_lider" @ %i @ "JetPack_micon;";
		eval(%eval);
		%eval = "%mySniperTab = a_lider" @ %i @ "SniperOn_tab;";
		eval(%eval);
		%eval = "%mySniperMicon = a_lider" @ %i @ "Sniper_micon;";
		eval(%eval);
		%eval = "%myMoralTab = a_lider" @ %i @ "MoralOn_tab;";
		eval(%eval);
		%eval = "%myMoralMicon = a_lider" @ %i @ "Moral_micon;";
		eval(%eval);
			
		%myEscudoTab.setVisible(false);
		%myEscudoMicon.setVisible(false);
		%myJetPackTab.setVisible(false);
		%myJetPackMicon.setVisible(false);
		%mySniperTab.setVisible(false);
		%mySniperMicon.setVisible(false);
		%myMoralTab.setVisible(false);
		%myMoralMicon.setVisible(false);
		
		%eval = "a_lider" @ %i @ "_btn.setActive(false);";
		eval(%eval);
	}
	
	if($myPersona.especie $= "humano"){
		a_soldados_btn.setVisible(true);
		a_tanques_btn.setVisible(true);
		a_navios_btn.setVisible(true);
		a_vermes_btn.setVisible(false);	
		a_rainhas_btn.setVisible(false);	
		a_cefaloks_btn.setVisible(false);	
		
		a_lider1_btn.setBitmap("game/data/images/academia/abtn_lider1");
		a_lider2_btn.setBitmap("game/data/images/academia/abtn_lider2");
		a_lider3_btn.setBitmap("game/data/images/academia/abtn_lider3");
	} else if($myPersona.especie $= "gulok"){
		a_soldados_btn.setVisible(false);
		a_tanques_btn.setVisible(false);
		a_navios_btn.setVisible(false);
		a_vermes_btn.setVisible(true);	
		a_rainhas_btn.setVisible(true);	
		a_cefaloks_btn.setVisible(true);	
		
		a_lider1_btn.setBitmap("game/data/images/academia/abtn_zangaoPreto");
		a_lider2_btn.setBitmap("game/data/images/academia/abtn_zangaoBranco");
		a_lider3_btn.setBitmap("game/data/images/academia/abtn_dragnal");
	}
}

function clientEntrarNaAcademia(%deOnde){
	clientClearAcademia(); //limpa a parte dos líderes;
	if(!$noSound)
		$tema_academia = alxPlay(tema_0);
		
	//troca os dialogs do Canvas:
	Canvas.popDialog(loggedInGui);
	Canvas.popDialog(atrioGui);
	Canvas.pushDialog(academiaGui);	
	$vendoAcademia = true; //marca que o client está vendo a academia
	
	if(%deOnde $= "loggedIn"){
		$primeiraLoggedIn = false;	
	} else if(%deOnde $= "atrio"){
		$primeiraAtrio = false;	
		commandToServer('SairDoAtrio');
	}
	
	//seleciona a imagem de fundo correta:
	if($myPersona.especie $= "humano"){
		acadamiaBase_img.bitmap = "game/data/images/academia/academiaImperial";
		a_lider1On_tab.bitmap = "game/data/images/academia/atab_lider1On";
		a_lider2On_tab.bitmap = "game/data/images/academia/atab_lider2On";
		a_lider3On_tab.bitmap = "game/data/images/academia/atab_lider3On";
		
		a_lider1Escudo_micon.tooltip = "Escudo";
		a_lider2Escudo_micon.tooltip = "Escudo";
		a_lider3Escudo_micon.tooltip = "Escudo";
		a_lider1JetPack_micon.tooltip = "Jet Pack";
		a_lider2JetPack_micon.tooltip = "Jet Pack";
		a_lider3JetPack_micon.tooltip = "Jet Pack";
		a_lider1Sniper_micon.tooltip = "Sniper";
		a_lider2Sniper_micon.tooltip = "Sniper";
		a_lider3Sniper_micon.tooltip = "Sniper";
		a_lider1Moral_micon.tooltip = "Moral";
		a_lider2Moral_micon.tooltip = "Moral";
		a_lider3Moral_micon.tooltip = "Moral";
		
		a_comerciante_btn.ToolTip = "REQUISITOS: Intel e Comerciante 90%+";
		a_intel_btn.ToolTip = "REQUISITO: Pesquise INTEL, nas pesquisas de VISIONÁRIO";
		a_lider1_btn.ToolTip = "REQUISITO: Pesquise LÍDER, nas pesquisas de ARREBATADOR";
		a_lider2_btn.ToolTip = "REQUISITO: Pesquise o segundo nível de LÍDER, nas pesquisas de ARREBATADOR";
		a_lider3_btn.ToolTip = "REQUISITO: Pesquise o terceiro nível de LÍDER, nas pesquisas de ARREBATADOR";
		a_planetas_btn.ToolTip = "REQUISITO: Pesquise PLANETAS, nas pesquisas de VISIONÁRIO";
		
		
	} else if($myPersona.especie $= "gulok"){
		acadamiaBase_img.bitmap = "game/data/images/academia/academia_gulok";
		a_lider1On_tab.bitmap = "game/data/images/academia/atab_Glider1On";
		a_lider2On_tab.bitmap = "game/data/images/academia/atab_Glider2On";
		a_lider3On_tab.bitmap = "game/data/images/academia/atab_Glider3On";

		a_lider1Escudo_micon.tooltip = "Asas";
		a_lider2Escudo_micon.tooltip = "Asas";
		a_lider3Escudo_micon.tooltip = "Entregar";
		a_lider1JetPack_micon.tooltip = "Canibalizar";
		a_lider2JetPack_micon.tooltip = "Metamorfose";
		a_lider3JetPack_micon.tooltip = "Sopro";
		a_lider1Sniper_micon.tooltip = "Carregar";
		a_lider2Sniper_micon.tooltip = "Carregar";
		a_lider3Sniper_micon.tooltip = "Fúria";
		a_lider1Moral_micon.tooltip = "Devorar Rainhas";
		a_lider2Moral_micon.tooltip = "Cortejar";
		a_lider3Moral_micon.tooltip = "Covil";
		

		a_comerciante_btn.ToolTip = "REQUISITOS: 3 Vitórias e Comerciante 90%+";
		a_intel_btn.ToolTip = "REQUISITO: Visionário 1 OU Arrebatador 3";
		a_lider1_btn.ToolTip = "";
		a_lider2_btn.ToolTip = "";
		a_lider3_btn.ToolTip = "Pesquise DRAGNAL, nas pesquisas de ARREBATADOR";
		a_planetas_btn.ToolTip = "";
	}
	
	//limpa os ícones:
	a_clearPesIcons();
	
	//popula a persona:
	clientPopularDados("academia"); //passa os dados para a tela de Academia
		
	//popula o status:
	clientPopularStatus();
	
	//abre a academia na página "Sobre":
	a_clearTela();
}

function a_clearPesIcons(){
	//humanos:
	a_intel_micon.setVisible(false);
	a_espionagem_micon.setVisible(false);
	a_filantropia_micon.setVisible(false);
	a_almirante_micon.setVisible(false);
	a_comerciante_micon.setVisible(false);
	a_diplomata_micon.setVisible(false);
	a_transporte_micon.setVisible(false);
	a_reciclagem_micon.setVisible(false);
	a_refinaria_micon.setVisible(false);
	a_airDrop_micon.setVisible(false);
	a_planetas_micon.setVisible(false);
	a_canhaoOrbital_micon.setVisible(false);
	a_carapaca_micon.setVisible(false);
	a_mira_micon.setVisible(false);
	a_ocultar_micon.setVisible(false);
	a_satelite_micon.setVisible(false);
	
	a_intelOn_tab.setVisible(false);
	a_espionagemOn_tab.setVisible(false);
	a_filantropiaOn_tab.setVisible(false);
	a_almiranteOn_tab.setVisible(false);
	a_comercianteOn_tab.setVisible(false);
	a_diplomataOn_tab.setVisible(false);
	a_transporteOn_tab.setVisible(false);
	a_reciclagemOn_tab.setVisible(false);
	a_refinariaOn_tab.setVisible(false);
	a_airDropOn_tab.setVisible(false);
	a_planetasOn_tab.setVisible(false);
	a_canhaoOrbitalOn_tab.setVisible(false);
	a_carapacaOn_tab.setVisible(false);
	a_miraOn_tab.setVisible(false);
	a_ocultarOn_tab.setVisible(false);
	a_sateliteOn_tab.setVisible(false);
		
	//guloks:
	a_fertilidadeOn_tab.setVisible(false);
	a_pilharOn_tab.setVisible(false);
	a_faroOn_tab.setVisible(false);
	a_exoesqueletoOn_tab.setVisible(false);
	a_hordaOn_tab.setVisible(false);
	a_metabolismoOn_tab.setVisible(false);
	a_instintoOn_tab.setVisible(false);
	a_incorporarOn_tab.setVisible(false);
	a_submergirOn_tab.setVisible(false);
	a_crisalidaOn_tab.setVisible(false);
	a_matriarcaOn_tab.setVisible(false);
	a_especializarOn_tab.setVisible(false);
	a_virusOn_tab.setVisible(false);
	a_expulsarOn_tab.setVisible(false);
	a_evolucaoOn_tab.setVisible(false);
	
	a_fertilidade_micon.setVisible(false);
	a_pilhar_micon.setVisible(false);
	a_faro_micon.setVisible(false);
	a_exoesqueleto_micon.setVisible(false);
	a_horda_micon.setVisible(false);
	a_metabolismo_micon.setVisible(false);
	a_instinto_micon.setVisible(false);
	a_incorporar_micon.setVisible(false);
	a_submergir_micon.setVisible(false);
	a_crisalida_micon.setVisible(false);
	a_matriarca_micon.setVisible(false);
	a_especializar_micon.setVisible(false);
	a_virus_micon.setVisible(false);
	a_expulsar_micon.setVisible(false);
	a_evolucao_micon.setVisible(false);
}

function a_sairBtnClick(%deOnde){
	//echo("SAIRBTNCLICK::deOnde = " @ %deOnde);
	if(%deOnde $= "academia"){
		$primeiraAcademia = false;	
	} else if(%deOnde $= "atrio"){
		$primeiraAtrio = false;
		commandToServer('SairDoAtrio');
	}
	alxStop($tema_academia); //desliga a música tema
	//troca os dialogs do Canvas:
	Canvas.popDialog(academiaGui);
	Canvas.pushDialog(loggedInGui);	
	$vendoAcademia = false; //marca que o client está vendo a academia
	
	//popula a persona:
	clientPopularDados("loggedIn"); //passa os dados para a tela de LoggedIn (para o caso de o usuário ter gasto créditos/omnis)
}

function a_voltarBtnClick(){
	if($a_estouVendo $= "sobre"){
		a_sairBtnClick("academia");
	} else {
		%estouVendo = $a_estouVendo;
		%inNode = $a_inNode;
		a_clearTela();
		
		if(%inNode){
			%eval = "%myBtn = a_" @ %estouVendo @ "_btn;";
			eval(%eval);
			%myBtn.performClick();
		} else {
			a_sairBtnClick("academia");
		}
	}
}

function clientPopularStatus(){
	%eval = "clientPopularStatus" @ $myPersona.especie @ "();";
	eval(%eval);
}

function clientPopularStatusHumano(){
	//textos:
	a_patente_txt.text = $myPersona.graduacaoNome;
	
	a_soldadosAtaque_txt.text = $myPersona.aca_s_a_min @ "  a  " @ $myPersona.aca_s_a_max;
	a_soldadosDefesa_txt.text = $myPersona.aca_s_d_min @ "  a  " @ $myPersona.aca_s_d_max;
	
	a_tanquesAtaque_txt.text = $myPersona.aca_t_a_min @ "  a  " @ $myPersona.aca_t_a_max;
	a_tanquesDefesa_txt.text = $myPersona.aca_t_d_min @ "  a  " @ $myPersona.aca_t_d_max;
	
	a_naviosAtaque_txt.text = $myPersona.aca_n_a_min @ "  a  " @ $myPersona.aca_n_a_max;
	a_naviosDefesa_txt.text = $myPersona.aca_n_d_min @ "  a  " @ $myPersona.aca_n_d_max;
	
	a_lider1Ataque_txt.text = $myPersona.aca_ldr_1_a_min @ "  a  " @ $myPersona.aca_ldr_1_a_max;
	a_lider1Defesa_txt.text = $myPersona.aca_ldr_1_d_min @ "  a  " @ $myPersona.aca_ldr_1_d_max;
	
	a_lider2Ataque_txt.text = $myPersona.aca_ldr_2_a_min @ "  a  " @ $myPersona.aca_ldr_2_a_max;
	a_lider2Defesa_txt.text = $myPersona.aca_ldr_2_d_min @ "  a  " @ $myPersona.aca_ldr_2_d_max;
	
	a_lider3Ataque_txt.text = $myPersona.aca_ldr_3_a_min @ "  a  " @ $myPersona.aca_ldr_3_a_max;
	a_lider3Defesa_txt.text = $myPersona.aca_ldr_3_d_min @ "  a  " @ $myPersona.aca_ldr_3_d_max;
	
	
	//Líderes:
	for(%i = 1; %i < $myPersona.aca_a_1 + 1; %i++){
		%eval = "a_lider" @ %i @ "On_tab.setVisible(true);";
		eval(%eval);
			
		%eval = "%myEscudo = $myPersona.aca_ldr_" @ %i @ "_h1;";
		eval(%eval);
		%eval = "%myJetPack = $myPersona.aca_ldr_" @ %i @ "_h2;";
		eval(%eval);
		%eval = "%mySniper = $myPersona.aca_ldr_" @ %i @ "_h3;";
		eval(%eval);
		%eval = "%myMoral = $myPersona.aca_ldr_" @ %i @ "_h4;";
		eval(%eval);
		%eval = "%myEscudoTab = a_lider" @ %i @ "EscudoOn_tab;";
		eval(%eval);
		%eval = "%myEscudoMicon = a_lider" @ %i @ "Escudo_micon;";
		eval(%eval);
		%eval = "%myJetPackTab = a_lider" @ %i @ "JetPackOn_tab;";
		eval(%eval);
		%eval = "%myJetPackMicon = a_lider" @ %i @ "JetPack_micon;";
		eval(%eval);
		%eval = "%mySniperTab = a_lider" @ %i @ "SniperOn_tab;";
		eval(%eval);
		%eval = "%mySniperMicon = a_lider" @ %i @ "Sniper_micon;";
		eval(%eval);
		%eval = "%myMoralTab = a_lider" @ %i @ "MoralOn_tab;";
		eval(%eval);
		%eval = "%myMoralMicon = a_lider" @ %i @ "Moral_micon;";
		eval(%eval);
			
		if(%myEscudo > 0){
			%myEscudoTab.setVisible(true);
			%myEscudoMicon.setVisible(true);
			%myEscudoMicon.bitmap = "~/data/images/academia/amicon_escudo" @ %myEscudo;
		} else {
			%myEscudoTab.setVisible(false);
			%myEscudoMicon.setVisible(false);
		}
		if(%myJetPack > 0){
			%myJetPackTab.setVisible(true);
			%myJetPackMicon.setVisible(true);
			%myJetPackMicon.bitmap = "~/data/images/academia/amicon_jetPack" @ %myJetPack;
		} else {
			%myJetPackTab.setVisible(false);
			%myJetPackMicon.setVisible(false);
		}
		if(%mySniper > 0){
			%mySniperTab.setVisible(true);
			%mySniperMicon.setVisible(true);
			%mySniperMicon.bitmap = "~/data/images/academia/amicon_sniper" @ %mySniper;
		} else {
			%mySniperTab.setVisible(false);
			%mySniperMicon.setVisible(false);
		}
		if(%myMoral > 0){
			%myMoralTab.setVisible(true);
			%myMoralMicon.setVisible(true);
			%myMoralMicon.bitmap = "~/data/images/academia/amicon_moral" @ %myMoral;
		} else {
			%myMoralTab.setVisible(false);
			%myMoralMicon.setVisible(false);
		}
	}
	if($myPersona.aca_a_1 > 0){
		a_lider1_btn.setActive(true);	
	} else {
		a_lider1_btn.setActive(false);		
	}
	if($myPersona.aca_a_1 > 1){
		a_lider2_btn.setActive(true);	
	} else {
		a_lider2_btn.setActive(false);	
	}
	if($myPersona.aca_a_1 > 2){
		a_lider3_btn.setActive(true);	
	} else {
		a_lider3_btn.setActive(false);	
	}
	
		
	//micons e btns:	
	a_verificarAmicon("aca_v_1", "intel", "comerciante", "intel");
	a_verificarAmicon("aca_i_1", "espionagem");
	a_verificarAmicon("aca_i_2", "filantropia");
	a_verificarAmicon("aca_i_3", "almirante");
	
	a_verificarAmicon("aca_c_1", "comerciante");
	a_verificarAmicon("aca_d_1", "diplomata");
	
	a_verificarAmicon("aca_v_2", "transporte");
	a_verificarAmicon("aca_v_3", "reciclagem");
	a_verificarAmicon("aca_v_4", "refinaria");
	a_verificarAmicon("aca_v_5", "airDrop");
	a_verificarAmicon("aca_v_6", "planetas", "planetas");
	
	a_verificarAmicon("aca_a_2", "canhaoOrbital");
		
	//verifica as avançadas:
	a_verificarAmicon("aca_av_1", "Carapaca");
	a_verificarAmicon("aca_av_2", "Mira");
	a_verificarAmicon("aca_av_3", "Ocultar");
	a_verificarAmicon("aca_av_4", "Satelite");
	
	//btns de soldados, visionário, arrebatador, diplomata:
	%myPesquisa = findPesquisaPorId($myPersona.aca_pea_id, $myPersona.especie);
	a_soldados_btn.setActive(true);	
	
	if($myPersona.TAXOvisionario < 1){
		a_visionario_btn.setActive(false);	
	} else {
		a_visionario_btn.setActive(true);	
	}
	if($myPersona.TAXOarrebatador < 1){
		a_arrebatador_btn.setActive(false);	
	} else {
		a_arrebatador_btn.setActive(true);	
	}
	if($myPersona.TAXOvisionario > 59 || $myPersona.TAXOarrebatador > 59){
		a_avancadas_btn.setActive(true);	
	} else {
		a_avancadas_btn.setActive(false);	
	}
	if($myPersona.myDiplomata < 70){
		if($myPersona.aca_d_1 == 0){
			if(%myPesquisa.tipo $= "Diplomata"){
				a_diplomata_btn.setActive(true);		
			} else {
				a_diplomata_btn.setActive(false);	
			}
		} else {
			a_diplomata_btn.setActive(true);	
		}
	} else {
		if($myPersona.TAXOvitorias > 2){
			a_diplomata_btn.setActive(true);	
		} else {
			a_diplomata_btn.setActive(false);	
		}
	}
	
	//verPEA_btn:
	if($myPersona.aca_pea_id !$= "" && $myPersona.aca_pea_id !$= "0"){
		a_verPEA_btn.setActive(true);	
	} else {
		a_verPEA_btn.setActive(false);	
	}
	
}

function clientPopularStatusGulok(){
	//textos:
	a_patente_txt.text = $myPersona.graduacaoNome;
	
	//vermes:
	a_soldadosAtaque_txt.text = $myPersona.aca_s_a_min @ "  a  " @ $myPersona.aca_s_a_max;
	a_soldadosDefesa_txt.text = $myPersona.aca_s_d_min @ "  a  " @ $myPersona.aca_s_d_max;
	
	//rainhas:
	a_tanquesAtaque_txt.text = $myPersona.aca_t_a_min @ "  a  " @ $myPersona.aca_t_a_max;
	a_tanquesDefesa_txt.text = $myPersona.aca_t_d_min @ "  a  " @ $myPersona.aca_t_d_max;
	
	//cefaloks:
	a_naviosAtaque_txt.text = $myPersona.aca_n_a_min @ "  a  " @ $myPersona.aca_n_a_max;
	a_naviosDefesa_txt.text = $myPersona.aca_n_d_min @ "  a  " @ $myPersona.aca_n_d_max;
	
	//zangão preto:
	a_lider1Ataque_txt.text = $myPersona.aca_ldr_1_a_min @ "  a  " @ $myPersona.aca_ldr_1_a_max;
	a_lider1Defesa_txt.text = $myPersona.aca_ldr_1_d_min @ "  a  " @ $myPersona.aca_ldr_1_d_max;
	
	//zangão branco:
	a_lider2Ataque_txt.text = $myPersona.aca_ldr_2_a_min @ "  a  " @ $myPersona.aca_ldr_2_a_max;
	a_lider2Defesa_txt.text = $myPersona.aca_ldr_2_d_min @ "  a  " @ $myPersona.aca_ldr_2_d_max;
	
	//dragnal:
	a_lider3Defesa_txt.text = $myPersona.aca_i_3 + ($myPersona.aca_ldr_3_h3 * 2); //"ataques"
	%myBando = $myPersona.aca_ldr_3_h4 + 1;
	if(%myBando == 1){
		%myBando = 0;	
	}
	a_lider3Ataque_txt.text = %myBando; //"bando"
	
	
	//Líderes:
	for(%i = 1; %i < 4; %i++){
		if(%i < 3){
			%eval = "a_lider" @ %i @ "On_tab.setVisible(true);";
			eval(%eval);
		} else {
			if($myPersona.aca_i_3 > 0){
				%eval = "a_lider" @ %i @ "On_tab.setVisible(true);";
				eval(%eval);	
			}
		}
		
		if(%i < 3){
			%eval = "%myH3 = $myPersona.aca_ldr_" @ %i @ "_h1;";
			eval(%eval);
			%eval = "%myH1 = $myPersona.aca_ldr_" @ %i @ "_h2;";
			eval(%eval);
			%eval = "%myH2 = $myPersona.aca_ldr_" @ %i @ "_h3;";
			eval(%eval);
			%eval = "%myH4 = $myPersona.aca_ldr_" @ %i @ "_h4;";
			eval(%eval);
		} else {
			%eval = "%myH1 = $myPersona.aca_ldr_" @ %i @ "_h1;";
			eval(%eval);
			%eval = "%myH2 = $myPersona.aca_ldr_" @ %i @ "_h2;";
			eval(%eval);
			%eval = "%myH3 = $myPersona.aca_ldr_" @ %i @ "_h3;";
			eval(%eval);
			%eval = "%myH4 = $myPersona.aca_ldr_" @ %i @ "_h4;";
			eval(%eval);
		}
		%eval = "%myH1Tab = a_lider" @ %i @ "EscudoOn_tab;";
		eval(%eval);
		%eval = "%myH1Micon = a_lider" @ %i @ "Escudo_micon;";
		eval(%eval);
		%eval = "%myH2Tab = a_lider" @ %i @ "JetPackOn_tab;";
		eval(%eval);
		%eval = "%myH2Micon = a_lider" @ %i @ "JetPack_micon;";
		eval(%eval);
		%eval = "%myH3Tab = a_lider" @ %i @ "SniperOn_tab;";
		eval(%eval);
		%eval = "%myH3Micon = a_lider" @ %i @ "Sniper_micon;";
		eval(%eval);
		%eval = "%myH4Tab = a_lider" @ %i @ "MoralOn_tab;";
		eval(%eval);
		%eval = "%myH4Micon = a_lider" @ %i @ "Moral_micon;";
		eval(%eval);
			
		if(%myH1 > 0){
			%myH1Tab.setVisible(true);
			%myH1Micon.setVisible(true);
			if(%i == 1 || %i == 2){
				%myH1Micon.bitmap = "~/data/images/academia/amicon_asas" @ %myH1;
			} else {
				%myH1Micon.bitmap = "~/data/images/academia/amicon_entregar" @ %myH1;
			}
		} else {
			%myH1Tab.setVisible(false);
			%myH1Micon.setVisible(false);
		}
		if(%myH2 > 0){
			%myH2Tab.setVisible(true);
			%myH2Micon.setVisible(true);
			if(%i == 1){
				%myH2Micon.bitmap = "~/data/images/academia/amicon_canibalizar" @ %myH2;
			} else if(%i == 2){
				%myH2Micon.bitmap = "~/data/images/academia/amicon_metamorfose" @ %myH2;
			} else {
				%myH2Micon.bitmap = "~/data/images/academia/amicon_sopro" @ %myH2;
			}
		} else {
			%myH2Tab.setVisible(false);
			%myH2Micon.setVisible(false);
		}
		if(%myH3 > 0){
			%myH3Tab.setVisible(true);
			%myH3Micon.setVisible(true);
			if(%i == 1 || %i == 2){
				%myH3Micon.bitmap = "~/data/images/academia/amicon_carregar" @ %myH3;
			} else {
				%myH3Micon.bitmap = "~/data/images/academia/amicon_furia" @ %myH3;
			}
		} else {
			%myH3Tab.setVisible(false);
			%myH3Micon.setVisible(false);
		}
		if(%myH4 > 0){
			%myH4Tab.setVisible(true);
			%myH4Micon.setVisible(true);
			if(%i == 1){
				%myH4Micon.bitmap = "~/data/images/academia/amicon_devorarRainhas" @ %myH4;
			} else if(%i == 2){
				%myH4Micon.bitmap = "~/data/images/academia/amicon_cortejar" @ %myH4;
			} else {
				%myH4Micon.bitmap = "~/data/images/academia/amicon_covil" @ %myH4;
			}
		} else {
			%myH4Tab.setVisible(false);
			%myH4Micon.setVisible(false);
		}
	}
	a_lider1_btn.setActive(true);	
	a_lider2_btn.setActive(true);	
	
	if($myPersona.aca_i_3 > 0){
		a_lider3_btn.setActive(true);	
	} else {
		a_lider3_btn.setActive(false);	
	}
	
		
	//micons e btns:	
	
	a_verificarAmicon("aca_i_1", "espionagem");
	a_verificarAmicon("aca_i_2", "pilhar");
		
	a_verificarAmicon("aca_c_1", "faro");
	a_verificarAmicon("aca_d_1", "fertilidade");
	
	a_verificarAmicon("aca_v_1", "metabolismo");
	a_verificarAmicon("aca_v_2", "instinto");
	a_verificarAmicon("aca_v_3", "incorporar");
	a_verificarAmicon("aca_v_4", "submergir");
	a_verificarAmicon("aca_v_5", "crisalida");
	a_verificarAmicon("aca_v_6", "matriarca");
	
	a_verificarAmicon("aca_a_1", "exoesqueleto");
	a_verificarAmicon("aca_a_2", "horda");
		
	//verifica as avançadas:
	a_verificarAmicon("aca_av_1", "especializar");
	a_verificarAmicon("aca_av_2", "virus");
	a_verificarAmicon("aca_av_3", "expulsar");
	a_verificarAmicon("aca_av_4", "evolucao");
	
	//btns de soldados, visionário, arrebatador, diplomata:
	%myPesquisa = findPesquisaPorId($myPersona.aca_pea_id, $myPersona.especie);
	a_soldados_btn.setActive(true);	
	
	if($myPersona.TAXOvisionario < 1){
		a_visionario_btn.setActive(false);
		if($myPersona.TAXOarrebatador < 3){
			a_intel_btn.setActive(false);	
		} else {
			a_intel_btn.setActive(true);	
		}
	} else {
		a_visionario_btn.setActive(true);	
		a_intel_btn.setActive(true);	
	}
	if($myPersona.TAXOarrebatador < 1){
		a_arrebatador_btn.setActive(false);	
	} else {
		a_arrebatador_btn.setActive(true);	
	}
	if($myPersona.TAXOvisionario > 59 || $myPersona.TAXOarrebatador > 59){
		a_avancadas_btn.setActive(true);	
	} else {
		a_avancadas_btn.setActive(false);	
	}
	if($myPersona.myDiplomata < 70){
		if($myPersona.aca_d_1 == 0){
			if(%myPesquisa.tipo $= "Fertilidade"){
				a_diplomata_btn.setActive(true);		
			} else {
				a_diplomata_btn.setActive(false);	
			}
		} else {
			a_diplomata_btn.setActive(true);	
		}
	} else {
		if($myPersona.TAXOvitorias > 2){
			a_diplomata_btn.setActive(true);	
		} else {
			a_diplomata_btn.setActive(false);	
		}
	}
	if($myPersona.myComerciante < 90){
		if($myPersona.aca_c_1 == 0){
			if(%myPesquisa.tipo $= "Faro"){
				a_comerciante_btn.setActive(true);		
			} else {
				a_comerciante_btn.setActive(false);	
			}
		} else {
			a_comerciante_btn.setActive(true);	
		}
	} else {
		if($myPersona.TAXOvitorias > 2){
			a_comerciante_btn.setActive(true);	
		} else {
			a_comerciante_btn.setActive(false);	
		}
	}
	
	//verPEA_btn:
	if($myPersona.aca_pea_id !$= "" && $myPersona.aca_pea_id !$= "0"){
		a_verPEA_btn.setActive(true);	
	} else {
		a_verPEA_btn.setActive(false);	
	}
	
}


function a_verificarAmicon(%pesId, %pesNome, %childBtn1, %childBtn2){
	%eval = "%myPesquisaLvl = $myPersona." @ %pesId @ ";";
	eval(%eval);
	%eval = "%myOnTab = a_" @ %pesNome @ "On_tab;";
	eval(%eval);
	%eval = "%myMicon = a_" @ %pesNome @ "_micon;";
	eval(%eval);
	
	if(%myPesquisaLvl > 0){
		%myOnTab.setVisible(true);
		%myMicon.setVisible(true);
		%myMicon.bitmap = "~/data/images/academia/amicon_" @ %pesNome @ %myPesquisaLvl;
		if(%childBtn1 !$= ""){
			%eval = "%myChildBtn = a_" @ %childBtn1 @ "_btn;";
			eval(%eval);
			%mychildBtn.setActive(true);
			if(%childBtn1 $= "comerciante"){
				%myPesquisa = findPesquisaPorId($myPersona.aca_pea_id, $myPersona.especie);
				if($myPersona.myComerciante < 90){
					if(%myPesquisa.tipo $= "Comerciante"){
						%mychildBtn.setActive(true);	
					} else {
						%mychildBtn.setActive(false);	
					}
				}
			}
		}
		if(%childBtn2 !$= ""){
			%eval = "%myChildBtn = a_" @ %childBtn2 @ "_btn;";
			eval(%eval);
			%mychildBtn.setActive(true);
		}
	} else {
		%myOnTab.setVisible(false);
		%myMicon.setVisible(false);
		if(%childBtn1 !$= ""){
			%eval = "%myChildBtn = a_" @ %childBtn1 @ "_btn;";
			eval(%eval);
			%mychildBtn.setActive(false);
		}
		if(%childBtn2 !$= ""){
			%eval = "%myChildBtn = a_" @ %childBtn2 @ "_btn;";
			eval(%eval);
			%mychildBtn.setActive(false);
		}
	}
}


/////////////
//Academia clear:
function a_clearTela(){
	$a_estouVendo = "sobre";
	$a_inNode = false;
	a_resetarAllBtns();
	a_apagarAllNodes();
	a_apagarAllTabs();	
	a_focoTXT_img.bitmap = "~/data/images/academia/atxt_sobre";
}
function a_apagarAtkEDefNodes(){
	a_ataque_node.setVisible(false);
	a_defesa_node.setVisible(false);	
}
function a_apagarAllNodes(){
	//apaga todas as nodes:
	a_apagarAtkEDefNodes();
	a_node1_node.setVisible(false);
	a_node2_node.setVisible(false);
	a_node3_node.setVisible(false);
	a_node4_node.setVisible(false);
	a_node5_node.setVisible(false);
	a_node6_node.setVisible(false);
	teluriaMap.setVisible(false);	
	
}
function a_apagarAllTabs(){
	$a_inNode = false;
	a_pesquisaTab1_tab.setVisible(false);	
	a_pesquisaTab2_tab.setVisible(false);	
	teluriaMap.setVisible(false);	
}
function a_resetarAllBtns(){
	a_soldados_btn.setStateOn(false);
	a_tanques_btn.setStateOn(false);
	a_navios_btn.setStateOn(false);
	a_visionario_btn.setStateOn(false);
	a_arrebatador_btn.setStateOn(false);
	a_comerciante_btn.setStateOn(false);
	a_diplomata_btn.setStateOn(false);
	a_intel_btn.setStateOn(false);
	a_lider1_btn.setStateOn(false);
	a_lider2_btn.setStateOn(false);
	a_lider3_btn.setStateOn(false);
	a_planetas_btn.setStateOn(false);
	a_avancadas_btn.setStateOn(false);
	a_vermes_btn.setStateOn(false);
	a_rainhas_btn.setStateOn(false);
	a_cefaloks_btn.setStateOn(false);
}

function clientFindPesquisaPorClassId(%aca_pea_id, %liderNum){
	%eval = "%status = $myPersona." @ %aca_pea_id @ ";";
	eval(%eval);
					
	%pesquisaAtualNum = %status + 1;
		
	%pesquisaAtual = %aca_pea_id @ %pesquisaAtualNum;
	%pesquisaMesmo = findPesquisaPorId(%pesquisaAtual, $myPersona.especie);
	
	
	//echo("pesquisaAtual: " @ %pesquisaAtual);
	return %pesquisaMesmo;
}



//////////////////////
//ver PEA:
function a_verPEA(){
	a_clearTela();
	%myPesquisa = clientFindPesquisaPorClassId($myPersona.aca_pea_id, $myPersona.aca_pea_ldr);
	if(isObject(%myPesquisa)){
		//echo("PEA ENCONTRADA: " @ %myPesquisa.id);	
		
		%myTipo = %myPesquisa.tipo;
		%myUpgrade = %myPesquisa.upgrade;
		if($myPersona.aca_pea_ldr){
			%myTipoBtn = %myTipo @ $myPersona.aca_pea_ldr;
			%eval = "a_" @ %myTipoBtn @ "_btn.setStateOn(true);";
			eval(%eval);
			$a_estouVendo = "lider" @ $myPersona.aca_pea_ldr;
			$a_estouVendoNum = $myPersona.aca_pea_ldr;
		} else {
			%eval = "a_" @ %myTipo @ "_btn.setStateOn(true);";
			eval(%eval);	
			$a_estouVendo = %myTipo;
		}
		
		//echo("%myTipo = " @ %myTipo);
		//echo("%liderNum = " @ $myPersona.aca_pea_ldr);
		//echo("%myUpgrade = " @ %myUpgrade);
		
		if(%myUpgrade $= "defesaMinima" || %myUpgrade $= "defesaMaxima"){
			//%myUpgrade = "defesa";	
			a_mostrarAtkEDefNodes();
			a_entrarNodeAtkDef($a_estouVendo, "defesa");
			$a_estouVendoNode = "defesa";
		} else if(%myUpgrade $= "ataqueMinimo" || %myUpgrade $= "ataqueMaximo"){
			//%myUpgrade = "ataque";
			a_mostrarAtkEDefNodes();
			a_entrarNodeAtkDef($a_estouVendo, "ataque");
			$a_estouVendoNode = "ataque";
		} else if(%myTipo $= "Diplomata"){
			//echo("A");
			$a_estouVendo = "";
			a_entrarDiplomataNode();
		} else if(%myTipo $= "Comerciante"){
			$a_estouVendo = "";
			a_entrarComercianteNode();
		} else {
			%eval = "a_mostrar" @ %myTipo @ "Nodes();";
			eval(%eval);
			//echo("%eval = " @ %eval);
			a_entrarNode(%myUpgrade);
		}
	}
}



///////////////
//para Soldados, Tanques e Navios (não são as nodes comuns):
function a_mostrarAtkEDefNodes(){
	//traz os nodes corretos:
	a_ataque_node.setVisible(true);
	a_defesa_node.setVisible(true);	
	if($myPersona.especie $= "humano"){
		a_ataque_node.bitmap = "game/data/images/academia/aicon_ataque";
		a_defesa_node.bitmap = "game/data/images/academia/aicon_defesa";
	} else if($myPersona.especie $= "gulok"){
		a_ataque_node.bitmap = "game/data/images/academia/aicon_Gataque";
		a_defesa_node.bitmap = "game/data/images/academia/aicon_Gdefesa";
	}
}
function a_unitsBtnClick(%tipo){
	if($a_estouVendo $= %tipo){
		 a_clearTela(); //volta pra tela "Sobre"
	} else {
		a_resetarAllBtns();	
		a_apagarAllNodes();
		a_apagarAllTabs();	
		
		%eval = "a_" @ %tipo @ "_btn.setStateOn(true);";
		eval(%eval);
		
		//marca quem estou nas pesquisas:
		$a_estouVendo = %tipo;
		
		//traz o texto correto:
		a_focoTXT_img.bitmap = "~/data/images/academia/atxt_" @ %tipo;
		
		a_mostrarAtkEDefNodes();
	}
		
}

////////////////
//para líderes (também guarda o número do líder):
function a_mostrarLiderNodes(){
	if($myPersona.especie $= "humano"){
		//traz os nodes corretos:
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);	
		a_node3_node.setVisible(true);
		a_node4_node.setVisible(true);	
		a_node5_node.setVisible(true);
		a_node6_node.setVisible(true);	
		
		a_node1_node.bitmap = "~/data/images/academia/aicon_defesa";
		a_node2_node.bitmap = "~/data/images/academia/aicon_ataque";
		a_node3_node.bitmap = "~/data/images/academia/aicon_escudo";
		a_node4_node.bitmap = "~/data/images/academia/aicon_jetPack";
		a_node5_node.bitmap = "~/data/images/academia/aicon_sniper";
		a_node6_node.bitmap = "~/data/images/academia/aicon_moral";
	} else if($myPersona.especie $= "gulok"){
		if($a_estouVendoNum == 1){
			//traz os nodes corretos:
			a_node1_node.setVisible(true);
			a_node2_node.setVisible(true);	
			a_node3_node.setVisible(true);
			a_node4_node.setVisible(true);	
			a_node5_node.setVisible(true);
			a_node6_node.setVisible(true);	
			
			a_node1_node.bitmap = "~/data/images/academia/aicon_Gdefesa";
			a_node2_node.bitmap = "~/data/images/academia/aicon_Gataque";
			a_node3_node.bitmap = "~/data/images/academia/aicon_asas";
			a_node4_node.bitmap = "~/data/images/academia/aicon_canibalizar";
			a_node5_node.bitmap = "~/data/images/academia/aicon_carregar_preto";
			a_node6_node.bitmap = "~/data/images/academia/aicon_devorarRainhas";
		} else if($a_estouVendoNum == 2){
			//traz os nodes corretos:
			a_node1_node.setVisible(true);
			a_node2_node.setVisible(true);	
			a_node3_node.setVisible(true);
			a_node4_node.setVisible(true);	
			a_node5_node.setVisible(true);
			a_node6_node.setVisible(true);	
			
			a_node1_node.bitmap = "~/data/images/academia/aicon_Gdefesa";
			a_node2_node.bitmap = "~/data/images/academia/aicon_Gataque";
			a_node3_node.bitmap = "~/data/images/academia/aicon_asas";
			a_node4_node.bitmap = "~/data/images/academia/aicon_metamorfose";
			a_node5_node.bitmap = "~/data/images/academia/aicon_carregar_branco";
			a_node6_node.bitmap = "~/data/images/academia/aicon_cortejar";
		} else if($a_estouVendoNum == 3){
			//traz os nodes corretos:
			a_node1_node.setVisible(true);
			a_node2_node.setVisible(true);	
			a_node3_node.setVisible(true);
			a_node4_node.setVisible(true);	
			
			a_node1_node.bitmap = "~/data/images/academia/aicon_entregar";
			a_node2_node.bitmap = "~/data/images/academia/aicon_sopro";
			a_node3_node.bitmap = "~/data/images/academia/aicon_furia";
			a_node4_node.bitmap = "~/data/images/academia/aicon_covil";
		}
	}
}

function a_liderBtnClick(%num){
	%tipo = "lider" @ %num;
	if($a_estouVendo $= %tipo){
		 a_clearTela(); //volta pra tela "Sobre"
	} else {
		a_resetarAllBtns();	
		a_apagarAllNodes();
		a_apagarAllTabs();	
		
		%eval = "a_" @ %tipo @ "_btn.setStateOn(true);";
		eval(%eval);
		
		//marca quem estou nas pesquisas:
		$a_estouVendo = %tipo;
		$a_estouVendoNum = %num;
		
		//traz o texto correto:
		if($myPersona.especie $= "humano"){
			a_focoTXT_img.bitmap = "~/data/images/academia/atxt_liderMesmo";
		} else if($myPersona.especie $= "gulok"){
			switch (%num){
				case 1:	a_focoTXT_img.bitmap = "~/data/images/academia/atxt_zangaoPreto";
				case 2:	a_focoTXT_img.bitmap = "~/data/images/academia/atxt_zangaoBranco";
				case 3:	a_focoTXT_img.bitmap = "~/data/images/academia/atxt_dragnalMesmo";
			}
			
		}
		
		a_mostrarLiderNodes();
	}
}

/////////////////////////////////////////////////
///////////////////////////////////////////////////
//PESQUISAS GENÉRICAS COM NODES:
function a_pesquisaGenericaBtnClick(%tipo){
	if($a_estouVendo $= %tipo){
		 a_clearTela(); //volta pra tela "Sobre"
	} else {
		a_resetarAllBtns();	
		a_apagarAllNodes();
		a_apagarAllTabs();	
		
		%eval = "a_" @ %tipo @ "_btn.setStateOn(true);";
		eval(%eval);
		
		//marca quem estou nas pesquisas:
		$a_estouVendo = %tipo;
				
		//traz o texto correto:
		if(%tipo $= "planetas"){
			if($myPersona.especie $= "humano"){
				a_focoTXT_img.bitmap = "~/data/images/academia/atxt_planetasMesmo";	
			} else if($myPersona.especie $= "gulok"){
				a_focoTXT_img.bitmap = "~/data/images/academia/atxt_GplanetasMesmo";	
			}
		} else if(%tipo $= "avancadas"){
			if($myPersona.especie $= "humano"){
				a_focoTXT_img.bitmap = "~/data/images/academia/atxt_avancadas";	
			} else if($myPersona.especie $= "gulok"){
				a_focoTXT_img.bitmap = "~/data/images/academia/atxt_Gavancadas";	
			}
		} else {
			a_focoTXT_img.bitmap = "~/data/images/academia/atxt_" @ %tipo;
		}
		
		%eval = "a_mostrar" @ %tipo @ "Nodes();";
		eval(%eval);
	}
}
/////////////
//para Visionário:
function a_mostrarVisionarioNodes(){
	//traz os nodes corretos:
	if($myPersona.especie $= "humano"){
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);	
		a_node3_node.setVisible(true);
		a_node4_node.setVisible(true);	
		a_node5_node.setVisible(true);
		a_node6_node.setVisible(true);	
		
		a_node1_node.bitmap = "~/data/images/academia/aicon_intel";
		a_node2_node.bitmap = "~/data/images/academia/aicon_transporte";
		a_node3_node.bitmap = "~/data/images/academia/aicon_reciclagem";
		a_node4_node.bitmap = "~/data/images/academia/aicon_refinaria";
		a_node5_node.bitmap = "~/data/images/academia/aicon_airDrop";
		a_node6_node.bitmap = "~/data/images/academia/aicon_planetas";
	} else if($myPersona.especie $= "gulok"){
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);	
		a_node3_node.setVisible(true);
		a_node4_node.setVisible(true);	
		a_node5_node.setVisible(true);
		a_node6_node.setVisible(true);	
				
		a_node1_node.bitmap = "~/data/images/academia/aicon_instinto";
		a_node2_node.bitmap = "~/data/images/academia/aicon_incorporar";
		a_node3_node.bitmap = "~/data/images/academia/aicon_submergir";
		a_node4_node.bitmap = "~/data/images/academia/aicon_crisalida";
		a_node5_node.bitmap = "~/data/images/academia/aicon_metabolismo"; //aca_v_1, foi trocada de lugar por requisitos altos
		a_node6_node.bitmap = "~/data/images/academia/aicon_matriarca";
	}
}

/////////////
//para Arrebatador:
function a_mostrarArrebatadorNodes(){
	if($myPersona.especie $= "humano"){
		//traz os nodes corretos:
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);	
			
		a_node1_node.bitmap = "~/data/images/academia/aicon_lider";
		a_node2_node.bitmap = "~/data/images/academia/aicon_canhaoOrbital";
	} else if($myPersona.especie $= "gulok"){
		//traz os nodes corretos:
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);	
		a_node3_node.setVisible(true);	
			
		a_node1_node.bitmap = "~/data/images/academia/aicon_exoesqueleto";
		a_node2_node.bitmap = "~/data/images/academia/aicon_horda";
		a_node3_node.bitmap = "~/data/images/academia/aicon_dragnal";
	}
}

/////////////
//para Intel:
function a_mostrarIntelNodes(){
	if($myPersona.especie $= "humano"){
		//traz os nodes corretos:
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);	
		a_node3_node.setVisible(true);	
		
		a_focoTXT_img.bitmap = "~/data/images/academia/atxt_intel";
			
		a_node1_node.bitmap = "~/data/images/academia/aicon_espionagem";
		a_node2_node.bitmap = "~/data/images/academia/aicon_filantropia";
		a_node3_node.bitmap = "~/data/images/academia/aicon_almirante";
	} else if($myPersona.especie $= "gulok"){
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);
		
		a_focoTXT_img.bitmap = "~/data/images/academia/atxt_Gintel";
		
		a_node1_node.bitmap = "~/data/images/academia/aicon_espionagem";
		a_node2_node.bitmap = "~/data/images/academia/aicon_pilhar";
	}
}

///////////////////
//para Planetas:
function a_mostrarPlanetasNodes(){
	if($myPersona.especie $= "humano"){
		//traz os nodes corretos:
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);	
		//a_node3_node.setVisible(true);
		
		a_node1_node.bitmap = "~/data/images/academia/aicon_ungart";
		a_node2_node.bitmap = "~/data/images/academia/aicon_teluria";
		//a_node3_node.bitmap = "~/data/images/academia/aicon_narsul";
	} else if($myPersona.especie $= "gulok"){
		//traz os nodes corretos:
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);	
		a_node3_node.setVisible(true);
		
		a_node1_node.bitmap = "~/data/images/academia/aicon_terra";
		a_node2_node.bitmap = "~/data/images/academia/aicon_ungart";
		a_node3_node.bitmap = "~/data/images/academia/aicon_teluria";
	}
}

//////////////
//Para Avançadas:
function a_mostrarAvancadasNodes(){
	if($myPersona.especie $= "humano"){
		//traz os nodes corretos:
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);	
		a_node3_node.setVisible(true);
		a_node4_node.setVisible(true);	
			
		a_node1_node.bitmap = "~/data/images/academia/aicon_carapaca";
		a_node2_node.bitmap = "~/data/images/academia/aicon_mira";
		a_node3_node.bitmap = "~/data/images/academia/aicon_ocultar";
		a_node4_node.bitmap = "~/data/images/academia/aicon_satelite";
	} else if($myPersona.especie $= "gulok"){
		//traz os nodes corretos:
		a_node1_node.setVisible(true);
		a_node2_node.setVisible(true);	
		a_node3_node.setVisible(true);
		a_node4_node.setVisible(true);	
			
		a_node1_node.bitmap = "~/data/images/academia/aicon_especializar";
		a_node2_node.bitmap = "~/data/images/academia/aicon_virus";
		a_node3_node.bitmap = "~/data/images/academia/aicon_expulsar";
		a_node4_node.bitmap = "~/data/images/academia/aicon_evolucao";
	}
}
///////////////////////////////
////////////////////////////////

//
//Node Clicks:
//
function a_selecionarNode(%node){
	if($myPersona.especie $= "humano"){
		switch$ (%node){
			case "1":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("intel");
				//$a_estouVendoNode = "intel";
			} else if($a_estouVendo $= "arrebatador"){
				a_entrarNode("lider");
				//$a_estouVendoNode = "lider";
			} else if($a_estouVendo $= "intel"){
				a_entrarNode("espionagem");
				//$a_estouVendoNode = "espionagem";
			} else if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2" || $a_estouVendo $= "lider3"){
				a_entrarNodeAtkDef($a_estouVendo, "defesa");
				//$a_estouVendoNode = "defesa";
			} else if($a_estouVendo $= "planetas"){
				a_entrarNode("ungart");
				//$a_estouVendoNode = "ungart";
			} else if($a_estouVendo $= "avancadas"){
				a_entrarNode("carapaca");
				//$a_estouVendoNode = "carapaca";
			}
				
			case "2":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("transporte");
				//$a_estouVendoNode = "transporte";
			} else if($a_estouVendo $= "arrebatador"){
				a_entrarNode("canhaoOrbital");
				//$a_estouVendoNode = "canhaoOrbital";
			} else if($a_estouVendo $= "intel"){
				a_entrarNode("filantropia");
				//$a_estouVendoNode = "filantropia";
			} else if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2" || $a_estouVendo $= "lider3"){
				a_entrarNodeAtkDef($a_estouVendo, "ataque");
				//$a_estouVendoNode = "ataque";
			} else if($a_estouVendo $= "planetas"){
				a_entrarNode("teluria");
				//$a_estouVendoNode = "teluria";
			} else if($a_estouVendo $= "avancadas"){
				a_entrarNode("mira");
				//$a_estouVendoNode = "mira";
			}
			
			case "3":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("reciclagem");
				//$a_estouVendoNode = "reciclagem";
			} else if($a_estouVendo $= "intel"){
				a_entrarNode("almirante");
				//$a_estouVendoNode = "almirante";
			} else if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2" || $a_estouVendo $= "lider3"){
				a_entrarNode("escudo");
				//$a_estouVendoNode = "escudo";
			} else if($a_estouVendo $= "planetas"){
				a_entrarNode("narsul");
				//$a_estouVendoNode = "narsul";
			} else if($a_estouVendo $= "avancadas"){
				a_entrarNode("ocultar");
				//$a_estouVendoNode = "ocultar";
			}
			
			case "4":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("refinaria");
				//$a_estouVendoNode = "refinaria";
			} else if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2" || $a_estouVendo $= "lider3"){
				a_entrarNode("jetPack");
				//$a_estouVendoNode = "jetPack";
			} else if($a_estouVendo $= "avancadas"){
				a_entrarNode("satelite");
				//$a_estouVendoNode = "satelite";
			}
			
			case "5":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("airDrop");
				//$a_estouVendoNode = "airDrop";
			} else if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2" || $a_estouVendo $= "lider3"){
				a_entrarNode("Sniper");
				//$a_estouVendoNode = "sniper";
			} 
			
			case "6":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("planetas");
				//$a_estouVendoNode = "planetas";
			} else if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2" || $a_estouVendo $= "lider3"){
				a_entrarNode("moral");
				//$a_estouVendoNode = "moral";
			} 
		}
	} else if($myPersona.especie $= "gulok"){
		switch$ (%node){
			case "1":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("instinto");
			} else if($a_estouVendo $= "arrebatador"){
				a_entrarNode("exoesqueleto");
			} else if($a_estouVendo $= "intel"){
				a_entrarNode("espionagem");
			} else if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2"){
				a_entrarNodeAtkDef($a_estouVendo, "defesa");
			} else if($a_estouVendo $= "lider3"){
				a_entrarNode("entregar");
			} else if($a_estouVendo $= "planetas"){
				a_entrarNode("terra");
			} else if($a_estouVendo $= "avancadas"){
				a_entrarNode("especializar");
			}
			
			case "2":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("incorporar");
			} else if($a_estouVendo $= "arrebatador"){
				a_entrarNode("horda");
			} else if($a_estouVendo $= "intel"){
				a_entrarNode("pilhar");
			} else if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2"){
				a_entrarNodeAtkDef($a_estouVendo, "ataque");
			} else if($a_estouVendo $= "lider3"){
				a_entrarNode("sopro");
			} else if($a_estouVendo $= "planetas"){
				a_entrarNode("ungart");
			} else if($a_estouVendo $= "avancadas"){
				a_entrarNode("virus");
			}
			
			case "3":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("submergir");
			} else if($a_estouVendo $= "arrebatador"){
				a_entrarNode("dragnal");
			} else if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2"){
				a_entrarNode("asas");
			} else if($a_estouVendo $= "lider3"){
				a_entrarNode("furia");
			} else if($a_estouVendo $= "planetas"){
				a_entrarNode("teluria");
			} else if($a_estouVendo $= "avancadas"){
				a_entrarNode("expulsar");
			}
			
			case "4":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("crisalida");
			} else if($a_estouVendo $= "lider1"){
				a_entrarNode("canibalizar");
			} else if($a_estouVendo $= "lider2"){
				a_entrarNode("metamorfose");
			} else if($a_estouVendo $= "lider3"){
				a_entrarNode("covil");
			} else if($a_estouVendo $= "avancadas"){
				a_entrarNode("evolucao");
			}
			
			case "5":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("metabolismo");
			} else if($a_estouVendo $= "lider1"){
				a_entrarNode("carregar");
			} else if($a_estouVendo $= "lider2"){
				a_entrarNode("carregar");
			}
			
			case "6":
			if($a_estouVendo $= "visionario"){
				a_entrarNode("matriarca");
			} else if($a_estouVendo $= "lider1"){
				a_entrarNode("devorarRainhas");
			} else if($a_estouVendo $= "lider2"){
				a_entrarNode("cortejar");
			}
		}
	}
	$a_inNode = true;
}

function a_entrarDiplomataNode(){
	if($a_estouVendo $= "Diplomata"){
		 a_clearTela(); //volta pra tela "Sobre"
	} else {
		a_resetarAllBtns();	
		a_apagarAllNodes();
		a_apagarAllTabs();	
		if($myPersona.especie $= "humano"){
			$a_estouVendo = "Diplomata";
			$a_estouVendoNode = "Diplomata";
			a_entrarNode("Diplomata");
			a_diplomata_btn.setStateOn(true);	
		} else if($myPersona.especie $= "gulok"){
			$a_estouVendo = "Fertilidade";
			$a_estouVendoNode = "Fertilidade";
			a_entrarNode("Fertilidade");
			a_diplomata_btn.setStateOn(true);	
		}
		$a_inNode = false;
	}
}

function a_entrarComercianteNode(){
	if($a_estouVendo $= "Comerciante"){
		 a_clearTela(); //volta pra tela "Sobre"
	} else {
		a_resetarAllBtns();	
		a_apagarAllNodes();
		a_apagarAllTabs();	
		if($myPersona.especie $= "humano"){
			$a_estouVendo = "Comerciante";
			$a_estouVendoNode = "Comerciante";
			a_entrarNode("Comerciante");
			a_comerciante_btn.setStateOn(true);	
		} else if($myPersona.especie $= "gulok"){
			$a_estouVendo = "Faro";
			$a_estouVendoNode = "Faro";
			a_entrarNode("Faro");
			a_comerciante_btn.setStateOn(true);	
		}
		$a_inNode = false;
		
	}
}

function a_entrarNode(%tipo){
	$a_estouVendoNode = %tipo;
	a_apagarAllNodes();	
	a_apagarAllTabs();	
	
	//traz o texto correto:
	if(%tipo $= "intel"){
		a_focoTXT_img.bitmap = "~/data/images/academia/atxt_intelOutside";
	} else if(%tipo $= "Espionagem"){
		if($myPersona.especie $= "humano"){
			a_focoTXT_img.bitmap = "~/data/images/academia/atxt_" @ %tipo;
		} else if($myPersona.especie $= "gulok"){
			a_focoTXT_img.bitmap = "~/data/images/academia/atxt_G" @ %tipo;
		}
	} else {
		a_focoTXT_img.bitmap = "~/data/images/academia/atxt_" @ %tipo;
	}
	
	//torna a tab de pesquisa genérica visível:
	a_pesquisaTab2_tab.setVisible(true);
	a_pesquisaTab2_title.bitmap = "~/data/images/academia/attl_" @ %tipo;
	
	//chama a função de cada sessão da academia:
	if(%tipo !$= "ungart"){
		%eval = "a_mostrar" @ %tipo @ "Tab();";
		eval(%eval);
	} else {
		if($myPersona.especie $= "gulok"){
			a_focoTXT_img.bitmap = "~/data/images/academia/atxt_Gungart";
		} else {
			a_focoTXT_img.bitmap = "~/data/images/academia/atxt_ungart";
		}
		a_pesquisaTab2_tab.setVisible(false);	
	}
	if($myPersona.especie $= "gulok"){
		if(%tipo $= "terra"){	
			a_pesquisaTab2_tab.setVisible(false);	
		}
	}
	
	$a_inNode = true;
}

function a_mostrarTab(%id, %max, %liderId){
	$a_estouVendoAtkOuDefFlag = false;
	if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2" || $a_estouVendo $= "lider3"){
		%id = "aca_ldr_" @ $a_estouVendoNum @ "_" @ %liderId;
	}
	%eval = "%pesquisaNaPersona = $myPersona." @ %id @ ";";
	eval(%eval);
	
	a_pesquisaTab2_number.bitmap = "~/data/images/academia/" @ %pesquisaNaPersona @ "g";
		
	if(%pesquisaNaPersona == %max){
		a_pesquisaTab2_tab.setVisible(false);
		if(%id $= "aca_pln_1"){
			teluriaMap.setVisible(true);	
		}
	} else {
		%idPesquisa = %id @ (%pesquisaNaPersona + 1);
		%pesquisa = findPesquisaPorId(%idPesquisa, $myPersona.especie);
		a_preencherTab2(%pesquisa);	
	}
}

function a_preencherTab2(%pesquisa){
	$tab2PesquisaId = %pesquisa.id;
	a_pesquisaTab2Omnis_txt.text = %pesquisa.cOMNI;
	a_investirTab2_btn.setVisible(false);
	a_emAndamentoTab2_txt.setVisible(false);
	atab_pesquisando_tab2.setVisible(false);
		
	
	if($myPersona.aca_pea_id $= %pesquisa.classId){
		//estou pesquisando esta aqui!
		a_pesquisaTab2CustoInicial_txt.text = "PAGO";
		a_pesquisaTab2CustoMin_txt.text = $myPersona.aca_pea_min @ " / " @ %pesquisa.cDevMin;
		a_pesquisaTab2CustoPet_txt.text = $myPersona.aca_pea_pet @ " / " @ %pesquisa.cDevPet;
		a_pesquisaTab2CustoUra_txt.text = $myPersona.aca_pea_ura @ " / " @ %pesquisa.cDevUra;
		a_cancelarTab2_btn.setVisible(true);  //deixa o botão de cancelar visível
		a_cancelarTab2_btn.setActive(true);	//deixa o botão de cancelar ativo;
		a_pesquisarTab2_btn.setVisible(false);	//apaga o botão de pesquisar
		a_comprarTab2_btn.setVisible(false);	//apaga o botão de comprar
		a_investirTab2_btn.setVisible(true); // liga o botão de investirCréditos (ele ocupa toda a extensão dos botões apagados)
		a_emAndamentoTab2_txt.setVisible(true); //liga o mostrador de "Pesquisa em andamento"
		atab_pesquisando_tab2.setVisible(true);
		a_pesquisaTab2_pesquisandoNumber.bitmap = "~/data/images/academia/" @ %pesquisa.num @ "gdA";
	} else {
		%tenhoRequisitos = a_verificarRequisitos(%pesquisa);
		a_pesquisaTab2CustoInicial_txt.text = %pesquisa.custoInicial @ " Créditos";
		a_pesquisaTab2CustoMin_txt.text = %pesquisa.cDevMin;
		a_pesquisaTab2CustoPet_txt.text = %pesquisa.cDevPet;
		a_pesquisaTab2CustoUra_txt.text = %pesquisa.cDevUra;
		if(%tenhoRequisitos){
			a_cancelarTab2_btn.setVisible(true);
			a_cancelarTab2_btn.setActive(false);	
			a_pesquisarTab2_btn.setVisible(true);	
			a_pesquisarTab2_btn.setActive(true);	
			a_comprarTab2_btn.setVisible(true);	
			a_comprarTab2_btn.setActive(true);	
		} else {
			a_cancelarTab2_btn.setVisible(false);
			a_pesquisarTab2_btn.setVisible(false);	
			a_comprarTab2_btn.setVisible(false);
			if(%pesquisa.requisitoTipo $= "Visionario"){
				%pesquisaRequisitoTipo = "Visionário";	
				a_pesquisaTab2Requisito_txt.text = "Requisito: " @ %pesquisaRequisitoTipo @ " ( " @ %pesquisa.requisitoValor @ " )";
			} else if(%pesquisa.requisitoTipo $= "Vitorias"){
				%pesquisaRequisitoTipo = "Vitórias";
				a_pesquisaTab2Requisito_txt.text = "Requisito: " @ %pesquisaRequisitoTipo @ " ( " @ %pesquisa.requisitoValor @ " )";
			} else {
				a_pesquisaTab2Requisito_txt.text = "Requisito: " @ %pesquisa.requisitoTipo @ " ( " @ %pesquisa.requisitoValor @ " )";
			}
		}
	}
}

function a_verificarRequisitos(%pesquisa){
	%eval = "%requisito = $myPersona.TAXO" @ %pesquisa.requisitoTipo @ ";";
	eval(%eval);
	if(%requisito >= %pesquisa.requisitoValor){
		%tenhoRequisitos = true;
	} else {
		%tenhoRequisitos = false;
	}
	
	return %tenhoRequisitos;
}

function a_mostrarIntelTab(){
	a_mostrarTab("aca_v_1", 2);
}

function a_mostrarTransporteTab(){
	a_mostrarTab("aca_v_2", 3);
}

function a_mostrarReciclagemTab(){
	a_mostrarTab("aca_v_3", 3);
}

function a_mostrarRefinariaTab(){
	a_mostrarTab("aca_v_4", 3);
}

function a_mostrarAirDropTab(){
	a_mostrarTab("aca_v_5", 3);
}

function a_mostrarPlanetasTab(){
	a_mostrarTab("aca_v_6", 3);
}

function a_mostrarLiderTab(){
	a_mostrarTab("aca_a_1", 3);
}

function a_mostrarCanhaoOrbitalTab(){
	a_mostrarTab("aca_a_2", 3);
}

function a_mostrarEspionagemTab(){
	a_mostrarTab("aca_i_1", 3);
}

function a_mostrarFilantropiaTab(){
	a_mostrarTab("aca_i_2", 3);
}

function a_mostrarAlmiranteTab(){
	a_mostrarTab("aca_i_3", 3);
}

function a_mostrarEscudoTab(){
	a_mostrarTab("aca_ldr_h1", 3, "h1");
}

function a_mostrarJetPackTab(){
	a_mostrarTab("aca_ldr_h2", 3, "h2");
}

function a_mostrarSniperTab(){
	a_mostrarTab("aca_ldr_h3", 3, "h3");
}

function a_mostrarMoralTab(){
	a_mostrarTab("aca_ldr_h4", 3, "h4");
}

function a_mostrarComercianteTab(){
	a_mostrarTab("aca_c_1", 3);
}

function a_mostrarDiplomataTab(){
	a_mostrarTab("aca_d_1", 3);
}

function a_mostrarCarapacaTab(){
	a_mostrarTab("aca_av_1", 3);
}

function a_mostrarMiraTab(){
	a_mostrarTab("aca_av_2", 3);
}

function a_mostrarOcultarTab(){
	a_mostrarTab("aca_av_3", 3);
}

function a_mostrarSateliteTab(){
	a_mostrarTab("aca_av_4", 3);
}

function a_mostrarTeluriaTab(){
	a_mostrarTab("aca_pln_1", 1);
}

/////////////////////////
//Guloks:
function a_mostrarMetabolismoTab(){
	a_mostrarTab("aca_v_1", 3);
}

function a_mostrarInstintoTab(){
	a_mostrarTab("aca_v_2", 3);
}

function a_mostrarIncorporarTab(){
	a_mostrarTab("aca_v_3", 3);
}

function a_mostrarSubmergirTab(){
	a_mostrarTab("aca_v_4", 3);
}

function a_mostrarCrisalidaTab(){
	a_mostrarTab("aca_v_5", 3);
}

function a_mostrarMatriarcaTab(){
	a_mostrarTab("aca_v_6", 3);
}

function a_mostrarExoesqueletoTab(){
	a_mostrarTab("aca_a_1", 3);
}

function a_mostrarHordaTab(){
	a_mostrarTab("aca_a_2", 3);
}

function a_mostrarDragnalTab(){
	a_mostrarTab("aca_i_3", 3);
}

function a_mostrarFaroTab(){
	a_mostrarTab("aca_c_1", 3);
}

function a_mostrarFertilidadeTab(){
	a_mostrarTab("aca_d_1", 3);
}

function a_mostrarPilharTab(){
	a_mostrarTab("aca_i_2", 3);
}

function a_mostrarCarregarTab(){
	a_mostrarTab("aca_ldr_h1", 3, "h1");
}

function a_mostrarAsasTab(){
	a_mostrarTab("aca_ldr_h2", 3, "h2");
}

function a_mostrarCanibalizarTab(){
	a_mostrarTab("aca_ldr_h3", 3, "h3");
}

function a_mostrarDevorarRainhasTab(){
	a_mostrarTab("aca_ldr_h4", 3, "h4");
}

function a_mostrarMetamorfoseTab(){
	a_mostrarTab("aca_ldr_h3", 3, "h3");
}

function a_mostrarCortejarTab(){
	a_mostrarTab("aca_ldr_h4", 3, "h4");
}

function a_mostrarEntregarTab(){
	a_mostrarTab("aca_ldr_h1", 3, "h1");
}

function a_mostrarSoproTab(){
	a_mostrarTab("aca_ldr_h2", 3, "h2");
}

function a_mostrarFuriaTab(){
	a_mostrarTab("aca_ldr_h3", 3, "h3");
}

function a_mostrarCovilTab(){
	a_mostrarTab("aca_ldr_h4", 3, "h4");
}

function a_mostrarEspecializarTab(){
	a_mostrarTab("aca_av_1", 3);
}

function a_mostrarVirusTab(){
	a_mostrarTab("aca_av_2", 3);
}

function a_mostrarExpulsarTab(){
	a_mostrarTab("aca_av_3", 3);
}

function a_mostrarEvolucaoTab(){
	a_mostrarTab("aca_av_4", 3);
}

//ataque e defesa:
function a_ataqueNodeClick(){
	%tipo = $a_estouVendo;
	a_entrarNodeAtkDef(%tipo, "Ataque");
	$a_inNode = true;
}

function a_defesaNodeClick(){
	%tipo = $a_estouVendo;
	a_entrarNodeAtkDef(%tipo, "Defesa");	
	$a_inNode = true;
}


function a_preencherTab1(%pesquisa){
	$tab1PesquisaId = %pesquisa.id;
	a_pesquisaTab1Omnis_txt.text = %pesquisa.cOMNI;
	a_investirTab1_btn.setVisible(false);
	a_emAndamentoTab1_txt.setVisible(false);
	atab_pesquisando_tab1.setVisible(false);
	
	if($myPersona.aca_pea_id $= %pesquisa.classId){
		//estou pesquisando esta aqui!
		a_pesquisaTab1CustoInicial_txt.text = "PAGO";
		a_pesquisaTab1CustoMin_txt.text = $myPersona.aca_pea_min @ " / " @ %pesquisa.cDevMin;
		a_pesquisaTab1CustoPet_txt.text = $myPersona.aca_pea_pet @ " / " @ %pesquisa.cDevPet;
		a_pesquisaTab1CustoUra_txt.text = $myPersona.aca_pea_ura @ " / " @ %pesquisa.cDevUra;
		a_cancelarTab1_btn.setVisible(true);  //deixa o botão de cancelar visível
		a_cancelarTab1_btn.setActive(true);	//deixa o botão de cancelar ativo;
		a_pesquisarTab1_btn.setVisible(false);	//apaga o botão de pesquisar
		a_comprarTab1_btn.setVisible(false);	//apaga o botão de comprar
		a_investirTab1_btn.setVisible(true); // liga o botão de investirCréditos (ele ocupa toda a extensão dos botões apagados)
		a_emAndamentoTab1_txt.setVisible(true); //liga o mostrador de "Pesquisa em andamento"
		atab_pesquisando_tab1.setVisible(true);
		a_pesquisaTab1_pesquisandoNumber.bitmap = "~/data/images/academia/" @ %pesquisa.num @ "gdA";
	} else {
		%tenhoRequisitos = a_verificarRequisitos(%pesquisa);
		a_pesquisaTab1CustoInicial_txt.text = %pesquisa.custoInicial @ " Créditos";
		a_pesquisaTab1CustoMin_txt.text = %pesquisa.cDevMin;
		a_pesquisaTab1CustoPet_txt.text = %pesquisa.cDevPet;
		a_pesquisaTab1CustoUra_txt.text = %pesquisa.cDevUra;
		if(%tenhoRequisitos){
			a_cancelarTab1_btn.setVisible(true);
			a_cancelarTab1_btn.setActive(false);	
			a_pesquisarTab1_btn.setVisible(true);	
			a_pesquisarTab1_btn.setActive(true);	
			a_comprarTab1_btn.setVisible(true);	
			a_comprarTab1_btn.setActive(true);	
		} else {
			a_cancelarTab1_btn.setVisible(false);
			a_pesquisarTab1_btn.setVisible(false);	
			a_comprarTab1_btn.setVisible(false);
			if(%pesquisa.requisitoTipo $= "Visionario"){
				%pesquisaRequisitoTipo = "Visionário";	
				a_pesquisaTab1Requisito_txt.text = "Requisito: " @ %pesquisaRequisitoTipo @ " ( " @ %pesquisa.requisitoValor @ " )";
			} else if(%pesquisa.requisitoTipo $= "Vitorias"){
				%pesquisaRequisitoTipo = "Vitórias";
				a_pesquisaTab1Requisito_txt.text = "Requisito: " @ %pesquisaRequisitoTipo @ " ( " @ %pesquisa.requisitoValor @ " )";
			} else {
				a_pesquisaTab1Requisito_txt.text = "Requisito: " @ %pesquisa.requisitoTipo @ " ( " @ %pesquisa.requisitoValor @ " )";
			}
		}
	}
}
function a_entrarNodeAtkDef(%tipo, %atkOuDef){
	a_apagarAllNodes();	
	a_apagarAllTabs();	
		
	a_focoTXT_img.bitmap = "~/data/images/academia/atxt_" @ %atkOuDef;
		
	//torna as duas tabs de pesquisa visíveis:
	a_pesquisaTab1_tab.setVisible(true);
	a_pesquisaTab2_tab.setVisible(true);
	if(%atkOuDef $= "Ataque"){
		a_pesquisaTab1_title.bitmap = "~/data/images/academia/attl_ataqueMinimo";
		a_pesquisaTab2_title.bitmap = "~/data/images/academia/attl_ataqueMaximo";
	} else {
		a_pesquisaTab1_title.bitmap = "~/data/images/academia/attl_defesaMinima";
		a_pesquisaTab2_title.bitmap = "~/data/images/academia/attl_defesaMaxima";
	}
	
	//chama a função de cada sessão da academia:
	%eval = "a_mostrarAtkDefTabs(" @ %atkOuDef @ ", " @ %tipo @ ");";
	eval(%eval);
}


function a_mostrarAtkDefTabs(%atkOuDef, %tipo){
	$a_estouVendoAtkOuDefFlag = true;
	$a_estouVendoAtkOuDef = %atkOuDef;
	if(%tipo $= "soldados" || %tipo $= "vermes"){
		if(%atkOuDef $= "ataque"){
			%idMn = "aca_s_a_min";
			%idMx = "aca_s_a_max";
		} else {
			%idMn = "aca_s_d_min";
			%idMx = "aca_s_d_max";
		}
		if(%tipo $= "soldados"){
			%maxMn = 6;
			%maxMx = 10;
		} else if(%tipo $= "vermes"){
			%maxMn = 8;
			%maxMx = 12;
		}		
	} else if(%tipo $= "tanques" || %tipo $= "rainhas"){
		if(%atkOuDef $= "ataque"){
			%idMn = "aca_t_a_min";
			%idMx = "aca_t_a_max";
		} else {
			%idMn = "aca_t_d_min";
			%idMx = "aca_t_d_max";
		}
		if(%tipo $= "tanques"){
			%maxMn = 6;
			%maxMx = 25;
		} else if(%tipo $= "rainhas"){
			%maxMn = 15;
			%maxMx = 35;
		}
	} else if(%tipo $= "navios" || %tipo $= "cefaloks"){
		if(%atkOuDef $= "ataque"){
			%idMn = "aca_n_a_min";
			%idMx = "aca_n_a_max";
		} else {
			%idMn = "aca_n_d_min";
			%idMx = "aca_n_d_max";
		}
		if(%tipo $= "navios"){
			%maxMn = 6;
			%maxMx = 25;
		} else if(%tipo $= "cefaloks"){
			%maxMn = 12;
			%maxMx = 28;
		}
		%maxMn = 6;
	} else {
		//explode %tipo e pega a segunda palavra é o número, depois de "lider"
		%exp = explode(%tipo, "lider");
		%exp = %exp.get[1];
		if(%atkOuDef $= "ataque"){
			%idMn = "aca_ldr_" @ %exp @ "_a_min";
			%idMx = "aca_ldr_" @ %exp @ "_a_max";
		} else {
			%idMn = "aca_ldr_" @ %exp @ "_d_min";
			%idMx = "aca_ldr_" @ %exp @ "_d_max";
		}
		if($myPersona.especie $= "humano"){
			%maxMn = 8;
			%maxMx = 35;	
		} else if($myPersona.especie $= "gulok"){
			%maxMn = 14;
			%maxMx = 35;	
		}
	}
			
	%eval = "%pesquisaMnNaPersona = $myPersona." @ %idMn @ ";";
	eval(%eval);
	%eval = "%pesquisaMxNaPersona = $myPersona." @ %idMx @ ";";
	eval(%eval);
	
	a_pesquisaTab1_number.bitmap = "~/data/images/academia/" @ %pesquisaMnNaPersona @ "g";
	a_pesquisaTab2_number.bitmap = "~/data/images/academia/" @ %pesquisaMxNaPersona @ "g";
	
	//echo("%pesquisaMnNaPersona = " @ %pesquisaMnNaPersona);
		
	if(%pesquisaMnNaPersona == %maxMn){
		/*
		%idPesquisaMn = %idMn @ %pesquisaMnNaPersona;
		%pesquisaMn = findPesquisaPorId(%idPesquisaMn, $myPersona.especie);
		a_preencherTab1(%pesquisaMn);
		a_pesquisarTab1_btn.setVisible(true);
		a_pesquisarTab1_btn.setActive(false);
		a_comprarTab1_btn.setVisible(true);
		a_comprarTab1_btn.setActive(false);
		a_cancelarTab1_btn.setVisible(true); 
		a_cancelarTab1_btn.setActive(false);
		a_pesquisaTab1CustoInicial_txt.text = "";
		a_pesquisaTab1CustoMin_txt.text = "";
		a_pesquisaTab1CustoPet_txt.text = "";
		a_pesquisaTab1CustoUra_txt.text = "";
		a_pesquisaTab1Omnis_txt.text = "";
		*/
		a_pesquisaTab1_tab.setVisible(false);
	} else {
		%idPesquisaMn = %idMn @ (%pesquisaMnNaPersona + 1);
		//echo("%idPesquisaMn = " @ %idPesquisaMn);
		%pesquisaMn = findPesquisaPorId(%idPesquisaMn, $myPersona.especie);
		a_preencherTab1(%pesquisaMn);
	}
	if(%pesquisaMxNaPersona == %maxMx){
		/*
		%idPesquisaMx = %idMx @ %pesquisaMxNaPersona;
		%pesquisaMx = findPesquisaPorId(%idPesquisaMx, $myPersona.especie);
		a_preencherTab2(%pesquisaMx);
		a_pesquisarTab2_btn.setVisible(true);
		a_pesquisarTab2_btn.setActive(false);
		a_comprarTab2_btn.setVisible(true);
		a_comprarTab2_btn.setActive(false);
		a_cancelarTab2_btn.setVisible(true); 
		a_cancelarTab2_btn.setActive(false); 
		a_pesquisaTab2CustoInicial_txt.text = "";
		a_pesquisaTab2CustoMin_txt.text = "";
		a_pesquisaTab2CustoPet_txt.text = "";
		a_pesquisaTab2CustoUra_txt.text = "";
		a_pesquisaTab2Omnis_txt.text = "";
		*/
		a_pesquisaTab2_tab.setVisible(false);
	} else {
		%idPesquisaMx = %idMx @ (%pesquisaMxNaPersona + 1);	
		%pesquisaMx = findPesquisaPorId(%idPesquisaMx, $myPersona.especie);
		a_preencherTab2(%pesquisaMx);
	}
}


////////////////////////////////////////
//////////////////
/// A pesquisa que está sendo visualizada em cada Tab fica gravada numa variável global: $tab1PesquisaId e $tab2PesquisaId. 
/// Quando um Líder está sendo visualizado, além de guardar o valor "lider" na varGlobal $a_estouVendo, também é gravado o número do líder na varGlobal $a_estouVendoNum.
//
//PESQUISAR:
//
function a_pesquisarBtnClick(%num){
	%eval = "%myPesquisaId = $tab" @ %num @ "PesquisaId;";
	eval(%eval);
	
	%pesquisa = findPesquisaPorId(%myPesquisaId, $myPersona.especie);
	
	if($myPersona.aca_pea_id $= "0"){
		if($myPersona.TAXOcreditos >= %pesquisa.custoInicial){
			//trazer um gui "Iniciando pesquisa. Por favor, aguarde."
			clientPushAguardeMsgBox();
			if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2" || $a_estouVendo $= "lider3"){
				commandToServer('a_pesquisar', %myPesquisaId, $a_estouVendoNum);	
			} else {
				commandToServer('a_pesquisar', %myPesquisaId);	
			}
		} else {
			//trazer um gui "Créditos insuficientes para iniciar a pesquisa";	
			clientMsgBoxOKT("CRÉDITOS INSUFICIENTES", "VOCÊ NÃO POSSUI CRÉDITOS PARA INICIAR ESTA PESQUISA.");
		}
	} else {
		//msg dizendo que só se pode fazer uma pesquisa de cada vez.
		clientMsgBoxOKT("IMPOSSÍVEL PESQUISAR", "VOCÊ JÁ POSSUI UMA PESQUISA EM ANDAMENTO.");
	}
}

function clientCmda_iniciarPesquisa(%pesquisaId, %liderNum, %creditosRestantes){
	//echo("clientCmda_iniciarPesquisa (" @ %pesquisaId @ ", " @ %liderNum @ ")");
	%pesquisa = findPesquisaPorId(%pesquisaId, $myPersona.especie);
			
	
	
	$myPersona.TAXOcreditos -= %pesquisa.custoInicial; 	
	$myPersona.aca_pea_id = %pesquisa.classId;
	$myPersona.aca_pea_min = 0;
	$myPersona.aca_pea_pet = 0;
	$myPersona.aca_pea_ura = 0;
	
	$myPersona.aca_pea_ldr = %liderNum;
	
	//dá um refresh na academia:
	clientPopularStatus();
	academiaCreditos_txt.text = %creditosRestantes;
	
	if($a_estouVendoAtkOuDefFlag){
		a_mostrarAtkDefTabs($a_estouVendoAtkOuDef, $a_estouVendo);
	} else {
		a_entrarNode($a_estouVendoNode);	
	}
	clientPopAguardeMsgBox();
	clientPopularPEA("academia");
}

//
//COMPRAR:
//
function a_comprarBtnClick(%num){
	%eval = "%myPesquisaId = $tab" @ $compraNum @ "PesquisaId;";
	eval(%eval);
	%pesquisa = findPesquisaPorId(%myPesquisaId, $myPersona.especie);
	
	if($myPersona.TAXOomnis >= %pesquisa.cOMNI){
		$compraNum = %num;
		//trazer msgBox de confirmação da compra:
		clientConfirmarCompraMsgBox();
	} else {
		//trazer um gui "Omnis insuficientes para comprar esta tecnologia";	
		clientMsgBoxOKT("OMNIS INSUFICIENTES", "ADQUIRA OMNIS NO SITE WWW.PROJETOIMPERIO.COM.");
	}
}

function clientConfirmarCompra(){
	%eval = "%myPesquisaId = $tab" @ $compraNum @ "PesquisaId;";
	eval(%eval);
	
	clientFecharConfirmacaoDeCompra();
		
	%pesquisa = findPesquisaPorId(%myPesquisaId, $myPersona.especie);
	
	//echo("confirmando compra: pesquisaId = " @ %myPesquisaId);
	//echo("myOmnis = " @ $myPersona.taxoOmnis);
	
	if($myPersona.TAXOomnis >= %pesquisa.cOMNI){ //faz uma segunda verificação, nunca é demais quando a verificação está no client :)
		clientPushAguardeMsgBox();
		if($a_estouVendo $= "lider1" || $a_estouVendo $= "lider2" || $a_estouVendo $= "lider3"){
			commandToServer('a_comprar', %myPesquisaId, $a_estouVendoNum);	
		} else {
			commandToServer('a_comprar', %myPesquisaId);	
		}
	} else {
		//trazer um gui "Omnis insuficientes para comprar esta tecnologia";	
		clientMsgBoxOKT("OMNIS INSUFICIENTES", "ADQUIRA OMNIS NO SITE WWW.PROJETOIMPERIO.COM.");
	}
}

function clientConfirmarCompraMsgBox(){
	canvas.pushDialog(confirmarCompraGui);	
}

function clientFecharConfirmacaoDeCompra(){
	echo("fechando confirmação de compra");
	canvas.popDialog(confirmarCompraGui);	
}



function clientCmda_comprarTecnologia(%pesquisaId, %liderNum, %omnisRestantes){
	//echo("clientCmda_comprarTecnologia (" @ %pesquisaId @ ", " @ %liderNum @ ")");
	%pesquisa = findPesquisaPorId(%pesquisaId, $myPersona.especie);
			
	%eval = "$myPersona." @ %pesquisa.classId @ " = " @ %pesquisa.num @ ";";
	eval(%eval);
	
	$myPersona.TAXOomnis -= %pesquisa.cOMNI; 	//o client guarda os omnis nas personas
	
	//dá um refresh na academia:
	clientPopularStatus();
	academiaOmnis_txt.text = %omnisRestantes;
	
	if($a_estouVendoAtkOuDefFlag){
		a_mostrarAtkDefTabs($a_estouVendoAtkOuDef, $a_estouVendo);
	} else {
		a_entrarNode($a_estouVendoNode);	
	}
	clientPopAguardeMsgBox();
	clientPopularPEA("academia");
	clientPushPesquisaCompletaMsgBox();
}

function clientCmda_reconhecerFimDePesquisa(%pesquisa){
	if(%pesquisa $= ""){
		%pesquisa = clientFindPesquisaPorClassId($myPersona.aca_pea_id);
	}
	
	//echo("PESQUISA COMPLETA: " @ %pesquisa.id);
	
	%eval = "$myPersona." @ %pesquisa.classId @ " = " @ %pesquisa.num @ ";";
	eval(%eval);
	//echo("%eval = " @ %eval);
		
	//Zera a pesquisa:
	$myPersona.aca_pea_id = 0;
	$myPersona.aca_pea_min = 0;
	$myPersona.aca_pea_pet = 0;
	$myPersona.aca_pea_ura = 0;
	$myPersona.aca_pea_ldr = 0;
	
	//popula a Academia Imperial:
	clientPopularPEA("academia");
	clientPopularStatus();
	
	//Inativa o botão de investir recursos:
	clientDesligarInvestirRecursos();
	investirRecursos_btn.setActive(false);
	
	//traz a msgBoxOk de pesquisa completa:
	clientPushPesquisaCompletaMsgBox();
}

function a_investirBtnClick(){
	if($myPersona.TAXOcreditos > 0){
		//chamar a msgBox de escolher se quer investir todos ou alguns
		//commandToServer('a_investirCreditos'); //esta linha investe todos os créditos	
		callInvDefGui();
	} else {
		clientMsgBoxOKT("CRÉDITOS INSUFICIENTES", "VOCÊ NÃO POSSUI CRÉDITOS PARA INVESTIR.");
	}
}

function a_investirDef(%creditos){
	if($myPersona.taxoCreditos >= %creditos){
		commandToServer('a_investirCreditosDef', %creditos);
	} else {
		echo("CREDITOS INSUFICIENTES!**");	
	}
}

function clientCmda_atualizarPEAInvestida(%PEAMin, %PEAPet, %PEAUra, %creditosRestantes, %pesquisaCompleta){
	echo("creditos restantes = " @ %creditosRestantes);
	%pesquisa = clientFindPesquisaPorClassId($myPersona.aca_pea_id);
	
	%creditosInvestidos = $myPersona.TAXOcreditos - %creditosRestantes;
	echo("creditos investidos = " @ %creditosInvestidos);
	a_investimentoGradual(%creditosInvestidos, %pesquisa, false, %pesquisaCompleta);
}

	
function a_investimentoGradual(%creditos, %pesquisa, %acabou, %pesquisaCompleta){
	//echo("a_investimentoGradual(" @ %creditos SPC %pesquisa SPC %acabou SPC %pesquisaCompleta @ ")");
	alxPlay( investir );
	clientPushServerComDot(); //tira o controle do usuário
	if(%creditos > 0 && $myPersona.aca_pea_ura < %pesquisa.cDevUra){
		$myPersona.TAXOcreditos--;
		%creditos--;
		$myPersona.aca_pea_ura++;
		%acabou = false;	
	} else if(%creditos > 0 && $myPersona.aca_pea_pet < %pesquisa.cDevPet){
		$myPersona.TAXOcreditos--;
		%creditos--;
		$myPersona.aca_pea_pet++;
		%acabou = false;	
	} else if(%creditos > 0 && $myPersona.aca_pea_min < %pesquisa.cDevMin){
		$myPersona.TAXOcreditos--;
		%creditos--;
		if(($myPersona.aca_pea_min + 2) > %pesquisa.cDevMin){
			$myPersona.aca_pea_min++;
		} else {
			$myPersona.aca_pea_min += 2;
		}
		%acabou = false;	
	} else {
		%acabou = true;	
		//dá um refresh na academia:
		if(%pesquisaCompleta){
			clientCmda_reconhecerFimDePesquisa(%pesquisa);
		}
		clientPopularStatus();
		academiaCreditos_txt.text = $myPersona.TAXOcreditos;
		if($a_estouVendoAtkOuDefFlag){
			a_mostrarAtkDefTabs($a_estouVendoAtkOuDef, $a_estouVendo);
		} else {
			a_entrarNode($a_estouVendoNode);	
		}
		clientPopServerComDot(); //devolve o controle pro usuário
	}
	
	if(%acabou == false){
		academiaCreditos_txt.text = $myPersona.TAXOcreditos;
		if($a_estouVendoAtkOuDefFlag){
			a_mostrarAtkDefTabs($a_estouVendoAtkOuDef, $a_estouVendo);
		} else {
			a_entrarNode($a_estouVendoNode);	
		}
		schedule(200, 0, "a_investimentoGradual", %creditos, %pesquisa, %acabou, %pesquisaCompleta); 
	}
	clientPopularPEA("academia");
}


function clientPesquisaCompletaOkBtnClick(){
	clientPopPesquisaCompletaMsgBox();	
}

function clientPopPesquisaCompletaMsgBox(){
	canvas.popDialog(msgBoxPesquisaCompletaGui);
}

function clientPushPesquisaCompletaMsgBox()
{
	clientMsgBoxOKT("PESQUISA COMPLETA", "TECNOLOGIA ADQUIRIDA PERMANENTEMENTE.");
}


////////
//investirDef Gui:
function callInvDefGui(){
	canvas.pushDialog(invDefGui);
	invDefGuiCreditosTxt.text = "0";
	verifyInvDefGuiSetas(0);
}

function InvDefGuiCancelar(){
	Canvas.popDialog(invDefGui);
}

function invDefInvestir(){
	%creditos = invDefGuiCreditosTxt.text;
	if(%creditos > 0){
		if(%creditos <= $myPersona.taxoCreditos){
			commandToServer('a_investirCreditosDef', %creditos);
			Canvas.popDialog(invDefGui);
		}
	}
}

function invDefGuiSetaRight(){
	%creditos = invDefGuiCreditosTxt.text;
	%creditos++;
	invDefGuiCreditosTxt.text = %creditos;
	verifyInvDefGuiSetas(%creditos);
}

function invDefGuiSetaRight2(){
	%creditos = invDefGuiCreditosTxt.text;
	%creditos += 10;
	invDefGuiCreditosTxt.text = %creditos;
	verifyInvDefGuiSetas(%creditos);
}

function invDefGuiSetaLeft(){
	%creditos = invDefGuiCreditosTxt.text;
	%creditos--;
	invDefGuiCreditosTxt.text = %creditos;
	verifyInvDefGuiSetas(%creditos);
}

function invDefGuiSetaLeft2(){
	%creditos = invDefGuiCreditosTxt.text;
	%creditos -= 10;
	invDefGuiCreditosTxt.text = %creditos;
	verifyInvDefGuiSetas(%creditos);
}

function verifyCreditosNecessarios(){
	%pesquisa = clientFindPesquisaPorClassId($myPersona.aca_pea_id);
	%maxMin = %pesquisa.cDevMin;
	%maxPet = %pesquisa.cDevPet;
	%maxUra = %pesquisa.cDevUra;
	%myMin = $myPersona.aca_pea_min;
	%myPet = $myPersona.aca_pea_pet;
	%myUra = $myPersona.aca_pea_ura;
	
	%minRestantes = %maxMin - %myMin;
	%petRestantes = %maxPet - %myPet;
	%uraRestantes = %maxUra - %myUra;
	
	%creditosNecessarios = %uraRestantes + %petRestantes + mCeil(%minRestantes / 2);
	
	return(%creditosNecessarios);
}

function verifyInvDefGuiSetas(%creditos){
	%creditosNecessarios = verifyCreditosNecessarios();
		
	//Right:
	if(%creditos + 1 > $myPersona.taxoCreditos || %creditos + 1 > %creditosNecessarios){
		invDefGuiArrowRightBtn.setActive(false);	
	} else {
		invDefGuiArrowRightBtn.setActive(true);	
	}
	if(%creditos + 10 > $myPersona.taxoCreditos || %creditos + 10 > %creditosNecessarios){
		invDefGuiArrowRight2Btn.setActive(false);	
	} else {
		invDefGuiArrowRight2Btn.setActive(true);	
	}
	
	//Left:
	if(%creditos == 0){
		invDefGuiArrowLeftBtn.setActive(false);	
	} else {
		invDefGuiArrowLeftBtn.setActive(true);	
	}
	if(%creditos >= 10){
		invDefGuiArrowLeft2Btn.setActive(true);	
	} else {
		invDefGuiArrowLeft2Btn.setActive(false);	
	}
}