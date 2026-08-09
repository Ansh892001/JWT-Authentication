public class ServiceCollection
{
    private readonly List<ServiceDescriptor> _services
        = new();

    public void Register<TService, TImplementation>()
    {
        _services.Add(
            new ServiceDescriptor(
                typeof(TService),
                typeof(TImplementation)));
    }

    // Naive Approach for creating DI
    // public TService Resolve<TService>()
    // {
    //     var descriptor = _services.FirstOrDefault(x =>
    //         x.ServiceType == typeof(TService));

    //     if (descriptor == null)
    //     {
    //         throw new Exception(
    //             $"Service {typeof(TService).Name} is not registered.");
    //     }

    //     // Temporary implementation
    //     return (TService)Activator.CreateInstance(
    //         descriptor.ImplementationType)!;
    // }

    public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(_services);
    }
}