// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientMsgBoxOKPadrao.cs
// Copyright          :  
// Author             :  Daniel Portugal
// Created on         :  sexta-feira, 18 de abril de 2008 17:52
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================


function clientMsgBoxOK(%msg, %txt1On, %txt2On, %txt3e4On){
	//dois textos descentralizados
	if(%txt1On $= ""){
		msgBoxOk_txt1.setVisible(false);
	} else {
		msgBoxOk_txt1.setVisible(%txt1On);
	}
	if(%txt2On $= ""){
		msgBoxOk_txt2.setVisible(false);
	} else {
		msgBoxOk_txt2.setVisible(%txt2On);
	}
	
	//duas linhas centralizadas:
	if(%txt3e4On $= ""){
		msgBoxOk_txt3.setVisible(false);
		msgBoxOk_txt4.setVisible(false);
	} else {
		msgBoxOk_txt3.setVisible(%txt3e4On);
		msgBoxOk_txt4.setVisible(%txt3e4On);
	}
	canvas.pushDialog(msgBoxOKPadraoGui);
	msgBoxOKPadrao.bitmap = "~/data/images/msgBoxOk/msgBox" @ %msg @ ".png";
}

function clientFecharMsgBoxOk(){
	canvas.popDialog(msgBoxOKPadraoGui);	
}

function clientMsgBoxOKT(%titulo, %texto)
{
	canvas.pushDialog(msgBoxOKTGui);
	msgBoxOKT_titulo_txt.text = %titulo;
	%finalText = "<just:center>" @ %texto @ "\n\n";
	msgBoxOKT_texto_txt.setText(%finalText);
}

function clientPopMsgBoxOKT()
{
	canvas.popDialog(msgBoxOKTGui);
}

function clientMsgBoxOKT3(%titulo, %texto)
{
	canvas.pushDialog(msgBoxOKT3Gui);
	msgBoxOKT3_titulo_txt.text = %titulo;
	%finalText = "<just:center>" @ %texto @ "\n\n";
	msgBoxOKT3_texto_txt.setText(%finalText);
}

function clientPopMsgBoxOKT3()
{
	canvas.popDialog(msgBoxOKT3Gui);
}