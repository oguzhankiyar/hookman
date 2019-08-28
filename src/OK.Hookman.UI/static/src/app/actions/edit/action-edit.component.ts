import { Component, OnInit } from '@angular/core';
import { ActionService } from '../../_services/action.service';
import { NotificationService } from '../../_services/notification.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { ActionModel } from '../../_models/action.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-action-edit',
    templateUrl: 'action-edit.component.html'
})
export class ActionEditComponent implements OnInit {

    actionForm = this.fb.group({
        name: ['', Validators.required]
    });

    constructor(
        private fb: FormBuilder,
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
                    this.actionForm.patchValue(action);
                });
        });
    }

    save(): void {
        this.notificationService.showInfo('Saving the action...');
        this.actionService
            .edit(this.activatedRoute.snapshot.params.id, this.actionForm.value)
            .subscribe((action: ActionModel) => {
                if (action) {
                    this.notificationService.showSuccess('Saved the action');
                    this.router.navigate(['actions']);
                }
            });
    }

    cancel(): void {
        this.router.navigate(['actions']);
    }
}
