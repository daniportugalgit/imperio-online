const express = require('express')
const router = express.Router()
const async = require('../../utils/async')
const models = require('../../models')

const gameService = require('../../services/game-service')

//   /sala/adicionar?idPersona=XXX&sala=sala%20YYY&login=username

router.get('/adicionar',  
    async.handler(async (req, res) => {

		gameService.addPersona(req.params.idPersona, req.params.sala)
		

		
		res.send("dadosAcademia dani " + zeroes);
    })
);

module.exports = router;