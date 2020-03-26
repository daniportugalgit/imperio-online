const express = require('express')
const router = express.Router()
const async = require('../../utils/async')

const userService = require('../../services/user-service')

router.post('/',  
    async.handler(async (req, res) => {
		const user = req.body
		created = await userService.create(user)

    	res.status(201).json(created)
    })
);

module.exports = router