import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../config/api.config';
import { Pizza } from '../models/models';

@Injectable({ providedIn: 'root' })
export class PizzaService {
  constructor(private http: HttpClient) {}

  listar(): Observable<Pizza[]> {
    return this.http.get<Pizza[]>(`${API_URL}/pizzas`);
  }
}
