export interface ServiceErrorResponse {
    errorType: ErrorType;
    msg?: string;
    label?: string;
}

export enum ErrorType { 
  Unauthorized = "Unauthorized",
  ServiceError502 = "502", 
  unknown = "unknown",
  err_network = "ERR_NETWORK"

}

export const ERRORCODE = [
  500, 502
]