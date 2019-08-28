import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HookModel } from '../_models/hook.model';
import { BaseService } from './base.service';
import { NotificationService } from './notification.service';

@Injectable()
export class HookService extends BaseService<HookModel> {
  constructor(httpClient: HttpClient, notificationService: NotificationService) {
    super('hooks', httpClient, notificationService);
  }
}