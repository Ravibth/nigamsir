'use strict';
const {
  roleModulePermissionMapping,
  moduleList,
  permissionList,
  modulePermissionList,
} = require('../data/module');
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    // Delete Data
    await queryInterface.bulkDelete('ROLE_MODULE_PERMISSION', null, {});
    await queryInterface.bulkDelete('MODULE_PERMISSION', null, {});
    await queryInterface.bulkDelete('MODULE', null, {});
    await queryInterface.bulkDelete('PERMISSION', null, {});
    // Insert Data
    await queryInterface.bulkInsert('PERMISSION', permissionList, {});
    await queryInterface.bulkInsert('MODULE', moduleList, {});
    await queryInterface.bulkInsert(
      'MODULE_PERMISSION',
      modulePermissionList(),
      {},
    );
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
