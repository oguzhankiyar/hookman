import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SenderModel } from '../_models/sender.model';
import { BaseService } from './base.service';
import { NotificationService } from './notification.service';

@Injectable()
export class SenderService extends BaseService<SenderModel> {
  constructor(httpClient: HttpClient, notificationService: NotificationService) {
    super('senders', httpClient, notificationService);
  }
}