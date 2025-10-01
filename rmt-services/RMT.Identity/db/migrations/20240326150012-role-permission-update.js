'use strict';
const { roleModulePermissionMapping } = require('../data/module');
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('ROLE_MODULE_PERMISSION', null, {});
    await queryInterface.bulkInsert(
      'ROLE_MODULE_PERMISSION',
      roleModulePermissionMapping,
      {},
    );
  },

  async down(queryInterface, Sequelize) {
    /**
     * Add reverting commands here.
     *
     * Example:
     * await queryInterface.dropTable('users');
     */
  },
};
