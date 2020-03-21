// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientGeoCanhao.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 23 de novembro de 2008 17:53
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientGeoCanhaoCallHud(){
	geoCanhaoTab.setVisible(true);
	if(($myPersona.aca_v_6 >= 2 && $myPersona.especie $= "humano") || $myPersona.especie $= "gulok"){
		//echo("Configurando hud do Geo-Canhão");	
		geoCanhao_tab_ajuda_btn.setActive(true);
		if($myPersona.aca_art_1 == 6){
			%myLvl = 2;
		} else if($myPersona.aca_art_1 >= 4){
			%myLvl = 1;
		} else {
			%myLvl = 0;
		}
		
		
		geoCanhao_tab_forca_txt.text = %myLvl @ " / 2";
		
		geoCanhao_tab_travado_img.setVisible(false);
		
		clientGeoCanhaoCalcularGauge(%myLvl);
		clientVerifyGeoCanhaoBtns(%myLvl);
	} else {
		//echo("Pesquisa Inexistente: Planetas Nível 2");
		geoCanhao_tab_ajuda_btn.setActive(false);
		geoCanhao_tab_travado_img.setVisible(true);
		geoCanhao_tab_investir_btn.setActive(false);
		geoCanhao_tab_forca_txt.text = "0 / 2";
		clientGeoCanhaoCalcularGauge();
	}	
	geoCanhao_tab_carga_txt.text = $myPersona.aca_art_1 @ " / 6";
}

function clientVerifyGeoCanhaoBtns(%lvl){
	clientGeoCanhaoVerificarInvestir();
	geoCanhao_tab_vulcoes_btn.setActive(false);
	geoCanhao_tab_furacoes_btn.setActive(false);
	if(%lvl == 1 && $mySelf.geoDisparos == 0){
		geoCanhao_tab_vulcoes_btn.setActive(true);
		geoCanhao_tab_furacoes_btn.setActive(false);
	} else if(%lvl == 2 && $mySelf.geoDisparos == 0){
		geoCanhao_tab_vulcoes_btn.setActive(false);
		geoCanhao_tab_furacoes_btn.setActive(true);
	}
}

function clientGeoCanhaoCalcularGauge(%lvl){
	geoCanhao_tab_nivel_1_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_tab_nivel_1_normal";
	geoCanhao_tab_nivel_2_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_tab_nivel_2_normal";
	
	//níveis:
	if(%lvl > 0){
		geoCanhao_tab_nivel_1_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_tab_nivel_1_ativo"; 
		if(%lvl > 1){
			geoCanhao_tab_nivel_2_img.bitmap = "~/data/images/artefatos/nexusAlquimico/nexus_tab_nivel_2_ativo"; 
		}
	}
	
	//barras:
	geoCanhao_fillBarras(%lvl);
}

function geoCanhao_fillBarras(%lvl){
	%carga = $myPersona.aca_art_1;
	if($primeiroJogoDaSessao){
		//RES::DEFAULT(1024x768):
		%myX1 = 136;
		%myY = 11;
		%myX2 = 67;
	} else {
		%myWindowResX = sceneWindow2d.getWindowExtents();
		$myWindowResX = getWord(%myWindowResX, 2);
		%myX1 = calcularNaRes(136, $myWindowResX);
		%myX2 = calcularNaRes(67, $myWindowResX);
		%myY = calcularNaRes(11, $myWindowResX);
	}
	
	geoCanhao_tab_barra_1_img.setVisible(true);	
	geoCanhao_tab_barra_2_img.setVisible(false);
	
	if(%carga > 4){
		%myCargaTemp = 4;
	} else {
		%myCargaTemp = %carga;
	}
	%extX = mFloor((%myCargaTemp / 4) * %myX1);
	%ext = %extX SPC %myY;
	geoCanhao_tab_barra_1_img.extent = %ext;	
		
	if(%lvl >= 1){
		geoCanhao_tab_barra_2_img.setVisible(true);
		%myInvestTemp = %carga - 4;
		
		%extX = mFloor((%myInvestTemp / 2) * %myX2);
		%ext = %extX SPC %myY;
		geoCanhao_tab_barra_2_img.extent = %ext;
	}
}

function clientGeoCanhaoVerificarInvestir(){
	geoCanhao_tab_investir_btn.setActive(false);
	if($mySelf.minerios > 0 && $mySelf.petroleos > 0 && $mySelf.uranios > 0){
		if($myPersona.aca_art_1 < 6 && $mySelf.geoDisparos == 0){
			if($numDePlayersNestaPartida > 2 && !$jogoEmDuplas){
				geoCanhao_tab_investir_btn.setActive(true);
			} else if($numDePlayersNestaPartida == 6 && $jogoEmDuplas){
				geoCanhao_tab_investir_btn.setActive(true);
			}
		}
	}
}

function clientAskGeoDisparo(%onde){
	$geoDisparoON = false;
	if($myPersona.aca_art_1 >= 4){
		clientPushServerComDot();
		%fronteirasCount = %onde.myFronteiras.getCount();
		%fronteirasValidas = 1; //conta a área em si
		%fronteirasNomes = %onde.getName();
		for(%i = 0; %i < %fronteirasCount; %i++){
			%area = %onde.myFronteiras.getObject(%i);
			if($myPersona.aca_art_1 == 6){
				%fronteirasNomes = %fronteirasNomes SPC %area.getName();
				%fronteirasValidas++;
			} else {
				if(%area.terreno $= "terra"){
					%fronteirasNomes = %fronteirasNomes SPC %area.getName();
					%fronteirasValidas++;
				}
			}
		}
		echo("ASKGEODISPARO: " @ %fronteirasValidas SPC %fronteirasNomes);
		$myPersona.aca_art_1 = 0;
		$mySelf.geoDisparos++;
		commandToServer('geoDisparo', %fronteirasValidas, %fronteirasNomes);
	}
}



function clientToggleGeoDisparoON(){
	if($geoDisparoON){
		clientCancelarCanhao();
	} else {
		$geoDisparoON = true;
		clientMsgGeoCanhaoDisparar();
	}
}


function clientAskInvestirGeoCanhao(){
	if($mySelf.minerios > 0 && $mySelf.petroleos > 0 && $mySelf.uranios > 0){ //esta é a segunda verificação, o botão nem deveria ter ficado ativo caso não houvesse recursos pra investir
		if($myPersona.aca_art_1 < 6){
			clientPushServerComDot();
			commandToServer('geoCanhao_investir');
		}
	} else {
		//o botão não poderia ter ficado ativo
	}
}

//server responde com isso clientCmd_aInvestirRecurso("minerios", true), por exemplo;
function clientCmdGeoCanhao_investir(){
	$mySelf.minerios -= 1;
	$mySelf.petroleos -= 1;
	$mySelf.uranios -= 1;
		
	alxPlay( investir );
		
	$myPersona.aca_art_1++;
		
	atualizarRecursosGui(); //retira os recursos investidos
	clientAtualizarPEATab(); //atualiza a pesquisa
	clientAtualizarEstatisticas(); //marca os pontos
	clientToggleRecursosBtns(); //verifica os btns de venda de recursos
	clientGeoCanhaoCallHud(); //recalcula o hud do geo-canhão
	clientPopServerComDot(); //devolve o controle pro usuário
}

///
//Tiro:
function clientCmdGeoDisparo(%fronteirasValidas, %fronteirasNomes, %tipo){
	resetSelection();
	clientClearDisparoMark();
	$jogadorDaVez.myDiplomataHud.bitmap = "~/data/images/playerHudAtaque.png"; //marca que quem atacou não é mais diplomata
	$jogadorDaVez.atacou = true;
	
	%tiroCopy = geoTiro_BP.createCopy();
	%tiroCopy.setPosition(geoCanhao_hp.getPosition());
	
	/*
	%burstEffect = new t2dParticleEffect(){scenegraph = %tiroCopy.scenegraph;};
	%burstEffect.loadEffect("~/data/particles/geoCanhaoBurst.eff");
	%burstEffect.setPosition(geoCanhao_hp.getPosition());
	%burstEffect.setEffectLifeMode("KILL", 1);
	%burstEffect.playEffect();
	*/
	
	%tiroEffect = new t2dParticleEffect(){scenegraph = %tiroCopy.scenegraph;};
	%tiroEffect.loadEffect("~/data/particles/geoTiro.eff");
	%tiroEffect.mount(%tiroCopy);
	%tiroEffect.playEffect();
	
	%tiroCopy.myEffect = %tiroEffect;
	%tiroCopy.myAlvo = firstWord(%fronteirasNomes);
	%tiroCopy.moveToLoc1(%onde, %fronteirasValidas, %fronteirasNomes, %tipo);	
}

function geoTiro::createCopy(%this){
      %copy = new t2dStaticSprite(){
         scenegraph = $strategyScene;
         class = %this.class;   
         imageMap = %this.imageMap;
         size = %this.size;
         position = %this.position;
         CollisionActiveReceive = %this.CollisionActiveReceive;
         CollisionActiveSend = %this.CollisionActiveSend;
         CollisionCallback = %this.CollisionCallback;
         CollisionResponseMode = %this.CollisionResponseMode;
         CollisionDetectionMode = %this.CollisionDetectionMode;
         CollisionPolyList = %this.CollisionPolyList;
         CollisionPhysicsReceive = %this.CollisionPhysicsReceive;
         CollisionPhysicsSend = %this.CollisionPhysicsSend;
         WorldLimitCallback = %this.WorldLimitCallback;
         
	};
   return %copy;
}

function clientGeoEfeito(%fronteirasValidas, %fronteirasNomes, %tipo){
	//primeiro pega cada área:
	for(%i = 0; %i < %fronteirasValidas; %i++){
		%myWord = getWord(%fronteirasNomes, %i);
		%eval = "%area[%i] = " @ %myWord @ ";";
		eval(%eval);
	}
			
	//primeiro mata todos na área:
	clientCmdDesastre(%area[0], %tipo);
	
	//agora mata cada fronteira
	for(%i = 1; %i < %fronteirasValidas + 1; %i++){
		schedule(1000 * %i, 0, "clientCmdDesastre", %area[%i], %tipo, false);
	}
	schedule(1000 * %i, 0, "clientPopServerComDot");
}


function geoTiro::moveToLoc1(%this, %areaAlvo, %fronteirasValidas, %fronteirasNomes, %tipo, %matriarca){
	%speed = 35;

	%orient = angleBetween(%this.getPosition(), geoTiro_BP.getPosition());
	%this.setRotation(%orient);
	
	%eval = "%this.myAlvo = " @ %areaAlvo @ ";";
	eval(%eval);
	
	%this.flag = 0;
	%this.fronteirasValidas = %fronteirasValidas;
	%this.fronteirasNomes = %fronteirasNomes;
	%this.tipo = %tipo;
   	// manda o objeto andar até lah e parar quando chegar lá (true):
	if(%tipo $= "virus"){
		%matPosX = firstWord(%matriarca.getPosition());
		%posY = -66;
		%firstTargetPos = %matPosX SPC %posY;
		%this.moveTo(%firstTargetPos, %speed, true, true); 
	} else {
		%this.moveTo(geoTiro_BP.getPosition(), %speed, true, true); 
	}
}

function geoTiro::moveToLoc2(%this){
	%speed = 35;

	%orient = angleBetween(%this.getPosition(), %this.myAlvo.pos1);
	%this.setRotation(%orient);	
	%this.moveTo(%this.myAlvo.pos1, %speed, true, true); 
}

function geoTiro::onPositionTarget(%this){
	%this.flag++;
	if(%this.flag == 1){
		%this.moveToLoc2();
	} else if(%this.flag == 2){
		%this.myEffect.dismount();
		%this.myEffect.stopEffect(1, 1);
		if(%this.tipo $= "virus"){
			%impacto = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
			%impacto.loadEffect("~/data/particles/virusImpact.eff");
			%impacto.setPosition(%this.myAlvo.pos1);
			%impacto.playEffect();
			clientVirusEfeito(%this.fronteirasValidas, %this.fronteirasNomes);
		} else {
			clientGeoEfeito(%this.fronteirasValidas, %this.fronteirasNomes, %this.tipo);
		}
		%this.safeDelete(); //destruir o míssil
	}
}
