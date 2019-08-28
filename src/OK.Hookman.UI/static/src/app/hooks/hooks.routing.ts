import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HookListComponent } from './list/hook-list.component';
import { HookDetailComponent } from './detail/hook-detail.component';
import { HookCreateComponent } from './create/hook-create.component';
import { HookDeleteComponent } from './delete/hook-delete.component';

const routes: Routes = [
    { path: '', component: HookListComponent },
    { path: 'detail/:id', component: HookDetailComponent },
    { path: 'create', component: HookCreateComponent },
    { path: 'delete/:id', component: HookDeleteComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class HooksRoutingModule {

}
