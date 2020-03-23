'use strict'

const express = require('express')
const api = express.Router()

const login = require('./login')
const persona = require('./persona')
const academia = require('./academia')

api.use('/login', login)
api.use('/persona', persona)
api.use('/academia', academia)

module.exports = api