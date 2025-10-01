'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('ROLE', {
      id: {
        type: Sequelize.INTEGER,
        primaryKey: true,
        autoIncrement: true,
        unique: true,
      },

      role_name: {
        type: Sequelize.STRING,
        allowNull: false,
        unique: true,
      },

      display: {
        type: Sequelize.STRING,
        allowNull: false,
      },

      description: {
        type: Sequelize.STRING,
        allowNull: false,
      },

      is_active: { type: Sequelize.BOOLEAN, defaultValue: true },

      is_view_by_admin: { type: Sequelize.BOOLEAN, defaultValue: false },

      is_display: { type: Sequelize.BOOLEAN, defaultValue: true },

      created_by: {
        type: Sequelize.STRING,
        allowNull: false,
      },

      updated_by: {
        type: Sequelize.STRING,
        allowNull: false,
      },

      created_at: {
        type: Sequelize.DATE,
        allowNull: false,
        defaultValue: Sequelize.NOW,
      },
      updated_at: {
        type: Sequelize.DATE,
        allowNull: false,
        defaultValue: Sequelize.NOW,
      },
    });
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable('ROLE');
  },
};
