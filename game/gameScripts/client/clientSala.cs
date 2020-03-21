// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientSala.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quarta-feira, 29 de abril de 2009 14:38
//
// Editor             :  Codeweaver v. 1.2.3341.40715
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientSalaSetarTipoDeJogo(%sala, %num)
{
	if(%sala.emDuplas){
		//imagem de "Jogo Clássico" muda pra "Jogo Em Duplas":
		%tipoBitmap = "game/data/images/sala_emDuplas.png";
	} else {
		if(%sala.semPesquisas){
			%tipoBitmap = "game/data/images/sala_semPesquisas.png";
		} else if(%sala.handicap){
			%tipoBitmap = "game/data/images/sala_handicap.png";
		} else if(%sala.set){
			%tipoBitmap = "game/data/images/sala_set.png";
		} else if(%sala.poker){
			%tipoBitmap = "game/data/images/sala_poker.png";
		} else {
			%tipoBitmap = "game/data/images/sala_classico.png";
		}
	}
	%eval = "atrioSala" @ %num @ "Tipo_img.bitmap = %tipoBitmap;";
	eval(%eval);	
}

function clientSalaSetarPlaneta(%sala, %num)
{
	if(%sala.poker)
	{
		%planetaBitmap = "game/data/images/SALA_pk_" @ %sala.blind * 20 @ "_Overlay";
		%planetaTituloBitmap = "game/data/images/SALA_fichasTitulo";
	}
	else
	{
		%planetaBitmap = "game/data/images/SALA_" @ %sala.planeta @ "Overlay";
		%planetaTituloBitmap = "game/data/images/SALA_" @ %sala.planeta @ "Titulo";
	}
	
	%eval = "atrioSala" @ %num @ "PlanetaOverlay_img.bitmap = %planetaBitmap;";
	eval(%eval);
	%eval = "atrioSala" @ %num @ "PlanetaTitulo_img.bitmap = %planetaTituloBitmap;";
	eval(%eval);	
}

function clientSalaSetarDificuldade(%sala, %num)
{
	if($myPersona.taxoVitorias < 100){
		%myDifLimit = 50;
	} else if($myPersona.taxoVitorias >= 100 && $myPersona.taxoVitorias < 200){
		%myDifLimit = 55;
	} else {
		%myDifLimit = 60;
	}
	%myDifLimitNeg = %myDifLimit * -1;
	
	%sala.dificuldadeNum = $myPersona.myDif - %sala.mediaVit;
	if((%sala.dificuldadeNum / 2) > %myDifLimit){
		%sala.dificuldade = "Facilima";
	} else if(%sala.dificuldadeNum > %myDifLimit){
		%sala.dificuldade = "Facil";
	} else if (%sala.dificuldadeNum < %myDifLimitNeg){
		if((%sala.dificuldadeNum / 2) < %myDifLimitNeg){
			%sala.dificuldade = "Dificilima";	
		} else {
			%sala.dificuldade = "Dificil";
		}
	} else {
		%sala.dificuldade = "Competitiva";
	}
		
	if(%sala.semPesquisas || %sala.poker){
		%sala.dificuldade = "Competitiva";
	}
		
	clientClearSalaDifIcons(%num);
	%eval = "atrioSala" @ %num @ "Dif" @ %sala.dificuldade @ "_icon.setVisible(true);";
	eval(%eval);
}

function clientSalaSetarNumero(%sala, %num)
{
	%eval = "atrioSala" @ %num @ "Num_txt.text = %sala.num;";
	eval(%eval);	
}

function clientSalaMostrarNaTela(%num)
{
	%eval = "atrioSala" @ %num @ "_img.setVisible(true);";
	eval(%eval);	
}

function clientSalaSetarLotacao(%sala, %num)
{
	%lotacao = %sala.numDePlayers SPC "/" SPC %sala.lotacao;
	%eval = "atrioSala" @ %num @ "Lotacao_txt.text = %lotacao;";
	eval(%eval);	
}

function clientSalaSetarEntrarBtn(%sala, %num)
{
	if(%sala.emJogo == true)
	{
		%eval = "sala" @ %num @ "Entrar_btn.setVisible(false);";
		eval(%eval);
		return;
	}
	
	%eval = "sala" @ %num @ "Entrar_btn.setVisible(true);";
	eval(%eval);
	
	if(%sala.numDePlayers == %sala.lotacao)
	{
		%eval = "sala" @ %num @ "Entrar_btn.setActive(false);";
		eval(%eval);
		return;
	}
	
	%eval = "sala" @ %num @ "Entrar_btn.setActive(true);";
	eval(%eval);
	$salasAbertas++;
}