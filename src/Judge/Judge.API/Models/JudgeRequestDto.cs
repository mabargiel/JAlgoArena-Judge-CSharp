namespace Judge.API.Models
{
    public class JudgeRequestDto
    {
        public string Language { get; set; }
        public string SourceCode { get; set; }
        public string UserId { get; set; }
    }
}