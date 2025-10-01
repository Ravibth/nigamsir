import { Injectable } from "@nestjs/common";
import { TcpClientOptions, Transport } from "@nestjs/microservices";
import { SequelizeModuleOptions } from "@nestjs/sequelize/dist";
import { TableOptions } from "sequelize-typescript";
import { Dialect } from "sequelize/types/sequelize";
import { EMicroServicesNames } from "../enum";

// eslint-disable-next-line @typescript-eslint/no-var-requires
const config = process.env;

interface IServerConfig {
  PG_PORT: string;
  PORT: string;
  ROUTE: string;
  GATEWAY_MS: string;
  CORSPOLICYCONSUMERHOSTURL: string;
}

interface ITcpServerConfig {
  port: number;
  host: string;
}

export const ModalConfigs: TableOptions = {
  paranoid: false,
  deletedAt: "deleted_at",
  createdAt: "created_at",
  updatedAt: "updated_at",
};

@Injectable()
export class SecretManager {
  private static instance: SecretManager = null;

  static getInstance(): SecretManager {
    if (!SecretManager.instance) {
      SecretManager.instance = new SecretManager();
    }

    return SecretManager.instance;
  }

  get appConfig(): IServerConfig {
    return {
      PG_PORT: config.PG_PORT,
      PORT: config.PORT,
      ROUTE: config.ROUTE,
      GATEWAY_MS: config.GATEWAY_MS,
      CORSPOLICYCONSUMERHOSTURL: config.CORSPOLICYCONSUMERHOSTURL,
    };
  }

  transformTransportConfigIntoPortAndHost(url: string): ITcpServerConfig {
    try {
      const [host, port] = url.split(":");
      return { host, port: Number(port) };
    } catch (err) {
      console.error(url, " not found");
    }
  }

  servicesTransportInfo<T extends EMicroServicesNames>(
    key: T
  ): TcpClientOptions {
    let url: string;
    switch (key) {
      case EMicroServicesNames.CONFIGURATION:
        url = process.env.CONFIGURATION_URL;
        break;

      case EMicroServicesNames.SERVICE_LAYER:
        url = process.env.SERVICE_LAYER_URL;
        break;

      case EMicroServicesNames.IDENTITY:
        url = process.env.IDENTITY_URL;
        break;

      default:
        throw new Error("transport config not found");
    }

    console.log(this.transformTransportConfigIntoPortAndHost(url));

    return {
      transport: Transport.TCP,
      options: this.transformTransportConfigIntoPortAndHost(url),
    };
  }

  get dbConfig(): SequelizeModuleOptions {
    const {
      PG_USER,
      PG_PASSWORD,
      PG_DATABASE,
      PG_HOST,
      PG_PORT,
      PG_TIMEOUT,
      PG_POOLMAX,
      PG_POOLIDLE,
      PG_POOLTIME,
      PG_CLOUD_ENV,
    } = config;

    var dialectOptionsConfig = {};
    if (PG_CLOUD_ENV?.toLowerCase() == "true") {
      dialectOptionsConfig = {
        connectTimeout: PG_TIMEOUT,
        statement_timeout: PG_TIMEOUT,
        ssl: {
          rejectUnauthorized: false,
        },
      };
    } else {
      dialectOptionsConfig = {
        connectTimeout: PG_TIMEOUT,
        statement_timeout: PG_TIMEOUT,
      };
    }

    const dbConnectionConfig: SequelizeModuleOptions = {
      dialect: "postgres",
      host: PG_HOST,
      port: +PG_PORT,
      username: PG_USER,
      password: PG_PASSWORD,
      database: PG_DATABASE,

      pool: {
        max: +PG_POOLMAX,
        min: 0,
        acquire: +PG_POOLTIME,
        idle: +PG_POOLIDLE,
      },
      logging: (msg) => {
        if (msg.includes("Executed (default): SELECT 1+1 AS result")) {
          console.log("Database connection established successfully.");
        }
      },

      autoLoadModels: true,
      dialectOptions: dialectOptionsConfig,
      synchronize: false,
    };
    return dbConnectionConfig;
  }
}
