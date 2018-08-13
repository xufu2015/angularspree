using System;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Create.IntegrationTest.Setup
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; }

        static ServiceProviderFactory()
        {
            HostingEnvironment env = new HostingEnvironment();
            TestStartUpDatabase startup = new TestStartUpDatabase(env);
            ServiceCollection sc = new ServiceCollection();
            startup.ConfigureServices(sc);
            ServiceProvider = sc.BuildServiceProvider();
        }
    }
}