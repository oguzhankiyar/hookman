import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard.routing';

import { DashboardComponent } from './dashboard.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    NgxChartsModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
