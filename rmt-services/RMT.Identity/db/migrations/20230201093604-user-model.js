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

    await queryInterface.createTable('USERS', {
      id: {
        type: Sequelize.INTEGER,
        primaryKey: true,
        autoIncrement: true,
        unique: true,
      },

      role_ids: {
        type: Sequelize.STRING,
      },

      name: {
        type: Sequelize.STRING,
      },

      email_id: {
        type: Sequelize.STRING,
        allowNull: false,
        validate: {
          isEmail: true,
        },
      },

      entity: {
        type: Sequelize.STRING,
      },

      employee_id: {
        type: Sequelize.STRING,
      },

      emp_code: {
        type: Sequelize.STRING,
        unique: true,
      },

      fname: {
        type: Sequelize.STRING,
      },

      lname: {
        type: Sequelize.STRING,
      },

      designation: {
        type: Sequelize.STRING,
      },

      location: {
        type: Sequelize.STRING,
      },

      business_unit: {
        type: Sequelize.STRING,
      },
      expertise: {
        type: Sequelize.STRING,
      },

      smeg: {
        type: Sequelize.STRING,
      },

      supercoach_name: {
        type: Sequelize.STRING,
      },

      co_supercoach_name: {
        type: Sequelize.STRING,
      },

      service_line: {
        type: Sequelize.STRING,
      },

      roles: {
        type: Sequelize.STRING,
      },

      created_by: {
        type: Sequelize.STRING,
      },

      is_active: { type: Sequelize.BOOLEAN, defaultValue: true },

      status: { type: Sequelize.BOOLEAN, defaultValue: true },

      updated_by: {
        type: Sequelize.STRING,
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
      sector: {
        type: Sequelize.STRING,
        allowNull: true,
      },
      industry: {
        type: Sequelize.DATE,
        allowNull: true,
      },
    });

    await queryInterface.addConstraint('USERS', {
      fields: ['email_id'],
      name: 'user-email-unique',
      type: 'unique',
    });
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.removeConstraint('USERS', 'user-email-unique');
    await queryInterface.dropTable('USERS');
  },
};
