
////////////////////////////////////////
//////////////////////////////////////
$areasDeTerra = new SimSet(areasDaTerra);
$areasDeTerra.clear(); //zera

$areasDeUngart = new SimSet(areasDeUngart);
$areasDeUngart.clear(); //zera

$areasDeTeluria = new SimSet(areasDeUngart);
$areasDeTeluria.clear(); //zera
////////////////////////

//esta função é usada pelos clients, já que o server só precisa dos dados setados na função createCopy() da Área:
function Area::onLevelLoaded(%this, %scenegraph){
   	if(!sceneWindow2D.getUseObjectMouseEvents()){
		sceneWindow2D.setUseObjectMouseEvents(true); // ter a certeza de que a cena aceita mouseEvents, não importa quantas vezes ela tenha sido refeita.
   	}
   	   	
   	%this.setUseMouseEvents(true); // setar a Área para aceitar mouseEvents
	%this.dono = 0; // Começa pertencendo a ninguém
	%this.desprotegida = false; //não está desprotegida porque nem tem Base ainda
	%this.pos0Quem = "nada"; //não há ninguém nas posições fortes
	%this.pos1Quem = "nada";
	%this.pos2Quem = "nada";
	%this.pos0Flag = false; //true ou false mesmo, significando que tem ou não tem base lá.
	%this.pos1Flag = "nada"; //as positionFlags não guardam 0 ou 1, mas sim [a classe da peça] ou ["nada"];
	%this.pos2Flag = "nada";
	%this.myName = %this.getName(); //seta a variável myName pra ser usada como getName nas cópias das áreas de cada jogo;
		
	//SimSets das posições reservas:
	%this.myPos3List = new SimSet();
	if(%this.terreno $= "terra"){
		%this.myPos4List = new SimSet();
	}
	
	%eval = "$areasDe" @ %this.planeta @ ".add(%this);";
	eval(%eval);
	if(!$IAmServer){
		%this.clientSetMyFronteiras();	
	}
}




////////


//criar aki embaixo o initFronteiras, que cria um simset pra cada área e coloca suas fronteiras lá. 
//depois a função do RightClick verifica se a área clicada é fronteiriça antes de chamar a função
//de movimentação. 

//Esta função é usada pelo client, o server nem precisa dela, pois registra as fronteiras diretamente nas áreas de cada jogo:
function Area::clientSetMyFronteiras(%this){
	%this.myFronteiras = new SimSet();	
	
	switch$(%this.myName){
		case "saoPaulo":
		%this.myFronteiras.add(argentina);
		%this.myFronteiras.add(bolivia);
		%this.myFronteiras.add(manaus);
		%this.myFronteiras.add(bTodosOsSantos);
		%this.myFronteiras.add(bGuanabara);
		
		case "manaus":
		%this.myFronteiras.add(saoPaulo);
		%this.myFronteiras.add(bolivia);
		%this.myFronteiras.add(venezuela);
		%this.myFronteiras.add(bMarDoCaribe);
		%this.myFronteiras.add(bTodosOsSantos);
		
		case "argentina":
		%this.myFronteiras.add(saoPaulo);
		%this.myFronteiras.add(bolivia);
		%this.myFronteiras.add(bEstreitoDeMagalhaes);
		%this.myFronteiras.add(bGuanabara);
		
		case "bolivia":
		%this.myFronteiras.add(saoPaulo);
		%this.myFronteiras.add(argentina);
		%this.myFronteiras.add(venezuela);
		%this.myFronteiras.add(manaus);
		%this.myFronteiras.add(bEstreitoDeMagalhaes);
		%this.myFronteiras.add(bDeLima);
		%this.myFronteiras.add(bGolfoDoPanama);
		
		case "venezuela":
		%this.myFronteiras.add(manaus);
		%this.myFronteiras.add(bolivia);
		%this.myFronteiras.add(mexico);
		%this.myFronteiras.add(bMarDoCaribe);
		%this.myFronteiras.add(bGolfoDoPanama);

		case "mexico":
		%this.myFronteiras.add(venezuela);
		%this.myFronteiras.add(losAngeles);
		%this.myFronteiras.add(houston);
		%this.myFronteiras.add(bGolfoDoMexico);
		%this.myFronteiras.add(bMarDoCaribe);
		%this.myFronteiras.add(bGolfoDoPanama);
		%this.myFronteiras.add(bGolfoDaCalifornia);
		
		case "losAngeles":
		%this.myFronteiras.add(mexico);
		%this.myFronteiras.add(houston);
		%this.myFronteiras.add(novaYork);
		%this.myFronteiras.add(toronto);
		%this.myFronteiras.add(vancouver);
		%this.myFronteiras.add(bGolfoDaCalifornia);
		%this.myFronteiras.add(bDeHecate);
		
		case "houston":
		%this.myFronteiras.add(mexico);
		%this.myFronteiras.add(losAngeles);
		%this.myFronteiras.add(novaYork);
		%this.myFronteiras.add(bGolfoDoMexico);

		case "novaYork":
		%this.myFronteiras.add(houston);
		%this.myFronteiras.add(losAngeles);
		%this.myFronteiras.add(toronto);
		%this.myFronteiras.add(montreal);
		%this.myFronteiras.add(bGolfoDoMaine);
		%this.myFronteiras.add(bGolfoDoMexico);

		case "vancouver":
		%this.myFronteiras.add(losAngeles);
		%this.myFronteiras.add(toronto);
		%this.myFronteiras.add(bakerlake);
		%this.myFronteiras.add(bDeHecate);
		
		case "toronto":
		%this.myFronteiras.add(novaYork);
		%this.myFronteiras.add(losAngeles);
		%this.myFronteiras.add(vancouver);
		%this.myFronteiras.add(bakerlake);
		%this.myFronteiras.add(montreal);
		%this.myFronteiras.add(bDeHudson);
		
		case "montreal":
		%this.myFronteiras.add(novaYork);
		%this.myFronteiras.add(toronto);
		%this.myFronteiras.add(bDeHudson);
		%this.myFronteiras.add(bGolfoDoMaine);
		
		case "bakerlake":
		%this.myFronteiras.add(toronto);
		%this.myFronteiras.add(vancouver);
		%this.myFronteiras.add(alaska);
		%this.myFronteiras.add(bDeHudson);
		%this.myFronteiras.add(bDeBaffin);
		%this.myFronteiras.add(bDeHecate);
		
		case "alaska":
		%this.myFronteiras.add(bakerlake);
		%this.myFronteiras.add(bDeHecate);
		%this.myFronteiras.add(bEstreitoDeBering);
		%this.myFronteiras.add(bDeBaffin);
		
		case "groenlandia":
		%this.myFronteiras.add(bEstreitoDaDinamarca);
		%this.myFronteiras.add(bMarDaGroenlandia);
		%this.myFronteiras.add(bDeBaffin);
		%this.myFronteiras.add(bDeHudson);
		
		case "londres":
		%this.myFronteiras.add(bMarNordico);
		
		case "estocolmo":
		%this.myFronteiras.add(moscou);
		%this.myFronteiras.add(bMarDaNoruega);
		
		case "comunidadeEuropeia":
		%this.myFronteiras.add(moscou);
		%this.myFronteiras.add(bagda);
		%this.myFronteiras.add(bMarMediterraneo);
		%this.myFronteiras.add(bMarNordico);
		%this.myFronteiras.add(bMarDaNoruega);
		
		case "marrakesh":
		%this.myFronteiras.add(cairo);
		%this.myFronteiras.add(kinshasa);
		%this.myFronteiras.add(bDeLuanda);
		%this.myFronteiras.add(bDeDakar);
		%this.myFronteiras.add(bMarMediterraneo);
		
		case "cairo":
		%this.myFronteiras.add(kinshasa);
		%this.myFronteiras.add(marrakesh);
		%this.myFronteiras.add(bagda);
		%this.myFronteiras.add(bMarDaArabia);
		%this.myFronteiras.add(bDeMogadicio);
		%this.myFronteiras.add(bMarMediterraneo);
		
		case "kinshasa":
		%this.myFronteiras.add(cidadeDoCabo);
		%this.myFronteiras.add(marrakesh);
		%this.myFronteiras.add(cairo);
		%this.myFronteiras.add(bDeMogadicio);
		%this.myFronteiras.add(bDeLuanda);
		
		case "cidadeDoCabo":
		%this.myFronteiras.add(kinshasa);
		%this.myFronteiras.add(bDeMogadicio);
		%this.myFronteiras.add(bCaboDaBoaEsperanca);
		%this.myFronteiras.add(bDeLuanda);
		
		case "bagda":
		%this.myFronteiras.add(cairo);
		%this.myFronteiras.add(comunidadeEuropeia);
		%this.myFronteiras.add(moscou);
		%this.myFronteiras.add(cabul);
		%this.myFronteiras.add(bMarDaArabia);
		%this.myFronteiras.add(bMarMediterraneo);
		
		case "cabul":
		%this.myFronteiras.add(bagda);
		%this.myFronteiras.add(cazaquistao);
		%this.myFronteiras.add(lhasa);
		%this.myFronteiras.add(india);
		%this.myFronteiras.add(bMarDaArabia);
	
		case "india":
		%this.myFronteiras.add(cabul);
		%this.myFronteiras.add(lhasa);
		%this.myFronteiras.add(vietna);
		%this.myFronteiras.add(bDeBengala);
		%this.myFronteiras.add(bMarDaArabia);
		
		case "vietna":
		%this.myFronteiras.add(india);
		%this.myFronteiras.add(lhasa);
		%this.myFronteiras.add(xangai);
		%this.myFronteiras.add(bMarDaChina);
		%this.myFronteiras.add(bMarDeJava);
		%this.myFronteiras.add(bDeBengala);
		
		case "xangai":
		%this.myFronteiras.add(vietna);
		%this.myFronteiras.add(lhasa);
		%this.myFronteiras.add(pequim);
		%this.myFronteiras.add(bMarDachina);
		
		case "lhasa":
		%this.myFronteiras.add(vietna);
		%this.myFronteiras.add(india);
		%this.myFronteiras.add(cabul);
		%this.myFronteiras.add(cazaquistao);
		%this.myFronteiras.add(mongolia);
		%this.myFronteiras.add(pequim);
		%this.myFronteiras.add(xangai);
		%this.myFronteiras.add(omsk);
		
		case "pequim":
		%this.myFronteiras.add(xangai);
		%this.myFronteiras.add(lhasa);
		%this.myFronteiras.add(mongolia);
		%this.myFronteiras.add(omsk);
		%this.myFronteiras.add(magadan);
		%this.myFronteiras.add(bMarDoJapao);
		%this.myFronteiras.add(bMarDaChina);
		
		case "mongolia":
		%this.myFronteiras.add(pequim);
		%this.myFronteiras.add(lhasa);
		%this.myFronteiras.add(omsk);
		
		case "cazaquistao":
		%this.myFronteiras.add(cabul);
		%this.myFronteiras.add(moscou);
		%this.myFronteiras.add(kirov);
		%this.myFronteiras.add(omsk);
		%this.myFronteiras.add(lhasa);

		case "moscou":
		%this.myFronteiras.add(estocolmo);
		%this.myFronteiras.add(comunidadeEuropeia);
		%this.myFronteiras.add(bagda);
		%this.myFronteiras.add(cazaquistao);
		%this.myFronteiras.add(kirov);
		%this.myFronteiras.add(bMarDeKara);
		%this.myFronteiras.add(bMarDaNoruega);
		
		case "kirov":
		%this.myFronteiras.add(cazaquistao);
		%this.myFronteiras.add(moscou);
		%this.myFronteiras.add(omsk);
		%this.myFronteiras.add(bMarDeKara);
		%this.myFronteiras.add(bMarDaSiberia);
		
		case "omsk":
		%this.myFronteiras.add(magadan);
		%this.myFronteiras.add(pequim);
		%this.myFronteiras.add(mongolia);
		%this.myFronteiras.add(lhasa);
		%this.myFronteiras.add(cazaquistao);
		%this.myFronteiras.add(kirov);
		%this.myFronteiras.add(bMarDaSiberia);
		
		case "magadan":
		%this.myFronteiras.add(pequim);
		%this.myFronteiras.add(omsk);
		%this.myFronteiras.add(bMarChukchi);
		%this.myFronteiras.add(bMarDoJapao);
		%this.myFronteiras.add(cazaquistao);
		%this.myFronteiras.add(kirov);
		%this.myFronteiras.add(bMarDaSiberia);
		
		case "japao":
		%this.myFronteiras.add(bMarDoJapao);
		
		case "sumatra":
		%this.myFronteiras.add(bMarDeJava);
		
		case "borneo":
		%this.myFronteiras.add(bMarDeJava);
		
		case "novaGuine":
		%this.myFronteiras.add(bMarDeCoral);
		
		case "perth":
		%this.myFronteiras.add(sydney);
		%this.myFronteiras.add(bGrandeBaiaAustraliana);
		%this.myFronteiras.add(bMarDeJava);
		
		case "sydney":
		%this.myFronteiras.add(perth);
		%this.myFronteiras.add(bGrandeBaiaAustraliana);
		%this.myFronteiras.add(bMarDeCoral);
		%this.myFronteiras.add(bMarDeJava);
		//////////////
		/////////////		
		case "bEstreitoDeMagalhaes":
		%this.myFronteiras.add(argentina);
		%this.myFronteiras.add(bolivia);
		%this.myFronteiras.add(bDeLima);
		%this.myFronteiras.add(bGuanabara);
		%this.myFronteiras.add(oceanoAtlanticoSul);
		%this.myFronteiras.add(oceanoPacificoOcidental);
	
		case "bDeLima":
		%this.myFronteiras.add(bolivia);
		%this.myFronteiras.add(bGolfoDoPanama);
		%this.myFronteiras.add(bEstreitoDeMagalhaes);
		%this.myFronteiras.add(oceanoPacificoOcidental);

		case "bGolfoDoPanama":
		%this.myFronteiras.add(mexico);
		%this.myFronteiras.add(venezuela);
		%this.myFronteiras.add(bolivia);
		%this.myFronteiras.add(bDeLima);
		%this.myFronteiras.add(bGolfoDaCalifornia);
		%this.myFronteiras.add(oceanoPacificoOcidental);
		
		case "bGolfoDaCalifornia":
		%this.myFronteiras.add(losAngeles);
		%this.myFronteiras.add(mexico);
		%this.myFronteiras.add(bDeHecate);
		%this.myFronteiras.add(bGolfoDoPanama);
		%this.myFronteiras.add(oceanoPacificoOcidental);

		case "bDeHecate":
		%this.myFronteiras.add(alaska);
		%this.myFronteiras.add(bakerlake);
		%this.myFronteiras.add(vancouver);
		%this.myFronteiras.add(losAngeles);
		%this.myFronteiras.add(bGolfoDaCalifornia);
		%this.myFronteiras.add(bEstreitoDeBering);
		%this.myFronteiras.add(oceanoPacificoOcidental);
		
		case "bEstreitoDeBering":
		%this.myFronteiras.add(alaska);
		%this.myFronteiras.add(bDeHecate);
		%this.myFronteiras.add(bDeBaffin);
		%this.myFronteiras.add(bMarChukchi);
		%this.myFronteiras.add(oceanoPacificoOcidental);

		case "bDeBaffin":
		%this.myFronteiras.add(alaska);
		%this.myFronteiras.add(bakerlake);
		%this.myFronteiras.add(groenlandia);
		%this.myFronteiras.add(bEstreitoDeBering);
		%this.myFronteiras.add(bMarDaGroenlandia);
		%this.myFronteiras.add(bDeHudson);

		case "bMarDaGroenlandia":
		%this.myFronteiras.add(groenlandia);
		%this.myFronteiras.add(bEstreitoDaDinamarca);
		%this.myFronteiras.add(bDeBaffin);
		%this.myFronteiras.add(oceanoArtico);
		
		case "bEstreitoDaDinamarca":
		%this.myFronteiras.add(groenlandia);
		%this.myFronteiras.add(bMarNordico);
		%this.myFronteiras.add(bDeHudson);
		%this.myFronteiras.add(bMarDaGroenlandia);
		%this.myFronteiras.add(oceanoArtico);
		%this.myFronteiras.add(oceanoAtlanticoNorte);
		
		case "bDeHudson":
		%this.myFronteiras.add(montreal);
		%this.myFronteiras.add(toronto);
		%this.myFronteiras.add(bakerlake);
		%this.myFronteiras.add(groenlandia);
		%this.myFronteiras.add(bDeBaffin);
		%this.myFronteiras.add(bEstreitoDaDinamarca);
		%this.myFronteiras.add(bGolfoDoMaine);
		%this.myFronteiras.add(oceanoAtlanticoNorte);
		
		case "bGolfoDoMaine":
		%this.myFronteiras.add(novaYork);
		%this.myFronteiras.add(montreal);
		%this.myFronteiras.add(bDeHudson);
		%this.myFronteiras.add(bGolfoDoMexico);
		%this.myFronteiras.add(oceanoAtlanticoNorte);
		
		case "bGolfoDoMexico":
		%this.myFronteiras.add(mexico);
		%this.myFronteiras.add(houston);
		%this.myFronteiras.add(novaYork);
		%this.myFronteiras.add(bGolfoDoMaine);
		%this.myFronteiras.add(bMarDoCaribe);
		%this.myFronteiras.add(oceanoAtlanticoNorte);
		
		case "bMarDoCaribe":
		%this.myFronteiras.add(mexico);
		%this.myFronteiras.add(venezuela);
		%this.myFronteiras.add(manaus);
		%this.myFronteiras.add(bTodosOsSantos);
		%this.myFronteiras.add(bGolfoDoMexico);
		%this.myFronteiras.add(oceanoAtlanticoNorte);
		
		case "bTodosOsSantos":
		%this.myFronteiras.add(manaus);
		%this.myFronteiras.add(saoPaulo);
		%this.myFronteiras.add(bMarDoCaribe);
		%this.myFronteiras.add(bDeDakar);
		%this.myFronteiras.add(bGuanabara);
		%this.myFronteiras.add(oceanoAtlanticoNorte);
		%this.myFronteiras.add(oceanoAtlanticoSul);
		
		case "bGuanabara":
		%this.myFronteiras.add(saoPaulo);
		%this.myFronteiras.add(argentina);
		%this.myFronteiras.add(bTodosOsSantos);
		%this.myFronteiras.add(bEstreitoDeMagalhaes);
		%this.myFronteiras.add(oceanoAtlanticoSul);
		
		case "bGrandeBaiaAustraliana":
		%this.myFronteiras.add(perth);
		%this.myFronteiras.add(sydney);
		%this.myFronteiras.add(bMarDeCoral);
		%this.myFronteiras.add(bMarDeJava);
		%this.myFronteiras.add(oceanoIndico);
		
		case "bMarDeCoral":
		%this.myFronteiras.add(sydney);
		%this.myFronteiras.add(novaGuine);
		%this.myFronteiras.add(bGrandeBaiaAustraliana);
		%this.myFronteiras.add(bMarDeJava);
		%this.myFronteiras.add(oceanoIndico);
		%this.myFronteiras.add(oceanoPacificoOriental);
		%this.myFronteiras.add(oceanoPacificoOcidental);
		
		case "bMarDeJava":
		%this.myFronteiras.add(vietna);
		%this.myFronteiras.add(sumatra);
		%this.myFronteiras.add(borneo);
		%this.myFronteiras.add(perth);
		%this.myFronteiras.add(sydney);
		%this.myFronteiras.add(bGrandeBaiaAustraliana);
		%this.myFronteiras.add(bMarDeCoral);
		%this.myFronteiras.add(bDeBengala);
		%this.myFronteiras.add(bMarDaChina);
		%this.myFronteiras.add(oceanoIndico);
		%this.myFronteiras.add(oceanoPacificoOriental);
		
		case "bMarDaChina":
		%this.myFronteiras.add(pequim);
		%this.myFronteiras.add(xangai);
		%this.myFronteiras.add(vietna);
		%this.myFronteiras.add(bMarDoJapao);
		%this.myFronteiras.add(bMarDeJava);
		%this.myFronteiras.add(oceanoPacificoOriental);
		
		case "bMarDoJapao":
		%this.myFronteiras.add(japao);
		%this.myFronteiras.add(pequim);
		%this.myFronteiras.add(magadan);
		%this.myFronteiras.add(bMarChukchi);
		%this.myFronteiras.add(bMarDaChina);
		%this.myFronteiras.add(oceanoPacificoOriental);
		
		case "bMarChukchi":
		%this.myFronteiras.add(magadan);
		%this.myFronteiras.add(bMarDoJapao);
		%this.myFronteiras.add(bMarDaSiberia);
		%this.myFronteiras.add(bEstreitoDeBering);
		%this.myFronteiras.add(oceanoArtico);
		%this.myFronteiras.add(oceanoPacificoOriental);
		
		case "bMarDaSiberia":
		%this.myFronteiras.add(magadan);
		%this.myFronteiras.add(omsk);
		%this.myFronteiras.add(kirov);
		%this.myFronteiras.add(bMarChukchi);
		%this.myFronteiras.add(bMarDeKara);
		%this.myFronteiras.add(oceanoArtico);
		
		case "bMarDeKara":
		%this.myFronteiras.add(kirov);
		%this.myFronteiras.add(moscou);
		%this.myFronteiras.add(bMarDaSiberia);
		%this.myFronteiras.add(bMarDaNoruega);
		%this.myFronteiras.add(oceanoArtico);
		
		case "bMarDaNoruega":
		%this.myFronteiras.add(estocolmo);
		%this.myFronteiras.add(moscou);
		%this.myFronteiras.add(comunidadeEuropeia);
		%this.myFronteiras.add(bMarDeKara);
		%this.myFronteiras.add(bMarNordico);
		%this.myFronteiras.add(oceanoArtico);
		
		case "bMarNordico":
		%this.myFronteiras.add(londres);
		%this.myFronteiras.add(comunidadeEuropeia);
		%this.myFronteiras.add(bEstreitoDaDinamarca);
		%this.myFronteiras.add(bMarDaNoruega);
		%this.myFronteiras.add(bMarMediterraneo);
		%this.myFronteiras.add(oceanoArtico);
		%this.myFronteiras.add(oceanoAtlanticoNorte);
		
		case "bMarMediterraneo":
		%this.myFronteiras.add(comunidadeEuropeia);
		%this.myFronteiras.add(bagda);
		%this.myFronteiras.add(cairo);
		%this.myFronteiras.add(marrakesh);
		%this.myFronteiras.add(bMarNordico);
		%this.myFronteiras.add(bDeDakar);
		%this.myFronteiras.add(oceanoAtlanticoNorte);
		
		case "bDeDakar":
		%this.myFronteiras.add(marrakesh);
		%this.myFronteiras.add(bMarMediterraneo);
		%this.myFronteiras.add(bTodosOsSantos);
		%this.myFronteiras.add(bDeLuanda);
		%this.myFronteiras.add(oceanoAtlanticoNorte);
		%this.myFronteiras.add(oceanoAtlanticoSul);
		
		case "bDeLuanda":
		%this.myFronteiras.add(marrakesh);
		%this.myFronteiras.add(kinshasa);
		%this.myFronteiras.add(cidadeDoCabo);
		%this.myFronteiras.add(bDeDakar);
		%this.myFronteiras.add(bCaboDaBoaEsperanca);
		%this.myFronteiras.add(oceanoAtlanticoSul);
		
		case "bCaboDaBoaEsperanca":
		%this.myFronteiras.add(cidadeDoCabo);
		%this.myFronteiras.add(bDeLuanda);
		%this.myFronteiras.add(bDeMogadicio);
		%this.myFronteiras.add(oceanoAtlanticoSul);
		%this.myFronteiras.add(oceanoIndico);

		case "bDeMogadicio":
		%this.myFronteiras.add(cidadeDoCabo);
		%this.myFronteiras.add(kinshasa);
		%this.myFronteiras.add(cairo);
		%this.myFronteiras.add(bMarDaArabia);
		%this.myFronteiras.add(bCaboDaBoaEsperanca);
		%this.myFronteiras.add(oceanoIndico);
		
		case "bMarDaArabia":
		%this.myFronteiras.add(cairo);
		%this.myFronteiras.add(bagda);
		%this.myFronteiras.add(cabul);
		%this.myFronteiras.add(india);
		%this.myFronteiras.add(bDeMogadicio);
		%this.myFronteiras.add(bDeBengala);
		%this.myFronteiras.add(oceanoIndico);
		
		case "bDeBengala":
		%this.myFronteiras.add(india);
		%this.myFronteiras.add(vietna);
		%this.myFronteiras.add(bMarDaArabia);
		%this.myFronteiras.add(bMarDeJava);
		%this.myFronteiras.add(oceanoIndico);
		/////////////
		////////////
		case "oceanoPacificoOcidental":
		%this.myFronteiras.add(bEstreitoDeMagalhaes);
		%this.myFronteiras.add(bDeLima);
		%this.myFronteiras.add(bGolfoDoPanama);
		%this.myFronteiras.add(bGolfoDaCalifornia);
		%this.myFronteiras.add(bDeHecate);
		%this.myFronteiras.add(bEstreitoDeBering);
		%this.myFronteiras.add(bMarDeCoral);
		%this.myFronteiras.add(oceanoPacificoOriental);
		%this.myFronteiras.add(oceanoIndico);
		
		case "oceanoAtlanticoNorte":
		%this.myFronteiras.add(bTodosOsSantos);
		%this.myFronteiras.add(bMarDoCaribe);
		%this.myFronteiras.add(bGolfoDoMexico);
		%this.myFronteiras.add(bGolfoDoMaine);
		%this.myFronteiras.add(bDeHudson);
		%this.myFronteiras.add(bEstreitoDaDinamarca);
		%this.myFronteiras.add(bMarNordico);
		%this.myFronteiras.add(bMarMediterraneo);
		%this.myFronteiras.add(bDeDakar);
		
		case "oceanoAtlanticoSul":
		%this.myFronteiras.add(bTodosOsSantos);
		%this.myFronteiras.add(bDeDakar);
		%this.myFronteiras.add(bDeLuanda);
		%this.myFronteiras.add(bCaboDaBoaEsperanca);
		%this.myFronteiras.add(bEstreitoDeMagalhaes);
		%this.myFronteiras.add(bGuanabara);
		%this.myFronteiras.add(oceanoIndico);

		case "oceanoIndico":
		%this.myFronteiras.add(bCaboDaBoaEsperanca);
		%this.myFronteiras.add(bDeMogadicio);
		%this.myFronteiras.add(bMarDaArabia);
		%this.myFronteiras.add(bDeBengala);
		%this.myFronteiras.add(bMarDeJava);
		%this.myFronteiras.add(bGrandeBaiaAustraliana);
		%this.myFronteiras.add(bMarDeCoral);
		%this.myFronteiras.add(oceanoAtlanticoSul);
		%this.myFronteiras.add(oceanoPacificoOcidental);
		
		case "oceanoPacificoOriental":
		%this.myFronteiras.add(bMarDeCoral);
		%this.myFronteiras.add(bMarDeJava);
		%this.myFronteiras.add(bMarDaChina);
		%this.myFronteiras.add(bMarDoJapao);
		%this.myFronteiras.add(bMarChukchi);
		%this.myFronteiras.add(oceanoPacificoOcidental);

		case "oceanoArtico":
		%this.myFronteiras.add(bMarDaGroenlandia);
		%this.myFronteiras.add(bEstreitoDaDinamarca);
		%this.myFronteiras.add(bMarNordico);
		%this.myFronteiras.add(bMarDaNoruega);
		%this.myFronteiras.add(bMarDeKara);
		%this.myFronteiras.add(bMarDaSiberia);
		%this.myFronteiras.add(bMarChukchi);
		
		
		/////////////////////////////////////
		// UNGART: ///////////////////////////
		//
		case "UNG_b01":
		%this.myFronteiras.add(UNG_b02);
		%this.myFronteiras.add(UNG_b04);
		%this.myFronteiras.add(UNG_b14);
		%this.myFronteiras.add(UNG_PlDo02);
		%this.myFronteiras.add(UNG_PlDo01);
		%this.myFronteiras.add(UNG_DeEx02);
		
		case "UNG_b02":
		%this.myFronteiras.add(UNG_b01);
		%this.myFronteiras.add(UNG_b03);
		%this.myFronteiras.add(UNG_b11);
		%this.myFronteiras.add(UNG_PlDo01);
		%this.myFronteiras.add(UNG_DeEx01);
		%this.myFronteiras.add(UNG_DeEx02);
		%this.myFronteiras.add(UNG_CaOr01);
		
		case "UNG_b03":
		%this.myFronteiras.add(UNG_b02);
		%this.myFronteiras.add(UNG_b04);
		%this.myFronteiras.add(UNG_b05);
		%this.myFronteiras.add(UNG_b30);
		%this.myFronteiras.add(UNG_DeEx01);
		%this.myFronteiras.add(UNG_CaOr01);
		
		case "UNG_b04":
		%this.myFronteiras.add(UNG_b01);
		%this.myFronteiras.add(UNG_b03);
		%this.myFronteiras.add(UNG_b29);
		%this.myFronteiras.add(UNG_DeEx01);
		%this.myFronteiras.add(UNG_DeEx02);
	
		case "UNG_b05":
		%this.myFronteiras.add(UNG_b03);
		%this.myFronteiras.add(UNG_b06);
		%this.myFronteiras.add(UNG_b31);
		%this.myFronteiras.add(UNG_CaOr01);
		%this.myFronteiras.add(UNG_CaOr03);

		case "UNG_b06":
		%this.myFronteiras.add(UNG_b05);
		%this.myFronteiras.add(UNG_b07);
		%this.myFronteiras.add(UNG_b32);
		%this.myFronteiras.add(UNG_VaOr02);
		%this.myFronteiras.add(UNG_CaOr03);
		
		case "UNG_b07":
		%this.myFronteiras.add(UNG_b06);
		%this.myFronteiras.add(UNG_b08);
		%this.myFronteiras.add(UNG_b33);
		%this.myFronteiras.add(UNG_VaOr02);
		
		case "UNG_b08":
		%this.myFronteiras.add(UNG_b07);
		%this.myFronteiras.add(UNG_b09);
		%this.myFronteiras.add(UNG_b10);
		%this.myFronteiras.add(UNG_VaOr02);
		%this.myFronteiras.add(UNG_VaOr03);
		
		case "UNG_b09":
		%this.myFronteiras.add(UNG_b08);
		%this.myFronteiras.add(UNG_b10);
		%this.myFronteiras.add(UNG_b17);
		%this.myFronteiras.add(UNG_ChOr01);
		
		case "UNG_b10":
		%this.myFronteiras.add(UNG_b08);
		%this.myFronteiras.add(UNG_b09);
		%this.myFronteiras.add(UNG_b13);
		%this.myFronteiras.add(UNG_ChOr01);
		%this.myFronteiras.add(UNG_ChOr02);
		%this.myFronteiras.add(UNG_CaOr02);
		%this.myFronteiras.add(UNG_VaOr01);
		%this.myFronteiras.add(UNG_VaOr03);
		
		case "UNG_b11":
		%this.myFronteiras.add(UNG_b02);
		%this.myFronteiras.add(UNG_b12);
		%this.myFronteiras.add(UNG_b13);
		%this.myFronteiras.add(UNG_CaOr01);
		%this.myFronteiras.add(UNG_CaOr02);
		%this.myFronteiras.add(UNG_PlDo01);
		
		case "UNG_b12":
		%this.myFronteiras.add(UNG_b11);
		%this.myFronteiras.add(UNG_b13);
		%this.myFronteiras.add(UNG_b14);
		%this.myFronteiras.add(UNG_PlDo01);
		%this.myFronteiras.add(UNG_PlDo02);
		%this.myFronteiras.add(UNG_VaNo01);
		%this.myFronteiras.add(UNG_VaNo03);

		case "UNG_b13":
		%this.myFronteiras.add(UNG_b10);
		%this.myFronteiras.add(UNG_b11);
		%this.myFronteiras.add(UNG_b12);
		%this.myFronteiras.add(UNG_b22);
		%this.myFronteiras.add(UNG_b23);
		%this.myFronteiras.add(UNG_VaNo01);
		%this.myFronteiras.add(UNG_CaOr02);
		%this.myFronteiras.add(UNG_ChOr02);

		case "UNG_b14":
		%this.myFronteiras.add(UNG_b15);
		%this.myFronteiras.add(UNG_b01);
		%this.myFronteiras.add(UNG_b12);
		%this.myFronteiras.add(UNG_PlDo02);
		%this.myFronteiras.add(UNG_VaNo03);
		%this.myFronteiras.add(UNG_VaNo04);
		
		case "UNG_b15":
		%this.myFronteiras.add(UNG_b14);
		%this.myFronteiras.add(UNG_b16);
		%this.myFronteiras.add(UNG_VaNo04);
		
		case "UNG_b16":
		%this.myFronteiras.add(UNG_b15);
		%this.myFronteiras.add(UNG_b28);
		%this.myFronteiras.add(UNG_CaNo01);
		%this.myFronteiras.add(UNG_CaNo03);

		case "UNG_b17":
		%this.myFronteiras.add(UNG_b09);
		%this.myFronteiras.add(UNG_b18);
		%this.myFronteiras.add(UNG_b22);	
		%this.myFronteiras.add(UNG_ChOr01);
		%this.myFronteiras.add(UNG_ChOr02);

		case "UNG_b18":
		%this.myFronteiras.add(UNG_b17);
		%this.myFronteiras.add(UNG_b19);
		%this.myFronteiras.add(UNG_b22);	
		%this.myFronteiras.add(UNG_VaGu01);
		
		case "UNG_b19":
		%this.myFronteiras.add(UNG_b18);
		%this.myFronteiras.add(UNG_b20);
		%this.myFronteiras.add(UNG_b39);	
		%this.myFronteiras.add(UNG_VaGu01);

		case "UNG_b20":
		%this.myFronteiras.add(UNG_b19);
		%this.myFronteiras.add(UNG_b21);
		%this.myFronteiras.add(UNG_b39);	
		%this.myFronteiras.add(UNG_ChOc01);
		
		case "UNG_b21":
		%this.myFronteiras.add(UNG_b20);
		%this.myFronteiras.add(UNG_b33);
		%this.myFronteiras.add(UNG_b34);	
		%this.myFronteiras.add(UNG_ChOc01);
		
		case "UNG_b22":
		%this.myFronteiras.add(UNG_b13);
		%this.myFronteiras.add(UNG_b17);
		%this.myFronteiras.add(UNG_b18);
		%this.myFronteiras.add(UNG_b23);
		%this.myFronteiras.add(UNG_VaGu01);
		%this.myFronteiras.add(UNG_VaGu02);
		%this.myFronteiras.add(UNG_ChOr02);
		
		case "UNG_b23":
		%this.myFronteiras.add(UNG_b13);
		%this.myFronteiras.add(UNG_b22);
		%this.myFronteiras.add(UNG_b24);
		%this.myFronteiras.add(UNG_b25);
		%this.myFronteiras.add(UNG_PaGu02);
		%this.myFronteiras.add(UNG_VaGu02);
		%this.myFronteiras.add(UNG_VaNo01);
		
		case "UNG_b24":
		%this.myFronteiras.add(UNG_b23);
		%this.myFronteiras.add(UNG_b25);
		%this.myFronteiras.add(UNG_b26);
		%this.myFronteiras.add(UNG_VaNo01);
		%this.myFronteiras.add(UNG_VaNo02);
		%this.myFronteiras.add(UNG_CaNo01);
		%this.myFronteiras.add(UNG_CaNo02);
		%this.myFronteiras.add(UNG_IlVu02);
		
		case "UNG_b25":
		%this.myFronteiras.add(UNG_b23);
		%this.myFronteiras.add(UNG_b24);
		%this.myFronteiras.add(UNG_b38);
		%this.myFronteiras.add(UNG_PaGu01);
		%this.myFronteiras.add(UNG_PaGu02);
		%this.myFronteiras.add(UNG_IlVu02);
		
		case "UNG_b26":
		%this.myFronteiras.add(UNG_b24);
		%this.myFronteiras.add(UNG_b27);
		%this.myFronteiras.add(UNG_CaNo02);
		%this.myFronteiras.add(UNG_CaNo04);
		%this.myFronteiras.add(UNG_IlVu01);
		%this.myFronteiras.add(UNG_IlVu02);
		
		case "UNG_b27":
		%this.myFronteiras.add(UNG_b26);
		%this.myFronteiras.add(UNG_b28);
		%this.myFronteiras.add(UNG_b37);
		%this.myFronteiras.add(UNG_CaNo04);
		%this.myFronteiras.add(UNG_MoVe04);
		%this.myFronteiras.add(UNG_MoVe03);
		%this.myFronteiras.add(UNG_MoVe02);
		%this.myFronteiras.add(UNG_IlVu01);
		
		case "UNG_b28":
		%this.myFronteiras.add(UNG_b16);
		%this.myFronteiras.add(UNG_b27);
		%this.myFronteiras.add(UNG_b29);
		%this.myFronteiras.add(UNG_CaNo04);
		%this.myFronteiras.add(UNG_CaNo03);
		%this.myFronteiras.add(UNG_MoVe04);
		
		case "UNG_b29":
		%this.myFronteiras.add(UNG_b28);
		%this.myFronteiras.add(UNG_b30);
		%this.myFronteiras.add(UNG_b04);
		%this.myFronteiras.add(UNG_MoVe04);
		
		case "UNG_b30":
		%this.myFronteiras.add(UNG_b29);
		%this.myFronteiras.add(UNG_b31);
		%this.myFronteiras.add(UNG_b03);
		%this.myFronteiras.add(UNG_MoVe04);
		%this.myFronteiras.add(UNG_MoVe03);
		%this.myFronteiras.add(UNG_MoVe02);
		
		case "UNG_b31":
		%this.myFronteiras.add(UNG_b05);
		%this.myFronteiras.add(UNG_b30);
		%this.myFronteiras.add(UNG_b32);
		%this.myFronteiras.add(UNG_b36);
		%this.myFronteiras.add(UNG_MoVe01);
		%this.myFronteiras.add(UNG_MoVe02);
		
		case "UNG_b32":
		%this.myFronteiras.add(UNG_b31);
		%this.myFronteiras.add(UNG_b33);
		%this.myFronteiras.add(UNG_b36);
		%this.myFronteiras.add(UNG_b06);
		%this.myFronteiras.add(UNG_PrEx01);
		%this.myFronteiras.add(UNG_PrEx02);
		
		case "UNG_b33":
		%this.myFronteiras.add(UNG_b32);
		%this.myFronteiras.add(UNG_b34);
		%this.myFronteiras.add(UNG_b21);
		%this.myFronteiras.add(UNG_b07);
		%this.myFronteiras.add(UNG_PrEx01);
		
		case "UNG_b34":
		%this.myFronteiras.add(UNG_b33);
		%this.myFronteiras.add(UNG_b21);
		%this.myFronteiras.add(UNG_b35);
		%this.myFronteiras.add(UNG_PrEx01);
		%this.myFronteiras.add(UNG_PrEx02);
		%this.myFronteiras.add(UNG_ChOc01);

		case "UNG_b35":
		%this.myFronteiras.add(UNG_b34);
		%this.myFronteiras.add(UNG_b36);
		%this.myFronteiras.add(UNG_b37);
		%this.myFronteiras.add(UNG_b38);
		%this.myFronteiras.add(UNG_PrEx02);
		%this.myFronteiras.add(UNG_ChOc01);
		%this.myFronteiras.add(UNG_ChOc02);

		case "UNG_b36":
		%this.myFronteiras.add(UNG_b32);
		%this.myFronteiras.add(UNG_b31);
		%this.myFronteiras.add(UNG_b35);
		%this.myFronteiras.add(UNG_b37);
		%this.myFronteiras.add(UNG_PrEx02);
		%this.myFronteiras.add(UNG_MoVe01);

		case "UNG_b37":
		%this.myFronteiras.add(UNG_b35);
		%this.myFronteiras.add(UNG_b36);
		%this.myFronteiras.add(UNG_b27);
		%this.myFronteiras.add(UNG_b38);
		%this.myFronteiras.add(UNG_IlVu01);
		%this.myFronteiras.add(UNG_MoVe01);
		%this.myFronteiras.add(UNG_MoVe02);
		
		case "UNG_b38":
		%this.myFronteiras.add(UNG_b35);
		%this.myFronteiras.add(UNG_b37);
		%this.myFronteiras.add(UNG_b25);
		%this.myFronteiras.add(UNG_b39);
		%this.myFronteiras.add(UNG_IlVu01);
		%this.myFronteiras.add(UNG_IlVu02);
		%this.myFronteiras.add(UNG_ChOc02);
		%this.myFronteiras.add(UNG_PaGu01);
		
		case "UNG_b39":
		%this.myFronteiras.add(UNG_b38);
		%this.myFronteiras.add(UNG_b19);
		%this.myFronteiras.add(UNG_b20);
		%this.myFronteiras.add(UNG_PaGu01);
		%this.myFronteiras.add(UNG_PaGu03);
		%this.myFronteiras.add(UNG_ChOc01);
		%this.myFronteiras.add(UNG_ChOc02);
		%this.myFronteiras.add(UNG_VaGu01);
		
		//Áreas Terrestres:
		//Chifre Ocidental:
		case "UNG_ChOc01":
		%this.myFronteiras.add(UNG_b20);
		%this.myFronteiras.add(UNG_b21);
		%this.myFronteiras.add(UNG_b34);
		%this.myFronteiras.add(UNG_b35);
		%this.myFronteiras.add(UNG_b39);
		%this.myFronteiras.add(UNG_ChOc02);
		
		case "UNG_ChOc02":
		%this.myFronteiras.add(UNG_b35);
		%this.myFronteiras.add(UNG_b38);
		%this.myFronteiras.add(UNG_b39);
		%this.myFronteiras.add(UNG_ChOc01);
		
		//Chifre Oriental:
		case "UNG_ChOr01":
		%this.myFronteiras.add(UNG_b09);
		%this.myFronteiras.add(UNG_b10);
		%this.myFronteiras.add(UNG_b17);
		%this.myFronteiras.add(UNG_ChOr02);
		
		case "UNG_ChOr02":
		%this.myFronteiras.add(UNG_b10);
		%this.myFronteiras.add(UNG_b13);
		%this.myFronteiras.add(UNG_b17);
		%this.myFronteiras.add(UNG_b22);
		%this.myFronteiras.add(UNG_ChOr01);
		
		//Platô Dourado:
		case "UNG_PlDo01":
		%this.myFronteiras.add(UNG_b01);
		%this.myFronteiras.add(UNG_b02);
		%this.myFronteiras.add(UNG_b11);
		%this.myFronteiras.add(UNG_b12);
		%this.myFronteiras.add(UNG_PlDo02);
		
		case "UNG_PlDo02":
		%this.myFronteiras.add(UNG_b01);
		%this.myFronteiras.add(UNG_b12);
		%this.myFronteiras.add(UNG_b14);
		%this.myFronteiras.add(UNG_PlDo01);
		
		//Deserto Externo:
		case "UNG_DeEx01":
		%this.myFronteiras.add(UNG_b02);
		%this.myFronteiras.add(UNG_b03);
		%this.myFronteiras.add(UNG_b04);
		%this.myFronteiras.add(UNG_DeEx02);
		
		case "UNG_DeEx02":
		%this.myFronteiras.add(UNG_b01);
		%this.myFronteiras.add(UNG_b02);
		%this.myFronteiras.add(UNG_b04);
		%this.myFronteiras.add(UNG_DeEx01);
	
		//Praias Externas:
		case "UNG_PrEx01":
		%this.myFronteiras.add(UNG_b32);
		%this.myFronteiras.add(UNG_b33);
		%this.myFronteiras.add(UNG_b34);
		%this.myFronteiras.add(UNG_PrEx02);
		
		case "UNG_PrEx02":
		%this.myFronteiras.add(UNG_b32);
		%this.myFronteiras.add(UNG_b34);
		%this.myFronteiras.add(UNG_b35);
		%this.myFronteiras.add(UNG_b36);
		%this.myFronteiras.add(UNG_PrEx01);
		
		//Montes Verticais:
		case "UNG_MoVe01":
		%this.myFronteiras.add(UNG_b31);
		%this.myFronteiras.add(UNG_b36);
		%this.myFronteiras.add(UNG_b37);
		%this.myFronteiras.add(UNG_MoVe02);
		
		case "UNG_MoVe02":
		%this.myFronteiras.add(UNG_b27);
		%this.myFronteiras.add(UNG_b30);
		%this.myFronteiras.add(UNG_b31);
		%this.myFronteiras.add(UNG_b37);
		%this.myFronteiras.add(UNG_MoVe01);
		%this.myFronteiras.add(UNG_MoVe03);
		
		case "UNG_MoVe03":
		%this.myFronteiras.add(UNG_b27);
		%this.myFronteiras.add(UNG_b30);
		%this.myFronteiras.add(UNG_MoVe02);
		%this.myFronteiras.add(UNG_MoVe04);
		
		case "UNG_MoVe04":
		%this.myFronteiras.add(UNG_b27);
		%this.myFronteiras.add(UNG_b28);
		%this.myFronteiras.add(UNG_b29);
		%this.myFronteiras.add(UNG_b30);
		%this.myFronteiras.add(UNG_MoVe03);
		
		//Ilha Vulcânica:
		case "UNG_IlVu01":
		%this.myFronteiras.add(UNG_b26);
		%this.myFronteiras.add(UNG_b27);
		%this.myFronteiras.add(UNG_b37);
		%this.myFronteiras.add(UNG_b38);
		%this.myFronteiras.add(UNG_IlVu02);
		
		case "UNG_IlVu02":
		%this.myFronteiras.add(UNG_b24);
		%this.myFronteiras.add(UNG_b25);
		%this.myFronteiras.add(UNG_b26);
		%this.myFronteiras.add(UNG_b38);
		%this.myFronteiras.add(UNG_IlVu01);
		
		//Cânion Nórdico
		case "UNG_CaNo01":
		%this.myFronteiras.add(UNG_b16);
		%this.myFronteiras.add(UNG_b24);
		%this.myFronteiras.add(UNG_VaNo02);
		%this.myFronteiras.add(UNG_VaNo04);
		%this.myFronteiras.add(UNG_CaNo02);
		%this.myFronteiras.add(UNG_CaNo03);
		
		case "UNG_CaNo02":
		%this.myFronteiras.add(UNG_b24);
		%this.myFronteiras.add(UNG_b26);
		%this.myFronteiras.add(UNG_CaNo01);
		%this.myFronteiras.add(UNG_CaNo03);
		%this.myFronteiras.add(UNG_CaNo04);
		
		case "UNG_CaNo03":
		%this.myFronteiras.add(UNG_b16);
		%this.myFronteiras.add(UNG_b28);
		%this.myFronteiras.add(UNG_CaNo01);
		%this.myFronteiras.add(UNG_CaNo02);
		%this.myFronteiras.add(UNG_CaNo04);
		
		case "UNG_CaNo04":
		%this.myFronteiras.add(UNG_b26);
		%this.myFronteiras.add(UNG_b27);
		%this.myFronteiras.add(UNG_b28);
		%this.myFronteiras.add(UNG_CaNo02);
		%this.myFronteiras.add(UNG_CaNo03);
		
		//Vale Nórdico:
		case "UNG_VaNo01":
		%this.myFronteiras.add(UNG_b12);
		%this.myFronteiras.add(UNG_b13);
		%this.myFronteiras.add(UNG_b23);
		%this.myFronteiras.add(UNG_b24);
		%this.myFronteiras.add(UNG_VaNo02);
		%this.myFronteiras.add(UNG_VaNo03);
		
		case "UNG_VaNo02":
		%this.myFronteiras.add(UNG_b24);
		%this.myFronteiras.add(UNG_VaNo01);
		%this.myFronteiras.add(UNG_VaNo03);
		%this.myFronteiras.add(UNG_VaNo04);
		%this.myFronteiras.add(UNG_CaNo01);
		
		case "UNG_VaNo03":
		%this.myFronteiras.add(UNG_b12);
		%this.myFronteiras.add(UNG_b14);
		%this.myFronteiras.add(UNG_VaNo01);
		%this.myFronteiras.add(UNG_VaNo02);
		%this.myFronteiras.add(UNG_VaNo04);
		
		case "UNG_VaNo04":
		%this.myFronteiras.add(UNG_b14);
		%this.myFronteiras.add(UNG_b15);
		%this.myFronteiras.add(UNG_b16);
		%this.myFronteiras.add(UNG_VaNo02);
		%this.myFronteiras.add(UNG_VaNo03);
		%this.myFronteiras.add(UNG_CaNo01);
		
		//Cânion Oriental:
		case "UNG_CaOr01":
		%this.myFronteiras.add(UNG_b02);
		%this.myFronteiras.add(UNG_b03);
		%this.myFronteiras.add(UNG_b05);
		%this.myFronteiras.add(UNG_b11);
		%this.myFronteiras.add(UNG_CaOr02);
		%this.myFronteiras.add(UNG_CaOr03);
		%this.myFronteiras.add(UNG_CaOr04);
		
		case "UNG_CaOr02":
		%this.myFronteiras.add(UNG_b10);
		%this.myFronteiras.add(UNG_b11);
		%this.myFronteiras.add(UNG_b13);
		%this.myFronteiras.add(UNG_CaOr01);
		%this.myFronteiras.add(UNG_CaOr04);
		%this.myFronteiras.add(UNG_VaOr01);
		
		case "UNG_CaOr03":
		%this.myFronteiras.add(UNG_b05);
		%this.myFronteiras.add(UNG_b06);
		%this.myFronteiras.add(UNG_CaOr01);
		%this.myFronteiras.add(UNG_CaOr04);
		%this.myFronteiras.add(UNG_VaOr02);
		
		case "UNG_CaOr04":
		%this.myFronteiras.add(UNG_CaOr01);
		%this.myFronteiras.add(UNG_CaOr02);
		%this.myFronteiras.add(UNG_CaOr03);
		%this.myFronteiras.add(UNG_VaOr01);
		%this.myFronteiras.add(UNG_VaOr02);
		%this.myFronteiras.add(UNG_VaOr03);
		
		case "UNG_VaOr01":
		%this.myFronteiras.add(UNG_b10);
		%this.myFronteiras.add(UNG_CaOr02);
		%this.myFronteiras.add(UNG_CaOr04);
		%this.myFronteiras.add(UNG_VaOr03);
		
		case "UNG_VaOr02":
		%this.myFronteiras.add(UNG_b06);
		%this.myFronteiras.add(UNG_b07);
		%this.myFronteiras.add(UNG_b08);
		%this.myFronteiras.add(UNG_VaOr03);
		%this.myFronteiras.add(UNG_CaOr03);
		%this.myFronteiras.add(UNG_CaOr04);
		
		case "UNG_VaOr03":
		%this.myFronteiras.add(UNG_b08);
		%this.myFronteiras.add(UNG_b10);
		%this.myFronteiras.add(UNG_VaOr01);
		%this.myFronteiras.add(UNG_VaOr02);
		%this.myFronteiras.add(UNG_CaOr04);
		
		//Pântano Gulok:
		case "UNG_PaGu01":
		%this.myFronteiras.add(UNG_b25);
		%this.myFronteiras.add(UNG_b38);
		%this.myFronteiras.add(UNG_b39);
		%this.myFronteiras.add(UNG_PaGu02);
		%this.myFronteiras.add(UNG_PaGu03);
		
		case "UNG_PaGu02":
		%this.myFronteiras.add(UNG_b23);
		%this.myFronteiras.add(UNG_b25);
		%this.myFronteiras.add(UNG_VaGu02);
		%this.myFronteiras.add(UNG_PaGu01);
		%this.myFronteiras.add(UNG_PaGu03);
		
		case "UNG_PaGu03":
		%this.myFronteiras.add(UNG_b39);
		%this.myFronteiras.add(UNG_VaGu01);
		%this.myFronteiras.add(UNG_VaGu02);
		%this.myFronteiras.add(UNG_PaGu01);
		%this.myFronteiras.add(UNG_PaGu02);
		
		//Vale dos Guloks
		case "UNG_VaGu01":
		%this.myFronteiras.add(UNG_b18);
		%this.myFronteiras.add(UNG_b19);
		%this.myFronteiras.add(UNG_b22);
		%this.myFronteiras.add(UNG_b39);
		%this.myFronteiras.add(UNG_VaGu02);
		%this.myFronteiras.add(UNG_PaGu03);
		
		case "UNG_VaGu02":
		%this.myFronteiras.add(UNG_b22);
		%this.myFronteiras.add(UNG_b23);
		%this.myFronteiras.add(UNG_VaGu01);
		%this.myFronteiras.add(UNG_PaGu02);
		%this.myFronteiras.add(UNG_PaGu03);
		
		
		///////////////////////
		//Telúria:
		case "TEL_b01":
		%this.myFronteiras.add(TEL_b02);
		%this.myFronteiras.add(TEL_zavinia01);
		%this.myFronteiras.add(TEL_zavinia02);
		%this.myFronteiras.add(TEL_dharin02);
		%this.myFronteiras.add(TEL_nir01);
		
		case "TEL_b02":
		%this.myFronteiras.add(TEL_b01);
		%this.myFronteiras.add(TEL_b03);
		%this.myFronteiras.add(TEL_b28);
		%this.myFronteiras.add(TEL_dharin02);
		%this.myFronteiras.add(TEL_dharin03);
		%this.myFronteiras.add(TEL_nir01);
		%this.myFronteiras.add(TEL_nir02);
		
		case "TEL_b03":
		%this.myFronteiras.add(TEL_b02);
		%this.myFronteiras.add(TEL_b04);
		%this.myFronteiras.add(TEL_b28);
		%this.myFronteiras.add(TEL_valinur01);
		%this.myFronteiras.add(TEL_argonia01);
		%this.myFronteiras.add(TEL_nir02);
		
		case "TEL_b04":
		%this.myFronteiras.add(TEL_b03);
		%this.myFronteiras.add(TEL_b05);
		%this.myFronteiras.add(TEL_b06);
		%this.myFronteiras.add(TEL_b07);
		%this.myFronteiras.add(TEL_nexus01);
		%this.myFronteiras.add(TEL_argonia01);
		%this.myFronteiras.add(TEL_nir02);
		
		case "TEL_b05":
		%this.myFronteiras.add(TEL_b04);
		%this.myFronteiras.add(TEL_nexus01);
		%this.myFronteiras.add(TEL_nir01);
		%this.myFronteiras.add(TEL_nir02);
		
		case "TEL_b06":
		%this.myFronteiras.add(TEL_b04);
		%this.myFronteiras.add(TEL_b07);
		%this.myFronteiras.add(TEL_b16);
		%this.myFronteiras.add(TEL_b18);
		%this.myFronteiras.add(TEL_vuldan01);
		%this.myFronteiras.add(TEL_argonia01);
		%this.myFronteiras.add(TEL_karzin01);
		
		case "TEL_b07":
		%this.myFronteiras.add(TEL_b04);
		%this.myFronteiras.add(TEL_b06);
		%this.myFronteiras.add(TEL_b08);
		%this.myFronteiras.add(TEL_nexus01);
		%this.myFronteiras.add(TEL_karzin01);
		
		case "TEL_b08":
		%this.myFronteiras.add(TEL_b07);
		%this.myFronteiras.add(TEL_b09);
		%this.myFronteiras.add(TEL_karzin01);
		%this.myFronteiras.add(TEL_karzin02);
		
		case "TEL_b09":
		%this.myFronteiras.add(TEL_b08);
		%this.myFronteiras.add(TEL_b15);
		%this.myFronteiras.add(TEL_karzin02);
		%this.myFronteiras.add(TEL_goruk01);
		%this.myFronteiras.add(TEL_goruk02);
		
		case "TEL_b10":
		%this.myFronteiras.add(TEL_b11);
		%this.myFronteiras.add(TEL_b30);
		%this.myFronteiras.add(TEL_keltur02);
		%this.myFronteiras.add(TEL_keltur03);
		
		case "TEL_b11":
		%this.myFronteiras.add(TEL_b10);
		%this.myFronteiras.add(TEL_b14);
		%this.myFronteiras.add(TEL_b31);
		%this.myFronteiras.add(TEL_keltur02);
		%this.myFronteiras.add(TEL_lornia02);
		
		case "TEL_b12":
		%this.myFronteiras.add(TEL_b13);
		%this.myFronteiras.add(TEL_b34);
		%this.myFronteiras.add(TEL_goruk01);
		%this.myFronteiras.add(TEL_goruk02);
		
		case "TEL_b13":
		%this.myFronteiras.add(TEL_b12);
		%this.myFronteiras.add(TEL_b14);
		%this.myFronteiras.add(TEL_b15);
		%this.myFronteiras.add(TEL_b33);
		%this.myFronteiras.add(TEL_goruk02);
		
		case "TEL_b14":
		%this.myFronteiras.add(TEL_b11);
		%this.myFronteiras.add(TEL_b13);
		%this.myFronteiras.add(TEL_b15);
		%this.myFronteiras.add(TEL_b32);
		%this.myFronteiras.add(TEL_lornia01);
		%this.myFronteiras.add(TEL_lornia02);
		
		case "TEL_b15":
		%this.myFronteiras.add(TEL_b09);
		%this.myFronteiras.add(TEL_b13);
		%this.myFronteiras.add(TEL_b14);
		%this.myFronteiras.add(TEL_b16);
		%this.myFronteiras.add(TEL_b17);
		%this.myFronteiras.add(TEL_karzin02);
		%this.myFronteiras.add(TEL_goruk02);
		%this.myFronteiras.add(TEL_lornia01);
		
		case "TEL_b16":
		%this.myFronteiras.add(TEL_b06);
		%this.myFronteiras.add(TEL_b15);
		%this.myFronteiras.add(TEL_b17);
		%this.myFronteiras.add(TEL_b18);
		%this.myFronteiras.add(TEL_karzin01);
		%this.myFronteiras.add(TEL_karzin02);
		%this.myFronteiras.add(TEL_canhao01);
		
		case "TEL_b17":
		%this.myFronteiras.add(TEL_b15);
		%this.myFronteiras.add(TEL_b16);
		%this.myFronteiras.add(TEL_b20);
		%this.myFronteiras.add(TEL_b21);
		%this.myFronteiras.add(TEL_lornia01);
		%this.myFronteiras.add(TEL_lornia02);
		%this.myFronteiras.add(TEL_canhao01);
		
		case "TEL_b18":
		%this.myFronteiras.add(TEL_b06);
		%this.myFronteiras.add(TEL_b16);
		%this.myFronteiras.add(TEL_b21);
		%this.myFronteiras.add(TEL_b23);
		%this.myFronteiras.add(TEL_vuldan01);
		%this.myFronteiras.add(TEL_vuldan02);
		%this.myFronteiras.add(TEL_canhao01);
		
		case "TEL_b19":
		%this.myFronteiras.add(TEL_b20);
		%this.myFronteiras.add(TEL_malik01);
		%this.myFronteiras.add(TEL_malik02);
		%this.myFronteiras.add(TEL_keltur01);
		%this.myFronteiras.add(TEL_keltur03);
		
		case "TEL_b20":
		%this.myFronteiras.add(TEL_b17);
		%this.myFronteiras.add(TEL_b19);
		%this.myFronteiras.add(TEL_b21);
		%this.myFronteiras.add(TEL_lornia02);
		%this.myFronteiras.add(TEL_malik02);
		%this.myFronteiras.add(TEL_keltur01);
		
		case "TEL_b21":
		%this.myFronteiras.add(TEL_b17);
		%this.myFronteiras.add(TEL_b18);
		%this.myFronteiras.add(TEL_b20);
		%this.myFronteiras.add(TEL_b23);
		%this.myFronteiras.add(TEL_canhao01);
		%this.myFronteiras.add(TEL_malik02);
		
		case "TEL_b22":
		%this.myFronteiras.add(TEL_b23);
		%this.myFronteiras.add(TEL_b24);
		%this.myFronteiras.add(TEL_malik01);
		%this.myFronteiras.add(TEL_malik02);
		
		case "TEL_b23":
		%this.myFronteiras.add(TEL_b18);
		%this.myFronteiras.add(TEL_b21);
		%this.myFronteiras.add(TEL_b22);
		%this.myFronteiras.add(TEL_b24);
		%this.myFronteiras.add(TEL_vuldan02);
		%this.myFronteiras.add(TEL_vuldan03);
		%this.myFronteiras.add(TEL_malik02);
		%this.myFronteiras.add(TEL_argonia04);
		
		case "TEL_b24":
		%this.myFronteiras.add(TEL_b22);
		%this.myFronteiras.add(TEL_b23);
		%this.myFronteiras.add(TEL_b25);
		%this.myFronteiras.add(TEL_argonia04);
		
		case "TEL_b25":
		%this.myFronteiras.add(TEL_b24);
		%this.myFronteiras.add(TEL_b26);
		%this.myFronteiras.add(TEL_b27);
		%this.myFronteiras.add(TEL_valinur02);
		%this.myFronteiras.add(TEL_terion02);

		case "TEL_b26":
		%this.myFronteiras.add(TEL_b25);
		%this.myFronteiras.add(TEL_terion01);
		%this.myFronteiras.add(TEL_terion02);
		
		case "TEL_b27":
		%this.myFronteiras.add(TEL_b25);
		%this.myFronteiras.add(TEL_b28);
		%this.myFronteiras.add(TEL_b29);
		%this.myFronteiras.add(TEL_valinur01);
		%this.myFronteiras.add(TEL_valinur02);
		%this.myFronteiras.add(TEL_terion02);
		
		case "TEL_b28":
		%this.myFronteiras.add(TEL_b02);
		%this.myFronteiras.add(TEL_b03);
		%this.myFronteiras.add(TEL_b27);
		%this.myFronteiras.add(TEL_b29);
		%this.myFronteiras.add(TEL_valinur01);
		%this.myFronteiras.add(TEL_dharin03);
		
		case "TEL_b29":
		%this.myFronteiras.add(TEL_b27);
		%this.myFronteiras.add(TEL_b28);
		%this.myFronteiras.add(TEL_b31);
		%this.myFronteiras.add(TEL_terion01);
		%this.myFronteiras.add(TEL_terion02);
		%this.myFronteiras.add(TEL_dharin03);
		
		case "TEL_b30":
		%this.myFronteiras.add(TEL_b10);
		%this.myFronteiras.add(TEL_b31);
		%this.myFronteiras.add(TEL_terion01);

		case "TEL_b31":
		%this.myFronteiras.add(TEL_b11);
		%this.myFronteiras.add(TEL_b29);
		%this.myFronteiras.add(TEL_b30);
		%this.myFronteiras.add(TEL_b32);
		%this.myFronteiras.add(TEL_terion01);
		%this.myFronteiras.add(TEL_dharin03);
		
		case "TEL_b32":
		%this.myFronteiras.add(TEL_b14);
		%this.myFronteiras.add(TEL_b31);
		%this.myFronteiras.add(TEL_b33);
		%this.myFronteiras.add(TEL_dharin01);
		%this.myFronteiras.add(TEL_dharin03);
		
		case "TEL_b33":
		%this.myFronteiras.add(TEL_b13);
		%this.myFronteiras.add(TEL_b32);
		%this.myFronteiras.add(TEL_b34);
		%this.myFronteiras.add(TEL_dharin01);
		%this.myFronteiras.add(TEL_zavinia02);
		
		case "TEL_b34":
		%this.myFronteiras.add(TEL_b12);
		%this.myFronteiras.add(TEL_b33);
		%this.myFronteiras.add(TEL_zavinia01);
		%this.myFronteiras.add(TEL_zavinia02);
		
		case "TEL_nir01":
		%this.myFronteiras.add(TEL_b01);
		%this.myFronteiras.add(TEL_b02);
		%this.myFronteiras.add(TEL_b05);
		%this.myFronteiras.add(TEL_nir02);
		
		case "TEL_nir02":
		%this.myFronteiras.add(TEL_b02);
		%this.myFronteiras.add(TEL_b03);
		%this.myFronteiras.add(TEL_b04);
		%this.myFronteiras.add(TEL_b05);
		%this.myFronteiras.add(TEL_nir01);
		
		case "TEL_karzin01":
		%this.myFronteiras.add(TEL_b06);
		%this.myFronteiras.add(TEL_b07);
		%this.myFronteiras.add(TEL_b08);
		%this.myFronteiras.add(TEL_b16);
		%this.myFronteiras.add(TEL_karzin02);
		
		case "TEL_karzin02":
		%this.myFronteiras.add(TEL_b08);
		%this.myFronteiras.add(TEL_b09);
		%this.myFronteiras.add(TEL_b15);
		%this.myFronteiras.add(TEL_b16);
		%this.myFronteiras.add(TEL_karzin01);
		
		case "TEL_goruk01":
		%this.myFronteiras.add(TEL_b09);
		%this.myFronteiras.add(TEL_b12);
		%this.myFronteiras.add(TEL_goruk02);
		
		case "TEL_goruk02":
		%this.myFronteiras.add(TEL_b09);
		%this.myFronteiras.add(TEL_b12);
		%this.myFronteiras.add(TEL_b13);
		%this.myFronteiras.add(TEL_b15);
		%this.myFronteiras.add(TEL_goruk01);
		
		case "TEL_malik01":
		%this.myFronteiras.add(TEL_b19);
		%this.myFronteiras.add(TEL_b22);
		%this.myFronteiras.add(TEL_malik02);
		
		case "TEL_malik02":
		%this.myFronteiras.add(TEL_b19);
		%this.myFronteiras.add(TEL_b20);
		%this.myFronteiras.add(TEL_b21);
		%this.myFronteiras.add(TEL_b22);
		%this.myFronteiras.add(TEL_b23);
		%this.myFronteiras.add(TEL_malik01);
		
		case "TEL_terion01":
		%this.myFronteiras.add(TEL_b26);
		%this.myFronteiras.add(TEL_b29);
		%this.myFronteiras.add(TEL_b30);
		%this.myFronteiras.add(TEL_b31);
		%this.myFronteiras.add(TEL_terion02);
		
		case "TEL_terion02":
		%this.myFronteiras.add(TEL_b25);
		%this.myFronteiras.add(TEL_b26);
		%this.myFronteiras.add(TEL_b27);
		%this.myFronteiras.add(TEL_b29);
		%this.myFronteiras.add(TEL_terion01);
		
		case "TEL_zavinia01":
		%this.myFronteiras.add(TEL_b01);
		%this.myFronteiras.add(TEL_b34);
		%this.myFronteiras.add(TEL_zavinia02);
		
		case "TEL_zavinia02":
		%this.myFronteiras.add(TEL_b01);
		%this.myFronteiras.add(TEL_b33);
		%this.myFronteiras.add(TEL_b34);
		%this.myFronteiras.add(TEL_dharin01);
		%this.myFronteiras.add(TEL_dharin02);
		%this.myFronteiras.add(TEL_zavinia01);
		
		case "TEL_dharin01":
		%this.myFronteiras.add(TEL_b32);
		%this.myFronteiras.add(TEL_b33);
		%this.myFronteiras.add(TEL_dharin02);
		%this.myFronteiras.add(TEL_dharin03);
		%this.myFronteiras.add(TEL_zavinia02);
		
		case "TEL_dharin02":
		%this.myFronteiras.add(TEL_b01);
		%this.myFronteiras.add(TEL_b02);
		%this.myFronteiras.add(TEL_dharin01);
		%this.myFronteiras.add(TEL_dharin03);
		%this.myFronteiras.add(TEL_zavinia02);
		
		case "TEL_dharin03":
		%this.myFronteiras.add(TEL_b02);
		%this.myFronteiras.add(TEL_b28);
		%this.myFronteiras.add(TEL_b29);
		%this.myFronteiras.add(TEL_b31);
		%this.myFronteiras.add(TEL_b32);
		%this.myFronteiras.add(TEL_dharin01);
		%this.myFronteiras.add(TEL_dharin02);
		
		case "TEL_valinur01":
		%this.myFronteiras.add(TEL_b03);
		%this.myFronteiras.add(TEL_b28);
		%this.myFronteiras.add(TEL_valinur02);
		%this.myFronteiras.add(TEL_argonia01);
		%this.myFronteiras.add(TEL_argonia02);		
		%this.myFronteiras.add(TEL_argonia03);
		
		case "TEL_valinur02":
		%this.myFronteiras.add(TEL_b25);
		%this.myFronteiras.add(TEL_b27);
		%this.myFronteiras.add(TEL_valinur01);
		%this.myFronteiras.add(TEL_argonia03);		
		%this.myFronteiras.add(TEL_argonia04);
		
		case "TEL_argonia01":
		%this.myFronteiras.add(TEL_b03);
		%this.myFronteiras.add(TEL_b04);
		%this.myFronteiras.add(TEL_b06);
		%this.myFronteiras.add(TEL_valinur01);
		%this.myFronteiras.add(TEL_argonia02);	
		%this.myFronteiras.add(TEL_vuldan01);	
		
		case "TEL_argonia02":
		%this.myFronteiras.add(TEL_valinur01);
		%this.myFronteiras.add(TEL_argonia01);	
		%this.myFronteiras.add(TEL_argonia03);	
		%this.myFronteiras.add(TEL_vuldan01);	
		%this.myFronteiras.add(TEL_vuldan02);
		
		case "TEL_argonia03":
		%this.myFronteiras.add(TEL_valinur01);
		%this.myFronteiras.add(TEL_valinur02);
		%this.myFronteiras.add(TEL_argonia02);	
		%this.myFronteiras.add(TEL_argonia04);	
		%this.myFronteiras.add(TEL_vuldan02);	
		%this.myFronteiras.add(TEL_vuldan03);
		
		case "TEL_argonia04":
		%this.myFronteiras.add(TEL_b23);
		%this.myFronteiras.add(TEL_b24);
		%this.myFronteiras.add(TEL_valinur02);
		%this.myFronteiras.add(TEL_argonia03);	
		%this.myFronteiras.add(TEL_vuldan03);
		
		case "TEL_vuldan01":
		%this.myFronteiras.add(TEL_b06);
		%this.myFronteiras.add(TEL_b18);
		%this.myFronteiras.add(TEL_vuldan02);
		%this.myFronteiras.add(TEL_argonia01);	
		%this.myFronteiras.add(TEL_argonia02);
		
		case "TEL_vuldan02":
		%this.myFronteiras.add(TEL_b18);
		%this.myFronteiras.add(TEL_b23);
		%this.myFronteiras.add(TEL_vuldan01);
		%this.myFronteiras.add(TEL_vuldan03);
		%this.myFronteiras.add(TEL_argonia02);	
		%this.myFronteiras.add(TEL_argonia03);
		
		case "TEL_vuldan03":
		%this.myFronteiras.add(TEL_b23);
		%this.myFronteiras.add(TEL_vuldan02);
		%this.myFronteiras.add(TEL_argonia03);	
		%this.myFronteiras.add(TEL_argonia04);
		
		case "TEL_lornia01":
		%this.myFronteiras.add(TEL_b14);
		%this.myFronteiras.add(TEL_b15);
		%this.myFronteiras.add(TEL_b17);
		%this.myFronteiras.add(TEL_lornia02);
		
		case "TEL_lornia02":
		%this.myFronteiras.add(TEL_b11);
		%this.myFronteiras.add(TEL_b14);
		%this.myFronteiras.add(TEL_b17);
		%this.myFronteiras.add(TEL_b20);
		%this.myFronteiras.add(TEL_lornia01);
		%this.myFronteiras.add(TEL_keltur01);
		%this.myFronteiras.add(TEL_keltur02);
		
		case "TEL_keltur01":
		%this.myFronteiras.add(TEL_b19);
		%this.myFronteiras.add(TEL_b20);
		%this.myFronteiras.add(TEL_lornia02);
		%this.myFronteiras.add(TEL_keltur02);
		%this.myFronteiras.add(TEL_keltur03);
		
		case "TEL_keltur02":
		%this.myFronteiras.add(TEL_b10);
		%this.myFronteiras.add(TEL_b11);
		%this.myFronteiras.add(TEL_lornia02);
		%this.myFronteiras.add(TEL_keltur01);
		%this.myFronteiras.add(TEL_keltur03);
		
		case "TEL_keltur03":
		%this.myFronteiras.add(TEL_b10);
		%this.myFronteiras.add(TEL_b19);
		%this.myFronteiras.add(TEL_keltur01);
		%this.myFronteiras.add(TEL_keltur02);
		
		case "TEL_canhao01":
		%this.myFronteiras.add(TEL_b16);
		%this.myFronteiras.add(TEL_b17);
		%this.myFronteiras.add(TEL_b18);
		%this.myFronteiras.add(TEL_b21);
		
		case "TEL_nexus01":
		%this.myFronteiras.add(TEL_b04);
		%this.myFronteiras.add(TEL_b05);
		%this.myFronteiras.add(TEL_b07);
	}
}


function area::limparCompartilhamento(%this){
	if(%this.dono $= "COMPARTILHADA"){
		if(isObject(%this.dono1)){
			if(%this.dono1.mySimAreas.isMember(%this)){
				%this.dono1.mySimAreas.remove(%this);
			}
		}
		if(isObject(%this.dono2)){
			if(%this.dono2.mySimAreas.isMember(%this)){
				%this.dono2.mySimAreas.remove(%this);
			}
		}
		%this.dono1 = 0;
		%this.dono2 = 0;
	}
}

function area::getTenhoGente(%this){
	if(%this.pos1Flag !$= "nada" || %this.pos2Flag !$= "nada"){
		return true;	
	}
	
	return false;
}

function area::getTenhoBase(%this){
	return %this.pos0Flag;
}

function area::getMyPos4ListCount(%this){
	if(%this.terreno $= "terra" && %this.ilha != 1){
		return %this.myPos4List.getCount();	
	}
	
	return 0;
}

function area::getTenhoRainhaGulok(){
	if(%this.pos0Flag){
		return %this.pos0Quem.gulok && !%this.pos0Quem.crisalida;
	}
	
	return false;
}

function area::getTenhoCrisalida(){
	if(%this.pos0Flag){
		return %this.pos0Quem.crisalida;	
	}
	
	return false;
}

function area::getTenhoBaseHumana(){
	if(%this.pos0Flag){
		return !%this.pos0Quem.gulok;
	}
	
	return false;
}

function area::getTenhoOvos(){
	if(%this.terreno $= "terra" && %this.myPos4List.getCount() > 0){
		for(%i = 0; %i < %this.myPos4List.getCount(); %i++){
			%unit = %this.myPos4List.getObject(%i);
			if(%unit.class $= "ovo"){
				return true;	
			}
		}
	}
	
	return false;
}



function area::getMyStatus(%this){
	%temGente = %this.getTenhoGente();
	%temBase = %this.getTenhoBase();
	%myPos4ListCount = %this.getMyPos4ListCount();
	
	if(%temGente == 0){ //se não tem unidades
		if(%temBase == 0 && %myPos4ListCount < 1){ //não tem bases nem ovos
			%status = "vazia"; //a área tá vazia
		} else { 
			if(%temBase == 1){
				if(%this.pos0Quem.gulok){
					if(!%this.pos0Quem.crisalida){
						//tem uma Rainha gulok:
						%status = "donoDaPos0";	
					} else {
						//tem uma crisálida:
						%status = "desprotegida"; //a área tá desprotegida
					}
				} else {
					//tem uma base humana:
					%status = "desprotegida"; //a área tá desprotegida
				}
			} else {
				//tem somente ovos:
				%status = "desprotegida";
			}
		}
	} else { //se tem unidades
		if(%this.pos2Flag $= "nada"){
			//só tem um truta na posição 1, ele é o dono da área (pode ter ovos do parceiro de dupla!)
			if(%temBase){
				if(%this.pos0Quem.dono != %this.pos1Quem.dono){
					if(%this.pos0Quem.dono.myDupla == %this.pos1Quem.dono){
						%status = "COMPARTILHADA";
						echo("COMPARTILHADA1");
					} else {
						if(%this.pos0Quem.gulok){
							if(!%this.pos0Quem.crisalida){
								//tem uma Rainha visitando ou recebendo visitas:
								%status = "MISTA";
								echo("MISTA1");
							} else {
								//alguém capturou uma crisálida!
								%status = "donoDaPos1";
							}
						} else {
							//alguém capturou uma base ou refinaria!
							%status = "donoDaPos1";
						}
					}
				} else {
					//ninguém capturou nada, apenas existe uma base/refinaria/rainha/crisálida que pertence ao dono da área:
					%status = "donoDaPos1";	
					
					//mas pode haver ovos do parceiro de dupla:
					//verifica se há visitantes na posição 4:
					if(%this.terreno $= "terra"){
						for (%i = 0; %i < %this.myPos4List.getCount(); %i++){ 
							if (%this.pos1Quem.dono.id !$= %this.myPos4List.getObject(%i).dono.id){
								if(%this.pos1Quem.dono.myDupla == %this.myPos4List.getObject(%i).dono){
									%status = "COMPARTILHADA";
									%i = %this.myPos4List.getCount();
									echo("COMPARTILHADA_X1");
								}
							}
						}
					}
				}
			} else {
				//nem tem base, o dono da área é a peça que está na posição 1
				%status = "donoDaPos1";
				
				//mas pode haver ovos do parceiro de dupla:
				//verifica se há visitantes na posição 4:
				if(%this.terreno $= "terra"){
					for (%i = 0; %i < %this.myPos4List.getCount(); %i++){ 
						if (%this.pos1Quem.dono.id !$= %this.myPos4List.getObject(%i).dono.id){
							if(%this.pos1Quem.dono.myDupla == %this.myPos4List.getObject(%i).dono){
								%status = "COMPARTILHADA";
								%i = %this.myPos4List.getCount();
								echo("COMPARTILHADA_X2");
							}
						}
					}
				}
			}
		} else {
			//tem alguém na pos 2
			if(%this.pos1Quem.dono != %this.pos2Quem.dono && %this.pos1Quem.dono !$= "nada"){
				if(%this.pos1Quem.dono.myDupla == %this.pos2Quem.dono){
					//se as peças da pos1 e 2 tiverem donos diferentes, mas companheiros de dupla
					%status = "COMPARTILHADA";
					echo("COMPARTILHADA2");
				} else {
					//se as peças da pos1 e 2 tiverem donos diferentes
					%status = "MISTA";
					echo("MISTA2");
				}				
			} else {
				//se as peças da pos1 e 2 tiverem o mesmo dono
				if(%this.pos0Quem.dono.myDupla == %this.pos1Quem.dono || %this.pos0Quem.dono.myDupla == %this.pos2Quem.dono){
					%status = "COMPARTILHADA";
				} else {
					%status = "donoDaPos1";
					
					//verifica se há visitantes na posição 3:
					for (%i = 0; %i < %this.myPos3List.getCount(); %i++){ 
						if (%this.pos1Quem.dono.id !$= %this.myPos3List.getObject(%i).dono.id){
							if(%this.pos1Quem.dono.myDupla == %this.myPos3List.getObject(%i).dono){
								%status = "COMPARTILHADA";
								echo("COMPARTILHADA3");
							} else {
								%status = "MISTA";
								echo("MISTA3");
							}
						}
					}
					
					//verifica se há visitantes na posição 4:
					if(%this.terreno $= "terra"){
						for (%i = 0; %i < %this.myPos4List.getCount(); %i++){ 
							if (%this.pos1Quem.dono.id !$= %this.myPos4List.getObject(%i).dono.id){
								if(%this.pos1Quem.dono.myDupla == %this.myPos4List.getObject(%i).dono){
									%status = "COMPARTILHADA";
									echo("COMPARTILHADA4");
								} else {
									%status = "MISTA";
									echo("MISTA4");
								}
							}
						}
					}
				
					//verifica se existe uma base que não pertence às peças da pos 1 e 2:
					if(%temBase){
						if(%this.pos0Quem.dono != %this.pos1Quem.dono){
							if(%this.pos0Quem.dono.myDupla == %this.pos1Quem.dono){
								%status = "COMPARTILHADA";
								echo("COMPARTILHADA5");
							} else {
								if(%this.pos0Quem.gulok){
									//uma rainha visita esta área ou é visitada por uma peça adversária na pos1:
									%status = "MISTA";
									echo("MISTA5");
								}
							}
						} else if(%this.pos0Quem.dono != %this.pos2Quem.dono){
							if(%this.pos0Quem.dono.myDupla == %this.pos2Quem.dono){
								%status = "COMPARTILHADA";
								echo("COMPARTILHADA6");
							} else {
								if(%this.pos0Quem.gulok){
									//uma rainha visita esta área ou é visitada por uma peça adversária na pos2:
									%status = "MISTA";
									echo("MISTA6");
								}
							}
						} else {
							//verifica se não tem um visitante na pos 3:
							for (%i = 0; %i < %this.myPos3List.getCount(); %i++){ 
								if (%this.pos0Quem.dono.id !$= %this.myPos3List.getObject(%i).dono.id){
									if(%this.pos0Quem.dono.myDupla == %this.myPos3List.getObject(%i).dono){
										%status = "COMPARTILHADA";
										echo("COMPARTILHADA7");
									} else {
										if(%this.pos0Quem.gulok){
											%status = "MISTA";
											echo("MISTA7");
										}
									}
								}
							}
							
							//verifica se não tem um visitante na pos 4:
							if(%this.terreno $= "terra"){
								for (%i = 0; %i < %this.myPos4List.getCount(); %i++){ 
									if (%this.pos0Quem.dono.id !$= %this.myPos4List.getObject(%i).dono.id){
										if(%this.pos0Quem.dono.myDupla == %this.myPos4List.getObject(%i).dono){
											%status = "COMPARTILHADA";
											echo("COMPARTILHADA8");
										} else {
											if(%this.pos0Quem.gulok){
												%status = "MISTA";
												echo("MISTA8");
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}	
	echo("AreaStatus (" @ %this.myName @ "):" SPC %status);
	%this.myStatus = %status;
	return %status;
}	

function area::clientAtualizarPosReservas(%this){
	%eval = "%myPos3Text =" SPC "pos3" @ %this.getName() @ "_img;";
	eval(%eval);
	
	%count = %this.myPos3List.getCount() - 2;
	if(%count < 0){
		%count = 0;
	}
	if(%this.myPos3List.getCount() < 2){
		%myPos3Text.setVisible(false);
	} else {
		%myPos3Text.setVisible(true);
		%myPos3Text.setFrame(%count);
	}
	
	if(%this.terreno !$= "mar"){
		if(%this.ilha != 1){ 
			%eval = "%myPos4Text =" SPC "pos4" @ %this.getName() @ "_img;";
			eval(%eval);
			%count = %this.myPos4List.getCount() - 2;
			if(%count < 0){
				%count = 0;
			}
			if(%this.myPos4List.getCount() < 2){
				%myPos4Text.setVisible(false);
			} else {
				%myPos4Text.setVisible(true);
				%myPos4Text.setFrame(%count);
			}
		}
	}
	clientFinalizarAtaque();
	clientFinalizarAiAtaque();
	
	clientAtualizarEstatisticas(); //aki é para cada client saber que perdeu uma área, rever objetivos e recursos; 
	if(isObject(%this.pos0Quem) && %this.pos0Quem.class $= "rainha"){
		schedule(1000, 0, "clientVerificarInstinto", %this.pos0Quem);	
	}
	schedule(1100, 0, "clientVerifyHorda", %this);		
}

function area::statusExec(%this, %status){
	switch$(%status){
		case "vazia":
		if(%this.dono $= "COMPARTILHADA"){
			%this.limparCompartilhamento();
		} else {
			if(isObject(%this.dono)){
				if(%this.dono.mySimAreas.isMember(%this)){
					%this.dono.mySimAreas.remove(%this);
				}
			}
			%this.dono = 0;
		}
		%this.desprotegida = false;
								
		case "desprotegida":
		%this.desprotegida = true;
		%this.limparCompartilhamento();
		if(%this.pos0Flag == true){
			%unitMinhaDona = %this.pos0Quem;			
		} else {
			%unitMinhaDona = %this.myPos4List.getObject(0);
		}
		%this.dono = %unitMinhaDona.dono;
		if(!%unitMinhaDona.dono.mySimAreas.isMember(%this)){
			%unitMinhaDona.dono.mySimAreas.add(%this);
		}
			
		case "donoDaPos1":
		if($IAMServer){
			echo("Dono de " @ %this.myName @ " -> " @ %this.pos1Quem.dono.persona.nome);
		} else {
			echo("Dono de : " @ %this.myName @ " -> " @ %this.pos1Quem.dono.nome);
		}
		%this.desprotegida = false;
		%this.limparCompartilhamento();
		if(!%this.pos1Quem.dono.mySimAreas.isMember(%this)){
			if(%this.oceano != 1 || $estouNoTutorial){ //se não for um oceano ou se eu estiver no tutorial
				%this.pos1Quem.dono.mySimAreas.add(%this);
			}
		}
		if(%this.pos0Flag == true){
			if(%this.pos0Quem.dono != %this.pos1Quem.dono){
				%lastDono = %this.pos0Quem.dono;
				%newDono = %this.pos1Quem.dono;
				%lastDono.mySimBases.remove(%this.pos0Quem);
				%newDono.mySimBases.add(%this.pos0Quem);
				if(%this.pos0Quem.refinaria){
					%lastDono.mySimRefinarias.remove(%this.pos0Quem);
					%newDono.mySimRefinarias.add(%this.pos0Quem);
				}
				if(%lastDono.mySimAreas.isMember(%this)){
					%lastDono.mySimAreas.remove(%this);
				}												
				//bloqueia o undo:
				if($IAmServer $= "1"){
					serverClearUndo(%newDono); //se eu for o server, zera o Undo do player dominante, não se pode voltar atrás depois de capturar uma base
					serverBlockUndo(%newDono); //bloqueia o undo para este movimento
					if(%lastDono.mySimAreas.getCount() < 1){
						serverPlayerKill(%lastDono, %newDono);
					}
				} else { //se eu for o client, zera o meu Undo, não se pode voltar atrás depois de capturar uma base
					clientClearUndo();
					$BLOCKUNDO =true;
				}
				%this.pos0Quem.dono = %this.pos1Quem.dono;
				if(%this.pos0Quem.crisalida){
					echo("CRISÁLIDA CAPTURADA!!!");
					%lastDono.mySimCrisalidas.remove(%this.pos0Quem);
					if(!isObject(%newDono.mySimCrisalidas)){
						%newDono.mySimCrisalidas = new SimSet();
					}
					%newDono.mySimCrisalidas.add(%this.pos0Quem);	
					if(!$IAMServer){
						%this.pos0Quem.setMyFX();
					}
					//e ainda pode ter ovos na posição 4!:
					if(%this.terreno $= "terra"){
						if(%this.ilha != 1){
							if(%this.myPos4List.getcount() > 0){
								%antigoDonoDosOvos = %this.myPos4List.getObject(0).dono;
								%tempPos4ListCount = %this.myPos4List.getcount();
								for(%i = 0; %i < %tempPos4ListCount; %i++){
									%ovo = %this.myPos4List.getObject(0);
									if(%ovo.dono != %this.pos1Quem.dono){
										if(%ovo.gulok){
											%ovo.kill();
											if(!$IAMServer){
												clientClearUndo();
												$BLOCKUNDO =true;
											}
											if(%antigoDonoDosOvos.mySimAreas.isMember(%this)){
												%antigoDonoDosOvos.mySimAreas.remove(%this);
											}
										}
									}
								}
							}
						}
					}
				} else {
					echo("BASE CAPTURADA!!!");
				}
				if($IAmServer $= "0"){
					clientVerifyOcultar(%this.pos0Quem, %lastDono); //se estiver oculta, aparece, a não ser que o dono também tenha ocultar.
				}
				if(%this.pos1Quem.gulok){
					%this.pos0Quem.safeDelete();
					if(isObject(%this.pos0Quem.myReciclarEffect))
						%this.pos0Quem.myReciclarEffect.safeDelete();
						
					%this.pos0Quem = 0;
					%this.pos0Flag = false;
					if($IAMServer $= "0"){
						if($mySelf == %this.pos1Quem.dono || ($mySelf.aiManager && $jogadorDaVez == $aiPlayer)){
							clientEvoluir(%this.pos1Quem, true); //pede para evoluir em rainha sem custo
						}
					}
				} else {
					if($IAMServer $= "0"){
						%this.pos0Quem.setMyImage();	
					}
				}
			}
		} else {
			//pode ter ovos na posição 4!:
			if(%this.terreno $= "terra"){
				if(%this.ilha != 1){
					if(%this.myPos4List.getcount() > 0){
						%antigoDonoDosOvos = %this.myPos4List.getObject(0).dono;
						for(%i = 0; %i < %this.myPos4List.getcount(); %i++){
							%ovo = %this.myPos4List.getObject(0);
							if(%ovo.dono != %this.pos1Quem.dono){
								if(%ovo.dono.myDupla != %this.pos1Quem.dono){
									if(%ovo.gulok){
										%ovo.kill();
										if(!$IAMServer){
											clientClearUndo();
											$BLOCKUNDO =true;
										}
										if(%antigoDonoDosOvos.mySimAreas.isMember(%this)){
											%antigoDonoDosOvos.mySimAreas.remove(%this);
										}
									}
								}
							}
						}
					}
				}
			}
		}
		%this.dono = %this.pos1Quem.dono;
		
		case "donoDaPos0":
		echo("Dono da Área: " @ %this.pos0Quem.dono.persona.nome);
		%this.desprotegida = false;
		%this.limparCompartilhamento();
		if(!%this.pos0Quem.dono.mySimAreas.isMember(%this)){
			if(%this.oceano != 1){ //se não for um oceano
				%this.pos0Quem.dono.mySimAreas.add(%this);
			}
		}
		%this.dono = %this.pos0Quem.dono;
		if(%this.terreno $= "terra"){
			if(%this.ilha != 1){
				if(%this.myPos4List.getcount() > 0){
					%antigoDonoDosOvos = %this.myPos4List.getObject(0).dono;
					for(%i = 0; %i < %this.myPos4List.getcount(); %i++){
						%ovo = %this.myPos4List.getObject(0);
						if(%ovo.dono != %this.pos0Quem.dono){
							%this.converterOvos(%this.pos0Quem.dono);
						}
					}
					%antigoDonoDosOvos.mySimAreas.remove(%this);
				}
			}
		}
				
		case "MISTA":
		if(%this.dono !$= "MISTA"){ //se a área não era mista antes
			%this.limparCompartilhamento();
			if(isObject(%this.dono)){
				if(%this.dono.mySimAreas.isMember(%this)){ //se a área estiver no simAreas do antigo dono mesmo
					%this.dono.mySimAreas.remove(%this); //tira a área do simAreas do antigo dono
				}
			}
		}
		%this.dono = "MISTA"; //se era mista, continua, senão, fica!
		
		case "COMPARTILHADA":
		%this.desprotegida = false;
		if(!%this.pos1Quem.dono.mySimAreas.isMember(%this)){
			if(%this.oceano != 1){ //se não for um oceano
				%this.pos1Quem.dono.mySimAreas.add(%this);
			}
		}
		if(!%this.pos2Quem.dono.mySimAreas.isMember(%this)){
			if(%this.oceano != 1){ //se não for um oceano
				%this.pos2Quem.dono.mySimAreas.add(%this);
			}
		}
		for (%i = 0; %i < %this.myPos3List.getCount(); %i++){
			%tUnit = %this.myPos3List.getObject(%i);
			if(!%tUnit.dono.mySimAreas.isMember(%this)){
				if(%this.oceano != 1){ //se não for um oceano
					%tUnit.dono.mySimAreas.add(%this);
				}
			}
		}
		if(%this.terreno $= "terra"){
			for (%i = 0; %i < %this.myPos4List.getCount(); %i++){
				%tUnit = %this.myPos4List.getObject(%i);
				if(!%tUnit.dono.mySimAreas.isMember(%this)){
					if(%this.oceano != 1){ //se não for um oceano
						%tUnit.dono.mySimAreas.add(%this);
					}
				}
			}
		}
		%this.dono = "COMPARTILHADA";
		%this.dono1 = %this.pos1Quem.dono;
		if(isObject(%this.pos0Quem) && %this.pos0Quem.dono != %this.dono1){
			%this.dono2 = %this.pos0Quem.dono;
		} else if(isObject(%this.pos2Quem) && %this.pos2Quem.dono != %this.dono1){
			%this.dono2 = %this.pos2Quem.dono;
		} else {
			for (%i = 0; %i < %this.myPos3List.getCount(); %i++){
				%tUnit = %this.myPos3List.getObject(%i);
				if(%tUnit.dono != %this.dono1){
					%this.dono2 = %tUnit.dono;
					%i = %this.myPos3List.getCount();
					%tUnitFound = true;
				}
			}
			if(!%tUnitfound){
				for (%i = 0; %i < %this.myPos4List.getCount(); %i++){
					%tUnit = %this.myPos4List.getObject(%i);
					if(%tUnit.dono != %this.dono1){
						%this.dono2 = %tUnit.dono;
						%i = %this.myPos4List.getCount();
						%tUnitFound = true;
					}
				}
			}
		}
	}	
}

function Area::resolverMyStatus(%this){
	%status = %this.getMyStatus();
	%this.statusExec(%status);
	
	if($IAmServer)
		return;
			
	%this.clientAtualizarPosReservas();
}

function area::killBase(%this)
{
	%donoAntigo = %this.pos0Quem.dono;
	
	%this.pos0Quem.dono.mySimBases.remove(%this.pos0Quem);
	if(isObject(%this.pos0Quem.myReciclarEffect))
		%this.pos0Quem.myReciclarEffect.safeDelete();
	
	%this.pos0Quem.safeDelete();
	%this.pos0Quem = 0;
	%this.pos0Flag = false;
	
	if(%this.getTenhoGente)
		return;
		
	if(!%donoAntigo.mySimAreas.isMember(%this))
		return;
		
	%donoAntigo.mySimAreas.remove(%this);
}

function area::positionUnit(%this, %unit){
	%unit.areaAntiga = %unit.onde;
	%unit.onde = %this;
	
	if(%unit.class $= "soldado" || %unit.class $= "verme"){ //soldados
		if (%this.pos1Flag $= "nada"){
			%unit.moverPara(%this, "pos1");
		} else {
			if (%this.pos2Flag $= "nada"){
			 	%unit.moverPara(%this, "pos2");
			} else {
				%unit.moverPara(%this, "pos3");
			}
		}	
	}
	
	else if (%unit.class $= "tanque"){
		if (%this.pos1Flag $= "nada"){
			%unit.moverPara(%this, "pos1");
		} else if (%this.pos1Flag $= "soldado" || %this.pos1Flag $= "verme"){
			%unitAntiga = %this.pos1Quem;
			if(%this.pos2Flag $= "nada"){
				%unitAntiga.moverPara(%this, "pos2");
				%unit.moverPara(%this, "pos1");
			}else{
				%unitAntiga.moverPara(%this, "pos3");
				%unit.moverPara(%this, "pos1");
			}
		} else if (%this.pos1Flag $= "tanque" || %this.pos1Flag $= "lider" || %this.pos1Flag $= "zangao"){
			if (%this.pos2Flag $= "nada"){
				%unit.moverPara(%this, "pos2");
			} else if (%this.pos2Flag $= "soldado"){
				%unitAntiga = %this.pos2Quem;
				%unitAntiga.moverPara(%this, "pos3");
				%unit.moverPara(%this, "pos2");
			} else {
				%unit.moverPara(%this, "pos4");
			}
		}
	}
	
	else if (%unit.class $= "navio" || %unit.class $= "cefalok"){
		if (%this.pos1Flag $= "nada"){
			%unit.moverPara(%this, "pos1");
		} else {
			if (%this.pos2Flag $= "nada"){
				%unit.moverPara(%this, "pos2");
			} else {
				if(isObject(%unit.myTransporte)){
					if(%unit.myTransporte.getCount() > 0){
						if (%this.pos1Quem.myTransporte.getCount() == 0){
							%unitAntiga = %this.pos1Quem;
							%unitAntiga.pos = "pos3";
							%this.myPos3List.add(%unitAntiga);
							%unitAntiga.moveToLoc(%this.pos3);
							%unit.moverPara(%this, "pos1");
						} else if (%this.pos2Quem.myTransporte.getCount() == 0){
							%unitAntiga = %this.pos2Quem;
							%unitAntiga.pos = "pos3";
							%this.myPos3List.add(%unitAntiga);
							%unitAntiga.moveToLoc(%this.pos3);
							%unit.moverPara(%this, "pos2");
						} else {
							%unit.moverPara(%this, "pos3");
						}
					} else {
						%unit.moverPara(%this, "pos3");
					}
				} else {
					%unit.moverPara(%this, "pos3");
				}
			}
		}
		
	}
	
	else if (%unit.class $= "lider" || %unit.class $= "zangao"){
		if (%this.pos1Flag $= "nada"){
			%unit.moverPara(%this, "pos1");
		} else {
			if (%this.pos1Flag $= "soldado" || %this.pos1Flag $= "navio" || %this.pos1Flag $= "verme" || %this.pos1Flag $= "cefalok"){
				if(%this.pos2Flag $= "nada"){
					%unitAntiga = %this.pos1Quem;
					%unitAntiga.moverPara(%this, "pos2");
					%unit.moverPara(%this, "pos1");
				}else{
					%unitAntiga = %this.pos1Quem;
					%unitAntiga.moverPara(%this, "pos3");
					%unit.moverPara(%this, "pos1");
				}
			} else if (%this.pos1Flag $= "tanque"){
				if (%this.pos2Flag $= "nada"){
					%unitAntiga = %this.pos1Quem;
					%unitAntiga.moverPara(%this, "pos2");
					%unit.moverPara(%this, "pos1");
				} else if (%this.pos2Flag $= "soldado" || %this.pos2Flag $= "verme" || %this.pos2Flag $= "cefalok"){
					%unitAntiga1 = %this.pos1Quem;
					%unitAntiga2 = %this.pos2Quem;
					%unitAntiga1.moverPara(%this, "pos2");
					%unitAntiga2.moverPara(%this, "pos3");
					%unit.moverPara(%this, "pos1");
				} else if (%this.pos2Flag $= "tanque" || %this.pos2Flag $= "lider"){
					%unitAntiga = %this.pos1Quem;
					%unitAntiga.moverPara(%this, "pos4");
					%unit.moverPara(%this, "pos1");
				}			
			} else if (%this.pos1Flag $= "lider" || %this.pos1Flag $= "zangao"){
				if (%this.pos2Flag $= "nada"){
					%unit.moverPara(%this, "pos2");
				} else if (%this.pos2Flag $= "soldado" || %this.pos2Flag $= "navio" || %this.pos2Flag $= "verme" || %this.pos2Flag $= "cefalok"){
					%unitAntiga1 = %this.pos1Quem;
					%unitAntiga2 = %this.pos2Quem;
					%unitAntiga1.moverPara(%this, "pos2");
					%unitAntiga2.moverPara(%this, "pos3");
					%unit.moverPara(%this, "pos1");
				} else if (%this.pos2Flag $= "tanque"){
					%unitAntiga1 = %this.pos1Quem;
					%unitAntiga2 = %this.pos2Quem;
					%unitAntiga1.moverPara(%this, "pos2");
					%unitAntiga2.moverPara(%this, "pos4");
					%unit.moverPara(%this, "pos1");
				} else if (%this.pos2Flag $= "lider" || %this.pos2Flag $= "zangao"){
					%unit.moverPara(%this, "pos3");
				}				
			}
		}
	}
	
	else if (%unit.class $= "rainha"){
		if (%this.pos0Flag == false){
			%unit.moverPara(%this, "pos0");
		}
	}
	
	else if (%unit.class $= "ovo"){
		%unit.moverPara(%this, "pos4");
	}
	
	%this.desprotegida = false;
}



/////////////////
function Area::createCopy(%this, %jogo){
    %copy = new t2dStaticSprite(){
    	class = "Area"; 
		dono = 0; // Começa pertencendo a ninguém
		desprotegida = false; //não está desprotegida porque nem tem Base ainda
		pos0Quem = "nada"; //não há ninguém nas posições fortes
		pos1Quem = "nada";
		pos2Quem = "nada";
		pos0Flag = false; //true ou false mesmo, significando que tem ou não tem base lá.
		pos1Flag = "nada"; //as positionFlags não guardam 0 ou 1, mas sim [a classe da peça] ou ["nada"];
		pos2Flag = "nada";
		terreno = %this.terreno;
		myName = %this.getName();
		myDesastre = %this.myDesastre;
		ilha = %this.ilha;
		oceano = %this.oceano;
		myGame = %jogo;
		size = "0.1 0.1"; //não sei se faz diferença, mas se fizer, é bão... (o tamanho default é "10 10")
    };
	//SimSets das posições reservas:
	%copy.myPos3List = new SimSet();
	
	if(%copy.terreno !$= "mar"){
		if(%copy.ilha !$= "1"){
			%copy.myPos4List = new SimSet();
		}
	}

	
   	return %copy;
}
///////////////
//SERVER ONLY:
//
function Area::setMyFronteiras(%this){
	%this.myFronteiras = new SimSet();	
	
	switch$(%this.myName){
		case "saoPaulo":
		%this.myFronteiras.add(%this.myGame.argentina);
		%this.myFronteiras.add(%this.myGame.bolivia);
		%this.myFronteiras.add(%this.myGame.manaus);
		%this.myFronteiras.add(%this.myGame.bTodosOsSantos);
		%this.myFronteiras.add(%this.myGame.bGuanabara);
		
		case "manaus":
		%this.myFronteiras.add(%this.myGame.saoPaulo);
		%this.myFronteiras.add(%this.myGame.bolivia);
		%this.myFronteiras.add(%this.myGame.venezuela);
		%this.myFronteiras.add(%this.myGame.bMarDoCaribe);
		%this.myFronteiras.add(%this.myGame.bTodosOsSantos);
		
		case "argentina":
		%this.myFronteiras.add(%this.myGame.saoPaulo);
		%this.myFronteiras.add(%this.myGame.bolivia);
		%this.myFronteiras.add(%this.myGame.bEstreitoDeMagalhaes);
		%this.myFronteiras.add(%this.myGame.bGuanabara);
		
		case "bolivia":
		%this.myFronteiras.add(%this.myGame.saoPaulo);
		%this.myFronteiras.add(%this.myGame.argentina);
		%this.myFronteiras.add(%this.myGame.venezuela);
		%this.myFronteiras.add(%this.myGame.manaus);
		%this.myFronteiras.add(%this.myGame.bEstreitoDeMagalhaes);
		%this.myFronteiras.add(%this.myGame.bDeLima);
		%this.myFronteiras.add(%this.myGame.bGolfoDoPanama);
		
		case "venezuela":
		%this.myFronteiras.add(%this.myGame.manaus);
		%this.myFronteiras.add(%this.myGame.bolivia);
		%this.myFronteiras.add(%this.myGame.mexico);
		%this.myFronteiras.add(%this.myGame.bMarDoCaribe);
		%this.myFronteiras.add(%this.myGame.bGolfoDoPanama);

		case "mexico":
		%this.myFronteiras.add(%this.myGame.venezuela);
		%this.myFronteiras.add(%this.myGame.losAngeles);
		%this.myFronteiras.add(%this.myGame.houston);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMexico);
		%this.myFronteiras.add(%this.myGame.bMarDoCaribe);
		%this.myFronteiras.add(%this.myGame.bGolfoDoPanama);
		%this.myFronteiras.add(%this.myGame.bGolfoDaCalifornia);
		
		case "losAngeles":
		%this.myFronteiras.add(%this.myGame.mexico);
		%this.myFronteiras.add(%this.myGame.houston);
		%this.myFronteiras.add(%this.myGame.novaYork);
		%this.myFronteiras.add(%this.myGame.toronto);
		%this.myFronteiras.add(%this.myGame.vancouver);
		%this.myFronteiras.add(%this.myGame.bGolfoDaCalifornia);
		%this.myFronteiras.add(%this.myGame.bDeHecate);
		
		case "houston":
		%this.myFronteiras.add(%this.myGame.mexico);
		%this.myFronteiras.add(%this.myGame.losAngeles);
		%this.myFronteiras.add(%this.myGame.novaYork);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMexico);

		case "novaYork":
		%this.myFronteiras.add(%this.myGame.houston);
		%this.myFronteiras.add(%this.myGame.losAngeles);
		%this.myFronteiras.add(%this.myGame.toronto);
		%this.myFronteiras.add(%this.myGame.montreal);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMaine);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMexico);

		case "vancouver":
		%this.myFronteiras.add(%this.myGame.losAngeles);
		%this.myFronteiras.add(%this.myGame.toronto);
		%this.myFronteiras.add(%this.myGame.bakerlake);
		%this.myFronteiras.add(%this.myGame.bDeHecate);
		
		case "toronto":
		%this.myFronteiras.add(%this.myGame.novaYork);
		%this.myFronteiras.add(%this.myGame.losAngeles);
		%this.myFronteiras.add(%this.myGame.vancouver);
		%this.myFronteiras.add(%this.myGame.bakerlake);
		%this.myFronteiras.add(%this.myGame.montreal);
		%this.myFronteiras.add(%this.myGame.bDeHudson);
		
		case "montreal":
		%this.myFronteiras.add(%this.myGame.novaYork);
		%this.myFronteiras.add(%this.myGame.toronto);
		%this.myFronteiras.add(%this.myGame.bDeHudson);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMaine);
		
		case "bakerlake":
		%this.myFronteiras.add(%this.myGame.toronto);
		%this.myFronteiras.add(%this.myGame.vancouver);
		%this.myFronteiras.add(%this.myGame.alaska);
		%this.myFronteiras.add(%this.myGame.bDeHudson);
		%this.myFronteiras.add(%this.myGame.bDeBaffin);
		%this.myFronteiras.add(%this.myGame.bDeHecate);
		
		case "alaska":
		%this.myFronteiras.add(%this.myGame.bakerlake);
		%this.myFronteiras.add(%this.myGame.bDeHecate);
		%this.myFronteiras.add(%this.myGame.bEstreitoDeBering);
		%this.myFronteiras.add(%this.myGame.bDeBaffin);
		
		case "groenlandia":
		%this.myFronteiras.add(%this.myGame.bEstreitoDaDinamarca);
		%this.myFronteiras.add(%this.myGame.bMarDaGroenlandia);
		%this.myFronteiras.add(%this.myGame.bDeBaffin);
		%this.myFronteiras.add(%this.myGame.bDeHudson);
		
		case "londres":
		%this.myFronteiras.add(%this.myGame.bMarNordico);
		
		case "estocolmo":
		%this.myFronteiras.add(%this.myGame.moscou);
		%this.myFronteiras.add(%this.myGame.bMarDaNoruega);
		
		case "comunidadeEuropeia":
		%this.myFronteiras.add(%this.myGame.moscou);
		%this.myFronteiras.add(%this.myGame.bagda);
		%this.myFronteiras.add(%this.myGame.bMarMediterraneo);
		%this.myFronteiras.add(%this.myGame.bMarNordico);
		%this.myFronteiras.add(%this.myGame.bMarDaNoruega);
		
		case "marrakesh":
		%this.myFronteiras.add(%this.myGame.cairo);
		%this.myFronteiras.add(%this.myGame.kinshasa);
		%this.myFronteiras.add(%this.myGame.bDeLuanda);
		%this.myFronteiras.add(%this.myGame.bDeDakar);
		%this.myFronteiras.add(%this.myGame.bMarMediterraneo);
		
		case "cairo":
		%this.myFronteiras.add(%this.myGame.kinshasa);
		%this.myFronteiras.add(%this.myGame.marrakesh);
		%this.myFronteiras.add(%this.myGame.bagda);
		%this.myFronteiras.add(%this.myGame.bMarDaArabia);
		%this.myFronteiras.add(%this.myGame.bDeMogadicio);
		%this.myFronteiras.add(%this.myGame.bMarMediterraneo);
		
		case "kinshasa":
		%this.myFronteiras.add(%this.myGame.cidadeDoCabo);
		%this.myFronteiras.add(%this.myGame.marrakesh);
		%this.myFronteiras.add(%this.myGame.cairo);
		%this.myFronteiras.add(%this.myGame.bDeMogadicio);
		%this.myFronteiras.add(%this.myGame.bDeLuanda);
		
		case "cidadeDoCabo":
		%this.myFronteiras.add(%this.myGame.kinshasa);
		%this.myFronteiras.add(%this.myGame.bDeMogadicio);
		%this.myFronteiras.add(%this.myGame.bCaboDaBoaEsperanca);
		%this.myFronteiras.add(%this.myGame.bDeLuanda);
		
		case "bagda":
		%this.myFronteiras.add(%this.myGame.cairo);
		%this.myFronteiras.add(%this.myGame.comunidadeEuropeia);
		%this.myFronteiras.add(%this.myGame.moscou);
		%this.myFronteiras.add(%this.myGame.cabul);
		%this.myFronteiras.add(%this.myGame.bMarDaArabia);
		%this.myFronteiras.add(%this.myGame.bMarMediterraneo);
		
		case "cabul":
		%this.myFronteiras.add(%this.myGame.bagda);
		%this.myFronteiras.add(%this.myGame.cazaquistao);
		%this.myFronteiras.add(%this.myGame.lhasa);
		%this.myFronteiras.add(%this.myGame.india);
		%this.myFronteiras.add(%this.myGame.bMarDaArabia);
	
		case "india":
		%this.myFronteiras.add(%this.myGame.cabul);
		%this.myFronteiras.add(%this.myGame.lhasa);
		%this.myFronteiras.add(%this.myGame.vietna);
		%this.myFronteiras.add(%this.myGame.bDeBengala);
		%this.myFronteiras.add(%this.myGame.bMarDaArabia);
		
		case "vietna":
		%this.myFronteiras.add(%this.myGame.india);
		%this.myFronteiras.add(%this.myGame.lhasa);
		%this.myFronteiras.add(%this.myGame.xangai);
		%this.myFronteiras.add(%this.myGame.bMarDaChina);
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		%this.myFronteiras.add(%this.myGame.bDeBengala);
		
		case "xangai":
		%this.myFronteiras.add(%this.myGame.vietna);
		%this.myFronteiras.add(%this.myGame.lhasa);
		%this.myFronteiras.add(%this.myGame.pequim);
		%this.myFronteiras.add(%this.myGame.bMarDachina);
		
		case "lhasa":
		%this.myFronteiras.add(%this.myGame.vietna);
		%this.myFronteiras.add(%this.myGame.india);
		%this.myFronteiras.add(%this.myGame.cabul);
		%this.myFronteiras.add(%this.myGame.cazaquistao);
		%this.myFronteiras.add(%this.myGame.mongolia);
		%this.myFronteiras.add(%this.myGame.pequim);
		%this.myFronteiras.add(%this.myGame.xangai);
		%this.myFronteiras.add(%this.myGame.omsk);
		
		case "pequim":
		%this.myFronteiras.add(%this.myGame.xangai);
		%this.myFronteiras.add(%this.myGame.lhasa);
		%this.myFronteiras.add(%this.myGame.mongolia);
		%this.myFronteiras.add(%this.myGame.omsk);
		%this.myFronteiras.add(%this.myGame.magadan);
		%this.myFronteiras.add(%this.myGame.bMarDoJapao);
		%this.myFronteiras.add(%this.myGame.bMarDaChina);
		
		case "mongolia":
		%this.myFronteiras.add(%this.myGame.pequim);
		%this.myFronteiras.add(%this.myGame.lhasa);
		%this.myFronteiras.add(%this.myGame.omsk);
		
		case "cazaquistao":
		%this.myFronteiras.add(%this.myGame.cabul);
		%this.myFronteiras.add(%this.myGame.moscou);
		%this.myFronteiras.add(%this.myGame.kirov);
		%this.myFronteiras.add(%this.myGame.omsk);
		%this.myFronteiras.add(%this.myGame.lhasa);

		case "moscou":
		%this.myFronteiras.add(%this.myGame.estocolmo);
		%this.myFronteiras.add(%this.myGame.comunidadeEuropeia);
		%this.myFronteiras.add(%this.myGame.bagda);
		%this.myFronteiras.add(%this.myGame.cazaquistao);
		%this.myFronteiras.add(%this.myGame.kirov);
		%this.myFronteiras.add(%this.myGame.bMarDeKara);
		%this.myFronteiras.add(%this.myGame.bMarDaNoruega);
		
		case "kirov":
		%this.myFronteiras.add(%this.myGame.cazaquistao);
		%this.myFronteiras.add(%this.myGame.moscou);
		%this.myFronteiras.add(%this.myGame.omsk);
		%this.myFronteiras.add(%this.myGame.bMarDeKara);
		%this.myFronteiras.add(%this.myGame.bMarDaSiberia);
		
		case "omsk":
		%this.myFronteiras.add(%this.myGame.magadan);
		%this.myFronteiras.add(%this.myGame.pequim);
		%this.myFronteiras.add(%this.myGame.mongolia);
		%this.myFronteiras.add(%this.myGame.lhasa);
		%this.myFronteiras.add(%this.myGame.cazaquistao);
		%this.myFronteiras.add(%this.myGame.kirov);
		%this.myFronteiras.add(%this.myGame.bMarDaSiberia);
		
		case "magadan":
		%this.myFronteiras.add(%this.myGame.pequim);
		%this.myFronteiras.add(%this.myGame.omsk);
		%this.myFronteiras.add(%this.myGame.bMarChukchi);
		%this.myFronteiras.add(%this.myGame.bMarDoJapao);
		%this.myFronteiras.add(%this.myGame.cazaquistao);
		%this.myFronteiras.add(%this.myGame.kirov);
		%this.myFronteiras.add(%this.myGame.bMarDaSiberia);
		
		case "japao":
		%this.myFronteiras.add(%this.myGame.bMarDoJapao);
		
		case "sumatra":
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		
		case "borneo":
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		
		case "novaGuine":
		%this.myFronteiras.add(%this.myGame.bMarDeCoral);
		
		case "perth":
		%this.myFronteiras.add(%this.myGame.sydney);
		%this.myFronteiras.add(%this.myGame.bGrandeBaiaAustraliana);
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		
		case "sydney":
		%this.myFronteiras.add(%this.myGame.perth);
		%this.myFronteiras.add(%this.myGame.bGrandeBaiaAustraliana);
		%this.myFronteiras.add(%this.myGame.bMarDeCoral);
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		//////////////
		/////////////		
		case "bEstreitoDeMagalhaes":
		%this.myFronteiras.add(%this.myGame.argentina);
		%this.myFronteiras.add(%this.myGame.bolivia);
		%this.myFronteiras.add(%this.myGame.bDeLima);
		%this.myFronteiras.add(%this.myGame.bGuanabara);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoSul);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOcidental);
	
		case "bDeLima":
		%this.myFronteiras.add(%this.myGame.bolivia);
		%this.myFronteiras.add(%this.myGame.bGolfoDoPanama);
		%this.myFronteiras.add(%this.myGame.bEstreitoDeMagalhaes);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOcidental);

		case "bGolfoDoPanama":
		%this.myFronteiras.add(%this.myGame.mexico);
		%this.myFronteiras.add(%this.myGame.venezuela);
		%this.myFronteiras.add(%this.myGame.bolivia);
		%this.myFronteiras.add(%this.myGame.bDeLima);
		%this.myFronteiras.add(%this.myGame.bGolfoDaCalifornia);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOcidental);
		
		case "bGolfoDaCalifornia":
		%this.myFronteiras.add(%this.myGame.losAngeles);
		%this.myFronteiras.add(%this.myGame.mexico);
		%this.myFronteiras.add(%this.myGame.bDeHecate);
		%this.myFronteiras.add(%this.myGame.bGolfoDoPanama);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOcidental);

		case "bDeHecate":
		%this.myFronteiras.add(%this.myGame.alaska);
		%this.myFronteiras.add(%this.myGame.bakerlake);
		%this.myFronteiras.add(%this.myGame.vancouver);
		%this.myFronteiras.add(%this.myGame.losAngeles);
		%this.myFronteiras.add(%this.myGame.bGolfoDaCalifornia);
		%this.myFronteiras.add(%this.myGame.bEstreitoDeBering);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOcidental);
		
		case "bEstreitoDeBering":
		%this.myFronteiras.add(%this.myGame.alaska);
		%this.myFronteiras.add(%this.myGame.bDeHecate);
		%this.myFronteiras.add(%this.myGame.bDeBaffin);
		%this.myFronteiras.add(%this.myGame.bMarChukchi);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOcidental);

		case "bDeBaffin":
		%this.myFronteiras.add(%this.myGame.alaska);
		%this.myFronteiras.add(%this.myGame.bakerlake);
		%this.myFronteiras.add(%this.myGame.groenlandia);
		%this.myFronteiras.add(%this.myGame.bEstreitoDeBering);
		%this.myFronteiras.add(%this.myGame.bMarDaGroenlandia);
		%this.myFronteiras.add(%this.myGame.bDeHudson);

		case "bMarDaGroenlandia":
		%this.myFronteiras.add(%this.myGame.groenlandia);
		%this.myFronteiras.add(%this.myGame.bEstreitoDaDinamarca);
		%this.myFronteiras.add(%this.myGame.bDeBaffin);
		%this.myFronteiras.add(%this.myGame.oceanoArtico);
		
		case "bEstreitoDaDinamarca":
		%this.myFronteiras.add(%this.myGame.groenlandia);
		%this.myFronteiras.add(%this.myGame.bMarNordico);
		%this.myFronteiras.add(%this.myGame.bDeHudson);
		%this.myFronteiras.add(%this.myGame.bMarDaGroenlandia);
		%this.myFronteiras.add(%this.myGame.oceanoArtico);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoNorte);
		
		case "bDeHudson":
		%this.myFronteiras.add(%this.myGame.montreal);
		%this.myFronteiras.add(%this.myGame.toronto);
		%this.myFronteiras.add(%this.myGame.bakerlake);
		%this.myFronteiras.add(%this.myGame.groenlandia);
		%this.myFronteiras.add(%this.myGame.bDeBaffin);
		%this.myFronteiras.add(%this.myGame.bEstreitoDaDinamarca);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMaine);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoNorte);
		
		case "bGolfoDoMaine":
		%this.myFronteiras.add(%this.myGame.novaYork);
		%this.myFronteiras.add(%this.myGame.montreal);
		%this.myFronteiras.add(%this.myGame.bDeHudson);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMexico);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoNorte);
		
		case "bGolfoDoMexico":
		%this.myFronteiras.add(%this.myGame.mexico);
		%this.myFronteiras.add(%this.myGame.houston);
		%this.myFronteiras.add(%this.myGame.novaYork);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMaine);
		%this.myFronteiras.add(%this.myGame.bMarDoCaribe);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoNorte);
		
		case "bMarDoCaribe":
		%this.myFronteiras.add(%this.myGame.mexico);
		%this.myFronteiras.add(%this.myGame.venezuela);
		%this.myFronteiras.add(%this.myGame.manaus);
		%this.myFronteiras.add(%this.myGame.bTodosOsSantos);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMexico);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoNorte);
		
		case "bTodosOsSantos":
		%this.myFronteiras.add(%this.myGame.manaus);
		%this.myFronteiras.add(%this.myGame.saoPaulo);
		%this.myFronteiras.add(%this.myGame.bMarDoCaribe);
		%this.myFronteiras.add(%this.myGame.bDeDakar);
		%this.myFronteiras.add(%this.myGame.bGuanabara);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoNorte);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoSul);
		
		case "bGuanabara":
		%this.myFronteiras.add(%this.myGame.saoPaulo);
		%this.myFronteiras.add(%this.myGame.argentina);
		%this.myFronteiras.add(%this.myGame.bTodosOsSantos);
		%this.myFronteiras.add(%this.myGame.bEstreitoDeMagalhaes);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoSul);
		
		case "bGrandeBaiaAustraliana":
		%this.myFronteiras.add(%this.myGame.perth);
		%this.myFronteiras.add(%this.myGame.sydney);
		%this.myFronteiras.add(%this.myGame.bMarDeCoral);
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		%this.myFronteiras.add(%this.myGame.oceanoIndico);
		
		case "bMarDeCoral":
		%this.myFronteiras.add(%this.myGame.sydney);
		%this.myFronteiras.add(%this.myGame.novaGuine);
		%this.myFronteiras.add(%this.myGame.bGrandeBaiaAustraliana);
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		%this.myFronteiras.add(%this.myGame.oceanoIndico);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOriental);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOcidental);
		
		case "bMarDeJava":
		%this.myFronteiras.add(%this.myGame.vietna);
		%this.myFronteiras.add(%this.myGame.sumatra);
		%this.myFronteiras.add(%this.myGame.borneo);
		%this.myFronteiras.add(%this.myGame.perth);
		%this.myFronteiras.add(%this.myGame.sydney);
		%this.myFronteiras.add(%this.myGame.bGrandeBaiaAustraliana);
		%this.myFronteiras.add(%this.myGame.bMarDeCoral);
		%this.myFronteiras.add(%this.myGame.bDeBengala);
		%this.myFronteiras.add(%this.myGame.bMarDaChina);
		%this.myFronteiras.add(%this.myGame.oceanoIndico);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOriental);
		
		case "bMarDaChina":
		%this.myFronteiras.add(%this.myGame.pequim);
		%this.myFronteiras.add(%this.myGame.xangai);
		%this.myFronteiras.add(%this.myGame.vietna);
		%this.myFronteiras.add(%this.myGame.bMarDoJapao);
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOriental);
		
		case "bMarDoJapao":
		%this.myFronteiras.add(%this.myGame.japao);
		%this.myFronteiras.add(%this.myGame.pequim);
		%this.myFronteiras.add(%this.myGame.magadan);
		%this.myFronteiras.add(%this.myGame.bMarChukchi);
		%this.myFronteiras.add(%this.myGame.bMarDaChina);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOriental);
		
		case "bMarChukchi":
		%this.myFronteiras.add(%this.myGame.magadan);
		%this.myFronteiras.add(%this.myGame.bMarDoJapao);
		%this.myFronteiras.add(%this.myGame.bMarDaSiberia);
		%this.myFronteiras.add(%this.myGame.bEstreitoDeBering);
		%this.myFronteiras.add(%this.myGame.oceanoArtico);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOriental);
		
		case "bMarDaSiberia":
		%this.myFronteiras.add(%this.myGame.magadan);
		%this.myFronteiras.add(%this.myGame.omsk);
		%this.myFronteiras.add(%this.myGame.kirov);
		%this.myFronteiras.add(%this.myGame.bMarChukchi);
		%this.myFronteiras.add(%this.myGame.bMarDeKara);
		%this.myFronteiras.add(%this.myGame.oceanoArtico);
		
		case "bMarDeKara":
		%this.myFronteiras.add(%this.myGame.kirov);
		%this.myFronteiras.add(%this.myGame.moscou);
		%this.myFronteiras.add(%this.myGame.bMarDaSiberia);
		%this.myFronteiras.add(%this.myGame.bMarDaNoruega);
		%this.myFronteiras.add(%this.myGame.oceanoArtico);
		
		case "bMarDaNoruega":
		%this.myFronteiras.add(%this.myGame.estocolmo);
		%this.myFronteiras.add(%this.myGame.moscou);
		%this.myFronteiras.add(%this.myGame.comunidadeEuropeia);
		%this.myFronteiras.add(%this.myGame.bMarDeKara);
		%this.myFronteiras.add(%this.myGame.bMarNordico);
		%this.myFronteiras.add(%this.myGame.oceanoArtico);
		
		case "bMarNordico":
		%this.myFronteiras.add(%this.myGame.londres);
		%this.myFronteiras.add(%this.myGame.comunidadeEuropeia);
		%this.myFronteiras.add(%this.myGame.bEstreitoDaDinamarca);
		%this.myFronteiras.add(%this.myGame.bMarDaNoruega);
		%this.myFronteiras.add(%this.myGame.bMarMediterraneo);
		%this.myFronteiras.add(%this.myGame.oceanoArtico);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoNorte);
		
		case "bMarMediterraneo":
		%this.myFronteiras.add(%this.myGame.comunidadeEuropeia);
		%this.myFronteiras.add(%this.myGame.bagda);
		%this.myFronteiras.add(%this.myGame.cairo);
		%this.myFronteiras.add(%this.myGame.marrakesh);
		%this.myFronteiras.add(%this.myGame.bMarNordico);
		%this.myFronteiras.add(%this.myGame.bDeDakar);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoNorte);
		
		case "bDeDakar":
		%this.myFronteiras.add(%this.myGame.marrakesh);
		%this.myFronteiras.add(%this.myGame.bMarMediterraneo);
		%this.myFronteiras.add(%this.myGame.bTodosOsSantos);
		%this.myFronteiras.add(%this.myGame.bDeLuanda);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoNorte);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoSul);
		
		case "bDeLuanda":
		%this.myFronteiras.add(%this.myGame.marrakesh);
		%this.myFronteiras.add(%this.myGame.kinshasa);
		%this.myFronteiras.add(%this.myGame.cidadeDoCabo);
		%this.myFronteiras.add(%this.myGame.bDeDakar);
		%this.myFronteiras.add(%this.myGame.bCaboDaBoaEsperanca);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoSul);
		
		case "bCaboDaBoaEsperanca":
		%this.myFronteiras.add(%this.myGame.cidadeDoCabo);
		%this.myFronteiras.add(%this.myGame.bDeLuanda);
		%this.myFronteiras.add(%this.myGame.bDeMogadicio);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoSul);
		%this.myFronteiras.add(%this.myGame.oceanoIndico);

		case "bDeMogadicio":
		%this.myFronteiras.add(%this.myGame.cidadeDoCabo);
		%this.myFronteiras.add(%this.myGame.kinshasa);
		%this.myFronteiras.add(%this.myGame.cairo);
		%this.myFronteiras.add(%this.myGame.bMarDaArabia);
		%this.myFronteiras.add(%this.myGame.bCaboDaBoaEsperanca);
		%this.myFronteiras.add(%this.myGame.oceanoIndico);
		
		case "bMarDaArabia":
		%this.myFronteiras.add(%this.myGame.cairo);
		%this.myFronteiras.add(%this.myGame.bagda);
		%this.myFronteiras.add(%this.myGame.cabul);
		%this.myFronteiras.add(%this.myGame.india);
		%this.myFronteiras.add(%this.myGame.bDeMogadicio);
		%this.myFronteiras.add(%this.myGame.bDeBengala);
		%this.myFronteiras.add(%this.myGame.oceanoIndico);
		
		case "bDeBengala":
		%this.myFronteiras.add(%this.myGame.india);
		%this.myFronteiras.add(%this.myGame.vietna);
		%this.myFronteiras.add(%this.myGame.bMarDaArabia);
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		%this.myFronteiras.add(%this.myGame.oceanoIndico);
		/////////////
		////////////
		case "oceanoPacificoOcidental":
		%this.myFronteiras.add(%this.myGame.bEstreitoDeMagalhaes);
		%this.myFronteiras.add(%this.myGame.bDeLima);
		%this.myFronteiras.add(%this.myGame.bGolfoDoPanama);
		%this.myFronteiras.add(%this.myGame.bGolfoDaCalifornia);
		%this.myFronteiras.add(%this.myGame.bDeHecate);
		%this.myFronteiras.add(%this.myGame.bEstreitoDeBering);
		%this.myFronteiras.add(%this.myGame.bMarDeCoral);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOriental);
		%this.myFronteiras.add(%this.myGame.oceanoIndico);
		
		case "oceanoAtlanticoNorte":
		%this.myFronteiras.add(%this.myGame.bTodosOsSantos);
		%this.myFronteiras.add(%this.myGame.bMarDoCaribe);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMexico);
		%this.myFronteiras.add(%this.myGame.bGolfoDoMaine);
		%this.myFronteiras.add(%this.myGame.bDeHudson);
		%this.myFronteiras.add(%this.myGame.bEstreitoDaDinamarca);
		%this.myFronteiras.add(%this.myGame.bMarNordico);
		%this.myFronteiras.add(%this.myGame.bMarMediterraneo);
		%this.myFronteiras.add(%this.myGame.bDeDakar);
		
		case "oceanoAtlanticoSul":
		%this.myFronteiras.add(%this.myGame.bTodosOsSantos);
		%this.myFronteiras.add(%this.myGame.bDeDakar);
		%this.myFronteiras.add(%this.myGame.bDeLuanda);
		%this.myFronteiras.add(%this.myGame.bCaboDaBoaEsperanca);
		%this.myFronteiras.add(%this.myGame.bEstreitoDeMagalhaes);
		%this.myFronteiras.add(%this.myGame.bGuanabara);
		%this.myFronteiras.add(%this.myGame.oceanoIndico);

		case "oceanoIndico":
		%this.myFronteiras.add(%this.myGame.bCaboDaBoaEsperanca);
		%this.myFronteiras.add(%this.myGame.bDeMogadicio);
		%this.myFronteiras.add(%this.myGame.bMarDaArabia);
		%this.myFronteiras.add(%this.myGame.bDeBengala);
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		%this.myFronteiras.add(%this.myGame.bGrandeBaiaAustraliana);
		%this.myFronteiras.add(%this.myGame.bMarDeCoral);
		%this.myFronteiras.add(%this.myGame.oceanoAtlanticoSul);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOcidental);
		
		case "oceanoPacificoOriental":
		%this.myFronteiras.add(%this.myGame.bMarDeCoral);
		%this.myFronteiras.add(%this.myGame.bMarDeJava);
		%this.myFronteiras.add(%this.myGame.bMarDaChina);
		%this.myFronteiras.add(%this.myGame.bMarDoJapao);
		%this.myFronteiras.add(%this.myGame.bMarChukchi);
		%this.myFronteiras.add(%this.myGame.oceanoPacificoOcidental);

		case "oceanoArtico":
		%this.myFronteiras.add(%this.myGame.bMarDaGroenlandia);
		%this.myFronteiras.add(%this.myGame.bEstreitoDaDinamarca);
		%this.myFronteiras.add(%this.myGame.bMarNordico);
		%this.myFronteiras.add(%this.myGame.bMarDaNoruega);
		%this.myFronteiras.add(%this.myGame.bMarDeKara);
		%this.myFronteiras.add(%this.myGame.bMarDaSiberia);
		%this.myFronteiras.add(%this.myGame.bMarChukchi);

	}
	//echo(%this.myFronteiras.getCount() @ " fronteiras registradas em: " @ %this.myName @ ", no jogo " @ %this.myGame.num);
}

//GULOK:

function area::converterOvos(%this, %novoDono){
	%jogo = %novoDono.jogo;
	for(%i = 0; %i < %this.myPos4List.getcount(); %i++){
		%ovo = %this.myPos4List.getObject(%i);
		if(%ovo.dono != %this.pos0Quem.dono){
			%ovo.dono = %novoDono;
			if(!$IAMServer){
				%ovo.setMyImage();
			}
		}
	}
}
