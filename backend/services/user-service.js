const userRepository = require('../repositories/user-repository')

class UserService {
	async create(user) {

		//TODO: Validate if user with same name already exists

		let created = await userRepository.add(user)
		created.password = null
		return created;
	}
}

module.exports = new UserService()