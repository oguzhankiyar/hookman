import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ReceiverListComponent } from './list/receiver-list.component';
import { ReceiverDetailComponent } from './detail/receiver-detail.component';
import { ReceiverCreateComponent } from './create/receiver-create.component';
import { ReceiverEditComponent } from './edit/receiver-edit.component';
import { ReceiverDeleteComponent } from './delete/receiver-delete.component';

const routes: Routes = [
    { path: '', component: ReceiverListComponent },
    { path: 'detail/:id', component: ReceiverDetailComponent },
    { path: 'create', component: ReceiverCreateComponent },
    { path: 'edit/:id', component: ReceiverEditComponent },
    { path: 'delete/:id', component: ReceiverDeleteComponent }
]

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class ReceiversRoutingModule {

}