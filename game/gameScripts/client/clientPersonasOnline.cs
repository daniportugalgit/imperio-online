// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientPersonasOnline.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 14 de abril de 2009 14:26
//
// Editor             :  Codeweaver v. 1.2.3341.40715
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function clientAskPersonasOnline()
{
	clientInitPersonasOnlineVectorText();
	clientPushAguardeMsgBox();
	commandtoServer('getListaPersonasOnline');	
}

function clientInitPersonasOnlineVectorText()
{
	if(isObject(personasOnlineVectorText)){
		atrioPersonasOnlineMessageText.detach();
		personasOnlineVectorText.delete();
	}
		
	new MessageVector(personasOnlineVectorText){};
	atrioPersonasOnlineMessageText.attach(personasOnlineVectorText);
}

function clientCmdPrintPersonasOnline(%listaString, %ondeString, %numDePersonasOnline)
{
	for(%i = 0; %i < getWordCount(%listaString); %i++)
	{
		%nome = getWord(%listaString, %i);
		%onde = getWord(%ondeString, %i);
		%onde = clientConverterPersonaOnde(%onde);
			
		personasOnlineVectorText.pushBackLine(" " @ %nome @ "  >>  " @ %onde, 0);
	}
	
	atrioPersonasOnlineNumDePersonas_txt.text = "Total de Personas Online: " @ %numDePersonasOnline;
	atrioPersonasOnlineFundo.setVisible(true);
	clientPopAguardeMsgBox();
}

function clientConverterPersonaOnde(%onde)
{
	if(%onde $= "chat")
		return "Chat Global";
	
	if(%onde $= "Academia")
		return "Academia Imperial";
	
	if(%onde !$= "Tutorial")
		return "Sala" SPC %onde;
		
	return "Tutorial";
}	

function clientPopPersonasOnlineGui()
{
	atrioPersonasOnlineFundo.setVisible(false);
}