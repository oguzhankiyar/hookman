import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActionModel } from '../_models/action.model';
import { BaseService } from './base.service';
import { NotificationService } from './notification.service';

@Injectable()
export class ActionService extends BaseService<ActionModel> {
  constructor(httpClient: HttpClient, notificationService: NotificationService) {
    super('actions', httpClient, notificationService);
  }
}