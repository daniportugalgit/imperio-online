module.exports = {
    async createPersona(req, res) {
        let name = req.params.nome;
        let userId = req.params.idUsuario;
        let species = req.params.especie;

        //res.send("personaOK " + name + " " + userId + " " + species);
        //res.send("personaOK Loki 10 gulok");
        res.send("personaOK newUser 99 humano");
    }
}