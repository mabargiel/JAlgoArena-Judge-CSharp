namespace Judge.Infrastructure.ProblemsSchema
{
    public class Return
    {
        public string Type { get; }
        public string Comment { get; }

        public Return (string type, string comment)
        {
            Type = type;
            Comment = comment;
        }
    }
}