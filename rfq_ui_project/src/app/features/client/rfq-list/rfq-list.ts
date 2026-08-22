import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-client-rfq-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './rfq-list.html',
  styleUrl: './rfq-list.css'
})
export class ClientRfqListComponent {}
