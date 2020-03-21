$pingTo = "nada";
function sceneWindow2D::onMouseDown( %this, %mod, %worldPos, %mouseClicks ){
	$lastFocus = foco.getObject(0); //pra usar no lançamento do vírus
	resetSelection(); //função do arquivo mainGui.cs que reseta todas as selectionMarks e HUDs
	//lista todos os objetos clicados neste ponto na strategyScene
    %objList = $strategyScene.pickPoint(%worldPos);
    //conta quantos tem
    %objCount = getWordCount(%objList);
	
	%eval = "%jogadorDaVez =" SPC $jogadorDaVez @ ";";
	eval(%eval);
	
	//////////////////////////////////
	if($explMarkersOn){ //se eu estiver querendo negociar:
		for(%i=0;%i<%objCount;%i++){
			//verificando o objeto de acordo com o índice
        	%marker = getWord(%objList, %i);
		
			//se encontrar um objeto que seja da classe Area
        	if(%marker.class $= "explMarker"){
				//marcamos que um marker foi encontrado
        	    %markFound = true;
        	    //e saímos do loop
        	    %i = %objCount;
			}
		}
		if(%markFound){
			%markedInfo = clientFindInfo(%marker.infoNum);
							
			if(%markedInfo.area.dono !$= "0" && %markedInfo.area.dono !$= "MISTA"){
				if(%markedInfo.area.dono $= "COMPARTILHADA"){
					%receptor = %markedInfo.area.dono1;	
				} else {
					%receptor = %markedInfo.area.dono;	
				}
				if(%marker.tipo $= "pt"){
					if($shiftOn){ //se o cara está segurando o LSHIFT
						clientAskProporTroca(%marker.infoNum, %receptor.id, true); 
					} else {
						clientAskProporTroca(%marker.infoNum, %receptor.id); 
						if($estouNoTutorial){
							tut_verificarObjetivo(false, "enviarExplClick");	
						}
					}
				} else {
					clientAskEnviarAcordoExpl(%marker.infoNum, $mySelf.id, %receptor.id); //acordo de exploração
					if($estouNoTutorial){
						tut_verificarObjetivo(false, "enviarExplClick");	
					}
				}
			} else {
				echo("Esta área ainda é neutra - não dá pra negociar");
			}
		}
	} else if ($pingTo !$= "nada"){
		for(%i=0;%i<%objCount;%i++){
			//verificando o objeto de acordo com o índice
        	%area = getWord(%objList, %i);
		
			//se encontrar um objeto que seja da classe Area
        	if(%area.class $= "Area"){
				//marcamos que uma area foi encontrada
        	    %areaEncontrada = true;
        	    //e saímos do loop
        	    %i = %objCount;
			}
		}
		if(%areaEncontrada){
			%meuId = $mySelf.id;
			%onde = %area.getName();
			commandToServer('sendPing', %meuId, $pingTo, %onde); 
			$lastPingBtn.performClick();
		}
	}
	/////////////////////////////
    
	//ir de um em um até o final da lista
    for(%i = 0; %i < %objCount; %i++){
		//verificando o objeto de acordo com o índice
        %obj = getWord(%objList, %i);
			 
        if(%obj.isSelectable){
			//echo("obj Selectable!");
			if(%obj.dono == $mySelf || ((%obj.class $= "reliquia" || %obj.class $= "artefato") && %obj.onde.dono == $mySelf)){ //se a peça for minha, eu posso selecionar.
				%selectedObj = getWord(%objList, %i);
				%selected = true;
				if(%obj.pos $= "pos3" || %obj.pos $= "pos4"){
					if(isObject(%obj.myTransporte)){
						if(%obj.myTransporte.getCount() > 0){
							%i = %objCount;
						}
					}
				} else {
					%i = %objCount;
				}
			} else {
				//echo("obj Selectable MAS NÃO MEU!");
				//se a peça não for minha, verifica se é uma base da minha dupla
				if($jogoEmDuplas){
					if(%obj.dono == $mySelf.myDupla && (%obj.class $= "Base" || %obj.class $= "Rainha")){ 
						%selectedObj = getWord(%objList, %i);
						%selected = true;
						%i = %objCount;	
					}
				}
			}
		} else {
			//echo("obj NOT Selectable!");	
		}
	}
	//se um objeto selecionável foi encontrado
    if(%selected){
		if (%selectedObj.dono == $jogadorDaVez || %selectedObj.dono == $mySelf || ($jogoEmDuplas == true && %selectedObj.dono == $mySelf.myDupla && %selectedObj.class $= "base") || ((%obj.class $= "reliquia" || %obj.class $= "artefato") && %obj.onde.dono == $mySelf && $mySelf == $jogadorDaVez)){
			if(isObject(Foco.getObject(0))){
				Foco.getObject(0).setLayer(1); //devolve o foco antigo pra layer comum das peças;	
			}
			Foco.clear(); //limpa o Foco (só pode ter uma coisa em foco por vez)
			Foco.add(%selectedObj); //atualiza o Foco
			%myMark = getMyMark(%selectedObj); //pega a marca de seleção conforme o tipo de unidade
			%myMark.mount(%selectedObj, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta no foco
			%myMark.setAutoRotation(50);
			clientSingleSelection(); //chama esta função no arquivo mainGui.cs	(coloca o foco 		
		}
	} 
	if ($escolhendoGrupo){
		for(%i=0;%i<%objCount;%i++){
			//verificando o objeto de acordo com o índice
        	%area = getWord(%objList, %i);
		
			//se encontrar um objeto que seja da classe Area
        	if(%area.class $= "Area"){
				//marcamos que uma area foi encontrada
        	    %selected = true;
        	    //e saímos do loop
        	    %i = %objCount;
			}
		}
		if(%selected){
			if($grupoStartCount == 0){
				for(%i = 0; %i < $grupoSorteadoAreas.getCount(); %i++){
					%areaDoGrupo = $grupoSorteadoAreas.getObject(%i);
					if(%area == %areaDoGrupo){
						if(!$estouNoTutorial){
							clientAskCriarBase(%area);
							%i = $grupoSorteadoAreas.getCount(); //sai do Loop;
							clientMarkMar($grupoStartNome, $jogadorDaVez.corR, $jogadorDaVez.corG, $jogadorDaVez.corB, $jogadorDaVez.corA);
						} else {
							if(%area.getName() $= "saoPaulo"){
								clientAskCriarBase(%area);
								%i = $grupoSorteadoAreas.getCount(); //sai do Loop;
								clientMarkMar($grupoStartNome, $jogadorDaVez.corR, $jogadorDaVez.corG, $jogadorDaVez.corB, $jogadorDaVez.corA);
								$myTUTAreaTerrestreInicial = %area;
								TUTmsg("brasilMaritimo", "right"); //traz a tutMsg grande
								tut_clearTips(); //apaga a arrow que mandava construir a base em saoPaulo
							}
						}
					}
				}
			} else if($grupoStartCount == 1){
				for(%i = 0; %i < $grupoSorteadoAreas.getCount(); %i++){
					%areaDoGrupo = $grupoSorteadoAreas.getObject(%i);
					if(%area == %areaDoGrupo){
						if(!$estouNoTutorial){
							clientAskCriarBase(%area);
							$grupoStartCount += 1;
							%i = $grupoSorteadoAreas.getCount(); //sai do Loop;
							clientAskPassarAVezEscolhendoGrupos();
							clientCmdPushAguardandoObjGui();
							$escolhendoGrupo = false;
						} else {
							if(%area.getName() $= "bTodosOsSantos"){
								tut_clearTips(); //apaga a arrow que mandava construir a base na bTodosSantos
								clientAskCriarBase(%area);
								$grupoStartCount += 1;
								%i = $grupoSorteadoAreas.getCount(); //sai do Loop;
								clientAskPassarAVezEscolhendoGrupos();
								clientCmdPushAguardandoObjGui();
								$escolhendoGrupo = false;
								$myTUTAreaMaritimaInicial = %area;
								aguardandoTXT.text = "Adversário está jogando";
								schedule(3000, 0, "tutIniciarNachina");
							}
						}
					}
				}
			}
		} else {
			echo("não foi encontrada uma area!");
		}
	} else if ($disparoOrbitalON || $geoDisparoON || $dragnalAtkON || $virusON){
		for(%i=0;%i<%objCount;%i++){
			//verificando o objeto de acordo com o índice
        	%area = getWord(%objList, %i);
		
			//se encontrar um objeto que seja da classe Area
        	if(%area.class $= "Area"){
				//marcamos que uma area foi encontrada
        	    %selected = true;
        	    //e saímos do loop
        	    %i = %objCount;
			}
		}
		if(%selected){
			if($disparoOrbitalON){
				clientCallConfirmarDisparoMsg("orbital", %area); //função no clientMsg.cs
			} else if($geoDisparoON){
				if($myPersona.aca_art_1 < 6){
					if(%area.terreno $= "terra"){
						clientCallConfirmarDisparoMsg("geo", %area); //função no clientMsg.cs
					} else {
						clientMsg("geoDisparoSohNaTerra", 4000);
						$geoDisparoON = false;
					}
				} else {
					clientCallConfirmarDisparoMsg("geo", %area); //função no clientMsg.cs
				}
			} else if($dragnalAtkON){
				clientCallConfirmarDisparoMsg("dragnalAtk", %area); //função no clientMsg.cs
			} else if($virusON){
				clientCallConfirmarDisparoMsg("virus", %area, $lastFocus); //função no clientMsg.cs
			}
		} else {
			echo("não foi encontrada uma area!");
		}
	}
	if($estouNoTutorial){
		tut_verificarObjetivo();
	}
}


//função para descobrir qual a marca de seleção correta para cada unidade:
function getMyMark(%obj){
	switch$(%obj.class){ //verifica qual é classe da unidade em Foco
		case "base":
		%selectionMark = $selectionBase;
				
        case "soldado":
		%selectionMark = $selectionSoldado;
						
		case "Tanque":
		%selectionMark = $selectionTanque;
						
		case "navio":
		%selectionMark = $selectionNavio;
				
		case "lider":
		%selectionMark = $selectionTanque;
		
		case "verme":
		%selectionMark = $selectionSoldado;
		
		case "rainha":
		%selectionMark = $selectionBase;
		
		case "cefalok":
		%selectionMark = $selectionNavio;
		
		case "ovo":
		%selectionMark = $selectionNavio;
		
		case "zangao":
		%selectionMark = $selectionTanque;
		
		case "reliquia":
		%selectionMark = $selectionReliquia;
		
		case "artefato":
		%selectionMark = $selectionReliquia;
	}
	
	if(%obj.dono != $mySelf){
		%selectionMark.setBlendColor($jogadorDaVez.corR, $jogadorDaVez.corG, $jogadorDaVez.corB, $jogadorDaVez.corA);
	} else {
		%selectionMark.setBlendColor($mySelf.corR, $mySelf.corG, $mySelf.corB, $mySelf.corA);
	}
	atualizarBotoesDeCompra(); //função no mainGui.cs
	
	return %selectionMark; //retorna a seleção para uso em outras funções
}  




function clientVerificarAreaSubmersa(%area){
	%submersa = false;
	if(%area.pos0Flag == true){
		if(%area.pos0Quem.class $= "rainha"){
			if(%area.pos1Flag $= "nada" && %area.pos2Flag $= "nada"){
				if(%area.pos0Quem.submersa){
					%submersa = true;	
				}
			}
		}
	}
	return %submersa;
}


$rightClickDelay = false; //zera a variável;
function setRightClickDelay(){
	$rightClickDelay = true;
	
	schedule(500, 0, "zerarRightClickDelay"); //delay de MEIO SEGUNDO (500 milissegundos, 0.5seg)
}

function zerarRightClickDelay(){
	$rightClickDelay = false;
}

//função para mostrar que unidade está sendo manipulada pelo jogadorDaVez
function ghostSelect(%unit){
	//echo("ghostSelect => " @ %unit.class @ %unit.dono.id);
	if($jogadorDaVez != $mySelf){
		if(!%unit.oculta){
			resetSelection();
			%myMark = getMyMark(%unit); //pega a marca de seleção conforme o tipo de unidade
			%myMark.mount(%unit, 0, 0, 0, 0, 0, 0, 0); //monta a selectionMark correta no foco
			%myMark.setAutoRotation(50);
			%myMark.setBlendAlpha(0.7);
			clientLigarFlechas(%unit.onde);
		}
	}
}

function resetGhostSelection(){
	if($jogadorDaVez != $mySelf){
		resetSelection();
	}
}

//==============================================================
//==============================================================
//==============================================================
//==============================================================
//==============================================================
//==============================================================

function sceneWindow2D::onRightMouseDown( %this, %mod, %worldPos, %mouseClicks ){
	if($rightClickDelay)
		return;
	
	setRightClickDelay();
			
	if ($disparoOrbitalON || $geoDisparoON || $dragnalAtkON || $virusON)
	{
		resetSelection();
		clientMouse_disparoEspecial(%worldPos);
		return;
	} 
	
	%foco = Foco.getObject(0); 
	
	if(%foco.class $= "ovo")
		return;
	
	if(!%foco.isMoveable)
		return;
		
	if($jogadorDaVez != $mySelf || %foco.dono != $mySelf)
		return;
		
	%areaClicada = clientGetAreaFromClick(%worldPos);
	if(!isObject(%areaClicada))
		return;
	
	%transporteClicado = clientGetTransporteFromClick(%worldPos);
	if(isObject(%transporteClicado))
		%alvoEhTransporte = true;
	
	%vizinhas = clientGetAreasVizinhas(%foco.onde, %areaClicada);
	if(!%vizinhas)
	{
		if(%areaClicada == %foco.onde)
			return;
				
		clientMsg("areasNaoVizinhas", 4000);
		return;
	}
		
	%areaAlvoAndavel = clientGetAreaAndavel(%areaClicada);
	if(%areaAlvoAndavel)
	{
		if(%foco.embarcavel && %alvoEhTransporte)
		{
			clientMouse_tentarEmbarcar(%foco.onde, %foco.pos, %areaClicada, %transporteClicado.pos);
			return;
		} 
				
		clientMouse_tentarAndar(%foco, %areaClicada, %alvoEhTransporte, %transporteClicado);
		return;
	}
	
	if($shiftOn)
	{
		clientMouse_shiftMover(%foco, %areaClicada, %alvoEhTransporte, %transporteClicado);
		return;
	}
	
	if(%areaClicada.oceano == 1)
	{
		clientMouse_moverParaOceano(%foco, %areaClicada);
		return;
	}
	
	if($SNIPERSHOT)
	{
		clientAskSniper(%foco.onde, %foco.pos, %areaClicada);
		return;
	} 
	
	clientMouse_tentarAtacar(%foco, %areaClicada);
}


function clientMouse_disparoEspecial(%worldPos)
{
	%area = clientGetAreaFromClick(%worldPos);
	
	if(!isObject(%area))
		return;
		
	if($disparoOrbitalON)
	{
		clientCallConfirmarDisparoMsg("orbital", %area); //função no clientMsg.cs
		return;
	}
	
	if($geoDisparoON){
		if($myPersona.aca_art_1 == 6 || %area.terreno $= "terra")
		{
			clientCallConfirmarDisparoMsg("geo", %area); //função no clientMsg.cs
			return;
		}
		
		clientMsg("geoDisparoSohNaTerra", 4000);
		$geoDisparoON = false;
		return;
	}
			
	if($dragnalAtkON)
	{
		clientCallConfirmarDisparoMsg("dragnalAtk", %area); //função no clientMsg.cs
		return;
	}
	
	if($virusON)
		clientCallConfirmarDisparoMsg("virus", %area, $lastFocus); //função no clientMsg.cs
}

function clientGetAreaFromClick(%worldPos)
{
	%objList = $strategyScene.pickPoint(%worldPos);
	%objCount = getWordCount(%objList);
	
	for(%i = 0; %i < %objCount; %i++)
	{
		%objeto = getWord(%objList, %i);
	
		if(%objeto.class $= "Area")
			return %objeto;
	}	
}

function clientGetTransporteFromClick(%worldPos)
{
	%objList = $strategyScene.pickPoint(%worldPos);
	%objCount = getWordCount(%objList); 
	
	for(%i = 0; %i < %objCount; %i++)
	{
		%objeto = getWord(%objList, %i);
	
		if(%objeto.class $= "navio" || %objeto.class $= "cefalok")
			return %objeto;
	}	
}

function clientGetAreasVizinhas(%area1, %area2)
{
	%listCount = %area1.myFronteiras.getCount(); 
	
	for (%i=0; %i < %listCount; %i++)
	{
		if(%area1.myFronteiras.getObject(%i).getName() $= %area2.getName())
			return true;
	}
	
	return false;
}

function clientMouse_tentarEmbarcar(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posDoTransporte)
{
	if(!%areaAlvo.pos0Flag == true)
	{
		clientMsg("embarqueImpossivel", 4000);
		return;
	}
	
	clientAskEmbarcar(%areaDeOrigem, %posDeOrigem, %areaAlvo, %posDoTransporte);
}

function clientMouse_tentarDesembarcar(%transporte, %areaClicada)
{
	%umaClasseNoTransporte = verificarClassesDoNavio(%transporte);
	%umaCorNoTransporte = verificarCoresNoNavio(%transporte);
	
	if(!%umaCorNoTransporte)
	{
		$classD_onde = %transporte.onde;
		$classD_pos = %transporte.pos;
		$classD_alvo = %areaClicada;
		clientLigarQueCorDesembarcaHud();
		return;	
	}
	
	if(!%umaClasseNoTransporte)
	{
		$classD_onde = %transporte.onde;
		$classD_pos = %transporte.pos;
		$classD_alvo = %areaClicada;
		clientLigarQuemDesembarcaHud();
		return;
	}
	
	clientAskDesembarcar(%transporte.onde, %transporte.pos, %areaClicada);
}

function clientGetJetPackOn(%foco)
{
	if(%foco.JPagora > 0)
		return true;
	
	return false;
}

function clientGetAreaAndavel(%areaClicada)
{
	if(%areaClicada.dono == $mySelf)
		return true;
		
	if(%areaClicada.dono $= "0")
		return true;
		
	if(%areaClicada.desprotegida)
		return true;
		
	if(%areaClicada.dono $= "MISTA")
		return true;
		
	if(%areaClicada.dono == $mySelf.myDupla)
		return true;
		
	if(%areaClicada.dono $= "COMPARTILHADA" && (%areaClicada.dono1 == $mySelf || %areaClicada.dono2 == $mySelf))
		return true;
		
	return false;	
}


function clientGetAreaCapturavel(%area)
{
	if(!%area.desprotegida)
		return false;
		
	if(%area.pos0Quem.dono == $mySelf)
		return false;
		
	if(%area.dono == $mySelf.myDupla)
		return false;
		
	if(%area.dono $= "MISTA")
		return false;
		
	return true;
}

function clientSetCapturaQMsgBox(%foco, %areaClicada)
{
	$lastCapturaAreaDeOrigem = %foco.onde;
	$lastCapturaPosDeOrigem = %foco.pos;
	$lastCapturaAreaAlvo = %areaClicada;
	clientPushCapturaQMsgBox();	
}



function clientMouse_tentarAndar(%foco, %areaClicada, %alvoEhTransporte, %transporteClicado)
{
	//movimentos:
	%jetPackOn = clientGetJetPackOn(%foco);
	if($jogadorDaVez.movimentos < 1 && !%jetPackOn)
	{
		clientMsg("movimentosAcabaram", 4000);
		return;
	}
	
	//desembarque:
	if (%foco.transporte && %areaClicada.terreno $= "terra" && %foco.myTransporte.getCount() > 0)
	{
		clientMouse_tentarDesembarcar(%foco, %areaClicada);
		return;
	}
	
	%areaAlvoCapturavel = clientGetAreaCapturavel(%areaClicada);
	
	if(%foco.onde.terreno $= %areaClicada.terreno)
	{
		if(%foco.class !$= "rainha")
		{
			if(%areaAlvoCapturavel && %foco.class $= "zangao")
			{
				clientSetCapturaQMsgBox(%foco, %areaClicada);
				return;
			}
			
			clientAskMovimentar(%foco.onde, %foco.pos, %areaClicada);
			return;
		}
		
		if(%areaAlvoCapturavel)
		{
			clientAskRainhaCapturarBase(%foco.onde, %foco.pos, %areaClicada);
			return;
		}
		
		if(%areaClicada.pos0Flag || (%areaClicada.oceano == 1 && %areaClicada.pos0Quem != 0))
		{
			clientMsg("duasRainhasUmaArea", 3000);
			return;
		}
		
		clientAskMovimentar(%foco.onde, %foco.pos, %areaClicada);
		return;
	}
	
	//Daki pra baixo são terrenos diferentes, áreaAlvo permanece andável
	if(%foco.anfibio)
	{
		if(%areaAlvoCapturavel && %foco.class $= "zangao")
		{
			clientSetCapturaQMsgBox(%foco, %areaClicada);
			return;	
		}
		
		clientAskMovimentar(%foco.onde, %foco.pos, %areaClicada); 
		return;
	}
	
	if(%foco.class $= "rainha")
		clientMouse_tentarMoverRainha(%foco, %areaClicada);	//Rainhas não contam como "anfíbios", são especiais... não lembro pq :(	
}

function clientMouse_tentarMoverRainha(%foco, %areaClicada)
{
	if(%areaClicada.pos0Flag == true && (%areaClicada.pos0Quem.dono == $mySelf || %areaClicada.pos0Quem.dono == $mySelf.myDupla))
	{
		clientMsg("duasRainhasUmaArea", 3000);
		return;
	}
		
	if(%areaClicada.ilha $= "1")
	{
		clientMsg("rainhasEmIlhas", 3000);	
		return;	
	}
	
	if(%foco.submersa && %areaClicada.terreno !$= "mar")
	{
		clientMsg("submersaNaumMoveTerra", 3000);
		return;
	}
	
	if(%areaClicada.pos0Flag)
	{
		clientAskRainhaCapturarBase(%foco.onde, %foco.pos, %areaClicada);
		return;
	}
	
	clientAskMovimentar(%foco.onde, %foco.pos, %areaClicada); 	
}

function clientMouse_shiftMover(%foco, %areaClicada, %alvoEhTransporte, %transporteClicado)
{
	if(%areaClicada.dono $= "COMPARTILHADA")
	{ 
		clientMsg("passagemCompartilhada", 5000);
		return;
	}
	
	if(%foco.JPagora > 0)
		%jetPackOn = true;
	
	if($jogadorDaVez.movimentos < 1 && !%jetPackOn)
	{
		clientMsg("movimentosAcabaram", 4000);
		return;	
	}
	
	$NEGareaAlvo = %areaClicada;
	
	if(%foco.onde.terreno $= %areaClicada.terreno)
	{ 
		clientAskPermissaoParaVisitar(%foco.onde, %foco.pos, %areaClicada);
		return;
	}
	
	if((%foco.embarcavel) && %alvoEhTransporte == true && %areaClicada.pos0Flag == true)
	{
		clientAskPermissaoParaEmbarcar(%foco.onde, %foco.pos, %areaClicada, %transporteClicado.pos);			
		return;
	}
		
	if(%foco.class $= "lider" || %foco.class $= "zangao")
	{
		if(!%foco.anfibio)
			return;
			
		clientAskPermissaoParaVisitar(%foco.onde, %foco.pos, %areaClicada);
		return;
	}
	
	if(%foco.class $= "rainha")
	{
		if(%areaClicada.pos0Flag || (%areaClicada.oceano == 1 && %areaClicada.pos0Quem != 0))
			return;
			
		clientAskPermissaoParaVisitar(%foco.onde, %foco.pos, %areaClicada);
		return;
	}
	
	if(%foco.transporte){
		if(%foco.myTransporte.getCount() < 1)
			return;
			
		clientAskPermissaoParaDesembarcar(%foco.onde, %foco.pos, %areaClicada);
		return;		
	}
}



function clientMouse_moverParaOceano(%foco, %areaClicada)
{
	if(%foco.class $= "rainha" && %areaClicada.pos0Quem != 0)
	{
		clientMsg("duasRainhasUmaArea", 3000);
		return;
	}
		
	clientAskMovimentar(%foco.onde, %foco.pos, %areaClicada); 	
}

function clientMouse_tentarAtacar(%foco, %areaClicada)
{
	if(%foco.submersa)
	{ 
		if(%areaClicada.terreno !$= "mar")
		{
			clientMsg("submersaNaumAtacaTerra", 3000);
			return;
		}
		
		clientAskAtacar(%foco.onde, %areaClicada); 
		return;
	}
		
	%areaAlvoSubmersa = clientVerificarAreaSubmersa(%areaClicada);
	if(%areaAlvoSubmersa && %foco.onde.terreno !$= "mar")
	{
		clientMsg("terraNaumAtacaSubmersa", 3000); 
		return;	
	}
	
	clientAskAtacar(%foco.onde, %areaClicada);
}