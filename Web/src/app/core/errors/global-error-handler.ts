import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { ErrorDialogService } from '../../shared/errors/error-dialog.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(
    private errorDialogService: ErrorDialogService,
    private zone: NgZone
  ) {}

  buildMessageFromError(error: any): string {

    var message: string = "";

    if(error?.error?.errors) {
      // maybe this massage is comming from Aspnet Core
      message += Object.values(error.error.errors).reduce<string>((accumulator, currentValue) => {
        var messageLines = (currentValue as string[]).reduce((acc,cur)=>{
          return acc + cur + "\n";
        }, "")
        var ret = (accumulator as string) + messageLines
        return ret;
      }, "")
    }

    if(error?.message) {
      message += error?.message + "\n";
    }

    if(!message) {
      message = 'Undefined client error';
    }
    return message;
  }

  handleError(error: any) {
    // Check if it's an error from an HTTP response
    if (!(error instanceof HttpErrorResponse)) {
      error = error.rejection; // get the error object
    }
    this.zone.run(() =>
      this.errorDialogService.openDialog(
        this.buildMessageFromError(error),
        error?.status
      )
    );

    console.error('Error from global error handler', error);
  }
}
