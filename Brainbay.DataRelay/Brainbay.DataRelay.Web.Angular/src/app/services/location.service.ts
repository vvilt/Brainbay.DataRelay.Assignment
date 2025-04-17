import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LocationIdName } from '../models/location-id-name.model';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private baseUrl = 'api/Location';

  constructor(private http: HttpClient) { }

  getLocationIdName(): Observable<LocationIdName[]> {
    return this.http.get<LocationIdName[]>(this.baseUrl);
   }
}
