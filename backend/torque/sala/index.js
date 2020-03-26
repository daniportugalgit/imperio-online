const express = require('express')
const router = express.Router()
const async = require('../../utils/async')
const models = require('../../models')

const gameService = require('../../services/game-service')

//   /sala/adicionar?idPersona=XXX&sala=sala%20YYY&login=username

router.get('/adicionar',  
    async.handler(async (req, res) => {

      gameService.addPersona(req.params.idPersona, req.params.sala)
      

      
      //res.send("idJogo" + " " + {gameId} + " " + "algumaCoisaAqui" + " " + {persona.user.name});
      //resposta esperada:
      //idJogo {gameId} {???} {username} //se a sala não existir mais, Torque espera uma resposta igual, só que com gameId == 0
    })
);

module.exports = router;