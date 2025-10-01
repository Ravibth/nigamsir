'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    /**
     * Add altering commands here.
     *
     * Example:
     * await queryInterface.createTable('users', { id: Sequelize.INTEGER });
     */
    await queryInterface.addColumn(
      'USERS', // table name
      'reporting_partner_mid', // new field name
      {
        type: Sequelize.STRING,
        allowNull: true,
      },
    );
    await queryInterface.addColumn(
      'USERS', // table name
      'employee_status', // new field name
      {
        type: Sequelize.STRING,
        allowNull: true,
      },
    );
    await queryInterface.addColumn(
      'USERS', // table name
      'employee_resignation_date', // new field name
      {
        type: Sequelize.DATE,
        allowNull: true,
      },
    );
    await queryInterface.addColumn(
      'USERS', // table name
      'employee_last_working_date', // new field name
      {
        type: Sequelize.DATE,
        allowNull: true,
      },
    );
  },

  async down(queryInterface, Sequelize) {
    /**
     * Add reverting commands here.
     *
     * Example:
     * await queryInterface.dropTable('users');
     */
    await queryInterface.removeColumn(
      'USERS', // table name
      'reporting_partner_mid', // new field name
    );
    await queryInterface.addColumn(
      'USERS', // table name
      'employee_status', // new field name
    );
    await queryInterface.addColumn(
      'USERS', // table name
      'employee_resignation_date', // new field name
    );
    await queryInterface.addColumn(
      'USERS', // table name
      'employee_last_working_date', // new field name
    );
  },
};

// @Column({ type: DataTypes.STRING })
// reporting_partner_mid?: string;
// @Column({ type: DataTypes.STRING })
// employee_status?: string;
// @Column({ type: DataTypes.DATE })
// employee_resignation_date?: Date;
// @Column({ type: DataTypes.DATE })
// employee_last_working_date?: Date;
