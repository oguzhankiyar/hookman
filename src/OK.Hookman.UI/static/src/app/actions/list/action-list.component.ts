import { Component, OnInit } from '@angular/core';
import { ActionService } from '../../_services/action.service';
import { ActionModel } from '../../_models/action.model';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { BasePagedResponseModel } from '../../_models/basePagedResponse.model';
import { DatePipe } from '@angular/common';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-action-list',
    templateUrl: 'action-list.component.html'
})
export class ActionListComponent implements OnInit {

    public isLoaded: boolean;
    public headers: string[];
    public items: string[][];
    public pageNumber = 1;
    public pageSize = 5;
    public pageCount: number;
    public recordCount: number;
    public pages: { number: number, isActive: boolean, isSeparator: boolean }[];

    constructor(
        private actionService: ActionService,
        private loaderService: LoaderService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private datePipe: DatePipe
    ) { }

    ngOnInit() {
        this.activatedRoute.queryParams.subscribe((params: Params) => {
            if (params.p) {
                this.pageNumber = params.p;
            }
            this.populateData(this.pageNumber);
        });

        this.headers = ['Id', 'Name', 'Created Date'];
    }

    changePage(pageNumber: number) {
        if (pageNumber > this.pageCount) {
            pageNumber = this.pageCount;
        } else if (pageNumber < 0) {
            pageNumber = 1;
        }

        this.router.navigate([], {
            queryParams: {
                p: pageNumber
            }
        });
    }

    populateData(pageNumber: number) {
        this.isLoaded = false;
        this.loaderService.show();
        this.actionService
            .getList(this.pageSize, pageNumber)
            .subscribe((response: BasePagedResponseModel<ActionModel[]>) => {
                this.loaderService.hide();

                this.pageCount = response.pageCount;
                this.pageNumber = response.pageNumber;
                this.recordCount = response.recordCount;

                this.pages = [];
                let isSeparatorAdded = false;
                for (let i = 1; i <= this.pageCount; i++) {
                    if (i === 1 || i === this.pageCount || i >= this.pageNumber - 2 && i <= this.pageNumber + 2) {
                        this.pages.push({ number: i, isActive: this.pageNumber === i, isSeparator: false });
                        isSeparatorAdded = false;
                    } else if (!isSeparatorAdded) {
                        this.pages.push({ number: null, isActive: false, isSeparator: true });
                        isSeparatorAdded = true;
                    }
                }

                this.items = [];
                response.data.forEach(action => {
                    this.items.push([
                        action.id.toString(),
                        action.name,
                        this.datePipe.transform(action.createdDate, 'dd.MM.yyyy HH:mm')
                    ]);
                });

                this.isLoaded = true;
            });
    }

    create(): void {
        this.router.navigate(['actions', 'create']);
    }

    detail(id: number): void {
        this.router.navigate(['actions', 'detail', id]);
    }

    edit(id: number): void {
        this.router.navigate(['actions', 'edit', id]);
    }

    delete(id: number): void {
        this.router.navigate(['actions', 'delete', id]);
    }
}
