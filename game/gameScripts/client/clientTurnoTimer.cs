// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientTurnoTimer.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 26 de novembro de 2007 14:24
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

$turnoTimer_txt = turnoTimer_txt;
$firstStartTime = 25;

function turnoTimer::iniciarTimer(%this)
{
	if($salaEmQueEstouTipoDeJogo $= "poker")
	{
		%this.setPokerTimer();
		return;
	}
	
	if($rodadaAtual == 0){
		%this.setStartTimer();	
	} else {
		%this.setJogoTimer();
	}
	%this.printTimer();
	%this.setTimerOn(1000); 
}

function turnoTimer::setPokerTimer(%this)
{
	if($mySelf.pk_jogo.notInPokerPlay && $rodadaAtual > 0){
		%this.setJogoTimer();
	} else {
		%this.setStartTimer();	
	}
		
	%this.printTimer();
	%this.setTimerOn(1000); 
}

function turnoTimer::setJogoTimer(%this)
{
	if($jogadorDaVez == $aiPlayer)
	{
		%this.turnoTimeLeft = 160;
		return;
	}
	
	%this.turnoTimeLeft = $turnoDesteJogo;
}

function turnoTimer::setStartTimer(%this)
{
	%this.turnoTimeLeft = $firstStartTime;
}

function turnoTimer::printTimer(%this){
	if($salaEmQueEstouTipoDeJogo $= "poker" && $inGame)
		$mySelf.pk_jogo.atualizarGMBOT();
	
	if($rodadaAtual == 0)
	{
		%this.printStartTimer();
		return;
	}
	
	%this.printTurnoTimer();
}

function turnoTimer::printTurnoTimer(%this)
{
	if(%this.turnoTimeLeft <= 0)
		return;
		
	$turnoTimer_txt.text = %this.turnoTimeLeft;	
}

function turnoTimer::printStartTimer(%this)
{
	if($estouNoTutorial)
	{
		firstStartTimer_txt.setVisible(false);	
		%this.apagarFirstStartTimers();	
		return;
	}
	
	%this.atualizarAguardandoTimer();
	
	if(%this.turnoTimeLeft <= 0)
		return;
	
	if($jogadorDaVez == $mySelf)
	{
		clientAtualizarMyFirstStartTimer(%this);
		return;	
	}
	
	%this.apagarFirstStartTimers();
}

function turnoTimer::apagarFirstStartTimers(%this)
{
	firstStartTimerOBJ_txt.setVisible(false);
	firstStartTimerGRP_txt.setVisible(false);
}

function clientAtualizarMyFirstStartTimer(%timer){
	firstStartTimerOBJ_txt.text = "Tempo restante: " @ %timer.turnoTimeLeft;	
	firstStartTimerGRP_txt.text = "Tempo restante: " @ %timer.turnoTimeLeft;	
	firstStartTimerOBJ_txt.setVisible(true);
	firstStartTimerGRP_txt.setVisible(true);
}

function turnoTimer::atualizarAguardandoTimer(%this)
{
	firstStartTimer_txt.setVisible(true);
	firstStartTimer_txt.text = %this.turnoTimeLeft;
}

function turnoTimer::pauseTimer(%this, %semFim)
{
	%this.paused = true;
	%this.setTimerOff();
	
	if(%semFim)
		return;
		
	
	%this.scheduleResume(4000);
}

function turnoTimer::scheduleResume(%this, %time)
{
	schedule(%time, 0, "clientResumeTurnoTimer"); //Se em 5 segundos o cara não tentar atacar de novo, o timer volta automaticamente;	
}

function turnoTimer::resumeTimer(%this)
{
	if(%this.paused)
		%this.setTimerOn(1000); 	//liga de volta a chamada a cada segundo;
}

function clientResumeTurnoTimer()
{
	palcoTurnoTimer.resumeTimer();
}

function clientCmdResumeTurnoTimer()
{
	clientResumeTurnoTimer();
}

function clientPauseTurnoTimer()
{
	palcoTurnoTimer.pauseTimer(true);	//semFim, tem que ser reiniciado
}

function turnoTimer::subtrairSegundo(%this)
{
	if(%this.turnoTimeLeft > 0)
		%this.turnoTimeLeft -= 1;	
}

function turnoTimer::onTimer(%this)
{
	%this.subtrairSegundo();
	%this.printTimer();
		
	if(%this.turnoTimeLeft <= 0){
		%this.tempoAcabou();
		return;
	}
	
	%this.atualizarGuiBtns();
}

function turnoTimer::tempoAcabou(%this)
{
	%this.setTimerOff();
	
	if($jogadorDaVez != $mySelf)
		return;
	
	if($salaEmQueEstouTipoDeJogo $= "poker" && $vendoPoker)
	{
		clientPk_checkFoldSorteio();
		return;
	}
	
	if($rodadaAtual == 0 && !$estouNoTutorial)
	{
		clientAskPassarAVezAntesDoInicio();			
		return;
	}
		
	clientMsg("tempoAcabou", 4000);
	clientAskFinalizarTurno();
}

function turnoTimer::atualizarGuiBtns(%this)
{
	if($jogadorDaVez == $mySelf)
	{
		%this.apagarBaterBtn();
		%this.piscarSePrecisar();
	}	
}

function turnoTimer::apagarBaterBtn(%this)
{
	if(%this.turnoTimeLeft == 1)
		bater_btn.setVisible(false);
}

function turnoTimer::piscarSePrecisar(%this)
{
	if(%this.turnoTimeLeft == ($turnoDesteJogo - 30) || %this.turnoTimeLeft == 20 || %this.turnoTimeLeft == 15 || %this.turnoTimeLeft == 10 || %this.turnoTimeLeft == 5 || %this.turnoTimeLeft == 3 || %this.turnoTimeLeft == 1){
		tempo_img.bitmap = "~/data/images/tempoPreto.png";
	} else if(%this.turnoTimeLeft == ($turnoDesteJogo - 31) || %this.turnoTimeLeft == 19 || %this.turnoTimeLeft == 14 || %this.turnoTimeLeft == 9 || %this.turnoTimeLeft == 4 || %this.turnoTimeLeft == 2 || %this.turnoTimeLeft == 0){
		tempo_img.bitmap = "~/data/images/tempo" @ $mySelf.myColor @ ".png";
	}
}



function clientCmdBonusEconomia(%gradNum){
	//echo("**GRAD-NUM: " @ %gradNum);
	if(%gradNum >= 10 && $myPersona.especie $= "humano"){
		clientMsg("bonusEconomia2", 5000);
		$mySelf.imperiais += 2;
	} else {
		if($myPersona.especie $= "humano"){
			clientMsg("bonusEconomia", 5000);
			$mySelf.imperiais++;
		} else if($myPersona.especie $= "gulok"){
			if($myPersona.aca_v_1 == 0){
				clientMsg("bonusEconomia", 5000);
				$mySelf.imperiais++;
			} else {
				clientMsg("bonusEconomiaMetabolismo", 5000);
				$mySelf.imperiais += 2;
			}
		}
	}
	atualizarImperiaisGui();
}

function clientAskPassarAVezAntesDoInicio(){
	commandToServer('passarAVezAntesDoInicio');	
}

function clientPauseGame(){
	clientPauseTurnoTimer();
	clientPushServercomDot();
}

function clientResumeGame(){
	clientResumeTurnoTimer();
	clientPopServerComDot();
}