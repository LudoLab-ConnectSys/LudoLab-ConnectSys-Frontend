using LudoLab_ConnectSys_Frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



builder.Services.AddMsalAuthentication(options =>
{
    /*var authentication = options.ProviderOptions.Authentication;
    authentication.ClientId = "b4382df8-e63a-4fa0-a985-4268f4563482";
    authentication.Authority = "https://login.microsoftonline.com/common";
    authentication.RedirectUri = builder.HostEnvironment.BaseAddress;

    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/.default");*/
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
});





await builder.Build().RunAsync();
