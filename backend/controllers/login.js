module.exports = {
    async login(req, res) {
        let username = req.params.login;
        let pass = req.params.senha;

        res.send("inicio username myUsername userId 1 omnis 0 conhece_g 0 qtde_personas 0 fim");
    }
}