using System;
using System.Collections.Generic;
using System.Linq;
using Judge.Infrastructure.ProblemsSchema;

namespace Judge.Infrastructure.Generators
{
    /// <summary>
    ///     Generates C# base code using <see cref="Problem" /> meta data
    /// </summary>
    public class CSharpSkeletonCodeGenerator : ISkeletonCodeGenerator
    {
        public string Generate(Function function)
        {
            string TranslateType(string javaType)
            {
                switch (javaType)
                {
                    case JavaTypes.Integer:
                        return typeof(int).FullName;
                    default:
                        throw new NotSupportedException($"{javaType} is not supported");
                }
            }

            string GenerateParametersString(Parameter[] parameters)
            {
                var parametersStringTmpList = new List<string>();

                foreach (var parameter in parameters)
                {
                    var comment = !string.IsNullOrEmpty(parameter.Comment) ? $"/*{parameter.Comment}*/" : string.Empty;
                    var s = $"{comment}{TranslateType(parameter.Type)} {parameter.Name}";
                    parametersStringTmpList.Add(s);
                }

                return string.Join(", ", parametersStringTmpList);
            }

            /*
                TODO Implement SkeletonCode generator for C#
                --------------------------------------------
                Method sygnature should be
                public <return type> Run (<parameter type> <parameter name> ...)

                Test cases:
                1. single input parameter for all primitive / value types
                2. single input parameter of a string type
                3. multi input parameters for all primitive / value types
                4. multi input parameters of a string type
                5. objects and reference types ARE NOT supported
                6. return / output types (prymitive types and string)

                NOTE: Metadata types are java type names. Map these to C# type names
                NOTE2: Remove this comment before pull request creation
            */

            var returnType = TranslateType(function.Return.Type);
            var parametersString = GenerateParametersString(function.Parameters);


        }
    }

    public static class JavaTypes
    {
        public const string Integer = "int";
    }
}