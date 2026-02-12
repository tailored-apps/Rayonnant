namespace Rayonnant.Tests;

/// <summary>
/// Global test assembly setup/teardown.
/// Ensures the server is stopped when all tests complete.
/// </summary>
[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public async Task Setup()
    {
        await ServerManager.EnsureStarted();
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        ServerManager.Stop();
    }
}
