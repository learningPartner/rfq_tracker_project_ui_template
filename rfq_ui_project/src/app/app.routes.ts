import { Routes } from '@angular/router';
import { LoginComponent } from './features/login/login';
import { RegisterComponent } from './features/register/register';
import { ForgotComponent } from './features/forgot/forgot';
import { LayoutComponent } from './shared/components/layout/layout';
import { ClientDashboardComponent } from './features/client/dashboard/dashboard';
import { ClientRfqListComponent } from './features/client/rfq-list/rfq-list';
import { ClientRfqCreateComponent } from './features/client/rfq-create/rfq-create';
import { ClientRfqDetailsComponent } from './features/client/rfq-details/rfq-details';
import { ClientCompareComponent } from './features/client/compare/compare';
import { ClientAwardsComponent } from './features/client/awards/awards';
import { ClientMarketplaceComponent } from './features/client/marketplace/marketplace';
import { MfgDashboardComponent } from './features/mfg/dashboard/dashboard';
import { MfgMarketplaceComponent } from './features/mfg/marketplace/marketplace';
import { MfgRfqDetailsComponent } from './features/mfg/rfq-details/rfq-details';
import { MfgCreateQuoteComponent } from './features/mfg/create-quote/create-quote';
import { MfgMyQuotesComponent } from './features/mfg/my-quotes/my-quotes';
import { OrgSettingsComponent } from './features/settings/org/org';
import { UserSettingsComponent } from './features/settings/users/users';
import { ProfileSettingsComponent } from './features/settings/profile/profile';
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
