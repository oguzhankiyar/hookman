import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EventModel } from '../_models/event.model';
import { BaseService } from './base.service';
import { NotificationService } from './notification.service';

@Injectable()
export class EventService extends BaseService<EventModel> {
  constructor(httpClient: HttpClient, notificationService: NotificationService) {
    super('events', httpClient, notificationService);
  }
}