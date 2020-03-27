const express = require('express')
const parser = require('body-parser');
const config = require('./config')
  
const app = express()

app.get('/', (req, res) => {
    return res.send("Server online")
})

app.use(express.json())
app.use(parser.json())
app.use('/torque', require('./torque')) 
app.use('/api', require('./api'))

app.listen(config.server.port, () => {
    console.log("Server is up and listening on port " + config.server.port + ".")
});