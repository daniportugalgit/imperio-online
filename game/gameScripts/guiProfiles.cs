//---------------------------------------------------------------------------------------------
// Torque Game Builder
// Copyright (C) GarageGames.com, Inc.
//---------------------------------------------------------------------------------------------
//
// This is the file you should define your custom gui profiles that are to be used
// in the editor.
//
function setProfileRes(){
	%file = new FileObject();
	%file.openForRead("game/data/files/myVideoPrefs.sav"); 
	$profileRes = %file.readLine();
	%file.close();
	%file.delete();
	
	if($profileRes $= ""){
		$profileRes = 800;
	}	
	
	///////////////////////////////////////////////
	//ATENÇÃO
	//$profileRes = 1024; //linha que deve ser retirada antes de distribuir ->> DEBUG ONLY!!!
	//ATENÇÃO
	///////////////////////////////////////////////
	
	if($profileRes == 800){
		$TXTsize1 = 12;
		$TXTsize2 = 12;
		$TXTsize3 = 12;
		$TXTsize4 = 12;
		$TXTsize5 = 13;
		$TXTsize6 = 14;
		$TXTsize7 = 15;
		$TXTsize8 = 16;
		$TXTsize9 = 18;
		$TXTsize10 = 22;
				
		$TXT_formal_size1 = 12;
		$TXT_formal_size2 = 13;
		$TXT_formal_size3 = 14;
		$TXT_formal_size4 = 16;
		$TXT_formal_size5 = 18;
	} else if ($profileRes == 1024){
		$TXTsize1 = 12;
		$TXTsize2 = 13;
		$TXTsize3 = 14;
		$TXTsize4 = 15;
		$TXTsize5 = 16;
		$TXTsize6 = 17;
		$TXTsize7 = 18;
		$TXTsize8 = 20;
		$TXTsize9 = 24;
		$TXTsize10 = 30;
				
		$TXT_formal_size1 = 16;
		$TXT_formal_size2 = 17;
		$TXT_formal_size3 = 18;
		$TXT_formal_size4 = 20;
		$TXT_formal_size5 = 24;
	} else if ($profileRes == 1280){
		$TXTsize1 = 14;
		$TXTsize2 = 15;
		$TXTsize3 = 16;
		$TXTsize4 = 17;
		$TXTsize5 = 18;
		$TXTsize6 = 20;
		$TXTsize7 = 22;
		$TXTsize8 = 24;
		$TXTsize9 = 30;
		$TXTsize10 = 35;
				
		$TXT_formal_size1 = 15;
		$TXT_formal_size1 = 16;
		$TXT_formal_size2 = 18;
		$TXT_formal_size3 = 20;
		$TXT_formal_size4 = 24;
		$TXT_formal_size5 = 30;
	}
	
		/*
		$TXTsize1 = 12;
		$TXTsize2 = 13;
		$TXTsize3 = 14;
		$TXTsize4 = 15;
		$TXTsize5 = 16;
		$TXTsize6 = 17;
		$TXTsize7 = 18;
		$TXTsize8 = 20;
		$TXTsize9 = 24;
		$TXTsize10 = 30;
		*/
}
setProfileRes();

if(!isObject(ExampleWindowProfile)) new GuiControlProfile(ExampleWindowProfile)
{
   opaque = true;
   border = 1;
   fillColor = "211 211 211";
   fillColorHL = "190 255 255";
   fillColorNA = "255 255 255";
   fontColor = "0 0 0";
   fontColorHL = "200 200 200";
   text = "untitled";
   bitmap = "common/gui/images/window";
   textOffset = "5 5";
   hasBitmapArray = true;
   justify = "center";
};

if(!isObject(ClearWindowProfile)) new GuiControlProfile (ClearWindowProfile){
   // use fillColor for guiControl background
   opaque = false; 

   // draw a border
   border = 1;       

   // set the border color -> "R G B"
   borderColor   = "80 140 140";

   // set the background color -> "R G B A"
   fillColor = "0 0 0 110";
};


if(!isObject(ExampleScrollProfile)) new GuiControlProfile (ExampleScrollProfile)
{
   opaque = true;
   fillColor = "255 255 255";
   border = 1;
   borderThickness = 2;
   bitmap = "common/gui/images/scrollBar";
   hasBitmapArray = true;
};

if(!isObject(ExampleButtonProfile)) new GuiControlProfile(ExampleButtonProfile)
{
   opaque = true;
   border = -1;
   fontColor = "0 0 0";
   fontColorHL = "32 100 100";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   bitmap = "common/gui/images/button";
};



if(!isObject(ExampleTextProfile)) new GuiControlProfile(ExampleTextProfile)
{
   fontType = "Arial";
   fontSize = $TXTsize5;
   fontColor = "0 0 0";
};


if(!isObject(Example5TextProfile)) new GuiControlProfile(Example5TextProfile)
{
   fontType = "Formal 436 BT";
   fontSize = 13;
   fontColor = "240 240 240";
};

if(!isObject(Example6TextProfile)) new GuiControlProfile(Example6TextProfile)
{
   fontType = "Formal 436 BT";
   fontSize = 14;
   fontColor = "240 240 240";
};

if(!isObject(Example7TextProfile)) new GuiControlProfile(Example7TextProfile)
{
   fontType = "Formal 436 BT";
   fontSize = 15;
   fontColor = "240 240 240";
};

if(!isObject(Example8TextProfile)) new GuiControlProfile(Example8TextProfile)
{
   fontType = "Formal 436 BT";
   fontSize = 16;
   fontColor = "240 240 240";
};

if(!isObject(Example9TextProfile)) new GuiControlProfile(Example9TextProfile)
{
   fontType = "Formal 436 BT";
   fontSize = 17;
   fontColor = "240 240 240";
};

if(!isObject(Example10TextProfile)) new GuiControlProfile(Example10TextProfile)
{
   fontType = "Formal 436 BT";
   fontSize = 18;
   fontColor = "240 240 240";
};

	//criação dos textProfiles:
if(!isObject(ImperiaisProfile)){
	new GuiControlProfile (ImperiaisProfile){
		// GuiMLTextCtrl's draw borders, so lets turn 'border' off on this profile
		border = 1;
		borderColor   = "255 255 0";
	
		//cursorColor = "0 0 0 255";
		bold = 1;
	
	
		// set the font type and color
		fontSize = $TXTsize5; //16
		//fontType = "Courier New";
		fontColor = "255 255 255";
	
		// set the highlighted text color to be the same as the normal text color
		fontColorHL = "255 255 255";
	
		justify = "center";
	
	
		// set the highlighted fillColor to be completely transparent
		// (on GuiMLTextCtrl's fillColor is the text background)
		// note that when alpha is 0, the RGB values don't matter
		fillColorHL = "0 0 0 0";
	};
}
if(!isObject(ImperiaisBlackProfile)){
	new GuiControlProfile (ImperiaisBlackProfile){
		border = 0;
	
	
		fontSize = $TXTsize5; //16
		//fontType = "Courier New";
		fontColor = "0 0 0";
	
		// set the highlighted text color to be the same as the normal text color
		fontColorHL = "0 0 0";
	
		justify = "center";
	
	
		// set the highlighted fillColor to be completely transparent
		// (on GuiMLTextCtrl's fillColor is the text background)
		// note that when alpha is 0, the RGB values don't matter
		fillColorHL = "0 0 0 0";
	};
}


if(!isObject(ImperiaisSMALLProfile)){
	new GuiControlProfile (ImperiaisSMALLProfile){
		fontSize = $TXTsize3; //14;
		border = 0;
		fontColor = "255 255 255";
		justify = "left";
	};
}

if(!isObject(Imperiais_14_Center)){
	new GuiControlProfile (Imperiais_14_Center){
		fontSize = $TXTsize3; //14;
		border = 0;
		fontColor = "250 250 250";
		justify = "center";
	};
}


if(!isObject(ImperiaisMEDIUM_SMALLProfile)){
	new GuiControlProfile (ImperiaisMEDIUM_SMALLProfile){
		fontSize = $TXTsize6; //17;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "center";
	};
}

if(!isObject(ImperiaisMEDIUMProfile)){
	new GuiControlProfile (ImperiaisMEDIUMProfile){
		fontSize = $TXTsize7; //18;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "center";
	};
}
if(!isObject(ImperiaisBIGProfile)){
	new GuiControlProfile (ImperiaisBIGProfile){
		fontSize = $TXTsize9; //24;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "center";
	};
}

if(!isObject(ImperiaisSUPERBIGProfile)){
	new GuiControlProfile (ImperiaisSUPERBIGProfile){
		fontSize = $TXTsize10; //30;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "center";
	};
}

if(!isObject(ImperiaisMEDIUMBlackProfile)){
	new GuiControlProfile (ImperiaisMEDIUMBlackProfile){
		fontSize = $TXTsize7; //18;
		fontColor = "0 0 0";
		//fontColorHL = "255 255 255";
		justify = "center";
	};
}

if(!isObject(ImperiaisBaterGuiProfile)){
	new GuiControlProfile (ImperiaisBaterGuiProfile){
		fontSize = $TXTsize7; //18;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "right";
	};
}
if(!isObject(ImperiaisBaterGuiSmallProfile)){
	new GuiControlProfile (ImperiaisBaterGuiSmallProfile){
		fontSize = $TXTsize5; //16;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "right";
	};
}

if(!isObject(Imperiais15RIGHTProfile)){
	new GuiControlProfile (Imperiais15RIGHTProfile){
		fontSize = $TXTsize4; //15;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "right";
	};
}

if(!isObject(Imperiais16RIGHTProfile)){
	new GuiControlProfile (Imperiais16RIGHTProfile){
		fontSize = $TXTsize5; //16;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "right";
	};
}

if(!isObject(Imperiais16CENTERProfile)){
	new GuiControlProfile (Imperiais16CENTERProfile){
		fontSize = $TXTsize5; //16;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "center";
	};
}

if(!isObject(Imperiais17CENTERProfile)){
	new GuiControlProfile (Imperiais17CENTERProfile){
		fontSize = $TXTsize6; //17;
		fontColor = "255 255 255";
		justify = "center";
	};
}

if(!isObject(Imperiais16LEFTProfile))
{
	new GuiControlProfile (Imperiais16LEFTProfile)
	{
		fontSize = $TXTsize5; //16;
		fontColor = "255 255 255";
		justify = "left";
	};
}

if(!isObject(Imperiais17LEFTProfile))
{
	new GuiControlProfile (Imperiais17LEFTProfile)
	{
		fontSize = $TXTsize6; //17;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "left";
	};
}

if(!isObject(Imperiais17LEFT_HiProfile))
{
	new GuiControlProfile (Imperiais17LEFT_HiProfile)
	{
		fontSize = $TXTsize6; //17;
		fontColor = "255 255 200";
		justify = "left";
	};
}

if(!isObject(ImperiaisAtrioProfile)){
	new GuiControlProfile (ImperiaisAtrioProfile){
		fontSize = $TXTsize9; //24;
		fontColor = "255 255 255";
		//fontColorHL = "255 255 255";
		justify = "right";
	};
}
//radio btn:
if(!isObject(ImperiaisRadioProfile)){
	new GuiControlProfile (ImperiaisRadioProfile){
		fontSize = $TXTsize5;
		fillColor = "250 250 250";
		fontColorHL = "200 200 200";
		fixedExtent = true;
		bitmap = "~/gui/images/radioButton";
		hasBitmapArray = true;
	};
}

//edit text:
if(!isObject(ImperiaisGuiTextEditProfile)){
	new GuiControlProfile (ImperiaisGuiTextEditProfile){
		opaque = false;
		fillColor = "255 255 255";
		fillColorHL = "128 128 128";
		border = -2;
		//bitmap = "common/gui/images/textEdit";
		bitmap = "~/data/images/textEdit"; //passo a bitmap errada pra ficar transparente
		borderColor = "40 40 40 100";
		fontColor = "255 255 255";
		fontColorHL = "255 255 255";
		fontColorNA = "128 128 128";
		textOffset = "4 2";
		autoSizeWidth = false;
		autoSizeHeight = true;
		tab = true;
		canKeyFocus = true;
	};
} 

//edit text:
if(!isObject(ImperiaisGuiTextEditProfileBLACK)) new GuiControlProfile (ImperiaisGuiTextEditProfileBLACK)
{
   opaque = false;
   fillColor = "255 255 255";
   fillColorHL = "128 128 128";
   border = -2;
   //bitmap = "common/gui/images/textEdit";
   bitmap = "~/data/images/textEdit"; //passo a bitmap errada pra ficar transparente
   borderColor = "40 40 40 100";
   fontColor = "0 0 0";
   justify = "center";
   fontColorHL = "255 255 255";
   fontColorNA = "128 128 128";
   textOffset = "4 2";
   autoSizeWidth = false;
   autoSizeHeight = true;
   tab = true;
   canKeyFocus = true;
   
};

////////////////////////////////////////////
function createFormal437Profiles_branco()
{
	for (%i = 1; %i < 5; %i++)
	{
		for (%j = 0; %j < 3; %j++)
		{
			switch(%j)
			{
				case 0: %myJust = "right";	
				case 1: %myJust = "center";
				case 2: %myJust = "left";
			}
			%eval = "%myObj = Formal437BT_" @ %i @ "_" @ %myJust @ ";";
			eval(%eval);
			if(!isObject(%myObj)){
				%myString = "Formal437BT_" @ %i @ "_" @ %myJust;
				%eval = "%mySize = $TXT_formal_size" @ %i @ ";";
				eval(%eval);
				new GuiControlProfile (%myString){
					fontType = "Formal 437 BT";
					fontSize = %mySize;
					border = 0;
					fontColor = "255 255 255";
					justify = %myJust;
				};
			}
		}
	}
}
createFormal437Profiles_branco();

function popularFormal437Cache()
{
	populateFontCacheString("Formal 437 BT", 20, "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890 çáàâãéèêíìóòõôúùû ÇÁÀÂÃÉÈÊÍÌÓÒÕÔÚÙÛ !@#$%¨&*()[]{},.;/<>:?-=+_");
}



////////////////////////////////////////////

//////////////
//Audio Profiles:
datablock AudioDescription(turnoStartDescription){
   	volume   = 1.0;
   	isLooping= false;
  	is3D     = false;
    type     = $SimAudioType;
};

datablock AudioProfile(turnoStart){
   filename = "~/data/audio/turnoStart.ogg";
   description = "turnoStartDescription";
   preload = false;
};

datablock AudioDescription(tema_0Description){
   	volume   = 1.0;
   	isLooping= true;
  	is3D     = false;
    type     = $SimAudioType;
};

datablock AudioDescription(preludio_description){
   	volume   = 1.0;
   	isLooping= false;
  	is3D     = false;
    type     = $SimAudioType;
};

datablock AudioProfile(tema_0){
   filename = "~/data/audio/tema_0.ogg";
   description = "tema_0Description";
   preload = false;
};

datablock AudioProfile(tema_1){
   filename = "~/data/audio/tema_1.ogg";
   description = "preludio_description";
   preload = false;
};

datablock AudioProfile(tema_2){
   filename = "~/data/audio/tema_2.ogg";
   description = "preludio_description";
   preload = false;
};

datablock AudioDescription(commonEffectDescription70){
   	volume   = 0.7;
   	isLooping= false;
  	is3D     = false;
    type     = $SimAudioType;
};

datablock AudioDescription(commonEffectDescription80){
   	volume   = 0.8;
   	isLooping= false;
  	is3D     = false;
    type     = $SimAudioType;
};

datablock AudioDescription(commonEffectDescription90){
   	volume   = 0.9;
   	isLooping= false;
  	is3D     = false;
    type     = $SimAudioType;
};

datablock AudioDescription(commonEffectDescription100){
   	volume   = 1.0;
   	isLooping= false;
  	is3D     = false;
    type     = $SimAudioType;
};

//Nova Sala:
datablock AudioProfile(novaSalaAberta){
   filename = "~/data/audio/novaSala2.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//EXPLOSÕES:
datablock AudioProfile(escudoPerdido){
   filename = "~/data/audio/escudoPerdido.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//Navio:
datablock AudioProfile(explosao3){
   filename = "~/data/audio/explosao3.ogg";
   description = "commonEffectDescription100";
   preload = false;
};


//Tanque:
datablock AudioProfile(explosao4){
   filename = "~/data/audio/explosao4.wav";
   description = "commonEffectDescription100";
   preload = false;
};

//Tanque:
datablock AudioProfile(explosao6){
   filename = "~/data/audio/explosao6.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//Tanque:
datablock AudioProfile(explosao7){
   filename = "~/data/audio/explosao7.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//Tanque:
datablock AudioProfile(explosao8){
   filename = "~/data/audio/explosao8.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//Tanque:
datablock AudioProfile(explosao9){
   filename = "~/data/audio/explosao9.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//soldado:
datablock AudioProfile(explosao5){
   filename = "~/data/audio/explosao5.ogg";
   description = "commonEffectDescription90";
   preload = false;
};

//Líder:
datablock AudioProfile(explosao1){
   filename = "~/data/audio/explosao3.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//Vermes, Cefaloks e Zangões:
datablock AudioProfile(gulok_morrer1){
   filename = "~/data/audio/gulok_morrer1.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(gulok_morrer2){
   filename = "~/data/audio/gulok_morrer2.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(gulok_morrer3){
   filename = "~/data/audio/gulok_morrer3.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(gulok_morrer4){
   filename = "~/data/audio/gulok_morrer4.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(gulok_morrer5){
   filename = "~/data/audio/gulok_morrer5.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(gulok_morrer6){
   filename = "~/data/audio/gulok_morrer6.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

datablock AudioProfile(matriarca_morrer1){
   filename = "~/data/audio/matriarca_morrer1.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(matriarca_morrer2){
   filename = "~/data/audio/matriarca_morrer2.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(matriarca_morrer3){
   filename = "~/data/audio/matriarca_morrer3.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

datablock AudioProfile(rainha_morrer1){
   filename = "~/data/audio/rainha_morrer1.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(rainha_morrer2){
   filename = "~/data/audio/rainha_morrer2.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(rainha_morrer3){
   filename = "~/data/audio/rainha_morrer3.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//MOVER:
//Tanque:
datablock AudioProfile(tanqueMover1){
   filename = "~/data/audio/tanqueMover1.ogg";
   description = "commonEffectDescription80";
   preload = false;
};

//Tanque:
datablock AudioProfile(tanqueMover2){
   filename = "~/data/audio/tanqueMover2.ogg";
   description = "commonEffectDescription80";
   preload = false;
};

//Tanque:
datablock AudioProfile(tanqueMover3){
   filename = "~/data/audio/tanqueMover3.ogg";
   description = "commonEffectDescription80";
   preload = false;
};

//Tanque:
datablock AudioProfile(tanqueMover4){
   filename = "~/data/audio/tanqueMover4.ogg";
   description = "commonEffectDescription80";
   preload = false;
};

//Tanque:
datablock AudioProfile(tanqueMover5){
   filename = "~/data/audio/tanqueMover5.ogg";
   description = "commonEffectDescription80";
   preload = false;
};




//NASCER:
//Navio:
datablock AudioProfile(navioNascer1){
   filename = "~/data/audio/navioNascer1.ogg";
   description = "commonEffectDescription80";
   preload = false;
};
datablock AudioProfile(navioNascer2){
   filename = "~/data/audio/navioNascer2.ogg";
   description = "commonEffectDescription80";
   preload = false;
};
datablock AudioProfile(navioNascer3){
   filename = "~/data/audio/navioNascer3.ogg";
   description = "commonEffectDescription80";
   preload = false;
};

datablock AudioProfile(cefalok_nascer1){
   filename = "~/data/audio/cefalok_nascer1.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(cefalok_nascer2){
   filename = "~/data/audio/cefalok_nascer2.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(cefalok_nascer3){
   filename = "~/data/audio/cefalok_nascer3.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

datablock AudioProfile(ovo_eclodir1){
   filename = "~/data/audio/ovo_eclodir1.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(ovo_eclodir2){
   filename = "~/data/audio/ovo_eclodir2.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(ovo_eclodir3){
   filename = "~/data/audio/ovo_eclodir3.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(ovo_eclodir4){
   filename = "~/data/audio/ovo_eclodir4.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(ovo_eclodir5){
   filename = "~/data/audio/ovo_eclodir5.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

datablock AudioProfile(rainha_nascer1){
   filename = "~/data/audio/rainha_nascer1.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(rainha_nascer2){
   filename = "~/data/audio/rainha_nascer2.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(rainha_nascer3){
   filename = "~/data/audio/rainha_nascer3.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

datablock AudioProfile(matriarca_nascer1){
   filename = "~/data/audio/matriarca_nascer1.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(matriarca_nascer2){
   filename = "~/data/audio/matriarca_nascer2.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

datablock AudioProfile(crisalida_nascer1){
   filename = "~/data/audio/crisalida_nascer1.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//ACADEMIA:
//investir:
datablock AudioProfile(investir){
   filename = "~/data/audio/investir.wav";
   description = "commonEffectDescription90";
   preload = false;
};


//FX INGAME:
//Ping:
datablock AudioProfile(ping){
   filename = "~/data/audio/ping.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//ChatPing:
datablock AudioProfile(chatPing){
   filename = "~/data/audio/chatPing.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//ChatPingPrive:
datablock AudioProfile(chatPingPrive){
   filename = "~/data/audio/chatPingPrive.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//Infos:
//Minério:
datablock AudioProfile(achouMinerio){
   filename = "~/data/audio/achouMinerio.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
//Petróleo:
datablock AudioProfile(achouPetroleo){
   filename = "~/data/audio/achouPetroleo.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
//Urânio:
datablock AudioProfile(achouUranio){
   filename = "~/data/audio/achouUranio.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//NEGOCIAÇÕES:
//Pedir passagem:
datablock AudioProfile(passagem){
   filename = "~/data/audio/passagem.ogg";
   description = "commonEffectDescription100";
   preload = false;
};
datablock AudioProfile(propostaRecebida){
   filename = "~/data/audio/propostaRecebida.ogg";
   description = "commonEffectDescription100";
   preload = false;
};


//INTEL:
//intelVendeu:
datablock AudioProfile(intelVendeu){
   filename = "~/data/audio/intelVendeu.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//Canibalizar/Devorar:
datablock AudioProfile(canibalizar){
   filename = "~/data/audio/canibalizar.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//AirDrop:
datablock AudioProfile(airDrop){
   filename = "~/data/audio/airDrop.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//Aparição dos guloks:
datablock AudioProfile(gulok_appear){
   filename = "~/data/audio/gulok_appear.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//NA SALA:
//pronto:
datablock AudioProfile(pronto){
   filename = "~/data/audio/pronto.ogg";
   description = "commonEffectDescription100";
   preload = false;
};

//buzina:
datablock AudioProfile(buzinaSala){
   filename = "~/data/audio/buzinaSala.ogg";
   description = "commonEffectDescription100";
   preload = false;
};




////
//comon profiles (da versão 1.1.3 do Torque):
if(!isObject(ImperiaisTransparentProfile)) new GuiControlProfile (ImperiaisTransparentProfile)
{
   opaque = false;
   border = false;
};


if(!isObject(ImperiaisToolTipProfile)) new GuiControlProfile (ImperiaisToolTipProfile)
{
   // fill color
   fillColor = "239 237 222";

   // border color
   borderColor   = "138 134 122";

   // font
   fontType = "Arial";
   fontSize = 14;
   fontColor = "0 0 0";

};

////////////////////
//fontBoy Teste:


// Font Boy TGB font creation tool by Joe Rossi

/*
 
function loadCustomFont( %name, %size, %weight){ 
    %pngdir = "pngs/";
    %fontdir = "common/ui/cache/";  
    %fileName =  %fontdir @ %pngdir @ %name SPC %size @ ".png";
    importCachedFont( %name,  %size, %fileName, %weight,  0);
}

So the call would look something like this:
   loadCustomFont( "Seabird SF", 80, 0 );

Notes/issues:
 *Creating "Baltic" versions of any font seems to crash the engine (TGB 1.5.0 anyway)
 *You may need to edit the weight value if you want more space between fonts, I left it at 0.
 *You'll have to create and edit a new font for each font size you want
*/
 
 
 
// Create Resource Descriptor
$instantResource = new ScriptObject(){
	Class   = "font_boy";
	Name    = "font_boy";
	User    = "TOOLS";
	LoadFunction   = "font_boy::LoadResource";
	UnloadFunction = "font_boy::UnloadResource";
};

$instantResource.Data = new SimGroup(){
	canSaveDynamicFields = "1";
};
  

function font_boy::UnloadResource( %this ){   }


// Load Resource Function - Hooks into game
function font_boy::LoadResource( %this ){   

  // A cheesy way to prevent running this in our game (While developing) 
  // Todo - figure out how to make it an editor-only resource. 
  if (!isobject(LevelBuilderToolBar)){  return;  }
    
   $fontboy_font  = "";
   $fontboy_sizes = "36"; 
   $fontboy_clicks = 0;
      
   exec( "resources/font_boy/fontboy.gui" );
   exec( "resources/font_boy/fontboy_preview.gui" );
     
     
   %fonts = enumerateFonts( );       

   for ( %i = 0;  %i < getfieldcount(%fonts); %i++ ){ 
        %name =  getfield( %fonts , %i  );      
        fontList.add(  %name, 0  ); 
   }   
  
     fontsizer.add( 8,  0 );   
     fontsizer.add( 9,  1 );     
     fontsizer.add( 10, 2 );     
     fontsizer.add( 11, 3 );     
     fontsizer.add( 12, 4 );     
     fontsizer.add( 14, 5 );     
     fontsizer.add( 16, 6 );     
     fontsizer.add( 18, 7 );   
     fontsizer.add( 20, 8 );   
     fontsizer.add( 22, 9 );     
     fontsizer.add( 24, 10 );     
     fontsizer.add( 26, 11 );     
     fontsizer.add( 28, 12 );     
     fontsizer.add( 36, 13 ); 
     fontsizer.add( 48, 14 );     
     fontsizer.add( 72, 15 );     
     fontsizer.add( 80, 16 );     
    //add more if you want to

      
     previewscene.loadlevel( expandFileName("resources/font_boy/preview.t2d" ));   
   
     LevelBuilderToolManager::addButtonToBar( LevelBuilderToolBar, "iconFontBoy", "fontboyclicked();", "Font Creation Tool" );
}


function  populateFonts(   %font, %sizes){ 
   for (%i = 0; %i < getWordCount(%sizes); %i++) populateFontCacheRange(%font, getWord(%sizes, %i), 32, 126);
   writeFontCache();
}


function  fontbitmapIO(  %font, %sizes, %import){
    if ( %font $= "") return; 
   %pngdir = "pngs/";
   %fontdir = "common/ui/cache/";  

   for (%i = 0; %i < getWordCount(%sizes); %i++){
      %size = getWord(%sizes,%i);
      
      %weight = 0; // %weight = 3;  // Edit this if you need more space between fonts

      %fileName =  %fontdir @ %pngdir @ %font SPC %size @ ".png";
      if (%import)     importCachedFont(%font, %size, %fileName, %weight, 0);
      else             exportCachedFont(%font, %size, %fileName, %weight, 0);
   }
   writeFontCache();
}




function fontboyclicked(  ){ 
   if ( !$fontboy_clicks ) {
        
     $fontboy_clicks = 1;  //handle button toggle logic
     canvas.pushdialog( fontboy_gui  );        
     
   }else{  
      //We already chose a font and edited the image..so import.
      $fontboy_clicks = 0; 
      fontbitmapIO(  $fontboy_font , $fontboy_sizes, true);
      
      previewtext.font      =  $fontboy_font;
      previewtext.fontsizes =  $fontboy_sizes;
      canvas.pushdialog( fontpreview  );  //show the preview      
    }          
}


function fontboy_selected(%font, %size){
  
  if (%size $= "") return;
  
  $fontboy_font  = %font;  
  $fontboy_sizes = %size;    
    
  populateFonts( $fontboy_font,  $fontboy_sizes ); 
  fontbitmapIO(  $fontboy_font,  $fontboy_sizes , false );       
  
  MessageBoxOK("Note", "You may now go into common/ui/cache/pngs and edit: \n" @ %font SPC  %size @ ".png \n\nThen click FontBoy again to import your new font image. You will have to close TGB and restart it to use the font in game. Use importFontCache to load your custom font image.", "");
  
}



/////////////////
//testes com botões:
if(!isObject(ImperiaisButtonProfile_2)) new GuiControlProfile(ImperiaisButtonProfile_2)
{
   opaque = true;
   border = -1;
   fontType = "Formal 437 BT";
   fontSize = $TXT_formal_size2; //17 em 1024x768;
   textOffset = "3 6";
   offset = "5 5";
   fontColor = "250 250 250";
   //fontColor = "0 0 0";
   fontColorHL = "255 255 150";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   bitmap = "common/gui/images/button2";
};
if(!isObject(ImperiaisButtonProfile_3)) new GuiControlProfile(ImperiaisButtonProfile_3)
{
   opaque = true;
   border = -1;
   fontType = "Formal 437 BT";
   fontSize = $TXT_formal_size2; //17 em 1024x768;
   textOffset = "3 6";
   offset = "5 5";
   fontColor = "250 250 250";
   //fontColor = "0 0 0";
   fontColorHL = "255 255 150";
fontColorNA = "155 155 155";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   bitmap = "common/gui/images/button3";
};
if(!isObject(ImperiaisButtonProfile_4)) new GuiControlProfile(ImperiaisButtonProfile_4)
{
   opaque = true;
   border = 1;
   fontType = "Formal 437 BT";
   fontSize = $TXT_formal_size2; //17 em 1024x768;
   textOffset = "3 6";
   offset = "5 5";
   fontColor = "250 250 250";
   //fontColor = "0 0 0";
   fontColorHL = "255 255 150";
fontColorNA = "155 155 155";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   bitmap = "common/gui/images/button4";
};
if(!isObject(ImperiaisButtonProfile_5)) new GuiControlProfile(ImperiaisButtonProfile_5)
{
   opaque = true;
   border = 1;
   fontType = "Formal 437 BT";
   fontSize = $TXT_formal_size2; 
   fontColor = "250 250 250";
   //fontColor = "0 0 0";
   fontColorHL = "255 255 150";
fontColorNA = "155 155 155";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   bitmap = "common/gui/images/button5";
};
if(!isObject(ImperiaisButtonProfile_6)) new GuiControlProfile(ImperiaisButtonProfile_6)
{
   opaque = true;
   border = 1;
   fontType = "Formal 437 BT";
   fontSize = $TXT_formal_size2;
   fontColor = "250 250 250";
   //fontColor = "0 0 0";
   fontColorHL = "255 255 150";
   fontColorNA = "155 155 155";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   bitmap = "common/gui/images/button6";
};
if(!isObject(ImperiaisButtonProfile6_font7)) new GuiControlProfile(ImperiaisButtonProfile6_font7)
{
   opaque = true;
   border = 1;
   fontType = "Formal 437 BT";
   fontSize = $TXT_formal_size2; //17 em 1024x768;
   fontColor = "250 250 250";
   //fontColor = "0 0 0";
   fontColorHL = "255 255 150";
   fontColorNA = "155 155 155";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   bitmap = "common/gui/images/button6";
};
if(!isObject(ImperiaisButtonProfile6_font8)) new GuiControlProfile(ImperiaisButtonProfile6_font8)
{
   opaque = true;
   border = 1;
   fontType = "Formal 437 BT";
   fontSize = $TXT_formal_size3; //17 em 1024x768;
   fontColor = "250 250 250";
   //fontColor = "0 0 0";
   fontColorHL = "255 255 150";
   fontColorNA = "155 155 155";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   bitmap = "common/gui/images/button6";
};
if(!isObject(ImperiaisButtonProfile3_font7)) new GuiControlProfile(ImperiaisButtonProfile3_font7)
{
   opaque = true;
   border = 1;
   fontType = "Formal 437 BT";
   fontSize = $TXT_formal_size2; //17 em 1024x768;
   fontColor = "250 250 250";
   //fontColor = "0 0 0";
   fontColorHL = "255 255 150";
   fontColorNA = "155 155 155";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   bitmap = "common/gui/images/button3";
};
if(!isObject(ImperiaisButtonProfile3_font8)) new GuiControlProfile(ImperiaisButtonProfile3_font8)
{
   opaque = true;
   border = 1;
   fontType = "Formal 437 BT";
   fontSize = $TXT_formal_size3; //18 em 1024x768;
   fontColor = "250 250 250";
   //fontColor = "0 0 0";
   fontColorHL = "255 255 150";
   fontColorNA = "155 155 155";
   fixedExtent = true;
   justify = "center";
   canKeyFocus = false;
   bitmap = "common/gui/images/button3";
};



