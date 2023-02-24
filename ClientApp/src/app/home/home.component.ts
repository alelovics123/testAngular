import { Component,ViewChild,AfterViewInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, } from '@angular/forms'
import { IUserViewModel } from 'src/app/model/IUserViewModel'
import { MatTable } from '@angular/material/table'
import { IUserFilter } from 'src/app/model/IUserFilter'
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements AfterViewInit {
  Users: IUserViewModel[] = [];
  form: FormGroup | null;
  savedFilter: any;
  @ViewChild(MatTable) table: MatTable<any> = {} as MatTable<any>;
  @ViewChild('.delete') deleteButton: HTMLButtonElement = {} as HTMLButtonElement;
  //private _userService: UserService;
  constructor(private userService: UserService) {
    this.userService = userService;
    
    this.form = new FormGroup<{
      stringSearch: FormControl<string | null>,
      fromDateOfBirth: FormControl<string | null>,
      toDateOfBirth: FormControl<string | null>,
      recommenderId: FormControl<string | null>
    }>
      ({
        fromDateOfBirth: new FormControl(''), stringSearch: new FormControl('', { updateOn: 'blur' }),
        recommenderId: new FormControl(''), toDateOfBirth: new FormControl('')
      })
    this.savedFilter = this.form.value;
    this.form.valueChanges.subscribe((filter: any) =>
    {
      if (JSON.stringify(filter) != JSON.stringify(this.savedFilter))
      {
        this.savedFilter = filter; this.Refresh();
      }
    })
  }
  ngAfterViewInit() { this.Refresh(); this.deleteButton.disabled = true; }
  //OnSelectedRow(e,selectedRow) { this.deleteButton.disabled = false; }
  Refresh()
  {
    this.Users = this.userService.getUsers({
      fromDateOfBirth: null,
      id: null,
      nameOrPlaceFilter: null,
      recommenderId: null,
      toDateOfBirth: null
    });
    this.table.renderRows();
  }
}
