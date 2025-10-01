/* eslint-disable @typescript-eslint/no-var-requires */
'use strict';

const { permissionList } = require('../data/module');

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    // await queryInterface.sequelize.query(`TRUNCATE TABLE "MODULE_PERMISSION" RESTART IDENTITY CASCADE`);
    //await queryInterface.sequelize.query(`TRUNCATE TABLE "PERMISSION" RESTART IDENTITY CASCADE`);
    await queryInterface.bulkDelete('PERMISSION', null, {});
    await queryInterface.bulkInsert('PERMISSION', permissionList, {});
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('PERMISSION', null, {});
  },
};
