using Microsoft.Playwright;
using Rayonnant.Tests.Support;

namespace Rayonnant.Tests;

[TestFixture]
public class ShellLayoutTests
{
    private IPage? _page;

    [SetUp]
    public async Task SetUp()
    {
        if (!TestFixture.IsBrowserAvailable)
        {
            Assert.Ignore("Playwright browser not installed.");
            return;
        }
        _page = await TestFixture.Browser!.NewPageAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        if (_page != null) await _page.CloseAsync();
    }

    [Test]
    public async Task Shell_Has_AppBar_With_Title()
    {
        await _page!.GotoAsync(TestFixture.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        var appBar = _page.Locator("header.mud-appbar");
        await appBar.WaitForAsync(new() { Timeout = 10000 });
        var text = await appBar.TextContentAsync();
        Assert.That(text, Does.Contain("Rayonnant"));
    }

    [Test]
    public async Task Shell_Has_Left_NavMenu()
    {
        await _page!.GotoAsync(TestFixture.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        var drawer = _page.Locator("aside.mud-drawer").First;
        await drawer.WaitForAsync(new() { Timeout = 10000 });
        var nav = drawer.Locator("nav.mud-navmenu");
        Assert.That(await nav.CountAsync(), Is.GreaterThan(0));
    }

    [Test]
    public async Task Shell_Has_Search_Bar()
    {
        await _page!.GotoAsync(TestFixture.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        var searchInput = _page.Locator("header.mud-appbar input");
        await searchInput.WaitForAsync(new() { Timeout = 10000 });
        Assert.That(await searchInput.CountAsync(), Is.GreaterThan(0));
    }

    [Test]
    public async Task Shell_Takes_Screenshot()
    {
        await _page!.GotoAsync(TestFixture.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await _page.WaitForTimeoutAsync(2000); // Let MudBlazor render

        var dir = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResults");
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, "shell-screenshot.png");

        await _page.ScreenshotAsync(new() { Path = path, FullPage = true });
        Assert.That(File.Exists(path), Is.True);
        TestContext.WriteLine($"Screenshot saved to {path}");
    }
}
