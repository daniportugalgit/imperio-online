const express = require('express');
const routes = express.Router();

const LoginController = require('./controllers/login');
const PersonaController = require('./controllers/persona');

routes.get('/torque/login/:username/:pass', LoginController.login);
routes.get('/torque/persona/criar', PersonaController.createPersona);

module.exports = routes;