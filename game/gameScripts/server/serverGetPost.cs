// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverGetPost.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 23 de novembro de 2007 15:33
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  comunicação com o banco de dados TAXO
//                    :  
//                    :  
// ============================================================
//serverInitFalhaPesquisas(); //iniciar uma variável de pesquisas no início de cada sessão
$serverPesquisaNumGlobal = 0;
$userAgent = "Torque/1.7";

function serverCriarUrl(%jogo){
	serverConverterNomes(%jogo);	
	
	%dados = "idJogo=" @ %jogo.taxoId;
	%dados = %dados @ "&idJogoTorque=" @ %jogo.num;
	%dados = %dados @ "&duracao=" @ %jogo.tempoDeJogo;
	
	for (%i = 0; %i < %jogo.playersAtivos; %i++){
		%player = %jogo.simPlayers.getObject(%i);
		%idsPersona = %idsPersona @ %player.persona.TAXOid @ ";";
		%creditos = %creditos @ %player.creditosAgora @ ";";
		%b_vitoria = %b_vitoria @ %player.ganhouOJogo @ ";";
		%pontos = %pontos @ %player.totalDePontos @ ";";
		%b_visionario = %b_visionario @ %player.imperio @ ";";
		%atacou = %atacou @ %player.atacou @ ";";
		%arrebatador = %arrebatador @ %player.arrebatadorAgora @ ";";
		%comerciante = %comerciante @ %player.comercianteAgora @ ";";
		%onde_saiu = %onde_saiu @ %player.grupoInicial @ ";";
		%id_obj1 = %id_obj1 @ %player.mySimObj.getObject(0).num @ ";";
		%id_obj2 = %id_obj2 @ %player.mySimObj.getObject(1).num @ ";";
		%qts_matou = %qts_matou @ %player.qtsMatou @ ";";
		%b_pk_power_play = %b_pk_power_play @ %player.partidaPerfeita @ ";";
		
		if(%player.suicidouSe !$= "s" && %player.suicidouSe !$= "r"){
			%player.suicidouSe = "f";	//F = finalizou; S = suicidou; R = rendeu;
		}
				
		%suicidou = %suicidou @ %player.suicidouSe @ ";";
	}
	%dados = %dados @ "&idsPersona=" @ %idsPersona @ "&creditos=" @ %creditos @ "&b_vitoria=" @ %b_vitoria @ "&pontos=" @ %pontos;
	%dados = %dados @ "&b_visionario=" @ %b_visionario @ "&b_atacou=" @ %atacou @ "&b_arrebatador=" @ %arrebatador @ "&b_comerciante=" @ %comerciante;
	%dados = %dados @ "&onde_saiu=" @ %onde_saiu @ "&id_obj1=" @ %id_obj1 @ "&id_obj2=" @ %id_obj2 @ "&qts_matou=" @ %qts_matou;
	%dados = %dados @ "&b_pk_power_play=" @ %b_pk_power_play;
	%dados = %dados @ "&termino=" @ %suicidou;
	%dados = %dados @ "&planeta=" @ %jogo.planeta.id;
		
	%jogo.rawUrl = %dados;
	%jogo.finalizarUrl = "/torque/jogo/finalizar?" @ %dados;
	
	return %dados;
}

//A powerPlay tem que ser marcada em cada player. Se o jogo não for de poker, powerPlay é uma partida perfeita.
//adicionar &b_pk_power_play=0;0;1;

//esta função não deve mais ser necessária, pois o taxo vai computar isso:
function serverConverterNomesUngart(%jogo){
	for (%i = 0; %i < %jogo.playersAtivos; %i++){
		%player = %jogo.simPlayers.getObject(%i);
		
		switch$ (%player.grupoInicial){
			case "ChOc":
			%player.grupoInicial = "Ch.Ocidental";
			
			case "ChOr":
			%player.grupoInicial = "Ch.Oriental";
			
			case "PrEx":
			%player.grupoInicial = "Praias";
			
			case "DeEx":
			%player.grupoInicial = "Deserto";
			
			case "PlDo":
			%player.grupoInicial = "Platô";
			
			case "MoVe":
			%player.grupoInicial = "M.Verticais";
			
			case "IlVu":
			%player.grupoInicial = "I.Vulcânica";
			
			case "CaNo":
			%player.grupoInicial = "Cn.Nórdico";
			
			case "VaNo":
			%player.grupoInicial = "V.Nórdico";
			
			case "CaOr":
			%player.grupoInicial = "Cn.Oriental";
			
			case "VaOr":
			%player.grupoInicial = "V.Oriental";
			
			case "PaGu":
			%player.grupoInicial = "Pântano";
			
			case "VaGu":
			%player.grupoInicial = "V.Gulok";
		}
	}
}


function serverConverterNomes(%jogo){
	for (%i = 0; %i < %jogo.playersAtivos; %i++){
		%player = %jogo.simPlayers.getObject(%i);
		
		switch$ (%player.grupoInicial){
			case "ChOc":
			%player.grupoInicial = "1";
			
			case "ChOr":
			%player.grupoInicial = "2";
			
			case "PrEx":
			%player.grupoInicial = "3";
			
			case "DeEx":
			%player.grupoInicial = "4";
			
			case "PlDo":
			%player.grupoInicial = "5";
			
			case "MoVe":
			%player.grupoInicial = "6";
			
			case "IlVu":
			%player.grupoInicial = "7";
			
			case "CaNo":
			%player.grupoInicial = "8";
			
			case "VaNo":
			%player.grupoInicial = "9";
			
			case "CaOr":
			%player.grupoInicial = "10";
			
			case "VaOr":
			%player.grupoInicial = "11";
			
			case "PaGu":
			%player.grupoInicial = "12";
			
			case "VaGu":
			%player.grupoInicial = "13";
			
			//Terra:
			case "Brasil":
			%player.grupoInicial = "1";
			
			case "EUA":
			%player.grupoInicial = "2";
			
			case "China":
			%player.grupoInicial = "3";
			
			case "Europa":
			%player.grupoInicial = "4";
			
			case "Canada":
			%player.grupoInicial = "5";
			
			case "Africa":
			%player.grupoInicial = "6";
			
			case "Australia":
			%player.grupoInicial = "7";
			
			case "Russia":
			%player.grupoInicial = "8";
			
			case "Oriente":
			%player.grupoInicial = "9";
			
			case "Terion":
			%player.grupoInicial = "1";
			
			case "Nir":
			%player.grupoInicial = "2";
			
			case "Karzin":
			%player.grupoInicial = "3";
			
			case "Goruk":
			%player.grupoInicial = "4";
			
			case "Malik":
			%player.grupoInicial = "5";
			
			case "Zavinia":
			%player.grupoInicial = "6";
			
			case "Dharin":
			%player.grupoInicial = "7";
			
			case "Valinur":
			%player.grupoInicial = "8";
			
			case "Argonia":
			%player.grupoInicial = "9";
			
			case "Vuldan":
			%player.grupoInicial = "10";
			
			case "Lornia":
			%player.grupoInicial = "11";
			
			case "Keltur":
			%player.grupoInicial = "12";
			
			case "Nexus":
			%player.grupoInicial = "13";
			
			case "GeoCanhao":
			%player.grupoInicial = "14";
		}
	}
}
//



$enviandoDados = false; //seta a variável que garante que os dados sejam enviados um por vez, sem perigo da variável global $dados ser modificada enquanto a conexão não acontece;
/////////////////////////

//
function tcpObjGet::onConnected(%this)
{
	severCancelEnviandoDadosTimer();
	%this.send("GET " @ $serverURL @ " HTTP/1.0\nHost: " @ $enderecoTaxo @ "\nUser-Agent: " @ $userAgent @ "\n\r\n\r\n");
	echo ("Conectado: " @ $enderecoTaxo @ $serverURL);
	schedule(1000, 0, "serverPermitirEnvio");
}


function tcpObjGet::onLine(%this, %dados)
{
	if (firstWord(%dados) $= "persona"){
		dadosRecebidos(%dados);
		echo(">>TAXODADOS RECEBIDOS (persona): " @ %dados);
	} else if(firstWord(%dados) $= "username"){ //isso é o que faz o sistema saber se está pesquisando uma persona ou um usuário (terá uma pesquisa por persona depois que o cara escolhe com quem jogar).
		loginRecebido(%dados);
		echo(">>TAXODADOS RECEBIDOS (login): " @ %dados);
	} else if (firstWord(%dados) $= "getPersona"){ 
		getPersonaRecebido(%dados);
		echo(">>TAXODADOS RECEBIDOS (getPersona): " @ %dados);
	} else if (firstWord(%dados) $= "personaOK"){ 
		TaxoPersonaOK(%dados);
		echo(">>TAXODADOS RECEBIDOS (personaOK): " @ %dados);
	} else if (firstWord(%dados) $= "personaNOK"){ 
		TaxoPersonaNOK(%dados);
		echo(">>TAXODADOS RECEBIDOS (personaNOK): " @ %dados);
	} else if (firstWord(%dados) $= "personaInativada"){ 
		TaxoPersonaInativada(%dados);
		echo(">>TAXODADOS RECEBIDOS (personaInativada): " @ %dados);
	} else if (firstWord(%dados) $= "dadosAcademia"){ 
		dadosAcademiaRecebido(%dados);
		echo(">>TAXODADOS RECEBIDOS (dadosAcademia): " @ %dados);
	} else if (firstWord(%dados) $= "url"){ 
		gravarMinhaFotoAddress(%dados);
		echo(">>TAXODADOS RECEBIDOS (url): " @ %dados);
	} else if (firstWord(%dados) $= "idJogo"){ 
		setarSalaJogoId(%dados);
		echo(">>TAXODADOS RECEBIDOS (idJogo): " @ %dados);
	} else if (firstWord(%dados) $= "OK"){
		serverConfirmarRegistroDeJogo(%dados);
		echo(">>TAXODADOS RECEBIDOS (OK): " @ %dados);
	} else if(firstWord(%dados) $= "personaTutorial"){
		%userName = getWord(%dados, 1);
		serverMarcarTutorialOk(%userName);
		echo(">>TAXODADOS RECEBIDOS (personaTutorial): " @ %dados);
	} else if(firstWord(%dados) $= "academiaOK"){
		%num = getWord(%dados, 1);
		serverMarcarPesquisaOk(%num);
		echo(">>TAXODADOS RECEBIDOS (academiaOK): " @ %dados);
	} else if(firstWord(%dados) $= "GULOK"){
		%num = getWord(%dados, 1);
		serverMarcarPesquisaOk(%num);
		echo(">>TAXODADOS RECEBIDOS (GULOK): " @ %dados);
	}
	echo("***TAXODADOS RECEBIDOS: " @ %dados);
}





//////////////
//recebendo um usuário e o preview de suas personas (sem informações sobre pesquisas):
function loginRecebido(%dados){
	echo("Login response: " @ %dados);

	%i = 0;
	%chave = "inicio";
	
	if (%chave !$= "fim"){
		%chave = getWord(%dados, %i);
		%valor = getWord(%dados, %i+1);

		if (%chave $= "username"){
			%TaxoDados[0] = %valor;
			%i = %i + 2;
			%chave = getWord(%dados, %i);
			%valor = getWord(%dados, %i+1);
			
			if (%chave $= "userId"){
				%TaxoDados[1] = %valor;
				%i = %i + 2;
				%chave = getWord(%dados, %i);
				%valor = getWord(%dados, %i+1);
			}
			
			if (%chave $= "omnis"){
				%TaxoDados[2] = %valor;
				%i = %i + 2;
				%chave = getWord(%dados, %i);
				%valor = getWord(%dados, %i+1);
			}
			
			if (%chave $= "conhece_g"){
				%TaxoDados[3] = %valor;
				%i = %i + 2;
				%chave = getWord(%dados, %i);
				%valor = getWord(%dados, %i+1);
			}
									
			if (%chave $= "qtde_personas"){ //o número de personas que o usuário possui
				%TaxoDados[4] = %valor;
				%i = %i + 2;
				%chave = getWord(%dados, %i);
				%valor = getWord(%dados, %i+1);
							
				for(%j = 1; %j < %TaxoDados[4] + 1; %j++){
					%eval = "%thisId = id" @ %j @ ";";
					eval(%eval);
					%eval = "%thisNome = nome" @ %j @ ";";
					eval(%eval);
					%eval = "%thisJogos = jogos" @ %j @ ";";
					eval(%eval);
					%eval = "%thisVitorias = vitorias" @ %j @ ";";
					eval(%eval);
					%eval = "%thisPontos = pontos" @ %j @ ";";
					eval(%eval);
					%eval = "%thisImperios = visionario" @ %j @ ";";
					eval(%eval);
					%eval = "%thisArrebatador = arrebatador" @ %j @ ";";
					eval(%eval);
					%eval = "%thisComerciante = comerciante" @ %j @ ";";
					eval(%eval);
					%eval = "%thisAtacou = atacou" @ %j @ ";";
					eval(%eval);
					%eval = "%thisCreditos = creditos" @ %j @ ";";
					eval(%eval);
					%eval = "%thisTutorial = b_tutorial" @ %j @ ";";
					eval(%eval);
					%eval = "%thisEspecie = especie" @ %j @ ";";
					eval(%eval);
					%eval = "%thisPk_fichas = pk_fichas" @ %j @ ";";
					eval(%eval);
					%eval = "%thisPk_vitorias = pk_vitorias" @ %j @ ";";
					eval(%eval);
					%eval = "%thisPk_power_plays = pk_power_plays" @ %j @ ";";
					eval(%eval);
									
					%base = 15*(%j-1);
					
					if (%chave $= %thisId){
						%TaxoDados[5 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisNome){
						%TaxoDados[6 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisJogos){
						%TaxoDados[7 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisVitorias){
						%TaxoDados[8 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisPontos){
						%TaxoDados[9 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisImperios){
						%TaxoDados[10 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisArrebatador){
						%TaxoDados[11 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisComerciante){
						%TaxoDados[12 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisAtacou){
						%TaxoDados[13 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisCreditos){
						%TaxoDados[14 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisTutorial){
						%TaxoDados[15 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisEspecie){
						%TaxoDados[16 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisPk_vitorias){
						%TaxoDados[17 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisPk_fichas){
						%TaxoDados[18 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
					if (%chave $= %thisPk_power_plays){
						%TaxoDados[19 + %base] = %valor;
						%i = %i + 2;
						%chave = getWord(%dados, %i);
						%valor = getWord(%dados, %i+1);
					}
				}
			}
		}
	}
		
	echo (">>>>>LOGIN:" @ %TaxoDados[0] @ " > Personas:" @ %TaxoDados[4]);

	echo("TAXO DADOS MONTADO:");
	for(%kk = 0; %kk < 80; %kk++) {
		echo(%TaxoDados[%kk]);
	}
	echo("FIM DO TAXO DADOS!");
	
	if(%TaxoDados[0] !$= "" && %TaxoDados[4] !$= ""){
		for(%k = 0; %k < $serverSimUSERS.getCount(); %k++){
			%user = $serverSimUSERS.getObject(%k);
			if(%TaxoDados[0] $= %user.login){ 
				%user.nome = %TaxoDados[0];
				%user.TAXOid = %TaxoDados[1];
				%user.TAXOomnis = %TaxoDados[2];
				%user.TAXOconhece_g = %TaxoDados[3];
				%user.personasCount = %TaxoDados[4];
								
				for(%m = 0; %m < %TaxoDados[4]; %m++){
					%base = 15 * %m;
					
					%persona = newSimObj("persona", %m);
					%user.myPersonas.add(%persona);
					
					%x = %user.myPersonas.getObject(%m);
					%x.TAXOid = %TaxoDados[5 + %base];
					%x.nome = %TaxoDados[6 + %base];
					%x.TAXOjogos = %TaxoDados[7 + %base];
					%x.TAXOvitorias = %TaxoDados[8 + %base];
					%x.TAXOpontos = %TaxoDados[9 + %base];
					%x.TAXOvisionario = %TaxoDados[10 + %base];
					%x.TAXOarrebatador = %TaxoDados[11 + %base];
					%x.TAXOcomerciante = %TaxoDados[12 + %base];
					%x.TAXOatacou = %TaxoDados[13 + %base];
					%x.TAXOcreditos = %TaxoDados[14 + %base];
					%x.TAXOtutorial = %TaxoDados[15 + %base];
					%x.especie = %TaxoDados[16 + %base];
					%x.pk_vitorias = %TaxoDados[17 + %base];
					%x.pk_fichas = %TaxoDados[18 + %base];
					%x.pk_power_plays = %TaxoDados[19 + %base];


					if(%x.TAXOjogos $= ""){
						%x.TAXOjogos = 0;	
					}
					if(%x.TAXOvitorias $= ""){
						%x.TAXOvitorias = 0;	
					}
					%x.setPatente();
					%x.setPorcentagens();
				}
				%k = $serverSimUSERS.getCount(); //sai do loop de busca por usuários;
			}
		}
	} 
	$servidor.pesquisaEmAndamento = false;
	verificarTaxoLogin(%user);
}


function schedulePesquisa(%nome){
	if($servidor.pesquisaEmAndamento){
		schedule(2500, 0, "schedulePesquisa", %nome); 
	} else {
		pesquisarPlayerPorNome(%nome);	
		$servidor.pesquisaEmAndamento = true;
	}
}

function serverDesconectarUsuario(%client){
	if(!isObject(%client))
		return;
		
	%client.user.offline = true;
	$serverSimUSERS.remove(%client.user);
	//%client.user.delete();
	%client.delete();	
}

function serverRespoderErroGenerico(%client)
{
	commandToClient(%client, 'erroGenericoNaConexao');	
}

$SO_explode = 1;
function explode(%string, %char){
	%eval = "new ScriptObject(dados" @ $SO_explode @ ");";
	eval(%eval);
	%eval = "%myDados = dados" @ $SO_explode @ ";";
	eval(%eval);

	%explodeCount = 0;
	%lastFound = 0;

	%endChar = strLen(%string);	
	%charLen = strLen(%char);

	for(%i=0;%i<%endChar;%i++){
		%charToCheck = getSubStr(%string, %i, %charLen);
		if(%charToCheck $= %char){
			%myDados.get[%explodeCount] = getSubStr(%string, %lastFound, (%i-%lastFound)); 
			%lastFound = %i + %charLen;
			%explodeCount++;
		}	
	}

	
	%myDados.get[%explodeCount] = getSubStr(%string, %lastFound, (%i-%lastFound)); 
	%myDados.count = %explodeCount + 1;	

	$SO_explode++;
	return %myDados;
}

$SO_hash = 1;
function obterHash(%chaves, %valores)
{
	%eval = "new ScriptObject(hash" @ $SO_hash @ ");";
	eval(%eval);
	%eval = "%myDados = hash" @ $SO_hash @ ";";
	eval(%eval);
	
	for(%i=0; %i<%chaves.count; %i++)
	{
		%chave = %chaves.get[%i];
		%valor = %valores.get[%i];
		%eval = "%myDados." @ %chave @ " = " @ %valor @ ";";
		eval(%eval);
		echo(%eval);
	}
	
	$SO_hash++;
	return %myDados;
}

function testarHash()
{
	$chaves = explode("nome,id", ",");
	$valores = explode("dani,14", ",");
	return obterHash($chaves , $valores);
}

function obterScriptObj(%nome)
{
	%strCont = "$SO_" @ %nome;
	%eval = %strCont @ "++;";
	eval(%eval);
	%eval = %myCont @ " = %strCont;";
	eval(%eval);
	
	%eval = "new ScriptObject(" @ %nome @ %myCont @ ");";
	eval(%eval);
	
	return %nome;
	
}


////////////////////
//Academia:
function pesquisarAcademiaPorPersona(%personaTaxoid){
	new TCPObject(tcpObjGet);
	
	$serverURL = "/torque/academia/buscar?idPersona=" @ %personaTaxoId;
	tcpObjGet.connect($enderecoTaxo @ ":80");
}

function dadosAcademiaRecebido(%taxoDados){
	%userName = getWord(%taxoDados, 1);
	%eval = "%persona = $USER" @ %userName @ ".persona;";
	eval(%eval);
	
	//soldados:
	%persona.aca_s_d_min = getWord(%taxoDados, 2);
	%persona.aca_s_d_max = getWord(%taxoDados, 3);
	%persona.aca_s_a_min = getWord(%taxoDados, 4);
	%persona.aca_s_a_max = getWord(%taxoDados, 5);
	//tanques
	%persona.aca_t_d_min = getWord(%taxoDados, 6);
	%persona.aca_t_d_max = getWord(%taxoDados, 7);
	%persona.aca_t_a_min = getWord(%taxoDados, 8);
	%persona.aca_t_a_max = getWord(%taxoDados, 9);
	//navios:
	%persona.aca_n_d_min = getWord(%taxoDados, 10);
	%persona.aca_n_d_max = getWord(%taxoDados, 11);
	%persona.aca_n_a_min = getWord(%taxoDados, 12);
	%persona.aca_n_a_max = getWord(%taxoDados, 13);
	//líder1:
	%persona.aca_ldr_1_d_min = getWord(%taxoDados, 14);
	%persona.aca_ldr_1_d_max = getWord(%taxoDados, 15);
	%persona.aca_ldr_1_a_min = getWord(%taxoDados, 16);
	%persona.aca_ldr_1_a_max = getWord(%taxoDados, 17);
	%persona.aca_ldr_1_h1 = getWord(%taxoDados, 18);
	%persona.aca_ldr_1_h2 = getWord(%taxoDados, 19);
	%persona.aca_ldr_1_h3 = getWord(%taxoDados, 20);
	%persona.aca_ldr_1_h4 = getWord(%taxoDados, 21);
	//líder2:
	%persona.aca_ldr_2_d_min = getWord(%taxoDados, 22);
	%persona.aca_ldr_2_d_max = getWord(%taxoDados, 23);
	%persona.aca_ldr_2_a_min = getWord(%taxoDados, 24);
	%persona.aca_ldr_2_a_max = getWord(%taxoDados, 25);
	%persona.aca_ldr_2_h1 = getWord(%taxoDados, 26);
	%persona.aca_ldr_2_h2 = getWord(%taxoDados, 27);
	%persona.aca_ldr_2_h3 = getWord(%taxoDados, 28);
	%persona.aca_ldr_2_h4 = getWord(%taxoDados, 29);
	//líder3:
	%persona.aca_ldr_3_d_min = getWord(%taxoDados, 30);
	%persona.aca_ldr_3_d_max = getWord(%taxoDados, 31);
	%persona.aca_ldr_3_a_min = getWord(%taxoDados, 32);
	%persona.aca_ldr_3_a_max = getWord(%taxoDados, 33);
	%persona.aca_ldr_3_h1 = getWord(%taxoDados, 34);
	%persona.aca_ldr_3_h2 = getWord(%taxoDados, 35);
	%persona.aca_ldr_3_h3 = getWord(%taxoDados, 36);
	%persona.aca_ldr_3_h4 = getWord(%taxoDados, 37);
	//visionário
	%persona.aca_v_1 = getWord(%taxoDados, 38);
	%persona.aca_v_2 = getWord(%taxoDados, 39);
	%persona.aca_v_3 = getWord(%taxoDados, 40);
	%persona.aca_v_4 = getWord(%taxoDados, 41);
	%persona.aca_v_5 = getWord(%taxoDados, 42);
	%persona.aca_v_6 = getWord(%taxoDados, 43);
	//arebatador:
	%persona.aca_a_1 = getWord(%taxoDados, 44);
	%persona.aca_a_2 = getWord(%taxoDados, 45);
	//comerciante:
	%persona.aca_c_1 = getWord(%taxoDados, 46);
	//diplomata:
	%persona.aca_d_1 = getWord(%taxoDados, 47);
	//intel:
	%persona.aca_i_1 = getWord(%taxoDados, 48);
	%persona.aca_i_2 = getWord(%taxoDados, 49);
	%persona.aca_i_3 = getWord(%taxoDados, 50);
	//Pesquisa Em Andamento:
	%persona.aca_pea_id = getWord(%taxoDados, 51);
	%persona.aca_pea_min = getWord(%taxoDados, 52);
	%persona.aca_pea_pet = getWord(%taxoDados, 53);
	%persona.aca_pea_ura = getWord(%taxoDados, 54);
	%persona.aca_pea_ldr = getWord(%taxoDados, 55);
	
	%persona.aca_tenhoDados = true;
	
	if(%persona.aca_pea_id $= "" || %persona.aca_pea_id $= "1"){
		%persona.aca_pea_id = 0;	
	}
	
	%persona.aca_av_1 = getWord(%taxoDados, 56);
	%persona.aca_av_2 = getWord(%taxoDados, 57);
	%persona.aca_av_3 = getWord(%taxoDados, 58);
	%persona.aca_av_4 = getWord(%taxoDados, 59);
	
	%persona.aca_pln_1 = getWord(%taxoDados, 60);
	
	%persona.aca_art_1 = getWord(%taxoDados, 61);
	%persona.aca_art_2 = getWord(%taxoDados, 62);
		
	%dadosAcademia = serverPegarDadosAcademia(%persona);
	serverCalcularPersonaDif(%persona);
	commandToClient(%persona.client, 'registrarDadosAcademia', %dadosAcademia);
}



function serverGravarFalha_pesq(%serverPesq){
	echo("PESQUISA " @ %serverPesq.num @ ": gravando falha.");
	%file = new FileObject();
	
	%file.openForAppend("game/data/files/falhas_academia.txt"); //abre para adicionar texto
	//atualiza o arquivo:
	%file.WriteLine(%serverPesq.url);
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM
	
	%serverPesq.delete(); //deleta tb a serverPesq, que já não serve pra nada.
}

function serverMarcarPesquisaOk(%serverPesqNum){
	%eval = "%myServerPesq = $serverPesq" @ %serverPesqNum @ ";";
	eval(%eval);
	%myServerPesq.ok = true;
	echo("PESQUISA(" @ %serverPesqNum @ ") REGISTRADA COM SUCESSO!");
}

///Função para ser usada no initServer (MAS CUIDADO PARA NÃO APAGAR A ÚLTIMA SESSÃO ANTES DE REGISTRAR AS FALHAS NO TAXO):
//Tb dá pra saber quando muda de sessão pq o idPesqTorque volta a zero; 
function serverZerarArquivosDeFalha(){
	%file = new FileObject();
	%file.openForWrite("game/data/files/falhas_academia.txt"); //zera o arquivo de falhas da academia
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM
	
	%file = new FileObject();
	%file.openForWrite("game/data/files/falhas_jogo.txt"); //zera o arquivo de falhas de jogos
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM
}

function serverInitFalhaPesquisas(){
	/*
	if(!isObject($serverListaPesquisas)){
		$serverListaPesquisas = new SimSet();
	} else {
		$serverListaPesquisas.clear();	
	}
	*/
	$serverPesquisaNumGlobal = 0;
}












/////////////////////
//Tut:


function serverMarcarTutorialOk(%userName){
	%eval = "%user = $user" @ %userName @ ";";
	eval(%eval);
	%persona = %user.persona;
	
	%persona.tutOK = true;
}



function serverGravarFalha_tut(%persona){
	echo("Persona " @ %persona.nome @ ": gravando tut_falha.");
	%file = new FileObject();
	
	%file.openForAppend("game/data/files/falhas_tut.txt"); //abre para adicionar texto
	//atualiza o arquivo:
	%file.WriteLine(%persona.tut_url);
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM
}


////////////////





function marcarJogoTaxoId(%dados){
	echo("MARCAR JOGO TAXOID:" @ %dados);
		
	%taxoId = getWord(%dados, 1);
	%jogoNum = getWord(%dados, 3);
	
	
	//echo("getWord(%dados, 1) = " @ getWord(%dados, 1));
	
	%eval = "%jogo = $jogo" @ %jogoNum @ ";";
	eval(%eval);
	
	echo("%taxoId Original = " @ %jogo.taxoId @ "; TaxoId novo = " @ %taxoId);
	
	%jogo.taxoId = %taxoId;
}

function serverConfirmarRegistroDeJogo(%dados){
	%jogoNum = getWord(%dados, 1);
	
	%eval = "%jogo = $jogo" @ %jogoNum @ ";";
	eval(%eval);
	
	%jogo.registrado = true;
	
	%proximoJogoId = getWord(%dados, 2); //pega o taxoId do proximo jogo
	%jogo.sala.jogoTAXOid = %proximoJogoId; //marca na sala o ID do próximo jogo
	%jogo.sala.ocupada = false; //marca que a sala já está desocupada e novos players podem entrar.
	serverAtualizarAtrioParaTodos();
	echo("JOGO " @ %jogoNum @ " REGISTRADO COM SUCESSO!  Próximo jogoTAXOid = " @ %proximoJogoId);
}



function serverGravarFalha_jogo(%jogo){
	echo("JOGO " @ %jogo.num @ ": gravando falha.");
	%file = new FileObject();
	
	%jogo.newErrorUrl =  "data_inicio=" @ %jogo.newDataInicio @ "&" @ %jogo.rawUrl;
	
	%file.openForAppend("game/data/files/falhas_jogo.txt"); //abre para adicionar texto
	//atualiza o arquivo:
	%file.WriteLine(%jogo.iniciarUrl SPC %jogo.finalizarUrl);
	%file.WriteLine(%jogo.newErrorUrl);
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM
}

////
//Marcar Disconnect:
function serverTAXOMarcarDisconnect(%userTaxoId){
	if($servidorEnviandoDados){
		schedule(1000, 0, "serverTAXOMarcarDisconnect", %userTaxoId);
	} else {
		$servidorEnviandoDados = true;
		
		new TCPObject(tcpObjMarcarDisconnect);
		
		$disconnectURL = "/torque/disconnect?idUsuario=" @ %userTaxoId;
		
		
		tcpObjMarcarDisconnect.connect($enderecoTaxo @ ":80");
	}
}
function tcpObjMarcarDisconnect::onConnected(%this){
	%this.send("GET " @ $disconnectURL @ " HTTP/1.0\nHost: " @ $enderecoTaxo @ "\nUser-Agent: " @ $userAgent @ "\n\r\n\r\n");
	echo ("Conectado: " @ $enderecoTaxo @ $disconnectURL);
	%this.disconnect();
	schedule(1000, 0, "serverPermitirEnvio");
}


function criarServerPesq(){
	%eval = "$serverPesq" @ $serverPesquisaNumGlobal @ " = new ScriptObject(){};";
	eval(%eval);
	%eval = "%myServerPesq = $serverPesq" @ $serverPesquisaNumGlobal @ ";";
	eval(%eval);
	%myServerPesq.num = $serverPesquisaNumGlobal;
	$serverPesquisaNumGlobal++; //incrementa o número global de pesquisas (iniciar/finalizar/investir/comprar: tudo num só)	
		
	return %myServerPesq;
}


///////////////////////
function setarSalaJogoId(%dados){
	%jogoTAXOid = getWord(%dados, 1);
	%username = getWord(%dados, 3);
	%eval = "%persona = $user" @ %username @ ".persona;";
	eval(%eval);
	if(%jogoTAXOid !$= "0"){
		%sala = %persona.sala;
		%sala.jogoTAXOid = %jogoTAXOid;
			
		echo(">>TAXODADOS RECEBIDOS(Sala): " @ %dados);	
	} else {
		echo("TAXO removeu a sala " @ %sala.num);
	}
}




//resposta = OK sigmus
function serverTAXOdescorbrirGuloks(%persona, %serverPesq){
	%serverPesq.tentativasDeEnvio++;
	if(%serverPesq.url $= ""){
		%serverPesq.url = "/torque/conhece_g/" @ %persona.user.nome @ "?idPesqTorque=" @ %serverPesq.num;
	}
	if($servidorEnviandoDados){
		if(%serverPesq.tentativasDeEnvio < 100){
			echo("TAXO Descobrir Guloks(" @ %serverPesq.num @ "): tentativa " @ %serverPesq.tentativasDeEnvio @ ", URL: " @ %serverPesq.url);
			schedule(1000, 0, "serverTAXOdescorbrirGuloks", %persona, %serverPesq); 
		} else {
			serverVerificarPesqOK(%serverPesq);
		}
	} else {
		echo("ATENÇÃO: serverTAXOdescorbrirGuloks URL = " @ %serverPesq.url);	
		$servidorEnviandoDados = true;
		
		new TCPObject(tcpObjDescobrirGuloks);
		$descobrirURL = %serverPesq.url;
		schedule(25000, 0, "serverVerificarPesqOK", %serverPesq);
		
		tcpObjDescobrirGuloks.connect($enderecoTaxo @ ":80");
	}	
}

function tcpObjDescobrirGuloks::onConnected(%this){
	%this.send("GET " @ $descobrirURL @ " HTTP/1.0\nHost: " @ $enderecoTaxo @ "\nUser-Agent: " @ $userAgent @ "\n\r\n\r\n");
	echo ("Conectado: " @ $enderecoTaxo @ $descobrirURL);
	//%this.disconnect();
	schedule(1000, 0, "serverPermitirEnvio");
}
function tcpObjDescobrirGuloks::onLine(%this, %dados){
	echo("***TAXODADOS RECEBIDOS(Descobrir Guloks): " @ %dados);
	if(firstWord(%dados) $= "OK"){
		%num = getWord(%dados, 1);
		serverMarcarPesquisaOk(%num);
	}
}