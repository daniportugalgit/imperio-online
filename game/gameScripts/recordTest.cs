// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\recordTest.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quinta-feira, 15 de novembro de 2007 21:26
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function testRecord(%nomeDoArquivo, %dados){
	%file = new FileObject();
	%file.openForWrite("game/data/files/" @ %nomeDoArquivo @ ".sav"); //escreve direto o resultado da primeira partida
	%file.writeLine(%dados); // [JOGOS] é o primeiro
	%file.close(); //fecha o arquivo.sav
	%file.delete(); //deleta o arquivo da memória RAM, não do HD;
}

function serverRecord(%nomeDoArquivo, %vitoria, %pontos){
	%file = new FileObject();
	
	if(!isFile("game/data/files/" @ %nomeDoArquivo @ ".sav")){
		%file.openForWrite("game/data/files/" @ %nomeDoArquivo @ ".sav"); //escreve direto o resultado da primeira partida
		%file.writeLine("1"); // [JOGOS] é o primeiro
		%file.writeLine(%vitoria); // [VITORIAS] é a primeira, se ganhou, marca um, senão, marca zero;
		%file.writeLine(%pontos); // [TOTAL DE PONTOS] é o primeiro jogo, o total de pontos é igual ao número de pontos marcados nesta partida.
		%file.writeLine(%pontos); // [MAX DE PONTOS EM UMA PARTIDA] é o primeiro jogo, o máximo é igual ao número de pontos marcados nesta partida.
		
		%file.close(); //fecha o arquivo.sav
		%file.delete(); //deleta o arquivo da memória RAM, não do HD;
	} else { //se o arquivo já existia, então tem que ler e sobreescrever:
		%file.openForRead("game/data/files/" @ %nomeDoArquivo @ ".sav");	
		%myJogos = %file.readLine();
		%myVitorias = %file.readLine();
		%myPontos = %file.readLine();
		%myMaxPontos = %file.readLine();
		%file.close(); //fechei para poder abrir para escrever.
		
		//variáveis temporárias atualizadas:
		%myJogos += 1; //soma 1 ao contador de partidas disputadas;
		%myVitorias += %vitoria; //soma 0 ou 1 ao contador de vitórias
		%myPontos += %pontos; //soma os pontos marcados nesta partida ao contador total de pontos
		if(%pontos > %myMaxPontos){
			%myMaxPontos = %pontos; //se bateu seu próprio recorde, marca, do contrário, mantém o número anterior. 	
		}
		
		%file.openForWrite("game/data/files/" @ %nomeDoArquivo @ ".sav"); //abre para escrever, apagando o que havia antes
		//atualiza o arquivo.sav:
		%file.WriteLine(%myJogos);
		%file.WriteLine(%myVitorias);
		%file.WriteLine(%myPontos);
		%file.WriteLine(%myMaxPontos);
		%file.close(); //fecha o arquivo
		%file.delete(); //apaga da memória RAM
	}
}


function serverWriteURLToFile(%url){
	%file = new FileObject();
	
	%file.openForAppend("game/data/files/url.txt"); //abre para adicionar texto
	//atualiza o arquivo:
	%file.WriteLine(%url);
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM
}

function serverWriteURLToBugFile(%url){
	%file = new FileObject();
	
	%file.openForAppend("game/data/files/url_BUG.txt"); //abre para adicionar texto
	//atualiza o arquivo:
	%file.WriteLine(%url);
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM
}

function serverGravarFalha_jogo(%jogo){
	%file = new FileObject();
	
	%file.openForAppend("game/data/files/falhas_jogo.txt"); //abre para adicionar texto
	//atualiza o arquivo:
	%file.WriteLine(%jogo.iniciarUrl SPC %jogo.finalizarUrl);
	%file.close(); //fecha o arquivo
	%file.delete(); //apaga da memória RAM
}
