import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  try {
    const token = localStorage.getItem('auth_token');
    const isApi = req.url.startsWith('http://localhost:5051');
    if (token && isApi) {
      req = req.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
    }
  } catch {}
  return next(req).pipe(
    catchError((err) => {
      if (err?.status === 401) {
        try {
          localStorage.removeItem('auth_token');
          window.dispatchEvent(new CustomEvent('auth:logout'));
        } catch {}
      }
      return throwError(() => err);
    })
  );
};
