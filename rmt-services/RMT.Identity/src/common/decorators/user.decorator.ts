import { createParamDecorator, ExecutionContext } from '@nestjs/common';

export interface IUser {
  email: string;
  name: string;
  designation: string;
  service_line: string;
  emp_code: string;
  roles: string[];
  token: string;
  id: number;
  role: string;
  supercoach_name: string;
  co_supercoach_name: string;
  employee_id: string;
  uemail_id: string;
  competency: string;
  competencyId: string;
  app_permissions: any[];
  access_token?: string;
}

export const User = createParamDecorator(
  (data: unknown, ctx: ExecutionContext) => {
    const request = ctx.switchToHttp().getRequest();

    if (!request?.headers?.userinfo) {
      return {
        email: '',
        name: '',
        service_line: '',
        designation: '',
        roles: [],
        emp_code: '',
        token: '',
        role: '',
        supercoach_name: '',
        co_supercoach_name: '',
        employee_id: '',
        uemail_id: '',
        competency: '',
        competencyId: '',
        app_permissions: [],
      };
    }
    const {
      name,
      email,
      designation,
      service_line,
      roles,
      emp_code,
      role,
      supercoach_name,
      co_supercoach_name,
      employee_id,
      uemail_id,
      competency,
      competencyId,
      app_permissions,
      token,
    } = JSON.parse(request?.headers?.userinfo);

    return {
      email,
      name,
      service_line,
      designation,
      roles,
      emp_code,
      role,
      supercoach_name,
      co_supercoach_name,
      employee_id,
      uemail_id,
      competency,
      competencyId,
      app_permissions,
      token: request.headers.authorization,
    };
  },
);
