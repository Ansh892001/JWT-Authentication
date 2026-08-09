var services = new ServiceCollection();

services.Register<ITokenService, TokenService>();
services.Register<JwtSettings, JwtSettings>();

var provider = services.BuildServiceProvider();
var token = provider.Resolve<ITokenService>();

var constructors =
    typeof(TokenService)
        .GetConstructors();

foreach (var constructor in constructors)
{
    Console.WriteLine(constructor);
}
// Console.WriteLine(token);