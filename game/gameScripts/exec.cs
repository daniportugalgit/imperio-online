//configuração do server:
$IAmServer = serverConfig.IAmServer;

exec("./splash.cs"); //Splash Screens
exec("./area.cs"); //propriedades e simSets de cada área
exec("./info.cs");  //propriedades das Informações
exec("./objetivos.cs"); //propriedades dos objetivos
exec("./player.cs"); //propriedades e simSets dos jogadores
exec("./shot.cs"); //funcionamento dos tiros
exec("./recordTest.cs"); //teste de gravação de arquivos
exec("./pesquisasGuloks.cs"); //Pesquisas dos Guloks
exec("./pesquisas.cs"); //Pesquisas
exec("./firstStart.cs"); //Funções genéricas que precisam ser usadas na configuração client/server

if($IAmServer $= "1"){ //se eu for o server
	//os arquivos abaixo SÓ PODEM SER EXECUTADOS NO SERVER (eles não vão junto com o client pra máquina do usuário, se tentar executar não faz nada)
	exec("./server/serverClasses.cs"); //aki estão as unidades do server
	exec("./server/serverJogo.cs"); //Funções de criação de jogos
	exec("./server/serverPlanetas.cs"); //Registro dos planetas
	exec("./server/initServer.cs"); //para iniciar variáveis e registrar persona;
	exec("./server/serverSorteios.cs"); //sorteios de Objetivos, Grupos, Cores e Infos, {commands e functions}
	exec("./server/serverPlayerKill.cs"); //morte e suicídio
	exec("./server/serverMovimentacao.cs"); //Movimento, embarque, desembarque, remoção de unidade, reposição de unidade
	exec("./server/serverAtaque.cs"); //ServerCmdAtacar e serverExecutarAtaque
	exec("./server/serverGetPost.cs"); //comunicacao com banco de dados TAXO
	exec("./server/serverCreditos.cs"); //Calculo de quantos Créditos cada player ganha pela partida;
	exec("./server/serverCriarSala.cs"); //Funções de criação de salas
	exec("./server/serverTurnos.cs"); //Funções de criação de salas
	exec("./server/serverFimDeJogo.cs"); //Funções de criação de salas
	exec("./server/serverPropostas.cs"); //Propostas de trocas de cartas de pontos
	exec("./server/serverNegociacoes.cs"); //O resto das negociações
	exec("./server/serverProducao.cs"); //Produção de peças
	exec("./server/serverVenderRecursos.cs"); //Venda de Recursos
	exec("./server/serverVerificarPlayerStatus.cs"); //Verifica Grupos e Objetivos inGame
	exec("./server/serverChat.cs"); //o Chat!
	exec("./server/serverUndo.cs"); //Undo
	exec("./server/serverCriarPersona.cs"); //Criação de novas personas e deleção de antigas
	exec("./server/serverEmboscada.cs"); //Emboscadas
	exec("./server/serverGM.cs"); //Emboscadas
	exec("./server/serverAcademia.cs"); //Academia
	exec("./server/serverIntel.cs"); //Intel
	exec("./server/serverReciclagem.cs"); //Reciclagem
	exec("./server/serverAirDrop.cs"); //AirDrops
	exec("./server/serverFilantropia.cs"); //Filantropia
	exec("./server/serverSniper.cs"); //Sniper
	exec("./server/serverTerceiroLider.cs"); //TerceiroLider
	exec("./server/serverMoral.cs"); //Moral
	exec("./server/serverDesastre.cs"); //Desastres
	exec("./server/serverCanhaoOrbital.cs"); //Canhao Orbital
	exec("./server/serverOcultar.cs"); //Ocultar
	exec("./server/serverNexus.cs"); //Nexus Alquímico
	exec("./server/serverGeoCanhao.cs"); //GeoCanhao
	exec("./server/serverSubmergir.cs"); //Submergir
	exec("./server/serverCrisalida.cs"); //Crisalida
	exec("./server/serverMatriarca.cs"); //Matriarca
	exec("./server/serverHorda.cs"); //Horda
	exec("./server/serverCanibalizar.cs"); //Canibalizar
	exec("./server/serverMetamorfose.cs");
	exec("./server/serverDevorarRainhas.cs"); //Devorar Rainhas
	exec("./server/serverCortejar.cs");
	exec("./server/serverDragnal.cs");
	exec("./server/serverVirus.cs");
	exec("./server/serverExpulsar.cs");
	exec("./server/serverPilhar.cs");
	exec("./server/serverGulokAppear.cs");
	exec("./server/serverGmBot.cs");
	exec("./server/serverSala.cs");
	exec("./server/serverPersona.cs");
	exec("./server/serverPatentes.cs");
	exec("./server/serverPoker.cs");
	exec("./server/serverPk_jogo.cs");
	exec("./server/serverFilas.cs");
	exec("./server/serverPersonasOnline.cs");
	exec("./server/serverSincronia.cs");
} else { //se eu for um client
	//os arquivos abaixo SÓ PODEM SER EXECUTADOS NO CLIENT
	exec("./client/mouse.cs"); //detecta clicks e objetos clicados, enviando pedidos pro server
	exec("./client/mainGui.cs"); //apaga e mostra gui e botões
	exec("./client/clientClasses.cs"); //aki estão as unidades e os explMarkes
	exec("./client/clientMsg.cs"); //todas as mensagens normais para os clients, {commands e functions}
	exec("./client/clientMovimentacao.cs"); //Movimento, embarque, desembarque, remoção de unidade, reposição de unidade
	exec("./client/clientAtaque.cs"); //cliantAskAtacar e clientCmdFire
	exec("./client/clientTurnoTimer.cs"); //o timer de cada turno
	exec("./client/clientStartSequence.cs"); //as telas iniciais antes de entrar em um jogo
	exec("./client/clientSala.cs"); //funções de criação de salas
	exec("./client/clientCriarSala.cs"); //funções de criação de salas
	exec("./client/clientFimDeJogo.cs"); //finalização de jogos
	exec("./client/clientOptionsMenu.cs"); //menu de opções (por enquanto só tem o vídeo e não é um menu, :S)
	exec("./client/clientCruzarOceanos.cs"); //mostrar flechas e cruzar oceanos;
	exec("./client/clientNegociacoes.cs"); //mostrar flechas e cruzar oceanos;
	exec("./client/clientVenderRecursos.cs"); //venda de recursos e GUIs correspondentes
	exec("./client/clientProducao.cs"); //para produzir peças
	exec("./client/clientRender.cs"); //render todos
	exec("./client/clientAtualizarEstatisticas.cs"); //atualizarEstatisticas
	exec("./client/clientReceberInfo.cs"); //atualizarEstatisticas
	exec("./client/clientPropostas.cs"); //trocas de cartas de pontos
	exec("./client/clientCamera.cs"); //a Câmera, naturalmente...
	exec("./client/clientChat.cs"); //o Chat!
	exec("./client/clientUndo.cs"); //Undo
	exec("./client/clientKeyBind.cs"); //KeyBind
	exec("./client/clientEmboscada.cs"); //Emboscadas
	exec("./client/clientGM.cs"); //GM
	exec("./client/clientAcademia.cs"); //Academia Imperial
	exec("./client/clientInvestirRecursos.cs"); //Investir Recursos InGAME
	exec("./client/clientIntel.cs"); //Intel InGame
	exec("./client/clientReciclagem.cs"); //Reciclagem
	exec("./client/clientAirDrop.cs"); //AirDrops
	exec("./client/clientFilantropia.cs"); //Filantropia
	exec("./client/clientAlmirante.cs"); //Almirante
	exec("./client/clientSniper.cs"); //Sniper
	exec("./client/clientTerceiroLider.cs"); //TerceiroLider
	exec("./client/clientMoral.cs"); //Moral
	exec("./client/clientDesastre.cs"); //Desastres
	exec("./client/clientCanhaoOrbital.cs"); //Canhao Orbital
	exec("./client/clientMsgBoxOKPadrao.cs"); //MsgBoxOK
	exec("./client/clientNetCom.cs"); //para pegar a foto do cara do taxo
	exec("./client/clientTutorial.cs"); //tutorial
	exec("./client/AI.cs"); //Inteligência Artificial
	exec("./client/clientPlanetas.cs"); //planetas no client 
	exec("./client/clientOcultar.cs"); //Ocultar
	exec("./client/clientPoker.cs"); //Primeiro teste do Poker Imperial
	exec("./client/clientNexus.cs"); //Nexus Alquímico
	exec("./client/clientGeoCanhao.cs"); //Geo-Canhão
	exec("./client/clientInstinto.cs"); //Instinto Materno
	exec("./client/clientIncorporar.cs"); //Incorporar
	exec("./client/clientSubmergir.cs"); //Submergir
	exec("./client/clientCrisalida.cs"); //Crisalida
	exec("./client/clientMatriarca.cs"); //Matriarca
	exec("./client/clientHorda.cs"); //Horda
	exec("./client/clientCarregar.cs"); //Carregar
	exec("./client/clientCanibalizar.cs"); //Canibalizar
	exec("./client/clientDevorarRainhas.cs"); //DevorarRainhas
	exec("./client/clientMetamorfose.cs"); 
	exec("./client/clientCortejar.cs"); 
	exec("./client/clientDragnal.cs"); 
	exec("./client/clientVirus.cs"); 
	exec("./client/clientExpulsar.cs"); 
	exec("./client/clientPilhar.cs"); 
	exec("./client/clientFXtxt.cs"); //Efeitos de texto pras habilidades das unidades
	exec("./client/clientDev.cs");  //falicita os testes de dev
	exec("./client/clientPersonasOnline.cs"); //busca a lista de Personas Online
	exec("./client/clientSincronia.cs");
	exec("./client/clientGalaxias.cs"); 
	exec("./client/clientDinamicTxt.cs"); 
	exec("./client/clientAjuda.cs"); 
	exec("./client/clientNoticias.cs"); 
}
exec("./start.cs"); //inicializa variáveis de client; função de orientação de tiros e peças;

function getServerIp(){
	%file = new FileObject();
	
	if(isFile("game/data/files/serverIp.txt")){
		%file.openForRead("game/data/files/serverIp.txt");	
		%ip = %file.readLine();
		%file.close();	
		$serverIp = %ip;
	} else {
		echo("ERRO ao pegar ip do servidor Império - marcando Default");
		$serverIp = "imperiogame.no-ip.biz";
	}
	%file.delete(); //apaga da memória RAM
}
getServerIp();

function connectToImperioServer(){ //função para usar num botão na tela splash inicial
	conectandoGui.setVisible(true); //antes de tudo, previne um segundo click no botão caso o client esteja tentando conectar

    $pref::Player::Name = playerName_itxt.getText();
	$pref::Player::Senha = playerSenha_itxt.getText();
    connectToServer($serverIp); //o que depois vai virar o login
}

function marcarVersao(){
	$versao = "0.763";
	versao_txt.text = "Versão de testes Alpha - v" @ $versao;
}
marcarVersao();
///////////////////////////

/////////////
//para debugar com o nix:
function setNix(){
	devOn();
	setRes(1024,768);
}

//////////////////
//para debugar com o fenix para o nix:
function setLan(){
	lan_btn.setVisible(true);	
}


//////////////
//função para executar mais facilmente um script:
function execC(%script){
	%evalStr = "game/gamescripts/client/" @ %script @ ".cs";
	exec(%evalStr);	
}
function execS(%script){
	%evalStr = "game/gamescripts/server/" @ %script @ ".cs";
	exec(%evalStr);	
}
function execR(%script){
	%evalStr = "game/gamescripts/" @ %script @ ".cs";
	exec(%evalStr);	
}