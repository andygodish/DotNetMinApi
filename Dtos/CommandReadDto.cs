namespace DotnetMinApi.Dtos
{
        public class CommandReadDto // Data we pass back during a read operation
        {
            public int Id { get; set; }
            public string? HowTo { get; set; }
            public string? Platform { get; set; }
            public string? CommandLine { get; set; }
        }
}