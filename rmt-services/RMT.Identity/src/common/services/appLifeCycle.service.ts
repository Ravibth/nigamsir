import {
  Injectable,
  OnApplicationBootstrap,
  Inject,
  OnApplicationShutdown,
} from '@nestjs/common';
import { ClientProxy } from '@nestjs/microservices';
import { EMicroServicesNames } from '../enum';

@Injectable()
export class ApplicationBootstrap implements OnApplicationBootstrap {
  constructor(
    @Inject(EMicroServicesNames.SERVICE_LAYER)
    private serviceLayerClient: ClientProxy,
  ) {}

  onApplicationBootstrap() {
    this.serviceLayerClient.connect();
    return true;
  }
}

@Injectable()
export class ApplicationShutdown implements OnApplicationShutdown {
  constructor(
    @Inject(EMicroServicesNames.SERVICE_LAYER)
    private serviceLayerClient: ClientProxy,
  ) {}

  onApplicationShutdown() {
    this.serviceLayerClient.close();
    return true;
  }
}
