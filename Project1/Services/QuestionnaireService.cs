using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Project1.Services;

public class QuestionnaireService
{
    private readonly IMongoCollection<Questionnaire> _questionnaireCollection;

    public QuestionnaireService(
        IOptions<QuestionsDatabaseSettings> questionnaireDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            questionnaireDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            questionnaireDatabaseSettings.Value.DatabaseName);

        _questionnaireCollection = mongoDatabase.GetCollection<Questionnaire>(
            questionnaireDatabaseSettings.Value.QuestionsCollectionName);
    }

    public async Task<List<Questionnaire>> GetAsync() =>
        await _questionnaireCollection.Find(_ => true).ToListAsync();

    // public async Task<Questionnaire?> GetAsync(string userId, string campaignId) =>
        // await _questionnaireCollection.Find(x => x.userId == userId && x.campaignId == campaignId).FirstOrDefaultAsync();
    public async Task<Questionnaire?> GetAsync(string userId) =>
        await _questionnaireCollection.Find(x => x.id == userId).FirstOrDefaultAsync();

    public async Task CreateAsync(Questionnaire newQuestion) =>
        await _questionnaireCollection.InsertOneAsync(newQuestion);

    public async Task UpdateAsync(string id, Questionnaire updatedBook) =>
        await _questionnaireCollection.ReplaceOneAsync(x => x.id == id, updatedBook);
    
    public async Task RemoveAsync(string id) =>
    await _questionnaireCollection.DeleteOneAsync(x => x.id == id);
}