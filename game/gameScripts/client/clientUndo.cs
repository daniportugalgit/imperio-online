// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientUndo.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 15 de fevereiro de 2008 16:56
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientCriarUndo(%unit, %areaDeOrigem, %especial, %embarque){
	if(!isObject($clientUNDO)){
		$clientUNDO = new ScriptObject(){};
	} else {
		clientClearUndo();
	}
	
	if(!$BLOCKUNDO){
		$clientUNDO.unit = %unit;
		$clientUNDO.areaDeOrigem = %areaDeOrigem;
		$clientUNDO.especial = %especial;
		$clientUNDO.embarque = %embarque;
	} else {
		//echo("unDo Blocked! unBlocking unDo.");
		$BLOCKUNDO = false;
	}
}

function clientClearUndo(){
	//echo("clientClearUndo();");
	$clientUNDO.unit = "no";
	$clientUNDO.areaDeOrigem = "no";
	$clientUNDO.especial = "no";
	$clientUNDO.embarque = "no";
}

function clientCmdDoUndo(){
	%unit = $clientUNDO.unit;
	%areaDeOrigem = $clientUNDO.areaDeOrigem;
	%especial = $clientUNDO.especial;
	%embarque = $clientUNDO.embarque;
		
	if(%unit !$= "no" && %areaDeOrigem !$= "no"){
		clientShowUndoMark(); //mostra que um UnDo está sendo efetuado;
		/*
		if(%embarque $= "embarque"){
			//unit.onde == %navio
			if(%navio.dono == %unit.dono){
				%corDeQuem = "minhaCor";	
			} else {
				%corDeQuem = "outro";	
			}
			clientCmdDesembarcarUnidade(%unit.onde.onde.getName(), %unit.onde.pos, %areaDeOrigem.getName(), %unit.class, %corDeQuem, true);
		} else if(%embarque $= "desembarque"){
			
		} else {
		*/
			clientRemoverUnidade(%unit, %unit.onde); //retira a unidade da área-alvo
			%areaDeOrigem.positionUnit(%unit); //coloca a unidade na área de origem
			%areaDeOrigem.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
			if(%especial == false){
				$jogadorDaVez.movimentos += 1; //soma um movimento, em vez de subtrair
			} else {
				%unit.JPagora++;	
			}
		//}
		atualizarMovimentosGui(); //atualiza os movimentos e já separa os meus dos adversários por um cinza inativo para os últimos (todos os clients vêem os movimentos do jogadorDaVez);
		clientLigarFlechas(%unit.onde.getName()); //tem que desligar e ligar quando seleciona outra peça;
		clientClearUndo(); //apenas um Undo fica gravado;
		atualizarBotoesDeCompra();
	} else {
	 	//echo("UNDO IMPOSSÍVEL: Não há ação para ser desfeita!");	
	} 
}


function clientAskUndo(){
	if($jogadorDaVez == $mySelf){
		if($clientUNDO.unit !$= "no" &&	$clientUNDO.areaDeOrigem !$= "no" && $clientUNDO.unit !$= "" &&	$clientUNDO.areaDeOrigem !$= ""){
			commandToServer('undo');
		}
	}
}

function clientShowUndoMark(){
	ctrlZ.setVisible(true);
	$undoMarkShedule = schedule(2000, 0, "clientApagarUndoMark");
}

function clientApagarUndoMark(){
	ctrlZ.setVisible(false);	
	cancel($undoMarkShedule); //cancela, caso o client tenha conseguido dar 2 undos rapidamente...
}