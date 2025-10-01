import { Injectable, Logger } from "@nestjs/common";
import { IUser } from "src/common/decorators/user.decorator";
import { UpdateSupercoachAndDelegateDto } from "src/lib/workflow/dto/updateSupercoachAndDelegate.dto";
import * as fetch from "superagent";
const LOG_CONTEXT = "project-service";
@Injectable()
export class ProjectService {
  constructor(private readonly logger: Logger) {}
  GetResourceReviewerEmailsByPipelineCode(
    pipelineCode: string,
    jobCode: string,
    user: IUser
  ): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      console.log(pipelineCode);
      console.log(user);
      if (pipelineCode && user) {
        fetch
          .get(
            `${process.env.GATEWAY_MS}/Project/GetResourceReviewerEmailsByPipelineCode?pipelineCode=${pipelineCode}&jobCode=${jobCode}`
          )
          .set("authorization", user.token)
          //   .set("access_token", user.access_token)
          //   .set("access-token", user.access_token)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);
              reject(err);
            } else {
              this.logger.debug(
                "Got Project By Resource Reviewer Emails ",
                LOG_CONTEXT
              );
              resolve(res && res.text ? res.text : "");
            }
          });
      } else {
        this.logger.debug("Unable to get user and params", LOG_CONTEXT);
        resolve([]);
      }
    });
  }
  //need to check if this method is in use?
  GetResourceRequestorEmailsByPipelineCode(
    pipelineCode: string,
    jobCode: string,
    user: IUser
  ): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      console.log(pipelineCode);
      console.log(user);
      if (pipelineCode && user) {
        fetch
          .get(
            `${process.env.GATEWAY_MS}/Project/GetResourceRequestorEmailsByPipelineCode?pipelineCode=${pipelineCode}&jobCode=${jobCode}`
          )
          .set("authorization", user.token)
          //   .set("access_token", user.access_token)
          //   .set("access-token", user.access_token)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);
              reject(err);
            } else {
              this.logger.debug(
                "Got Project By Resource Reviewer Emails ",
                LOG_CONTEXT
              );
              resolve(res && res.text ? res.text : "");
            }
          });
      } else {
        this.logger.debug("Unable to get user and params", LOG_CONTEXT);
        resolve([]);
      }
    });
  }
  GetRequestorEmailsForAllocationWorkflow(
    pipelineCode: string,
    jobCode: string,
    workflowStartedBy: string,
    user: IUser
  ): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      if (pipelineCode) {
        fetch
          .get(
            `${process.env.GATEWAY_MS}/Project/GetRequestorEmailsForAllocationWorkflow?pipelineCode=${pipelineCode}&jobCode=${jobCode}&workflowStartedBy=${workflowStartedBy}`
          )
          .set("authorization", user.token)
          //   .set("access_token", user.access_token)
          //   .set("access-token", user.access_token)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);
              reject(err);
            } else {
              this.logger.debug(
                "Got Project By Resource Reviewer Emails ",
                LOG_CONTEXT
              );
              resolve(res && res.text ? res.text : "");
            }
          });
      } else {
      }
    });
  }
  GetProjectDetailsByPipelineCode(
    pipelineCode: string,
    jobCode: string,
    user: IUser
  ): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      console.log(pipelineCode);
      console.log(user);
      if (pipelineCode && user) {
        fetch
          .get(
            `${
              process.env.GATEWAY_MS
            }/Project/GetProjectFullDetailsByPipelineCode?pipelineCode=${encodeURIComponent(
              pipelineCode
            )}&jobCode=${encodeURIComponent(jobCode)}`
          )
          .set("authorization", user.token)
          //   .set("access_token", user.access_token)
          //   .set("access-token", user.access_token)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);
              reject(err);
            } else {
              this.logger.debug("Got Project By Pipeline Code", LOG_CONTEXT);
              resolve(res && res.body ? res.body : []);
            }
          });
      } else {
        this.logger.debug("Unable to get user and params", LOG_CONTEXT);
        resolve([]);
      }
    });
  }
  UpdateProjectRolesForSupercoachDelegate(
    supercoachData: UpdateSupercoachAndDelegateDto,
    user: IUser
  ) {
    const data = {
      supercoach_email: supercoachData.supercoach_email,
      prev_allocation_delegate_email:
        supercoachData.prev_allocation_delegate_email,
      new_allocation_delegate_email:
        supercoachData.new_allocation_delegate_email,
      new_allocation_delegate_name: supercoachData.new_allocation_delegate_name,
    };
    return new Promise((resolve, reject) => {
      fetch
        .post(
          `${process.env.GATEWAY_MS}/Project/UpdateProjectSuperCoachDelegate?pipelineCode`
        )
        .set("authorization", user.token)
        .send(data)
        .end((err, res) => {
          if (err) {
            this.logger.error(err, LOG_CONTEXT);
            reject(err);
          } else {
            this.logger.debug("Got Project By Pipeline Code", LOG_CONTEXT);
            resolve(res && res.body ? res.body : []);
          }
        });
    });
  }
}
