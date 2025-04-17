import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { CharacterService } from '../services/character.service';
import { CharacterDetail } from '../models/character-detail.model';
import { PagedResult } from '../models/paged-result.model';
import { PageEvent } from '@angular/material/paginator';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'app-character-origin-list',
  standalone: false,
  templateUrl: './character-origin-list.component.html',
  styleUrls: ['./character-origin-list.component.scss']
})
export class CharacterOriginListComponent implements OnInit {
  origin: string = '';
  characters: CharacterDetail[] = [];
  totalCount: number = 0;
  page: number = 1;
  pageSize: number = 10;
  loading: boolean = false;
  errorMessage: string = '';

  constructor(private characterService: CharacterService
    , private route: ActivatedRoute
    , private router: Router
  ) { }

  ngOnInit(): void {
    combineLatest([this.route.paramMap, this.route.queryParamMap])
      .subscribe(([params, queryParams]: [ParamMap, ParamMap]) => {
        const originFromUrl = params.get('origin');
        this.origin = originFromUrl ? originFromUrl.trim() : '';

        const pageParam = queryParams.get('page');
        const pageSizeParam = queryParams.get('pageSize');

        this.page = pageParam ? + pageParam : 1;
        this.pageSize = pageSizeParam ? + pageSizeParam : 10;

        this.loadCharacters();
     });
  }

  searchCharacters(): void {
    this.errorMessage = '';
    this.page = 1;
    this.loadCharacters();
  }

  loadCharacters(): void {
    this.loading = true;
    this.characterService.getCharactersByOrigin(this.origin.trim(), this.page, this.pageSize)
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

  onSearch(): void {
    const trimmedOrigin = this.origin.trim();
    if (trimmedOrigin) {
      this.router.navigate(['/by-origin', trimmedOrigin], {
        queryParams: {
          page: this.page,
          pageSize: this.pageSize
        }
      });
    } else {
      this.router.navigate(['/by-origin'], {
        queryParams: {
          page: this.page,
          pageSize: this.pageSize
        }
      });
    }

    this.loadCharacters();
  }

  onPageChange(event: PageEvent): void {
    this.page = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    const trimmedOrigin = this.origin.trim();
    this.router.navigate(['/by-origin', trimmedOrigin], {
      queryParams: {
        page: this.page,
        pageSize: this.pageSize
      }
    });
  }
}
