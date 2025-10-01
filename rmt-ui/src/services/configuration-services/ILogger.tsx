export enum ILogLevel {
  Trace = 0,
  Debug = 1,
  Information = 2,
  Warning = 3,
  Error = 4,
  Critical = 5,
  None = 6,
}

export interface ILogger {
  logLevel: ILogLevel;
  category: string;
  function: string;
  message: string;
  stackTrace: string;
  logObjects: any[];
}
