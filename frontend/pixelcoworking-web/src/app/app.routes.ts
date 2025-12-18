import { Routes } from '@angular/router';
import { SpacesComponent } from './pages/spaces/spaces.component';

export const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'spaces'},
    { path: 'spaces', component: SpacesComponent }
];
