'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('ROLE_MODULE_PERMISSION', {
      id: {
        type: Sequelize.INTEGER,
        primaryKey: true,
        autoIncrement: true,
      },

      role: {
        type: Sequelize.STRING,
        unique: 'role_module_permission',
        allowNull: false,
        references: {
          key: 'role_name',
          model: 'ROLE',
        },
      },

      module_permission_id: {
        type: Sequelize.STRING,
        unique: 'role_module_permission',
        allowNull: false,
        references: {
          key: 'code',
          model: 'MODULE_PERMISSION',
        },
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
    await queryInterface.dropTable('ROLE_MODULE_PERMISSION');
  },
};
