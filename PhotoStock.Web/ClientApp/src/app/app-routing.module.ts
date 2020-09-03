import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PhotoComponent } from './photo/photo.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ImportComponent } from './import/import.component';

const routes: Routes = [
  {path: 'photo', component: PhotoComponent},
  {path: '', component: HomeComponent},
  {path: 'account/login', component: LoginComponent},
  {path: 'account/registration', component: RegistrationComponent},
  {path: 'import', component: ImportComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
