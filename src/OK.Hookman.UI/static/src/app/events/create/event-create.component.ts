import { Component, OnInit } from '@angular/core';
import { EventService } from '../../_services/event.service';
import { NotificationService } from '../../_services/notification.service';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormArray } from '@angular/forms';
import { EventModel } from '../../_models/event.model';
import { SenderService } from '../../_services/sender.service';
import { ReceiverService } from '../../_services/receiver.service';
import { ActionService } from '../../_services/action.service';
import { SenderModel } from '../../_models/sender.model';
import { BasePagedResponseModel } from '../../_models/basePagedResponse.model';
import { ReceiverModel } from '../../_models/receiver.model';
import { ActionModel } from '../../_models/action.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-event-create',
    templateUrl: 'event-create.component.html'
})
export class EventCreateComponent implements OnInit {

    senders = [];
    receivers = [];
    actions = [];
    methods = ['GET', 'POST', 'PUT', 'DELETE'];

    eventForm = this.fb.group({
        senderId: [null],
        receiverId: [null, Validators.required],
        actionId: [null, Validators.required],
        method: [null, Validators.required],
        path: [null],
        headerItems: this.fb.array([]),
        queryStringItems: this.fb.array([]),
        body: [null],
        retryCount: [null, Validators.required]
    });

    get headerItems(): FormArray {
        return this.eventForm.get('headerItems') as FormArray;
    }

    get queryStringItems(): FormArray {
        return this.eventForm.get('queryStringItems') as FormArray;
    }

    constructor(
        private fb: FormBuilder,
        private eventService: EventService,
        private senderService: SenderService,
        private receiverService: ReceiverService,
        private actionService: ActionService,
        private notificationService: NotificationService,
        private loaderService: LoaderService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.loaderService.show();
        this.senderService
            .getList(100, 1)
            .subscribe((response: BasePagedResponseModel<SenderModel[]>) => {
                this.loaderService.hide();
                this.senders = response.data;
            });

        this.loaderService.show();
        this.receiverService
            .getList(100, 1)
            .subscribe((response: BasePagedResponseModel<ReceiverModel[]>) => {
                this.loaderService.hide();
                this.receivers = response.data;
            });

        this.loaderService.show();
        this.actionService
            .getList(100, 1)
            .subscribe((response: BasePagedResponseModel<ActionModel[]>) => {
                this.loaderService.hide();
                this.actions = response.data;
            });
    }

    addHeader(): void {
        this.headerItems.push(this.fb.group({
            key: ['', Validators.required],
            value: ['', Validators.required]
        }));
    }

    deleteHeader(index): void {
        this.headerItems.removeAt(index);
    }

    addQueryString(): void {
        this.queryStringItems.push(this.fb.group({
            key: ['', Validators.required],
            value: ['', Validators.required]
        }));
    }

    deleteQueryString(index): void {
        this.queryStringItems.removeAt(index);
    }

    save(): void {
        const formValue = this.eventForm.value;
        const headersValue = formValue.headerItems as { key: any, value: any }[];
        const queryStringsValue = formValue.queryStringItems as { key: any, value: any }[];

        formValue.headers = {};
        for (const item of headersValue) {
            formValue.headers[item.key] = item.value;
        }

        formValue.queryStrings = {};
        for (const item of queryStringsValue) {
            formValue.queryStrings[item.key] = item.value;
        }

        this.notificationService.showInfo('Saving the event...');
        this.eventService
            .create(formValue)
            .subscribe((event: EventModel) => {
                if (event) {
                    this.notificationService.showSuccess('Saved the event');
                    this.router.navigate(['events']);
                }
            });
    }

    cancel(): void {
        this.router.navigate(['events']);
    }
}
