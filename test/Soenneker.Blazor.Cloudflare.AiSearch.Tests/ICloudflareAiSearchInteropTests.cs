using Soenneker.Blazor.Cloudflare.AiSearch.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Cloudflare.AiSearch.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class ICloudflareAiSearchInteropTests : HostedUnitTest
{
    private readonly ICloudflareAiSearchInterop _blazorlibrary;

    public ICloudflareAiSearchInteropTests(Host host) : base(host)
    {
        _blazorlibrary = Resolve<ICloudflareAiSearchInterop>(true);
    }

    [Test]
    public void Default()
    {

    }
}
