using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamApp.Entities
{
    [Table("Exam")]
    public class Exam
    {
        [Key]
        public string id { get; set; }
        public string ContentId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
