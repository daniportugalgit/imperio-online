'use strict'

const express = require('express')
const api = express.Router()

const user = require('./user')
const ranking = require('./ranking')

api.use('/users', user)
api.use('/ranking', ranking)

module.exports = api
