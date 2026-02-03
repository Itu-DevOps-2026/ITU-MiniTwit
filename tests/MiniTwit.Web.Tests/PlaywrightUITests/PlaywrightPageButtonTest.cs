using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[TestFixture]
[Category("Playwright")]

public class PlaywrightPageButtonTest : PageTest
{
    [Test]
    public async Task NextPreviousButtonsChangesPages()
    {
        var context = await Browser.NewContextAsync();
        var page = await context.NewPageAsync();
        
        // Check previous button is invisible on page 1
        await page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        var previous1 = page.GetByRole(AriaRole.Link, new() { Name = "previous" });
        await Expect(previous1).Not.ToBeVisibleAsync();
       
        // Check next button is visible
        var next1 = page.GetByRole(AriaRole.Link, new() { Name = "next" });
        await Expect(next1).ToBeVisibleAsync();
        
        // Check url option is correct after pressing next
        await page.GetByRole(AriaRole.Link, new() { Name = "next" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=2");
        
        await page.GetByRole(AriaRole.Link, new() { Name = "next" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=3");
        
        await page.GetByRole(AriaRole.Link, new() { Name = "next" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=4");
        
        // Check next button is invisible on page 21
        await page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=21");
        var next2 = page.GetByRole(AriaRole.Link, new() { Name = "next" });
        await Expect(next2).Not.ToBeVisibleAsync();
        
        // Check previous button is visible
        var previous2 = page.GetByRole(AriaRole.Link, new() { Name = "previous" });
        await Expect(previous2).ToBeVisibleAsync();
        
        // Check url option is correct after pressing previous
        await page.GetByRole(AriaRole.Link, new() { Name = "previous" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=20");
        
        await page.GetByRole(AriaRole.Link, new() { Name = "previous" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=19");
        
        await page.GetByRole(AriaRole.Link, new() { Name = "previous" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=18");
    }
}


