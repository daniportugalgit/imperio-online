// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\objetivos.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 30 de julho de 2007 9:00
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

//tornar as cartas de objetivos visíveis (para quando não for o primeiro jogo)
for (%i = 1; %i < 27; %i++){
	%eval = "obj"@%i@"_carta.setVisible(true);";
	eval(%eval);
}

//função para encontrar a info de qualquer planeta:
function clientFindObjetivo(%objNum){
	%eval = "%obj = $objetivo" @ $myPersona.planetaAtual @ %objNum @ ";";
	eval(%eval);
	return %obj;
}

//////////////////////
//PLANETA TERRA:
//Oriente:
$objetivoTerra1 = new ScriptObject(){
	num = 1;
	grupo = oriente;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 12;
	territorios = 0;
	desc1 = "Oriente";
	desc2 = "Mar"; 
};
$objetivoTerra2 = new ScriptObject(){
	num = 2;
	grupo = oriente;
	minerios = 0;
	petroleos = 0;
	uranios = 8;
	baias = 0;
	territorios = 0;
	desc1 = "Oriente";
	desc2 = "Urânio"; 
};
$objetivoTerra3 = new ScriptObject(){
	num = 3;
	grupo = oriente;
	minerios = 10;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Oriente";
	desc2 = "Minério"; 
};

//Austrália:
$objetivoTerra4 = new ScriptObject(){
	num = 4;
	grupo = australia;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 12;
	territorios = 0;
	desc1 = "Austrália";
	desc2 = "Mar"; 
};
$objetivoTerra5 = new ScriptObject(){
	num = 5;
	grupo = australia;
	minerios = 0;
	petroleos = 8;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Austrália";
	desc2 = "Petróleo"; 
};

//Brasil:
$objetivoTerra6 = new ScriptObject(){
	num = 6;
	grupo = brasil;
	minerios = 0;
	petroleos = 8;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Brasil";
	desc2 = "Petróleo"; 
};
$objetivoTerra7 = new ScriptObject(){
	num = 7;
	grupo = brasil;
	minerios = 0;
	petroleos = 0;
	uranios = 8;
	baias = 0;
	territorios = 0;
	desc1 = "Brasil";
	desc2 = "Urânio"; 
};
$objetivoTerra8 = new ScriptObject(){
	num = 8;
	grupo = brasil;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 12;
	territorios = 0;
	desc1 = "Brasil";
	desc2 = "Mar"; 
};

//EUA:
$objetivoTerra9 = new ScriptObject(){
	num = 9;
	grupo = eua;
	minerios = 0;
	petroleos = 8;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "EUA";
	desc2 = "Petróleo"; 
};
$objetivoTerra10 = new ScriptObject(){
	num = 10;
	grupo = eua;
	minerios = 0;
	petroleos = 0;
	uranios = 8;
	baias = 0;
	territorios = 0;
	desc1 = "EUA";
	desc2 = "Urânio"; 
};
$objetivoTerra11 = new ScriptObject(){
	num = 11;
	grupo = eua;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 12;
	territorios = 0;
	desc1 = "EUA";
	desc2 = "Mar"; 
};

//Europa:
$objetivoTerra12 = new ScriptObject(){
	num = 12;
	grupo = europa;
	minerios = 0;
	petroleos = 8;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Europa";
	desc2 = "Petróleo"; 
};
$objetivoTerra13 = new ScriptObject(){
	num = 13;
	grupo = europa;
	minerios = 0;
	petroleos = 0;
	uranios = 8;
	baias = 0;
	territorios = 0;
	desc1 = "Europa";
	desc2 = "Urânio"; 
};
$objetivoTerra14 = new ScriptObject(){
	num = 14;
	grupo = europa;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 12;
	territorios = 0;
	desc1 = "Europa";
	desc2 = "Mar"; 
};

//China:
$objetivoTerra15 = new ScriptObject(){
	num = 15;
	grupo = china;
	minerios = 0;
	petroleos = 8;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "China";
	desc2 = "Petróleo"; 
};
$objetivoTerra16 = new ScriptObject(){
	num = 16;
	grupo = china;
	minerios = 0;
	petroleos = 0;
	uranios = 8;
	baias = 0;
	territorios = 0;
	desc1 = "China";
	desc2 = "Urânio"; 
};
$objetivoTerra17 = new ScriptObject(){
	num = 17;
	grupo = china;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 12;
	territorios = 0;
	desc1 = "China";
	desc2 = "Mar"; 
};

//Rússia:
$objetivoTerra18 = new ScriptObject(){
	num = 18;
	grupo = russia;
	minerios = 0;
	petroleos = 8;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Rússia";
	desc2 = "Petróleo"; 
};
$objetivoTerra19 = new ScriptObject(){
	num = 19;
	grupo = russia;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 12;
	territorios = 0;
	desc1 = "Rússia";
	desc2 = "Mar"; 
};

//África:
$objetivoTerra20 = new ScriptObject(){
	num = 20;
	grupo = africa;
	minerios = 0;
	petroleos = 8;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "África";
	desc2 = "Petróleo"; 
};
$objetivoTerra21 = new ScriptObject(){
	num = 21;
	grupo = africa;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 12;
	territorios = 0;
	desc1 = "África";
	desc2 = "Mar"; 
};

//Canadá:
$objetivoTerra22 = new ScriptObject(){
	num = 22;
	grupo = canada;
	minerios = 0;
	petroleos = 8;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Canadá";
	desc2 = "Petróleo"; 
};
$objetivoTerra23 = new ScriptObject(){
	num = 23;
	grupo = canada;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 12;
	territorios = 0;
	desc1 = "Canadá";
	desc2 = "Mar"; 
};

//Especiais:
$objetivoTerra24 = new ScriptObject(){
	num = 24;
	grupo = 0;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Mar";
	desc2 = ""; 
};
$objetivoTerra25 = new ScriptObject(){
	num = 25;
	grupo = 0;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 18;
	desc1 = "Terra";
	desc2 = ""; 
};
$objetivoTerra26 = new ScriptObject(){
	num = 26;
	grupo = 0;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Petróleo";
	desc2 = ""; 
};





//////////////////////
//PLANETA UNGART:
//PrEx:
$objetivoUngart1 = new ScriptObject(){
	num = 1;
	grupo = PrEx;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Praias";
	desc2 = "Mar"; 
};

$objetivoUngart2 = new ScriptObject(){
	num = 2;
	grupo = PrEx;
	minerios = 0;
	petroleos = 0;
	uranios = 10;
	baias = 0;
	territorios = 0;
	desc1 = "Praias";
	desc2 = "Urânio"; 
};

$objetivoUngart3 = new ScriptObject(){
	num = 3;
	grupo = PrEx;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Praias";
	desc2 = "Petróleo"; 
};

//ChOc:
$objetivoUngart4 = new ScriptObject(){
	num = 4;
	grupo = ChOc;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Ch.Ocidental";
	desc2 = "Mar"; 
};

$objetivoUngart5 = new ScriptObject(){
	num = 5;
	grupo = ChOc;
	minerios = 0;
	petroleos = 0;
	uranios = 10;
	baias = 0;
	territorios = 0;
	desc1 = "Ch.Ocidental";
	desc2 = "Urânio"; 
};

$objetivoUngart6 = new ScriptObject(){
	num = 6;
	grupo = ChOc;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Ch.Ocidental";
	desc2 = "Petróleo"; 
};

//ChOr:
$objetivoUngart7 = new ScriptObject(){
	num = 7;
	grupo = ChOr;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Ch.Oriental";
	desc2 = "Mar"; 
};

$objetivoUngart8 = new ScriptObject(){
	num = 8;
	grupo = ChOr;
	minerios = 0;
	petroleos = 0;
	uranios = 10;
	baias = 0;
	territorios = 0;
	desc1 = "Ch.Oriental";
	desc2 = "Urânio"; 
};

$objetivoUngart9 = new ScriptObject(){
	num = 9;
	grupo = ChOr;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Ch.Oriental";
	desc2 = "Petróleo"; 
};

//PlDo:
$objetivoUngart10 = new ScriptObject(){
	num = 10;
	grupo = PlDo;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Platô";
	desc2 = "Mar"; 
};

$objetivoUngart11 = new ScriptObject(){
	num = 11;
	grupo = PlDo;
	minerios = 0;
	petroleos = 0;
	uranios = 10;
	baias = 0;
	territorios = 0;
	desc1 = "Platô";
	desc2 = "Urânio"; 
};

$objetivoUngart12 = new ScriptObject(){
	num = 12;
	grupo = PlDo;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Platô";
	desc2 = "Petróleo"; 
};

//DeEx:
$objetivoUngart13 = new ScriptObject(){
	num = 13;
	grupo = DeEx;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Deserto";
	desc2 = "Mar"; 
};

$objetivoUngart14 = new ScriptObject(){
	num = 14;
	grupo = DeEx;
	minerios = 0;
	petroleos = 0;
	uranios = 10;
	baias = 0;
	territorios = 0;
	desc1 = "Deserto";
	desc2 = "Urânio"; 
};

$objetivoUngart15 = new ScriptObject(){
	num = 15;
	grupo = DeEx;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Deserto";
	desc2 = "Petróleo"; 
};

//CaOr:
$objetivoUngart16 = new ScriptObject(){
	num = 16;
	grupo = CaOr;
	minerios = 15;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Cn.Oriental";
	desc2 = "Minério"; 
};

$objetivoUngart17 = new ScriptObject(){
	num = 17;
	grupo = CaOr;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Cn.Oriental";
	desc2 = "Mar"; 
};

$objetivoUngart18 = new ScriptObject(){
	num = 18;
	grupo = CaOr;
	minerios = 0;
	petroleos = 0;
	uranios = 10;
	baias = 0;
	territorios = 0;
	desc1 = "Cn.Oriental";
	desc2 = "Urânio"; 
};

//CaNo:
$objetivoUngart19 = new ScriptObject(){
	num = 19;
	grupo = CaNo;
	minerios = 15;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Cn.Nórdico";
	desc2 = "Minério"; 
};

$objetivoUngart20 = new ScriptObject(){
	num = 20;
	grupo = CaNo;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Cn.Nórdico";
	desc2 = "Mar"; 
};

$objetivoUngart21 = new ScriptObject(){
	num = 21;
	grupo = CaNo;
	minerios = 0;
	petroleos = 0;
	uranios = 10;
	baias = 0;
	territorios = 0;
	desc1 = "Cn.Nórdico";
	desc2 = "Urânio"; 
};

//PaGu:
$objetivoUngart22 = new ScriptObject(){
	num = 22;
	grupo = PaGu;
	minerios = 15;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Pântano";
	desc2 = "Minério"; 
};

$objetivoUngart23 = new ScriptObject(){
	num = 23;
	grupo = PaGu;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Pântano";
	desc2 = "Mar"; 
};

$objetivoUngart24 = new ScriptObject(){
	num = 24;
	grupo = PaGu;
	minerios = 0;
	petroleos = 0;
	uranios = 10;
	baias = 0;
	territorios = 0;
	desc1 = "Pântano";
	desc2 = "Urânio"; 
};

//VaGu:
$objetivoUngart25 = new ScriptObject(){
	num = 25;
	grupo = VaGu;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "V.Gulok";
	desc2 = "Petróleo"; 
};

$objetivoUngart26 = new ScriptObject(){
	num = 26;
	grupo = VaGu;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "V.Gulok";
	desc2 = "Mar"; 
};

//VaOr:
$objetivoUngart27 = new ScriptObject(){
	num = 27;
	grupo = VaOr;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "V.Oriental";
	desc2 = "Petróleo"; 
};

$objetivoUngart28 = new ScriptObject(){
	num = 28;
	grupo = VaOr;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "V.Oriental";
	desc2 = "Mar"; 
};

//VaNo:
$objetivoUngart29 = new ScriptObject(){
	num = 29;
	grupo = VaNo;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "V.Nórdico";
	desc2 = "Petróleo"; 
};

$objetivoUngart30 = new ScriptObject(){
	num = 30;
	grupo = VaNo;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "V.Nórdico";
	desc2 = "Mar"; 
};

//MoVe:
$objetivoUngart31 = new ScriptObject(){
	num = 31;
	grupo = MoVe;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "M.Verticais";
	desc2 = "Petróleo"; 
};

$objetivoUngart32 = new ScriptObject(){
	num = 32;
	grupo = MoVe;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "M.Verticais";
	desc2 = "Mar"; 
};

//IlVu:
$objetivoUngart33 = new ScriptObject(){
	num = 33;
	grupo = IlVu;
	minerios = 0;
	petroleos = 10;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "I.Vulcânica";
	desc2 = "Petróleo"; 
};

$objetivoUngart34 = new ScriptObject(){
	num = 34;
	grupo = IlVu;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "I.Vulcânica";
	desc2 = "Mar"; 
};

$objetivoUngart35 = new ScriptObject(){
	num = 35;
	grupo = 0;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 18;
	territorios = 0;
	desc1 = "Mar";
	desc2 = ""; 
};

/*
$objetivoUngart36 = new ScriptObject(){
	num = 36;
	grupo = 0;
	minerios = 18;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Minério";
	desc2 = ""; 
};
*/

$objetivoUngart36 = new ScriptObject(){
	num = 36;
	grupo = 0;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Petróleo";
	desc2 = ""; 
};

$objetivoUngart37 = new ScriptObject(){
	num = 37;
	grupo = 0;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 15;
	desc1 = "Terra";
	desc2 = ""; 
};


//////////////////////
//PLANETA TELÚRIA:
//Térion
$objetivoTeluria1 = new ScriptObject(){
	num = 1;
	grupo = Terion;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Térion";
	desc2 = "Mar"; 
};
$objetivoTeluria2 = new ScriptObject(){
	num = 2;
	grupo = Terion;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Térion";
	desc2 = "Petróleo"; 
};
$objetivoTeluria3 = new ScriptObject(){
	num = 3;
	grupo = Terion;
	minerios = 0;
	petroleos = 0;
	uranios = 12;
	baias = 0;
	territorios = 0;
	desc1 = "Térion";
	desc2 = "Urânio"; 
};

//Nir
$objetivoTeluria4 = new ScriptObject(){
	num = 4;
	grupo = Nir;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Nir";
	desc2 = "Mar"; 
};
$objetivoTeluria5 = new ScriptObject(){
	num = 5;
	grupo = Nir;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Nir";
	desc2 = "Petróleo"; 
};
$objetivoTeluria6 = new ScriptObject(){
	num = 6;
	grupo = Nir;
	minerios = 0;
	petroleos = 0;
	uranios = 12;
	baias = 0;
	territorios = 0;
	desc1 = "Nir";
	desc2 = "Urânio"; 
};

//Karzin
$objetivoTeluria7 = new ScriptObject(){
	num = 7;
	grupo = Karzin;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Karzin";
	desc2 = "Mar"; 
};
$objetivoTeluria8 = new ScriptObject(){
	num = 8;
	grupo = Karzin;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Karzin";
	desc2 = "Petróleo"; 
};
$objetivoTeluria9 = new ScriptObject(){
	num = 9;
	grupo = Karzin;
	minerios = 0;
	petroleos = 0;
	uranios = 12;
	baias = 0;
	territorios = 0;
	desc1 = "Karzin";
	desc2 = "Urânio"; 
};

//Goruk
$objetivoTeluria10 = new ScriptObject(){
	num = 10;
	grupo = Goruk;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Goruk";
	desc2 = "Mar"; 
};
$objetivoTeluria11 = new ScriptObject(){
	num = 11;
	grupo = Goruk;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Goruk";
	desc2 = "Petróleo"; 
};
$objetivoTeluria12 = new ScriptObject(){
	num = 12;
	grupo = Goruk;
	minerios = 0;
	petroleos = 0;
	uranios = 12;
	baias = 0;
	territorios = 0;
	desc1 = "Goruk";
	desc2 = "Urânio"; 
};

//Malik
$objetivoTeluria13 = new ScriptObject(){
	num = 13;
	grupo = Malik;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Malik";
	desc2 = "Mar"; 
};
$objetivoTeluria14 = new ScriptObject(){
	num = 14;
	grupo = Malik;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Malik";
	desc2 = "Petróleo"; 
};
$objetivoTeluria15 = new ScriptObject(){
	num = 15;
	grupo = Malik;
	minerios = 0;
	petroleos = 0;
	uranios = 12;
	baias = 0;
	territorios = 0;
	desc1 = "Malik";
	desc2 = "Urânio"; 
};

//Zavinia
$objetivoTeluria16 = new ScriptObject(){
	num = 16;
	grupo = Zavinia;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Zavínia";
	desc2 = "Mar"; 
};
$objetivoTeluria17 = new ScriptObject(){
	num = 17;
	grupo = Zavinia;
	minerios = 20;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Zavínia";
	desc2 = "Minério"; 
};
$objetivoTeluria18 = new ScriptObject(){
	num = 18;
	grupo = Zavinia;
	minerios = 0;
	petroleos = 0;
	uranios = 12;
	baias = 0;
	territorios = 0;
	desc1 = "Zavínia";
	desc2 = "Urânio"; 
};

//Argonia
$objetivoTeluria19 = new ScriptObject(){
	num = 19;
	grupo = Argonia;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Argônia";
	desc2 = "Mar"; 
};
$objetivoTeluria20 = new ScriptObject(){
	num = 20;
	grupo = Argonia;
	minerios = 20;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Argônia";
	desc2 = "Minério"; 
};
$objetivoTeluria21 = new ScriptObject(){
	num = 21;
	grupo = Argonia;
	minerios = 0;
	petroleos = 0;
	uranios = 12;
	baias = 0;
	territorios = 0;
	desc1 = "Argônia";
	desc2 = "Urânio"; 
};

//Lornia
$objetivoTeluria22 = new ScriptObject(){
	num = 22;
	grupo = Lornia;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Lórnia";
	desc2 = "Mar"; 
};
$objetivoTeluria23 = new ScriptObject(){
	num = 23;
	grupo = Lornia;
	minerios = 20;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Lórnia";
	desc2 = "Minério"; 
};
$objetivoTeluria24 = new ScriptObject(){
	num = 24;
	grupo = Lornia;
	minerios = 0;
	petroleos = 0;
	uranios = 12;
	baias = 0;
	territorios = 0;
	desc1 = "Lórnia";
	desc2 = "Urânio"; 
};

//Dharin
$objetivoTeluria25 = new ScriptObject(){
	num = 25;
	grupo = Dharin;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Dharin";
	desc2 = "Mar"; 
};
$objetivoTeluria26 = new ScriptObject(){
	num = 26;
	grupo = Dharin;
	minerios = 20;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Dharin";
	desc2 = "Minério"; 
};
$objetivoTeluria27 = new ScriptObject(){
	num = 27;
	grupo = Dharin;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Dharin";
	desc2 = "Petróleo"; 
};

//Valinur
$objetivoTeluria28 = new ScriptObject(){
	num = 28;
	grupo = Valinur;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Valinur";
	desc2 = "Mar"; 
};
$objetivoTeluria29 = new ScriptObject(){
	num = 29;
	grupo = Valinur;
	minerios = 20;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Valinur";
	desc2 = "Minério"; 
};
$objetivoTeluria30 = new ScriptObject(){
	num = 30;
	grupo = Valinur;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Valinur";
	desc2 = "Petróleo"; 
};

//Vuldan
$objetivoTeluria31 = new ScriptObject(){
	num = 31;
	grupo = Vuldan;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Vuldan";
	desc2 = "Mar"; 
};
$objetivoTeluria32 = new ScriptObject(){
	num = 32;
	grupo = Vuldan;
	minerios = 20;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Vuldan";
	desc2 = "Minério"; 
};
$objetivoTeluria33 = new ScriptObject(){
	num = 33;
	grupo = Vuldan;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Vuldan";
	desc2 = "Petróleo"; 
};

//Keltur
$objetivoTeluria34 = new ScriptObject(){
	num = 34;
	grupo = Keltur;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Keltur";
	desc2 = "Mar"; 
};
$objetivoTeluria35 = new ScriptObject(){
	num = 35;
	grupo = Keltur;
	minerios = 20;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Keltur";
	desc2 = "Minério"; 
};
$objetivoTeluria36 = new ScriptObject(){
	num = 36;
	grupo = Keltur;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Keltur";
	desc2 = "Petróleo"; 
};

//Nexus
$objetivoTeluria37 = new ScriptObject(){
	num = 37;
	grupo = Nexus;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Nexus";
	desc2 = "Mar"; 
};
$objetivoTeluria38 = new ScriptObject(){
	num = 38;
	grupo = Nexus;
	minerios = 20;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Nexus";
	desc2 = "Minério"; 
};
$objetivoTeluria39 = new ScriptObject(){
	num = 39;
	grupo = Nexus;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Nexus";
	desc2 = "Petróleo"; 
};
$objetivoTeluria40 = new ScriptObject(){
	num = 40;
	grupo = Nexus;
	minerios = 0;
	petroleos = 0;
	uranios = 12;
	baias = 0;
	territorios = 0;
	desc1 = "Nexus";
	desc2 = "Urânio"; 
};

//Canhao
$objetivoTeluria41 = new ScriptObject(){
	num = 41;
	grupo = GeoCanhao;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 15;
	territorios = 0;
	desc1 = "Geo-Canhão";
	desc2 = "Mar"; 
};
$objetivoTeluria42 = new ScriptObject(){
	num = 42;
	grupo = GeoCanhao;
	minerios = 20;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Geo-Canhão";
	desc2 = "Minério"; 
};
$objetivoTeluria43 = new ScriptObject(){
	num = 43;
	grupo = GeoCanhao;
	minerios = 0;
	petroleos = 12;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Geo-Canhão";
	desc2 = "Petróleo"; 
};
$objetivoTeluria44 = new ScriptObject(){
	num = 44;
	grupo = GeoCanhao;
	minerios = 0;
	petroleos = 0;
	uranios = 12;
	baias = 0;
	territorios = 0;
	desc1 = "Geo-Canhão";
	desc2 = "Urânio"; 
};
$objetivoTeluria45 = new ScriptObject(){
	num = 45;
	grupo = 0;
	minerios = 0;
	petroleos = 15;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Petróleo";
	desc2 = ""; 
};


///////////////////////////
//Objetivo de Matar a Grande Matriarca:
$obj_guloks = new ScriptObject(){
	num = 99;
	grupo = 0;
	minerios = 0;
	petroleos = 0;
	uranios = 0;
	baias = 0;
	territorios = 0;
	desc1 = "Grande";
	desc2 = "Matriarca"; 
};