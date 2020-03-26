const express = require('express');
const router = express.Router();
const async = require('../../utils/async');

const taxoAdapter = require("../../services/taxo-adapter")
const userService = require('../../services/user-service')

router.get('/criar',  
    async.handler(async (req, res) => {
        let nome = req.params.nome;
        let idUsuario = req.params.idUsuario;
        let especie = req.params.especie;

        let persona = await userService.createPersona(nome, idUsuario, taxoAdapter.translateEspecie(especie))

        res.send("personaOK " + persona.user.name + " " + persona.id + " " + species)
    })
);

module.exports = router;