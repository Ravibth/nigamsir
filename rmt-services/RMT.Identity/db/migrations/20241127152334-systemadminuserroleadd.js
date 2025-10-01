'use strict';

const { SYSTEMADMINUSERROLEID, SYSTEMADMINUSERMID, SYSTEMADMINUSEREMAIL } =
  process.env;

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    //await queryInterface.bulkDelete('USER_ROLE', [], {});
    await queryInterface.bulkInsert(
      'USER_ROLE',
      [
        {
          id: SYSTEMADMINUSERROLEID,
          user: SYSTEMADMINUSERMID + '__' + SYSTEMADMINUSEREMAIL,
          role: 'SystemAdmin',
          is_active: true,
          created_by: 'system',
          updated_by: 'system',
          created_at: '2024-11-12T00:00:00Z',
          updated_at: '2024-11-12T00:00:00Z',
          employee_id: SYSTEMADMINUSERMID,
        },
      ],
      {},
    );
  },

  async down(queryInterface, Sequelize) {
    // await queryInterface.bulkDelete('USER_ROLE', [], {});
  },
};
