using System;
using System.Collections.Generic;
using System.Text;
using Judge.Infrastructure.Generators;
using Judge.Infrastructure.ProblemsSchema;
using Xbehave;
using Xunit;

namespace Judge.UnitTests
{
    public class CSharpSkeletonCodeGeneratorTests
    {
        [Scenario]
        public void BaseCodeGenerationScenario(Function func, CSharpSkeletonCodeGenerator generator,
            string skeletonCode)
        {
            "Given the function meta data"
                .x(() => func = CreateFunction());

            "And the generator"
                .x(() => generator = new CSharpSkeletonCodeGenerator());

            "When I launch generate"
                .x(() => skeletonCode = generator.Generate(func));

            "Then a c# code with an empty method that matches the function schema is generated"
                .x(() => Assert.Equal($"public int SumTwoIntegers(int a, int b){Environment.NewLine}{{{Environment.NewLine}\treturn a+b;{Environment.NewLine}}}", skeletonCode));
        }

        private Function CreateFunction()
        {
            return new Function("SumTwoIntegers", new Return(JavaTypes.Integer, "return"),
                new[] {new Parameter("a", JavaTypes.Integer, "a"), new Parameter("b", JavaTypes.Integer, "b")});
        }
    }
}
