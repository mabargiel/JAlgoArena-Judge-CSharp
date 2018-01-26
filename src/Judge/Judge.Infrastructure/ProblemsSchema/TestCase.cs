using System;

namespace Judge.Infrastructure.ProblemsSchema
{
    public class TestCase
    {
        public object Input { get; }
        public object Output { get; }

        public TestCase (object input, object output)
        {
            Input = input;
            Output = output;
        }
    }
}