import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-client-awards',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './awards.html',
  styleUrl: './awards.css'
})
export class ClientAwardsComponent {}
