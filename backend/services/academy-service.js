'use strict'

const personaRepository = require('../repositories/persona-repository')
const AcademyResearcher = require('./academy-researcher')
const metrics = require("../utils/metrics")

class AcademyService {
	async startResearch(personaId, researchId, leaderId, credits) {
		let persona = await personaRepository.get(personaId)
		if (!persona)
			throw Error("Persona not found: " + personaId)

		let researcher = new AcademyResearcher(persona.academy)
		researcher.start(researchId, leaderId)

		await persona.update({academy: researcher.academy, credits: persona.credits - credits})
	}

	async finishResearch(personaId, researchId, credits) {
		let persona = await personaRepository.get(personaId)
		if (!persona)
			throw Error("Persona not found: " + personaId)

		let researcher = new AcademyResearcher(persona.academy)
		researcher.finish()

		await persona.update({academy: researcher.academy, credits: persona.credits - credits})

		metrics.endResearch.inc({research: researchId})
	}

	async investResearch(personaId, min, pet, ura, credits) {
		let persona = await personaRepository.get(personaId)
		if (!persona)
			throw Error("Persona not found: " + personaId)

		let researcher = new AcademyResearcher(persona.academy)
		researcher.invest(min, pet, ura)

		await persona.update({academy: researcher.academy, credits: persona.credits - credits})

		metrics.investResearch.inc({min: min, pet: pet, ura: ura, credits: credits})
	}
}

module.exports = new AcademyService()
