import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { SendersRoutingModule } from './senders.routing';

import { SenderListComponent } from './list/sender-list.component';
import { SenderDetailComponent } from './detail/sender-detail.component';
import { SenderCreateComponent } from './create/sender-create.component';
import { SenderEditComponent } from './edit/sender-edit.component';
import { SenderDeleteComponent } from './delete/sender-delete.component';

@NgModule({
  declarations: [
    SenderListComponent,
    SenderDetailComponent,
    SenderCreateComponent,
    SenderEditComponent,
    SenderDeleteComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SendersRoutingModule
  ]
})
export class SendersModule { }