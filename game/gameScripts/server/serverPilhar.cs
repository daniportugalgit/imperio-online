// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\server\serverPilhar.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 16 de dezembro de 2008 13:50
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function serverVerificarPilhagem(%area, %novoDono){
	echo("->4");
	%simInfo = %area.pos0Quem.dono.mySimInfo;
	for(%i = 0; %i < %simInfo.getcount(); %i++){
		echo("X:%simInfo.getcount() = " @ %simInfo.getcount());
		%info = %simInfo.getObject(%i);
		echo("Verificando Info " @ %info.num @ ": %info.area.myName=" @ %info.area.myName);
		if(%info.area.myName $= %area.myName && %info.bonusPt > 0){
			echo("5->");
			serverPilhar(%info, %area.pos0Quem.dono, %novoDono);	
			%i = %simInfo.getcount(); //sai do loop;
		}
	}
}

function serverPilhar(%info, %antigoDono, %novoDono){
	echo("->6");
	%antigoDono.mySimInfo.remove(%info);
	%novoDono.mySimInfo.add(%info);
	%novoDono.minhasPilhagens++;
	commandToClient(%antigoDono.client, 'infoPilhada', "perdeu", %info.num);
	commandToClient(%novoDono.client, 'infoPilhada', "ganhou", %info.num);
}