'use strict';

module.exports = (sequelize, DataTypes) => {
  const User = sequelize.define('user', {
    id: {
      type: DataTypes.INTEGER,
      autoIncrement: true,
      primaryKey: true
    },
    name: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    password: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    omnis: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },
    guloks: {
      type: DataTypes.BOOLEAN,
      allowNull: false,
      defaultValue: false
    },    
  }, {
    underscored: true,
    timestamps: false,
  });
  User.associate = function(models) {
    this.hasMany(models.persona)
  };

  return User;
};