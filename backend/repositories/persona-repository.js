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

	async getAllForRanking() {
		return await this.persona.findAll({attributes: ['name', 'games', 'victories', 'points', 'visionary', 'sweeper', 'trader', 'attacked', 'species']})
	}
}

module.exports = new PersonaRepository()
