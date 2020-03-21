// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverGmBot.cs
// Copyright          :  
// Author             :  admin
// Created on         :  sexta-feira, 30 de janeiro de 2009 4:13
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverGmBot(%jogo, %num){
	switch(%num){
		case 1: serverCTCupdateChatText(%jogo, "[GM-BOT]>>", "Lembre-se: partidas muito demoradas valem menos Créditos.");
	}
}