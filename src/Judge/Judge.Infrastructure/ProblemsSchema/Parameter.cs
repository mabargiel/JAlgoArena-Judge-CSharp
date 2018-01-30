namespace Judge.Infrastructure.ProblemsSchema
{
    public class Parameter
    {
        public Parameter(string name, string type, string comment)
        {
            Name = name;
            Type = type;
            Comment = comment;
        }

        public string Name { get; }
        public string Type { get; }
        public string Comment { get; }
    }
}