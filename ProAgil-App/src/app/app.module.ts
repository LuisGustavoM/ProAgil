import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TooltipModule, BsDropdownModule, ModalModule, BsModalService, BsDatepickerModule} from 'ngx-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';

import { EventoService } from './_services/evento.service';

import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';
import { ContatosComponent } from './contatos/contatos.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { TituloComponent} from './_shared/titulo/titulo.component';


import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';
import { from } from 'rxjs';
import { PlatformLocation } from '@angular/common';
import { UserComponent } from './User/User.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';



@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipePipe,
      ContatosComponent,
      DashboardComponent,
      PalestrantesComponent,
      TituloComponent,
      UserComponent,
      RegistrationComponent,
      LoginComponent
   ],
   imports: [
      BrowserModule,
      BsDatepickerModule.forRoot(),
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      ToastrModule.forRoot(),
      BrowserAnimationsModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule
   ],
   providers: [
      EventoService,
      BsModalService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
