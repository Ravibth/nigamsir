'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable(
      'USER_ROLE',

      {
        id: {
          type: Sequelize.STRING,
          primaryKey: true,
          defaultValue: Sequelize.UUIDV4,
        },

        user: {
          type: Sequelize.STRING,
          allowNull: false,

          references: {
            model: 'USERS',
            key: 'email_id',
          },
        },

        role: {
          type: Sequelize.STRING,
          allowNull: false,
          references: {
            model: 'ROLE',
            key: 'role_name',
          },
        },

        is_active: {
          type: Sequelize.BOOLEAN,
          defaultValue: true,
        },

        created_by: {
          type: Sequelize.STRING,
          allowNull: false,
        },

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
      },
    );
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable('USER_ROLE');
  },
};
