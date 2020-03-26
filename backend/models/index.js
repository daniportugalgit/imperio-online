'use strict';

const fs = require('fs')
const path = require('path')
const config = require('../config')
const Sequelize = require('sequelize')

class Models {
  static getDB() {
    const db = {};

    const d = config.database
    const sequelize = new Sequelize(d.database, d.username, d.password, d);
    const files = fs.readdirSync(__dirname).filter(file => {
        return (file.indexOf('.') !== 0) && (file !== path.basename(__filename)) && (file.slice(-3) === '.js');
    });
    
    files.forEach(file => {
        const model = sequelize.import(path.join(__dirname, file));
        db[model.name] = model;
    });
    
    Object.keys(db).forEach(modelName => {
        if (db[modelName].associate) {
            db[modelName].associate(db);
        }
    });
  
    db.sequelize = sequelize;
    db.Sequelize = Sequelize;    
    return db;
  }


}

module.exports = Models.getDB();


