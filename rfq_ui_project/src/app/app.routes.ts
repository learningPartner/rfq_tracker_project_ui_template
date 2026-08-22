import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login';
import { RegisterComponent } from './pages/register/register';
import { ForgotComponent } from './pages/forgot/forgot';
import { LayoutComponent } from './shared/components/layout/layout';
import { ClientDashboardComponent } from './pages/client/dashboard/dashboard';
import { ClientRfqListComponent } from './pages/client/rfq-list/rfq-list';
import { ClientRfqCreateComponent } from './pages/client/rfq-create/rfq-create';
import { ClientRfqDetailsComponent } from './pages/client/rfq-details/rfq-details';
import { ClientCompareComponent } from './pages/client/compare/compare';
import { ClientAwardsComponent } from './pages/client/awards/awards';
import { ClientMarketplaceComponent } from './pages/client/marketplace/marketplace';
import { MfgDashboardComponent } from './pages/mfg/dashboard/dashboard';
import { MfgMarketplaceComponent } from './pages/mfg/marketplace/marketplace';
import { MfgRfqDetailsComponent } from './pages/mfg/rfq-details/rfq-details';
import { MfgCreateQuoteComponent } from './pages/mfg/create-quote/create-quote';
import { MfgMyQuotesComponent } from './pages/mfg/my-quotes/my-quotes';
import { OrgSettingsComponent } from './pages/settings/org/org';
import { UserSettingsComponent } from './pages/settings/users/users';
import { ProfileSettingsComponent } from './pages/settings/profile/profile';
import { PageNotFound } from './shared/components/page-not-found/page-not-found';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotComponent },
  {
    path: '',
    component: LayoutComponent,
    children: [
      // Client views
      { path: 'client/dashboard', component: ClientDashboardComponent },
      { path: 'client/rfqs', component: ClientRfqListComponent },
      { path: 'client/rfqs/create', component: ClientRfqCreateComponent },
      { path: 'client/rfqs/:id', component: ClientRfqDetailsComponent },
      { path: 'client/marketplace', component: ClientMarketplaceComponent },
      { path: 'client/compare/:id', component: ClientCompareComponent },
      { path: 'client/awards', component: ClientAwardsComponent },

      // Manufacturer views
      { path: 'manufacturer/dashboard', component: MfgDashboardComponent },
      { path: 'manufacturer/marketplace', component: MfgMarketplaceComponent },
      { path: 'manufacturer/rfqs/:id', component: MfgRfqDetailsComponent },
      { path: 'manufacturer/rfqs/:id/quote', component: MfgCreateQuoteComponent },
      { path: 'manufacturer/quotes', component: MfgMyQuotesComponent },

      // Shared settings views
      { path: 'org', component: OrgSettingsComponent },
      { path: 'users', component: UserSettingsComponent },
      { path: 'profile', component: ProfileSettingsComponent }
    ]
  },
  { path: '**', component: PageNotFound }
];
