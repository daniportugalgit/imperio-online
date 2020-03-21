// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\start.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  domingo, 29 de julho de 2007 5:56
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
/////////////////////////////////////////////////////////////////////
//função global usada para orientar peças e tiros:
function angleBetween(%unit, %destino){
	// Separar a posição destino
    %destinoX = getWord(%destino,0);
    %destinoY = getWord(%destino,1);

	// separar a posição do navio
    %unitX = getWord(%unit,0);
    %unitY = getWord(%unit,1);

    // Calcular ângulo do navio para o destino (converter para graus):
    %angle = mRadToDeg( mAtan( %destinoX-%unitX, %unitY-%destinoY ) );
	%angle -= 90;

	return %angle;
}
	
//apaga o HUD:
if(!$IAmServer){
	$infoMarksGlobal = new SimSet(); //simSets usados para fazer o unloadGame
	
	//Alvos da seleção de grupos:
	$alvo1 = alvoAnim1;
	$alvo2 = alvoAnim2;
	$alvo3 = alvoAnim3;
	$alvo4 = alvoAnim4;
	$alvo5 = alvoAnim5;
	$alvo6 = alvoAnim6;
	$alvo7 = alvoAnim7;
	
	//cartas de grupos ficam visíveis, se não estavam antes:
	grupo1_carta.setVisible(true);
	grupo2_carta.setVisible(true);
	grupo3_carta.setVisible(true);
	grupo4_carta.setVisible(true);
	
	//botões das cores ficam visíveis, se não estavam antes:
	azul_btn.setVisible(true);
	verde_btn.setVisible(true);
	amarelo_btn.setVisible(true);
	vermelho_btn.setVisible(true);
	roxo_btn.setVisible(true);
	
	//Seta o jogador da vez e zera a variável global de transporte de saoldados em navios
	$jogadorDaVez = $player1;
	$transporteGlobalCount = 0;
	
	//cria container "foco":
	$foco = new SimSet(Foco);
	
	//guardar imagem das selectionMarks numa variável global:
	$selectionReliquia = selectionReliquia; //mouse down
	$selectionBase = selectionBase; //mouse down
	$selectionSoldado = selectionSoldado;
	$selectionTanque = selectionTanque;
	$selectionNavio = selectionNavio;
	$semiSelectionReliquia = semiSelectionReliquia; //mouse down
	$semiSelectionBase = semiSelectionBase; //mouse over
	$semiSelectionSoldado = semiSelectionSoldado;
	$semiSelectionTanque = semiSelectionTanque;
	$semiSelectionNavio = semiSelectionNavio;
	$reciclandoTXT = reciclandoTXT;
	
	
	apagarBtns();
	bater_btn.setVisible(false);
	finalizarTurno_btn.setVisible(false);
	renderTudo_btnIcon.setActive(false);
}

function t2dSceneObject::action(%this, %action, %val){
	// juntar os nomes para o engine aceitar o comando
   	%eval = %this @ "." @ %action @ "(\"" @ %val @ "\");";
   	eval(%eval);   
}


function clientDebugMaster(){
	commandToServer('debugMaster');	
}

function clientNotDebug(){
	commandToServer('NOTdebugMaster');		
}



loadDev_keyBind(); //permite atalhos de dev antes de carregar o jogo em si