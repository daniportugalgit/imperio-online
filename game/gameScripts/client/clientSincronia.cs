// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientSincronia.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 5 de maio de 2009 17:04
//
// Editor             :  Codeweaver v. 1.2.3341.40715
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskConfirmarAtoFeito(%playerId)
{
	if($estouNoTutorial)
	{
		clientPopServerComDot();
		return;
	}
	
	commandToServer('confirmarAtoFeito', %playerId);	
}