using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Microsoft.Playwright;

namespace Rayonnant.Tests.Support;

[SetUpFixture]
public class TestFixture
{
    public static string BaseUrl { get; private set; } = null!;
    public static IPlaywright? Playwright { get; private set; }
    public static IBrowser? Browser { get; private set; }
    private static Process? _serverProcess;
    private static bool _browserAvailable;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        var port = GetRandomPort();
        BaseUrl = $"http://localhost:{port}";

        var testDir = TestContext.CurrentContext.TestDirectory;
        var projectRoot = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", "..", ".."));
        var shellDll = Path.Combine(projectRoot, "src", "Rayonnant.Shell", "bin", "Release", "net10.0", "Rayonnant.Shell.dll");
        if (!File.Exists(shellDll))
            shellDll = Path.Combine(projectRoot, "src", "Rayonnant.Shell", "bin", "Debug", "net10.0", "Rayonnant.Shell.dll");
        if (!File.Exists(shellDll))
            throw new FileNotFoundException($"Cannot find Rayonnant.Shell.dll at {shellDll}");

        var dotnet = Environment.GetEnvironmentVariable("DOTNET_CMD") ?? "dotnet";

        _serverProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = dotnet,
                Arguments = $"\"{shellDll}\" --urls {BaseUrl}",
                WorkingDirectory = projectRoot,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Environment = { ["ASPNETCORE_ENVIRONMENT"] = "Development" }
            }
        };

        _serverProcess.OutputDataReceived += (_, e) => { if (e.Data != null) TestContext.Progress.WriteLine(e.Data); };
        _serverProcess.ErrorDataReceived += (_, e) => { if (e.Data != null) TestContext.Progress.WriteLine(e.Data); };

        _serverProcess.Start();
        _serverProcess.BeginOutputReadLine();
        _serverProcess.BeginErrorReadLine();

        // Wait for server to be ready
        using var httpClient = new HttpClient();
        for (int i = 0; i < 30; i++)
        {
            try
            {
                var response = await httpClient.GetAsync(BaseUrl);
                if (response.IsSuccessStatusCode) break;
            }
            catch { }
            await Task.Delay(1000);
        }

        // Try launching Playwright
        try
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new() { Headless = true });
            _browserAvailable = true;
        }
        catch (Exception ex)
        {
            TestContext.Progress.WriteLine($"Playwright browser not available: {ex.Message}");
            TestContext.Progress.WriteLine($"PLAYWRIGHT_BROWSERS_PATH={Environment.GetEnvironmentVariable("PLAYWRIGHT_BROWSERS_PATH")}");
            _browserAvailable = false;
        }
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        if (Browser != null) await Browser.CloseAsync();
        Playwright?.Dispose();

        if (_serverProcess != null && !_serverProcess.HasExited)
        {
            _serverProcess.Kill(true);
            _serverProcess.Dispose();
        }
    }

    public static bool IsBrowserAvailable => _browserAvailable;

    private static int GetRandomPort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }
}
