using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Rayonnant.Tests;

[TestFixture]
public class ShellLayoutTests : PageTest
{
    // Server started/stopped by GlobalSetup [SetUpFixture]

    [Test]
    public async Task Shell_Has_AppBar_With_Title()
    {
        await Page.GotoAsync(ServerManager.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);
        var appBar = Page.Locator("header.mud-appbar");
        await appBar.WaitForAsync(new() { Timeout = 10000 });
        var text = await appBar.TextContentAsync();
        Assert.That(text, Does.Contain("Rayonnant"));
    }

    [Test]
    public async Task Shell_Has_Left_NavMenu()
    {
        await Page.GotoAsync(ServerManager.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);
        var drawer = Page.Locator("aside.mud-drawer").First;
        await drawer.WaitForAsync(new() { Timeout = 10000 });
        var nav = drawer.Locator("nav.mud-navmenu");
        Assert.That(await nav.CountAsync(), Is.GreaterThan(0));
    }

    [Test]
    public async Task Shell_Has_Search_Bar()
    {
        await Page.GotoAsync(ServerManager.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);
        var searchInput = Page.Locator("header.mud-appbar input");
        await searchInput.WaitForAsync(new() { Timeout = 10000 });
        Assert.That(await searchInput.CountAsync(), Is.GreaterThan(0));
    }

    [Test]
    public async Task Shell_Has_All_Module_NavLinks()
    {
        await Page.GotoAsync(ServerManager.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);
        var navLinks = Page.Locator("nav.mud-navmenu a.mud-nav-link");
        var count = await navLinks.CountAsync();
        Assert.That(count, Is.GreaterThanOrEqualTo(4), "Should have Dashboard, Users, Monitoring, Data Explorer + Settings");
    }

    [Test]
    public async Task Users_Page_Has_Table()
    {
        await Page.GotoAsync(ServerManager.BaseUrl + "/users", new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);
        var table = Page.Locator("table.mud-table-root");
        await table.First.WaitForAsync(new() { Timeout = 10000 });
        Assert.That(await table.CountAsync(), Is.GreaterThan(0));
    }

    [Test]
    public async Task Monitoring_Page_Has_Progress_Bars()
    {
        await Page.GotoAsync(ServerManager.BaseUrl + "/monitoring", new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);
        var progress = Page.Locator(".mud-progress-linear");
        Assert.That(await progress.CountAsync(), Is.GreaterThanOrEqualTo(4), "Should have CPU, RAM, Disk, Network bars");
    }

    [Test]
    public async Task DataExplorer_Page_Has_Query_Editor()
    {
        await Page.GotoAsync(ServerManager.BaseUrl + "/data", new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);
        var textarea = Page.Locator("textarea");
        Assert.That(await textarea.CountAsync(), Is.GreaterThan(0), "Should have SQL query textarea");
    }

    [Test]
    public async Task Shell_Takes_Screenshot_AllPages()
    {
        var dir = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResults", "screenshots");
        Directory.CreateDirectory(dir);

        var pages = new[] { ("/", "dashboard"), ("/users", "users"), ("/monitoring", "monitoring"), ("/data", "data-explorer") };
        foreach (var (path, name) in pages)
        {
            await Page.GotoAsync(ServerManager.BaseUrl + path, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
            await Page.WaitForTimeoutAsync(3000);
            var file = Path.Combine(dir, $"{name}.png");
            await Page.ScreenshotAsync(new() { Path = file, FullPage = true });
            Assert.That(File.Exists(file), Is.True, $"Screenshot {name} should exist");
            Assert.That(new FileInfo(file).Length, Is.GreaterThan(10000), $"Screenshot {name} should be >10KB (real content)");
            TestContext.WriteLine($"Screenshot: {file}");
        }
    }
}
