import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ReceiversRoutingModule } from './receivers.routing';

import { ReceiverListComponent } from './list/receiver-list.component';
import { ReceiverDetailComponent } from './detail/receiver-detail.component';
import { ReceiverCreateComponent } from './create/receiver-create.component';
import { ReceiverEditComponent } from './edit/receiver-edit.component';
import { ReceiverDeleteComponent } from './delete/receiver-delete.component';

@NgModule({
  declarations: [
    ReceiverListComponent,
    ReceiverDetailComponent,
    ReceiverCreateComponent,
    ReceiverEditComponent,
    ReceiverDeleteComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ReceiversRoutingModule
  ]
})
export class ReceiversModule { }