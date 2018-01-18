using System.Reflection;
using Autofac;
using Autofac.Core;


public class ContainerBuild
{
    private static IContainer Container;

    public void Build()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var builder = new ContainerBuilder();
        builder.RegisterAssemblyTypes(assembly)
            .AsSelf()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder.RegisterType<CorrectIntDataValidator>()
            .AsSelf()
            .AsImplementedInterfaces()
            .Named<IValidator>("correctIntValidator")
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
            .SingleInstance();

        builder.RegisterType<CorrectFloatDataValidator>()
            .AsSelf()
            .AsImplementedInterfaces()
            .Named<IValidator>("correctFloatValidator")
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
            .SingleInstance();

        builder.RegisterType<AuthorManager>()
            .AsSelf()
            .AsImplementedInterfaces()
            .WithParameter(ResolvedParameter.ForNamed<IValidator>("correctIntValidator"))
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
            .SingleInstance();

        builder.RegisterType<BookManager>()
            .AsSelf()
            .AsImplementedInterfaces()
            .WithParameter(new ResolvedParameter(
                (pi, cc) => pi.Name == "_validators",
                (pi, cc) => new[] { cc.ResolveNamed<IValidator>("correctIntValidator"), cc.ResolveNamed<IValidator>("correctFloatValidator") }))
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
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

        Container = builder.Build();
    }

    public IContainer GetContainer()
    {
        return Container;
    }
}

