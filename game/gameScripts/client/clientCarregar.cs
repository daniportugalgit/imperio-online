// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientCarregar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quinta-feira, 11 de dezembro de 2008 3:47
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskCarregar(){
	%zangao = foco.getObject(0);
	if(%zangao.onde.pos1Quem.class $= "verme" && %zangao.onde.pos1Quem.dono == $mySelf){
		%posDeOrigem = "pos1";	
	} else if(%zangao.onde.pos2Quem.class $= "verme" && %zangao.onde.pos2Quem.dono == $mySelf){
		%posDeOrigem = "pos2";	
	} else {
		%posDeOrigem = "pos3";	
	}
	clientAskEmbarcar(%zangao.onde, %posDeOrigem, %zangao.onde, %zangao.pos, false, false, true);		
}

function clientAskDescarregar(){
	%zangao = foco.getObject(0);
	if(isObject(%zangao.myTransporte)){
		if(%zangao.myTransporte.getCount() > 0){
			if(%zangao.onde.terreno $= "terra"){
				clientAskDesembarcar(%zangao.onde, %zangao.pos, %zangao.onde, "verme", "minhaCor", true);	
			}
		}
	}
}