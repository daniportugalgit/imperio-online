// ============================================================
// Project            :  Império
// File               :  ..\..\..\Program Files\TorqueGameBuilderPro\games\Imperio\gameScripts\client\clientMsg.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  domingo, 28 de outubro de 2007 9:35
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  Todaas as mensagens para o cliente devem aparacer
//                    :  neste arquivo, a não ser que sejam especiais.
//                    :  
// ============================================================
function clientClearCentralButtonControl(){
	darPassagem_btn.setVisible(false); //desativa os botões	
	negarPassagem_btn.setVisible(false);
	sim_btn.setVisible(false);
	nao_btn.setVisible(false);
	permitir_btn.setVisible(false);
	permitirE_btn.setVisible(false);
	naoPermitir_btn.setVisible(false);
	renderTudo_btn.setVisible(false); 
	naoRender_btn.setVisible(false);
	cancelarCanhaoBtn.setVisible(false);
	confirmarDisparo_btn.setVisible(false);
	desembarqueCorHud_NBtn.setVisible(false); //desembarque amigável
	desembarqueCorHud_VBtn.setVisible(false);
	desembarqueHud_SBtn.setVisible(false);
	desembarqueHud_GBtn.setVisible(false);
	propHud_aceitarBtn.setVisible(false);
	propHud_negarBtn.setVisible(false);
	propHud_cancelarBtn.setVisible(false);
}

function clientMsgGuiZerar(){
	clientClearCentralButtonControl();
	msgGui.bitmap = "~/data/images/centralHudMsg.png";
	msgGuiTitle_txt.setVisible(false); //apaga os textos
	msgGuiLinha1_txt.setVisible(false);
	msgGuiLinha2_txt.setVisible(false);
	msgGuiTxt_img.setVisible(false);
}

function clientApagarMsgGui(){
	clientClearCentralButtonControl();
	msgGui.setVisible(false);
}
function clientApagarMsgGuiEm(%milisegundos){
	$msgGuiSchedule = schedule(%milisegundos, 0, "clientApagarMsgGui");
}

//%tipo é a imagem, %tempo é em milissegundos pra apagr o msgGui:
function clientMsg(%tipo, %tempo){
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	clientMsgGuiZerar();
	msgGui.setVisible(true);
	msgGuiTxt_img.setVisible(true);
	msgGui.bitmap = "~/data/images/centralHudMsg.png";
	msgGuiTxt_img.bitmap = "~/data/images/clientMsg_" @ %tipo @ ".png";
	clientApagarMsgGuiEm(%tempo);	
}

///////////////////////CLIENT MESSAGES/////////////////////////
//Primeiro os clientCOMMANDS:

//PERGUNTA - pode entrar aki?:
///função clientCmdPushPerguntaVisita(%quemPede, %ondeQuerEntrar);
function clientCmdPushPerguntaVisita(%visitanteId, %areaDeOrigem, %posDeOrigem, %areaAlvo){
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	%eval = "%visitante = $" @ %visitanteId @ ";";
	eval(%eval);
	
	msgGui.setVisible(true); //primeiro traz o msgGui
	clientMsgGuiZerar();
	darPassagem_btn.setVisible(true); //ativa os botões	
	negarPassagem_btn.setVisible(true);
	msgGuiTitle_txt.setVisible(true); //ativa o texto de título
	msgGuiTitle_txt.text = %visitante.nome SPC "pede passagem"; //ajusta o texto:
	
	%unit = clientGetGameUnit(%areaDeOrigem, %posDeOrigem);
	ghostSelect(%unit); //marca pro receptor do pedido qual é a unidade que está pedindo passagem
	
	//grava as strings em variáveis globais aki no client:
	$NEGareaDeOrigem = %areaDeOrigem;
	$NEGposDeOrigem = %posDeOrigem;
	$NEGareaAlvo = %areaAlvo;
	
	alxPlay(passagem);
	
	//liga o marcador na tela:
	%animationName = "arrow" @ %visitante.myColor @ "Anim";
	arrowAnim.playAnimation(%animationName);
	arrowAnim.setPosition(%areaAlvo.pos2);
}

//PERGUNTA - pode Embarcar neste navio?:
function clientCmdPushPerguntaEmbarque(%visitanteId, %areaDeOrigem, %posDeOrigem, %areaAlvo, %navioPos){
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	%eval = "%visitante = $" @ %visitanteId @ ";";
	eval(%eval);
	
	msgGui.setVisible(true); //primeiro traz o msgGui
	clientMsgGuiZerar();
	permitirE_btn.setVisible(true); //ativa os botões	
	naoPermitir_btn.setVisible(true);
	msgGuiTitle_txt.setVisible(true); //ativa o texto de título
	msgGuiTitle_txt.text = %visitante.nome SPC "deseja embarcar"; //ajusta o texto:
	
	//prepara para marcar na tela:
	%unit = clientGetGameUnit(%areaDeOrigem, %posDeOrigem);
	ghostSelect(%unit); //marca pro receptor do pedido qual é a unidade que está pedindo embarque
	%eval = "%areaAlvo =" SPC %areaAlvo @ ";";
	eval(%eval);
	
	//grava as strings em variáveis globais aki no client:
	$NEGareaDeOrigem = %areaDeOrigem;
	$NEGposDeOrigem = %posDeOrigem;
	$NEGareaAlvo = %areaAlvo;
	$NEGnavioPos = %navioPos;
		
	alxPlay(passagem);
		
	//liga o marcador na tela:
	%animationName = "arrow" @ %visitante.myColor @ "Anim";
	arrowAnim.playAnimation(%animationName);
	%eval = "arrowAnim.setPosition(%areaAlvo." @ %navioPos @ ");";
	eval(%eval);
}

//PERGUNTA - pode desembarcar aki?:
function clientCmdPushPerguntaDesembarque(%visitanteId, %areaDeOrigem, %posDeOrigem, %areaAlvo){
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	%eval = "%visitante = $" @ %visitanteId @ ";";
	eval(%eval);
	
	msgGui.setVisible(true); //primeiro traz o msgGui
	clientMsgGuiZerar();
	permitir_btn.setVisible(true); //ativa os botões	
	naoPermitir_btn.setVisible(true);
	msgGuiTitle_txt.setVisible(true); //ativa o texto de título
	msgGuiTitle_txt.text = %visitante.nome SPC "deseja desembarcar"; //ajusta o texto:
	
	//prepara para marcar na tela:
	%eval = "%areaAlvo =" SPC %areaAlvo @ ";";
	eval(%eval);
	
	//grava as strings em variáveis globais aki no client:
	$NEGareaDeOrigem = %areaDeOrigem;
	$NEGposDeOrigem = %posDeOrigem;
	$NEGareaAlvo = %areaAlvo;
	
	alxPlay(passagem);	
		
	//liga o marcador na tela:
	%animationName = "arrow" @ %visitante.myColor @ "Anim";
	arrowAnim.playAnimation(%animationName);
	arrowAnim.setPosition(%areaAlvo.pos2);
}
	
//clientMsg Pedido Negado:
function clientCmdReceberPedidoNegado(){
	cancel($apagarPerguntaTimer);
	$estouPerguntando = false;
	clientMsg("pedidoNegado", 3000);
	clientPopServercomDot();
	arrowAnim.setPosition("-70 -70"); //marcador fora da tela;
}

//clientMsg Não houve resposta:
function clientCmdNaoHouveResposta(){
	clientMsg("semResposta", 3000);
	clientPopServercomDot();
	arrowAnim.setPosition("-70 -70"); //marcador fora da tela;
	if($estouPerguntando){
		cancel($apagarPerguntaTimer);
		$estouPerguntando = false;
	}
	if($estouPropondo){
		cancel($apagarPropostaTimer);
		$piscarBase1Mark = false; //faz parar de piscar
		$piscarBase2Mark = false;
		$estouPropondo = false;
	}
}

//clientMsg Visita Autorizada:
function clientCmdMsgGuiVisitaAutorizada(){
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	$estouPerguntando = false;
	clientMsg("passagemAutorizada", 3000);
	arrowAnim.setPosition("-70 -70"); //marcador fora da tela;
}

//clientMsg Desembarque Autorizado:
function clientCmdMsgGuiDesembarqueAutorizado(){
	clientPopServercomDot();
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	$estouPerguntando = false;
	clientMsg("desembarqueAutorizado", 3000);
	arrowAnim.setPosition("-70 -70"); //marcador fora da tela;
}

//clientMsg Embarque Autorizado:
function clientCmdMsgGuiEmbarqueAutorizado(){
	clientPopServercomDot();
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	$estouPerguntando = false;
	clientMsg("embarqueAutorizado", 3000);
	arrowAnim.setPosition("-70 -70"); //marcador fora da tela;
}

//Apagar o msgGui:
function clientCmdApagarMsgGui(){
	clientClearCentralButtonControl();
	msgGui.setVisible(false);
	arrowAnim.setPosition("-70 -70"); //marcador fora da tela;
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	
}
///////////
function clientMsgInfoDoadaRecebida(%quemDoou){
	clientMsg("missaoDoadaRecebida", 4000);
	msgGuiLinha2_txt.setVisible(true); //ativa o texto de título
	msgGuiLinha2_txt.text = %quemDoou;
}


function clientCmdClientMsgPlayerMorreu(%quemMorreu, %quemMatou){
	clientMsg("jogadorEliminado", 5000);
	msgGuiLinha1_txt.setVisible(true); //ativa o texto de título
	msgGuiLinha1_txt.text = %quemMorreu;
	
	clientCancelarAcordosComMorto(%quemMorreu);
	clientAtualizarExplHud();
	clientAtualizarEstatisticas();
	clientAtualizarExplRoubados(); //torna coloridos os ExplMarkers que eram compartilhados e estavam na vez do morto
	clientMarcarMorto(%quemMorreu);
}

function clientCmdClientMsgPlayerSuicidou(%quemMorreu){
	clientMsg("jogadorEliminado", 5000);
	msgGuiLinha1_txt.setVisible(true); //ativa o texto de título
	msgGuiLinha1_txt.text = %quemMorreu;
	
	clientCancelarAcordosComMorto(%quemMorreu);
	clientAtualizarExplHud();
	clientAtualizarEstatisticas();
	clientAtualizarExplRoubados(); //torna coloridos os ExplMarkers que eram compartilhados e estavam na vez do morto
	clientMarcarMorto(%quemMorreu);
}

function clientCmdClientMsgMatouPlayer(%quemMorreu){
	clientMsg("jogadorEliminado", 5000);
	msgGuiLinha1_txt.setVisible(true); //ativa o texto de título
	msgGuiLinha1_txt.text = %quemMorreu;
	clientAtualizarExplRoubados(); //torna coloridos os ExplMarkers que eram compartilhados e estavam na vez do morto
	clientMarcarMorto(%quemMorreu);
	setNormalZoom();
}

function clientCmdClientMsgMorri(%quemMatou){
	clientMsg("voceMorreu", 5000);
	clientExplGuiClear();
	finalizarTurno_btn.setVisible(false);
	clientMarcarMorto($myPersona.nome);
	$mySelf.mySimInfo.clear();
	$mySelf.mySimExpl.clear();
	
	//deletar todos os marcadores de missão:
	for(%i = 0; %i < 80; %i++){
		%info = clientFindInfo(%i + 1);
		if(isObject(%info.myMark)){
			%info.myMark.safeDelete();	
		}
		if(isObject(%info.myExplMark)){
			%info.myExplMark.safeDelete();	
		}
		%info.jahFoiOferecida = false;
	}
	setNormalZoom();
}

function clientCmdClientMsgSincronizando(){
	clientMsg("sincronizando", 2000);
	palcoTurnoTimer.pauseTimer(); //pausa o turnoTimer;
	clientPopServerComDot();
	$ultimoAtaqueFinalizado = true;
}

function clientCmdClientMsgNaoHaEspaco(){
	clientMsg("naoHaEspaco", 4000);
	clientPopServerComDot();
}


//ClientMSg Doação efetuada
function clientCmdClientMsgDoacaoEfetuada(%quemRecebeu)
{
	//clientMsgBoxOK("DoacaoEfetuada", false, false, true);
	//msgBoxOk_txt3.text = %quemRecebeu;
	//msgBoxOk_txt4.text = "";
	clientMsgBoxOKT("DOAÇÃO EFETUADA", strupr(%quemRecebeu) @ " RECEBEU SUA DOAÇÃO.");	
}







//ClientMSg Doação recebida
function clientCmdClientMsgDoacaoRecebida(%quemDoou, %imperiais, %minerios, %petroleos, %uranios){
	doacaoRecebidaGui.setVisible(true);
	doacaoRecebidaGuiImp.text = %imperiais;
	doacaoRecebidaGuiMin.text = %minerios;
	doacaoRecebidaGuiPet.text = %petroleos;
	doacaoRecebidaGuiUra.text = %uranios;
	doacaoRecebidaGuiTXT.text = %quemDoou;
	
	clientApagarDoacaoRecebidaGuiEm(6000);
}

function clientApagarDoacaoRecebidaGuiEm(%tempo){
	cancel($doacaoRecebidaGuiSchedule);
	$doacaoRecebidaGuiSchedule = schedule(%tempo, 0, "clientApagarDoacaoRecebidaGui");	
}

function clientApagarDoacaoRecebidaGui(){
	cancel($doacaoRecebidaGuiSchedule);
	doacaoRecebidaGui.setVisible(false);
}	


function clientCmdClientMsgAtkPrimeiraRodada(){
	clientMsg("ataqueIlegal", 3000);
	clientPopServerComDot();
}


function clientCmdMovimentosAcabaram(){
	clientMsg("movimentosAcabaram", 4000);
	clientPopServerComDot();
}


///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////
//Agora as clientFUNCTIONS:
//canhão Orbital:
function clientMsgCanhaoOrbital(){
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	clientMsgGuiZerar();
	msgGui.setVisible(true);
	msgGuiTxt_img.setVisible(true);
	msgGui.bitmap = "~/data/images/centralHudMsg.png";
	msgGuiTxt_img.bitmap = "~/data/images/clientMsg_canhaoOrbitalDisparar.png";
	cancelarCanhaoBtn.setVisible(true);	
}

//DragnalAtk:
function clientMsgDragnalAtk(){
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	clientMsgGuiZerar();
	msgGui.setVisible(true);
	msgGuiTxt_img.setVisible(true);
	msgGui.bitmap = "~/data/images/centralHudMsg.png";
	msgGuiTxt_img.bitmap = "~/data/images/clientMsg_dragnalAtacar.png";
	cancelarCanhaoBtn.setVisible(true);	
}

//Vírus:
function clientMsgVirusON(){
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	clientMsgGuiZerar();
	msgGui.setVisible(true);
	msgGuiTxt_img.setVisible(true);
	msgGui.bitmap = "~/data/images/centralHudMsg.png";
	msgGuiTxt_img.bitmap = "~/data/images/clientMsg_virusDisparar.png";
	cancelarCanhaoBtn.setVisible(true);	
}


//PERGUNTA - quer mesmo render esta peça?:
///pergunta de segurança antes de render uma peça:
function clientRenderUnidadeQuestion(){
	%eval = "%mySelf =" SPC $mySelf @ ";";
	eval(%eval);
	%unit = Foco.getObject(0);
		
	if(%unit.dono.id $= %mySelf.id){
		if(%unit.class !$= "base"){
			clientMsg("renderUnidade", 6000);
			$myLastUnitPraRender = %unit;
			sim_btn.setVisible(true); //ativa os botões	
			nao_btn.setVisible(true);
		} else {
			echo("Bases não podem ser rendidas");	
		}
	} else {
		echo ("Esta unidade não te pertence!");	
	}
}

//////Doação:
function clientDoarQuestion(){
	if($doarQuestionOn == false){
		msgGui.setVisible(true);
		clientMsgGuiZerar();
		msgGuiTitle_txt.setVisible(true); //ativa os textos
		msgGuiTitle_txt.text = "DOAR"; //ajusta o texto
		renda_btn.setVisible(true); //ativa os botões	
		info_btn.setVisible(true);
		$doarQuestionOn = true;
	} else {
		msgGui.setVisible(false);
		$doarQuestionOn = false;
	}
}


//Deseja doar ou cancelar [question]:
function clientDoarOuCancelarQuestion(){
	msgGui.setVisible(true);
	clientMsgGuiZerar();
	msgGuiTitle_txt.setVisible(true); //ativa os textos
	msgGuiTitle_txt.text = "DOAR"; //ajusta o texto
	msgGuiLinha1_txt.setVisible(true); //ativa os textos
	msgGuiLinha1_txt.text = "Selecione a Missão"; //ajusta o texto
	doarInfoCancelar_btn.setVisible(true); //ativa os botões	
}




////////////////
//Moratória:
function clientMsgEmbargo(%nomeDoParceiro){
	clientMsg("moratoriaDecretada", 4000);
	msgGuiLinha2_txt.setVisible(true); //ativa o texto de título
	msgGuiLinha2_txt.text = %nomeDoParceiro;
}




//render tudo???!!
function clientToggleRenderTudoQuestion(){
	clientMsg("renderTodos", 6000);
	renderTudo_btn.setVisible(true); //ativa os botões	
	naoRender_btn.setVisible(true);
}



//Recursos Insuficientes:
function clientCmdRecursosInsuficientes(){
	clientMsg("recursosInsuficientes", 4000);
	clientToggleRecursosBtns();
	clientPopServerComDot();
}


//Confirmar Disparo:
function clientCallConfirmarDisparoMsg(%tipo, %area, %unitOrigem){
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
		
	msgGui.setVisible(true); //primeiro traz o msgGui
	clientMsgGuiZerar();
	confirmarDisparo_btn.setVisible(true); //btn SIM
	cancelarCanhaoBtn.setVisible(true); //btn CANCELAR
	msgGuiTitle_txt.setVisible(true); //ativa o texto de título
	if(%tipo !$= "dragnalAtk"){
		msgGuiTitle_txt.text = "CONFIRMAR DISPARO?"; //ajusta o texto:
	} else {
		msgGuiTitle_txt.text = "CONFIRMAR ATAQUE?"; //ajusta o texto:
	}
	
	//grava as strings em variáveis globais aki no client:
	$DISPARO_AREA = %area;
	$DISPARO_TIPO = %tipo;
	$UNIT_ORIGEM = %unitOrigem;
		
	//alxPlay(disparoQuestion);
	
	//liga o marcador na tela:
	clientMarkDisparo(%area);
}

function clientMarkDisparo(%area){
	disparoMark.setPosition(%area.pos1);
	%disparoEffect = new t2dParticleEffect(){scenegraph = disparoMark.scenegraph;};
	%disparoEffect.loadEffect("~/data/particles/confirmarDisparoFX.eff");
	%disparoEffect.mount(disparoMark);
	%disparoEffect.playEffect();
	disparoMark.myEffect = %disparoEffect;
}

function clientClearDisparoMark(){
	disparoMark.setPosition("-300 -300");
	disparoMark.myEffect.safeDelete();
}

function clientMsgGeoCanhaoDisparar(){
	cancel($msgGuiSchedule);
	cancel($apagarPerguntaTimer);
	clientMsgGuiZerar();
	msgGui.setVisible(true);
	msgGuiTxt_img.setVisible(true);
	msgGui.bitmap = "~/data/images/centralHudMsg.png";
	msgGuiTxt_img.bitmap = "~/data/images/clientMsg_geoCanhaoDisparar.png";
	cancelarCanhaoBtn.setVisible(true);	
}

function clientCmdClientMsgNaoHaEspacoRainha(){
	clientMsg("NaoHaEspacoRainha", 3000);
	clientpopServercomdot();
}


///////////////
//Mensagem reciba direto do server:
function clientCmdClientMsg(%txt, %popServerComDot)
{
	clientMsg(%txt, 3000);
	
	if(%popServerComDot)
		clientPopServerComDot();
}