import { Component, OnInit } from '@angular/core';
import { CharacterService } from '../services/character.service';
import { CharacterDetail } from '../models/character-detail.model';
import { PagedResult } from '../models/paged-result.model';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-character-list',
  standalone: false,
  templateUrl: './character-list.component.html',
  styleUrls: ['./character-list.component.scss']
})
export class CharacterListComponent implements OnInit {
  characters: CharacterDetail[] = [];
  totalCount: number = 0;
  page: number = 1;
  pageSize: number = 10;
  loading: boolean = false;
  errorMessage: string = '';

  constructor(private characterService: CharacterService) {}

  ngOnInit(): void {
    this.loadCharacters();
  }

  loadCharacters(): void {
    this.loading = true;
    this.characterService.getAllCharacters(this.page, this.pageSize)
      .subscribe({
        next: (result: PagedResult<CharacterDetail>) => {
          this.characters = result.items;
          this.totalCount = result.totalCount;
          this.loading = false;
        },
        error: () => {
          this.errorMessage = 'Error loading characters';
          this.loading = false;
        }
      });
  }

  onPageChange(event: PageEvent): void {
    this.page = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadCharacters();
  }
}
