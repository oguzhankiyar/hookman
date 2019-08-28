import { Component, OnInit } from '@angular/core';
import { ActionService } from '../../_services/action.service';
import { NotificationService } from '../../_services/notification.service';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { ActionModel } from '../../_models/action.model';

@Component({
    selector: 'app-action-create',
    templateUrl: 'action-create.component.html'
})
export class ActionCreateComponent implements OnInit {

    actionForm = this.fb.group({
        name: [null, Validators.required]
    });

    constructor(
        private fb: FormBuilder,
        private actionService: ActionService,
        private notificationService: NotificationService,
        private router: Router
    ) { }

    ngOnInit(): void {

    }

    save(): void {
        this.notificationService.showInfo('Saving the action...');
        this.actionService
            .create(this.actionForm.value)
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
