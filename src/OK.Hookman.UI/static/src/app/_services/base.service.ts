import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subject } from 'rxjs';
import { environment } from '../../environments/environment';
import { NotificationService } from './notification.service';
import { BasePagedResponseModel } from '../_models/basePagedResponse.model';

@Injectable()
export class BaseService<T> {
  public apiUrl: string;

  constructor(
    private urlPath: string,
    private httpClient: HttpClient,
    private notificationService: NotificationService
  ) {
    this.apiUrl = environment.apiUrl + '/' + this.urlPath;
  }

  public getList(pageSize: number, pageNumber: number): Subject<BasePagedResponseModel<T[]>> {
    const result: Subject<BasePagedResponseModel<T[]>> = new Subject<BasePagedResponseModel<T[]>>();

    this.httpClient
      .get(this.apiUrl + '?pageSize=' + pageSize + '&pageNumber=' + pageNumber)
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

  public getDetail(id: number): Subject<T> {
    const result: Subject<T> = new Subject<T>();

    this.httpClient
      .get(this.apiUrl + '/' + id)
      .subscribe(
        (response: any) => {
          result.next(response.data);
        },
        (error: any) => {
          this.handleError(error);
          result.next(null);
        }
      );

    return result;
  }

  public create(model: any): Subject<T> {
    const result: Subject<T> = new Subject<T>();

    this.httpClient
      .post(this.apiUrl, JSON.stringify(model), {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      })
      .subscribe(
        (response: any) => {
          result.next(response.data);
        },
        (error: any) => {
          this.handleError(error);
          result.next(null);
        }
      );

    return result;
  }

  public edit(id: number, model: any): Subject<T> {
    const result: Subject<T> = new Subject<T>();

    model.id = id;

    this.httpClient
      .put(this.apiUrl + '/' + id, JSON.stringify(model), {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      })
      .subscribe(
        (response: any) => {
          result.next(response.data);
        },
        (error: any) => {
          this.handleError(error);
          result.next(null);
        }
      );

    return result;
  }

  public delete(id: number): Subject<boolean> {
    const result: Subject<boolean> = new Subject<boolean>();

    this.httpClient
      .delete(this.apiUrl + '/' + id, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      })
      .subscribe(
        (response: any) => {
          result.next(response.status);
        },
        (error: any) => {
          this.handleError(error);
          result.next(false);
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
