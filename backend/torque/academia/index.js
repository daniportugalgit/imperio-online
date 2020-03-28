const express = require('express');
const router = express.Router();
const async = require('../../utils/async');

const taxoAdapter = require('../../services/taxo-adapter')
const personaRepository = require('../../repositories/persona-repository')
const AcademyResearcher = require('../../services/academy-researcher')

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
		// let builder = new AcademyManager(persona.academy)

		// builder.finishResearch('aca...'')
		
		// models.persona.update({academy: manager.academy})
		
    	res.send("dadosAcademia " + persona.user.name + " " + zeroes);
    })
);

module.exports = router;


async.handler(async (req, res) => {
	const user = await authenticationService.login(req.params.username, req.params.password)      

	if (user == null) {
		res.send("NOK")
		return
	}

	res.send(taxoAdapter.adaptLogin(user))
})