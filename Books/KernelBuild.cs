using Ninject;
using Ninject.Extensions.Conventions;

public class KernelBuild
{
    private IKernel kernel;

    public void Build()
    {
        kernel = new StandardKernel();

        kernel.Bind(services =>
        {
            services.FromThisAssembly()
            .SelectAllClasses()
            .InheritedFrom<IValidator>()
            .BindSingleInterface()
            .Configure(b => b.InSingletonScope());
        });

        kernel.Bind<IErrorOutput>()
            .To<ConsoleErrorOutput>()
            .InSingletonScope();

        kernel.Bind<CommandExecutor>()
            .ToSelf()
            .InSingletonScope()
            .WithPropertyValue("Validator", kernel.Get<LengthValidator>());
       
        kernel.Bind<CommandManager>()
            .ToSelf()
            .InSingletonScope();

        kernel.Bind<LineInterpreter>()
            .ToSelf()
            .InSingletonScope()
            .WithPropertyValue("validator", kernel.Get<CommandExistsValidator>());
    }

    public IKernel GetKernel()
    {
        return kernel;
    }
}

