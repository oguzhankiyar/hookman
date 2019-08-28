import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const AppRoutes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

    { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
    { path: 'actions', loadChildren: './actions/actions.module#ActionsModule' },
    { path: 'events', loadChildren: './events/events.module#EventsModule' },
    { path: 'hooks', loadChildren: './hooks/hooks.module#HooksModule' },
    { path: 'receivers', loadChildren: './receivers/receivers.module#ReceiversModule' },
    { path: 'senders', loadChildren: './senders/senders.module#SendersModule' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(AppRoutes),
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
