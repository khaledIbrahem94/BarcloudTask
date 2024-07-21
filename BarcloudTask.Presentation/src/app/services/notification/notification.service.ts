import { Injectable } from '@angular/core';
import { NotificationService } from '@progress/kendo-angular-notification';

@Injectable({
  providedIn: 'root',
})
export class CustomNotificationService {
  constructor(public notificationService: NotificationService) {}

  showSuccess(message: string): void {
    this.showNotification(message, 'success');
  }

  showError(message: string): void {
    this.showNotification(message, 'error');
  }

  showInfo(message: string): void {
    this.showNotification(message, 'info');
  }

  showWarning(message: string): void {
    this.showNotification(message, 'warning');
  }

  private showNotification(
    message: string,
    type: 'success' | 'error' | 'info' | 'warning'
  ): void {
    this.notificationService.show({
      content: message,
      cssClass: `k-notification-${type} k-m-lg font24`,
      animation: { type: 'fade', duration: 400 },
      position: { horizontal: 'left', vertical: 'top' },
      type: { style: type, icon: true },
      hideAfter: 3000,
    });
  }
}
