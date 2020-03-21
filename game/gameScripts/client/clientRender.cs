// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientRender.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 25 de janeiro de 2008 17:02
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function clientAskRenderUnidade(){
	%unit = $myLastUnitPraRender;
	%pos = %unit.pos;
	%area = %unit.onde;
	%areaClient = %area.getName();
	msgGui.setVisible(false);
	commandToServer('renderUnidade', %areaClient, %pos);	
	clientClearCentralButtonControl();
}

function clientCmdRenderUnidade(%areaClient, %pos, %playerId){
	%unit = clientGetGameUnitDoPlayer(%areaClient, %pos, %playerId);
	%unit.render();
}

function clientAskRenderTodos(){
	clientPushRenderTodosMsgBox();
	commandToServer('renderTodos');	
}

function clientCmdKillUnidadesRestantes(%playerId)
{
	%eval = "%player = $" @ %playerId @ ";";
	eval(%eval);
	
	%unitCount = %player.mySimUnits.getCount();
	for(%i = 0; %i < %unitCount; %i++)
	{
		%unit = %player.mySimUnits.getObject(0);
		%unit.kill();
	}
}

function clientCmdRenderTodos(%rendidoId){
	clientPopRenderTodosMsgBox();
	%eval = "%rendido = $" @ %rendidoId @ ";";
	eval(%eval);
	//echo("RENDER TODOS: %rendidoId =" SPC %rendidoId @ ", %rendido =" SPC %rendido @ ", %rendido.id =" SPC %rendidoId.id);
	
	//tira as infos do jogador:
	for(%i = 0; %i < %rendido.mySimInfo.getCount(); %i++){
		%info = %rendido.mySimInfo.getObject(0);
		%rendido.mySimInfo.remove(%info);
	}
	
	//tira os acordos do jogador:
	for(%i = 0; %i < %rendido.mySimExpl.getCount(); %i++){
		%info = %rendido.mySimExpl.getObject(0);
		%rendido.mySimExpl.remove(%info);
	}
	
	//rende cada unidade do jogador:
	for(%j = 0; %j < %rendido.mySimUnits.getCount(); %j++){
		%unit = %rendido.mySimUnits.getObject(0);
		%unit.render();
	}
	
	//se tiver rendido todas, deleta as bases:
	if(%rendido.mySimUnits.getCount() > 0){
		schedule(300, 0, "clientCmdRenderTodos", %rendidoId);	
	} else { //se acabaram as unidades de quem se rendeu
		%areaCount = %rendido.mySimAreas.getCount();
		if(%areaCount > 0){ //se restaram áreas
			for(%i = 0; %i < %areaCount; %i++){ 
				%area = %rendido.mySimAreas.getObject(0);
				if(%area.pos0Flag){ //se a área tem base
					if(isObject(%area.pos0Quem.myReciclarEffect))
						%area.pos0Quem.myReciclarEffect.safeDelete();
						
					%area.pos0Quem.safeDelete(); //deleta a peça
					%area.pos0Flag = false; //deleta a peça
					%rendido.mySimAreas.remove(%area); //remove a área do player;
					%area.pos0Quem = 0; //marca que ela não é mais o quem da pos0 da área
					%area.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
				}
			}
		}
	}
	clientAtualizarExplHud();
	clientAtualizarEstatisticas();
	if($mySelf.id $= %rendidoId){
		//apaga o btn de finalizar jogada:
		finalizarTurno_btn.setVisible(false);
		
		//deleta todos os marcadores de missão:
		for(%i = 0; %i < 78; %i++){
			%info = clientFindInfo(%i + 1);
			if(isObject(%info.myMark)){
				%info.myMark.safeDelete();	
			}
			if(isObject(%info.myExplMark)){
				%info.myExplMark.safeDelete();	
			}
			%info.jahFoiOferecida = false;
		}
		$mySelf.mySimInfo.clear();
		$mySelf.mySimExpl.clear();
	}
}


function clientGetGameUnitDoPlayer(%area, %pos, %playerId){
	if(%pos $= "pos2" || %pos $= "pos1" || %pos $= "pos0"){
		%eval = "%unit = " @ %area @ "." @ %pos @ "Quem;";
		eval(%eval);
	} else if(%pos $= "pos3"){
		if(%area.dono !$= "MISTA" && %area.dono !$= "COMPARTILHADA"){
			%unit = %area.myPos3List.getObject(0);
		} else {
			%pos3Count = %area.myPos3List.getCount();
			for(%i = 0; %i < %pos3Count; %i++){
				%tempUnit = %area.myPos3List.getObject(%i);
				if(%tempUnit.dono.id $= %playerId){
					//echo("Unidade visitante encontrada");
					%unit = %area.myPos3List.getObject(%i);
					echo("Unidade encontrada, DONO: " @ %unit.dono);
					%i = %pos3Count;
				}
			}
		}
	} else if (%pos $= "pos4"){
		if(%area.dono !$= "MISTA" && %area.dono !$= "COMPARTILHADA"){
			%unit = %area.myPos4List.getObject(0);
		} else {
			%pos4Count = %area.myPos4List.getCount();
			for(%i = 0; %i < %pos4Count; %i++){
				%tempUnit = %area.myPos4List.getObject(%i);
				if(%tempUnit.dono.id $= %playerId){
					//echo("Unidade visitante encontrada");
					%unit = %area.myPos4List.getObject(%i);
					echo("Unidade encontrada, DONO: " @ %unit.dono);
					%i = %pos4Count;
				}
			}
		}
	}
	
	return %unit;
}
