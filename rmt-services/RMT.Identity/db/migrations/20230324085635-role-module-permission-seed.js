/* eslint-disable @typescript-eslint/no-var-requires */
'use strict';
const { roleModulePermissionMapping } = require('../data/module');

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    ////Commented data seeding ned to assigne unique id as int todo by jay 
    await queryInterface.bulkDelete('ROLE_MODULE_PERMISSION', null, {});
    await queryInterface.bulkInsert(
      'ROLE_MODULE_PERMISSION',
      roleModulePermissionMapping,
      {},
    );
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('ROLE_MODULE_PERMISSION', null, {});
  },
};
