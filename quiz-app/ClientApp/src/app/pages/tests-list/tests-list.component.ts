import { Component, OnInit, Inject } from '@angular/core';
import { TestsService } from "../../services/tests.service";

import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Router} from "@angular/router";

export interface TestOverview {
  id: string;
  name: string;
  description: string;
}

@Component({
  selector: 'app-tests-list',
  templateUrl: './tests-list.component.html',
  styleUrls: ['./tests-list.component.css']
})
export class TestsListComponent implements OnInit {
  testsList: any[] = [];
  displayedColumns: string[] = ['testName', 'actionButton'];

  constructor(
    private tests: TestsService,
    public dialog: MatDialog
  ) {}
  ngOnInit(): void {
    this.tests.getAvailableTests().subscribe(
      data => {
        this.testsList = data.tests;
      }
    )
  }

  openDialog(id: string, name: string, description: string): void {
    const dialogRef = this.dialog.open(TestOverviewComponent, {
      data: { id: id, name: name, description: description }
    });
  }
}

@Component({
  selector: 'app-test-overview',
  templateUrl: '/test-overview/test-overview.component.html',
  styleUrls: ['./test-overview/test-overview.component.css']
})
export class TestOverviewComponent {
  agreedToStart: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<TestOverviewComponent>,
    public router: Router,
    @Inject(MAT_DIALOG_DATA) public data: TestOverview
  ) {}

  onProceedClick(): void {
    this.router.navigate(['/quiz'], {
      queryParams: { id: this.data.id }
    });
  }
}
