export class BasePagedResponseModel<T> {
    constructor(
        public recordCount: number,
        public pageCount: number,
        public pageSize: number,
        public pageNumber: number,
        public data: T
    ) { }
}