const userRepository = require("../repositories/user-repository")

class AuthenticationService {
	async login(username, password) {
		const user = await userRepository.getByUsername(username)

		if (user == null)
			return null

		if (user.password != password)
			return null

		user.password = null
		return user
	}
}

module.exports = new AuthenticationService()