module.exports = {
    async login(req, res) {
        let username = req.params.login;
        let pass = req.params.senha;

        //res.send("username daniportug userId 10 omnis 0 conhece_g 0 qtde_personas 1 id1 99 nome1 Loki jogos1 0 vitorias1 0 pontos1 0 visionario1 0 arrebatador1 0 comerciante1 0 atacou1 0 creditos1 0 b_tutorial1 0 especie1 1 pk_fichas1 0 pk_vitorias1 0 pk_power_plays1 0 fim");
        res.send("username newUser userId 10 omnis 0 conhece_g 0 qtde_personas 0 fim");
    }
}