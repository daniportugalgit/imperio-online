// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientTerceiroLider.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 1 de abril de 2008 1:25
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function clientAskChamarTerceiroLider(){
	%onde = Foco.getObject(0).onde.getName();
	if($salaEmQueEstouTipoDeJogo $= "poker" && $mySelf.lideresDisponiveis > 0)
	{
		commandToServer('convocarPkLider', %onde);
		return;
	}
	
	if($myPersona.aca_a_1 > 2){
		if($mySelf.terceiroLiderOn == false){
			commandToServer('chamarTerceiroLider', %onde);
			$mySelf.terceiroLiderOn = true;
		}
	}
}

function clientCmdChamarTerceiroLider(%onde, %newLiderEscudo, %newLiderJetPack, %newLiderSnipers, %newLiderMoral){
	%base = clientGetGameUnit(%onde, "pos0");
	
	%newLider = %base.spawnUnit("lider");
	%newLider.liderNum = 3;
	%newLider.setMyImage();
	
	if(%newLiderEscudo > 0){
		%newLider.criarMeuEscudo();
		%newLider.myEscudos = %newLiderEscudo; //escudos no jogo
	}
	
	if(%newLiderJetPack > 1){
		%newLider.JPBP = %newLiderJetPack - 1;
		%newLider.JPagora = %newLiderJetPack - 1;
		%newLider.anfibio = true;
	} else {
		%newLider.JPBP = %newLiderJetPack; //jetPack BluePrint
		%newLider.JPagora = %newLiderJetPack; //JetPack na rodada
	}
	%newLider.mySnipers = %newLiderSnipers; //Snipers no jogo
	%newLider.myMoral = %newLiderMoral; //Snipers no jogo
	atualizarBotoesDeCompra();
	if(%base.dono == $mySelf){
		clientVerificarMoral();	
	}
}

function clientCmdConvocarPkLider(%onde)
{
	%base = clientGetGameUnit(%onde, "pos0");
	
	%newLider = %base.spawnUnit("lider");
	%newLider.liderNum = %base.dono.mySimLideres.getCount();
	%newLider.setMyImage();
	
	%newLider.criarMeuEscudo();
	%newLider.myEscudos = 2;
	
	%newLider.JPBP = 1;
	%newLider.JPagora = 1;
	%newLider.anfibio = true;
	%newLider.mySnipers = 3;
	%newLider.myMoral = 2;
	
	
	
	if(%base.dono == $mySelf)
	{
		$mySelf.lideresDisponiveis--;
		clientVerificarMoral();	
	}
	atualizarBotoesDeCompra();
}