'use strict';
module.exports = (sequelize, DataTypes) => {
  const Academy = sequelize.define('academy', {
    soldierDefenseMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    soldierDefenseMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 6,
    },
    soldierAttackMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    soldierAttackMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 6,
    },
    
    tankDefenseMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    tankDefenseMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 12,
    },
    tankAttackMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    tankAttackMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 12,
    },

    shipDefenseMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    shipDefenseMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 12,
    },
    shipAttackMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    shipAttackMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 12,
    },

    leader1DefenseMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    leader1DefenseMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 12,
    },
    leader1AttackMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    leader1AttackMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 12,
    },
    leader1Skill1: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    leader1Skill2: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    leader1Skill3: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    leader1Skill4: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    
    leader2DefenseMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    leader2DefenseMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 12,
    },
    leader2AttackMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    leader2AttackMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 12,
    },
    leader2Skill1: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    leader2Skill2: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    leader2Skill3: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    leader2Skill4: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },

    leader3DefenseMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    leader3DefenseMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 12,
    },
    leader3AttackMin: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 1,
    },
    leader3AttackMax: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 12,
    },
    leader3Skill1: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    leader3Skill2: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    leader3Skill3: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    leader3Skill4: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },

    visionary1: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    visionary2: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    visionary3: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    visionary4: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    visionary5: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    visionary6: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },

    sweeper1: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    sweeper2: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },

    trader: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    diplomat: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },

    intel1: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    intel2: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    intel3: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },

    ongoingId: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    ongoingOre: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    ongoingOil: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    ongoingUranium: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    ongoingLeader: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },

    advanced1: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    advanced2: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    advanced3: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    advanced4: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },

    planets: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    artifact1: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    artifact2: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
  }, {
    underscored: true,
    timestamps: false,
  });
  
  return Academy;
};