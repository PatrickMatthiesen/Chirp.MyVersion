namespace Chirp.UI.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public partial class Tests : PageTest {
    [Test]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage() {
        await Page.GotoAsync("https://localhost:7102");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(ChirpRegEx());

        // create a locator
        var getStarted = Page.Locator("text=Login");

        // Expect an attribute "to be strictly equal" to the value.
        await Expect(getStarted).ToHaveAttributeAsync("href", "/Identity/Account/Login");

        // Click the get started link.
        await getStarted.ClickAsync();

        // Expects the URL to contain intro.
        await Expect(Page).ToHaveURLAsync(new Regex(".*Acc.*"));
    }

    [GeneratedRegex("Chirp")]
    private static partial Regex ChirpRegEx();
}
