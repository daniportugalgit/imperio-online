const express = require('express')
const router = express.Router()
const async = require('../../utils/async')
const models = require('../../models')

const gameService = require('../../services/game-service')
const personaRepository = require('../../repositories/persona-repository')

//   /sala/adicionar?idPersona=XXX&sala=sala%20YYY&login=username

router.get('/adicionar',  
    async.handler(async (req, res) => {
      const personaId = req.query.idPersona
      const gameId = req.query.idJogo
      const roomName = req.query.sala

      let persona = await personaRepository.get(personaId)
      if (!persona)
			  throw Error("Persona not found: " + personaId)

      let game = gameId ? 
        await gameService.addPersona(gameId, persona) :
        await gameService.createGame(roomName, persona)
  	
      res.send("idJogo " + game.id + " username " + persona.user.name);
    })
);

module.exports = router;