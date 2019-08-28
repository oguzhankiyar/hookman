import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { EventsRoutingModule } from './events.routing';

import { EventListComponent } from './list/event-list.component';
import { EventDetailComponent } from './detail/event-detail.component';
import { EventCreateComponent } from './create/event-create.component';
import { EventEditComponent } from './edit/event-edit.component';
import { EventDeleteComponent } from './delete/event-delete.component';

@NgModule({
  declarations: [
    EventListComponent,
    EventDetailComponent,
    EventCreateComponent,
    EventEditComponent,
    EventDeleteComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    EventsRoutingModule
  ]
})
export class EventsModule { }