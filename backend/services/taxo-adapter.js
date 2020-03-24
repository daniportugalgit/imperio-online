class TaxoAdapter {

    adaptLogin(user) {

        const personas = user.personas || []

        const login = {
            username: user.name,
            userId: user.id,
            omnis: user.omnis,
            conhece_g: user.guloks ? "1" : "0",
            qtde_personas: personas.length,
        }

        const species = {
            human: "humano",
            gulok: "gulok"
        }

        personas.forEach((p, i) => {
            const c = i + 1

            login["id"+c] = p.id
            login["nome"+c] = p.name
            login["jogos"+c] = p.games
            login["vitorias"+c] = p.victories
            login["pontos"+c] = p.points
            login["visionario"+c] = p.visionary
            login["arrebatador"+c] = p.sweeper
            login["comerciante"+c] = p.trader
            login["atacou"+c] = p.attacked
            login["creditos"+c] = p.credits
            login["b_tutorial"+c] = p.tutorial  ? "1" : "0"
            login["especie"+c] = species[p.species]
            login["pk_fichas"+c] = 0
            login["pk_vitorias"+c] = 0
            login["pk_power_plays"+c] = 0
        })

        return this.adaptResponse(login)
    }

    adaptResponse(obj) {
        let response = ""
        for (const [key, value] of Object.entries(obj)) {
            response += key + " " + value + " "
        }
        return response + "fim"
    }
}

module.exports = new TaxoAdapter()