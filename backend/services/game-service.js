const gameRepository = require('../repositories/game-repository')
const personaRepository = require('../repositories/persona-repository')
const models = require('../models');

class GameService {
	async addPersona(personaID, roomName) {
        persona = personaRepository.get(personaID)
		game = gameRepository.getByName(roomName) || models.game.Build({roomName: roomName})

		if (game.id == 0) {
            game.personas = [persona]
		}
		else {
			game.personas.push(persona)
        }
	}
}

module.exports = new GameService()