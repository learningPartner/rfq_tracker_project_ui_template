import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-mfg-my-quotes',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './my-quotes.html',
  styleUrl: './my-quotes.css'
})
export class MfgMyQuotesComponent {}
