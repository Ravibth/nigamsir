import { ILogLevel, ILogger } from "../configuration-services/ILogger";
import { LogContentService } from "../configuration-services/configuration.service";

export async function errorMessage(
  modulename: string,
  functionname: string,
  error: any,
  args: [] = []
) {
  console.log(
    `{modulename: ${modulename}, functionname: ${functionname}, error: ${error} , Date: ${new Date()}`
  );
  let logData: ILogger = {
    logLevel: ILogLevel.Error,
    category: modulename,
    function: functionname,
    message: error,
    stackTrace: "",
    logObjects: args,
  };
  await LogContentService(logData);
}

export async function warningMessage(
  modulename: string,
  functionname: string,
  error: any,
  args: [] = []
) {
  console.log(
    `{modulename: ${modulename}, functionname: ${functionname} , warningMessage: ${error}, Date: ${new Date()}`
  );
  let logData: ILogger = {
    logLevel: ILogLevel.Warning,
    category: modulename,
    function: functionname,
    message: error,
    stackTrace: "",
    logObjects: args,
  };

  await LogContentService(logData);
}

export async function informationMessage(
  modulename: string,
  functionname: string,
  error: any,
  args: [] = []
) {
  console.log(
    `{modulename: ${modulename}, functionname: ${functionname} , InformationMessage: ${error}, Date: ${new Date()}`
  );
  let logData: ILogger = {
    logLevel: ILogLevel.Information,
    category: modulename,
    function: functionname,
    message: error,
    stackTrace: "",
    logObjects: args,
  };

  await LogContentService(logData);
}
