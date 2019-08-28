import { Component, OnInit } from '@angular/core';
import { EventService } from '../../_services/event.service';
import { NotificationService } from '../../_services/notification.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, Validators, FormArray } from '@angular/forms';
import { EventModel } from '../../_models/event.model';
import { SenderService } from '../../_services/sender.service';
import { ReceiverService } from '../../_services/receiver.service';
import { ActionService } from '../../_services/action.service';
import { BasePagedResponseModel } from '../../_models/basePagedResponse.model';
import { SenderModel } from '../../_models/sender.model';
import { ReceiverModel } from '../../_models/receiver.model';
import { ActionModel } from '../../_models/action.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-event-edit',
    templateUrl: 'event-edit.component.html'
})
export class EventEditComponent implements OnInit {

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
        private router: Router,
        private activatedRoute: ActivatedRoute
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

        this.loaderService.show();
        this.activatedRoute.params.subscribe((params: Params) => {
            this.eventService
                .getDetail(params.id)
                .subscribe((event: EventModel) => {
                    this.loaderService.hide();
                    this.eventForm.patchValue(event);

                    for (const key of Object.keys(event.headers)) {
                        const value = event.headers[key];

                        this.headerItems.push(this.fb.group({
                            key: [key, Validators.required],
                            value: [value, Validators.required]
                        }));
                    }

                    for (const key of Object.keys(event.queryStrings)) {
                        const value = event.queryStrings[key];

                        this.queryStringItems.push(this.fb.group({
                            key: [key, Validators.required],
                            value: [value, Validators.required]
                        }));
                    }
                });
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
            .edit(this.activatedRoute.snapshot.params.id, formValue)
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
