'use strict';
module.exports = (sequelize, DataTypes) => {
  const persona_game = sequelize.define('persona_game', {
    credits: {
      allowNull: false,
      type: DataTypes.INTEGER
    },
    has_won: {
      allowNull: false,
      type: DataTypes.BOOLEAN
    },
    points: {
      allowNull: false,
      type: DataTypes.INTEGER
    },
    visionary: {
      allowNull: false,
      type: DataTypes.BOOLEAN
    },
    has_attacked: {
      allowNull: false,
      type: DataTypes.BOOLEAN
    },
    sweeper: {
      allowNull: false,
      type: DataTypes.BOOLEAN
    },
    trader: {
      allowNull: false,
      type: DataTypes.BOOLEAN
    },
    starting_region_id: {
      allowNull: false,
      type: DataTypes.INTEGER
    },
    objective1_id: {
      allowNull: false,
      type: DataTypes.INTEGER
    },
    objective2_id: {
      allowNull: false,
      type: DataTypes.INTEGER
    },
    kill_count: {
      allowNull: false,
      type: DataTypes.INTEGER
    },
    power_play: {
      allowNull: false,
      type: DataTypes.INTEGER
    },
    end_game_status: {
      allowNull: false,
      type: DataTypes.STRING
    }
  }, {
    underscored: true,
    timestamps: false
  })

  return persona_game
};