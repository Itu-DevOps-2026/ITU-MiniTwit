using Microsoft.Playwright;

namespace MiniTwit.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightEndToEndTest : PageTest
{
    private const string HomePage = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";
    private const string TestEmail = "robert@test.dk";
    private const string TestPassword = "Robert@test.dk1";

    // Helpers
    // Registering with test user

    private async Task <(string Email, string Password)> Register()
    {
        // Generate unique suffix
        var unique = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        
        var uniqueUsername = $"Test{unique}";
        var uniqueEmail = $"{uniqueUsername}@test.dk";
        
        await Page.GotoAsync(HomePage);
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync(uniqueUsername);
        await Page.GetByPlaceholder("name@example.com").FillAsync(uniqueEmail);
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync(TestPassword);
        await Page.GetByLabel("Confirm Password").FillAsync(TestPassword);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

        return (uniqueEmail, TestPassword);
    }

// Log in with test user
    private async Task Login(string email, string password)
    {
        
        await Page.GotoAsync(HomePage);
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync(email);
        await Page.GetByPlaceholder("password").FillAsync(password);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

    }

    private async Task PostCheep(string cheep)
    {
        await Page.GotoAsync(HomePage);
        await Page.Locator("#CheepText").FillAsync(cheep);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
    }
    
    
    [Test]
    public async Task RegisterNewUser_AllowsCheeping()
    {
        var (email, password) = await Register();
        await PostCheep("Hi I'm a newly registered user");
         
        Assert.That(await Page.ContentAsync(), Does.Contain("Hi I'm a newly registered user"));
    }

    // Follow link for specific author

    [Test]
    public async Task Login_AllowsCheeping()
    {
        await Login(TestEmail, TestPassword);
        var cheepMessage = "Cheep from known user";
        await PostCheep(cheepMessage);

        var content = await Page.ContentAsync();
        Assert.That(content.Contains(cheepMessage));
    }

    [Test]
    public async Task Login_CanLikeCheep()
    {
        await Login(TestEmail, TestPassword);

        // Cheep unique cheep
        var msg = $"Like test {DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
        await PostCheep(msg);

        var cheep = Page.Locator("li").Filter(new() { HasText = msg }).First;

        // Like the cheep
        await Task.WhenAll(
            Page.WaitForLoadStateAsync(LoadState.NetworkIdle),
            cheep.GetByRole(AriaRole.Button, new() { Name = "Like" }).ClickAsync()
        );

        // Expect UnLike and 1 Likes
        await Expect(cheep.GetByRole(AriaRole.Button, new() { Name = "UnLike" }))
            .ToBeVisibleAsync(new() { Timeout = 15000 });

        await Expect(cheep).ToContainTextAsync("1 Likes", new() { Timeout = 15000 });

        // Stored in DB: navigate to About me and confirm the cheep is listed
        await Page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
        await Expect(Page.GetByText(msg)).ToBeVisibleAsync(new() {Timeout = 15000
        });
    }
    
    [Test]
    public async Task UnauthenticatedUser_CannotCheep()
    {
        await Page.GotoAsync(HomePage);
        
        // Public timeline is visible
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Public timeline"})).ToBeVisibleAsync();
        
        // Cheepbox is not visible
        await Expect(Page.Locator("#CheepText")).Not.ToBeVisibleAsync();
        
        //Like is not visible
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Like"})).Not.ToBeVisibleAsync();
    }
    
}