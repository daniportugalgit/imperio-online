const express = require('express');
const router = express.Router();
const async = require('../../utils/async');

const taxoAdapter = require("../../services/taxo-adapter")
const userService = require('../../services/user-service')

router.get('/criar',  
    async.handler(async (req, res) => {
        let nome = req.query.nome;
        let idUsuario = req.query.idUsuario;
        let especie = req.query.especie;

        let persona = await userService.createPersona(idUsuario, nome, taxoAdapter.translateEspecie(especie))

        res.send("personaOK " + persona.user.name + " " + persona.id + " " + especie)
    })
);

//   /persona/tutorial?idPersona= @ %persona.taxoId @ "&creditos=5&idUsuario=" @ %persona.user.taxoId;
router.get('/tutorial',  
    async.handler(async (req, res) => {
        const personaId = req.query.idPersona
        const credits = req.query.creditos
        
        let persona = await personaRepository.get(personaId)
        if (!persona)
		    throw Error("Persona not found: " + personaId)

        await persona.update({credits: persona.credits + credits})

        res.send("personaTutorial " + persona.user.name)
    })
);


module.exports = router;