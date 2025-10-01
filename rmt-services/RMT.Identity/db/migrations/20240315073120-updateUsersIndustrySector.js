'use strict';
 
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    // Check if the 'industry' column exists before attempting to remove it
    const industryExists = await queryInterface.sequelize.query(
      `SELECT * FROM information_schema.columns WHERE table_name = 'USERS' AND column_name = 'industry'`
    );
    if (industryExists[0].length > 0) {
      await queryInterface.removeColumn('USERS', 'industry');
    }
 
    // Check if the 'sector' column exists before attempting to remove it
    const sectorExists = await queryInterface.sequelize.query(
      `SELECT * FROM information_schema.columns WHERE table_name = 'USERS' AND column_name = 'sector'`
    );
    if (sectorExists[0].length > 0) {
      await queryInterface.removeColumn('USERS', 'sector');
    } 
   
    // Add the new columns
    await queryInterface.addColumn('USERS', 'industry', {
      type: Sequelize.STRING,
      allowNull: true,
    });

    const sub_industryExists = await queryInterface.sequelize.query(
      `SELECT * FROM information_schema.columns WHERE table_name = 'USERS' AND column_name = 'sub_industry'`
    );
    if (sub_industryExists[0].length == 0) {
      await queryInterface.addColumn('USERS', 'sub_industry', {
        type: Sequelize.STRING,
        allowNull: true,
      });
    }
 

   
  },
 
  async down(queryInterface, Sequelize) {
    // This function should be implemented to revert the changes made in the 'up' function.
    // You will need to add appropriate 'queryInterface.addColumn' commands to revert the changes.
    // Example:
    // await queryInterface.addColumn('USERS', 'sector', {
    //   type: Sequelize.STRING,
    //   allowNull: true,
    // });
    // await queryInterface.addColumn('USERS', 'industry', {
    //   type: Sequelize.STRING,
    //   allowNull: true,
    // });
  },
};