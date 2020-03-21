// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverMoral.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 1 de abril de 2008 17:46
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function jogo::verificarMoral(%this, %player){
	%moralDefesa = 0;
	%moralAtaque = 0;
	for(%i = 0; %i < %player.mySimLideres.getcount(); %i++){
		%lider = %player.mySimLideres.getObject(%i);
		if(%lider.myMoral > 0){
			%moralDefesa++;
		}
		if(%lider.myMoral > 1){
			%moralAtaque++;
		}
	}
	%player.moralDefesa = %moralDefesa;
	%player.moralAtaque = %moralAtaque;
}