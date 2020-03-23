const AuthenticationService = require("../services/authentication-service")

module.exports = {
    async login(req, res) {
        let username = req.params.username
        let password = req.params.pass

        const authenticationService = new AuthenticationService()
        const user = await authenticationService.login(username, password)      

        if (user == null) {
        	res.send("NOK")
        	return
        }

        //res.send("username dani userId 10 omnis 0 conhece_g 0 qtde_personas 1
        //id1 99 nome1 Loki jogos1 0 vitorias1 0 pontos1 0 visionario1 0 arrebatador1 0
        //comerciante1 0 atacou1 0 creditos1 0 b_tutorial1 0 especie1 1 pk_fichas1 0
        //pk_vitorias1 0 pk_power_plays1 0 fim");
        res.send(adapt(user))
    }
}

function adapt(user) {
	let result = "username " + user.name + " userId " + user.id
	
	result += " omnis " + user.omnis
	
	result += " conhece_g " + (user.guloks ? "1":"0")
	
	result += " qtde_personas " + user.personas.length + " "
	
	let count = 1;
	user.personas.forEach((persona) => {
		Object.keys(persona).forEach((key) => { 
			result += key + count + " " + persona[key] + " "
		})

		count++
	})

	result += "fim"

	return result
}

