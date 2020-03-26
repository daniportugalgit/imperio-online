const models = require('../models');

class PersonaRepository {
	constructor() {
		this.persona = models.persona
	}

	async get(id) {
		return await this.user.findOne({
			where: {
				id: id
			}
		})
	}
}

module.exports = new PersonaRepository()

