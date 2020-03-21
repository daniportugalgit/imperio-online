// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\server\serverAtaque.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 30 de outubro de 2007 22:21
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function jogo::getAreaNoJogo(%this, %areaNome)
{
	%eval = "%areaNoJogo = " @ %this @ "." @ %areaNome @ ";";
	eval(%eval);
	
	return %areaNoJogo;
}

function jogo::findPlayerUnit(%this, %area, %player){
	%areaNoJogo = %this.getAreaNoJogo(%area);
	if(!isObject(%areaNoJogo))
	{
		echo("ATENÇÃO: não encontrei a área no jogo!!!");	
		return;
	}
		
	if(%areaNoJogo.pos1Quem.dono == %player)
		return %areaNoJogo.pos1Quem;
		
	if(%areaNoJogo.pos2Quem.dono == %player)
		return %areaNoJogo.pos2Quem;
		
	if(isObject(%areaNoJogo.myPos4List))
	{
		for(%i = 0; %i < %areaNoJogo.myPos4List.getCount(); %i++)
		{
			if(%areaNoJogo.myPos4List.getObject(%i).dono == %player)
				return %areaNoJogo.myPos4List.getObject(%i);
		}
	}
	
	for(%i = 0; %i < %areaNoJogo.myPos3List.getCount(); %i++)
	{
		if(%areaNoJogo.myPos3List.getObject(%i).dono == %player)
			return %areaNoJogo.myPos3List.getObject(%i);
	}
	
	if(%areaNoJogo.pos0Quem.dono == %player)
		return %areaNoJogo.pos0Quem;
	
	//se chegou aki, não achou a unidade:
	echo("PLAYER UNIT **NOT** FOUND NA AREA: " @ %area @ "; player = " @ %player.id);		
}

/*
function jogo::findPlayerUnit(%this, %area, %player){
	%eval = "%areaNoJogo = " @ %this @ "." @ %area @ ";";
	eval(%eval);
	if(!isObject(%areaNoJogo)){
		echo("ATENÇÃO: não encontrei a área no jogo!!!");	
	} else {
		echo("BUSCANDO UNIDADE DO " @ %player.id @ " NA AREA " @ %areaNoJogo.myName);	
		echo("%areaNoJogo.pos1Quem = " @ %areaNoJogo.pos1Quem.class @ " -> " @ %areaNoJogo.pos1Quem.dono.id);
		echo("%areaNoJogo.pos2Quem = " @ %areaNoJogo.pos2Quem.class @ " -> " @ %areaNoJogo.pos2Quem.dono.id);
		for(%i = 0; %i < %areaNoJogo.myPos3List.getCount(); %i++){
			echo("%areaNoJogo.pos3Quem = " @ %areaNoJogo.myPos3List.getObject(%i).class @ " -> " @ %areaNoJogo.myPos3List.getObject(%i).dono.id);
		}
		for(%i = 0; %i < %areaNoJogo.myPos4List.getCount(); %i++){
			echo("%areaNoJogo.pos4Quem = " @ %areaNoJogo.myPos4List.getObject(%i).class @ " -> " @ %areaNoJogo.myPos4List.getObject(%i).dono.id);
		}
	}
	if(%areaNoJogo.pos1Quem.dono == %player){
		%unit = %areaNoJogo.pos1Quem;
		echo("PLAYER UNIT FOUND NA POS1: " @ %unit.class);
	} else if (%areaNoJogo.pos2Quem.dono == %player){
		%unit = %areaNoJogo.pos2Quem;
		echo("PLAYER UNIT FOUND NA POS2: " @ %unit.class);
	} else if(isObject(%areaNoJogo.myPos4List)){
		if (%areaNoJogo.myPos4List.getCount() > 0){
			for(%i = 0; %i < %areaNoJogo.myPos4List.getCount(); %i++){
				if(%areaNoJogo.myPos4List.getObject(%i).dono == %player){
					%unit =  %areaNoJogo.myPos4List.getObject(%i);
					echo("PLAYER UNIT FOUND NA POS4: " @ %unit.class);
					%i = %areaNoJogo.myPos4List.getCount(); //sai do loop;
				}
			}
		} else if (%areaNoJogo.myPos3List.getCount() > 0){
			for(%i = 0; %i < %areaNoJogo.myPos3List.getCount(); %i++){
				if(%areaNoJogo.myPos3List.getObject(%i).dono == %player){
					%unit =  %areaNoJogo.myPos3List.getObject(%i);
					echo("PLAYER UNIT FOUND NA POS3: " @ %unit.class);
					%i = %areaNoJogo.myPos3List.getCount(); //sai do loop;
				}
			}
		}
	} else if (%areaNoJogo.myPos3List.getCount() > 0){
		for(%i = 0; %i < %areaNoJogo.myPos3List.getCount(); %i++){
			if(%areaNoJogo.myPos3List.getObject(%i).dono == %player){
				echo("PLAYER UNIT FOUND NA POS3: " @ %unit.class);
				%unit =  %areaNoJogo.myPos3List.getObject(%i);
				%i = %areaNoJogo.myPos3List.getCount(); //sai do loop;
			}
		}
	}
	
	if(!isObject(%unit)){
		echo("PLAYER UNIT **NOT** FOUND NA AREA: " @ %area @ "; player = " @ %player);		
	}
	return %unit;
}
*/


/////////////////////////////////////////
//Verificação de delay nos clients:
function serverAtaqueOcorrendo(%jogo){
	%serverAtaqueOcorrendo = false;
	
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		if(%jogo.simPlayers.getObject(%i).ataqueOcorrendo == true && %jogo.simPlayers.getObject(%i).taMorto == false){
			%serverAtaqueOcorrendo = true;
		}
	}
			
	//echo("AtaqueOcorrendo, JOGO(" @ %jogo.num @ "): " @ %serverAtaqueOcorrendo);
	return %serverAtaqueOcorrendo;
}
///////////////////////////////////////////
//marcar no server que determinado client já atacou:
function serverCmdMarcarAtaqueConcluido(%client, %playerId){
	%jogo = %client.player.jogo;
	%eval = "%player = %jogo." @ %playerId @ ";";
	eval(%eval);
	
	%player.ataqueOcorrendo = false;
	//echo(%player.id @ ".client finalizou o ataque, JOGO(" @ %jogo.num @ ")");
}



function serverCmdAtacar(%client, %areaDeOrigem, %areaAlvo){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	
	if(%jogo.guloksDespertados){
		if(%areaAlvo $= "UNG_PaGu03"){
				
		}
	}
		
	if(%jogadorDaVez.client == %client || %jogadorDaVez == %jogo.aiPlayer){ //SEGURANÇA 1 -> se for a vez do client
		if(%jogo.primeiraRodada == false){
			if(serverAtaqueOcorrendo(%jogo) == false){
				///aki entra o server marcando que enviou o ataque para os clients:
				for(%i = 0; %i < %jogo.playersAtivos; %i++){
					%jogo.simPlayers.getObject(%i).ataqueOcorrendo = true;
				}
								
				//////////////////
				if(%areaDeOrigemNoJogo.dono $= "MISTA"){
					serverAtacarEspecial(%client, %areaDeOrigem, %areaAlvo);
				} else {
					if (%areaDeOrigemNoJogo.pos1Flag $= "nada" && %areaDeOrigemNoJogo.pos2Flag !$= "nada"){
						if (%areaAlvoNoJogo.pos1Flag $= "nada" && %areaAlvoNoJogo.pos2Flag !$= "nada"){
							serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, "pos2", "pos2");
						} else if (%areaAlvoNoJogo.pos1Flag !$= "nada" && %areaAlvoNoJogo.pos2Flag $= "nada"){
							serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, "pos2", "pos1");
						} else if(%areaAlvoNoJogo.pos1Flag $= "nada" && %areaAlvoNoJogo.pos2Flag $= "nada"){
							serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, "pos2", "pos0");
						} else {
							serverExecutarAtaque1x2(%jogo,%areaDeOrigem, %areaAlvo, "pos2");
						}
					} else if (%areaDeOrigemNoJogo.pos1Flag !$= "nada" && %areaDeOrigemNoJogo.pos2Flag $= "nada"){
						if (%areaAlvoNoJogo.pos1Flag $= "nada" && %areaAlvoNoJogo.pos2Flag !$= "nada"){
							serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, "pos1", "pos2");
						} else if (%areaAlvoNoJogo.pos1Flag !$= "nada" && %areaAlvoNoJogo.pos2Flag $= "nada"){
							serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, "pos1", "pos1");
						} else if(%areaAlvoNoJogo.pos1Flag $= "nada" && %areaAlvoNoJogo.pos2Flag $= "nada"){
							serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, "pos1", "pos0");
						} else {
							serverExecutarAtaque1x2(%jogo, %areaDeOrigem, %areaAlvo, "pos1");
						}
					} else if(%areaDeOrigemNoJogo.pos1Flag $= "nada" && %areaDeOrigemNoJogo.pos2Flag $= "nada"){
						if (%areaAlvoNoJogo.pos1Flag $= "nada" && %areaAlvoNoJogo.pos2Flag !$= "nada"){
							serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, "pos0", "pos2");
						} else if (%areaAlvoNoJogo.pos1Flag !$= "nada" && %areaAlvoNoJogo.pos2Flag $= "nada"){
							serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, "pos0", "pos1");
						} else if(%areaAlvoNoJogo.pos1Flag $= "nada" && %areaAlvoNoJogo.pos2Flag $= "nada"){
							serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, "pos0", "pos0");
						} else {
							serverExecutarAtaque1x2(%jogo, %areaDeOrigem, %areaAlvo, "pos0");
						}
					} else {
						if (%areaAlvoNoJogo.pos1Flag $= "nada" && %areaAlvoNoJogo.pos2Flag !$= "nada"){
							serverExecutarAtaque2x1(%jogo, %areaDeOrigem, %areaAlvo, "pos2");
						} else if (%areaAlvoNoJogo.pos1Flag !$= "nada" && %areaAlvoNoJogo.pos2Flag $= "nada"){
							serverExecutarAtaque2x1(%jogo, %areaDeOrigem, %areaAlvo, "pos1");
						} else if(%areaAlvoNoJogo.pos1Flag $= "nada" && %areaAlvoNoJogo.pos2Flag $= "nada"){
							serverExecutarAtaque2x1(%jogo, %areaDeOrigem, %areaAlvo, "pos0");
						} else {
							serverExecutarAtaque2x2(%jogo, %areaDeOrigem, %areaAlvo);
						}
					}
				}
				serverClearUndo(%jogadorDaVez);
			} else {
				echo("Ataque ignorado: serverAtaqueOcorrendo == true");	
				commandToClient(%client, 'clientMsgSincronizando');
			}
		} else {
			commandToClient(%client, 'clientMsgAtkPrimeiraRodada');	
		}
	}
}

//dados:
function dadoMM(%min, %max, %bonus){
	%resultado = getRandom(%min, %max) + %bonus;
	return %resultado;
}

function a_dado(%unit, %defOuAtk){
	%persona = %unit.dono.persona;
		
	if(%unit.class $= "verme" || %unit.class $= "soldado"){
		%prefixoBase = "aca_s_";
	} else if(%unit.class $= "rainha" || %unit.class $= "tanque"){
		%prefixoBase = "aca_t_";
	} else if(%unit.class $= "cefalok" || %unit.class $= "navio"){
		%prefixoBase = "aca_n_";
	} else if(%unit.class $= "zangao" || %unit.class $= "lider"){
		%prefixoBase = "aca_ldr_" @ %unit.liderNum @ "_"; //fica "aca_ldr_2_", por exemplo
	}
		
	if(%defOuAtk $= "Def"){
		%prefixo1 = %prefixoBase @ "d_min";
		%prefixo2 = %prefixoBase @ "d_max";
	} else {
		%prefixo1 = %prefixoBase @ "a_min";
		%prefixo2 = %prefixoBase @ "a_max";
	}
	
	%eval = "%minimo = %persona." @ %prefixo1 @ ";";
	eval(%eval);
	%eval = "%maximo = %persona." @ %prefixo2 @ ";";
	eval(%eval);
	
	if(%defOuAtk $= "Def"){
		%minimo += %unit.dono.moralDefesa;
		%maximo += %unit.dono.moralDefesa;
		//soma a carapaça se o truta for humano:
		if(%unit.dono.persona.especie $= "humano"){
			//%minimo += %unit.dono.persona.aca_av_1 * 2;
			%maximo += %unit.dono.persona.aca_av_1 * 2;
			
			//verifica se existe um vírus na área:
			if(isObject(%unit.dono.jogo.simAreasComVirus)){
				if(%unit.dono.jogo.simAreasComVirus.isMember(%unit.onde)){
					%minimo -= 10;
					%maximo -= 10;
					if(%minimo < 1){
						%minimo = 1;	
					}
					if(%maximo < 1){
						%maximo = 1;	
					}
				}
			}
			
		} else if(%unit.dono.persona.especie $= "gulok"){
			if(%unit.class $= "rainha" && $tipoDeJogo !$= "semPesquisas"){
				if(%unit.dono.instinto){
					%count = serverGetOvosCount(%unit.onde);//%unit.onde.myPos4List.getCount();
					if(%count > 0 && %unit.onde.myPos4List.getObject(0).class $= "ovo"){
						%minimo += %unit.dono.persona.aca_v_2 * %count;	
						%maximo += %unit.dono.persona.aca_v_2 * %count;
					}
				}
			} else if(%unit.class $= "verme" && $tipoDeJogo !$= "semPesquisas"){
				%minimo += %unit.dono.persona.aca_a_1;
				%maximo += %unit.dono.persona.aca_a_1;	
				
				%maximo += serverPegarBonusHorda(%unit.onde);
			} else if(%unit.class $= "zangao" && $tipoDeJogo !$= "semPesquisas"){
				%minimo += %unit.myBonusDevorar;
				%maximo += %unit.myBonusDevorar;	
			}
			if(isObject(%unit.dono.mySimMatriarcas)){
				if(%unit.dono.mySimMatriarcas.getCount() > 0){
					//%minimo += %unit.dono.persona.aca_v_6 * 2;
					%maximo += %unit.dono.persona.aca_v_6 * 2;
				}
			}
		}
	} else {
		%minimo += %unit.dono.moralAtaque;
		%maximo += %unit.dono.moralAtaque;
		
		//soma a mira eletrônica, se o truta for humano:
		if(%unit.dono.persona.especie $= "humano"){
			//%minimo += %unit.dono.persona.aca_av_2 * 2;
			%maximo += %unit.dono.persona.aca_av_2 * 2;
		} else if(%unit.dono.persona.especie $= "gulok"){
			if(%unit.class $= "verme"){
				%maximo += serverPegarBonusHorda(%unit.onde);	
			} else if(%unit.class $= "zangao"){
				%maximo += %unit.myBonusCanibalMax;
				%minimo += %unit.myBonusDevorar;
				%maximo += %unit.myBonusDevorar;
			}
			if(isObject(%unit.dono.mySimMatriarcas)){
				if(%unit.dono.mySimMatriarcas.getCount() > 0){
					//%minimo += %unit.dono.persona.aca_v_6 * 2;
					%maximo += %unit.dono.persona.aca_v_6 * 2;
				}
			}
		}
	}
	
	if(%unit.grandeMatriarca){
		%minimo = 12;
		%maximo = 50;
	}
		
	%resultado = dadoMM(%minimo, %maximo, 0);
	echo("COMBATE -> " @ %unit.class @ " " @ %unit.dono.persona.nome @ "-> de " @ %minimo @ " a " @ %maximo @ " => RESULTADO: " @ %resultado);
	
	if(%resultado > 50){
		%resultado = 50;	
	}
	return %resultado;
}

function serverGetOvosCount(%ondeNoJogo)
{
	if(!isObject(%ondeNoJogo.myPos4List))
		return;
		
	for(%i = 0; %i < %ondeNoJogo.myPos4List.getCount(); %i++)
	{
		%unit = %ondeNoJogo.myPos4List.getObject(%i);
		if(%unit.class $= "ovo")
			%count++;
	}
	return %count;
}


//ataque 1x1:
function serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, %posDeOrigem, %posAlvo){
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	
	%eval = "%areaAlvoPosQuem = %areaAlvoNoJogo." @ %posAlvo @ "Quem;";
	eval(%eval);
	%eval = "%areaDeOrigemPosQuem = %areaDeOrigemNoJogo." @ %posDeOrigem @ "Quem;";
	eval(%eval);
	
	if(%jogo.semPesquisas){
		%atk = dado(%areaDeOrigemPosQuem.dado, 0);
		%def = dado(%areaAlvoPosQuem.dado, 0);
	} else {
		%atk = a_dado(%areaDeOrigemPosQuem, "Atk");
		%def = a_dado(%areaAlvoPosQuem, "Def");
	}
	%areaDeOrigemPosQuem.dono.atacou = 1;
		
	echo("Atk(" @ %atk @ ") vs Def(" @ %def @ ")");
	if (%atk > %def){
		%areaDeOrigemPosQuem.fire(%areaAlvoPosQuem); //atualiza no server
	} else {
		%areaAlvoPosQuem.fire(%areaDeOrigemPosQuem);
	}
	
	//clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk, %resultDef, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2)
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		commandToClient(%jogo.simPlayers.getObject(%i).client, 'fire', %areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %atk, %def, "no", "no", "no", "no");
	}	
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'fire', %areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %atk, %def, "no", "no", "no", "no");
	}
}


function serverExecutarAtaque1x2(%jogo, %areaDeOrigem, %areaAlvo, %pos){
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	%eval = "%areaDeOrigemPosQuem = %areaDeOrigemNoJogo." @ %pos @ "Quem;";
	eval(%eval);

	if(%jogo.semPesquisas){
		%atk = dado(%areaDeOrigemPosQuem.dado, 0);
		%def1 = dado(%areaAlvoNoJogo.Pos1Quem.dado, 0);
		%def2 = dado(%areaAlvoNoJogo.Pos2Quem.dado, 0);
	} else {
		%atk = a_dado(%areaDeOrigemPosQuem, "Atk");
		%def1 = a_dado(%areaAlvoNoJogo.Pos1Quem, "Def");
		%def2 = a_dado(%areaAlvoNoJogo.Pos2Quem, "Def");
	}
	%areaDeOrigemPosQuem.dono.atacou = 1;

	if (%def1 >= %def2){
		echo("Def1(" @ %def1 @ ") vs Atk(" @ %atk @ ")  -- Def2(" @ %def2 @ ")");
		if (%def1 >= %atk){
			%areaAlvoNoJogo.Pos1Quem.fire(%areaDeOrigemPosQuem);
		} else {
			%areaDeOrigemPosQuem.fire(%areaAlvoNoJogo.Pos1Quem);
		}
		//clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk, %resultDef, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2)
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			commandToClient(%jogo.simPlayers.getObject(%i).client, 'fire', %areaDeOrigem, %pos, %areaAlvo, "pos1", %atk, %def1, "no", "pos2", "no", %def2); 
		}
		if(%jogo.observadorOn){
			commandToClient(%jogo.observador, 'fire', %areaDeOrigem, %pos, %areaAlvo, "pos1", %atk, %def1, "no", "pos2", "no", %def2); 
		}
	} else {
		echo("Def2(" @ %def2 @ ") vs Atk(" @ %atk @ ")  -- Def1(" @ %def1 @ ")");
		if (%def2 >= %atk){
			%areaAlvoNoJogo.Pos2Quem.fire(%areaDeOrigemPosQuem);
		} else {
			%areaDeOrigemPosQuem.fire(%areaAlvoNoJogo.Pos2Quem);
		}
		//clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk, %resultDef, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2)
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			commandToClient(%jogo.simPlayers.getObject(%i).client, 'fire', %areaDeOrigem, %pos, %areaAlvo, "pos2", %atk, %def2, "no", "pos1", "no", %def1); 
		}
		if(%jogo.observadorOn){
			commandToClient(%jogo.observador, 'fire', %areaDeOrigem, %pos, %areaAlvo, "pos2", %atk, %def2, "no", "pos1", "no", %def1); 
		}
	}
}




function serverExecutarAtaque2x1(%jogo, %areaDeOrigem, %areaAlvo, %pos){
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	%eval = "%areaAlvoPosQuem = %areaAlvoNoJogo." @ %pos @ "Quem;";
	eval(%eval);
	
	if(%jogo.semPesquisas){
		%atk1 = dado(%areaDeOrigemNoJogo.Pos1Quem.dado, 0);
		%atk2 = dado(%areaDeOrigemNoJogo.Pos2Quem.dado, 0);
		%def = dado(%areaAlvoPosQuem.dado, 0);
	} else {
		%atk1 = a_dado(%areaDeOrigemNoJogo.Pos1Quem, "Atk");
		%atk2 = a_dado(%areaDeOrigemNoJogo.Pos2Quem, "Atk");
		%def = a_dado(%areaAlvoPosQuem, "Def");
	}
	%areaDeOrigemNoJogo.Pos1Quem.dono.atacou = 1;
	%areaDeOrigemNoJogo.Pos2Quem.dono.atacou = 1;
	

	if (%atk1 >= %atk2){
		echo("Atk1(" @ %atk1 @ ") vs Def(" @ %def @ ")  -- Atk2(" @ %atk2 @ ")");
		if (%atk1 > %def){
			%areaDeOrigemNoJogo.Pos1Quem.fire(%areaAlvoPosQuem);
		} else {
			%areaAlvoPosQuem.fire(%areaDeOrigemNoJogo.Pos1Quem);
		}
		//clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk, %resultDef, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2)
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			commandToClient(%jogo.simPlayers.getObject(%i).client, 'fire', %areaDeOrigem, "pos1", %areaAlvo, %pos, %atk1, %def, "pos2", "no", %atk2, "no"); 
		}
		if(%jogo.observadorOn){
			commandToClient(%jogo.observador, 'fire', %areaDeOrigem, "pos1", %areaAlvo, %pos, %atk1, %def, "pos2", "no", %atk2, "no");
		}
	} else {
		echo("Atk2(" @ %atk2 @ ") vs Def(" @ %def @ ")  -- Atk1(" @ %atk1 @ ")");
		if (%atk2 > %def){
			%areaDeOrigemNoJogo.Pos2Quem.fire(%areaAlvoPosQuem);
		} else {
			%areaAlvoPosQuem.fire(%areaDeOrigemNoJogo.Pos2Quem);
		}
		//clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk, %resultDef, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2)
		for(%i = 0; %i < %jogo.playersAtivos; %i++){
			commandToClient(%jogo.simPlayers.getObject(%i).client, 'fire', %areaDeOrigem, "pos2", %areaAlvo, %pos, %atk2, %def, "pos1", "no", %atk1, "no"); 
		}
		if(%jogo.observadorOn){
			commandToClient(%jogo.observador, 'fire', %areaDeOrigem, "pos2", %areaAlvo, %pos, %atk2, %def, "pos1", "no", %atk1, "no"); 
		}
	}
}
///////////////


function serverExecutarAtaque2x2(%jogo, %areaDeOrigem, %areaAlvo){
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	
	if(%jogo.semPesquisas){
		%atk1 = dado(%areaDeOrigemNoJogo.Pos1Quem.dado, 0);
		%atk2 = dado(%areaDeOrigemNoJogo.Pos2Quem.dado, 0);
		%def1 = dado(%areaAlvoNoJogo.Pos1Quem.dado, 0);
		%def2 = dado(%areaAlvoNoJogo.Pos2Quem.dado, 0);
	} else {
		%atk1 = a_dado(%areaDeOrigemNoJogo.Pos1Quem, "Atk");
		%atk2 = a_dado(%areaDeOrigemNoJogo.Pos2Quem, "Atk");
		%def1 = a_dado(%areaAlvoNoJogo.Pos1Quem, "Def");
		%def2 = a_dado(%areaAlvoNoJogo.Pos2Quem, "Def");
	}
	%areaDeOrigemNoJogo.Pos1Quem.dono.atacou = 1;
	%areaDeOrigemNoJogo.Pos2Quem.dono.atacou = 1;
	
	%atk1MaiorQueDef1 = false;
	%atk1MaiorQueDef2 = false;
	%atk2MaiorQueDef1 = false;
	%atk2MaiorQueDef2 = false;
	%def1MaiorQueAtk1 = false;
	%def1MaiorQueAtk2 = false;
	%def2MaiorQueAtk1 = false;
	%def2MaiorQueAtk2 = false;
	
	if (%atk1 >= %atk2){
		if (%def1 >= %def2){
			echo("Atk1(" @ %atk1 @ ") vs Def1(" @ %def1 @ ") -- Atk2(" @ %atk2 @ ") vs Def2(" @ %def2 @ ")");
			if (%atk1 > %def1){
				%atk1MaiorQueDef1 = true;
			} else {
				%def1MaiorQueAtk1 = true;
			}
			if (%atk2 > %def2){
				%atk2MaiorQueDef2 = true;
			} else {
				%def2MaiorQueAtk2 = true;
			}
			//clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk, %resultDef, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2)
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'fire', %areaDeOrigem, "pos1", %areaAlvo, "pos1", %atk1, %def1, "pos2", "pos2", %atk2, %def2); 
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'fire', %areaDeOrigem, "pos1", %areaAlvo, "pos1", %atk1, %def1, "pos2", "pos2", %atk2, %def2); 
			}
		} else {
			echo("Atk1(" @ %atk1 @ ") vs Def2(" @ %def2 @ ") -- Atk2(" @ %atk2 @ ") vs Def1(" @ %def1 @ ")");
			if (%atk1 > %def2){
				%atk1MaiorQueDef2 = true;
			} else {
				%def2MaiorQueAtk1 = true;
			}
			if (%atk2 > %def1){
				%atk2MaiorQueDef1 = true;
			} else {
				%def1MaiorQueAtk2 = true;
			}
			//clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk, %resultDef, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2)
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'fire', %areaDeOrigem, "pos1", %areaAlvo, "pos2", %atk1, %def2, "pos2", "pos1", %atk2, %def1); 
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'fire', %areaDeOrigem, "pos1", %areaAlvo, "pos2", %atk1, %def2, "pos2", "pos1", %atk2, %def1); 
			}
		}
	} else {
		if (%def1 >= %def2){
			echo("Atk2(" @ %atk2 @ ") vs Def1(" @ %def1 @ ") -- Atk1(" @ %atk1 @ ") vs Def2(" @ %def2 @ ")");
			if (%atk2 > %def1){
				%atk2MaiorQueDef1 = true;
			} else {
				%def1MaiorQueAtk2 = true;
			}
			if (%atk1 > %def2){
				%atk1MaiorQueDef2 = true;
			} else {
				%def2MaiorQueAtk1 = true;
			}
			//clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk, %resultDef, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2)
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'fire', %areaDeOrigem, "pos2", %areaAlvo, "pos1", %atk2, %def1, "pos1", "pos2", %atk1, %def2); 
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'fire', %areaDeOrigem, "pos2", %areaAlvo, "pos1", %atk2, %def1, "pos1", "pos2", %atk1, %def2); 
			}
		} else {
			echo("Atk2(" @ %atk2 @ ") vs Def2(" @ %def2 @ ") -- Atk1(" @ %atk1 @ ") vs Def1(" @ %def1 @ ")");
			if (%atk2 > %def2){
				%atk2MaiorQueDef2 = true;
			} else {
				%def2MaiorQueAtk2 = true;
			}
			if (%atk1 > %def1){
				%atk1MaiorQueDef1 = true;
			} else {
				%def1MaiorQueAtk1 = true;
			}
			//clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk, %resultDef, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2)
			for(%i = 0; %i < %jogo.playersAtivos; %i++){
				commandToClient(%jogo.simPlayers.getObject(%i).client, 'fire', %areaDeOrigem, "pos2", %areaAlvo, "pos2", %atk2, %def2, "pos1", "pos1", %atk1, %def1); 
			}
			if(%jogo.observadorOn){
				commandToClient(%jogo.observador, 'fire', %areaDeOrigem, "pos2", %areaAlvo, "pos2", %atk2, %def2, "pos1", "pos1", %atk1, %def1); 
			}
		}
	}
	
	
	
	serverFire2x2(%jogo, %areaDeOrigem, %areaAlvo, %atk1MaiorQueDef1, %atk1MaiorQueDef2, %atk2MaiorQueDef1, %atk2MaiorQueDef2, %def1MaiorQueAtk1, %def1MaiorQueAtk2, %def2MaiorQueAtk1, %def2MaiorQueAtk2);
}


function serverFire2x2(%jogo, %areaDeOrigem, %areaAlvo, %atk1MaiorQueDef1, %atk1MaiorQueDef2, %atk2MaiorQueDef1, %atk2MaiorQueDef2, %def1MaiorQueAtk1, %def1MaiorQueAtk2, %def2MaiorQueAtk1, %def2MaiorQueAtk2){
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	
	%atacante1 = %areaDeOrigemNoJogo.pos1Quem;
	%atacante2 = %areaDeOrigemNoJogo.pos2Quem;
	%defensor1 = %areaAlvoNoJogo.pos1Quem;
	%defensor2 = %areaAlvoNoJogo.pos2Quem;
		
	//if((%atk1MaiorQueDef1 && %atk2MaiorQueDef2) || (%def1MaiorQueAtk1 && %def2MaiorQueAtk2)){
	if(%atk1MaiorQueDef1 && %atk2MaiorQueDef2){
		//%fire2x2:
		serverFireSemRepor(%atacante1, %defensor1, %atacante2, %defensor2);
	} else if (%def1MaiorQueAtk1 && %def2MaiorQueAtk2){
		//%fire2x2:
		serverFireSemRepor(%defensor1, %atacante1, %defensor2, %atacante2);
	} else {
		//%fire2x2 = false;
		if(%atk1MaiorQueDef1 && %Def2MaiorQueAtk2){
			serverFireSemRepor(%atacante1, %defensor1, %defensor2, %atacante2);
		} else if(%atk1MaiorQueDef2 && %Def1MaiorQueAtk2){
			serverFireSemRepor(%atacante1, %defensor2, %defensor1, %atacante2);
		} else if(%atk2MaiorQueDef1 && %Def2MaiorQueAtk1){
			serverFireSemRepor(%atacante2, %defensor1, %defensor2, %atacante1);
		} else if(%atk1MaiorQueDef2 && %Atk2MaiorQueDef1){
			serverFireSemRepor(%atacante1, %defensor2, %atacante2, %defensor1);
		} else if(%def1MaiorQueAtk1 && %Atk2MaiorQueDef2){
			serverFireSemRepor(%defensor1, %atacante1, %atacante2, %defensor2);
		} else if(%def2MaiorQueAtk1 && %def1MaiorQueAtk2){
			serverFireSemRepor(%defensor2, %atacante1, %defensor1, %atacante2);
		} else {
			echo("serverFire2x2: ERRO, condição impossível!");	
		}
	}
}
 
function serverUnitAtk(%quemMata, %quemMorre){
	%quemMata.fire(%quemMorre);
}

function serverFireSemRepor(%quemMata1, %quemMorre1, %quemMata2, %quemMorre2){
	%jogo = %quemMata1.dono.jogo;
	%area1 = %quemMorre1.onde;
	%area2 = %quemMorre2.onde;
	
	%quemMorre1.kill(%quemMata1.dono);
	%quemMorre2.kill(%quemMata2.dono);
}




function serverAtacarEspecial(%client, %areaDeOrigem, %areaAlvo){
	%jogo = %client.player.jogo;
	%jogadorDaVez = %jogo.jogadorDaVez;
	%eval = "%areaDeOrigemNoJogo = %jogo." @ %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	
	//marca no server que enviou o ataque para os clients:
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		%jogo.simPlayers.getObject(%i).ataqueOcorrendo = true;
	}
	
	//encontra a unidade atacante, para pegar sua posição (não pode depender do client, senão permite cheats!
	%unit = %jogo.findPlayerUnit(%areaDeOrigem, %jogadorDaVez);
	
	%jogadorDaVez.atacou = 1; //para ver se é diplomata
	%unitDefensora = serverGetUnitDefensoraEspecial(%areaAlvoNoJogo);
	
	if(%unit.pos $= "pos1" || %unit.pos $= "pos2"){ 
		if (%areaAlvoNoJogo.pos2Flag $= "nada"){
			//ataque 1x1
			serverExecutarAtaque1x1(%jogo, %areaDeOrigem, %areaAlvo, %unit.pos, %unitDefensora.pos);
		} else {
			//ataque 1x2
			serverExecutarAtaque1x2(%jogo, %areaDeOrigem, %areaAlvo, %unit.pos);
		}	
	} else {
		//tem que fazer um ataque da posição 3 ou 4!	
		serverExecutarAtaquePosReserva(%jogo, %unit, %areaDeOrigem, %areaAlvo);
	}
}

function serverExecutarAtaquePosReserva(%jogo, %unit, %areaDeOrigem, %areaAlvo){
	
	%eval = "%areaAlvoNoJogo = %jogo." @ %areaAlvo @ ";";
	eval(%eval);
	
	%unitDefensora = serverGetUnitDefensoraEspecial(%areaAlvoNoJogo);
	if(!isObject(%unitDefensora))
	{
		commandToClient(%unit.dono.client, 'erro2b');
		echo("############### ERRO 2B ##############");
		return;
	}
		
	
	if(%jogo.semPesquisas){
		%atk = dado(%unit.dado, 0);
		%def = dado(%unitDefensora.dado, 0);
	} else {
		%atk = a_dado(%unit, "Atk");
		%def = a_dado(%unitDefensora, "Def");
	}
	
	echo(%unit.class @ " " @ %unit.dono.persona.nome @ " (" @ %atk @ ") vs (" @ %def @ ") " @ %unitDefensora.class @ " " @ %unitDefensora.dono.persona.nome);
	if (%atk > %def){
		%unit.fire(%unitDefensora); //atualiza no server
	} else {
		%unitDefensora.fire(%unit);
	}
	
	//clientCmdFire(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posAlvo, %resultAtk, %resultDef, %posDeOrigem2, %posAlvo2, %resultAtk2, %resultDef2)
	for(%i = 0; %i < %jogo.playersAtivos; %i++){
		commandToClient(%jogo.simPlayers.getObject(%i).client, 'fire', %areaDeOrigem, %unit.pos, %areaAlvo, %unitDefensora.pos, %atk, %def, "no", "no", "no", "no"); 
	}	
	if(%jogo.observadorOn){
		commandToClient(%jogo.observador, 'fire', %areaDeOrigem, %unit.pos, %areaAlvo, %unitDefensora.pos, %atk, %def, "no", "no", "no", "no"); 
	}
}

function serverGetUnitDefensoraEspecial(%areaAlvoNoJogo)
{
	if(isObject(%areaAlvoNoJogo.Pos1Quem))
		return %areaAlvoNoJogo.Pos1Quem;
		
	if(isObject(%areaAlvoNoJogo.Pos2Quem))
		return %areaAlvoNoJogo.Pos2Quem;
		
	if(isObject(%areaAlvoNoJogo.Pos0Quem) && %areaAlvoNoJogo.Pos0Quem.class $= "rainha" && !%areaAlvoNoJogo.Pos0Quem.crisalida)
		return %areaAlvoNoJogo.Pos0Quem;
}