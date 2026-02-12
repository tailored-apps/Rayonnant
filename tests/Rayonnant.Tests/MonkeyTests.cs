using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Rayonnant.Tests;

/// <summary>
/// Monkey tests — randomly interact with each module page,
/// click buttons, expand panels, hover elements, and verify no crashes.
/// </summary>
[TestFixture]
public class MonkeyTests : PageTest
{
    // Server started/stopped by GlobalSetup [SetUpFixture]

    [Test]
    public async Task Dashboard_Monkey_Click_All_NavLinks()
    {
        await Page.GotoAsync(ServerManager.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);

        // Click every nav link and verify page doesn't crash
        var navLinks = Page.Locator("nav.mud-navmenu a.mud-nav-link");
        var count = await navLinks.CountAsync();
        Assert.That(count, Is.GreaterThanOrEqualTo(4));

        for (int i = 0; i < count; i++)
        {
            var link = navLinks.Nth(i);
            var text = await link.TextContentAsync();
            await link.ClickAsync();
            await Page.WaitForTimeoutAsync(2000);

            // Verify no error overlay appeared
            var errorBoundary = Page.Locator(".blazor-error-ui");
            var visible = await errorBoundary.IsVisibleAsync().ContinueWith(t => t.Result);
            Assert.That(visible, Is.False, $"Blazor error shown after clicking '{text?.Trim()}'");
        }
    }

    [Test]
    public async Task Users_Monkey_Click_Buttons_And_Icons()
    {
        await Page.GotoAsync(ServerManager.BaseUrl + "/users", new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);

        // Click Add User button
        var addBtn = Page.GetByText("Add User");
        if (await addBtn.CountAsync() > 0)
            await addBtn.ClickAsync();
        await Page.WaitForTimeoutAsync(500);

        // Click edit icons on table rows
        var editBtns = Page.Locator("table button").Filter(new() { Has = Page.Locator("svg") });
        var editCount = await editBtns.CountAsync();
        for (int i = 0; i < Math.Min(editCount, 6); i++)
        {
            await editBtns.Nth(i).ClickAsync();
            await Page.WaitForTimeoutAsync(300);
        }

        // Verify page still renders
        var heading = Page.GetByText("User Management");
        Assert.That(await heading.CountAsync(), Is.GreaterThan(0), "Page should still show heading after monkey clicks");

        await TakeScreenshot("monkey-users");
    }

    [Test]
    public async Task Monitoring_Monkey_Click_Services_And_Alerts()
    {
        await Page.GotoAsync(ServerManager.BaseUrl + "/monitoring", new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);

        // Click refresh/stop buttons on services
        var actionBtns = Page.Locator("table button");
        var count = await actionBtns.CountAsync();
        for (int i = 0; i < Math.Min(count, 8); i++)
        {
            await actionBtns.Nth(i).ClickAsync();
            await Page.WaitForTimeoutAsync(200);
        }

        // Click expansion panel
        var expPanel = Page.Locator(".mud-expand-panel-header");
        if (await expPanel.CountAsync() > 0)
        {
            await expPanel.First.ClickAsync();
            await Page.WaitForTimeoutAsync(500);
            await expPanel.First.ClickAsync(); // toggle
            await Page.WaitForTimeoutAsync(500);
        }

        // Verify alerts still visible
        var alerts = Page.Locator(".mud-alert");
        Assert.That(await alerts.CountAsync(), Is.GreaterThanOrEqualTo(4), "Alerts should survive monkey clicks");

        await TakeScreenshot("monkey-monitoring");
    }

    [Test]
    public async Task DataExplorer_Monkey_Type_Query_And_Click()
    {
        await Page.GotoAsync(ServerManager.BaseUrl + "/data", new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);

        // Type into query editor
        var textarea = Page.Locator("textarea").First;
        await textarea.FillAsync("SELECT * FROM monkeys WHERE chaos = true ORDER BY bananas DESC");
        await Page.WaitForTimeoutAsync(500);

        // Click Execute button
        var execBtn = Page.GetByText("Execute");
        if (await execBtn.CountAsync() > 0)
            await execBtn.ClickAsync();
        await Page.WaitForTimeoutAsync(500);

        // Click table/JSON/export button group
        var btnGroup = Page.Locator(".mud-button-group button");
        var btnCount = await btnGroup.CountAsync();
        for (int i = 0; i < btnCount; i++)
        {
            await btnGroup.Nth(i).ClickAsync();
            await Page.WaitForTimeoutAsync(300);
        }

        // Click Save Query and History
        var saveBtn = Page.GetByText("Save Query");
        if (await saveBtn.CountAsync() > 0) await saveBtn.ClickAsync();
        var histBtn = Page.GetByText("History");
        if (await histBtn.CountAsync() > 0) await histBtn.ClickAsync();
        await Page.WaitForTimeoutAsync(500);

        // Click sort headers
        var sortLabels = Page.Locator(".mud-table-sort-label");
        var sortCount = await sortLabels.CountAsync();
        for (int i = 0; i < sortCount; i++)
        {
            await sortLabels.Nth(i).ClickAsync();
            await Page.WaitForTimeoutAsync(300);
        }

        // Verify table still has data
        var rows = Page.Locator("table tbody tr");
        Assert.That(await rows.CountAsync(), Is.GreaterThanOrEqualTo(5), "Table should still have rows after monkey clicks");

        // Click play buttons on saved queries
        var playBtns = Page.Locator(".mud-list button");
        var playCount = await playBtns.CountAsync();
        for (int i = 0; i < playCount; i++)
        {
            await playBtns.Nth(i).ClickAsync();
            await Page.WaitForTimeoutAsync(200);
        }

        await TakeScreenshot("monkey-data-explorer");
    }

    [Test]
    public async Task Monkey_Rapid_Navigation_Stress()
    {
        // Rapidly switch between all pages to test for race conditions
        var routes = new[] { "/", "/users", "/monitoring", "/data", "/settings", "/", "/monitoring", "/users", "/data" };

        await Page.GotoAsync(ServerManager.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(2000);

        foreach (var route in routes)
        {
            await Page.GotoAsync(ServerManager.BaseUrl + route, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
            await Page.WaitForTimeoutAsync(500); // fast switches
        }

        // After rapid navigation, verify we're on a valid page
        await Page.WaitForTimeoutAsync(2000);
        var layout = Page.Locator(".mud-layout");
        Assert.That(await layout.CountAsync(), Is.GreaterThan(0), "Layout should survive rapid navigation");

        await TakeScreenshot("monkey-stress-final");
    }

    [Test]
    public async Task Monkey_Toggle_Drawers()
    {
        await Page.GotoAsync(ServerManager.BaseUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await Page.WaitForTimeoutAsync(3000);

        // Toggle left drawer (hamburger menu)
        var menuBtn = Page.Locator("header button").First;
        for (int i = 0; i < 5; i++)
        {
            await menuBtn.ClickAsync();
            await Page.WaitForTimeoutAsync(400);
        }

        // Toggle right drawer (sidebar button)
        var sidebarBtn = Page.Locator("header button").Nth(2);
        if (await sidebarBtn.CountAsync() > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                await sidebarBtn.ClickAsync();
                await Page.WaitForTimeoutAsync(400);
            }
        }

        // Verify layout survives
        var appBar = Page.Locator("header.mud-appbar");
        Assert.That(await appBar.CountAsync(), Is.GreaterThan(0), "AppBar should survive drawer toggling");

        await TakeScreenshot("monkey-drawers");
    }

    private async Task TakeScreenshot(string name)
    {
        var dir = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResults", "screenshots");
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, $"{name}.png");
        await Page.ScreenshotAsync(new() { Path = path, FullPage = true });
        TestContext.WriteLine($"Screenshot: {path}");
    }
}
