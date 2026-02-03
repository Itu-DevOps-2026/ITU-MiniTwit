using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace MiniTwit.Web.Tests.PlaywrightUITests;

[TestFixture]
[Category("Playwright")]

public class PlaywrightSecurityTest: PageTest
{
    [Test] 
    public async Task CheepboxCanSustainXSSandSQLAttacks() 
    {
        // Register as user registered with SQL injection as username
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync("Robert'); DROP TABLE Cheeps;--");
        await Page.GetByPlaceholder("name@example.com").FillAsync("SQLRobert@test.dk");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("SQLRobert@test.dk1");
        await Page.GetByLabel("Confirm Password", new() { Exact = true }).FillAsync("SQLRobert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        
        // Test with XSS attack
        await Page.Locator("#CheepText").ClickAsync();
        await Page.Locator("#CheepText").FillAsync("Hello, I am feeling good!<script>alert('If you see this in a popup, you are in trouble!');</script>"); //input text is directly from slides from lecture 10
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        await Expect(Page.GetByText("Robert'); DROP TABLE Cheeps;-- Hello, I am  feeling good!<script>alert('If you see this in a popup, you are in trouble!');</script>").First).ToBeVisibleAsync();

        // Forget me - to clean up
        await Page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Forget me!" }).ClickAsync();
    }
}