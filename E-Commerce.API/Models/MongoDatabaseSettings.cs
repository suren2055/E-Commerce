namespace E_Commerce.API.Models;

public class MongoDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;
    public string User { get; set; }
    public string Password { get; set; }


    public void Configure(string connectionString, string databaseName, string user, string password)
    {
        ConnectionString = connectionString;
        DatabaseName = databaseName;
        User = user;
        Password = password;
    }
}