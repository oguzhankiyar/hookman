import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ActionsRoutingModule } from './actions.routing';

import { ActionListComponent } from './list/action-list.component';
import { ActionDetailComponent } from './detail/action-detail.component';
import { ActionCreateComponent } from './create/action-create.component';
import { ActionEditComponent } from './edit/action-edit.component';
import { ActionDeleteComponent } from './delete/action-delete.component';

@NgModule({
  declarations: [
    ActionListComponent,
    ActionDetailComponent,
    ActionCreateComponent,
    ActionEditComponent,
    ActionDeleteComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ActionsRoutingModule
  ]
})
export class ActionsModule { }