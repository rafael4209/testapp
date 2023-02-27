namespace Project1;

public class Question
{
    public String QuestionId { get; set; }
    public String QuestionText { get; set; }
    public int AnswerType { get; set; }
    public String? AnswerValue { get; set; }

    public Question(string questionId, string questionText, String answerValue, int answerType)
    {
        QuestionId = questionId;
        QuestionText = questionText;
        AnswerValue = answerValue;
        AnswerType = answerType;
    }
}