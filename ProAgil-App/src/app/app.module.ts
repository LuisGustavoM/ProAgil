import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TooltipModule, BsDropdownModule, ModalModule, BsModalService, BsDatepickerModule, TabsModule} from 'ngx-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { ToastrModule } from 'ngx-toastr';
import { EventoService } from './_services/evento.service';

import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { EventoEditComponent } from './eventos/eventoEdit/eventoEdit.component';
import { NavComponent } from './nav/nav.component';
import { ContatosComponent } from './contatos/contatos.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { TituloComponent} from './_shared/titulo/titulo.component';

import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';
import { UserComponent } from './User/User.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { NgxMaskModule } from 'ngx-mask';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      EventoEditComponent,
      NavComponent,
      DateTimeFormatPipePipe,
      ContatosComponent,
      DashboardComponent,
      PalestrantesComponent,
      TituloComponent,
      UserComponent,
      RegistrationComponent,
      LoginComponent,

   ],

   imports: [
      NgxMaskModule.forRoot(),
      TabsModule.forRoot(),
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
      {
         provide: HTTP_INTERCEPTORS,
         useClass: AuthInterceptor,
         multi: true
      },
      BsModalService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
