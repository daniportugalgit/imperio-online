const express = require('express');

const config = require('./config');
  
const app = express();

app.get('/', (req, res) => {
    return res.send("Server online");
});

app.use(express.json());
app.use('/torque', require('./torque')); 

app.listen(config.server.port, () => {
    console.log("Server is up!");
});