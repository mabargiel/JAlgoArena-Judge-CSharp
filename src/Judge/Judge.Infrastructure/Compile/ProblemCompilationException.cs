using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Judge.Infrastructure.Compile
{
    public class ProblemCompilationException : Exception
    {
        public ProblemCompilationException(string message)
            :base(message)
        {
            
        }

        public ProblemCompilationException(string message, ImmutableArray<Diagnostic> diagnostics)
            :base(message + '\n' + string.Join(Environment.NewLine, diagnostics.Select(x => x.GetMessage())))
        {
             
        }
    }
}