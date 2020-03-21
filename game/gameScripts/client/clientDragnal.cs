// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientDragnal.cs
// Copyright          :  
// Author             :  admin
// Created on         :  domingo, 14 de dezembro de 2008 17:41
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function clientToggleDragnalBtns(){
	if($vendoDragnalBtns){
		apagarDragnalBtns();
	} else {
		mostrarDragnalBtns();
	}
	//atualizarBotoesDeCompra();
}

function apagarDragnalBtns(){
	hud_dragAtk_btn.setVisible(false);	
	hud_entregar_btn.setVisible(false);	
	hud_sopro_btn.setVisible(false);	
	$vendoDragnalBtns = false;	
	hud_dragnal_btn.setStateOn(false);
}

function mostrarDragnalBtns(){
	hud_dragAtk_btn.setVisible(true);	
	hud_entregar_btn.setVisible(true);	
	hud_sopro_btn.setVisible(true);	
	$vendoDragnalBtns = true;	
	hud_dragnal_btn.setStateOn(true);
}


function dragAtk_btnClick(){
	clientToggleDragnalAtkON();
}

function clientToggleDragnalAtkON(){
	if($dragnalAtkON){
		clientCancelarDragnalAtk();
	} else {
		$dragnalAtkON = true;
		clientMsgDragnalAtk();
		hud_dragAtk_btn.setStateOn(true);
	}
}

function clientCancelarDragnalAtk(){
	$dragnalAtkON = false;
	clientMsgGuiZerar();	
	hud_dragAtk_btn.setStateOn(false);
	msgGui.setVisible(false);
	clientClearDisparoMark();
}

function clientAskDragnalAtacar(%area, %AI){
	%custo = 2;	
	%myMax = 1;
	
	if($myPersona.aca_av_1 == 3){
		%custo = 1;	
	}
	if($myPersona.aca_av_1 >= 2){
		%myMax = 2;
	}
	
	
	echo("pedindo ataque de Dragnal");
	$dragnalAtkON = false;
	if($myPersona.aca_i_3 > 0 || %AI){
		if($mySelf.dragnalAtks < %myMax || %AI){
			if($rodadaAtual > 1){
				clientPushServercomDot();
				
				if(!%AI){
					$mySelf.imperiais -= %custo;
					$mySelf.dragnalAtks++;
					%meuBando = $myPersona.aca_ldr_3_h4 + 1;
				} else {
					%meuBando = 2;
				}
				
				
				if(%meuBando == 1){
					%meuBando = 0;	
				}
				%fronteirasCount = %area.myFronteiras.getCount();
				if(%meuBando > %fronteirasCount){
					%meuBando = %fronteirasCount;	
				}
				
				%fronteirasAtingidas = 1; 
				%fronteirasNomes = %area.getName();
								
				%simTempFronteiras = new SimSet();
				for(%i = 0; %i < %fronteirasCount; %i++){
					%simTempFronteiras.add(%area.myFronteiras.getObject(%i));	
				}
							
				if(%meuBando > 0){			
					for(%i = 0; %i < %meuBando; %i++){
						%result = dado(%simTempFronteiras.getcount(), -1);
						%altAreaAlvo = %simTempFronteiras.getObject(%result);
						if((!%AI && %altAreaAlvo.dono != $mySelf) || (%AI && %altAreaAlvo.dono != $aiPlayer)){
							%simTempFronteiras.remove(%altAreaAlvo);
							%fronteirasNomes = %fronteirasNomes SPC %altAreaAlvo.getName();
							%fronteirasAtingidas++;
						} else {
							%result = dado(%simTempFronteiras.getcount(), -1);
							%altAreaAlvo = %simTempFronteiras.getObject(%result);
							if((!%AI && %altAreaAlvo.dono != $mySelf) || (%AI && %altAreaAlvo.dono != $aiPlayer)){
								%simTempFronteiras.remove(%altAreaAlvo);
								%fronteirasNomes = %fronteirasNomes SPC %altAreaAlvo.getName();
								%fronteirasAtingidas++;
							} else {
								%result = dado(%simTempFronteiras.getcount(), -1);
								%altAreaAlvo = %simTempFronteiras.getObject(%result);
								if((!%AI && %altAreaAlvo.dono != $mySelf) || (%AI && %altAreaAlvo.dono != $aiPlayer)){
									%simTempFronteiras.remove(%altAreaAlvo);
									%fronteirasNomes = %fronteirasNomes SPC %altAreaAlvo.getName();
									%fronteirasAtingidas++;
								} else {
									%result = dado(%simTempFronteiras.getcount(), -1);
									%altAreaAlvo = %simTempFronteiras.getObject(%result);
									if((!%AI && %altAreaAlvo.dono != $mySelf) || (%AI && %altAreaAlvo.dono != $aiPlayer)){
										%simTempFronteiras.remove(%altAreaAlvo);
										%fronteirasNomes = %fronteirasNomes SPC %altAreaAlvo.getName();
										%fronteirasAtingidas++;
									}
								}
							}
						}
					}
					echo("DRAGNALATK em BANDO: " @ %fronteirasAtingidas SPC %fronteirasNomes);
				}
								
				
				atualizarImperiaisGui();
				commandToServer('dragnalAtacar', %area.getName(), %meuBando, %fronteirasAtingidas, %fronteirasNomes);	
			}
		}
	}

}

function clientCmdDragnalAtacar(%onde, %ataques, %irmaos, %fronteirasAtingidas, %fronteirasNomes){
	atualizarBotoesDeCompra(); //inativa o botão de dragnal
	clientMsgGuiZerar();	
	msgGui.setVisible(false);
	clientClearDisparoMark();
	if($mySelf == $jogadorDaVez){
		resetSelection();	
	}
	//clientPopServerComDot();
	%dragnal = dragnal_BP.createCopy();
	%dragnal.dono = $jogadorDaVez;
	%dragnal.ataques = %ataques;
	$jogadorDaVez.myDiplomataHud.bitmap = "~/data/images/playerHudAtaque.png"; //marca que quem atacou não é mais diplomata
	$jogadorDaVez.atacou = true;
	%dragnal.viajarIda(%onde, "atacar");
	
	if(%irmaos > 0){
		for(%i = 0; %i < %fronteirasAtingidas; %i++){
			%dragnal = dragnal_BP.createCopy();
			%dragnal.dono = $jogadorDaVez;
			%dragnal.ataques = 1; //os irmãos atacam uma única vez
			
			%myWord = getWord(%fronteirasNomes, %i+1);
			%eval = "%myAlvo = " @ %myWord @ ";";
			eval(%eval);
			
			%dragnal.viajarIda(%myAlvo, "atacar");
		}
	}
}

function dragnal::createCopy(%this){
      %copy = new t2dAnimatedSprite(){
         scenegraph = %this.sceneGraph;
         class = %this.class;   
         imageMap = %this.imageMap;
         size = %this.size;
		 animationName = "dragnalAnim";
		
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
         WorldLimitMax = %this.WorldLimitMax;
         WorldLimitMin = %this.WorldLimitMin;
         WorldLimitMode = %this.WorldLimitMode;
    };
   %copy.setLayer(0);
   %copy.dono = $jogadorDaVez;
   return %copy;
}


function dragnal::viajarIda(%this, %areaAlvo, %finalidade){
	%this.myOriginalSizeX = %this.getSizeX();
	
	%this.areaAlvo = %areaAlvo;
	%speed = 10;
	%this.myFinalidade = %finalidade;
	
	if(%finalidade $= "atacar"){
		if(%onde.ilha){
			%myTargetPos = %areaAlvo.pos1;
		} else {
			%myTargetPos = %areaAlvo.pos0;
		}
	} else if(%finalidade $= "entregar" || %finalidade $= "soprar"){
		%myTargetPos = %areaAlvo.pos4;
	}
	
	%orient = angleBetween(%this.getPosition(), %myTargetPos);
	%this.setRotation(%orient);
	
	//FX:
	%dragnalFX = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%dragnalFX.loadEffect("~/data/particles/dragnalFX.eff");
	%dragnalFX.setEffectLifeMode("INFINITE");
	%dragnalFX.mount(%this, 0, 0, 0, 1, 0, 0, 0); //o quarto número é 1[bool], quer dizer pra herdar a rotação do objeto-base
	%dragnalFX.playEffect();
	%this.myFX = %dragnalFX;
		
   	// manda o objeto andar, parar quando chegar lá e chamar o callback onPositionTarget:
   	%this.moveTo(%myTargetPos, %speed, true, true); 
}

function dragnal::onPositionTarget(%this){
	apagarDragnalBtns();
	if(%this.myFinalidade $= "atacar"){
		for(%i = 0; %i < %this.ataques; %i++){
			%time = (%i * 1000) + 100;
			schedule(%time, 0, "clientDragnalAtacar", %this);
		}
		schedule(%time + 1000, 0, "clientDragnalIrEmbora", %this);
	} else if(%this.myFinalidade $= "entregar"){
		clientDragnalDescerNoTabuleiro(%this);
	} else if(%this.myFinalidade $= "soprar"){
		%this.soprar();
	}
}

function clientDragnalAtacar(%dragnal){
	%dragnal.atacar(%dragnal.areaAlvo);
}

function dragnal::atacar(%this, %area){
	//%this.myFX.setEffectLifeMode("KILL", 1);
	
	if(isObject(%area.pos1Quem)){
		%this.sniperFire(%area.pos1Quem);	
	} else if(isObject(%area.pos2Quem)){
		%this.sniperFire(%area.pos2Quem);	
	} else if(isObject(%area.pos0Quem) && %area.pos0Quem.class $= "rainha"){
		if(!%area.pos0Quem.grandeMatriarca){
			%this.sniperFire(%area.pos0Quem);	
		} else {
			%this.irEmbora(); //foge da grande matriarca
		}
	} else {
		//não tem mais ninguém, vai embora:
		%this.irEmbora();	
	}
}

function clientDragnalKillFX(%dragnal){
	%dragnal.myFX.setEffectLifeMode("KILL", 1);
}

function dragnal::sniperFire(%this, %inimigo){
  	// Criar o projétil:
	%tiroDragnal = soldadoTiro;
	%projetil = %tiroDragnal.createCopy();
	
	%orient = angleBetween(%this.getPosition(), %inimigo.getPosition());
	%this.setRotation(%orient);
	
	universalCreateShot(%this, %inimigo, %projetil, "dragnalFire", "");
}

function clientDragnalIrEmbora(%dragnal){
	%dragnal.irEmbora();
}

function dragnal::irEmbora(%this){
	%orient = angleBetween(%this.getPosition(), "5 41");
	%this.setRotation(%orient);
	%this.moveTo("5 41", 15, true); 	
	
	schedule(1000, 0, "clientPopServerComDot");
	schedule(10000, 0, "clientDragnalKillFX", %this);
	schedule(10100, 0, "delDragnal", %this);
}

function dragnal::moveToLoc(%this, %worldPos){
	%speed = 15; 

	%orient = angleBetween(%this.getPosition(), %worldPos);
	%this.setRotation(%orient);

   	// manda o objeto andar e parar quando chegar lá:
   	%this.moveTo(%worldPos, %speed, true); 
}

function delDragnal(%dragnal){
	%dragnal.myFX.safeDelete();
	%dragnal.safeDelete();	
}



//////////////
//Entregar:
function clientAskDragnalEntregar(){
	if($myPersona.aca_av_1 == 3){
		%custo = 1;	
	} else {
		%custo = 2;
	}
	if($myPersona.aca_av_1 > 0){
		%myMax = 2;	
	} else {
		%myMax = 1;	
	}
	
	if($jogadorDaVez == $mySelf){
		if(Foco.getObject(0).onde.terreno $= "terra"){
			if($mySelf.dragnalEntregas < %myMax && $mySelf.imperiais >= %custo){
				clientPushServerComDot();
				$mySelf.imperiais -= %custo;
				atualizarImperiaisGui();
				$mySelf.dragnalEntregas++;
				if($mySelf.dragnalEntregas < %myMax){
					hud_entregar_btn.setActive(true);	
				} else {
					hud_entregar_btn.setActive(false);	
				}
				commandToServer('dragnalEntregar', Foco.getObject(0).onde.getName());
			}
		}
	}
}


function clientCmdDragnalEntregar(%onde, %ovos){
	atualizarBotoesDeCompra(); //inativa o botão de entrega se for preciso
	apagarDragnalBtns();
	clientPopServerComDot();
	%dragnal = dragnal_BP.createCopy();
	%dragnal.dono = $jogadorDaVez;
	%dragnal.ovos = %ovos;
	%dragnal.viajarIda(%onde, "entregar");
}




function clientDragnalDescerNoTabuleiro(%dragnal){
	%mySizeX = %dragnal.getSizeX();
	%mySizeY = %dragnal.getSizeY();
	if(%mySizeX > %dragnal.myOriginalSizeX / 1.5){
		%dragnal.setSizeX(%mySizeX / 1.1); 
		%dragnal.setSizeY(%mySizeY / 1.1);
		schedule(200, 0, "clientDragnalDescerNoTabuleiro", %dragnal);
	} else {
		%dragnal.droparOvos();
		clientDragnalSubirDoTabuleiro(%dragnal);
	}
}
function clientDragnalSubirDoTabuleiro(%dragnal){
	%mySizeX = %dragnal.getSizeX();
	%mySizeY = %dragnal.getSizeY();
	if(%mySizeX < %dragnal.myOriginalSizeX){
		%dragnal.setSizeX(%mySizeX * 1.1); 
		%dragnal.setSizeY(%mySizeY * 1.1);
		schedule(100, 0, "clientDragnalSubirDoTabuleiro", %dragnal);
	} else {
		%dragnal.irEmbora();
	}
}

function dragnal::droparOvos(%this){
	echo("DROPANDO OVOS:: AreaAlvo = " @ %this.areaAlvo);
	%eval = "%areaAlvo = " @ %this.areaAlvo @ ";";
	eval(%eval);
		
	for(%i = 0; %i < %this.ovos; %i++){
		%newUnit = ovo_BP.createCopy();
		
		%newUnit.onde = %areaAlvo;
		%newUnit.setPosition(%newUnit.onde.pos4);
		%newUnit.isSelectable = 1;
		%newUnit.isMoveable = 0;
		%newUnit.dono = %this.dono;
		%newUnit.setMyImage();
		%newUnit.dono.mySimUnits.add(%newUnit); //adiciona a unit no simset de unidades do player;	
		%newUnit.dono.mySimOvos.add(%newUnit); //adiciona o ovo no simSet de ovos
		%newUnit.onde.positionUnit(%newUnit);
		
		%explosao = new t2dParticleEffect(){scenegraph = %newUnit.scenegraph;};
		%explosao.loadEffect("~/data/particles/ovoBotado.eff");
		%explosao.setEffectLifeMode("KILL", 1);
		%explosao.setPosition(%newUnit.getPosition());
		%explosao.playEffect();
		
		%newUnit.onde.resolverMyStatus();
	}
}


/////////////
//SOPRAR:
//Entregar:
function clientAskDragnalSoprar(){
	if($myPersona.aca_av_1 == 3){
		%custo = 1;	
	} else {
		%custo = 2;
	}
	if($myPersona.aca_av_1 > 0){
		%myMax = 2;	
	} else {
		%myMax = 1;	
	}
	
	if($jogadorDaVez == $mySelf){
		if(Foco.getObject(0).class $= "ovo"){
			if($mySelf.dragnalSopradas < %myMax && $mySelf.imperiais >= %custo){
				clientPushServerComDot();
				$mySelf.imperiais -= %custo;
				atualizarImperiaisGui();
				$mySelf.dragnalSopradas++;
				if($mySelf.dragnalSopradas < %myMax){
					dragnalSoprar_btn.setActive(true);	
				} else {
					dragnalSoprar_btn.setActive(false);	
				}
				commandToServer('dragnalSoprar', Foco.getObject(0).onde.getName());
			}
		} else {
			echo("Não há ovo selecionado");	
		}
	}
}


function clientCmdDragnalSoprar(%onde, %ovos){
	atualizarBotoesDeCompra(); //inativa o botão de sopro se for preciso
	apagarDragnalBtns();
	clientPopServerComDot();
	%dragnal = dragnal_BP.createCopy();
	%dragnal.dono = $jogadorDaVez;
	%dragnal.ovos = %ovos;
	%dragnal.viajarIda(%onde, "soprar");
}

function dragnal::soprar(%this){
	//FX:
	%chocarFX = new t2dParticleEffect(){scenegraph = %this.scenegraph;};
	%chocarFX.loadEffect("~/data/particles/dragnalChocarFX.eff");
	%chocarFX.setEffectLifeMode("KILL", 3);
	%chocarFX.setPosition(%this.areaAlvo.pos4);
	%chocarFX.playEffect();
	
	for(%i = 0; %i < %this.ovos; %i++){
		%ovo = %this.areaAlvo.myPos4List.getObject(0);
		%ovo.eclodir();
	}
	schedule(2000, 0, "clientDragnalIrEmbora", %this);
}



