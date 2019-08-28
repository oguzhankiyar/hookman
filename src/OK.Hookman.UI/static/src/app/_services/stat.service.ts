import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { environment } from '../../environments/environment';
import { NotificationService } from './notification.service';
import { BasePagedResponseModel } from '../_models/basePagedResponse.model';
import { StatTopActionModel } from '../_models/stat.model';

@Injectable()
export class StatService {
  public apiUrl: string;

  constructor(
    private httpClient: HttpClient,
    private notificationService: NotificationService
  ) {
    this.apiUrl = environment.apiUrl + '/stats';
  }

  public getTopActionList(period: string): Subject<BasePagedResponseModel<StatTopActionModel[]>> {
    const result: Subject<BasePagedResponseModel<StatTopActionModel[]>> = new Subject<BasePagedResponseModel<StatTopActionModel[]>>();

    this.httpClient
      .get(this.apiUrl + '/topactions?period=' + period)
      .subscribe(
        (response: any) => {
          result.next(response);
        },
        (error: any) => {
          this.handleError(error);
          result.next(null);
        }
      );

    return result;
  }

  private handleError(error): void {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    this.notificationService.showError(errorMessage);
  }
}
