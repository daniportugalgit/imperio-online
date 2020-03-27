class TaxoAdapter {

    translateSpecies(species) {
        const map = {
            human: "humano",
            gulok: "gulok"
        }

        return map[species]
    }

    translateEspecie(especie) {
        const map = {
            humano: "human",
            gulok: "gulok"
        }

        return map[especie]
    }

    translateBoolean(b) {
        const map = {
            true: "1",
            false: "0"
        }

        return map[b]
    }

    translateTermino(termino) {
        const map = {
            F: "finished",
            S: "suicided",
            R: "surrended"
        }

        return map[termino]
    }

    adaptLogin(user) {

        const personas = user.personas || []

        const login = {
            username: user.name,
            userId: user.id,
            omnis: user.omnis,
            conhece_g: this.translateBoolean(user.guloks),
            qtde_personas: personas.length
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
            login["b_tutorial"+c] = this.translateBoolean(p.tutorial)
            login["especie"+c] = this.translateSpecies(p.species)
            login["pk_vitorias"+c] = 0
            login["pk_fichas"+c] = 0
            login["pk_power_plays"+c] = 0
        })

        return this.adaptResponse(login)
    }

    adaptAcademy(persona) {
        /*
        const academy = persona.academy || {} // || new Academy()?

        const response = "dadosAcademia " + {persona.user.name} + " ";
        for (const [key, value] of Object.entries(obj)) {
            response += " " + value
        }
        
        return this.adaptAcademyResponse(response)
        //resposta esperada:
        //dadosAcademia {username} {...}
        //ESTE NÃO TERMINA COM 'fim'
        */
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