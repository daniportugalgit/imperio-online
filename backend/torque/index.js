'use strict'

const express = require('express')
const api = express.Router()

const login = require('./login')
const persona = require('./persona')
const academia = require('./academia')
const sala = require('./sala')
const jogo = require('./jogo')
const ranking = require('./ranking')

api.use('/login', login)
api.use('/persona', persona)
api.use('/academia', academia)
api.use('/sala', sala)
api.use('/jogo', jogo)
api.use('/ranking', ranking)

module.exports = api

//TODO:
//Guloks, Aterfatos