import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { HooksRoutingModule } from './hooks.routing';

import { HookListComponent } from './list/hook-list.component';
import { HookDetailComponent } from './detail/hook-detail.component';
import { HookCreateComponent } from './create/hook-create.component';
import { HookDeleteComponent } from './delete/hook-delete.component';

@NgModule({
  declarations: [
    HookListComponent,
    HookDetailComponent,
    HookCreateComponent,
    HookDeleteComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HooksRoutingModule
  ]
})
export class HooksModule { }
