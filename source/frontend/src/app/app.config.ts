import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { providePrimeNG } from 'primeng/config';
import { definePreset } from '@primeng/themes';
import Aura from '@primeng/themes/aura';
import { ArcElement, Colors, DoughnutController, Legend, registerables } from 'chart.js';
import { provideCharts } from 'ng2-charts';

const WakeUpPreset = definePreset(Aura, {
    semantic: {
        primary: {
            50: '{gray.50}',
            100: '{gray.100}',
            200: '{gray.200}',
            300: '{gray.300}',
            400: '{gray.400}',
            500: '{gray.500}',
            600: '{gray.600}',
            700: '{gray.700}',
            800: '{gray.800}',
            900: '{gray.900}',
            950: '{gray.950}'
        }
    }
});

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(),
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: WakeUpPreset
      }
    }),
    provideCharts({ registerables: [DoughnutController, Legend, Colors, ArcElement]})]
};

