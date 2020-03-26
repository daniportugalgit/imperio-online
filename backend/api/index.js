'use strict'

const express = require('express')
const api = express.Router()

const user = require('./user')

api.use('/users', user)

module.exports = api
