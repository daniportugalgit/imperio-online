// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverAcademia.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 18 de março de 2008 22:42
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
//quando inicia e quando investe mandar a_PEApercent;


function serverPegarDadosAcademia(%persona){
	//soldados:
	%var = %persona.aca_s_d_min; //0
	%var = %var SPC %persona.aca_s_d_max; //1
	%var = %var SPC %persona.aca_s_a_min; //2
	%var = %var SPC %persona.aca_s_a_max; //3
	//tanques:
	%var = %var SPC %persona.aca_t_d_min; //4
	%var = %var SPC %persona.aca_t_d_max; //5
	%var = %var SPC %persona.aca_t_a_min; //6
	%var = %var SPC %persona.aca_t_a_max; //7 
	//navios:
	%var = %var SPC %persona.aca_n_d_min; //8
	%var = %var SPC %persona.aca_n_d_max; //9
	%var = %var SPC %persona.aca_n_a_min; //10
	%var = %var SPC %persona.aca_n_a_max; //11
	//líder1:
	%var = %var SPC %persona.aca_ldr_1_d_min; //12
	%var = %var SPC %persona.aca_ldr_1_d_max; //13
	%var = %var SPC %persona.aca_ldr_1_a_min; //14
	%var = %var SPC %persona.aca_ldr_1_a_max; //15
	%var = %var SPC %persona.aca_ldr_1_h1; //16
	%var = %var SPC %persona.aca_ldr_1_h2; //17
	%var = %var SPC %persona.aca_ldr_1_h3; //18
	%var = %var SPC %persona.aca_ldr_1_h4; //19
	//líder2:
	%var = %var SPC %persona.aca_ldr_2_d_min; //20
	%var = %var SPC %persona.aca_ldr_2_d_max; //21
	%var = %var SPC %persona.aca_ldr_2_a_min; //22
	%var = %var SPC %persona.aca_ldr_2_a_max; //23
	%var = %var SPC %persona.aca_ldr_2_h1; //24
	%var = %var SPC %persona.aca_ldr_2_h2; //25
	%var = %var SPC %persona.aca_ldr_2_h3; //26
	%var = %var SPC %persona.aca_ldr_2_h4; //27
	//líder3:
	%var = %var SPC %persona.aca_ldr_3_d_min; //28
	%var = %var SPC %persona.aca_ldr_3_d_max; //29
	%var = %var SPC %persona.aca_ldr_3_a_min; //30
	%var = %var SPC %persona.aca_ldr_3_a_max; //31
	%var = %var SPC %persona.aca_ldr_3_h1; //32
	%var = %var SPC %persona.aca_ldr_3_h2; //33
	%var = %var SPC %persona.aca_ldr_3_h3; //34
	%var = %var SPC %persona.aca_ldr_3_h4; //35
	//visionário
	%var = %var SPC %persona.aca_v_1; //36
	%var = %var SPC %persona.aca_v_2; //37
	%var = %var SPC %persona.aca_v_3; //38
	%var = %var SPC %persona.aca_v_4; //39
	%var = %var SPC %persona.aca_v_5; //40
	%var = %var SPC %persona.aca_v_6; //41
	//arebatador:
	%var = %var SPC %persona.aca_a_1; //42
	%var = %var SPC %persona.aca_a_2; //43
	//comerciante: 
	%var = %var SPC %persona.aca_c_1; //44
	//diplomata: 
	%var = %var SPC %persona.aca_d_1; //45
	//intel:
	%var = %var SPC %persona.aca_i_1; //46
	%var = %var SPC %persona.aca_i_2; //47
	%var = %var SPC %persona.aca_i_3; //48
	//Pesquisa Em Andamento:
	%var = %var SPC %persona.aca_pea_id; //49
	%var = %var SPC %persona.aca_pea_min; //50
	%var = %var SPC %persona.aca_pea_pet; //51
	%var = %var SPC %persona.aca_pea_ura; //52
	%var = %var SPC %persona.aca_pea_ldr; //53
	//marca que a persona tem dados de academia:
	%var = %var SPC %persona.aca_tenhoDados; //54
	//manda os omnis do usuário como último parâmetro:
	%var = %var SPC %persona.user.TAXOomnis; //55
	//Manda a boleana do tutorial:
	%var = %var SPC %persona.TAXOtutorial; //56
	//pesquisas avançadas:
	%var = %var SPC %persona.aca_av_1; //57
	%var = %var SPC %persona.aca_av_2; //58
	%var = %var SPC %persona.aca_av_3; //59
	%var = %var SPC %persona.aca_av_4; //60
	
	%var = %var SPC %persona.aca_pln_1; //61
	%var = %var SPC %persona.aca_art_1; //62
	%var = %var SPC %persona.aca_art_2; //63
	
	%var = %var SPC %persona.especie; //64
	return %var;
}

//para os novos ids:
function findPesquisaPorClassId(%persona, %aca_pea_id){
	echo("findPesquisaPorClassId(%persona = " @ %persona.nome @ "; %aca_pea_id = " @ %aca_pea_id @ ")");
	%eval = "%status = %persona." @ %aca_pea_id @ ";";
	eval(%eval);
					
	%pesquisaAtualNum = %status + 1;
		
	%pesquisaAtual = %aca_pea_id @ %pesquisaAtualNum;
	
	%pesquisaMesmo = findPesquisaPorId(%pesquisaAtual, %persona.especie);
	echo("pesquisaAtual: " @ %pesquisaAtual);
	return %pesquisaMesmo;
}

//////////////
//marcando o tutorial:
function serverCmdMarcarTaxoTutorial(%client){
	%persona = %client.persona;
	commandToClient(%client, 'MarcarTaxoTutorial');
	
	if(%persona.taxoTutorial > 0)
		return;
	
	%persona.taxoTutorial = 1;
	%persona.tut_url = "/torque/persona/tutorial?idPersona=" @ %persona.taxoId @ "&creditos=5&idUsuario=" @ %persona.user.taxoId;
	$filas_handler.newFilaObj("finalizar_tutorial", %persona.tut_url, 4, %persona);
	%persona.taxoCreditos += 5;
}



/////////
//
// RECEBENDO PESQUISAS:
//
function serverZerarPEA(%persona){
	%persona.aca_pea_id = 0;
	%persona.aca_pea_min = 0;
	%persona.aca_pea_pet = 0;
	%persona.aca_pea_ura = 0;
	%persona.aca_pea_ldr = 0;	
}

function serverCriarIdProTaxo(%pesquisa, %liderNum){
	/*
	if(%liderNum == 1 || %liderNum == 2 || %liderNum == 3){
		%pesquisaIDproTAXO = "aca_ldr_" @ %liderNum @ "_" @ %pesquisa.classId;
	} else {
		%pesquisaIDproTAXO = %pesquisa.classId;	
	}	
	*/
	
	%pesquisaIDproTAXO = %pesquisa.classId;
	echo("PESQUISA ID PRO TAXO: " @ %pesquisaIDproTAXO);
	return %pesquisaIDproTAXO;
}

function serverCmda_pesquisar(%client, %pesquisaId, %liderNum){
	echo("serverCmda_pesquisar(" @ %client @ ", " @ %pesquisaId @ ", " @ %liderNum @ ")");
	%persona = %client.persona;
	%pesquisa = findPesquisaPorId(%pesquisaId, %persona.especie);
	
	
	if(%persona.TAXOcreditos >= %pesquisa.custoInicial){
		//zera a pesquisa em andamento:
		serverZerarPEA(%persona);
		
		//marca o id da pesquisa em andamento:
		%persona.aca_pea_id = %pesquisa.classId; 
		
		//marca se é uma pesquisa de líder ou não:
		%persona.aca_pea_ldr = %liderNum;
				
		//Credita a compra
		%persona.TAXOcreditos -= %pesquisa.custoInicial; 	//enviar isso pro TAXO, mais a nova situação do dado alterado
		%creditosRestantes = %persona.TAXOcreditos;
		
		//cria o id pra ser enviado ao taxo:
		%pesquisaIDproTAXO = serverCriarIdProTaxo(%pesquisa, %liderNum);
		
				
		//Cria um objeto que vai guardar esta url e o num da pesquisa, para verificar falhas no registro:
		%myServerPesq = criarServerPesq();
		
		//Request pro taxo:
		%myServerPesq.url = "/torque/academia/iniciar?idPersona=" @ %persona.TAXOid @ "&pesq=" @ %pesquisaIDproTAXO @ "&lider=" @ %liderNum @ "&creditos=-" @ %pesquisa.custoInicial @ "&idPesqTorque=" @ %myServerPesq.num;
		$filas_handler.newFilaObj("iniciar_pesquisa", %myServerPesq.url, 2, %myServerPesq, %persona);
		
		//devolve pro client um OK:
		commandToClient(%client, 'a_iniciarPesquisa', %pesquisaId, %liderNum, %creditosRestantes);
	} else {
		//client cheater MOTHAFUCKA!!!	(pode até deixar ele preso, sem responder o popAguardando! Seu merdinha....
		echo("**CHEATER??? Persona: " @ %persona.nome @ "; User: " @ %persona.user @ "; ERRO: não tinha créditos pra pesquisar, mas o client não bloqueou o pedido.");
	}
}

function serverCmda_investirRecurso(%client, %recurso){
	%persona = %client.persona;
	%jogo = %client.player.jogo;	
	
	if(!%jogo.terminado){
		%pesquisa = findPesquisaPorClassId(%persona, %persona.aca_pea_id);
		
		if(isObject(%pesquisa)){
		
			%eval = "%myRecurso = %persona.player." @ %recurso @ ";";
			eval(%eval);
			if(%myRecurso > 0){ //esta é a terceira verificação, totalmente anti-hacks!
				%eval = "%persona.player." @ %recurso @ " -= 1;"; //subtrai da conta o recurso investido
				eval(%eval);
				if(%recurso $= "minerios"){
					%persona.aca_pea_min ++;
					%persona.player.mineriosInvestidos++;
				} else if(%recurso $= "petroleos"){
					%persona.aca_pea_pet ++;
					%persona.player.petroleosInvestidos++;
				} else if(%recurso $= "uranios"){
					%persona.aca_pea_ura ++;
					%persona.player.uraniosInvestidos++;
				}	
			
			
				%pesquisaCompleta = servera_verificarPesquisaCompleta(%pesquisa.id, %persona);
				commandToClient(%client, 'a_InvestirRecurso', %recurso);
				
				if(%pesquisaCompleta){
					echo("PESQUISA COMPLETA para o usuário " @ %client.user.nome @ ", na Persona " @ %persona.nome @ " (" @ %pesquisa.id @ ")");
					%pesquisaIDproTAXO = serverCriarIdProTaxo(%pesquisa, %persona.aca_pea_ldr);
					%eval = "%persona." @ %pesquisa.classId @ " = " @ %pesquisa.num @ ";";
					eval(%eval);
					
					//Cria um objeto que vai guardar esta url e o num da pesquisa, para verificar falhas no registro:
					%myServerPesq = criarServerPesq();
					%myServerPesq.url = "/torque/academia/finalizar?idPersona=" @ %persona.TAXOid @ "&pesq=" @ %pesquisaIDproTAXO @ "&creditos=0&idPesqTorque=" @ %myServerPesq.num;
					$filas_handler.newFilaObj("finalizar_pesquisa", %myServerPesq.url, 2, %myServerPesq, %persona);
					
					commandToClient(%persona.client, 'a_reconhecerFimDePesquisa');	
					serverZerarPEA(%persona);
				} else {
					echo("DESENVOLVENDO PESQUISA para o usuário " @ %client.user.nome @ ", na Persona " @ %persona.nome @ " (" @ %pesquisa.id @ ")");
				}
				
			} else {
				//Cheater Mothafucka, deixa o cara preso com aguradeMsgBox;
				echo("**CHEATER?? Usuário " @ %client.user.nome @ ", Persona " @ %persona.nome @ " (" @ %pesquisa.id @ "): não tinha o Recurso pra investir!");	
			} 
		} else {
			echo("Não foi possível encontrar a pesquisa para investir! %persona.aca_pea_id = " @ %persona.aca_pea_id);	
		}
	}
}

function servera_verificarPesquisaCompleta(%pesquisaId, %persona){
	%pesquisa = findPesquisaPorId(%pesquisaId, %persona.especie);
	
	if(%pesquisa.cDevMin <= %persona.aca_pea_min && %pesquisa.cDevPet <= %persona.aca_pea_pet && %pesquisa.cDevUra <= %persona.aca_pea_ura){
		return true;	
	} else {
		return false;
	} 
}



function serverCmda_comprar(%client, %pesquisaId, %liderNum){
	%persona = %client.persona;
	%pesquisa = findPesquisaPorId(%pesquisaId, %persona.especie);
	
	if(%persona.user.TAXOomnis >= %pesquisa.cOMNI){
		echo("Persona (" @ %persona.nome @ ") comprando tecnologia: " @ %pesquisa.id);
		%pesquisaIDproTAXO = serverCriarIdProTaxo(%pesquisa, %liderNum);
		//ajusta os valores no server:
		%eval = "%persona." @ %pesquisa.classId @ " = " @ %pesquisa.num @ ";";
		eval(%eval);
			
		%persona.user.TAXOomnis -= %pesquisa.cOMNI; 	//enviar isso pro TAXO, mais a nova situação do dado alterado
		echo("%persona.user.TAXOomnis = " @ %persona.user.TAXOomnis);
		%omnisRestantes = %persona.user.TAXOomnis;
		
		//Cria um objeto que vai guardar esta url e o num da pesquisa, para verificar falhas no registro:
		%myServerPesq = criarServerPesq();
		
		//enviar pro TAXO a nova situação do dado alterado e a quantidade de Omnis restantes no usuário
		%myServerPesq.url = "/torque/academia/comprar?idPersona=" @ %persona.TAXOid @ "&pesq=" @ %pesquisaIDproTAXO @ "&omnis=-" @ %pesquisa.cOMNI @ "&idPesqTorque=" @ %myServerPesq.num;
		$filas_handler.newFilaObj("comprar_pesquisa", %myServerPesq.url, 2, %myServerPesq, %persona);
		
		//devolve a msg pro client que tudo foi feito:
		commandToClient(%client, 'a_comprarTecnologia', %pesquisaId, %liderNum, %omnisRestantes);
		servera_registrarPesquisa(%persona, %pesquisaId, "compras");
	} else {
		//client cheater MOTHAFUCKA!!!	(pode até deixar ele preso, sem responder o popAguardando! Tentou roubar OMNIIIIIISSS!!!! MORRAAA, MISERÁÁÁÁVEEEEL!!!
		echo("**CHEATER??? Persona: " @ %persona.nome @ "; User: " @ %persona.user @ "; ERRO: não tinha omnis pra comprar, mas o client não bloqueou o pedido.");
	}
}


function servera_registrarPesquisa(%persona, %pesquisaId, %compraOuPesquisa){
	%hora = getLocalTime();
	
	%txt = "User: " @ %persona.user.nome @ "; ";
	%txt = %txt @ "Persona: " @ %persona.nome @ "; ";
	%txt = %txt @ "Pesquisa: " @ %pesquisaId @ "; ";
	%txt = %txt @ "Completa em: " @ %hora @ "; ";
	%txt = %txt @ "Jogos até agora: " @ %persona.TAXOjogos @ "; ";
	%txt = %txt @ "Vitórias agora: " @ %persona.TAXOvitorias @ "; ";
	%txt = %txt @ "Pontos agora: " @ %persona.TAXOpontos @ "; ";
	%txt = %txt @ "Visionário agora: " @ %persona.TAXOvisionario @ "; ";
	%txt = %txt @ "Arrebatador agora: " @ %persona.TAXOarrebatador @ "; ";
	%txt = %txt @ "Comerciante agora: " @ %persona.myComerciante @ "; ";
	%txt = %txt @ "Diplomata agora: " @ %persona.myDiplomata @ "; ";
	
	%file = new FileObject();
	%file.openForAppend(expandFileName("game/data/files/" @ %compraOuPesquisa @ ".txt")); //abre para adicionar texto
	//atualiza o arquivo:
	%file.WriteLine(%txt);
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM
}

//para investir um número definido de créditos:
function serverCmda_investirCreditosDef(%client, %paraInvestir){
	%persona = %client.persona;
	echo("INVESTIRCREDITOS: %persona.aca_pea_id = " @ %persona.aca_pea_id);
	%pesquisa = findPesquisaPorClassId(%persona, %persona.aca_pea_id);
	
	if(isObject(%pesquisa)){
		if(%persona.TAXOcreditos >= %paraInvestir){
			%persona.TAXOcreditos -= %paraInvestir; //já retira todos os créditos investidos
			
			//zera as variáveis:
			%mineriosInvestidos = 0;
			%petroleosInvestidos = 0;
			%uraniosInvestidos = 0;
			%creditosInvestidos = 0;
			
			while(%creditosInvestidos < %paraInvestir && %persona.aca_pea_ura < %pesquisa.cDevUra){
				%persona.aca_pea_ura++;
				%uraniosInvestidos++;
				%creditosInvestidos++;
			}
			while(%creditosInvestidos < %paraInvestir && %persona.aca_pea_pet < %pesquisa.cDevPet){
				%persona.aca_pea_pet++;
				%petroleosInvestidos++;
				%creditosInvestidos++;
			}
			while(%creditosInvestidos < %paraInvestir && %persona.aca_pea_min < %pesquisa.cDevMin){
				if((%persona.aca_pea_min + 2) > %pesquisa.cDevMin){
					%persona.aca_pea_min++;
					%mineriosInvestidos++;
				} else {
					%persona.aca_pea_min += 2;
					%mineriosInvestidos += 2;
				}
				%creditosInvestidos++;
			}
			
			%pesquisaCompleta = servera_verificarPesquisaCompleta(%pesquisa.id, %persona);
			
			if(%pesquisaCompleta){
				echo("PESQUISA COMPLETA para o usuário " @ %client.user.nome @ ", na Persona " @ %persona.nome @ " (" @ %pesquisa.id @ ")");
				%pesquisaIDproTAXO = serverCriarIdProTaxo(%pesquisa, %persona.aca_pea_ldr);
				%eval = "%persona." @ %pesquisa.classId @ " = " @ %pesquisa.num @ ";";
				eval(%eval);
							
				//Cria um objeto que vai guardar esta url e o num da pesquisa, para verificar falhas no registro:
				%myServerPesq = criarServerPesq();
				%myServerPesq.url = "/torque/academia/finalizar?idPersona=" @ %persona.TAXOid @ "&pesq=" @ %pesquisaIDproTAXO @ "&creditos=-" @ %creditosInvestidos @ "&idPesqTorque=" @ %myServerPesq.num;
				$filas_handler.newFilaObj("finalizar_pesquisa", %myServerPesq.url, 2, %myServerPesq, %persona);
				
				commandToClient(%client, 'a_atualizarPEAInvestida', %persona.aca_pea_min, %persona.aca_pea_pet, %persona.aca_pea_ura, %persona.TAXOcreditos, true);
				servera_registrarPesquisa(%persona, %pesquisa.id, "investimentos");
				
				serverZerarPEA(%persona);
			} else {
				echo("PESQUISA INVESTIDA => Persona: " @ %persona.nome @ "; User: " @ %persona.user @ "; CreditosRestantes: " @ %persona.TAXOcreditos);
				%myServerPesq = criarServerPesq();
				%myServerPesq.url = "/torque/academia/investir?idPersona=" @ %persona.TAXOid @ "&min=" @ %mineriosInvestidos @  "&pet=" @ %petroleosInvestidos @  "&ura=" @ %uraniosInvestidos @ "&creditos=-" @ %creditosInvestidos @ "&idPesqTorque=" @ %myServerPesq.num;
				$filas_handler.newFilaObj("investir", %myServerPesq.url, 2, %myServerPesq, %persona);
								
				commandToClient(%client, 'a_atualizarPEAInvestida', %persona.aca_pea_min, %persona.aca_pea_pet, %persona.aca_pea_ura, %persona.TAXOcreditos, false);
			}
		} else {
			echo("**CHEATER?? (investirDef) Persona: " @ %persona.nome @ "; User: " @ %persona.user @ "; ERRO: não tinha creditos pra investir, mas o client não bloqueou o pedido.");	
		}
	} else {
		echo("INVESTIRDEF: Não encontrei a pesquisa a ser investida!"); 	
	}
}
