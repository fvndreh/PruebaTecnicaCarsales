import { Component, OnInit } from '@angular/core';
import { RickAndMortyService } from '../../../Services/RickAndMorty.service';
import { HttpClientModule } from '@angular/common/http';
import { TableComponent } from '../table/table.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-episodes',
  standalone: true,
  imports: [CommonModule, HttpClientModule, TableComponent],
  templateUrl: './episode-list.component.html',
  styleUrls: ['./episode-list.component.scss'],
})
export class EpisodeListComponent implements OnInit {
  data: any[] = [];
  columns: string[] = [];
  page = 0;
  pageSize = 20;
  totalCount = 0;
  totalPages = 0;

  constructor(private rickAndMortyService: RickAndMortyService) {}

  ngOnInit(): void {
    this.loadEpisodes();
  }

  loadEpisodes(): void {
    this.rickAndMortyService.getEpisodes<any>(this.page, this.pageSize).subscribe((response) => {
      this.totalCount = response.totalCount;
      this.totalPages = response.totalPages;
      this.data = response.data;
      if (this.data.length > 0) {
        this.columns = Object.keys(this.data[0]);
      }
    });
  }

  onNextPage(): void {
    if (this.page+1 < this.totalPages) {
      this.page++;
      this.loadEpisodes();
    }
  }

  onPreviousPage(): void {
    if (this.page > 0) {
      this.page--;
      this.loadEpisodes();
    }
  }
}
