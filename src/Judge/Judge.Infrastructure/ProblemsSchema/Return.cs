namespace Judge.Infrastructure.ProblemsSchema
{
    public class Return
    {
        public Return(string type, string comment)
        {
            Type = type;
            Comment = comment;
        }

        public string Type { get; }
        public string Comment { get; }
    }
}