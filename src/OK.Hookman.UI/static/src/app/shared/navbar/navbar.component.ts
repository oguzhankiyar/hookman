import { Component, OnInit, ViewChild, ElementRef, Renderer2 } from '@angular/core';
import { ROUTES } from '../sidebar/sidebar.component';
import { Router, ActivatedRoute } from '@angular/router';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';

@Component({
    selector: 'app-navbar',
    templateUrl: 'navbar.component.html'
})

export class NavbarComponent implements OnInit {
    private listTitles: any[];
    location: Location;
    private nativeElement: Node;
    private toggleButton;
    private sidebarVisible: boolean;

    // @ViewChild('navbar-cmp') button;

    constructor(location: Location, private renderer: Renderer2, private element: ElementRef, private router: Router) {
        this.location = location;
        this.nativeElement = element.nativeElement;
        this.sidebarVisible = false;
    }

    ngOnInit() {
        this.listTitles = ROUTES.filter(listTitle => listTitle);
        const navbar: HTMLElement = this.element.nativeElement;
        this.toggleButton = navbar.getElementsByClassName('navbar-toggle')[0];
        this.router.events.subscribe((event) => {
            this.sidebarClose();
        });
    }

    getTitle() {
        const baseHref = document.getElementsByTagName('base')[0].href;
        let path = window.location.href.replace(baseHref, '');
        if (path.includes('/')) {
            path = path.split('/')[0];
        }

        for (const item of this.listTitles) {
            if (item.path === path) {
                return item.title;
            }
        }

        return 'DASHBOARD';
    }

    sidebarToggle() {
        // const $toggle = document.getElementsByClassName('navbar-toggler')[0];

        if (this.sidebarVisible === false) {
            this.sidebarOpen();
        } else {
            this.sidebarClose();
        }
    }

    sidebarOpen() {
        const toggleButton = this.toggleButton;
        const body = document.getElementsByTagName('body')[0];
        setTimeout(() => {
            toggleButton.classList.add('toggled');
        }, 500);

        body.classList.add('nav-open');

        this.sidebarVisible = true;
    }

    sidebarClose() {
        const body = document.getElementsByTagName('body')[0];
        this.toggleButton.classList.remove('toggled');
        this.sidebarVisible = false;
        body.classList.remove('nav-open');
    }
}
