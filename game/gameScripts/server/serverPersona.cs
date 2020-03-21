// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverPersona.cs
// Copyright          :  
// Author             :  admin
// Created on         :  segunda-feira, 2 de fevereiro de 2009 1:15
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function persona::getStats(%this)
{
	return %this.nome SPC %this.TAXOvitorias SPC %this.TAXOpontos SPC %this.TAXOvisionario SPC %this.TAXOarrebatador SPC %this.myComerciante SPC %this.myDiplomata SPC %this.patente.nome SPC %this.pronto SPC %this.especie SPC %this.mediaVit SPC %this.pk_fichas;
}

function persona::getStatsPlus(%this)
{
	return %this.getStats() SPC %this.pronto SPC %this.especie SPC %this.mediaVit;
}

function persona::removerDaSala(%this)
{
	%this.sala.removerPersona(%this);
}

function persona::temUngart(%this)
{
	if(%this.especie $= "humano" && %this.aca_v_6 >= 1)
		return true;
	
	if(%this.especie $= "gulok")
		return true;
	
	return false;
}

function persona::temTeluria(%this)
{
	return %this.aca_pln_1 >= 1;
}

function persona::ctcPopularAtrio(%this, %salasString, %vindoDeOnde)
{
	%personaDados = %this.nome SPC %this.TAXOvitorias SPC %this.TAXOpontos SPC %this.TAXOvisionario SPC %this.TAXOarrebatador SPC %this.myComerciante SPC %this.myDiplomata SPC %this.TAXOcreditos SPC %this.patente.nome SPC %this.especie SPC %this.pk_vitorias SPC %this.pk_power_plays;
	%numDePersonasNoChat = $personasNoAtrio.getCount();
	commandToClient(%this.client, 'popularAtrio', %salasString, %vindoDeOnde, %personaDados, %numDePersonasNoChat); 
}

function persona::rebuildSalaComDados(%this)
{
	if(%this.sala.jogoTAXOid $= "")
	{
		commandToClient(%this.client, 'SalaSendoConfigurada');
		return;
	}
	
	%this.inGame = false;
	
	for(%i = 0; %i < 6; %i++)
	{
		%statsPlus[%i] = %this.autoGetPersonaStatsPlus(%i);
	}
	
	%playersAtivos = %this.sala.simPersonas.getCount();	
	commandToClient(%this.client, 'rebuildSalaComDados', %playersAtivos,
		%statsPlus[0],
		%statsPlus[1],
		%statsPlus[2],
		%statsPlus[3],
		%statsPlus[4],
		%statsPlus[5]);
}

function persona::autoGetPersonaStatsPlus(%this, %num)
{
	if(%num > %this.sala.simPersonas.getCount() - 1)
		return 0;
		
	%statsPlus = %this.sala.getPersonaStatsPlus(%num);
	return %statsPlus;
}

function persona::getPatente(%this)
{
	%simPatentes = %this.getSimPatentes();
	for(%i = 0; %i < %simPatentes.getCount(); %i++)
	{
		%patente = %simPatentes.getObject(%i);
		if(%this.TAXOvitorias >= %patente.minVit && %this.TAXOvitorias <= %patente.maxVit)
		{
			return %patente;	
		}
	}
}

function persona::getSimPatentes(%this)
{
	%eval = "%simPatentes = $simPatentes_" @ %this.especie @ ";";
	eval(%eval);
	return %simPatentes;	
}

function persona::setPatente(%this)
{
	%patente = %this.getPatente();
	%this.patente = %patente;
}

function persona::setPorcentagens(%this)
{
	%this.myComerciante = mFloor((%this.TAXOcomerciante / %this.TAXOjogos) * 100);
	%this.myDiplomata = mFloor(((%this.TAXOjogos - %this.TAXOAtacou) / %this.TAXOjogos) * 100);
}

function persona::setAcadBKP(%this)
{
	if(isObject(%this.acadBKP))
		return;
	
	%this.acadBKP = new ScriptObject(){};
	
	%this.acadBKP.aca_s_d_min = %this.aca_s_d_min;
	%this.acadBKP.aca_s_d_max = %this.aca_s_d_max;
	%this.acadBKP.aca_s_a_min = %this.aca_s_a_min;
	%this.acadBKP.aca_s_a_max = %this.aca_s_a_max;
	//tanques:
	%this.acadBKP.aca_t_d_min = %this.aca_t_d_min;
	%this.acadBKP.aca_t_d_max = %this.aca_t_d_max;
	%this.acadBKP.aca_t_a_min = %this.aca_t_a_min;
	%this.acadBKP.aca_t_a_max = %this.aca_t_a_max;
	//navios:
	%this.acadBKP.aca_n_d_min = %this.aca_n_d_min;
	%this.acadBKP.aca_n_d_max = %this.aca_n_d_max;
	%this.acadBKP.aca_n_a_min = %this.aca_n_a_min;
	%this.acadBKP.aca_n_a_max = %this.aca_n_a_max;
	//líder1:
	%this.acadBKP.aca_ldr_1_d_min = %this.aca_ldr_1_d_min;
	%this.acadBKP.aca_ldr_1_d_max = %this.aca_ldr_1_d_max;
	%this.acadBKP.aca_ldr_1_a_min = %this.aca_ldr_1_a_min;
	%this.acadBKP.aca_ldr_1_a_max = %this.aca_ldr_1_a_max;
	%this.acadBKP.aca_ldr_1_h1 = %this.aca_ldr_1_h1;
	%this.acadBKP.aca_ldr_1_h2 = %this.aca_ldr_1_h2;
	%this.acadBKP.aca_ldr_1_h3 = %this.aca_ldr_1_h3;
	%this.acadBKP.aca_ldr_1_h4 = %this.aca_ldr_1_h4;
	//líder2:
	%this.acadBKP.aca_ldr_2_d_min = %this.aca_ldr_2_d_min;
	%this.acadBKP.aca_ldr_2_d_max = %this.aca_ldr_2_d_max;
	%this.acadBKP.aca_ldr_2_a_min = %this.aca_ldr_2_a_min;
	%this.acadBKP.aca_ldr_2_a_max = %this.aca_ldr_2_a_max;
	%this.acadBKP.aca_ldr_2_h1 = %this.aca_ldr_2_h1;
	%this.acadBKP.aca_ldr_2_h2 = %this.aca_ldr_2_h2;
	%this.acadBKP.aca_ldr_2_h3 = %this.aca_ldr_2_h3;
	%this.acadBKP.aca_ldr_2_h4 = %this.aca_ldr_2_h4;
	//líder3:
	%this.acadBKP.aca_ldr_3_d_min = %this.aca_ldr_3_d_min;
	%this.acadBKP.aca_ldr_3_d_max = %this.aca_ldr_3_d_max;
	%this.acadBKP.aca_ldr_3_a_min = %this.aca_ldr_3_a_min;
	%this.acadBKP.aca_ldr_3_a_max = %this.aca_ldr_3_a_max;
	%this.acadBKP.aca_ldr_3_h1 = %this.aca_ldr_3_h1;
	%this.acadBKP.aca_ldr_3_h2 = %this.aca_ldr_3_h2;
	%this.acadBKP.aca_ldr_3_h3 = %this.aca_ldr_3_h3;
	%this.acadBKP.aca_ldr_3_h4 = %this.aca_ldr_3_h4;
	
	//visionário
	%this.acadBKP.aca_v_1 = %this.aca_v_1;
	%this.acadBKP.aca_v_2 = %this.aca_v_2;
	%this.acadBKP.aca_v_3 = %this.aca_v_3;
	%this.acadBKP.aca_v_4 = %this.aca_v_4;
	%this.acadBKP.aca_v_5 = %this.aca_v_5;
	%this.acadBKP.aca_v_6 = %this.aca_v_6;
	//arebatador:
	%this.acadBKP.aca_a_1 = %this.aca_a_1;
	%this.acadBKP.aca_a_2 = %this.aca_a_2;
	//comerciante:
	%this.acadBKP.aca_c_1 = %this.aca_c_1;
	//diplomata:
	%this.acadBKP.aca_d_1 = %this.aca_d_1;
	//intel:
	%this.acadBKP.aca_i_1 = %this.aca_i_1;
	%this.acadBKP.aca_i_2 = %this.aca_i_2;
	%this.acadBKP.aca_i_3 = %this.aca_i_3;
	
	%this.acadBKP.aca_pea_id = %this.aca_pea_id;
	%this.acadBKP.aca_pea_min = %this.aca_pea_min;
	%this.acadBKP.aca_pea_pet = %this.aca_pea_pet;
	%this.acadBKP.aca_pea_ura = %this.aca_pea_ura;
	%this.acadBKP.aca_pea_ldr = %this.aca_pea_ldr;
		
	//pesquisas Avançadas:
	%this.acadBKP.aca_av_1 = %this.aca_av_1;
	%this.acadBKP.aca_av_2 = %this.aca_av_2;
	%this.acadBKP.aca_av_3 = %this.aca_av_3;
	%this.acadBKP.aca_av_4 = %this.aca_av_4;
	
	%this.acadBKP.aca_pln_1 = %this.aca_pln_1;
	
	%this.acadBKP.aca_art_1 = %this.aca_art_1; 
	%this.acadBKP.aca_art_2 = %this.aca_art_2;
	
	//marca que a persona tem dados de academia no backup:
	%this.acadBKP.aca_tenhoDados = true;
	
	echo("BKP de academia criado com sucesso para a Persona " @ %this.nome);
}

function persona::resetAcadFromBKP(%this)
{
	if(!isObject(%this.acadBKP))
	{	
		echo("*** ERRO: BKP da academia não está disponível para ser carregado!");
		return;
	}
			
	%this.aca_s_d_min = %this.acadBKP.aca_s_d_min;
	%this.aca_s_d_max = %this.acadBKP.aca_s_d_max;
	%this.aca_s_a_min = %this.acadBKP.aca_s_a_min;
	%this.aca_s_a_max = %this.acadBKP.aca_s_a_max;
	//tanques:
	%this.aca_t_d_min = %this.acadBKP.aca_t_d_min;
	%this.aca_t_d_max = %this.acadBKP.aca_t_d_max;
	%this.aca_t_a_min = %this.acadBKP.aca_t_a_min;
	%this.aca_t_a_max = %this.acadBKP.aca_t_a_max;
	//navios:
	%this.aca_n_d_min = %this.acadBKP.aca_n_d_min;
	%this.aca_n_d_max = %this.acadBKP.aca_n_d_max;
	%this.aca_n_a_min = %this.acadBKP.aca_n_a_min;
	%this.aca_n_a_max = %this.acadBKP.aca_n_a_max;
	//líder1:
	%this.aca_ldr_1_d_min = %this.acadBKP.aca_ldr_1_d_min;
	%this.aca_ldr_1_d_max = %this.acadBKP.aca_ldr_1_d_max;
	%this.aca_ldr_1_a_min = %this.acadBKP.aca_ldr_1_a_min;
	%this.aca_ldr_1_a_max = %this.acadBKP.aca_ldr_1_a_max;
	%this.aca_ldr_1_h1 = %this.acadBKP.aca_ldr_1_h1;
	%this.aca_ldr_1_h2 = %this.acadBKP.aca_ldr_1_h2;
	%this.aca_ldr_1_h3 = %this.acadBKP.aca_ldr_1_h3;
	%this.aca_ldr_1_h4 = %this.acadBKP.aca_ldr_1_h4;
	//líder2:
	%this.aca_ldr_2_d_min = %this.acadBKP.aca_ldr_2_d_min;
	%this.aca_ldr_2_d_max = %this.acadBKP.aca_ldr_2_d_max;
	%this.aca_ldr_2_a_min = %this.acadBKP.aca_ldr_2_a_min;
	%this.aca_ldr_2_a_max = %this.acadBKP.aca_ldr_2_a_max;
	%this.aca_ldr_2_h1 = %this.acadBKP.aca_ldr_2_h1;
	%this.aca_ldr_2_h2 = %this.acadBKP.aca_ldr_2_h2;
	%this.aca_ldr_2_h3 = %this.acadBKP.aca_ldr_2_h3;
	%this.aca_ldr_2_h4 = %this.acadBKP.aca_ldr_2_h4;
	//líder3:
	%this.aca_ldr_3_d_min = %this.acadBKP.aca_ldr_3_d_min;
	%this.aca_ldr_3_d_max = %this.acadBKP.aca_ldr_3_d_max;
	%this.aca_ldr_3_a_min = %this.acadBKP.aca_ldr_3_a_min;
	%this.aca_ldr_3_a_max = %this.acadBKP.aca_ldr_3_a_max;
	%this.aca_ldr_3_h1 = %this.acadBKP.aca_ldr_3_h1;
	%this.aca_ldr_3_h2 = %this.acadBKP.aca_ldr_3_h2;
	%this.aca_ldr_3_h3 = %this.acadBKP.aca_ldr_3_h3;
	%this.aca_ldr_3_h4 = %this.acadBKP.aca_ldr_3_h4;
	
	//visionário
	%this.aca_v_1 = %this.acadBKP.aca_v_1;
	%this.aca_v_2 = %this.acadBKP.aca_v_2;
	%this.aca_v_3 = %this.acadBKP.aca_v_3;
	%this.aca_v_4 = %this.acadBKP.aca_v_4;
	%this.aca_v_5 = %this.acadBKP.aca_v_5;
	%this.aca_v_6 = %this.acadBKP.aca_v_6;
	//arebatador:
	%this.aca_a_1 = %this.acadBKP.aca_a_1;
	%this.aca_a_2 = %this.acadBKP.aca_a_2;
	//comerciante:
	%this.aca_c_1 = %this.acadBKP.aca_c_1;
	//diplomata:
	%this.aca_d_1 = %this.acadBKP.aca_d_1;
	//intel:
	%this.aca_i_1 = %this.acadBKP.aca_i_1;
	%this.aca_i_2 = %this.acadBKP.aca_i_2;
	%this.aca_i_3 = %this.acadBKP.aca_i_3;
	
	%this.aca_pea_id = %this.acadBKP.aca_pea_id;
	%this.aca_pea_min = %this.acadBKP.aca_pea_min;
	%this.aca_pea_pet = %this.acadBKP.aca_pea_pet;
	%this.aca_pea_ura = %this.acadBKP.aca_pea_ura;
	%this.aca_pea_ldr = %this.acadBKP.aca_pea_ldr;
		
	//pesquisas Avançadas:
	%this.aca_av_1 = %this.acadBKP.aca_av_1;
	%this.aca_av_2 = %this.acadBKP.aca_av_2;
	%this.aca_av_3 = %this.acadBKP.aca_av_3;
	%this.aca_av_4 = %this.acadBKP.aca_av_4;
	
	%this.aca_pln_1 = %this.acadBKP.aca_pln_1;
	
	%this.aca_art_1 = %this.acadBKP.aca_art_1; 
	%this.aca_art_2 = %this.acadBKP.aca_art_2;
	
	//marca que a persona tem dados de academia:
	%this.aca_tenhoDados = true;
	
	%this.CTCresetAcadFromBKP();
	%this.delAcadBKP(); //deleta o BKP pra persona poder usar academia e depois o sistema criar um novo BKP atualizado.
	echo("BKP foi usado com sucesso para resetar os dados de academia da Persona " @ %this.nome);
}

function persona::delAcadBKP(%this)
{
	if(!isObject(%this.acadBKP))
		return;
		
	%this.acadBKP.delete();	
	echo("BKP de academia deletado para Persona " @ %this.nome);
}

function persona::CTCresetAcadFromBKP(%this)
{
	commandToClient(%this.client, 'resetAcadFromBKP');
}

function persona::getMySalaSimPos(%this)
{
	for(%i = 0; %i < %this.sala.simPersonas.getCount(); %i++)
	{
		if(%this == %this.sala.simPersonas.getObject(%i))
			return %i;
	}
}

function persona::getArrebatador(%this)
{
	if(%this.player.obj1Completo && %this.player.obj2Completo)	
		return true;
		
	return false;
}