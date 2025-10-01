'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('MODULE_PERMISSION', {
      id: {
        type: Sequelize.INTEGER,
        primaryKey: true,
        autoIncrement: true,
      },

      module_id: {
        type: Sequelize.INTEGER,
        allowNull: false,
        references: {
          key: 'id',
          model: 'MODULE',
        },
      },

      permission_id: {
        type: Sequelize.INTEGER,
        allowNull: false,
        references: {
          key: 'id',
          model: 'PERMISSION',
        },
      },

      code: {
        type: Sequelize.STRING,
        unique: true,
        allowNull: false,
      },

      is_active: { type: Sequelize.BOOLEAN, defaultValue: true },

      created_by: {
        type: Sequelize.STRING,
      },

      updated_by: {
        type: Sequelize.STRING,
      },

      created_at: {
        type: Sequelize.DATE,

        defaultValue: Sequelize.NOW,
      },
      updated_at: {
        type: Sequelize.DATE,
        defaultValue: Sequelize.NOW,
      },
    });
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable('MODULE_PERMISSION');
  },
};
