// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientAlmirante.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  domingo, 30 de março de 2008 2:56
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function clienti_almirante_BtnClick(){
	clientMostrarAlmiranteTab();	
}

function clientMostrarAlmiranteTab(){
	almiranteTab.setVisible(true);
	clientAtualizarAlmiranteTab();
}

function clientApagarAlmiranteTab(){
	almiranteTab.setVisible(false);
}

function clientAtualizarAlmiranteTab(){
	%basesNoMar = 0;
	for(%i = 0; %i < $mySelf.mySimBases.getCount(); %i++){
		%base = $mySelf.mySimBases.getObject(%i);
		if(%base.onde.terreno $= "mar"){
			%basesNoMar++;	
		}
	}
	
	almiranteTxt.text = %basesNoMar @ " / 5";
}