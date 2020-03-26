const gameRepository = require('../repositories/game-repository')
const personaRepository = require('../repositories/persona-repository')
const models = require('../models');

class GameService {
	async createGame(roomName, planetID, personaID) {
		let persona = await personaRepository.get(personaID)
		if (!persona)
			throw Error("Persona not found.")

		let game = models.game.create({
			userId: persona.user.id,
			planetId: planetID,
			roomName: roomName, 
			participations: [this.buildParticipation(persona)]
		})

		return game
	}

	async addPersona(gameID, personaID) {
		let persona = await personaRepository.get(personaID)
		if (!persona)
			throw Error("Persona not found: " + personaID)

		let game = await gameRepository.get(gameID)
		if (!game)
			throw Error("Game not found: " + gameID)

		game.participations.push(this.buildParticipation(persona))
		await game.update({participations: game.participations})
	}

	buildParticipation(persona) {
		return {
			personaId: persona.id,
			personaName: persona.name
		}
	}
}

module.exports = new GameService()

require('make-runnable')