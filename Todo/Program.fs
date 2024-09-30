namespace Todo


open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open DataContext
open Microsoft.Extensions.Hosting

open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting


module Program =
    let exitCode = 0

    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseStartup<Startup>() |> ignore
            )

    [<EntryPoint>]
    let main args =
        let host = CreateHostBuilder(args).Build()
       // use scope = host.Services.CreateScope()
       // let services = scope.ServiceProvider
       // let context = services.GetRequiredService<ToDoContext>()
        host.Run()
        exitCode
