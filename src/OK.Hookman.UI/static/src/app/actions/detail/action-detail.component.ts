import { Component, OnInit } from '@angular/core';
import { ActionService } from '../../_services/action.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ActionModel } from '../../_models/action.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-action-detail',
    templateUrl: 'action-detail.component.html'
})
export class ActionDetailComponent implements OnInit {

    action: ActionModel;

    constructor(
        private actionService: ActionService,
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

    edit(): void {
        this.router.navigate(['actions', 'edit', this.activatedRoute.snapshot.params.id]);
    }

    delete(): void {
        this.router.navigate(['actions', 'delete', this.activatedRoute.snapshot.params.id]);
    }

    cancel(): void {
        this.router.navigate(['actions']);
    }
}
