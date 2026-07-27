[![](https://img.shields.io/nuget/v/soenneker.blazor.cloudflare.aisearch.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.cloudflare.aisearch/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.cloudflare.aisearch/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.cloudflare.aisearch/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.cloudflare.aisearch.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.cloudflare.aisearch/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.cloudflare.aisearch)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.cloudflare.aisearch/codeql.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.cloudflare.aisearch/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.Cloudflare.AiSearch
### A Blazor library for integrating Cloudflare AI Search.

## Installation

```bash
dotnet add package Soenneker.Blazor.Cloudflare.AiSearch
```

## Setup

Register services in `Program.cs`:

```csharp
builder.Services.AddCloudflareAiSearchInteropAsScoped();
```

## Usage

Enable a public endpoint for your AI Search instance, allow your application's origin under **Authorized hosts**, and render the component:

```razor
@using Soenneker.Blazor.Cloudflare.AiSearch
@using Soenneker.Blazor.Cloudflare.AiSearch.Configuration
@using Soenneker.Blazor.Cloudflare.AiSearch.Enums

<CloudflareAiSearchBar Configuration="_configuration" />

@code {
    private readonly CloudflareAiSearchBarConfiguration _configuration = new()
    {
        ApiUrl = "https://<INSTANCE_ID>.search.ai.cloudflare.com/",
        Placeholder = "Search the docs...",
        MaxResults = 50,
        MaxRenderResults = 10,
        ShowUrl = true
    };
}
```

The package provides four components matching Cloudflare's UI surfaces:

- `CloudflareAiSearchBar`
- `CloudflareAiSearchModal`
- `CloudflareAiSearchChatBubble`
- `CloudflareAiSearchChatPage`

Each component loads Cloudflare's snippet module automatically after its first render.

Each component has a matching configuration type: `CloudflareAiSearchBarConfiguration`, `CloudflareAiSearchModalConfiguration`, `CloudflareAiSearchChatBubbleConfiguration`, and `CloudflareAiSearchChatPageConfiguration`.

Common configuration options include `Theme`, `Placeholder`, `HideBranding`, and `Translations`. Search configurations also support `MaxResults`, `MaxRenderResults`, `DebounceMilliseconds`, `ShowUrl`, `ShowDate`, `GroupBy`, and `RequestOptions`. Chat configuration supports `ChatQueryRewrite`.

The component defaults to Cloudflare snippet version `0.0.25`. Use `ScriptVersion` to select another hosted version, or `ScriptUrl` to provide the full ES module URL.
