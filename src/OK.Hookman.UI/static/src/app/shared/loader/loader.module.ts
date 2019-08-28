import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LoaderComponent } from './loader.component';

@NgModule({
    imports: [ RouterModule, CommonModule, MatProgressSpinnerModule ],
    declarations: [ LoaderComponent ],
    exports: [ LoaderComponent ]
})

export class LoaderModule {}
