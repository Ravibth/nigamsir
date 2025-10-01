"use strict";

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable("WORKFLOW_TASK", {
      id: {
        type: Sequelize.STRING,
        defaultValue: Sequelize.UUIDV4,
        primaryKey: true,
      },

      title: {
        type: Sequelize.STRING,
        allowNull: false,
      },
      workflow_id: {
        type: Sequelize.STRING,
        allowNull: false,
        references: {
          model: "WORKFLOW",
          key: "id",
        },
      },
      description: { type: Sequelize.STRING(500), allowNull: false },
      assigned_to: { type: Sequelize.STRING, allowNull: false },
assigned_to_userName: { type: Sequelize.STRING, allowNull: true },
      type: { type: Sequelize.STRING, allowNull: false },
      proxy_approval_by: { type: Sequelize.STRING },
      due_date: { type: Sequelize.DATE },
      status: { type: Sequelize.STRING, allowNull: false },
      comment: { type: Sequelize.STRING(500) },

      created_by: {
        type: Sequelize.STRING,
      },

      created_at: {
        type: Sequelize.DATE,
        defaultValue: Sequelize.NOW,
      },

      updated_by: { type: Sequelize.STRING },
      updated_at: { type: Sequelize.DATE, defaultValue: Sequelize.NOW },
    });
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable("WORKFLOW_TASK");
  },
};
