
function apagarBtns(){
	if($myPersona.gulok){
		apagarBtnsGulok();
	} else {
		apagarBtnsHumanos();
	}
}

function apagarBtnsGulok(){
	//construir unidades:
	botarOvo_btn.setVisible(false);	
	botarCefalok_btn.setVisible(false);	
	evoluirEmRainha_btn.setVisible(false);	
	
	//rainha:
	hud_incorporar_btn.setVisible(false);
	hud_submergir_btn.setVisible(false);
	hud_crisalida_btn.setVisible(false);
	hud_expulsar_btn.setVisible(false);
	
	//Zangao Preto:
	hud_canibalizar_btn.setVisible(false);
	hud_carregar_btn.setVisible(false);
	hud_descarregar_btn.setVisible(false);
	hud_devorarRainhas_btn.setVisible(false);
	
	//Zangao Branco:
	hud_metamorfose_btn.setVisible(false);
	hud_cortejar_btn.setVisible(false);
	
	//crisálida:
	hud_matriarca_btn.setVisible(false);
	
	//matriarca:
	hud_virus_btn.setVisible(false);
	
	//dragnal:
	hud_dragnal_btn.setVisible(true);
	if($myPersona.aca_i_3 > 0){
		hud_dragnal_btn.setActive(true);
		hud_dragAtk_btn.setStateOn(false);
	} else {
		hud_dragnal_btn.setActive(false);	
	}
	apagarDragnalBtns();
		
	hud_Gemboscada1_btn.setVisible(false);
	hud_Gemboscada2_btn.setVisible(false);
}

function apagarBtnsHumanos(){
	criarSoldado_btn.setVisible(false);
	criarTanque_btn.setVisible(false);
	criarNavio_btn.setVisible(false);
	criarBase_btn.setVisible(false);
	emboscada_btn.setVisible(false);
	reciclagemHudBtn.setVisible(false);	
	refinaria_btn.setVisible(false);	
	airDropHud_btn.setActive(false);
	sniper_btn.setVisible(false);
	terceiroLider_btn.setVisible(false);
	
	//não dei xa o btn de dragnal se intrometer:
	//Dragnal:
	hud_dragnal_btn.setVisible(false);	
}

function verificarCanhaoOrbitalBtn(){
	//echo("VERIFICANDO CANHAO ORBITAL");
	canhaoOrbital_btn.setVisible(false);
	canhaoOrbital_btn.setActive(false);	
	
	if($myPersona.especie $= "humano"){
		if($tipoDeJogo !$= "semPesquisas"){
			%myCost = (4 - $myPersona.aca_a_2);
			
			if($jogadorDaVez == $mySelf){
				if($myPersona.aca_a_2 > 0){
					//echo("TENHO CANHAO");
					if($rodadaAtual >= $mySelf.reqCanhao){
						//echo("POSSO DISPARAR");
						if($mySelf.disparosOrbitais == 0){
							//echo("AINDA NÃO DISPAREI");
							canhaoOrbital_btn.setVisible(true);	
							if($mySelf.uranios >= %myCost){
								echo("CANHAO ORBITAL DISPONÍVEL");
								canhaoOrbital_btn.setActive(true);	
								canhaoOrbital_btn.setStateOn(false);	
							}
						}
					} else {
						//echo("AINDA NÃO POSSO DISPARAR");	
					}
				}
			}
		}
	}
}

function verificarOcultarBtn(){
	ocultarInGame_btn.setVisible(false);
	ocultarInGame_btn.setActive(false);	
	if($myPersona.especie $= "humano"){
		if($tipoDeJogo !$= "semPesquisas"){
			if($jogadorDaVez == $mySelf){
				if($myPersona.aca_av_3 > 0){
					if($rodadaAtual >= $mySelf.reqOcultar){
						if(!$mySelf.oculto){
							ocultarInGame_btn.setVisible(true);	
							if($mySelf.imperiais >= $mySelf.ocultarCusto){
								echo("OCULTAR DISPONIVEL");
								ocultarInGame_btn.setActive(true);	
								ocultarInGame_btn.setStateOn(false);	
							}
						}
					}
				}
			}
		}
	}
}

function resetAirDropBtn()
{
	airDropHud_btn.setActive(false);	
	
	if($myPersona.aca_v_5 > 2){
		%rodadaEmQuePosso = 2;
		%custoARD = 4;
	} else {
		%rodadaEmQuePosso = 3;
		%custoARD = 5;
	}
	
	if($mySelf.airDrops < 1 || $mySelf.airDrops $= "")
		return;
	
	if($mySelf.imperiais < %custoARD)
		return;
	
	if($rodadaAtual < %rodadaEmQuePosso)
		return;
	
	if($tipoDeJogo $= "semPesquisas")
		return;
	
	echo("$mySelf.airDrops = " @ $mySelf.airDrops);
	airDropHud_btn.setActive(true);	
}

function atualizarBotoesDeCompraHumanos(){
	if($mySelf == $jogadorDaVez){
				
		if(isObject(Foco.getObject(0))){
			%foco = Foco.getObject(0);
			
			if($myPersona.aca_v_4 > 2){
				refinaria_btn.setBitmap("game/data/images/refinariaBtn4");
			} else {
				refinaria_btn.setBitmap("game/data/images/refinariaBtn5");
			}
			
			
			if(%foco.onde.terreno $= "terra"){
				resetAirDropBtn();
				
				if(%foco.class $= "base"){
					refinaria_btn.setVisible(false);
					if(!%foco.refinaria){
						criarBase_btn.setVisible(false);
						criarNavio_btn.setVisible(false);
						criarSoldado_btn.setVisible(true);
						criarTanque_btn.setVisible(true);
					}
					if($myPersona.aca_v_3 > 0 && $tipoDeJogo !$= "semPesquisas"){
						reciclagemHudBtn.setVisible(true);	
					} else {
						reciclagemHudBtn.setVisible(false);	
					}
					if($myPersona.aca_a_1 > 2 && $tipoDeJogo !$= "semPesquisas"){
						if($mySelf.mySimLideres.getCount() < 2){
							if($mySelf.terceiroLiderOn == false && !%foco.refinaria){
								terceiroLider_btn.setVisible(true);
							} else {
								terceiroLider_btn.setVisible(false);
							}
						} else {
							terceiroLider_btn.setVisible(false);
						}
					} else {
						terceiroLider_btn.setVisible(false);	
					}
					if($salaEmQueEstouTipoDeJogo $= "poker" && $mySelf.lideresDisponiveis > 0)
					{
						terceiroLider_btn.setVisible(true);
					}
				} else {
					criarBase_btn.setVisible(true);
					reciclagemHudBtn.setVisible(false);	
					terceiroLider_btn.setVisible(false);	
					if(%foco.onde.dono $= "MISTA"){
						emboscada_btn.setVisible(true);	
					} else {
						emboscada_btn.setVisible(false);		
					}
					if($myPersona.aca_v_4 > 0 && $tipoDeJogo !$= "semPesquisas"){
						if(%foco.onde.dono $= "MISTA"){
							refinaria_btn.setVisible(false);	
						} else {
							refinaria_btn.setVisible(true);	
						}
					} else {
						refinaria_btn.setVisible(false);	
					}
					if(%foco.mySnipers > 0 && $rodadaAtual > 1 && $tipoDeJogo !$= "semPesquisas"){
						sniper_btn.setVisible(true);
					} else {
						sniper_btn.setVisible(false);	
					}
				}
			} else {
				refinaria_btn.setVisible(false);		
				terceiroLider_btn.setVisible(false);	
				if(%foco.class $= "base"){
					criarBase_btn.setVisible(false);
					criarSoldado_btn.setVisible(false);
					criarTanque_btn.setVisible(false);
					criarNavio_btn.setVisible(true);
					if($myPersona.aca_v_3 > 0 && $tipoDeJogo !$= "semPesquisas"){
						reciclagemHudBtn.setVisible(true);	
					} else {
						reciclagemHudBtn.setVisible(false);	
					}
				} else {
					criarBase_btn.setVisible(true);
					reciclagemHudBtn.setVisible(false);	
					if(%foco.onde.dono $= "MISTA"){
						emboscada_btn.setVisible(true);	
					} else {
						emboscada_btn.setVisible(false);		
					}
					if(%foco.mySnipers > 0 && $rodadaAtual > 1 && $tipoDeJogo !$= "semPesquisas"){
						sniper_btn.setVisible(true);
					} else {
						sniper_btn.setVisible(false);	
					}
				}
			}
			clientVerificarBtnsAtivos(%foco); //ativa e desativa os botões conforme a grana do client;
		} else {
			apagarBtnsHumanos();
		}
	} else {
		apagarBtnsHumanos();
	}
}

function clientSetEvoluirEmRainhaBtnGfx()
{
	if($myPersona.aca_av_4 == 3 && $tipoDeJogo !$= "semPesquisas")
	{
		evoluirEmRainha_btn.setBitmap("game/data/images/rainhaBTN9");
		return;
	}
	
	evoluirEmRainha_btn.setBitmap("game/data/images/rainhaBTN10");	
}

function atualizarBotoesDeCompraGulok(){
	apagarBtnsGulok();
	
	if($mySelf == $jogadorDaVez){
		if(isObject(Foco.getObject(0))){
			%foco = Foco.getObject(0);
			clientSetEvoluirEmRainhaBtnGfx();
			
			if(%foco.class $= "verme"){
				evoluirEmRainha_btn.setVisible(true);
				if(%foco.onde.dono $= "MISTA"){
					hud_Gemboscada1_btn.setVisible(true);	
				}
			} else if(%foco.class $= "cefalok"){
				evoluirEmRainha_btn.setVisible(true);
				if(%foco.onde.dono $= "MISTA"){
					hud_Gemboscada1_btn.setVisible(true);	
				}
			} else if(%foco.class $= "rainha"){
				if(!%foco.crisalida && $tipoDeJogo !$= "semPesquisas"){
					hud_incorporar_btn.setvisible(true);
					hud_submergir_btn.setvisible(true);
					hud_crisalida_btn.setvisible(true);
					hud_expulsar_btn.setvisible(true);
				} else {
					if($tipoDeJogo !$= "semPesquisas")
						hud_matriarca_btn.setvisible(true);	
				}
				if(%foco.matriarca){
					hud_submergir_btn.setvisible(false);
					hud_crisalida_btn.setvisible(false);
					if($tipoDeJogo !$= "semPesquisas")
						hud_virus_btn.setvisible(true);
				}
				if(%foco.onde.terreno $= "terra"){
					if(!%foco.crisalida){
						botarOvo_btn.setVisible(true);
						if(%foco.matriarca || %foco.cortejada){
							botarOvo_btn.setBitmap("game/data/images/ovoBTN2");
						} else {
							botarOvo_btn.setBitmap("game/data/images/ovoBTN3");
						}
					}
				} else {
					botarCefalok_btn.setVisible(true);	
				}
				if(%foco.onde.dono $= "MISTA" && !%foco.crisalida){
					hud_Gemboscada2_btn.setVisible(true);	
				}
			} else if(%foco.class $= "zangao"){
				if($tipoDeJogo !$= "semPesquisas")
				{
					if(%foco.liderNum == 1){
						hud_canibalizar_btn.setvisible(true);
						hud_carregar_btn.setvisible(true);
						hud_descarregar_btn.setvisible(true);
						hud_devorarRainhas_btn.setvisible(true);	
					} else {
						hud_metamorfose_btn.setvisible(true);
						hud_carregar_btn.setvisible(true);
						hud_descarregar_btn.setvisible(true);
						hud_cortejar_btn.setvisible(true);	
					}
				}
				if(%foco.onde.dono $= "MISTA"){
					hud_Gemboscada2_btn.setVisible(true);	
				}
			}
		}
	}
}

function clientSetNaviosBtnImg()
{
	if($mySelf.marinheiro)
	{
		criarNavio_btn.setBitmap("game/data/images/navioBTN2");
		return;
	}
	criarNavio_btn.setBitmap("game/data/images/navioBTN");
}

function clientSetBasesBtnImg()
{
	if($mySelf.engenheiro)
	{
		criarBase_btn.setBitmap("game/data/images/baseBTN7");
		return;
	}
	criarBase_btn.setBitmap("game/data/images/baseBTN");
}

function atualizarBotoesDeCompra(){
	if($myPersona.gulok){
		atualizarBotoesDeCompraGulok();
	} else {
		atualizarBotoesDeCompraHumanos();
	}
	clientVerificarBtnsAtivos();
}

function clientVerificarBtnsAtivos(%foco){
	if($myPersona.gulok){
		clientVerificarBtnsAtivosGulok();
	} else {
		clientVerificarBtnsAtivosHumanos();
	}	
}

function clientVerificarBtnsAtivosHumanos(){
	%foco = Foco.getObject(0);
	%custoREF = 5;
	if($myPersona.aca_v_4 > 2){
		%custoREF = 4;
	}
	%myMaxRefinarias = $myPersona.aca_v_4;
	if(%myMaxRefinarias > 2){
		%myMaxRefinarias = 2;
	}
	
	if($mySelf.marinheiro){
		%custoNavio = 2;	
	} else {
		%custoNavio = 3;
	}
	
	if($mySelf.engenheiro){
		%custoBase = 7;	
	} else {
		%custoBase = 10;
	}
	
	if($mySelf.imperiais > 0){
		criarSoldado_btn.setActive(true);
		if($mySelf.imperiais > 1){
			criarTanque_btn.setActive(true);
			if($mySelf.imperiais >= %custoNavio){
				criarNavio_btn.setActive(true);
				if(%foco.onde.pos0Flag || %foco.onde.reliquia == 1 || %foco.onde.artefato == 1){
					criarBase_btn.setActive(false);
					refinaria_btn.setActive(false);
				} else {
					if(%foco.onde.oceano == 1 || %foco.onde.ilha == 1){
						criarBase_btn.setActive(false);
						refinaria_btn.setActive(false);
					} else {
						if($mySelf.imperiais >= %custoREF){
							if(%myMaxRefinarias > $mySelf.mySimRefinarias.getCount()){
								if(%foco.onde.terreno $= "terra"){
									refinaria_btn.setActive(true);
								} else {
									refinaria_btn.setActive(false);
								}
							} else {
								refinaria_btn.setActive(false);
							}
							if($mySelf.imperiais >= %custoBase){
								criarBase_btn.setActive(true);
							} else {
								criarBase_btn.setActive(false);
							}
						} else {
							refinaria_btn.setActive(false);
						}	
					}
				}
			} else {
				criarNavio_btn.setActive(false);
				criarBase_btn.setActive(false);
				refinaria_btn.setActive(false);
			}
		} else {
			criarTanque_btn.setActive(false);
			criarNavio_btn.setActive(false);
			criarBase_btn.setActive(false);
			refinaria_btn.setActive(false);
		}
	} else {
		criarSoldado_btn.setActive(false);	
		criarTanque_btn.setActive(false);
		criarNavio_btn.setActive(false);
		criarBase_btn.setActive(false);
		refinaria_btn.setActive(false);
	}
}

function clientVerificarBtnsAtivosGulok(){
	%foco = foco.getObject(0); 
	if(%foco.class $= "rainha" && (%foco.cortejada || %foco.matriarca)){
		%custoOvo = 2;
	} else {
		%custoOvo = 3;
	}
	if($mySelf.imperiais >= %custoOvo){
		botarOvo_btn.setActive(true);
		if($mySelf.imperiais >= 4 && !%foco.onde.oceano){
			botarCefalok_btn.setActive(true);
		} else {
			botarCefalok_btn.setActive(false);
		}
		
		if(%foco.onde.pos0Flag == true || %foco.onde.reliquia == 1 || %foco.onde.artefato == 1){
			evoluirEmRainha_btn.setActive(false);	
		} else {
			%custoRainha = 10;
			if($myPersona.aca_av_4 == 3)
			{
				%custoRainha = 9;
			}
			if($mySelf.imperiais >= %custoRainha)
			{
				evoluirEmRainha_btn.setActive(true);		
			} else {
				evoluirEmRainha_btn.setActive(false);	
			}
		}
	} else {
		botarOvo_btn.setActive(false);
		botarCefalok_btn.setActive(false);
		evoluirEmRainha_btn.setActive(false);	
	}
	
	//////////
	//Dragnal:
	//Dragnal:
	hud_dragAtk_btn.setActive(false);
	hud_dragAtk_btn.setStateOn(false);
	hud_entregar_btn.setActive(false);	
	hud_sopro_btn.setActive(false);	
	
	if($myPersona.aca_i_3 > 0){
		if($myPersona.aca_av_1 > 0){
			%dragnalMax = 2;	
		} else {
			%dragnalMax = 1;	
		}
		if($myPersona.aca_av_1 == 3){
			%dragnalCusto = 1;	
		} else {
			%dragnalCusto = 2;
		}
		if($myPersona.aca_av_1 >= 2){
			%dragnalAtkMax = 2;
		} else {
			%dragnalAtkMax = 1;
		}
		//Atacar:
		if($mySelf.dragnalAtks < %dragnalAtkMax && $mySelf.imperiais >= %dragnalCusto){
			if($rodadaAtual > 1){
				hud_dragAtk_btn.setActive(true);	
			}
		}
		
		//Entregar:
		if($myPersona.aca_ldr_3_h1 > 0){
			if($mySelf.dragnalEntregas < %dragnalMax && $mySelf.imperiais >= %dragnalCusto){
				if(foco.getCount() > 0){
					echo("FOCO = " @ foco.getObject(0).class @ "; " @ foco.getObject(0).onde @ ";" @ foco.getObject(0).pos);
					if(%foco.class $= "verme" || %foco.class $= "zangao" || %foco.class $= "rainha"){ 
						if(foco.getObject(0).onde.terreno $= "terra"){
							hud_entregar_btn.setActive(true);
						}
					}
				} else {
					echo("FOCO.getcount() == 0");
				}
			}
		}
	}
	if(%foco.class $= "ovo"){
		if($myPersona.aca_ldr_3_h2 > 0){
			if($mySelf.dragnalSopradas < %dragnalMax && $mySelf.imperiais >= %dragnalCusto){
				hud_sopro_btn.setActive(true);	
			}
		}
	} 
	/////////
	
	
	////////////////
	//Habilidades:
	//Rainhas:
	if(%foco.class $= "rainha"){
		//Incorporar:
		if($myPersona.aca_v_3 > 0){
			if(clientVerifyVermes(%foco.onde) > 0){
				%myMaxIncorporar = ($myPersona.aca_v_3 * 2) - 1;
				if(isObject(%foco.myTransporte)){
					if(%foco.myTransporte.getcount() < %myMaxIncorporar){
						hud_incorporar_btn.setActive(true);	
					} else {
						hud_incorporar_btn.setActive(false);		
					}
				} else {
					hud_incorporar_btn.setActive(true);		
				}
			} else {
				hud_incorporar_btn.setActive(false);		
			}
		} else {
			hud_incorporar_btn.setActive(false);	
		}
		
		//Submergir:
		if($myPersona.aca_v_4 > 0){
			//primeiro seta a imagem do btn:
			if(%foco.submersa){
				%custoSubmergir = 0;
				hud_submergir_btn.setBitmap("game/data/images/GhudMiniBtns/hud_emergir_btn");
			} else {
				%custoSubmergir = 4 - $myPersona.aca_v_4;
				hud_submergir_btn.setBitmap("game/data/images/GhudMiniBtns/hud_submergir_btn");
			}
			if($mySelf.imperiais >= %custoSubmergir){
				if(%foco.onde.terreno $= "mar"){
					hud_submergir_btn.setActive(true);	
				} else {
					hud_submergir_btn.setActive(false);		
				}
			} else {
				hud_submergir_btn.setActive(false);	
			}
		} else {
			hud_submergir_btn.setActive(false);		
		}
		
		//Crisalida:
		if($myPersona.aca_v_5 > 0){
			%custoCrisalida = 10;
			if($myPersona.aca_v_5 == 3){
				%custoCrisalida = 8;	
			}
			if($myPersona.aca_av_4 > 0){
				%custoCrisalida = 7;	
			}
			%myMaxCrisalidas = $myPersona.aca_v_5;
			if(%myMaxCrisalidas == 3){
				%myMaxCrisalidas = 2;	
			}
			//
			if($mySelf.imperiais >= %custoCrisalida){
				if(isObject($mySelf.mySimCrisalidas)){
					if($mySelf.mySimCrisalidas.getCount() < %myMaxCrisalidas){
						if(%foco.onde.terreno $= "terra"){
							hud_crisalida_btn.setActive(true);	
						} else {
							hud_crisalida_btn.setActive(false);	
						}
					} else {
						hud_crisalida_btn.setActive(false);	
					}
				} else {
					if(%foco.onde.terreno $= "terra"){
						hud_crisalida_btn.setActive(true);	
					} else {
						hud_crisalida_btn.setActive(false);	
					}
				}
			} else {
				hud_crisalida_btn.setActive(false);	
			}
		} else {
			hud_crisalida_btn.setActive(false);		
		}
		
		//Expulsar:
		if($myPersona.aca_av_3 > 0){
			%custoExpulsar = 3 - $myPersona.aca_av_3;
			if($mySelf.imperiais >= %custoExpulsar){
				if(isObject(%foco.myTransporte)){
					if(%foco.myTransporte.getcount() > 0){
						hud_expulsar_btn.setActive(true);
					} else {
						hud_expulsar_btn.setActive(false);	
					}
				} else {
					hud_expulsar_btn.setActive(false);	
				}	
			} else {
				hud_expulsar_btn.setActive(false);	
			}
		} else {
			hud_expulsar_btn.setActive(false);		
		}
		
		
		//Em forma de crisálida:
		if(%foco.crisalida){
			if($myPersona.aca_v_6 > 0){
				%custoMatriarca = 10;
				if($myPersona.aca_av_4 > 0){
					%custoMatriarca = 8;	
				}
				if(isObject($mySelf.mySimMatriarcas)){
					if($mySelf.mySimMatriarcas.getCount() == 0){
						if(%foco.estahVerde){
							hud_matriarca_btn.setActive(false);
						} else {
							if($mySelf.imperiais >= %custoMatriarca){
								hud_matriarca_btn.setActive(true);	
							} else {
								hud_matriarca_btn.setActive(false);
							}
						}
					} else {
						hud_matriarca_btn.setActive(false);	
					}
				} else {
					if(%foco.estahVerde){
						hud_matriarca_btn.setActive(false);
					} else {
						if($mySelf.imperiais >= %custoMatriarca){
							hud_matriarca_btn.setActive(true);	
						} else {
							hud_matriarca_btn.setActive(false);
						}	
					}
				}
			} else {
				hud_matriarca_btn.setActive(false);	
			}
		}
		
		//Em forma de Matriarca:
		if(%foco.matriarca){
			if($myPersona.aca_av_2 > 0){
				if($mySelf.minerios > 0 && $mySelf.petroleos > 0 && $mySelf.uranios > 0){
					if($mySelf.virusDisparados == 0){
						hud_virus_btn.setActive(true);
					} else {
						hud_virus_btn.setActive(false);
					}
				} else {
					hud_virus_btn.setActive(false);
				}
			} else {
				hud_virus_btn.setActive(false);
			}
		}
	} else if(%foco.class $= "zangao"){
		if(%foco.lidernum == 1){
			//Canibalizar:
			if($myPersona.aca_ldr_1_h3 > 0){
				if(clientVerifyVermes(%foco.onde) > 0){
					hud_canibalizar_btn.setActive(true);	
				} else {
					hud_canibalizar_btn.setActive(false);		
				}
			} else {
				hud_canibalizar_btn.setActive(false);	
			}
			
			//Carregar:
			if($myPersona.aca_ldr_1_h1 > 0){
				if(clientVerifyVermes(%foco.onde) > 0){
					hud_carregar_btn.setActive(true);	
				} else {
					hud_carregar_btn.setActive(false);		
				}
			} else {
				hud_carregar_btn.setActive(false);	
			}
			
			//Descarregar:
			if($myPersona.aca_ldr_1_h1 > 0){
				if(isObject(%foco.myTransporte)){
					if(%foco.myTransporte.getcount() > 0){
						if(%foco.onde.terreno $= "terra"){
							hud_descarregar_btn.setActive(true);	
						} else {
							hud_descarregar_btn.setActive(false);	
						}
					} else {
						hud_descarregar_btn.setActive(false);	
					}
				} else {
					hud_descarregar_btn.setActive(false);		
				}
			} else {
				hud_descarregar_btn.setActive(false);	
			}
			
			//Devorar Rainha:
			if($myPersona.aca_ldr_1_h4 > 0){
				if(%foco.onde.pos0Quem.class $= "rainha"){
					if(!%foco.onde.pos0Quem.matriarca){
						hud_devorarRainhas_btn.setActive(true);	
					} else {
						hud_devorarRainhas_btn.setActive(false);	
					}
				} else {
					hud_devorarRainhas_btn.setActive(false);		
				}
			} else {
				hud_devorarRainhas_btn.setActive(false);	
			}
		} else {
			//Metamorfose:
			if($myPersona.aca_ldr_2_h3 > 0){
				%custoMetamorfose = 10 - ($myPersona.aca_ldr_2_h3 * 3);
				if(%foco.onde.pos0Flag == false && !%foco.onde.reliquia && !%foco.onde.artefato == 1){
					if($mySelf.imperiais >= %custoMetamorfose){
						hud_metamorfose_btn.setActive(true);	
					} else {
						hud_metamorfose_btn.setActive(false);		
					}
				} else {
					hud_metamorfose_btn.setActive(false);		
				}
			} else {
				hud_metamorfose_btn.setActive(false);	
			}
			
			//Carregar:
			if($myPersona.aca_ldr_2_h1 > 0){
				if(clientVerifyVermes(%foco.onde) > 0){
					hud_carregar_btn.setActive(true);	
				} else {
					hud_carregar_btn.setActive(false);		
				}
			} else {
				hud_carregar_btn.setActive(false);	
			}
			
			//Descarregar:
			if($myPersona.aca_ldr_2_h1 > 0){
				if(isObject(%foco.myTransporte)){
					if(%foco.myTransporte.getcount() > 0){
						if(%foco.onde.terreno $= "terra"){
							hud_descarregar_btn.setActive(true);	
						} else {
							hud_descarregar_btn.setActive(false);	
						}
					} else {
						hud_descarregar_btn.setActive(false);	
					}
				} else {
					hud_descarregar_btn.setActive(false);		
				}
			} else {
				hud_descarregar_btn.setActive(false);	
			}
			
			//Cortejar:
			if($myPersona.aca_ldr_2_h4 > 0){
				if(%foco.onde.pos0Quem.class $= "rainha"){
					if($mySelf.cortejos < $myPersona.aca_ldr_2_h4){
						if(%foco.onde.pos0Quem.cortejada){
							hud_cortejar_btn.setActive(false);	
						} else {
							hud_cortejar_btn.setActive(true);	
						}
					} else {
						hud_cortejar_btn.setActive(false);	
					}
				} else {
					hud_cortejar_btn.setActive(false);		
				}
			} else {
				hud_cortejar_btn.setActive(false);	
			}
		}
	}
}

function atualizarMovimentosGui(){
	if($mySelf == $jogadorDaVez)
	{
		movGui.bitmap = "~/data/images/" @ $jogadorDaVez.movimentos @ "mov.png";
		return;
	}
	
	movGui.bitmap = "~/data/images/" @ $jogadorDaVez.movimentos @ "movAdv.png";
}




function atualizarRecursosGui(){
	minerios_txt.text = $mySelf.minerios;
	petroleos_txt.text = $mySelf.petroleos;
	uranios_txt.text = $mySelf.uranios;
	clientAtualizarPEATab();
	clientVerificarFilantropia();
	verificarCanhaoOrbitalBtn();
}

function atualizarImperiaisGui(){
	if(!$estouNoTutorial || $mySelf == $jogadorDaVez){
		imperiais_txt.text = $mySelf.imperiais;
		clientVerificarBtnsAtivos(Foco.getObject(0));
		verificarOcultarBtn();
	}
}

function clientCmdAtualizarGrana(%imperiais, %minerios, %petroleos, %uranios){
	$mySelf.imperiais = %imperiais;
	$mySelf.minerios = %minerios;
	$mySelf.petroleos = %petroleos;
	$mySelf.uranios = %uranios;
		
	imperiais_txt.text = $mySelf.imperiais;
	minerios_txt.text = $mySelf.minerios;
	petroleos_txt.text = $mySelf.petroleos;
	uranios_txt.text = $mySelf.uranios;
	
	clientAtualizarEstatisticas();
	clientToggleRecursosBtns();
	clientAtualizarPEATab();
	clientVerificarFilantropia();
	verificarCanhaoOrbitalBtn();
	verificarOcultarBtn();
	clientVerificarBtnsAtivos(Foco.getObject(0));
}


/////////////////////////
//////////////////////
//Selection:
function clientSingleSelection(){
	// pega o objeto selecionado e guarda na variável local %obj
	%obj = Foco.getObject(0);
	
	clientLigarFlechas(%obj.onde.getName());
	
	// chama a função onSelected do objeto
	%obj.onSelected();
}

// --------------------------------------------------------------------
// t2dSceneObject::onSelected()
// função colocada em todos os objetos! Esta função vai ser desenvolvida.
// é aki que entra a chamada pros huds de cada unidade, por exemplo;
// --------------------------------------------------------------------
function t2dSceneObject::onSelected(%this){
	%this.setLayer(0); //coloca o objeto selecionado em primeiro plano, sempre (0 é a primeira camada, 31 é a camada de fundo)
	
	//chamar o hud especial de cada peça ao ser selcionada;
	//%this.callMyHud(); //masnão quero chamar sempre; agora chama este hud quando o usuário pressiona "a"
	
	if(%this.class $= "artefato" || %this.class $= "reliquia"){
		%this.callMyHud();
	}
}

//////Unit HUD:
function clientMostrarAcademiaDados(){
	%foco = Foco.getObject(0);
	if(%foco.dono == $mySelf){
		%foco.callMyHud();	
	}
}

function clientApagarAcademiaDados(){
	unitHud.setVisible(false);	
}

function clientZerarUnitHud(){
	escHudMicon.setVisible(false);	
	jpkHudMicon.setVisible(false);	
	snpHudMicon.setVisible(false);	
	mrlHudMicon.setVisible(false);	
	
	unitHudDefesa_txt.setVisible(false);
	unitHudDefesaL_txt.setVisible(false);
	unitHudAtaque_txt.setVisible(false);
	unitHudAtaqueL_txt.setVisible(false);
}




function resetSelection(){
	apagarBtns(); //apaga o HUD atual 
	apagarDragnalBtns(); //apaga os btns de dragnal
	resetSelectionMarks(); //coloca todas as marcas de seleção fora da tela;
	if(isObject(Foco.getObject(0))){
		Foco.getObject(0).setLayer(1); //devolve o antigo foco pra layer comum das peças;
	}
	Foco.clear(); //limpa o foco, ou seja, des-seleciona o que estava selecionado
	clientClearCruzarFlechas();
	unitHud.setVisible(false);
	desligarSniperShot(); //se o cara tinha selecionado o líder e clicado em snipershot, para evitar que outra peça use o rifle do líder;
	clientApagarRelArtTabs(); //apaga os huds de relíquia e artefato, que são ligados quando o cara clica no objeto
	clientVerificarBtnsAtivosGulok(); //verifica se o btn de entregar pode ficar ativo
}

function clientCmdResetSelection(){
	resetSelection();
}


function resetSelectionMarks(){
	$semiSelectionTanque.setPosition("-300 -300"); //fora da tela
	$semiSelectionSoldado.setPosition("-300 -300"); //fora da tela
	$semiSelectionNavio.setPosition("-300 -300"); //fora da tela
	$semiSelectionBase.setPosition("-300 -300"); //fora da tela
	if(isObject($semiSelectionReliquia)){
		$semiSelectionReliquia.setPosition("-300 -300"); //fora da tela
		$semiSelectionReliquia.dismount();
	}
	$selectionTanque.setPosition("-300 -300"); //fora da tela
	$selectionSoldado.setPosition("-300 -300"); //fora da tela
	$selectionNavio.setPosition("-300 -300"); //fora da tela
	$selectionBase.setPosition("-300 -300"); //fora da tela
	if(isObject($selectionReliquia)){
		$selectionReliquia.setPosition("-300 -300"); //fora da tela
		$selectionReliquia.dismount();
	}
		
	$selectionTanque.dismount();
	$selectionSoldado.dismount();
	$selectionNavio.dismount();
	$selectionBase.dismount();
	$semiSelectionTanque.dismount();
	$semiSelectionSoldado.dismount();
	$semiSelectionNavio.dismount();
	$semiSelectionBase.dismount();
}



function clientCmdPushAguardandoObjGui(){
	Canvas.pushDialog(aguardandoObjGui);
	aguardandoTXT.text = $jogadorDaVez.nome @ " está jogando";
}

function clientCmdPopAguardandoObjGui(){
	Canvas.popDialog(aguardandoObjGui);	
}

function clientCmdPopGruposGui(){
	Canvas.popDialog(gruposGui);
}

function clientCmdApagarBtns(){
	criarSoldado_btn.setVisible(false);
	criarTanque_btn.setVisible(false);
	criarNavio_btn.setVisible(false);
	criarBase_btn.setVisible(false);
}

function clientCmdLigarBaterBtn(){
	bater_btn.setVisible(true);	
}
function clientCmdDesligarBaterBtn(){
	bater_btn.setVisible(false);	
}



function clientPushAguardeMsgBox(){
	canvas.pushDialog(aguardeMsgBoxGui);	
}

function clientPopAguardeMsgBox(){
	canvas.popDialog(aguardeMsgBoxGui);	
}

function clientPushRenderTodosMsgBox(){
	canvas.pushDialog(renderTodosMsgBoxGui);		
}

function clientPopRenderTodosMsgBox(){
	clientClearCentralButtonControl();
	canvas.popDialog(renderTodosMsgBoxGui);		
}

function clientPushServerComDot(){
	canvas.pushDialog(serverComDotGui);		
}

function clientPopServerComDot(){
	canvas.popDialog(serverComDotGui);		
}

function clientPopNetworkMenu(){
	Canvas.popDialog(networkMenu);
}





/////////////////////////
function clientAtualizarPosReservaTxt(%area){
	%eval = "%areaPos3Text =" SPC "pos3" @ %area.getName() @ "_img;";
	eval(%eval);
		
	%count = %area.myPos3List.getCount() - 2;
	if(%count < 0){
		%count = 0;
	}
	if(%area.myPos3List.getCount() < 2){
		%areaPos3Text.setVisible(false);
	} else {
		%areaPos3Text.setVisible(true);
		%areaPos3Text.setFrame(%count);
	}
	
	if(%area.terreno !$= "mar"){ //se não for no mar (deixei assim pra poder colocar outros terrenos com 4 posições)
		if(%area.ilha != 1){ //se não for uma ilha
			%eval = "%areaPos4Text =" SPC "pos4" @ %area.getName() @ "_img;";
			eval(%eval);
			%count = %area.myPos4List.getCount() - 2;
			if(%count < 0){
				%count = 0;
			}
			if(%area.myPos4List.getCount() < 2){
				%areaPos4Text.setVisible(false);
			} else {
				%areaPos4Text.setVisible(true);
				%areaPos4Text.setFrame(%count);
			}
		}
	}
}

///////////////////////

//mostrar os objHuds:
function clientMostrarHudObjs(){
	%obj1Num = $mySelf.mySimObj.getObject(0).num;
	%obj2Num = $mySelf.mySimObj.getObject(1).num;
	
	%myObj1 = $mySelf.mySimObj.getObject(0);
	%myObj2 = $mySelf.mySimObj.getObject(1);

	//hudObj1_img.bitmap = "~/data/images/hudObj" @ $planetaAtual @ %obj1Num @ "_preview.png"; 
	if(%myObj1.grupo !$= "0"){
		hudObj1_img.bitmap = "~/data/images/objetivos/obj_img_" @ $planetaAtual @ "_" @ %myObj1.grupo @ ".png"; 
	} else {
		if(%myObj1.baias !$= "0"){
			hudObj1_img.bitmap = "~/data/images/objetivos/obj_img_" @ $planetaAtual @ "_mar.png"; 
		} else if(%myObj1.petroleos !$= "0"){
			hudObj1_img.bitmap = "~/data/images/objetivos/obj_img_petroleo.png"; 
		} else if(%myObj1.territorios !$= "0"){
			hudObj1_img.bitmap = "~/data/images/objetivos/obj_img_" @ $planetaAtual @ "_terra.png"; 
		}
	}
	
	if(%myObj2.grupo !$= "0"){
		hudObj2_img.bitmap = "~/data/images/objetivos/obj_img_" @ $planetaAtual @ "_" @ %myObj2.grupo @ ".png"; 
	} else {
		if(%myObj2.baias !$= "0"){
			hudObj2_img.bitmap = "~/data/images/objetivos/obj_img_" @ $planetaAtual @ "_mar.png"; 
		} else if(%myObj2.petroleos !$= "0"){
			hudObj2_img.bitmap = "~/data/images/objetivos/obj_img_petroleo.png"; 
		} else if(%myObj2.territorios !$= "0"){
			hudObj2_img.bitmap = "~/data/images/objetivos/obj_img_" @ $planetaAtual @ "_terra.png"; 
		}
	}
	
	hudObj1_img.setVisible(true);
	hudObj2_img.setVisible(true);
	
	hudDesc1_1.text = $mySelf.mySimObj.getObject(0).desc1;
	hudDesc1_2.text = $mySelf.mySimObj.getObject(0).desc2;
	hudDesc2_1.text = $mySelf.mySimObj.getObject(1).desc1;
	hudDesc2_2.text = $mySelf.mySimObj.getObject(1).desc2;
}

function clientMostrarGuloksObj(){
	$guloksDespertaram = true;
	hud_guloks_obj_tab.setvisible(true);
	hudDesc1_1.text = "Grande";
	hudDesc1_2.text = "Matriarca";
	hudDesc2_1.text = "";
	hudDesc2_2.text = "";
	clientAtualizarEstatisticas();
}

/////////////////
//desembarque HUD:
function clientLigarQuemDesembarcaHud(){
	desembarqueHud_SBtn.setVisible(true);
	desembarqueHud_GBtn.setVisible(true);
	desembarqueHud.setVisible(true);
}
function clientDesligarQuemDesembarcaHud(){
	clientClearCentralButtonControl();
	desembarqueHud.setVisible(false);
}

/////////////////
//clientLigarQueCorDesembarcaHud:
function clientLigarQueCorDesembarcaHud(){
	desembarqueCorHud_NBtn.setVisible(true); //desembarque amigável
	desembarqueCorHud_VBtn.setVisible(true);
	desembarqueCorHud.setVisible(true);
}
function clientDesligarQueCorDesembarcaHud(){
	clientClearCentralButtonControl();
	desembarqueCorHud.setVisible(false);
}

function clientMarcarMorto(%nomeDoMorto){
	%tempIconName = "game/data/images/playerHudMorto.png";
	
	for(%i = 1; %i < 7; %i++){
		%eval = "%player = $player" @ %i @ ";";
		eval(%eval);
		if(%player.nome $= %nomeDoMorto){
			%eval = "inGameDiplomata" @ %i @ ".bitmap = %tempIconName;";
			eval(%eval);
			%eval = "p" @ %i @ "DoarBtn.setActive(false);";
			eval(%eval);
			%player.taMorto = true;
			%eval = "%playerMorto = $player" @ %i @ ";";
			eval(%eval);
			%i = 7;
		}
	}
	clientCancelarAcordosComMorto(%nomeDoMorto);	
	
	if(%nomeDoMorto $= $myPersona.nome)
	{
		$mySelf.minerios = 0;	
		$mySelf.petroleos = 0;
		$mySelf.uranios = 0;
		$mySelf.imperiais = 0;
		$mySelf.mySimExpl.clear();
		$mySelf.mySimInfo.clear();
	}
	
	clientPopularFilantropiaGui($numDePlayersNestaPartida);
}


