'use strict';

const { roleList } = require('../data/module');

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('ROLE', null, {});
    await queryInterface.bulkInsert('ROLE', roleList, {});
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.bulkDelete('ROLE', null, {});
  },
};
