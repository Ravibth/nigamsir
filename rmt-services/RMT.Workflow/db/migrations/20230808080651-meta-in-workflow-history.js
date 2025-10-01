"use strict";

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    const table = "WORKFLOW_HISTORY";

    await queryInterface.sequelize.query(
      `ALTER TABLE "${table}" ADD  "meta" json`
    );
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.removeColumn("WORKFLOW_HISTORY", "meta");
  },
};
