/* eslint-disable @typescript-eslint/no-var-requires */
import { config } from "dotenv";
config();
const snowflake = require("snowflake-sdk");
snowflake.configure({ logLevel: "trace" });

import { ValidationPipe, Logger } from "@nestjs/common";
import { NestFactory } from "@nestjs/core";
import { useContainer } from "class-validator";
import { AppModule } from "./app.module";
import { SecretManager } from "./common/secretManager/secretManager";

async function bootstrap() {
  const secretManager = SecretManager.getInstance();

  const { PORT, CORSPOLICYCONSUMERHOSTURL } = secretManager.appConfig;

  const ROUTE = "workflow";
  const app = await NestFactory.create(AppModule, {
    logger: !["dev", "log", "qa"].includes(process.env.NODE_ENV)
      ? ["warn", "error", "log"]
      : ["warn", "error", "debug", "verbose", "log"],
  });
  const logger = new Logger("bootstrap");
  app.useGlobalPipes(new ValidationPipe({ whitelist: true, transform: true }));
  app.setGlobalPrefix(ROUTE);
  app.enableCors({
    origin: [CORSPOLICYCONSUMERHOSTURL],
    methods: ["GET", "POST", "PUT", "DELETE"],
    // credentials: true,
  });
  useContainer(app.select(AppModule), { fallbackOnErrors: true });
  await app.listen(PORT, () => {
    logger.log(`Application running at ${PORT}`);
    logger.debug(` Route "${ROUTE}" initialized`);
  });
}
bootstrap();
