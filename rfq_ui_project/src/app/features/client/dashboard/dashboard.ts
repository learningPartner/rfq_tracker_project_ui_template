import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-client-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class ClientDashboardComponent implements OnInit {
  toastService = inject(ToastService);

  ngOnInit(): void {
    // Trigger Success notification for testing
    this.toastService.showToast({
      type: 'success',
      title: 'Welcome Back!',
      message: 'Logged in successfully as Rahul Sharma.'
    });

  }
}
