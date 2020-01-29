import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  user: User;

  constructor(public fb: FormBuilder,
              private toastr: ToastrService,
              private authService: AuthService,
              private router: Router) {}

  ngOnInit( ) {
    this.validation();
  }

  validation() {
      this.registerForm = this.fb.group({
      nomeCompleto : ['', Validators.required],
      email : ['', [Validators.required,  Validators.email]],
      userName : ['', Validators.required],
      passwords : this.fb.group ({
      passwordHash : ['', [Validators.required,  Validators.minLength(4)]],
      confirmPassword : ['', Validators.required]},
      { validator: this.compararSenhas
      })
    });
  }

  compararSenhas(fb: FormGroup) {
    const confirmSenhaCtrl = fb.get('confirmPassword');
    if (confirmSenhaCtrl.errors == null || 'mismatch' in confirmSenhaCtrl.errors) {
      if (fb.get('passwordHash').value !== confirmSenhaCtrl.value) {
        confirmSenhaCtrl.setErrors({mismatch: true});
      } else {
        confirmSenhaCtrl.setErrors(null);
      }

    }
  }

  cadastrarUsuario() {
    if (this.registerForm.valid) {
        this.user = Object.assign(
        {passwordHash: this.registerForm.get
        ('passwords.passwordHash').value},
        this.registerForm.value);
        console.log(this.user);
        this.authService.register(this.user).subscribe(
           () => {
              this.router.navigate(['/user/login']);
              this.toastr.success('Cadastrado Realizado!');
            }, error => {
              const erro = error.error;
              console.log(erro);
              erro.forEach(element  => {
                switch (element.code) {
                  case 'DuplicateUserName':
                    this.toastr.error('Você ja possui um cadastro com este usuario, tente redefinir a senha');
                    break;
                  default:
                    this.toastr.error(`Erro no cadastro! CODE: ${element.code}`);
                    break;
              }
            });
          }
        );
    }
  }
}


