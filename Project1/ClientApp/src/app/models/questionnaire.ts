

export type IAnswerValue = any

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

export interface IQuestionnaireRequest {
  questionnaireCampaignId: string
  userId: string
}

export interface ISetAnswerRequest {
  questionnaireCampaignId: string
  userId: string
  questionId: string
  answerValue: IAnswerValue
}
