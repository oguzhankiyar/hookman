import { Component, OnInit } from '@angular/core';
import { SenderService } from '../../_services/sender.service';
import { NotificationService } from '../../_services/notification.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { SenderModel } from '../../_models/sender.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-sender-delete',
    templateUrl: 'sender-delete.component.html'
})
export class SenderDeleteComponent implements OnInit {

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

    delete(): void {
        this.notificationService.showInfo('Deleting the sender...');
        this.senderService
            .delete(this.activatedRoute.snapshot.params.id)
            .subscribe((status: boolean) => {
                if (status) {
                    this.notificationService.showSuccess('Deleted the sender');
                    this.router.navigate(['senders']);
                }
            });
    }

    cancel(): void {
        this.router.navigate(['senders']);
    }
}
