import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './layout.html',
  styleUrl: './layout.css'
})
export class LayoutComponent {
  private readonly router = inject(Router);

  public currentRole: 'client' | 'mfg' = 'client';
  public showNotifPanel = false;
  public activeUrl = '';

  constructor() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      const url = event.urlAfterRedirects || event.url || '';
      this.activeUrl = url;
      if (url.includes('/client/')) {
        this.currentRole = 'client';
      } else if (url.includes('/manufacturer/')) {
        this.currentRole = 'mfg';
      }
    });

    const initialUrl = this.router.url;
    this.activeUrl = initialUrl;
    if (initialUrl.includes('/client/')) {
      this.currentRole = 'client';
    } else if (initialUrl.includes('/manufacturer/')) {
      this.currentRole = 'mfg';
    }
  }

  public get breadcrumb(): string {
    const url = this.activeUrl;
    if (url.includes('/client/dashboard')) return 'Dashboard';
    if (url.includes('/client/rfqs/create')) return 'RFQs / Create RFQ';
    if (url.includes('/client/rfqs/')) return 'RFQs / RFQ-2026-0142';
    if (url.includes('/client/rfqs')) return 'RFQs';
    if (url.includes('/client/marketplace')) return 'Marketplace';
    if (url.includes('/client/compare/')) return 'RFQs / Compare Quotes';
    if (url.includes('/client/awards')) return 'Awards';
    
    if (url.includes('/manufacturer/dashboard')) return 'Dashboard';
    if (url.includes('/manufacturer/marketplace')) return 'Marketplace RFQs';
    if (url.includes('/manufacturer/rfqs/')) {
      if (url.includes('/quote')) return 'Marketplace RFQs / Create Quote';
      return 'Marketplace RFQs / RFQ-2026-0142';
    }
    if (url.includes('/manufacturer/quotes')) return 'My Quotations';
    
    if (url.includes('/org')) return 'Organization';
    if (url.includes('/users')) return 'Users';
    if (url.includes('/profile')) return 'Profile';
    
    return 'Dashboard';
  }

  public toggleNotif(): void {
    this.showNotifPanel = !this.showNotifPanel;
  }

  public closeNotif(): void {
    this.showNotifPanel = false;
  }

  public switchRole(role: 'client' | 'mfg'): void {
    this.currentRole = role;
    this.closeNotif();
    if (role === 'client') {
      this.router.navigate(['/client/dashboard']);
    } else {
      this.router.navigate(['/manufacturer/dashboard']);
    }
  }

  public logout(): void {
    this.closeNotif();
    this.router.navigate(['/login']);
  }
}
