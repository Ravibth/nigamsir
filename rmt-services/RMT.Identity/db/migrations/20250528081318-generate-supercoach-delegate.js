'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    /**
     * Add altering commands here.
     *
     * Example:
     * await queryInterface.createTable('users', { id: Sequelize.INTEGER });
     */
        queryInterface.createTable('SUPERCOACH_DELEGATES', {
      id: {
        type: Sequelize.UUID,
        primaryKey: true,
        allowNull: false,
      },
      supercoach_mid: {
        type: Sequelize.STRING,
        allowNull: false,
      },
      allocation_delegate_mid: {
        type: Sequelize.STRING,
        allowNull: true,
      },
      allocation_delegate_email: {
        type: Sequelize.STRING,
        allowNull: true,
      },
      allocation_delegate_name: {
        type: Sequelize.STRING,
        allowNull: true,
      },
      skill_delegate_mid: {
        type: Sequelize.STRING,
        allowNull: true,
      },
      skill_delegate_email: {
        type: Sequelize.STRING,
        allowNull: true,
      },
      skill_delegate_name: {
        type: Sequelize.STRING,
        allowNull: true,
      },
      created_at: {
        type: Sequelize.DATE,
        allowNull: false,
      },
      updated_at: {
        type: Sequelize.DATE,
        allowNull: true,
      },
      created_by: {
        type: Sequelize.STRING,
        allowNull: false,
      },
      updated_by: {
        type: Sequelize.STRING,
        allowNull: true,
      },
    });
  },

  async down (queryInterface, Sequelize) {
    /**
     * Add reverting commands here.
     *
     * Example:
     * await queryInterface.dropTable('users');
     */
  }
};
