import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-forgot',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './forgot.html',
  styleUrl: './forgot.css'
})
export class ForgotComponent {}
