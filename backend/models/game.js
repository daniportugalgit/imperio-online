JsonField = require('sequelize-json'),

'use strict';
module.exports = (sequelize, DataTypes) => {
  const Game = sequelize.define('game', {
    id: {
      allowNull: false,
      autoIncrement: true,
      primaryKey: true,
      type: DataTypes.INTEGER
    },
    participations: JsonField(sequelize, 'game', 'participations'),
    userId: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    roomName: {
      type: DataTypes.STRING,
      allowNull: false
    },
    started_at: {
      type: DataTypes.DATE
    },
    finished_at: {
      type: DataTypes.DATE
    },
    duration: {
      type: DataTypes.INTEGER
    },
    planetId: {
      type: DataTypes.INTEGER,
      allowNull: false
    }
  }, {
    underscored: true,
    timestamps: false,
  });
  Game.associate = function(models) {
    //this.hasOne()    
  };
  return Game
};