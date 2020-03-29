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

    adaptAcademy(academy) {
        let researches = [academy.aca_s_d_min, academy.aca_s_d_max, academy.aca_s_a_min, academy.aca_s_a_max, academy.aca_t_d_min, academy.aca_t_d_max, academy.aca_t_a_min, //7
                        academy.aca_t_a_max, academy.aca_n_d_min, academy.aca_n_d_max, academy.aca_n_a_min, academy.aca_n_a_max, //7
                        academy.aca_ldr_1_d_min, academy.aca_ldr_1_d_max, academy.aca_ldr_1_a_min, academy.aca_ldr_1_a_max, academy.aca_ldr_1_h1, academy.aca_ldr_1_h2, //6
                        academy.aca_ldr_1_h3, academy.aca_ldr_1_h4, academy.aca_ldr_2_d_min, academy.aca_ldr_2_d_max, academy.aca_ldr_2_a_min, academy.aca_ldr_2_a_max, //6
                        academy.aca_ldr_2_h1, academy.aca_ldr_2_h2, academy.aca_ldr_2_h3, academy.aca_ldr_2_h4, academy.aca_ldr_3_d_min, academy.aca_ldr_3_d_max, //6
                        academy.aca_ldr_3_a_min, academy.aca_ldr_3_a_max, academy.aca_ldr_3_h1, academy.aca_ldr_3_h2, academy.aca_ldr_3_h3, academy.aca_ldr_3_h4, //6
                        academy.aca_v_1, academy.aca_v_2, academy.aca_v_3, academy.aca_v_4, academy.aca_v_5, academy.aca_v_6, academy.aca_a_1, academy.aca_a_2, //8
                        academy.aca_c_1, academy.aca_d_1, academy.aca_i_1, academy.aca_i_2, academy.aca_i_3, academy.aca_pea_id, academy.aca_pea_min, academy.aca_pea_pet, //8
                        academy.aca_pea_ura, academy.aca_pea_ldr, academy.aca_av_1, academy.aca_av_2, academy.aca_av_3, academy.aca_av_4, academy.aca_pln_1, academy.aca_art_1, academy.aca_art_2] //9

        return researches.join(" ")
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

    adaptResponse(obj) {
        let response = ""
        for (const [key, value] of Object.entries(obj)) {
            response += key + " " + value + " "
        }
        return response + "fim"
    }
}

module.exports = new TaxoAdapter()