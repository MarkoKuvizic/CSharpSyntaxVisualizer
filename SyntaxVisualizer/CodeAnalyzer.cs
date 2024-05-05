using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxVisualizer;

class CodeAnalyzer
{
    public static SyntaxTree TREE;
    public static void Analyze(string code)
    {
        // Input C# code

        TREE = CSharpSyntaxTree.ParseText(code);


    }

}

