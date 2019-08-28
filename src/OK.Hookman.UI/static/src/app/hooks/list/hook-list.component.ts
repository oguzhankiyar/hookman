import { Component, OnInit } from '@angular/core';
import { HookService } from '../../_services/hook.service';
import { HookModel } from '../../_models/hook.model';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { BasePagedResponseModel } from '../../_models/basePagedResponse.model';
import { DatePipe } from '@angular/common';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-hook-list',
    templateUrl: 'hook-list.component.html'
})
export class HookListComponent implements OnInit {

    public isLoaded: boolean;
    public headers: string[];
    public items: string[][];
    public pageNumber = 1;
    public pageSize = 5;
    public pageCount: number;
    public recordCount: number;
    public pages: { number: number, isActive: boolean, isSeparator: boolean }[];

    constructor(
        private hookService: HookService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private loaderService: LoaderService,
        private datePipe: DatePipe
    ) { }

    ngOnInit() {
        this.activatedRoute.queryParams.subscribe((params: Params) => {
            if (params.p) {
                this.pageNumber = params.p;
            }
            this.populateData(this.pageNumber);
        });

        this.headers = ['Id', 'Sender', 'Receiver', 'Action', 'Status', 'Created Date'];
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
        this.hookService
            .getList(this.pageSize, pageNumber)
            .subscribe((response: BasePagedResponseModel<HookModel[]>) => {
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
                response.data.forEach(hook => {
                    this.items.push([
                        hook.id.toString(),
                        hook.sender.name,
                        hook.event.receiver.name,
                        hook.event.action.name,
                        hook.status.name,
                        this.datePipe.transform(hook.createdDate, 'dd.MM.yyyy HH:mm')
                    ]);
                });

                this.isLoaded = true;
            });
    }

    create(): void {
        this.router.navigate(['hooks', 'create']);
    }

    detail(id: number): void {
        this.router.navigate(['hooks', 'detail', id]);
    }

    delete(id: number): void {
        this.router.navigate(['hooks', 'delete', id]);
    }
}
