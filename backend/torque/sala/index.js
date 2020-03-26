const express = require('express')
const router = express.Router()
const async = require('../../utils/async')
const models = require('../../models')

const gameService = require('../../services/game-service')

//   /sala/adicionar?idPersona=XXX&sala=sala%20YYY&login=username

router.get('/adicionar',  
    async.handler(async (req, res) => {
      const personID = req.params.idPersona
      const gameID = req.params.idJogo
      const roomName = req.params.sala
      const planetID = 1 // hardcoded ????

      let game = gameID ? 
        await gameService.addPersona(gameID, personID) :
        await gameService.createGame (roomName, planetID, persona)
  	
      res.send("idJogo " + game.id);
    })
);

module.exports = router;