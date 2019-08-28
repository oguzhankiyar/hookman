import { Component, OnInit } from '@angular/core';
import { ReceiverService } from '../../_services/receiver.service';
import { NotificationService } from '../../_services/notification.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, Validators, FormArray } from '@angular/forms';
import { ReceiverModel } from '../../_models/receiver.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-receiver-edit',
    templateUrl: 'receiver-edit.component.html'
})
export class ReceiverEditComponent implements OnInit {

    receiverForm = this.fb.group({
        name: ['', Validators.required],
        url: ['', Validators.required],
        path: [''],
        headerItems: this.fb.array([]),
        queryStringItems: this.fb.array([])
    });

    get headerItems(): FormArray {
        return this.receiverForm.get('headerItems') as FormArray;
    }

    get queryStringItems(): FormArray {
        return this.receiverForm.get('queryStringItems') as FormArray;
    }

    constructor(
        private fb: FormBuilder,
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
                    this.receiverForm.patchValue(receiver);

                    for (const key of Object.keys(receiver.headers)) {
                        const value = receiver.headers[key];

                        this.headerItems.push(this.fb.group({
                            key: [key, Validators.required],
                            value: [value, Validators.required]
                        }));
                    }

                    for (const key of Object.keys(receiver.queryStrings)) {
                        const value = receiver.queryStrings[key];

                        this.queryStringItems.push(this.fb.group({
                            key: [key, Validators.required],
                            value: [value, Validators.required]
                        }));
                    }
                });
        });
    }

    addHeader(): void {
        this.headerItems.push(this.fb.group({
            key: ['', Validators.required],
            value: ['', Validators.required]
        }));
    }

    deleteHeader(index): void {
        this.headerItems.removeAt(index);
    }

    addQueryString(): void {
        this.queryStringItems.push(this.fb.group({
            key: ['', Validators.required],
            value: ['', Validators.required]
        }));
    }

    deleteQueryString(index): void {
        this.queryStringItems.removeAt(index);
    }

    save(): void {
        const formValue = this.receiverForm.value;
        const headersValue = formValue.headerItems as { key: any, value: any }[];
        const queryStringsValue = formValue.queryStringItems as { key: any, value: any }[];

        formValue.headers = {};
        for (const item of headersValue) {
            formValue.headers[item.key] = item.value;
        }

        formValue.queryStrings = {};
        for (const item of queryStringsValue) {
            formValue.queryStrings[item.key] = item.value;
        }

        this.notificationService.showInfo('Saving the receiver...');
        this.receiverService
            .edit(this.activatedRoute.snapshot.params.id, formValue)
            .subscribe((receiver: ReceiverModel) => {
                if (receiver) {
                    this.notificationService.showSuccess('Saved the receiver');
                    this.router.navigate(['receivers']);
                }
            });
    }

    cancel(): void {
        this.router.navigate(['receivers']);
    }
}
