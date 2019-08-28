import { EventModel } from './event.model';
import { StatusModel } from './status.model';
import { SenderModel } from './sender.model';

export class HookModel {
    constructor(
        public id: number,
        public eventId: number,
        public senderId: number,
        public statusId: number,
        public data: string,
        public message: string,
        public requestUrl: string,
        public requestHeaders: {[key: string]: string},
        public requestBody: string,
        public responseCode: number,
        public responseHeaders: {[key: string]: string},
        public responseBody: string,
        public event: EventModel,
        public sender: SenderModel,
        public status: StatusModel,
        public createdDate: Date,
        public updatedDate: Date
    ) { }
}
