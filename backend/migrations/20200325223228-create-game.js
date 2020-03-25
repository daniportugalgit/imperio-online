'use strict';
module.exports = {
  up: (queryInterface, Sequelize) => {
    return queryInterface.createTable('Games', {
      id: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.DataTypes.INTEGER
      },
      user_id: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false
      },
      room_name: {
        type: Sequelize.DataTypes.STRING,
        allowNull: false
      },
      finishedAt: {
        type: Sequelize.DataTypes.DATE,
        allowNull: false
      },
      duration: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false
      },
      planet_id: {
        type: Sequelize.DataTypes.INTEGER,
        allowNull: false
      }
    });
  },
  down: (queryInterface, Sequelize) => {
    return queryInterface.dropTable('Games');
  }
};