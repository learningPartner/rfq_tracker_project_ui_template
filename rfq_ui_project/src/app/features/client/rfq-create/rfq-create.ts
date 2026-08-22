import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-client-rfq-create',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './rfq-create.html',
  styleUrl: './rfq-create.css'
})
export class ClientRfqCreateComponent {
  public currentStep = 1;
}
