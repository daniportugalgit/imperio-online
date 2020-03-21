// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientAtualizarEstatisticas.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 25 de janeiro de 2008 17:04
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function clientCmdAtualizarEstatisticas(){
	clientAtualizarEstatisticas();	
}

$statsAtuais = false; //para que a função possa fazer uma coisa uma única vez, ligar os gauges.

function clientLigarObjetivosGauges()
{
	if ($statsAtuais)
		return;
		
	gauge1_1.setVisible(true);
	gauge1_2.setVisible(true);
	gauge2_1.setVisible(true);
	gauge2_2.setVisible(true);
	$statsAtuais = true;
}

function clientSetMyRendaImperiais()
{
	%quantasAreas = $mySelf.mySimAreas.getCount(); //guarda o número de áreas na variável
	%imperiaisPorAreas = mFloor(%quantasAreas/2);
	
	if($myPersona.aca_av_4 > 0 && $myPersona.especie $= "humano"){
		%imperiaisPorSatelite = clientGetRendaImperiaisPorSatelite(%imperiaisPorAreas);
		clientAtualizarRendaImperiaisComSatelite(%imperiaisPorAreas, %imperiaisPorSatelite);
	} else {
		clientAtualizarRendaImperiaisSemSatelite(%imperiaisPorAreas);
	}
}

function clientGetRendaImperiaisPorSatelite(%imperiaisPorAreas)
{
	if($myPersona.especie !$= "humano" || $myPersona.aca_av_4 <= 0)
		return 0;
	
	
	switch ($myPersona.aca_av_4)
	{
		case 1: %mod = 0.3;	
		case 2: %mod = 0.5;
		case 3: %mod = 0.7;
	}
	
	return (mFloor(%imperiaisPorAreas * %mod));
}

function clientAtualizarRendaImperiaisSemSatelite(%val)
{
	rendaImperiais_txt.text = "+ " @ %val; 	
}

function clientAtualizarRendaImperiaisComSatelite(%imperiaisPorAreas, %imperiaisPorSatelite)
{
	rendaImperiais_txt.text = "+ " @ %imperiaisPorAreas @ " + " @ %imperiaisPorSatelite; 
}

function clientGetRendaMineriosPorRefinarias(%player)
{
	%minerios = 0;
	
	for(%i = 0; %i < %player.mySimRefinarias.getCount(); %i++)
	{
		%minerios++;	
	}	
	
	return %minerios;
}

function clientGetAreasTerrestres(%player)
{
	for(%i = 0; %i < %player.mySimAreas.getCount(); %i++)
	{
		%area = %player.mySimAreas.getObject(%i);
		if(%area.terreno $= "terra")
			%terrestres += 1;
	}
	return %terrestres;
}

function clientGetAreasMaritimas(%player)
{
	for(%i = 0; %i < %player.mySimAreas.getCount(); %i++)
	{
		%area = %player.mySimAreas.getObject(%i);
		if(%area.terreno $= "mar")
			%mares += 1;
	}
	return %mares;
}

function clientAtualizarRendaAreasTerrestres(%val)
{
	areasTerrestres_txt.text = %val; 
}

function clientAtualizarRendaAreasMaritimas(%val)
{
	areasMaritimas_txt.text = %val; 
}

function clientAtualizarRendaBases(%bases)
{
	bases_txt.text = %bases;	
}

function clientGetBases(%player)
{
	if($jogoEmDuplas)
	{
		for(%i = 0; %i < %player.mySimAreas.getCount(); %i++)
		{
			%area = %player.mySimAreas.getObject(%i);
			if(%area.pos0Flag == 1)
				%bases++;			
		}
		return %bases;
	}
	
	return %player.mySimBases.getcount();
}

function clientGetImperio(%player)
{
	%bases = clientGetBases(%player);
	if($jogoEmDuplas && %bases > 14)
		return true;
	
	if(%bases > 9)
		return true;
		
	return false;
}

function clientSetImperio(%player)
{
	if(!clientGetImperio(%player))
	{
		imperioMark_img.setVisible(false);
		return;
	}
		
	imperioMark_img.setVisible(true);
	imperioMark_img.bitmap = "~/data/images/imperioMark" @ $mySelf.myColor @ ".png";
}

function clientGetValorDoImperio()
{
	%imperiosFeitos = clientGetImperiosFeitos($playersNesteJogo);
	%valorDoImperio = mFloor(10 / %imperiosFeitos);
	return %valorDoImperio;
}

function clientAtualizarEstatisticas()
{
	if($souObservador){
		schedule(100, 0, "clientOBS_atualizarEstatisticas");
		return;
	}
	
	clientLigarObjetivosGauges();
	clientSetMyRendaImperiais();
		
	//zera as variáveis:
	%bases = 0;
	%minerios = 0;
	%petroleos = 0;
	%uranios = 0;
	%pontos = 0;
		
	%mineriosDeRefinarias = clientGetRendaMineriosPorRefinarias($mySelf);
	%minerios += %mineriosDeRefinarias;
	
	%terrestres = clientGetAreasTerrestres($mySelf); 
	%maritimas = clientGetAreasMaritimas($mySelf);	
		
	clientAtualizarRendaAreasTerrestres(%terrestres);
	clientAtualizarRendaAreasMaritimas(%maritimas);
	clientAtualizarRendaBases(clientGetBases($mySelf));
	
	if(clientGetImperio($mySelf))
		%pontos += clientGetValorDoImperio();
		
	clientSetImperio($mySelf);
	
	
	//Almirante:
	%basesNoMar = 0;
	if($myPersona.aca_i_3 > 0){
		for(%i = 0; %i < $mySelf.mySimAreas.getCount(); %i++){
			%area = $mySelf.mySimAreas.getObject(%i);
			if(%area.terreno $= "mar"){
				if(%area.pos0Flag == 1){
					%basesNoMar++;	
				}
			}
		}
		if(%basesNoMar > 4){
			%sum = 3 + $myPersona.aca_i_3;
			%pontos += %sum; //Almirante	
		}
	}
	
	//verifica as crisalidas e matriarcas:
	if(isObject($mySelf.mySimCrisalidas)){
		if($mySelf.mySimCrisalidas.getcount() > 0){
			%pontos += $mySelf.mySimCrisalidas.getcount() * 3;	
		}
	}
	if(isObject($mySelf.mySimMatriarcas)){
		if($mySelf.mySimMatriarcas.getcount() > 0){
			%pontos += $mySelf.mySimMatriarcas.getcount() * 5;	
			if($myPersona.aca_v_1 > 1)
			{
				%minerios += $myPersona.aca_v_1 - 1;	
			}
		}
	}
	
	//Os Grupos:
	%recursosDeGrupos = clientVerificarGrupos();
	%Gmin = FirstWord(%recursosDeGrupos);
	%Gpet = getWord(%recursosDeGrupos, 1);
	%Gura = getWord(%recursosDeGrupos, 2);
	%minerios += %Gmin;
	%petroleos += %Gpet;
	%uranios += %Gura;
	
	//agora as missões:	
	%haAcordosPossiveis = false;
	for(%i = 0; %i < $mySelf.mySimInfo.getCount(); %i++){
		%areaDaMissao = $mySelf.mySimInfo.getObject(%i).area;
		%info = $mySelf.mySimInfo.getObject(%i);
		if(%areaDaMissao.dono !$= "0" && %areaDaMissao.dono != $mySelf && %areaDaMissao.dono !$= "MISTA"){
			if(%areaDaMissao.dono $= "COMPARTILHADA"){
				if(%areaDaMissao.dono1 != $mySelf && %areaDaMissao.dono2 != $mySelf){
					if(!%info.jahFoiOferecida){
						%haAcordosPossiveis = true;
					}
				}
			} else {
				if(!%info.jahFoiOferecida){
					%haAcordosPossiveis = true;
				}
			}
		}
		if(%info.bonusM > 0){
			if(%areaDaMissao.dono.id $= $mySelf.id || (%areaDaMissao.dono $= "COMPARTILHADA" && (%areaDaMissao.dono1 == $mySelf || %areaDaMissao.dono2 == $mySelf))){
				if($mySelf.mySimExpl.isMember(%info)){
					%info.myMark.setFrame(1);
					if(%info.minhaVezDeGanhar == true){
						%mineriosAC += 1;
					}
				} else {
					%info.myMark.setFrame(2);
					%minerios += 1;
				}
				%info.myMark.setAutoRotation(-15);
			} else {
				if($mySelf.mySimExpl.isMember(%info)){
					%info.myMark.setFrame(1);
				} else {
					%info.myMark.setFrame(0);
				}
				%info.myMark.setAutoRotation(0);
			}
		} else if (%info.bonusP > 0){
			if(%areaDaMissao.dono.id $= $mySelf.id || (%areaDaMissao.dono $= "COMPARTILHADA" && (%areaDaMissao.dono1 == $mySelf || %areaDaMissao.dono2 == $mySelf))){
				if($mySelf.mySimExpl.isMember(%info)){
					%info.myMark.setFrame(1);
					if(%info.minhaVezDeGanhar == true){
						%petroleosAC += 1;
					}
				} else {
					%info.myMark.setFrame(2);
					%petroleos += 1;
				}
				%info.myMark.setAutoRotation(-15);
			} else {
				if($mySelf.mySimExpl.isMember(%info)){
					%info.myMark.setFrame(1);
				} else {
					%info.myMark.setFrame(0);
				}
				%info.myMark.setAutoRotation(0);
			}
		} else if (%info.bonusU > 0){
			if(%areaDaMissao.dono.id $= $mySelf.id || (%areaDaMissao.dono $= "COMPARTILHADA" && (%areaDaMissao.dono1 == $mySelf || %areaDaMissao.dono2 == $mySelf))){
				if($mySelf.mySimExpl.isMember(%info)){
					%info.myMark.setFrame(1);
					if(%info.minhaVezDeGanhar == true){
						%uraniosAC += 1;	
					}
				} else {
					%info.myMark.setFrame(2);
					%uranios += 1;	
				}
				%info.myMark.setAutoRotation(-15);
			} else {
				if($mySelf.mySimExpl.isMember(%info)){
					%info.myMark.setFrame(1);
				} else {
					%info.myMark.setFrame(0);
				}
				%info.myMark.setAutoRotation(0);	
			}
		} else if (%info.bonusPt > 0){
			if(%areaDaMissao.dono.id $= $mySelf.id || (%areaDaMissao.dono $= "COMPARTILHADA" && (%areaDaMissao.dono1 == $mySelf || %areaDaMissao.dono2 == $mySelf))){
				if(%areaDaMissao.pos0Flag == true){
					%pontos += 2;
					%info.myMark.setAutoRotation(-15);
					%info.myMark.setFrame(2); 
					%info.myMark.setBlendColor($mySelf.corR, $mySelf.corG, $mySelf.corB, 0.7);
				} else {
					%info.myMark.setAutoRotation(0);
					%info.myMark.setFrame(1); 
					%info.myMark.setBlendColor(1, 1, 1, 1);
				}
			} else { //se é uma carta de pontos e a área não é minha:
				if(!%info.jahFoiOferecida){ //se esta info ainda não foi oferecida pra alguém, mas está no território adversário ou neutro, marca normalmente:
					%info.myMark.setAutoRotation(0);
					%info.myMark.setFrame(1); 
					%info.myMark.setBlendColor(1, 1, 1, 1);	
				} 
			}		
		}
	}
	if(%haAcordosPossiveis){
		//clientDesligarExplMarkers();
		toggleAcordos_btn.setActive(true);	
		toggleAcordos_btn.setStateOn(false);
	} else {
		//clientDesligarExplMarkers();
		toggleAcordos_btn.setActive(false);	
	}
	clientVerifyExplConquest(); //verifica se as missões negociadas continuam sob o controle dos negociantes ou se estão inativas
	clientVerifyProps(); //cancela propostas enviadas cuja área tenha sido perdida pelo futuro parceiro
	
	//verifica se algum acordo enviado estah inativo:
	for(%i = 0; %i < $mySelf.mySimExpl.getCount(); %i++){
		%areaDaMissao = $mySelf.mySimExpl.getObject(%i).area;
		%info = $mySelf.mySimExpl.getObject(%i);
		if(!$mySelf.mySimInfo.isMember(%info)){
			if(%areaDaMissao.dono == %info.parceiro || (%areaDaMissao.dono $= "COMPARTILHADA" && (%areaDaMissao.dono1 == %info.parceiro || %areaDaMissao.dono2 == %info.parceiro))){
				%info.myMark.setAutoRotation(-15);	
			} else {
				%info.myMark.setAutoRotation(0);
			}
		}
	}
	
	//agora os pontos de filantropia:
	%pontos += $mySelf.filantropiasEfetuadas * 3;
		
	
	
	rendaMinerios_txt.text = "+ " @ %minerios; //atualiza o texto na tela
	rendaPetroleos_txt.text = "+ " @ %petroleos;
	rendaUranios_txt.text = "+ " @ %uranios;
	if(%mineriosAC > 0){
		rendaMineriosAC_txt.setVisible(true);
		rendaMineriosAC_txt.text = "+ " @ %mineriosAC; //atualiza o texto na tela
	} else {
		rendaMineriosAC_txt.setVisible(false);
	}
	if(%petroleosAC > 0){
		rendaPetroleosAC_txt.setVisible(true);
		rendaPetroleosAC_txt.text = "+ " @ %petroleosAC;
	} else {
		rendaPetroleosAC_txt.setVisible(false);	
	}
	if(%uraniosAC > 0){
		rendaUraniosAC_txt.setVisible(true);
		rendaUraniosAC_txt.text = "+ " @ %uraniosAC;
	} else {
		rendaUraniosAC_txt.setVisible(false);	
	}
	
	
	
	
	%myMin = $mySelf.minerios; //temporary para calcular quandos pontos o cara tem
	%myPet = $mySelf.petroleos;
	%myUra = $mySelf.uranios;
	%tenhoRecursos = false;
	if(%myMin > 4){
		%tenhoRecursos = true;
	} else if (%myPet > 3){
		%tenhoRecursos = true;
	} else if (%myUra > 2){
		%tenhoRecursos = true;
	} else if (%myMin > 0 && %myPet > 0 && %myUra > 0){
		%tenhoRecursos = true;
	}
	venderRecursos_btn.setStateOn(false);
	if(%tenhoRecursos){
		venderRecursos_btn.setActive(true);	
	} else {
		venderRecursos_btn.setActive(false);	
	}
	
	///agora os gauges dos objetivos:
	//Primeiro Objetivo:
	if(!$guloksDespertaram){
		if ($mySelf.mySimObj.getObject(0).grupo $= "0"){
			gauge1_2.text = "";
			if($mySelf.mySimObj.getObject(0).baias > 0){
				%jahTenho = %maritimas;
				%meFaltam = $mySelf.mySimObj.getObject(0).baias;
			} else if ($mySelf.mySimObj.getObject(0).territorios > 0){
				%jahTenho = %terrestres;
				%meFaltam = $mySelf.mySimObj.getObject(0).territorios;
			} else {
				%jahTenho = $mySelf.petroleos;
				%meFaltam = $mySelf.mySimObj.getObject(0).petroleos;
			}
			gauge1_1.text = "(" @ %jahTenho @ "/" @ %meFaltam @ ")";
			if(%jahTenho >= %meFaltam){
				%obj1Completo = true;
				%pontos += 5;
				mainGuiTick1_img.setVisible(true);
				$mySelf.obj1Completo = true;
				//%myMin -= $mySelf.mySimObj.getObject(0).minerios;
				%myPet -= $mySelf.mySimObj.getObject(0).petroleos; //a única carta que não envolve grupos é de petróleo, as outras são baía ou territórios
				//%myUra -= $mySelf.mySimObj.getObject(0).uranios;
			} else {
				mainGuiTick1_img.setVisible(false);	
				$mySelf.obj1Completo = false;
			}
		} else {
			//verifica a posse do grupo:
			//%eval = "%myGrupo = %" @ $mySelf.mySimObj.getObject(0).grupo @ ";";
			//eval(%eval);
			%eval = "%myGrupo = $mySelf." @ $mySelf.mySimObj.getObject(0).grupo @ ";";
			eval(%eval);
			if (%myGrupo == true){
				gauge1_1.text = "(1/1)";
			} else {
				gauge1_1.text = "(0/1)";
			}
			
			//e agora do recurso:
			if ($mySelf.mySimObj.getObject(0).minerios > 0){
				%jahTenho = $mySelf.minerios;
				%meFaltam = $mySelf.mySimObj.getObject(0).minerios;
			} else if ($mySelf.mySimObj.getObject(0).petroleos > 0){
				%jahTenho = $mySelf.petroleos;
				%meFaltam = $mySelf.mySimObj.getObject(0).petroleos;
			} else if ($mySelf.mySimObj.getObject(0).uranios > 0){
				%jahTenho = $mySelf.uranios;
				%meFaltam = $mySelf.mySimObj.getObject(0).uranios;
			} else {
				%jahTenho = %maritimas;
				%meFaltam = $mySelf.mySimObj.getObject(0).baias;
			}
			gauge1_2.text = "(" @ %jahTenho @ "/" @ %meFaltam @ ")";
			if(%jahTenho >= %meFaltam && %myGrupo == true){
				%obj1Completo = true;
				%pontos += 5;
				mainGuiTick1_img.setVisible(true);
				%myMin -= $mySelf.mySimObj.getObject(0).minerios;
				%myPet -= $mySelf.mySimObj.getObject(0).petroleos;
				%myUra -= $mySelf.mySimObj.getObject(0).uranios;
				$mySelf.obj1Completo = true;
			} else {
				mainGuiTick1_img.setVisible(false);	
				$mySelf.obj1Completo = false;
			}
		}
		
		//Segundo Objetivo:
		if ($mySelf.mySimObj.getObject(1).grupo $= "0"){
			gauge2_2.text = "";
			if($mySelf.mySimObj.getObject(1).baias > 0){
				%jahTenho = %maritimas;
				%meFaltam = $mySelf.mySimObj.getObject(1).baias;
			} else if ($mySelf.mySimObj.getObject(1).territorios > 0){
				%jahTenho = %terrestres;
				%meFaltam = $mySelf.mySimObj.getObject(1).territorios;
			} else {
				%jahTenho = $mySelf.petroleos;
				%meFaltam = $mySelf.mySimObj.getObject(1).petroleos;
			}
			gauge2_1.text = "(" @ %jahTenho @ "/" @ %meFaltam @ ")";
			if(%jahTenho >= %meFaltam){
				%obj2Completo = true;
				%pontos += 5;
				mainGuiTick2_img.setVisible(true);
				$mySelf.obj2Completo = true;
				
				if (%obj1Completo == false){
					//%myMin -= $mySelf.mySimObj.getObject(1).minerios;
					%myPet -= $mySelf.mySimObj.getObject(1).petroleos; //apenas petróleos (10 petróleos)
					//%myUra -= $mySelf.mySimObj.getObject(1).uranios;
				} else {
					//aki verifica se os objetivos tinham o mesmo recurso como alvo (só se for petróleo ou urânio)
					if($mySelf.mySimObj.getObject(0).petroleos > 0 && $mySelf.mySimObj.getObject(0).petroleos == $mySelf.mySimObj.getObject(1).petroleos){
					} else {
						if($mySelf.mySimObj.getObject(0).petroleos > 0 && $mySelf.mySimObj.getObject(0).petroleos < $mySelf.mySimObj.getObject(1).petroleos){
							%myPet -= 2;
						} else {
							%myPet -= $mySelf.mySimObj.getObject(1).petroleos;
						}
					}
				}
			} else {
				mainGuiTick2_img.setVisible(false);	
				$mySelf.obj2Completo = false;
			}
		} else {
			//verifica a posse do grupo:
			//%eval = "%myGrupo = %" @ $mySelf.mySimObj.getObject(1).grupo @ ";";
			//eval(%eval);
			%eval = "%myGrupo = $mySelf." @ $mySelf.mySimObj.getObject(1).grupo @ ";";
			eval(%eval);
			if (%myGrupo == true){
				gauge2_1.text = "(1/1)";
			} else {
				gauge2_1.text = "(0/1)";
			}
			
			//e agora do recurso:
			if ($mySelf.mySimObj.getObject(1).minerios > 0){
				%jahTenho = $mySelf.minerios;
				%meFaltam = $mySelf.mySimObj.getObject(1).minerios;
			} else if ($mySelf.mySimObj.getObject(1).petroleos > 0){
				%jahTenho = $mySelf.petroleos;
				%meFaltam = $mySelf.mySimObj.getObject(1).petroleos;
			} else if ($mySelf.mySimObj.getObject(1).uranios > 0){
				%jahTenho = $mySelf.uranios;
				%meFaltam = $mySelf.mySimObj.getObject(1).uranios;
			} else {
				%jahTenho = %maritimas;
				%meFaltam = $mySelf.mySimObj.getObject(1).baias;
			}
			gauge2_2.text = "(" @ %jahTenho @ "/" @ %meFaltam @ ")";
			if(%jahTenho >= %meFaltam && %myGrupo == true){
				%obj2Completo = true;
				%pontos += 5;
				mainGuiTick2_img.setVisible(true);
				$mySelf.obj2Completo = true;
				
				if (%obj1Completo == false){
					%myMin -= $mySelf.mySimObj.getObject(1).minerios;
					%myPet -= $mySelf.mySimObj.getObject(1).petroleos;
					%myUra -= $mySelf.mySimObj.getObject(1).uranios;
				} else {
					//aki verifica se os objetivos tinham o mesmo recurso como alvo (só se for petróleo ou urânio)
					if($mySelf.mySimObj.getObject(0).petroleos > 0 && $mySelf.mySimObj.getObject(0).petroleos == $mySelf.mySimObj.getObject(1).petroleos){
					} else {
						if($mySelf.mySimObj.getObject(0).petroleos > 0 && $mySelf.mySimObj.getObject(0).petroleos < $mySelf.mySimObj.getObject(1).petroleos){
							%myPet -= 2;
						} else if($mySelf.mySimObj.getObject(0).petroleos > 0 && $mySelf.mySimObj.getObject(0).petroleos > $mySelf.mySimObj.getObject(1).petroleos){
							//não faz nada, já descontou 10;
						} else {
							%myPet -= $mySelf.mySimObj.getObject(1).petroleos;
						}
					}
					if($mySelf.mySimObj.getObject(0).uranios > 0 && $mySelf.mySimObj.getObject(0).uranios == $mySelf.mySimObj.getObject(1).uranios){
					} else {
						%myUra -= $mySelf.mySimObj.getObject(1).uranios;
					}
					%myMin -= $mySelf.mySimObj.getObject(1).minerios;
				}
			} else {
				mainGuiTick2_img.setVisible(false);	
				$mySelf.obj2Completo = false;
			}
		}	
	} else {
		gauge1_1.text = "";	
		gauge1_2.text = "";
		gauge2_1.text = "";	
		gauge2_2.text = "";
	}
		
	//uma tentativa de função diferente para os recursos:
	if (%myUra > 0 && %myPet > 0 && %myMin > 0){
		%existeConjunto = true;
	} else {
		%existeConjunto = false;
	}
	while(%existeConjunto){
		%pontos+=1;
		%myMin -= 1;
		%myPet -= 1;
		%myUra -= 1;
		
		if (%myUra > 0 && %myPet > 0 && %myMin > 0){
			%existeConjunto = true;
		} else {
			%existeConjunto = false;
		}
	}
	
	
	//agora as trocas remanescentes:
	
	%pontos += mFloor(%myUra/3);
	%pontos += mFloor(%myPet/4);
	%pontos += mFloor(%myMin/5);
	
	//agora o poker:
	if($tipoDeJogo $= "poker")
		%pontos += $mySelf.pk_pontos;
	
	pontosHud_txt.text = %pontos;
	
	if($estouNoTutorial){
		tut_verificarObjetivo();	
	} else {
		clientQuickVerifyBaterBtn();	
	}
}



function clientGetPlaneta(%nome){
	for(%i = 0; %i < $todosOsPlanetas.getCount(); %i++){
		%tempPlaneta = $todosOsPlanetas.getObject(%i);
		if(%tempPlaneta.nome $= %nome){
			%planeta = %tempPlaneta;
			%i = $todosOsPlanetas.getCount();
		}
	}
	if(isObject(%planeta)){
		return %planeta;
	} else {
		echo("## ERRO CRÍTICO ##: planeta não encontrado!!!");	
	}
}

function clientVerificarGrupos(){
	//zera as variáveis:
	%minerio = 0;
	%petroleo = 0;
	%uranio = 0;
	
	//pega o planeta:
	%planeta = clientGetPlaneta($planetaAtual);
	
	//verifica cada grupo:
	for(%i = 0; %i < %planeta.grupos.getCount(); %i++){
		%grupo = %planeta.grupos.getObject(%i);
		%areasMinhas = 0;
		
		//echo("Verificando Grupo (" @ %grupo.nome @ ") no planeta " @ %planeta.nome @ ":");
		//echo("Este grupo possui " @ %grupo.simAreas.getCount() @ " áreas.");
		for (%j = 0; %j < %grupo.numDeAreas; %j++){
			%area = %grupo.simAreas.getObject(%j);
			//echo("AREA " @ %j @ ": " @ %area.myName @ "; Dono: " @ %area.dono);
			if(%area.dono == $mySelf){
				//echo("Esta área é minha!");
				%areasMinhas++;
			} else {
				if(%area.dono $= "COMPARTILHADA"){
					if(%area.dono1 == $mySelf || %area.dono2 == $mySelf){
						//echo("Esta área é minha (compartilhada).");
						%areasMinhas++;	
					}
				} else {
					//echo("Esta área NÃO é minha!");	
				}
			}
		}
		
		if(%areasMinhas == %grupo.simAreas.getCount()){
			//se o grupo não estava conquistado antes, chama o efeito de conquista:
			%eval = "%myVar = $mySelf." @ %grupo.nome @ ";";
			eval(%eval);
			if(!%myVar){
				clientConquistarGrupoShow(%grupo.nome);
			}
			
			//marca no player que ele possui o grupo:
			%eval = "$mySelf." @ %grupo.nome @ " = true;";
			eval(%eval);
			
			//adiciona o recurso às estatísticas:
			%eval = "%" @ %grupo.recurso @ "++;";
			eval(%eval);
			
			//deixa as fronteiras brancas:
			%eval = %grupo.nome @ "ON_over.setVisible(true);";
			eval(%eval);
		} else {
			//marca que o grupo não está com o player:
			%eval = "$mySelf." @ %grupo.nome @ " = false;";
			eval(%eval);
			
			//apaga a marcação das fronteiras brancas:
			%eval = %grupo.nome @ "ON_over.setVisible(false);";
			eval(%eval);
		}
	}
	
	%return = %minerio SPC %petroleo SPC %uranio;
	return %return;
}




function clientConquistarGrupoShow(%grupo){
	//liga a imagem do grupo conquistado:
	%eval = %grupo @ "ON_over.setVisible(true);";
	eval(%eval);
	if($planetaAtual $= "Terra"){
		%eval = %grupo @ "ON_over.setBlendColor(1, 1, 1, 0.8);";
		eval(%eval);
	}
	
	//chama o efeito de conquista desse grupo:
	%conquestFX = new t2dParticleEffect() { scenegraph = $strategyScene; };
	%conquestFX.loadEffect("~/data/particles/" @ %grupo @ "conquestFX.eff");
	%conquestFX.setEffectLifeMode("KILL", 2);
	//seta a posição correta:
	switch$(%grupo){
		case "brasil":	
		%conquestFX.setPosition(-20.4, 2.2);
		
		case "eua":
		%conquestFX.setPosition(-34.5, -16.1);
		
		case "canada":
		%conquestFX.setPosition(-31.9, -23.8);
		
		case "europa":
		%conquestFX.setPosition(-1.2, -18.9);
		
		case "africa":
		%conquestFX.setPosition(-1.7, -3);
		
		case "oriente":
		%conquestFX.setPosition(11.5, -11.3);
		
		case "russia":
		%conquestFX.setPosition(24.6, -24.4);
		
		case "china":
		%conquestFX.setPosition(29, -17.5);
		
		case "australia":
		%conquestFX.setPosition(37.2, 5.6);
		
		//Ungart:
		case "PrEx":
		%conquestFX.setPosition(-39.9, 0.3);
		
		case "ChOc":
		%conquestFX.setPosition(-27.7, 3.9);
		
		case "PaGu":
		%conquestFX.setPosition(-9, -0.7);
		
		case "VaGu":
		%conquestFX.setPosition(-1.3, 3.8);
		
		case "ChOr":
		%conquestFX.setPosition(16.6, 3.9);
		
		case "VaOr":
		%conquestFX.setPosition(34.8, 3.7);
		
		case "CaOr":
		%conquestFX.setPosition(34.1, -10);
		
		case "DeEx":
		%conquestFX.setPosition(38.1, -28.5);
		
		case "PlDo":
		%conquestFX.setPosition(21.8, -25);
		
		case "VaNo":
		%conquestFX.setPosition(7.7, -19.8);
		
		case "CaNo":
		%conquestFX.setPosition(-12.8, -23);
		
		case "IlVu":
		%conquestFX.setPosition(-17.9, -11.9);
		
		case "MoVe":
		%conquestFX.setPosition(-36.5, -20.4);
		
		//Telúria:
		case "terion":
		%conquestFX.setPosition(-30.6, 8.3);
		
		case "malik":
		%conquestFX.setPosition(19.3, 7.2);
		
		case "nir":
		%conquestFX.setPosition(-18.8, -27.4);
		
		case "karzin":
		%conquestFX.setPosition(16.8, -24.9);
		
		case "goruk":
		%conquestFX.setPosition(36.3, -28.4);
		
		case "geoCanhao":
		%conquestFX.setPosition(18.6, -8.7);
		
		case "nexus":
		%conquestFX.setPosition(-2.1, -30.1);
		
		case "zavinia":
		%conquestFX.setPosition(-37.3, -28.9);
		
		case "dharin":
		%conquestFX.setPosition(-34.3, -14.3);
		
		case "keltur":
		%conquestFX.setPosition(37.3, 5.9);
		
		case "lornia":
		%conquestFX.setPosition(37.1, -9.2);
		
		case "valinur":
		%conquestFX.setPosition(-13.2, -3);
		
		case "vuldan":
		%conquestFX.setPosition(2.3, -5.1);
		
		case "argonia":
		%conquestFX.setPosition(-5.8, -4);
	}
	
	//Texto:
	%conquestTXT = new t2dParticleEffect() { scenegraph = $strategyScene; };
	%conquestTXT.loadEffect("~/data/particles/" @ %grupo @ "TXT.eff");
	%conquestTXT.setEffectLifeMode("KILL", 1);
	%conquestTXT.setPosition(%conquestFX.getPosition());
	%myTempEmitter = %conquestTXT.getEmitterObject(0);
	%myTempEmitter.setIntenseParticles(false);
	
	%conquestTXT.playEffect();
	%conquestFX.playEffect();
	
}


////////////Observador:
function clientOBS_atualizarEstatisticas(){
	if ($statsAtuais == false){
		gauge1_1.setVisible(true);
		gauge1_2.setVisible(true);
		gauge2_1.setVisible(true);
		gauge2_2.setVisible(true);
		$statsAtuais = true;
	}
		
	%quantasAreas = $jogadorDavez.mySimAreas.getCount(); //guarda o número de áreas na variável
	%imperiaisPorAreas = mFloor(%quantasAreas/2);
	rendaImperiais_txt.text = "+ " @ %imperiaisPorAreas; 
		
	//zera as variáveis:
	%terrestres = 0; 
	%maritimas = 0;
	%bases = 0;
			
	//verifica quantas terras, quantos mares e quantas bases:
	for(%i = 0; %i < $jogadorDavez.mySimAreas.getCount(); %i++){
		%area = $jogadorDavez.mySimAreas.getObject(%i);
		if(%area.terreno $= "terra"){
			%terrestres += 1; //para cada área que for terra, soma 1 à variável terrestres
		} else if (%area.terreno $= "mar"){
			%maritimas += 1; //para cada área que for mar, soma 1 à variável maritimas
		}
	}
	areasTerrestres_txt.text = %terrestres; //atualiza o texto na tela
	areasMaritimas_txt.text = %maritimas;
	
	%bases = $jogadorDavez.mySimBases.getcount();
	if(%bases > 9){
		imperioMark_img.setVisible(true); //liga mostrador
		imperioMark_img.bitmap = "~/data/images/imperioMark" @ $jogadorDavez.myColor @ ".png"; //imperioMarkVermelho.png, por exemplo;
	} else {
		imperioMark_img.setVisible(false); //desliga mostrador
	}
	bases_txt.text = %bases;
			
	rendaMinerios_txt.text = "??" @ %minerios; //atualiza o texto na tela
	rendaPetroleos_txt.text = "??" @ %petroleos;
	rendaUranios_txt.text = "??" @ %uranios;
	
	gauge1_1.text = "???";
	gauge1_2.text = "???";
	gauge2_1.text = "???";
	gauge2_2.text = "???";
		
	pontosHud_txt.text = "???";
}


function clientQuickVerifyBaterBtn()
{
	if($mySelf.obj1Completo || $mySelf.obj2Completo)
		return;
		
	bater_btn.setVisible(false);
}