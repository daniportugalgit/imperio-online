const express = require('express');
const router = express.Router();

const async = require('../../utils/async');
const authenticationService = require("../../services/authentication-service")
const taxoAdapter = require("../../adapters/taxo-adapter")

router.get('/:username/:password',  
    async.handler(async (req, res) => {
        const user = await authenticationService.login(req.params.username, req.params.password)      

        if (user == null) {
        	res.send("NOK")
        	return
        }

        res.send(taxoAdapter.adaptLogin(user))
    })
);

module.exports = router;