import { Component, OnInit } from '@angular/core';
import { HookService } from '../../_services/hook.service';
import { NotificationService } from '../../_services/notification.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HookModel } from '../../_models/hook.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-hook-delete',
    templateUrl: 'hook-delete.component.html'
})
export class HookDeleteComponent implements OnInit {

    hook: HookModel;

    constructor(
        private hookService: HookService,
        private notificationService: NotificationService,
        private loaderService: LoaderService,
        private router: Router,
        private activatedRoute: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.loaderService.show();
        this.activatedRoute.params.subscribe((params: Params) => {
            this.hookService
                .getDetail(params.id)
                .subscribe((hook: HookModel) => {
                    this.loaderService.hide();
                    this.hook = hook;
                });
        });
    }

    delete(): void {
        this.notificationService.showInfo('Deleting the hook...');
        this.hookService
            .delete(this.activatedRoute.snapshot.params.id)
            .subscribe((status: boolean) => {
                if (status) {
                    this.notificationService.showSuccess('Deleted the hook');
                    this.router.navigate(['hooks']);
                }
            });
    }

    cancel(): void {
        this.router.navigate(['hooks']);
    }
}