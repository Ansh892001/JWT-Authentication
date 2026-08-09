public class ServiceDescriptor
{
    public Type ServiceType { get; }

    public Type ImplementationType { get; }

    public ServiceDescriptor(
        Type serviceType,
        Type implementationType)
    {
        ServiceType = serviceType;
        ImplementationType = implementationType;
    }
}