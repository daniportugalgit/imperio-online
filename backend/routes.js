const express = require('express');
const routes = express.Router();

const LoginController = require('./controllers/login');

routes.get('/torque/login/:username/:pass', LoginController.login);

module.exports = routes;