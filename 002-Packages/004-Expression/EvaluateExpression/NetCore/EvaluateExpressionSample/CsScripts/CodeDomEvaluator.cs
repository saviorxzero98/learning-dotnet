using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace EvaluateExpressionSample.CsScripts
{
    /// <summary>
    /// https://www.codeproject.com/Articles/11939/Evaluate-C-Code-Eval-Function
    /// </summary>
    public class CodeDomEvaluator
    {
        public CodeNamespace Namespace { get; set; }
        public CodeTypeDeclaration Class { get; set; }
        public CodeCompileUnit Assembly { get; set; }

        public void CreateNamespace()
        {
            Namespace = new CodeNamespace("mynamespace");
        }

        public void CreateImports()
        {
            Namespace.Imports.Add(new CodeNamespaceImport("System"));
        }

        public static object Eval(string expression)
        {
            CSharpCodeProvider c = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();

            cp.ReferencedAssemblies.Add("system.dll");
            cp.ReferencedAssemblies.Add("system.math.dll");
            cp.ReferencedAssemblies.Add("system.linq.dll"); 
            cp.ReferencedAssemblies.Add("system.text.dll");
            cp.ReferencedAssemblies.Add("system.collections.dll");

            cp.CompilerOptions = "/t:library";
            cp.GenerateInMemory = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("using System;\n");
            sb.Append("using System.Math;\n");
            sb.Append("using System.Linq;\n");
            sb.Append("using System.Text;\n");
            sb.Append("using System.Text.RegularExpressions;\n");
            sb.Append("using System.Collections;\n");
            sb.Append("using System.Collections.Generic;\n");

            sb.Append("namespace CSCodeEvaler{ \n");
            sb.Append("public class CSCodeEvaler{ \n");
            sb.Append("public object EvalCode(){\n");
            sb.Append("return " + expression + "; \n");
            sb.Append("} \n");
            sb.Append("} \n");
            sb.Append("}\n");

            CompilerResults cr = c.CompileAssemblyFromSource(cp, sb.ToString());
            if (cr.Errors.Count > 0)
            {
                Console.WriteLine("ERROR: " + cr.Errors[0].ErrorText, "Error evaluating cs code");
                return null;
            }

            System.Reflection.Assembly a = cr.CompiledAssembly;
            object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");

            Type t = o.GetType();
            MethodInfo mi = t.GetMethod("EvalCode");

            object s = mi.Invoke(o, null);
            return s;
        }
    }
}
