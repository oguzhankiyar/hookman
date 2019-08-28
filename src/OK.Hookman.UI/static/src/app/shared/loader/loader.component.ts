import { Component, OnInit, Input } from '@angular/core';
import { LoaderService } from '../../_services/loader.service';

@Component({
    selector: 'app-loader',
    templateUrl: 'loader.component.html'
})
export class LoaderComponent implements OnInit {

    @Input() isVisible: boolean;

    constructor(private loaderService: LoaderService) { }

    ngOnInit(): void {
        this.loaderService.onToggle.subscribe((status) => {
            this.isVisible = status;
        });
    }
}