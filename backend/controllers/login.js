module.exports = {
    async login(req, res) {
        let username = req.params.login;
        let pass = req.params.senha;

        res.send("inicio username 1 0 0 0 fim");
    }
}