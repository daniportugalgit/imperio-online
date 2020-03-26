const express = require('express');
const router = express.Router();

const async = require('../../utils/async');

router.get('/buscar',  
    async.handler(async (req, res) => {
		let id = req.params.idPersona;
		const academy = await authenticationService.login(req.params.username, req.params.password)      


    	let zeroes = "";

    	for(let i = 0; i < 60; i++) {
    		zeroes += "0 "
    	}

    	zeroes += "0"; //the 61st zero;

    	res.send("dadosAcademia dani " + zeroes);
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