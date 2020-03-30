const express = require('express');
const router = express.Router();
const async = require('../../utils/async');

const personaRepository = require('../../repositories/persona-repository')


//   /torque/ranking
router.get('', 
    async.handler(async (req, res) => {
        console.log("BATEU!")

        let personas = await personaRepository.getAll()
        if (!personas)
		    throw Error("Personas not found")
        
        res.json(personas)
    })
);


module.exports = router;