export interface Pagination {
    currentPage: number;
    totalpages: number;
    itemsPerPage: number;
    totalItems: number;
}

export class PaginatedResult<T> {
    result: T;
    pagination: Pagination;
}