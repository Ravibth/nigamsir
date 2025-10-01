import { Injectable } from "@nestjs/common";
import { UpdateEngagementByCEODto } from "../../lib/workflow/dto/updateWorkflow.dto";
import * as fetch from "superagent";
import { IKpiUpdate } from "./interfaces";

@Injectable()
export class KpiService {
  updateKpi(params: IKpiUpdate, token: string): Promise<void> {
    return new Promise<any>((resolve, reject) => {
      if (Object.keys(params).length) {
        console.log(params, " -- updating kpi");
        fetch
          .post(`${process.env.GATEWAY_MS}/kpi/v1/update-kpi`)
          .set("authorization", token)
          .send(params)
          .end((err, res) => {
            if (err) {
              console.log("Errr====>", err);
              reject(err.text);
              return;
            }

            resolve(res && res.body ? res.body : []);
          });
      } else {
        resolve([]);
      }
    });
  }
}
