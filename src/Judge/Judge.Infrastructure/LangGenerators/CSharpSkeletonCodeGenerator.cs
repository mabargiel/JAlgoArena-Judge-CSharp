using System;
using System.Linq;
using Judge.Infrastructure.ProblemsSchema;

namespace Judge.Infrastructure.LangGenerators
{
    public class CSharpSkeletonCodeGenerator : ISkeletonCodeGenerator
    {
        /// <summary>
        ///     Generates C# base code using <see cref="Problem" /> meta data
        /// </summary>
        public string Generate(Function function)
        {
            if (function == null)
                return null;

            return
                $@"public class Solution
{{
  {FunctionComment()}
  {FunctionSygnature()}
  {{
    //Write your code here.
  }}
}}";

            //HELPERS
            string ParamsComment()
            {
                return string.Join(Environment.NewLine + "  ", function.Parameters.Select(parameter =>
                    $@"///<param name=""{parameter.Name}"">{parameter.Comment}</param>"));
            }

            string FunctionComment()
            {
                return ParamsComment() + Environment.NewLine + $"  ///<returns>{function.Return.Comment}</returns>";
            }

            string Parameters()
            {
                var parametersStringTmpList = function.Parameters
                    .Select(parameter => $"{JtoCSharp.Translate(parameter.Type)} {parameter.Name}").ToList();

                return string.Join(", ", parametersStringTmpList);
            }

            string FunctionSygnature()
            {
                return $"public {JtoCSharp.Translate(function.Return.Type)} {function.Name}({Parameters()})";
            }
        }

        private static class JtoCSharp
        {
            private const string JavaLangBoolean = "java.lang.Boolean";
            private const string Boolean = "boolean";
            private const string JavaLangByte = "java.lang.Byte";
            private const string Byte = "byte";
            private const string JavaLangShort = "java.lang.Short";
            private const string Short = "short";
            private const string JavaLangInteger = "java.lang.Integer";
            private const string Integer = "int";
            private const string JavaLangLong = "java.lang.Long";
            private const string Long = "long";
            private const string JavaLangChar = "java.lang.Character";
            private const string Char = "char";
            private const string JavaLangDouble = "java.lang.Double";
            private const string Double = "double";
            private const string BigDecimal = "java.math.BigDecimal";

            public static string Translate(string javaType)
            {
                switch (javaType)
                {
                    case JavaLangBoolean:
                    case Boolean:
                        return "bool";
                    case JavaLangByte:
                    case Byte:
                        return "byte";
                    case JavaLangShort:
                    case Short:
                        return "short";
                    case JavaLangInteger:
                    case Integer:
                        return "int";
                    case JavaLangLong:
                    case Long:
                        return "long";
                    case JavaLangChar:
                    case Char:
                        return "char";
                    case JavaLangDouble:
                    case Double:
                        return "double";
                    case BigDecimal:
                        return "decimal";
                    default:
                        throw new NotSupportedException($"{javaType} is not supported.");
                }
            }
        }
    }
}