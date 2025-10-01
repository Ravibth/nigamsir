/* eslint-disable prettier/prettier */
'use strict';

const { SYSTEMADMINUSERID, SYSTEMADMINUSERMID, SYSTEMADMINUSEREMAIL } =
  process.env;

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.bulkInsert(
      'USERS',
      [
        {
          id: SYSTEMADMINUSERID,
          name: 'SystemAdmin',
          email_id: SYSTEMADMINUSERMID + '__' + SYSTEMADMINUSEREMAIL,
          employee_id: SYSTEMADMINUSERMID,
          emp_code: SYSTEMADMINUSERMID,
          fname: 'SystemAdmin',
          lname: 'SystemAdmin',
          supercoach_name: SYSTEMADMINUSERMID + '__' + SYSTEMADMINUSEREMAIL,
          co_supercoach_name: SYSTEMADMINUSERMID + '__' + SYSTEMADMINUSEREMAIL,
          created_by: 'System',
          is_active: true,
          status: true,
          updated_by: 'System',
          created_at: '2024-11-12T00:00:00Z',
          updated_at: '2024-11-12T00:00:00Z',
          reporting_partner_mid: SYSTEMADMINUSERMID,
          supercoach_mid: SYSTEMADMINUSERMID,
          co_supercoach_mid: SYSTEMADMINUSERMID,
          uemail_id: SYSTEMADMINUSEREMAIL,
        },
      ],
      {},
    );
  },

  async down(queryInterface, Sequelize) {
    // await queryInterface.bulkDelete('USERS', null, {});
  },
};
