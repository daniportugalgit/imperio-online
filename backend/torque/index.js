'use strict'

const express = require('express')
const api = express.Router()

const login = require('./login')
const persona = require('./persona')
const academia = require('./academia')
const sala = require('./sala')
const jogo = require('./jogo')

api.use('/login', login)
api.use('/persona', persona)
api.use('/academia', academia)
api.use('/sala', sala)
api.use('/jogo', jogo)

module.exports = api

//TODO
//Um arquivo que ajuda a compreender as respostas esperadas pelo Torque é o game/gameScripts/server/serverGetPost.cs

//Prioridade 1: Sala, Persona, Jogo,
//Prioridade 2: Pesquisas
//Prioridade 3: Tutorial, Guloks