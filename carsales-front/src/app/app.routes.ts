import { Routes } from '@angular/router';
import { EpisodeListComponent } from './Episodes/Components/episode-list/episode-list.component';

export const appRoutes: Routes = [
  { path: '', redirectTo: '/episodes', pathMatch: 'full' },
  { path: 'episodes', component: EpisodeListComponent },
  { path: '**', redirectTo: '/episodes' }
];
