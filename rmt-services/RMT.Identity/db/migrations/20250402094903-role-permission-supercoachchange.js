'use strict';
const {
  roleModulePermissionMapping,
  moduleList,
  permissionList,
  modulePermissionList,roleList
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
    await queryInterface.bulkInsert('ROLE', [
      {
        id: 13,
        role_name: 'SkillSuperCoach',
        display: 'SkillSuperCoach',
        description: 'SkillSuperCoach',
        is_view_by_admin: true,
        is_active: true,
        is_display: true,
        created_by: 'System Update',
        created_at: '2024-10-10T00:00:00Z',
        updated_by: 'System Update',
        updated_at: '2024-10-10T00:00:00Z',
      },
    ]);
 
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
