import { Component, OnInit } from '@angular/core';
import { HookService } from '../../_services/hook.service';
import { NotificationService } from '../../_services/notification.service';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { HookModel } from '../../_models/hook.model';
import { EventService } from '../../_services/event.service';
import { BasePagedResponseModel } from '../../_models/basePagedResponse.model';
import { EventModel } from '../../_models/event.model';
import { LoaderService } from '../../_services/loader.service';
import { ActionService } from 'src/app/_services/action.service';
import { ActionModel } from 'src/app/_models/action.model';
import { SenderService } from 'src/app/_services/sender.service';
import { SenderModel } from 'src/app/_models/sender.model';

@Component({
    selector: 'app-hook-create',
    templateUrl: 'hook-create.component.html'
})
export class HookCreateComponent implements OnInit {

    creationType = 1;
    events = [];
    senders = [];
    actions = [];

    hookForm = this.fb.group({
        eventId: [null],
        senderToken: [null],
        actionId: [null],
        data: [null]
    });

    constructor(
        private fb: FormBuilder,
        private hookService: HookService,
        private eventService: EventService,
        private senderService: SenderService,
        private actionService: ActionService,
        private notificationService: NotificationService,
        private loaderService: LoaderService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.loaderService.show();
        this.eventService
            .getList(100, 1)
            .subscribe((response: BasePagedResponseModel<EventModel[]>) => {
                this.loaderService.hide();
                this.events = response.data;
            });

        this.loaderService.show();
        this.senderService
            .getList(100, 1)
            .subscribe((response: BasePagedResponseModel<SenderModel[]>) => {
                this.loaderService.hide();
                this.senders = response.data;
            });

        this.loaderService.show();
        this.actionService
            .getList(100, 1)
            .subscribe((response: BasePagedResponseModel<ActionModel[]>) => {
                this.loaderService.hide();
                this.actions = response.data;
            });
    }

    save(): void {
        const request = this.hookForm.value;

        if (this.creationType === 1) {
            if (!request.eventId) {
                this.notificationService.showWarning('Please specify an event');
            }
            request.actionId = null;
        } else if (this.creationType === 2) {
            if (!request.actionId) {
                this.notificationService.showWarning('Please specify an action');
            }
            request.eventId = null;
        }

        this.notificationService.showInfo('Saving the hook...');
        this.hookService
            .create(request)
            .subscribe(() => {
                this.notificationService.showSuccess('Saved the hook');
                this.router.navigate(['hooks']);
            });
    }

    cancel(): void {
        this.router.navigate(['hooks']);
    }
}
