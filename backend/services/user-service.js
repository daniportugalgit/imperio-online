const userRepository = require('../repositories/user-repository')
const personaRepository = require('../repositories/persona-repository')
const AcademyResearcher = require('../services/academy-researcher')
const models = require('../models')

class UserService {
	async create(user) {
		//TODO: Validate if user with same name already exists

		let created = await userRepository.add(user)
		created.password = null
		return created;
	}

	async createPersona(userId, name, species) {
		let user = await userRepository.get(userId)
		if (!user)
			throw Error("User not found: " + userId)

		let persona = models.persona.build({
			name: name, 
			species: species,
			userId: user.id
		})

		let researcher = new AcademyResearcher()
		
		persona.user = user
		persona.academy = researcher.academy

		await personaRepository.add(persona)

		return persona
	}
}

module.exports = new UserService()