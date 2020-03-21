// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverMetamorfose.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 14 de dezembro de 2008 5:44
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverCmdMetamorfose(%client, %areaNome, %pos){
	%jogo = %client.player.jogo;
	%eval = "%areaNoJogo = %jogo." @ %areaNome @ ";";
	eval(%eval);
	%eval = "%zangao = %areaNoJogo." @ %pos @ "Quem;";
	eval(%eval);
	
	serverCmdEvoluirEmRainha(%client, %areaNome, %pos, false, %client.persona.aca_ldr_2_h3);
}