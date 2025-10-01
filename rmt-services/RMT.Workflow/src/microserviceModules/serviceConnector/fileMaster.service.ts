import { Injectable } from '@nestjs/common';
import * as fetch from 'superagent';
import { FileDetailsResDto } from './dto/fileDetailsRes.dto';
import { FindFileDetailsDto } from './dto/findFileDetails.dto';

@Injectable()
export class FileMasterService {
    findDetails(params: FindFileDetailsDto, token: string): Promise<FileDetailsResDto[]> {
        return new Promise<any>((resolve, reject) => {
            if (Object.keys(params).length) {
                console.log(params, ' -- finding file details');
                fetch
                    .get(`${process.env.GATEWAY_MS}/service-connector/fileMaster/v1/query`)
                    .set('authorization', token)
                    .query(params)
                    .end((err, res) => {
                        if (err) {
                            console.log('Errr====>', err);
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

    findFileStatus(params: FindFileDetailsDto, token: string): Promise<FileDetailsResDto[]> {
        return new Promise<any>((resolve, reject) => {
            if (Object.keys(params).length) {
                console.log(params, ' -- finding file details');
                fetch
                    .put(
                        `${process.env.GATEWAY_MS}/service-connector/fileMaster/v1/getUploadedFileStatusFromDEP`,
                    )
                    .set('authorization', token)
                    .query(params)
                    .end((err, res) => {
                        if (err) {
                            console.log('Errr====>', err);
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
}
