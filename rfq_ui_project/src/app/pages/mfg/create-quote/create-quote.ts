import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-mfg-create-quote',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './create-quote.html',
  styleUrl: './create-quote.css'
})
export class MfgCreateQuoteComponent {}
