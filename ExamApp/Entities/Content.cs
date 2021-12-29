using System;
using System.ComponentModel.DataAnnotations;

namespace ExamApp.Entities
{
    public class Content
    {
        [Key]
        public string id { get; set; }

        public string Title { get; set; }
        public string Link { get; set; }
        public string Descriiption { get; set; }
        public string ContentText { get; set; }
        public virtual Exam Exam { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
