"use strict";

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable("WORKFLOW", {
      id: {
        type: Sequelize.STRING,
        defaultValue: Sequelize.UUIDV4,
        primaryKey: true,
      },

      name: {
        type: Sequelize.STRING,
        allowNull: false,
        unique: "work_flow_unique",
      },
      module: {
        type: Sequelize.STRING,
        allowNull: false,
        unique: "work_flow_unique",
      },
      sub_module: {
        type: Sequelize.STRING,
        allowNull: false,
        unique: "work_flow_unique",
      },
      item_id: {
        type: Sequelize.STRING,
        allowNull: false,
        unique: "work_flow_unique",
      },

      parent_id: {
        type: Sequelize.STRING,
      },
      entity_type: { type: Sequelize.STRING, allowNull: true },
      entity_meta_data: { type: Sequelize.JSON, allowNull: true },
      outcome: { type: Sequelize.STRING, allowNull: false },
      status: { type: Sequelize.STRING, allowNull: false },
      created_by: { type: Sequelize.STRING, allowNull: false },
      created_at: { type: Sequelize.DATE, defaultValue: Sequelize.NOW },
      updated_by: { type: Sequelize.STRING },
      updated_at: { type: Sequelize.DATE, defaultValue: Sequelize.NOW },
      is_active: { type: Sequelize.BOOLEAN, defaultValue: true },
    });
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable("WORKFLOW");
  },
};
