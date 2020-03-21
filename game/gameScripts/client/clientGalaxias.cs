// ============================================================
// Project            :  Imperio
// File               :  .\Imperio\game\gameScripts\client\clientGalaxias.cs
// Copyright          :  
// Author             :  admin
// Created on         :  quinta-feira, 4 de junho de 2009 19:59
//
// Editor             :  Codeweaver v. 1.2.3430.42233
//
// Description        :  
//                    :  
//                    :  
// ============================================================
//testes:

function clientTestGalaxy()
{
	canvas.popDialog(clientStartGui);
	canvas.popDialog(genericSplash);
	grupo1_carta.setVisible(false);
	grupo2_carta.setVisible(false);
	grupo3_carta.setVisible(false);
	grupo4_carta.setVisible(false);
	%arquivo = "game/data/levels/galaxia_0.t2d";
	sceneWindow2D.loadLevel(%arquivo);
}
///////////