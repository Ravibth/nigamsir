/* eslint-disable @typescript-eslint/no-var-requires */
'use strict';

const { modulePermissionList } = require('../data/module');

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    //Commented data seeding need to assigne unique id as int todo by jay :done
    await queryInterface.bulkDelete('MODULE_PERMISSION', null, {});
    await queryInterface.bulkInsert(
      'MODULE_PERMISSION',

      modulePermissionList(),
      {},
    );
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('MODULE_PERMISSION', null, {});
  },
};
