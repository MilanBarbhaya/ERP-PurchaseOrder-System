import {
  HttpErrorResponse,
  HttpInterceptorFn
} from '@angular/common/http';

import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn =
(req, next) => {

  return next(req).pipe(

    catchError((error: HttpErrorResponse) => {

      let message = 'Something went wrong';

      if (error.status === 0) {

        message =
          'Cannot connect to server';

      } else if (error.status === 400) {

        message =
          'Bad Request';

      } else if (error.status === 401) {

        message =
          'Unauthorized';

      } else if (error.status === 403) {

        message =
          'Access Denied';

      } else if (error.status === 404) {

        message =
          'Resource Not Found';

      } else if (error.status === 500) {

        message =
          'Internal Server Error';
      }

      alert(message);

      console.error(
        'HTTP ERROR',
        error
      );

      return throwError(
        () => error
      );
    })
  );
};