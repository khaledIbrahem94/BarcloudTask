import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { HttpParamsService } from '../http-params/http-params.service';

@Injectable({
  providedIn: 'root',
})
export class ClientService extends BaseService {
  constructor(
    public override http: HttpClient,
    public override httpParams: HttpParamsService
  ) {
    super(http, httpParams);
    this.ctr = 'Clients';
  }
}
