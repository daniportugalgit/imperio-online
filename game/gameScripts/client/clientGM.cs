// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientGM.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 10 de março de 2008 20:10
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function GMgive(%praQuem, %imp, %min, %pet, %ura){
	commandToServer('GMAskGive', %praQuem, %imp, %min, %pet, %ura);
}

function GMdesastre(%tipo, %onde){
	commandToServer('GMAskDesastre', %tipo, %onde);	
}

function GMFinalizarPrizeNight(){
	commandToServer('finalizarPrizeNight');
}

function clientCmdResultPrizeNight(%resultString){
	echo("RESULTADO DA NOITE: " @ %resultString); 	
}

function GMcomprarCom100Omnis(%username, %pesquisaId, %liderNum){
	commandToServer('comprarCom100Omnis', %userName, %pesquisaId, %liderNum);	
}

function GMEntrar(%salaNum){
	commandToServer('EntrarNaSalaComoObservador', %salaNum);	
}

function GMMakeGulok(%userName){
	commandToServer('GMMakeGulok', %userName);
}

function clientCmdGMMakeGulok(){
	$myPersona.gulok = true;	
}

function GMsalaPoker(%blind, %apostaMax, %fichasIniciaisGlobal)
{
	commandToServer('GMsalaPoker', %blind, %apostaMax, %fichasIniciaisGlobal);	
}

function GMgiveTaxoPk_fichas(%userNome, %qtd)
{
	echo("FICHAS: " @ %qtd);
	commandToServer('GMgiveTaxoPk_fichas', %userNome, %qtd); 
}











//////////////////////////////////////////////////
//Godel:
function fatorial(%N){
	%num = %N;
	for(%i = 1; %i < %N; %i++){
		echo(%num @ " * " @ %N - %i);
		%num = %num * (%N - %i);
	}
		
	echo("FIM DO TESTE: " @ %N @ " fatorial = " @ %num);
	
	return %num;
}

function assombroso(%N, %original, %passos){
	%result = "DESASSOMBROSO";
	%num = %N;
	
	if (%num != 1){
		if(verificarImpar(%num)){
			//echo(%num @ " é ímpar; x3 +1...");
			%num *=	3;
			%num++;
		} else {
			//echo(%num @ " é par; /2...");
			%num /= 2;	
		}
	}
	if(%num == 1){
		%result = "ASSOMBROSO";	
		echo("TESTE FINALIZADO: " @ %original @ " é " @ %result @ "; Em " @ %passos @ " passos.");
	} else {
		%passos++;
		schedule(200, 0, "assombroso", %num, %original, %passos);	
	}
}

function verificarImpar(%num){
	%impar = false;
	%result1 = %num / 2;
	
	%exp = explode(%result1, ".");
	%exp = %exp.get[1];
	
	if(%exp !$= "0" && %exp !$= ""){
		%impar = true;	
	}
	
	//echo(%impar);
	return %impar;
}

function explode(%string, %char){
	%eval = "new ScriptObject(dados" @ $SO_explode @ ");";
	eval(%eval);
	%eval = "%myDados = dados" @ $SO_explode @ ";";
	eval(%eval);

	%explodeCount = 0;
	%lastFound = 0;

	%endChar = strLen(%string);	
	%charLen = strLen(%char);

	for(%i=0;%i<%endChar;%i++){
		%charToCheck = getSubStr(%string, %i, %charLen);
		if(%charToCheck $= %char){
			%myDados.get[%explodeCount] = getSubStr(%string, %lastFound, (%i-%lastFound)); 
			%lastFound = %i + %charLen;
			%explodeCount++;
		}	
	}

	
	%myDados.get[%explodeCount] = getSubStr(%string, %lastFound, (%i-%lastFound)); 
	%myDados.count = %explodeCount + 1;	

	$SO_explode++;
	return %myDados;
}


function verificarAssombrosoAteh(%num){
	for(%i = 1; %i < %num+1; %i++){
		assombroso(%i, %i, 1);
	}
	echo("FIM DO TESTE");
}