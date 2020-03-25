'use strict';

module.exports = {
  up: (queryInterface, Sequelize) => {
    return queryInterface.createTable('Users', {
      id: {
      type: Sequelize.DataTypes.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    name: {
      type: Sequelize.DataTypes.STRING,
      allowNull: false,
    },
    password: {
      type: Sequelize.DataTypes.STRING,
      allowNull: false,
    },
    omnis: {
      type: Sequelize.DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },
    guloks: {
      type: Sequelize.DataTypes.BOOLEAN,
      allowNull: false,
    },    
    });
  },
  down: (queryInterface, Sequelize) => {
    return queryInterface.dropTable('Users')
  }
};
