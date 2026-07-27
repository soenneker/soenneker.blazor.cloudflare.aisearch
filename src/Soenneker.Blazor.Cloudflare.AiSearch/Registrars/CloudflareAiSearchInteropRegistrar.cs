using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Cloudflare.AiSearch.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Blazor.Cloudflare.AiSearch.Registrars;

/// <summary>
/// Registration for the interop and utility services.
/// </summary>
public static class CloudflareAiSearchInteropRegistrar
{
    /// <summary>
    /// Adds <see cref="ICloudflareAiSearchInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddCloudflareAiSearchInteropAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
                .TryAddScoped<ICloudflareAiSearchInterop, CloudflareAiSearchInterop>();

        return services;
    }
}
