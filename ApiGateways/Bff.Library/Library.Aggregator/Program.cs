using Library.Aggregator;
using Microsoft.AspNetCore;

await BuildWebHost(args).RunAsync();

IWebHost BuildWebHost(string[] args) =>
    WebHost
        .CreateDefaultBuilder(args)
        .UseStartup<StartUp>()
        .Build();
public partial class Program { }