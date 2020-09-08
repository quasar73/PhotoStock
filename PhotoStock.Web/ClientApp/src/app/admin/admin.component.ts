import { Component, OnInit } from '@angular/core';
import { User } from '../shared/models/user.model';
import { AdminService } from '../shared/admin/admin.service'

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  usersList: User[];
  displayedColumns: string[] = ['userName', 'email', 'numberOfImports'];

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.adminService.getUsers().subscribe((users) =>this.usersList = users);
  }


}
