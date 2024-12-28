import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
export interface Car {
  id: number;
  name: string;
  airDate: string;
  episode: string;
}

export interface PaginationResult<T> {
  totalCount: number;
  totalPages: number;
  page: number;
  pageSize: number;
  data: T[];
}

@Injectable({
  providedIn: 'root'
})
export class RickAndMortyService {
  private readonly apiUrl = environment.urlBack;

  constructor(private http: HttpClient) {}

  getEpisodes<T>(page: number, pageSize: number): Observable<PaginationResult<T>> {
    return this.http.get<PaginationResult<T>>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}`);
  }
}
