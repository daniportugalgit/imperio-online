// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientDinamicTxt.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quarta-feira, 24 de junho de 2009 17:27
//
// Editor             :  Codeweaver v. 1.2.3430.42233
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function setGlobalTxtVars()
{
	$TXT_planeta = "PLANETA";	
	$TXT_tipoDeJogo = "TIPO DE JOGO";	
	$TXT_turno = "TURNO";
	$TXT_lotacao = "LOTAÇÃO";
	$TXT_sala = "SALA";
	$TXT_tabuleiro = "TABULEIRO";
	$TXT_chat = "CHAT";
	$TXT_blind = "BLIND";
}
setGlobalTxtVars();
function clientSetDTxt_salaInside()
{
	salaInside_planeta_btn.text = $TXT_planeta;
	salaInside_tipoDeJogo_btn.text = $TXT_tipoDeJogo;
	salaInside_turno_btn.text = $TXT_turno;
	salaInside_lotacao_btn.text = $TXT_lotacao;
	salaInside_sala_txt.text = $TXT_sala;
	salaInside_tabuleiro_txt.text = $TXT_tabuleiro;
	salaInside_chat_txt.text = $TXT_chat;
}



