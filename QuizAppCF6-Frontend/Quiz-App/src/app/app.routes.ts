import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { HomeComponent } from './components/home/home.component';

export const routes: Routes = [
    { path: 'login', component: UserLoginComponent },
    // { path: 'register', component: RegisterComponent },
  { path: 'home', component: HomeComponent },
//   { path: 'admin', component: AdminComponent },
//   { path: 'quiz/:id', component: QuizComponent },
    { path: '**', redirectTo: 'login' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
