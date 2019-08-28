import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app.routing';

import { DatePipe } from '@angular/common';

import { StatService } from './_services/stat.service';
import { ActionService } from './_services/action.service';
import { SenderService } from './_services/sender.service';
import { ReceiverService } from './_services/receiver.service';
import { EventService } from './_services/event.service';
import { HookService } from './_services/hook.service';
import { NotificationService } from './_services/notification.service';
import { LoaderService } from './_services/loader.service';

import { AppComponent } from './app.component';
import { SidebarModule } from './shared/sidebar/sidebar.module';
import { NavbarModule } from './shared/navbar/navbar.module';
import { LoaderModule } from './shared/loader/loader.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { ActionsModule } from './actions/actions.module';
import { EventsModule } from './events/events.module';
import { ReceiversModule } from './receivers/receivers.module';
import { SendersModule } from './senders/senders.module';
import { HooksModule } from './hooks/hooks.module';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,

    SidebarModule,
    NavbarModule,
    LoaderModule,

    DashboardModule,
    ActionsModule,
    EventsModule,
    ReceiversModule,
    SendersModule,
    HooksModule,

    BrowserAnimationsModule,
    NgxChartsModule,
    MatProgressSpinnerModule
  ],
  providers: [
    DatePipe,
    StatService,
    ActionService,
    SenderService,
    ReceiverService,
    EventService,
    HookService,
    NotificationService,
    LoaderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
