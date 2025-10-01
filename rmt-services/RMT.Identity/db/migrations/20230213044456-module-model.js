'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    /**
     * Add altering commands here.
     *
     * Example:
     * await queryInterface.createTable('users', { id: Sequelize.INTEGER });
     */

    await queryInterface.createTable('MODULE', {
      id: {
        type: Sequelize.INTEGER,
        primaryKey: true,
        unique: true,
        autoIncrement: true,
      },

      module_name: {
        type: Sequelize.STRING,
        unique: true,
        allowNull: false,
      },

      module_display: {
        type: Sequelize.STRING,
        allowNull: false,
      },
      is_active: { type: Sequelize.BOOLEAN, defaultValue: true },
      is_display: { type: Sequelize.BOOLEAN, defaultValue: true },

      parent_id: { type: Sequelize.INTEGER },

      order: { type: Sequelize.INTEGER, allowNull: false },

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
    await queryInterface.dropTable('MODULE');
  },
};
