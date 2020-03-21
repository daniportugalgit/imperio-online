// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\pesquisasgulok.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quinta-feira, 28 de agosto de 2008 18:50
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function criarPesquisaG(%tipo, %upgrade, %num, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, %classId){
	%eval = "$pesG_" @ %id @ " = new ScriptObject(){";
	%eval = %eval @ "id = " @ %id @ ";";
	%eval = %eval @ "classId = " @ %classId @ ";"; 
	%eval = %eval @ "tipo = " @ %tipo @ ";";
	%eval = %eval @ "upgrade = " @ %upgrade @ ";";
	%eval = %eval @ "num = " @ %num @ ";";
	%eval = %eval @ "custoInicial = " @ %custoInicial @ ";";
	%eval = %eval @ "cDevMin = " @ %cDevMin @ ";";
	%eval = %eval @ "cDevPet = " @ %cDevPet @ ";";
	%eval = %eval @ "cDevUra = " @ %cDevUra @ ";";
	%eval = %eval @ "cOMNI = " @ mFloor((%custoInicial/3) + (%cDevMin/5) + (%cDevPet/4) + (%cDevUra/4)) @ ";";
	%eval = %eval @ "};";
	eval(%eval);
	
	%eval = "%estaPesquisa = $pesG_" @ %id @ ";";
	eval(%eval);
	
	if(%estaPesquisa.cOMNI > 39){
		%estaPesquisa.cOMNI = 39;	
	}
	
	%estaPesquisa.desc = %tipo @ " >> " @ %upgrade @ " (" @ %num @ ")";
	
	$pesquisasPool_guloks.add(%estaPesquisa);
		
	%estaPesquisa.totalEmCreditos = %custoInicial + (%cDevMin / 2) + %cDevPet + %cDevUra;
	
	echo("PESQUISA (gulok): " @ %estaPesquisa.desc @ "-> Omnis: " @ %estaPesquisa.cOmni @ "| Total em Créditos: " @ %estaPesquisa.totalEmCreditos);
}

//Vermes:
function a_initVermesPesquisas(){
	//defesaMinima:
	for(%i = 0; %i < 7; %i++){
		%id = "aca_s_d_min" @ (%i + 2);
		%custoInicial = (%i * 4) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 4) + 8;
		%cDevUra = %i * 4;
		criarPesquisaG("Vermes", "defesaMinima", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_s_d_min");
	}
		
	//defesaMaxima:
	for(%i = 0; %i < 6; %i++){
		%id = "aca_s_d_max" @ (%i + 9);
		%custoInicial = (%i * 4) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 4) + 8;
		%cDevUra = %i * 4;
		criarPesquisaG("Vermes", "defesaMaxima", %i + 9, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_s_d_max");
	}
	
	//ataqueMinimo:
	for(%i = 0; %i < 7; %i++){
		%id = "aca_s_a_min" @ (%i + 2);
		%custoInicial = (%i * 4) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 4) + 8;
		%cDevUra = %i * 4;
		criarPesquisaG("Vermes", "ataqueMinimo", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_s_a_min");
	}
		
	//ataqueMaximo:
	for(%i = 0; %i < 6; %i++){
		%id = "aca_s_a_max" @ (%i + 9);
		%custoInicial = (%i * 4) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 4) + 8;
		%cDevUra = %i * 4;
		criarPesquisaG("Vermes", "ataqueMaximo", %i + 9, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_s_a_max");
	}
}

//Cefaloks:
function a_initCefaloksPesquisas(){
	//defesaMinima:
	for(%i = 0; %i < 9; %i++){
		%id = "aca_n_d_min" @ (%i + 2);
		%custoInicial = (%i * 3) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 3) + 8;
		%cDevUra = %i * 3;
		criarPesquisaG("Cefaloks", "defesaMinima", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_n_d_min");
	}
		
	//defesaMaxima:
	for(%i = 0; %i < 14; %i++){
		%id = "aca_n_d_max" @ (%i + 15);
		%custoInicial = (%i * 3) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 3) + 8;
		%cDevUra = %i * 3;
		criarPesquisaG("Cefaloks", "defesaMaxima", %i + 15, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_n_d_max");
	}
	
	//ataqueMinimo:
	for(%i = 0; %i < 9; %i++){
		%id = "aca_n_a_min" @ (%i + 2);
		%custoInicial = (%i * 3) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 3) + 8;
		%cDevUra = %i * 3;
		criarPesquisaG("Cefaloks", "ataqueMinimo", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_n_a_min");
	}
		
	//ataqueMaximo:
	for(%i = 0; %i < 14; %i++){
		%id = "aca_n_a_max" @ (%i + 15);
		%custoInicial = (%i * 3) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 3) + 8;
		%cDevUra = %i * 3;
		criarPesquisaG("Cefaloks", "ataqueMaximo", %i + 15, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_n_a_max");
	}
}

//Rainhas:
function a_initRainhasPesquisas(){
	//defesaMinima:
	for(%i = 0; %i < 14; %i++){
		%id = "aca_t_d_min" @ (%i + 2);
		%custoInicial = (%i * 3) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 2) + 8;
		%cDevUra = %i * 2;
		criarPesquisaG("Rainhas", "defesaMinima", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_t_d_min");
	}
		
	//defesaMaxima:
	for(%i = 0; %i < 20; %i++){
		%id = "aca_t_d_max" @ (%i + 16);
		%custoInicial = (%i * 3) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 2) + 8;
		%cDevUra = %i * 2;
		criarPesquisaG("Rainhas", "defesaMaxima", %i + 16, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_t_d_max");
	}
	
	//ataqueMinimo:
	for(%i = 0; %i < 14; %i++){
		%id = "aca_t_a_min" @ (%i + 2);
		%custoInicial = (%i * 3) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 2) + 8;
		%cDevUra = %i * 2;
		criarPesquisaG("Rainhas", "ataqueMinimo", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_t_a_min");
	}
		
	//ataqueMaximo:
	for(%i = 0; %i < 20; %i++){
		%id = "aca_t_a_max" @ (%i + 16);
		%custoInicial = (%i * 3) + 15;
		%cDevMin = (%i * 4) + 12;
		%cDevPet = (%i * 2) + 8;
		%cDevUra = %i * 2;
		criarPesquisaG("Rainhas", "ataqueMaximo", %i + 16, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_t_a_max");
	}
}

//Zangões:
function a_initZangaoPesquisas(){
	for(%a = 1; %a < 3; %a++){
		//defesaMinima:
		for(%i = 0; %i < 13; %i++){
			%id = "aca_ldr_" @ %a @ "_d_min" @ (%i + 2);
			%classId = "aca_ldr_" @ %a @ "_d_min";
			%custoInicial = (%i * 2) + 8;
			%cDevMin = (%i * 2) + 8;
			%cDevPet = (%i * 2) + 8;
			%cDevUra = %i * 2;
			criarPesquisaG("zangao", "defesaMinima", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, %classId);
		}
			
		//defesaMaxima:
		for(%i = 0; %i < 21; %i++){
			%id = "aca_ldr_" @ %a @ "_d_max" @ (%i + 15);
			%classId = "aca_ldr_" @ %a @ "_d_max";
			%custoInicial = (%i * 2) + 8;
			%cDevMin = (%i * 2) + 8;
			%cDevPet = (%i * 2) + 8;
			%cDevUra = %i * 2;
			if(%cDevMin > 30){
				%cDevMin = 30;
			}
			if(%cDevPet > 25){
				%cDevPet = 25;
			}
			if(%cDevUra > 25){
				%cDevUra = 25;
			}
			criarPesquisaG("zangao", "defesaMaxima", %i + 15, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, %classId);
		}
		
		//ataqueMinimo:
		for(%i = 0; %i < 13; %i++){
			%id = "aca_ldr_" @ %a @ "_a_min" @ (%i + 2);
			%classId = "aca_ldr_" @ %a @ "_a_min";
			%custoInicial = (%i * 2) + 8;
			%cDevMin = (%i * 2) + 8;
			%cDevPet = (%i * 2) + 8;
			%cDevUra = %i * 2;
			criarPesquisaG("zangao", "ataqueMinimo", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, %classId);
		}
			
		//ataqueMaximo:
		for(%i = 0; %i < 21; %i++){
			%id = "aca_ldr_" @ %a @ "_a_max" @ (%i + 15);
			%classId = "aca_ldr_" @ %a @ "_a_max";
			%custoInicial = (%i * 2) + 8;
			%cDevMin = (%i * 2) + 8;
			%cDevPet = (%i * 2) + 8;
			%cDevUra = %i * 2;
			if(%cDevMin > 30){
				%cDevMin = 30;
			}
			if(%cDevPet > 25){
				%cDevPet = 25;
			}
			if(%cDevUra > 25){
				%cDevUra = 25;
			}
			criarPesquisaG("zangao", "ataqueMaximo", %i + 15, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, %classId);
		}
		
		//Escudo de Vermes / Escudo:
		criarPesquisaG("zangao", "Carregar", 1, "aca_ldr_" @ %a @ "_h11", 30, 50, 50, 50, "aca_ldr_" @ %a @ "_h1");
		a_setarRequisito("aca_ldr_" @ %a @ "_h11", "Visionario", 6, "gulok");
		criarPesquisaG("zangao", "Carregar", 2, "aca_ldr_" @ %a @ "_h12", 50, 65, 65, 65, "aca_ldr_" @ %a @ "_h1");
		a_setarRequisito("aca_ldr_" @ %a @ "_h12", "Visionario", 20, "gulok");
		criarPesquisaG("zangao", "Carregar", 3, "aca_ldr_" @ %a @ "_h13", 50, 100, 100, 100, "aca_ldr_" @ %a @ "_h1");
		a_setarRequisito("aca_ldr_" @ %a @ "_h13", "Visionario", 40, "gulok");
		
		//Asas / JetPack:
		criarPesquisaG("zangao", "Asas", 1, "aca_ldr_" @ %a @ "_h21", 30, 50, 50, 50, "aca_ldr_" @ %a @ "_h2");
		a_setarRequisito("aca_ldr_" @ %a @ "_h21", "Arrebatador", 1, "gulok");
		criarPesquisaG("zangao", "Asas", 2, "aca_ldr_" @ %a @ "_h22", 50, 65, 65, 65, "aca_ldr_" @ %a @ "_h2");
		a_setarRequisito("aca_ldr_" @ %a @ "_h22", "Arrebatador", 20, "gulok");
		criarPesquisaG("zangao", "Asas", 3, "aca_ldr_" @ %a @ "_h23", 50, 200, 100, 100, "aca_ldr_" @ %a @ "_h2");
		a_setarRequisito("aca_ldr_" @ %a @ "_h23", "Arrebatador", 60, "gulok");
	}
	
	//Pesquisas diferentes para Zangões Preto e Branco:
	//Canibalizar / Sniper (L1):
	criarPesquisaG("zangao", "Canibalizar", 1, "aca_ldr_1_h31", 30, 50, 50, 50, "aca_ldr_1_h3");
	a_setarRequisito("aca_ldr_1_h31", "Arrebatador", 3, "gulok");
	criarPesquisaG("zangao", "Canibalizar", 2, "aca_ldr_1_h32", 50, 65, 65, 65, "aca_ldr_1_h3");
	a_setarRequisito("aca_ldr_1_h32", "Arrebatador", 20, "gulok");
	criarPesquisaG("zangao", "Canibalizar", 3, "aca_ldr_1_h33", 50, 100, 100, 100, "aca_ldr_1_h3");
	a_setarRequisito("aca_ldr_1_h33", "Arrebatador", 40, "gulok");
	
	//Metamorfose / Sniper (L2):
	criarPesquisaG("zangao", "Metamorfose", 1, "aca_ldr_2_h31", 30, 35, 35, 35, "aca_ldr_2_h3");
	a_setarRequisito("aca_ldr_2_h31", "Visionario", 3, "gulok");
	criarPesquisaG("zangao", "Metamorfose", 2, "aca_ldr_2_h32", 50, 65, 65, 65, "aca_ldr_2_h3");
	a_setarRequisito("aca_ldr_2_h32", "Visionario", 6, "gulok");
	criarPesquisaG("zangao", "Metamorfose", 3, "aca_ldr_2_h33", 50, 80, 80, 80, "aca_ldr_2_h3");
	a_setarRequisito("aca_ldr_2_h33", "Visionario", 10, "gulok");
	
	//DevorarRainhas / Moral (L1):
	criarPesquisaG("zangao", "DevorarRainhas", 1, "aca_ldr_1_h41", 50, 50, 50, 50, "aca_ldr_1_h4");
	a_setarRequisito("aca_ldr_1_h41", "Arrebatador", 10, "gulok");
	criarPesquisaG("zangao", "DevorarRainhas", 2, "aca_ldr_1_h42", 50, 65, 65, 65, "aca_ldr_1_h4");
	a_setarRequisito("aca_ldr_1_h42", "Arrebatador", 20, "gulok");
	criarPesquisaG("zangao", "DevorarRainhas", 3, "aca_ldr_1_h43", 50, 100, 100, 100, "aca_ldr_1_h4");
	a_setarRequisito("aca_ldr_1_h43", "Arrebatador", 40, "gulok");
	
	//Cortejar / Moral (L2):
	criarPesquisaG("zangao", "Cortejar", 1, "aca_ldr_2_h41", 50, 50, 50, 50, "aca_ldr_2_h4");
	a_setarRequisito("aca_ldr_2_h41", "Visionario", 10, "gulok");
	criarPesquisaG("zangao", "Cortejar", 2, "aca_ldr_2_h42", 50, 65, 65, 65, "aca_ldr_2_h4");
	a_setarRequisito("aca_ldr_2_h42", "Visionario", 20, "gulok");
	criarPesquisaG("zangao", "Cortejar", 3, "aca_ldr_2_h43", 50, 100, 100, 100, "aca_ldr_2_h4");
	a_setarRequisito("aca_ldr_2_h43", "Visionario", 40, "gulok");
}

function a_initDragnalPesquisas(){
	//Entregar / Escudo:
	criarPesquisaG("Dragnal", "Entregar", 1, "aca_ldr_3_h11", 50, 80, 80, 80, "aca_ldr_3_h1");
	a_setarRequisito("aca_ldr_3_h11", "Visionario", 20, "gulok");
	criarPesquisaG("Dragnal", "Entregar", 2, "aca_ldr_3_h12", 50, 100, 100, 100, "aca_ldr_3_h1");
	a_setarRequisito("aca_ldr_3_h12", "Visionario", 30, "gulok");
	criarPesquisaG("Dragnal", "Entregar", 3, "aca_ldr_3_h13", 50, 150, 150, 150, "aca_ldr_3_h1");
	a_setarRequisito("aca_ldr_3_h13", "Visionario", 40, "gulok");
	
	//Sopro / JetPack:
	criarPesquisaG("Dragnal", "Sopro", 1, "aca_ldr_3_h21", 50, 80, 80, 80, "aca_ldr_3_h2");
	a_setarRequisito("aca_ldr_3_h21", "Visionario", 20, "gulok");
	criarPesquisaG("Dragnal", "Sopro", 2, "aca_ldr_3_h22", 100, 100, 100, 100, "aca_ldr_3_h2");
	a_setarRequisito("aca_ldr_3_h22", "Visionario", 30, "gulok");
	criarPesquisaG("Dragnal", "Sopro", 3, "aca_ldr_3_h23", 50, 150, 150, 150, "aca_ldr_3_h2");
	a_setarRequisito("aca_ldr_3_h23", "Visionario", 40, "gulok");
	
	//Fúria / Sniper:
	criarPesquisaG("Dragnal", "Furia", 1, "aca_ldr_3_h31", 50, 100, 100, 100, "aca_ldr_3_h3");
	a_setarRequisito("aca_ldr_3_h31", "Arrebatador", 30, "gulok");
	criarPesquisaG("Dragnal", "Furia", 2, "aca_ldr_3_h32", 50, 150, 150, 150, "aca_ldr_3_h3");
	a_setarRequisito("aca_ldr_3_h32", "Arrebatador", 60, "gulok");
	criarPesquisaG("Dragnal", "Furia", 3, "aca_ldr_3_h33", 50, 200, 200, 200, "aca_ldr_3_h3");
	a_setarRequisito("aca_ldr_3_h33", "Arrebatador", 80, "gulok");
	
	//Covil / Moral:
	criarPesquisaG("Dragnal", "Covil", 1, "aca_ldr_3_h41", 50, 150, 150, 150, "aca_ldr_3_h4");
	a_setarRequisito("aca_ldr_3_h41", "Arrebatador", 40, "gulok");
	criarPesquisaG("Dragnal", "Covil", 2, "aca_ldr_3_h42", 50, 175, 175, 175, "aca_ldr_3_h4");
	a_setarRequisito("aca_ldr_3_h42", "Arrebatador", 60, "gulok");
	criarPesquisaG("Dragnal", "Covil", 3, "aca_ldr_3_h43", 50, 200, 200, 200, "aca_ldr_3_h4");
	a_setarRequisito("aca_ldr_3_h43", "Arrebatador", 80, "gulok");
}

function a_initGVisionarioPesquisas(){
	//Visionário:
	criarPesquisaG("visionario", "Metabolismo", 1, "aca_v_11", 50, 80, 80, 80, "aca_v_1");
	a_setarRequisito("aca_v_11", "Visionario", 20, "gulok");
	criarPesquisaG("visionario", "Metabolismo", 2, "aca_v_12", 50, 100, 100, 100, "aca_v_1");
	a_setarRequisito("aca_v_12", "Visionario", 30, "gulok"); 
	criarPesquisaG("visionario", "Metabolismo", 3, "aca_v_13", 50, 150, 150, 150, "aca_v_1");
	a_setarRequisito("aca_v_13", "Visionario", 40, "gulok");
		
	criarPesquisaG("visionario", "Instinto", 1, "aca_v_21", 30, 35, 35, 35, "aca_v_2");
	a_setarRequisito("aca_v_21", "Visionario", 1, "gulok");
	criarPesquisaG("visionario", "Instinto", 2, "aca_v_22", 40, 50, 50, 50, "aca_v_2");
	a_setarRequisito("aca_v_22", "Visionario", 10, "gulok");
	criarPesquisaG("visionario", "Instinto", 3, "aca_v_23", 50, 65, 65, 65, "aca_v_2");
	a_setarRequisito("aca_v_23", "Visionario", 20, "gulok");
	
	criarPesquisaG("visionario", "Incorporar", 1, "aca_v_31", 30, 35, 35, 35, "aca_v_3");
	a_setarRequisito("aca_v_31", "Visionario", 3, "gulok");
	criarPesquisaG("visionario", "Incorporar", 2, "aca_v_32", 40, 50, 50, 50, "aca_v_3");
	a_setarRequisito("aca_v_32", "Visionario", 6, "gulok");
	criarPesquisaG("visionario", "Incorporar", 3, "aca_v_33", 50, 65, 65, 65, "aca_v_3");
	a_setarRequisito("aca_v_33", "Visionario", 20, "gulok");
	
	criarPesquisaG("visionario", "Submergir", 1, "aca_v_41", 40, 40, 40, 40, "aca_v_4");
	a_setarRequisito("aca_v_41", "Visionario", 6, "gulok");
	criarPesquisaG("visionario", "Submergir", 2, "aca_v_42", 50, 50, 50, 50, "aca_v_4");
	a_setarRequisito("aca_v_42", "Visionario", 10, "gulok");
	criarPesquisaG("visionario", "Submergir", 3, "aca_v_43", 50, 65, 65, 65, "aca_v_4");
	a_setarRequisito("aca_v_43", "Visionario", 20, "gulok");
	
	criarPesquisaG("visionario", "Crisalida", 1, "aca_v_51", 50, 65, 65, 65, "aca_v_5");
	a_setarRequisito("aca_v_51", "Visionario", 10, "gulok");
	criarPesquisaG("visionario", "Crisalida", 2, "aca_v_52", 50, 80, 80, 80, "aca_v_5");
	a_setarRequisito("aca_v_52", "Visionario", 20, "gulok");
	criarPesquisaG("visionario", "Crisalida", 3, "aca_v_53", 50, 100, 100, 100, "aca_v_5");
	a_setarRequisito("aca_v_53", "Visionario", 30, "gulok"); 
	
	criarPesquisaG("visionario", "Matriarca", 1, "aca_v_61", 50, 80, 80, 80, "aca_v_6");
	a_setarRequisito("aca_v_61", "Visionario", 20, "gulok");
	criarPesquisaG("visionario", "Matriarca", 2, "aca_v_62", 50, 100, 100, 100, "aca_v_6");
	a_setarRequisito("aca_v_62", "Visionario", 30, "gulok");
	criarPesquisaG("visionario", "Matriarca", 3, "aca_v_63", 50, 150, 150, 150, "aca_v_6");
	a_setarRequisito("aca_v_63", "Visionario", 40, "gulok");
}

function a_initGArrebatadorPesquisas(){
	//Arrebatador:
	criarPesquisaG("Arrebatador", "Exoesqueleto", 1, "aca_a_11", 10, 20, 20, 20, "aca_a_1");
	a_setarRequisito("aca_a_11", "Arrebatador", 1, "gulok");
	criarPesquisaG("Arrebatador", "Exoesqueleto", 2, "aca_a_12", 25, 30, 30, 30, "aca_a_1");
	a_setarRequisito("aca_a_12", "Arrebatador", 10, "gulok");
	criarPesquisaG("Arrebatador", "Exoesqueleto", 3, "aca_a_13", 30, 50, 50, 50, "aca_a_1");
	a_setarRequisito("aca_a_13", "Arrebatador", 20, "gulok");
	
	criarPesquisaG("Arrebatador", "Horda", 1, "aca_a_21", 35, 35, 35, 35, "aca_a_2");
	a_setarRequisito("aca_a_21", "Arrebatador", 6, "gulok");
	criarPesquisaG("Arrebatador", "Horda", 2, "aca_a_22", 50, 65, 65, 65, "aca_a_2");
	a_setarRequisito("aca_a_22", "Arrebatador", 20, "gulok");
	criarPesquisaG("Arrebatador", "Horda", 3, "aca_a_23", 50, 100, 100, 100, "aca_a_2");
	a_setarRequisito("aca_a_23", "Arrebatador", 40, "gulok");
}

function a_initGIntelPesquisas(){
	//intel:
	criarPesquisaG("Intel", "Espionagem", 1, "aca_i_11", 30, 30, 30, 30, "aca_i_1");
	a_setarRequisito("aca_i_11", "Visionario", 1, "gulok");
	criarPesquisaG("Intel", "Espionagem", 2, "aca_i_12", 50, 50, 50, 50, "aca_i_1");
	a_setarRequisito("aca_i_12", "Visionario", 30, "gulok");
	criarPesquisaG("Intel", "Espionagem", 3, "aca_i_13", 50, 100, 100, 100, "aca_i_1");
	a_setarRequisito("aca_i_13", "Visionario", 60, "gulok");

	criarPesquisaG("Intel", "Pilhar", 1, "aca_i_21", 30, 30, 30, 30, "aca_i_2");
	a_setarRequisito("aca_i_21", "Arrebatador", 3, "gulok");
	criarPesquisaG("Intel", "Pilhar", 2, "aca_i_22", 50, 50, 50, 50, "aca_i_2");
	a_setarRequisito("aca_i_22", "Arrebatador", 10, "gulok");
	criarPesquisaG("Intel", "Pilhar", 3, "aca_i_23", 50, 65, 65, 65, "aca_i_2");
	a_setarRequisito("aca_i_23", "Arrebatador", 20, "gulok");
	
	criarPesquisaG("Intel", "Dragnal", 1, "aca_i_31", 30, 30, 30, 30, "aca_i_3");
	a_setarRequisito("aca_i_31", "Arrebatador", 20, "gulok");
	criarPesquisaG("Intel", "Dragnal", 2, "aca_i_32", 50, 50, 50, 50, "aca_i_3");
	a_setarRequisito("aca_i_32", "Arrebatador", 20, "gulok");
	criarPesquisaG("Intel", "Dragnal", 3, "aca_i_33", 50, 65, 65, 65, "aca_i_3");
	a_setarRequisito("aca_i_33", "Arrebatador", 20, "gulok");
}

function a_initGComerciantePesquisas(){
	//Comerciante:
	criarPesquisaG("Comerciante", "Faro", 1, "aca_c_11", 35, 50, 50, 50, "aca_c_1");
	a_setarRequisito("aca_c_11", "Vitorias", 3, "gulok"); 
	criarPesquisaG("Comerciante", "Faro", 2, "aca_c_12", 50, 65, 65, 65, "aca_c_1");
	a_setarRequisito("aca_c_12", "Vitorias", 10, "gulok");
	criarPesquisaG("Comerciante", "Faro", 3, "aca_c_13", 50, 80, 80, 80, "aca_c_1");
	a_setarRequisito("aca_c_13", "Vitorias", 25, "gulok");
}

function a_initGDiplomataPesquisas(){
	//Diplomata:
	criarPesquisaG("Diplomata", "Fertilidade", 1, "aca_d_11", 35, 40, 40, 40, "aca_d_1");
	a_setarRequisito("aca_d_11", "Vitorias", 3, "gulok");
	criarPesquisaG("Diplomata", "Fertilidade", 2, "aca_d_12", 50, 50, 50, 50, "aca_d_1");
	a_setarRequisito("aca_d_12", "Vitorias", 10, "gulok");
	criarPesquisaG("Diplomata", "Fertilidade", 3, "aca_d_13", 50, 80, 80, 80, "aca_d_1");
	a_setarRequisito("aca_d_13", "Vitorias", 25, "gulok");
}

function a_initGPlanetasPesquisas(){
	criarPesquisaG("Planetas", "Teluria", 1, "aca_pln_11", 50, 50, 50, 50, "aca_pln_1");
	a_setarRequisito("aca_pln_11", "Visionario", 20, "gulok");
}

function a_initGAVPesquisas(){
	criarPesquisaG("Avancadas", "Especializar", 1, "aca_av_11", 50, 100, 100, 100, "aca_av_1");
	a_setarRequisito("aca_av_11", "Arrebatador", 60, "gulok");
	criarPesquisaG("Avancadas", "Especializar", 2, "aca_av_12", 50, 150, 150, 150, "aca_av_1");
	a_setarRequisito("aca_av_12", "Arrebatador", 80, "gulok");
	criarPesquisaG("Avancadas", "Especializar", 3, "aca_av_13", 50, 200, 200, 200, "aca_av_1");
	a_setarRequisito("aca_av_13", "Arrebatador", 100, "gulok");
	
	criarPesquisaG("Avancadas", "Virus", 1, "aca_av_21", 50, 125, 125, 125, "aca_av_2");
	a_setarRequisito("aca_av_21", "Arrebatador", 100, "gulok");
	criarPesquisaG("Avancadas", "Virus", 2, "aca_av_22", 50, 160, 160, 160, "aca_av_2");
	a_setarRequisito("aca_av_22", "Arrebatador", 130, "gulok");
	criarPesquisaG("Avancadas", "Virus", 3, "aca_av_23", 50, 200, 200, 200, "aca_av_2");
	a_setarRequisito("aca_av_23", "Arrebatador", 160, "gulok");
	
	criarPesquisaG("Avancadas", "Expulsar", 1, "aca_av_31", 50, 100, 100, 100, "aca_av_3");
	a_setarRequisito("aca_av_31", "Visionario", 60, "gulok");
	criarPesquisaG("Avancadas", "Expulsar", 2, "aca_av_32", 50, 125, 125, 125, "aca_av_3");
	a_setarRequisito("aca_av_32", "Visionario", 80, "gulok");
	criarPesquisaG("Avancadas", "Expulsar", 3, "aca_av_33", 50, 150, 150, 150, "aca_av_3");
	a_setarRequisito("aca_av_33", "Visionario", 100, "gulok");
	
	criarPesquisaG("Avancadas", "Evolucao", 1, "aca_av_41", 50, 125, 125, 125, "aca_av_4");
	a_setarRequisito("aca_av_41", "Visionario", 100, "gulok");
	criarPesquisaG("Avancadas", "Evolucao", 2, "aca_av_42", 50, 160, 160, 160, "aca_av_4");
	a_setarRequisito("aca_av_42", "Visionario", 130, "gulok");
	criarPesquisaG("Avancadas", "Evolucao", 3, "aca_av_43", 50, 200, 200, 200, "aca_av_4");
	a_setarRequisito("aca_av_43", "Visionario", 160, "gulok");
}



//requisitos de atk e def:
//Vermes:
function a_initGAtkEDefReq(){
	a_setarRequisito("aca_s_d_min4", "Pontos", 60, "gulok");
	a_setarRequisito("aca_s_a_min4", "Pontos", 60, "gulok");
	a_setarRequisito("aca_s_d_min6", "Pontos", 200, "gulok");
	a_setarRequisito("aca_s_a_min6", "Pontos", 200, "gulok");
	a_setarRequisito("aca_s_d_min8", "Pontos", 500, "gulok");
	a_setarRequisito("aca_s_a_min8", "Pontos", 500, "gulok");
	a_setarRequisito("aca_s_d_max10", "Pontos", 60, "gulok");
	a_setarRequisito("aca_s_a_max10", "Pontos", 60, "gulok");
	a_setarRequisito("aca_s_d_max11", "Pontos", 200, "gulok");
	a_setarRequisito("aca_s_a_max11", "Pontos", 200, "gulok");
	a_setarRequisito("aca_s_d_max12", "Pontos", 500, "gulok");
	a_setarRequisito("aca_s_a_max12", "Pontos", 500, "gulok");
	a_setarRequisito("aca_s_d_max13", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_s_a_max13", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_s_d_max14", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_s_a_max14", "Pontos", 1500, "gulok");
	//Rainhas:
	a_setarRequisito("aca_t_d_min5", "Pontos", 60, "gulok");
	a_setarRequisito("aca_t_a_min5", "Pontos", 60, "gulok");
	a_setarRequisito("aca_t_d_min8", "Pontos", 200, "gulok");
	a_setarRequisito("aca_t_a_min8", "Pontos", 200, "gulok");
	a_setarRequisito("aca_t_d_min12", "Pontos", 500, "gulok");
	a_setarRequisito("aca_t_a_min12", "Pontos", 500, "gulok");
	a_setarRequisito("aca_t_d_min13", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_t_a_min13", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_t_d_min14", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_t_a_min14", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_t_d_min15", "Pontos", 2000, "gulok");
	a_setarRequisito("aca_t_a_min15", "Pontos", 2000, "gulok");
	a_setarRequisito("aca_t_d_max20", "Pontos", 60, "gulok");
	a_setarRequisito("aca_t_a_max20", "Pontos", 60, "gulok");
	a_setarRequisito("aca_t_d_max25", "Pontos", 200, "gulok");
	a_setarRequisito("aca_t_a_max25", "Pontos", 200, "gulok");
	a_setarRequisito("aca_t_d_max30", "Pontos", 500, "gulok");
	a_setarRequisito("aca_t_a_max30", "Pontos", 500, "gulok");
	a_setarRequisito("aca_t_d_max33", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_t_a_max33", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_t_d_max34", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_t_a_max34", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_t_d_max35", "Pontos", 2000, "gulok");
	a_setarRequisito("aca_t_a_max35", "Pontos", 2000, "gulok");
	//Cefaloks:
	a_setarRequisito("aca_n_d_min5", "Pontos", 60, "gulok");
	a_setarRequisito("aca_n_a_min5", "Pontos", 60, "gulok");
	a_setarRequisito("aca_n_d_min7", "Pontos", 200, "gulok");
	a_setarRequisito("aca_n_a_min7", "Pontos", 200, "gulok");
	a_setarRequisito("aca_n_d_min8", "Pontos", 500, "gulok");
	a_setarRequisito("aca_n_a_min8", "Pontos", 500, "gulok");
	a_setarRequisito("aca_n_d_min9", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_n_a_min9", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_n_d_min10", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_n_a_min10", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_n_d_max18", "Pontos", 60, "gulok");
	a_setarRequisito("aca_n_a_max18", "Pontos", 60, "gulok");
	a_setarRequisito("aca_n_d_max22", "Pontos", 200, "gulok");
	a_setarRequisito("aca_n_a_max22", "Pontos", 200, "gulok");
	a_setarRequisito("aca_n_d_max24", "Pontos", 500, "gulok");
	a_setarRequisito("aca_n_a_max24", "Pontos", 500, "gulok");
	a_setarRequisito("aca_n_d_max26", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_n_a_max26", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_n_d_max27", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_n_a_max27", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_n_d_max28", "Pontos", 2000, "gulok");
	a_setarRequisito("aca_n_a_max28", "Pontos", 2000, "gulok");
	//Zangões (o branco tem requisitos de defesa e o preto de ataque):
	a_setarRequisito("aca_ldr_1_a_min5", "Pontos", 60, "gulok");
	a_setarRequisito("aca_ldr_2_d_min5", "Pontos", 60, "gulok");
	a_setarRequisito("aca_ldr_1_a_min8", "Pontos", 200, "gulok");
	a_setarRequisito("aca_ldr_2_d_min8", "Pontos", 200, "gulok");
	a_setarRequisito("aca_ldr_1_a_min10", "Pontos", 500, "gulok");
	a_setarRequisito("aca_ldr_2_d_min10", "Pontos", 500, "gulok");
	a_setarRequisito("aca_ldr_1_a_min12", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_ldr_2_d_min12", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_ldr_1_a_min13", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_ldr_2_d_min13", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_ldr_1_a_min14", "Pontos", 2000, "gulok");
	a_setarRequisito("aca_ldr_2_d_min14", "Pontos", 2000, "gulok");
	a_setarRequisito("aca_ldr_1_a_max20", "Pontos", 60, "gulok");
	a_setarRequisito("aca_ldr_2_d_max20", "Pontos", 60, "gulok");
	a_setarRequisito("aca_ldr_1_a_max25", "Pontos", 200, "gulok");
	a_setarRequisito("aca_ldr_2_d_max25", "Pontos", 200, "gulok");
	a_setarRequisito("aca_ldr_1_a_max30", "Pontos", 500, "gulok");
	a_setarRequisito("aca_ldr_2_d_max30", "Pontos", 500, "gulok");
	a_setarRequisito("aca_ldr_1_a_max32", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_ldr_2_d_max32", "Pontos", 1000, "gulok");
	a_setarRequisito("aca_ldr_1_a_max34", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_ldr_2_d_max34", "Pontos", 1500, "gulok");
	a_setarRequisito("aca_ldr_1_a_max35", "Pontos", 2000, "gulok");
	a_setarRequisito("aca_ldr_2_d_max35", "Pontos", 2000, "gulok");
}





