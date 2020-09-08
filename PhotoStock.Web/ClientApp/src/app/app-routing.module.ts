import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PhotoComponent } from './photo/photo.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ImportComponent } from './import/import.component';
import { UserGuard } from './shared/guards/user.guard';
import { AdminGuard } from './shared/guards/admin.guard';
import { AdminComponent } from './admin/admin.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'photo', component: PhotoComponent},
  {path: 'account/login', component: LoginComponent},
  {path: 'account/registration', component: RegistrationComponent},
  {path: 'import', component: ImportComponent, canActivate: [ UserGuard ]},
  {path: 'admin', component: AdminComponent, canActivate: [ AdminGuard ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
