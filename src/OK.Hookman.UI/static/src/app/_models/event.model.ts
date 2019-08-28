import { SenderModel } from './sender.model';
import { ReceiverModel } from './receiver.model';
import { ActionModel } from './action.model';

export class EventModel {
    constructor(
        public id: number,
        public senderId: number,
        public receiverId: number,
        public actionId: number,
        public method: string,
        public path: string,
        public headers: {[key: string]: string},
        public queryStrings: {[key: string]: string},
        public body: string,
        public retryCount: number,
        public createdDate: Date,
        public updatedDate: Date,
        public sender: SenderModel,
        public receiver: ReceiverModel,
        public action: ActionModel
    ) { }
}
