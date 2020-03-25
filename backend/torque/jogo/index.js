const express = require('express');
const router = express.Router();

const async = require('../../utils/async');

router.get('/finalizar',  
    async.handler(async (req, res) => {
        let id = req.params.idJogo;
        //pega o jogo no GameRepository
        //grava o que veio do Torque
        //descobre qual é o próximo GameID para passar pro Torque
        let nextId = id + 1;

    	res.send("idJogo " + id + " " + nextId);
    })
);

module.exports = router;