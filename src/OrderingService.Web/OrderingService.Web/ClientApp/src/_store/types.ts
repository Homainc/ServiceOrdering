export interface PagedResult<T> {
    value: Array<T>;
    pagesCount: number;
    total: number;
};