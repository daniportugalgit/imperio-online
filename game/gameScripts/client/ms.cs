// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\ms.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quinta-feira, 10 de julho de 2008 18:56
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
$useZeroBeforeSmallNumbers = true;

function initMsSystem()
{
	$msSorteios = new SimSet();
	$msNumbers = new SimSet();
	initMsNumbers();
	startMs();
}

function initMsNumbers()
{
	for(%i = 1; %i < 61; %i++)
	{
		%tempNum = %i;
		if(%tempNum < 10)
			%tempNum = "0" @ %tempNum;
			
		%eval = "$ms" @ %tempNum @ " = new ScriptObject(){num = " @ %tempNum @ ";qtd = 0;};";
		eval(%eval);
		%eval = "$msNumbers.add($ms" @ %tempNum @ ");";
		eval(%eval);
	}
}

function addMsSorteio(%num1, %num2, %num3, %num4, %num5, %num6)
{
	%eval = "$msSorteio" @ $msSorteios.getcount()+1 @ " = new ScriptObject(){};";
	eval(%eval);
	%eval = "$msSorteio" @ $msSorteios.getcount()+1 @ ".myNumbers = new SimSet();";
	eval(%eval);	
	
	%eval = "%mySorteio = $msSorteio" @ $msSorteios.getcount()+1 @ ";";
	eval(%eval);
	
	%mySorteio.num = $msSorteios.getcount()+1;
	
	echo("      |-> adding numbers to array mySorteio" @ %mySorteio.num);
	
	%eval = "%mySorteio.myNumbers.add($ms" @ %num1 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($ms" @ %num2 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($ms" @ %num3 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($ms" @ %num4 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($ms" @ %num5 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($ms" @ %num6 @ ");";
	eval(%eval);
	echo("      |-> numbers added to array mySorteio" @ %mySorteio.num);
	echo("            |-> adding array to $msSorteios array...");
	$msSorteios.add(%mySorteio);
	echo("            |-> array added to $msSorteios array...");
}

function calcularMsNumberPool(){
	for(%i = 0; %i < $msSorteios.getCount(); %i++)
	{
		%mySorteio = $msSorteios.getObject(%i);
		
		for(%j = 0; %j < 6; %j++)
		{
			%myNum = %mySorteio.myNumbers.getObject(%j).num;
			if(%myNum < 10)
				%myNum = "0" @ %myNum;
			
			echo("SORTEADO: " @ %myNum); 
			%eval = "$ms" @ %myNum @ ".qtd++;";
			eval(%eval);
		}
	}
}

function startMs(){
	%file = new FileObject();
	
	%file.openForRead("game/gamescripts/server/msR2.txt");	
	for(%i = 1; %i < 1110; %i++)
	{
		%myJogo = %file.readLine();
		
		%myNum1 = getWord(%myJogo, 2);
		%myNum2 = getWord(%myJogo, 3);
		%myNum3 = getWord(%myJogo, 4);
		%myNum4 = getWord(%myJogo, 5);
		%myNum5 = getWord(%myJogo, 6);
		%myNum6 = getWord(%myJogo, 7);
		
		if(%myNum1 < 10)
			%myNum1 = "0" @ %myNum1;
		
		if(%myNum2 < 10)
			%myNum2 = "0" @ %myNum2;
		
		if(%myNum3 < 10)
			%myNum3 = "0" @ %myNum3;
		
		if(%myNum4 < 10)
			%myNum4 = "0" @ %myNum4;
		
		if(%myNum5 < 10)
			%myNum5 = "0" @ %myNum5;
		
		if(%myNum6 < 10)
			%myNum6 = "0" @ %myNum6;
		
		echo("Trying to add sorteio #" @ getWord(%myJogo, 0));
		addMsSorteio(%myNum1, %myNum2, %myNum3, %myNum4, %myNum5, %myNum6);
		echo("Sorteio adicionado com sucesso: " @ %myNum1 SPC %myNum2 SPC %myNum3 SPC %myNum4 SPC %myNum5 SPC %myNum6);
		echo("...............");
	}
	%file.close();
	%file.delete();
	
	calcularMsNumberPool();
	echo("FREQUÊNCIA DE CADA NÚMERO:");
	showNumberFreq(150);
	echo("/////////////////////");
	showNumbersMaisDistantes(20);
	echo("/////////////////////");
	echo("CRUZANDO DADOS:");
	msCruzarDados(10);
}

function showNumberFreq(%max){
	%maxSimSet = new SimSet();
	for(%i = 1; %i < 61; %i++){
		%myMsNum = $msNumbers.getObject(%i-1);
		echo("O número " @ %i @ " já foi sorteado " @ %myMsNum.qtd @ " vezes.");
		if(%myMsNum.qtd < %max){
			%maxSimSet.add(%myMsNum);
		} else {
			if(%maxSimSet.isMember(%myMsNum)){
				%maxSimSet.remove(%myMsNum);
			}
		}
	}
	
	echo("NÚMEROS QUE FORAM SORTEADOS MENOS DE " @ %max @ " VEZES:");
	for(%j = 0; %j < %maxSimSet.getCount(); %j++){
		echo(%maxSimSet.getObject(%j).num @ " (" @ %maxSimSet.getObject(%j).qtd @ " vezes)");	
		%myMsNum = %maxSimSet.getObject(%j);
		%sorteiosCount = $msSorteios.getCount();
		for(%m = 0; %m < %sorteiosCount; %m++)
		{
			%mySorteio = $msSorteios.getObject(%m);
			if(%mySorteio.myNumbers.isMember(%myMsNum))
			{
				if(%myMsNum.lastSorteio.num < %mySorteio.num || %myMsNum.lastSorteio.num $= "")
				{
					%myMsNum.lastSorteio = %mySorteio;
					%myMsNum.myDist = %sorteiosCount - %mySorteio.num;
				}
			}
		}
		echo("O número " @ %myMsNum.num @ " foi sorteado " @ %myMsNum.myDist @ " sorteios atrás.");
	}
}

function showNumbersMaisDistantes(%min){
	%distSimSet = new SimSet();
	for(%i = 1; %i < 61; %i++)
	{
		%myMsNum = $msNumbers.getObject(%i-1);
		if(%myMsNum.myDist > %min)
		{
			%distSimSet.add(%myMsNum);
		}
		else
		{
			if(%distSimSet.isMember(%myMsNum))
				%distSimSet.remove(%myMsNum);
		}
	}
	
	echo("NÚMEROS QUE FORAM SORTEADOS A MAIS DE " @ %min @ " SORTEIOS:");
	for(%j = 0; %j < %distSimSet.getCount(); %j++)
	{
		%myMsNum = %distSimSet.getObject(%j);
		echo("O número " @ %myMsNum.num @ " foi sorteado " @ %myMsNum.myDist @ " sorteios atrás.");
	}
}

function msCruzarDados(%palpiteSize){
	//cria um simSet que vai guardar os melhores números:
	%melhoresNumeros = new SimSet(); 
	
	//popula um simset provisório com todos os números:
	%todosOsNumeros = new SimSet();	
	for(%i = 0; %i < $msNumbers.getCount(); %i++)
	{
		%number = $msNumbers.getObject(%i);
		%todosOsNumeros.add(%number);
	}
	
	for(%n = 0; %n < %palpiteSize; %n++)
	{
		for(%i = 0; %i < %todosOsNumeros.getCount(); %i++)
		{
			%newNumber = %todosOsNumeros.getObject(%i);
			if(isObject(%melhoresNumeros.getObject(%n)))
			{
				%oldNumber = %melhoresNumeros.getObject(%n);
				if(%newNumber.qtd < %oldNumber.qtd)
				{
					%melhoresNumeros.remove(%oldNumber);	
					%melhoresNumeros.add(%newNumber);	
				}
			} else {
				%melhoresNumeros.add(%newNumber);	
			}
		}
	}
	
	for(%n = 0; %n < %palpiteSize; %n++){
		for(%i = 0; %i < %todosOsNumeros.getCount(); %i++){
			%newNumber = %todosOsNumeros.getObject(%i);
			if(isObject(%melhoresNumeros.getObject(%n + %palpiteSize))){
				%oldNumber = %melhoresNumeros.getObject(%n + %palpiteSize);
				if(%newNumber.myDist > %oldNumber.myDist){
					%melhoresNumeros.remove(%oldNumber);	
					%melhoresNumeros.add(%newNumber);	
					%todosOsNumeros.remove(%newNumber);	
					%todosOsNumeros.add(%oldNumber);	
				}
			} else {
				%melhoresNumeros.add(%newNumber);	
				%todosOsNumeros.remove(%newNumber);	
			}
		}
	}
	
	for(%i = 0; %i < %melhoresNumeros.getCount(); %i++)
	{
		%lastNumber = %melhoresNumeros.getObject(%i);
		echo(":: " @ %lastNumber.num @ " :: sorteado " @ %lastNumber.qtd @ " vezes :: " @ "sorteado há " @ %lastNumber.myDist @ " sorteios.");
	}
}
/////////////////////////////////////////
///////////////////////////
//Teste de sorteios:
function MSTestPalpite(%tentativas, %num1, %num2, %num3, %num4, %num5, %num6)
{
	$n[1] = %num1;
	$n[2] = %num2;
	$n[3] = %num3;
	$n[4] = %num4;
	$n[5] = %num5;
	$n[6] = %num6;
	
	$myPalpiteTentativas = 0;
	MSsortearAtehGanhar(%tentativas);
}

function MSsortearAtehGanhar(%maxTentativas){
	if(isObject($todosOsMSNumeros))
	{
		$todosOsMSNumeros.clear();
	}
	else
	{
		$todosOsMSNumeros = new SimSet();	
	}
	for(%i = 0; %i < $msNumbers.getCount(); %i++)
	{
		%number = $msNumbers.getObject(%i);
		$todosOsMSNumeros.add(%number);
	}
	
	$myPalpiteTentativas++;	
	for(%i = 1; %i <= 6; %i++)
		%n[%i] = MSdado();
	
	for(%i = 1; %i <= 6; %i++)
	{
		for(%j = 1; %j <= 6; %j++)
		{	
			if($n[%i] $= %n[%j])
			{
				%acertos++;
				%n[%j].right = "*";
				%j = 7;
			}
		}
	}
	
	echo("TENTATIVA " @ $myPalpiteTentativas @ ": " @ %acertos @ " ACERTOS::(" @ %n[1] @ %n[1].right SPC %n[2] @ %n[2].right SPC %n[3] @ %n[3].right SPC %n[4] @ %n[4].right SPC %n[5] @ %n[5].right SPC %n[6] @ %n[6].right @ ")");
	if(%acertos < 6)
	{
		%eval = "$acertos" @ %acertos @ "++;";
		eval(%eval);
		if($myPalpiteTentativas < %maxTentativas)
		{
			if(!$stopMS)
				schedule(10, 0, "MSSortearAtehGanhar", %maxTentativas);		
		}
		else
		{
			echo("Após " @ %maxTentativas @ " sorteios, nenhum jackpot acertado:");
			echo("1 Acertos: " @ $acertos1);	
			echo("2 Acertos: " @ $acertos2);	
			echo("3 Acertos: " @ $acertos3);	
			echo("4 Acertos: " @ $acertos4);	
			echo("5 Acertos: " @ $acertos5);	
			echo("6 Acertos: " @ $acertos6);	
		}
	}
	else
	{
		echo("***************JACKPOT!!!!!*****************");
		echo("      |-> Após " @ $myPalpiteTentativas @ " sorteios, ganhou na Mega-Sena!");
		echo("1 Acertos: " @ $acertos1);	
		echo("2 Acertos: " @ $acertos2);	
		echo("3 Acertos: " @ $acertos3);	
		echo("4 Acertos: " @ $acertos4);	
		echo("5 Acertos: " @ $acertos5);	
		echo("6 Acertos: " @ $acertos6);	
	}
}

function MSdado()
{
	%result = getRandom(0, $todosOsMSNumeros.getCount()-1);
	%myLFNum = $todosOsMSNumeros.getObject(%result);
	%myNum = %myLfNum.num;
	$todosOsMSNumeros.remove(%myLfNum);
	
	return(%myNum);
}


////////////////////////////////////////
///////////////
//LF:
function initLFSystem(){
	$LFSorteios = new SimSet();
	$LFNumbers = new SimSet();
	initLFNumbers();
}

function initLFNumbers(){
	for(%i = 1; %i < 26; %i++){
		%eval = "$LF" @ %i @ " = new ScriptObject(){num = " @ %i @ ";qtd = 0;};";
		eval(%eval);
		%eval = "$LFNumbers.add($LF" @ %i @ ");";
		eval(%eval);
	}
}

function addLFSorteio(%num1, %num2, %num3, %num4, %num5, %num6, %num7, %num8, %num9, %num10, %num11, %num12, %num13, %num14, %num15){
	%eval = "$LFSorteio" @ $LFSorteios.getcount()+1 @ " = new ScriptObject(){};";
	eval(%eval);
	%eval = "$LFSorteio" @ $LFSorteios.getcount()+1 @ ".myNumbers = new SimSet();";
	eval(%eval);	
	
	%eval = "%mySorteio = $LFSorteio" @ $LFSorteios.getcount()+1 @ ";";
	eval(%eval);
	
	%mySorteio.num = $LFSorteios.getcount()+1;
	
	%eval = "%mySorteio.myNumbers.add($LF" @ %num1 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num2 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num3 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num4 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num5 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num6 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num7 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num8 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num9 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num10 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num11 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num12 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num13 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num14 @ ");";
	eval(%eval);
	%eval = "%mySorteio.myNumbers.add($LF" @ %num15 @ ");";
	eval(%eval);
		
	$LFSorteios.add(%mySorteio);
}

function calcularLFNumberPool(){
	for(%i = 0; %i < $LFSorteios.getCount(); %i++){
		%mySorteio = $LFSorteios.getObject(%i);
		for(%j = 0; %j < 6; %j++){
			%myNum = %mySorteio.myNumbers.getObject(%j).num;
			echo("SORTEADO: " @ %myNum); 
			%eval = "$LF" @ %myNum @ ".qtd++;";
			eval(%eval);
		}
	}
}

function startLF(){
	%file = new FileObject();
	
	%file.openForRead("game/gamescripts/server/LFR.txt");	
	for(%i = 1; %i < 349; %i++){
		%myJogo = %file.readLine();
		
		%myNum1 = getWord(%myJogo, 2);
		%myNum2 = getWord(%myJogo, 3);
		%myNum3 = getWord(%myJogo, 4);
		%myNum4 = getWord(%myJogo, 5);
		%myNum5 = getWord(%myJogo, 6);
		%myNum6 = getWord(%myJogo, 7);
		%myNum7 = getWord(%myJogo, 8);
		%myNum8 = getWord(%myJogo, 9);
		%myNum9 = getWord(%myJogo, 10);
		%myNum10 = getWord(%myJogo, 11);
		%myNum11 = getWord(%myJogo, 12);
		%myNum12 = getWord(%myJogo, 13);
		%myNum13 = getWord(%myJogo, 14);
		%myNum14 = getWord(%myJogo, 15);
		%myNum15 = getWord(%myJogo, 16);
		addLFSorteio(%myNum1, %myNum2, %myNum3, %myNum4, %myNum5, %myNum6, %myNum7, %myNum8, %myNum9, %myNum10, %myNum11, %myNum12, %myNum13, %myNum14, %myNum15);
		echo(%myNum1 SPC %myNum2 SPC %myNum3 SPC %myNum4 SPC %myNum5 SPC %myNum6 SPC %myNum7 SPC %myNum8 SPC %myNum9 SPC %myNum10 SPC %myNum11 SPC %myNum12 SPC %myNum13 SPC %myNum14 SPC %myNum15);
	}
	%file.close();
	%file.delete();
	
	calcularLFNumberPool();
	showLFNumberFreq(100);
}

function showLFNumberFreq(%max){
	%maxSimSet = new SimSet();
	for(%i = 1; %i < 26; %i++){
		%myLFNum = $LFNumbers.getObject(%i-1);
		echo("O número " @ %i @ " já foi sorteado " @ %myLFNum.qtd @ " vezes.");
		if(%myLFNum.qtd < %max){
			%maxSimSet.add(%myLFNum);
		} else {
			if(%maxSimSet.isMember(%myLFNum)){
				%maxSimSet.remove(%myLFNum);
			}
		}
	}
	
	echo("NÚMEROS QUE FORAM SORTEADOS MENOS DE " @ %max @ " VEZES:");
	for(%j = 0; %j < %maxSimSet.getCount(); %j++){
		echo(%maxSimSet.getObject(%j).num @ " (" @ %maxSimSet.getObject(%j).qtd @ " vezes)");	
		%myLFNum = %maxSimSet.getObject(%j);
		%sorteiosCount = $LFSorteios.getCount();
		for(%m = 0; %m < %sorteiosCount; %m++){
			%mySorteio = $LFSorteios.getObject(%m);
			if(%mySorteio.myNumbers.isMember(%myLFNum)){
				if(%myLFNum.lastSorteio.num < %mySorteio.num || %myLFNum.lastSorteio.num $= ""){
					%myLFNum.lastSorteio = %mySorteio;
					%myLFNum.myDist = %sorteiosCount - %mySorteio.num;
				}
			}
		}
		echo("O número " @ %myLFNum.num @ " foi sorteado " @ %myLFNum.myDist @ " sorteios atrás.");
	}
}

function showLFNumbersMaisDistantes(%min){
	%distSimSet = new SimSet();
	for(%i = 1; %i < 26; %i++){
		%myLFNum = $LFNumbers.getObject(%i-1);
		if(%myLFNum.myDist > %min){
			%distSimSet.add(%myLFNum);
		} else {
			if(%distSimSet.isMember(%myLFNum)){
				%distSimSet.remove(%myLFNum);
			}
		}
	}
	
	echo("NÚMEROS QUE FORAM SORTEADOS A MAIS DE " @ %min @ " SORTEIOS:");
	for(%j = 0; %j < %distSimSet.getCount(); %j++){
		%myLFNum = %distSimSet.getObject(%j);
		echo("O número " @ %myLFNum.num @ " foi sorteado " @ %myLFNum.myDist @ " sorteios atrás.");
	}
}

function LFCruzarDados(%palpiteSize){
	//cria um simSet que vai guardar os melhores números:
	%melhoresNumeros = new SimSet(); 
	
	//popula um simset provisório com todos os números:
	%todosOsNumeros = new SimSet();	
	for(%i = 0; %i < $LFNumbers.getCount(); %i++){
		%number = $LFNumbers.getObject(%i);
		%todosOsNumeros.add(%number);
	}
	
	for(%n = 0; %n < %palpiteSize; %n++){
		for(%i = 0; %i < %todosOsNumeros.getCount(); %i++){
			%newNumber = %todosOsNumeros.getObject(%i);
			if(isObject(%melhoresNumeros.getObject(%n))){
				%oldNumber = %melhoresNumeros.getObject(%n);
				if(%newNumber.qtd < %oldNumber.qtd){
					%melhoresNumeros.remove(%oldNumber);	
					%melhoresNumeros.add(%newNumber);	
				}
			} else {
				%melhoresNumeros.add(%newNumber);	
			}
		}
	}
	
	for(%n = 0; %n < %palpiteSize; %n++){
		for(%i = 0; %i < %todosOsNumeros.getCount(); %i++){
			%newNumber = %todosOsNumeros.getObject(%i);
			if(isObject(%melhoresNumeros.getObject(%n + %palpiteSize))){
				%oldNumber = %melhoresNumeros.getObject(%n + %palpiteSize);
				if(%newNumber.myDist > %oldNumber.myDist){
					%melhoresNumeros.remove(%oldNumber);	
					%melhoresNumeros.add(%newNumber);	
					%todosOsNumeros.remove(%newNumber);	
					%todosOsNumeros.add(%oldNumber);	
				}
			} else {
				%melhoresNumeros.add(%newNumber);	
				%todosOsNumeros.remove(%newNumber);	
			}
		}
	}
	
	for(%i = 0; %i < %melhoresNumeros.getCount(); %i++){
		%lastNumber = %melhoresNumeros.getObject(%i);
		echo(":: " @ %lastNumber.num @ " :: sorteado " @ %lastNumber.qtd @ " vezes :: " @ "sorteado há " @ %lastNumber.myDist @ " sorteios.");
	}
}

//LFTestPalpite(1, 2, 3, 4, 6, 9, 10, 11, 13, 15, 16, 18, 20, 22, 24)
//LFTestPalpite(4, 6, 7, 11, 13, 17, 18, 20, 21, 9, 2, 23, 8, 22, 24)

function LFTestPalpite(%num1, %num2, %num3, %num4, %num5, %num6, %num7, %num8, %num9, %num10, %num11, %num12, %num13, %num14, %num15){
	$n[1] = %num1;
	$n[2] = %num2;
	$n[3] = %num3;
	$n[4] = %num4;
	$n[5] = %num5;
	$n[6] = %num6;
	$n[7] = %num7;
	$n[8] = %num8;
	$n[9] = %num9;
	$n[10] = %num10;
	$n[11] = %num11;
	$n[12] = %num12;
	$n[13] = %num13;
	$n[14] = %num14;
	$n[15] = %num15;
	$myPalpiteTentativas = 0;
	LFsortearAtehGanhar();
}

function LFSortearAtehGanhar(){
	if(isObject($todosOsLFNumeros)){
		$todosOsLFNumeros.clear();
	} else {
		$todosOsLFNumeros = new SimSet();	
	}
	for(%i = 0; %i < $LFNumbers.getCount(); %i++){
		%number = $LFNumbers.getObject(%i);
		$todosOsLFNumeros.add(%number);
	}
	
	$myPalpiteTentativas++;	
	for(%i = 1; %i < 16; %i++){
		%n[%i] = LFdado();
	}
	
	for(%i = 1; %i < 16; %i++){
		for(%j = 1; %j < 16; %j++){	
			if($n[%i] $= %n[%j]){
				%acertos++;
				%n[%j].right = "*";
				%j = 16;
			}
		}
	}
	
	echo("TENTATIVA " @ $myPalpiteTentativas @ ": " @ %acertos @ " ACERTOS::(" @ %n[1] @ %n[1].right SPC %n[2] @ %n[2].right SPC %n[3] @ %n[3].right SPC %n[4] @ %n[4].right SPC %n[5] @ %n[5].right SPC %n[6] @ %n[6].right SPC %n[7] @ %n[7].right SPC %n[8] @ %n[8].right SPC %n[9] @ %n[9].right SPC %n[10] @ %n[10].right SPC %n[11] @ %n[11].right SPC %n[12] @ %n[12].right SPC %n[13] @ %n[13].right SPC %n[14] @ %n[14].right SPC %n[15] @ %n[15].right @ ")");
	if(%acertos > 4){
		%eval = "$acertos" @ %acertos @ "++;";
		eval(%eval);
		if($myPalpiteTentativas < 200000){
			if(!$stopLF){
				schedule(10, 0, "LFSortearAtehGanhar");		
			}
		} else {
			echo("5 Acertos: " @ $acertos5);	
			echo("6 Acertos: " @ $acertos6);	
			echo("7 Acertos: " @ $acertos7);	
			echo("8 Acertos: " @ $acertos8);	
			echo("9 Acertos: " @ $acertos9);	
			echo("10 Acertos: " @ $acertos10);	
			echo("11 Acertos: " @ $acertos11);	
			echo("12 Acertos: " @ $acertos12);	
			echo("13 Acertos: " @ $acertos13);	
			echo("14 Acertos: " @ $acertos14);	
			echo("15 Acertos: " @ $acertos15);	
		}
	} else {
		echo("**************ERRO***************>> 4 Acertos << Encerrando programa.");	
	}
}

function LFdado(){
	%result = getRandom(0, $todosOsLFNumeros.getCount()-1);
	%myLFNum = $todosOsLFNumeros.getObject(%result);
	%myNum = %myLfNum.num;
	$todosOsLFNumeros.remove(%myLfNum);
	
	return(%myNum);
}









//////////////////////////
//função do lex pra conseguir o tempo que um objeto longo demora pra passar por um espaço - uma ponte, ou algo q o valha:

function getDeltaS(%tamanhoDaPonte, %tamanhoDoTrem)
{
	return %tamanhoDoTrem + %tamanhoDaPonte;
}

function getDeltaT(%deltaS, %vel)
{
	return (%deltaS / %vel) * 3600;	
}

//input em Quilômetros; output em segundos;
function getTempoPorEspaco(%tamanhoDoEspaco, %tamanhoDoObjeto, %vel)
{
	%deltaS = getDeltaS(%tamanhoDoEspaco, %tamanhoDoObjeto);
	%deltaT = getDeltaT(%deltaS, %vel);
		
	return %deltaT;
}




initMsSystem();