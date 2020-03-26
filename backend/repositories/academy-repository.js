class AcademyRepository {
	constructor() {
		this.academy = models.academy
	}

	async get(personaId) {
		return await this.academy.findOne({
			where: {
				persona_id: personaId // isso parece errado, eu não criei uma foreign key na mão! Como sei como encontrá-la?
			}
		})
	}
}

module.exports = new AcademyRepository()

