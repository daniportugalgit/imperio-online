const userRepository = require("../repositories/user-repository")
const metrics = require("../utils/metrics")

class AuthenticationService {
	async login(username, password) {
		const user = await userRepository.getByUsername(username)

		if (user == null)
			return null

		if (user.password != password)
			return null

		metrics.login.inc()
		user.password = null
		return user
	}
}

module.exports = new AuthenticationService()