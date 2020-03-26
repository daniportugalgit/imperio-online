const express = require('express')
const router = express.Router()
const async = require('../../utils/async')
const models = require('../../models')

const gameService = require('../../services/game-service')
const personaRepository = require('../../repositories/persona-repository')

//   /sala/adicionar?idPersona=XXX&sala=sala%20YYY&login=username

router.get('/adicionar',  
    async.handler(async (req, res) => {
      const personaID = req.query.idPersona
      const gameID = req.query.idJogo
      const roomName = req.query.sala

      let persona = await personaRepository.get(personaID)
      if (!persona)
			  throw Error("Persona not found: " + personaID)

      let game = gameID ? 
        await gameService.addPersona(gameID, persona) :
        await gameService.createGame(roomName, persona)
  	
      res.send("idJogo " + game.id + " username " + persona.user.name);
    })
);

module.exports = router;