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
				nome:"Loki",
				jogos:0,
				pontos:0,
				visionario:0,
				arrebatador:0,
				comerciante:0,
				atacou:0,
				creditos:0,
				b_tutorial:0,
				especie:"humano",
				pk_fichas:0,
				pk_vitorias:0,
				pk_power_plays:0
			}]
		}
	}
}

module.exports = UserRepository

