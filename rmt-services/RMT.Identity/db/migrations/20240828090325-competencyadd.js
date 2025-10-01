/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.addColumn('USERS', 'competency', {
      type: Sequelize.STRING,
    });
    await queryInterface.addColumn('USERS', 'competencyId', {
      type: Sequelize.STRING,
    });
  },

  async down(queryInterface, Sequelize) {
    await queryInterface.removeColumn(('USERS', 'competency'));
    await queryInterface.removeColumn(('USERS', 'competencyId'));
  },
};
