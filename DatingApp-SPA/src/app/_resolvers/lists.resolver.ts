import { Injectable } from '@angular/core';
import { Resolve, Router } from '@angular/router';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';
import { ActivatedRouteSnapshot} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';

@Injectable()
export class ListsResolver implements Resolve<PaginatedResult<User[]>> {
    pageNumber = 1;
    pageSize = 5;
    likesParam = 'Likers';

    constructor(private alertify: AlertifyService,
                private userService: UserService,
                private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<User[]>> {
        return this.userService.getUsers(this.pageNumber, this.pageSize, null, this.likesParam).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data.');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
