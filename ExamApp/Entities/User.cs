using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamApp.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public string id { get; set; }
        public int UserType { get; set; }   

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
