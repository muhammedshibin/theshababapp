export class PaginatedResult<T>{
    result: T[];
    pagination: Pagination;
}

export interface Pagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    totalPages: number;
  }