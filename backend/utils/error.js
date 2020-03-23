
class Error {
    static handler(err, req, res, next) {
        console.log(err)
    }
}

module.exports = Error;