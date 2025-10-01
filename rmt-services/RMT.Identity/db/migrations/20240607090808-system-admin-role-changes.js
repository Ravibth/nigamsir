'use strict';
const { roleList } = require('../data/module');
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    /**
     * Add altering commands here.
     *
     * Example:
     * await queryInterface.createTable('users', { id: Sequelize.INTEGER });
     */
    // Insert Data

    //Check if role already exists or not ?
    const roleToAdd = 'SystemAdmin';
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
