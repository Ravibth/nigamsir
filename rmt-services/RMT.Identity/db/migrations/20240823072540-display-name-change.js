'use strict';
const {
  roleList,
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

    //Check if role already exists or not ?
    const roleToAdd = 'CEOCOO';
    const rolesToInsert = roleList.filter(
      (item) => item.role_name === roleToAdd,
    );

    const rows = await queryInterface.sequelize.query(
      'SELECT * FROM "ROLE" WHERE role_name = ? ',
      {
        replacements: [roleToAdd],
        type: queryInterface.sequelize.QueryTypes.SELECT,
      },
    );
    if (rows && rows.length > 0) {
      console.log('Data already exists.', roleToAdd);
    } else {
      await queryInterface.bulkInsert('ROLE', rolesToInsert, {});
    }

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
