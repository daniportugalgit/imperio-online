// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientStartSequence.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quarta-feira, 26 de dezembro de 2007 9:21
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function connectToServer(%ip, %senha){
   // Just in case we're already connected.
   disconnect();
   
   if(%ip $= ""){
      MessageBoxOK("Cannot connect to server. IP address not specified.");
      return;
   }

   %conn = new GameConnection(ServerConnection);
   %conn.setConnectArgs($pref::Player::name, $pref::Player::senha, $versao);
   %conn.connect(%ip);
   
   $serverConnected = true;
   $serverLocal = false;
}

function clientAskDesconectar()
{
	disconnect();
	Canvas.pushDialog(clientStartGui);
	Canvas.popDialog(escolhaDeComandantesGui);
	conectandoGui.setVisible(false);
}

//função que avisa que o último jogo ainda não foi terminado, portanto o user tem que escolher outra persona:
function clientCmdUltimoJogoEmAndamento(){
	MessageBoxOK("O último jogo desta persona ainda não foi finalizado. Por favor, escolha outra persona.");
}

function onConnect(){
	if(waitingForServer.isAwake()){
		canvas.popDialog(waitingForServer);
	}

   //MessageBoxOK("Connection Established...", "Connection Established with the server!", "");
	Canvas.popDialog(networkMenu);
	$primeiraComm = true; //marca que o client ainda não saiu do commDados:
	$primeiraSalaInside = true; //marca que o client ainda não saiu de nenhuma sala, nem acabou um jogo:
	$primeiraAcademia = true; //marca que o client ainda não saiu da academia:
	$primeiraAtrio = true; //marca que o client ainda não saiu do átrio:
	$primeiraLoggedIn = true; //marca que o client ainda não saiu do LoggedIn:
	initNoticiasSys();
}

function apagarCommBtns(){
	//echo("apagando btns");
	perfil1Selecionar_btn.setActive(false);	
	perfil2Selecionar_btn.setActive(false);	
	perfil3Selecionar_btn.setActive(false);	
	perfil4Selecionar_btn.setActive(false);	
	perfil5Selecionar_btn.setActive(false);
	perfil1Apagar_btn.setActive(false);	
	perfil2Apagar_btn.setActive(false);	
	perfil3Apagar_btn.setActive(false);	
	perfil4Apagar_btn.setActive(false);	
	perfil5Apagar_btn.setActive(false);
	perfil1Criar_btn.setVisible(false);	
	perfil2Criar_btn.setVisible(false);	
	perfil3Criar_btn.setVisible(false);	
	perfil4Criar_btn.setVisible(false);	
	perfil5Criar_btn.setVisible(false);	
	perfil1CreditosPart.setVisible(false);	
	perfil2CreditosPart.setVisible(false);	
	perfil3CreditosPart.setVisible(false);	
	perfil4CreditosPart.setVisible(false);	
	perfil5CreditosPart.setVisible(false);	
}

function clientApagarCommPersonas(){
	for(%i = 1; %i < 6; %i++){
		%eval = "commPersona" @ %i @ "_img.setVisible(false);";
		eval(%eval);
	}
}

function clientVoltarPraCommSelect(){
	for(%i = 0; %i < $myPersonas.getCount(); %i++){
		%persona = $myPersonas.getObject(%i);
		%personaDados[%i] = %persona.nome SPC %persona.TAXOvitorias SPC %persona.TAXOpontos SPC %persona.TAXOvisionario SPC %persona.TAXOarrebatador SPC %persona.myComerciante SPC %persona.myDiplomata SPC %persona.TAXOcreditos SPC %persona.graduacaoNome SPC %persona.TAXOomnis SPC %persona.especie SPC %persona.pk_vitorias SPC %persona.pk_fichas SPC %persona.pk_power_plays;
	}
	
	TUTintroClose(); //fecha a itnro do tutorial, caso estivesse aberta;
	Canvas.popDialog(loggedInGui);
	$primeiraLoggedIn = false;	
	$jahCorrigiPEA = 0;
	$vendoCommSelect = false;
	clientCmdPopularComandantes($myPersonas.getcount(), %personaDados[0], %personaDados[1], %personaDados[2], %personaDados[3], %personaDados[4], %conhece_g);	
}

//recém logou, o server chama esta função aki no client:
function clientCmdPopularComandantes(%numDePersonas, %persona1Dados, %persona2Dados, %persona3Dados, %persona4Dados, %persona5Dados, %conhece_g){
	//echo("ClientCmdPopularComandantes::Número de Personas = " @ %numDePersonas);
	if(isObject($myPersonas)){
		//limpa, caso o usuário esteja deletando uma persona
		$myPersonas.clear(); 
		clientApagarCommPersonas();
		clientPopConfirmarDeletarMsgBox();
		clientPopAguardeMsgBox();
	} else {
		$myPersonas = new SimSet(); //cria um simSet que guarda os dados básicos de todas as suas personas (max = 5; ainda não implementado)
	}
	
	Canvas.popDialog(clientStartGui);
	Canvas.popDialog(networkMenu);
	Canvas.pushDialog(escolhaDeComandantesGui);	
	
	apagarCommBtns(); //apaga os btns de selecionar;
	
		
	echo("%persona1Dados = " @ %persona1Dados);
	echo("%persona2Dados = " @ %persona2Dados);
	echo("%persona3Dados = " @ %persona3Dados);
	echo("%persona4Dados = " @ %persona4Dados);
	echo("%persona5Dados = " @ %persona5Dados);
	echo("%conhece_g = " @ %conhece_g);
	
	if(%conhece_g == 1){
		$myPersona.user.conhece_guloks = 1;
		$conheco_guloks = true;	
	}
	
	for(%i = 1; %i < %numDePersonas + 1; %i++){
		%persona = new ScriptObject(){};
		$myPersonas.add(%persona);
		%eval = "%myDados = %persona" @ %i @ "Dados;";
		eval(%eval);
		//echo("ClientCmdPopularComandantes::%myDados = " @ %myDados);
				
		%persona.nome = getWord(%myDados, 0);
		%persona.TAXOvitorias = getWord(%myDados, 1);
		%persona.TAXOpontos = getWord(%myDados, 2);
		%persona.TAXOVisionario = getWord(%myDados, 3);
		%persona.TAXOArrebatador = getWord(%myDados, 4);
		%persona.myComerciante = getWord(%myDados, 5);
		%persona.myDiplomata = getWord(%myDados, 6);
		%persona.TAXOcreditos = getWord(%myDados, 7);
		%persona.graduacaoNome = getWord(%myDados, 8);
		%persona.TAXOomnis = getWord(%myDados, 9);
		%persona.especie = getWord(%myDados, 10);
		%persona.pk_vitorias = getWord(%myDados, 11);
		%persona.pk_fichas = getWord(%myDados, 12);
		%persona.pk_power_plays = getWord(%myDados, 13);
		
		%personaDados = %persona.nome SPC %persona.TAXOvitorias SPC %persona.TAXOpontos SPC %persona.TAXOvisionario SPC %persona.TAXOarrebatador SPC %persona.myComerciante SPC %persona.myDiplomata SPC %persona.TAXOcreditos SPC %persona.graduacaoNome SPC %persona.TAXOomnis SPC %persona.especie;
		clientPopularCommDados(%personaDados, %i); //passa os dados para a tela de escolha de comandantes (o terceiro parâmetro é o número da persona, de 1 a 5);
	}
	
	initSalasNaTela(); //prepara para entrar no Átrio;
	$primeiracomm = false;
	$vendoCommSelect = true;
		
	%personasCount = $myPersonas.getCount();
	if($myPersonas.getCount() < 5){
		%eval = "perfil" @ %personasCount+1 @ "Criar_btn.setVisible(true);";
		eval(%eval);
	}
	criarPerfilGui.setVisible(false);
	escolhaDeEspecie_tab.setVisible(false);
}

function clientPopularCommDados(%personaDados, %personaNum){
	$vendoCommSelect = true;
	clientPopAguardeMsgBox(); //devolve o controle ao usuário
	//echo("Populando Comm Dados na persona(" @ %personaNum @ "): " @ %personaDados);
	
	%nome = getWord(%personaDados, 0);
	%TAXOvitorias = getWord(%personaDados, 1);
	%TAXOpontos = getWord(%personaDados, 2);
	%TAXOvisionario = getWord(%personaDados, 3);
	%TAXOarrebatador = getWord(%personaDados, 4);
	%myComerciante = getWord(%personaDados, 5);
	%myDiplomata = getWord(%personaDados, 6);
	%TAXOcreditos = getWord(%personaDados, 7);
	%graduacaoNome = getWord(%personaDados, 8);
	%especie = getWord(%personaDados, 10);
		
	//torna visível a persona em questão:
	//muda a imgem conforme a espécie:
	if(%especie $= "humano"){
		%bitmap = "game/data/images/salaPersona.png";
	} else {
		%bitmap = "game/data/images/persona_gulok.png";
	}
	%eval = "commPersona" @ %personaNum @ "_img.bitmap = %bitmap;";
	eval(%eval);
	%eval = "commPersona" @ %personaNum @ "_img.setVisible(true);";
	eval(%eval);
	%eval = "perfil" @ %personaNum @ "Selecionar_btn.setActive(true);";
	eval(%eval);
	%eval = "perfil" @ %personaNum @ "Apagar_btn.setActive(true);";
	eval(%eval);
	%eval = "perfil" @ %personaNum @ "CreditosPart.setVisible(true);";
	eval(%eval);
	%eval = "perfil" @ %personaNum @ "CreditosPartTXT.text = %TAXOcreditos;";
	eval(%eval);
	
		
	%percent = "%";
	%indefinido = "??";
	%eval = "commNome" @ %personaNum @ "_txt.text = %nome;";
	eval(%eval);
	%eval = "commVitorias" @ %personaNum @ "_txt.text = %TAXOvitorias;";
	eval(%eval);
	%eval = "commPontos" @ %personaNum @ "_txt.text = %TAXOpontos;";
	eval(%eval);
	%eval = "commVisionario" @ %personaNum @ "_txt.text = %TAXOvisionario;";
	eval(%eval);
	%eval = "commArrebatador" @ %personaNum @ "_txt.text = %TAXOarrebatador;";
	eval(%eval);
	if(%myComerciante !$= "-1"){
		%eval = "commComerciante" @ %personaNum @ "_txt.text = %myComerciante @ %percent;";
		eval(%eval);
	} else {
		%eval = "commComerciante" @ %personaNum @ "_txt.text = %indefinido;";
		eval(%eval);
	}
	if(%myDiplomata !$= "-1"){
		%eval = "commDiplomata" @ %personaNum @ "_txt.text = %myDiplomata @ %percent;";
		eval(%eval);
	} else {
		%eval = "commDiplomata" @ %personaNum @ "_txt.text = %indefinido;";
		eval(%eval);	
	}
		
	clientGaugeGradual(50, "vitorias", %personaNum, %TAXOvitorias, -1, "comm", %nome, %especie);
	clientGaugeGradual(50, "pontos", %personaNum, %TAXOpontos, -1, "comm", %nome, %especie);
	clientGaugeGradual(50, "visionario", %personaNum, %TAXOvisionario, -1, "comm", %nome, %especie);
	clientGaugeGradual(50, "arrebatador", %personaNum, %TAXOarrebatador, -1, "comm", %nome, %especie);
	clientGaugeGradual(50, "comerciante", %personaNum, %myComerciante, %TAXOvitorias, "comm", %nome, %especie);
	clientGaugeGradual(50, "diplomata", %personaNum, %myDiplomata, %TAXOvitorias, "comm", %nome, %especie);
}

function clientCriarPerfilBtnClick(){
	criarPerfilGui.setVisible(true);
	if($conheco_guloks){
		clientSetEspeciesBtns();
		escolhaDeEspecie_tab.setVisible(true);	
	} else {
		escolhaDeEspecie_tab.setVisible(false);		
	}
}

function clientSetEspeciesBtns(){
	clientClearEspeciesBtns();
	especie_humano_btn.setStateOn(true);
	$lastPersonaCriadaEspecie = "humano";
}

function clientClearEspeciesBtns(){
	especie_humano_btn.setStateOn(false);	
	especie_gulok_btn.setStateOn(false);
}

function clientSelecionarEspecie(%especie){
	clientClearEspeciesBtns();
	%eval = "%myBtn = especie_" @ %especie @ "_btn;";
	eval(%eval);
	%myBtn.setStateOn(true);
	$lastPersonaCriadaEspecie = %especie;
}

function clientVerificarCodinome(%nome){
	//verificar se este usuário jah tem persona com este nome
	%okToGo = true;
	for(%i = 0; %i < $myPersonas.getCount(); %i++){
		%personaExistente = $myPersonas.getObject(%i);
		if(%personaExistente.nome $= %nome){
			%okToGo = false;	
		}
	}
	return %okToGo;
}

function clientCriarNovoPerfil(){
	%nome = codinome_itxt.getText();
	%wordCount = getWordCount(codinome_itxt.getText());
	
	if(%wordCount != 1 || clientVerificarCodinomeReservado())
	{
		clientMsgBoxOKT("CONDINOME INVÁLIDO", "POR FAVOR, NÃO USE ESPAÇOS NEM CARACTERES ESPECIAIS.");	
		return;
	}
	
	%nome = trim(%nome); //tira espaços em branco no início e fim do codinome
	codinome_itxt.setText(%nome); //devolve pro usuário um codinome sem espaços
	
	if(clientVerificarCodinomeReservado(%nome))
	{
		clientMsgBoxOKT("CONDINOME INVÁLIDO", "POR FAVOR, NÃO USE ESPAÇOS NEM CARACTERES ESPECIAIS.");	
		return;
	}
	
	if(clientVerificarCodinome(%nome)){
		commandToServer('criarPersona', %nome, $lastPersonaCriadaEspecie);
		codinome_img.setVisible(false);
		perfilOK_btn.setVisible(false);
		perfilCancelar_btn.setVisible(false);
		perfilCriar_btn.setVisible(true);
		clientPushAguardeMsgBox();
		$primeiraComm = false;
	} else {
		clientCmdCodinomeJahExiste();
	}
}

function clientVerificarCodinomeReservado(%codinome)
{
	%exp = explode(%codinome, "imperio");
	if(%exp.count > 1)
		return true;
	
	%exp = explode(%codinome, "GM");
	if(%exp.count > 1)
		return true;
	
	%exp = explode(%codinome, "G.M.");
	if(%exp.count > 1)
		return true;
	
	return false;
}

function perfilCancelarBtnClick(){
	criarPerfilGui.setVisible(false);
	escolhaDeEspecie_tab.setVisible(false);		
	clientPopAguardeMsgBox();
}

function clientPushCodinomeJahExiste(){
	canvas.pushDialog(codinomeJahExisteGui);	
	clientPopAguardeMsgBox();	
}

function clientCmdCodinomeJahExiste()
{
	clientPopAguardeMsgBox();
	clientMsgBoxOKT("CONDINOME JÁ EXISTE", "ESCOLHA UM OUTRO CODINOME E TENTE DE NOVO.");	
}

function clientPopCodinomeJahExisteMsgBoxOk(){
	canvas.popDialog(codinomeJahExisteGui);	
}

function clientPushConfirmarDeletarMsgBox(%nome){
	canvas.pushDialog(confirmarDeletarMsgBoxGui);
	confirmarDeletarTxt.text = %nome;
}
function clientPopConfirmarDeletarMsgBox(){
	canvas.popDialog(confirmarDeletarMsgBoxGui);	
}


function deletarEstaPersonaBtnClick(%num){
	if($myPersonas.getCount() > 1){
		//trazer gui de confimação, substituir o btn de deletar perfil pelo de cancelar
		$lastDelPersonaNum = %num;
		%persona = $myPersonas.getObject(%num);
		clientPushConfirmarDeletarMsgBox(%persona.nome);
	} else {
		//msgBox dizendo que é preciso ter no mínimo um perfil
		clientMsgBoxOKT("IMPOSSÍVEL APAGAR", "É NECESSÁRIO TER NO MÍNIMO UMA PERSONA ATIVA.");	
	}
}

function confirmarPersonaDelBtnClick(){
	clientPushAguardeMsgBox();
	$primeiraComm = false;
	commandToServer('apagarPersona', $lastDelPersonaNum);
	$lastDelPersonaNum = "no";
}

function perfilDeletarCancelarBtnClick(){
	//apagar o gui de confirmação e trocar os btns de deletar pelos de selecionar e o de cancelar pelo deletar perfil	
	clientPopConfirmarDeletarMsgBox();
}

function clientPerfilSelecionar(%num){
	%eval = "%personaSelecionada = $myPersonas.getObject(" @ %num - 1 @ ");";
	eval(%eval);
	$myPersona = new ScriptObject(myPersona){};//cria uma persona no client, para que seja fácil referenciar valores;
	$myPersona = %personaSelecionada;
	%personaDados = %personaSelecionada.nome SPC %personaSelecionada.TAXOvitorias SPC %personaSelecionada.TAXOpontos SPC %personaSelecionada.TAXOvisionario SPC %personaSelecionada.TAXOarrebatador SPC %personaSelecionada.myComerciante SPC %personaSelecionada.myDiplomata SPC %personaSelecionada.TAXOcreditos SPC %personaSelecionada.graduacaoNome SPC %personaSelecionada.TAXOomnis;
	commandToServer('selecionarPersona', %num);
	clientCmdLoggedIn(%personaDados);
	clientPushConfigAcademiaMsgBox();
	$vendoCommSelect = false;
}

function clientPushConfigAcademiaMsgBox(){
	canvas.pushDialog(configAcademiaGui);
}

function clientPopConfigAcademiaMsgBox(){
	canvas.popDialog(configAcademiaGui);
}


function clientCmdRegistrarDadosAcademia(%dados){
	//echo("REGISTRANDO ACADEMIA... Dados = " @ %dados);
	
	//soldados/vermes:
	$myPersona.aca_s_d_min = getWord(%dados, 0);
	$myPersona.aca_s_d_max = getWord(%dados, 1);
	$myPersona.aca_s_a_min = getWord(%dados, 2);
	$myPersona.aca_s_a_max = getWord(%dados, 3);
	//tanques/rainhas:
	$myPersona.aca_t_d_min = getWord(%dados, 4);
	$myPersona.aca_t_d_max = getWord(%dados, 5);
	$myPersona.aca_t_a_min = getWord(%dados, 6);
	$myPersona.aca_t_a_max = getWord(%dados, 7);
	//navios/cefaloks:
	$myPersona.aca_n_d_min = getWord(%dados, 8);
	$myPersona.aca_n_d_max = getWord(%dados, 9);
	$myPersona.aca_n_a_min = getWord(%dados, 10);
	$myPersona.aca_n_a_max = getWord(%dados, 11);
	//líder1:
	$myPersona.aca_ldr_1_d_min = getWord(%dados, 12);
	$myPersona.aca_ldr_1_d_max = getWord(%dados, 13);
	$myPersona.aca_ldr_1_a_min = getWord(%dados, 14);
	$myPersona.aca_ldr_1_a_max = getWord(%dados, 15);
	$myPersona.aca_ldr_1_h1 = getWord(%dados, 16);
	$myPersona.aca_ldr_1_h2 = getWord(%dados, 17);
	$myPersona.aca_ldr_1_h3 = getWord(%dados, 18);
	$myPersona.aca_ldr_1_h4 = getWord(%dados, 19);
	//líder2:
	$myPersona.aca_ldr_2_d_min = getWord(%dados, 20);
	$myPersona.aca_ldr_2_d_max = getWord(%dados, 21);
	$myPersona.aca_ldr_2_a_min = getWord(%dados, 22);
	$myPersona.aca_ldr_2_a_max = getWord(%dados, 23);
	$myPersona.aca_ldr_2_h1 = getWord(%dados, 24);
	$myPersona.aca_ldr_2_h2 = getWord(%dados, 25);
	$myPersona.aca_ldr_2_h3 = getWord(%dados, 26);
	$myPersona.aca_ldr_2_h4 = getWord(%dados, 27);
	//líder3:
	$myPersona.aca_ldr_3_d_min = getWord(%dados, 28);
	$myPersona.aca_ldr_3_d_max = getWord(%dados, 29);
	$myPersona.aca_ldr_3_a_min = getWord(%dados, 30);
	$myPersona.aca_ldr_3_a_max = getWord(%dados, 31);
	$myPersona.aca_ldr_3_h1 = getWord(%dados, 32);
	$myPersona.aca_ldr_3_h2 = getWord(%dados, 33);
	$myPersona.aca_ldr_3_h3 = getWord(%dados, 34);
	$myPersona.aca_ldr_3_h4 = getWord(%dados, 35);
	//visionário
	$myPersona.aca_v_1 = getWord(%dados, 36);
	$myPersona.aca_v_2 = getWord(%dados, 37);
	$myPersona.aca_v_3 = getWord(%dados, 38);
	$myPersona.aca_v_4 = getWord(%dados, 39);
	$myPersona.aca_v_5 = getWord(%dados, 40);
	$myPersona.aca_v_6 = getWord(%dados, 41);
	//arebatador:
	$myPersona.aca_a_1 = getWord(%dados, 42);
	$myPersona.aca_a_2 = getWord(%dados, 43);
	//comerciante:
	$myPersona.aca_c_1 = getWord(%dados, 44);
	//diplomata:
	$myPersona.aca_d_1 = getWord(%dados, 45);
	//intel:
	$myPersona.aca_i_1 = getWord(%dados, 46);
	$myPersona.aca_i_2 = getWord(%dados, 47);
	$myPersona.aca_i_3 = getWord(%dados, 48);
	//Pesquisa Em Andamento:
	$myPersona.aca_pea_id = getWord(%dados, 49);
	$myPersona.aca_pea_min = getWord(%dados, 50);
	$myPersona.aca_pea_pet = getWord(%dados, 51);
	$myPersona.aca_pea_ura = getWord(%dados, 52);
	$myPersona.aca_pea_ldr = getWord(%dados, 53);
	
	//marca que a persona tem dados de academia:
	$myPersona.aca_tenhoDados = getWord(%dados, 54);
	
	//pega os omnis do usuário e coloca na $myPersona:
	$myPersona.TAXOomnis = getWord(%dados, 55);
	
	//pega a variável boleana:
	$myPersona.TAXOtutorial = getWord(%dados, 56);
	
	//Pesquisas avançadas:
	$myPersona.aca_av_1 = getWord(%dados, 57);
	$myPersona.aca_av_2 = getWord(%dados, 58);
	$myPersona.aca_av_3 = getWord(%dados, 59);
	$myPersona.aca_av_4 = getWord(%dados, 60);
	$myPersona.aca_pln_1 = getWord(%dados, 61);
	$myPersona.aca_art_1 = getWord(%dados, 62);
	$myPersona.aca_art_2 = getWord(%dados, 63);
	
	//espécie:
	$myPersona.especie = getWord(%dados, 64);
	
		
	clientPopConfigAcademiaMsgBox();
	if($myPersona.aca_tenhoDados == 1){
		echo("ACADEMIA IMPERIAL CONFIGURADA COM SUCESSO!");	
	}
	if($myPersona.TAXOvitorias > 0 || $myPersona.especie $= "gulok"){
		loggedInAcademia_btn.setActive(true);	
	} else {
		loggedInAcademia_btn.setActive(false);	
	}
	
	loggedInOmnis_txt.text = $myPersona.TAXOomnis;
	
	if($myPersona.aca_pea_id $= "" || $myPersona.aca_pea_id $= "1"){
		$myPersona.aca_pea_id = 0;	
	} 
	
	
	if($myPersona.especie $= "gulok"){
		$myPersona.gulok = true;
		loggedInTutorial_btn.setActive(false);	
	} else {
		loggedInTutorial_btn.setActive(true);	
	}
	
	clientPopularPokerTab(); 
		
	clientPopularPEA("loggedIn");
}

function clientPopularPokerTab()
{
	loggedIn_poker_tab.setVisible(true);
	loggedIn_pk_fichas_txt.text = $myPersona.pk_fichas;
	loggedIn_pk_vitorias_txt.text = $myPersona.pk_vitorias;
		
	loggedIn_powerPlays_tab.setVisible(true);
	loggedIn_powerPlays_txt.text = $myPersona.pk_power_plays;
	
	loggedIn_comprarFichas_btn.setVisible(true);
	loggedIn_venderFichas_btn.setVisible(true);
	clientSetVenderFichasBtn();
	clientSetComprarFichasBtn();
}

function clientSetVenderFichasBtn()
{
	if($myPersona.pk_fichas >= 1000)
	{
		loggedIn_venderFichas_btn.setActive(true);
		return;
	}
	loggedIn_venderFichas_btn.setActive(false);
}

function clientSetComprarFichasBtn()
{
	/*
	if($myPersona.TAXOvitorias >= 25)
	{
		loggedIn_comprarFichas_btn.setActive(true);
		return;
	}
	*/
	loggedIn_comprarFichas_btn.setActive(false);
}

function clientPopPokerTab()
{
	loggedIn_poker_tab.setVisible(false);	
	loggedIn_powerPlays_tab.setVisible(false);
	loggedIn_comprarFichas_btn.setVisible(false);
	loggedIn_venderFichas_btn.setVisible(false);
}
	

function clientPopularPEA(%tela){
	%myPesquisa = clientFindPesquisaPorClassId($myPersona.aca_pea_id, $myPersona.aca_pea_ldr);
	//echo("%MYPESQUISA = " @ %myPesquisa);
	if(isObject(%myPesquisa)){
		
		//echo("PESQUISA ENCONTRADA: " @ %myPesquisa.id);
		%eval = %tela @ "_a_PEA_tab.setVisible(true);";
		eval(%eval);
		%eval = "%myMicon = " @ %tela @ "_a_PEATAB_micon;";
		eval(%eval);
		%eval = "%myMiconL = " @ %tela @ "_a_PEATAB_miconL;";
		eval(%eval);
		if($myPersona.especie $= "humano"){
			%myBmp = "game/data/images/academia/atab_PEA";
			%eval = %tela @ "_a_PEA_tab.bitmap = %myBmp;";
			eval(%eval);	
		} else if($myPersona.especie $= "gulok"){
			%myBmp = "game/data/images/academia/atab_PEA_gulok";
			%eval = %tela @ "_a_PEA_tab.bitmap = %myBmp;";
			eval(%eval);	
		}
		
		//seta o tamanho do ícone (é diferente pra líderes e pesquisas especiais):
		if($myPersona.aca_pea_ldr > 0){
			if(%myPesquisa.upgrade $= "Escudo" || %myPesquisa.upgrade $= "jetPack" || %myPesquisa.upgrade $= "Sniper" || %myPesquisa.upgrade $= "Moral" || %myPesquisa.upgrade $= "Asas" || %myPesquisa.upgrade $= "Carregar" || %myPesquisa.upgrade $= "Canibalizar" || %myPesquisa.upgrade $= "Metamorfose" || %myPesquisa.upgrade $= "Cortejar" || %myPesquisa.upgrade $= "DevorarRainhas" || %myPesquisa.upgrade $= "Entregar" || %myPesquisa.upgrade $= "Sopro" || %myPesquisa.upgrade $= "Furia" || %myPesquisa.upgrade $= "Covil"){
				%myMicon.setVisible(false);
				%myMiconL.setVisible(true);
				%myMicon = %myMiconL;
			} else {
				%myMicon.setVisible(true);
				%myMiconL.setVisible(false);	
			}
		} else {
			%myMicon.setVisible(true);
			%myMiconL.setVisible(false);
		}
		
		//seta o tipo da pesquisa:
		if(%myPesquisa.tipo $= "visionario"){
			%myTEXT = "Visionário";
			%eval = %tela @ "_a_PEATAB_tipo_txt.text = %myTEXT;";
			eval(%eval);	
		} else if (%myPesquisa.tipo $= "lider"){
			%myTEXT = "Líder " @ $myPersona.aca_pea_ldr;
			%eval = %tela @ "_a_PEATAB_tipo_txt.text = %myTEXT;";
			eval(%eval);	
		} else if (%myPesquisa.tipo $= "zangao"){
			%myTEXT = "Zangão " @ $myPersona.aca_pea_ldr;
			%eval = %tela @ "_a_PEATAB_tipo_txt.text = %myTEXT;";
			eval(%eval);	
		} else if (%myPesquisa.tipo $= "dragnal"){
			%myTEXT = "Dragnal";
			%eval = %tela @ "_a_PEATAB_tipo_txt.text = %myTEXT;";
			eval(%eval);	
		} else if (%myPesquisa.tipo $= "avancadas"){
			%myTEXT = "Avançadas";
			%eval = %tela @ "_a_PEATAB_tipo_txt.text = %myTEXT;";
			eval(%eval);	
		} else {
			%eval = %tela @ "_a_PEATAB_tipo_txt.text = %myPesquisa.tipo;";
			eval(%eval);	
		}
		
		
		//seta a pesquisa em si e seu ícone:		
		if(%myPesquisa.upgrade $= "ataqueMaximo"){
			%myTEXT = "Ataque Máximo";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			if($myPersona.especie $= "humano"){
				%myMicon.bitmap = "~/data/images/academia/amicon_ataque";
			} else if($myPersona.especie $= "gulok"){
				%myMicon.bitmap = "~/data/images/academia/amicon_Gataque";
			}
		} else if(%myPesquisa.upgrade $= "ataqueMinimo"){
			%myTEXT = "Ataque Mínimo";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			if($myPersona.especie $= "humano"){
				%myMicon.bitmap = "~/data/images/academia/amicon_ataque";
			} else if($myPersona.especie $= "gulok"){
				%myMicon.bitmap = "~/data/images/academia/amicon_Gataque";
			}
		} else if(%myPesquisa.upgrade $= "defesaMaxima"){
			%myTEXT = "Defesa Máxima";	
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			if($myPersona.especie $= "humano"){
				%myMicon.bitmap = "~/data/images/academia/amicon_defesa";
			} else if($myPersona.especie $= "gulok"){
				%myMicon.bitmap = "~/data/images/academia/amicon_Gdefesa";
			}
		} else if(%myPesquisa.upgrade $= "defesaMinima"){
			%myTEXT = "Defesa Mínima";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			if($myPersona.especie $= "humano"){
				%myMicon.bitmap = "~/data/images/academia/amicon_defesa";
			} else if($myPersona.especie $= "gulok"){
				%myMicon.bitmap = "~/data/images/academia/amicon_Gdefesa";
			}
		} else if(%myPesquisa.upgrade $= "prospeccao"){
			%myTEXT = "Prospecção";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_comerciante" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "forcaD"){
			%myTEXT = "Força Diplomática";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_diplomata" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "canhaoOrbital"){
			%myTEXT = "Canhão Orbital";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_canhaoOrbital" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "airDrop"){
			%myTEXT = "Air Drop";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_airDrop" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "jetPack"){
			%myTEXT = "Jet Pack";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_jetPack" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "lider"){
			%myTEXT = "Líder";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_lider" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "carapaca"){
			%myTEXT = "Carapaça";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_carapaca" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "mira"){
			%myTEXT = "Mira Eletrônica";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_mira" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "satelite"){
			%myTEXT = "Satélite";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_satelite" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "crisalida"){
			%myTEXT = "Crisálida";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_crisalida" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "furia"){
			%myTEXT = "Fúria";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_furia" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "virus"){
			%myTEXT = "Vírus";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_virus" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "evolucao"){
			%myTEXT = "Evolução";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_evolucao" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "dragnal"){
			%myTEXT = "Dragnal";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_dragnal" @ %myPesquisa.num;
		} else if(%myPesquisa.upgrade $= "devorarRainhas"){
			%myTEXT = "Devorar Rainhas";
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myTEXT;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_devorarRainhas" @ %myPesquisa.num;
		} else {
			%eval = %tela @ "_a_PEATAB_upgrade_txt.text = %myPesquisa.upgrade;";
			eval(%eval);
			%myMicon.bitmap = "~/data/images/academia/amicon_" @ %myPesquisa.upgrade @ %myPesquisa.num;
		}
		
		//seta a porcentagem da pesquisa:
		%myPesquisaCustoTotal = %myPesquisa.custoInicial + %myPesquisa.cDevMin + %myPesquisa.cDevPet + %myPesquisa.cDevUra;
		%myPesquisaCustoPago = %myPesquisa.custoInicial + $myPersona.aca_pea_min + $myPersona.aca_pea_pet + $myPersona.aca_pea_ura;		
		%myPesquisaPercent = mFloor((100 * %myPesquisaCustoPago) / %myPesquisaCustoTotal);		
		%percent = "%";
		%eval = %tela @ "_a_PEATAB_pago_txt.text = %myPesquisaPercent @ %percent;";
		eval(%eval);
	} else {
		
		//echo("PESQUISA *NÃO* ENCONTRADA: $myPersona.aca_pea_id = " @ $myPersona.aca_pea_id);
		%eval = %tela @ "_a_PEA_tab.setVisible(false);";
		eval(%eval);
	}
}


function clientPerfilDeletar(%num){
	commandToServer('apagarPersona', %num);
	clientPushAguardeMsgBox();
}

//recém escolheu a persona, o server chama esta função:
function clientCmdLoggedIn(%personaDados){
	/*
	$myPersona = new ScriptObject(myPersona){};//cria uma persona no client, para que seja fácil referenciar valores;
	
	$myPersona.nome = getWord(%personaDados, 0);
	$myPersona.TAXOvitorias = getWord(%personaDados, 1);
	$myPersona.TAXOpontos = getWord(%personaDados, 2);
	$myPersona.TAXOVisionario = getWord(%personaDados, 3);
	$myPersona.TAXOArrebatador = getWord(%personaDados, 4);
	$myPersona.myComerciante = getWord(%personaDados, 5);
	$myPersona.myDiplomata = getWord(%personaDados, 6);
	$myPersona.TAXOcreditos = getWord(%personaDados, 7);
	$myPersona.graduacaoNome = getWord(%personaDados, 8);
	$myPersona.TAXOomnis = getWord(%personaDados, 9);
	*/
	
	echo("LOGADO como " @ $myPersona.nome);
	Canvas.popDialog(clientStartGui);
	Canvas.popDialog(networkMenu);
	
	clientPopularDados("loggedIn"); //passa os dados para a tela de loggedIn
	Canvas.popDialog(escolhaDeComandantesGui);	
	Canvas.pushDialog(loggedInGui);	
	//clientTurnLinksOff();
	initSalasNaTela(); //prepara para entrar no Átrio;
}

function clientTurnLinksOff()
{
	links_home_btn.setActive(false);
	links_ranking_btn.setActive(false);
	links_recados_btn.setActive(false);
	links_blog_btn.setActive(false);
	links_forum_btn.setActive(false);
	links_vt_btn.setActive(false);
	links_omnis_btn.setActive(false);	
}

//função para popular dados no átrio ou no loggedIn:
function clientPopularDados(%tela, %personaDados){
	//clientTurnLinksOff();
	/*
	if(%personaDados !$= ""){
		$myPersona.nome = getWord(%personaDados, 0);
		$myPersona.TAXOvitorias = getWord(%personaDados, 1);
		$myPersona.TAXOpontos = getWord(%personaDados, 2);
		$myPersona.TAXOvisionario = getWord(%personaDados, 3);
		$myPersona.TAXOarrebatador = getWord(%personaDados, 4);
		$myPersona.myComerciante = getWord(%personaDados, 5);
		$myPersona.myDiplomata = getWord(%personaDados, 6);
		$myPersona.TAXOcreditos = getWord(%personaDados, 7);
		$myPersona.graduacaoNome = getWord(%personaDados, 8);
		$myPersona.especie = getWord(%personaDados, 10);
	}
	*/
	
	if(%personaDados !$= ""){
		$myPersona.nome = getWord(%personaDados, 0);
		$myPersona.TAXOvitorias = getWord(%personaDados, 1);
		$myPersona.TAXOpontos = getWord(%personaDados, 2);
		$myPersona.TAXOvisionario = getWord(%personaDados, 3);
		$myPersona.TAXOarrebatador = getWord(%personaDados, 4);
		$myPersona.myComerciante = getWord(%personaDados, 5);
		$myPersona.myDiplomata = getWord(%personaDados, 6);
		$myPersona.TAXOcreditos = getWord(%personaDados, 7);
		$myPersona.graduacaoNome = getWord(%personaDados, 8);
		$myPersona.especie = getWord(%personaDados, 9);
		$myPersona.pk_vitorias = getWord(%personaDados, 10);
		$myPersona.pk_power_plays = getWord(%personaDados, 11);
	}
	
	if(%tela $= "loggedIn" || %tela $= "atrio"){
		%texString = "game/data/images/personaGrande_" @ $myPersona.especie;
		%eval = %tela @ "_personaGrande_img.bitmap = %texString;";
		eval(%eval);
		if($myPersona.TAXOvitorias > 0 || $myPersona.especie $= "gulok"){
			%eval = %tela @ "Academia_btn.setActive(true);";
			eval(%eval);
		} else {
			%eval = %tela @ "Academia_btn.setActive(false);";
			eval(%eval);
		}
		
		if($myPersona.especie $= "gulok"){
			loggedInTutorial_btn.setActive(false);	
		} else {
			loggedInTutorial_btn.setActive(true);	
		}
	}
	
	
	%percent = "%";
	%indefinido = "??";
	%eval = %tela @ "Nome1_txt.text = $myPersona.nome;";
	eval(%eval);
	%eval = %tela @ "Vitorias1_txt.text = $myPersona.TAXOvitorias;";
	eval(%eval);
	%eval = %tela @ "Pontos1_txt.text = $myPersona.TAXOpontos;";
	eval(%eval);
	%eval = %tela @ "Visionario1_txt.text = $myPersona.TAXOvisionario;";
	eval(%eval);
	%eval = %tela @ "Arrebatador1_txt.text = $myPersona.TAXOarrebatador;";
	eval(%eval);
	
	if($myPersona.myComerciante !$= "-1"){
		%eval = %tela @ "Comerciante1_txt.text = $myPersona.myComerciante @ %percent;";
		eval(%eval);
	} else {
		%eval = %tela @ "Comerciante1_txt.text = %indefinido;";
		eval(%eval);
	}
	if($myPersona.myDiplomata !$= "-1"){
		%eval = %tela @ "Diplomata1_txt.text = $myPersona.myDiplomata @ %percent;";
		eval(%eval);
	} else {
		%eval = %tela @ "Diplomata1_txt.text = %indefinido;";
		eval(%eval);	
	}
	
	%eval = %tela @ "Creditos_txt.text = $myPersona.TAXOcreditos;";
	eval(%eval);
	%eval = %tela @ "Omnis_txt.text = $myPersona.TAXOomnis;";
	eval(%eval);
	
	clientPopularPokerTab();
	
	clientCalcularGauge("vitorias", 1, $myPersona.TAXOvitorias, -1, %tela, $myPersona.nome, $myPersona.especie);
	clientCalcularGauge("pontos", 1, $myPersona.TAXOpontos, -1, %tela, $myPersona.nome, $myPersona.especie);
	clientCalcularGauge("visionario", 1, $myPersona.TAXOvisionario, -1, %tela, $myPersona.nome, $myPersona.especie);
	clientCalcularGauge("arrebatador", 1, $myPersona.TAXOarrebatador, -1, %tela, $myPersona.nome, $myPersona.especie);
	clientCalcularGauge("comerciante", 1, $myPersona.myComerciante, $myPersona.TAXOvitorias, %tela, $myPersona.nome, $myPersona.especie);
	clientCalcularGauge("diplomata", 1, $myPersona.myDiplomata, $myPersona.TAXOvitorias, %tela, $myPersona.nome, $myPersona.especie);
	
	//PEA:
	clientPopularPEA(%tela);
	
	$noticias_handler.sortearNovaNoticia(); //SORTEIA UMA NOVA NOTÍCIA
}

//esta função é chamada quando o cara clica no loggedInSalasDeJogo_btn;
function clientGoToAtrio(){
	Canvas.popDialog(loggedInGui);
	Canvas.pushDialog(atrioGui);
	
	clientPopularDados("atrio"); //passa os dados para a tela do Átrio
	
	clientAskPopularSalas(); //pede pro server a relação de salas;
	
	if($myPersona.TAXOvitorias > 0 || $myPersona.especie $= "gulok"){
		atrioAcademia_btn.setActive(true);	
	} else {
		atrioAcademia_btn.setActive(false);	
	}
	$primeiraLoggedIn = false;	
	initAtrioChatGui();
	clientSetDTxt_salaInside(); //seta os textos da sala
}

function clientAskPopularSalas(){
	$PlayersNaSalaEmQueEstou = 0;
	clientPushAguardeMsgBox();
	commandToServer('pegarRelacaoDeSalas', "loggedIn"); //pede a relação das salas pro server, vindo da tela LoggedIn;
}

//marca no client quem é o $mySelf:
function clientCmdSetMyPlayer(%player, %planeta, %playersAtivos, %imperiaisBonus, %turnoTime){
	$tipoDeJogo = $salaEmQueEstouTipoDeJogo;
	$turnoDesteJogo = %turnoTime;
	$playersNesteJogo = %playersAtivos;
	$inGame = true;
	clientPopAguardeMsgBox(); //devolve o controle ao usuário
	clientResetIniciarJogoBtn(); //para de piscar o btn de iniciar jogo, caso estivesse piscando
	$rodadaAtual = 0;
	%eval = "$mySelf = $" @ %player @ ";";
	eval(%eval);
	if(%player $= "OBSERVADOR"){
		$souObservador = true;	
	}
	echo("Sou o " @ %player);
	$myPersona.planetaAtual = %planeta;
	clientCarregarTabuleiro(%planeta, %playersAtivos, %p1Oculto, %p2Oculto, %p3Oculto, %p4Oculto, %p5Oculto, %p6Oculto);
	
	Canvas.popDialog(newSalaInsideGui);
	Canvas.pushDialog(objetivosGuii);
	if($tipoDeJogo !$= "semPesquisas" && $tipoDeJogo !$= "poker"){
		echo("ImperiaisBonus = " @ %imperiaisBonus);
		$mySelf.imperiais = 10 + %imperiaisBonus; //o player já começa sabendo quantos imperiais ele vai ter
	} else {
		$mySelf.imperiais = 10; 
	}
	imperiais_txt.text = $mySelf.imperiais; 
	clientCmdPushAguardandoObjGui();
	$jogoEmDuplas = false; //zera, pois em seguida ele verifica se é mesmo em duplas ou não
}

function clientCarregarAlvosDeObjs(){
	if(!isObject($alvosObjPool1)){
		$alvosObjPool1 = new SimSet();
		$alvosObjPool2 = new SimSet();
		$alvosObjPool3 = new SimSet();
	} else {
		$alvosObjPool1.clear();
		$alvosObjPool2.clear();
		$alvosObjPool3.clear();
	}
	
	$alvosObjPool1.add(alvo1Obj1);
	$alvosObjPool1.add(alvo2Obj1);
	$alvosObjPool1.add(alvo3Obj1);
	$alvosObjPool1.add(alvo4Obj1);
	$alvosObjPool2.add(alvo1Obj2);
	$alvosObjPool2.add(alvo2Obj2);
	$alvosObjPool2.add(alvo3Obj2);
	$alvosObjPool2.add(alvo4Obj2);
	$alvosObjPool3.add(alvo1Obj1e2);
	$alvosObjPool3.add(alvo2Obj1e2);
	$alvosObjPool3.add(alvo3Obj1e2);
	$alvosObjPool3.add(alvo4Obj1e2);
}

//função para carregar o tabuleiro e reinicializar variáveis de área
function clientCarregarTabuleiro(%planeta, %playersAtivos){
	canvas.popDialog(genericSplash); //apaga a tela de carregando
	%arquivo = "game/data/levels/" @ %planeta @ ".t2d";
	sceneWindow2D.loadLevel(%arquivo);
	$strategyScene = sceneWindow2D.getSceneGraph();
	$planetaAtual = %planeta;
	
	%eval = "clientCriarPlaneta" @ %planeta @ "();";
	eval(%eval);
	
	ocultarInGame_btn.setVisible(false); //apaga o btn de Ocultar
	firstStartTimerOBJ_txt.setVisible(false);
	firstStartTimerGRP_txt.setVisible(false);
	
	//mostrar cartas de objetivos:
	for(%i = 1; %i < 27; %i++){
		%eval = "%nomeDaCarta = obj" @ %i @ "_carta;";
		eval(%eval);
		%nomeDaCarta.setVisible(true);	
	}
	
	//client (re)seta todas as áreas (resolvendo seu status):
	if(!$primeiroJogo){
		%eval = "%planetaSimAreas = $areasDe" @ %planeta @ ";";
		eval(%eval);
		for(%i = 0; %i < %planetaSimAreas.getCount(); %i++){
			%area = %planetaSimAreas.getObject(%i);
			%area.dono = 0; // Começa pertencendo a ninguém
			%area.desprotegida = false; //não está desprotegida porque nem tem Base ainda
			%area.pos0Quem = "nada"; //não há ninguém nas posições fortes
			%area.pos1Quem = "nada";
			%area.pos2Quem = "nada";
			%area.pos0Flag = false; //true ou false mesmo, significando que tem ou não tem base lá.
			%area.pos1Flag = "nada"; //as positionFlags não guardam 0 ou 1, mas sim [a classe da peça] ou ["nada"];
			%area.pos2Flag = "nada";
			%area.myPos3List.clear();
			if(isObject(%area.myPos4List)){
				%area.myPos4List.clear();
			}
		}
	}
	
	
	//apagar os infoMarkers e explMarkers:
	for(%i = 0; %i < 80; %i++){
		%info = clientFindInfo(%i + 1);
		if(isObject(%info.myMark)){
			%info.myMark.safeDelete();	
		}
		if(isObject(%info.myExplMark)){
			%info.myExplMark.safeDelete();	
		}
		%info.jahFoiOferecida = false;
	}
	
	jogoInvalidoMsgBox.setVisible(false); //apaga a msgBox de jogo inválido
	
	//torna inativos os btns do gui:
	venderRecursos_btn.setActive(false);
	clientDesligarVenderRecursos();
	toggleAcordos_btn.setActive(false);
	embargar_btn.setActive(false);
	mais_btn.setActive(false);
	mais_btn.setBitmap("game/data/images/maisBTN");
	reciclagemHudBtn.setVisible(false);
	unitHud.setVisible(false); 
	
	clientInitPropostasPool(); //(re)seta a propostasPool do client
	clientPropostasGuiClear(); //limpa o hud de propostas (o canto esquerdo da tela)
	clientApagarPropHud(); //apaga o propHud e a msg de proposta recebida
	
	//prepara para sortear objetivos:
	setObjetivosZoom();
	clientZerarObjMarkers();
	clientCarregarAlvosDeObjs();
	objetivosGuiTxt.bitmap = "~/data/images/escolhaObjsTXT";
	
	//apaga os grupos conquistados:
	if(%planeta $= "Terra"){
		brasilON_over.setVisible(false);
		africaON_over.setVisible(false);
		australiaON_over.setVisible(false);	
		orienteON_over.setVisible(false);
		canadaON_over.setVisible(false);
		euaON_over.setVisible(false);
		chinaON_over.setVisible(false);
		russiaON_over.setVisible(false);
		europaON_over.setVisible(false);
	} else if(%planeta $= "Ungart"){
		PrExON_over.setVisible(false);
		ChOcON_over.setVisible(false);
		PaGuON_over.setVisible(false);	
		VaNoON_over.setVisible(false);
		ChOrON_over.setVisible(false);
		VaOrON_over.setVisible(false);
		CaOrON_over.setVisible(false);
		DeExON_over.setVisible(false);
		PlDoON_over.setVisible(false);
		VaNoON_over.setVisible(false);
		CaNoON_over.setVisible(false);
		IlVuON_over.setVisible(false);
		MoVeON_over.setVisible(false);
	} else if(%planeta $= "Teluria"){
		terionON_over.setVisible(false);
		nirON_over.setVisible(false);
		malikON_over.setVisible(false);	
		gorukON_over.setVisible(false);
		karzinON_over.setVisible(false);
		zaviniaON_over.setVisible(false);
		argoniaON_over.setVisible(false);
		lorniaON_over.setVisible(false);
		valinurON_over.setVisible(false);
		dharinON_over.setVisible(false);
		vuldanON_over.setVisible(false);
		kelturON_over.setVisible(false);
		geoCanhaoON_over.setVisible(false);
		nexusON_over.setVisible(false);
	}
	
	clientClearCruzarFlechas(); //apagar flechas de cruzar oceanos
	resetSelection(); //apagar seleção
	canvas.popDialog(baterGui); //garante que o baterGui não fique por cima do tabuleiro no segundo jogo
	initMissionMarksPool(); //zera a pool de missionMarkes
	clientApagarDoacaoRecebidaGui(); //apaga o gui de doação recebida, caso esteja ligado;
	comercianteMark_img.setVisible(false); // desliga o comercianteMark de um jogo para o outro;
	Canvas.popDialog(doarGui); //desliga o doarGui
	clientApagarDoacaoRecebidaGui(); //desliga o gui de doação recebida
	$mySelf.decretouMoratoria = false;
	
	//seta ou volta pra página 1 do explHud:
	$explHudPagAtual = 1;
	$vendoPag1ExplHud = 1;
	$mySelf.trocas = 0; //pro comerciante
		
	clientDesligarQuemDesembarcaHud(); //apaga o desembarcarHud, caso estivesse ligado
	
	//zera o Undo:
	$myUndoCount = 0;
	clientClearUndo();
	
	//ativa o botão de investir recursos caso o sujeito tenha alguma pesquisa em andamento:
	%podeInvestir = clientGetPodeInvestir(%playersAtivos);
	if(%podeInvestir){
		investirRecursos_btn.setActive(true);
	} else {
		investirRecursos_btn.setActive(false);
	}
	clientAtualizarPEATab(); //atualiza a tab de investimento;
	clientDesligarInvestirRecursos(); //apaga a tab de investimento;
		
	clientFecharIntelGui(); //fecha a IntelTab, caso o usuário estivesse com ela aberta quando o último jogo terminou
	
	clientInitAcaVars();
	
	nave_BP.setVisible(false); //apaga a nave pra ela não aparecer no zoom out dos objetivos;
	
	desligarSniperShot(); //desliga o sniper
	
	%eval = "setCruzarOceanos" @ %planeta @ "();"; //marca nas áreas de borda quem são suas fronteiras
	eval(%eval);
	
	$mySelf.terceiroLiderOn = false; //marca que o terceiro líder não está no tabuleiro;
		
	$disparoOrbitalON = false;
	canhaoOrbital_btn.setVisible(false);
	
	$mySelf.geoDisparos = 0; //zera os geoDisparos
	
	$jogoEmDuplas = false; //depois será um parâmetro.
	
	$naumApagueiAguardeObjetivo = true; //variável de inicialização (tá dando pau no cancel());
	
	$rodadaAtual = 0;
	
	tut_InitArrow(); //inicia a tutArrow:
	tut_fecharJogo_btn.setVisible(false); //depois verifica se é um jogo tutorial e liga o btn se for necessário	
	tut_berco.setVisible(false); //depois verifica se é um jogo tutorial e liga o berço se for necessário	
	tut_clearTips(); //apaga qq msg de tutorial que pudesse estar ligada, bem como todas as tutArrows
	
	clientSetPagPropTab(1); //ajusta a propTab
	propTab_mais_btn.setBitmap("game/data/images/propMaisBTN");
	propTab_mais_btn.setActive(false);
	
	apagarDragnalBtns(); //apaga o menu de dragnal
	clientClearAllViruses();
	apagarBtnsGulok();
	apagarBtnsHumanos();
	
	$guloksDespertaram = false;
	hud_guloks_obj_tab.setvisible(false);
	msgBoxParabens_guloks.setVisible(false);	//tira a msg de parabéns do batergui
	
	clientSetNaviosBtnImg();
	clientSetBasesBtnImg();
}

function clientGetPodeInvestir(%playersAtivos)
{
	if($myPersona.aca_pea_id $= "0" || $myPersona.aca_pea_id $= "")
		return false;
	
	if($salaEmQueEstouTipoDeJogo $= "emDuplas" && %playersAtivos == 6)
		return true;
	
	if(%playersAtivos > 2 && $salaEmQueEstouTipoDeJogo !$= "emDuplas")
		return true;
		
	return false;
}

function clientInitAcaVars(){
	if($myPersona.gulok){
			
	} else {
		//Refinaria:
		if($myPersona.aca_v_3 < 3){
			%vRef = 5;	
		} else {
			%vRef = 4;
		}
		refinaria_btn.setBitmap("game/data/images/refinariaBtn" @ %vRef); //coloca a imagem correta no botão de refinaria
		
		//AirDrop:
		$mySelf.airDrops = $myPersona.aca_v_5;
		if($salaEmQueEstouTipoDeJogo $= "poker")
			$mySelf.airDrops = 0;
		
		if($mySelf.airDrops == 3){
			$mySelf.airDrops = 2;	
		}
		if($myPersona.aca_v_5 > 0){
			airDropHud_btn.setActive(true);	
		} else {
			airDropHud_btn.setActive(false);	
		}
		
		//filantropia:
		$mySelf.filantropiasEfetuadas = 0; //zera o número de filantropias efetuadas	
	
		//Canhão Orbital:
		$mySelf.disparosOrbitais = 0;
		$mySelf.reqCanhao = 9 - ($myPersona.aca_a_2 * 2);
		
		//Ocultar:
		if($myPersona.aca_av_3 > 0){
			$mySelf.ocultarCusto = 7 - ($myPersona.aca_av_3 * 2);	
			$mySelf.reqOcultar = 7 - ($myPersona.aca_av_3 * 2);	
		}
	}
}

function clientCmdSetMyDupla(%playersAtivos){
	$jogoEmDuplas = true;
	if(%playersAtivos == 4){
		$player1.myDupla = $player3;
		$player2.myDupla = $player4;
		$player3.myDupla = $player1;
		$player4.myDupla = $player2;
	} else if(%playersAtivos == 6){
		$player1.myDupla = $player4;
		$player2.myDupla = $player5;
		$player3.myDupla = $player6;
		$player4.myDupla = $player1;
		$player5.myDupla = $player2;
		$player6.myDupla = $player3;
	}
}

///////////////////////////////////////////
//Sorteio de ordem:
//
function clientCmdSorteioDeOrdemShow(%playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome){
	clientCmdPopAguardandoObjGui(); //começa sem a msg de aguardando;
	if($estouNoTutorial){
		canvas.pushDialog(carregandoGruposGui);
		clientMostrarGrupos($planetaAtual);
		schedule(21000, 0, "clientCmdSorteioDeOrdemShowDeveras", %playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome);
		schedule(21500, 0, "clientPopCarregandoGruposGui");
	} else {
		clientCmdSorteioDeOrdemShowDeveras(%playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome);
		clientPopCarregandoGruposGui();
	}	
}

function clientCmdSorteioDeOrdemShowDeveras(%playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome){
	$numDePlayersNestaPartida = %playersAtivos;
	for(%i = 1; %i < %playersAtivos + 1; %i++){
		%eval = "$player" @ %i @ ".nome = %p" @ %i @ "Nome;";
		eval(%eval);
	}
	clientClearSorteioOGui();
	clientCmdPopAguardandoObjGui();
	aguardandoTXT.text = "Sorteando ordem...";
	canvas.pushDialog(sortearOrdemGui);
	objetivosGuiTxt.setVisible(false); //apaga o texto "escolha dois objetivos"
	clientOSortGradualVisible(%playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome);
	if($jogoEmDuplas){
		MsgBoxDuplas_img.setVisible(true);	
		clientPopularDuplas(%playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome);
	} else {
		MsgBoxDuplas_img.setVisible(false);	
	}
}

function clientClearSorteioOGui(){
	for(%i = 1; %i < 7; %i++){
		%nada = "";
		%eval = "sorteandoOrdemFundo" @ %i @ "_txt.text = %nada;";
		eval(%eval);
		%eval = "sorteandoOrdemFundo" @ %i @ "_txt.setVisible(false);";
		eval(%eval);
		%eval = "sorteandoOrdemFundo" @ %i @ "_img.setVisible(false);";
		eval(%eval);
	}
	
	MsgBoxSorteandoOrdem_img.bitmap = "game/data/images/MsgBoxSorteandoOrdem.png";
}


function clientOSortGradualVisible(%playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome){
	%ativoBitmap = "game/data/images/MsgBoxSorteandoOrdemPLACE.png";
	%inativoBitmap = "game/data/images/MsgBoxSorteandoOrdemPLACEgray.png";
	
	for(%i = 1; %i < 7; %i++){
		%eval = "sorteandoOrdemFundo" @ %i @ "_img.setVisible(true);";
		eval(%eval);
		if(%i <= %playersAtivos){
			%eval = "sorteandoOrdemFundo" @ %i @ "_img.bitmap = %ativoBitmap;";
			eval(%eval);
			%eval = "sorteandoOrdemFundo" @ %i @ "_txt.setVisible(true);";
			eval(%eval);
		} else {
			%eval = "sorteandoOrdemFundo" @ %i @ "_img.bitmap = %inativoBitmap;";
			eval(%eval);	
		}
				
		schedule(2000, 0, "clientShowSorteioONomes", %playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome); //começa a mostrar os nomes mudando de lugar;
		$clientSorteioONum = 0;
	}
}

//função de dado:
function dado(%faces, %bonus){
	%resultado = getRandom(1, %faces) + %bonus;
	return %resultado;
	
	//echo("DADO RESULT = " @ %resultado);
}

//função de dado com números negativos:
function dadoReal(%min, %max){
	%resultado = getRandom(%min, %max);
	return %resultado;
}

function clientShowSorteioONomes(%playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome){
	//echo("clientShowSorteioONomes(" @ %playersAtivos @ "," @ %p1Nome @ "," @ %p2Nome @ "," @ %p3Nome @ "," @ %p4Nome @ "," @ %p5Nome @ "," @ %p6Nome @ ")");
	if($clientSorteioONum < 10){
		%nomesOnline = new SimSet();
		for(%i = 1; %i < %playersAtivos+1; %i++){
			%eval = "%p" @ %i @ "ScrNome = new ScriptObject(){nome = %p" @ %i @ "Nome;};";
			eval(%eval);
			%eval = "%nome = %p" @ %i @ "ScrNome;";
			eval(%eval);
			%nomesOnline.add(%nome);
		}
		
		%nomesCount = %nomesOnline.getCount();
		for(%i = 0; %i < %nomesCount; %i++){
			%sorteio = dado(%nomesOnline.getCount(), -1);
			%nome[%i] = %nomesOnline.getObject(%sorteio);
			%nomesOnline.remove(%nome[%i]);
		}	
		
		for(%i = 0; %i < %nomesCount; %i++){
			%nome = %nome[%i].nome;
			%eval = "sorteandoOrdemFundo" @ %i + 1 @ "_txt.text = %nome;";
			eval(%eval);
		}
		$clientSorteioONum++;
		$clientSorteioONumSchedule = schedule(400, 0, "clientShowSorteioONomes", %playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome); 
	} else {
		cancel($clientSorteioONumSchedule);
		for(%i = 0; %i < %playersAtivos; %i++){
			%eval = "%myName = %p" @ %i+1 @ "Nome;";
			eval(%eval);
			%eval = "sorteandoOrdemFundo" @ %i + 1 @ "_txt.text = %myName;";
			eval(%eval);
			//echo("MARCANDO NOME NA POSIÇÃO " @ %i + 1 @ ": " @ %myName);
		}
		//schedule(8000, 0, "clientApagarSorteioOMsgBox");
		schedule(8000, 0, "clientApagarSorteioOMsgBox");
		schedule(4000, 0, "clientMarcarOrdemDefinida");
		$clientSorteioONum = 0;
	}
}


function clientMarcarOrdemDefinida(){
	MsgBoxSorteandoOrdem_img.bitmap = "game/data/images/MsgBoxSorteandoOrdemDefinida.png";
	aguardandoTXT.text = $player1.nome @ " está jogando";
	$jogadorDaVez = $player1;
}

function clientPopularDuplas(%playersAtivos, %p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome){
	%ativoBitmap = "game/data/images/MsgBoxSorteandoOrdemPLACE.png";
	%inativoBitmap = "game/data/images/MsgBoxSorteandoOrdemPLACEgray.png";
	
	for(%i = 1; %i < 7; %i++){
		%eval = "duplasFundo" @ %i @ "_img.setVisible(true);";
		eval(%eval);
		if(%i <= %playersAtivos){
			%eval = "duplasFundo" @ %i @ "_img.bitmap = %ativoBitmap;";
			eval(%eval);
			%eval = "duplasFundo" @ %i @ "_txt.setVisible(true);";
			eval(%eval);
		} else {
			%eval = "duplasFundo" @ %i @ "_img.bitmap = %inativoBitmap;";
			eval(%eval);
			%eval = "duplasFundo" @ %i @ "_txt.setVisible(false);";
			eval(%eval);
		}
	}
	
	if(%playersAtivos == 4){
		duplasFundo1_txt.text = %p1Nome;
		duplasFundo2_txt.text = %p3Nome;
		duplasFundo3_txt.text = %p2Nome;
		duplasFundo4_txt.text = %p4Nome;
	} else {
		duplasFundo1_txt.text = %p1Nome;
		duplasFundo2_txt.text = %p4Nome;
		duplasFundo3_txt.text = %p2Nome;
		duplasFundo4_txt.text = %p5Nome;
		duplasFundo5_txt.text = %p3Nome;
		duplasFundo6_txt.text = %p6Nome;
	}
}



function clientApagarSorteioOMsgBox(){
	canvas.popDialog(sortearOrdemGui);
	if($mySelf.id $= "player1"){
		if($naumApagueiAguardeObjetivo){
			$naumApagueiAguardeObjetivo = false;
			Canvas.popDialog(aguardandoObjGui);	
			if($estouNoTutorial){
				TUTmsg("objetivos");
			} else {
				clientCmdSetJogadorDaVez("player1", 0);
				firstStartTimer_txt.setVisible(true);	
			}
		}
	} else {
		if($naumApagueiAguardeObjetivo){
			$naumApagueiAguardeObjetivo = false;
			Canvas.popDialog(aguardandoObjGui);	
			Canvas.pushDialog(aguardandoObjGui);
			clientCmdSetJogadorDaVez("player1", 0);
			if(!$estouNoTutorial){
				firstStartTimer_txt.setVisible(true);		
			}
		}
	}
	objetivosGuiTxt.setVisible(true); //mostra o texto "escolha dois objetivos"
	MsgBoxDuplas_img.setVisible(false);	//apaga a msgBox de duplas, caso ela esteja ligada
	if(!$estouNoTutorial)
		clientPushSincronizandoMsgBox();
		
	clientAskFinalizeiSorteioDeOrdemShow();
}

function clientAskFinalizeiSorteioDeOrdemShow()
{
	commandToServer('finalizeiSorteioDeOrdemShow');
}

function clientPushSincronizandoMsgBox()
{
	canvas.pushDialog(sincronizandoGui);
	palcoTurnoTimer.pauseTimer(true);
}

function clientPopSincronizandoMsgBox()
{
	canvas.popDialog(sincronizandoGui);	
}

function clientCmdGoodToGo()
{
	clientPopSincronizandoMsgBox();	
	clientResumeTurnoTimer();
}
//////////////////////////////////////////





function clientZerarObjMarkers(){
	//zera os markers:
	%pos = "-40 -100";
	for(%i = 1; %i < 5; %i++){
		%eval = "alvo" @ %i @ "Obj1.setPosition(%pos);";
		eval(%eval);
		%eval = "alvo" @ %i @ "Obj2.setPosition(%pos);";
		eval(%eval);
		%eval = "alvo" @ %i @ "Obj1e2.setPosition(%pos);";
		eval(%eval);
		%eval = "alvo" @ %i @ "Obj1.setBlendColor(1, 1, 1, 1);";	
		eval(%eval);
		%eval = "alvo" @ %i @ "Obj2.setBlendColor(1, 1, 1, 1);";
		eval(%eval);
		%eval = "alvo" @ %i @ "Obj1e2.setBlendColor(1, 1, 1, 1);";
		eval(%eval);
	}
}

function clientMuquiarObjMarkers(){
	//diminui o alpha dos markers:
	for(%i = 1; %i < 5; %i++){
		%eval = "alvo" @ %i @ "Obj1.setBlendColor(1, 1, 1, 0.5);";	
		eval(%eval);
		%eval = "alvo" @ %i @ "Obj2.setBlendColor(1, 1, 1, 0.5);";
		eval(%eval);
		%eval = "alvo" @ %i @ "Obj1e2.setBlendColor(1, 1, 1, 0.5);";
		eval(%eval);
	}
}

function clientPiscarObjMarkers(){
	if($hiObjMarkersOn){
		if($objMarkersHi){
			for(%i = 0; %i < 4; %i++){
				%alvo1 = $alvosObjPool1.getObject(%i);
				%alvo2 = $alvosObjPool2.getObject(%i);
				%alvo3 = $alvosObjPool3.getObject(%i);
				
				%alvo1.setImageMap(obj1MarkPretoImageMap);
				%alvo2.setImageMap(obj2MarkPretoImageMap);
				%alvo3.setImageMap(obj1e2MarkPretoImageMap);
			}
			$objMarkersHi = false;
		} else {
			for(%i = 0; %i < 4; %i++){
				%alvo1 = $alvosObjPool1.getObject(%i);
				%alvo2 = $alvosObjPool2.getObject(%i);
				%alvo3 = $alvosObjPool3.getObject(%i);
				
				%alvo1.setImageMap(obj1MarkAmareloImageMap);
				%alvo2.setImageMap(obj2MarkAmareloImageMap);
				%alvo3.setImageMap(obj1e2MarkAmareloImageMap);
			}
			$objMarkersHi = true;
		}
		cancel($objMarkersPiscarSchedule);
		$objMarkersPiscarSchedule = schedule(400, 0, "clientPiscarObjMarkers");
	}
}

//////
//
//Turnos:
//
function clientCmdSetJogadorDaVez(%playerId, %rodada){
	%eval = "$jogadorDaVez = $" @ %playerId @ ";";
	eval(%eval);
	aguardandoTXT.text = $jogadorDaVez.nome @ " está jogando";
	
	$rodadaAtual = %rodada;
	
	if(%rodada == 1){
		$primeiraRodada = true;
		rodadaAtual_txt.text = "1";
	} else if ($rodadaAtual > 1){
		$primeiraRodada = false;	
	}
	
	rodadaAtual_txt.text = %rodada;
	
	turno_img.bitmap = "~/data/images/turno" @ $jogadorDaVez.myColor @ ".png";
	turnoNome_txt.text = $jogadorDaVez.nome; 
	tempo_img.bitmap = "~/data/images/tempo" @ $jogadorDaVez.myColor @ ".png";
	
	apagarBtns(); //apaga o HUD atual //função no arquivo MainGui.cs
	resetSelectionMarks(); //coloca todas as marcas de seleção fora da tela;
	Foco.clear(); //limpa o foco, ou seja, des-seleciona o que estava selecionado
	
	palcoTurnoTimer.iniciarTimer();
}

//////////
//
// Objetivos:
//
//pedir pro server:
function clientAskPassarAVezEscolhendoGrupos(){
	if(!$estouNoTutorial){
		commandToServer('passarAVezEscolhendoGrupos');	
	} else {
		clientCmdSetJogadorDaVez("player2", 0);	
	}
}

function clientAskSortearObjetivo(%carta){  
	if(!$estouNoTutorial){
		clientPushAguardeMsgBox();
		commandToServer('sortearObjetivo', %carta);	
	} else {
		clientCmdApagarCartaObjetivo(%carta);
		if($mySelf.mySimObj.getCount() == 0){
			clientCmdAddObjetivo(7); //Brasil + Urânio
		} else if($mySelf.mySimObj.getCount() == 1){
			clientCmdAddObjetivo(19); //Rússia + Mar
			aguardandoTXT.text = "Adversário está jogando";
			canvas.pushDialog(aguardandoObjGui);
			//vai pra escolha de cores:
			schedule(3000, 0 , "clientCmdPushCoresGui");
			schedule(3002, 0 , "TUTmsg", "tabuleiro");
			schedule(3004, 0 , "clientPopAguardandoObjGui");
		} 
	}
}

function clientPopAguardandoObjGui(){
	canvas.popDialog(aguardandoObjGui);	
}

//quando alguém escolhe um objetivo:
function clientCmdApagarCartaObjetivo(%carta){
	%eval = "%nomeDaCarta = obj" @ %carta @ "_carta;";
	eval(%eval);
	%nomeDaCarta.setVisible(false);	
}

function clientFillSortObj(%num, %objetivo, %gruposGui){
	if(%gruposGui){
		%eval = "gruposGuiObjSorteado" @ %num @ "_img.setVisible(true);";	
		eval(%eval);
		%eval = "%myTtl = obj" @ %num @ "SortG_ttl;";
		eval(%eval);
		%eval = "%myL2 = obj" @ %num @ "SortG_L2;";
		eval(%eval);
		%eval = "%myImg = obj" @ %num @ "SortG_img;";
		eval(%eval);
		%eval = "%myL3 = obj" @ %num @ "SortG_L3;";
		eval(%eval);
	} else {
		%eval = "hudObjSorteado" @ %num @ "_img.setVisible(true);";	
		eval(%eval);
		%eval = "%myTtl = obj" @ %num @ "Sort_ttl;";
		eval(%eval);
		%eval = "%myL2 = obj" @ %num @ "Sort_L2;";
		eval(%eval);
		%eval = "%myImg = obj" @ %num @ "Sort_img;";
		eval(%eval);
		%eval = "%myL3 = obj" @ %num @ "Sort_L3;";
		eval(%eval);
	}
	if(%objetivo.grupo !$= "0"){
		%myTtl.bitmap = "~/data/images/objetivos/obj_ttl_conquisteOGrupo.png";
		%myL2.bitmap = "~/data/images/objetivos/obj_L2_" @ %objetivo.grupo @ ".png";
		%myImg.bitmap = "~/data/images/objetivos/obj_img_" @ $planetaAtual @ "_" @ %objetivo.grupo @ ".png";
		if(%objetivo.minerios > 0){
			%myL3.bitmap = "~/data/images/objetivos/obj_L3_" @ %objetivo.minerios @ "minerios.png";
		} else if(%objetivo.petroleos > 0){
			%myL3.bitmap = "~/data/images/objetivos/obj_L3_" @ %objetivo.petroleos @ "petroleos.png";
		} else if(%objetivo.uranios > 0){
			%myL3.bitmap = "~/data/images/objetivos/obj_L3_" @ %objetivo.uranios @ "uranios.png";
		} else if(%objetivo.baias > 0){
			%myL3.bitmap = "~/data/images/objetivos/obj_L3_" @ %objetivo.baias @ "mares.png";
		}
	} else {
		if(%objetivo.territorios > 0){
			%myTtl.bitmap = "~/data/images/objetivos/obj_ttl_conquiste" @ %objetivo.territorios @ ".png";
			%myL2.bitmap = "~/data/images/objetivos/obj_L2_terra.png";
			%myImg.bitmap = "~/data/images/objetivos/obj_img_" @ $planetaAtual @ "_terra.png";
			%myL3.bitmap = "~/data/images/objetivos/obj_L3_aSuaEscolha.png";
		} else if(%objetivo.baias > 0){
			%myTtl.bitmap = "~/data/images/objetivos/obj_ttl_conquiste" @ %objetivo.baias @ ".png";
			%myL2.bitmap = "~/data/images/objetivos/obj_L2_mar.png";
			%myImg.bitmap = "~/data/images/objetivos/obj_img_" @ $planetaAtual @ "_mar.png";
			%myL3.bitmap = "~/data/images/objetivos/obj_L3_aSuaEscolha.png";
		} else if(%objetivo.petroleos > 0){
			%myTtl.bitmap = "";
			%myL2.bitmap = "~/data/images/objetivos/obj_L2_acumule.png";
			%myImg.bitmap = "~/data/images/objetivos/obj_img_petroleo.png";
			%myL3.bitmap = "~/data/images/objetivos/obj_L3_" @ %objetivo.petroleos @ "petroleosSoh.png";
		}
	}
}
//Adicionar objetivos:
function clientCmdAddObjetivo(%objNum){
	clientPopAguardeMsgBox();
	%mySimObj = $mySelf.mySimObj;
	%obj = clientFindObjetivo(%objNum);
	%mySimObj.add(%obj);
	echo("OBJETIVO SORTEADO: " @ %obj.desc1 @ " + " @ %obj.desc2);
			
	if(%mySimObj.getCount() == 1){
		clientFillSortObj(1, %obj);
	} else {
		clientFillSortObj(2, %obj);
	}
	
	switch$ (%obj.desc1){
		case "Austrália":
		clientObjMarkAreas(2, "sydney", "perth");
		
		case "Rússia":
		clientObjMarkAreas(4, "moscou", "kirov", "omsk", "magadan");
		
		case "África":
		clientObjMarkAreas(4, "marrakesh", "cairo", "kinshasa", "cidadeDoCabo");
		
		case "Canadá":
		clientObjMarkAreas(4, "bakerlake", "vancouver", "toronto", "montreal");
		
		case "EUA":
		clientObjMarkAreas(3, "losAngeles", "houston", "novaYork");
		
		case "Brasil":
		clientObjMarkAreas(2, "manaus", "saoPaulo");
		
		case "Europa":
		clientObjMarkAreas(3, "londres", "estocolmo", "comunidadeEuropeia");
		
		case "China":
		clientObjMarkAreas(3, "lhasa", "pequim", "xangai");
		
		case "Oriente":
		clientObjMarkAreas(2, "bagda", "cabul");
		
		case "Ch.Ocidental":
		clientObjMarkAreas(2, "UNG_ChOc01", "UNG_ChOc02");
		
		case "Ch.Oriental":
		clientObjMarkAreas(2, "UNG_ChOr01", "UNG_ChOr02");
		
		case "Praias":
		clientObjMarkAreas(2, "UNG_PrEx01", "UNG_PrEx02");
		
		case "Platô":
		clientObjMarkAreas(2, "UNG_PlDo01", "UNG_PlDo02");
		
		case "Deserto":
		clientObjMarkAreas(2, "UNG_DeEx01", "UNG_DeEx02");
		
		case "M.Verticais":
		clientObjMarkAreas(4, "UNG_MoVe01", "UNG_MoVe02", "UNG_MoVe03", "UNG_MoVe04");
		
		case "I.Vulcânica":
		clientObjMarkAreas(2, "UNG_IlVu01", "UNG_IlVu02");
		
		case "Cn.Nórdico":
		clientObjMarkAreas(4, "UNG_CaNo01", "UNG_CaNo02", "UNG_CaNo03", "UNG_CaNo04");
		
		case "V.Nórdico":
		clientObjMarkAreas(4, "UNG_VaNo01", "UNG_VaNo02", "UNG_VaNo03", "UNG_VaNo04");
		
		case "Cn.Oriental":
		clientObjMarkAreas(4, "UNG_CaOr01", "UNG_CaOr02", "UNG_CaOr03", "UNG_CaOr04");
		
		case "V.Oriental":
		clientObjMarkAreas(3, "UNG_VaOr01", "UNG_VaOr02", "UNG_VaOr03");
		
		case "Pântano":
		clientObjMarkAreas(3, "UNG_PaGu01", "UNG_PaGu02", "UNG_PaGu03");
		
		case "V.Gulok":
		clientObjMarkAreas(2, "UNG_VaGu01", "UNG_VaGu02");
		
		case "Térion":
		clientObjMarkAreas(2, "TEL_terion01", "TEL_terion02");
		
		case "Nir":
		clientObjMarkAreas(2, "TEL_nir01", "TEL_nir02");
		
		case "Karzin":
		clientObjMarkAreas(2, "TEL_karzin01", "TEL_karzin02");
		
		case "Goruk":
		clientObjMarkAreas(2, "TEL_goruk01", "TEL_goruk02");
		
		case "Malik":
		clientObjMarkAreas(2, "TEL_malik01", "TEL_malik02");
		
		case "Zavínia":
		clientObjMarkAreas(2, "TEL_zavinia01", "TEL_zavinia02");
		
		case "Lórnia":
		clientObjMarkAreas(2, "TEL_lornia01", "TEL_lornia02");
		
		case "Argônia":
		clientObjMarkAreas(4, "TEL_argonia01", "TEL_argonia02", "TEL_argonia03", "TEL_argonia04");
		
		case "Dharin":
		clientObjMarkAreas(3, "TEL_dharin01", "TEL_dharin02", "TEL_dharin03");
		
		case "Valinur":
		clientObjMarkAreas(2, "TEL_valinur01", "TEL_valinur02");
		
		case "Keltur":
		clientObjMarkAreas(3, "TEL_keltur01", "TEL_keltur02", "TEL_keltur03");
		
		case "Vuldan":
		clientObjMarkAreas(3, "TEL_vuldan01", "TEL_vuldan02", "TEL_vuldan03");
		
		case "Nexus":
		clientObjMarkAreas(1, "TEL_nexus01");
		
		case "Geo-Canhão":
		clientObjMarkAreas(1, "TEL_canhao01");
	}
}

function clientObjMarkAreas(%numDeMarkers, %area1, %area2, %area3, %area4){
	if($mySelf.mySimObj.getCount() == 1){
		%objNum = 1;	
	} else if($mySelf.mySimObj.getCount() == 2){
		%objNum = 2;
		if($mySelf.mySimObj.getObject(0).desc1 $= $mySelf.mySimObj.getObject(1).desc1){ //se ambos os objetivos tiverem os mesmos grupos
			%objNum = "1e2";
			clientZerarObjMarkers(); //apaga os primeiros markers, que serviam só para o primeiro objetivo
		}
	}
	//pega as áreas recém carregadas:
	%eval = "%area1 = " @ %area1 @ ";";
	eval(%eval);
	%eval = "%area2 = " @ %area2 @ ";";
	eval(%eval);
	%eval = "%area3 = " @ %area3 @ ";";
	eval(%eval);
	%eval = "%area4 = " @ %area4 @ ";";
	eval(%eval);
	
	
	//marca corretamente:
	for(%i = 1; %i < %numDeMarkers + 1; %i++){
		%eval = "alvo" @ %i @ "Obj" @ %objNum @ ".setPosition(%area" @ %i @ ".pos1);";
		eval(%eval);
	}
	$hiObjMarkersOn = true;
	clientPiscarObjMarkers();
}

//quando todos já escolheram:
function clientApagarObjetivos(){
	for(%i = 1; %i < 27; %i++){
		%eval = "%nomeDaCarta = obj" @ %i @ "_carta;";
		eval(%eval);
		%nomeDaCarta.setVisible(false);	
	}
}


//acho que esta funcao naum estah em uso
//apaga o gui de escolha de objetivos:
function clientCmdPopObjetivosGui(){
	Canvas.popDialog(objetivosGuii);
	Canvas.pushDialog(escolhaDeCores);
	apagarBtns();	
	
}


function clientZerarPlayersHuds(){
	for(%i = 1; %i < 7; %i++){
		%eval = "p" @ %i @ "Hud.setVisible(false);";
		eval(%eval);
	}
}




//
// Cores:
//
function clientCmdPushCoresGui(){
	objetivosGuiTxt.bitmap = "~/data/images/escolhaCorTXT"; // seta a instrução
	clientZerarPlayersHuds(); //apaga os huds de players, caso este não seja o primeiro jogo
	//mostrar todas as cores:
	verde_btn.setVisible(true);
	amarelo_btn.setVisible(true);
	vermelho_btn.setVisible(true);
	azul_btn.setVisible(true);
	roxo_btn.setVisible(true);
	laranja_btn.setVisible(true);
	indigo_btn.setVisible(true);
	
	clientApagarObjetivos();
	Canvas.popDialog(aguardandoObjGui);
	Canvas.pushDialog(escolhaDeCores);
	clientCmdPushAguardandoObjGui();
	if($mySelf == $jogadorDaVez){
		Canvas.popDialog(aguardandoObjGui);
	}
}

function clientAskEscolherCor(%cor){
	if(!$estouNoTutorial){
		clientPushAguardeMsgBox();
		commandToServer('escolherCor', %cor);	
	} else {
		%eval = "%corEscolhida = $" @ %cor @ ";";
		eval(%eval);
		%eval = "%corClicada =" SPC %cor @ "_btn;";
		eval(%eval);
		clientCmdSetColor(%corEscolhida, "player1", %corClicada);
		aguardandoTXT.text = "Adversário está jogando";
		canvas.pushDialog(aguardandoObjGui);
		//o Adversário escolhe vermelho se vc não tiver escolhido, ou verde se vc escolheu vermelho;
		if(%cor $= "vermelho"){
			schedule(3000, 0, "clientCmdSetColor", $verde, "player2", "verde_btn");
		} else {
			schedule(3000, 0, "clientCmdSetColor", $vermelho, "player2", "vermelho_btn");
		}
		schedule(3300, 0, "clientCmdPopCoresGui", $myPersona.nome, "Adversário");
		schedule(3302, 0, "TUTmsg", "gruposIniciais");
		schedule(3304, 0, "clientPopAguardandoObjGui");
	}
}

function clientCmdSetColor(%color, %player, %botaoClicado, %aiPlayer){
	clientPopAguardeMsgBox();
	if(!%aiPlayer){
		clientCmdPushAguardandoObjGui();
	}
	canvas.popDialog(baterGui);
	%eval = "%jogadorCorreto = $" @ %player @ ";";
	eval(%eval);
		
	%jogadorCorreto.corR = getWord(%color, 0);
	%jogadorCorreto.corG = getWord(%color, 1);
	%jogadorCorreto.corB = getWord(%color, 2);
	%jogadorCorreto.corA = getWord(%color, 3);	
	%jogadorCorreto.myColor = getWord(%color, 4);
	echo(%jogadorCorreto.id @ ".myColor =" SPC %jogadorCorreto.myColor);
	
	%botaoClicado.setVisible(false);
	
	if(%player $= "player1"){
		p1Hud.bitmap = "~/data/images/playerHud" @ %jogadorCorreto.myColor @ ".png";
		p1Hud.setVisible(true);
		inGameDiplomata1.bitmap = "~/data/images/playerHudDefesa.png";
		%jogadorCorreto.myDiplomataHud = inGameDiplomata1;
		%me = "p1";
	} else if(%player $= "player2"){
		p2Hud.bitmap = "~/data/images/playerHud" @ %jogadorCorreto.myColor @ ".png";
		p2Hud.setVisible(true);
		inGameDiplomata2.bitmap = "~/data/images/playerHudDefesa.png";
		%jogadorCorreto.myDiplomataHud = inGameDiplomata2;
		%me = "p2";
	} else if(%player $= "player3"){
		p3Hud.bitmap = "~/data/images/playerHud" @ %jogadorCorreto.myColor @ ".png";
		p3Hud.setVisible(true);
		inGameDiplomata3.bitmap = "~/data/images/playerHudDefesa.png";
		%jogadorCorreto.myDiplomataHud = inGameDiplomata3;
		%me = "p3";
	} else if(%player $= "player4"){
		p4Hud.bitmap = "~/data/images/playerHud" @ %jogadorCorreto.myColor @ ".png";
		p4Hud.setVisible(true);
		inGameDiplomata4.bitmap = "~/data/images/playerHudDefesa.png";
		%jogadorCorreto.myDiplomataHud = inGameDiplomata4;
		%me = "p4";
	} else if(%player $= "player5"){
		p5Hud.bitmap = "~/data/images/playerHud" @ %jogadorCorreto.myColor @ ".png";
		p5Hud.setVisible(true);
		inGameDiplomata5.bitmap = "~/data/images/playerHudDefesa.png";
		%jogadorCorreto.myDiplomataHud = inGameDiplomata5;
		%me = "p5";
	} else if(%player $= "player6"){
		p6Hud.bitmap = "~/data/images/playerHud" @ %jogadorCorreto.myColor @ ".png";
		p6Hud.setVisible(true);
		inGameDiplomata6.bitmap = "~/data/images/playerHudDefesa.png";
		%jogadorCorreto.myDiplomataHud = inGameDiplomata6;
		%me = "p6";
	}
	
	%eval = "%myDoarBtn =" SPC %me @ "DoarBtn;";
	eval(%eval);
	%eval = "%myPingBtn =" SPC %me @ "PingBtn;";
	eval(%eval);
	if(%jogadorCorreto == $mySelf){
		%myDoarBtn.setActive(false);
		%myPingBtn.setActive(false);
	} else {
		%myDoarBtn.setActive(true);
		%myPingBtn.setActive(true);
	}
	aguardandoTXT.text = $jogadorDaVez.nome @ " está jogando";
}

function clientCmdPopCoresGui(%p1Nome, %p2Nome, %p3Nome, %p4Nome, %p5Nome, %p6Nome, %p1Grad, %p2Grad, %p3Grad, %p4Grad, %p5Grad, %p6Grad){
	echo("PARTICIPANTES: " @ %p1Grad SPC %p1Nome @ "; " @ %p2Grad SPC %p2Nome @ "; " @ %p3Grad SPC %p3Nome @ "; " @ %p4Grad SPC %p4Nome @ "; " @ %p5Grad SPC %p5Nome @ "; " @ %p6Grad SPC %p6Nome);
	
	clientMuquiarObjMarkers(); //diminui o alpha dos marcadores de objetivos, para que o usuário possa enxergar o grupo inicial
	Canvas.popDialog(objetivosGuii);
	Canvas.popDialog(escolhaDeCores);
	Canvas.popDialog(aguardandoObjGui);
	fundoGruposGuiSetVisible(true);
	clientPopularGruposGui();
	clientCmdPushAguardandoObjGui();
		
	for(%i = 1; %i < 7; %i++){
		%eval = "p" @ %i @ "Nome_txt.text = %p" @ %i @ "Nome;";
		eval(%eval);
		%eval = "$player" @ %i @ ".nome = %p" @ %i @ "Nome;";
		eval(%eval);
		%eval = "$player" @ %i @ ".graduacaoNome = %p" @ %i @ "Grad;";
		eval(%eval);
	}
	
	/*
	//mostrar todas as cartas de grupos:
	for(%i = 0; %i < 4; %i++){
		%eval = "grupo" @ %i + 1 @ "_carta.setVisible(true);";
		eval(%eval);
	}
	*/
	
	clientMostrarHudObjs();
}

function clientCmdSetGULOKAiPlayer(){
	$playersNesteJogo++;
	%numDoAiPlayer = $playersNesteJogo;
	%eval = "$aiPlayer = $player" @ %numDoAiPlayer @ ";";
	eval(%eval);
	$aiPlayer.nome = "Gulok";
	$aiPlayer.gulok = true;
	
	%eval = "p" @ %numDoAiPlayer @ "Nome_txt.text = $aiPlayer.nome;";
	eval(%eval);
	$aiPlayer.imperiais = 999;
}

//
// Grupos:
//
function fundoGruposGuiSetVisible(%param){
	fundoObjDown.setVisible(%param);
	fundoObjUp.setVisible(%param);
	fundoObjLeft.setVisible(%param);
	fundoObjRight.setVisible(%param);
	
	gruposGuiObjSorteado1_img.setVisible(%param);
	gruposGuiObjSorteado2_img.setVisible(%param);
	gruposGuiTxt.setVisible(%param);
	
	grupo1_carta.setVisible(%param);
	grupo2_carta.setVisible(%param);
	grupo3_carta.setVisible(%param);
	grupo4_carta.setVisible(%param);
}

function clientPopularGruposGui(){
	clientFillSortObj(1, $mySelf.mySimObj.getObject(0), true);
	clientFillSortObj(2, $mySelf.mySimObj.getObject(1), true);
	gruposGuiTxt.setVisible(true);
	grupo1_carta.setVisible(true);
	grupo2_carta.setVisible(true);
	grupo3_carta.setVisible(true);
	grupo4_carta.setVisible(true);
	
	if($planetaAtual !$= "Terra"){
		clientCmdReloadGruposGui();
	}
}

function clientCmdReloadGruposGui(){
	grupo1_carta.setVisible(false);
	grupo2_carta.setVisible(false);
	grupo3_carta.setVisible(false);
	grupo4_carta.setVisible(false);
	
	grupo5_carta.setVisible(true);
	grupo6_carta.setVisible(true);
	grupo7_carta.setVisible(true);
	grupo8_carta.setVisible(true);
	grupo9_carta.setVisible(true);
}


function clientAskEscolherGrupo(%carta){
	if(!$estouNoTutorial){
		clientPushAguardeMsgBox();
		commandToServer('escolherGrupo', %carta);	
	} else {
		clientCmdApagarCartaGrupo(%carta);
		clientCmdMarkGrupo("Brasil", $mySelf.corR, $mySelf.corG, $mySelf.corB, $mySelf.corA);
		TUTmsg("brasilTerrestre", "right");
	}	
}

function clientCmdApagarCartaGrupo(%carta){
	clientPopAguardeMsgBox();
	%eval = "%nomeDaCarta = grupo" @ %carta @ "_carta;";
	eval(%eval);
	%nomeDaCarta.setVisible(false);	
}

function clientSetAlvoColor(%red, %green, %blue, %alpha){
	$alvo1.setBlendColor(%red, %green, %blue, %alpha);	
	$alvo2.setBlendColor(%red, %green, %blue, %alpha);	
	$alvo3.setBlendColor(%red, %green, %blue, %alpha);	
	$alvo4.setBlendColor(%red, %green, %blue, %alpha);	
	$alvo5.setBlendColor(%red, %green, %blue, %alpha);	
	$alvo6.setBlendColor(%red, %green, %blue, %alpha);	
	$alvo7.setBlendColor(%red, %green, %blue, %alpha);	
}
//
function clientCmdMarkGrupo(%nomeDoGrupo, %red, %green, %blue, %alpha){
	clientSetAlvoColor(%red, %green, %blue, %alpha);
	clientApagarCartasDeGrupos();
	$escolhendoGrupo = true;
	$grupoStartNome = %nomeDoGrupo;
	$grupoStartCount = 0;
	if(isObject($grupoSorteadoAreas)){
		$grupoSorteadoAreas.clear();
	} else {
		$grupoSorteadoAreas = new SimSet();
	}
	%grupo = clientFindGrupo(%nomeDoGrupo);	
	if(%nomeDoGrupo $= "Europa"){
		clientAskCriarBase(comunidadeEuropeia);
		$grupoStartCount += 1;
		$grupoSorteadoAreas.add(bMarMediterraneo);
		$grupoSorteadoAreas.add(bMarNordico);
		$grupoSorteadoAreas.add(bMarDaNoruega);	
	} else {
		for(%i = 0; %i < %grupo.simAreas.getcount(); %i++){
			%area = %grupo.simAreas.getObject(%i);
			%areaName = %area.getName();
			if(!%area.ilha && %areaName !$= "Estocolmo"){
				$grupoSorteadoAreas.add(%area);
			}
		}
	}
		
	for(%i = 0; %i < $grupoSorteadoAreas.getCount(); %i++){
		%area = $grupoSorteadoAreas.getObject(%i);
		%eval = "$alvo" @ %i+1 @ ".setPosition(%area.pos0);";
		eval(%eval);
	}
}

function clientResetAlvos(){
	$alvo1.setPosition("100 100");
	$alvo2.setPosition("100 100");
	$alvo3.setPosition("100 100");
	$alvo4.setPosition("100 100");
	$alvo5.setPosition("100 100");
	$alvo6.setPosition("100 100");
	$alvo7.setPosition("100 100");
}

function clientMarkMar(%nomeDoGrupo, %red, %green, %blue, %alpha){
	$grupoStartCount = 1;
	clientResetAlvos();
	%grupo = clientFindGrupo(%nomeDoGrupo);	
	
	clientSetAlvoColor(%red, %green, %blue, %alpha);
	if(isObject($grupoSorteadoAreas)){
		$grupoSorteadoAreas.clear();
	} else {
		$grupoSorteadoAreas = new SimSet();
	}
	
	for(%i = 0; %i < %grupo.simAreas.getcount(); %i++){
		%area = %grupo.simAreas.getObject(%i);
				
		%fronteirasCount = %area.myFronteiras.getCount(); //conta quantas fronteiras a Área de origem tem
		for (%j = 0; %j < %fronteirasCount; %j++){ //verifica as fronteiras de acordo com o índice
			%vizinha = %area.myFronteiras.getObject(%j);
			if(%vizinha.terreno $= "mar"){ //se  se for uma baía
				if($grupoSorteadoAreas.getCount() < 7){ //se ainda não tiver pego 7
					if(!%vizinha.pos0Flag){
						$grupoSorteadoAreas.add(%vizinha);
					}
				}
			}
		}
	}
	
	for(%j = 0; %j < $grupoSorteadoAreas.getCount(); %j++){
		%area = $grupoSorteadoAreas.getObject(%j);
		%eval = "$alvo" @ %j+1 @ ".setPosition(%area.pos0);";
		eval(%eval);
	}
}

function clientCmdServerGetFronteiraMaritima(%areaNome){
	%eval = "%area = " @ %areaNome @ ";";
	eval(%eval);
	%fronteirasCount = %area.myFronteiras.getCount(); //conta quantas fronteiras a Área de origem tem
	for (%j = 0; %j < %fronteirasCount; %j++){ //verifica as fronteiras de acordo com o índice
		%vizinha = %area.myFronteiras.getObject(%j);
		if(%vizinha.terreno $= "mar"){ //se  se for uma baía
			if(!%vizinha.pos0Flag){
				%j = %fronteirasCount;
			}
		}
	}
	
	clientAskCriarBase(%vizinha);
	$grupoStartCount = 2;
	clientAskPassarAVezEscolhendoGrupos();
	clientCmdPushAguardandoObjGui();
	$escolhendoGrupo = false;
}

function clientApagarCartasDeGrupos(){
	grupo1_carta.setVisible(false);
	grupo2_carta.setVisible(false);
	grupo3_carta.setVisible(false);
	grupo4_carta.setVisible(false);
	grupo5_carta.setVisible(false);
	grupo6_carta.setVisible(false);
	grupo7_carta.setVisible(false);
	grupo8_carta.setVisible(false);
	grupo9_carta.setVisible(false);
}


//
// Iniciar Partida:
//
function clientCmdIniciarPartida(%numDePlayers){
	$escolhendoGrupo = false;
	$tipoDeJogo = $salaEmQueEstouTipoDeJogo;
	$estaPartida.numDePlayers = %numDePlayers;
	$hiObjMarkersOn = false; //pára de piscar os objtivos
	setNormalZoom(); //coloca o mapa no tamanho certo
	apagarBtns(); //apaga o HUD atual //função no arquivo MainGui.cs
	Canvas.popDialog(aguardandoObjGui); //apaga o gui que impede ações do usuário
	fundoGruposGuiSetVisible(false); //apaga as imagens que compõe o gruposGui
	clientApagarCartasDeGrupos(); //apaga as cartas de grupos de planetas estranhos (5 a 9)
	clientZerarObjMarkers(); //apaga os marcadores de objetivos
	clientAtualizarEstatisticas();
	atualizarRecursosGui();
	clientPopularChatPlayerBtns(%numDePlayers);
	clientPopularFilantropiaGui(%numDePlayers);
	$primeiraRodada = true;
	$rodadaAtual = 1;
	rodadaAtual_txt.text = "1";
	clientSetIntel(); //prepara a IntelTab
			
	//(re)seta o chatVector:
	initJogoChatGui();
	clientChamarChat(); //chama o chat de jogo
	
	schedule(5000, 0, "clientMostrarNave"); //depois do zoom out, torna visível a nave dos AirDrops
		
	if($estouNoTutorial){
		tut_fecharJogo_btn.setVisible(true);	
		tut_berco.setVisible(true);
	}
	
	if($tipoDeJogo $= "poker"){
		$mySelf.pk_jogo.setPokerImperialBtn();
	} else {
		clientUnsetPokerImperialBtn();		
	}
	
	palcoTurnoTimer.iniciarTimer();
	echo("PARTIDA INICIADA!");
}

function clientMostrarNave(){
	nave_BP.setVisible(true);	
}

function clientPopularChatPlayerBtns(%numDePlayers){
	apagarChatBtns(); //começa apagando todos os btns;
	%simPlayers = new SimSet();
	for(%i = 1; %i < %numDePlayers + 1; %i++){
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		%simPlayers.add(%player);
	}
	
	%eval = "%me = $" @ $mySelf.id @ ";";
	eval(%eval);
	%simPlayers.remove(%me);
	
	%count = %simPlayers.getCount() + 1;
	for(%i = 1; %i < %count; %i++){
		%player = %simPlayers.getObject(0);
		%eval = "%myChatBtn = chat" @ %i @ "Btn;";
		eval(%eval);
		%myChatBtn.setVisible(true);
		%myChatBtn.setBitmap("game/data/images/chat" @ %player.myColor);	
		
		if(%i == 1){
			$chat1PlayerId = %player.id;	
		} else if (%i == 2){
			$chat2PlayerId = %player.id;	
		} else if (%i == 3){
			$chat3PlayerId = %player.id;	
		} else if (%i == 4){
			$chat4PlayerId = %player.id;	
		} else if (%i == 5){
			$chat5PlayerId = %player.id;	
		}
		
		%simPlayers.remove(%player);
	}
			
	ligarChatTodosBtn(); //seleciona o todosBtn;
}

function clientAskFinalizarTurno(){
	if($estouPerguntando){
		clientCancelarPergunta(); //cancela qualquer pergunta pendente
	}
	if(!$estouNoTutorial){
		finalizarTurno_btn.setVisible(false);
		commandToServer('finalizarTurno', palcoTurnoTimer.turnoTimeLeft);
		bater_btn.setVisible(false);
		resetSelection();
	} else {
		if($tut_campanha.passo.objetivo $= "finalizarJogada"){
			bater_btn.setVisible(false);
			finalizarTurno_btn.setVisible(false);
			clientCmdSetJogadorDaVez("player2", $rodadaAtual);
			clientCmdSetJogadorDaVezMovimentos(5);
			tut_verificarObjetivo(false, "finalizarJogada");
			%eval = "%myInfoNum = $tut_campanha.infoNum" @ $rodadaAtual @ ";";
			eval(%eval);
			clientCmdSetNewInfo(%myInfoNum);	
			
			schedule(5000, 0, "TUTaiJogar", $rodadaAtual);
		}
	}
	//renderTudo_btnIcon.setActive(false);
	clientClearCruzarFlechas();
	unitHud.setVisible(false); 
}

function clientCmdSetJogadorDaVezMovimentos(%quantos){
	clientClearUndo();
	finalizarTurno_btn.setVisible(false);
	
	if(!$vendoPoker)
		setNormalZoom();
		
	clientDesligarExplMarkers();
	verificarCanhaoOrbitalBtn(); //apaga o canhaoOrbitalBtn pq não é minha vez
	verificarOcultarBtn();
	desligarBonusCanibais();
	clientRemoverCortejadas();
	clientClearAllViruses();
	//devolve o jetPack pra todos os líderes do jogador da vez:
	for(%i = 0; %i < $jogadorDaVez.mySimLideres.getCount(); %i++){
		%lider = $jogadorDaVez.mySimLideres.getObject(%i);
		%lider.JPagora = %lider.JPBP;
	}
	
	//verifica as reciclagens do jogador da vez:
	clientVerificarReciclagens();
	//verifica os ovos do jogador da vez!:
	clientVerificarOvos();
	
	$jogadorDaVez.movimentos = %quantos;
	atualizarMovimentosGui(); //atualiza o gui;
}

function clientCmdAtualizarMovimentosDoPrimeiroPlayerVivo(%quantos)
{
	%primeiroPlayerVivo = clientGetPrimeiroPlayerVivo();
	%primeiroPlayerVivo.movimentos = %quantos;
	atualizarMovimentosGui();
}

function clientCmdInicializarMeuTurno(%movimentos, %imperiais, %minerios, %petroleos, %uranios){
	clientClearUndo();
	setNormalZoom();
	if($vendoPoker && !$forcePokerGui)
		clientPopPokerGui();
	
	clientDesligarExplMarkers();
	clientMsg("suaVez", 3000);
	verificarCanhaoOrbitalBtn(); //verifica se precisa ligar o canhaoOrbitalBtn
	verificarOcultarBtn();
	verificarCrisalidasVerdes();
	desligarBonusCanibais();
	clientRemoverCortejadas();
	clientClearAllViruses();
	$mySelf.alquimias = 0; //zera as alquimias feitas nesta rodada;
	
	//devolve o jetPack pra os meus líderes:
	for(%i = 0; %i < $jogadorDaVez.mySimLideres.getCount(); %i++){
		%lider = $jogadorDaVez.mySimLideres.getObject(%i);
		%lider.JPagora = %lider.JPBP;
	}
	
	clientVerificarReciclagens();
	clientVerificarOvos();
	
	//renderTudo_btnIcon.setActive(true);
		
	$mySelf.movimentos = %movimentos;
	$mySelf.imperiais = %imperiais;
	$mySelf.minerios = %minerios;
	$mySelf.petroleos = %petroleos;
	$mySelf.uranios = %uranios;	
	
	clientToggleRecursosBtns();
	
	finalizarTurno_btn.setVisible(true);
	atualizarMovimentosGui(); 
	atualizarRecursosGui();
	atualizarImperiaisGui();
	clientAtualizarEstatisticas();
	
	clientPlayMyTurnSound();
	
	if($estouNoTutorial){
		if($rodadaAtual > 1){
			if(isObject($tut_campanha.passo)){
				schedule(%myTime, 0, "tut_arrowOn", $tut_campanha.passo.alvo, $tut_campanha.passo.titulo, $tut_campanha.passo.linha1, $tut_campanha.passo.linha2, $tut_campanha.passo.linha3, $tut_campanha.passo.linha4, $tut_campanha.passo.linha1b, $tut_campanha.passo.linha2b, $tut_campanha.passo.linha3b, $tut_campanha.passo.icon1, $tut_campanha.passo.icon2);	
				if($rodadaAtual == 7){
					clientCmdLigarBaterBtn();	
				} 
				if($tut_campanha.passo.timedMsg){
					schedule($tut_campanha.passo.time, 0, "tut_completarObjetivo");	
				}	
			}
		}
	}
}

function clientPlayMyTurnSound()
{
	if($noSound)
		return;
		
	alxPlay(turnoStart);
}
	



//quando um player não escolhe cor ou grupo:
function clientCmdMorrerAntesDoInicio(){
	clientCmdFimDeJogo(0, 0);
	clientUnloadGame();
	Canvas.popDialog(objetivosGuii);
	Canvas.popDialog(escolhaDeCores);
	Canvas.popDialog(aguardandoObjGui);
	Canvas.popDialog(baterGui);
	clientAskSairDaSala();
	schedule(10000, 0, "clientMsgBoxOK", "rendicaoCompulsoria");
}

//////////////
//Mostrar os grupos no início de cada partida:
function clientMostrarGrupos(%planeta){
	%eval = "clientMostrarGruposDe" @ %planeta @ "();";
	eval(%eval);
}

function clientMostrarGruposDeTerra(){
	if($estouNoTutorial){
		%myMult = 1;
	} else {
		%myMult = 0.75;
	}
	
	schedule(1000 * %myMult, 0, "clientConquistarGrupoShow", "brasil");
	schedule(3000 * %myMult, 0, "clientConquistarGrupoShow", "eua");
	schedule(5000 * %myMult, 0, "clientConquistarGrupoShow", "canada");
	schedule(7000 * %myMult, 0, "clientConquistarGrupoShow", "europa");
	schedule(9000 * %myMult, 0, "clientConquistarGrupoShow", "russia");
	schedule(11000 * %myMult, 0, "clientConquistarGrupoShow", "china");
	schedule(13000 * %myMult, 0, "clientConquistarGrupoShow", "australia");
	schedule(15000 * %myMult, 0, "clientConquistarGrupoShow", "africa");
	schedule(17000 * %myMult, 0, "clientConquistarGrupoShow", "oriente");
	schedule(21000 * %myMult, 0, "clientAtualizarEstatisticas");
}

function clientMostrarGruposDeUngart(){
	schedule(1000, 0, "clientConquistarGrupoShow", "CaNo");
	schedule(3000, 0, "clientConquistarGrupoShow", "VaNo");
	schedule(5000, 0, "clientConquistarGrupoShow", "PlDo");
	schedule(7000, 0, "clientConquistarGrupoShow", "DeEx");
	schedule(9000, 0, "clientConquistarGrupoShow", "CaOr");
	schedule(11000, 0, "clientConquistarGrupoShow", "VaOr");
	schedule(13000, 0, "clientConquistarGrupoShow", "ChOr");
	schedule(15000, 0, "clientConquistarGrupoShow", "VaGu");
	schedule(17000, 0, "clientConquistarGrupoShow", "PaGu");
	schedule(19000, 0, "clientConquistarGrupoShow", "ChOc");
	schedule(21000, 0, "clientConquistarGrupoShow", "PrEx");
	schedule(23000, 0, "clientConquistarGrupoShow", "MoVe");
	schedule(25000, 0, "clientConquistarGrupoShow", "IlVu");
	schedule(29000, 0, "clientAtualizarEstatisticas");
}

function clientPopCarregandoGruposGui(){
	canvas.popDialog(carregandoGruposGui);	
}



///////////////////////
//quando o torque ainda não recebeu um novo taxoId

function clientCmdAguardeTAXOid()
{
	clientMsgBoxOKT3("OCORREU UM ERRO", "BANCO DE DADOS OFFLINE: POR FAVOR, REPORTE NO FÓRUM DO SITE WWW.PROJETOIMPERIO.COM.");	
	clientPopAguardeMsgBox();
}



//função para saber algumas variáveis dos players adversários:
function clientCmdSetGuloksVars(%playerId, %instinto, %horda, %exoEsqueleto){
	%eval = "%player = $" @ %playerId @ ";";
	eval(%eval);
	%player.instinto = %instinto;
	%player.horda = %horda;
	%player.exoEsqueleto = %exoEsqueleto;
}


//////////////////
//Links:
function clientGotoMeusRecados()
{
	gotoWebPage("www.projetoimperio.com/recados/" @ $pref::Player::name);	
	//gotoWebPage("dev.projetoimperio.com/recados/" @ $pref::Player::name);	
}

//esta é a função correta pro botão de ajuda. No momento estou colocando uma MsgBoxOk que avisa que está em desenvolvimento

function clientAjuda_links()
{
	ajuda_links_tab.setVisible(true);	
}
/*
function clientAjuda_links()
{
	clientMsgBoxOKT("EM DESENVOLVIMENTO", "ESTA OPÇÃO ESTÁ INDISPONÍVEL NO MOMENTO.");	
}
*/

function clientPopAjudaLinks()
{
	ajuda_links_tab.setVisible(false);
}

