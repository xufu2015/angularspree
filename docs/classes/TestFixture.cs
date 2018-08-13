using System;
using System.Net.Http;
using AutoMapper;
using Create.Core.Jwt;
using Create.Data;
using Create.Data.Reporting;
using Create2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;


namespace Create.IntegrationTest
{
  public class TestFixture<TStartup> : IDisposable where TStartup : class
  {

    private readonly TestServer _testServer;
    public HttpClient HttpClient { get; }

    public TestFixture()
    {
      ServiceCollectionExtensions.UseStaticRegistration = false;
      var webHostBuilder = new WebHostBuilder().UseStartup<TStartup>();

    
      _testServer = new TestServer(webHostBuilder);
      HttpClient = _testServer.CreateClient();
      HttpClient.BaseAddress = new Uri("http://localhost:53036");


      var databaseInitializer = GetService<IDatabaseInitializer>();
      databaseInitializer.SeedAsync().Wait();

      AuthJwtOptions.SetAuthOptions(
        issuer: "test iusser",
        audience: "audience",
        key: "very_long_very_secret_secret",
        lifetime: 1);
    }

    public TService GetService<TService>() where TService : class
    {
      return _testServer?.Host?.Services?.GetService(typeof(TService)) as TService;
    }

    public void Dispose()
    {
      HttpClient.Dispose();
      _testServer.Dispose();
      Mapper.Reset();
    }
  }
}