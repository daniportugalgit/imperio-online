//cores (7 ativas):
$vermelho = "1 0 0 1 vermelho"; 
$laranja = "1 0.59 0 1 laranja"; 
$amarelo = "1 1 0 1 amarelo"; 
$verde = "0 1 0 1 verde";  
$azul = "0 1 1 1 azul"; 
$indigo = "0 0.4 1 1 indigo"; 
$roxo = "0.78 0.4 0.78 1 roxo"; 
//$verdeMilitar = "0 0.51 0 1";
//$verdeEscuro = "0 0.78 0 1";
//$rosa = "1 0.59 1 1";
//$dourado = "1 0.78 0 1";
//$laranjaEscuro = "1 0.39 0 1"; 
//$marrom = "0.51 0 0 1";
//$azulPiscina = "0 1 0.59 1";
//$azulEscuro = "0 0 1 1";
//$branco = "1 1 1 1";
//$cinza = "0.78 0.78 0.78 1";


//esta função antiga pode ser usada no client;
//a nova é necessária no server
$playersCreated = 0;
function clientCreatePlayer(){
	%eval = "$player" @ $playersCreated + 1 SPC "= new ScriptObject(){";
		%eval = %eval @ "class = player;";
		%eval = %eval @ "id = player" @ $playersCreated + 1 @ ";";
		%eval = %eval @ "num = $playersCreated + 1;";
		%eval = %eval @ "brasil = false;";
		%eval = %eval @ "eua = false;";
		%eval = %eval @ "canada = false;";
		%eval = %eval @ "europa = false;";
		%eval = %eval @ "africa = false;";
		%eval = %eval @ "oriente = false;";
		%eval = %eval @ "russia = false;";
		%eval = %eval @ "china = false;";
		%eval = %eval @ "australia = false;";
		%eval = %eval @ "imperiais = 10;";
		%eval = %eval @ "minerios = 0;";
		%eval = %eval @ "petroleos = 0;";
		%eval = %eval @ "uranios = 0;";
		%eval = %eval @ "movimentos = 5;";
		%eval = %eval @ "obj1Completo = false;";
		%eval = %eval @ "obj2Completo = false;";
		%eval = %eval @ "pontos = 0;";
		%eval = %eval @ "imperio = false;";
		%eval = %eval @ "atacou = false;";
		%eval = %eval @ "qtsMatou = 0;";
		%eval = %eval @ "liderVive = 1;";
		%eval = %eval @ "ganhouOJogo = 0;";
		%eval = %eval @ "arrebatadorAgora = 0;";
		%eval = %eval @ "taMorto = false;";
	%eval = %eval @ "};";
	eval(%eval);
	
	//criando SimSets:
	%eval = "$player" @ $playersCreated + 1 @ "Areas = new SimSet(player" @ $playersCreated + 1 @ "Areas);";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ "SimInfo = new SimSet(player" @ $playersCreated + 1 @ "SimInfo);";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ "SimObj = new SimSet(player" @ $playersCreated + 1 @ "SimObj);";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ "SimExpl = new SimSet(player" @ $playersCreated + 1 @ "SimExpl);";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ "SimUnits = new SimSet(player" @ $playersCreated + 1 @ "SimUnits);";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ "SimBases = new SimSet(player" @ $playersCreated + 1 @ "SimBases);";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ "SimLideres = new SimSet(player" @ $playersCreated + 1 @ "SimLideres);";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ "SimRefinarias = new SimSet(player" @ $playersCreated + 1 @ "SimRefinarias);";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ "SimOvos = new SimSet(player" @ $playersCreated + 1 @ "SimOvos);";
	eval(%eval);
	
	//referenciando os SimSets:
	%eval = "$player" @ $playersCreated + 1 @ ".mySimAreas = $player" @ $playersCreated + 1 @ "Areas;";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ ".mySimInfo = $player" @ $playersCreated + 1 @ "SimInfo;";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ ".mySimObj = $player" @ $playersCreated + 1 @ "SimObj;";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ ".mySimExpl = $player" @ $playersCreated + 1 @ "SimExpl;";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ ".mySimUnits = $player" @ $playersCreated + 1 @ "SimUnits;";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ ".mySimBases = $player" @ $playersCreated + 1 @ "SimBases;";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ ".mySimLideres = $player" @ $playersCreated + 1 @ "SimLideres;";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ ".mySimRefinarias = $player" @ $playersCreated + 1 @ "SimRefinarias;";
	eval(%eval);
	%eval = "$player" @ $playersCreated + 1 @ ".mySimOvos = $player" @ $playersCreated + 1 @ "SimOvos;";
	eval(%eval);
	
	$playersCreated++;
}

//cria 4 players (depois chamarei createPlayer() apenas quando realmente precisar de um novo jogador):
function clientReStartPlayers(){
	for(%i = 1; %i < 7; %i++){
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		if(isObject(%player.mySimAreas))
			%player.delete();
	}
	$playersCreated = 0;
	
	for(%i = 1; %i < 7; %i++){
		clientCreatePlayer();	
	}
}

if($IAmServer == 0){
	clientReStartPlayers();
}


///////////////
//server:
function createPlayer(%jogo){
	%playersCreated = %jogo.simPlayers.getCount();
	%eval = "%jogo.player" @ %playersCreated + 1 SPC "= new ScriptObject(){";
		%eval = %eval @ "class = player;";
		%eval = %eval @ "id = player" @ %playersCreated + 1 @ ";";
		%eval = %eval @ "brasil = false;";
		%eval = %eval @ "eua = false;";
		%eval = %eval @ "canada = false;";
		%eval = %eval @ "europa = false;";
		%eval = %eval @ "africa = false;";
		%eval = %eval @ "oriente = false;";
		%eval = %eval @ "russia = false;";
		%eval = %eval @ "china = false;";
		%eval = %eval @ "australia = false;";
		%eval = %eval @ "imperiais = 10;";
		%eval = %eval @ "minerios = 0;";
		%eval = %eval @ "petroleos = 0;";
		%eval = %eval @ "uranios = 0;";
		%eval = %eval @ "movimentos = 5;";
		%eval = %eval @ "obj1Completo = false;";
		%eval = %eval @ "obj2Completo = false;";
		%eval = %eval @ "negInGame = 0;";
		%eval = %eval @ "pontos = 0;";
		%eval = %eval @ "imperio = false;";
		%eval = %eval @ "atacou = false;";
		%eval = %eval @ "qtsMatou = 0;";
		%eval = %eval @ "liderVive = 1;";
		%eval = %eval @ "ganhouOJogo = 0;";
		%eval = %eval @ "arrebatadorAgora = 0;";
		%eval = %eval @ "mineriosInvestidos = 0;";
		%eval = %eval @ "petroleosInvestidos = 0;";
		%eval = %eval @ "uraniosInvestidos = 0;";
		%eval = %eval @ "taMorto = false;";
		%eval = %eval @ "jogo = " @ %jogo @ ";";
		%eval = %eval @ "num = " @ %playersCreated + 1 @ ";";
	%eval = %eval @ "};";
	eval(%eval);
	
	%eval = "%estePlayer = %jogo.player" @ %playersCreated + 1 @ ";";
	eval(%eval);
	
	//criando SimSets:
	%estePlayer.mySimAreas = new SimSet(%jogo @ "player" @ %playersCreated + 1 @ "SimAreas");
	%estePlayer.mySimInfo = new SimSet(%jogo @ "player" @ %playersCreated + 1 @ "SimInfo");
	%estePlayer.mySimObj = new SimSet(%jogo @ "player" @ %playersCreated + 1 @ "SimObj");
	%estePlayer.mySimExpl = new SimSet(%jogo @ "player" @ %playersCreated + 1 @ "SimExpl");
	%estePlayer.mySimUnits = new SimSet(%jogo @ "player" @ %playersCreated + 1 @ "SimUnits");
	%estePlayer.mySimBases = new SimSet(%jogo @ "player" @ %playersCreated + 1 @ "SimBases");
	%estePlayer.mySimLideres = new SimSet(%jogo @ "player" @ %playersCreated + 1 @ "SimLideres");
	%estePlayer.mySimRefinarias = new SimSet(%jogo @ "player" @ %playersCreated + 1 @ "SimRefinarias");
	%estePlayer.mySimOvos = new SimSet(%jogo @ "player" @ %playersCreated + 1 @ "SimOvos");
		
	%jogo.simPlayers.add(%estePlayer);
}

///cria o observador:
function clientCriarPlayerObservador()
{
	$OBSERVADOR = new scriptObject()
	{
		id = "OBSERVADOR";
		num = 99;
	};	
}
clientCriarPlayerObservador();

function serverCriarObservador(%jogo)
{
	%jogo.playerObservador = new ScriptObject()
	{
		id = "OBSERVADOR";
		jogo = %jogo;
		num = 99;
	};	
}

function player::getArrebatador(%this)
{
	if(%this.obj1Completo && %this.obj2Completo)	
		return true;
		
	return false;
}