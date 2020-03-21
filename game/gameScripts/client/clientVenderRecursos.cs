// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientVenderRecursos.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 15 de janeiro de 2008 18:48
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


/////////Venda de recursos:

function clientAskVenderUranio(){
	if(!$estouNoTutorial){
		clientDesligarVenderRecursos();
		clientPushServerComDot();
		commandToServer('venderUranio', $mySelf.id);
	} else {
		//clientCmdVenderUranio();
		//não acontece nada, pois vc não pode vender urânio no tutorial
	}
}

function clientAskVenderPetroleo(){
	clientDesligarVenderRecursos();
	if(!$estouNoTutorial){
		clientPushServerComDot();
		commandToServer('venderPetroleo', $mySelf.id);
	} else {
		clientCmdVenderPetroleo();
	}
}

function clientAskVenderMinerio(){
	clientDesligarVenderRecursos();
	if(!$estouNoTutorial){
		clientPushServerComDot();
		commandToServer('venderMinerio', $mySelf.id);
	} else {
		clientCmdVenderMinerio();
	}
}

function clientAskVenderConjunto(){
	clientDesligarVenderRecursos();
	if(!$estouNoTutorial){
		clientPushServerComDot();
		commandToServer('venderConjunto', $mySelf.id);
	} else {
		clientCmdVenderConjunto();	
	}
}

function clientToggleRecursosBtns(){
	//liga o botão de vender um Conjunto:
	if ($mySelf.minerios < 1 || $mySelf.petroleos < 1 || $mySelf.uranios < 1){
		venderConjunto_btn.setActive(false);
	} else {
		venderConjunto_btn.setActive(true);
	}
	
	//liga o botão de vender 5 Minérios:
	if ($mySelf.minerios < 5){
		venderMinerio_btn.setActive(false);
	} else {
		venderMinerio_btn.setActive(true);
	}
	
	//liga o botão de vender 4 Petróleos:
	if ($mySelf.petroleos < 4){
		venderPetroleo_btn.setActive(false);
	} else {
		venderPetroleo_btn.setActive(true);
	}
	
	//liga o botão de vender 3 Urânios:
	if ($mySelf.uranios < 3){
		venderUranio_btn.setActive(false);
	} else {
		venderUranio_btn.setActive(true);
	}
}




function clientLigarVenderRecursos(){
	if($estouNoTutorial){
		if($tut_campanha.passo.key $= "venderRecursosClick"){
			clientToggleRecursosBtns();
			venderRecursosGui.setVisible(true); //traz o gui;
			venderRecursos_btn.setStateOn(true); //deixa o btn pressionado
			tut_verificarObjetivo(false, "venderRecursosClick");
		} else {
			venderRecursos_btn.setStateOn(true); //deixa o btn despressionado
		}
	} else {
		clientToggleRecursosBtns();
		venderRecursosGui.setVisible(true); //traz o gui;
		venderRecursos_btn.setStateOn(true); //deixa o btn pressionado
	}
}

function clientDesligarVenderRecursos(){
	venderRecursosGui.setVisible(false); //apaga o gui;
	venderRecursos_btn.setStateOn(false); //deixa o btn despressionado
}


function clientCmdVenderUranio(){
	clientPopServerComDot();
	$mySelf.uranios -= 3;
	$mySelf.imperiais += 10;
	echo("3 Uranios vendidos por 10 Imperiais");
		
	atualizarRecursosGui();
	atualizarImperiaisGui();
	clientAtualizarEstatisticas();
	clientDesligarVenderRecursos();
	clientVerifyNexusBtns();
	clientClearUndo(); //limpa o Undo, só há Undo pra movimentos
}

function clientCmdVenderPetroleo(){
	clientPopServerComDot();
	$mySelf.petroleos -= 4;
	$mySelf.imperiais += 10;
	echo("4 Petroleos vendidos por 10 Imperiais");
		
	atualizarRecursosGui();
	atualizarImperiaisGui();
	clientAtualizarEstatisticas();
	clientDesligarVenderRecursos();
	clientVerifyNexusBtns();
	clientClearUndo(); //limpa o Undo, só há Undo pra movimentos
}

function clientCmdVenderMinerio(){
	clientPopServerComDot();
	$mySelf.minerios -= 5;
	$mySelf.imperiais += 10;
	echo("5 Minerios vendidos por 10 Imperiais");
	
	atualizarRecursosGui();
	atualizarImperiaisGui();
	clientAtualizarEstatisticas();
	clientDesligarVenderRecursos();
	clientVerifyNexusBtns();
	clientClearUndo(); //limpa o Undo, só há Undo pra movimentos
}

function clientCmdVenderConjunto(){
	clientPopServerComDot();
	$mySelf.minerios -= 1;
	$mySelf.petroleos -= 1;
	$mySelf.uranios -= 1;
	$mySelf.imperiais += 10;
	echo("1 conjunto de Recursos vendido por 10 Imperiais");
	
	atualizarRecursosGui();
	atualizarImperiaisGui();
	clientAtualizarEstatisticas();
	clientDesligarVenderRecursos();
	clientVerifyNexusBtns();
	clientClearUndo(); //limpa o Undo, só há Undo pra movimentos
	if($estouNoTutorial){
		tut_verificarObjetivo(false, "venderConjuntoClick");	
	}
}

