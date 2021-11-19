using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test3
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            var str1 = "111";
            var str2 = "222";
            var test = new InvokeMethodTest();
            var invokeMethod = new InvokeMethod
            {
                TargetObject = new InArgument<InvokeMethodTest>(ctx => test),
                MethodName = "ConsoleStr",
                Parameters =
                {
                     new InArgument<string>(str1),
                     new InArgument<string>(str2)
                },
                RunAsynchronously = false
            };

            invokeMethod.BeginExecute
        }
    }
}
