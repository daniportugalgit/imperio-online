// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientExpulsar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 16 de dezembro de 2008 9:35
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAskExpulsar(){
	%rainha = foco.getObject(0);
	
	if($myPersona.aca_av_3 > 0){
		commandToServer('expulsar', %rainha.onde.getName());	
	}
}

function clientCmdExpulsar(%areaNome, %todos){
	%eval = "%area = " @ %areaNome @ ";";
	eval(%eval);
	%rainha = %area.pos0Quem;
	
	//efeito de texto:
	clientFXtxt(%rainha, "expulsar");
		
	if(%todos){
		%transporteCount = %rainha.myTransporte.getCount();
		for(%i = 0; %i < %transporteCount; %i++){
			%unit = %rainha.myTransporte.getObject(0);
			%unit.dismount();
			%unit.setPosition(%rainha.getPosition());
			%unit.setVisible(true);
			%rainha.unMark();
			clientDesembarcar(%unit, %area, false, true);
			%rainha.myTransporte.remove(%unit);
			%rainha.reMark();	
		}
	} else {
		%unit = %rainha.myTransporte.getObject(0);
		%unit.dismount();
		%unit.setPosition(%rainha.getPosition());
		%unit.setVisible(true);
		%rainha.unMark();
		clientDesembarcar(%unit, %area, false, true);
		%rainha.myTransporte.remove(%unit);
		%rainha.reMark();	
	}
	
	if(%rainha.dono == $mySelf){
		%custo = 3 - $myPersona.aca_av_3;	
		$mySelf.imperiais -= %custo;
		atualizarImperiaisgui();
		atualizarBotoesDeCompra();
	}
	clientClearUndo();
}