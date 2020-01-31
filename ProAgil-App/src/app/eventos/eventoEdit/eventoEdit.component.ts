import { Component, OnInit } from '@angular/core';
import { EventoService } from 'src/app/_services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap';
import { FormBuilder, Form, FormGroup, Validators, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/_models/Evento';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-evento-edit',
  templateUrl: './eventoEdit.component.html',
  styleUrls: ['./eventoEdit.component.css']
})
export class EventoEditComponent implements OnInit {

  titulo = 'Edição Detalhada';
  evento: Evento = new Evento();
  registerForm: FormGroup;
  file: File;
  imagemURL = 'assets/img/upload.png';
  bodyRemoverLote = '';
  bodyRemoverRedeSocial = '';
  fileNameToUpdate: string;
  dataAtual = '';
  dataEvento =  '';

  get lotes(): FormArray {
    return this.registerForm.get('lotes') as FormArray;
  }

  get redesSociais(): FormArray {
    return this.registerForm.get('redesSociais') as FormArray;
  }

  constructor(
    private eventoService: EventoService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService,
    private Router: ActivatedRoute
    ) {}

  ngOnInit() {
    this.validation();
    this.carregarEvento();
  }

  carregarEvento() {
    const idEvento = +this.Router.snapshot.paramMap.get('id');
    this.eventoService.getEventoById(idEvento)
    .subscribe((evento: Evento) => {
        this.evento = Object.assign({}, evento);
        this.fileNameToUpdate = evento.imagemURL.toString();
        this.evento.imagemURL = `http://localhost:5000/Resources/Imagens/${this.evento.imagemURL}}?_ts=${this.dataAtual}}`;
        this.registerForm.patchValue(this.evento);

        this.evento.lotes.forEach(lote => {
          this.lotes.push(this.criaLote(lote));
        });
        this.evento.redesSociais.forEach(redeSocial => {
          this.redesSociais.push(this.criaRedeSocial(redeSocial));
        });


    });
  }

  validation() {
    this.registerForm = this.fb.group({
      id: [],
      tema: [ '', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: [ '', Validators.required],
      dataEvento: [ '', Validators.required],
      qtdPessoas: [ '', [Validators.required, Validators.max(1500)]],
      imagemURL: [ ''],
      telefone: [ '', Validators.required],
      email: [ '', [Validators.required, Validators.email]],
      lotes: this.fb.array ([]),
      redesSociais: this.fb.array ([])
  });
}


criaLote(lote: any): FormGroup {
  return this.fb.group({
    id: [lote.id],
    nome: [lote.nome, Validators.required],
    quantidade: [lote.quantidade, Validators.required],
    preco: [lote.preco, Validators.required],
    dataInicio: [lote.dataInicio, Validators.required],
    dataFim: [lote.dataFim, Validators.required]
  });
}

 criaRedeSocial(redeSocial: any): FormGroup {
      return this.fb.group ({
      id: [redeSocial.id],
      nome: [redeSocial.nome, Validators.required],
      url: [redeSocial.url, Validators.required]
    });
  }

      adicionarLotes() {
      this.lotes.push(this.criaLote({id: 0}));
      }

      removeLote(id: number) {
        this.lotes.removeAt(id);
      }

      adicionarRedeSocial() {
        this.redesSociais.push(this.criaRedeSocial({id: 0}));
        }

      removeRedeSocial(id: number) {
        this.redesSociais.removeAt(id);
      }

  onFileChange(file: FileList) {
    const reader = new FileReader();
    reader.onload = (event: any ) => {
    this.imagemURL = event.target.result;
    this.file = event.target.files;
    };
    reader.readAsDataURL(file[0]);
  }


    salvarEvento() {
          this.evento.imagemURL = this.fileNameToUpdate;
          this.eventoService.putEvento(this.evento).subscribe(
          () => {
            this.toastr.success('Editado com Sucesso');
          }, error => {
            this.toastr.error('Erro ao Editar: ${error}, reporte ao administrador');
          }
        );
        }

    uploadImagem() {
      if (this.registerForm.get('imagemURL').value !== '') {
      this.eventoService.postUpload(this.file, this.fileNameToUpdate).
      subscribe(
        () => {
          this.dataAtual = new Date().getMilliseconds().toString();
          this.imagemURL = `http://localhost:5000/Resources/Imagens/${this.evento.imagemURL}}?_ts=${this.dataAtual}}`;
        }
      );
    }
  }
}
