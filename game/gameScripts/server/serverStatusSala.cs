// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverStatusSala.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 1 de fevereiro de 2009 22:27
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function serverCriarStatusSalaObj()
{
	$status = new ScriptObject(){};
	%status = $sala;
	%status.EmJogo = "em_jogo";
	%status.Lotada = "lotada";
	%status.Livre = "livre";
	
	return %status;
}
