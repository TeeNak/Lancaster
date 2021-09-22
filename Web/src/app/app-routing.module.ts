import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ExplanationViewComponent } from './file-explorer/components/explanation-view/explanation-view.component'
import { ExplorerViewComponent } from './file-explorer/components/explorer-view/explorer-view.component'

const routes: Routes = [
  { path: '', redirectTo: '/explorer', pathMatch: 'full' },
  { path: 'explanation', component: ExplanationViewComponent },
  { path: 'explorer', component: ExplorerViewComponent },
  { path: '**', redirectTo: '/explorer' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
