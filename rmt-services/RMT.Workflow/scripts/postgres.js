const dotenv = require("dotenv");
dotenv.config();

const { Pool } = require("pg");
const { Client } = require("pg");

async function getPostgresConnection() {
  const { PG_USER, PG_HOST, PG_DATABASE, PG_PASSWORD, PG_PORT, PG_CLOUD_ENV } =
    process.env;
  try {
    console.log("Db credentials", {
      PG_USER,
      PG_HOST,
      PG_DATABASE,
      PG_PASSWORD,
      PG_PORT,
      PG_CLOUD_ENV,
    });

    //Now create the database using default postgres database

    var clientPG_Config = {};
    if (PG_CLOUD_ENV.toLowerCase() == "true") {
      clientPG_Config = {
        user: PG_USER,
        host: PG_HOST,
        database: "postgres", //Default database name to create a new database
        password: PG_PASSWORD,
        port: +PG_PORT,
        ssl: {
          rejectUnauthorized: false,
        },
      };
    } else {
      clientPG_Config = {
        user: PG_USER,
        host: PG_HOST,
        database: "postgres", //Default database name to create a new database
        password: PG_PASSWORD,
        port: +PG_PORT,
      };
    }

    const clientPG = new Client(clientPG_Config);

    console.log("Client initialized");

    clientPG.on("error", (err) => {
      console.log("From event");
      console.log(err);
    });

    var resConnPG = await clientPG.connect(function (err, conn) {
      if (err) {
        console.log("Unable to connect:-", err.message);
        // reject('Connection failed: ' + err.message);
      } else {
        console.log("Successfully connected:-", conn.connectionParameters);
        // console.log('Connection object:-', conn.connection);
        // resolve(conn);
      }
    });

    console.log("----------Query Execution Start----------");
    console.log(await clientPG.query("SELECT NOW()"));
    var dbExistRes = await clientPG.query(
      "select exists(SELECT datname FROM pg_catalog.pg_database WHERE lower(datname) = lower('" +
        PG_DATABASE +
        "'));"
    );
    var isDBExists = dbExistRes["rows"][0]["exists"];
    console.log("isDBExists", isDBExists);

    console.log("----------Query Execution End----------");
    var queryCmd1 = "";
    if (isDBExists == false) {
      console.log("Database creation started", PG_DATABASE);

      var dbCreationCommand = 'CREATE DATABASE "' + PG_DATABASE + '"';
      queryCmd1 = `CREATE TABLE IF NOT EXISTS public."SequelizeMeta" 
    (name character varying(255) COLLATE pg_catalog."default" NOT NULL, 
    CONSTRAINT "SequelizeMeta_pkey" PRIMARY KEY (name))`;

      console.log("----------DB Creation Start----------");
      var resDBCmd = await clientPG.query(dbCreationCommand);
      console.log(resDBCmd);
      console.log("----------DB Creation End----------");
      await clientPG.end();
    } else {
      console.log("Database already exists", PG_DATABASE);
      queryCmd1 = "SELECT NOW()";
      await clientPG.end();
    }

    var clientDB_PG_Config = {};
    if (PG_CLOUD_ENV.toLowerCase() == "true") {
      clientDB_PG_Config = {
        user: PG_USER,
        host: PG_HOST,
        database: PG_DATABASE,
        password: PG_PASSWORD,
        port: PG_PORT,
        ssl: { rejectUnauthorized: false },
      };
    } else {
      clientDB_PG_Config = {
        user: PG_USER,
        host: PG_HOST,
        database: PG_DATABASE,
        password: PG_PASSWORD,
        port: PG_PORT,
      };
    }

    //Now create the Table for the above created database
    const clientDB = new Client(clientDB_PG_Config);

    var resConnDB = await clientDB.connect(function (err, conn) {
      if (err) {
        console.log("Unable to connect:-", err.message);
        // reject('Connection failed: ' + err.message);
      } else {
        console.log("Successfully connected:-", conn.connectionParameters);
        // console.log('Connection object:-', conn.connection);
        // resolve(conn);
      }
    });

    console.log("----------DB queryCmd1 Start----------");
    var resQueryCmd1 = await clientDB.query(queryCmd1);
    console.log(resQueryCmd1);
    console.log("----------DB queryCmd1 End----------");

    await clientDB.end();
  } catch (err) {
    console.error("getPostgresConnection Error:-", err);
    process.exit(1);
  }
}

async function getConnection() {
  const { DIALECT } = process.env;

  switch (DIALECT) {
    case "postgres":
      await getPostgresConnection();
      console.log("postgres data connection Success");
      break;

    default:
      throw new Error("No dialect matched! Pls check the config");
  }
}

function closeConnection(connection) {
  connection.destroy(function (err, conn) {
    if (err) {
      console.error("Unable to disconnect: " + err.message);
    } else {
      console.log("Disconnected connection with id: " + connection.getId());
    }
  });
}

async function createDatabase() {
  try {
    const connection = await getConnection();
  } catch (e) {
    console.log(e);
  }
}

createDatabase();
