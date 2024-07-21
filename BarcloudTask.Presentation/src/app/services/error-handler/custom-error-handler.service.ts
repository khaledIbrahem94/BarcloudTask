import { ErrorHandler, Injectable } from '@angular/core';
import { CustomNotificationService } from '../notification/notification.service';
import { ErrorHandelingService } from './error-handeling.service';

@Injectable({
  providedIn: 'root',
})
export class CustomErrorHandlerService implements ErrorHandler {
  constructor(
    public notification: CustomNotificationService,
    public emitError: ErrorHandelingService
  ) {}

  handleError(error: any): void {
    let errorStr = undefined;

    if (typeof error.error === 'string') {
      errorStr = error.error;
    }
    const errorMsg =
      errorStr ?? 'An unexpected error occurred. Please try again later.';
    this.notification.showError(errorMsg);

    this.emitError.emitError.next(errorMsg);
  }
}
