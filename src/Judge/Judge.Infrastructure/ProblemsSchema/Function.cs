using System.Collections.Generic;

namespace Judge.Infrastructure.ProblemsSchema
{
    public class Function
    {
        public string Name { get; set; }
        public Return Return { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}