import { Injectable } from '@angular/core';
import { Resolve, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';
import { ActivatedRouteSnapshot} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';
import { Message } from '../_models/message';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class MessagesResolver implements Resolve<PaginatedResult<Message[]>> {
    pageNumber = 1;
    pageSize = 5;
    messageContainer = 'Unread';

    constructor(private alertify: AlertifyService,
                private userService: UserService,
                private router: Router,
                private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<Message[]>> {
        return this.userService.getMessages(this.authService.decodedToken.nameid,
                this.pageNumber, this.pageSize, this.messageContainer).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving Messages');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
