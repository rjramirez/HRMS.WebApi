import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { AgGridAngular } from 'ag-grid-angular';

import { AppComponent } from './app.component';
import { EmployeesGridComponent } from './employees/employees-grid.component';
import { LoadingSpinnerComponent } from './shared/components/loading-spinner/loading-spinner.component';

@NgModule({
  declarations: [],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule,
    FormsModule,
    ToastrModule.forRoot(),
    AgGridAngular,
    AppComponent,
    EmployeesGridComponent,
    LoadingSpinnerComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }