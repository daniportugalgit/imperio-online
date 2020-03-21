// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverCriarSala.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 26 de dezembro de 2007 11:53
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

$numDeSalasUniversal = 0;
//$statusSala = serverCriarStatusSalaObj();

function serverCriarSalaObj()
{
	$numDeSalasUniversal++;

	%sala = newSimObj("sala", $numDeSalasUniversal);
	%sala.num = $numDeSalasUniversal;
	%sala.simPersonas = new SimSet();
	
	// Valores Default da sala
	%sala.emDuplas = false;
	%sala.tipoDeJogo = "Classico"; 
	%sala.turno = 100;
	%sala.planeta = $planetaTerra;
	%sala.lotacao = 4;
	$serverSimSalas.add(%sala);
	
	return %sala;
}

function serverCmdSairDoAtrio(%client)
{
	%persona = %client.persona;
	$personasNoAtrio.remove(%persona); //remove a persona do átrio;
	%persona.atrioPagina = 1; //coloca o átrio na página inicial;
	serverAtualizarPersonasNoAtrioParaTodos();
}

function serverCmdAlterarAtrioPagina(%client, %pag)
{
	%persona = %client.persona;
	%persona.atrioPagina = %pag;
	serverCmdPegarRelacaoDeSalas(%client, "atrio");
}

function serverCmdCriarSala(%client)
{
	
	%sala = serverCriarSalaObj();
	
	// TODO: Trocar booleanos por status
	%sala.ocupada = false;
	%sala.status = $statusSala.Livre;
			
	%persona = %client.persona; //pega a persona do client que criou a sala;
	
	%sala.adicionarPersona(%persona);

	commandToClient(%client, 'criarSala', %sala.num); //coloca o client na sala, como criador:
				
	serverAtualizarAtrioParaTodos();
		
	$filas_handler.newFilaObj("criar_sala", "/torque/sala/adicionar?idPersona=" @ %persona.taxoId @ "&sala=sala%20" @ %sala.num @ "&login=" @ %persona.user.login, 1, %sala, %persona);
}

function serverCmdCriarSalaDePoker(%client, %blind)
{
	serverCmdCriarSala(%client);
	
	%sala = %client.persona.sala;
	%sala.setTipoDeJogo("poker");
	
	if(%blind !$= ""){
		%sala.blind = %blind;
	} else {
		%sala.blind = 1;	
	}
	
	if(%apostaMax !$= ""){
		%sala.apostaMax = %apostaMax;
	} else {
		%sala.apostaMax = 6;	
	}
	
	%sala.fichasIniciaisGlobal = %blind * 20;
		
	%sala.CTCalterarTipoDeJogo("poker");
	serverAtualizarAtrioParaTodos();
}

function serverCmdEntrarNaSala(%client, %salaNum)
{
	%eval = "%sala = $sala" @ %salaNum @ ";";
	eval(%eval);
	
	if(!isObject(%sala))
	{
		echo("A sala deixou de existir bem na hora! Que pena!");
		return;
	}
	
	if(%sala.ocupada)
	{
		echo("SALA(" @ %salaNum @ ") OCUPADA!");
		return;
	}
	
	if (%sala.getLotada())
	{
		echo("SALA(" @ %salaNum @ ") LOTADA!");
		return;
	}
	
	if(%sala.checkBan(%client.persona))
	{
		commandToClient(%client, 'ImpossivelEntrarNaSalaBanido');
		return;	
	}
		
	if (!(%sala.jogoTAXOid !$= "" || $DEBUGMASTER)){
		echo("SALA(" @ %salaNum @ ") Ainda não possui TaxoId do próximo jogo...");
		commandToClient(%client, 'salaSendoConfigurada');
		return;
	}

	serverAdicionarPersonaNaSala(%client, %sala);
	serverCTCQuemEstavaNaSala(%client, %sala);
	serverAtualizarAtrioParaTodos();
}

function serverCTCQuemEstavaNaSala(%client, %sala)
{
	%playersOn = %sala.simPersonas.getCount();
	
	%duplasInfo = %sala.p1Dupla SPC %sala.p2Dupla SPC %sala.p3Dupla SPC %sala.p4Dupla SPC %sala.p5Dupla SPC %sala.p6Dupla;

	for(%i = 0; %i < 6; %i++)
	{
		%statsPlus[%i] = %sala.autoGetPersonaStatsPlus(%i);
	}

	//envia os dados de quem já estava na sala para o novoPlayer, caso seja -1, sabe que aquilo não é um player:
	commandToClient(%client, 'entrarNaSala', %playersOn, 
			%statsPlus[0],
			%statsPlus[1],
			%statsPlus[2],
			%statsPlus[3],
			%statsPlus[4],
			%statsPlus[5],
			%sala.num, %sala.planeta.nome, %sala.tipoDeJogo, %sala.turno, %sala.lotacao, %duplasInfo); 
}

function sala::autoGetPersonaStatsPlus(%this, %num)
{
	if(%num > %this.simPersonas.getCount() - 1)
		return 0;
		
	%statsPlus = %this.getPersonaStatsPlus(%num);
	return %statsPlus;
}

function serverAdicionarPersonaNaSala(%client, %sala)
{
	%persona = %client.persona;
	serverCalcularPersonaDif(%persona);
	%sala.adicionarPersona(%persona);
	%sala.lastAddUrl = "/torque/sala/adicionar?idPersona=" @ %persona.taxoId @ "&idJogo=" @ %sala.jogoTaxoId @ "&login=" @ %persona.user.login;
	$filas_handler.newFilaObj("adicionar_na_sala", %sala.lastAddUrl, 2, %sala, %persona);
		
	serverCTCIncluirPersonaNaSala(%client, %sala);
}

function serverCTCIncluirPersonaNaSala(%client, %sala)
{
	for(%i = 0; %i < %sala.simPersonas.getCount(); %i++){
		%persona = %sala.getPersona(%i);
		if (%novaPersona.taxoId != %persona.taxoId){
			commandToClient(%persona.client, 'incluirPlayerNaSala', %client.persona.getStatsPlus());
		}
	}
	
	if(isObject(%sala.observador)){
		commandToClient(%sala.observador, 'incluirPlayerNaSala', %client.persona.getStatsPlus());
	}
}

////////////Atrio:
//Quando o client pede a relaçao de salas pro server:
function serverCmdPegarRelacaoDeSalas(%client, %vindoDeOnde)
{
	if(%client.persona.offLine == false){
		if(!$personasNoAtrio.isMember(%client.persona)){
			$personasNoAtrio.add(%client.persona);
		}
		
		%salasString = serverGetAtrioSalasString(%client.persona.atrioPagina);	
		%numDePersonasNoChat = $personasNoAtrio.getCount();
		echo(%numDePersonasNoChat @ " personas no chat do atrio");
		
		commandToClient(%client, 'popularAtrio', %salasString, %vindoDeOnde, "", %numDePersonasNoChat);
		serverAtualizarPersonasNoAtrioParaTodos();
	}
}

function serverAtualizarPersonasNoAtrioParaTodos()
{
	%personasNoAtrio = $personasNoAtrio.getCount();	
	
	for(%i = 0; %i < %personasNoAtrio; %i++){
		%persona = $personasNoAtrio.getObject(%i);
		commandToClient(%persona.client, 'atualizarNumDePersonasNoAtrio', %personasNoAtrio);
	}
}

function serverCmdSairDaSala(%client)
{	
	if(%client.persona.offLine)
		return;
		
	%persona = %client.persona;
	%sala = %persona.sala;
	
	if (!(%sala.jogoTAXOid !$= "" || $DEBUGMASTER)) 
	{ 
		echo("SALA(" @ %salaNum @ ") Ainda não possui TaxoId do próximo jogo...");
		commandToClient(%client, 'salaSendoConfigurada');
		return;
	}
	
	if(%client == %sala.observador)
	{
		%sala.setNoObservador();
	}
	
	
	%sala.removerPersona(%persona);
	%persona.ctcPopularAtrio(serverGetAtrioSalasString(), "sala");
	serverAtualizarAtrioParaTodos();
		
	%url = "/torque/sala/remover?idPersona=" @ %persona.taxoId @ "&idJogo=" @ %persona.lastJogoTaxoId @ "&login=" @ %persona.user.login;
	%sala.lastRemoverUrl = %url;
	$filas_handler.newFilaObj("remover_da_sala", %url, 2, %sala, %persona);
}

////////////Atrio:
function serverSairDaSala(%client, %vindoDeOnde)
{
	%salasString = serverGetAtrioSalasString();	
	%client.persona.ctcPopularAtrio(%vindoDeOnde);
	serverAtualizarAtrioParaTodos();
}

function serverTirarDaSalaComTaxo(%persona)
{
	%sala = %persona.sala;
	serverTirarDaSala(%persona);
	echo("Removendo persona da sala, função não veio com especial!");
	
	%url = "/torque/sala/remover?idPersona=" @ %persona.taxoId @ "&idJogo=" @ %persona.lastJogoTaxoId @ "&login=" @ %persona.user.login;
	%sala.lastRemoverUrl = %url;
	$filas_handler.newFilaObj("remover_da_sala", %url, 2, %sala, %persona);
}

function serverTirarDaSala(%persona)
{
	%persona.removerDaSala();
	
	if(%persona.inGame)
	{
		%persona.inGame = false;
		echo("Persona (" @ %persona.nome @ ") caiu e foi tirada do jogo e da sala");
		return;
	}
	serverCmdPegarRelacaoDeSalas(%persona.client, "sala");
}

function serverVerificarPlaneta(%sala)
{
	%sala.resetarPlaneta();	
}

function serverDestruirSala(%sala){
	$serverSimSalas.remove(%sala);
	%sala.delete();
	
	serverAtualizarAtrioParaTodos();
	serverVerificarResetSalas();
}	


function serverRebuildSala(%sala){
	%playersAtivos = %sala.simPersonas.getCount();

	%novaOrdem = %playersAtivos;
	for(%i = 0; %i < %playersAtivos; %i++){
		%tempPersona = %sala.simPersonas.getObject(%i);
		%novaOrdem = %novaOrdem SPC %tempPersona.nome;
	}
	
	for(%i = 0; %i < %playersAtivos; %i++){
		%persona = %sala.simPersonas.getObject(%i);
		if(!%persona.inGame){
			commandToClient(%persona.client, 'rebuildSala', %novaOrdem); //envia os dados do novo player pra cada um que já estava na sala	
			echo("Enviando nova ordem para a persona " @ %persona @ ", na SALA(" @ %sala.num @ "): " @ %novaOrdem);
		}
	}
}

function serverCmdRebuildSalaComDados(%client){
	%sala = %client.persona.sala;
	%sala.rebuildSalaComDadosPrivate(%client);
	
	if(%sala.tipoDeJogo !$= "poker")
		return;
		
	if(%client.persona.pk_fichas < %sala.blind*20)
	{
		%mySalaSimPos = %client.persona.getMySalaSimPos();
		//%sala.kickarPersona(%mySalaSimPos);	
		%sala.kickarPersonaPorFaltaDeFichas(%mySalaSimPos);	
	}
}

//
function serverCalcularPersonaDif(%persona){
	%mediaVit = 0;
	if(%persona.especie $= "humano"){
		%mediaVit += %persona.taxoVitorias / 10;
		%mediaVit += %persona.aca_v_1 * (%persona.aca_v_1 * 2);
		%mediaVit += %persona.aca_v_2 * 2;
		%mediaVit += %persona.aca_v_3 * (%persona.aca_v_3 * 3);
		%mediaVit += %persona.aca_v_4 * 3;
		%mediaVit += %persona.aca_v_5 * 3;
		%mediaVit += %persona.aca_v_6 * 3; //Planetas
		%mediaVit += %persona.aca_a_1 * 2; //Líderes
		%mediaVit += %persona.aca_a_2 * 3;
		%mediaVit += %persona.aca_i_1 * 2;
		%mediaVit += %persona.aca_i_2 * 15;
		%mediaVit += %persona.aca_i_3 * 3;
		%mediaVit += %persona.aca_c_1 * 2;
		%mediaVit += %persona.aca_d_1 * 2;
		%mediaVit += %persona.aca_t_d_max / 3;
		%mediaVit += %persona.aca_t_a_max / 3;
		%mediaVit += %persona.aca_n_d_max / 3;
		%mediaVit += %persona.aca_n_a_max / 3;
		%mediaVit += %persona.aca_ldr_1_h1 * 2;
		%mediaVit += %persona.aca_ldr_2_h1 * 2;
		%mediaVit += %persona.aca_ldr_1_h2 * (%persona.aca_ldr_1_h2 * 4);
		%mediaVit += %persona.aca_ldr_2_h2 * (%persona.aca_ldr_2_h2 * 4);
		%mediaVit += %persona.aca_ldr_1_h3 * 2;
		%mediaVit += %persona.aca_ldr_2_h3 * 2;
		%mediaVit += %persona.aca_ldr_1_h4 * 2;
		%mediaVit += %persona.aca_ldr_2_h4 * 2;
		
		//bônus por sinergia com prospecção (filantropia, almirante e reciclagem):
		if(%persona.aca_c_1 > 0){
			%mediaVit += %persona.aca_i_2 * %persona.aca_c_1;
			%mediaVit += %persona.aca_i_3 * %persona.aca_c_1;
			%mediaVit += %persona.aca_v_3 * %persona.aca_c_1;
		}
		
		//bônus por sinergia de almirante com filantropia:
		if(%persona.aca_i_2 > 0){
			%mediaVit += %persona.aca_i_3 * %persona.aca_i_2;
		}
		
		//pesquisas avançadas:
		%mediaVit += %persona.aca_av_1 * 3;	 //Carapaça
		%mediaVit += %persona.aca_av_2 * 3;	 //Mira Eletrônica
		%mediaVit += %persona.aca_av_3 * 3;	 //Ocultar
		%mediaVit += %persona.aca_av_4 * (%persona.aca_av_4 * 3);	 //Satélite
		
		%mediaVit -= 16; //base dos humanos
	} else if(%persona.especie $= "gulok"){
		%mediaVit += 76; //base dos guloks
		%mediaVit += %persona.taxoVitorias / 10;
		%mediaVit += %persona.aca_v_1 * 3; //Metabolismo
		%mediaVit += %persona.aca_v_2 * 3; //Instinto Materno
		%mediaVit += %persona.aca_v_3 * 3; //Incorporar
		%mediaVit += %persona.aca_v_4 * 3; //Submergir
		%mediaVit += %persona.aca_v_5 * 15; //Crisálida
		%mediaVit += %persona.aca_v_6 * 5; //Matriarca
		%mediaVit += %persona.aca_a_1 * 2; //Exoesqueleto
		%mediaVit += %persona.aca_a_2 * 3; //Horda
		%mediaVit += %persona.aca_i_1 * 2; //Espionagem
		%mediaVit += %persona.aca_i_2 * 2; //Pilhar
		%mediaVit += %persona.aca_i_3 * 3; //Dragnal
		%mediaVit += %persona.aca_c_1 * 2; //Faro Extremo
		%mediaVit += %persona.aca_d_1 * 2; //Fertilidade
		%mediaVit += %persona.aca_t_d_max / 3; //Rainhas
		%mediaVit += %persona.aca_t_a_max / 3;
		%mediaVit += %persona.aca_n_d_max / 3; //Cefaloks
		%mediaVit += %persona.aca_n_a_max / 3;
		%mediaVit += %persona.aca_s_d_max / 3; //Vermes
		%mediaVit += %persona.aca_s_a_max / 3;
		%mediaVit += %persona.aca_ldr_1_h1 * (%persona.aca_ldr_1_h1 * 3); //Asas
		%mediaVit += %persona.aca_ldr_2_h1 * (%persona.aca_ldr_2_h1 * 3);
		%mediaVit += %persona.aca_ldr_1_h2 * 3; //Carregar
		%mediaVit += %persona.aca_ldr_2_h2 * 3;
		%mediaVit += %persona.aca_ldr_1_h3 * 3; //Canibalizar
		%mediaVit += %persona.aca_ldr_2_h3 * 3; //Metamorfose
		%mediaVit += %persona.aca_ldr_1_h4 * 3; //Devorar Rainhas
		%mediaVit += %persona.aca_ldr_2_h4 * 3; //Cortejar
		%mediaVit += %persona.aca_ldr_3_h1 * (%persona.aca_ldr_3_h1 * 3); //Entregar
		%mediaVit += %persona.aca_ldr_3_h2 * (%persona.aca_ldr_3_h2 * 3); //Sopro
		%mediaVit += %persona.aca_ldr_3_h3 * (%persona.aca_ldr_3_h3 * 3); //Fúria
		%mediaVit += %persona.aca_ldr_3_h4 * (%persona.aca_ldr_3_h4 * 3); //Covil
		
		//bônus por sinergia com Instinto Materno (horda, cortejar e entregar):
		if(%persona.aca_v_2 > 0){
			%mediaVit += %persona.aca_v_2 * %persona.aca_a_2;
			%mediaVit += %persona.aca_v_2 * %persona.aca_ldr_2_h4;
			%mediaVit += %persona.aca_v_2 * %persona.aca_ldr_3_h1;
		}
		
		//bônus por sinergia de crisálida com matriarca:
		if(%persona.aca_v_6 > 0){
			%mediaVit += %persona.aca_v_5 * %persona.aca_v_6;
		}
				
		//pesquisas avançadas:
		%mediaVit += %persona.aca_av_1 * %persona.aca_ldr_3_h1 * %persona.aca_ldr_3_h2 * %persona.aca_i_3;	 //Especializar
		%mediaVit += %persona.aca_av_2 * 3;	 //Vírus Gulok
		%mediaVit += %persona.aca_av_3 * 3;	 //Expulsar
		%mediaVit += %persona.aca_av_4 * (%persona.aca_av_4 * 3);	 //Evolução Avançada
	}	
	
	
	%persona.mediaVit = mFloor(%mediaVit);
}

function serverCmdGetSalaDif(%client){
	%mediaVitRaw = %client.persona.sala.getDificuldade();
	commandToClient(%client, 'getSalaDif', %mediaVitRaw);	
}
//
function serverCmdSetPlayerPronto(%client, %num, %param){
	%sala = %client.persona.sala;
	%client.persona.pronto = %param;
	%playersAtivos = %sala.simPersonas.getCount();	
	
	for(%i = 0; %i < %playersAtivos; %i++){
		%persona = %sala.simPersonas.getObject(%i);	
		commandToClient(%persona.client, 'setPlayerPronto', %num, %param);
	}
	if(%sala.observadorOn){
		commandToClient(%sala.observador, 'setPlayerPronto', %num, %param);	
	}
}
//
function serverInitAtrioPagSys(){
	$atrioPaginas = new SimSet();
	
	//cria 34 páginas:
	for (%i = 1; %i < 35; %i++){
		%eval = "$atrioPagina" @ %i @ " = new SimSet();";
		eval(%eval);
	}
	
	
}

function serverOrganizarSalas(){
	%numDeSalas = $serverSimSalas.getCount();
	$atrioPaginas.clear();
	
	for(%i = 0; %i < $serverSimSalas.getCount(); %i++){
		%sala = $serverSimSalas.getObject(%i);
		$atrioPagina1.add(%sala);	
	}
}

function serverGetAtrioSalasString(%pagina){
	%salasString = $serverSimSalas.getCount(); //número de salas é a primeira palavra
	for(%i = 0; %i < $serverSimSalas.getCount(); %i++){
		%sala = $serverSimSalas.getObject(%i);
		
		if(%pagina $= ""){
			%pagina = 1;	
		}
		
		if(%i >= (%pagina - 1) * 6 && %i < %pagina * 6){
			%salasString = %salasString SPC %sala.num; //número da sala
			%salasString = %salasString SPC %sala.simPersonas.getCount(); //qts players ativos
			%salasString = %salasString SPC %sala.ocupada; //se está em jogo ou não
			%salasString = %salasString SPC %sala.lotacao; //qual eh a lotação da sala
			%salasString = %salasString SPC %sala.planeta.nome; //o planeta selecionado
			%salasString = %salasString SPC %sala.emDuplas; //se eh um jogo em duplas
			%salasString = %salasString SPC %sala.semPesquisas; //se eh um jogo sem pesquisas
			%salasString = %salasString SPC %sala.handicap; //se eh um jogo com handicap
			%salasString = %salasString SPC %sala.set; //se eh um jogo em set
			%salasString = %salasString SPC %sala.poker; //se eh um jogo de poker
			%salasString = %salasString SPC %sala.getDificuldade();
			%salasString = %salasString SPC %sala.blind;
		}
	}
	
	return %salasString;
}


function serverAtualizarAtrioParaTodos(){
	%personasNoAtrio = $personasNoAtrio.getCount();
	for(%i = 0; %i < %personasNoAtrio; %i++){
		%persona = $personasNoAtrio.getObject(%i);
		%salasString = serverGetAtrioSalasString(%persona.atrioPagina);	
		commandToClient(%persona.client, 'popularAtrio', %salasString, "Atrio", "", %personasNoAtrio);
	}
}

///zera o número global de salas caso não exista sala alguma (executada quando uma sala é destruída)
function serverVerificarResetSalas(){
	%numDeSalas = $serverSimSalas.getCount();
	if(%numDeSalas == 0){
		$numDeSalasUniversal = 0;	
	}
}


//////////////////////////////
//Observador:
function serverCmdEntrarNaSalaComoObservador(%client, %salaNum){
	%persona = %client.persona;
	%eval = "%sala = $sala" @ %salaNum @ ";";
	eval(%eval);
	
	if(%sala.ocupada)
		return;
		
	
	$personasNoAtrio.remove(%persona);
	%persona.sala = %sala;
	%sala.observadorOn = true;
	%sala.observador = %client;
		
	serverCTCQuemEstavaNaSalaProObservador(%client, %sala);
}

function serverCTCQuemEstavaNaSalaProObservador(%client, %sala)
{
	%playersOn = %sala.simPersonas.getCount();
	
	%duplasInfo = %sala.p1Dupla SPC %sala.p2Dupla SPC %sala.p3Dupla SPC %sala.p4Dupla SPC %sala.p5Dupla SPC %sala.p6Dupla;

	//envia os dados de quem já estava na sala para o novoPlayer, caso seja -1, sabe que aquilo não é um player:
	commandToClient(%client, 'entrarNaSalaComoObservador', %playersOn, 
			%sala.getPersonaStatsPlus(0),
			%sala.getPersonaStatsPlus(1),
			%sala.getPersonaStatsPlus(2),
			%sala.getPersonaStatsPlus(3), 
			%sala.getPersonaStatsPlus(4),
			%sala.getPersonaStatsPlus(5),
			%sala.num, %sala.planeta.nome, %sala.tipoDeJogo, %sala.turno, %sala.lotacao, %duplasInfo); 
}


////////////
//Planeta:
function serverCmdAlterarPlaneta(%client, %planeta){
	%sala = %client.persona.sala;
	
	%eval = "%sala.planeta = $planeta" @ %planeta @ ";";
	eval(%eval);	

	%sala.CTCalterarPlaneta(%planeta);
	
	serverCmdAlterarTipoDeJogo(%client, "Classico");
	serverAtualizarAtrioParaTodos();
}

////////////
//Tipo de Jogo:
function serverCmdAlterarTipoDeJogo(%client, %tipo)
{
	%sala = %client.persona.sala;
	%sala.setTipoDeJogo(%tipo);
	%sala.CTCalterarTipoDeJogo(%tipo);
	serverAtualizarAtrioParaTodos();
}

////////////
//Tipo de Jogo:
function serverCmdAlterarTurno(%client, %tempo)
{
	%sala = %client.persona.sala;
	%sala.turno = %tempo;
	%sala.CTCalterarTurno(%tempo);
}

////////////
//Lotação:
function serverCmdAlterarLotacao(%client, %lotacao)
{
	%sala = %client.persona.sala;
	%sala.lotacao = %lotacao;
	%sala.CTCalterarLotacao(%lotacao);
	serverAtualizarAtrioParaTodos();
}



////////////////////
//Criar Duplas:
function serverCmdCriarDuplas(%client, %p1, %p2, %p3, %p4, %p5, %p6){
	%sala = %client.persona.sala;
	%sala.p1Dupla = %p1;
	%sala.p2Dupla = %p2;
	%sala.p3Dupla = %p3;
	%sala.p4Dupla = %p4;
	%sala.p5Dupla = %p5;
	%sala.p6Dupla = %p6;
	
	for(%i = 0; %i < %sala.simPersonas.getCount(); %i++){
		%persona = %sala.simPersonas.getObject(%i);
		commandToClient(%persona.client, 'setarDuplas', %p1, %p2, %p3, %p4, %p5, %p6);
	}
	if(isObject(%sala.observador)){
		commandToClient(%sala.observador, 'setarDuplas', %p1, %p2, %p3, %p4, %p5, %p6);
	}
}



///////////////
//Buscar jogador:
function serverCmdBuscarPersona(%client, %nomeDaPersona){
	%online = false;
	%numDePersonasOnline = $serverSimPersonas.getCount();
	for (%i = 0; %i < %numDePersonasOnline; %i++){
		%persona = $serverSimPersonas.getObject(%i);
		if(%persona.nome $= %nomeDaPersona){
			if(!%persona.offline){
				%online = true;
				if($personasNoAtrio.isMember(%persona)){
					%onde = "Chat";	
				} else if(isObject(%persona.sala)){
					for(%j = 0; %j < %persona.sala.simPersonas.getCount(); %j++){
						%tempPersona = %persona.sala.simPersonas.getObject(%j);
						if(%tempPersona == %persona){
							%onde = %persona.sala.num;	
							%j = %persona.sala.simPersonas.getCount();
						}
					}
					if(%onde $= ""){
						%onde = "Academia";		
					}
				} else {
					%onde = "Academia";	
				}
			}
			//%i = %numDePersonasOnline;
		}
	}
	commandToClient(%client, 'personaBuscada', %nomeDaPersona, %online, %onde);
}

//////////////////////
//getSalaInfoAtual:
function serverCmdGetSalaInfoAtual(%client, %salaNum)
{
	%eval = "%sala = $sala" @ %salaNum @ ";";
	eval(%eval);
	
	%sala.setInfoAtual();
	%sala.CTCsalaInfoAtual(%client);
}




////////////////
//Kick
function serverCmdKick(%client, %num)
{
	%sala = %client.persona.sala;
	%sala.kickarPersona(%num);	
}

