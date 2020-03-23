const express = require('express');
const router = express.Router();

const async = require('../../utils/async');

router.get('/buscar',  
    async.handler(async (req, res) => {
        let id = req.params.idPersona;
    	let zeroes = "";

    	for(let i = 0; i < 60; i++) {
    		zeroes += "0 "
    	}

    	zeroes += "0"; //the 61st zero;

    	res.send("dadosAcademia dani " + zeroes);
    })
);

module.exports = router;