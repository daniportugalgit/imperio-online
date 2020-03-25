'use strict';

module.exports = {
  up: (queryInterface, Sequelize) => {
    return queryInterface.createTable('Personas', {
      id: {
        type: Sequelize.DataTypes.INTEGER,
        autoIncrement: true,
        primaryKey: true
      },
      user_id: {
        type: Sequelize.DataTypes.INTEGER
      },
      academy_id: {
        type: Sequelize.DataTypes.INTEGER
      },
      name: {
        type: Sequelize.DataTypes.STRING,
        allowNull: false
      },
      games: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false,
        defaultValue: 0
      },
      victories: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false,
        defaultValue: 0
      },
      points: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false,
        defaultValue: 0
      },
      visionary: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false,
        defaultValue: 0
      },
      sweeper: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false,
        defaultValue: 0
      },  
      trader: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false,
        defaultValue: 0
      },     
      attacked: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false,
        defaultValue: 0
      },     
      credits: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false,
        defaultValue: 0
      },     
      tutorial: {
        type: Sequelize.DataTypes.BOOLEAN,
        allowNull: false,
        defaultValue: false
      },     
      species: {
        type: Sequelize.DataTypes.STRING,
        allowNull: false,
        defaultValue: "human"
      },   
    });
  },

  down: (queryInterface, Sequelize) => {
    return queryInterface.dropTable('Personas')
  }
};
