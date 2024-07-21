import {
  ApplicationConfig,
  ErrorHandler,
  importProvidersFrom,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideHttpClient, withFetch } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IconsService } from '@progress/kendo-angular-icons';
import { NotificationService } from '@progress/kendo-angular-notification';
import { provideToastr } from 'ngx-toastr';
import { routes } from './app.routes';
import { CustomErrorHandlerService } from './services/error-handler/custom-error-handler.service';

export const appConfig: ApplicationConfig = {
  providers: [
    NotificationService,
    IconsService,
    provideRouter(routes),
    provideHttpClient(withFetch()),
    provideToastr({}),
    importProvidersFrom([BrowserAnimationsModule]),
    {
      provide: ErrorHandler,
      useClass: CustomErrorHandlerService,
    },
  ],
};
