using System.Reflection;

public class ServiceProvider
{
    private readonly List<ServiceDescriptor> _services;

    public ServiceProvider(List<ServiceDescriptor> services)
    {
        _services = services;
    }

    public TService Resolve<TService>()
    {
        return (TService)Resolve(typeof(TService));
    }

    public object Resolve(Type serviceType)
    {
        var descriptor =
            GetServiceDescriptor(serviceType);

        return CreateInstance(
            descriptor.ImplementationType);
    }

    private ServiceDescriptor GetServiceDescriptor(Type serviceType)
    {
        var descriptor = _services.FirstOrDefault(
            x => x.ServiceType == serviceType);

        if (descriptor == null)
        {
            throw new InvalidOperationException(
                $"Service '{serviceType.Name}' is not registered.");
        }

        return descriptor;
    }

    private object CreateInstance(Type implementationType)
    {
        var constructor =
            implementationType.GetConstructors().Single();

        var arguments =
            ResolveConstructorArguments(constructor);

        var instance =
            Activator.CreateInstance(
                implementationType,
                arguments);

        if (instance == null)
        {
            throw new InvalidOperationException(
                $"Unable to create {implementationType.Name}");
        }

        return instance;
    }

    private object[] ResolveConstructorArguments(
    ConstructorInfo constructor)
    {
        return constructor
            .GetParameters()
            .Select(parameter =>
                Resolve(parameter.ParameterType))
            .ToArray();
    }
    // public TService Resolve<TService>()
    // {
    //     var descriptor = _services.FirstOrDefault(x =>
    //         x.ServiceType == typeof(TService));

    //     if (descriptor == null)
    //     {
    //         throw new Exception(
    //             $"Service {typeof(TService).Name} is not registered.");
    //     }

    //     return (TService)Activator.CreateInstance(
    //         descriptor.ImplementationType)!;
    // }
}