import { Component, OnInit, Input, AfterViewChecked} from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { Message } from 'src/app/_models/message';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit, AfterViewChecked {
  @Input() recipientId: number;
  container: HTMLElement;
  messages: Message[];
  newMessage: any = {};

  constructor(private alertify: AlertifyService,
              private authService: AuthService, private userService: UserService) { }

  ngOnInit() {
    this.loadMessages();
  }

  ngAfterViewChecked() {
    this.container = document.getElementById('cardBody');
    this.container.scrollTop = this.container.scrollHeight;
  }

  loadMessages() {
    const userId = +this.authService.decodedToken.nameid;
    this.userService.getMessageThread(this.authService.decodedToken.nameid, this.recipientId)
      .pipe(
        tap(messages => {
          // tslint:disable-next-line: prefer-for-of
          for (let i = 0; i < messages.length; i++) {
            if (messages[i].isread === false && messages[i].recipientId === userId) {
              this.userService.markAsRead(userId, messages[i].id);
            }
          }
        })
      )
      .subscribe(data => {
        this.messages = data;
      }, error => {
        this.alertify.error(error);
      });
  }

  sendMessage() {
    this.newMessage.recipientId = this.recipientId;
    this.userService.sendMessage(this.authService.decodedToken.nameid, this.newMessage)
      .subscribe((message: Message) => {
        this.messages.push(message);
        this.newMessage.content = '';
      }, error => {
        this.alertify.error(error);
      });
  }

}
