// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientCruzarOceanos.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  quinta-feira, 10 de janeiro de 2008 14:45
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function clientClearCruzarFlechas(){
	if($planetaAtual $= "Terra"){
		cruzarFlechabEstreitoDeBering.setVisible(false);
		cruzarFlechaOceanoPacificoOcidental.setVisible(false);
		cruzarFlechabMarChukchi.setVisible(false);
		cruzarFlechaOceanoPacificoOriental.setVisible(false);
		cruzarFlechabMarDeCoral.setVisible(false);
		cruzarFlechaOceanoIndico.setVisible(false);
	} else if($planetaAtual $= "Ungart"){
		cruzarFlechaUNG_b29.setVisible(false);
		cruzarFlechaUNG_b30.setVisible(false);
		cruzarFlechaUNG_b31.setVisible(false);
		cruzarFlechaUNG_b32.setVisible(false);
		cruzarFlechaUNG_b33.setVisible(false);
		cruzarFlechaUNG_b03.setVisible(false);
		cruzarFlechaUNG_b04.setVisible(false);
		cruzarFlechaUNG_b05.setVisible(false);
		cruzarFlechaUNG_b06.setVisible(false);
		cruzarFlechaUNG_b07.setVisible(false);
	}  else if($planetaAtual $= "Teluria"){
		cruzarFlechaTEL_b34.setVisible(false);
		cruzarFlechaTEL_b33.setVisible(false);
		cruzarFlechaTEL_b32.setVisible(false);
		cruzarFlechaTEL_b31.setVisible(false);
		cruzarFlechaTEL_b30.setVisible(false);
		cruzarFlechaTEL_b14.setVisible(false);
		cruzarFlechaTEL_b13.setVisible(false);
		cruzarFlechaTEL_b12.setVisible(false);
		cruzarFlechaTEL_b11.setVisible(false);
		cruzarFlechaTEL_b10.setVisible(false);
	}
	$cruzarDe = "nada";
}
	
function clientLigarFlechas(%ondeEstou){
	clientClearCruzarFlechas();
	
	if($jogadorDaVez == $mySelf){
		if(isObject(%ondeEstou.myCruzarFronteiras)){
			$cruzarDe = %ondeEstou.getName();
			for(%i = 0; %i < %ondeEstou.myCruzarFronteiras.getcount(); %i++){
				%fronteira = %ondeEstou.myCruzarFronteiras.getObject(%i);
				if(%fronteira.dono == $mySelf || %fronteira.dono $= "0" || %fronteira.oceano $= "1"){
					clientLigarFlechaCruzar(%ondeEstou.myCruzarFronteiras.getObject(%i).getName());	
				}
			}
		} else {
			$cruzarDe = "nada";
		}
	}
}

function clientLigarFlechaCruzar(%onde){
	%eval = "%myCruzarFlecha = cruzarFlecha" @ %onde @ ";";
	eval(%eval);
	%myAnimation = "flecha" @ %myCruzarFlecha.side @ "Animation";
		
	%myCruzarFlecha.setVisible(true);
	%myCruzarFlecha.playAnimation(%myAnimation);
}


function setCruzarOceanosTerra(){
	bEstreitoDeBering.myCruzarFronteiras = new SimSet();
	bEstreitoDeBering.myCruzarFronteiras.add(bMarChukchi);
	bEstreitoDeBering.borda = true;
	
	oceanoPacificoOcidental.myCruzarFronteiras = new SimSet();
	oceanoPacificoOcidental.myCruzarFronteiras.add(oceanoPacificoOriental);
	oceanoPacificoOcidental.myCruzarFronteiras.add(bMarDeCoral);
	oceanoPacificoOcidental.myCruzarFronteiras.add(oceanoIndico);
	oceanoPacificoOcidental.borda = true;
	
	bMarChukchi.myCruzarFronteiras = new SimSet();
	bMarChukchi.myCruzarFronteiras.add(bEstreitoDeBering);
	bMarChukchi.borda = true;
	
	oceanoPacificoOriental.myCruzarFronteiras = new SimSet();
	oceanoPacificoOriental.myCruzarFronteiras.add(oceanoPacificoOcidental);
	oceanoPacificoOriental.borda = true;
	
	bMarDeCoral.myCruzarFronteiras = new SimSet();
	bMarDeCoral.myCruzarFronteiras.add(oceanoPacificoOcidental);
	bMarDeCoral.borda = true;
	
	oceanoIndico.myCruzarFronteiras = new SimSet();
	oceanoIndico.myCruzarFronteiras.add(oceanoPacificoOcidental);
	oceanoIndico.borda = true;
}

function setCruzarOceanosUngart(){
	//Ocidente:
	UNG_b29.myCruzarFronteiras = new SimSet();
	UNG_b29.myCruzarFronteiras.add(UNG_b04);
	UNG_b29.borda = true;
	
	UNG_b30.myCruzarFronteiras = new SimSet();
	UNG_b30.myCruzarFronteiras.add(UNG_b03);
	UNG_b30.borda = true;
	
	UNG_b31.myCruzarFronteiras = new SimSet();
	UNG_b31.myCruzarFronteiras.add(UNG_b05);
	UNG_b31.borda = true;
	
	UNG_b32.myCruzarFronteiras = new SimSet();
	UNG_b32.myCruzarFronteiras.add(UNG_b06);
	UNG_b32.borda = true;
	
	UNG_b33.myCruzarFronteiras = new SimSet();
	UNG_b33.myCruzarFronteiras.add(UNG_b07);
	UNG_b33.borda = true;
	
	
	//Oriente:
	UNG_b04.myCruzarFronteiras = new SimSet();
	UNG_b04.myCruzarFronteiras.add(UNG_b29);
	UNG_b04.borda = true;
	
	UNG_b03.myCruzarFronteiras = new SimSet();
	UNG_b03.myCruzarFronteiras.add(UNG_b30);
	UNG_b03.borda = true;
	
	UNG_b05.myCruzarFronteiras = new SimSet();
	UNG_b05.myCruzarFronteiras.add(UNG_b31);
	UNG_b05.borda = true;
	
	UNG_b06.myCruzarFronteiras = new SimSet();
	UNG_b06.myCruzarFronteiras.add(UNG_b32);
	UNG_b06.borda = true;
	
	UNG_b07.myCruzarFronteiras = new SimSet();
	UNG_b07.myCruzarFronteiras.add(UNG_b33);
	UNG_b07.borda = true;
}

function setCruzarOceanosTeluria(){
	//Ocidente:
	TEL_b34.myCruzarFronteiras = new SimSet();
	TEL_b34.myCruzarFronteiras.add(TEL_b12);
	TEL_b34.borda = true;
	
	TEL_b33.myCruzarFronteiras = new SimSet();
	TEL_b33.myCruzarFronteiras.add(TEL_b13);
	TEL_b33.borda = true;
	
	TEL_b32.myCruzarFronteiras = new SimSet();
	TEL_b32.myCruzarFronteiras.add(TEL_b14);
	TEL_b32.borda = true;
	
	TEL_b31.myCruzarFronteiras = new SimSet();
	TEL_b31.myCruzarFronteiras.add(TEL_b11);
	TEL_b31.borda = true;
	
	TEL_b30.myCruzarFronteiras = new SimSet();
	TEL_b30.myCruzarFronteiras.add(TEL_b10);
	TEL_b30.borda = true;
	
	
	//Oriente:
	TEL_b14.myCruzarFronteiras = new SimSet();
	TEL_b14.myCruzarFronteiras.add(TEL_b32);
	TEL_b14.borda = true;
	
	TEL_b13.myCruzarFronteiras = new SimSet();
	TEL_b13.myCruzarFronteiras.add(TEL_b33);
	TEL_b13.borda = true;
	
	TEL_b12.myCruzarFronteiras = new SimSet();
	TEL_b12.myCruzarFronteiras.add(TEL_b34);
	TEL_b12.borda = true;
	
	TEL_b11.myCruzarFronteiras = new SimSet();
	TEL_b11.myCruzarFronteiras.add(TEL_b31);
	TEL_b11.borda = true;
	
	TEL_b10.myCruzarFronteiras = new SimSet();
	TEL_b10.myCruzarFronteiras.add(TEL_b30);
	TEL_b10.borda = true;
}


