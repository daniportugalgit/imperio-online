// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientDev.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quinta-feira, 5 de fevereiro de 2009 12:22
//
// Editor             :  Codeweaver v. 1.2.2685.32755
//
// Description        :  
//                    :  
//                    :  
// ============================================================

function dev_login()
{
	if(isObject($myPersonas))
		return;
		
	dev_setLoginSenha();
	JoinServerGui.queryLan();
	
	schedule(3000, 0, "dev_connectToNix");
}

function dev_setLoginSenha()
{
	$pref::Player::Name = playerName_itxt.getText();
	$pref::Player::Senha = playerSenha_itxt.getText();
}

function dev_connectToNix()
{
	if (setServerInfo(1))
	{
    	connectToServer($ServerInfo::Address);
    	canvas.pushDialog(waitingForServer);
    	JoinServerGui.exit();
    }
}