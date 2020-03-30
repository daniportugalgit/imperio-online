const models = require('../models');

class PersonaRepository {
	constructor() {
		this.persona = models.persona
	}

	async get(id) {
		return await this.persona.findByPk(id, { 
			include: [{
			  model: models.user
			}]
		})
	}

	async add(persona) {
		await persona.save()
	}

	async getAll() {
		return await this.persona.findAll()
	}
}

module.exports = new PersonaRepository()

