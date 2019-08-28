import { Component, OnInit } from '@angular/core';

declare var $: any;

export interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}

export const ROUTES: RouteInfo[] = [
    { path: 'dashboard', title: 'DASHBOARD', icon: 'ti-angle-right', class: '' },
    { path: 'actions', title: 'ACTIONS', icon: 'ti-angle-right', class: '' },
    { path: 'senders', title: 'SENDERS', icon: 'ti-angle-right', class: '' },
    { path: 'receivers', title: 'RECEIVERS', icon: 'ti-angle-right', class: '' },
    { path: 'events', title: 'EVENTS', icon: 'ti-angle-right', class: '' },
    { path: 'hooks', title: 'HOOKS', icon: 'ti-angle-right', class: '' }
];

@Component({
    selector: 'app-sidebar',
    templateUrl: 'sidebar.component.html',
})
export class SidebarComponent implements OnInit {
    public menuItems: any[];

    ngOnInit() {
        this.menuItems = ROUTES.filter(menuItem => menuItem);
    }

    isNotMobileMenu() {
        if ($(window).width() > 991) {
            return false;
        }
        return true;
    }
}
