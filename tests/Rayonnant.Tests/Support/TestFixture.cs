using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Rayonnant.Tests;

/// <summary>
/// Manages the Rayonnant Shell server process for E2E testing.
/// </summary>
public static class ServerManager
{
    public static string BaseUrl { get; private set; } = null!;
    private static Process? _serverProcess;
    private static bool _initialized;

    public static async Task EnsureStarted()
    {
        if (_initialized) return;
        _initialized = true;

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

        _serverProcess.OutputDataReceived += (_, e) => { };
        _serverProcess.ErrorDataReceived += (_, e) => { };
        _serverProcess.Start();
        _serverProcess.BeginOutputReadLine();
        _serverProcess.BeginErrorReadLine();

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
    }

    public static void Stop()
    {
        if (_serverProcess != null && !_serverProcess.HasExited)
        {
            _serverProcess.Kill(true);
            _serverProcess.Dispose();
        }
    }

    private static int GetRandomPort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }
}
