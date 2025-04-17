import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { CharacterDetail } from '../models/character-detail.model';

@Component({
  selector: 'app-character-table',
  standalone: false,
  templateUrl: './character-table.component.html',
  styleUrls: ['./character-table.component.scss']
})
export class CharacterTableComponent implements OnChanges {
  @Input() characters: CharacterDetail[] = [];
  @Input() totalCount: number = 0;
  @Input() page: number = 1;
  @Input() pageSize: number = 10;

  @Output() pageChange = new EventEmitter<PageEvent>();

  dataSource: MatTableDataSource<CharacterDetail> = new MatTableDataSource([]);
  displayedColumns: string[] = ['name', 'location', 'origin'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['characters']) {
      this.dataSource.data = this.characters;
    }
  }

  onPageChange(event: PageEvent): void {
    this.pageChange.emit(event);
  }
}
