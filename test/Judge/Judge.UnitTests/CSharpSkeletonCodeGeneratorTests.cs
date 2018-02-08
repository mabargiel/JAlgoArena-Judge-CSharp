using System;
using System.Collections.Generic;
using System.IO;
using Judge.Infrastructure.LangGenerators;
using Judge.Infrastructure.ProblemsSchema;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xbehave;
using Xunit;

namespace Judge.UnitTests
{
    public class CSharpSkeletonCodeGeneratorTests
    {
        /// <summary>
        /// </summary>
        /// <param name="func"></param>
        /// <param name="generator"></param>
        /// <param name="skeletonCode"></param>
        [Scenario]
        public void BaseCodeGenerationScenario(Function func, CSharpSkeletonCodeGenerator generator,
            (string lang, string code) skeletonCode)
        {
            "Given the function meta data"
                .x(() => func = CreateFunction());

            "And the generator"
                .x(() => generator = new CSharpSkeletonCodeGenerator());

            "When I launch generate"
                .x(() =>
                {
                    var (lang, code) = generator.Generate(func);
                    skeletonCode = (lang, code);
                });

            "Then a c# code with an empty method that matches the function schema is generated"
                .x(() => Assert.Equal(File.ReadAllText("./Resources/fib-problem-skeleton.cs"), skeletonCode.code));
        }

        private Function CreateFunction()
        {
            var json = File.ReadAllText("./Resources/fib-problem.json");
            var problem = JsonConvert.DeserializeObject<Problem>(json,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});

            return problem.Function;
        }

        [Scenario]
        public void FunctionContainsNotSupportedJavaTypesScenario(Function func, CSharpSkeletonCodeGenerator generator)
        {
            "Given the function meta data"
                .x(() => func = CreateFunctionWithNotSupportedType());

            "And the generator"
                .x(() => generator = new CSharpSkeletonCodeGenerator());

            "When I launch generate Then NotSupportedException with message is thrown"
                .x(() =>
                {
                    var ex = Assert.Throws<NotSupportedException>(() => generator.Generate(func));
                    Assert.Equal("java.util.LinkedList is not supported.", ex.Message);
                });
        }

        private Function CreateFunctionWithNotSupportedType()
        {
            return new Function
            {
                Name = "CountAList",
                Return = new Return {Type = "int", Comment = "return"},
                Parameters =
                    new List<Parameter> {new Parameter {Type = "java.util.LinkedList", Name = "a", Comment = "a"}}
            };
        }
    }
}