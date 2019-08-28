import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ActionListComponent } from './list/action-list.component';
import { ActionDetailComponent } from './detail/action-detail.component';
import { ActionCreateComponent } from './create/action-create.component';
import { ActionEditComponent } from './edit/action-edit.component';
import { ActionDeleteComponent } from './delete/action-delete.component';

const routes: Routes = [
    { path: '', component: ActionListComponent },
    { path: 'detail/:id', component: ActionDetailComponent },
    { path: 'create', component: ActionCreateComponent },
    { path: 'edit/:id', component: ActionEditComponent },
    { path: 'delete/:id', component: ActionDeleteComponent }
]

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class ActionsRoutingModule {

}