import { Injectable, Logger } from '@nestjs/common';
import * as fetch from 'superagent';
import { IUser } from '../../common/decorators/user.decorator';

const LOG_CONTEXT = 'workflow-engagement-service';

@Injectable()
export class ErpService {
    constructor(private readonly logger: Logger) {}
    getPartnerApprovRequiredStatus(service_line, user: IUser) {
        console.log('USER', user);
        return new Promise<any>((resolve, reject) => {
            if (service_line) {
                console.log(service_line, ' getPartnerApprovRequiredStatus');
                fetch
                    .get(`${process.env.GATEWAY_MS}/erp/serviceline/getPartnerApprov/`)
                    .set('authorization', user.token)
                    .set('access_token', user.access_token)
                    .set('access-token', user.access_token)
                    .query({ service_line: service_line })
                    .end((err, res) => {
                        if (err) {
                            this.logger.error(err, LOG_CONTEXT);

                            reject(err);
                            return;
                        }
                        this.logger.debug('engagement  updated', LOG_CONTEXT);
                        resolve(res && res.body ? res.body : []);
                    });
            } else {
                this.logger.debug('engagement not updated', LOG_CONTEXT);
                resolve([]);
            }
        });
    }
}
