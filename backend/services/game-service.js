const gameRepository = require('../repositories/game-repository')
const personaRepository = require('../repositories/persona-repository')
const models = require('../models');

class GameService {
	async createGame(roomName, persona) {
		let game = await models.game.create({
			userId: persona.user.id,
			roomName: roomName,
			participations: {}
		})

		let created = await gameRepository.get(game.id)
		created.participations[persona.id] = this.buildParticipation(persona)
		await created.update({participations: created.participations})

		return created
	}

	async addPersona(gameID, persona) {
		let game = await gameRepository.get(gameID)
		if (!game)
			throw Error("Game not found: " + gameID)

		game.participations[persona.id] = this.buildParticipation(persona)
		await game.update({participations: game.participations})
		
		return game
	}

	async endGame(gameId, participations, duration, planetId) {
		let game = await gameRepository.get(gameId)
		if (!game)
			throw Error("Game not found: " + gameId)

		let gameParticipations = game.participations
		for (const [key, value] of Object.entries(gameParticipations)) {
			game.participations[key] = participations[key]
			game.participations[key].personaName = value.personaName //Aqui tá dando pau no finalizar jogo
		}
		
		console.log(game.participations)
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