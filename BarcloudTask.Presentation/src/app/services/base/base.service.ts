import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../assets/environment/environment';
import { GridRequestParamters } from '../../models/base/grid-request';
import { SaveAction } from '../../models/base/saveaction';
import { HttpParamsService } from '../http-params/http-params.service';

@Injectable({
  providedIn: 'root',
})
export class BaseService {
  private readonly apiBaseUrl = `${environment.baseUrl}`;
  public ctr: string = '';
  constructor(public http: HttpClient, public httpParams: HttpParamsService) {}

  getAll<T>(
    gridRequestParams: GridRequestParamters | undefined = undefined
  ): Observable<T> {
    let params: HttpParams | undefined = undefined;
    if (gridRequestParams) {
      params = this.httpParams.getHttpParams(gridRequestParams);
    }
    return this.http.get<T>(`${this.apiBaseUrl}/${this.ctr}/GetAll`, {
      params,
    });
  }

  getById<T>(id: number): Observable<T> {
    return this.http.get<T>(`${this.apiBaseUrl}/${this.ctr}/GetById/${id}`);
  }

  create<T>(entity: T): Observable<SaveAction> {
    return this.http.post<SaveAction>(
      `${this.apiBaseUrl}/${this.ctr}/Create`,
      entity
    );
  }

  update<T>(entity: T): Observable<SaveAction> {
    return this.http.put<SaveAction>(
      `${this.apiBaseUrl}/${this.ctr}/Update`,
      entity
    );
  }

  delete(id: number): Observable<SaveAction> {
    return this.http.delete<SaveAction>(
      `${this.apiBaseUrl}/${this.ctr}/Delete/${id}`
    );
  }
}
