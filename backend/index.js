const express = require('express');
  
const app = express();

app.get('/', (req, res) => {
    return res.send("Server online");
});

app.use(express.json());
app.use('/torque', require('./torque')); 

app.listen(80, () => {
    console.log("Server is up!");
});