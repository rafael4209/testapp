import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {IQuestion, IQuestionnaireResponse} from "../models/questionnaire";
import {BehaviorSubject, map, switchMap, tap} from "rxjs";

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {

  public selectOptions: string[] =  ['Option 1', 'Option 2', 'Option 3' ];
  public data$: BehaviorSubject<IQuestion[]> = new BehaviorSubject<IQuestion[]>([])

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    http.get<IQuestionnaireResponse>(baseUrl + 'weatherforecast').subscribe(result => {
      this.data$.next(result.data)
    }, error => console.error(error));
  }

  public onAnswer(question: IQuestion, answerValue: string): void {
    const newQuestion: IQuestion = {...question, answerValue};
    this.http.post(this.baseUrl + 'weatherforecast',  newQuestion).pipe(
      switchMap(() => this.data$),
      map((qarr: IQuestion[]): IQuestion[] => {
        return qarr.map(e => e.questionId === newQuestion.questionId ? {...e, answerValue} : e)
      }),
      tap((moddedArr: IQuestion[]) =>  {
        this.data$.next(moddedArr)
      })
    ).subscribe()
  }

  onClear() {
    this.http.delete(this.baseUrl + 'weatherforecast' ).pipe(
      switchMap(() => this.http.get<IQuestionnaireResponse>(this.baseUrl + 'weatherforecast')),
      tap((response: IQuestionnaireResponse) => {
        this.data$.next(response.data)
        alert('db cleared')
      })
    ).subscribe()
  }

}

