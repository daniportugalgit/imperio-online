// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\firstStart.cs
// Copyright          :  
// Author             :  admin
// Created on         :  terça-feira, 24 de fevereiro de 2009 11:39
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function newSimObj(%classe, %num)
{
	%eval = "$" @ %classe @ %num @ "= new ScriptObject(){";
	%eval = %eval @ "class = " @ %classe @ ";";
	%eval = %eval @ "};";
	eval(%eval);
		
	%eval = "%esteObj = $" @ %classe @ %num @ ";"; 
	eval(%eval);
	
	//echo(%eval);
	
	return %esteObj;
}

function clientCmdErroGenericoNaConexao()
{
	disconnect();
	clientMsgBoxGenErro();
}

function clientMsgBoxGenErro()
{
	MessageBoxOK("DESCONECTADO", "Impossível conectar com o servidor! Por favor, tente novamente mais tarde.");
	conectandoGui.setVisible(false);
}

function clientCmdMsgBoxLoginOuSenhaRejected()
{
	clientAskDesconectar();
	conectandoGui.setVisible(false);
	MessageBoxOK("DESCONECTADO", "Login ou senha incorretos! Caso tenha esquecido seu login ou senha, verifique seu e-mail (ao se cadastrar no Projeto Império você recebeu um e-mail contendo estes dados).");	
}