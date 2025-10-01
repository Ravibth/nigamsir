import { Injectable } from '@nestjs/common';

import { EmployeeDto, IEmployeeQuery } from './interface';
import * as superFetch from 'superagent';
import { IUser } from '../../common/decorators/user.decorator';
import { SecretManager } from '../../common/secretManager/secretManager';

@Injectable()
export class ErpConnectorService {
  private clientSecret: SecretManager;
  constructor() {
    this.clientSecret = SecretManager.getInstance();
  }

  async getUserDetailsByEmail(
    query: IEmployeeQuery,
    user: IUser,
  ): Promise<EmployeeDto[]> {
    try {
      const gatewayUrl = this.clientSecret.appConfig.GATEWAY_MS;

      const url = `${gatewayUrl}/erp/employee/email`;
      // url = url.replace('//', '/');

      const result = await new Promise<EmployeeDto[]>((resolve, reject) => {
        superFetch
          .get(url)
          .set('Authorization', user.token)
          .query({ email: query.email })
          .end((err, res) => {
            if (err) {
              reject(err.body || err.text || err);
            }
            resolve(res.body ?? []);
          });
      });

      return result;
    } catch (e) {
      throw e;
    }
  }
}
