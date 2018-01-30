using System;
using Judge.Infrastructure.ProblemsSchema;

namespace Judge.Infrastructure.Generators
{
    public class CSharpSkeletonCodeGenerator : ISkeletonCodeGenerator
    {
        public void GenerateFor(Problem problem)
        {
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

            throw new NotImplementedException();
        }
    }
}