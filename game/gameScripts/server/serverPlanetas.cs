// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverPlanetas.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sábado, 16 de fevereiro de 2008 3:54
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
$todosOsPlanetas = new SimSet();

function criarPlaneta(%nomeDoPlaneta, %desastres, %totalDeInfos, %totalDeObjs, %id, %lotacao){
	%palavraPlaneta = "planeta";
	%eval = "$planeta" @ %nomeDoPlaneta @ "= new ScriptObject(){";
	%eval = %eval @ "class = " @ %palavraPlaneta @ ";";
	%eval = %eval @ "nome = " @ %nomeDoPlaneta @ ";";
	%eval = %eval @ "desastres = " @ %desastres @ ";";
	%eval = %eval @ "totalDeInfos = " @ %totalDeInfos @ ";";
	%eval = %eval @ "totalDeObjs = " @ %totalDeObjs @ ";";
	%eval = %eval @ "id = " @ %id @ ";";
	%eval = %eval @ "lotacao = " @ %lotacao @ ";";
	%eval = %eval @ "};";
	eval(%eval);
	
	%eval = "$planeta" @ %nomeDoPlaneta @ ".initAll();";
	eval(%eval);
}

function planeta::initAll(%this){
	%this.initAreas();
	%this.initInfos();
	%this.initObjetivos(); 
	%eval = "init" @ %this.nome @ "Grupos();";
	eval(%eval);
	%eval = "init" @ %this.nome @ "Desastres();";
	eval(%eval);
	$todosOsPlanetas.add(%this);
}

function planeta::initAreas(%this){
	%this.terra = 0; //zera o número de áreas terrestres
	%this.mar = 0; //zera o número de áreas marítimas
	%this.oceano = 0; //zera o número de oceanos
	
	%this.areasPool = new SimSet();
	%eval = "%myDefaultContainer = $areasDe" @ %this.nome @ ";";
	eval(%eval);
	for(%i = 0; %i < %myDefaultContainer.getCount(); %i++){
		%area = %myDefaultContainer.getObject(%i);
		if(%area.terreno $= "terra"){
			%this.terra++;	
		} else {
			if(%area.oceano == 1){
				%this.oceano++;
			} else {
				%this.mar++;
			}
		}
		%this.areasPool.add(%area);
	}
	%this.numDeAreas = %this.areasPool.getCount();
}

function planeta::initInfos(%this){
	%this.infoMin = 0; //zera o número de informações de minério
	%this.infoPet = 0; //zera o número de informações de petróleo
	%this.infoUra = 0; //zera o número de informações de urânio
	%this.infoPt = 0; //zera o número de informações de pontos
	
	%this.infoPool = new SimSet();
	for(%i = 1; %i < %this.totalDeInfos + 1; %i++){
		%eval = "%info = $info" @ %this.nome @ %i @ ";";
		eval(%eval);
		%this.infoPool.add(%info);
		
		if(%info.bonusM > 0){
			%this.infoMin++;
		} else if(%info.bonusP > 0){
			%this.infoPet++;
		} else if(%info.bonusU > 0){
			%this.infoUra++;
		} else if(%info.bonusPt > 0){
			%this.infoPt++;
		}
	}		
}

function planeta::initObjetivos(%this){
	%this.objPool = new SimSet();
	for(%i = 1; %i < %this.totalDeObjs + 1; %i++){
		%eval = "%obj = $objetivo" @ %this.nome @ %i @ ";";
		eval(%eval);
		%this.objPool.add(%obj);
	}
}

function planeta::addGrupo(%this, %nomeDoGrupo, %numDeAreas, %nomeDasAreas, %recurso){
	if(!isObject(%this.grupos)){
		%this.grupos = new SimSet();
	}
	if(!isObject(%this.gruposParaSorteio)){
		%this.gruposParaSorteio = new SimSet();
	}
	if(!isObject(%this.gruposDeDuplas)){
		%this.gruposDeDuplas = new SimSet();
	}
	
	%proximoGrupo = %this.grupos.getCount()+1;
		
	%eval = "%grupo" @ %proximoGrupo @ " = new ScriptObject(){";
	%eval = %eval @ "num = " @ %proximoGrupo @ ";";
	%eval = %eval @ "nome = " @ %nomeDoGrupo @ ";";
	%eval = %eval @ "numDeAreas = " @ %numDeAreas @ ";";
	for(%j = 1; %j < %numDeAreas +1; %j++){
		%nomeDaArea = getWord(%nomeDasAreas, %j-1);	
		%eval = %eval @ "area" @ %j @ "Nome = " @ %nomeDaArea @ ";";	
	}
	%eval = %eval @ "recurso = " @ %recurso @ ";";
	%eval = %eval @ "};";
	eval(%eval);
		
	%eval = "%esteGrupo = %grupo" @ %proximoGrupo @ ";";
	eval(%eval);
	
	%this.grupos.add(%esteGrupo);
	if(%recurso $= "minerio"){
		%this.gruposParaSorteio.add(%esteGrupo);
		%this.minerio++;
	} else if(%recurso $= "petroleo"){
		%this.gruposDeDuplas.add(%esteGrupo);
		%this.petroleo++;
	} else if(%recurso $= "uranio"){
		%this.gruposDeDuplas.add(%esteGrupo);
		%this.uranio++;
	}
}

function planeta::registrarDesastre(%this, %nomeDaArea, %desastre){
	%areaFound = false;
	for(%i = 0; %i < %this.numDeAreas; %i++){
		%area = %this.areasPool.getObject(%i);
		if(%area.getName() $= %nomeDaArea){
			%areaFound = true;	
			%i = %this.numDeAreas;
		}
	}
	if(%areaFound){
		%area.myDesastre = %desastre;
		%this.areasDeDesastre.add(%area);
	}
}
 
/////////////////////////////////
//grupos da Terra:
function initTerraGrupos(){
	$planetaTerra.addGrupo("Brasil", 2, "saoPaulo" SPC "manaus", "minerio");
	$planetaTerra.addGrupo("EUA", 3, "losAngeles" SPC "houston" SPC "novaYork", "minerio");
	$planetaTerra.addGrupo("Europa", 3, "londres" SPC "comunidadeEuropeia" SPC "estocolmo", "minerio");
	$planetaTerra.addGrupo("China", 3, "pequim" SPC "lhasa" SPC "xangai", "minerio");
	
	$planetaTerra.addGrupo("Oriente", 2, "bagda" SPC "cabul", "petroleo");
	
	$planetaTerra.addGrupo("Canada", 4, "bakerlake" SPC "vancouver" SPC "toronto" SPC "montreal", "uranio");
	$planetaTerra.addGrupo("Africa", 4, "marrakesh" SPC "cairo" SPC "kinshasa" SPC "cidadeDoCabo", "uranio");
	$planetaTerra.addGrupo("Russia", 4, "moscou" SPC "kirov" SPC "omsk" SPC "magadan", "uranio");
	$planetaTerra.addGrupo("Australia", 2, "perth" SPC "sydney", "uranio");
}

//desastres da Terra:
function initTerraDesastres(){
	$planetaTerra.areasDeDesastre = new SimSet();
	$planetaTerra.registrarDesastre("comunidadeEuropeia", "vulcao");	
	$planetaTerra.registrarDesastre("moscou", "vulcao");	
	$planetaTerra.registrarDesastre("kirov", "vulcao");	
	$planetaTerra.registrarDesastre("cazaquistao", "vulcao");	
	$planetaTerra.registrarDesastre("bagda", "vulcao");	
	$planetaTerra.registrarDesastre("cairo", "vulcao");	
	$planetaTerra.registrarDesastre("mexico", "vulcao");	
	$planetaTerra.registrarDesastre("houston", "furacao");	
	$planetaTerra.registrarDesastre("losAngeles", "furacao");	
	$planetaTerra.registrarDesastre("novaYork", "furacao");	
	$planetaTerra.registrarDesastre("montreal", "furacao");	
	$planetaTerra.registrarDesastre("toronto", "furacao");	
	$planetaTerra.registrarDesastre("bMarDoJapao", "furacao");	
	$planetaTerra.registrarDesastre("bMarDaChina", "furacao");	
	$planetaTerra.registrarDesastre("vietna", "furacao");	
	$planetaTerra.registrarDesastre("xangai", "furacao");	
	$planetaTerra.registrarDesastre("india", "furacao");	
	$planetaTerra.registrarDesastre("bGolfoDoMexico", "furacao");	
}


///////////////////////////////////////////
/////////////////////////////////////
//UNGART:
//grupos de Ungart:
function initUngartGrupos(){
	$planetaUngart.addGrupo("PrEx", 2, "UNG_PrEx01" SPC "UNG_PrEx02", "minerio");
	$planetaUngart.addGrupo("DeEx", 2, "UNG_DeEx01" SPC "UNG_DeEx02", "minerio");
	$planetaUngart.addGrupo("PlDo", 2, "UNG_PlDo01" SPC "UNG_PlDo02", "minerio");
	$planetaUngart.addGrupo("ChOc", 2, "UNG_ChOc01" SPC "UNG_ChOc02", "minerio");
	$planetaUngart.addGrupo("ChOr", 2, "UNG_ChOr01" SPC "UNG_ChOr02", "minerio");
	
	$planetaUngart.addGrupo("PaGu", 3, "UNG_PaGu01" SPC "UNG_PaGu02" SPC "UNG_PaGu03", "petroleo");
	$planetaUngart.addGrupo("CaNo", 4, "UNG_CaNo01" SPC "UNG_CaNo02" SPC "UNG_CaNo03" SPC "UNG_CaNo04", "petroleo");
	$planetaUngart.addGrupo("CaOr", 4, "UNG_CaOr01" SPC "UNG_CaOr02" SPC "UNG_CaOr03" SPC "UNG_CaOr04", "petroleo");
	
	$planetaUngart.addGrupo("MoVe", 4, "UNG_MoVe01" SPC "UNG_MoVe02" SPC "UNG_MoVe03" SPC "UNG_MoVe04", "uranio");
	$planetaUngart.addGrupo("VaNo", 4, "UNG_VaNo01" SPC "UNG_VaNo02" SPC "UNG_VaNo03" SPC "UNG_VaNo04", "uranio");
	$planetaUngart.addGrupo("VaOr", 3, "UNG_VaOr01" SPC "UNG_VaOr02" SPC "UNG_VaOr03", "uranio");
	$planetaUngart.addGrupo("IlVu", 2, "UNG_IlVu01" SPC "UNG_IlVu02", "uranio");
	$planetaUngart.addGrupo("VaGu", 2, "UNG_VaGu01" SPC "UNG_VaGu02", "uranio");
}

//desastres de Ungart:
function initUngartDesastres(){
	$planetaUngart.areasDeDesastre = new SimSet();
	$planetaUngart.registrarDesastre("UNG_IlVu01", "vulcao");	
	$planetaUngart.registrarDesastre("UNG_IlVu02", "vulcao");	
	
	$planetaUngart.registrarDesastre("UNG_VaGu01", "gas");
	$planetaUngart.registrarDesastre("UNG_VaGu02", "gas");
	$planetaUngart.registrarDesastre("UNG_PaGu01", "gas");
	$planetaUngart.registrarDesastre("UNG_PaGu02", "gas");
	$planetaUngart.registrarDesastre("UNG_PaGu03", "gas");
	//Adiciona pela segunda vez, para duplicar a chance do desastre sair aki:
	$planetaUngart.registrarDesastre("UNG_VaGu01", "gas");
	$planetaUngart.registrarDesastre("UNG_VaGu02", "gas");
	$planetaUngart.registrarDesastre("UNG_PaGu01", "gas");
	$planetaUngart.registrarDesastre("UNG_PaGu02", "gas");
	$planetaUngart.registrarDesastre("UNG_PaGu03", "gas");
	
	$planetaUngart.registrarDesastre("UNG_ChOc02", "furacao");
	$planetaUngart.registrarDesastre("UNG_b39", "furacao");
	$planetaUngart.registrarDesastre("UNG_b38", "furacao");
	$planetaUngart.registrarDesastre("UNG_b37", "furacao");
	$planetaUngart.registrarDesastre("UNG_b35", "furacao");
	
	$planetaUngart.registrarDesastre("UNG_b13", "furacao");
	$planetaUngart.registrarDesastre("UNG_ChOr02", "furacao");
	
	$planetaUngart.registrarDesastre("UNG_CaOr04", "vulcao");
	$planetaUngart.registrarDesastre("UNG_CaOr02", "furacao");
	
	$planetaUngart.registrarDesastre("UNG_CaNo01", "vulcao");
	
	$planetaUngart.registrarDesastre("UNG_VaNo01", "furacao");
}


function serverCarregarUngart(){
		new t2dStaticSprite(UNG_b01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "26.3 -34.2";
				pos1 = "27.6 -31.2";
				pos2 = "22.6 -32.7";
				pos3 = "31 -35";
				terreno = "mar";
		};
		UNG_b01.onLevelLoaded();
		new t2dStaticSprite(UNG_b02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "31.8 -24.9";
				pos1 = "33.2 -21.7";
				pos2 = "34.8 -23.5";
				pos3 = "28.9 -27.3";
				terreno = "mar";
		};
		UNG_b02.onLevelLoaded();
		new t2dStaticSprite(UNG_b03) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "44.4 -22.9";
				pos1 = "39.6 -23.2";
				pos2 = "40.9 -21.2";
				pos3 = "46.7 -25.4";
				terreno = "mar";
		};
		UNG_b03.onLevelLoaded();
		new t2dStaticSprite(UNG_b04) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "45 -33.7";
				pos1 = "46.8 -30.9";
				pos2 = "42.1 -35.3";
				pos3 = "38.8 -34.4";
				terreno = "mar";
		};
		UNG_b04.onLevelLoaded();
		new t2dStaticSprite(UNG_b05) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "47 -13.7";
				pos1 = "46.9 -10.5";
				pos2 = "47.4 -16.5";
				pos3 = "45 -18";
				terreno = "mar";
		};
		UNG_b05.onLevelLoaded();
		new t2dStaticSprite(UNG_b06) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "46.7 -2.1";
				pos1 = "47.4 -5";
				pos2 = "46.6 1.5";
				pos3 = "45.5 -6.6";
				terreno = "mar";
		};
		UNG_b06.onLevelLoaded();
		new t2dStaticSprite(UNG_b07) {
			class = "Area";
			
				mountID = "77";
				planeta = "Ungart";
				pos0 = "46.6 11.1";
				pos1 = "47.4 8";
				pos2 = "45.7 5.9";
				pos3 = "46.7 14.3";
				terreno = "mar";
		};
		UNG_b07.onLevelLoaded();
		new t2dStaticSprite(UNG_b08) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "34.5 12.7";
				pos1 = "32 10.3";
				pos2 = "37.9 13.7";
				pos3 = "31.5 14.1";
				terreno = "mar";
		};
		UNG_b08.onLevelLoaded();
		new t2dStaticSprite(UNG_b09) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "22.6 12.4";
				pos1 = "24.9 10.1";
				pos2 = "25.2 14.4";
				pos3 = "26.2 12.4";
				terreno = "mar";
		};
		UNG_b09.onLevelLoaded();
		new t2dStaticSprite(UNG_b10) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "22.2 3.8";
				pos1 = "23.2 1.2";
				pos2 = "22 6.6";
				pos3 = "24.6 6.6";
				terreno = "mar";
		};
		UNG_b10.onLevelLoaded();
		new t2dStaticSprite(UNG_b11) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "24.9 -17.4";
				pos1 = "28.4 -19.7";
				pos2 = "23.3 -12.8";
				pos3 = "25.5 -14.7";
				terreno = "mar";
		};
		UNG_b11.onLevelLoaded();
		new t2dStaticSprite(UNG_b12) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "15 -18.1";
				pos1 = "19.2 -17.4";
				pos2 = "13.5 -21.9";
				pos3 = "17.4 -15.5";
				terreno = "mar";
		};
		UNG_b12.onLevelLoaded();
		new t2dStaticSprite(UNG_b13) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "19.3 -6.8";
				pos1 = "16 -8";
				pos2 = "20.5 -9.7";
				pos3 = "20.6 -3.1";
				terreno = "mar";
		};
		UNG_b13.onLevelLoaded();
		new t2dStaticSprite(UNG_b14) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "12.3 -31.3";
				pos1 = "9.7 -27.7";
				pos2 = "12.6 -26.4";
				pos3 = "14.7 -34.5";
				terreno = "mar";
		};
		UNG_b14.onLevelLoaded();
		new t2dStaticSprite(UNG_b15) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "3.3 -33.5";
				pos1 = "0.1 -34.3";
				pos2 = "7.3 -33.4";
				pos3 = "50.8 -30.7";
				terreno = "mar";
		};
		UNG_b15.onLevelLoaded();
		new t2dStaticSprite(UNG_b16) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-9.8 -33.4";
				pos1 = "-13 -33";
				pos2 = "-6.2 -32.9";
				pos3 = "-9.4 -30.8";
				terreno = "mar";
		};
		UNG_b16.onLevelLoaded();
		new t2dStaticSprite(UNG_b17) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "11.2 12.5";
				pos1 = "11 9.8";
				pos2 = "15.2 14.4";
				pos3 = "9.7 7.6";
				terreno = "mar";
		};
		UNG_b17.onLevelLoaded();
		new t2dStaticSprite(UNG_b18) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "2.6 14.1";
				pos1 = "5.7 12.3";
				pos2 = "0.2 14.3";
				pos3 = "5.2 9.4";
				terreno = "mar";
		};
		UNG_b18.onLevelLoaded();
		new t2dStaticSprite(UNG_b19) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-8.1 12.3";
				pos1 = "-4.9 12.6";
				pos2 = "-10.4 10.3";
				pos3 = "-11.2 14";
				terreno = "mar";
		};
		UNG_b19.onLevelLoaded();
		new t2dStaticSprite(UNG_b20) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-19.5 10.5";
				pos1 = "-18.1 13.8";
				pos2 = "-16.7 8.6";
				pos3 = "-16.2 11";
				terreno = "mar";
		};
		UNG_b20.onLevelLoaded();
		new t2dStaticSprite(UNG_b21) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-29.7 14.5";
				pos1 = "-33.2 -12.7";
				pos2 = "-25.4 14.3";
				pos3 = "-3.9 14.5";
				terreno = "mar";
		};
		UNG_b21.onLevelLoaded();
		new t2dStaticSprite(UNG_b22) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "12.2 1.3";
				pos1 = "12 -1.8";
				pos2 = "9.8 3.5";
				pos3 = "6.2 4.2";
				terreno = "mar";
		};
		UNG_b22.onLevelLoaded();
		new t2dStaticSprite(UNG_b23) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "4.4 -6.6";
				pos1 = "0.8 -7.2";
				pos2 = "8.4 -6.6";
				pos3 = "6.7 -4";
				terreno = "mar";
		};
		UNG_b23.onLevelLoaded();
		new t2dStaticSprite(UNG_b24) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-4.2 -13.9";
				pos1 = "-2.4 10.7";
				pos2 = "-6.1 -16.2";
				pos3 = "-7.1 -13.9";
				terreno = "mar";
		};
		UNG_b24.onLevelLoaded();
		new t2dStaticSprite(UNG_b25) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-9.4 -8.3";
				pos1 = "-12.4 -8.6";
				pos2 = "-6.4 -7.7";
				pos3 = "-8.7 -10.7";
				terreno = "mar";
		};
		UNG_b25.onLevelLoaded();
		new t2dStaticSprite(UNG_b26) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-15.2 -17";
				pos1 = "-11.3 -16.7";
				pos2 = "-19 -18.2";
				pos3 = "-18.3 -16.4";
				terreno = "mar";
		};
		UNG_b26.onLevelLoaded();
		new t2dStaticSprite(UNG_b27) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-27.3 -19.5";
				pos1 = "-23 -19.4";
				pos2 = "-17.6 -22.1";
				pos3 = "-23.5 -17.3";
				terreno = "mar";
		};
		UNG_b27.onLevelLoaded();
		new t2dStaticSprite(UNG_b28) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-25.4 -30.2";
				pos1 = "-27 -27.3";
				pos2 = "-23.7 -32.4";
				pos3 = "-22 -34.5";
				terreno = "mar";
		};
		UNG_b28.onLevelLoaded();
		new t2dStaticSprite(UNG_b29) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-37.9 -34.2";
				pos1 = "-41.8 -34.7";
				pos2 = "-33.8 -34.3";
				pos3 = "-32.3 -32.3";
				terreno = "mar";
		};
		UNG_b29.onLevelLoaded();
		new t2dStaticSprite(UNG_b30) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-43.4 -25.3";
				pos1 = "-44.3 -27.9";
				pos2 = "-42.8 -22.4";
				pos3 = "-46.5 -23.9";
				terreno = "mar";
		};
		UNG_b30.onLevelLoaded();
		new t2dStaticSprite(UNG_b31) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-44.3 -14.9";
				pos1 = "-46.1 -12";
				pos2 = "-44.7 -18";
				pos3 = "-47 -14.9";
				terreno = "mar";
		};
		UNG_b31.onLevelLoaded();
		new t2dStaticSprite(UNG_b32) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-46 -1.7";
				pos1 = "-45.5 -4.7";
				pos2 = "-47 -1.4";
				pos3 = "-47 -7.3";
				terreno = "mar";
		};
		UNG_b32.onLevelLoaded();
		new t2dStaticSprite(UNG_b33) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-43.9 11.9";
				pos1 = "-40.6 11.1";
				pos2 = "-45.2 9.4";
				pos3 = "-40.7 14.3";
				terreno = "mar";
		};
		UNG_b33.onLevelLoaded();
		new t2dStaticSprite(UNG_b34) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-34.9 6.3";
				pos1 = "-33.7 4";
				pos2 = "-34.4 9.4";
				pos3 = "-36.9 8.4";
				terreno = "mar";
		};
		UNG_b34.onLevelLoaded();
		new t2dStaticSprite(UNG_b35) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-32.1 -1.3";
				pos1 = "-28.5 -5";
				pos2 = "-31.4 -5.5";
				pos3 = "-30.9 -3.5";
				terreno = "mar";
		};
		UNG_b35.onLevelLoaded();
		new t2dStaticSprite(UNG_b36) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-39.3 -8.6";
				pos1 = "-35.3 -7.6";
				pos2 = "-42.7 -8.7";
				pos3 = "-42.1 -7.2";
				terreno = "mar";
		};
		UNG_b36.onLevelLoaded();
		new t2dStaticSprite(UNG_b37) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-28.9 -11.9";
				pos1 = "-29 -8.5";
				pos2 = "-29 -14.9";
				pos3 = "-31.7 -9.1";
				terreno = "mar";
		};
		UNG_b37.onLevelLoaded();
		new t2dStaticSprite(UNG_b38) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-21.5 -5.9";
				pos1 = "-18.1 -6.2";
				pos2 = "-24.8 -7.7";
				pos3 = "-17.2 -8";
				terreno = "mar";
		};
		UNG_b38.onLevelLoaded();
		new t2dStaticSprite(UNG_b39) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-22.4 2.5";
				pos1 = "-17.1 2.6";
				pos2 = "-21.7 -0.6";
				pos3 = "-18.5 5.1";
				terreno = "mar";
		};
		UNG_b39.onLevelLoaded();
		new t2dStaticSprite(UNG_ChOc01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-27.4 10";
				pos1 = "-28.4 7.2";
				pos2 = "-25.9 7.3";
				pos3 = "-25.7 8.7";
				pos4 = "-29.7 9.7";
				terreno = "terra";
		};
		UNG_ChOc01.onLevelLoaded();
		new t2dStaticSprite(UNG_ChOc02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-28.7 0.4";
				pos1 = "-26.6 1.3";
				pos2 = "-26 -0.6";
				pos3 = "-27.7 2.6";
				pos4 = "-24.9 -2.5";
				terreno = "terra";
		};
		UNG_ChOc02.onLevelLoaded();
		new t2dStaticSprite(UNG_ChOr02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "16.6 2.6";
				pos1 = "17.5 0.8";
				pos2 = "16.1 -0.2";
				pos3 = "18.2 2.3";
				pos4 = "15.9 -2";
				terreno = "terra";
		};
		UNG_ChOr02.onLevelLoaded();
		new t2dStaticSprite(UNG_ChOr01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "17.4 8.9";
				pos1 = "15.4 7.7";
				pos2 = "18.1 7.1";
				pos3 = "15.7 9.4";
				pos4 = "16.5 6.1";
				terreno = "terra";
		};
		UNG_ChOr01.onLevelLoaded();
		new t2dStaticSprite(UNG_PrEx02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-37.2 -0.6";
				pos1 = "-38.7 -3.8";
				pos2 = "-36.4 -2.7";
				pos3 = "-38.9 -2.2";
				pos4 = "-41.1 -2.9";
				terreno = "terra";
		};
		UNG_PrEx02.onLevelLoaded();
		new t2dStaticSprite(UNG_PrEx01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-42.7 5.4";
				pos1 = "-42 2.3";
				pos2 = "-41.3 3.7";
				pos3 = "-40.8 5";
				pos4 = "-41 0.9";
				terreno = "terra";
		};
		UNG_PrEx01.onLevelLoaded();
		new t2dStaticSprite(UNG_PlDo01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "23.1 -21.9";
				pos1 = "22 -23.8";
				pos2 = "18.7 -22.5";
				pos3 = "21.1 -22.1";
				pos4 = "25.4 -24";
				terreno = "terra";
		};
		UNG_PlDo01.onLevelLoaded();
		new t2dStaticSprite(UNG_PlDo02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "17.4 -30";
				pos1 = "19.6 -27.1";
				pos2 = "17.7 -26.5";
				pos3 = "19.3 -29";
				pos4 = "16.9 -28";
				terreno = "terra";
		};
		UNG_PlDo02.onLevelLoaded();
		new t2dStaticSprite(UNG_DeEx01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "42.2 -28.6";
				pos1 = "38.6 -26.9";
				pos2 = "38.8 -28.2";
				pos3 = "40.5 -28.4";
				pos4 = "40.4 -27.1";
				terreno = "terra";
		};
		UNG_DeEx01.onLevelLoaded();
		new t2dStaticSprite(UNG_DeEx02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "37.3 -31.4";
				pos1 = "34.3 -28.9";
				pos2 = "34.3 -30.2";
				pos3 = "35.8 -30.6";
				pos4 = "34.3 -31.5";
				terreno = "terra";
		};
		UNG_DeEx02.onLevelLoaded();
		new t2dStaticSprite(UNG_MoVe01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-33.6 -13";
				pos1 = "-37.5 -12.7";
				pos2 = "-35.6 -12.3";
				pos3 = "-35.8 -13.5";
				pos4 = "-39.6 -12.9";
				terreno = "terra";
		};
		UNG_MoVe01.onLevelLoaded();
		new t2dStaticSprite(UNG_MoVe02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-34.2 -17.8";
				pos1 = "-36.2 -17.4";
				pos2 = "-38.2 -17.3";
				pos3 = "-37.3 -18.5";
				pos4 = "-40 -17.1";
				terreno = "terra";
		};
		UNG_MoVe02.onLevelLoaded();
		new t2dStaticSprite(UNG_MoVe03) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-37.1 -22.4";
				pos1 = "-33.8 -22.5";
				pos2 = "-32 -21.5";
				pos3 = "-35.4 -22.6";
				pos4 = "-38.1 -24.1";
				terreno = "terra";
		};
		UNG_MoVe03.onLevelLoaded();
		new t2dStaticSprite(UNG_MoVe04) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-35.8 -29.1";
				pos1 = "-35.2 -26.8";
				pos2 = "-37.4 -27.5";
				pos3 = "-37.4 -29.4";
				pos4 = "-33.4 -26.1";
				terreno = "terra";
		};
		UNG_MoVe04.onLevelLoaded();
		new t2dStaticSprite(UNG_IlVu01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-24.5 -13.6";
				pos1 = "-21.2 -11.8";
				pos2 = "-22 -13.5";
				pos3 = "-23.2 -11.7";
				pos4 = "-21.7 -10.3";
				terreno = "terra";
		};
		UNG_IlVu01.onLevelLoaded();
		new t2dStaticSprite(UNG_IlVu02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-12.2 -13";
				pos1 = "-15.5 -13.1";
				pos2 = "-17.2 -11.9";
				pos3 = "-13.9 -13.1";
				pos4 = "-17.8 -13";
				terreno = "terra";
		};
		UNG_IlVu02.onLevelLoaded();
		new t2dStaticSprite(UNG_VaGu01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-1.4 6";
				pos1 = "-3.5 6.7";
				pos2 = "-5.3 5.5";
				pos3 = "0.6 5.6";
				pos4 = "-8 5.9";
				terreno = "terra";
		};
		UNG_VaGu01.onLevelLoaded();
		new t2dStaticSprite(UNG_VaGu02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "0.5 1";
				pos1 = "2.3 -0.8";
				pos2 = "-1.2 -0.3";
				pos3 = "-1.3 1.3";
				pos4 = "4.9 -0.1";
				terreno = "terra";
		};
		UNG_VaGu02.onLevelLoaded();
		new t2dStaticSprite(UNG_VaOr01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "27.8 3.3";
				pos1 = "29.8 2";
				pos2 = "28.9 0.3";
				pos3 = "27.6 1.6";
				pos4 = "30.4 -1.7";
				terreno = "terra";
		};
		UNG_VaOr01.onLevelLoaded();
		new t2dStaticSprite(UNG_VaOr02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "41.3 1";
				pos1 = "41.3 3.6";
				pos2 = "41.2 6.1";
				pos3 = "41.2 -0.8";
				pos4 = "41.8 8.3";
				terreno = "terra";
		};
		UNG_VaOr02.onLevelLoaded();
		new t2dStaticSprite(UNG_VaOr03) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "35.6 6.1";
				pos1 = "35.2 2.7";
				pos2 = "36.8 3.8";
				pos3 = "34.9 4.2";
				pos4 = "34 1.1";
				terreno = "terra";
		};
		UNG_VaOr03.onLevelLoaded();
		new t2dStaticSprite(UNG_VaNo01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "11.6 -12";
				pos1 = "9.2 -12.3";
				pos2 = "6.9 -12.6";
				pos3 = "13.5 -11.8";
				pos4 = "4.9 -12";
				terreno = "terra";
		};
		UNG_VaNo01.onLevelLoaded();
		new t2dStaticSprite(UNG_VaNo02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "0.3 -14.9";
				pos1 = "1.4 -17.2";
				pos2 = "2.7 -19.4";
				pos3 = "2.1 -15.7";
				pos4 = "0.9 -20.5";
				terreno = "terra";
		};
		UNG_VaNo02.onLevelLoaded();
		new t2dStaticSprite(UNG_VaNo03) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "8 -17.1";
				pos1 = "7.2 -19";
				pos2 = "7.6 -20.6";
				pos3 = "8.8 -18.8";
				pos4 = "9.1 -22.2";
				terreno = "terra";
		};
		UNG_VaNo03.onLevelLoaded();
		new t2dStaticSprite(UNG_VaNo04) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "1.8 -26.5";
				pos1 = "4.4 -24.1";
				pos2 = "2.3 -24.1";
				pos3 = "0.4 -28.4";
				pos4 = "5.7 -25.3";
				terreno = "terra";
		};
		UNG_VaNo04.onLevelLoaded();
		new t2dStaticSprite(UNG_CaNo01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-4.2 -26.4";
				pos1 = "-3.5 -22.3";
				pos2 = "-3.7 -24.2";
				pos3 = "-5.8 -25";
				pos4 = "-3.5 -20.2";
				terreno = "terra";
		};
		UNG_CaNo01.onLevelLoaded();
		new t2dStaticSprite(UNG_CaNo02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-8.9 -21.6";
				pos1 = "-11.4 -21.5";
				pos2 = "-13.7 -21.5";
				pos3 = "-7 -21.5";
				pos4 = "-16 -21.4";
				terreno = "terra";
		};
		UNG_CaNo02.onLevelLoaded();
		new t2dStaticSprite(UNG_CaNo03) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-10.9 -26.8";
				pos1 = "-13.3 -27";
				pos2 = "-15.2 -26.6";
				pos3 = "-9.1 -26.7";
				pos4 = "-15.6 -28.3";
				terreno = "terra";
		};
		UNG_CaNo03.onLevelLoaded();
		new t2dStaticSprite(UNG_CaNo04) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-23.4 -23.5";
				pos1 = "-20.2 -25";
				pos2 = "-20.6 -23.5";
				pos3 = "-22 -25.2";
				pos4 = "-19.9 -26.6";
				terreno = "terra";
		};
		UNG_CaNo04.onLevelLoaded();
		new t2dStaticSprite(UNG_CaOr01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "35.3 -15.6";
				pos1 = "37 -17.4";
				pos2 = "32.1 -15.6";
				pos3 = "33.4 -14.4";
				pos4 = "39.3 -17.3";
				terreno = "terra";
		};
		UNG_CaOr01.onLevelLoaded();
		new t2dStaticSprite(UNG_CaOr02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "26 -5.9";
				pos1 = "28 -7.6";
				pos2 = "29.3 -9.3";
				pos3 = "27.1 -3.9";
				pos4 = "30.6 -10.9";
				terreno = "terra";
		};
		UNG_CaOr02.onLevelLoaded();
		new t2dStaticSprite(UNG_CaOr03) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "41.7 -12.2";
				pos1 = "40.9 -9.9";
				pos2 = "40.5 -7.9";
				pos3 = "40.3 -13.6";
				pos4 = "40.9 -5.6";
				terreno = "terra";
		};
		UNG_CaOr03.onLevelLoaded();
		new t2dStaticSprite(UNG_CaOr04) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "35.6 -8.7";
				pos1 = "35.6 -5.7";
				pos2 = "33.2 -6.3";
				pos3 = "36 -10.6";
				pos4 = "36.3 -3.5";
				terreno = "terra";
		};
		UNG_CaOr04.onLevelLoaded();
		new t2dStaticSprite(UNG_PaGu01) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-11 -3";
				pos1 = "-14.3 -3.5";
				pos2 = "-13.2 -2";
				pos3 = "-12.3 -4.6";
				pos4 = "-16.5 -2.6";
				terreno = "terra";
		};
		UNG_PaGu01.onLevelLoaded();
		new t2dStaticSprite(UNG_PaGu02) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-5.1 -2.3";
				pos1 = "-3.7 -4.3";
				pos2 = "-6.6 -4";
				pos3 = "-7 -2.4";
				pos4 = "-1.2 -4";
				terreno = "terra";
		};
		UNG_PaGu02.onLevelLoaded();
		new t2dStaticSprite(UNG_PaGu03) {
			class = "Area";
			
				planeta = "Ungart";
				pos0 = "-10.7 2.4";
				pos1 = "-7.3 1.8";
				pos2 = "-9.7 0.8";
				pos3 = "-8.9 1.9";
				pos4 = "-5.5 1.8";
				terreno = "terra";
		};
		UNG_PaGu03.onLevelLoaded();
}


///////////////////
//Telúria:
//grupos de Telúria:
function initTeluriaGrupos(){
	$planetaTeluria.addGrupo("Terion", 2, "TEL_terion01" SPC "TEL_terion02", "minerio");
	$planetaTeluria.addGrupo("Nir", 2, "TEL_nir01" SPC "TEL_nir02", "minerio");
	$planetaTeluria.addGrupo("Karzin", 2, "TEL_karzin01" SPC "TEL_karzin02", "minerio");
	$planetaTeluria.addGrupo("Goruk", 2, "TEL_goruk01" SPC "TEL_goruk02", "minerio");
	$planetaTeluria.addGrupo("Malik", 2, "TEL_malik01" SPC "TEL_malik02", "minerio");
	
	$planetaTeluria.addGrupo("Zavinia", 2, "TEL_zavinia01" SPC "TEL_zavinia02", "petroleo");
	$planetaTeluria.addGrupo("Lornia", 2, "TEL_lornia01" SPC "TEL_lornia02", "petroleo");
	$planetaTeluria.addGrupo("Argonia", 4, "TEL_argonia01" SPC "TEL_argonia02" SPC "TEL_argonia03" SPC "TEL_argonia04", "petroleo");
	
	$planetaTeluria.addGrupo("Dharin", 3, "TEL_dharin01" SPC "TEL_dharin02" SPC "TEL_dharin03", "uranio");
	$planetaTeluria.addGrupo("Valinur", 2, "TEL_valinur01" SPC "TEL_valinur02", "uranio");
	$planetaTeluria.addGrupo("Vuldan", 3, "TEL_vuldan01" SPC "TEL_vuldan02" SPC "TEL_vuldan03", "uranio");
	$planetaTeluria.addGrupo("Keltur", 3, "TEL_keltur01" SPC "TEL_keltur02" SPC "TEL_keltur03", "uranio");
	
	$planetaTeluria.addGrupo("Nexus", 1, "TEL_nexus01", "Reliquia");
	$planetaTeluria.addGrupo("GeoCanhao", 1, "TEL_canhao01", "Artefato");
}

function initTeluriaDesastres(){
	$planetaTeluria.areasDeDesastre = new SimSet();
	$planetaTeluria.registrarDesastre("TEL_valinur01", "vulcao");	
	$planetaTeluria.registrarDesastre("TEL_valinur02", "vulcao");	
	
	$planetaTeluria.registrarDesastre("TEL_vuldan01", "vulcao");	
	$planetaTeluria.registrarDesastre("TEL_vuldan02", "vulcao");	
	$planetaTeluria.registrarDesastre("TEL_vuldan03", "vulcao");	
	
	$planetaTeluria.registrarDesastre("TEL_dharin01", "vulcao");	
	$planetaTeluria.registrarDesastre("TEL_dharin02", "vulcao");	
	$planetaTeluria.registrarDesastre("TEL_dharin03", "vulcao");	
	
	$planetaTeluria.registrarDesastre("TEL_keltur01", "vulcao");	
	$planetaTeluria.registrarDesastre("TEL_keltur02", "vulcao");	
	$planetaTeluria.registrarDesastre("TEL_keltur03", "vulcao");	
	
	$planetaTeluria.registrarDesastre("TEL_argonia01", "gas");
	$planetaTeluria.registrarDesastre("TEL_argonia02", "gas");
	$planetaTeluria.registrarDesastre("TEL_argonia03", "gas");
	$planetaTeluria.registrarDesastre("TEL_argonia04", "gas");
	
	$planetaTeluria.registrarDesastre("TEL_zavinia01", "gas");
	$planetaTeluria.registrarDesastre("TEL_zavinia02", "gas");
	
	$planetaTeluria.registrarDesastre("TEL_lornia01", "gas");
	$planetaTeluria.registrarDesastre("TEL_lornia02", "gas");
	
	$planetaTeluria.registrarDesastre("TEL_b01", "furacao");
	$planetaTeluria.registrarDesastre("TEL_b02", "furacao");
	$planetaTeluria.registrarDesastre("TEL_b04", "furacao");
	$planetaTeluria.registrarDesastre("TEL_b07", "furacao");
	$planetaTeluria.registrarDesastre("TEL_b08", "furacao");
	$planetaTeluria.registrarDesastre("TEL_b09", "furacao");
	$planetaTeluria.registrarDesastre("TEL_b20", "furacao");
	$planetaTeluria.registrarDesastre("TEL_b23", "furacao");
	$planetaTeluria.registrarDesastre("TEL_b27", "furacao");
	$planetaTeluria.registrarDesastre("TEL_b28", "furacao");
	$planetaTeluria.registrarDesastre("TEL_b29", "furacao");
	
	$planetaTeluria.registrarDesastre("TEL_nir02", "furacao"); 
	$planetaTeluria.registrarDesastre("TEL_malik02", "furacao");
	$planetaTeluria.registrarDesastre("TEL_terion02", "furacao");
	$planetaTeluria.registrarDesastre("TEL_karzin01", "furacao");
}


function serverCarregarTeluria(){
	new t2dStaticSprite(TEL_b01) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-28.8 -34.1";
         pos1 = "-29.9 -31.2";
         pos2 = "-26.9 -31.2";
         pos3 = "-29.9 -28.2";
         terreno = "mar";
   };
TEL_b01.onLevelLoaded();
   new t2dStaticSprite(TEL_b02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-25.3 -21.2";
         pos1 = "-24.2 -18.3";
         pos2 = "-24.6 -15.5";
         pos3 = "-26.4 -24.1";
         terreno = "mar";
   };
TEL_b02.onLevelLoaded();
   new t2dStaticSprite(TEL_b03) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "-15.9 -18";
         pos1 = "-17.4 -14.9";
         pos2 = "-13.6 -15.5";
         pos3 = "-19.6 -17.5";
         terreno = "mar";
   };
TEL_b03.onLevelLoaded();
   new t2dStaticSprite(TEL_b04) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-6.6 -22.2";
         pos1 = "-3.2 -22.3";
         pos2 = "-9.4 -20.3";
         pos3 = "-9.7 -23.3";
         terreno = "mar";
   };
TEL_b04.onLevelLoaded();
   new t2dStaticSprite(TEL_b05) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-12.3 -34.1";
         pos1 = "-8.8 -28.6";
         pos2 = "-11.7 -27.3";
         pos3 = "-11.6 -30.5";
         terreno = "mar";
   };
TEL_b05.onLevelLoaded();
   new t2dStaticSprite(TEL_b06) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "4.7 -18.4";
         pos1 = "7.7 -17.6";
         pos2 = "2.3 -20.3";
         pos3 = "1.8 -17.5";
         terreno = "mar";
   };
TEL_b06.onLevelLoaded();
   new t2dStaticSprite(TEL_b07) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "6.1 -30.5";
         pos1 = "4.3 -26.5";
         pos2 = "6.2 -24.2";
         pos3 = "6.3 -34.5";
         terreno = "mar";
   };
TEL_b07.onLevelLoaded();
   new t2dStaticSprite(TEL_b08) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "15.1 -34.3";
         pos1 = "12 -34.8";
         pos2 = "18.8 -33.7";
         pos3 = "11.2 -32.2";
         terreno = "mar";
   };
TEL_b08.onLevelLoaded();
   new t2dStaticSprite(TEL_b09) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "26.7 -32.1";
         pos1 = "24 -29.8";
         pos2 = "28 -29.2";
         pos3 = "25 -34.8";
         terreno = "mar";
   };
TEL_b09.onLevelLoaded();
   new t2dStaticSprite(TEL_b10) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "45.3 13.3";
         pos1 = "45.5 9.6";
         pos2 = "47.4 11.1";
         pos3 = "42.3 14.5";
         terreno = "mar";
         transpos = "0";
   };
TEL_b10.onLevelLoaded();
   new t2dStaticSprite(TEL_b11) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "46.7 0.8";
         pos1 = "47.5 3.5";
         pos2 = "46.5 -2.4";
         pos3 = "45.2 5.6";
         terreno = "mar";
         transpos = "0";
   };
TEL_b11.onLevelLoaded();
   new t2dStaticSprite(TEL_b12) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "46.7 -33.3";
         pos1 = "47 -30.8";
         pos2 = "47 -34";
         pos3 = "46.1 -28.2";
         terreno = "mar";
         transpos = "0";
   };
TEL_b12.onLevelLoaded();
   new t2dStaticSprite(TEL_b13) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "43.3 -21.8";
         pos1 = "40 -20.4";
         pos2 = "46.7 -23.1";
         pos3 = "47 -20.1";
         terreno = "mar";
         transpos = "0";
   };
TEL_b13.onLevelLoaded();
   new t2dStaticSprite(TEL_b14) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "45.5 -12.9";
         pos1 = "44 -16.1";
         pos2 = "46.7 -9.5";
         pos3 = "39.5-16.4";
         terreno = "mar";
         transpos = "0";
   };
TEL_b14.onLevelLoaded();
   new t2dStaticSprite(TEL_b15) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "29.2 -21.4";
         pos1 = "28 -18.8";
         pos2 = "33 -19.8";
         pos3 = "28 -24.8";
         terreno = "mar";
   };
TEL_b15.onLevelLoaded();
   new t2dStaticSprite(TEL_b16) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "17.3 -16.5";
         pos1 = "20.3 -16.3";
         pos2 = "13.8 -16.1";
         pos3 = "22.3 -18.3";
         terreno = "mar";
   };
TEL_b16.onLevelLoaded();
   new t2dStaticSprite(TEL_b17) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "25.3 -11.5";
         pos1 = "27.5 -14.2";
         pos2 = "23.3 -12.3";
         pos3 = "28.3 -9.3";
         terreno = "mar";
   };
TEL_b17.onLevelLoaded();
   new t2dStaticSprite(TEL_b18) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "8.7 -9.9";
         pos1 = "11.8 -10.6";
         pos2 = "7.4 -12.6";
         pos3 = "9.3 -6.3";
         terreno = "mar";
   };
TEL_b18.onLevelLoaded();
   new t2dStaticSprite(TEL_b19) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "27.8 12.2";
         pos1 = "29.4 8.8";
         pos2 = "26.3 8.7";
         pos3 = "24.3 13.4";
         terreno = "mar";
   };
TEL_b19.onLevelLoaded();
   new t2dStaticSprite(TEL_b20) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "27.3 0.5";
         pos1 = "28.5 3.2";
         pos2 = "28.3 -4.2";
         pos3 = "25.3 4.5";
         terreno = "mar";
   };
TEL_b20.onLevelLoaded();
   new t2dStaticSprite(TEL_b21) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "18.6 -3.1";
         pos1 = "22.3 -1.2";
         pos2 = "15.3 -3.2";
         pos3 = "23.7 -3.5";
         terreno = "mar";
   };
TEL_b21.onLevelLoaded();
   new t2dStaticSprite(TEL_b22) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "10.3 12.8";
         pos1 = "7.2 11.4";
         pos2 = "6.3 13.8";
         pos3 = "12.3 9.7";
         terreno = "mar";
   };
TEL_b22.onLevelLoaded();
   new t2dStaticSprite(TEL_b23) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "11 3";
         pos1 = "8.3 5.8";
         pos2 = "11.3 -0.2";
         pos3 = "5.3 7.6";
         terreno = "mar";
   };
TEL_b23.onLevelLoaded();
   new t2dStaticSprite(TEL_b24) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-3.3 13.1";
         pos1 = "0 13.7";
         pos2 = "-6.7 13.7";
         pos3 = "0.3 10.7";
         terreno = "mar";
   };
TEL_b24.onLevelLoaded();
   new t2dStaticSprite(TEL_b25) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-19.4 7.7";
         pos1 = "-15.7 10.7";
         pos2 = "-13 12.7";
         pos3 = "-11.7 9.7";
         terreno = "mar";
   };
TEL_b25.onLevelLoaded();
   new t2dStaticSprite(TEL_b26) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "-28.7 13.5";
         pos1 = "-22.7 11.7";
         pos2 = "-20.7 13.7";
         pos3 = "-24.7 13.7";
         terreno = "mar";
   };
TEL_b26.onLevelLoaded();
   new t2dStaticSprite(TEL_b27) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-22.7 0.1";
         pos1 = "-18.1 1.6";
         pos2 = "-18.9 -0.6";
         pos3 = "-19.7 3.5";
         terreno = "mar";
   };
TEL_b27.onLevelLoaded();
   new t2dStaticSprite(TEL_b28) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-22.6 -7.5";
         pos1 = "-19.9 -9.2";
         pos2 = "-22.7 -4.5";
         pos3 = "-24.7 -10.3";
         terreno = "mar";
   };
TEL_b28.onLevelLoaded();
   new t2dStaticSprite(TEL_b29) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-31 -1.1";
         pos1 = "-27.8 -1.3";
         pos2 = "-28.7 -4.3";
         pos3 = "-32.7 1.7";
         terreno = "mar";
   };
TEL_b29.onLevelLoaded();
   new t2dStaticSprite(TEL_b30) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-44.9 12.9";
         pos1 = "-43.7 8.7";
         pos2 = "-41.7 10.3";
         pos3 = "-46.6 9.7";
         terreno = "mar";
         transpos = "0";
   };
TEL_b30.onLevelLoaded();
   new t2dStaticSprite(TEL_b31) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-41.6 1.0";
         pos1 = "-40.7 1.7";
         pos2 = "-37.8 -0.8";
         pos3 = "-42.7 4.7";
         terreno = "mar";
         transpos = "0";
   };
TEL_b31.onLevelLoaded();
   new t2dStaticSprite(TEL_b32) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-44.7 -8";
         pos1 = "-43.5 -5.3";
         pos2 = "-46.7 -4.3";
         pos3 = "-45.5 -11";
         terreno = "mar";
         transpos = "0";
   };
TEL_b32.onLevelLoaded();
   new t2dStaticSprite(TEL_b33) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "44.8 19.2";
         pos1 = "-43.8 -16.1";
         pos2 = "-43.7 -15.3";
         pos3 = "-45.7 -22.3";
         terreno = "mar";
         transpos = "0";
   };
TEL_b33.onLevelLoaded();
   new t2dStaticSprite(TEL_b34) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-45.6 -34.5";
         pos1 = "-46.6 -29.8";
         pos2 = "-45.7 -27.2";
         pos3 = "-44.6 -31.3";
         terreno = "mar";
         transpos = "0";
   };
TEL_b34.onLevelLoaded();
   new t2dStaticSprite(TEL_terion01) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "-35.9 12.8";
         pos1 = "-35.6 10.2";
         pos2 = "-33.2 9.6";
         pos3 = "-38.3 13.5";
         pos4 = "-37 8";
         terreno = "terra";
   };
TEL_terion01.onLevelLoaded();
   new t2dStaticSprite(TEL_terion02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-28.8 6.4";
         pos1 = "-25 6.3";
         pos2 = "-26.2 4.5";
         pos3 = "-23.1 6.2";
         pos4 = "-25.8 8.1";
         terreno = "terra";
   };
TEL_terion02.onLevelLoaded();
   new t2dStaticSprite(TEL_nir01) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-21.7 -33.9";
         pos1 = "-19 -30.8";
         pos2 = "-19.1 -32.9";
         pos3 = "-21.2 -30.6";
         pos4 = "-22.4 -28.8";
         terreno = "terra";
   };
TEL_nir01.onLevelLoaded();
   new t2dStaticSprite(TEL_nir02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-19.5 -22.1";
         pos1 = "-16.7 -24.2";
         pos2 = "-18.3 -25.8";
         pos3 = "-18.8 -24.1";
         pos4 = "-15.7 -25.9";
         terreno = "terra";
   };
TEL_nir02.onLevelLoaded();
   new t2dStaticSprite(TEL_karzin01) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "10.3 -25.1";
         pos1 = "13.1 -23.2";
         pos2 = "11.1 -22.1";
         pos3 = "13.5 -21.5";
         pos4 = "13.1 -25.8";
         terreno = "terra";
   };
TEL_karzin01.onLevelLoaded();
   new t2dStaticSprite(TEL_karzin02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "41.3 1";
         pos1 = "41.3 3.6";
         pos2 = "41.2 6.1";
         pos3 = "41.2 -0.8";
         pos4 = "41.8 8.3";
         terreno = "terra";
   };
TEL_karzin02.onLevelLoaded();
   new t2dStaticSprite(TEL_karzin02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "21.1 -22.8";
         pos1 = "18.7 -25";
         pos2 = "21.1 -24.8";
         pos3 = "19.9 -26.5";
         pos4 = "17.9 -29.1";
         terreno = "terra";
   };
TEL_karzin02.onLevelLoaded();
   new t2dStaticSprite(TEL_goruk02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "36.2 -26.6";
         pos1 = "38.9 -25.7";
         pos2 = "37.3 -24.3";
         pos3 = "36 -22.7";
         pos4 = "39.8 -27.8";
         terreno = "terra";
   };
TEL_goruk02.onLevelLoaded();
   new t2dStaticSprite(TEL_goruk01) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "38.5 -34.6";
         pos1 = "35.5 -32.6";
         pos2 = "37.8 -32.3";
         pos3 = "33.1 -31.1";
         pos4 = "35.2 -30.5";
         terreno = "terra";
   };
TEL_goruk01.onLevelLoaded();
   new t2dStaticSprite(TEL_malik01) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "16.4 13.6";
         pos1 = "19.9 10.1";
         pos2 = "16.8 10";
         pos3 = "21.8 10.1";
         pos4 = "18.1 11.7";
         terreno = "terra";
   };
TEL_malik01.onLevelLoaded();
   new t2dStaticSprite(TEL_malik02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "17.2 5.8";
         pos1 = "17.1 3";
         pos2 = "19.5 4.5";
         pos3 = "20.1 6.1";
         pos4 = "19.4 2";
         terreno = "terra";
   };
TEL_malik02.onLevelLoaded();
   new t2dStaticSprite(TEL_zavinia01) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-34.9 -34.2";
         pos1 = "-37.3 -32.5";
         pos2 = "-39.8 -32.5";
         pos3 = "-40 -34.2";
         pos4 = "-37.9 -34.2";
         terreno = "terra";
   };
TEL_zavinia01.onLevelLoaded();
   new t2dStaticSprite(TEL_zavinia02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-36.5 -24.7";
         pos1 = "-37.5 -27.2";
         pos2 = "-39 -25.3";
         pos3 = "-35.7 -28.3";
         pos4 = "-39.7 -27.3";
         terreno = "terra";
   };
TEL_zavinia02.onLevelLoaded();
   new t2dStaticSprite(TEL_dharin01) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "-40 -17";
         pos1 = "-37.6 -17.2";
         pos2 = "-37.6 -15";
         pos3 = "-39.4 -14";
         pos4 = "-38.4 -19.4";
         terreno = "terra";
   };
TEL_dharin01.onLevelLoaded();
   new t2dStaticSprite(TEL_dharin02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-31.5 -15.8";
         pos1 = "-32.1 -18";
         pos2 = "-32.5 -20.2";
         pos3 = "-33.4 -16.4";
         pos4 = "-32.6 -22.2";
         terreno = "terra";
   };
TEL_dharin02.onLevelLoaded();
   new t2dStaticSprite(TEL_dharin03) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-33.2 -7.7";
         pos1 = "-34.4 -9.9";
         pos2 = "-36 -7.9";
         pos3 = "-38.1 -8.1";
         pos4 = "-31.5 -10.2";
         terreno = "terra";
   };
TEL_dharin03.onLevelLoaded();
   new t2dStaticSprite(TEL_valinur01) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-11.9 -10.4";
         pos1 = "-13.8 -6.8";
         pos2 = "-13.4 -8.6";
         pos3 = "-14.5 -5.2";
         pos4 = "-16.3 -5.7";
         terreno = "terra";
   };
TEL_valinur01.onLevelLoaded();
   new t2dStaticSprite(TEL_valinur02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-11.6 4.7";
         pos1 = "-11.5 1.3";
         pos2 = "-10 2.7";
         pos3 = "-12 2.7";
         pos4 = "-12.6 -0.6";
         terreno = "terra";
   };
TEL_valinur02.onLevelLoaded();
   new t2dStaticSprite(TEL_vuldan01) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "3.9 -8.5";
         pos1 = "0.7 -11.3";
         pos2 = "1.7 -9.6";
         pos3 = "0 -9.6";
         pos4 = "-0.5 -12.8";
         terreno = "terra";
   };
TEL_vuldan01.onLevelLoaded();
   new t2dStaticSprite(TEL_vuldan02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "4.6 -4.4";
         pos1 = "1.8 -4.3";
         pos2 = "-0.4 -3.5";
         pos3 = "-2.6 -4.1";
         pos4 = "-0.6 -5.8";
         terreno = "terra";
   };
TEL_vuldan02.onLevelLoaded();
   new t2dStaticSprite(TEL_vuldan03) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "3.2 2.2";
         pos1 = "3.3 0";
         pos2 = "6.1 1";
         pos3 = "5.5 -0.6";
         pos4 = "0.8 0.8";
         terreno = "terra";
   };
TEL_vuldan03.onLevelLoaded();
   new t2dStaticSprite(TEL_argonia01) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-3.4 -16.8";
         pos1 = "-6.2 -13.2";
         pos2 = "-6.1 -15.5";
         pos3 = "-4.6 -14.6";
         pos4 = "-9 -13.5";
         terreno = "terra";
   };
TEL_argonia01.onLevelLoaded();
   new t2dStaticSprite(TEL_argonia02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-4 -9.8";
         pos1 = "-6.2 -6.5";
         pos2 = "-6.2 -8.5";
         pos3 = "-8.2 -8.3";
         pos4 = "-8.7-6.5";
         terreno = "terra";
   };
TEL_argonia02.onLevelLoaded();
   new t2dStaticSprite(TEL_argonia03) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-6 1.7";
         pos1 = "-5.6 -0.3";
         pos2 = "-6.3 -2.5";
         pos3 = "-7.2 -0.9";
         pos4 = "-8.7 -3";
         terreno = "terra";
   };
TEL_argonia03.onLevelLoaded();
   new t2dStaticSprite(TEL_argonia04) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-4.4 8.2";
         pos1 = "-3.1 5.7";
         pos2 = "-1.8 7.2";
         pos3 = "-1.8 4.5";
         pos4 = "-5.8 6.2";
         terreno = "terra";
   };
TEL_argonia04.onLevelLoaded();
   new t2dStaticSprite(TEL_lornia01) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "33 -12.5";
         pos1 = "37.6 -10.6";
         pos2 = "35.6 -11.8";
         pos3 = "40.9 -11.5";
         pos4 = "40.6 -9.7";
         terreno = "terra";
   };
TEL_lornia01.onLevelLoaded();
   new t2dStaticSprite(TEL_lornia02) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "40.7 -4.7";
         pos1 = "35.6 -5";
         pos2 = "38.2 -4.4";
         pos3 = "36.1 -6.7";
         pos4 = "33.5 -4.2";
         terreno = "terra";
   };
TEL_lornia02.onLevelLoaded();
   new t2dStaticSprite(TEL_keltur01) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "33 1.1";
         pos1 = "34.2 3.5";
         pos2 = "35.3 0.5";
         pos3 = "34 5.3";
         pos4 = "32.7 -1.2";
         terreno = "terra";
   };
TEL_keltur01.onLevelLoaded();
   new t2dStaticSprite(TEL_keltur02) {
     class = "Area";
         planeta = "Teluria";
         pos0 = "41 9";
         pos1 = "39.7 4.5";
         pos2 = "40.2 6.7";
         pos3 = "41.2 0.3";
         pos4 = "39.9 2.1";
         terreno = "terra";
   };
TEL_keltur02.onLevelLoaded();
   new t2dStaticSprite(TEL_keltur03) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "34.4 13.5";
         pos1 = "36.1 10.9";
         pos2 = "37.6 12.1";
         pos3 = "34.1 11.5";
         pos4 = "34.6 9.7";
         terreno = "terra";
   };
TEL_keltur03.onLevelLoaded();
   new t2dStaticSprite(TEL_canhao01) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "16.3 -8.4";
         pos1 = "19.6 -9.8";
         pos2 = "18.7 -11.4";
         pos3 = "20 -8.5";
         pos4 = "20.5 -7";
         terreno = "terra";
		 artefato = 1;
   };
TEL_canhao01.onLevelLoaded();
   new t2dStaticSprite(TEL_nexus01) {
      class = "Area";
         planeta = "Teluria";
         pos0 = "-1 -28.2";
         pos1 = "-4.5 -31.7";
         pos2 = "-3.9 -29.6";
         pos3 = "-5.6 -33.9";
         pos4 = "-3.5 -33.9";
         reliquia = 1;
         terreno = "terra";
   };
TEL_nexus01.onLevelLoaded();
}