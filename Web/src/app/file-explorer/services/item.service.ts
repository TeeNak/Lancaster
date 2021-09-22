import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEvent } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Item, ItemForCreation, ITEM_TYPE } from '../models/item'
import { environment } from '../../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  private fileItemApiPath = '/api/FileItem';
  private fileItemApiUrl = environment.serverUrl + this.fileItemApiPath;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient
  ) {
  }

  getItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this.fileItemApiUrl)
      .pipe(
        tap(_ => this.log('fetched items')),
        catchError(this.handleError<Item[]>('getItems', []))
      );
  }


  addAndUploadItem(formData: FormData): Observable<any> {

    return this.http.post(this.fileItemApiUrl, formData,
      {reportProgress: true, observe: 'events'})
  }

  deleteItem(guid: string): Observable<object> {
    const url = `${this.fileItemApiUrl}/${guid}`;

    return this.http.delete<Item>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted item id=${guid}`)),
      catchError(this.handleError<Item>('deleteItem'))
    );
  }


  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    console.log(`ItemService: ${message}`);
  }

}
