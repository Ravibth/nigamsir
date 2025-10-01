import { Injectable } from "@nestjs/common";
import { IUserEmails } from "./interfaces";
import * as fetch from "superagent";

@Injectable()
export class IdentityService {
  getAllUserDetails(params: IUserEmails, token: string): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      if (params?.email_id && params.email_id.length) {
        fetch
          .get(`${process.env.GATEWAY_MS}/identity/user/multiuser/v1`)
          .set("authorization", token)
          .query({ email_id: params.email_id })
          .end((err, res) => {
            if (err) {
              resolve([]);
              return;
            }

            resolve(res && res.body ? res.body : []);
          });
      } else {
        resolve([]);
      }
    });
  }
  GetUsersByEmails(params: IUserEmails, token: string) {
    return new Promise<any>((resolve, reject) => {
      if (params?.email_id && params.email_id.length) {
        fetch
          .post(`${process.env.GATEWAY_MS}/identity/user/GetUsersByEmails`)
          .send(params.email_id)
          .set("authorization", token)
          .end((err, res) => {
            if (err) {
              resolve([]);
              return;
            }
            resolve(res && res.body ? res.body : []);
          });
      } else {
        resolve([]);
      }
    });
  }
  getListOfAllUsers(params: any, token: string): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      fetch
        .get(
          `${process.env.GATEWAY_MS}/identity/user/all?service_line=${params.service_line}`
        )
        .set("authorization", token)
        // .query({ email_id: params.email_id })
        .end((err, res) => {
          if (err) {
            resolve([]);
            return;
          }

          resolve(res && res.body ? res.body : []);
        });
    });
  }
}
