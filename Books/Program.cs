using Autofac;
using System;
using System.ComponentModel;

namespace Books
{
    class Program
    { 
        static void Main(string[] args)
        {
            ContainerBuild containerBuild = new ContainerBuild();
            containerBuild.Build();
            var container = containerBuild.GetContainer();

            Console.WriteLine("Commands: ");
            Console.WriteLine("/add <book/author>");
            Console.WriteLine("/view <book/author>");
            Console.WriteLine("/find <book/author> <ID>");
            Console.WriteLine("/remove <book/author> <ID>");
            Console.WriteLine("/stop");

            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<CommandManager>().RegisterCommands();
                while (true)
                {
                    string line = scope.Resolve<InputReader>().ReadInput();
                    scope.Resolve<LineInterpreter>().Interpret(line);
                }
            }
        }
    }
}