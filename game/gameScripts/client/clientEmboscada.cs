// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientEmboscada.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  segunda-feira, 10 de março de 2008 13:58
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function clientFindUnitInimiga(%area, %pos, %player){ //o parâmetro %player é referente a quem está pedindo o comando, não ao inimigo;
	if(%pos $= "pos2" || %pos $= "pos1" || %pos $= "pos0"){
		%eval = "%tempUnit = " @ %area @ "." @ %pos @ "Quem;";
		eval(%eval);
		if(%tempUnit.dono != %client.player){
			%eval = "%unit = " @ %area @ "." @ %pos @ "Quem;";
			eval(%eval);
		}
	} else if(%pos $= "pos3"){
		if(%area.dono !$= "MISTA"){
			%unit = %area.myPos3List.getObject(0);
		} else {
			%pos3Count = %area.myPos3List.getCount();
			for(%i = 0; %i < %pos3Count; %i++){
				%tempUnit = %area.myPos3List.getObject(%i);
				if(%tempUnit.dono != %player){
					%unit = %area.myPos3List.getObject(%i);
					echo("Unidade Inimiga encontrada, DONO: " @ %unit.dono.persona.nome);
					%i = %pos3Count;
				}
			}
		}
	} else if (%pos $= "pos4"){
		if(%area.dono !$= "MISTA"){
			%unit = %area.myPos4List.getObject(0);
		} else {
			%pos4Count = %area.myPos4List.getCount();
			for(%i = 0; %i < %pos4Count; %i++){
				%tempUnit = %area.myPos4List.getObject(%i);
				if(%tempUnit.dono != %player){
					%unit = %area.myPos4List.getObject(%i);
					echo("Unidade encontrada, DONO: " @ %unit.dono.persona.nome);
					%i = %pos4Count;
				}
			}
		}
	}
	return %unit;
}

function clientFindUnitInimigaEmboscada(%area, %player){ //o parâmetro %player é referente a quem está pedindo o comando, não ao inimigo;
	%tempUnit = %area.pos0Quem;
	if(%tempUnit.dono != %player && isObject(%tempUnit) && %tempUnit.gulok == true){
		%unidadeInimiga = %area.pos0Quem;	
	} else {
		%tempUnit = %area.pos1Quem;	
		if(%tempUnit.dono != %player){
			%unidadeInimiga = %area.pos1Quem;	
		} else {
			%tempUnit = %area.pos2Quem;	
			if(%tempUnit.dono != %player){
				%unidadeInimiga = %area.pos2Quem;	
			} else {
				%pos3Count = %area.myPos3List.getCount();
				for(%i = 0; %i < %pos3Count; %i++){
					%tempUnit = %area.myPos3List.getObject(%i);
					if(%tempUnit.dono != %player){
						%unidadeInimiga = %area.myPos3List.getObject(%i);
						%i = %pos3Count;
					}
				}	
			}
			if(!isObject(%unidadeInimiga)){
				%pos4Count = %area.myPos4List.getCount();
				for(%i = 0; %i < %pos4Count; %i++){
					%tempUnit = %area.myPos4List.getObject(%i);
					if(%tempUnit.dono != %player){
						%unidadeInimiga = %area.myPos4List.getObject(%i);
						%i = %pos4Count;
					}
				}	
			}
		}
	}
	
	echo("EMBOSCADA: unidade inimiga encontrada na " @ %unit.pos @ " de " @ %unit.onde.getName() @ " (" @ %unit.class @ " do " @ %unit.dono.id @ " - " @ %unit.dono.persona.nome @ ")");
	return %unidadeInimiga;
}

function emboscadaBtnClick(){
	%foco = Foco.getObject(0);
	clientAskEmboscada(%foco.onde, %foco.pos);
}

function clientAskEmboscada(%area, %posDeOrigem){
	if($mySelf.atacou){
		commandToServer('emboscada', %area.getName(), %posDeOrigem);	
		$ultimoAtaqueFinalizado = false;
		clientPushServerComDot();
	} else {
		$lastATKAreaDeOrigem = %area;
		$lastATKposDeOrigem = %posDeOrigem;
		clientPushEmboscadaQMsgBox();	
	}
}

function clientCmdExecutarEmboscada(%areaName, %posDeOrigem, %posAlvo, %atk, %def){
	clientPopServerComDot();
	clientCmdFire(%areaName, %posDeOrigem, %areaName, %posAlvo, %atk, %def, "no", "no", "no", "no");
	emboscada_btn.setVisible(false);
	schedule(1000, 0, "atualizarBotoesDeCompra"); //dá o tempo necessário pra peça morrer, ser reposta e pra área verificar se continua MISTA ou não
}

function clientConfirmarEmboscada(){
	%areaDeOrigemClient = $lastATKAreaDeOrigem.getName();
	%posDeOrigem = $lastATKposDeOrigem;
	commandToServer('emboscada', %areaDeOrigemClient, %posDeOrigem);
	$ultimoAtaqueFinalizado = false;
	clientPushServerComDot();
	clientPopEmboscadaQGui();
}

function clientPushEmboscadaQMsgBox(){
	canvas.pushDialog(emboscadaQGui);	
}

function clientPopEmboscadaQGui(){
	canvas.popDialog(emboscadaQGui);	
}