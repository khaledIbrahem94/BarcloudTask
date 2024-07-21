import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class HttpParamsService {
  constructor() {}

  public getHttpParams(queryParams?: object): HttpParams {
    let params = new HttpParams();

    if (queryParams) {
      const requestQueryParams = this.convertObjectToQueryParams(queryParams);
      Object.keys(requestQueryParams).forEach((key) => {
        const safeKey = key as keyof typeof requestQueryParams;
        params = params.append(key, requestQueryParams[safeKey]);
      });
    }

    return params;
  }

  private convertObjectToQueryParams<T>(obj: T): { [key in keyof T]: string } {
    const stringObj: { [key in keyof T]?: string } = {};

    for (const key in obj) {
      if (Object.prototype.hasOwnProperty.call(obj, key)) {
        const value = obj[key as keyof T];
        stringObj[key] =
          value !== null && value !== undefined ? String(value) : '';
      }
    }

    return stringObj as { [key in keyof T]: string };
  }
}
