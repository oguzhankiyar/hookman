import { Component, OnInit } from '@angular/core';
import { SenderService } from '../../_services/sender.service';
import { NotificationService } from '../../_services/notification.service';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { SenderModel } from '../../_models/sender.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-sender-create',
    templateUrl: 'sender-create.component.html'
})
export class SenderCreateComponent implements OnInit {

    senderForm = this.fb.group({
        name: ['', Validators.required]
    });

    constructor(
        private fb: FormBuilder,
        private senderService: SenderService,
        private notificationService: NotificationService,
        private loaderService: LoaderService,
        private router: Router
    ) { }

    ngOnInit(): void {

    }

    save(): void {
        this.notificationService.showInfo('Saving the sender...');
        this.senderService
            .create(this.senderForm.value)
            .subscribe((sender: SenderModel) => {
                if (sender) {
                    this.notificationService.showSuccess('Saved the sender');
                    this.router.navigate(['senders']);
                }
            });
    }

    cancel(): void {
        this.router.navigate(['senders']);
    }
}
