import { Component, OnInit } from '@angular/core';
import { EventService } from '../../_services/event.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { EventModel } from '../../_models/event.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-event-detail',
    templateUrl: 'event-detail.component.html'
})
export class EventDetailComponent implements OnInit {

    event: EventModel;

    constructor(
        private eventService: EventService,
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

    edit(): void {
        this.router.navigate(['events', 'edit', this.activatedRoute.snapshot.params.id]);
    }

    delete(): void {
        this.router.navigate(['events', 'delete', this.activatedRoute.snapshot.params.id]);
    }

    cancel(): void {
        this.router.navigate(['events']);
    }
}
