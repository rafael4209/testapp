namespace Project1;


public class Questionnaire
{
    public List<Question> data { get; set; }
    public string id { get; set; }
    // public string campaignId { get; set; }

    public Questionnaire(string userId, string campaignId, List<Question> data)
    {
        this.data = data;
        this.id = userId;
        // this.campaignId = campaignId;
    }
}