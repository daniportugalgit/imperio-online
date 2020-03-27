const gameRepository = require('../repositories/game-repository')
const personaRepository = require('../repositories/persona-repository')
const models = require('../models');

class GameService {
	async createGame(roomName, persona) {
		let game = await models.game.create({
			userId: persona.user.id,
			roomName: roomName,
			participations: [this.buildParticipation(persona)]
		})

		return game
	}

	async addPersona(gameID, persona) {
		let game = await gameRepository.get(gameID)
		if (!game)
			throw Error("Game not found: " + gameID)

		let participations = game.participations.concat([this.buildParticipation(persona)])
		await game.update({participations: participations})
		
		return game
	}

	async endGame(gameId, participations, duration, planetId) {
		let game = await gameRepository.get(gameId)
		if (!game)
			throw Error("Game not found: " + gameId)

		let personas = await this.updateAllParticipantStats(participations)		

		await game.update({participations: participations, duration: duration, planetId: planetId})	

		return await this.createNextGame(game, personas)
	}
	
	async updateAllParticipantStats(participations) {
		let personas = []
		for (let i = 0; i < participations.length; i++) {
			let persona = await this.updateParticipantStats(participations[i])
			persona.save()
			personas.push(persona)
		}

		return personas
	}

	async updateParticipantStats(participation) {
		let persona = await personaRepository.get(participation.personaId)
		
		persona.credits += participation.credits
		persona.victories += participation.victory
		persona.attacked += participation.attacked
		persona.points += participation.points
		persona.sweeper += participation.sweeper
		persona.trader += participation.trader
		persona.visionary += participation.visionary

		return persona
	}

	async createNextGame(game, personas) {
		let next = await this.createGame(game.roomName, personas[0])

		for (let i = 1; i < personas.length; i++) {
			await this.addPersona(next.id, personas[i])
		}

		return next;
	}

	buildParticipation(persona) {
		return {
			personaId: persona.id
		}
	}
}

module.exports = new GameService()

require('make-runnable')