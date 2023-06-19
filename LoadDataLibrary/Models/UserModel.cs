using MongoDB.Bson;

namespace LoadDataLibrary.Models;

public class UserModel
{
    public ObjectId Id { get; set; }

    public string Name { get; set; } = ""; 
    
    public string Surname { get; set; } = ""; 

    public string Password { get; set; } = "";

    public string Email { get; set; } = "";

    public string Login { get; set; } = "";
}