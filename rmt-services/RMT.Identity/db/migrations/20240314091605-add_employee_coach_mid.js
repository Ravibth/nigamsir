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
      'supercoach_mid', // new field name
      {
        type: Sequelize.STRING,
        allowNull: true,
      },
    );
    await queryInterface.addColumn(
      'USERS', // table name
      'co_supercoach_mid', // new field name
      {
        type: Sequelize.STRING,
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
      'supercoach_mid', // new field name
    );
    await queryInterface.addColumn(
      'USERS', // table name
      'co_supercoach_mid', // new field name
    );
  },
};
// @Column({ type: DataTypes.STRING })
//   supercoach_mid?: string;
//   @Column({ type: DataTypes.STRING })
//   co_supercoach_mid?: string;
