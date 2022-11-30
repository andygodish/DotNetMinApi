using System.ComponentModel.DataAnnotations; // ctrl + .



namespace DotnetMinApi.Models
{
        public class Command
        {
            [Key] // These are validation attributes
            public int Id { get; set; }

            [Required]
            public string? HowTo { get; set; }

            [Required]
            [MaxLength(5)] // What's the difference between this and what comes standard in terms of validation in the MVC model 
            public string? Platform { get; set; }
            
            [Required]
            public string? CommandLine { get; set; } // prop + tab
        }
}