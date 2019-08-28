import { Component, OnInit } from '@angular/core';
import { ActionService } from '../../_services/action.service';
import { NotificationService } from '../../_services/notification.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ActionModel } from '../../_models/action.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-action-delete',
    templateUrl: 'action-delete.component.html'
})
export class ActionDeleteComponent implements OnInit {

    action: ActionModel;

    constructor(
        private actionService: ActionService,
        private notificationService: NotificationService,
        private loaderService: LoaderService,
        private router: Router,
        private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.loaderService.show();
        this.activatedRoute.params.subscribe((params: Params) => {
            this.actionService
                .getDetail(params.id)
                .subscribe((action: ActionModel) => {
                    this.loaderService.hide();
                    this.action = action;
                });
        });
    }

    delete(): void {
        this.notificationService.showInfo('Deleting the action...');
        this.actionService
            .delete(this.activatedRoute.snapshot.params.id)
            .subscribe((status: boolean) => {
                if (status) {
                    this.notificationService.showSuccess('Deleted the action');
                    this.router.navigate(['actions']);
                }
            });
    }

    cancel(): void {
        this.router.navigate(['actions']);
    }
}
