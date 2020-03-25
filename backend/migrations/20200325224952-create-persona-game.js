'use strict';
module.exports = {
  up: (queryInterface, Sequelize) => {
    return queryInterface.createTable('Personas_Games', {
      game_id: {
        allowNull: false,
        type: Sequelize.INTEGER
      },
      persona_id: {
        allowNull: false,
        type: Sequelize.INTEGER
      },
      credits: {
        allowNull: false,
        type: Sequelize.INTEGER
      },
      has_won: {
        allowNull: false,
        type: Sequelize.BOOLEAN
      },
      points: {
        allowNull: false,
        type: Sequelize.INTEGER
      },
      visionary: {
        allowNull: false,
        type: Sequelize.BOOLEAN
      },
      has_attacked: {
        allowNull: false,
        type: Sequelize.BOOLEAN
      },
      sweeper: {
        allowNull: false,
        type: Sequelize.BOOLEAN
      },
      trader: {
        allowNull: false,
        type: Sequelize.BOOLEAN
      },
      starting_region_id: {
        allowNull: false,
        type: Sequelize.INTEGER
      },
      objective1_id: {
        allowNull: false,
        type: Sequelize.INTEGER
      },
      objective2_id: {
        allowNull: false,
        type: Sequelize.INTEGER
      },
      kill_count: {
        allowNull: false,
        type: Sequelize.INTEGER
      },
      power_play: {
        allowNull: false,
        type: Sequelize.INTEGER
      },
      end_game_status: {
        allowNull: false,
        type: Sequelize.STRING
      }
    })
  },
  down: (queryInterface, Sequelize) => {
    return queryInterface.dropTable('persona_games')
  }
};