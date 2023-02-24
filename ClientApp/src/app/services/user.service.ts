import { Inject, Injectable } from '@angular/core';

import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { IUserViewModel } from '../model/IUserViewModel'
import { IUserFilter } from '../model/IUserFilter';
@Injectable()
export class UserService
{
  baseUrl: string;
  httpClient: HttpClient;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.httpClient = http;
  }

  getUsers(filter: IUserFilter): IUserViewModel[] {
    let parameters = this.MaptoFilter(filter);
    var retVal: IUserViewModel[] = [];
    this.httpClient.get<IUserViewModel[]>(this.baseUrl + 'user', { params: parameters }).subscribe(result => {
     retVal= result;
    }, error => console.error(error));
    return retVal;
  }

  create(model: IUserViewModel)
  {
    let parameters = this.MapFromViewModel(model);
    this.httpClient.post(this.baseUrl + 'user', { parms: parameters }).subscribe(result => { }, error => console.error(error));

  }

  update(model: IUserViewModel) {
    let parameters = this.MapFromViewModel(model);
    this.httpClient.put(this.baseUrl + 'user', { parms: parameters }).subscribe(result => { }, error => console.error(error));

  }
  delete(model: IUserViewModel): boolean | null {
    let parameters = this.MapFromViewModel(model);
    let retVal: boolean | null = null;
    this.httpClient.put(this.baseUrl + 'user', { parms: parameters }).subscribe(result => { }, error => { if (error.status == 304) { retVal = true } retVal = false; });
    return retVal;
  }
  addDefaults()
  {
    this.httpClient.post(this.baseUrl + 'user/AddDefaults', {}).subscribe(result => { }, error => console.error(error));

  }
  MaptoFilter(filter: IUserFilter): HttpParams
  {
    let retVal=new HttpParams();
    retVal = retVal.append('DateOfBirth', (filter.fromDateOfBirth==null?"null": filter.fromDateOfBirth.toISOString()))
    retVal = retVal.append('DateOfBirth', (filter.toDateOfBirth==null?"null":filter.toDateOfBirth.toISOString()))
    retVal = retVal.append('Id', filter.id??"null")
    retVal = retVal.append('NameOrPlaceFilter', filter.nameOrPlaceFilter ?? "null")
    retVal = retVal.append('RecommenderId', filter.recommenderId ?? "null")
    return retVal;
  };
  MapFromViewModel(filter: IUserViewModel): HttpParams {
    let retVal = new HttpParams();
    retVal = retVal.append('DateOfBirth', filter.dateOfBirth.toISOString())
    retVal = retVal.append('Id', filter.id)
    retVal = retVal.append('Name', filter.name)
    retVal = retVal.append('PlaceOfBirth', filter.placeOfBirth)
    retVal = retVal.append('RecommenderId', filter.recommenderId)
    return retVal;
  };
    
}
