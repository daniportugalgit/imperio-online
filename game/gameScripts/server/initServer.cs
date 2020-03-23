// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\initServer.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  domingo, 5 de agosto de 2007 19:09
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
//$debugMaster=true; //estou debugando
function devOn(){
	$enderecoTaxo
	= "dev.projetoimperio.com";	
}

function devOff(){
	$enderecoTaxo= "www.projetoimperio.com";	
}

function serverCmdDebugMaster(){
	$debugMaster=true; //estou debugando
}

function serverCmdNOTdebugMaster(){
	$debugMaster=false; //estou debugando
}


function getEnderecoTaxo(){
	echo("**getEnderecoTaxo()");

	%file = new FileObject();
	
	/*
	if(isFile("game/gameScripts/server/ini/enderecoTaxo.ini")){
		%file.openForRead("imperio/gameScripts/server/ini/enderecoTaxo.ini");	
		$enderecoTaxo = %file.readLine();
		%file.close();	
	} else {
		echo("ERRO ao pegar endereçoTaxo - marcando Default");
		$enderecoTaxo = "www.taxo.com.br";
	}
	%file.delete(); //apaga da memória RAM
	*/
	$enderecoTaxo = "www.projetoimperio.com";
}

function justATest() {
	echo("this is just a test!");
}

function onServerCreated(){
	echo("******* On Server Created()");
	$connectionNum = 0;
	if($IAmServer){
		%arquivo = "game/data/levels/terra.t2d";
		sceneWindow2D.loadLevel(%arquivo);
		$strategyScene = sceneWindow2D.getSceneGraph();	
		getEnderecoTaxo();
		toggleConsole();
	}
}
///////////
function GameConnection::onConnect(%client, %name, %senha, %versao){
	echo("***GameConnection::onConnect()");

   // Send down the connection error info. The client is responsible for displaying this
   // message if a connection error occures.
   commandToClient(%client, 'SetConnectionError', $Pref::Server::ConnectionError);
   
   %client.setPlayerName(%name);
   %client.senha = %senha;
   %client.versao = %versao;
   echo("**Client Connected: " @ %client @ " " @ %client.getAddress());
	$Server::PlayerCount++;
    onClientConnected(%client);
}
///////////////
//onConnected refactory2:
function onClientConnected(%client){
	if($connectionNum == 0){
		echo("CONNECTION ZERO ESTABILSHED: the server is connected!");
		$serverConnection = %client;
		$connectionNum = 1;
		Canvas.popDialog(clientStartGui); //apaga o hud de Login;
		Canvas.popDialog(objetivosGuii); //apaga o hud de escolha de objetivos;
		Canvas.popDialog(networkMenu); //apaga o networkMenu;
		$serverSimPersonas = new SimSet();
		$serverSimUSERS = new SimSet();
		$serverSimSalas = new SimSet();
		$personasNoAtrio = new SimSet();
		//////////////////////
		//criar planetas:
		serverCarregarUngart();//Primeiro carrega as áreas de Ungart;
		serverCarregarTeluria();//Primeiro carrega as áreas de Telúria;
		criarPlaneta("Terra", 1, 78, 26, 1, 4); //%nome, %desastres, %infos, %objs, %id, %lotacao;
		criarPlaneta("Ungart", 14, 80, 38, 2, 5); //%nome, %desastres, %infos, %objs, %id;
		criarPlaneta("Teluria", 10, 73, 45, 3, 4); //%nome, %desastres, %infos, %objs, %id;
		schedule(5000, 0, "stopHeartBeat"); //pára de ficar enviando msgns pro master server da garage games
	} else {
		echo("New player connected!");
		registrarUsuario(%client, %client.name, %client.senha, %client.versao);
		$connectionNum++;	
	} 
}

function registrarUsuario(%client, %login, %senha, %versao){
	//primeiro, verifica se o usuário já existe no server;
	%eval = "%user = $USER" @ %login @ ";";
	eval(%eval);
	if(isObject(%user)){
		echo("Deletando usuárioGhost: " @ %login);
		if(%user.offLine == false){
			serverMatarPersona(%user.persona);
			%user.delete();
		}
	}

	echo("Registrando login para username " @ %login @ " e senha " @ %senha @ " e versão " @ %versao);
	
	%eval = "$USER" @ %login @ " = new ScriptObject(){";
	%eval = %eval @ "login = %login;";
	%eval = %eval @ "client = %client;";
	%eval = %eval @ "senha = %senha;";
	%eval = %eval @ "versao = %versao;";
	%eval = %eval @ "};";
	eval(%eval);
	
	%eval = "%esteUser = $USER" @ %login @ ";";
	eval(%eval);
		
	%client.user = %esteUser; //guarda a persona no client;
	%esteUser.client = %client;
		
	$serverSimUSERS.add(%esteUser); //adiciona ao simSet universal de usuários
	%esteUser.personasCount = -2; //prepara para a verificação futura
	%esteUser.offLine = false;
	%esteUser.falhasNaPesquisa = 0; //contador para tirar o server do loop caso a pesquisa esteja falhando demais
	if(isObject(%esteUser.myPersonas)){
		%esteUser.myPersonas.clear(); //limpa o simSet de personas deste usuário
	} else {
		%esteUser.myPersonas = new SimSet(); //cria o simSet de personas deste usuário
	}
	
	$filas_handler.newFilaObj("login", "/torque/login/" @ %login @ "/" @ %senha, 0, %esteUser);
}

//Esta função é chamada autoamticamente quando acaba de receber a resposta do taxo refente ao login solicitado
function verificarTaxoLogin(%user){
	echo("verificarTaxoLogin()");
	if(%user.personasCount == -1)
	{
		echo("Usuário não encontrado no banco de dados!! Desconectando client...feito!");
		commandToClient(%user.client, 'msgBoxLoginOuSenhaRejected', %user.client);
		//serverDesconectarUsuario(%user.client);
		return;
	}
	if(%user.personasCount == -2)
	{
		//Não conseguiu se conectar com o taxo!
		echo("Não obtive resposta do TAXO para o login " @ %user.nome @ ". Desconectando usuário...feito!");
		serverRespoderErroGenerico(%user.client);
	}
	
	for(%i = 0; %i < 5; %i++)
		%dados[%i] = "-1";
					
	for(%i = 0; %i < %user.personasCount; %i++)
	{
		%thisPersona = %user.myPersonas.getObject(%i);
		%dados[%i] = %thisPersona.nome SPC %thisPersona.TAXOvitorias SPC %thisPersona.TAXOpontos SPC %thisPersona.TAXOvisionario SPC %thisPersona.TAXOarrebatador SPC %thisPersona.myComerciante SPC %thisPersona.myDiplomata SPC %thisPersona.TAXOcreditos SPC %thisPersona.patente.nome SPC %user.TAXOomnis SPC %thisPersona.especie SPC %thisPersona.pk_vitorias SPC %thisPersona.pk_fichas SPC %thisPersona.pk_power_plays;			
		%thisPersona.dados = %dados[%i];
	}
	
	commandToClient(%user.client, 'popularComandantes', %user.personasCount,  %dados[0],  %dados[1],  %dados[2],  %dados[3],  %dados[4], %user.TAXOconhece_g);//apaga o clientStartGui e o NetworkMenu, e chama o escolherComandanteGui; esta função está no clientStartSequence
}


function serverCmdSelecionarPersona(%client, %num){
	%pNum = %num - 1;
	%client.persona = %client.user.myPersonas.getObject(%pNum); //marca no client a persona ativa no momento
	if(%client.persona.jogo.partidaIniciada && !%client.persona.jogo.partidaEncerrada){
		commandToClient(%client, 'ultimoJogoEmAndamento');
	} else {
		%client.persona.sala = "no"; //marca que não está em uma sala;
		%client.persona.jogo = "no"; //marca que a persona não está em qualquer jogo
		%client.persona.offLine = false;
		%client.user.persona = %client.persona; //marca no user a persona ativa no momento
		%client.persona.client = %client; //marca o client na persona ativa
		%client.persona.user = %client.user; //marca o user na persona ativa
		%client.persona.atrioPagina = 1; //seta a página 1 do átrio pra pesona
		if(%client.persona.especie $= "gulok"){
			%client.persona.gulok = true;	
		}
		$serverSimPersonas.add(%client.persona); //adiciona ao simSet universal de personas
		serverMarcarPersonasOfflineExcept(%client.persona);
		
		//pesquisarAcademiaPorPersona(%client.persona.TaxoId);
		$filas_handler.newFilaObj("buscar_academia", "/torque/academia/buscar?idPersona=" @ %client.persona.TaxoId, 2, %client.user);
	}
}

function serverMarcarPersonasOfflineExcept(%persona)
{
	%user = %persona.user;
	
	for(%i = 0; %i < %user.myPersonas.getCount(); %i++)
	{
		%tempPersona = %user.myPersonas.getObject(%i);
		if(%tempPersona != %persona)
		{
			%tempPersona.offLine = true;	
		}
	}
}




/////////////////////////////////////////////////////////
//função de dado:
function dado(%faces, %bonus){
	%resultado = getRandom(1, %faces) + %bonus;
	return %resultado;
}

////////////////////////////////////////////////////////






///////////////////////////////
////////////////////////////
//função para testes:
function serverCheat(%jogo, %playerId){
	%eval = "%estePlayer = " @ %jogo @ "." @ %playerId @ ";";
	eval(%eval);
	%estePlayer.imperiais = 500;
	%estePlayer.minerios += 20;
	%estePlayer.petroleos += 20;
	%estePlayer.uranios += 20;
	echo("CHEAT MODE ON");
}



//VERSÕES:
//0.189: serverComDot para movimentos, embarques, desembarques, compras e trocas 
//0.190: JogoInválido e enviarTaxoResult Atomático
//0.191: Átrio atualizável
//0.192: Resolução inicial, minorBugs resolvidos, graduação verify
//0.193: Cruzar oceanos, minorBugs resolvidos
//0.194: Novas Peças, Novo Hud com bug dos botões autômatos
//0.195: Bug dos botões corrigido, Novas cores (laranja e Indigo)
//0.196: Novo BaterGui, salaInsideGui, loggedInGui e AtrioGui (agora recebe os créditos ao final da partida!)
//0.197: Novos missionMarkers de 3 frames
//0.198: Novo objetivos no HUD, nova organização de arquivos (sem clientFunctions e clientCommands)
//0.199: Com trocas de cartas de pontos semi-funcionais
//0.200: Gauges de stats, trocas funcionais
//0.201: Nova escolha de objetivos!
//0.202: GradIcons; Areas mistas recebem e fazem pedidos de visita; [renderTodos] faz player perder créditos; flechas indicam propostas com acordos possíveis; arrebatador = completar 2 objs;
//0.203: Segunda página de acordosExpl; risco vermelho em expl quando perde área; toggleMissionMarkers_btn; Chat primitivo, mas funcionando. Agora tem que transformar em parte do MainGui... 
//0.204: Chat melhorado, mas sem msgs privativas; SEM EDITOR agora funciona! Yayy!!
//0.205: Chat com msg privativa;
//0.206: Com usuários que têm personas - precisa testar no TaxoServer, por enquanto estou simulando a recepção de dados;
//0.207: DesembarqueHud pra desembarcar soldado ou general;
//0.208: Undo com Ctrl+z
//0.209: Agora o JOGO é um objeto de classe! (tem funções próprias)
//0.210: Pega o serverIp do arquivo game/data/files/serverip.txt e o endereçoTaxo do arquivo Imperio/gameScripts/server/ini/enderecoTaxo.ini
//0.211: Grupos são objetos com simAreas!!
//0.212: Adversários conseguem ver a peça selecionada pelo jogador da vez
//0.213: Com Planetas
//0.214: Desembarque amigável e USERS com criação de personas
//0.215: Tamanho de texto condicional ao tamanho da janela; muitos bugs corrigidos;
//0.216: Bugs corrigidos: 4ª persona fantasma na volta à sala após um jogo;
//0.217: Trocas de cartas e acordos enviados valem 2 pontos de neg; msg privativa pisca no chatBtn; Oceanos livres; Ataques de areas mistas; ctrl+z == lshift+LEFT; Tempo pisca quando estah acabando; CameraShake;
//0.218: Criação e deleção de Personas; morteMark no AtkGui; acordosPossiveisPing; propostas inteligentes; chatMark;
//0.219: Build Fechada; Emboscadas; Peças diferentes para resoluções pequenas; imperioMark;
//0.220: Ataques, defesas, embarques, número de líderes e seus respectivos escudos já pegam o valor da Academia Imperial; clientAcademia com navegação primária
//0.221: Pesquisar, Comprar e investir funcionando. Investimento inGame funcionando; Pegando e gravando dados de academia direto no TAXO! 
//0.222: Intel; Espionagem1; Força Diplomática; JetPack; Prospecção; Reciclagem; Refinaria;
//0.223: UnitHud; Jogo-Treino; Air Drop; Filantropia; Almirante; Sniper;
//0.224: Academia expandida com novos ícones e níveis de pesquisa; Terceiro Líder
//0.225: Música tema na Academia; Botão a_voltar funcionando; a_PEA_tab;
//0.226: Bugs corrigidos em pesquisas, semáforo implementado pra enviar dados;
//0.227: Desastres;
//0.228: SalaChat;
//0.229: 1024x768 como Default para evitar imagens turvas em 1024x768;
//0.230: Sistema reconhece morte por desastre em qualquer caso;
//0.231: Canhão Orbital;
//0.232: Bugs em propostas corrigidos; delay de 3 segundos na batida compulsória
//0.233: Com mostrador de diplomacia inGame
//0.234: Btns de compra ficam ativos ou inativos conforme a grana do client; Emboscada agora apaga o btn por 1 seg pra verificar se já pode emboscar de novo (corrigiu o bug);
//0.235: Bug de ataque de áreas mistas corrigido;
//0.236: Líderes substituem soldados iniciais; bônus de economia; Diplomata diminuiu pra 60% e tem navio no último nível; clientTimeOut corrigido; atualizarMovimentos corrigido;
//0.237: Propostas e Acordos Possíveis têm timer pra fechar; SalaChatGui iniciado assim que um jogo acaba; max de 5 personas por usuário;
//0.238: Início da lógica do jogo de duplas implementado: areas compartilhadas. 
//0.239: Com GM-Observador e jogo em duplas mais firmeza;
//0.240: Duplas funcionando (falta mostrar pro usuário); bug da prisão nos acordosExpl corrigido; novo escolhaDeCommandantesGui; Nova MsgBox padrão;
//0.241: Bug dos botões embaixo do finalizar jogada corrigido; REcomendação pra reiniciar caso mude a resolução; MsgBox de doação filantropica; textos corretos na PEA inGame;
//0.242: Jogo em Duplas teoricamente funcionando (sorteio automático);
//0.243: Embarque na pos3; Bug de Atk e Def de Lider corrigido; em duplas vc doa cartas de pts e de recursos; zera o venderGui de um jogo para outro;
//0.244: Desastres e Emboscadas configurados pro observador; Bugs no PlayerGui em duplas corrigidos; Bug em prospecção e ForçaD corrigidos; bug da captura de bases aliadas corrigido;
//0.245: Ver o status das suas peças mesmo quando não é sua vez (ctrl+A); em Duplas precisa completar apenas 1 objetivo; Bases Realmente compartilhadas em duplas; 
//0.246: Desembaracar da Pos3; Client naum gasta grana do aliado quando faz unidade em base aliada;
//0.247: Novo DoarGui, novo DoacaoEfetuadaGui e novo DoacaoRecebidaGui; emboscada corrigida (pegando atk e def da academia e não da peça);
//0.248: ExplGui que vc clica e mostra a missão no tabuleiro; Caveira no playerGui pra quem morre; proibido doar pra morto; voltarPraSalaBtn é um botão central;
//0.249: clientMsg "Sua Vez de Jogar"; SorteioDeOrdemDosJogadores_Show;
//0.250: Espionagem, Filantropia e Almirante agora têm 3 níveis; Contra-Espionagem implementada no jogo;
//0.251: Nova proporção da barra de Pontos; novos pré-requisitos para ataques e defesas; Render da pos 3 ou 4; Bug do btn invisível sobre investir recursos Corrigido;
//0.252: Confirmação de ataque se você ainda é diplomata;
//0.253: Tempo pisca quando chega em 70 segundos; Academia atualiza automaticamente quando uma pesquisa é finalizada; cada player já começa o jgo vendo seu bônus de imperiais;
//0.254: Tempo da pergunta de passagem aumentado; Verificação dos Objetivos completos (bug do falso arrebatador corrigido); Correção na imagem e no posicionamento do terceiro Líder;
//0.255: Undo para JetPack; Undo pode ser feito indefinidamente; Undo para jetPacks; Porcentagem de completude de Objetivos e Império; Batida vale 1 crédito;
//0.256: Texto indicando que a batida agora vale 1 crédito; Porcentagens finais corrigidas; Pesquisas custam um pouco mais de urânios; Em 3 jogadores basta ter 8 Comerciante;
//0.257: Tempo da pergunta da visita alterado para 5 segundos; Jogadores visualizam bonus inicial de imperiais (sem bugs); AirDrop proibido no mar; Escolha de grupos em duplas sem bugs;
//0.258: Ataque de áreas mistas  e ataque da posição 3 corrigidos; Sem msg de moratória quando faz um acordo que já estava comprometido; novo moratóriaFX torna mais claro o local da missão;
//0.259: Custo das pesquisas está mais equilibrado; novos bônus de patentes (menores); CTRL+Z para embarques e desembarques;
//0.260: Texto indica quando não existe uma pesquisa em andamento; ícones para espionagem3, filantropia3 e almirante3; Client 2Mb mais leve (novo clientMsg); 
//0.261: Espionagem1 vê status de pesquisas; Espionagem2 vê venda de recursos;
//0.262: Bug do embarque na pos3 com vários navios corrigido; btn de desembarque de "General" para "Líder"; Barras animadas na entrada do jogo e nas salas;
//0.263: Corrigio o bug que deixava os botões de acordos possíveis ligados para sempre; corrigido o bug da proposta negada (o marcador da missão desaparecia);
//0.264: Refinarias somente em terra; Barras animadas de status de persona; texto "Nível Atual" em Pesquisas;
//0.265: [SALA] Escolha do tempo de cada turno e da lotação;
//0.266: Botão de iniciar jogo atualizavel; Objetivos verificados após reciclagem; [DUPLAS] 4 jogadores = treino, 6 jogadores = jogo, senão botão trava; preços das pesquisasre-equilibrados;
//0.267: Botão para alterar o jogo para duplas;
//0.268: Canhão Orbital tem como pré-requisito Arrebatador (20); Bug da pergunta que desaparecia corrigido;btn de refinaria fica inativo quando vc não pode construí-la;
//0.269: Após rendimento de peça o sistema verifica todos os objetivos; Após morte de participante ele perde as missões que possuía; Observador consegue ver quem entra e sai da sala;
//0.270: Bug de não construir a base terrestre inicial corrigido; bug de dar unDo nos movs dos outros corrigido; quando observador sai da sala ela reconhece que não possui mais um observador;
//0.271: Perder diplomacia por sniper ou ataqeuOrbital; texto "lotada" para as salas com lotação esgotada, no átrio; sala liberada após fimDeJogo (ocupada/desocupada);
//0.272: Botão de comprar tanques fica inativo qndo vc não tem grana;
//0.273: Canhão Orbital2 Dispara na quarta rodada; Planetas são descobertos em cada perfil; Quando alguém sai da sala as barras não são refeitas; btn de escolher tipo na sala é toggle btn;
//0.274: MsgBoxOk dizendo que vc ainda não conehce outro planeta; Três ou mais barcos embarcados na mesma área; Tempo do "Sincronizando" diminuído para 2 segundos;
//0.275: Corrigido o bug que mostrava os explMarkers pra sempre; Seleção de tanques igual à de navios; MsgBoxOk AirDrop só na terra; Doação de cartas de pontos (usando Shift); Max 15 Rodadas;
//0.276: Pesquisas de soldados mais baratas; Reciclagem de várias bases simultanemante; mandar reciclrar uma base que já estava reciclando dava pau [corrigido];
//0.277: Rodadas sendo contadas após morte do player 1; Tooltip de reciclagem; Reciclagem3 pode recilar base que já está reciclando; sorteio de ordem sem peek-a-boo; Jogo não canga se demorar;
//0.300: Com atualizações automáticas!!!! XD
//0.301: Teste de atualização (só sobe uma versão pra ver se est[a atualizando);
//0.302: ComercianteMark;
//0.303: Correção no bug da comercianteMark (ficava por cima de tudo); 
//0.304: Bloquear pedido de passagem quando a pessoa não tem movimentos; Quando acaba a pesquisa o botao de investir fica inativo; Dificuldade da sala com ícones e toolTips
//0.305: Nova nave de AirDrop; novo finalizarPesquisa; finalizarTurno_btn fica invisível se vc se rende;
//0.306: Nova msgBox de venda de recursos
//0.307: Full
//0.308: Corrigido o bug das barras na salaInside; Tiros cruzam o mapa corretamente; Ajustem em pesquisas caras; Max 39 omnis por pesquisa; corrigido bug nas flechas de cruzar oceanos;
//0.309: AirDrop3 precisa Visionário 40; Sniper dá 2 tiros por nível; líder anfíbio pede passagem pra terrenos diferentes; Canhão Orbital refeito;
//0.310: MsgBoxOk na Academia; 60 seg pra escolher objetivos/cor/grupo; novo tiro e novo efeito de sniper; ícones de líderes são amarelos e vermelhos; batida compulsária não vale créditos
//0.311: Dificuldade da sala também é mostrada no interior dela, não apenas no átrio;
//0.312: Não se pode fazer doação filantropica pra própria dupla; Não se pode doar pra morto; não se pode bater duas vezes (compulsoria); versão é um número; modo de jogo Sem Pesquisas;
//0.313: Mínimo de uma persona por usuário; Sala semPesquisas sempre é Competitiva; Btn Diplomata só abre após 3 vitórias; Movimento in´valido não queima ctrl+z; no sroteio se vê quem escolhe;
//0.314: Peças se orientam conforme o tiro que dão; minorBugs em morte de player corrigidos; "Perfil" virou "Persona" mesmo; btn de voltar pra escolher outra persona; Início de Gulok;
//0.315: Se a luly cai não perde créditos; bug das barras na volta pra commSelect corrido;
//0.316: Texto de Intel refeito; DoarRendaGui é restado de um jogo a outro; filantropia anonima com shift; Jogo em duplas tem máximo de 10 rodadas;
//0.317: Sons de explosões
//0.318: Novo som de TurnoStar e sons de movimentação de tanques
//0.319: Som da produção de navios; b_imperios trocado por b_visionario no serverGetPost; Não pede passagem pro oceano; Quando volta pra tela de escolha de comandantes, atualiza a persona;
//ServerComDot quando faz doação filantrópica; Impedir trocas depois que a partida acabou; Quando altera características do jogo o observador também é atualizado;
//Acentuação nos medidores de dificuldades das salas, tanto no atrio, quanto na salainsideGi; Tooltip do btn de diplomata na cademia mudou para "3 Vitórias e Diplomata 60%+";
//Texto de Intel levemente modificado; ToolTips nos ícones da Academia;Ícones de Filantropia refeitos (cor); Som de Ping;

//0.320: novo tooltip de venda de recursos;Gráficos de Trocas de Cartas resetados de um jogo para o outro;Ícones de acordos de exploração são apagados de um jogo pro outro;
//Cancelar propostas de quem morre;Quando vende recursos e não tem, recebe msg de resposta e atualiza os btns;Espionagem também é retirada em jogos sem pesquisas;
//Mensagem de Login ou senha incorretos;Espionagem 3 pode espionar todos mas só pode ser espionada por espionagem 3;Observador recebe o chat da sala; Observador agora pode mandar chat no jogo;
//Limpar todos os símbolos do tabuleiro e dos acordos de exploração gui antes de iniciar uma partida;Corrigido o bug que não reconhecia as pesquisas especiais de líderes até que re-logasse.
