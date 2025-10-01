'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    // await queryInterface.bulkDelete('USER_ROLE', [], {});
    // await queryInterface.bulkInsert(
    //   'USER_ROLE',
    //   [
    //     {
    //       id: '59f01b07-c574-4137-a7bb-b4c308cd78de',
    //       user: 'Suman.Pathak@DevStagenet.onmicrosoft.com',
    //       created_by: 'system',
    //       is_active: true,
    //       created_at: new Date(),
    //       updated_at: new Date(),
    //       role: 'Approvers',
    //     },
    //     {
    //       id: 'edccfeb1-6b98-4a1c-9a8f-50fb7757ac6e',
    //       user: 'Himanshu.Sharma08@expdiginetdev.onmicrosoft.com',
    //       created_by: 'system',
    //       created_at: new Date(),
    //       updated_at: new Date(),
    //       is_active: true,
    //       role: 'Admin',
    //     },
    //     {
    //       id: '40ffe68d-55f0-4425-ab30-a6b9a573483f',
    //       user: 'Saif.Khan@expdiginetdev.onmicrosoft.com',
    //       created_by: 'system',
    //       created_at: new Date(),
    //       updated_at: new Date(),
    //       is_active: true,
    //       role: 'Admin',
    //     },
    //     {
    //       id: 'ea85fc2a-91e3-4be3-abe2-25abdb23434a',
    //       user: 'Saif.Khan@expdiginetdev.onmicrosoft.com',
    //       created_by: 'system',
    //       created_at: new Date(),
    //       updated_at: new Date(),
    //       is_active: true,
    //       role: 'Approvers',
    //     },
    //     {
    //       id: '3187b429-eb41-4ead-a763-542264774a94',
    //       user: 'Piyusha.Sinha@DevStagenet.onmicrosoft.com',
    //       created_by: 'system',
    //       created_at: new Date(),
    //       is_active: true,
    //       updated_at: new Date(),
    //       role: 'Engagement Creator',
    //     },
    //     {
    //       id: 'c0b3d8f9-e208-40b9-a74c-d6d412cf3368',
    //       user: 'Sanjay.Pathak@expdiginetdev.onmicrosoft.com',
    //       created_by: 'system',
    //       created_at: new Date(),
    //       is_active: true,
    //       updated_at: new Date(),
    //       role: 'Admin',
    //     },
    //     {
    //       id: '2449991e-8290-4e77-8ddc-f13da181ef0d',
    //       user: 'Aayush.Garg@expdiginetdev.onmicrosoft.com',
    //       created_by: 'system',
    //       created_at: new Date(),
    //       is_active: true,
    //       updated_at: new Date(),
    //       role: 'System Admin',
    //     },
    //     {
    //       id: '9ba39cd0-291d-484f-ba1a-eb705cbcb3f1',
    //       user: 'Aayush.Garg@expdiginetdev.onmicrosoft.com',
    //       created_by: 'system',
    //       created_at: new Date(),
    //       is_active: true,
    //       updated_at: new Date(),
    //       role: 'Resource Requestor',
    //     },
    //     {
    //       id: '07a6b73c-a683-49bc-93df-124e0c808fba',
    //       user: 'Tarun.Singh@expdiginetdev.onmicrosoft.com',
    //       created_by: 'system',
    //       created_at: new Date(),
    //       updated_at: new Date(),
    //       is_active: true,
    //       role: 'Admin',
    //     },
    //   ],
    //   {},
    // );
  },

  async down(queryInterface, Sequelize) {
    // await queryInterface.bulkDelete('USER_ROLE', [], {});
  },
};
