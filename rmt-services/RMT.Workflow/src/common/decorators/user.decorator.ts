import {
  createParamDecorator,
  ExecutionContext,
  ForbiddenException,
} from "@nestjs/common";

export interface IUser {
  id: number;
  emp_code: string;
  email: string;
  name: string;
  role: string;
  supercoach_name: string;
  co_supercoach_name: string;
  supercoach_mid: string;
  co_supercoach_mid: string;
  employee_id: string;
  uemail_id: string;
  competency: string;
  competencyId: string;
  app_permissions: any[];
  token?: string;
  access_token?: string;
  designation: string;
  service_line?: string;
  roles: string[];
}

export const User = createParamDecorator(
  (data: unknown, ctx: ExecutionContext) => {
    try {
      const request = ctx.switchToHttp().getRequest();
      if (!request?.headers?.userinfo) {
        return {
          email: "",
          name: "",
          service_line: "",
          designation: "",
          roles: [],
          emp_code: "",
          token: "",
          role: "",
          supercoach_name: "",
          co_supercoach_name: "",
          employee_id: "",
          uemail_id: "",
          competency: "",
          competencyId: "",
          app_permissions: [],
        };
      }
      const {
        name,
        email,
        emp_code,
        designation,
        service_line,
        roles,
        role,
        supercoach_name,
        co_supercoach_name,
        supercoach_mid,
        co_supercoach_mid,
        employee_id,
        uemail_id,
        competency,
        competencyId,
        app_permissions,
      } = JSON.parse(request.headers.userinfo);

      return {
        name,
        email,
        designation,
        service_line,
        roles,
        emp_code,
        role,
        supercoach_name,
        co_supercoach_name,
        supercoach_mid,
        co_supercoach_mid,
        employee_id,
        uemail_id,
        competency,
        competencyId,
        app_permissions,
        token: request.headers?.authorization,
        access_token:
          request.headers.access_token?.trim() ||
          request.headers["access-token"]?.trim(),
      };
    } catch (error) {
      throw new ForbiddenException(error, "error in context parsing 1");
    }
  }
);
