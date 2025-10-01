import { Injectable, Logger } from "@nestjs/common";
import { UpdateEngagementByCEODto } from "../../lib/workflow/dto/updateWorkflow.dto";
import * as fetch from "superagent";
import { IEngagementUpdate, IKpiExecutionUpdate } from "./interfaces";
import { IUser } from "../../common/decorators/user.decorator";

const LOG_CONTEXT = "workflow-engagement-service";

@Injectable()
export class EngagementService {
  constructor(private readonly logger: Logger) {}
  updateStatus(params: IEngagementUpdate, user: IUser): Promise<void> {
    console.log("USER", user);
    return new Promise<any>((resolve, reject) => {
      if (params.id && Object.keys(params.toUpdate).length) {
        console.log(params, " -- updating engagement");
        fetch
          .put(`${process.env.GATEWAY_MS}/engagement/v1/update/${params.id}`)
          .set("authorization", user.token)
          .set("access_token", user.access_token)
          .set("access-token", user.access_token)
          .send(params.toUpdate)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);

              reject(err);
              return;
            }
            this.logger.debug("engagement  updated", LOG_CONTEXT);
            resolve(res && res.body ? res.body : []);
          });
      } else {
        this.logger.debug("engagement not updated", LOG_CONTEXT);
        resolve([]);
      }
    });
  }

  updateEngagementForProxy(
    params: IEngagementUpdate,
    user: IUser
  ): Promise<void> {
    console.log("USER", user);
    return new Promise<any>((resolve, reject) => {
      if (params.id && Object.keys(params.toUpdate).length) {
        console.log(params, " -- updating engagement");
        fetch
          .put(
            `${process.env.GATEWAY_MS}/engagement/v1/updateforproxy/${params.id}`
          )
          .set("authorization", user.token)
          .set("access_token", user.access_token)
          .set("access-token", user.access_token)
          .send(params.toUpdate)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);

              reject(err);
              return;
            }
            this.logger.debug("engagement  updated", LOG_CONTEXT);
            resolve(res && res.body ? res.body : []);
          });
      } else {
        this.logger.debug("engagement not updated", LOG_CONTEXT);
        resolve([]);
      }
    });
  }

  updateCOEAcceptance(
    params: UpdateEngagementByCEODto,
    token: string
  ): Promise<void> {
    return new Promise<any>((resolve, reject) => {
      if (params.id && Object.keys(params).length) {
        console.log(params, " -- updating coe acceptance");
        fetch
          .post(`${process.env.GATEWAY_MS}/engagement/v1/updateCOE`)
          .set("authorization", token)
          .send(params)
          .end((err, res) => {
            if (err) {
              console.log("Error in CEO Acceptance====>", err);
              reject(err);
              return;
            }

            resolve(res && res.body ? res.body : []);
          });
      } else {
        resolve([]);
      }
    });
  }

  updateKpiExecutionStatus(
    params: IKpiExecutionUpdate,
    user: IUser
  ): Promise<void> {
    return new Promise<any>((resolve, reject) => {
      if (params.id && Object.keys(params).length) {
        this.logger.debug(
          "Updating KPI Execution  " + JSON.stringify(params),
          LOG_CONTEXT
        );
        fetch
          .post(
            `${process.env.GATEWAY_MS}/engagement/v1/kpi-execution/status-update`
          )
          .set("authorization", user.token)
          .send(params)
          .end((err, res) => {
            if (err) {
              this.logger.debug("Error KPI Execution  ", LOG_CONTEXT);
              this.logger.error(err, LOG_CONTEXT);
              resolve([]);

              return;
            }

            this.logger.debug(" KPI Execution Updated ", LOG_CONTEXT);
            resolve(res && res.body ? res.body : []);
          });
      } else {
        this.logger.debug(" KPI Execution not updated ", LOG_CONTEXT);
        resolve([]);
      }
    });
  }
}
