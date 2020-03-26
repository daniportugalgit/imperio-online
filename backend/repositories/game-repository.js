const models = require('../models');

class GameRepository {
	constructor() {
		this.game = models.game
	}

	async get(id) {
		return await this.game.findByPk(id)
	}
}

module.exports = new GameRepository()

