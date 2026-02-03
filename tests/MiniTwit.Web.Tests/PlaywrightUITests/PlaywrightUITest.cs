using Microsoft.Playwright;

namespace MiniTwit.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
[Category("Playwright")]

public class PlaywrightUiTest : PageTest
{
    [Test]
    public async Task CheepBoxAppearsWhenUserHasLoggedIn()
    {
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        // Cheep box should not be visible
        var cheepBox = Page.Locator(".CheepBox");
        await Expect(cheepBox).Not.ToBeVisibleAsync();
        // Log in
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();        
        // Go back to homepage after login
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        // CheepBox should be visible
        cheepBox = Page.Locator("#CheepText");
        await Expect(cheepBox).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task BehaviourWhenCheepIsLongerThan160Chars()
    {
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        // Log in
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        // Go back to homepage after login
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        var cheepInput = Page.Locator("#CheepText");
        const string tooLongCheep =
            "12345678910111213141516171819202122232425262728293031323334353637383940414243444546575859606162636465666768697071727374757677787980818283848586878889909192939495";
        // Has to be typed char by char
        await cheepInput.ClickAsync(); //read what is in the box
        await Page.Keyboard.TypeAsync(tooLongCheep);
        var value = await cheepInput.InputValueAsync();
        // Assert
        Assert.That(value.Length, Is.EqualTo(160));
        Assert.That(value, Is.EqualTo(tooLongCheep.Substring(0, 160)));
    }
}