import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-client-compare',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './compare.html',
  styleUrl: './compare.css'
})
export class ClientCompareComponent {}
