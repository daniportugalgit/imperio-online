// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientNegociacoes.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  terça-feira, 15 de janeiro de 2008 15:48
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================
function clientSetApagarPerguntaTimer(%tempo){
	cancel($apagarPerguntaTimer);
	$apagarPerguntaTimer = schedule(%tempo, 0, "clientCancelarPergunta");	
}

function clientCancelarPergunta(){
	cancel($apagarPerguntaTimer);
	commandToServer('cancelarPergunta', $NEGareaAlvo.getName());
	$NEGareaDeOrigem = "";
	$NEGposDeOrigem = "";
	$NEGareaAlvo = "";
}

function clientAskPermissaoParaVisitar(%areaDeOrigem, %posDeOrigem, %areaAlvo){
	%foco = Foco.getObject(0);
	if($mySelf.movimentos > 0 || (%foco.class $= "lider" && %foco.JPagora > 0)){
		if($estouPerguntando){
			clientCancelarPergunta();	
		} else {
			if(!(%foco.class $= "rainha" && %areaAlvo.pos0Flag == true)){
				%eval = "%mySelf =" SPC $mySelf @ ";";
				eval(%eval);
				
				%areaDeOrigemClient = %areaDeOrigem.getName();
				%areaAlvoClient = %areaAlvo.getName();
				clientPushServercomDot();
				commandToServer('askPermissaoParaVisitar', %areaDeOrigemClient, %posDeOrigem, %areaAlvoClient);	
				
				//agora atualiza o msgGui de quem pediu:
				cancel($msgGuiSchedule);
				cancel($apagarPerguntaTimer);
				clientMsg("pedirPassagem", 5000);
				
				//liga o marcador na tela:
				%animationName = "arrow" @ %mySelf.myColor @ "Anim";
				arrowAnim.playAnimation(%animationName);
				arrowAnim.setPosition(%areaAlvo.pos2);
				clientSetApagarPerguntaTimer(5000); //cancela a pergunta dentro de 8 segundos se não houver qualquer resposta;
				$estouPerguntando = true;
			}
		}	
	} else {
		clientMsg("movimentosAcabaram", 3000);	
	}
}
//resposta positiva, grátis (simplesmente manda andar em todos os clients e atualiza o msgGui):
function permissoGuiDeixarPassar(){
	arrowAnim.setPosition("-70 -70"); //marcador fora da tela
	cancel($apagarPerguntaTimer);
	clientPopServercomDot();
	commandToServer('visitarArea', $NEGareaDeOrigem, $NEGposDeOrigem, $NEGareaAlvo);
}

//resposta negativa (apaga a seta e chama uma clientMsg pra quem recebeu a negada):
function permissoGuiNaoDeixarPassar(){
	arrowAnim.setPosition("-70 -70"); //marcador fora da tela
	cancel($apagarPerguntaTimer);
	clientPopServercomDot();
	commandToServer('visitaNegada', $NEGareaDeOrigem, $NEGposDeOrigem);
}
///////////

//clientAskPermissaoParaDesembarcar(%foco.onde, %foco.pos, $areaObj, %navio);
function clientAskPermissaoParaEmbarcar(%areaDeOrigem, %posDeOrigem, %areaAlvo, %navioPos){
	if($estouPerguntando){
		clientCancelarPergunta();	
	} else {
		%eval = "%mySelf =" SPC $mySelf @ ";";
		eval(%eval);
		
		%areaDeOrigemClient = %areaDeOrigem.getName();
		%areaAlvoClient = %areaAlvo.getName();
		clientPushServercomDot();
		commandToServer('askPermissaoParaEmbarcar', %areaDeOrigemClient, %posDeOrigem, %areaAlvoClient, %navioPos);	
		
		//agora atualiza o msgGui de quem pediu:
		cancel($msgGuiSchedule);
		cancel($apagarPerguntaTimer);
		clientMsg("embarqueAmigavel", 8000);
		
		//liga o marcador na tela:
		%animationName = "arrow" @ %mySelf.myColor @ "Anim";
		arrowAnim.playAnimation(%animationName);
		%eval = "arrowAnim.setPosition(%areaAlvo." @ %navioPos @ ");";
		eval(%eval);
		clientSetApagarPerguntaTimer(8000); //cancela a pergunta dentro de 8 segundos se não houver qualquer resposta;
		$estouPerguntando = true;
	}
}


//clientAskPermissaoParaDesembarcar(%foco.onde, %foco.pos, $areaObj);
function clientAskPermissaoParaDesembarcar(%areaDeOrigem, %posDeOrigem, %areaAlvo){
	if($mySelf.movimentos > 0){
		if($estouPerguntando){
			clientCancelarPergunta();	
		} else {
			%eval = "%mySelf =" SPC $mySelf @ ";";
			eval(%eval);
			
			%areaDeOrigemClient = %areaDeOrigem.getName();
			%areaAlvoClient = %areaAlvo.getName();
			clientPushServercomDot();
			commandToServer('askPermissaoParaDesembarcar', %areaDeOrigemClient, %posDeOrigem, %areaAlvoClient);	
			
			//agora atualiza o msgGui de quem pediu:
			cancel($msgGuiSchedule);
			cancel($apagarPerguntaTimer);
			clientMsg("desembarqueAmigavel", 8000);
			
			//liga o marcador na tela:
			%animationName = "arrow" @ %mySelf.myColor @ "Anim";
			arrowAnim.playAnimation(%animationName);
			arrowAnim.setPosition(%areaAlvo.pos2);
			clientSetApagarPerguntaTimer(8000); //cancela a pergunta dentro de 5 segundos se não houver qualquer resposta;
			$estouPerguntando = true;
		}
	} else {
		clientMsg("movimentosAcabaram", 3000);	
	}
}

//resposta positiva ao desembarque amigável:
function permissoGuiPermitirDesembarque(){
	arrowAnim.setPosition("-70 -70"); //marcador fora da tela
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	clientPopServercomDot();
	commandToServer('desembarcarVisitando', $NEGareaDeOrigem, $NEGposDeOrigem, $NEGareaAlvo);
}

//resposta positiva ao desembarque amigável:
function permissoGuiPermitirEmbarque(){
	arrowAnim.setPosition("-70 -70"); //marcador fora da tela
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	clientPopServercomDot();
	commandToServer('embarcarVisitando', $NEGareaDeOrigem, $NEGposDeOrigem, $NEGareaAlvo, $NEGnavioPos);
}

////////////////////////////////
//visita com desembarque:
function clientCmdDesembarcarVisitando(%areaDeOrigem, %posDeOrigem, %areaAlvo){
	%eval = "%areaDeOrigem =" SPC %areaDeOrigem @ ";";
	eval(%eval);
	%eval = "%areaAlvo =" SPC %areaAlvo @ ";";
	eval(%eval);
	%eval = "%navio =" SPC %areaDeOrigem @ "." @ %posDeOrigem @ "Quem;";
	eval(%eval);
		
	
	%navio.unMark();
	$desembarcando = true;
	%soldado = %navio.myTransporte.getObject(0);
	%soldado.dismount();
	%soldado.setPosition(%navio.getPosition());
	%soldado.setVisible(true);
	if(%areaAlvo.desprotegida == true){ //cancela a visita e captura a base
		%eval = "%jogadorInvadido = $" @ %areaAlvo.dono.id @ ";"; //garante q o sistema reconheça o jogador invadido
		eval(%eval);
		%jogadorInvadido.mySimAreas.remove(%areaAlvo); //remove a Área-alvo do jogador invadido
		%areaAlvo.pos0Quem.dono = $jogadorDaVez; //marca na Base capturada seu novo dono
		%areaAlvo.pos0Quem.setMyImage(); //colore a Base da cor do novo dono
		clientDesembarcar(%soldado, %navio, %areaAlvo.getName());
	} else {
		clientDesembarcarVisitando(%soldado, %navio, %areaAlvo.getName());
	}
		
	%navio.myTransporte.remove(%soldado);
	%navio.reMark();
	$desembarcando = false;
	clientAtualizarEstatisticas();
	clientPopServercomDot();
}


//////////////
//desembarcar visitando:
function clientDesembarcarVisitando(%unit, %navio, %areaAlvo){
	//estabelecendo o jogador da vez para a função:
	%eval = "%jogadorDaVez =" SPC $jogadorDaVez @ ";";
	eval(%eval);
	%eval = "%jogadorDaVezAreas = $" @ $jogadorDaVez.id @ "Areas;";
	eval(%eval);
	%eval = "%jogadorVisitadoAreas = $" @ %areaAlvo.dono.id @ "Areas;";
	eval(%eval);
	
	%areaAlvo.positionUnit(%unit);
	%areaAlvo.resolverMyStatus(); //verifica de quem é a área, marca o dono na área, adiciona a área ao simset do dono se for o caso, captura base se for o caso
	%jogadorDaVez.movimentos -= 1;
	atualizarMovimentosGui();
	clientAtualizarEstatisticas();
	clientClearUndo(); //limpa o Undo, só há Undo pra movimentos
}

////////

//função unificada para o embarque de qq posição:
function clientCmdEmbarcarVisitando(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posDoNavio){
	clientPopServerComDot();
		
	if(%posDeOrigem $= "pos3"){
		%unidade = %areaDeOrigem.myPos3List.getObject(0);
	} else {
		%eval = "%unidade =" SPC %areaDeOrigem @ "." @ %posDeOrigem @ "Quem;";
		eval(%eval);
	}
	%eval = "%navio =" SPC %areaAlvo @ "." @ %posDoNavio @ "Quem;";
	eval(%eval);
		
	if (%navio.transporteFlag == false){
		%navio.myTransporte = new SimSet();
		%navio.transporteFlag = true;
	}
	
	resetSelection();
	%navio.myTransporte.add(%unidade);
	clientRemoverUnidade(%unidade, %unidade.onde);
	%unidade.action("moveToLoc", %navio.getPosition());
	%unidade.onde = "navio";
	%unidade.setVisible(false);
	%navio.mark(%navio.myTransporte.getCount());
	%unidade.mount(%navio, 0, 0, 0, 0, 0, 0, 0); //monta o truta no navio
	
	clientAtualizarEstatisticas();
	clientClearUndo(); //limpa o Undo, só há Undo pra movimentos
	ghostSelect(%navio); //mostra pros outros clients que é o navio que está sendo manipulado pelo jogadorDaVez
}



/////////////////////////////////////////////////////
//Exploração:
function clientLigarExplMarkers(){
	clientDesligarExplMarkers();
	
	if($explMarkersOn == false){
		if($doarMarkersOn){
			clientDesligarDoarMarkers();
		}
		if($moratoriaMarkersOn){
			clientDesligarMoratoriaMarkers();
		}
		for (%i = 0; %i < $mySelf.mySimInfo.getCount(); %i++){
			%info = $mySelf.mySimInfo.getObject(%i);
			if (%info.bonusM > 0){
				%eval = "%areaDaMissao =" SPC %info.area @ ";";
				eval(%eval);
				if(%areaDaMissao.dono.id !$= $mySelf.id && %areaDaMissao.dono !$= "0" && %areaDaMissao.dono !$= "MISTA"){
					if(%areaDaMissao.dono $= "COMPARTILHADA" && (%areaDaMissao.dono1 == $mySelf || %areaDaMissao.dono2 == $mySelf)){
						
					} else {
						%explMark = explMarkerMinerio.createCopy();	
						%explMark.setPosition(%areaDaMissao.pos2);
						%info.myExplMark = %explMark;
						%explMark.setUseMouseEvents(true);
						%explMark.infoNum = %info.num;
						%explMark.area = %info.area;
						clientExplFx(%explMark);	
					}
				}
			} else if (%info.bonusP > 0){
				%eval = "%areaDaMissao =" SPC %info.area @ ";";
				eval(%eval);
				if(%areaDaMissao.dono.id !$= $mySelf.id && %areaDaMissao.dono !$= "0" && %areaDaMissao.dono !$= "MISTA"){
					if(%areaDaMissao.dono $= "COMPARTILHADA" && (%areaDaMissao.dono1 == $mySelf || %areaDaMissao.dono2 == $mySelf)){
						
					} else {
						%explMark = explMarkerPetroleo.createCopy();	
						%explMark.setPosition(%areaDaMissao.pos2);
						%info.myExplMark = %explMark;
						%explMark.setUseMouseEvents(true);
						%explMark.infoNum = %info.num;
						%explMark.area = %info.area;
						clientExplFx(%explMark);	
					}
				}
			} else if (%info.bonusU > 0){
				%eval = "%areaDaMissao =" SPC %info.area @ ";";
				eval(%eval);
				if(%areaDaMissao.dono.id !$= $mySelf.id && %areaDaMissao.dono !$= "0" && %areaDaMissao.dono !$= "MISTA"){
					if(%areaDaMissao.dono $= "COMPARTILHADA" && (%areaDaMissao.dono1 == $mySelf || %areaDaMissao.dono2 == $mySelf)){
						
					} else {
						%explMark = explMarkerUranio.createCopy();	
						%explMark.setPosition(%areaDaMissao.pos1);
						%info.myExplMark = %explMark;
						%explMark.setUseMouseEvents(true);
						%explMark.infoNum = %info.num;
						%explMark.area = %info.area;
						clientExplFx(%explMark);
					}
				}
			} else if( %info.bonusPt > 0){
				%eval = "%areaDaMissao =" SPC %info.area @ ";";
				eval(%eval);
				if(%areaDaMissao.dono.id !$= $mySelf.id && %areaDaMissao.dono !$= "0" && %areaDaMissao.dono !$= "MISTA"){
					if(%areaDaMissao.dono $= "COMPARTILHADA" && (%areaDaMissao.dono1 == $mySelf || %areaDaMissao.dono2 == $mySelf)){
						
					} else {
						if(!%info.jahFoiOferecida){
							%explMark = explMarkerPontos.createCopy();	
							%explMark.setPosition(%areaDaMissao.pos0);
							%info.myExplMark = %explMark;
							%explMark.setUseMouseEvents(true);
							%explMark.infoNum = %info.num;
							%explMark.area = %info.area;
							clientExplFx(%explMark);
						}
					}
				}
			}
		}
		toggleAcordos_btn.setStateOn(true); //faz o botão ficar clicado;
		$explMarkersOn = true;
		//echo("Ligando EXPL markers...");
		$explMarkersTimer = schedule(8000, 0, "clientDesligarExplMarkers");
		if($estouNoTutorial){
			tut_verificarObjetivo(false, "acordosPossiveisClick");	
		}
	} else {
		//echo("EXPLmarkersOn == true; ExplMarkers já estavam ligados; ignorando último comando!");	
	}
}

//Parte da lógica de ligar o botão de acordos está na função clientAtualizarEstatisticas
function clientDesligarExplMarkers(){
	cancel($explMarkersTimer);
	for (%i = 0; %i < $mySelf.mySimInfo.getCount(); %i++){
		%info = $mySelf.mySimInfo.getObject(%i);
		if (isObject(%info.myExplMark)){
			%info.myExplMark.safeDelete();
		}
	}
	toggleAcordos_btn.setStateOn(false); //faz o botão ficar desclicado;
	$explMarkersOn = false;
	//echo("Desligando EXPL markers...");
}

function clientToggleExplMarkers(){
	if(!$estouNoTutorial){
		desExpandirPropostas();
		if($explMarkersOn){
			clientDesligarExplMarkers();
		} else {
			clientLigarExplMarkers();	
		}
	} else {
		if($tut_campanha.passo.key $= "acordosPossiveisClick" && $mySelf == $jogadorDaVez){	
			desExpandirPropostas();
			if($explMarkersOn){
				clientDesligarExplMarkers();
			} else {
				clientLigarExplMarkers();	
			}	
		} else {
			toggleAcordos_btn.setStateOn(false); //faz o botão ficar desclicado;
		}
	}
}


function clientAskEnviarAcordoExpl(%infoNum, %doador, %receptor){
	%info = clientFindInfo(%infoNum);
	echo("%info = " @ %info @ ", %infoNum = " @ %infoNum);
	desExpandirPropostas();
	clientDesligarExplMarkers();
	toggleAcordos_btn.setStateOn(false);
		
	if(!$estouNoTutorial){
		if($mySelf.mySimExpl.isMember(%info)){
			commandToServer('embargar', %infoNum, true);
		}
		commandToServer('enviarAcordoExpl', %infoNum, %doador, %receptor);
	} else {
		clientCmdAcordoExpl("doar", %infoNum, "player2");	
	}
}

function clientTogglePagExplHud(){
	clientDesligarMoratoriaMarkers();
	if($vendoPag1ExplHud){
		clientSetPagExplHud(2);
	} else {
		clientSetPagExplHud(1);
	}
}

function clientSetPagExplHud(%num){
	$explHudPagAtual = %num;
	if(%num == 1){
		$vendoPag1ExplHud = true;
		mais_btn.setBitmap("game/data/images/maisBTN");
	} else if (%num == 2){
		$vendoPag1ExplHud = false;
		mais_btn.setBitmap("game/data/images/menosBTN");
	}
	clientAtualizarExplHud();
}

function clientAtualizarExplHud(){
	clientExplGuiClear(); //começa zerando o hud, para o caso de haver moratórias/morte de adversários;
	//começa zerando as coisas:
	%sndBtns = 1;
	%recBtns = 1;
	%recebidas = 0;
	%enviadas = 0;
	
	if($explHudPagAtual == 1){
		for(%i = 0; %i < $mySelf.mySimExpl.getcount(); %i++){
			%info = $mySelf.mySimExpl.getObject(%i); //pega a info
			//estabelece qual é o recurso em jogo:
			if(%info.bonusM > 0){
				%recursoDaMissao = "Min";
			} else if (%info.bonusP > 0){
				%recursoDaMissao = "Pet";
			} else if (%info.bonusU > 0){
				%recursoDaMissao = "Ura";
			}
			
			%estaEhMenor = false;
			if(!$mySelf.mySimInfo.isMember(%Info)){ //se eu doei a missão:
				%enviadas++; //missões enviadas, para saber se preciso de uma segunda página
				if(%enviadas <= 5){
					//pega o botão correto (3 objetos):
					%eval = "%nextBtnFundo = explHudSndBtn" @ %sndBtns @ ";"; //o botão ativo se chama...
					eval(%eval); 
					%eval = "%nextIcon = explHudSndIcon" @ %sndBtns @ ";"; //pega o nome do texto deste botão
					eval(%eval); 
					%eval = "%nextText = explHudSndText" @ %sndBtns @ ";"; //pega o nome do texto deste botão
					eval(%eval); 	
					%eval = "%nextShowDeal = sndBtnShowDeal" @ %sndBtns @ ";"; //pega o btn de showDeal
					eval(%eval); 
											
					%sndBtns++;
					%estaEhMenor = true;
				}
			} else { //se eu recebi a missão:
				%recebidas++; //missões recebidas, para saber se posso decretar moratória e se preciso de uma segunda página
				if(%recebidas <= 5){
					//pega o botão correto (3 objetos):
					%eval = "%nextBtnFundo = explHudRecBtn" @ %recBtns @ ";"; //o botão ativo se chama...
					eval(%eval); 
					%eval = "%nextIcon = explHudRecIcon" @ %recBtns @ ";"; //pega o nome do texto deste botão
					eval(%eval); 
					%eval = "%nextText = explHudRecText" @ %recBtns @ ";"; //pega o nome do texto deste botão
					eval(%eval); 	
					%eval = "%nextShowDeal = recBtnShowDeal" @ %recBtns @ ";"; //pega o btn de showDeal
					eval(%eval); 
						
					%recBtns++;
					%estaEhMenor = true;
				}
			}
			
			if(%estaEhMenor){
				//torna visível:
				%nextBtnFundo.setVisible(true);
				%nextIcon.setVisible(true);
				%nextText.setVisible(true);
				%nextShowDeal.setVisible(true);
				
				//seta a cor do fundo:
				if(%info.minhaVezDeGanhar == false){
					%fundo = "cinza";
				} else {
					%fundo = %info.parceiro.myColor; 
				}
				
				//agora atualiza o botão (são 3 objetos):
				%nextBtnFundo.bitmap = "~/data/images/explHudBtn" @ %fundo @ ".png"; //explHudBtnCinza, por exemplo
				%nextIcon.bitmap = "~/data/images/explHudIcon" @ %recursoDaMissao @ ".png"; //explHudIconMin, por exemplo
				%nextText.text = %info.parceiro.nome;
								
				//registra na info este botão (3 objetos), para que possam ser apagados em caso de moratória:
				%info.myExplBtn = %nextBtnFundo; //guarda na info o nome do botão em que ela foi marcada
				%info.myExplIcon = %nextIcon; //guarda na info o nome do texto do botão em que ela foi marcada
				%info.myExplText = %nextText; //guarda na info o nome do texto do botão em que ela foi marcada
			}
		}
	} else if ($explHudPagAtual == 2){
		for(%i = 0; %i < $mySelf.mySimExpl.getcount(); %i++){
			%info = $mySelf.mySimExpl.getObject(%i); //pega a info
			//estabelece qual é o recurso em jogo:
			if(%info.bonusM > 0){
				%recursoDaMissao = "Min";
			} else if (%info.bonusP > 0){
				%recursoDaMissao = "Pet";
			} else if (%info.bonusU > 0){
				%recursoDaMissao = "Ura";
			}
			
			%estaEhMaior = false;
			if(!$mySelf.mySimInfo.isMember(%Info)){ //se eu doei a missão:
				%enviadas++; //missões enviadas, para saber se preciso de uma segunda página
				if(%enviadas > 5){
					//pega o botão correto (3 objetos):
					%eval = "%nextBtnFundo = explHudSndBtn" @ %sndBtns @ ";"; //o botão ativo se chama...
					eval(%eval); 
					%eval = "%nextIcon = explHudSndIcon" @ %sndBtns @ ";"; //pega o nome do texto deste botão
					eval(%eval); 
					%eval = "%nextText = explHudSndText" @ %sndBtns @ ";"; //pega o nome do texto deste botão
					eval(%eval); 	
					%eval = "%nextShowDeal = sndBtnShowDeal" @ %sndBtns @ ";"; //pega o btn de showDeal
					eval(%eval); 
										
					%sndBtns++;
					%estaEhMaior = true;
				}
			} else { //se eu recebi a missão:
				%recebidas++; //missões recebidas, para saber se posso decretar moratória e se preciso de uma segunda página
				if(%recebidas > 5){
					//pega o botão correto (3 objetos):
					%eval = "%nextBtnFundo = explHudRecBtn" @ %recBtns @ ";"; //o botão ativo se chama...
					eval(%eval); 
					%eval = "%nextIcon = explHudRecIcon" @ %recBtns @ ";"; //pega o nome do texto deste botão
					eval(%eval); 
					%eval = "%nextText = explHudRecText" @ %recBtns @ ";"; //pega o nome do texto deste botão
					eval(%eval); 	
					%eval = "%nextShowDeal = recBtnShowDeal" @ %recBtns @ ";"; //
					eval(%eval); 
						
					%recBtns++;
					%estaEhMaior = true;
				}
			}
			
			if(%estaEhMaior){
				//torna visível:
				%nextBtnFundo.setVisible(true);
				%nextIcon.setVisible(true);
				%nextText.setVisible(true);
				%nextShowDeal.setVisible(true);
				
				//seta a cor do fundo:
				if(%info.minhaVezDeGanhar == false){
					%fundo = "cinza";
				} else {
					%fundo = %info.parceiro.myColor; 
				}
				
				//agora atualiza o botão (são 3 objetos):
				%nextBtnFundo.bitmap = "~/data/images/explHudBtn" @ %fundo @ ".png"; //explHudBtnCinza, por exemplo
				%nextIcon.bitmap = "~/data/images/explHudIcon" @ %recursoDaMissao @ ".png"; //explHudIconMin, por exemplo
				%nextText.text = %info.parceiro.nome;
												
				//registra na info este botão (3 objetos), para que possam ser apagados em caso de moratória:
				%info.myExplBtn = %nextBtnFundo; //guarda na info o nome do botão em que ela foi marcada
				%info.myExplIcon = %nextIcon; //guarda na info o nome do texto do botão em que ela foi marcada
				%info.myExplText = %nextText; //guarda na info o nome do texto do botão em que ela foi marcada
			}
		}
	} else {
		echo("ERRO: $explHudPagAtual != 1 && $explHudPagAtual != 2");
		echo("Não consigo mostrar o explHud: $explHudPagAtual == " @ $explHudPagAtual);	
	} 
		
	embargar_btn.setStateOn(false);	
	if(%recebidas > 0){
		embargar_btn.setActive(true);
	} else {
		embargar_btn.setActive(false);	
	}
	
	//se houver uma seghunda página, o botão de mais/menos fica ativo; do contrário, fica inativo;
	if(%enviadas > 5 || %recebidas > 5){
		mais_btn.setActive(true);	
	} else {
		mais_btn.setActive(false);	
	}
	
	clientCalcularComerciante();
}

function clientCalcularComerciante(){
	if($mySelf.decretouMoratoria){
		comercianteMark_img.setVisible(false);
	} else {
		if($mySelf.trocas > 0 || $mySelf.mySimExpl.getCount() > 0){
			comercianteMark_img.setVisible(true);
		} else {
			comercianteMark_img.setVisible(false);
		}
	}	
}

function clientExplGuiClear(){
	for(%i = 1; %i < 6; %i++){
		%eval = "%nextBtnFundo = explHudSndBtn" @ %i @ ";"; //o botão ativo se chama...
		eval(%eval); 
		%eval = "%nextIcon = explHudSndIcon" @ %i @ ";"; //pega o nome do texto deste botão
		eval(%eval); 
		%eval = "%nextText = explHudSndText" @ %i @ ";"; //pega o nome do texto deste botão
		eval(%eval); 	
		%eval = "%nextShowDeal = sndBtnShowDeal" @ %i @ ";"; //pega o nome do texto deste botão
		eval(%eval); 
				
		//torna invisível:
		if(%nextBtnFundo.visible == true){ //se o botão estiver visível
			%nextBtnFundo.setVisible(false);
			%nextIcon.setVisible(false);
			%nextText.setVisible(false);
			%nextShowDeal.setVisible(false);
		}
	}
	for(%i = 1; %i < 6; %i++){
		%eval = "%nextBtnFundo = explHudRecBtn" @ %i @ ";"; //o botão ativo se chama...
		eval(%eval); 
		%eval = "%nextIcon = explHudRecIcon" @ %i @ ";"; //pega o nome do texto deste botão
		eval(%eval); 
		%eval = "%nextText = explHudRecText" @ %i @ ";"; //pega o nome do texto deste botão
		eval(%eval); 
		%eval = "%nextShowDeal = recBtnShowDeal" @ %i @ ";"; //pega o nome do texto deste botão
		eval(%eval); 
		
		//torna invisível:
		if(%nextBtnFundo.visible == true){ //se o botão estiver visível
			%nextBtnFundo.setVisible(false);
			%nextIcon.setVisible(false);
			%nextText.setVisible(false);
			%nextShowDeal.setVisible(false);
		}
	}
}

function clientShowDeal(%sndOUrec, %btnNum){
	%eval = "%myBtnFundo = explHud" @ %sndOUrec @ "Btn" @ %btnNum @ ";"; //o botão ativo se chama...
	eval(%eval); 
	%eval = "%myIcon = explHud" @ %sndOUrec @ "Icon" @ %btnNum @ ";"; //pega o nome do texto deste botão
	eval(%eval); 
	
	%pagAtual = $explHudPagAtual;
	
	for(%i = 0; %i < $mySelf.mySimExpl.getCount(); %i++){
		%info = $mySelf.mySimExpl.getObject(%i);
		if(%info.myExplBtn !$= %myBtnFundo){
			//echo("NOT FOUND");
		} else {
			if(%pagAtual == 1){
				%myInfo = %info;	
				%i = $mySelf.mySimExpl.getCount();
				clientPiscarShowDeal(%myIcon, %myInfo);
			} else {
				%pagAtual--;
			}
		}
	}
}

$clientPiscandoShowDeal = 0;	
$clientPiscandoShowDealIcon = "";
$clientPiscandoShowDealInfoMark = "";
function clientPiscarShowDeal(%icon, %info){
	if($clientPiscandoShowDealIcon != %icon){
		cancel($piscandoShowDealSchedule);
		$clientPiscandoShowDealIcon.setVisible(true);
		$clientPiscandoShowDealInfoMark.setVisible(true);
		$clientPiscandoShowDealIcon = %icon;
		$clientPiscandoShowDealInfoMark = %info.myExplMark;	
	}
	if($clientPiscandoShowDeal < 4){
		$piscandoShowDealSchedule = schedule(500, 0, "clientPiscarShowDeal", %icon, %info);
		
		if($clientPiscandoShowDealOn){
			%icon.setVisible(true);
			%info.myMark.setVisible(true);
			$clientPiscandoShowDealOn = false;
		} else {
			%icon.setVisible(false);
			%info.myMark.setVisible(false);
			$clientPiscandoShowDealOn = true;
		}
		$clientPiscandoShowDeal++;
	} else {
		$clientPiscandoShowDeal = 0;	
		%icon.setVisible(true);
		%info.myMark.setVisible(true);
		clientAtualizarExplHud();
	}
}




///////função para acordos de exploração:
function clientCmdAcordoExpl(%oQue, %infoNum, %comQuem){
	%info = clientFindInfo(%infoNum);
	%eval = "%comQuem = $" @ %comQuem @ ";"; //vem apenas o id, tem que atualizar
	eval(%eval);
	
	//estabelece qual é o recurso em jogo:
	if(%info.bonusM > 0){
		%recursoDaMissao = "Min";
	} else if (%info.bonusP > 0){
		%recursoDaMissao = "Pet";
	} else if (%info.bonusU > 0){
		%recursoDaMissao = "Ura";
	}
	
	if(isObject(%info.myMark)){
		%info.myMark.setFrame(1); //marca na tela que é um acordo;
	}
	
	if(%oQue $= "doar"){
		//missão deixa de ser minha:
		%info.myMark.setAutoRotation(-15); //passa a girar o marker
		$mySelf.mySimInfo.remove(%info); //deleta a missão do doador
		%info.minhaVezDeGanhar = true; //quem vai ganhar a próxima vez? (quem doa começa ganhando)
		clientDesligarExplMarkers(); //desliga o botão e os markers
		clientPushAcordoEfetuado(%info, %comQuem.nome);
	} else if (%oQue $= "receber"){
		//missão passa a ser minha:
		$mySelf.mySimInfo.add(%info);
		clientMarkNewMission(%infoNum); //marca a missão corretamente
		%info.minhaVezDeGanhar = false; //quem vai ganhar a próxima vez? (quem doa começa ganhando)
		clientAtualizarEstatisticas();	//só deve atualizar as que ganho no meu turno
		clientPushAcordoRecebido(%info, %comQuem.nome); //mostra uma tip de que vc recebeu um acordo.
	}
	
	%info.parceiro = %comQuem; //guarda na info o $player(real, numérico) do parceiro
	$mySelf.mySimExpl.add(%info); //adiciona a missão no simObj responsável pelos acordos de exploração
				
	clientAtualizarExplHud(); //o sistema verifica todos os acordos e reorganiza de forma correta;
		
	echo("ACORDO DE EXPLORAÇÃO COM" SPC %comQuem.nome @ "(" @ %comQuem.id @ "):" SPC %info.area);
	echo("Minérios:" SPC %info.bonusM);
	echo("Petroleos:" SPC %info.bonusP);
	echo("Uranios:" SPC %info.bonusU);
	clientAtualizarEstatisticas();
}
	
//função para receber um pagamento de Exploração:
function clientCmdReceberPagamentoExpl(%minerio, %petroleo, %uranio){
	$mySelf.minerios += %minerio;
	$mySelf.petroleos += %petroleo;
	$mySelf.uranios += %uranio;
	
	clientToggleRecursosBtns();
	atualizarRecursosGui();
	clientAtualizarEstatisticas();
}

//função para alternar quem ganha o recurso de um acordo de Exploração:
function clientCmdToggleVezDeGanhar(%infoNum){
	%info = clientFindInfo(%infoNum);
	
	if(%info.minhaVezDeGanhar == true){
		%info.minhaVezDeGanhar = false;
	} else {
		%info.minhaVezDeGanhar = true;
	}
	clientAtualizarExplHud(); //o sistema verifica todos os acordos e reorganiza de forma correta;
}

///////cancelando todos os acordos de exploração com um jogador morto:
function clientCancelarAcordosComMorto(%nomeDoMorto){
	%mySimInfo = $mySelf.mySimInfo;
	%mySelfExpl = $mySelf.mySimExpl;
	
	%numDeAcordos = %mySelfExpl.getCount();
	
	//criar um SimSetEspelho temporário:
	if(!isObject($simExplEspelhoTemporario)){
		$simExplEspelhoTemporario = new SimSet(simExplEspelhoTemporario);
	} else {
		$simExplEspelhoTemporario.clear(); //zera o simSet temporário	
	}
	
	//copia o simSet para o simExpl temporário:
	for(%i = 0; %i < %numDeAcordos; %i++){
		%info = %mySelfExpl.getObject(%i); //pega a info
		$simExplEspelhoTemporario.add(%info);
	}
	for(%i = 0; %i < %numDeAcordos; %i++){
		%info = $simExplEspelhoTemporario.getObject(%i); //pega a info
		if(%info.parceiro.nome $= %nomeDoMorto){
			%mySelfExpl.remove(%info);
			//if(!%mySimInfo.isMember(%info)){
				%info.myMark.safeDelete(); //deleta a carta do mapa, ela volta pelo comando do server de roubar cartas
			//}
		}	
	}
	
	//agora cancelar propostas:
	%numeroDePropostas = $propostasPool.getcount();
	for(%i = 0; %i < %numeroDePropostas; %i++){
		%proposta[%i] = $propostasPool.getObject(%i);
	}
	for(%i = 0; %i < %numeroDePropostas; %i++){
		%eval = "%parceiro = $" @ %proposta[%i].parceiroId @ ";";
		eval(%eval);
		if(%parceiro.nome $= %nomeDoMorto){
			commandToServer('cancelarTroca', %proposta[%i].numDaInfo1, %proposta[%i].numDaInfo2, %proposta[%i].parceiroId);
		}
	}
	
	//atualiza o propostasGui:
	clientAtualizarPropostasGui();
	
	//ajusta os ícones de missões conquistadas, compartilhadas e por conquistar:
	clientAtualizarEstatisticas(); 
	
	//atualiza o hud de exploração:
	clientAtualizarExplHud();
}

//para deixar cooridos os explMArkers roubados:
function clientAtualizarExplRoubados(){
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	%eval = "%mySelfExpl = $" @ %mySelf.id @ "SimExpl;";
	eval(%eval);
	%eval = "%mySelfSimInfo = $" @ %mySelf.id @ "SimInfo;";
	eval(%eval);
	
	%numDeCartas = %mySelfSimInfo.getCount();
	
	for(%i = 0; %i < %numDeCartas; %i++){
		%info = %mySelfSimInfo.getObject(%i);
		
		if(!%info.bonusPt > 0){
			if(!%mySelfExpl.isMember(%info)){
				%info.myMark.setFrame(2);
			} else {
				%info.myMark.setFrame(1);	
			}
		}
	}
}
////////////////////////////////////////////////////////////////



//
//DOAÇÃO:
//quando o cara clica no botão de doação, aparecem no HUD central duas opções: Título: DOAR; opção 1: imperiais/recursos; opção 2: Informação;
//se o cara clicar em Informação é que ele chama a seguinte função:
//-> MsgGui pergunta [R: informação] -> (clientToggleDoarMarkers)
function clientToggleDoarMarkers(){
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	%mySelfInfo = $mySelf.mySimInfo;
	
	if($doarMarkersOn){
		for (%i = 0; %i < %mySelfInfo.getCount(); %i++){
			%info = %mySelfInfo.getObject(%i);
			if (isObject(%info.myDoarMark)){
				%info.myDoarMark.safeDelete();
			}
		}
		$doarMarkersOn = false;
	} else {
		if($explMarkersOn){
			clientToggleExplMarkers();
		}
		if($moratoriaMarkersOn){
			clientToggleEmbargos();
		}
			
		
		for (%i = 0; %i < %mySelfInfo.getCount(); %i++){
			%info = %mySelfInfo.getObject(%i);
			if (%info.bonusM > 0){
				%eval = "%areaDaMissao =" SPC %info.area @ ";";
				eval(%eval);
				if(%areaDaMissao.dono.id !$= %mySelf.id && %areaDaMissao.dono !$= "0"){
					%doarMark = explMarkerMinerio.createCopy();	
					%doarMark.setPosition(%areaDaMissao.pos3);
					%info.myDoarMark = %doarMark;
					%doarMark.setUseMouseEvents(true);
					%doarMark.infoNum = %info.num;
					%doarMark.area = %info.area;
				}
			} else if (%info.bonusP > 0){
				%eval = "%areaDaMissao =" SPC %info.area @ ";";
				eval(%eval);
				if(%areaDaMissao.dono.id !$= %mySelf.id && %areaDaMissao.dono !$= "0"){
					%doarMark = explMarkerPetroleo.createCopy();	
					%doarMark.setPosition(%areaDaMissao.pos2);
					//%doarMark.setBlendColor(1, 1, 1, 0.75);
					//%doarMark.setAutoRotation(-15);
					%info.myDoarMark = %doarMark;
					%doarMark.setUseMouseEvents(true);
					%doarMark.infoNum = %info.num;
					%doarMark.area = %info.area;
				}
			} else if (%info.bonusU > 0){
				%eval = "%areaDaMissao =" SPC %info.area @ ";";
				eval(%eval);
				if(%areaDaMissao.dono.id !$= %mySelf.id && %areaDaMissao.dono !$= "0"){
					%doarMark = explMarkerUranio.createCopy();	
					%doarMark.setPosition(%areaDaMissao.pos1);
					//%doarMark.setBlendColor(1, 1, 1, 0.75);
					//%doarMark.setAutoRotation(-15);
					%info.myDoarMark = %doarMark;
					%doarMark.setUseMouseEvents(true);
					%doarMark.infoNum = %info.num;
					%doarMark.area = %info.area;
				}
			} else if( %info.bonusPt > 0){
				%eval = "%areaDaMissao =" SPC %info.area @ ";";
				eval(%eval);
				if(%areaDaMissao.dono.id !$= %mySelf.id && %areaDaMissao.dono !$= "0"){
					%doarMark = explMarkerPontos.createCopy();	
					%doarMark.setPosition(%areaDaMissao.pos0);
					%info.myDoarMark = %doarMark;
					%doarMark.setUseMouseEvents(true);
					%doarMark.infoNum = %info.num;
					%doarMark.area = %info.area;
				}
			}
		}
		$doarMarkersOn = true;
		clientDoarQuestion();
		clientDoarOuCancelarQuestion(); //deixa a opção do cara cancela a doação, se perceber na tela que não há boas opções.
	}		
}

function clientAskDoarMissao(%infoNum, %doador, %receptor){
	if($mySelf.mySimExpl.isMember(%info)){
		echo("INFO JÁ ESTAVA COMPROMETIDA; DECLARANDO MORATÓRIA PARA O ANTIGO PARCEIRO");
		commandToServer('embargar', %infoNum);
	}
	
	commandToServer('doarMissao', %infoNum, %doador, %receptor);
	clientToggleDoarMarkers();
}

function clientDoarInfoCancelar(){
	clientToggleDoarMarkers();
	msgGui.setVisible(false);
}

function clientCmdDoarMissao(%infoNum, %paraQuem){
	%info = clientFindInfo(%infoNum);
	%info.myMark.safeDelete();
	$mySelf.mySimInfo.remove(%info);
	
	clientMsg("doacaoEfetuada", 4000);
	clientAtualizarEstatisticas();
}

function clientCmdReceberInfoDoada(%infoNum, %quemDoou){
	%info = clientFindInfo(%infoNum);
	$mySelf.mySimInfo.add(%info);
	if(isObject(%info.myMark)){
		%info.myMark.safeDelete();
	}
	clientMarkNewMission(%infoNum); //marca a missão corretamente
	clientAtualizarEstatisticas();	
	
	clientMsgInfoDoadaRecebida(%quemDoou);
	%doacaoRecebidaFX = new t2dParticleEffect() { scenegraph = $strategyScene; };
	%doacaoRecebidaFX.loadEffect("~/data/particles/doacaoRecebidaFX.eff");
	%doacaoRecebidaFX.setPosition(%info.area.pos0);
	%doacaoRecebidaFX.setEffectLifeMode("KILL", 1);
	%doacaoRecebidaFX.playEffect();
}


function clientCmdRoubarInfo(%infoNum){
	%info = clientFindInfo(%infoNum);
	$mySelf.mySimInfo.add(%info);
	
	clientMarkNewMission(%infoNum); //marca a missão corretamente
}

//DoarGui:
function callDoarGui(%praQuem){
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	%eval = "%praQuem =" SPC %praQuem @ ";";
	eval(%eval);
	
	%nomeDoReceptor = %praQuem.nome;
	$atualReceptor = %praQuem;
	
	canvas.pushDialog(doarGui);
	doarGuiNomeTxt.text = %nomeDoReceptor;
	
	doarGuiImperiaisTxt.text = "0";
	doarGuiMineriosTxt.text = "0";
	doarGuiPetroleosTxt.text = "0";
	doarGuiUraniosTxt.text = "0";
	
	//apaga as flechas pra esquerda, já que estão todas no zero;
	doarGuiImperiaisArrowLeftBtn.setActive(false);	
	doarGuiMineriosArrowLeftBtn.setActive(false);	
	doarGuiPetroleosArrowLeftBtn.setActive(false);	
	doarGuiUraniosArrowLeftBtn.setActive(false);	
	
	if(%mySelf.imperiais < 1){
		doarGuiImperiaisArrowRightBtn.setActive(false);	
	} else {
		doarGuiImperiaisArrowRightBtn.setActive(true);	
	}
	if(%mySelf.minerios < 1){
		doarGuiMineriosArrowRightBtn.setActive(false);	
	} else {
		doarGuiMineriosArrowRightBtn.setActive(true);	
	}
	if(%mySelf.petroleos < 1){
		doarGuiPetroleosArrowRightBtn.setActive(false);	
	} else {
		doarGuiPetroleosArrowRightBtn.setActive(true);	
	}
	if(%mySelf.uranios < 1){
		doarGuiUraniosArrowRightBtn.setActive(false);	
	} else {
		doarGuiUraniosArrowRightBtn.setActive(true);	
	}
}

function clientDoarGuiCancelar(){
	switch$($atualReceptor.id){
		case "player1":
		p1DoarBtn.performClick();
			
		case "player2":
		p2DoarBtn.performClick();
		
		case "player3":
		p3DoarBtn.performClick();
		
		case "player4":
		p4DoarBtn.performClick();
		
		case "player5":
		p5DoarBtn.performClick();
		
		case "player6":
		p6DoarBtn.performClick();
	}
	Canvas.popDialog(doarGui);
}

function doarGuiDoar(){
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	//pega os números do doarGui:
	%imperiais = doarGuiImperiaisTxt.text;
	%minerios = doarGuiMineriosTxt.text;
	%petroleos = doarGuiPetroleosTxt.text;
	%uranios = doarGuiUraniosTxt.text;
	%meuId = %mySelf.id;
	
	if(%imperiais > 0 || %minerios > 0 || %petroleos > 0 || %uranios > 0){
		commandToServer('doarRenda', %imperiais, %minerios, %petroleos, %uranios, %meuId, $atualReceptor.id);
		switch$($atualReceptor.id){
			case "player1":
			p1DoarBtn.performClick();
			
			case "player2":
			p2DoarBtn.performClick();
			
			case "player3":
			p3DoarBtn.performClick();
			
			case "player4":
			p4DoarBtn.performClick();
			
			case "player5":
			p5DoarBtn.performClick();
			
			case "player6":
			p6DoarBtn.performClick();
		}
		Canvas.popDialog(doarGui);
	}
}

function doarGuiSetaRight(%deQue){
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	//pega os números do doarGui:
	%imperiais = doarGuiImperiaisTxt.text;
	%minerios = doarGuiMineriosTxt.text;
	%petroleos = doarGuiPetroleosTxt.text;
	%uranios = doarGuiUraniosTxt.text;
	
	switch$ (%deQue){
		case "imperiais":
		doarGuiImperiaisTxt.text = %imperiais + 1;
		doarGuiImperiaisArrowLeftBtn.setActive(true);
		if(%imperiais + 2 > %mySelf.imperiais){
			doarGuiImperiaisArrowRightBtn.setActive(false);	
		}
			
		case "minerios":
		doarGuiMineriosTxt.text = %minerios + 1;
		doarGuiMineriosArrowLeftBtn.setActive(true);
		if(%minerios + 2 > %mySelf.minerios){
			doarGuiMineriosArrowRightBtn.setActive(false);	
		}
		
		case "petroleos":
		doarGuiPetroleosTxt.text = %petroleos +1;
		doarGuiPetroleosArrowLeftBtn.setActive(true);
		if(%petroleos + 2 > %mySelf.petroleos){
			doarGuiPetroleosArrowRightBtn.setActive(false);	
		}
		
		case "uranios":
		doarGuiUraniosTxt.text = %uranios + 1;
		doarGuiUraniosArrowLeftBtn.setActive(true);
		if(%uranios + 2 > %mySelf.uranios){
			doarGuiUraniosArrowRightBtn.setActive(false);	
		}
	}
}

function doarGuiSetaLeft(%deQue){
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	//pega os números do doarGui:
	%imperiais = doarGuiImperiaisTxt.text;
	%minerios = doarGuiMineriosTxt.text;
	%petroleos = doarGuiPetroleosTxt.text;
	%uranios = doarGuiUraniosTxt.text;
	
	switch$ (%deQue){
		case "imperiais":
		doarGuiImperiaisTxt.text = %imperiais - 1;
		doarGuiImperiaisArrowRightBtn.setActive(true);
		if(%imperiais - 2 < 0){
			doarGuiImperiaisArrowLeftBtn.setActive(false);	
		}
			
		case "minerios":
		doarGuiMineriosTxt.text = %minerios - 1;
		doarGuiMineriosArrowRightBtn.setActive(true);
		if(%minerios - 2 < 0){
			doarGuiMineriosArrowLeftBtn.setActive(false);	
		}
		
		case "petroleos":
		doarGuiPetroleosTxt.text = %petroleos - 1;
		doarGuiPetroleosArrowRightBtn.setActive(true);
		if(%petroleos - 2 < 0){
			doarGuiPetroleosArrowLeftBtn.setActive(false);	
		}
		
		case "uranios":
		doarGuiUraniosTxt.text = %uranios - 1;
		doarGuiUraniosArrowRightBtn.setActive(true);
		if(%uranios - 2 < 0){
			doarGuiUraniosArrowLeftBtn.setActive(false);	
		}
	}
}
///////////////////////////////////////


///////////////////////////////////////////
//Moratória:
function clientZerarMoratoriaMarks(){
	explHudMoratoria1.setVisible(false);
	explHudMoratoria2.setVisible(false);
	explHudMoratoria3.setVisible(false);
	explHudMoratoria4.setVisible(false);
	explHudMoratoria5.setVisible(false);
}

function clientLigarMoratoriaMarkers(){
	if($moratoriaMarkersOn == false){
		clientApagarChat(); //apaga o chat pro truta ver os acordos
		clientDesligarInvestirRecursos(); //apaga o hud de investir recursos
		clientFecharIntelGui(); //apaga o gui de intel
		if($explMarkersOn){
			clientToggleExplMarkers();
			toggleAcordos_btn.performClick();
		}
		if($doarMarkersOn){
			clientToggleDoarMarkers();
		}
	
		%mySelfSimInfo = $mySelf.mySimInfo;
		%mySelfExpl = $mySelf.mySimExpl;
		%btns = 1;
		
		%embargosPossiveis = 0;
		if($explHudPagAtual == 1){
			for(%i = 0; %i < %mySelfExpl.getCount(); %i++){
				%info = %mySelfExpl.getObject(%i); //pega a info	
				if(%mySelfSimInfo.isMember(%info)){ //se a missão está comigo, eu posso decretar moratória
					%embargosPossiveis++;
					if(%embargosPossiveis < 6){
						%eval = "%nextBtnMoratoria = explHudMoratoria" @ %btns @ ";"; //o botão ativo se chama...
						eval(%eval); 
						%eval = "%nextText = explHudText" @ %btns @ ";"; //pega o nome do texto deste botão
						eval(%eval); 
						%nextBtnMoratoria.setVisible(true);
						%nextBtnMoratoria.infoNum = %info.num;
												
						//registra na info este botão (2 objetos), para que possam ser apagados em caso de moratória:
						%info.myExplText = %nextText; //guarda na info o nome do texto do botão em que ela foi marcada
						%info.myMoratoriaBtn = %nextBtnMoratoria;
						%btns++;
					}
				}
			}
		} else {
			for(%i = 0; %i < %mySelfExpl.getCount(); %i++){
				%info = %mySelfExpl.getObject(%i); //pega a info	
				if(%mySelfSimInfo.isMember(%info)){ //se a missão está comigo, eu posso decretar moratória
					%embargosPossiveis++;
					if(%embargosPossiveis > 5){
						%eval = "%nextBtnMoratoria = explHudMoratoria" @ %btns @ ";"; //o botão ativo se chama...
						eval(%eval); 
						%eval = "%nextText = explHudText" @ %btns @ ";"; //pega o nome do texto deste botão
						eval(%eval); 
						%nextBtnMoratoria.setVisible(true);
						%nextBtnMoratoria.infoNum = %info.num;
												
						//registra na info este botão (2 objetos), para que possam ser apagados em caso de moratória:
						%info.myExplText = %nextText; //guarda na info o nome do texto do botão em que ela foi marcada
						%info.myMoratoriaBtn = %nextBtnMoratoria;
						%btns++;
					}
				}
			}
		}
		embargar_btn.setStateOn(true); //faz o botão ficar clicado;
		$moratoriaMarkersOn = true;
	}  else {
		echo("MORATORIAmarkersOn == true; MORATORIAMarkers já estavam ligados; ignorando último comando!");	
	}
}

function clientDesligarMoratoriaMarkers(){
	if($moratoriaMarkersOn){
		clientZerarMoratoriaMarks();
		embargar_btn.setStateOn(false); //faz o botão ficar desclicado;
		$moratoriaMarkersOn = false;	
	}  else {
		echo("MORATORIAmarkersOn == false; MORATORIAMarkers já estavam desligados; ignorando último comando!");	
	}
}

function clientToggleEmbargos(){
	if($moratoriaMarkersOn){
		clientDesligarMoratoriaMarkers();
	} else {
		clientLigarMoratoriaMarkers();
	}
}

function clientAskEmbargar(%btn){
	%eval = "%btnMoratoria = explHudMoratoria" @ %btn @ ";"; //o botão ativo se chama...
	eval(%eval);
	%infoNum = %btnMoratoria.infoNum;
	
	commandToServer('embargar', %infoNum, false);
	clientToggleEmbargos();
}

function clientCmdMoratoria(%infoNum, %decretarOuSofrer, %silent){
	%info = clientFindInfo(%infoNum);
	%nomeDoParceiro = %info.parceiro.nome;
	$mySelf.mySimExpl.remove(%info);
	%infoMark = %info.myMark;
	
	if(!%silent){
		clientMsgEmbargo(%nomeDoParceiro);
		%moratoriaFX = new t2dParticleEffect() { scenegraph = $strategyScene; };
		%moratoriaFX.loadEffect("~/data/particles/moratoriaFX.eff");
		%moratoriaFX.setPosition(%info.area.pos1);
		%moratoriaFX.setEffectLifeMode("KILL", 1);
		%moratoriaFX.playEffect();
	}
	
	if(%decretarOuSofrer $= "Decretar"){
		%info.myMark.setFrame(2);//marca como fullyActive
		clientDesligarMoratoriaMarkers();
		if(!%silent){
			clientPiscarInfoMark(%infoMark);
			comercianteMark_img.setVisible(false);	//tira o comercianteMark de quem decretou a moratoria
			$mySelf.decretouMoratoria = true;
		}
	} else if (%decretarOuSofrer $= "Sofrer"){
		if(!%silent){
			clientPiscarExplBtn(%infoNum);
			clientPiscarEApagarInfoMark(%infoMark);
			clientPushMoratoria(%info, %nomeDoParceiro);
		} else {
			//simplesmente apagar o info.myMark
			%info.myMark.safeDelete();
		}
	}
		
	clientSetPagExplHud(1); //sempre que decreta ou sofre moratória volta pra página 1 dos acordosExpl, pra evitar que o cara fique preso na página 2;
	clientAtualizarExplHud();
	clientAtualizarEstatisticas();
}

function clientPiscarExplBtn(%infoNum){
	%info = clientFindInfo(%infoNum);
	%explBtn = %info.myExplBtn;
	%explIcon = %info.myExplIcon;
	%explText = %info.myExplText;
	
	clientPiscarExec(%explBtn, %explIcon, %explText);
}

$piscando = 0;	
function clientPiscarExec(%explBtn, %explIcon, %explText){
	if($piscando < 10){
		$piscarExplBtnSchedule = schedule(500, 0, "clientPiscarExec", %explBtn, %explIcon, %explText);
		
		if($piscarOn){
			%explBtn.setVisible(false);
			%explIcon.setVisible(false);
			%explText.setVisible(false);			
			$piscarOn = false;
		} else {
			%explBtn.setVisible(true);
			%explIcon.setVisible(true);
			%explText.setVisible(true);	
			$piscarOn = true;
		}
		$piscando++;
	} else {
		$piscando = 0;		
		clientAtualizarExplHud();
	}
}

$infoMarkPiscando = 0;	
function clientPiscarInfoMark(%infoMark){
	if($piscandoMark < 10){
		$piscarInfoMarkSchedule = schedule(500, 0, "clientPiscarInfoMark", %infoMark);
		
		if($piscarMarkOn){
			%infoMark.setVisible(false);			
			$piscarMarkOn = false;
		} else {
			%infoMark.setVisible(true);	
			$piscarMarkOn = true;
		}
		$piscandoMark++;
	} else {
		$piscandoMark = 0;	
		%infoMark.setVisible(true);	
	}
}

$piscEApagar = 0;
function clientPiscarEApagarInfoMark(%infoMark){
	if($piscEApagar < 10){
		$piscarEApagarSchedule = schedule(500, 0, "clientPiscarEApagarInfoMark", %infoMark);
		
		if($piscarEApagarOn){
			%infoMark.setVisible(false);			
			$piscarEApagarOn = false;
		} else {
			%infoMark.setVisible(true);	
			$piscarEApagarOn = true;
		}
		$piscEApagar++;
	} else {
		$piscEApagar = 0;	
		%infoMark.setVisible(false);	
		%infoMark.safeDelete();	
	}
}
///////////////////////////

////////////////////////////////////
//Ping:
$pingDelay = false; //zera a variável;
function setPingDelay(){
	$pingDelay = true;
	
	schedule(2000, 0, "zerarPingDelay"); //delay de 2seg
}

function zerarPingDelay(){
	$pingDelay = false;
}

function zerarPingMark(%qualMark){
	%qualMark.setPosition("80 80");	
	%qualMark.inUse = false;
}

function clientClearPingBtnOn(){
	for(%i = 1; %i < 7; %i++){
		%eval = "p" @ %i @ "PingBtn.setStateOn(false);";
		eval(%eval);	
	}
}

function pingBtnClick(%donoId, %nomeDoBtn){
	if($pingTo $= "nada"){
		$pingTo = %donoId;
		$lastPingBtn = %nomeDoBtn;
	} else {
		if($pingTo $= %donoId){
			$pingTo = "nada";
			$lastPingBtn = "nada";
		} else {
			clientClearPingBtnOn();
			%eval = %nomeDoBtn @ ".setStateOn(true);";
			eval(%eval);
			$pingTo = %donoId;
			$lastPingBtn = %nomeDoBtn;
		}
	}
}

function clientCmdPing(%enviarOuReceber, %onde, %interlocutorId){
	%eval = "%onde =" SPC %onde @ ";";
	eval(%eval);
	
		
	if(%enviarOuReceber $= "pingar"){
		%playerPingante = $mySelf;
	} else if (%enviarOuReceber $= "receberPing"){
		%eval = "%playerPingante = $" @ %interlocutorId @ ";";
		eval(%eval);
	}
	
	%pingCorreto = clientGetPingCorreto();
	
	clientSetAlvoXColor(%pingCorreto, %playerPingante.corR, %playerPingante.corG, %playerPingante.corB, 1);
	%pingCorreto.setPosition(%onde.pos1);
	%pingCorreto.setAnimationFrame(6);
	%pingCorreto.inUse = true;
	if(!$noSound){
		alxPlay( Ping );
	}
	
	schedule(4000, 0, "zerarPingMark", %pingCorreto);
	echo("PING de " @ %interlocutorId);
}

function clientGetPingCorreto()
{
	if(!$alvo1.inUse)
		return $alvo1;	
	
	if(!$alvo2.inUse)
		return $alvo2;	
		
	if(!$alvo3.inUse)
		return $alvo3;	
		
	if(!$alvo4.inUse)
		return $alvo4;	
}


function clientSetAlvoXColor(%alvo, %red, %green, %blue, %alpha){
	%alvo.setBlendColor(%red, %green, %blue, %alpha);
}

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////

function initMissionMarksPool(){
	if(!isObject($missionMarksPool)){
		$missionMarksPool = new SimSet();
	} else {
		$missionMarksPool.clear();	
	}
}

function clientToggleVisualizarMarkers(){
	if($ocultandoMissionMarks){
		mostrarMissionMarks();	
		$ocultandoMissionMarks = false;
	} else {
		ocultarMissionMarks();
		$ocultandoMissionMarks = true;
	}
}

function mostrarMissionMarks(){
	for(%i = 0; %i < $missionMarksPool.getcount(); %i++){
		%missionMark = $missionMarksPool.getObject(%i);
		%missionMark.setVisible(true);
	}
	//fazer o botão ficar normal:
	mainGuiVisualizarMarkers_btn.setStateOn(false);
}

function ocultarMissionMarks(){
	for(%i = 0; %i < $missionMarksPool.getcount(); %i++){
		%missionMark = $missionMarksPool.getObject(%i);
		%missionMark.setVisible(false);
	}
	//fazer o botão ficar apertado;
	mainGuiVisualizarMarkers_btn.setStateOn(true);
}

/////////////////////////////

function clientVerifyExplConquest(){
	for(%i = 0; %i < $mySelf.mySimExpl.getCount(); %i++){
		%info = $mySelf.mySimExpl.getObject(%i);
		if(%info.minhaVezDeGanhar == false){
			%fundo = "cinza";
		} else {
			%fundo = %info.parceiro.myColor;
		}
		
		if($mySelf.mySimInfo.isMember(%info)){ //se a carta estiver comigo
			if(%info.area.dono != $mySelf){
				if(%info.area.dono $= "COMPARTILHADA" && (%info.area.dono1 == $mySelf || %info.area.dono2 == $mySelf)){
					%info.myExplBtn.bitmap = "~/data/images/explHudBtn" @ %fundo @ ".png"; //explHudBtnCinza, por exemplo
				} else {
					%info.myExplBtn.bitmap = "~/data/images/explHudBtnOFF";
				}
			} else {
				%info.myExplBtn.bitmap = "~/data/images/explHudBtn" @ %fundo @ ".png"; //explHudBtnCinza, por exemplo
			}
		} else { //se a carta está no poder do parceiro:
			if(%info.area.dono != %info.parceiro){
				if(%info.area.dono $= "COMPARTILHADA" && (%info.area.dono1 == %info.parceiro || %info.area.dono2 == %info.parceiro)){
					%info.myExplBtn.bitmap = "~/data/images/explHudBtn" @ %fundo @ ".png"; //explHudBtnCinza, por exemplo
				} else{
					%info.myExplBtn.bitmap = "~/data/images/explHudBtnOFF";
				}
			} else {
				%info.myExplBtn.bitmap = "~/data/images/explHudBtn" @ %fundo @ ".png"; //explHudBtnCinza, por exemplo
			}
		}
	}
}


///Marcar acordos recebidos de um modo mais visível:
function clientPushAcordoRecebido(%info, %deQuemNome){
	cancel($acordoTipSchedule);
	tut_clearTips();
	if($tutMod_arrowSoldadoX $= ""){
		tut_pegarResolucao();	
	}
	
	%area = %info.area;
	if(%info.bonusM > 0){
		%recurso = "minerio";
		%recursoNome = "minério";
	} else if(%info.bonusP > 0){
		%recurso = "petroleo";
		%recursoNome = "petróleo";
	} else if(%info.bonusU > 0){
		%recurso = "uranio";
		%recursoNome = "urânio";
	}
	%img1 = "tut" @ %recurso @ "Icon";
	%img2 = "tut" @ %recurso @ "Icon";
	tut_arrowOn(%area, "ACORDO RECEBIDO", "Acordo de exploração", "recebido de " @ %deQuemNome @ ".", "De duas em duas rodadas", "você receberá +1 " @ %recursonome @ ".", "", "", "", %img1, %img2, false, true); //os últimos dois param são: inicial, closeBtn
	$acordoTipSchedule = schedule(6500, 0, "tut_clearTips");
}

///Marcar moratórias de um modo mais visível:
function clientPushMoratoria(%info, %deQuemNome){
	cancel($acordoTipSchedule);
	tut_clearTips();
	if($tutMod_arrowSoldadoX $= ""){
		tut_pegarResolucao();	
	}
	
	%area = %info.area;
	if(%info.bonusM > 0){
		%recurso = "minerio";
		%recursoNome = "minério";
	} else if(%info.bonusP > 0){
		%recurso = "petroleo";
		%recursoNome = "petróleo";
	} else if(%info.bonusU > 0){
		%recurso = "uranio";
		%recursoNome = "urânio";
	}
	%img1 = "tut" @ %recurso @ "Icon";
	%img2 = "tut" @ %recurso @ "Icon";
	tut_arrowOn(%area, "MORATÓRIA", %deQuemNome @ " diz: Este " @ %recursoNome, "pertence ao nosso povo!", "Não podemos aceitar que você", "explore nosso território.", "", "", "", %img1, %img2, false, true); //os últimos dois param são: inicial, closeBtn
	$acordoTipSchedule = schedule(6500, 0, "tut_clearTips");
}

///Marcar moratórias de um modo mais visível:
function clientPushAcordoEfetuado(%info, %deQuemNome){
	if(!$estouNoTutorial){
		cancel($acordoTipSchedule);
		tut_clearTips();
		if($tutMod_arrowSoldadoX $= ""){
			tut_pegarResolucao();	
		}
		
		%area = %info.area;
		if(%info.bonusM > 0){
			%recurso = "minerio";
			%recursoNome = "minério";
		} else if(%info.bonusP > 0){
			%recurso = "petroleo";
			%recursoNome = "petróleo";
		} else if(%info.bonusU > 0){
			%recurso = "uranio";
			%recursoNome = "urânio";
		}
		%img1 = "tut" @ %recurso @ "Icon";
		%img2 = "tut" @ %recurso @ "Icon";
		tut_arrowOn(%area, "ACORDO EFETUADO", "Acordo de exploração", "efetuado com " @ %deQuemNome @ ".", "De duas em duas rodadas", "você receberá +1 " @ %recursonome @ ".", "", "", "", %img1, %img2, false, true); //os últimos dois param são: inicial, closeBtn
		$acordoTipSchedule = schedule(6500, 0, "tut_clearTips");
	}
}