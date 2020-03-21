// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientMetamorfose.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 14 de dezembro de 2008 5:38
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function clientAskMetamorfose(){
	%zangao = foco.getObject(0);
	if($myPersona.aca_ldr_2_h3 > 0){
		if(!%zangao.onde.pos0Flag){
			commandToServer('metamorfose', %zangao.onde.getName(), %zangao.pos); 	
		}
	}	
}

function clientCmdMetamorfose(%areaNome, %pos, %metamorfose){
	%eval = "%area = " @ %areaNome @ ";";
	eval(%eval);
	%eval = "%zangao = %area." @ %pos @ "Quem;";
	eval(%eval);
		
	clientFXtxt(%zangao, "metamorfose");
	
	clientCmdEvoluirEmRainha(%areaNome, %pos, false, %metamorfose);
}