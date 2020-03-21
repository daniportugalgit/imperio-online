// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverPersonasOnline.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 14 de abril de 2009 14:36
//
// Editor             :  Codeweaver v. 1.2.3341.40715
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdGetListaPersonasOnline(%client)
{
	%totalDePersonas_sessao = $serverSimPersonas.getCount();
	for (%i = 0; %i < %totalDePersonas_sessao; %i++)
	{
		%persona = $serverSimPersonas.getObject(%i);
		if(!%persona.offline)
		{
			%numDePersonasOnline++;
			%listaString = %listaString @ %persona.nome @ " ";
			serverSetPersonaOnde(%persona);
			%ondeString = %ondeString @ %persona.onde @ " ";	
		}
	}
	
	commandToClient(%client, 'PrintPersonasOnline', %listaString, %ondeString, %numDePersonasOnline);
}

function serverSetPersonaOnde(%persona)
{
	if($personasNoAtrio.isMember(%persona))
	{
		%persona.onde = "Chat";	
		return;
	}
	
	if(isObject(%persona.sala))
	{
		for(%j = 0; %j < %persona.sala.simPersonas.getCount(); %j++){
			%tempPersona = %persona.sala.simPersonas.getObject(%j);
			if(%tempPersona == %persona)
			{
				%persona.onde = %persona.sala.num;	
				return;
			}
		}
	}
	
	if(%persona.fazendoTutorial)
	{
		%persona.onde = "Tutorial";		
		return;
	}
	
	%persona.onde = "Academia";		
}

function serverCmdEntreiNoTutorial(%client)
{
	%client.persona.fazendoTutorial = true;
}

function serverCmdSaiDoTutorial(%client)
{
	%client.persona.fazendoTutorial = false;
}