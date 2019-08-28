import { Component, OnInit } from '@angular/core';
import { ReceiverService } from '../../_services/receiver.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ReceiverModel } from '../../_models/receiver.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-receiver-detail',
    templateUrl: 'receiver-detail.component.html'
})
export class ReceiverDetailComponent implements OnInit {

    receiver: ReceiverModel;

    constructor(
        private receiverService: ReceiverService,
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

    edit(): void {
        this.router.navigate(['receivers', 'edit', this.activatedRoute.snapshot.params.id]);
    }

    delete(): void {
        this.router.navigate(['receivers', 'delete', this.activatedRoute.snapshot.params.id]);
    }

    cancel(): void {
        this.router.navigate(['receivers']);
    }
}
