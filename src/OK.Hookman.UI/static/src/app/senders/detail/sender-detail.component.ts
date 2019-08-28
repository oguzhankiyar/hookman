import { Component, OnInit } from '@angular/core';
import { SenderService } from '../../_services/sender.service';
import { NotificationService } from '../../_services/notification.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { SenderModel } from '../../_models/sender.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-sender-detail',
    templateUrl: 'sender-detail.component.html'
})
export class SenderDetailComponent implements OnInit {

    sender: SenderModel;

    constructor(
        private senderService: SenderService,
        private notificationService: NotificationService,
        private loaderService: LoaderService,
        private router: Router,
        private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.loaderService.show();
        this.activatedRoute.params.subscribe((params: Params) => {
            this.senderService
                .getDetail(params.id)
                .subscribe((sender: SenderModel) => {
                    this.loaderService.hide();
                    this.sender = sender;
                });
        });
    }

    edit(): void {
        this.router.navigate(['senders', 'edit', this.activatedRoute.snapshot.params.id]);
    }

    delete(): void {
        this.router.navigate(['senders', 'delete', this.activatedRoute.snapshot.params.id]);
    }

    cancel(): void {
        this.router.navigate(['senders']);
    }
}
