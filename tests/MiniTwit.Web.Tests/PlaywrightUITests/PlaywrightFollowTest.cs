namespace MiniTwit.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;

[TestFixture]
[Category("Playwright")]
public class PlaywrightFollowTest : PageTest
{
    // Url
    private const string HomePage = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";

    const string Author = "Jacqualine Gilcoine";

    // LoginHelper
    // Logs in Robert and ensures return to public timeline
    private async Task LoginHelperTestUser()
    {
        // Log in
        await Page.GotoAsync(HomePage);
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        // Redirect to homepage after login
        await Page.GotoAsync(HomePage);
    }

    // Follow link for specific author
    private ILocator FollowLinkForAuthor(string authorName)
    {
        // Find the locator that contains the author's name
        var cheep = Page.Locator("p").Filter(new() { HasText = authorName });

        // For that cheep, return the follow link
        return cheep.GetByRole(AriaRole.Link, new() { Name = "Follow" });
    }

    // Unfollow link for specific author
    private ILocator UnfollowLinkForAuthor(string authorName)
    {
        // Find locator containing the author´s name
        var cheep = Page.Locator("p").Filter(new() { HasText = authorName });
        // For that cheep. return the unfollow link
        return cheep.GetByRole(AriaRole.Link, new() { Name = "Unfollow" });
    }

        private async Task GoToPageWhereAuthorIsVisible(string authorName)
    {
        // Start from the public timeline
        await Page.GotoAsync(HomePage);

        while (true)
        {
            // Is the author visible on this page
            var cheepsByAuthor = Page.Locator("p").Filter(new() { HasText = authorName });
            if (await cheepsByAuthor.CountAsync() > 0)
            {
                return; // Found author, stay on this page
            }

            // If there is a "next" link, go to next page
            var nextLink = Page.GetByRole(AriaRole.Link, new() { Name = "next" });

            if (await nextLink.IsVisibleAsync())
            {
                await nextLink.ClickAsync();
            }
            else
            {
                Assert.Fail($"Could not find any cheeps by {authorName} on the public timeline.");
            }
        }
    }
    
    private async Task GoToPrivateTimeline()
    {
        await Page.GetByRole(AriaRole.Link, new() { Name = "my timeline" }).ClickAsync();
    }
    
    [Test]
    public async Task FollowButtonAppearsOnPublicTimeline()
    {
        await LoginHelperTestUser();
        var followLink = FollowLinkForAuthor(Author);
        // Just check we have at least one follow link for her
        var count = await followLink.CountAsync();
        Assert.That(count, Is.GreaterThan(0), "Expected follow link for Jacqualine.");
    }

    [Test]
    public async Task FollowChangesToUnfollow()
    {
        await LoginHelperTestUser();

        // Start by not following Jacqualine
        var unfollowLink = UnfollowLinkForAuthor(Author);
        if (await unfollowLink.CountAsync() > 0)
        {
            await unfollowLink.First.ClickAsync(); // reset to follow
        }

        // Click follow
        var followLink = FollowLinkForAuthor(Author);
        Assert.That(await followLink.CountAsync(), Is.GreaterThan(0));
        await followLink.First.ClickAsync(); //follow

        // Expect unfollow
        var newUnfollowLinks = UnfollowLinkForAuthor(Author);
        Assert.That(await newUnfollowLinks.CountAsync(), Is.GreaterThan(0));
    }

    [Test]
    public async Task UnfollowChangesBackToFollow()
    {
        await LoginHelperTestUser();
        await GoToPageWhereAuthorIsVisible(Author);

        // Ensure we are not already following Jacqualine
        var existingUnfollow = UnfollowLinkForAuthor(Author);
        if (await existingUnfollow.CountAsync() > 0)
        {
            await existingUnfollow.First.ClickAsync();
            await FollowLinkForAuthor(Author).First.WaitForAsync();
        }
        
        // Follow Jacqualine
        var followLink = FollowLinkForAuthor(Author);
        await followLink.First.ClickAsync();
        
        // Click unfollow
        var unfollowLink = UnfollowLinkForAuthor(Author);
        await unfollowLink.First.WaitForAsync();
        await unfollowLink.First.ClickAsync();

        // Expect follow
        var newFollowLinks = FollowLinkForAuthor(Author);
        await newFollowLinks.First.WaitForAsync();
        Assert.That(await newFollowLinks.CountAsync(), Is.GreaterThan(0));
    }
    [Test]
    public async Task Login_FollowUser_CheepsAppearInTimeline()
    {
        await LoginHelperTestUser();

        // Make sure we’re on a page where the chosen author actually appears
        await GoToPageWhereAuthorIsVisible(Author);

        // Ensure we start from a clean state: not following the author
        var unfollowLink = UnfollowLinkForAuthor(Author);
        if (await unfollowLink.CountAsync() > 0)
        {
            await unfollowLink.First.ClickAsync(); // reset to "Follow"
        }

        // Click Follow for that author
        var followLink = FollowLinkForAuthor(Author);
        Assert.That(await followLink.CountAsync(), Is.GreaterThan(0), "Expected a Follow link before following.");
        await followLink.First.ClickAsync();

        //Go to private timeline
        await GoToPrivateTimeline();

        //Assert the author shows up on private timeline
        var privateCheepsByAuthor = Page.Locator("p").Filter(new() { HasText = Author });
        Assert.That(await privateCheepsByAuthor.CountAsync(), Is.GreaterThan(0),
            "Expected followed author's cheeps on private timeline.");

        //change page on private timeline (if paging exists)
        var nextLink = Page.GetByRole(AriaRole.Link, new() { Name = "next" });
        if (await nextLink.IsVisibleAsync())
        {
            await nextLink.ClickAsync();

            // Either still see the author, or at least assert paging doesn’t blow up
            var privateCheepsPage2 = Page.Locator("p").Filter(new() { HasText = Author });
            Assert.That(await privateCheepsPage2.CountAsync(), Is.GreaterThanOrEqualTo(0));
        }
    }
    [Test]
    public async Task UnfollowingAuthor_RemovesCheepsFromTimeline()
    {
        await LoginHelperTestUser();
        await GoToPageWhereAuthorIsVisible(Author);
        
        //ensure already following
        var followLink = FollowLinkForAuthor(Author);
        if (await followLink.CountAsync() > 0)
        {
            // If there's a Follow link, click it so we are now following
            await followLink.First.ClickAsync();
        }
        //go to private timeline, ensuring cheeps appeared
        await GoToPrivateTimeline();
        var cheepsByAuthorOnPrivate = Page.Locator("p").Filter(new() { HasText = Author });
        Assert.That(await cheepsByAuthorOnPrivate.CountAsync(), Is.GreaterThan(0),
            $"Expected to see {Author}'s cheeps on private timeline after following.");

        // Now go back to public timeline and unfollow
        await GoToPageWhereAuthorIsVisible(Author);
        var unfollowLink = UnfollowLinkForAuthor(Author);
        Assert.That(await unfollowLink.CountAsync(), Is.GreaterThan(0),
            $"Expected an Unfollow link for {Author} on public timeline.");
        await unfollowLink.First.ClickAsync();

        // Back to private timeline – author should be gone
        await GoToPrivateTimeline();
        var cheepsAfterUnfollow = Page.Locator("p").Filter(new() { HasText = Author });
        Assert.That(await cheepsAfterUnfollow.CountAsync(), Is.EqualTo(0),
            $"Expected NOT to see {Author}'s cheeps on private timeline after unfollowing.");
    }
}