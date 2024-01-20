using MSArchivosEscalafonDocente.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MSArchivosEscalafonDocente.Services;

public class FilesService
{
    private readonly IMongoCollection<FilesModel> _filesCollection;

    public FilesService(
        IOptions<DataBaseSettings> fileStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            fileStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            fileStoreDatabaseSettings.Value.DatabaseName);

        _filesCollection = mongoDatabase.GetCollection<FilesModel>(
            fileStoreDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<FilesModel>> GetAsync() =>
        await _filesCollection.Find(_ => true).ToListAsync();

    public async Task<FilesModel?> GetAsync(string id) =>
        await _filesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(FilesModel newFile) =>
        await _filesCollection.InsertOneAsync(newFile);

    public async Task UpdateAsync(string id, FilesModel updatedFile) =>
        await _filesCollection.ReplaceOneAsync(x => x.Id == id, updatedFile);

    public async Task RemoveAsync(string id) =>
        await _filesCollection.DeleteOneAsync(x => x.Id == id);
}