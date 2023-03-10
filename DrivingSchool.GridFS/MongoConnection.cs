using MongoDB.Driver;

namespace DrivingSchool.GridFS;

public class MongoConnection
{
    private MongoClient? Client { get; }
    internal IMongoDatabase? Database { get; }

    public MongoConnection(string connectionString, string databaseName)
    {
        var s = MongoClientSettings.FromConnectionString(connectionString);
        s.ServerSelectionTimeout = TimeSpan.FromSeconds(5);
        Client = new MongoClient(s);
        Database = Client.GetDatabase(databaseName);
    }
}