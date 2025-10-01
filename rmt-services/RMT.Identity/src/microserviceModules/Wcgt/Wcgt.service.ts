import { Injectable } from '@nestjs/common';
import * as superFetch from 'superagent';
import { IUser } from '../../common/decorators/user.decorator';
import { SecretManager } from '../../common/secretManager/secretManager';
import {
  IGetUserInfoWcgtRequestDTO,
  IGetUserInfoWcgtResponseDTO,
} from './interface';

@Injectable()
export class WCGTService {
  private clientSecret: SecretManager;
  constructor() {
    this.clientSecret = SecretManager.getInstance();
  }

  async getUserDetailsByEmailFromWCGT(
    query: IGetUserInfoWcgtRequestDTO,
    user: IUser,
  ): Promise<IGetUserInfoWcgtResponseDTO> {
    try {
      const wcgt_ms = this.clientSecret.appConfig.WCGT_MS;

      let url = wcgt_ms
        ? `${wcgt_ms}api/WcgtData/GetEmployeeByParam`
        : `https://localhost:7294/api/WcgtData/GetEmployeeByParam/`;
      if (query.emp_emailid) {
        url = url + '?emp_emailid=' + query.emp_emailid;
      }
      if (query.emp_mid) {
        url = url + '&emp_emailid=' + query.emp_emailid;
      }
      // url = url.replace('//', '/');
      console.log(url);
      const result = await new Promise<IGetUserInfoWcgtResponseDTO>(
        (resolve, reject) => {
          superFetch
            .get(url)
            //TODO CHeck for ssl
            //Calls can be forwarded using below line
            .disableTLSCerts()
            //.set('Authorization', user?.token)
           // .query({ emp_emailid: query.emp_emailid, emp_mid: query.emp_mid })
            .end((err, res) => {
              if (err) {
                console.log(err);
                reject(err.body || err.text || err);
              }
              console.log(res.body);
              resolve(res.body ?? res.body);
            });
        },
      );

      return result;
    } catch (e) {
      throw e;
    }
  }
}
