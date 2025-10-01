'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('PERMISSION', {
      id: {
        type: Sequelize.INTEGER,

        primaryKey: true,
        unique: true,
        autoIncrement: true,
      },

      permission_name: {
        type: Sequelize.STRING,
        unique: true,
        allowNull: false,
      },
      is_active: { type: Sequelize.BOOLEAN, defaultValue: true },
    });
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable('PERMISSION');
  },
};
