class UserRepository {
	async get(username) {
		return {
			name:"dani",
			password:"123456",
			id:10,
			omnis:0,
			guloks:false,
			personas:[{
				id:99,
				name:"Loki",
				games:0,
				victories: 0,
				points:0,
				visionary:0,
				arrebatador:0,
				trader:0,
				attacked:0,
				credits:0,
				tutorial: false,
				species:"humano"
			}]
		}
	}
}

module.exports = new UserRepository()

