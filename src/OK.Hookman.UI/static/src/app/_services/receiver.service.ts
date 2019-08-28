import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReceiverModel } from '../_models/receiver.model';
import { BaseService } from './base.service';
import { NotificationService } from './notification.service';

@Injectable()
export class ReceiverService extends BaseService<ReceiverModel> {
  constructor(httpClient: HttpClient, notificationService: NotificationService) {
    super('receivers', httpClient, notificationService);
  }
}