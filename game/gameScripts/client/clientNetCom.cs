// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientNetCom.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 17 de junho de 2008 1:12
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientPegarMinhaFoto(){
	%largura = 185;
	
	new TCPObject(tcpObjGet);
	$userAgent = "Torque/1.7";
	$serverURL = "/imperio/torque/obter-foto/" @ $pref::Player::name @ "?largura=" @ %largura;
		
	tcpObjGet.connect("www.taxo.com.br:80");
}

function tcpObjGet::onConnected(%this){
	%this.send("GET " @ $serverURL @ " HTTP/1.0\nHost: www.taxo.com.br\nUser-Agent: " @ $userAgent @ "\n\r\n\r\n");
	echo ("Conectado: www.taxo.com.br" @ $serverURL);
}

function tcpObjGet::onLine(%this, %dados){
	if (getWord(%dados, 0) $= "imagem"){ 
		echo("DADOS(RAW) DA FOTO RECEBIDOS: " @ %dados);
		gravarMinhaFoto(%dados);
	}
	
}

function gravarMinhaFoto(%dados){
	%i = 0;
	%chave = "inicio";
	
	if (%chave !$= "fim"){
		%chave = getWord(%dados, %i);
		%valor = getWord(%dados, %i+1);

		if (%chave $= "imagem"){
			%fotoRaw = %valor;
			clientRecordFoto(%fotoRaw);
		}
	}
}

function clientRecordFoto(%dadosRAW){
	%file = new FileObject();
	%file.openForWrite("game/data/files/minhaFoto.jpg"); 
	%file.writeLine(%dadosRAW); //
	%file.close(); //fecha o arquivo
	%file.delete(); //deleta o arquivo da memória RAM, não do HD;	
	setarMinhaFoto();
}

function setarMinhaFoto(){
	loggedInMinhaFoto_img.bitmap = "game/data/files/minhaFoto.jpg";	
	//loggedInMinhaFoto_img.bitmap = expandFilename("game/data/files/minhaFoto.jpg");	
}