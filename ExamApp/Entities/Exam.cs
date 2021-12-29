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
        [ForeignKey("Content")]
        public string ContentId { get; set; }
        public string Title { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
        
        public virtual Content Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
