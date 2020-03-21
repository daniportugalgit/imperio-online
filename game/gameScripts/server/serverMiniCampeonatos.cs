// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverMiniCampeonatos.cs
// Copyright          :  
// Author             :  admin
// Created on         :  sexta-feira, 23 de janeiro de 2009 14:59
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


$miniCampeonatos_numGlobal = 0;
function serverCriarMiniCampeonato(%numDeParticipantes, %personasString){
	$miniCampeonatos_numGlobal++;
	%myNum = $miniCampeonatos_numGlobal;
	
	%eval = "$miniCampeonato" @ %myNum @ " = new ScriptObject(){};";
	eval(%eval); //cria um novo ScriptObject sem nada dentro;
	%eval = "%esteCampeonato = $miniCampeonato" @ %myNum @ ";"; 
	eval(%eval);//referencia dentro desta função, pela variável %esteCampeonato;
	
	%esteCampeonato.num = %myNum;
	%esteCampeonato.numDeParticipantes = %numDeParticipantes;
	
	for(%i = 0; %i < %numDeParticipantes; %i++){
		%eval = "%persona = getWord(%personasString, " @ %i @ ");";
		
		%eval = "%esteCampeonato.persona" @ %i+1 @ " = getWord(%personasString, " @ %i @ ");";
		eval(%eval);
	}
}