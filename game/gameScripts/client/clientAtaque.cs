// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientAtaque.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 30 de outubro de 2007 22:24
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function clientAtkGuiZerar(){
	atk2Result_img.setVisible(false); //desativa os resultados	
	def2Result_img.setVisible(false);
	atk2Fundo_img.setVisible(false); //desativa os fundos	
	def2Fundo_img.setVisible(false);
	atk1Soldado_img.setVisible(false); //apaga os incones
	atk2Soldado_img.setVisible(false);
	def1Soldado_img.setVisible(false);
	def2Soldado_img.setVisible(false);
	atk1Tanque_img.setVisible(false);
	atk2Tanque_img.setVisible(false);
	def1Tanque_img.setVisible(false);
	def2Tanque_img.setVisible(false);
	atk1Navio_img.setVisible(false);
	atk2Navio_img.setVisible(false);
	def1Navio_img.setVisible(false);
	def2Navio_img.setVisible(false);
	atk1Lider_img.setVisible(false);
	atk2Lider_img.setVisible(false);
	def1Lider_img.setVisible(false);
	def2Lider_img.setVisible(false);
	atk1guloks_img.setVisible(false);
	atk2guloks_img.setVisible(false);
	def1guloks_img.setVisible(false);
	def2guloks_img.setVisible(false);
}

function clientApagarAtkGui(){
	atkGui.setVisible(false);
}

function clientApagarAtkGuiEm(%milisegundos){
	cancel($atkGuiSchedule);

	$atkGuiSchedule = schedule(%milisegundos, 0, "clientApagarAtkGui");
}


$ultimoAtaqueFinalizado = true;
function clientAskAtacar(%areaDeOrigem, %areaAlvo){
	if($estouNoTutorial)
		return;
	
	if(!$ultimoAtaqueFinalizado)
		return;
		
	if($primeiraRodada)
	{
		clientMsg("ataqueIlegal", 3000);
		return;
	}
	
	if($mySelf.atacou || $jogadorDaVez == $aiPlayer){
		if(isObject(%areaAlvo.pos1Quem) || (isObject(%areaAlvo.pos0Quem) && !%areaalvo.desprotegida)){
			%areaDeOrigemClient = %areaDeOrigem.getName();
			%areaAlvoClient = %areaAlvo.getName();
			commandToServer('atacar', %areaDeOrigemClient, %areaAlvoClient);
			$ultimoAtaqueFinalizado = false;
			clientPushServerComDot();
		} else {
			echo("ERRO3");	
		} 
	} else {
		$lastATKAreaDeOrigem = %areaDeOrigem;
		$lastATKAreaAlvo = %areaAlvo;
		clientPushDiplomaciaQMsgBox();	
	}
}

function clientConfirmarAtaque(){
	%areaDeOrigemClient = $lastATKAreaDeOrigem.getName();
	%areaAlvoClient = $lastATKAreaAlvo.getName();
	commandToServer('atacar', %areaDeOrigemClient, %areaAlvoClient);
	$ultimoAtaqueFinalizado = false;
	clientPushServerComDot();
	clientPopDiplomaciaQGui();
}

function clientPushDiplomaciaQMsgBox(){
	canvas.pushDialog(diplomaciaQGui);	
}

function clientPopDiplomaciaQGui(){
	canvas.popDialog(diplomaciaQGui);	
}

function clientApagarMorteMarks(){
	morteMarkDef1.setVisible(false);	
	morteMarkDef2.setVisible(false);	
	morteMarkAtk1.setVisible(false);	
	morteMarkAtk2.setVisible(false);	
}

//ataque:
function clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk1, %resultDef1, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2){
	clientApagarMorteMarks();
	resetGhostSelection(); //apaga a seleção ghost, pq se uma peça morrer, ela ficaria no palco
	$ultimoAtaqueFinalizado = true;
	clientPopServerComDot();
	palcoTurnoTimer.resumeTimer(); //se o timer estivesse parado para sincronizar, manda ele voltar a contar
	$myAtaqueOcorrendo = true; //marca que começou um ataque
	%eval = "%areaDeOrigem =" SPC %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvo =" SPC %areaAlvo @ ";";
	eval(%eval);
	
	%unidadeAtacante1 = clientGetGameUnit(%areaDeOrigem, %posDeOrigem);
	%unidadeDefensora1 = clientFindUnitInimiga(%areaAlvo, %posAlvo, %unidadeAtacante1.dono);
	
	//zoom apenas para quem está sendo atacado:
	if(%unidadeDefensora1.dono == $mySelf){
		setUniZoom(%areaDeOrigem, %areaAlvo, 4000);
	}
	
	%unidadeAtacante1.dono.myDiplomataHud.bitmap = "~/data/images/playerHudAtaque.png"; //marca que quem atacou não é mais diplomata
	%unidadeAtacante1.dono.atacou = true;
	
	%corDoAtacante1 = %unidadeAtacante1.dono.myColor;
	%corDoDefensor1 = %unidadeDefensora1.dono.myColor;
	%classeDoAtacante1 = %unidadeAtacante1.class;
	%classeDoDefensor1 = %unidadeDefensora1.class;
	atkGui.setVisible(true);
	
	%envolvidosCount = 2; //todo combate tem pelo menos 2 envolvidos
	clientAtkGuiZerar(); //é um novo ataque
	atk1Fundo_img.bitmap = "~/data/images/atk1" @ %corDoAtacante1 @ ".png"; //atk1Vermelho.png, por exemplo;
	
	clientSetCombateSil(%unidadeAtacante1, "atk", 1);
		
	atk1Result_img.bitmap = "~/data/images/" @ %resultAtk1 @ ".png"; //1.png, por exemplo;
		///////////////////////////////////////////
		//////////////////////////////////////////
	def1Fundo_img.bitmap = "~/data/images/def1" @ %corDoDefensor1 @ ".png"; //def1Vermelho.png, por exemplo;
	
		
	clientSetCombateSil(%unidadeDefensora1, "def", 1);	
		
	def1Result_img.bitmap = "~/data/images/" @ %resultDef1 @ ".png"; //1.png, por exemplo;
		
	if(%resultAtk2 $= "no"){ //verificação pra ver se tem 4 unidades brigando ou só 3;
	} else {
		%eval = "%unidadeAtacante2 = " @ %areaDeOrigem.getName() @ "." @ %posDeOrigem2 @ "Quem;";
		eval(%eval);
		
		%unidadeAtacante2.dono.atacou = true;	
			
		%corDoAtacante2 = %unidadeAtacante2.dono.myColor;
		%classeDoAtacante2 = %unidadeAtacante2.class;
				
		atk2Fundo_img.setVisible(true);
		atk2Result_img.setVisible(true);
			
		atk2Fundo_img.bitmap = "~/data/images/atk2" @ %corDoAtacante2 @ ".png"; //atk2Vermelho.png, por exemplo;
		
		clientSetCombateSil(%unidadeAtacante2, "atk", 2);
			
		atk2Result_img.bitmap = "~/data/images/" @ %resultAtk2 @ ".png"; //1.png, por exemplo;
	}
		///////////////////////////////////////////
		//////////////////////////////////////////
	if(%resultDef2 $= "no"){ //verificação pra ver se tem 4 unidades brigando ou só 3;
	} else {
		%eval = "%unidadeDefensora2 = " @ %areaAlvo.getName() @ "." @ %posAlvo2 @ "Quem;";
		eval(%eval);
		
		%corDoDefensor2 = %unidadeDefensora2.dono.myColor;
		%classeDoDefensor2 = %unidadeDefensora2.class;
		
		def2Fundo_img.setVisible(true);
		def2Result_img.setVisible(true);
			
		def2Fundo_img.bitmap = "~/data/images/def2" @ %corDoDefensor2 @ ".png"; //def2Vermelho.png, por exemplo;
		
		clientSetCombateSil(%unidadeDefensora2, "def", 2);		
					
		def2Result_img.bitmap = "~/data/images/" @ %resultDef2 @ ".png"; //1.png, por exemplo;
	}
	
	clientApagarAtkGuiEm(6000);
	
	/////////////
	//disferir ataque se houve batalha, do contrário apenas mostrar no hud o resultado:
	if(%resultAtk1 > %resultDef1){
		%unidadeAtacante1.fire(%unidadeDefensora1, %resultAtk1);
		//%unidadeDefensora1.missFire(%unidadeAtacante1, %resultDef1); //a unidade defensora é pega de surpresa!
		morteMarkDef1.setVisible(true);	
	} else {
		%unidadeDefensora1.fire(%unidadeAtacante1, %resultDef1);
		%unidadeAtacante1.missFire(%unidadeDefensora1, %resultAtk1);
		morteMarkAtk1.setVisible(true);	
	}
	if(%resultDef2 !$= "no" && %resultAtk2 !$= "no"){
		if(%resultAtk2 > %resultDef2){
			%unidadeAtacante2.fire(%unidadeDefensora2, %resultAtk2);
			//%unidadeDefensora2.missFire(%unidadeAtacante2, %resultDef2); //a unidade defensora é pega de surpresa!
			morteMarkDef2.setVisible(true);	
		} else {
			%unidadeDefensora2.fire(%unidadeAtacante2, %resultDef2);
			%unidadeAtacante2.missFire(%unidadeDefensora2, %resultAtk2);
			morteMarkAtk2.setVisible(true);	
		}
	}
	clientClearUndo(); //limpa o Undo, só há Undo pra movimentos
}


///seta a silhueta da peça no hud de combate
function clientSetCombateSil(%unit, %atkOuDef, %umOuDois){
	
	if(%unit.class $= "verme" || %unit.class $= "zangao" || %unit.class $= "rainha" || %unit.class $= "cefalok"){
		
		%eval = "%image = " @ %atkOuDef @ %umOuDois @ "guloks_img;"; //atk1guloks_img, por exemplo
		eval(%eval);
		if(%atkOuDef $= "atk"){
			%dirOuEsq = "dir";	
		} else {
			%dirOuEsq = "esq";
		}
		if(%unit.class $= "cefalok"){
			%image.bitmap = "game/data/images/" @ %unit.class @ %dirOuEsq;
		} else if(%unit.class $= "rainha"){
			if(%unit.grandeMatriarca){
				%image.bitmap = "game/data/images/rainhaGMat" @ %dirOuEsq;
			} else {
				%image.bitmap = "game/data/images/" @ %unit.class @ %dirOuEsq;
			}
		} else {
			if(%unit.class $= "zangao"){
				if(%unit.liderNum == 1){
					if(%unit.JPBP > 0){
						%image.bitmap = "game/data/images/zangaoPretoAsa" @ %dirOuEsq;	
					} else {
						%image.bitmap = "game/data/images/zangaoPreto" @ %dirOuEsq;	
					}
				} else {
					if(%unit.JPBP > 0){
						%image.bitmap = "game/data/images/zangaoBrancoAsa" @ %dirOuEsq;	
					} else {
						%image.bitmap = "game/data/images/zangaoBranco" @ %dirOuEsq;	
					}
				}
			} else if(%unit.class $= "verme"){
				if(%unit.dono.exoesqueleto > 0){
					%image.bitmap = "game/data/images/vermeBocarra" @ %dirOuEsq;	
				} else {
					%image.bitmap = "game/data/images/verme" @ %dirOuEsq;	
				}
			}
		}
	} else {
		%eval = "%image = " @ %atkOuDef @ %umOuDois @ %unit.class @ "_img;"; //atk1Soldado_img, por exemplo
		eval(%eval);
	}
	
	%image.setVisible(true);
}

function clientFinalizarAtaque(){
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	
	if($myAtaqueOcorrendo == true){
		$myAtaqueOcorrendo = false;
		commandToServer('marcarAtaqueConcluido', %mySelf.id);
		echo("Finalizando ataque == " @ %mySelf.id);
	}
}

function clientFinalizarAIAtaque(){
	if($mySelf.aiManager){
		$myAtaqueOcorrendo = false;
		commandToServer('marcarAtaqueConcluido', $aiPlayer.id);
		echo("Finalizando ataque == " @ $aiPlayer.id);
	}
}