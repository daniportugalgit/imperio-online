const models = require('../models');

class PersonaRepository {
	constructor() {
		this.persona = models.persona
	}
}

module.exports = new PersonaRepository()

