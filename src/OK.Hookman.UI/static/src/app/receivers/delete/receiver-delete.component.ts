import { Component, OnInit } from '@angular/core';
import { ReceiverService } from '../../_services/receiver.service';
import { NotificationService } from '../../_services/notification.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ReceiverModel } from '../../_models/receiver.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-receiver-delete',
    templateUrl: 'receiver-delete.component.html'
})
export class ReceiverDeleteComponent implements OnInit {

    receiver: ReceiverModel;

    constructor(
        private receiverService: ReceiverService,
        private notificationService: NotificationService,
        private loaderService: LoaderService,
        private router: Router,
        private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.loaderService.show();
        this.activatedRoute.params.subscribe((params: Params) => {
            this.receiverService
                .getDetail(params.id)
                .subscribe((receiver: ReceiverModel) => {
                    this.loaderService.hide();
                    this.receiver = receiver;
                });
        });
    }

    delete(): void {
        this.notificationService.showInfo('Deleting the receiver...');
        this.receiverService
            .delete(this.activatedRoute.snapshot.params.id)
            .subscribe((status: boolean) => {
                if (status) {
                    this.notificationService.showSuccess('Deleted the receiver');
                    this.router.navigate(['receivers']);
                }
            });
    }

    cancel(): void {
        this.router.navigate(['receivers']);
    }
}
