import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
  { 
    path: '',
    redirectTo: 'home',
     pathMatch: 'full' 
  },
  {
    path: '',
    children: [
      {
        path: 'home',
        component: HomeComponent,
      },
    ],
  },
];
