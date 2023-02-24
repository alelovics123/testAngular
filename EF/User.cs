using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace repos.EF
{
    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? RecommenderId { get; set; }
        [ForeignKey($"{nameof(User.RecommenderId)}")]
        public User? Reccommender { get; set; }
    }
}
