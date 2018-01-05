using Autofac;
using Autofac.Core;
using System;
using System.Reflection;

namespace Books
{
    class Program
    {
        static IContainer container;
        static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assembly)
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<LengthValidator>()           
                .AsSelf()         
                .AsImplementedInterfaces()  
                .Named<IValidator>("lengthValidator")  
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)            
                .SingleInstance();

            builder.RegisterType<CommandExistsValidator>()
                .AsSelf()
                .AsImplementedInterfaces()
                .Named<IValidator>("commandExists")
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterType<CommandExecutor>()                  
                .AsSelf()                   
                .WithParameter(ResolvedParameter.ForNamed<IValidator>("lengthValidator"))                  
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)  
                .SingleInstance();

            builder.RegisterType<CommandManager>()
                .AsSelf()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterType<LineInterpreter>()
                .AsSelf()
                .WithParameter(ResolvedParameter.ForNamed<IValidator>("commandExists"))
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            container = builder.Build();
            Console.WriteLine("Commands: ");
            Console.WriteLine("/add <book/author>");
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