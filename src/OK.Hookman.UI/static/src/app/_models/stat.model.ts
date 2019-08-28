export class StatTopActionModel {
    constructor(
        public action: string,
        public values: StatTopActionDateValueModel[]
    ) { }
}

export class StatTopActionDateValueModel {
    constructor(
        public date: string,
        public value: string
    ) { }
}
