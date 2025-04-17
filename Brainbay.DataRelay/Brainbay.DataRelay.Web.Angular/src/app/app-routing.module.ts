import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CharacterOriginListComponent } from './character-origin-list/character-origin-list.component';
import { CharacterListComponent } from './character-list/character-list.component';
import { CharacterAddComponent } from './character-add/character-add.component';

const routes: Routes = [
  { path: '', redirectTo: 'all-characters', pathMatch: 'full' },
  { path: 'all-characters', component: CharacterListComponent },
  { path: 'by-origin', component: CharacterOriginListComponent },
  { path: 'by-origin/:origin', component: CharacterOriginListComponent },
  { path: 'add-character', component: CharacterAddComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
