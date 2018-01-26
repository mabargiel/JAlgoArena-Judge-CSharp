namespace Judge.Infrastructure.ProblemsSchema
    {
        public class Function
        {
            public string Name { get; }
            public Return Return { get; }
            public Parameter[] Parameters { get; }

            public Function (string name, Return @return, Parameter[] parameters)
            {
                Name = name;
                Return = @return;
                Parameters = parameters;
            }
    }
}