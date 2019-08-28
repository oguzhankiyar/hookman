export class SenderModel {
    constructor(
        public id: number,
        public token: string,
        public name: string,
        public createdDate: Date,
        public updatedDate: Date
    ) { }
}
