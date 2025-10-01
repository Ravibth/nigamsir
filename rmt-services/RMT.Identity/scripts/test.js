'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    try {
      console.log('getting in migration');
      await queryInterface.createTable('EMPLOYEE_MASTER', {
        id: {
          allowNull: false,
          autoIncrement: true,
          primaryKey: true,
          type: Sequelize.INTEGER,
        },
        // name: {
        //   type: Sequelize.STRING,
        // },
        // email_id: {
        //   type: Sequelize.STRING,
        // },
        // entity: {
        //   type: Sequelize.STRING,
        // },
        // emp_id: {
        //   type: Sequelize.STRING,
        // },
        // emp_code: {
        //   type: Sequelize.STRING,
        // },
        // fname: {
        //   type: Sequelize.STRING,
        // },
        // lname: {
        //   type: Sequelize.STRING,
        // },

        // designation: {
        //   type: Sequelize.STRING,
        // },

        // location: {
        //   type: Sequelize.STRING,
        // },
        // expertise: {
        //   type: Sequelize.STRING,
        // },
        // smeg: {
        //   type: Sequelize.STRING,
        // },
        // lwd: {
        //   type: Sequelize.STRING,
        // },
        // is_active: {
        //   type: Sequelize.BOOLEAN,
        // },
        // co_supercoach_name: {
        //   type: Sequelize.STRING,
        // },
        // supercoach_name: {
        //   type: Sequelize.STRING,
        // },
        // cr_date: {
        //   allowNull: true,
        //   type: Sequelize.DATE,
        // },
        // up_date: {
        //   allowNull: true,
        //   type: Sequelize.DATE,
        // },
        // deleted_at: {
        //   allowNull: true,
        //   type: Sequelize.DATE,
        // },
      });
    } catch (error) {
      console.error(error);
    }
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable('EMPLOYEE_MASTER');
  },
};
