const express = require('express');
  
const App = express();

App.get('/', (req, res) => {
    return res.send("Server online");
});

App.use(express.json());
App.use(require('./routes'));

App.listen(80, () => {
    console.log("Server is up!");
});