// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\AI.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 8 de julho de 2008 14:15
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function TUTaiRodada1(){
	//cria 5 soldados:
	for(%i = 1; %i < 6; %i++){
		schedule(%i * 1000, 0, "clientAskSpawnUnit", pequim, "soldado");
	}
	
	//movimenta os 5 soldados, pegando a china, a monólia e duas áreas da rússia:
	schedule(6000, 0, "clientAskMovimentar", pequim, "pos1", xangai);
	schedule(7000, 0, "clientAskMovimentar", pequim, "pos2", lhasa);
	schedule(8000, 0, "clientAskMovimentar", pequim, "pos1", mongolia);
	schedule(9000, 0, "clientAskMovimentar", pequim, "pos2", omsk);
	schedule(10000, 0, "clientAskMovimentar", pequim, "pos1", magadan);
	
	//passa a vez:
	schedule(11000, 0, "clientCmdSetJogadorDaVez", "player1", $rodadaAtual+1);
	%myImperiais = mFloor($mySelf.imperiais += $mySelf.mySimAreas.getCount()/2);
	schedule(11002, 0, "clientCmdInicializarMeuTurno", 5, %myImperiais, 1, 0, 0);
}

function TUTaiRodada2(){
	//cria 2 tanques e 1 soldado:
	for(%i = 1; %i < 3; %i++){
		schedule(%i * 1000, 0, "clientAskSpawnUnit", pequim, "tanque");
	}
	schedule(3000, 0, "clientAskSpawnUnit", pequim, "soldado");
	
	//movimenta 1 tanque e 1 soldado, pegando a rússia:
	schedule(4000, 0, "clientAskMovimentar", pequim, "pos1", omsk);
	schedule(5000, 0, "clientAskMovimentar", omsk, "pos1", kirov);
	schedule(6000, 0, "clientAskMovimentar", kirov, "pos1", moscou);
	schedule(7000, 0, "clientAskMovimentar", pequim, "pos1", omsk);
	schedule(8000, 0, "clientAskMovimentar", omsk, "pos1", kirov);
		
	//passa a vez:
	schedule(9000, 0, "clientCmdSetJogadorDaVez", "player1", $rodadaAtual+1);
	%myImperiais = mFloor($mySelf.imperiais += $mySelf.mySimAreas.getCount()/2);
	schedule(9002, 0, "clientCmdInicializarMeuTurno", 5, %myImperiais, 2, 0, 1);
}

function TUTaiRodada3(){
	//cria 3 tanques:
	for(%i = 1; %i < 4; %i++){
		schedule(%i * 1000, 0, "clientAskSpawnUnit", pequim, "tanque");
	}
	
	//movimenta 2 tanques, pegando o oriente:
	schedule(4000, 0, "clientAskMovimentar", pequim, "pos1", lhasa);
	schedule(5000, 0, "clientAskMovimentar", lhasa, "pos1", cabul);
	schedule(6000, 0, "clientAskMovimentar", cabul, "pos1", bagda);
	schedule(7000, 0, "clientAskMovimentar", pequim, "pos2", lhasa);
	schedule(8000, 0, "clientAskMovimentar", lhasa, "pos1", cabul);
		
	//passa a vez:
	schedule(9000, 0, "clientCmdSetJogadorDaVez", "player1", $rodadaAtual+1);
	%myImperiais = mFloor($mySelf.imperiais += $mySelf.mySimAreas.getCount()/2);
	schedule(9002, 0, "clientCmdInicializarMeuTurno", 5, %myImperiais, 3, 0, 2);
}

function TUTaiRodada4(){
	//cria 2 tanques e 1 soldado:
	for(%i = 1; %i < 3; %i++){
		schedule(%i * 1000, 0, "clientAskSpawnUnit", pequim, "tanque");
	}
	schedule(3000, 0, "clientAskSpawnUnit", pequim, "soldado");
	
	//movimenta 2 tanques, pegando o cazaca e a índia:
	schedule(4000, 0, "clientAskMovimentar", pequim, "pos1", lhasa);
	schedule(5000, 0, "clientAskMovimentar", lhasa, "pos1", cazaquistao);
	schedule(6000, 0, "clientAskMovimentar", pequim, "pos1", lhasa);
	schedule(7000, 0, "clientAskMovimentar", lhasa, "pos2", india);
	
	//embarca um truta num navio e desembarca no vietnã:
	schedule(8000, 0, "clientAskEmbarcar", pequim, "pos3", bMarDaChina, "pos1");
	schedule(10000, 0, "clientAskDesembarcar", bMarDaChina, "pos1", vietna, "soldado", "minhaCor");
		
	//passa a vez:
	schedule(12000, 0, "clientCmdSetJogadorDaVez", "player1", $rodadaAtual+1);
	%myImperiais = mFloor($mySelf.imperiais += $mySelf.mySimAreas.getCount()/2);
	schedule(12002, 0, "clientCmdInicializarMeuTurno", 5, %myImperiais, 4, 0, 4);
}

function TUTaiRodada5(){
	//embarca dois trutas num navio:
	schedule(1000, 0, "clientAskEmbarcar", pequim, "pos3", bMarDaChina, "pos1");
	schedule(2000, 0, "clientAskEmbarcar", pequim, "pos3", bMarDaChina, "pos1");
	
	//movimenta o navio até o mar nórdico:
	schedule(3000, 0, "clientAskMovimentar", bMarDaChina, "pos1", bMarDoJapao);
	schedule(4000, 0, "clientAskMovimentar", bMarDoJapao, "pos1", bMarChukchi);
	schedule(5000, 0, "clientAskMovimentar", bMarChukchi, "pos1", oceanoArtico);
	schedule(7000, 0, "clientAskMovimentar", oceanoArtico, "pos1", bMarNordico);
	
	//desembarca um truta em Londres:
	schedule(9000, 0, "clientAskDesembarcar", bMarNordico, "pos1", londres, "soldado", "minhaCor");
	
	//cria 2 navios pra guardar o porto da china:
	schedule(10000, 0, "clientAskSpawnUnit", bMarDaChina, "navio");
	schedule(11000, 0, "clientAskSpawnUnit", bMarDaChina, "navio");
			
	//passa a vez:
	schedule(13000, 0, "clientCmdSetJogadorDaVez", "player1", $rodadaAtual+1);
	%myImperiais = mFloor($mySelf.imperiais += $mySelf.mySimAreas.getCount()/2);
	schedule(13002, 0, "clientCmdInicializarMeuTurno", 5, %myImperiais, 5, 1, 6);
}

function TUTaiRodada6(){
	//paga 1 urânio que foi negociado:
	$mySelf.uranios++;
	atualizarRecursosGui();
	
	//desembarca um truta na Comunidade Européia:
	schedule(1000, 0, "clientAskDesembarcar", bMarNordico, "pos1", comunidadeEuropeia, "soldado", "minhaCor");
	
	//constrói uma base na comunidade européia:
	schedule(3000, 0, "clientAskConstruirBase", comunidadeEuropeia);
	
	//cria 3 tanques pra guardar a nova base:
	schedule(5000, 0, "clientAskSpawnUnit", comunidadeEuropeia, "tanque");
	schedule(6000, 0, "clientAskSpawnUnit", comunidadeEuropeia, "tanque");	
	schedule(7000, 0, "clientAskSpawnUnit", comunidadeEuropeia, "tanque");	
	
	//movimenta o soldado até estocolmo:
	schedule(9000, 0, "clientAskMovimentar", comunidadeEuropeia, "pos3", moscou);
	schedule(10000, 0, "clientAskMovimentar", moscou, "pos2", estocolmo);
		
	//cria mais um truta e um barco na china:
	schedule(12000, 0, "clientAskSpawnUnit", pequim, "soldado");
	schedule(13000, 0, "clientAskSpawnUnit", bMarDachina, "navio");
	
	//embarca o truta no barco:
	schedule(15000, 0, "clientAskEmbarcar", pequim, "pos3", bMarDaChina, "pos1");
	
	//movimenta o barco pro mar do japão:
	schedule(16000, 0, "clientAskMovimentar", bMarDaChina, "pos1", bMarDoJapao);
	
	//desembarca o carinha no japão:
	schedule(17000, 0, "clientAskDesembarcar", bMarDoJapao, "pos1", japao, "soldado", "minhaCor");
			
	//passa a vez:
	schedule(18000, 0, "clientCmdSetJogadorDaVez", "player1", $rodadaAtual+1);
	%myImperiais = mFloor($mySelf.imperiais += $mySelf.mySimAreas.getCount()/2);
	schedule(18002, 0, "clientCmdInicializarMeuTurno", 5, %myImperiais, 7, 2, 9);
}

function TUTaiEnviarResposta(){
	clientCmdRespostaRecebida(64, 72);	
}

/////////////////////////
//////////////////////////////////////////
////////////////////////////////////////////////////////////////
//////////////////////////////////////////
////////////////////////
//Guloks:
function clientCmdGulokArmaggedon(){
	clientPauseGame();
	clientCmdDesastre(UNG_PaGu03, "gas", false);
	
	//liga o efeito especial do surgimento da matriarca:
	%explosao = new t2dParticleEffect(){scenegraph = UNG_PaGu03.scenegraph;};
	%explosao.loadEffect("~/data/particles/grandeMatriarcaFX2.eff");
	%explosao.setEffectLifeMode("KILL", 15);
    %explosao.setPosition(UNG_PaGu03.pos0);
    %explosao.playEffect();
	
	alxPlay( gulok_appear );
	
	schedule(900, 0, "clientCmdDesastre", UNG_PaGu01, "gas", false);	
	schedule(1800, 0, "clientCmdDesastre", UNG_PaGu02, "gas", false);
	schedule(2700, 0, "clientCmdDesastre", UNG_VaGu02, "gas", false);	
	schedule(3600, 0, "clientCmdDesastre", UNG_VaGu01, "gas", false);	
	schedule(4500, 0, "clientCmdDesastre", UNG_b19, "gas", false);	
	schedule(5400, 0, "clientCmdDesastre", UNG_b18, "gas", false);	
	schedule(6300, 0, "clientCmdDesastre", UNG_b22, "gas", false);	
	schedule(7200, 0, "clientCmdDesastre", UNG_b23, "gas", false);	
	schedule(8100, 0, "clientCmdDesastre", UNG_b25, "gas", false);	
	schedule(9000, 0, "clientCmdDesastre", UNG_b38, "gas", false);	
	schedule(9900, 0, "clientCmdDesastre", UNG_b39, "gas", false);	
	schedule(10800, 0, "clientCmdDesastre", UNG_ChOc02, "gas", false);	
	schedule(11700, 0, "clientCmdDesastre", UNG_ChOc01, "gas", false);	
	schedule(15000, 0, "clientResumeGame");		
	$mySelf.mySimObj.clear();
	clientMostrarGuloksObj();
}

function clientCmdSetAiManager(){
	$mySelf.aiManager = true;	
}

function clientCmdAskAIdirections(%step){
	if(%step == 0){
		$mySelf.aiManager = true;
		//Guloks estão nascendo. Criar e posicionar primeiras peças:
		clientAskCriarGrandeMatriarca(UNG_PaGu03); //primeiro a grande matriarca
		//agora as rainhas complementares:
		schedule(1000, 0, "clientAskCriarRainha", UNG_PaGu01, false, true); //%onde, %semLideres, %AI
		schedule(2000, 0, "clientAskCriarRainha", UNG_PaGu02, true, true);
		schedule(3000, 0, "clientAskCriarRainha", UNG_VaGu02, true, true);
		schedule(4000, 0, "clientAskCriarRainha", UNG_VaGu01, true, true);
		schedule(5000, 0, "clientAskCriarRainha", UNG_b19, true, true);
		schedule(6000, 0, "clientAskCriarRainha", UNG_b18, true, true);
		schedule(7000, 0, "clientAskCriarRainha", UNG_b22, true, true);
		schedule(8000, 0, "clientAskCriarRainha", UNG_b23, true, true); 
		schedule(9000, 0, "clientAskCriarRainha", UNG_b25, true, true);
		schedule(10000, 0, "clientAskCriarRainha", UNG_b38, true, true);
		schedule(11000, 0, "clientAskCriarRainha", UNG_b39, true, true);
		schedule(12000, 0, "clientAskCriarRainha", UNG_ChOc02, true, true);
		schedule(13000, 0, "clientAskCriarRainha", UNG_ChOc01, true, true);
	} else if(%step == 1){
		//ataca uma área vizinha de cada área que possui;
		//movimenta um cefalok ou verme para uma área vizinha vazia, até 5 movimentos;
		//evolui vermes e cefaloks solitários para rainhas;
		//bota um ovo ou cria um cefalok em cada rainha;
		//passa a vez;
				
		schedule(5000, 0, "GULOK_AI_atacarAreasVizinhas", "GULOK_AI_movimentarParaAreasVizinhas2", "GULOK_AI_evoluirGuloksSolitarios", "GULOK_AI_botarOvos", "GULOK_AI_passarAVez");
	} else if(%step == 2){
		//movimenta um cefalok ou verme para uma área vizinha vazia, até 5 movimentos;
		//evolui vermes e cefaloks de todas as áreas que não tenham rainhas;
		//cria ovos e cefaloks em todas as rainhas;
		//faz um ataque dragnal na primeira base de quem despertou os guloks;
		//passa a vez;
		
		schedule(5000, 0, "GULOK_AI_movimentarParaAreasVizinhas2", "GULOK_AI_evoluirPossiveisRainhas", "GULOK_AI_botarOvos", "GULOK_AI_dragnalNoSacana", "GULOK_AI_passarAVez"); 
		
	} else if(%step == 3 || %step == 5 || %step == 7 || %step >= 9){
		%result = dado(4, 0);
		GULOK_AI_decretarMoratorias();
		if(%result == 1 || 4){
			//ataca uma área vizinha de cada área que possui;
			//movimenta um cefalok, verme ou rainha para uma área vizinha vazia, até 5 movimentos;
			//evolui vermes e cefaloks possíveis para rainhas;
			//bota um ovo ou cria um cefalok em cada rainha;
			//passa a vez;
			schedule(5000, 0, "GULOK_AI_atacarAreasVizinhas", "GULOK_AI_movimentarParaAreasVizinhas2", "GULOK_AI_evoluirPossiveisRainhas", "GULOK_AI_botarOvos", "GULOK_AI_passarAVez"); //segundo param é função a ser exec
		} else if(%result == 2){
			schedule(5000, 0, "GULOK_AI_atacarAreasVizinhas", "GULOK_AI_movimentarParaAreasVizinhas2", "GULOK_AI_evoluirGuloksSolitarios", "GULOK_AI_botarOvos", "GULOK_AI_passarAVez"); //segundo param é função a ser exec
		} else if(%result == 3){
			schedule(5000, 0, "GULOK_AI_atacarAreasVizinhas", "GULOK_AI_movimentarParaAreasVizinhas2", "GULOK_AI_dragnalNoSacana", "GULOK_AI_botarOvos", "GULOK_AI_passarAVez"); //segundo param é função a ser exec
		}
	} else if(%step == 4){
		schedule(5000, 0, "GULOK_AI_virusNoSacana", "GULOK_AI_atacarAreasVizinhas", "GULOK_AI_movimentarParaAreasVizinhas2", "GULOK_AI_botarOvos", "GULOK_AI_passarAVez"); 
	} else if(%step == 6){
		schedule(5000, 0, "GULOK_AI_atacarAreasVizinhas", "GULOK_AI_movimentarParaAreasVizinhas2", "GULOK_AI_dragnalNoSacana", "GULOK_AI_botarOvos", "GULOK_AI_passarAVez"); //segundo param é função a ser exec
	} else if(%step == 8){
		schedule(5000, 0, "GULOK_AI_virusNoSacana", "GULOK_AI_atacarAreasVizinhas", "GULOK_AI_atacarAreasVizinhas", "GULOK_AI_movimentarParaAreasVizinhas2", "GULOK_AI_passarAVez"); 
	}
}

function GULOK_AI_atacarAreasVizinhas(%execFunction1, %execFunction2, %execFunction3, %execFunction4){
	//pegar a primeira área que faz fronteira com algum adversário:
	for(%i = 0; %i < $aiPlayer.mySimAreas.getCount(); %i++){
		%area = $aiPlayer.mySimAreas.getObject(%i);
		%fronteirasCount = %area.myFronteiras.getCount();
		for(%j = 0; %j < %fronteirasCount; %j++){
			%vizinha = %area.myFronteiras.getObject(%j);
			if(%vizinha.dono != $aiPlayer && %vizinha.dono != 0 && !%vizinha.desprotegida){
				if(%area.myName !$= "UNG_PaGu03"){
					%areaAlvoSubmersa = clientVerificarAreaSubmersa(%vizinha);
					if(!%areaAlvoSubmersa || %area.terreno $= "mar"){
						//escolher esta para atacar:
						schedule(1000 + (2000 * %ataques), 0, "clientAskAtacar", %area, %vizinha);
						%j = %fronteirasCount;
						%ataques++;
					}
				}
			}
		}
	}
	//33% de chance de refazer os ataques, apenas de áreas que possuem alguém na pos1 e uma rainha:
	%result = dado(3, 0);
	if(%result == 1){
		for(%i = 0; %i < $aiPlayer.mySimAreas.getCount(); %i++){
			%area = $aiPlayer.mySimAreas.getObject(%i);
			%fronteirasCount = %area.myFronteiras.getCount();
			for(%j = 0; %j < %fronteirasCount; %j++){
				%vizinha = %area.myFronteiras.getObject(%j);
				if(%vizinha.dono != $aiPlayer && %vizinha.dono != 0 && !%vizinha.desprotegida){
					if(%area.myName !$= "UNG_PaGu03"){
						if(%area.pos0Flag == true && isObject(%area.pos1Quem)){
							%areaAlvoSubmersa = clientVerificarAreaSubmersa(%vizinha);
							if(!%areaAlvoSubmersa || %area.terreno $= "mar"){
								//escolher esta para atacar:
								schedule(1000 + (2000 * %ataques), 0, "clientAskAtacar", %area, %vizinha);
								%j = %fronteirasCount;
								%ataques++;
							}
						}
					}
				}
			}
		}
	}
	%totalTime = 1000 + (2000 * %ataques);
	if(%execFunction1 !$= ""){
		schedule(%totalTime, 0, %execFunction1, %execFunction2, %execFunction3, %execFunction4);
	}
	//echo("##_AI: fim do Atacar Áreas Vizinhas; ataques feitos: " @ %ataques @ ".");	
}

function GULOK_AI_movimentarParaAreasVizinhas(%execFunction1, %execFunction2, %execFunction3, %execFunction4){
	//pegar a primeira área fronteiriça que está vazia:
	%myMaxMov = 5;
	if($aiPlayer.mysimBases.getcount() >= 10){
		%myMaxMov = 10;
	}
	for(%i = 0; %i < $aiPlayer.mySimAreas.getCount(); %i++){
		%area = $aiPlayer.mySimAreas.getObject(%i);
		%fronteirasCount = %area.myFronteiras.getCount();
		for(%j = 0; %j < %fronteirasCount; %j++){
			%vizinha = %area.myFronteiras.getObject(%j);
			if(%vizinha.dono $= "0" && %movimentos < %myMaxMov){
				if(%area.pos1Flag !$= "nada"){
					if(%area.terreno $= %vizinha.terreno){
						if(%area.getName() !$= "UNG_PaGu03"){
							//escolher esta para mover:
							schedule(1000 + (2000 * %movimentos), 0, "clientAskMovimentar", %area, "pos1", %vizinha); //move um cefalok, zangão, ou verme
							%j = %fronteirasCount;
							%movimentos++;
						}
					}
				}
			}
		}
	}
	%totalTime = 1000 + (2000 * %movimentos);
	if(%execFunction1 !$= ""){
		schedule(%totalTime, 0, %execFunction1, %execFunction2, %execFunction3, %execFunction4);
	}
}

function GULOK_AI_movimentarParaAreasVizinhas2(%execFunction1, %execFunction2, %execFunction3, %execFunction4){
	%movimentos = 0;
	%myMaxMov = 5;
	if($aiPlayer.mysimBases.getcount() >= 10){
		%myMaxMov = 10;
	}
	//pegar a primeira área fronteiriça que está vazia:
	for(%i = 0; %i < $aiPlayer.mySimAreas.getCount(); %i++){
		%area = $aiPlayer.mySimAreas.getObject(%i);
		%fronteirasCount = %area.myFronteiras.getCount();
		for(%j = 0; %j < %fronteirasCount; %j++){
			%vizinha = %area.myFronteiras.getObject(%j);
			if(%movimentos < %myMaxMov){
				if(%vizinha.dono $= "0"){
					if(%area.pos1Flag !$= "nada"){
						if(%area.terreno $= %vizinha.terreno){
							if(%area.getName() !$= "UNG_PaGu03"){
								//escolher esta para mover:
								schedule(1000 + (2000 * %movimentos), 0, "clientAskMovimentar", %area, "pos1", %vizinha); //move um cefalok, zangão, ou verme
								%j = %fronteirasCount;
								%movimentos++;
							}
						} else {
							if(%area.pos0Flag == true){
								if(%area.getName() !$= "UNG_PaGu03"){
									//entrar com a rainha:
									schedule(1000 + (2000 * %movimentos), 0, "clientAskMovimentar", %area, "pos0", %vizinha); //move a rainha
									%j = %fronteirasCount;
									%movimentos++;	
								}
							}
						}
					}
				} else {
					//verifica se a área estava desprotegida:
					if(%vizinha.pos0Flag == true && %vizinha.pos1Flag $= "nada" && %vizinha.pos2Flag $= "nada" && %vizinha.dono != $aiPlayer && %vizinha.pos0Quem.class !$= "Rainha"){
						//esta área está desprotegida. Capturar!
						if(%area.pos0Flag == true){ //tenho uma rainha pra capturar esta área
							if(%area.getName() !$= "UNG_PaGu03"){
								schedule(1000 + (2000 * %movimentos), 0, "clientAskRainhaCapturarBase", %area, "pos0", %vizinha); //move a rainha
								%movimentos++;
							}
						} else {
							if(%area.terreno $= %vizinha.terreno){
								if(%area.getName() !$= "UNG_PaGu03"){
									schedule(1000 + (2000 * %movimentos), 0, "clientAskMovimentar", %area, "pos1", %vizinha); //move um cefalok, zangão, ou verme
									%movimentos++;
								}
							}
						}
					}
				}
			} 
		}
	}
	%totalTime = 1000 + (2000 * %movimentos);
	if(%execFunction1 !$= ""){
		schedule(%totalTime, 0, %execFunction1, %execFunction2, %execFunction3, %execFunction4);
	}
}


function GULOK_AI_evoluirGuloksSolitarios(%execFunction2, %execFunction3, %execFunction4){
	//pegar a primeira área
	for(%i = 0; %i < $aiPlayer.mySimAreas.getCount(); %i++){
		%area = $aiPlayer.mySimAreas.getObject(%i);
		if((%area.pos1Quem.class $= "cefalok" || %area.pos1Quem.class $= "verme")&& %area.pos2Flag $= "nada" && %area.pos0Flag == false){
			//só tem um cefalok ou um verme solitário nesta área. Evoloui ele numa rainha:
			schedule(1000 + (1000 * %evolucoes), 0, "clientAskEvoluirEmRainha", %area, "pos1", true);
			%evolucoes++;
		}
	}
	%totalTime = 1000 + (1000 * %evolucoes);
	if(%execFunction2 !$= ""){
		schedule(%totalTime, 0, %execFunction2, %execFunction3, %execFunction4);
	}
}

function GULOK_AI_evoluirPossiveisRainhas(%execFunction2, %execFunction3, %execFunction4){
	//pegar a primeira área
	for(%i = 0; %i < $aiPlayer.mySimAreas.getCount(); %i++){
		%area = $aiPlayer.mySimAreas.getObject(%i);
		if((%area.pos1Quem.class $= "cefalok" || %area.pos1Quem.class $= "verme") && %area.pos0Flag == false){
			//tem cefaloks ou vermes nesta área, mas nenhuma rainha. Evolui numa rainha:
			schedule(1000 + (1000 * %evolucoes), 0, "clientAskEvoluirEmRainha", %area, "pos1", true);
			%evolucoes++;
		}
	}
	%totalTime = 1000 + (1000 * %evolucoes);
	if(%execFunction2 !$= ""){
		schedule(%totalTime, 0, %execFunction2, %execFunction3, %execFunction4);
	}
}

function GULOK_AI_botarOvos(%execFunction3, %execFunction4){
	//pegar a primeira rainha:
	for(%i = 0; %i < $aiPlayer.mySimBases.getCount()-1; %i++){
		%rainha = $aiPlayer.mySimBases.getObject(%i+1);
		if(%rainha.onde.terreno $= "terra"){
			if(%rainha.onde.myPos3List.getcount() < 6){
				schedule(1000 + (1000 * %unitSpawned), 0, "clientAskSpawnUnit", %rainha.onde, "ovo", true);
				%unitSpawned++;
			}
		} else {
			if(%rainha.onde.myPos3List.getcount() < 1){
				schedule(1000 + (1000 * %unitSpawned), 0, "clientAskSpawnUnit", %rainha.onde, "cefalok", true);
				%unitSpawned++;
			}
		}
	}
	%totalTime = 1000 + (1000 * %unitSpawned);
	if(%execFunction3 !$= ""){
		schedule(%totalTime, 0, %execFunction3, %execFunction4);
	}
}

function GULOK_AI_dragnalNoSacana(%execFunction3, %execFunction4){
	%result = dado(3, 0);
	%eval = "%adversario = $player" @ %result @ ";";
	eval(%eval);
	if(%adversario == $aiPlayer){
		%eval = "%adversario = $player" @ %result - 1 @ ";";
		eval(%eval);	
	}
	//pegar a primeira base do player:
	if(isObject(%adversario.mySimBases.getObject(0))){
		%base = %adversario.mySimBases.getObject(0);
		schedule(1000, 0, "clientAskDragnalAtacar", %base.onde, true);
		if(%execFunction3 !$= ""){
			schedule(10000, 0, %execFunction3, %execFunction4);
		}
	} else {
		if(%execFunction3 !$= ""){
			schedule(1000, 0, %execFunction3, %execFunction4);
		}
	}
}

function GULOK_AI_virusNoSacana(%execFunction1, %execFunction2, %execFunction3, %execFunction4){
	%result = dado(3, 0);
	%eval = "%adversario = $player" @ %result @ ";";
	eval(%eval);
	if(%adversario == $aiPlayer){
		%eval = "%adversario = $player" @ %result - 1 @ ";";
		eval(%eval);	
	}
	//pegar a primeira base do player:
	if(isObject(%adversario.mySimBases.getObject(0))){
		%base = %adversario.mySimBases.getObject(0);
		if(isObject(UNG_PaGu03.pos0Quem)){
			schedule(1000, 0, "clientAskVirusGulok", %base.onde, true, UNG_PaGu03.pos0Quem);
			if(%execFunction1 !$= ""){
				schedule(8000, 0, %execFunction1, %execFunction2, %execFunction3, %execFunction4);
			}
		} else {
			if(%execFunction1 !$= ""){
				schedule(1000, 0, %execFunction1, %execFunction2, %execFunction3, %execFunction4);
			}
		}
	} else {
		if(%execFunction1 !$= ""){
			schedule(1000, 0, %execFunction1, %execFunction2, %execFunction3, %execFunction4);
		}
	}
}

function GULOK_AI_decretarMoratorias(){
	echo("##AI: Decretando moratórias...");
	commandToServer('gulokAIdecretarMoratorias');
}

function GULOK_AI_passarAVez(){
	commandToServer('AiPassarAVez');
}


function clientCmdDescobrirGuloks(){
	$myPersona.user.conhece_guloks = 1;
	$conheco_guloks = true;
	msgBoxParabens_guloks.setVisible(true);
}

function clientPopParabens_guloks(){
	msgBoxParabens_guloks.setVisible(false);	
}
