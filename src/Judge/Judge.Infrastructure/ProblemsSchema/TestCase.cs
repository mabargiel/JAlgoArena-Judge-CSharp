namespace Judge.Infrastructure.ProblemsSchema
{
    public class TestCase
    {
        public TestCase(object input, object output)
        {
            Input = input;
            Output = output;
        }

        public object Input { get; }
        public object Output { get; }
    }
}