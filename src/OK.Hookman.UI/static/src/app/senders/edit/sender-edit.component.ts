import { Component, OnInit } from '@angular/core';
import { SenderService } from '../../_services/sender.service';
import { NotificationService } from '../../_services/notification.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { SenderModel } from '../../_models/sender.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-sender-edit',
    templateUrl: 'sender-edit.component.html'
})
export class SenderEditComponent implements OnInit {

    senderForm = this.fb.group({
        name: ['', Validators.required]
    });

    constructor(
        private fb: FormBuilder,
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
                    this.senderForm.patchValue(sender);
                });
        });
    }

    save(): void {
        this.notificationService.showInfo('Saving the sender...');
        this.senderService
            .edit(this.activatedRoute.snapshot.params.id, this.senderForm.value)
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
