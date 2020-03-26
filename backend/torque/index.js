'use strict'

const express = require('express')
const api = express.Router()

const login = require('./login')
const persona = require('./persona')
const academia = require('./academia')
const sala = require('./sala')
const jogo = require('./jogo')

api.use('/login', login)
api.use('/persona', persona)
api.use('/academia', academia)
api.use('/sala', sala)
api.use('/jogo', jogo)

module.exports = api

//TODO

//Um arquivo que ajuda a compreender as respostas esperadas pelo Torque é o game/gameScripts/server/serverGetPost.cs

/*
- Criar sala:
/sala/adicionar ? idPersona=XX & sala='sala 1' & login=username
Resposta? ACHO que é:
idJogo 12

- Entrar em uma sala:
/sala/adicionar ? idPersona=XX & idJogo=12 & login=username
Resposta? ACHO que é:
idJogo 12

- Sair de uma sala:
/sala/remover ? idPersona=XX & idJogo=12 & login=username
Resposta? ACHO que é:
idJogo 12

- Finalizar um jogo:
/jogo/finalizar?
idJogo=12
&
idJogoTorque=1
&
duracao=16789
&
idsPersona=1;6;8;
&
creditos=10;6;1;
&
b_vitoria=1;0;0;
&
pontos=8;7;6;
&
b_visionario=1;0;0;
&
b_atacou=0;1;0;
&
b_arrebatador=0;0;1;
&
b_comerciante=1;1;0;
&
onde_saiu=1;3;2; (é o ID interno do Grupo; China == 3, e assim por diante)
&
id_obj1=2;6;23;
&
id_obj2=3;7;24;
&
qts_matou=0;0;1;
&
b_pk_power_play=0;0;0;
&
termino=F;F;S;  //F = finalizou; S = suicidou; R = rendeu;
&
planeta=2

Resposta?
ACHO que é:
"OK " + gameId + " " + nextGameId
OK 12 13
*/