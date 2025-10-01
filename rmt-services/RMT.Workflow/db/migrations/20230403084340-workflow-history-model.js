'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('WORKFLOW_HISTORY', {
      id: {
        type: Sequelize.STRING,
        defaultValue: Sequelize.UUIDV4,
        primaryKey: true,
      },
      action: {
        type: Sequelize.STRING,
        allowNull: false,
      },

      workflow_id: {
        type: Sequelize.STRING,
        allowNull: false,
        references: {
          model: 'WORKFLOW',
          key: 'id',
        },
      },

      workflow_task_id: {
        type: Sequelize.STRING,
        references: {
          model: 'WORKFLOW_TASK',
          key: 'id',
        },
      },

      comments: {
        type: Sequelize.STRING(500),
      },

      created_by: {
        type: Sequelize.STRING,
      },

      created_at: {
        type: Sequelize.DATE,
        defaultValue: Sequelize.NOW,
      },
    });
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable('WORKFLOW_HISTORY');
  },
};
