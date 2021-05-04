import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs/operators";
import { PaginatedResult} from './../models/pagination';

export function getPaginatedResult<T>(http: HttpClient,url: string, params: HttpParams) {
    let paginatedResult = new PaginatedResult<T>();
    return http
      .get<T[]>(url, { observe: 'response', params })
      .pipe(
        map((response) => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return paginatedResult;
        })
      );
  }

  export function getPaginationHeaders(pageNumber?: number , pageSize?: number) {
    let params = new HttpParams();
    if (pageNumber)
      params = params.append('pageIndex', pageNumber.toString());
    if (pageSize)
      params = params.append('pageSize', pageSize.toString());

    return params;
  }