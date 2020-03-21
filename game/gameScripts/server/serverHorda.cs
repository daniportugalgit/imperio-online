// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverHorda.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quarta-feira, 10 de dezembro de 2008 5:42
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverPegarBonusHorda(%area){
	%bonus = 0;
	if(%area.dono.persona.especie $= "gulok"){
		if(%area.dono !$= "MISTA" && %area.dono.horda > 0){
			%vermes = 0;
			if(%area.pos1Quem.class $= "verme"){
				%vermes++;	
			}
			if(%area.pos2Quem.class $= "verme"){
				%vermes++;	
			}
			if(%area.myPos3List.getcount() > 0){
				%vermes += %area.myPos3List.getcount();
			}
			if(%vermes >= 4){
				%bonus = (mFloor(%vermes / 4)) * %area.dono.horda;
				//echo("Bônus de Horda = " @ %bonus);
			} else {
				//echo("Bônus de Horda (0): não há mais de 3 vermes na área (" @ %vermes @ ")");
			}		
		} else {
			//echo("Bônus de Horda (0): área mista ou horda < 1!");
		}
	} else {
		//echo("Bônus de Horda (0): A área não é Gulok.");
	}
	return %bonus;
}