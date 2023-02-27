using Microsoft.AspNetCore.Mvc;
using Project1.Services;

namespace Project1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string userId = "1";
        private static readonly string campaignId = "1";

        private static List<Question> freshQuestionsList = new List<Question>
        {
            new Question("1", "Question One", null, 0),
            new Question("2", "Question Two", null, 1),
            new Question("3", "Question Three", null, 2),
            new Question("4", "Question Four", null, 0),
            new Question("5", "Question Five", null, 1),
            new Question("6", "Question Six", null, 2),
            new Question("7", "Question Seven", null, 0),
            new Question("8", "Question Ocho", null, 1),
            new Question("9", "Question Nueve", null, 2),
            new Question("10", "Question Dies", null, 0),
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly QuestionnaireService _questionnaireService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            QuestionnaireService questionnaireService
        )
        {
            _logger = logger;
            _questionnaireService = questionnaireService;
        }

        [HttpGet]
        public async Task<Questionnaire> Get()
        {
            var questionnaire = await _questionnaireService.GetAsync(userId);

            // if there is state for this user and this campaign, return it. 
            // otherwise, create raw and return it

            if (questionnaire is not null)
            {
                return questionnaire;
            }

            await _questionnaireService.CreateAsync(new Questionnaire(userId, campaignId, freshQuestionsList));
            var questionnaire2 = await _questionnaireService.GetAsync(userId);
            return questionnaire2;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Question answeredQuestion)
        {

            // return NoContent();
            var questionnaire = await _questionnaireService.GetAsync(userId);

            // если стейта нет то швырнуть ошибку
            // если стейт есть то вставить ответ по номеру

            if (questionnaire is null)
            {
                return NotFound();
            }


            List<Question> updatedQuestionnaire = questionnaire.data.Select(question =>
                {
                    if (question.QuestionId == answeredQuestion.QuestionId)
                    {
                        question.AnswerValue = answeredQuestion.AnswerValue;
                        return question;
                    }

                    return question;
                }
            ).ToList();


            await _questionnaireService.UpdateAsync(userId,
                new Questionnaire(userId, campaignId, updatedQuestionnaire));

            return NoContent();
        }
        
        [HttpDelete()]
        public async Task<IActionResult> Delete()
        {
            
            await _questionnaireService.RemoveAsync(userId);

            return NoContent();
        }
    }
}