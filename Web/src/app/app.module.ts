import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { ExplorerViewComponent } from './file-explorer/components/explorer-view/explorer-view.component';
import { ExplanationViewComponent } from './file-explorer/components/explanation-view/explanation-view.component';
import { AddItemDialogComponent } from './file-explorer/components/add-item-dialog/add-item-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    ExplorerViewComponent,
    ExplanationViewComponent,
    AddItemDialogComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    CoreModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
