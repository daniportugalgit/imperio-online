const models = require('../models');

class UserRepository {
	constructor() {
		this.user = models.user
	}

	async add(user) {
		return await this.user.create(user)
	}

	async get(username) {
		return await this.user.findOne({
			include: [{
			  model: models.persona
			}], 
			where: {
				name: username
			}
		})
	}
}

module.exports = new UserRepository()

