// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientPilhar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 16 de dezembro de 2008 13:58
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientCmdInfoPilhada(%perdeuOUganhou, %infoNum){
	%info = clientFindInfo(%infoNum);
	
	if(%perdeuOUganhou $= "ganhou"){
		clientCmdSetNewInfo(%infoNum, false, true); //infonum, faro, pilhar
	} else if(%perdeuOUganhou $= "perdeu"){
		clientTerInfoPilhada(%infoNum);
	} else {
		echo("ERRO: perdeuOUganhou não estava definida");	
	}
}

function clientTerInfoPilhada(%infoNum){
	%info = clientFindInfo(%infoNum);
	$mySelf.mySimInfo.remove(%info);
	
	clientFXtxt(%info.myMark, "pilhar");	
		
	%info.myMark.safeDelete();
		
	clientAtualizarEstatisticas();
}