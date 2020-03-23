module.exports = {
    async createPersona(req, res) {
        let name = req.params.nome; //esse cara vai pro banco de dados, é o nome da persona
        let userId = req.params.idUsuario; //para associar a persona ao user correto no banco
        let species = req.params.especie; //pode ser "humano" ou "gulok" ou ""; se for "" é humano.

        //res.send("personaOK " + username + " " + personaId + " " + species);
        res.send("personaOK dani 99 humano"); //o nome da persona não é retornado ao client; está correto.
    },

    async getAcademy(req, res) {
    	let id = req.params.idPersona;
    	let zeroes = "";

    	for(let i = 0; i < 60; i++) {
    		zeroes += "0 "
    	}

    	zeroes += "0"; //the 61th zero;

    	res.send("dadosAcademia dani " + zeroes);
    }
}

