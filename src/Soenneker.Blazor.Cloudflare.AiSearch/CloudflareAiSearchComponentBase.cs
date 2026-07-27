using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Cloudflare.AiSearch.Abstract;
using Soenneker.Blazor.Cloudflare.AiSearch.Configuration;

namespace Soenneker.Blazor.Cloudflare.AiSearch;

/// <summary>
/// Provides common configuration and lifecycle behavior for Cloudflare AI Search components.
/// </summary>
public abstract class CloudflareAiSearchComponentBase<TConfiguration> : ComponentBase
    where TConfiguration : CloudflareAiSearchConfiguration, new()
{
    private string? _loadedScriptUrl;

    [Inject]
    private ICloudflareAiSearchInterop Interop { get; set; } = null!;

    /// <summary>
    /// The attributes applied to the rendered Cloudflare web component.
    /// </summary>
    protected IReadOnlyDictionary<string, object?> Attributes { get; private set; } = new Dictionary<string, object?>();

    /// <summary>
    /// The component configuration.
    /// </summary>
    [Parameter]
    public TConfiguration Configuration { get; set; } = new();

    /// <summary>
    /// Additional attributes applied to the rendered Cloudflare web component.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object?>? AdditionalAttributes { get; set; }

    protected override void OnParametersSet()
    {
        if (string.IsNullOrWhiteSpace(Configuration.ApiUrl))
            throw new InvalidOperationException($"{nameof(Configuration.ApiUrl)} is required.");

        if (!Uri.TryCreate(Configuration.ApiUrl, UriKind.Absolute, out Uri? uri) ||
            (uri.Scheme != Uri.UriSchemeHttps && uri.Scheme != Uri.UriSchemeHttp))
        {
            throw new InvalidOperationException($"{nameof(Configuration.ApiUrl)} must be an absolute HTTP or HTTPS URL.");
        }

        var attributes = AdditionalAttributes is null
            ? new Dictionary<string, object?>()
            : new Dictionary<string, object?>(AdditionalAttributes);

        attributes["api-url"] = Configuration.ApiUrl;
        attributes["theme"] = Configuration.Theme.Value;

        AddAttribute(attributes, "placeholder", Configuration.Placeholder);
        AddAttribute(attributes, "translations", Configuration.Translations);

        if (Configuration.HideBranding)
            attributes["hide-branding"] = "true";

        AddComponentAttributes(attributes);
        Attributes = attributes;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        string scriptUrl = GetScriptUrl();

        if (string.Equals(scriptUrl, _loadedScriptUrl, StringComparison.Ordinal))
            return;

        await Interop.Initialize(scriptUrl);
        _loadedScriptUrl = scriptUrl;
    }

    /// <summary>
    /// Adds attributes that are specific to the concrete Cloudflare component.
    /// </summary>
    protected virtual void AddComponentAttributes(IDictionary<string, object?> attributes)
    {
    }

    /// <summary>
    /// Adds a non-null, non-blank attribute value.
    /// </summary>
    protected static void AddAttribute(IDictionary<string, object?> attributes, string name, object? value)
    {
        if (value is string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
                attributes[name] = text;

            return;
        }

        if (value is not null)
            attributes[name] = value;
    }

    private string GetScriptUrl()
    {
        if (!string.IsNullOrWhiteSpace(Configuration.ScriptUrl))
            return Configuration.ScriptUrl;

        if (string.IsNullOrWhiteSpace(Configuration.ScriptVersion))
            throw new InvalidOperationException($"{nameof(Configuration.ScriptVersion)} is required when {nameof(Configuration.ScriptUrl)} is not specified.");

        return $"{Configuration.ApiUrl.TrimEnd('/')}/assets/v{Configuration.ScriptVersion}/search-snippet.es.js";
    }
}
