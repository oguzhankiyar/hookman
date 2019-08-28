import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EventListComponent } from './list/event-list.component';
import { EventDetailComponent } from './detail/event-detail.component';
import { EventCreateComponent } from './create/event-create.component';
import { EventEditComponent } from './edit/event-edit.component';
import { EventDeleteComponent } from './delete/event-delete.component';

const routes: Routes = [
    { path: '', component: EventListComponent },
    { path: 'detail/:id', component: EventDetailComponent },
    { path: 'create', component: EventCreateComponent },
    { path: 'edit/:id', component: EventEditComponent },
    { path: 'delete/:id', component: EventDeleteComponent }
]

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class EventsRoutingModule {

}