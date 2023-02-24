using repos.EF;
using System.Runtime.CompilerServices;

namespace repos.Models
{
    public class UserFilter
    {
        public int? Id { get; set; }
        public string? NameOrPlaceFilter { get; set; }
        public DateTime? FromDateOfBirth { get; set; }
        public DateTime? ToDateOfBirth { get; set; }
        public int? RecommenderId { get; set; }

    }

}
