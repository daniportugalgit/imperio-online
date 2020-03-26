const userRepository = require('../repositories/user-repository')
const personaRepository = require('../repositories/persona-repository')
const models = require('../models')

class UserService {
	async create(user) {

		//TODO: Validate if user with same name already exists

		let created = await userRepository.add(user)
		created.password = null
		return created;
	}

	async createPersona(userID, name, species) {
		let user = await userRepository.get(userID)
		if (!user)
			throw Error("User not found.")

		let persona = models.persona.build({
			name: name, 
			species: species,
			userId: user.id
		})

		await personaRepository.add(persona)

		return persona
	}
}

module.exports = new UserService()