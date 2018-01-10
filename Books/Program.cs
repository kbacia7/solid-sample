using Ninject;
using System;

namespace Books
{
    class Program
    {
        static void Main(string[] args)
        {
            KernelBuild kernelBuild = new KernelBuild();
            kernelBuild.Build();
            IKernel kernel = kernelBuild.GetKernel();

            Console.WriteLine("Commands: ");
            Console.WriteLine("/add <book/author>");
            Console.WriteLine("/stop");
            kernel.Get<CommandManager>().RegisterCommands();
            while (true)
            {
                string line = kernel.Get<InputReader>().ReadInput();
                kernel.Get<LineInterpreter>().Interpret(line);
            }
        }
    }
}