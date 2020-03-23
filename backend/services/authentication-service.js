const UserRepository = require("../repositories/user-repository")

class AuthenticationService {
	async login(username, password) {
		const userRepository = new UserRepository()
		const user = await userRepository.get(username)

		console.log(user);
		console.log(password);

		if (user == null)
			return null

		if(user.password != password)
			return null

		return user
	}
}

module.exports = AuthenticationService