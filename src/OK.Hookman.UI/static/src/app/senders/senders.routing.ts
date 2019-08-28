import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SenderListComponent } from './list/sender-list.component';
import { SenderDetailComponent } from './detail/sender-detail.component';
import { SenderCreateComponent } from './create/sender-create.component';
import { SenderEditComponent } from './edit/sender-edit.component';
import { SenderDeleteComponent } from './delete/sender-delete.component';

const routes: Routes = [
    { path: '', component: SenderListComponent },
    { path: 'detail/:id', component: SenderDetailComponent },
    { path: 'create', component: SenderCreateComponent },
    { path: 'edit/:id', component: SenderEditComponent },
    { path: 'delete/:id', component: SenderDeleteComponent }
]

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class SendersRoutingModule {

}