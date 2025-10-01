import { Injectable, Logger } from "@nestjs/common";
import { IUser } from "src/common/decorators/user.decorator";
import { updateAllocationStatusDTO } from "src/lib/workflow/dto/updateAllocationStatus.dto";
import * as fetch from "superagent";

const LOG_CONTEXT = "allocation-service";

@Injectable()
export class AllocationService {
  constructor(private readonly logger: Logger) {}
  GetResourceAllocationDetailsById(item_id: string, user: IUser): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      console.log(item_id);
      console.log(user);
      if (item_id && user) {
        fetch
          .get(
            `${process.env.GATEWAY_MS}/ResourceAllocation/ResourceAllocationDetailsByGuid?guid=${item_id}`
            // `${process.env.GATEWAY_MS}/Project/GetResourceReviewerEmailsByPipelineCode`
          )
          .set("authorization", user.token)
          //   .set("access_token", user.access_token)
          //   .set("access-token", user.access_token)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);
              reject(err);
            } else {
              this.logger.debug("Got Resource allocation detils ", LOG_CONTEXT);
              resolve(res && res.body ? res.body : []);
            }
          });
      } else {
        this.logger.debug("Unable to get user and params", LOG_CONTEXT);
        resolve([]);
      }
    });
  }
  UpdateAllocationStatus(
    params: updateAllocationStatusDTO,
    user: IUser
  ): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      console.log(params);
      console.log(user);
      if (params && user) {
        fetch
          .post(
            `${process.env.GATEWAY_MS}/ResourceAllocation/UpdateAllocationStatusInResourceAllocationDetails`
          )
          .send(params)
          .set("authorization", user.token)
          //   .set("access_token", user.access_token)
          //   .set("access-token", user.access_token)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);
              reject(err);
            } else {
              this.logger.debug(
                "Updated Allocation Status Details ",
                LOG_CONTEXT
              );
              resolve(res && res.body ? res.body : {});
            }
          });
      } else {
        this.logger.debug("Unable to get user and Params", LOG_CONTEXT);
        resolve([]);
      }
    });
  }
}
