// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverCriarPersona.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 18 de fevereiro de 2008 21:33
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdCriarPersona(%client, %nome, %especie){
	%user = %client.user;
	
	//verificar se este usuário jah tem persona com este nome
	%okToGo = true;
	for(%i = 0; %i < %user.myPersonas.getCount(); %i++){
		%personaExistente = %user.myPersonas.getObject(%i);
		if(%personaExistente.nome $= %nome){
			%okToGo = false;	
		}
	}
	
	if(%okToGo){
		%user.lastNameTried = %nome;
			if(%especie $= ""){
				%especie = "humano";
			}
		
		%url = "/torque/persona/criar?idUsuario=" @ %user.TAXOid @ "&nome=" @ %nome @ "&especie=" @ %especie;
		
		$filas_handler.newFilaObj("criar_persona", %url, 3, %user);
	}
}

function serverCmdApagarPersona(%client, %personaNum){
	%user = %client.user;
	%persona = %user.myPersonas.getObject(%personaNum);
			
	%url = "/torque/persona/inativar?idPersona=" @ %persona.TAXOid @ "&idUsuario=" @ %user.TAXOid;		

	$filas_handler.newFilaObj("criar_persona", %url, 3, %user);
	
	//apagar gráfico da persona no commGui e persona do usuário
	%user.myPersonas.remove(%persona);
	for(%i = 0; %i < %user.myPersonas.getCount(); %i++){
		%persona = %user.myPersonas.getObject(%i);
		if(%persona.nome $= ""){
			%user.myPersonas.remove(%persona);		
		}
	}
		
	echo("%user.myPersonas.getCount() = " @ %user.myPersonas.getCount());
	commandToClient(%user.client, 'popularComandantes', %user.myPersonas.getCount(),  %user.myPersonas.getObject(0).dados,  %user.myPersonas.getObject(1).dados,  %user.myPersonas.getObject(2).dados,  %user.myPersonas.getObject(3).dados,  %user.myPersonas.getObject(4).dados);//apaga o clientStartGui e o NetworkMenu, e chama o escolherComandanteGui; esta função está no clientStartSequence
}



//personaCriada: personaOK username taxoId
//se não conseguiu criar pq o noem jah existe: personaNOK username 0
//personaInativada: personaInativada 2143

function TaxoPersonaOk(%dados){
	echo("TaxoPersonaOk::dados = " @ %dados);
	%userName = getWord(%dados, 1);
	%personaTAXOid = getWord(%dados, 2);
	%personaEspecie = getWord(%dados, 3);
	if(%personaEspecie $= ""){
		%personaEspecie = "humano";	
	}
	
	%eval = "%user = $USER" @ %userName @ ";";
	eval(%eval);
	echo("getPersonaRecbido::username = " @ %userName);
	echo("getPersonaRecbido::user = " @ %user);
	echo("getPersonaRecbido::user.nome = " @ %user.nome);
	echo("getPersonaRecbido::persona.TAXOid = " @ %personaTAXOid);
	
	
	echo("%user.myPersonas.getCount(1) = " @ %user.myPersonas.getCount());
	if(%personaTAXOid !$= "0"){
		%nomeDaPersona = %user.lastNameTried;
		if(%personaTAXOid !$= ""){
			%estaPersona = new ScriptObject(){class = "persona";};
			
			%estaPersona.user = %user;
			%estaPersona.TAXOid = %personaTAXOid;
			%estaPersona.nome = %user.lastNameTried;
			%estaPersona.TAXOjogos = 0;
			%estaPersona.TAXOvitorias = 0;
			%estaPersona.TAXOpontos = 0;
			%estaPersona.TAXOvisionario = 0;
			%estaPersona.TAXOarrebatador = 0;
			%estaPersona.TAXOcomerciante = 0;
			%estaPersona.TAXOatacou = 0;
			%estaPersona.TAXOcreditos = 0;
			%estaPersona.especie = %personaEspecie;
			serverZerarDadosAcademia(%estaPersona); //prepara a academia da persona;
			
			echo("%user.myPersonas.getCount(2) = " @ %user.myPersonas.getCount());
			
			%personaNova = true;
			%count = %user.myPersonas.getCount();
			%simTruePersonas = new SimSet();
			for(%i = 0; %i < %count; %i++){
				echo("%user.myPersonas.getCount(x) = " @ %user.myPersonas.getCount());
				%tempPersona = %user.myPersonas.getObject(%i);
				if(%estaPersona.nome $= %tempPersona.nome){
					%personaNova = false;
				}
				if(%tempPersona.nome $= ""){
					%tempPersona.decoy = true;
				} else {
					%simTruePersonas.add(%tempPersona);
				}
				echo("%user.myPersonas.getCount(xx) = " @ %user.myPersonas.getCount());
			}
			
			%user.myPersonas = %simTruePersonas;
			echo("Mantendo apenas personas verdadeiras; NEW COUNT = " @ %user.myPersonas.getCount());
			
			if(%personaNova){
				echo("%user.myPersonas.getCount(3) = " @ %user.myPersonas.getCount());
				%user.myPersonas.add(%estaPersona);
				echo("%user.myPersonas.getCount(4) = " @ %user.myPersonas.getCount());
							
				for(%i = 0; %i < %user.myPersonas.getCount(); %i++){
					echo("%user.myPersonas.getCount(y) = " @ %user.myPersonas.getCount());
					%thisPersona = %user.myPersonas.getObject(%i);
					%thisPersona.setPatente();
					%thisPersona.setPorcentagens();
					%thisPersona.dados = %thisPersona.nome SPC %thisPersona.TAXOvitorias SPC %thisPersona.TAXOpontos SPC %thisPersona.TAXOvisionario SPC %thisPersona.TAXOarrebatador SPC %thisPersona.myComerciante SPC %thisPersona.myDiplomata SPC %thisPersona.TAXOcreditos SPC %thisPersona.patente.nome SPC %user.taxoOmnis SPC %thisPersona.especie SPC %thisPersona.pk_vitorias SPC %thisPersona.pk_fichas SPC %thisPersona.pk_power_plays;	
					echo("%thisPersona.dados = " @ %thisPersona.dados);
					echo("%estaPersona.dados = " @ %estaPersona.dados);
					echo("%user.myPersonas.getCount(yy) = " @ %user.myPersonas.getCount());
				}
				commandToClient(%user.client, 'popularComandantes', %user.myPersonas.getCount(),  %user.myPersonas.getObject(0).dados,  %user.myPersonas.getObject(1).dados,  %user.myPersonas.getObject(2).dados,  %user.myPersonas.getObject(3).dados,  %user.myPersonas.getObject(4).dados);//apaga o clientStartGui e o NetworkMenu, e chama o escolherComandanteGui; esta função está no clientStartSequence
				echo("commandToClient(%user.client, 'popularComandantes', " @ %user.myPersonas.getCount() @ ", " @ %user.myPersonas.getObject(0).dados @ ", " @  %user.myPersonas.getObject(1).dados @ ", " @ %user.myPersonas.getObject(2).dados @ ", " @ %user.myPersonas.getObject(3).dados @ ", " @ %user.myPersonas.getObject(4).dados @ ")");
			
				
				echo("%user.myPersonas.getCount(5) = " @ %user.myPersonas.getCount());
			} else {
				echo("getPersonaRecebido DUPLICADO! Segundo comando ignorado.");	
			}
		} else {
			echo("ATENÇÃO: TaxoId inexistente (persona): " @ %nomeDaPersona);	
		}
	} else {
		echo("ATENÇÃO: TaxoId inexistente (persona): " @ %nomeDaPersona);	
	}	
}

function TaxoPersonaNOK(%dados){
	echo("TaxoPersonaNOk::dados = " @ %dados);
	%userName = getWord(%dados, 1);
	%personaTAXOid = getWord(%dados, 2);
	
	%eval = "%user = $USER" @ %userName @ ";";
	eval(%eval);
	echo("getPersonaRecbido::username = " @ %userName);
	echo("getPersonaRecbido::user = " @ %user);
	echo("getPersonaRecbido::user.nome = " @ %user.nome);
	echo("getPersonaRecbido::persona.TAXOid = " @ %personaTAXOid);
	commandToClient(%user.client, 'codinomeJahExiste'); //retornar clientMsg "este codinome já existe"
}

function TaxoPersonaInativada(%dados){
	echo("TaxoPersonaInativada::dados = " @ %dados);
	%userName = getWord(%dados, 1);
	%personaTAXOid = getWord(%dados, 2);
	
	%eval = "%user = $USER" @ %userName @ ";";
	eval(%eval);
	echo("getPersonaRecbido::username = " @ %userName);
	echo("getPersonaRecbido::user = " @ %user);
	echo("getPersonaRecbido::user.nome = " @ %user.nome);
	echo("getPersonaRecbido::persona.TAXOid = " @ %personaTAXOid);
	//este é o ok do taxo
}


function serverZerarDadosAcademia(%persona){
	if(%persona.especie $= "humano"){
		//soldados:
		%persona.aca_s_d_min = 1;
		%persona.aca_s_d_max = 6;
		%persona.aca_s_a_min = 1;
		%persona.aca_s_a_max = 6;
		//tanques:
		%persona.aca_t_d_min = 1;
		%persona.aca_t_d_max = 12;
		%persona.aca_t_a_min = 1;
		%persona.aca_t_a_max = 12;
		//navios:
		%persona.aca_n_d_min = 1;
		%persona.aca_n_d_max = 12;
		%persona.aca_n_a_min = 1;
		%persona.aca_n_a_max = 12;
		//líder1:
		%persona.aca_ldr_1_d_min = 1;
		%persona.aca_ldr_1_d_max = 12;
		%persona.aca_ldr_1_a_min = 1;
		%persona.aca_ldr_1_a_max = 12;
		%persona.aca_ldr_1_h1 = 0;
		%persona.aca_ldr_1_h2 = 0;
		%persona.aca_ldr_1_h3 = 0;
		%persona.aca_ldr_1_h4 = 0;
		//líder2:
		%persona.aca_ldr_2_d_min = 1;
		%persona.aca_ldr_2_d_max = 12;
		%persona.aca_ldr_2_a_min = 1;
		%persona.aca_ldr_2_a_max = 12;
		%persona.aca_ldr_2_h1 = 0;
		%persona.aca_ldr_2_h2 = 0;
		%persona.aca_ldr_2_h3 = 0;
		%persona.aca_ldr_2_h4 = 0;
		//líder3:
		%persona.aca_ldr_3_d_min = 1;
		%persona.aca_ldr_3_d_max = 12;
		%persona.aca_ldr_3_a_min = 1;
		%persona.aca_ldr_3_a_max = 12;
		%persona.aca_ldr_3_h1 = 0;
		%persona.aca_ldr_3_h2 = 0;
		%persona.aca_ldr_3_h3 = 0;
		%persona.aca_ldr_3_h4 = 0;
	} else if(%persona.especie $= "gulok"){
		//vermes:
		%persona.aca_s_d_min = 1;
		%persona.aca_s_d_max = 8;
		%persona.aca_s_a_min = 1;
		%persona.aca_s_a_max = 8;
		//Rainhas:
		%persona.aca_t_d_min = 1;
		%persona.aca_t_d_max = 15;
		%persona.aca_t_a_min = 1;
		%persona.aca_t_a_max = 15;
		//Cefaloks:
		%persona.aca_n_d_min = 1;
		%persona.aca_n_d_max = 14;
		%persona.aca_n_a_min = 1;
		%persona.aca_n_a_max = 14;
		//Zangão1:
		%persona.aca_ldr_1_d_min = 1;
		%persona.aca_ldr_1_d_max = 14;
		%persona.aca_ldr_1_a_min = 1;
		%persona.aca_ldr_1_a_max = 14;
		%persona.aca_ldr_1_h1 = 0;
		%persona.aca_ldr_1_h2 = 0;
		%persona.aca_ldr_1_h3 = 0;
		%persona.aca_ldr_1_h4 = 0;
		//Zangão2:
		%persona.aca_ldr_2_d_min = 1;
		%persona.aca_ldr_2_d_max = 14;
		%persona.aca_ldr_2_a_min = 1;
		%persona.aca_ldr_2_a_max = 14;
		%persona.aca_ldr_2_h1 = 0;
		%persona.aca_ldr_2_h2 = 0;
		%persona.aca_ldr_2_h3 = 0;
		%persona.aca_ldr_2_h4 = 0;
		//Dragnal2:
		%persona.aca_ldr_3_d_min = 15;
		%persona.aca_ldr_3_d_max = 30;
		%persona.aca_ldr_3_a_min = 15;
		%persona.aca_ldr_3_a_max = 30;
		%persona.aca_ldr_3_h1 = 0;
		%persona.aca_ldr_3_h2 = 0;
		%persona.aca_ldr_3_h3 = 0;
		%persona.aca_ldr_3_h4 = 0;
	}
	//visionário
	%persona.aca_v_1 = 0;
	%persona.aca_v_2 = 0;
	%persona.aca_v_3 = 0;
	%persona.aca_v_4 = 0;
	%persona.aca_v_5 = 0;
	%persona.aca_v_6 = 0;
	//arebatador:
	%persona.aca_a_1 = 0;
	%persona.aca_a_2 = 0;
	//comerciante:
	%persona.aca_c_1 = 0;
	//diplomata:
	%persona.aca_d_1 = 0;
	//intel:
	%persona.aca_i_1 = 0;
	%persona.aca_i_2 = 0;
	%persona.aca_i_3 = 0;
	//Pesquisa Em Andamento:
	%persona.aca_pea_id = 0;
	%persona.aca_pea_min = 0;
	%persona.aca_pea_pet = 0;
	%persona.aca_pea_ura = 0;
	%persona.aca_pea_ldr = 0;
		
	//marca que a persona tem dados de academia:
	%persona.aca_tenhoDados = true;
	
	//pesquisas Avançadas:
	%persona.aca_av_1 = 0;
	%persona.aca_av_2 = 0;
	%persona.aca_av_3 = 0;
	%persona.aca_av_4 = 0;
	
	%persona.aca_pln_1 = 0;
	
	%persona.aca_art_1 = 0; //Geo-Canhão
	%persona.aca_art_2 = 0; //Nexus Temporal
	
	%persona.pk_fichas = 150;
	%persona.pk_vitorias = 0;
}


function serverCriarPersonaAiGulok(%jogo){
	%jogo.aiPlayer.persona = new ScriptObject(){};
	%persona = %jogo.aiPlayer.persona;
	%persona.especie = "gulok";
	%persona.nome = "Gulok";
	//vermes:
	%persona.aca_s_d_min = 3;
	%persona.aca_s_d_max = 10;
	%persona.aca_s_a_min = 3;
	%persona.aca_s_a_max = 10;
	//Rainhas:
	%persona.aca_t_d_min = 5;
	%persona.aca_t_d_max = 25;
	%persona.aca_t_a_min = 5;
	%persona.aca_t_a_max = 25;
	//Cefaloks:
	%persona.aca_n_d_min = 5;
	%persona.aca_n_d_max = 25;
	%persona.aca_n_a_min = 5;
	%persona.aca_n_a_max = 25;
	//Zangão1:
	%persona.aca_ldr_1_d_min = 10;
	%persona.aca_ldr_1_d_max = 25;
	%persona.aca_ldr_1_a_min = 14;
	%persona.aca_ldr_1_a_max = 30;
	%persona.aca_ldr_1_h1 = 2;
	%persona.aca_ldr_1_h2 = 2;
	%persona.aca_ldr_1_h3 = 2;
	%persona.aca_ldr_1_h4 = 2;
	//Zangão2:
	%persona.aca_ldr_2_d_min = 14;
	%persona.aca_ldr_2_d_max = 30;
	%persona.aca_ldr_2_a_min = 10;
	%persona.aca_ldr_2_a_max = 25;
	%persona.aca_ldr_2_h1 = 2;
	%persona.aca_ldr_2_h2 = 2;
	%persona.aca_ldr_2_h3 = 2;
	%persona.aca_ldr_2_h4 = 2;
	//Dragnal2:
	%persona.aca_ldr_3_d_min = 15;
	%persona.aca_ldr_3_d_max = 30;
	%persona.aca_ldr_3_a_min = 15;
	%persona.aca_ldr_3_a_max = 30;
	%persona.aca_ldr_3_h1 = 3;
	%persona.aca_ldr_3_h2 = 3;
	%persona.aca_ldr_3_h3 = 1;
	%persona.aca_ldr_3_h4 = 1;
	
	//visionário
	%persona.aca_v_1 = 3;
	%persona.aca_v_2 = 3;
	%persona.aca_v_3 = 3;
	%persona.aca_v_4 = 3;
	%persona.aca_v_5 = 3;
	%persona.aca_v_6 = 3;
	//arebatador:
	%persona.aca_a_1 = 3;
	%persona.aca_a_2 = 3;
	//comerciante:
	%persona.aca_c_1 = 0;
	//diplomata:
	%persona.aca_d_1 = 0;
	//intel:
	%persona.aca_i_1 = 0;
	%persona.aca_i_2 = 0;
	%persona.aca_i_3 = 3;
	//Pesquisa Em Andamento:
	%persona.aca_pea_id = 0;
	%persona.aca_pea_min = 0;
	%persona.aca_pea_pet = 0;
	%persona.aca_pea_ura = 0;
	%persona.aca_pea_ldr = 0;
		
	//marca que a persona tem dados de academia:
	%persona.aca_tenhoDados = true;
	
	//pesquisas Avançadas:
	%persona.aca_av_1 = 3;
	%persona.aca_av_2 = 3;
	%persona.aca_av_3 = 3;
	%persona.aca_av_4 = 3;
	
	%persona.myDiplomata = 80;
}
