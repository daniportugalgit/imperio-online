const models = require('../models');

class GameRepository {
	constructor() {
		this.game = models.game
	}
}

module.exports = new GameRepository()

