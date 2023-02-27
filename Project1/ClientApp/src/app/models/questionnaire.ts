
export interface IQuestion {
  questionId: string
  questionText: string
  answerValue: string
  answerType: number
}

export interface IQuestionnaireResponse {
  data: IQuestion[]
  id: string
}
