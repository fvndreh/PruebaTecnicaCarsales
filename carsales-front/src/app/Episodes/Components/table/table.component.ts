import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class TableComponent {
  @Input() data: any[] = [];
  @Input() columns: string[] = [];
  @Input() page: number = 0;
  @Input() pageSize: number = 10;
  @Input() totalCount: number = 0;
  @Input() totalPages: number = 0;

  @Output() nextPage = new EventEmitter<void>();
  @Output() previousPage = new EventEmitter<void>();

  onNextPage() {
    this.nextPage.emit();
  }

  onPreviousPage() {
    this.previousPage.emit();
  }
}
