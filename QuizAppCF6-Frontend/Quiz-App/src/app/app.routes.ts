import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LeaderboardsComponent } from './components/leaderboards/leaderboards.component';
import { authGuard } from './shared/guards/auth.guard';
import { redirectGuard } from './shared/guards/redirect.guard';

export const routes: Routes = [
    { path: '', component: WelcomeComponent },
    { path: 'login', component: UserLoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'home', component: HomeComponent, canActivate: [authGuard] },
    { path: 'leaderboards', component: LeaderboardsComponent }, // Νέο route για Leaderboards
    { path: '**', redirectTo: 'login' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}
