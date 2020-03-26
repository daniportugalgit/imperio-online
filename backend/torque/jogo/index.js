'use strict';

const express = require('express')
const router = express.Router()
const async = require('../../utils/async')

const gameService = require('../../services/game-service')

router.get('/finalizar',  
    async.handler(async (req, res) => {
        let participations = {}

        let order = []
        req.query.idsPersona.split(';').forEach((id, i) => {
            order[i] = id
            participations[id] = {personaID: id}
        })

        let add = (input, output) => {
            if (!req.query[input])
                return

            req.query[input].split(';').forEach((x, i) => {
                participations[order[i]][output] = x
            })
        }

        add('creditos', 'credits')
        add('b_vitoria', 'hasWon')
        add('pontos', 'points')
        add('b_visionario', 'visionary')
        add('b_arrebatador', 'sweeper')
        add('b_comerciante', 'trader')
        add('id_obj1', 'objective1')
        add('id_obj2', 'objective2')
        add('onde_saiu', 'startingRegion')
        add('qts_matou', 'kills')
        add('b_pk_power_play', 'powerPlay')
        add('termino', 'endGameStatus')
        
        let nextGame = await gameService.endGame(req.query.idJogo, participations, req.query.duracao, req.query.planeta)
        
    	res.send("idJogo " + req.query.idJogo+ " " + nextGame.id);
    })
);

module.exports = router;

/*
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
*/