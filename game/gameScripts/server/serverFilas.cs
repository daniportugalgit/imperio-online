// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverFilas.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 7 de abril de 2009 2:33
//
// Editor             :  Codeweaver v. 1.2.3341.40715
//
// Description        : Filas de envio pro taxo 
//                    :  
//                    :  
// ============================================================


function initFilaSys()
{
	$filaSysNum++;

	$filas_handler = newSimObj("filas_handler", $filaSysNum);
	$filas_handler.initSimFilas();
}

function filas_handler::initSimFilas(%this)
{
	if(!isObject(%this.simFilas))
	{
		%this.simFilas = new SimSet();
		%this.criarTodasAsFilas();
	}	
}

function filas_handler::criarTodasAsFilas(%this)
{
	%this.prioridade_0 = new SimSet();
	%this.prioridade_1 = new SimSet();
	%this.prioridade_2 = new SimSet();
	%this.prioridade_3 = new SimSet();
	%this.prioridade_4 = new SimSet();
	%this.prioridade_5 = new SimSet();
	%this.enviados = new SimSet();
}

function filas_handler::executarProxima(%this)
{
	if(%this.prioridade_0.getCount() > 0)
	{
		%this.executar(%this.prioridade_0.getObject(0));	
		return;
	}
	
	if(%this.prioridade_1.getCount() > 0)
	{
		%this.executar(%this.prioridade_1.getObject(0));	
		return;
	}
	
	if(%this.prioridade_2.getCount() > 0)
	{
		%this.executar(%this.prioridade_2.getObject(0));	
		return;
	}
	
	if(%this.prioridade_3.getCount() > 0)
	{
		%this.executar(%this.prioridade_3.getObject(0));	
		return;
	}
	
	if(%this.prioridade_4.getCount() > 0)
	{
		%this.executar(%this.prioridade_4.getObject(0));	
		return;
	}
	
	if(%this.prioridade_5.getCount() > 0)
	{
		%this.executar(%this.prioridade_5.getObject(0));	
		return;
	}
}

function filas_handler::executar(%this, %filaObj)
{
	if($servidorEnviandoDados)
		return;
		
	$filas_handler.enviados.add(%filaObj);
	%filaObj.myPrioridadeOriginalSimSet.remove(%filaObj);
	serverEnviarDadosDaFila(%filaObj);
	
	//Na função abaixo, entram apenas as linhas que não estão na função serverEnviarDadosDaFila(%filaObj);
	switch$(%filaObj.tipo)
	{
		case "login": schedule(15000, 0, "verificarLogin", %filaObj); //prioridade 1
		case "fim_de_jogo": schedule(25000, 0, "verificarRegistroDoJogo", %filaObj); //prioridade 1
		case "criar_sala": %filaObj.obj_rel.criarUrl = %filaObj.url; //prioridade 1
		//case "criar_jogo": FEITO! básico basta//prioridade 1 - precisa de verificação, pro caso de cair antes disso!
		//case "buscar_academia": FEITO! básico basta [verificação??]//prioridade 2
		//case "adicionar_na_sala": FEITO! básico basta//prioridade 2
		//case "remover_da_sala": FEITO! básico basta//prioridade 2
		case "investir": schedule(25000, 0, "serverVerificarPesqOK", %filaObj); //prioridade 2
		case "iniciar_pesquisa": schedule(25000, 0, "serverVerificarPesqOK", %filaObj); //prioridade 2
		case "comprar_pesquisa": schedule(25000, 0, "serverVerificarPesqOK", %filaObj); //prioridade 2
		case "finalizar_pesquisa": schedule(25000, 0, "serverVerificarPesqOK", %filaObj); //prioridade 2
		case "usar_artefato": schedule(25000, 0, "serverVerificarPesqOK", %filaObj); //prioridade 2
		//case "criar_persona": FEITO! báscio basta //prioridade 3
		//case "apagar_persona": FEITO! báscio basta //prioridade 3
		case "finalizar_tutorial": schedule(25000, 0, "verificarRegistroDoTutorial", %filaObj);//prioridade 4 
		case "abrir_guloks": schedule(25000, 0, "verificarRegistroDeGuloks", %filaObj);//prioridade 2 
		case "pk_fichas": schedule(25000, 0, "serverVerificarPesqOK", %filaObj); //prioridade 2 
	}
}

function filas_handler::newFilaObj(%this, %tipo, %url, %prioridade, %obj_rel, %obj2_rel)
{
	$filaObjNum++;

	%newFilaObj = new ScriptObject(){};
	%newFilaObj.tipo = %tipo;
	%newFilaObj.url = %url;
	%newFilaObj.prioridade = %prioridade;
	%newFilaObj.obj_rel = %obj_rel;
	%newFilaObj.obj2_rel = %obj2_rel;
	
	%eval = "%this.prioridade_" @ %prioridade @ ".add(%newFilaObj);";
	eval(%eval);
	
	%eval = "%newFilaObj.myPrioridadeOriginalSimSet = %this.prioridade_" @ %prioridade @ ";";
	eval(%eval);
		
	%this.executarProxima();
}

/////////////
//Básico:
function serverEnviarDadosDaFila(%filaObj)
{
	$servidorEnviandoDados = true;
	new TCPObject(tcpObjGet);
	$serverURL = %filaObj.url;
	tcpObjGet.connect($enderecoTaxo @ ":80");
	
	serverStarEnviandoDadosTimer();
}

function serverPermitirEnvio()
{
	$servidorEnviandoDados = false;
	$filas_handler.executarProxima();
}

function severCancelEnviandoDadosTimer()
{
	cancel($enviandoDadosTimer);	
}

$tempoPraEnviarDados = 20000; //20 segundos de timeOut
function serverStarEnviandoDadosTimer()
{
	schedule($tempoPraEnviarDados, 0, "serverPermitirEnvio");
}
//////////////
//Login:
function verificarLogin(%filaObj){
	%user = %filaObj.obj_rel;
	if(%user.personasCount == -2)
	{
		//Não conseguiu se conectar com o taxo!
		echo("Não obtive resposta do TAXO para o login " @ %user.nome @ ". Desconectando usuário...feito!");
		serverRespoderErroGenerico(%user.client);
		return;
	}
	
	echo("LOGIN OK: " @ %user.nome);
}


/////////////////////////
//Fim de Jogo:
function enviarTAXOResultado(%jogo)
{
	%dados = serverCriarUrl(%jogo);
	$filas_handler.newFilaObj("fim_de_jogo", "/torque/jogo/finalizar?" @ %dados, 1, %jogo);
}

function verificarRegistroDoJogo(%filaObj)
{
	$filas_handler.enviados.remove(%filaObj);
	
	%jogo = %filaObj.obj_rel;
	if(%jogo.registrado)
	{
		%filaObj.delete();
		return;
	}
		
	echo("JOGO " @ %jogo.num @ " não foi gravado no Taxo.");
	serverGravarFalha_jogo(%jogo);
	echo("Voltando jogo para prioridade 1!");
	$filas_handler.prioridade_1.add(%filaObj);
	$filas_handler.executarProxima();
}


/////////////////////
//Pesquisas:
function serverVerificarPesqOK(%filaObj){
	%serverPesq = %filaObj.obj_rel;
	if(!isObject(%serverPesq))
		return;
	
	if(!%serverPesq.ok){
		//o registro no taxo falhou!
		echo("ServerPesq(" @ %serverPesq.num @ ") não foi registrada no Taxo.");
		serverGravarFalha_pesq(%serverPesq);
		echo("Voltando jogo para prioridade 2!");
		$filas_handler.prioridade_2.add(%filaObj);
		$filas_handler.executarProxima();
	} else {
		echo("Verificação da serverPesq(" @ %serverPesq.num @ "): OK! Objeto apagado da memória.");
		%serverPesq.delete();
	}
}


////////////////
//Tutorial:
function verificarRegistroDoTutorial(%filaObj)
{
	%persona = %filaObj.obj_rel;
	if(%persona.tutOK)
		return;
		
	echo("Persona " @ %persona.nome @ " não conseguiu finalizar o tutorial no Taxo.");
	serverGravarFalha_tut(%persona);
	echo("Voltando jogo para prioridade 4!");
	$filas_handler.prioridade_4.add(%filaObj);
	$filas_handler.executarProxima();
}


/////////////
//Guloks:
function verificarRegistroDeGuloks(%filaObj)
{
	gravarRegistroDeGuloks(%filaObj);
	if(!%filaObj.obj_rel.ok)
	{
		echo("Persona " @ %filaObj.obj2_rel.nome @ " não conseguiu abrir os Guloks no Taxo.");
		echo("Voltando jogo para prioridade 2!");
		$filas_handler.prioridade_2.add(%filaObj);
		$filas_handler.executarProxima();
		return;
	}
	
	echo("Registro de abertura de Guloks OK!");	
}

function gravarRegistroDeGuloks(%filaObj)
{
	%hora = getLocalTime();
	%persona = %filaObj.obj2_rel;
	
	%txt = "User: " @ %persona.user.nome @ "; ";
	%txt = %txt @ "Persona: " @ %persona.nome @ " -> ";
	%txt = %txt @ "Abriu os Guloks! (data: " @ %hora @ ")";
	%txt = %txt @ "Jogos até agora: " @ %persona.TAXOjogos @ "; ";
	%txt = %txt @ "Vitórias agora: " @ %persona.TAXOvitorias @ "; ";
	%txt = %txt @ "Pontos agora: " @ %persona.TAXOpontos @ "; ";
	%txt = %txt @ "Visionário agora: " @ %persona.TAXOvisionario @ "; ";
	%txt = %txt @ "Arrebatador agora: " @ %persona.TAXOarrebatador @ "; ";
	%txt = %txt @ "Comerciante agora: " @ %persona.myComerciante @ "; ";
	%txt = %txt @ "Diplomata agora: " @ %persona.myDiplomata @ "; ";
	
	%file = new FileObject();
	%file.openForAppend(expandFileName("game/data/files/conheceG.txt")); //abre para adicionar texto
	//atualiza o arquivo:
	%file.WriteLine(%txt);
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM		
}












initFilaSys();