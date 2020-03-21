// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverPesquisas.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  domingo, 16 de março de 2008 15:29
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function a_initSimSets(){
	$pesquisasPool_humanos = new SimSet();
	$pesquisasPool_guloks = new SimSet();
	//echo("SimSets de Pesquisas Inicializados");
}
a_initSimSets();

//usada pra criar pesquisas:
function criarPesquisa(%tipo, %upgrade, %num, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, %classId){
	%eval = "$pes_" @ %id @ " = new ScriptObject(){";
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
	
	%eval = "%estaPesquisa = $pes_" @ %id @ ";";
	eval(%eval);
	
	if(%estaPesquisa.cOMNI > 39){
		%estaPesquisa.cOMNI = 39;	
	}
	
	%estaPesquisa.desc = %tipo @ " >> " @ %upgrade @ " (" @ %num @ ")";
	
	$pesquisasPool_humanos.add(%estaPesquisa);
		
	%estaPesquisa.totalEmCreditos = %custoInicial + (%cDevMin / 2) + %cDevPet + %cDevUra;
	
	echo("PESQUISA (humanos): " @ %estaPesquisa.desc @ "-> Omnis: " @ %estaPesquisa.cOmni @ "| Total em Créditos: " @ %estaPesquisa.totalEmCreditos);
}

//
// REQUISITOS:
//
function findPesquisaPorId(%id, %especie){
	//echo("FIND PESQUISA POR ID: " @ %id @ "; especie: " @ %especie);
	if(%especie $= "gulok"){
		for(%i = 0; %i < $pesquisasPool_guloks.getCount(); %i++){
			%pesquisa = $pesquisasPool_guloks.getObject(%i);
			if(%pesquisa.id $= %id){
				%pesquisaEncontrada = $pesquisasPool_guloks.getObject(%i);
				%i = $pesquisasPool_guloks.getCount();
			}
		}
	} else {
		for(%i = 0; %i < $pesquisasPool_humanos.getCount(); %i++){
			%pesquisa = $pesquisasPool_humanos.getObject(%i);
			if(%pesquisa.id $= %id){
				%pesquisaEncontrada = $pesquisasPool_humanos.getObject(%i);
				%i = $pesquisasPool_humanos.getCount();
			}
		}
	}
	
	
	return %pesquisaEncontrada;
}

function a_setarRequisito(%pesquisaId, %reqTipo, %reqValor, %especie){
	%pesquisa = findPesquisaPorId(%pesquisaId, %especie);	
	%pesquisa.requisitoTipo = %reqTipo;
	%pesquisa.requisitoValor = %reqValor;
	//echo("Requisito setado na pesquisa " @ %pesquisaId @ ": " @ %pesquisa.requisitoTipo @ " (" @ %pesquisa.requisitoValor @ ")"); 
}


//Líderes:
function a_initLiderPesquisas(){
	for(%a = 1; %a < 4; %a++){
		//defesaMinima:
		for(%i = 0; %i < 7; %i++){
			%id = "aca_ldr_" @ %a @ "_d_min" @ (%i + 2);
			%classId = "aca_ldr_" @ %a @ "_d_min";
			%custoInicial = (%i * 3) + 6;
			%cDevMin = (%i * 3) + 3;
			%cDevPet = (%i * 3) + 5;
			%cDevUra = %i * 4;
			//criarPesquisa("lider", "defesaMinima", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "DMn");
			criarPesquisa("lider", "defesaMinima", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, %classId);
		}
			
		//defesaMaxima:
		for(%i = 0; %i < 23; %i++){
			%id = "aca_ldr_" @ %a @ "_d_max" @ (%i + 13);
			%classId = "aca_ldr_" @ %a @ "_d_max";
			%custoInicial = (%i * 2) + 6;
			%cDevMin = (%i * 3) + 5;
			%cDevPet = %i + 6;
			%cDevUra = %i * 3;
			if(%cDevMin > 30){
				%cDevMin = 30;
			}
			if(%cDevPet > 25){
				%cDevPet = 25;
			}
			if(%cDevUra > 25){
				%cDevUra = 25;
			}
			criarPesquisa("lider", "defesaMaxima", %i + 13, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, %classId);
		}
		
		//ataqueMinimo:
		for(%i = 0; %i < 7; %i++){
			%id = "aca_ldr_" @ %a @ "_a_min" @ (%i + 2);
			%classId = "aca_ldr_" @ %a @ "_a_min";
			%custoInicial = (%i * 3) + 6;
			%cDevMin = (%i * 3) + 3;
			%cDevPet = (%i * 3) + 5;
			%cDevUra = %i * 4;
			criarPesquisa("lider", "ataqueMinimo", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, %classId);
		}
			
		//ataqueMaximo:
		for(%i = 0; %i < 23; %i++){
			%id = "aca_ldr_" @ %a @ "_a_max" @ (%i + 13);
			%classId = "aca_ldr_" @ %a @ "_a_max";
			%custoInicial = (%i * 2) + 6;
			%cDevMin = (%i * 3) + 5;
			%cDevPet = (%i * 2) + 6;
			%cDevUra = %i * 3;
			if(%cDevMin > 30){
				%cDevMin = 30;
			}
			if(%cDevPet > 25){
				%cDevPet = 25;
			}
			if(%cDevUra > 25){
				%cDevUra = 25;
			}
			criarPesquisa("lider", "ataqueMaximo", %i + 13, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, %classId);
		}
		
		//escudo:
		criarPesquisa("lider", "Escudo", 1, "aca_ldr_" @ %a @ "_h11", 20, 15, 15, 25, "aca_ldr_" @ %a @ "_h1");
		a_setarRequisito("aca_ldr_" @ %a @ "_h11", "Arrebatador", 3);
		criarPesquisa("lider", "Escudo", 2, "aca_ldr_" @ %a @ "_h12", 50, 50, 50, 50, "aca_ldr_" @ %a @ "_h1");
		a_setarRequisito("aca_ldr_" @ %a @ "_h12", "Arrebatador", 20);
		criarPesquisa("lider", "Escudo", 3, "aca_ldr_" @ %a @ "_h13", 50, 100, 100, 100, "aca_ldr_" @ %a @ "_h1");
		a_setarRequisito("aca_ldr_" @ %a @ "_h13", "Arrebatador", 60);
		
		//jetPack:
		criarPesquisa("lider", "jetPack", 1, "aca_ldr_" @ %a @ "_h21", 30, 25, 25, 35, "aca_ldr_" @ %a @ "_h2");
		a_setarRequisito("aca_ldr_" @ %a @ "_h21", "Arrebatador", 6);
		criarPesquisa("lider", "jetPack", 2, "aca_ldr_" @ %a @ "_h22", 50, 50, 50, 50, "aca_ldr_" @ %a @ "_h2");
		a_setarRequisito("aca_ldr_" @ %a @ "_h22", "Arrebatador", 20);
		criarPesquisa("lider", "jetPack", 3, "aca_ldr_" @ %a @ "_h23", 50, 200, 100, 100, "aca_ldr_" @ %a @ "_h2");
		a_setarRequisito("aca_ldr_" @ %a @ "_h23", "Arrebatador", 80);
		
		//sniper:
		criarPesquisa("lider", "Sniper", 1, "aca_ldr_" @ %a @ "_h31", 30, 25, 25, 35, "aca_ldr_" @ %a @ "_h3");
		a_setarRequisito("aca_ldr_" @ %a @ "_h31", "Arrebatador", 10);
		criarPesquisa("lider", "Sniper", 2, "aca_ldr_" @ %a @ "_h32", 50, 50, 50, 50, "aca_ldr_" @ %a @ "_h3");
		a_setarRequisito("aca_ldr_" @ %a @ "_h32", "Arrebatador", 20);
		criarPesquisa("lider", "Sniper", 3, "aca_ldr_" @ %a @ "_h33", 50, 100, 100, 100, "aca_ldr_" @ %a @ "_h3");
		a_setarRequisito("aca_ldr_" @ %a @ "_h33", "Arrebatador", 60);
		
		//moral:
		criarPesquisa("lider", "Moral", 1, "aca_ldr_" @ %a @ "_h41", 50, 50, 50, 50, "aca_ldr_" @ %a @ "_h4");
		a_setarRequisito("aca_ldr_" @ %a @ "_h41", "Arrebatador", 20);
		criarPesquisa("lider", "Moral", 2, "aca_ldr_" @ %a @ "_h42", 50, 50, 50, 50, "aca_ldr_" @ %a @ "_h4");
		a_setarRequisito("aca_ldr_" @ %a @ "_h42", "Arrebatador", 30);
		criarPesquisa("lider", "Moral", 3, "aca_ldr_" @ %a @ "_h43", 50, 200, 100, 100, "aca_ldr_" @ %a @ "_h4");
		a_setarRequisito("aca_ldr_" @ %a @ "_h43", "Arrebatador", 130);
	}
}

//Soldados:
function a_initSoldadosPesquisas(){
	//defesaMinima:
	for(%i = 0; %i < 5; %i++){
		%id = "aca_s_d_min" @ (%i + 2);
		%custoInicial = (%i * 5) + 15;
		%cDevMin = (%i * 4) + 10;
		%cDevPet = (%i * 4) + 6;
		%cDevUra = %i * 4;
		criarPesquisa("Soldados", "defesaMinima", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_s_d_min");
	}
		
	//defesaMaxima:
	for(%i = 0; %i < 4; %i++){
		%id = "aca_s_d_max" @ (%i + 7);
		%custoInicial = (%i * 5) + 15;
		%cDevMin = (%i * 4) + 10;
		%cDevPet = (%i * 4) + 6;
		%cDevUra = %i * 4;
		criarPesquisa("Soldados", "defesaMaxima", %i + 7, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_s_d_max");
	}
	
	//ataqueMinimo:
	for(%i = 0; %i < 5; %i++){
		%id = "aca_s_a_min" @ (%i + 2);
		%custoInicial = (%i * 5) + 15;
		%cDevMin = (%i * 4) + 10;
		%cDevPet = (%i * 4) + 6;
		%cDevUra = %i * 4;
		criarPesquisa("Soldados", "ataqueMinimo", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_s_a_min");
	}
		
	//ataqueMaximo:
	for(%i = 0; %i < 4; %i++){
		%id = "aca_s_a_max" @ (%i + 7);
		%custoInicial = (%i * 5) + 15;
		%cDevMin = (%i * 4) + 10;
		%cDevPet = (%i * 4) + 6;
		%cDevUra = %i * 4;
		criarPesquisa("Soldados", "ataqueMaximo", %i + 7, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_s_a_max");
	}
}

//Tanques e Navios:
function a_initTanquesENaviosPesquisas(){
	//defesaMinima:
	for(%i = 0; %i < 5; %i++){
		%id = "aca_t_d_min" @ (%i + 2);
		%custoInicial = (%i * 4) + 15;
		%cDevMin = (%i * 3) + 10;
		%cDevPet = (%i * 3) + 6;
		%cDevUra = %i * 4;
		criarPesquisa("Tanques", "defesaMinima", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_t_d_min");
		
		%id = "aca_n_d_min" @ (%i + 2);
		criarPesquisa("Navios", "defesaMinima", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_n_d_min");
	}
		
	//defesaMaxima:
	for(%i = 0; %i < 13; %i++){
		%id = "aca_t_d_max" @ (%i + 13);
		%custoInicial = (%i * 3) + 15;
		%cDevMin = (%i * 3) + 10;
		%cDevPet = (%i * 3) + 6;
		%cDevUra = %i * 3;
		if(%cDevMin > 35){
			%cDevMin = 35;
		}
		if(%cDevPet > 25){
			%cDevPet = 25;
		}
		if(%cDevUra > 35){
			%cDevUra = 35;
		}
		criarPesquisa("Tanques", "defesaMaxima", %i + 13, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_t_d_max");
		
		%id = "aca_n_d_max" @ (%i + 13);
		criarPesquisa("Navios", "defesaMaxima", %i + 13, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_n_d_max");
	}
	
	//ataqueMinimo:
	for(%i = 0; %i < 5; %i++){
		%id = "aca_t_a_min" @ (%i + 2);
		%custoInicial = (%i * 4) + 15;
		%cDevMin = (%i * 3) + 10;
		%cDevPet = (%i * 3) + 6;
		%cDevUra = %i * 4;
		criarPesquisa("Tanques", "ataqueMinimo", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_t_a_min");
		
		%id = "aca_n_a_min" @ (%i + 2);
		criarPesquisa("Navios", "ataqueMinimo", %i + 2, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_n_a_min");
	}
		
	//ataqueMaximo:
	for(%i = 0; %i < 13; %i++){
		%id = "aca_t_a_max" @ (%i + 13);
		%custoInicial = (%i * 3) + 15;
		%cDevMin = (%i * 3) + 10;
		%cDevPet = (%i * 3) + 6;
		%cDevUra = %i * 3;
		if(%cDevMin > 35){
			%cDevMin = 35;
		}
		if(%cDevPet > 25){
			%cDevPet = 25;
		}
		if(%cDevUra > 35){
			%cDevUra = 35;
		}
		criarPesquisa("Tanques", "ataqueMaximo", %i + 13, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_t_a_max");
		
		%id = "aca_n_a_max" @ (%i + 13);
		criarPesquisa("Navios", "ataqueMaximo", %i + 13, %id, %custoInicial, %cDevMin, %cDevPet, %cDevUra, "aca_n_a_max");
	}
}

function a_initVisionarioPesquisas(){
	//Visionário:
	criarPesquisa("visionario", "Intel", 1, "aca_v_11", 25, 25, 25, 25, "aca_v_1");
	a_setarRequisito("aca_v_11", "Visionario", 1);
	criarPesquisa("visionario", "Intel", 2, "aca_v_12", 50, 50, 50, 50, "aca_v_1");
	a_setarRequisito("aca_v_12", "Visionario", 20); //ver mapa de todos os planetas
		
	criarPesquisa("visionario", "Transporte", 1, "aca_v_21", 25, 25, 25, 15, "aca_v_2");
	a_setarRequisito("aca_v_21", "Visionario", 3);
	criarPesquisa("visionario", "Transporte", 2, "aca_v_22", 25, 25, 25, 30, "aca_v_2");
	a_setarRequisito("aca_v_22", "Visionario", 3);
	criarPesquisa("visionario", "Transporte", 3, "aca_v_23", 35, 30, 30, 30, "aca_v_2");
	a_setarRequisito("aca_v_23", "Visionario", 3);
	
	criarPesquisa("visionario", "Reciclagem", 1, "aca_v_31", 30, 20, 20, 25, "aca_v_3");
	a_setarRequisito("aca_v_31", "Visionario", 6);
	criarPesquisa("visionario", "Reciclagem", 2, "aca_v_32", 35, 25, 25, 35, "aca_v_3");
	a_setarRequisito("aca_v_32", "Visionario", 6);
	criarPesquisa("visionario", "Reciclagem", 3, "aca_v_33", 50, 50, 50, 50, "aca_v_3");
	a_setarRequisito("aca_v_33", "Visionario", 10);
	
	criarPesquisa("visionario", "Refinaria", 1, "aca_v_41", 35, 40, 40, 40, "aca_v_4");
	a_setarRequisito("aca_v_41", "Visionario", 10);
	criarPesquisa("visionario", "Refinaria", 2, "aca_v_42", 50, 50, 50, 50, "aca_v_4");
	a_setarRequisito("aca_v_42", "Visionario", 20);
	criarPesquisa("visionario", "Refinaria", 3, "aca_v_43", 50, 50, 50, 50, "aca_v_4");
	a_setarRequisito("aca_v_43", "Visionario", 30);
	
	criarPesquisa("visionario", "airDrop", 1, "aca_v_51", 50, 50, 50, 50, "aca_v_5");
	a_setarRequisito("aca_v_51", "Visionario", 20);
	criarPesquisa("visionario", "airDrop", 2, "aca_v_52", 50, 65, 65, 65, "aca_v_5");
	a_setarRequisito("aca_v_52", "Visionario", 30);
	criarPesquisa("visionario", "airDrop", 3, "aca_v_53", 50, 80, 80, 80, "aca_v_5");
	a_setarRequisito("aca_v_53", "Visionario", 40); 
	
	criarPesquisa("visionario", "Planetas", 1, "aca_v_61", 50, 50, 50, 50, "aca_v_6");
	a_setarRequisito("aca_v_61", "Visionario", 20);
	criarPesquisa("visionario", "Planetas", 2, "aca_v_62", 50, 65, 65, 65, "aca_v_6");
	a_setarRequisito("aca_v_62", "Visionario", 30);
	criarPesquisa("visionario", "Planetas", 3, "aca_v_63", 50, 80, 80, 80, "aca_v_6");
	a_setarRequisito("aca_v_63", "Visionario", 40);
}

function a_initArrebatadorPesquisas(){
	//Arrebatador:
	criarPesquisa("Arrebatador", "Lider", 1, "aca_a_11", 20, 15, 15, 15, "aca_a_1");
	a_setarRequisito("aca_a_11", "Arrebatador", 1);
	criarPesquisa("Arrebatador", "Lider", 2, "aca_a_12", 30, 20, 20, 20, "aca_a_1");
	a_setarRequisito("aca_a_12", "Arrebatador", 3);
	criarPesquisa("Arrebatador", "Lider", 3, "aca_a_13", 30, 20, 20, 20, "aca_a_1");
	a_setarRequisito("aca_a_13", "Arrebatador", 6);
	
	criarPesquisa("Arrebatador", "canhaoOrbital", 1, "aca_a_21", 50, 50, 50, 50, "aca_a_2");
	a_setarRequisito("aca_a_21", "Arrebatador", 20);
	criarPesquisa("Arrebatador", "canhaoOrbital", 2, "aca_a_22", 50, 65, 65, 65, "aca_a_2");
	a_setarRequisito("aca_a_22", "Arrebatador", 30);
	criarPesquisa("Arrebatador", "canhaoOrbital", 3, "aca_a_23", 50, 80, 80, 80, "aca_a_2");
	a_setarRequisito("aca_a_23", "Arrebatador", 40);
}

function a_initComerciantePesquisas(){
	//Comerciante:
	criarPesquisa("Comerciante", "prospeccao", 1, "aca_c_11", 20, 15, 15, 20, "aca_c_1");
	a_setarRequisito("aca_c_11", "Vitorias", 3); 
	criarPesquisa("Comerciante", "prospeccao", 2, "aca_c_12", 30, 30, 30, 30, "aca_c_1");
	a_setarRequisito("aca_c_12", "Vitorias", 10);
	criarPesquisa("Comerciante", "prospeccao", 3, "aca_c_13", 35, 35, 35, 35, "aca_c_1");
	a_setarRequisito("aca_c_13", "Vitorias", 25);
}

function a_initDiplomataPesquisas(){
	//Diplomata:
	criarPesquisa("Diplomata", "forcaD", 1, "aca_d_11", 20, 15, 15, 20, "aca_d_1");
	a_setarRequisito("aca_d_11", "Vitorias", 3);
	criarPesquisa("Diplomata", "forcaD", 2, "aca_d_12", 30, 30, 30, 30, "aca_d_1");
	a_setarRequisito("aca_d_12", "Vitorias", 10);
	criarPesquisa("Diplomata", "forcaD", 3, "aca_d_13", 35, 35, 35, 35, "aca_d_1");
	a_setarRequisito("aca_d_13", "Vitorias", 25);
}

function a_initIntelPesquisas(){
	//intel:
	criarPesquisa("Intel", "Espionagem", 1, "aca_i_11", 20, 20, 20, 20, "aca_i_1");
	a_setarRequisito("aca_i_11", "Vitorias", 3);
	criarPesquisa("Intel", "Espionagem", 2, "aca_i_12", 35, 35, 35, 35, "aca_i_1");
	a_setarRequisito("aca_i_12", "Vitorias", 6);
	criarPesquisa("Intel", "Espionagem", 3, "aca_i_13", 50, 50, 50, 50, "aca_i_1");
	a_setarRequisito("aca_i_13", "Visionario", 6);

	criarPesquisa("Intel", "Filantropia", 1, "aca_i_21", 30, 30, 30, 30, "aca_i_2");
	a_setarRequisito("aca_i_21", "Visionario", 6);
	criarPesquisa("Intel", "Filantropia", 2, "aca_i_22", 40, 40, 40, 40, "aca_i_2");
	a_setarRequisito("aca_i_22", "Visionario", 10);
	criarPesquisa("Intel", "Filantropia", 3, "aca_i_23", 50, 50, 50, 50, "aca_i_2");
	a_setarRequisito("aca_i_23", "Visionario", 20);
		
	criarPesquisa("Intel", "Almirante", 1, "aca_i_31", 30, 25, 25, 40, "aca_i_3");
	a_setarRequisito("aca_i_31", "Visionario", 6);
	criarPesquisa("Intel", "Almirante", 2, "aca_i_32", 40, 30, 35, 45, "aca_i_3");
	a_setarRequisito("aca_i_32", "Visionario", 10);
	criarPesquisa("Intel", "Almirante", 3, "aca_i_33", 50, 50, 50, 50, "aca_i_3");
	a_setarRequisito("aca_i_33", "Visionario", 20);
}

//Pesquisas Avançadas:
function a_initAVPesquisas(){
	criarPesquisa("Avancadas", "Carapaca", 1, "aca_av_11", 50, 150, 80, 80, "aca_av_1");
	a_setarRequisito("aca_av_11", "Arrebatador", 60);
	criarPesquisa("Avancadas", "Carapaca", 2, "aca_av_12", 50, 200, 100, 100, "aca_av_1");
	a_setarRequisito("aca_av_12", "Arrebatador", 80);
	criarPesquisa("Avancadas", "Carapaca", 3, "aca_av_13", 50, 200, 150, 150, "aca_av_1");
	a_setarRequisito("aca_av_13", "Arrebatador", 100);
	
	criarPesquisa("Avancadas", "Mira", 1, "aca_av_21", 50, 150, 80, 80, "aca_av_2");
	a_setarRequisito("aca_av_21", "Arrebatador", 60);
	criarPesquisa("Avancadas", "Mira", 2, "aca_av_22", 50, 200, 100, 100, "aca_av_2");
	a_setarRequisito("aca_av_22", "Arrebatador", 80);
	criarPesquisa("Avancadas", "Mira", 3, "aca_av_23", 50, 200, 150, 150, "aca_av_2");
	a_setarRequisito("aca_av_23", "Arrebatador", 100);
	
	criarPesquisa("Avancadas", "Ocultar", 1, "aca_av_31", 50, 150, 100, 100, "aca_av_3");
	a_setarRequisito("aca_av_31", "Visionario", 60);
	criarPesquisa("Avancadas", "Ocultar", 2, "aca_av_32", 50, 200, 125, 125, "aca_av_3");
	a_setarRequisito("aca_av_32", "Visionario", 80);
	criarPesquisa("Avancadas", "Ocultar", 3, "aca_av_33", 50, 200, 150, 150, "aca_av_3");
	a_setarRequisito("aca_av_33", "Visionario", 100);
	
	criarPesquisa("Avancadas", "Satelite", 1, "aca_av_41", 50, 200, 100, 100, "aca_av_4");
	a_setarRequisito("aca_av_41", "Visionario", 100);
	criarPesquisa("Avancadas", "Satelite", 2, "aca_av_42", 50, 200, 150, 150, "aca_av_4");
	a_setarRequisito("aca_av_42", "Visionario", 130);
	criarPesquisa("Avancadas", "Satelite", 3, "aca_av_43", 50, 200, 200, 200, "aca_av_4");
	a_setarRequisito("aca_av_43", "Visionario", 160);
}

//Pesquisas Avançadas:
function a_initPlanetasPesquisas(){
	criarPesquisa("Planetas", "Teluria", 1, "aca_pln_11", 50, 50, 50, 50, "aca_pln_1");
	a_setarRequisito("aca_pln_11", "Visionario", 20);
}




function a_initAtkEDefReq(){
	//Requisitos de Ataque e Defesa:
	//Soldados:
	//a_setarRequisito("aca_s_a_min2", "Pontos", 60);
	a_setarRequisito("aca_s_a_min3", "Pontos", 60);
	a_setarRequisito("aca_s_a_min4", "Pontos", 60);
	a_setarRequisito("aca_s_a_min5", "Pontos", 200);
	a_setarRequisito("aca_s_a_min6", "Pontos", 500);
	//a_setarRequisito("aca_s_d_min2", "Pontos", 60);
	a_setarRequisito("aca_s_d_min3", "Pontos", 60);
	a_setarRequisito("aca_s_d_min4", "Pontos", 60);
	a_setarRequisito("aca_s_d_min5", "Pontos", 200);
	a_setarRequisito("aca_s_d_min6", "Pontos", 500);
	//a_setarRequisito("aca_s_a_max7", "Pontos", 60);
	a_setarRequisito("aca_s_a_max8", "Pontos", 60);
	a_setarRequisito("aca_s_a_max9", "Pontos", 200);
	a_setarRequisito("aca_s_a_max10", "Pontos", 500);
	//a_setarRequisito("aca_s_d_max7", "Pontos", 60);
	a_setarRequisito("aca_s_d_max8", "Pontos", 60);
	a_setarRequisito("aca_s_d_max9", "Pontos", 200);
	a_setarRequisito("aca_s_d_max10", "Pontos", 500);
	
	//Tanques:
	a_setarRequisito("aca_t_a_min2", "Pontos", 0);
	a_setarRequisito("aca_t_a_min3", "Pontos", 60);
	a_setarRequisito("aca_t_a_min4", "Pontos", 60);
	a_setarRequisito("aca_t_a_min5", "Pontos", 200);
	a_setarRequisito("aca_t_a_min6", "Pontos", 500);
	a_setarRequisito("aca_t_d_min2", "Pontos", 0);
	a_setarRequisito("aca_t_d_min3", "Pontos", 60);
	a_setarRequisito("aca_t_d_min4", "Pontos", 60);
	a_setarRequisito("aca_t_d_min5", "Pontos", 200);
	a_setarRequisito("aca_t_d_min6", "Pontos", 500);
	a_setarRequisito("aca_t_a_max13", "Pontos", 0);
	a_setarRequisito("aca_t_a_max14", "Pontos", 0);
	a_setarRequisito("aca_t_a_max15", "Pontos", 0);
	a_setarRequisito("aca_t_a_max16", "Pontos", 60);
	a_setarRequisito("aca_t_a_max17", "Pontos", 60);
	a_setarRequisito("aca_t_a_max18", "Pontos", 200);
	a_setarRequisito("aca_t_a_max19", "Pontos", 200);
	a_setarRequisito("aca_t_a_max20", "Pontos", 500);
	a_setarRequisito("aca_t_a_max21", "Pontos", 500);
	a_setarRequisito("aca_t_a_max22", "Pontos", 1000);
	a_setarRequisito("aca_t_a_max23", "Pontos", 1000);
	a_setarRequisito("aca_t_a_max24", "Pontos", 1000);
	a_setarRequisito("aca_t_a_max25", "Pontos", 1500);
	a_setarRequisito("aca_t_d_max13", "Pontos", 0);
	a_setarRequisito("aca_t_d_max14", "Pontos", 0);
	a_setarRequisito("aca_t_d_max15", "Pontos", 0);
	a_setarRequisito("aca_t_d_max16", "Pontos", 60);
	a_setarRequisito("aca_t_d_max17", "Pontos", 60);
	a_setarRequisito("aca_t_d_max18", "Pontos", 200);
	a_setarRequisito("aca_t_d_max19", "Pontos", 200);
	a_setarRequisito("aca_t_d_max20", "Pontos", 500);
	a_setarRequisito("aca_t_d_max21", "Pontos", 500);
	a_setarRequisito("aca_t_d_max22", "Pontos", 1000);
	a_setarRequisito("aca_t_d_max23", "Pontos", 1000);
	a_setarRequisito("aca_t_d_max24", "Pontos", 1000);
	a_setarRequisito("aca_t_d_max25", "Pontos", 1500);
	
	//Navios:
	a_setarRequisito("aca_n_a_min2", "Pontos", 0);
	a_setarRequisito("aca_n_a_min3", "Pontos", 60);
	a_setarRequisito("aca_n_a_min4", "Pontos", 60);
	a_setarRequisito("aca_n_a_min5", "Pontos", 200);
	a_setarRequisito("aca_n_a_min6", "Pontos", 500);
	a_setarRequisito("aca_n_d_min2", "Pontos", 0);
	a_setarRequisito("aca_n_d_min3", "Pontos", 60);
	a_setarRequisito("aca_n_d_min4", "Pontos", 60);
	a_setarRequisito("aca_n_d_min5", "Pontos", 200);
	a_setarRequisito("aca_n_d_min6", "Pontos", 500);
	a_setarRequisito("aca_n_a_max13", "Pontos", 0);
	a_setarRequisito("aca_n_a_max14", "Pontos", 0);
	a_setarRequisito("aca_n_a_max15", "Pontos", 0);
	a_setarRequisito("aca_n_a_max16", "Pontos", 60);
	a_setarRequisito("aca_n_a_max17", "Pontos", 60);
	a_setarRequisito("aca_n_a_max18", "Pontos", 200);
	a_setarRequisito("aca_n_a_max19", "Pontos", 200);
	a_setarRequisito("aca_n_a_max20", "Pontos", 500);
	a_setarRequisito("aca_n_a_max21", "Pontos", 500);
	a_setarRequisito("aca_n_a_max22", "Pontos", 1000);
	a_setarRequisito("aca_n_a_max23", "Pontos", 1000);
	a_setarRequisito("aca_n_a_max24", "Pontos", 1000);
	a_setarRequisito("aca_n_a_max25", "Pontos", 1500);
	a_setarRequisito("aca_n_d_max13", "Pontos", 0);
	a_setarRequisito("aca_n_d_max14", "Pontos", 0);
	a_setarRequisito("aca_n_d_max15", "Pontos", 0);
	a_setarRequisito("aca_n_d_max16", "Pontos", 60);
	a_setarRequisito("aca_n_d_max17", "Pontos", 60);
	a_setarRequisito("aca_n_d_max18", "Pontos", 200);
	a_setarRequisito("aca_n_d_max19", "Pontos", 200);
	a_setarRequisito("aca_n_d_max20", "Pontos", 500);
	a_setarRequisito("aca_n_d_max21", "Pontos", 500);
	a_setarRequisito("aca_n_d_max22", "Pontos", 1000);
	a_setarRequisito("aca_n_d_max23", "Pontos", 1000);
	a_setarRequisito("aca_n_d_max24", "Pontos", 1000);
	a_setarRequisito("aca_n_d_max25", "Pontos", 1500);
	
	//Líderes:
	a_setarRequisito("aca_ldr_a_min2", "Pontos", 0);
	a_setarRequisito("aca_ldr_a_min3", "Pontos", 0);
	a_setarRequisito("aca_ldr_a_min4", "Pontos", 60);
	a_setarRequisito("aca_ldr_a_min5", "Pontos", 60);
	a_setarRequisito("aca_ldr_a_min6", "Pontos", 200);
	a_setarRequisito("aca_ldr_a_min7", "Pontos", 500);
	a_setarRequisito("aca_ldr_a_min8", "Pontos", 1000);
	a_setarRequisito("aca_ldr_d_min2", "Pontos", 0);
	a_setarRequisito("aca_ldr_d_min3", "Pontos", 0);
	a_setarRequisito("aca_ldr_d_min4", "Pontos", 60);
	a_setarRequisito("aca_ldr_d_min5", "Pontos", 60);
	a_setarRequisito("aca_ldr_d_min6", "Pontos", 200);
	a_setarRequisito("aca_ldr_d_min7", "Pontos", 500);
	a_setarRequisito("aca_ldr_d_min8", "Pontos", 1000);
	a_setarRequisito("aca_ldr_a_max13", "Pontos", 0);
	a_setarRequisito("aca_ldr_a_max14", "Pontos", 0);
	a_setarRequisito("aca_ldr_a_max15", "Pontos", 0);
	a_setarRequisito("aca_ldr_a_max16", "Pontos", 0);
	a_setarRequisito("aca_ldr_a_max17", "Pontos", 60);
	a_setarRequisito("aca_ldr_a_max18", "Pontos", 60);
	a_setarRequisito("aca_ldr_a_max19", "Pontos", 60);
	a_setarRequisito("aca_ldr_a_max20", "Pontos", 200);
	a_setarRequisito("aca_ldr_a_max21", "Pontos", 200);
	a_setarRequisito("aca_ldr_a_max22", "Pontos", 500);
	a_setarRequisito("aca_ldr_a_max23", "Pontos", 500);
	a_setarRequisito("aca_ldr_a_max24", "Pontos", 500);
	a_setarRequisito("aca_ldr_a_max25", "Pontos", 1000);
	a_setarRequisito("aca_ldr_a_max26", "Pontos", 1000);
	a_setarRequisito("aca_ldr_a_max27", "Pontos", 1000);
	a_setarRequisito("aca_ldr_a_max28", "Pontos", 1000);
	a_setarRequisito("aca_ldr_a_max29", "Pontos", 1000);
	a_setarRequisito("aca_ldr_a_max30", "Pontos", 1500);
	a_setarRequisito("aca_ldr_a_max31", "Pontos", 1500);
	a_setarRequisito("aca_ldr_a_max32", "Pontos", 1500);
	a_setarRequisito("aca_ldr_a_max33", "Pontos", 1500);
	a_setarRequisito("aca_ldr_a_max34", "Pontos", 1500);
	a_setarRequisito("aca_ldr_a_max35", "Pontos", 2000);
	a_setarRequisito("aca_ldr_d_max13", "Pontos", 0);
	a_setarRequisito("aca_ldr_d_max14", "Pontos", 0);
	a_setarRequisito("aca_ldr_d_max15", "Pontos", 0);
	a_setarRequisito("aca_ldr_d_max16", "Pontos", 0);
	a_setarRequisito("aca_ldr_d_max17", "Pontos", 60);
	a_setarRequisito("aca_ldr_d_max18", "Pontos", 60);
	a_setarRequisito("aca_ldr_d_max19", "Pontos", 60);
	a_setarRequisito("aca_ldr_d_max20", "Pontos", 200);
	a_setarRequisito("aca_ldr_d_max21", "Pontos", 200);
	a_setarRequisito("aca_ldr_d_max22", "Pontos", 500);
	a_setarRequisito("aca_ldr_d_max23", "Pontos", 500);
	a_setarRequisito("aca_ldr_d_max24", "Pontos", 500);
	a_setarRequisito("aca_ldr_d_max25", "Pontos", 1000);
	a_setarRequisito("aca_ldr_d_max26", "Pontos", 1000);
	a_setarRequisito("aca_ldr_d_max27", "Pontos", 1000);
	a_setarRequisito("aca_ldr_d_max28", "Pontos", 1000);
	a_setarRequisito("aca_ldr_d_max29", "Pontos", 1000);
	a_setarRequisito("aca_ldr_d_max30", "Pontos", 1500);
	a_setarRequisito("aca_ldr_d_max31", "Pontos", 1500);
	a_setarRequisito("aca_ldr_d_max32", "Pontos", 1500);
	a_setarRequisito("aca_ldr_d_max33", "Pontos", 1500);
	a_setarRequisito("aca_ldr_d_max34", "Pontos", 1500);
	a_setarRequisito("aca_ldr_d_max35", "Pontos", 2000);
}

function pes_echoTotalOmnis(){
	%totalOmnis = 0;
	for(%i = 0; %i < $pesquisasPool_humanos.getCount(); %i++){
		%pesquisa = $pesquisasPool_humanos.getObject(%i);
		%totalOmnis += %pesquisa.cOMNI;
		%totalInicial += %pesquisa.custoInicial;
		%totalGeralCreditos += %pesquisa.totalEmCreditos;
	}
	for(%i = 0; %i < $pesquisasPool_guloks.getCount(); %i++){
		%pesquisa = $pesquisasPool_guloks.getObject(%i);
		%totalOmnis += %pesquisa.cOMNI;
		%totalInicial += %pesquisa.custoInicial;
		%totalGeralCreditos += %pesquisa.totalEmCreditos;
	}
	echo(">>CUSTO TOTAL DA ACADEMIA EM OMNIS: " @ %totalOmnis);
	echo(">>CUSTO INICIAL TOTAL: " @ %totalInicial @ " CRÉDITOS");
	echo(">>CUSTO TOTAL EM CREDITOS: " @ %totalGeralCreditos @ " CRÉDITOS");
}


//iniciar todas as pesquisas humanas:
function a_initAllPesquisas(){
	a_initLiderPesquisas();	
	a_initSoldadosPesquisas();
	a_initTanquesENaviosPesquisas();
	a_initVisionarioPesquisas();
	a_initArrebatadorPesquisas();
	a_initComerciantePesquisas();
	a_initDiplomataPesquisas();
	a_initIntelPesquisas();
	a_initAVPesquisas();
	a_initPlanetasPesquisas();
	a_initAtkEDefReq();
	
	
	//guloks:
	a_initZangaoPesquisas();	
	a_initVermesPesquisas();
	a_initCefaloksPesquisas();
	a_initRainhasPesquisas();
	a_initDragnalPesquisas();
	a_initGVisionarioPesquisas();
	a_initGArrebatadorPesquisas();
	a_initGComerciantePesquisas();
	a_initGDiplomataPesquisas();
	a_initGIntelPesquisas();
	a_initGPlanetasPesquisas();
	a_initGAVPesquisas();
	a_initGAtkEDefReq();
	
	echo(">>PESQUISAS HUMANAS: " @ $pesquisasPool_humanos.getCount());
	echo(">>PESQUISAS GULOKS: " @ $pesquisasPool_guloks.getCount());
	pes_echoTotalOmnis();
}
a_initAllPesquisas();




function gravarTodasAsPesquisas(){
	%file = new FileObject();
	%file.openForAppend("game/data/files/listaDePesquisas.txt"); //abre para adicionar texto
			
	for(%i = 0; %i < $pesquisasPool_humanos.getCount(); %i++){
		%pesq = $pesquisasPool_humanos.getObject(%i);
		%txt = "Código: " @ %pesq.id @ "; ";
		%txt = %txt @ "Tipo: " @ %pesq.tipo @ "; ";
		%txt = %txt @ "Pesquisa: " @ %pesq.upgrade @ "; ";
		
		//atualiza o arquivo:
		%file.WriteLine(%txt);
	}	
	
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM
}












