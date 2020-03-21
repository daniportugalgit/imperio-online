// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientMoral.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 1 de abril de 2008 19:10
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientVerificarMoral(){
	%moralDefesa = 0;
	%moralAtaque = 0;
	for(%i = 0; %i < $mySelf.mySimLideres.getcount(); %i++){
		%lider = $mySelf.mySimLideres.getObject(%i);
		if(%lider.myMoral > 0){
			%moralDefesa++;
		}
		if(%lider.myMoral > 1){
			%moralAtaque++;
		}
	}
	$mySelf.moralDefesa = %moralDefesa;
	$mySelf.moralAtaque = %moralAtaque;
}