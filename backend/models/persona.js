'use strict';

module.exports = (sequelize, DataTypes) => {
  const Persona = sequelize.define('persona', {
    id: {
      type: DataTypes.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    name: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    games: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },
    victories: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },
    points: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },
    visionary: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },
    sweeper: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },  
    trader: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },     
    attacked: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },     
    credits: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },     
    tutorial: {
      type: DataTypes.BOOLEAN,
      allowNull: false,
      defaultValue: false
    },     
    species: {
      type: DataTypes.STRING,
      allowNull: false,
      defaultValue: "human"
    },                     
  }, {
    underscored: true,
    timestamps: false,
  });
  Persona.associate = function(models) {
    this.belongsTo(models.user)
  };
  return Persona;
};
