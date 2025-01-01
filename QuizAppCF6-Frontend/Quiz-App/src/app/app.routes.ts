import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LeaderboardsComponent } from './components/leaderboards/leaderboards.component';
import { authGuard } from './shared/guards/auth.guard';
import { redirectGuard } from './shared/guards/redirect.guard';
import { CanDeactivateGuard } from './shared/guards/can-deactivate.guard';
import { QuizComponent } from './components/quiz/quiz.component';
import { ResultsComponent } from './components/results/results.component';
import { AdminUsersComponent } from './components/admin-users/admin-users.component';
import { EditUserComponent } from './components/admin/edit-user/edit-user.component';

export const routes: Routes = [
    { path: '', component: WelcomeComponent },
    { path: 'welcome', component: WelcomeComponent },
    { path: 'login', component: UserLoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'home', component: HomeComponent, canActivate: [authGuard] },
    { path: 'leaderboards', component: LeaderboardsComponent },
    { path: 'quiz/:id/start', component: QuizComponent, canActivate: [authGuard], canDeactivate: [CanDeactivateGuard] },
    { path: 'results', component: ResultsComponent, canActivate: [authGuard] },
    { path: 'admin/users', component: AdminUsersComponent, canActivate: [authGuard] },
    { path: 'admin/users/edit/:id', component: EditUserComponent, canActivate: [authGuard]},
    { path: '**', redirectTo: 'login' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
