const express = require('express')
const router = express.Router()
const async = require('../../utils/async')

const personaRepository = require('../../repositories/persona-repository')

router.get('/',  
    async.handler(async (req, res) => {
        let personas = await personaRepository.getAllForRanking()
        
        res.json(personas)
    })
);

module.exports = router