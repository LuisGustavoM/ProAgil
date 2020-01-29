import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  titulo = 'Login';
  model: any = {};

    constructor(
      private toastr: ToastrService,
      public authService: AuthService,
      public router: Router) {}

  ngOnInit() {
    }

    logout() {
      localStorage.removeItem('token');
      this.toastr.show('DESCONECTADO');
      this.router.navigate(['/user/login']);
    }

  loggdIn() {
    this.authService.login(this.model).subscribe(
      () => {
        this.router.navigate(['/eventos']);
        this.toastr.success('Conectado');
      },
      error => {
        this.toastr.error('Erro ao logar');
      }
      );

  }
}
