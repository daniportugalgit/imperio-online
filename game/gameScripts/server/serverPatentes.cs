// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverPatentes.cs
// Copyright          :  
// Author             :  admin
// Created on         :  segunda-feira, 9 de fevereiro de 2009 13:34
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function serverInitPatentes()
{
	$patenteCount_humano = 0;
	$patenteCount_gulok = 0;
	$simPatentes_gulok = new SimSet();
	$simPatentes_humano = new SimSet();
	serverCriarPatentesHumano();
	serverCriarPatentesGulok();
}

function serverCriarPatente(%especie, %nome, %impBonus, %movBonus, %minVit, %maxVit)
{
	%patente = new ScriptObject(){};	
	%patente.especie = %especie;
	%patente.nome = %nome;
	%patente.impBonus = %impbonus;
	%patente.movBonus = %movBonus;
	%patente.minVit = %minVit;
	%patente.maxVit = %maxVit;
	%patente.num = serverGetPatenteCount(%especie);
	serverAddPatenteCount(%especie);
	serverAddPatenteToSim(%patente);
}

function serverAddPatenteCount(%especie)
{
	switch$(%especie)
	{
		case "humano": serverAddPatenteCountHumano();
		case "gulok": serverAddPatenteCountGulok();
	}
}	

function serverAddPatenteCountHumano()
{
	$patenteCount_humano++;
}

function serverAddPatenteCountGulok()
{
	$patenteCount_gulok++;
}

function serverGetPatenteCount(%especie)
{
	%eval = "%patenteCount = $patenteCount_" @ %especie @ ";";
	eval(%eval);
	return %patenteCount;
}

function serverAddPatenteToSim(%patente)
{
	%eval = "%simPatentes = $simPatentes_" @ %patente.especie @ ";";
	eval(%eval);
	%simPatentes.add(%patente);
}

function serverCriarPatentesGulok()
{
	serverCriarPatente("gulok", "Larva", 0, 0, 0, 0);
	serverCriarPatente("gulok", "Verme", 0, 0, 1, 9);
	serverCriarPatente("gulok", "Zangão", 1, 0, 10, 24);
	serverCriarPatente("gulok", "Infame", 2, 0, 25, 49);
	serverCriarPatente("gulok", "Bestial", 3, 0, 50, 99);
	serverCriarPatente("gulok", "Terrível", 4, 0, 100, 149);
	serverCriarPatente("gulok", "Medonho", 5, 0, 150, 199);
	serverCriarPatente("gulok", "Monstruoso", 6, 0, 200, 299);
	serverCriarPatente("gulok", "Revoltante", 8, 0, 300, 399);
	serverCriarPatente("gulok", "Pavoroso", 10, 0, 400, 499);
	serverCriarPatente("gulok", "Grotesco", 15, 0, 500, 649);
	serverCriarPatente("gulok", "Demoníaco", 20, 0, 650, 799);
	serverCriarPatente("gulok", "Aberração", 25, 0, 800, 999);
	serverCriarPatente("gulok", "Ancião", 25, 1, 1000, 99999);
}

function serverCriarPatentesHumano()
{
	serverCriarPatente("humano", "Recruta", 0, 0, 0, 0);
	serverCriarPatente("humano", "Cadete", 0, 0, 1, 9);
	serverCriarPatente("humano", "Aspirante", 1, 0, 10, 24);
	serverCriarPatente("humano", "Sargento", 2, 0, 25, 49);
	serverCriarPatente("humano", "Tenente", 3, 0, 50, 99);
	serverCriarPatente("humano", "Capitão", 4, 0, 100, 149);
	serverCriarPatente("humano", "Major", 5, 0, 150, 199);
	serverCriarPatente("humano", "Coronel", 6, 0, 200, 299);
	serverCriarPatente("humano", "General", 8, 0, 300, 399);
	serverCriarPatente("humano", "Marechal", 10, 0, 400, 499);
	serverCriarPatente("humano", "Imperador", 15, 0, 500, 649);
	serverCriarPatente("humano", "Imortal", 20, 0, 650, 799);
	serverCriarPatente("humano", "Lendário", 25, 0, 800, 999);
	serverCriarPatente("humano", "Titã", 25, 1, 1000, 99999);
}




serverInitPatentes();

