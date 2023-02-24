namespace repos.Models;
public class UserViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PlaceOfBirth { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int? RecommenderId { get; set; }
}

