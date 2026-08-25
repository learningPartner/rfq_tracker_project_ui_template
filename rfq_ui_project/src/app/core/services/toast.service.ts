import { Injectable, signal } from '@angular/core';

export interface Toast {
  id: number;
  type: 'success' | 'error' | 'delete' | 'info';
  title: string;
  message: string;
  icon?: string;
}

export interface ToastConfig {
  type: 'success' | 'error' | 'delete' | 'info';
  title: string;
  message: string;
  icon?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  public readonly toasts = signal<Toast[]>([]);
  private nextId = 1;

  /**
   * Shows a generic toast notification.
   * Usage: 
   * toastService.showToast({
   *   type: 'success',
   *   title: 'Successfully Succeeded',
   *   message: 'The operation completed successfully.',
   *   icon: 'assets/custom-success.svg' // optional
   * });
   */
  public showToast(config: ToastConfig): void {
    const id = this.nextId++;
    const newToast: Toast = {
      id,
      type: config.type,
      title: config.title,
      message: config.message,
      icon: config.icon
    };

    // Add to toasts list
    this.toasts.update(current => [...current, newToast]);

    // Automatically remove after 6 seconds
    setTimeout(() => {
      this.remove(id);
    }, 6000);
  }

  public remove(id: number): void {
    this.toasts.update(current => current.filter(t => t.id !== id));
  }
}
