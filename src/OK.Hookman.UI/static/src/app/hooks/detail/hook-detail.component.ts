import { Component, OnInit } from '@angular/core';
import { HookService } from '../../_services/hook.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HookModel } from '../../_models/hook.model';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-hook-detail',
    templateUrl: 'hook-detail.component.html'
})
export class HookDetailComponent implements OnInit {

    hook: HookModel;

    constructor(
        private hookService: HookService,
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
        this.router.navigate(['hooks', 'delete', this.activatedRoute.snapshot.params.id]);
    }

    cancel(): void {
        this.router.navigate(['hooks']);
    }
}
