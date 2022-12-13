import {Component, ElementRef, Inject, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {TestsService} from "../../services/tests.service";
import {TestCheckModel} from "../../models/test-check.model";
import {MatList, MatSelectionList} from "@angular/material/list";
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {TestOverview, TestOverviewComponent} from "../tests-list/tests-list.component";

export interface TestResults {
  name: string;
  score: string;
}

@Component({
  selector: 'app-test-form',
  templateUrl: './test-form.component.html',
  styleUrls: ['./test-form.component.css']
})
export class TestFormComponent implements OnInit {
  currentQuestion: number = 1
  testId: string | null = null;
  testName: string = "";
  testDescription: string = "";
  questions: any[] = [];

  @ViewChild('answersList') answersList!: MatSelectionList;

  answers: TestCheckModel = new TestCheckModel();

  constructor(
    private tests: TestsService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams
      .subscribe(params => {
          this.testId = params.id;
        }
      );

    if (this.testId == null) {
      this.router.navigate(["/quizzes"]);
    }
    else {
      this.tests.getTest(this.testId!).subscribe(
        data => {
          this.testName = data.test.name;
          this.testDescription = data.test.description;
          this.questions = data.test.questions;
        }
      )
    }
  }

  moveToNextQuestion(): void {
    if (this.currentQuestion < this.questions.length && this.answersList.selectedOptions.hasValue()) {
      this.answers.answers.push({
        text: this.questions[this.currentQuestion - 1].text,
        answer: this.getCurrentAnswer()
      })

      this.currentQuestion++;
    }
    else
    {
      this.answers.testId = this.testId!;
      this.answers.answers.push({
        text: this.questions[this.currentQuestion - 1].text,
        answer: this.getCurrentAnswer()
      })

      this.finishTest();
    }
  }

  finishTest(): void {
    this.tests.checkTest(this.answers).subscribe(
      data => {
        this.openTestResults(data.score);
      }
    )
  }

  openTestResults(score: number): void {
    const dialogRef = this.dialog.open(TestResultComponent, {
      data: { name: this.testName, score: score }
    });
  }

  getCurrentQuestionText(): string {
    return this.questions[this.currentQuestion - 1].text;
  }

  getFirstAnswer(): string {
    return this.questions[this.currentQuestion - 1].firstAnswer;
  }

  getSecondAnswer(): string {
    return this.questions[this.currentQuestion - 1].secondAnswer;
  }

  getThirdAnswer(): string {
    return this.questions[this.currentQuestion - 1].thirdAnswer;
  }
  getFourthAnswer(): string {
    return this.questions[this.currentQuestion - 1].fourthAnswer;
  }

  getCurrentAnswer(): number {
    let selectedOptionValue = this.answersList.selectedOptions.selected[0].value;

    let firstAnswer = this.questions[this.currentQuestion - 1].firstAnswer;
    let secondAnswer = this.questions[this.currentQuestion - 1].secondAnswer;
    let thirdAnswer = this.questions[this.currentQuestion - 1].thirdAnswer;
    let fourthAnswer = this.questions[this.currentQuestion - 1].fourthAnswer;

    switch (selectedOptionValue) {
      case firstAnswer: {
        return 1;
      }
      case secondAnswer: {
        return 2;
      }
      case thirdAnswer: {
        return 3;
      }
      case fourthAnswer: {{
        return 4;
      }}
      default: {
        return -1;
      }
    }
  }
}

@Component({
  selector: 'app-test-result',
  templateUrl: '/test-result/test-result.component.html',
  styleUrls: ['./test-result/test-result.component.css']
})
export class TestResultComponent {
  agreedToStart: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<TestResultComponent>,
    public router: Router,
    @Inject(MAT_DIALOG_DATA) public data: TestResults
  ) {}

  onBackClick(): void {
    this.router.navigate(['/quizzes']);
  }
}
