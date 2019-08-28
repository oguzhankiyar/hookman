export class ReceiverModel {
    constructor(
        public id: number,
        public name: string,
        public url: string,
        public path: string,
        public headers: {[key: string]: string},
        public queryStrings: {[key: string]: string},
        public createdDate: Date,
        public updatedDate: Date
    ) { }
}
