import { Injectable, Logger } from "@nestjs/common";
import { group } from "console";
import { IUser } from "src/common/decorators/user.decorator";
import * as fetch from "superagent";

const LOG_CONTEXT = "configuration-service";

@Injectable()
export class ConfigurationService {
  constructor(private readonly logger: Logger) {}
  //NOT_IN_USE
  getConfiguration(
    configGroup: string,
    configType: string,
    user: IUser
  ): Promise<void> {
    return new Promise<any>((resolve, reject) => {
      console.log(user);
      console.log(configType);
      if (configType && user) {
        fetch
          .get(
            `${process.env.GATEWAY_MS}/Configuration/GetConfigurationGroupByGroupNameAndConfigType?groupName=${configGroup}&ConfigType=${configType}`
          )
          .set("authorization", user.token)
          //   .set("access_token", user.access_token)
          //   .set("access-token", user.access_token)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);
              reject(err);
            }
            this.logger.debug("Got Configurations Data", LOG_CONTEXT);
            resolve(res && res.body ? res.body : []);
          });
      } else {
        this.logger.debug("Unable to get user and params", LOG_CONTEXT);
        resolve([]);
      }
    });
  }
  //NOT_IN_USE
  getConfigurationByExpertiesNameAndGroupName(
    expertiesName: string,
    groupName: string,
    user: IUser
  ): Promise<void> {
    return new Promise<any>((resolve, reject) => {
      console.log(user);
      console.log(expertiesName);
      if (expertiesName && groupName && user) {
        fetch
          .get(
            `${
              process.env.GATEWAY_MS
            }/Configuration/GetExpertiesConfigurationByExpertiesNameAndConfigGroup?expertiesName=${encodeURIComponent(
              expertiesName
            )}&configurationGroup=${groupName}`
          )
          .set("authorization", user.token)
          //   .set("access_token", user.access_token)
          //   .set("access-token", user.access_token)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);
              reject(err);
            }
            this.logger.debug("Got Configurations Data", LOG_CONTEXT);
            resolve(res && res.body ? res.body : []);
          });
      } else {
        this.logger.debug("Unable to get user and params", LOG_CONTEXT);
        resolve([]);
      }
    });
  }
  GetConfigurationByConfigGroupConfigKeyAndConfigType(
    groupName: string,
    configKey: string,
    configType: string,
    user: IUser
  ): Promise<void> {
    return new Promise<any>((resolve, reject) => {
      console.log(user);
      // console.log(expertiesName);
      if (configKey && configType && groupName && user) {
        fetch
          .get(
            // GetConfigurationByConfigGroupConfigKeyAndConfigType?groupName=Resource_allocation_review&configKey=Resource_allocation_review&configType=EXPERTISE
            `${process.env.GATEWAY_MS}/Configuration/GetConfigurationByConfigGroupConfigKeyAndConfigType?groupName=${groupName}&configKey=${configKey}&configType=${configType}`
          )
          .set("authorization", user.token)
          //   .set("access_token", user.access_token)
          //   .set("access-token", user.access_token)
          .end((err, res) => {
            if (err) {
              this.logger.error(err, LOG_CONTEXT);
              reject(err);
            }
            this.logger.debug("Got Configurations Data", LOG_CONTEXT);
            resolve(res && res.body ? res.body : []);
          });
      } else {
        this.logger.debug("Unable to get user and params", LOG_CONTEXT);
        resolve([]);
      }
    });
  }
}
