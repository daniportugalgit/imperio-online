const express = require('express')
const parser = require('body-parser')
const morgan = require('morgan')
const config = require('./config')
  
const app = express()

const prometheus = require('./utils/metrics');

app.get('/', (req, res) => {
    return res.send("Server online")
})

app.use(prometheus.middleware)
app.use(express.json())
app.use(parser.json())
app.use(morgan('tiny'))
app.use('/torque', require('./torque')) 
app.use('/api', require('./api'))

app.listen(config.server.port, () => {
    console.log("Server is up and listening on port " + config.server.port + ".")
});