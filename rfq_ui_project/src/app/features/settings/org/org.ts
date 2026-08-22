import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-org-settings',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './org.html',
  styleUrl: './org.css'
})
export class OrgSettingsComponent {}
