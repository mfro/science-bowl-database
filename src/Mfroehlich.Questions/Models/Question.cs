using System;
using System.ComponentModel.DataAnnotations;

namespace Mfroehlich.Questions.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        public string Comment { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public bool TossUp { get; set; }
        
        [Required]
        public bool Flagged { get; set; }
        
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public string Answer { get; set; }

        public int Correct { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }

        public Group Group { get; set; }
    }
}