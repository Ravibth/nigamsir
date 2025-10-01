import {
  ArgumentsHost,
  Catch,
  ExceptionFilter,
  HttpException,
  HttpStatus,
} from "@nestjs/common";
import { Response } from "express";

@Catch()
export class HttpExceptionFilter implements ExceptionFilter {
  catch(exception: any, host: ArgumentsHost) {
    if (host.getType() === "rpc") {
      return exception;
    }

    const context = host.switchToHttp();
    const response = context.getResponse<Response>();
    const request = context.getRequest<Request>();
    const status =
      exception instanceof HttpException
        ? exception.getStatus()
        : HttpStatus.INTERNAL_SERVER_ERROR;

    const msgs = exception?.response?.message || [];

    response.status(status).json({
      statusCode: status,
      error: exception.message,
      messages: typeof msgs == "string" ? [msgs] : msgs,
      timestamp: new Date().toISOString(),
      path: request.url,
    });
  }
}
