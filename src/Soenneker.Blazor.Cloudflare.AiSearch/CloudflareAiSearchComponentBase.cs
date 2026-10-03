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
    private string? _validatedApiUrl;
    private string? _scriptApiUrl;
    private string? _scriptVersion;
    private string? _configuredScriptUrl;
    private string? _scriptUrl;
    private readonly Dictionary<string, object?> _attributes = new(12);

    [Inject]
    private ICloudflareAiSearchInterop Interop { get; set; } = null!;

    /// <summary>
    /// The attributes applied to the rendered Cloudflare web component.
    /// </summary>
    protected IReadOnlyDictionary<string, object?> Attributes => _attributes;

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

        if (_validatedApiUrl != Configuration.ApiUrl)
        {
            ValidateApiUrl(Configuration.ApiUrl);
            _validatedApiUrl = Configuration.ApiUrl;
        }

        Dictionary<string, object?> attributes = _attributes;
        attributes.Clear();
        if (AdditionalAttributes is not null)
        {
            foreach (KeyValuePair<string, object?> attribute in AdditionalAttributes)
                attributes[attribute.Key] = attribute.Value;
        }

        attributes["api-url"] = Configuration.ApiUrl;
        attributes["theme"] = Configuration.Theme.Value;

        AddAttribute(attributes, "placeholder", Configuration.Placeholder);
        AddAttribute(attributes, "translations", Configuration.Translations);

        if (Configuration.HideBranding)
            attributes["hide-branding"] = "true";

        AddComponentAttributes(attributes);
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
        if (_scriptUrl is not null && _scriptApiUrl == Configuration.ApiUrl &&
            _scriptVersion == Configuration.ScriptVersion && _configuredScriptUrl == Configuration.ScriptUrl)
            return _scriptUrl;

        string result = BuildScriptUrl();
        _scriptApiUrl = Configuration.ApiUrl;
        _scriptVersion = Configuration.ScriptVersion;
        _configuredScriptUrl = Configuration.ScriptUrl;
        return _scriptUrl = result;
    }

    private string BuildScriptUrl()
    {
        if (!string.IsNullOrWhiteSpace(Configuration.ScriptUrl))
        {
            ValidateSecureAbsoluteUrl(Configuration.ScriptUrl, nameof(Configuration.ScriptUrl));
            return Configuration.ScriptUrl;
        }

        if (string.IsNullOrWhiteSpace(Configuration.ScriptVersion))
            throw new InvalidOperationException($"{nameof(Configuration.ScriptVersion)} is required when {nameof(Configuration.ScriptUrl)} is not specified.");

        string version = Uri.EscapeDataString(Configuration.ScriptVersion);
        return $"{Configuration.ApiUrl.TrimEnd('/')}/assets/v{version}/search-snippet.es.js";
    }

    private static void ValidateApiUrl(string apiUrl)
    {
        Uri uri = ValidateSecureAbsoluteUrl(apiUrl, nameof(Configuration.ApiUrl));

        if (!string.IsNullOrEmpty(uri.UserInfo) || !string.IsNullOrEmpty(uri.Query) || !string.IsNullOrEmpty(uri.Fragment))
            throw new InvalidOperationException($"{nameof(Configuration.ApiUrl)} cannot contain credentials, a query, or a fragment.");
    }

    private static Uri ValidateSecureAbsoluteUrl(string value, string propertyName)
    {
        if (!Uri.TryCreate(value, UriKind.Absolute, out Uri? uri))
            throw new InvalidOperationException($"{propertyName} must be an absolute URL.");

        bool isHttps = string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);
        bool isLoopbackHttp = uri.IsLoopback && string.Equals(uri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase);

        if (!isHttps && !isLoopbackHttp)
            throw new InvalidOperationException($"{propertyName} must use HTTPS unless it is a loopback HTTP URL.");

        if (!string.IsNullOrEmpty(uri.UserInfo))
            throw new InvalidOperationException($"{propertyName} cannot contain credentials.");

        return uri;
    }
}
