
function clientAskVirusGulok(%onde, %ai, %matriarca){
	echo("pedindo Vírus Gulok");
	$virusON = false;
	if($myPersona.aca_av_2 > 0 || %ai == true){
		if($mySelf.virusDisparados == 0 || %ai == true){
			if(($mySelf.minerios > 0 && $mySelf.petroleos > 0 && $mySelf.uranios > 0) || %ai == true){
				hud_virus_btn.setVisible(false);
				clientPushServercomDot();
				
				if(!%ai){
					$mySelf.minerios -= 1;
					$mySelf.petroleos -= 1;
					$mySelf.uranios -= 1;
					
					$mySelf.virusDisparados++;
					atualizarRecursosGui();
					%fronteirasMax = $myPersona.aca_av_2 + 1;
				} else {
					%fronteirasMax = 4;
				}
				
				%fronteirasCount = %onde.myFronteiras.getCount();
				
				if(%fronteirasMax > %fronteirasCount){
					%fronteirasMax = %fronteirasCount;	
				}
				%fronteirasAtingidas = 1; 
				%fronteirasNomes = %onde.getName();
											
				%simTempFronteiras = new SimSet();
				for(%i = 0; %i < %fronteirasCount; %i++){
					%simTempFronteiras.add(%onde.myFronteiras.getObject(%i));	
				}
							
				if(%fronteirasMax > 0){			
					for(%i = 0; %i < %fronteirasMax; %i++){
						%result = dado(%simTempFronteiras.getcount(), -1);
						%altAreaAlvo = %simTempFronteiras.getObject(%result);
						%simTempFronteiras.remove(%altAreaAlvo);
						%fronteirasNomes = %fronteirasNomes SPC %altAreaAlvo.getName();
						%fronteirasAtingidas++;
					}
					echo("VIRUS GULOK NAS ÁREAS: " @ %fronteirasAtingidas SPC %fronteirasNomes);
				}
				
				if(!%ai){
					commandToServer('dispararVirus', $UNIT_ORIGEM.onde.getName(), %fronteirasAtingidas, %fronteirasNomes);	
				} else {
					commandToServer('dispararVirus', %matriarca.onde.getName(), %fronteirasAtingidas, %fronteirasNomes, true);	
				}
			}
		}
	}
}

function clientCmdDispararVirus(%matriarcaOnde, %fronteirasValidas, %fronteirasNomes){
	resetSelection();
	clientClearDisparoMark();
	clientMsg("virusDetectado", 8000); //mostra a msg de virus
	
	%eval = "%matriarca = " @ %matriarcaOnde.pos0Quem @ ";";
	eval(%eval);
		
	%tiroCopy = geoTiro_BP.createCopy();
	%tiroCopy.setPosition(%matriarca.getPosition());
		
	%tiroEffect = new t2dParticleEffect(){scenegraph = %matriarca.scenegraph;};
	%tiroEffect.loadEffect("~/data/particles/virusTiro.eff");
	%tiroEffect.mount(%tiroCopy);
	%tiroEffect.playEffect();
	
	%tiroCopy.myEffect = %tiroEffect;
	%tiroCopy.myAlvo = firstWord(%fronteirasNomes);
	%tiroCopy.moveToLoc1(%onde, %fronteirasValidas, %fronteirasNomes, "virus", %matriarca);	
}

function clientVirusEfeito(%fronteirasValidas, %fronteirasNomes){
	//primeiro pega cada área:
	for(%i = 0; %i < %fronteirasValidas; %i++){
		%myWord = getWord(%fronteirasNomes, %i);
		%eval = "%area[%i] = " @ %myWord @ ";";
		eval(%eval);
	}
			
	//primeiro solta o vírus na área-alvo:
	clientVirus(%area[0]);
	%desastreMark = new t2dParticleEffect(){scenegraph = %area[0].scenegraph;};
	%desastreMark.loadEffect("~/data/particles/desastreMark.eff");
	if(%area[0].ilha){
		%desastreMark.setPosition(%area[0].pos1);
	} else {
		%desastreMark.setPosition(%area[0].pos0);
	}
	%desastreMark.playEffect();
	
	//agora solta o vírus em cada fronteira
	for(%i = 1; %i < %fronteirasValidas + 1; %i++){
		schedule(1000 * %i, 0, "clientVirus", %area[%i]);
	}
	schedule(1000 * %i, 0, "clientPopServerComDot");
}

function clientVirus(%area){
	if(!isObject($simAreasComVirus)){
		$simAreasComVirus = new SimSet();
	}
	$simAreasComVirus.add(%area);
	
	%pos1X = firstWord(%area.pos1);
	%pos1Y = getWord(%area.pos1, 1);
	%pos2X = firstWord(%area.pos2);
	%pos2Y = getWord(%area.pos2, 1);
	%posX = (%pos1X + %pos2X) / 2;
	%posY = (%pos1Y + %pos2Y) / 2;
	
	%virusHaze = new t2dParticleEffect(){scenegraph = %area.scenegraph;};
	%virusHaze.loadEffect("~/data/particles/virusFX.eff");
	%virusHaze.setPosition(%posX SPC %posY);
	%virusHaze.setSize("4 4");
	%virusHaze.playEffect();
	%area.myVirusHazeFX = %virusHaze;
}

function clientClearAllViruses(){
	if(isObject($simAreasComVirus)){
		for(%i = 0; %i < $simAreasComVirus.getcount(); %i++){
			%area = $simAreasComVirus.getObject(%i);
			%area.myVirusHazeFX.setEffectLifeMode("KILL", 1);
		}
		for(%i = 0; %i < $simAreasComVirus.getcount(); %i++){
			%area = $simAreasComVirus.getObject(0);
			$simAreasComVirus.remove(%area);
		}
	}
}


function clientToggleVirusON(){
	if($virusON){
		clientCancelarCanhao();
	} else {
		$virusON = true;
		clientMsgVirusON();
		hud_virus_btn.setStateOn(true);
	}
}






