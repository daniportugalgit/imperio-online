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

module.exports = router;