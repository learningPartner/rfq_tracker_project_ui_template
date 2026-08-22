import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-mfg-rfq-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './rfq-details.html',
  styleUrl: './rfq-details.css'
})
export class MfgRfqDetailsComponent {}
