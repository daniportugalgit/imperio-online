const express = require('express');
const router = express.Router();
const async = require('../../utils/async');

const taxoAdapter = require('../../services/taxo-adapter')
const personaRepository = require('../../repositories/persona-repository')
const academyService = require('../../services/academy-service')

router.get('/buscar',  
    async.handler(async (req, res) => {
		let persona = await personaRepository.get(req.query.idPersona)
		if (!persona)
			throw Error("Persona not found: " + req.query.idPersona)

    	res.send("dadosAcademia " + persona.user.name + " " + taxoAdapter.adaptAcademy(persona.academy));
    })
);

router.get('/iniciar',  
    async.handler(async (req, res) => {
		await academyService.startResearch(req.query.idPersona, req.query.pesq, req.query.lider, -req.query.creditos)
		
    	res.send("academiaOK " + req.query.idPesqTorque);
    })
);

router.get('/finalizar',  
    async.handler(async (req, res) => {
		await academyService.finishResearch(req.query.idPersona, req.query.pesq, -req.query.creditos)
		
    	res.send("academiaOK " + req.query.idPesqTorque);
    })
);

router.get('/investir',  
    async.handler(async (req, res) => {
		await academyService.investResearch(req.query.idPersona, req.query.min, req.query.pet, req.query.ura, -req.query.creditos)
		
    	res.send("academiaOK " + req.query.idPesqTorque);
    })
);

module.exports = router;

