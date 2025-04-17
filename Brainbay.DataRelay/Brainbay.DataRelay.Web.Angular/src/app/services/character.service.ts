import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedResult } from '../models/paged-result.model';
import { CharacterDetail } from '../models/character-detail.model';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {
  private baseUrl = 'api/Character';

  constructor(private http: HttpClient) { }

  getCharactersByOrigin(origin: string, page: number, pageSize: number): Observable<PagedResult<CharacterDetail>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<PagedResult<CharacterDetail>>(`${this.baseUrl}/origin/${origin}`, { params });
  }

  getAllCharacters(page: number, pageSize: number): Observable<PagedResult<CharacterDetail>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<PagedResult<CharacterDetail>>(this.baseUrl, { params });
  }

  addCharacter(character: any): Observable<any> {
    return this.http.post(this.baseUrl, character);
  }
}
