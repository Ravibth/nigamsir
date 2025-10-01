import { Controller, Get, NotFoundException } from '@nestjs/common';
import { MessagePattern } from '@nestjs/microservices';
import { Payload } from '@nestjs/microservices/decorators';

@Controller()
export class AppController {
  @Get('health')
  health(): { status: string } {
    try {
      return { status: 'OK' };
    } catch (error) {
      throw error;
    }
  }

  @MessagePattern({ cmd: 'authorization' })
  async accessValidation(@Payload() params: any): Promise<boolean> {
    console.log(params);
    return true;
  }
}
