import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Space {
  id: number;
  name: string;
  type: number;
  capacity: number;
  hasPrivateBathroom: boolean;
}

export interface SpaceCreateDto {
  name: string;
  type: number;
  capacity: number;
  hasPrivateBathroom: boolean;
}

@Injectable({ providedIn: 'root'})
export class SpacesService {
  private readonly url = '/api/spaces';

  constructor (private http: HttpClient){}

  getAll(): Observable<Space[]>{
    return this.http.get<Space[]>(this.url);
  }

  create(dto:SpaceCreateDto): Observable<Space>{
    return this.http.post<Space>(this.url, dto)
  }
}


