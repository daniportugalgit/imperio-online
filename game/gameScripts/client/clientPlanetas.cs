// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientPlanetas.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quinta-feira, 30 de outubro de 2008 13:57
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

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

function clientCriarPlaneta(%nomeDoPlaneta, %desastres, %totalDeInfos, %totalDeObjs, %id, %lotacao){
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
	clientCriarSimAreasDeGrupos(%esteGrupo);
}

function clientCriarSimAreasDeGrupos(%grupo){
	%grupo.simAreas = new SimSet();
	for(%i = 0; %i < %grupo.numDeAreas; %i++){
		%eval = "%nomeDaArea = %grupo.area" @ %i+1 @ "Nome;";
		eval(%eval);
		
		%eval = "%grupo.simAreas.add(" @ %nomeDaArea @ ");";
		eval(%eval);
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
//falta criar os desastres!


//cria de fato os planetas
function clientCriarPlanetaTerra(){
	if(isObject($planetaTerra)){
		$planetaTerra.delete();
	}
	clientCriarPlaneta("Terra", 1, 78, 26, 1, 4); //%nome, %desastres, %infos, %objs, %id, %lotacao;
	$planetaTerra.desc = "O PLANETA TERRA SE CARACTERIZA PRINCIPALMENTE PELA PRESENÇA DE OCEANOS E PELA ESCASSEZ DE PETRÓLEO.";
	echo("***********CRIANDO PLANETA TERRA!****************");
}
function clientCriarPlanetaUngart(){
	if(isObject($planetaUngart)){
		$planetaUngart.delete();
	}
	clientCriarPlaneta("Ungart", 14, 80, 38, 2, 5); //%nome, %desastres, %infos, %objs, %id, %lotacao;
	$planetaUngart.desc = "UNGART POSSUI RECURSOS ABUNDANTES, DESASTRES FREQUENTES E UM COMPLEXO SISTEMA DE CANAIS MARÍTIMOS.";
	echo("***********CRIANDO PLANETA UNGART!****************");
}
function clientCriarPlanetaTeluria(){
	if(isObject($planetaTeluria)){
		$planetaTeluria.delete();
	}
	clientCriarPlaneta("Teluria", 10, 73, 45, 3, 4); //%nome, %desastres, %infos, %objs, %i, %lotacao;
	$nexus = nexusAlquimico;
	$nexus.onde = TEL_nexus01;
	$geoCanhao = geoCanhao;
	$geoCanhao.onde = TEL_canhao01;
	$planetaTeluria.desc = "PLANETA CARACTERIZADO POR UM ARTEFATO E UMA RELÍQUIA DE TECNOLOGIA ALIENÍGENA. OS RECURSOS SÃO ABUNDANTES.";
	echo("***********CRIANDO PLANETA TELÚRIA!****************");
}




/////////////
function clientFindGrupo(%nome){
	%planeta = clientGetPlaneta($planetaAtual);
	for(%i = 0; %i < %planeta.grupos.getCount(); %i++){
		%grupo = %planeta.grupos.getObject(%i);
		if(%grupo.nome $= %nome){
			return %grupo;
		}
	}
}
