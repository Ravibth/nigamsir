/* eslint-disable @typescript-eslint/no-var-requires */
'use strict';

const { moduleList } = require('../data/module');

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    // await queryInterface.sequelize.query(`TRUNCATE TABLE "MODULE"`);
    await queryInterface.bulkDelete('MODULE', null, {});
    await queryInterface.bulkInsert('MODULE', moduleList, {});
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('MODULE', null, {});
  },
};
