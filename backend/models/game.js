'use strict';
module.exports = (sequelize, DataTypes) => {
  const Game = sequelize.define('Game', {
    id: {
      allowNull: false,
      autoIncrement: true,
      primaryKey: true,
      type: DataTypes.INTEGER
    },
    user_id: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    room_name: {
      type: DataTypes.STRING,
      allowNull: false
    },
    finishedAt: {
      type: DataTypes.DATE,
      allowNull: false
    },
    duration: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    planet_id: {
      type: DataTypes.INTEGER,
      allowNull: false
    }
  }, {
    underscored: true,
    timestamps: false,
  });
  Game.associate = function(models) {
    this.belongsToMany(models.persona, { through: 'Personas_Games'})
  };
  return Game
};