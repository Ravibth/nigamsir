/* eslint-disable @typescript-eslint/no-var-requires */
require('dotenv').config();

const {
  PG_USER,
  PG_PASSWORD,
  PG_DATABASE,
  PG_HOST,
  PG_PORT,
  PG_TIMEOUT,
  DIALECT,
  PG_CLOUD_ENV,
} = process.env;
console.log(PG_USER);
console.log(PG_PASSWORD);

var moduleExportConfig = {};
if (PG_CLOUD_ENV?.toLowerCase() == 'true') {
  moduleExportConfig = {
    username: PG_USER,
    password: PG_PASSWORD,
    database: PG_DATABASE,
    host: PG_HOST,
    port: PG_PORT,
    ssl: { rejectUnauthorized: false },
    logging: true,
    dialect: DIALECT,
    dialectOptions: {
      connectTimeout: PG_TIMEOUT,
      statement_timeout: PG_TIMEOUT,
      ssl: {
        rejectUnauthorized: false,
      },
    },
  };
} else {
  moduleExportConfig = {
    username: PG_USER,
    password: PG_PASSWORD,
    database: PG_DATABASE,
    host: PG_HOST,
    port: PG_PORT,
    logging: true,
    dialect: DIALECT,
  };
}

module.exports = moduleExportConfig;
