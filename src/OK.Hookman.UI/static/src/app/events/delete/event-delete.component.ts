import { Component, OnInit } from '@angular/core';
import { EventService } from '../../_services/event.service';
import { NotificationService } from '../../_services/notification.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { EventModel } from '../../_models/event.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-event-delete',
    templateUrl: 'event-delete.component.html'
})
export class EventDeleteComponent implements OnInit {

    event: EventModel;

    constructor(
        private eventService: EventService,
        private notificationService: NotificationService,
        private loaderService: LoaderService,
        private router: Router,
        private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.loaderService.show();
        this.activatedRoute.params.subscribe((params: Params) => {
            this.eventService
                .getDetail(params.id)
                .subscribe((event: EventModel) => {
                    this.loaderService.hide();
                    this.event = event;
                });
        });
    }

    delete(): void {
        this.notificationService.showInfo('Deleting the event...');
        this.eventService
            .delete(this.activatedRoute.snapshot.params.id)
            .subscribe((status: boolean) => {
                if (status) {
                    this.notificationService.showSuccess('Deleted the event');
                    this.router.navigate(['events']);
                }
            });
    }

    cancel(): void {
        this.router.navigate(['events']);
    }
}
