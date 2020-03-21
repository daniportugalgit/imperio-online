// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientAjuda.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 30 de junho de 2009 21:25
//
// Editor             :  Codeweaver v. 1.2.3430.42233
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientAjudaGenericaLoggedIn(%titulo, %texto, %tamanhoDaJanela)
{
	loggedIn_pk_ajuda_txtML.setText("<just:center>" @ %texto @ "\n\n");
	ajudaPoker_titulo_txt.text = %titulo;
	ajuda_poker_tab.setVisible(true);
}

function clientPopAjudaGenericaLoggedIn()
{
	ajuda_poker_tab.setVisible(false);		
}

function clientAjuda_powerPlay()
{
	%titulo = "PARTIDA PERFEITA / POWERPLAY";
	//%texto = "ESTE CAMPO MARCA A SOMA  <color:FDFDD8>UNIÃO ENTRE IMPÉRIO E POKER, <color:FFFFFF>ONDE SÃO APOSTADAS FICHAS ESPECIAIS.\n\nCADA PERSONA RECEBE <color:FDFDD8>150 FICHAS INICIAIS COMO CORTESIA. <color:FFFFFF>QUANDO ESTAS TERMINAREM, É POSSÍVEL COMPRAR MAIS FICHAS COM <color:FDFDD8>CRÉDITOS OU OMNIS. <color:FFFFFF>\n\nAS FICHAS TAMBÉM PODEM SER <color:FDFDD8>REVENDIDAS POR CRÉDITOS <color:FFFFFF>(A PARTIR DE 1000 FICHAS).\n\nCADA CARTA DE POKER REPRESENTA UMA <color:FDFDD8>TECNOLOGIA DA ACADEMIA IMPERIAL, <color:FFFFFF>QUE É ADQUIRIDA TEMPORARIAMENTE QUANDO VOCÊ RECEBE A CARTA.\n\nÉ ALTAMENTE RECOMENDADO ASSISTIR AO VÍDEO-TUTORIAL NO SITE <color:FDFDD8>WWW.PROJETOIMPERIO.COM <color:FFFFFF>ANTES DE JOGAR SUA PRIMEIRA PARTIDA DE POKER IMPERIAL.");";
	%texto = "<color:FDFDD8>*NÃO VALE EM JOGOS COM HANDICAP* <color:FFFFFF>\nPARA FAZER UMA PARTIDA PERFEITA (POWERPLAY) VOCÊ PRECISA:\n\n- BATER ANTES DA DÉCIMA RODADA.\n\n- FAZER MAIS PONTOS QUE TODOS OS ADVERSÁRIOS.\n\n- CONSTRUIR UM IMPÉRIO.\n\n- COMPLETAR OS DOIS OBJETIVOS.\n\n- EFETUAR PELO MENOS UM ACORDO.\n\n- NÃO DECRETAR MORATÓRIAS.\n\n- NÃO ATACAR.\n\n- ADVERSÁRIOS NÃO PODEM TER IMPÉRIO.\n\nNO POKER IMPERIAL, POWERPLAY SIGNIFICA GANHAR A PARTIDA MESMO SEM TER O MELHOR JOGO DE POKER.";
	clientAjudaGenericaLoggedIn(%titulo, %texto, %tamanhoDaJanela);	
}