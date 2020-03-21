// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\info.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 30 de julho de 2007 14:24
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

//função para encontrar a info de qualquer planeta (CLIENT):
function clientFindInfo(%infoNum){
	%eval = "%info = $info" @ $myPersona.planetaAtual @ %infoNum @ ";";
	eval(%eval);
	return %info;
}

// para encontrar o objeto global INFO[x], conforme o planeta (SERVER):
function jogo::findInfo(%this, %infoNum){
	%eval = "%info = $info" @ %this.planeta.nome @ %infoNum @ ";";
	eval(%eval);
	return %info;
}
/////////////////////////////////

///////////////////////////////////////////////
//PLANETA TERRA:
//////////////////////////////////////////////
//Minérios:
$infoTerra1 = new ScriptObject(){
	tipo = "recurso";
	Area = sumatra;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 1;
};

$infoTerra2 = new ScriptObject(){
	tipo = "recurso";
	Area = losAngeles;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 2;
};

$infoTerra3 = new ScriptObject(){
	tipo = "recurso";
	Area = borneo;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 3;
};

$infoTerra4 = new ScriptObject(){
	tipo = "recurso";
	Area = india;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 4;
};

$infoTerra5 = new ScriptObject(){
	tipo = "recurso";
	Area = cidadeDoCabo;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 5;
};

$infoTerra6 = new ScriptObject(){
	tipo = "recurso";
	Area = omsk;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 6;
};

$infoTerra7 = new ScriptObject(){
	tipo = "recurso";
	Area = cazaquistao;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 7;
};

$infoTerra8 = new ScriptObject(){
	tipo = "recurso";
	Area = lhasa;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 8;
};

$infoTerra9 = new ScriptObject(){
	tipo = "recurso";
	Area = vancouver;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 9;
};

$infoTerra10 = new ScriptObject(){
	tipo = "recurso";
	Area = mongolia;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 10;
};

$infoTerra11 = new ScriptObject(){
	tipo = "recurso";
	Area = cabul;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 11;
};

$infoTerra12 = new ScriptObject(){
	tipo = "recurso";
	Area = bolivia;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 12;
};

$infoTerra13 = new ScriptObject(){
	tipo = "recurso";
	Area = perth;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 13;
};

$infoTerra14 = new ScriptObject(){
	tipo = "recurso";
	Area = comunidadeEuropeia;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 14;
};

$infoTerra15 = new ScriptObject(){
	tipo = "recurso";
	Area = saoPaulo;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 15;
};

$infoTerra16 = new ScriptObject(){
	tipo = "recurso";
	Area = novaGuine;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 16;
};

$infoTerra17 = new ScriptObject(){
	tipo = "recurso";
	Area = groenlandia;
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 17;
};

//Petróleos:
$infoTerra18 = new ScriptObject(){
	tipo = "recurso";
	Area = marrakesh;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 18;
};

$infoTerra19 = new ScriptObject(){
	tipo = "recurso";
	Area = bGolfoDoMaine;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 19;
};

$infoTerra20 = new ScriptObject(){
	tipo = "recurso";
	Area = bMarDeKara;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 20;
};

$infoTerra21 = new ScriptObject(){
	tipo = "recurso";
	Area = kirov;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 21;
};

$infoTerra22 = new ScriptObject(){
	tipo = "recurso";
	Area = bMarDaNoruega;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 22;
};

$infoTerra23 = new ScriptObject(){
	tipo = "recurso";
	Area = novaYork;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 23;
};

$infoTerra24 = new ScriptObject(){
	tipo = "recurso";
	Area = bGuanabara;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 24;
};

$infoTerra25 = new ScriptObject(){
	tipo = "recurso";
	Area = bMarDaArabia;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 25;
};

$infoTerra26 = new ScriptObject(){
	tipo = "recurso";
	Area = bMarDoCaribe;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 26;
};

$infoTerra27 = new ScriptObject(){
	tipo = "recurso";
	Area = manaus;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 27;
};

$infoTerra28 = new ScriptObject(){
	tipo = "recurso";
	Area = bDeBaffin;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 28;
};

$infoTerra29 = new ScriptObject(){
	tipo = "recurso";
	Area = bDeLuanda;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 29;
};

$infoTerra30 = new ScriptObject(){
	tipo = "recurso";
	Area = bagda;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 30;
};

$infoTerra31 = new ScriptObject(){
	tipo = "recurso";
	Area = xangai;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 31;
};

$infoTerra32 = new ScriptObject(){
	tipo = "recurso";
	Area = montreal;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 32;
};

$infoTerra33 = new ScriptObject(){
	tipo = "recurso";
	Area = venezuela;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 33;
};

$infoTerra34 = new ScriptObject(){
	tipo = "recurso";
	Area = bMarDeJava;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 34;
};

$infoTerra35 = new ScriptObject(){
	tipo = "recurso";
	Area = comunidadeEuropeia;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 35;
};

$infoTerra36 = new ScriptObject(){
	tipo = "recurso";
	Area = sydney;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 36;
};


//Agora os Urânios:
$infoTerra37 = new ScriptObject(){
	tipo = "recurso";
	Area = saoPaulo;
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 37;
};

$infoTerra38 = new ScriptObject(){
	tipo = "recurso";
	Area = houston;
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 38;
};

$infoTerra39 = new ScriptObject(){
	tipo = "recurso";
	Area = cazaquistao;
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 39;
};

$infoTerra40 = new ScriptObject(){
	tipo = "recurso";
	Area = pequim;
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 40;
};

$infoTerra41 = new ScriptObject(){
	tipo = "recurso";
	Area = comunidadeEuropeia;
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 41;
};


//Agora as construções:
$infoTerra42 = new ScriptObject(){
	tipo = "build";
	Area = bMarDaChina;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 42;
};

$infoTerra43 = new ScriptObject(){
	tipo = "build";
	Area = kinshasa;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 43;
};

$infoTerra44 = new ScriptObject(){
	tipo = "build";
	Area = bMarDaArabia;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 44;
};

$infoTerra45 = new ScriptObject(){
	tipo = "build";
	Area = bGolfoDoPanama;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 45;
};

$infoTerra46 = new ScriptObject(){
	tipo = "build";
	Area = sydney;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 46;
};

$infoTerra47 = new ScriptObject(){
	tipo = "build";
	Area = bGolfoDoMaine;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 47;
};

$infoTerra48 = new ScriptObject(){
	tipo = "build";
	Area = mongolia;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 48;
};

$infoTerra49 = new ScriptObject(){
	tipo = "build";
	Area = bGuanabara;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 49;
};

$infoTerra50 = new ScriptObject(){
	tipo = "build";
	Area = toronto;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 50;
};

$infoTerra51 = new ScriptObject(){
	tipo = "build";
	Area = novaYork;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 51;
};

$infoTerra52 = new ScriptObject(){
	tipo = "build";
	Area = pequim;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 52;
};

$infoTerra53 = new ScriptObject(){
	tipo = "build";
	Area = india;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 53;
};

$infoTerra54 = new ScriptObject(){
	tipo = "build";
	Area = groenlandia;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 54;
};

$infoTerra55 = new ScriptObject(){
	tipo = "build";
	Area = vietna;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 55;
};

$infoTerra56 = new ScriptObject(){
	tipo = "build";
	Area = moscou;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 56;
};

$infoTerra57 = new ScriptObject(){
	tipo = "build";
	Area = bMarDeJava;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 57;
};

$infoTerra58 = new ScriptObject(){
	tipo = "build";
	Area = bMarNordico;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 58;
};

$infoTerra59 = new ScriptObject(){
	tipo = "build";
	Area = cazaquistao;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 59;
};

$infoTerra60 = new ScriptObject(){
	tipo = "build";
	Area = bDeDakar;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 60;
};

$infoTerra61 = new ScriptObject(){
	tipo = "build";
	Area = bTodosOsSantos;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 61;
};

$infoTerra62 = new ScriptObject(){
	tipo = "build";
	Area = bDeHecate;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 62;
};

$infoTerra63 = new ScriptObject(){
	tipo = "build";
	Area = argentina;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 63;
};

$infoTerra64 = new ScriptObject(){
	tipo = "build";
	Area = comunidadeEuropeia;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 64;
};

$infoTerra65 = new ScriptObject(){
	tipo = "build";
	Area = mexico;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 65;
};

$infoTerra66 = new ScriptObject(){
	tipo = "build";
	Area = bolivia;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 66;
};

$infoTerra67 = new ScriptObject(){
	tipo = "build";
	Area = bMarDaSiberia;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 67;
};

$infoTerra68 = new ScriptObject(){
	tipo = "build";
	Area = venezuela;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 68;
};

$infoTerra69 = new ScriptObject(){
	tipo = "build";
	Area = bMarMediterraneo;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 69;
};

$infoTerra70 = new ScriptObject(){
	tipo = "build";
	Area = bGrandeBaiaAustraliana;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 70;
};

$infoTerra71 = new ScriptObject(){
	tipo = "build";
	Area = bMarDoJapao;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 71;
};

$infoTerra72 = new ScriptObject(){
	tipo = "build";
	Area = saoPaulo;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 72;
};

$infoTerra73 = new ScriptObject(){
	tipo = "build";
	Area = bDeMogadicio;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 73;
};

$infoTerra74 = new ScriptObject(){
	tipo = "build";
	Area = bagda;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 74;
};

$infoTerra75 = new ScriptObject(){
	tipo = "recurso";
	Area = saoPaulo;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 75;
};

//últimas adições, para equilibrar o Brasil:
$infoTerra76 = new ScriptObject(){
	tipo = "recurso";
	Area = bTodosOsSantos;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 76;
};
$infoTerra77 = new ScriptObject(){
	tipo = "recurso";
	Area = manaus;
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 77;
};
$infoTerra78 = new ScriptObject(){
	tipo = "recurso";
	Area = bolivia;
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 78;
};


///////////////////////////////////////////////
//PLANETA UNGART:
//////////////////////////////////////////////
//Minérios:
$infoUngart1 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_PlDo02; //Platô Dourado 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 1;
};
$infoUngart2 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_ChOr02; //Chifre Oriental
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 2;
};
$infoUngart3 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_PrEx01; //Praias Externas
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 3;
};
$infoUngart4 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_MoVe03; //Montes Verticais
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 4;
};
$infoUngart5 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_IlVu02; //Ilha Vulcânica
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 5;
};
$infoUngart6 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_CaNo01; //Cânion Nórdico
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 6;
};
$infoUngart7 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_VaNo02; //Vale Nórdico
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 7;
};
$infoUngart8 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_CaOr02; //Cânion Oriental
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 8;
};
$infoUngart9 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_VaOr01; //Vale Oriental
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 9;
};
$infoUngart10 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_VaGu02; //Vale dos Guloks
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 10;
};
$infoUngart11 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_PaGu03; //Pântano Gulok
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 11;
};
$infoUngart12 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_MoVe01; //Montes Verticais
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 12;
};
$infoUngart13 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_MoVe02; //Montes Verticais
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 13;
};
$infoUngart14 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_MoVe04; //Montes Verticais
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 14;
};
$infoUngart15 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_ChOc02; //Chifre Ocidental
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 15;
};
$infoUngart16 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_VaOr02; //Vale Oriental
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 16;
};
$infoUngart17 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_VaOr03; //Vale Oriental
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 17;
};
$infoUngart18 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_VaNo03; //Vale Nórdico
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 18;
};
$infoUngart19 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_VaNo04; //Vale Nórdico
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 19;
};

//////////////
//Urânios:
$infoUngart20 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_DeEx01; //Deserto Externo
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 20;
};
$infoUngart21 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_DeEx02; //Deserto Externo
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 21;
};
$infoUngart22 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_PlDo01; //Platô Dourado
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 22;
};
$infoUngart23 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_PaGu03; //Pântano Gulok
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 23;
};
$infoUngart24 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_CaNo02; //Cânion Nórdico
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 24;
};
$infoUngart25 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_CaOr04; //Cânion Oriental
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 25;
};

//////////////
//Petróleos:
$infoUngart26 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b01;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 26;
};
$infoUngart27 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b02;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 27;
};
$infoUngart28 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b03;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 28;
};
$infoUngart29 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b04;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 29;
};
$infoUngart30 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b27;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 30;
};
$infoUngart31 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b28;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 31;
};
$infoUngart32 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b29;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 32;
};
$infoUngart33 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b30;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 33;
};
$infoUngart34 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b32;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 34;
};
$infoUngart35 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b33;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 35;
};
$infoUngart36 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b34;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 36;
};
$infoUngart37 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b35;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 37;
};
$infoUngart38 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b13;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 38;
};
$infoUngart39 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b23;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 39;
};
$infoUngart40 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b24;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 40;
};

////////////////
//Pontos:
$infoUngart41 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b37;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 41;
};
$infoUngart42 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b27;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 42;
};
$infoUngart43 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b23;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 43;
};
$infoUngart44 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b28;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 44;
};
$infoUngart45 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b30;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 45;
};
$infoUngart46 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b34;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 46;
};
$infoUngart47 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b35;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 47;
};
$infoUngart48 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b39;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 48;
};
$infoUngart49 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b22;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 49;
};
$infoUngart50 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b13;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 50;
};
$infoUngart51 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b12;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 51;
};
$infoUngart52 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b02;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 52;
};
$infoUngart53 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b03;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 53;
};
$infoUngart54 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b01;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 54;
};
$infoUngart55 = new ScriptObject(){
	tipo = "build";
	Area = UNG_CaOr04; //Cânion Oriental
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 55;
};
$infoUngart56 = new ScriptObject(){
	tipo = "build";
	Area = UNG_VaOr03; //Vale Oriental
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 56;
};
$infoUngart57 = new ScriptObject(){
	tipo = "build";
	Area = UNG_VaGu02; //Vale Gulok
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 57;
};
$infoUngart58 = new ScriptObject(){
	tipo = "build";
	Area = UNG_PaGu02; //Pântano Gulok
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 58;
};
$infoUngart59 = new ScriptObject(){
	tipo = "build";
	Area = UNG_IlVu01; //Ilha Vulcânica
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 59;
};
$infoUngart60 = new ScriptObject(){
	tipo = "build";
	Area = UNG_MoVe02; //Montes Verticais
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 60;
};
$infoUngart61 = new ScriptObject(){
	tipo = "build";
	Area = UNG_CaNo03; //Cânion Nórdico
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 61;
};
$infoUngart62 = new ScriptObject(){
	tipo = "build";
	Area = UNG_VaNo02; //Vale Nórdico
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 62;
};
$infoUngart63 = new ScriptObject(){
	tipo = "build";
	Area = UNG_PlDo01; //Platô Dourado
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 63;
};
$infoUngart64 = new ScriptObject(){
	tipo = "build";
	Area = UNG_DeEx02; //Deserto Externo
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 64;
};
$infoUngart65 = new ScriptObject(){
	tipo = "build";
	Area = UNG_ChOr01; //Chifre Oriental
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 65;
};
$infoUngart66 = new ScriptObject(){
	tipo = "build";
	Area = UNG_ChOc01; //Chifre Ocidental
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 66;
};
$infoUngart67 = new ScriptObject(){
	tipo = "build";
	Area = UNG_PrEx02; //Praias Externas
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 67;
};
$infoUngart68 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b15;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 68;
};
$infoUngart69 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b16;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 69;
};
$infoUngart70 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b17;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 70;
};
$infoUngart71 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b18;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 71;
};
$infoUngart72 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b08;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 72;
};
$infoUngart73 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b19;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 73;
};
$infoUngart74 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b05;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 74;
};
$infoUngart75 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b06;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 75;
};
$infoUngart76 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b07;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 76;
};
$infoUngart77 = new ScriptObject(){
	tipo = "build";
	Area = UNG_DeEx01; //Deserto Externo
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 77;
};
$infoUngart78 = new ScriptObject(){
	tipo = "build";
	Area = UNG_PrEx01; //Praias Externas
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 78;
};
$infoUngart79 = new ScriptObject(){
	tipo = "build";
	Area = UNG_MoVe04; //Montes Verticais
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 79;
};
$infoUngart80 = new ScriptObject(){
	tipo = "build";
	Area = UNG_b38;
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 80;
};

//EXTRAS:

$infoUngart81 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b18;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 81;
};
$infoUngart82 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b19;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 82;
};
$infoUngart83 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_b06;
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 83;
};
$infoUngart84 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_PaGu02; //Pântano Gulok
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 84;
};
$infoUngart85 = new ScriptObject(){
	tipo = "recurso";
	Area = UNG_PaGu01; //Pântano Gulok
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 85;
};


////////////
//Telúria:
$infoTeluria1 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_zavinia01; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 1;
};
$infoTeluria2 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_zavinia02; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 2;
};
$infoTeluria3 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_dharin01; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 3;
};
$infoTeluria4 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_dharin02; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 4;
};
$infoTeluria5 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_dharin03; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 5;
};
$infoTeluria6 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_lornia01; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 6;
};
$infoTeluria7 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_lornia02; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 7;
};
$infoTeluria8 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_keltur01; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 8;
};
$infoTeluria9 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_keltur02; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 9;
};
$infoTeluria10 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_keltur03; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 10;
};
$infoTeluria11 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_valinur01; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 11;
};
$infoTeluria12 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_valinur02; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 12;
};
$infoTeluria13 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_argonia01; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 13;
};
$infoTeluria14 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_argonia02; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 14;
};
$infoTeluria15 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_argonia03; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 15;
};
$infoTeluria16 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_argonia04; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 16;
};
$infoTeluria17 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_vuldan01; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 17;
};
$infoTeluria18 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_vuldan02; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 18;
};
$infoTeluria19 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_vuldan03; 
	bonusM = 1;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 19;
};

$infoTeluria20 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_goruk01; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 20;
};
$infoTeluria21 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_goruk02; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 21;
};

$infoTeluria22 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b09; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 22;
};
$infoTeluria23 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b16; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 23;
};
$infoTeluria24 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b21; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 24;
};
$infoTeluria25 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b05; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 25;
};
$infoTeluria26 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b26; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 26;
};
$infoTeluria27 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b08; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 27;
};
$infoTeluria28 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b07; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 28;
};
$infoTeluria29 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b30; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 29;
};
$infoTeluria30 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b28; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 30;
};
$infoTeluria31 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b19; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 31;
};
$infoTeluria32 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b12; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 32;
};
$infoTeluria33 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b13; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 33;
};
$infoTeluria34 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b34; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 34;
};
$infoTeluria35 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b33; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 35;
};
$infoTeluria36 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b32; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 36;
};
$infoTeluria37 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b10; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 37;
};
$infoTeluria38 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_b11; 
	bonusM = 0;
	bonusP = 1;
	bonusU = 0;
	bonusPt = 0;
	ativaFlag = false;
	num = 38;
};

$infoTeluria39 = new ScriptObject(){
	tipo = "build";
	Area = TEL_terion02; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 39;
};
$infoTeluria40 = new ScriptObject(){
	tipo = "build";
	Area = TEL_nir02; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 40;
};
$infoTeluria41 = new ScriptObject(){
	tipo = "build";
	Area = TEL_karzin01; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 41;
};
$infoTeluria42 = new ScriptObject(){
	tipo = "build";
	Area = TEL_goruk02; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 42;
};
$infoTeluria43 = new ScriptObject(){
	tipo = "build";
	Area = TEL_malik02; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 43;
};
$infoTeluria44 = new ScriptObject(){
	tipo = "build";
	Area = TEL_lornia02; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 44;
};
$infoTeluria45 = new ScriptObject(){
	tipo = "build";
	Area = TEL_keltur01; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 45;
};
$infoTeluria46 = new ScriptObject(){
	tipo = "build";
	Area = TEL_vuldan02; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 46;
};
$infoTeluria47 = new ScriptObject(){
	tipo = "build";
	Area = TEL_valinur01; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 47;
};
$infoTeluria48 = new ScriptObject(){
	tipo = "build";
	Area = TEL_argonia03; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 48;
};
$infoTeluria49 = new ScriptObject(){
	tipo = "build";
	Area = TEL_zavinia02; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 49;
};
$infoTeluria50 = new ScriptObject(){
	tipo = "build";
	Area = TEL_dharin02; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 50;
};
$infoTeluria51 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b24; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 51;
};
$infoTeluria52 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b04; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 52;
};
$infoTeluria53 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b14; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 53;
};
$infoTeluria54 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b34; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 54;
};
$infoTeluria55 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b33; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 55;
};
$infoTeluria56 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b32; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 56;
};
$infoTeluria57 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b10; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 57;
};
$infoTeluria58 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b11; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 58;
};
$infoTeluria59 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b18; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 59;
};
$infoTeluria60 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b28; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 60;
};
$infoTeluria61 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b17; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 61;
};
$infoTeluria62 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b19; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 62;
};
$infoTeluria63 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b26; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 63;
};
$infoTeluria64 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b05; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 64;
};
$infoTeluria65 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b07; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 65;
};
$infoTeluria66 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b12; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 66;
};
$infoTeluria67 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b13; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 67;
};
$infoTeluria68 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b30; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 68;
};
$infoTeluria69 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b31; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 69;
};
$infoTeluria70 = new ScriptObject(){
	tipo = "build";
	Area = TEL_b08; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 0;
	bonusPt = 2;
	ativaFlag = false;
	num = 70;
};
$infoTeluria71 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_nexus01; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 71;
};
$infoTeluria72 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_canhao01; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 72;
};
$infoTeluria73 = new ScriptObject(){
	tipo = "recurso";
	Area = TEL_karzin02; 
	bonusM = 0;
	bonusP = 0;
	bonusU = 1;
	bonusPt = 0;
	ativaFlag = false;
	num = 73;
};










function toggleIntelBases(%terreno){
	if($myIntelMarkersBasesOn){
		for (%i = 1; %i < 79; %i++){
			%info = clientFindInfo(%i);
			if (%info.bonusPt > 0){
				%eval = "%mission = markerBase" @ %i @ ";";
				eval(%eval);
				
				if(isObject(%mission)){
					%mission.safeDelete();
				}
			}
		}
		$myIntelMarkersBasesOn = false;
	} else {
		for (%i = 1; %i < 79; %i++){
			%info = clientFindInfo(%i);
			if (%info.bonusPt > 0){
				%eval = "%areaDaMissao =" SPC %info.area @ ";";
				eval(%eval);	
				
				if(%areaDaMissao.terreno $= %terreno){
					%newAlvo = missaoMarkerBase.clone();	
					%newAlvo.setPosition(%areaDaMissao.pos0);
					//%newAlvo.setAutoRotation(-15);
					%name = "markerBase" @ %i;
					%newAlvo.setName(%name);
				}
			}
		}
		$myIntelMarkersBasesOn = true;		
	}
}

