using System.ComponentModel.DataAnnotations;

namespace DotnetMinApi.Dtos
{
        public class CommandUpdateDto // PUT is supported, but not PATCH (for Min APIs)
        {
            [Required]
            public string? HowTo { get; set; }
            [Required]
            [MaxLength(5)]
            public string? Platform { get; set; }
            [Required]
            public string? CommandLine { get; set; }
        }
}