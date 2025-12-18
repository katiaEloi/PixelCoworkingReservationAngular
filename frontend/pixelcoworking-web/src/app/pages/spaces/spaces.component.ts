import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SpacesService, SpaceCreateDto, Space } from '../../services/spaces.service';

@Component({
  selector: 'app-spaces',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './spaces.component.html',
  styleUrl: './spaces.component.scss'
})
export class SpacesComponent  implements OnInit{
  spaces: Space[] = [];
  error: string | null = null;

  model: SpaceCreateDto = {
  name: '',
  type: 1,
  capacity: 0,
  hasPrivateBathroom: false
};

  constructor (private spacesService: SpacesService){}

  ngOnInit(): void {
    this.load();
  }
  
  load(): void{
    this.error = null;
    this.spacesService.getAll().subscribe({
      next: data => this.spaces = data,
      error: err => this.error = err?.message ?? 'Error cargando espacios'
    });
  }

  create(): void {
    if (!this.model.name.trim()) return;

    this.spacesService.create({ ...this.model, name: this.model.name.trim() }).subscribe({
      next: _ => { this.model.name = ''; this.load(); },
      error: err => this.error = err?.message ?? 'Error creando espacio'
    });
  }
}
