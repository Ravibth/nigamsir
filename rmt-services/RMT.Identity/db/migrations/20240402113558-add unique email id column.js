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

    const uemail_idExists = await queryInterface.sequelize.query(
      `SELECT * FROM information_schema.columns WHERE table_name = 'USERS' AND column_name = 'uemail_id'`,
    );
    if (uemail_idExists[0].length == 0) {
      await queryInterface.addColumn('USERS', 'uemail_id', {
        type: Sequelize.STRING,
        allowNull: true,
      });
    }

    const employee_id_user_role_Exists = await queryInterface.sequelize.query(
      `SELECT * FROM information_schema.columns WHERE table_name = 'USER_ROLE' AND column_name = 'employee_id'`,
    );

    if (employee_id_user_role_Exists[0].length == 0) {
      await queryInterface.addColumn('USER_ROLE', 'employee_id', {
        type: Sequelize.STRING,
        allowNull: true,
      });
    }

    const constraints_users = await queryInterface.sequelize.query(
      `
    SELECT constraint_name, constraint_type
    FROM information_schema.table_constraints
    WHERE table_name = 'USERS';
  `,
      { type: queryInterface.sequelize.QueryTypes.SELECT },
    );

    // await queryInterface.addConstraint('USERS', {
    //   type: 'unique',
    //   fields: ['employee_id'], // Specify the column(s) for the unique constraint
    //   name: 'unique_employee_id', // Optionally specify a name for your unique constraint
    // });

    constraints_users.forEach(async (constraints_user) => {
      if (constraints_user.constraint_name === 'unique_employee_id') {
        console.log('Deleting constraint unique_employee_id');
        await queryInterface.removeConstraint('USERS', 'unique_employee_id');
      }
    });

    await queryInterface.sequelize.query(`
    ALTER TABLE "USERS" ADD CONSTRAINT unique_employee_id UNIQUE (employee_id);
    `);

    await queryInterface.sequelize.query(`
    alter table "USER_ROLE" add constraint fk_user_role_employee_id foreign key (employee_id) REFERENCES "USERS" (employee_id);
    `);

    await queryInterface.sequelize.query(`
    UPDATE "USER_ROLE"
    SET employee_id = "USERS".employee_id
    FROM "USERS"
    WHERE "USER_ROLE".user = "USERS".email_id;
    `);

    await queryInterface.sequelize.query(`
    UPDATE "USERS" SET uemail_id = "USERS".email_id::text;
    `);

    //Commented on 11 04 as discussed with ravi
    // await queryInterface.sequelize.query(`
    // UPDATE "USERS" SET email_id = "USERS".employee_id::text || '__' || "USERS".email_id::text;
    // `);

    const constraints = await queryInterface.sequelize.query(
      `
    SELECT constraint_name, constraint_type
    FROM information_schema.table_constraints
    WHERE table_name = 'USER_ROLE';
  `,
      { type: queryInterface.sequelize.QueryTypes.SELECT },
    );

    constraints.forEach(async (constraint) => {
      if (constraint.constraint_name === 'USER_ROLE_user_fkey') {
        await queryInterface.removeConstraint(
          'USER_ROLE',
          'USER_ROLE_user_fkey',
        );
      }
    });

    // await queryInterface.removeConstraint('USER_ROLE', 'USER_ROLE_user_fkey');

    // await queryInterface.foreign('user').references('USERS.employee_id');

    await queryInterface.addConstraint('USER_ROLE', {
      fields: ['employee_id'],
      type: 'foreign key',
      name: 'USER_ROLE_user_fkey',
      references: {
        table: 'USERS',
        field: 'employee_id',
      },
      onDelete: 'CASCADE',
      onUpdate: 'CASCADE',
    });
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
