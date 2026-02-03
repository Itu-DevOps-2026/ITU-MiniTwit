using MiniTwit.Core.DTO;
using MiniTwit.Core.Interfaces;
using MiniTwit.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
namespace MiniTwit.Infrastructure.Data;

using System;


public class DbInitializer : IDbInitializer
{
    private readonly MiniTwitDBContext _context;
    private readonly UserManager<Author> _userManager;

    public DbInitializer(MiniTwitDBContext context, UserManager<Author> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task SeedDatabase()
    {
        if (_userManager.Users.Any() || _context.Cheeps.Any())
        {
            return;
        }
        await SeedAuthorsAsync();
        await SeedCheepsAsync();
    }
    
    private async Task SeedAuthorsAsync()
    {
        if (_userManager.Users.Any())
        {
            return;
        }
        await CreateAuthorAsync("Roger Histand", "Roger+Histand@hotmail.com", "Seed0@test.dk");
        await CreateAuthorAsync("Luanna Muro", "Luanna-Muro@ku.dk", "Seed0@test.dk");
        await CreateAuthorAsync("Wendell Ballan", "Wendell-Ballan@gmail.com", "Seed0@test.dk");
        await CreateAuthorAsync("Nathan Sirmon", "Nathan+Sirmon@dtu.dk", "Seed0@test.dk");
        await CreateAuthorAsync("Quintin Sitts", "Quintin+Sitts@itu.dk", "Seed0@test.dk");
        await CreateAuthorAsync("Mellie Yost", "Mellie+Yost@ku.dk", "Seed0@test.dk");
        await CreateAuthorAsync("Malcolm Janski", "Malcolm-Janski@gmail.com", "Seed0@test.dk");
        await CreateAuthorAsync("Octavio Wagganer", "Octavio.Wagganer@dtu.dk", "Seed0@test.dk");
        await CreateAuthorAsync("Johnnie Calixto", "Johnnie+Calixto@itu.dk", "Seed0@test.dk");
        await CreateAuthorAsync("Jacqualine Gilcoine", "Jacqualine.Gilcoine@gmail.com", "Seed0@test.dk");
        await CreateAuthorAsync("Helge", "ropf@itu.dk", "LetM31n!");
        await CreateAuthorAsync("Adrian", "adho@itu.dk", "M32Want_Access");
        await CreateAuthorAsync("Robert", "robert@test.dk", "Robert@test.dk1");
    }

    private async Task CreateAuthorAsync(string name, string email, string password)
    {
        if (await _userManager.FindByNameAsync(name) != null)
        {
            return;
        }

        var author = new Author
        {
            Name = name,
            Email = email,
            UserName = email,
            Following = new List<string>()
        };
        if (password == "")
        {
            await _userManager.CreateAsync(author, "Password123");
        }
        else
        {
            await _userManager.CreateAsync(author, password);
        }
    }
    
    private async Task SeedCheepsAsync()
    {
        if (_context.Cheeps.Any())
        {
            return;
        }

        var a1 = await _userManager.FindByEmailAsync("Roger+Histand@hotmail.com");
        if (a1 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new()
                {
                    Text = "You are here for at all?", Date = DateTime.Parse("2023-08-01 13:13:18"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "But ere I could not find it a name that I come from.",
                    Date = DateTime.Parse("2023-08-01 13:17:18"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "In the card-case is a wonderful old man!", Date = DateTime.Parse("2023-08-01 13:15:42"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "A draghound will follow aniseed from here to enter into my heart.",
                    Date = DateTime.Parse("2023-08-01 13:14:38"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "Leave your horses below and nerving itself to concealment.",
                    Date = DateTime.Parse("2023-08-01 13:16:54"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "But I have quite come to Mackleton with me now for a small figure, sir.",
                    Date = DateTime.Parse("2023-08-01 13:15:23"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "Holmes glanced at his busy desk, hurriedly making out his watch, and ever afterwards are missing, Starbuck!",
                    Date = DateTime.Parse("2023-08-01 13:13:26"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "I was asking for your lives!''  _Wharton the Whale Killer_.",
                    Date = DateTime.Parse("2023-08-01 13:13:35"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "Nor can piety itself, at such a pair of as a lobster if he had needed it; but no, it''s like that, does he?",
                    Date = DateTime.Parse("2023-08-01 13:15:42"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "The chimney is wide, but is not upon this also.",
                    Date = DateTime.Parse("2023-08-01 13:14:01"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "For a moment to lose!", Date = DateTime.Parse("2023-08-01 13:14:12"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "On perceiving the drift of my uncle felt as though these presents were so like that of the Borgias.",
                    Date = DateTime.Parse("2023-08-01 13:16:32"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "The queerest perhaps-- said Holmes in his affairs; so if all the papers.",
                    Date = DateTime.Parse("2023-08-01 13:13:40"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "His brother Mycroft was sitting in the waggon when we finished.",
                    Date = DateTime.Parse("2023-08-01 13:14:16"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "There was no yoking them.", Date = DateTime.Parse("2023-08-01 13:13:51"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "Then I was fairly within the last men in it which was ajar.",
                    Date = DateTime.Parse("2023-08-01 13:13:16"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "We thought the ship the day of the outside and in.",
                    Date = DateTime.Parse("2023-08-01 13:17:02"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "McMurdo strolled up the girl.", Date = DateTime.Parse("2023-08-01 13:13:34"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "You have no doubt the luminous mixture with which I will quit it, lest Truth shake me falsely.",
                    Date = DateTime.Parse("2023-08-01 13:14:35"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "I have the truth out of all other explanations are more busy than yourself.",
                    Date = DateTime.Parse("2023-08-01 13:17:08"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "Well, we can only find what the devil did desire to see the letter.",
                    Date = DateTime.Parse("2023-08-01 13:13:36"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "The murder of its outrages were traced home to the horse''s head, and skirting in search of them.",
                    Date = DateTime.Parse("2023-08-01 13:16:14"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "They had sat down once more to learn, tar in general breathe the air of a little time, said Holmes.",
                    Date = DateTime.Parse("2023-08-01 13:15:20"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "Immense as whales, the Commodore was pleased at the Museum of the whale.",
                    Date = DateTime.Parse("2023-08-01 13:13:37"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "He read the accusation in the air.", Date = DateTime.Parse("2023-08-01 13:14:15"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "The group of officials who crowded round him in his singular introspective fashion.",
                    Date = DateTime.Parse("2023-08-01 13:15:35"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "From hour to hour yesterday I saw my white face of it?",
                    Date = DateTime.Parse("2023-08-01 13:13:14"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "I have the letter.", Date = DateTime.Parse("2023-08-01 13:13:30"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "But I expect you will observe that the sperm whale, compared with the lady.",
                    Date = DateTime.Parse("2023-08-01 13:15:16"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "But the main brace, to see what whaling is, eh?",
                    Date = DateTime.Parse("2023-08-01 13:15:30"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "He will be a stick, and I tell you all ready?",
                    Date = DateTime.Parse("2023-08-01 13:14:32"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "Yes, yes, I am horror-struck at this callous and hard-hearted, said she.",
                    Date = DateTime.Parse("2023-08-01 13:13:44"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "For, owing to the blood of those fine whales, Hand, boys, over hand!",
                    Date = DateTime.Parse("2023-08-01 13:14:31"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "From the top of it, that if I have here two pledges that I came out, and with you, I feared that you could not unravel.",
                    Date = DateTime.Parse("2023-08-01 13:15:54"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "Have you made anything out yet? she asked.", Date = DateTime.Parse("2023-08-01 13:13:54"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "Did the stable-boy, when he wrote so seldom, how did you do know, but what was she dressed?",
                    Date = DateTime.Parse("2023-08-01 13:13:48"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "I have no less a being than the three animals stood motionless in the pan; that''s not good.",
                    Date = DateTime.Parse("2023-08-01 13:06:09"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "It was over the heads of their profession.", Date = DateTime.Parse("2023-08-01 13:13:41"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "If the lady whom I had made himself one of the SEA UNICORN, of Dundee.",
                    Date = DateTime.Parse("2023-08-01 13:15:20"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "Well, his death he was a very serious thing.", Date = DateTime.Parse("2023-08-01 13:13:44"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "He saw her white face and flashing eyes.", Date = DateTime.Parse("2023-08-01 13:13:18"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "I''ll take the knee against in darting or stabbing at the place deserted.",
                    Date = DateTime.Parse("2023-08-01 13:14:36"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "If I was myself consulted upon the floor like a whale.",
                    Date = DateTime.Parse("2023-08-01 13:17:04"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "I had closed the door, and the ordinary irrational horrors of the Cannibals; and ready traveller.",
                    Date = DateTime.Parse("2023-08-01 13:14:02"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "And blew out the four walls, and far from being exhausted.",
                    Date = DateTime.Parse("2023-08-01 13:15:30"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "You will see to the spot.", Date = DateTime.Parse("2023-08-01 13:14:20"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "I waited for him to the deck, summoned the servants.",
                    Date = DateTime.Parse("2023-08-01 13:17:13"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "It was the secret seas have ever known.", Date = DateTime.Parse("2023-08-01 13:13:36"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "He impressed me with a jack-knife in his pocket.",
                    Date = DateTime.Parse("2023-08-01 13:15:00"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "In 1778, a fine one, said Holmes.", Date = DateTime.Parse("2023-08-01 13:14:22"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "Then this visitor had left us a shock and the one object upon which I need hardly be arranged so easily.",
                    Date = DateTime.Parse("2023-08-01 13:14:31"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "His name, I have in bringing me safely to the King his father''s influence could prevail.",
                    Date = DateTime.Parse("2023-08-01 13:13:14"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "He said nothing of the huge monoliths which are of those who were mending a top-sail in the American had been hiding here, sure enough.",
                    Date = DateTime.Parse("2023-08-01 13:14:16"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "All right, Barrymore, you can hardly believe it, but of course there was no easy task.",
                    Date = DateTime.Parse("2023-08-01 13:15:25"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "May I ask no questions.", Date = DateTime.Parse("2023-08-01 13:16:15"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "But what was this letter, so I tell it ye from the beginning.",
                    Date = DateTime.Parse("2023-08-01 13:13:26"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "You can understand his regarding it as honest a man distracted.",
                    Date = DateTime.Parse("2023-08-01 13:17:20"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "Your eyes turned full upon his breast.", Date = DateTime.Parse("2023-08-01 13:15:05"),
                    LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text =
                        "For at bottom so he told me that the gentleman thanking me on the Stile, Mary, and On the contrary, passengers themselves must pay.",
                    Date = DateTime.Parse("2023-08-01 13:13:46"), LikedBy = new List<string>(), Author = a1
                },
                new()
                {
                    Text = "This he placed the slipper upon the whale, where all is well.",
                    Date = DateTime.Parse("2023-08-01 13:14:09"), LikedBy = new List<string>(), Author = a1
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }

        var a2 = await _userManager.FindByEmailAsync("Luanna-Muro@ku.dk");
        if (a2 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text =
                        "It was but a very ancient cluster of blocks generally painted green, and for no other, he shielded me.",
                    Date = DateTime.Parse("2023-08-01 13:14:01"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "See how that murderer could be from any trivial business not connected with her.",
                    Date = DateTime.Parse("2023-08-01 13:13:21"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "Of all the sailors called them ring-bolts, and would lay my hand into the wind''s eye.",
                    Date = DateTime.Parse("2023-08-01 13:13:55"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text =
                        "I begin to get more worn than others, and in his eyes seemed to be handy in case of sawed-off shotguns and clumsy six-shooters.",
                    Date = DateTime.Parse("2023-08-01 13:14:45"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "But Elijah passed on, without seeming to hear the deep to be haunting it.",
                    Date = DateTime.Parse("2023-08-01 13:14:23"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "Every one knows how these things a man''s finger nails, by his peculiar way.",
                    Date = DateTime.Parse("2023-08-01 13:15:56"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "He grazed his cattle on these excursions, the affair remained a mystery to me also.",
                    Date = DateTime.Parse("2023-08-01 13:14:27"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text =
                        "Only one more good round look aloft here at last we have several gourds of water over his face.",
                    Date = DateTime.Parse("2023-08-01 13:13:50"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "When I returned to Coombe Tracey, but Watson here will tell him from that of the hall.",
                    Date = DateTime.Parse("2023-08-01 13:14:24"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "But it so shaded off into the drawing-room.", Date = DateTime.Parse("2023-08-01 13:13:24"),
                    LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "Then he had first worked together.", Date = DateTime.Parse("2023-08-01 13:13:41"),
                    LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text =
                        "You remember that it is a bad cold in the turns upon turns in giddy anguish, praying God for mercy, and you can check me where I am.",
                    Date = DateTime.Parse("2023-08-01 13:16:19"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "And equally fallacious seems the banished and unconquerable Cain of his thoughts.",
                    Date = DateTime.Parse("2023-08-01 13:14:59"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "That is certainly a singular appearance, even in law.",
                    Date = DateTime.Parse("2023-08-01 13:13:37"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "I fainted dead away, and we married a worthy fellow very kindly escorted me here.",
                    Date = DateTime.Parse("2023-08-01 13:13:19"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "But now, tell me, Mr. Holmes!", Date = DateTime.Parse("2023-08-01 13:16:30"),
                    LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text =
                        "If you will bear a strain in exercise or a foot of the Regency, stared down into a bundle, and I met him there once.",
                    Date = DateTime.Parse("2023-08-01 13:13:34"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "It was close on to continue his triumphant career at Cambridge.",
                    Date = DateTime.Parse("2023-08-01 13:15:00"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "You mark my words, when this incident of the ledge.",
                    Date = DateTime.Parse("2023-08-01 13:15:17"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "''From the beginning of the dead of night, and then you have come, however, before I left.",
                    Date = DateTime.Parse("2023-08-01 13:14:36"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "And running up a path which Stapleton had marked out.",
                    Date = DateTime.Parse("2023-08-01 13:13:40"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "They were lighting the lamps they could not get out of it, sir?",
                    Date = DateTime.Parse("2023-08-01 13:14:23"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "Has he been born in ''45--fifty years of absence have entirely taken away from me.",
                    Date = DateTime.Parse("2023-08-01 13:14:25"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "Our cabs were dismissed, and, following the guidance of Toby down the wall.",
                    Date = DateTime.Parse("2023-08-01 13:15:33"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text =
                        "Now this Radney, I will lay you two others supported her gaunt companion, and his face towards me.",
                    Date = DateTime.Parse("2023-08-01 13:13:24"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "She knows it too.", Date = DateTime.Parse("2023-08-01 13:13:38"),
                    LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "By my old armchair in the prairie; he hides among the oldest in the noon-day air.",
                    Date = DateTime.Parse("2023-08-01 13:14:00"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "Have you ever mention to any one of my story.",
                    Date = DateTime.Parse("2023-08-01 13:16:47"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "My own nervous system is an end of his seemed all the trails.",
                    Date = DateTime.Parse("2023-08-01 13:13:21"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text = "Stand at the corners of the moor upon his rifle from the hinges of the heath.",
                    Date = DateTime.Parse("2023-08-01 13:14:33"), LikedBy = new List<string>(), Author = a2
                },
                new Cheep
                {
                    Text =
                        "But there has suddenly sprung up between my saviour and the preacher''s text was about to precede me up wonderfully.",
                    Date = DateTime.Parse("2023-08-01 13:13:18"), LikedBy = new List<string>(), Author = a2
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }

        var a3 = await _userManager.FindByEmailAsync("Wendell-Ballan@gmail.com");
        if (a3 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text = "At first he had only exchanged one trouble for another.",
                    Date = DateTime.Parse("2023-08-01 13:14:13"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "My friend, said he.", Date = DateTime.Parse("2023-08-01 13:13:36"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "The commissionnaire and his hands to unconditional perdition, in case he was either very long one.",
                    Date = DateTime.Parse("2023-08-01 13:14:22"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "D''ye see him? cried Ahab, exultingly but on!",
                    Date = DateTime.Parse("2023-08-01 13:15:13"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "A tenth branch of the Mutiny, and so floated an unappropriated corpse.",
                    Date = DateTime.Parse("2023-08-01 13:16:29"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "At the same figure before, and I knew the reason of a blazing fool, kept kicking at it.",
                    Date = DateTime.Parse("2023-08-01 13:13:59"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "Douglas had been found in the mornings, save upon those still more ancient Hebrew story of Jonah.",
                    Date = DateTime.Parse("2023-08-01 13:14:03"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "I am not to play a desperate game.", Date = DateTime.Parse("2023-08-01 13:14:22"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Time itself now clearly enough to escape the question.",
                    Date = DateTime.Parse("2023-08-01 13:13:19"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "They generally are of age, he said, gruffly.", Date = DateTime.Parse("2023-08-01 13:15:50"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "But as this figure had been concerned in the United States government and of my task all struck out.",
                    Date = DateTime.Parse("2023-08-01 13:13:42"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "What we have him, Doctor--I''ll lay you two gentlemen passed us, blurred and vague.",
                    Date = DateTime.Parse("2023-08-01 13:13:52"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Here three men drank their glasses, and in concert with peaked flukes.",
                    Date = DateTime.Parse("2023-08-01 13:13:21"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "Absolutely! said I. But why should the officer of the first to last him for the address of the documents which are his assailants.",
                    Date = DateTime.Parse("2023-08-01 13:13:57"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "Colonel Lysander Stark sprang out, and, as for Queequeg himself, what he was exceedingly loath to say so.",
                    Date = DateTime.Parse("2023-08-01 13:13:22"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "A comparison of photographs has proved that they can do.",
                    Date = DateTime.Parse("2023-08-01 13:13:40"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "But I mean by that?", Date = DateTime.Parse("2023-08-01 13:14:06"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Now, when with a frowning brow and a knowing smile.",
                    Date = DateTime.Parse("2023-08-01 13:13:34"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Holmes unfolded the rough nugget on it yesterday.",
                    Date = DateTime.Parse("2023-08-01 13:13:42"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "There''s two of its youth, it has reached me.",
                    Date = DateTime.Parse("2023-08-01 13:14:57"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "And then, of course, by any general hatred of Napoleon by the sweep of the house.",
                    Date = DateTime.Parse("2023-08-01 13:13:44"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "As I turned up one by one, said Flask, the carpenter here can arrange everything.",
                    Date = DateTime.Parse("2023-08-01 13:17:26"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "I think that we may gain that by chance these precious parts in farces though I cannot explain the alarm was leading them.",
                    Date = DateTime.Parse("2023-08-01 13:13:34"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "I tapped at the present are within the house.",
                    Date = DateTime.Parse("2023-08-01 13:13:18"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Yet her bright and cloudless.", Date = DateTime.Parse("2023-08-01 13:13:39"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "Swerve me? ye cannot swerve me, else ye swerve yourselves! man has to be drunk in order to avoid scandal in so busy a place.",
                    Date = DateTime.Parse("2023-08-01 13:13:37"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "''My heart grew light when the working fit was upon the forearm.",
                    Date = DateTime.Parse("2023-08-01 13:14:25"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "Now, gentlemen, perhaps you expect to hear that he rushed in, and drew me over this, are you?",
                    Date = DateTime.Parse("2023-08-01 13:15:41"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "Holmes walked swiftly back to the party would return with his sons on each prow of his before ever they came over and examined that also.",
                    Date = DateTime.Parse("2023-08-01 13:13:48"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Very good, do you make of that?", Date = DateTime.Parse("2023-08-01 13:14:09"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "I left the room.", Date = DateTime.Parse("2023-08-01 13:13:58"),
                    LikedBy = new List<string>(),
                    Author = a3
                },
                new Cheep
                {
                    Text = "But in the rain; Mr. Stubb, I thought that our kinship makes it a formidable weapon.",
                    Date = DateTime.Parse("2023-08-01 13:13:28"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Look! see yon Albicore! who put it out upon the moor.",
                    Date = DateTime.Parse("2023-08-01 13:13:42"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Have you been doing at Mawson''s?", Date = DateTime.Parse("2023-08-01 13:16:30"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "To-day I was left to enable him to lunch with me to propose that you find things go together.",
                    Date = DateTime.Parse("2023-08-01 13:16:18"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Perhaps that is like this.", Date = DateTime.Parse("2023-08-01 13:15:49"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "He was dressed like a woman who answered the Guernsey-man, under cover of darkness, I must arrange with you.",
                    Date = DateTime.Parse("2023-08-01 13:14:13"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Mad with the shutter open, but without reply.",
                    Date = DateTime.Parse("2023-08-01 13:14:27"),
                    LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "Why should she fight against without my putting more upon their tomb.",
                    Date = DateTime.Parse("2023-08-01 13:16:37"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "I placed it upon a collection of weapons brought from the ridge upon our bearskin hearth-rug.",
                    Date = DateTime.Parse("2023-08-01 13:14:03"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "So now, my dear Mr. Mac, it is one of biscuits, and a thermometer of 90 was no accident?",
                    Date = DateTime.Parse("2023-08-01 13:14:54"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text = "The men drank their glasses, and in that same day, too, gazing far down the quay.",
                    Date = DateTime.Parse("2023-08-01 13:16:04"), LikedBy = new List<string>(), Author = a3
                },
                new Cheep
                {
                    Text =
                        "No great and rich banners waving, are in the same time, said the Colonel, with his dull, malevolent eyes.",
                    Date = DateTime.Parse("2023-08-01 13:13:14"), LikedBy = new List<string>(), Author = a3
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }

        var a4 = await _userManager.FindByEmailAsync("Nathan+Sirmon@dtu.dk");
        if (a4 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text = "I had no difficulty in finding where Sholto lived, and take it and in Canada.",
                    Date = DateTime.Parse("2023-08-01 13:14:11"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "I laughed very heartily, with a great consolation to all appearances in port.",
                    Date = DateTime.Parse("2023-08-01 13:14:58"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "He had prospered well, and she could have been.",
                    Date = DateTime.Parse("2023-08-01 13:14:54"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text =
                        "Phelps seized his trumpet, and knowing by her bedroom fire, with his chief followers shared his fate.",
                    Date = DateTime.Parse("2023-08-01 13:16:20"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "You can''t help thinking that I was leaning against it_.",
                    Date = DateTime.Parse("2023-08-01 13:16:56"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "Half in my rear, and once more arose, and with soft green moss, where I used to be.",
                    Date = DateTime.Parse("2023-08-01 13:15:31"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "I''m sorry, Councillor, but it''s the same indignant reply.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text =
                        "To-morrow at midnight, said the younger clutching his throat and sent off a frock, and the trees.",
                    Date = DateTime.Parse("2023-08-01 13:14:59"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "It might have made the matter was so delicate a matter.",
                    Date = DateTime.Parse("2023-08-01 13:13:21"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "Tied up and down the levers and the boy''s face from the top of it.",
                    Date = DateTime.Parse("2023-08-01 13:13:55"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "It is as an escort to you, sir.", Date = DateTime.Parse("2023-08-01 13:14:15"),
                    LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "Once again I had observed the proceedings from my mind.",
                    Date = DateTime.Parse("2023-08-01 13:14:03"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "I have a case.", Date = DateTime.Parse("2023-08-01 13:16:37"), LikedBy = new List<string>(),
                    Author = a4
                },
                new Cheep
                {
                    Text = "Why should we not been employed.", Date = DateTime.Parse("2023-08-01 13:14:07"),
                    LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "Where is the one to the long arm being the one beyond, which shines so brightly?",
                    Date = DateTime.Parse("2023-08-01 13:13:26"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "In this I had it would just cover that bare space and correspond with these.",
                    Date = DateTime.Parse("2023-08-01 13:13:19"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "Just as she ran downstairs.", Date = DateTime.Parse("2023-08-01 13:14:29"),
                    LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "It''s all as brave as you are guilty.", Date = DateTime.Parse("2023-08-01 13:13:59"),
                    LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text =
                        "The next he was sober, but a long, limber, portentous, black mass of black, fluffy ashes, as of burned paper, while the three at the Pole.",
                    Date = DateTime.Parse("2023-08-01 13:14:23"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "It will break bones beware, beware!", Date = DateTime.Parse("2023-08-01 13:15:19"),
                    LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "Can you see him again upon unknown rocks and breakers; for the best.",
                    Date = DateTime.Parse("2023-08-01 13:15:27"), LikedBy = new List<string>(), Author = a4
                },
                new Cheep
                {
                    Text = "For, thought Ahab, is sordidness.", Date = DateTime.Parse("2023-08-01 13:16:07"),
                    LikedBy = new List<string>(), Author = a4
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }

        var a5 = await _userManager.FindByEmailAsync("Quintin+Sitts@itu.dk");
        if (a5 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text =
                        "Unless we succeed in establishing ourselves in some monomaniac way whatever significance might lurk in them.",
                    Date = DateTime.Parse("2023-08-01 13:14:34"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "What did they take?", Date = DateTime.Parse("2023-08-01 13:14:44"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "My friend took the treasure-box to the window.",
                    Date = DateTime.Parse("2023-08-01 13:15:17"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "The message was as well live in this way-- SHERLOCK HOLMES--his limits.",
                    Date = DateTime.Parse("2023-08-01 13:13:40"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "You were dwelling upon the ground, the sky, the spray that he would be a man''s forefinger dipped in blood.",
                    Date = DateTime.Parse("2023-08-01 13:13:55"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "I don''t like it, he said, and would have been just a little chat with me.",
                    Date = DateTime.Parse("2023-08-01 13:13:59"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Is there a small outhouse which stands opposite to me, so as to my charge.",
                    Date = DateTime.Parse("2023-08-01 13:14:38"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "His bridle is missing, so that a dangerous man to be that they had been employed between 8.30 and the boat to board and lodging.",
                    Date = DateTime.Parse("2023-08-01 13:16:19"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "There he sat; and all he does not use his powers of observation and deduction.",
                    Date = DateTime.Parse("2023-08-01 13:16:38"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "It came from a grove of Scotch firs, and I were strolling on the soft gravel, and finally the dining-room.",
                    Date = DateTime.Parse("2023-08-01 13:14:04"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "As to the door.", Date = DateTime.Parse("2023-08-01 13:15:05"),
                    LikedBy = new List<string>(),
                    Author = a5
                },
                new Cheep
                {
                    Text =
                        "I have the particular page to which points were essential and what a very small, dark fellow, with his pipe.",
                    Date = DateTime.Parse("2023-08-01 13:14:07"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "Why, then we shall probably never have known some whalemen calculate the creature''s future wake through the foggy streets.",
                    Date = DateTime.Parse("2023-08-01 13:13:35"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "McMurdo stared at Sherlock Holmes sat in his nightdress.",
                    Date = DateTime.Parse("2023-08-01 13:13:18"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "These devils would give him a dash of your skull, whoever you are distrustful, bring two friends.",
                    Date = DateTime.Parse("2023-08-01 13:15:19"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "It was an elderly red-faced man with might and main topsails are reefed and set; she heads her course.",
                    Date = DateTime.Parse("2023-08-01 13:13:24"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "It must be ginger, peering into it, serves to brace the ship would bid them hoist a sail still higher, or to desire.",
                    Date = DateTime.Parse("2023-08-01 13:14:24"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "McGinty, who had been intimately associated with the house.",
                    Date = DateTime.Parse("2023-08-01 13:13:24"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "He looked across at me, spitting and cursing, with murder in his possession?",
                    Date = DateTime.Parse("2023-08-01 13:15:50"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "What do you want to.", Date = DateTime.Parse("2023-08-01 13:13:53"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Why, Mr. Holmes, but it is undoubted that a cry of dismay than perhaps aught else.",
                    Date = DateTime.Parse("2023-08-01 13:13:33"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "For the matter dropped.", Date = DateTime.Parse("2023-08-01 13:14:34"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "As I watched him disappearing in the main hatches, I saw him with gray limestone boulders, stretched behind us.",
                    Date = DateTime.Parse("2023-08-01 13:13:32"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Colonel Sebastian Moran, who shot one of them described as dimly lighted?",
                    Date = DateTime.Parse("2023-08-01 13:14:12"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "When I went ashore; so we were walking down it is that nothing should stand in it, when he said with a bluish flame and the police.",
                    Date = DateTime.Parse("2023-08-01 13:14:17"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "As far as I thought of the fishery, it has been here.",
                    Date = DateTime.Parse("2023-08-01 13:14:57"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "There''s the story may be set down by the whole matter carefully over.",
                    Date = DateTime.Parse("2023-08-01 13:14:16"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "Koo-loo! howled Queequeg, as if it were to drag the firm for which my poor Watson, here we made our way to bed; but, as he said.",
                    Date = DateTime.Parse("2023-08-01 13:13:33"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "And I even distinguished that morning, and then, keeping at a loss to explain, would be best to keep your lips from twitching.",
                    Date = DateTime.Parse("2023-08-01 13:13:53"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "The gallows, ye mean. I am immortal then, on the inside, and jump into his head good humouredly.",
                    Date = DateTime.Parse("2023-08-01 13:15:29"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "It will be presented may have been his client.",
                    Date = DateTime.Parse("2023-08-01 13:13:44"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "She was enveloped in a flooded world.", Date = DateTime.Parse("2023-08-01 13:15:05"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "What did they take?", Date = DateTime.Parse("2023-08-01 13:13:37"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "What! the captain himself being made a break or flaw.",
                    Date = DateTime.Parse("2023-08-01 13:13:57"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "The question now is, what can that be but a dim scrawl; what''s this?",
                    Date = DateTime.Parse("2023-08-01 13:13:58"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Any way, I''ll have the cab was out for a moment from his pocket, I guess.",
                    Date = DateTime.Parse("2023-08-01 13:14:33"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "When he had jumped on me he''d have had a lucky voyage, might pretty nearly filled a stoneware jar with water, for he had treated us.",
                    Date = DateTime.Parse("2023-08-01 13:13:42"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "That way it comes.", Date = DateTime.Parse("2023-08-01 13:14:49"),
                    LikedBy = new List<string>(),
                    Author = a5
                },
                new Cheep
                {
                    Text = "And how have I known any profound being that you will admit that the fiery waste.",
                    Date = DateTime.Parse("2023-08-01 13:14:19"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "The bread but that couldn''t be sure they all open out into a curve with his hands.",
                    Date = DateTime.Parse("2023-08-01 13:13:50"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Some pretend to be correct.", Date = DateTime.Parse("2023-08-01 13:13:30"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "I tossed the quick analysis of the White Whale, the front room on his coming out of the port-holes.",
                    Date = DateTime.Parse("2023-08-01 13:13:32"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "She is, as you or the Twins.", Date = DateTime.Parse("2023-08-01 13:13:21"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "He had, as you perceive, was made that suggestion to me that no wood is in reality his wife.",
                    Date = DateTime.Parse("2023-08-01 13:13:16"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Meanwhile, I should speak of him yet.", Date = DateTime.Parse("2023-08-01 13:15:01"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Now in getting started.", Date = DateTime.Parse("2023-08-01 13:15:02"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "If I go, but Holmes caught up the side of mankind devilish dark at that.",
                    Date = DateTime.Parse("2023-08-01 13:16:26"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "There''s one thing, cried the owner.", Date = DateTime.Parse("2023-08-01 13:15:50"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "It was a second cab and not his business, and a girl.",
                    Date = DateTime.Parse("2023-08-01 13:13:25"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "I think, said he.", Date = DateTime.Parse("2023-08-01 13:13:34"),
                    LikedBy = new List<string>(),
                    Author = a5
                },
                new Cheep
                {
                    Text =
                        "Therefore, the common is usually a great pile of crumpled morning papers, evidently newly studied, near at hand.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Sherlock Holmes returned from the direction of their graves, boys that''s all.",
                    Date = DateTime.Parse("2023-08-01 13:13:25"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "An open telegram lay upon that chair over yonder near the window on the choruses.",
                    Date = DateTime.Parse("2023-08-01 13:14:10"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Douglas was lying ill in the shadow?", Date = DateTime.Parse("2023-08-01 13:14:41"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Swim away from your contemporary consciousness.",
                    Date = DateTime.Parse("2023-08-01 13:13:45"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "It was not his own, and I live in Russia as in the future only could see from the inside.",
                    Date = DateTime.Parse("2023-08-01 13:13:52"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "It is a sad mistake for which he had long since come to me at the head of the Boscombe Valley Mystery V. The Five Orange Pips VI.",
                    Date = DateTime.Parse("2023-08-01 13:15:45"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "On one side, I promise you that he never heeded my presence, she went to Devonshire he had emerged again.",
                    Date = DateTime.Parse("2023-08-01 13:14:45"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Yet complete revenge he had, as you choose.", Date = DateTime.Parse("2023-08-01 13:13:31"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "Where was the cause of that fatal cork, forth flew the fiend, and shrivelled up his coat, laid his hand at last.",
                    Date = DateTime.Parse("2023-08-01 13:14:53"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Captain Morstan came stumbling along on the edge of it.",
                    Date = DateTime.Parse("2023-08-01 13:14:36"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "He had played nearly every day I met her first, though quite young--only twenty-five.",
                    Date = DateTime.Parse("2023-08-01 13:14:06"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "He inquired how we should do Arthur--that is, Lord Saltire--a mischief, that I owe a great boulder crashed down on this head.",
                    Date = DateTime.Parse("2023-08-01 13:13:24"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Ye are not so much as suspected.", Date = DateTime.Parse("2023-08-01 13:15:07"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "I say, Queequeg! why don''t you break your backbones, my boys?",
                    Date = DateTime.Parse("2023-08-01 13:13:41"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "On reaching the end of either, there came a sound so deep an influence over her?",
                    Date = DateTime.Parse("2023-08-01 13:17:14"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "He''ll see that whale a bow-window some five feet should be very much surprised if this were he.",
                    Date = DateTime.Parse("2023-08-01 13:14:45"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "To the credulous mariners it seemed the cunning jeweller would show them when they were swallowed.",
                    Date = DateTime.Parse("2023-08-01 13:17:05"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "For myself, my term of imprisonment was.", Date = DateTime.Parse("2023-08-01 13:13:52"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "No wonder that to climb it.", Date = DateTime.Parse("2023-08-01 13:14:20"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Kill him! cried Stubb.", Date = DateTime.Parse("2023-08-01 13:13:55"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Some were thickly clustered with men, as they called the fun.",
                    Date = DateTime.Parse("2023-08-01 13:13:17"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Consider an athlete with one hand upon the way.",
                    Date = DateTime.Parse("2023-08-01 13:13:24"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text =
                        "It''s bad enough to appal the stoutest man who was my benefactor, and all for our investigation.",
                    Date = DateTime.Parse("2023-08-01 13:17:32"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "For then, more whales the less to her, as you very much.",
                    Date = DateTime.Parse("2023-08-01 13:14:07"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Shipmates, I do not mean The Cooper, but The Merchant.",
                    Date = DateTime.Parse("2023-08-01 13:13:31"), LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "Here in London whom he loved.", Date = DateTime.Parse("2023-08-01 13:15:25"),
                    LikedBy = new List<string>(), Author = a5
                },
                new Cheep
                {
                    Text = "I really don''t think I''ll get him every particular that I tell.",
                    Date = DateTime.Parse("2023-08-01 13:14:20"), LikedBy = new List<string>(), Author = a5
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }

        var a6 = await _userManager.FindByEmailAsync("Mellie+Yost@ku.dk");
        if (a6 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text = "A well-fed, plump Huzza Porpoise will yield you about saying, sir?",
                    Date = DateTime.Parse("2023-08-01 13:13:32"), LikedBy = new List<string>(), Author = a6
                },
                new Cheep
                {
                    Text = "He walked slowly back the lid.", Date = DateTime.Parse("2023-08-01 13:16:23"),
                    LikedBy = new List<string>(), Author = a6
                },
                new Cheep
                {
                    Text = "But what was behind the barricade.", Date = DateTime.Parse("2023-08-01 13:17:33"),
                    LikedBy = new List<string>(), Author = a6
                },
                new Cheep
                {
                    Text = "He glared from one of the forecastle.", Date = DateTime.Parse("2023-08-01 13:14:27"),
                    LikedBy = new List<string>(), Author = a6
                },
                new Cheep
                {
                    Text =
                        "I whisked round to you, Mr. Holmes, to glance out of her which forms the great docks of Antwerp, in Napoleon''s time.",
                    Date = DateTime.Parse("2023-08-01 13:13:37"), LikedBy = new List<string>(), Author = a6
                },
                new Cheep
                {
                    Text = "Only wait a long time.", Date = DateTime.Parse("2023-08-01 13:13:48"),
                    LikedBy = new List<string>(), Author = a6
                },
                new Cheep
                {
                    Text = "Has only one in the attic save a pair of silent shoes?",
                    Date = DateTime.Parse("2023-08-01 13:16:32"), LikedBy = new List<string>(), Author = a6
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }
        
        var a7 = await _userManager.FindByEmailAsync("Malcolm-Janski@gmail.com");
        if (a7 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text = "The room into which one hopes.", Date = DateTime.Parse("2023-08-01 13:13:19"),
                    LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "The area before the fire until he broke at clapping, as at Coxon''s.",
                    Date = DateTime.Parse("2023-08-01 13:15:10"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "Besides,'' thinks I, ''it was only a simple key?",
                    Date = DateTime.Parse("2023-08-01 13:13:38"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "You notice those bright green fields and the successive monarchs of the lodge.",
                    Date = DateTime.Parse("2023-08-01 13:16:06"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "And all this while, drew nigh the wharf.", Date = DateTime.Parse("2023-08-01 13:13:41"),
                    LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "Oh, the rare virtue in his hand.", Date = DateTime.Parse("2023-08-01 13:13:33"),
                    LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "As I looked with amazement at my home.", Date = DateTime.Parse("2023-08-01 13:14:19"),
                    LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text =
                        "This fin is some connection between the finger and thumb in his straight-bodied coat, spilled tuns upon tuns of leviathan gore.",
                    Date = DateTime.Parse("2023-08-01 13:13:40"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "I would have unseated any but natural causes.",
                    Date = DateTime.Parse("2023-08-01 13:14:34"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "I see that it led me, after that point, it blisteringly passed through and through.",
                    Date = DateTime.Parse("2023-08-01 13:13:59"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "And now, having brought your case very clear.",
                    Date = DateTime.Parse("2023-08-01 13:14:45"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "It may well be a blessing in disguise.", Date = DateTime.Parse("2023-08-01 13:13:55"),
                    LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text =
                        "All the indications which might very well that he was sitting up in some honest-hearted men, restrain the gush of clotted blood.",
                    Date = DateTime.Parse("2023-08-01 13:13:35"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "The government was compelled, therefore, to use the salt, precisely who knows?",
                    Date = DateTime.Parse("2023-08-01 13:13:53"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text =
                        "And now, Mr. Barker, I seem to think the chances are that they had a faithful member--you all know how much you do then?",
                    Date = DateTime.Parse("2023-08-01 13:14:28"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "When I reached home.", Date = DateTime.Parse("2023-08-01 13:13:25"),
                    LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text =
                        "In truth, it was in possession of a striking and peculiar portion of the singular mystery which he reentered the house.",
                    Date = DateTime.Parse("2023-08-01 13:14:29"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text =
                        "The harpoon dropped from the point of real delirium, united to help us now with a supply of drink for future purposes.",
                    Date = DateTime.Parse("2023-08-01 13:16:20"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "I have been using myself up rather than in stages.",
                    Date = DateTime.Parse("2023-08-01 13:15:07"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "I am more to concentrate the snugness of his food.",
                    Date = DateTime.Parse("2023-08-01 13:14:24"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "And those sublimer towers, the White Whale is an exceptionally sensitive one.",
                    Date = DateTime.Parse("2023-08-01 13:15:32"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "You are not over yet, I say that it gives us the news.",
                    Date = DateTime.Parse("2023-08-01 13:17:23"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text =
                        "Was not that as she spoke, I saw them from learning the news of the hollow, he had taken this fragment from the back room.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text =
                        "At present I cannot spare energy and determination such as I did look up I saw a gigantic Sperm Whale is toothless.",
                    Date = DateTime.Parse("2023-08-01 13:17:29"), LikedBy = new List<string>(), Author = a7
                },
                new Cheep
                {
                    Text = "The one is very hard, and yesterday evening in an open door leading to the staple fuel.",
                    Date = DateTime.Parse("2023-08-01 13:13:44"), LikedBy = new List<string>(), Author = a7
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }

        var a8 = await _userManager.FindByEmailAsync("Octavio.Wagganer@dtu.dk");
        if (a8 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text = "Mr. Thaddeus Sholto WAS with his methods of work, Mr. Mac.",
                    Date = DateTime.Parse("2023-08-01 13:15:23"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text = "10,800 barrels of sperm; above which, in some sort of Feegee fish.",
                    Date = DateTime.Parse("2023-08-01 13:14:15"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text =
                        "At that instant that she is not the stranger whom I had lived and in the old man seems to me to wake the master.",
                    Date = DateTime.Parse("2023-08-01 13:13:53"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text = "What was the name of Murphy had given him a coat, which was stolen?",
                    Date = DateTime.Parse("2023-08-01 13:14:15"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text =
                        "I am no antiquarian, but I rolled about into every face, so regular that it has been woven round the corner.",
                    Date = DateTime.Parse("2023-08-01 13:16:25"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text = "Oh, then it is good cheer in store for you, Mr. Holmes.",
                    Date = DateTime.Parse("2023-08-01 13:13:49"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text = "Jonah enters, and would no doubt that she protested and resisted.",
                    Date = DateTime.Parse("2023-08-01 13:13:43"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text = "I didn''t know I like it.", Date = DateTime.Parse("2023-08-01 13:13:49"),
                    LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text = "And another thousand to him as possible.", Date = DateTime.Parse("2023-08-01 13:15:34"),
                    LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text = "I cannot guarantee that I was weary and haggard.",
                    Date = DateTime.Parse("2023-08-01 13:14:33"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text = "It''s mum with me when he was the smartest man in the morning.",
                    Date = DateTime.Parse("2023-08-01 13:14:09"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text =
                        "You were first a coiner and then there came a sudden turn, and I could not bring myself to find one stubborn, at the lodge proceeded.",
                    Date = DateTime.Parse("2023-08-01 13:13:46"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text = "It was a sawed-off shotgun; so he fell back dead.",
                    Date = DateTime.Parse("2023-08-01 13:17:01"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text =
                        "But Godfrey is a successful, elderly medical man, well-esteemed since those who have never met a straighter man in a dream.",
                    Date = DateTime.Parse("2023-08-01 13:15:10"), LikedBy = new List<string>(), Author = a8
                },
                new Cheep
                {
                    Text = "He''s out of Nantucket, and seeing what the sounds that were pushing us.",
                    Date = DateTime.Parse("2023-08-01 13:14:48"), LikedBy = new List<string>(), Author = a8
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }

        var a9 = await _userManager.FindByEmailAsync("Johnnie+Calixto@itu.dk");
        if (a9 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text = "Mrs. Straker tells us that his mates thanked God the direful disorders seemed waning.",
                    Date = DateTime.Parse("2023-08-01 13:14:00"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "I think, said he, Holmes, with all hands to stand on!",
                    Date = DateTime.Parse("2023-08-01 13:14:50"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "A lens and rolling this way I have written and show my agreement.",
                    Date = DateTime.Parse("2023-08-01 13:15:23"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "He is not the baronet--it is--why, it is in thee.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "But now, tell me, Stubb, do you propose to begin breaking into the matter up.",
                    Date = DateTime.Parse("2023-08-01 13:14:16"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "One is the correct solution.", Date = DateTime.Parse("2023-08-01 13:13:49"),
                    LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "It seemed as a cart, or a change in the year 1842, on the floor.",
                    Date = DateTime.Parse("2023-08-01 13:14:46"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "We would think that you should soar above it.",
                    Date = DateTime.Parse("2023-08-01 13:15:10"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "I come now to put the paper fireboard.", Date = DateTime.Parse("2023-08-01 13:16:49"),
                    LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "I confess that I am addressing and not-- No, this is life.",
                    Date = DateTime.Parse("2023-08-01 13:14:33"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "Your discretion is as much as dare to say so.",
                    Date = DateTime.Parse("2023-08-01 13:14:56"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "Hunter was seated all in this way, then.", Date = DateTime.Parse("2023-08-01 13:13:48"),
                    LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text =
                        "Now we come twenty thousand miles to the red cord which were blank and dreary, save that here before morning.",
                    Date = DateTime.Parse("2023-08-01 13:14:02"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text =
                        "As she did hear something like those of a distant triumph which had been arrested as the second window.",
                    Date = DateTime.Parse("2023-08-01 13:13:46"), LikedBy = new List<string>(), Author = a9
                },
                new Cheep
                {
                    Text = "What do you think so meanly of him?", Date = DateTime.Parse("2023-08-01 13:13:56"),
                    LikedBy = new List<string>(), Author = a9
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }

        var a10 = await _userManager.FindByEmailAsync("Jacqualine.Gilcoine@gmail.com");
        if (a10 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text =
                        "They were married in Chicago, with old Smith, and was expected aboard every day; meantime, the two went past me.",
                    Date = DateTime.Parse("2023-08-01 13:14:37"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "And then, as he listened to all that''s left o'' twenty-one people.",
                    Date = DateTime.Parse("2023-08-01 13:15:21"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "In various enchanted attitudes, like the Sperm Whale.",
                    Date = DateTime.Parse("2023-08-01 13:14:58"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "At last we came back!", Date = DateTime.Parse("2023-08-01 13:14:35"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "In the first watch, and every creditor paid in full.",
                    Date = DateTime.Parse("2023-08-01 13:16:13"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The folk on trust in me!", Date = DateTime.Parse("2023-08-01 13:15:30"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "It is a damp, drizzly November in my pocket, and switching it backward and forward with a most suspicious aspect.",
                    Date = DateTime.Parse("2023-08-01 13:13:34"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It struck cold to see you, Mr. White Mason, to our shores a number of young Alec.",
                    Date = DateTime.Parse("2023-08-01 13:13:23"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Then Sherlock looked across at the window, candle in his wilful disobedience of the road.",
                    Date = DateTime.Parse("2023-08-01 13:14:30"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I commend that fact very carefully in the afternoon.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "But this is his name! said Holmes, shaking his hand.",
                    Date = DateTime.Parse("2023-08-01 13:13:21"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "She had turned suddenly, and a lady who has satisfied himself that he has heard it.",
                    Date = DateTime.Parse("2023-08-01 13:15:51"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "With back to my friend, patience!", Date = DateTime.Parse("2023-08-01 13:16:58"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I was too crowded, even on a leaf of my adventures, and had a license for the gallows.",
                    Date = DateTime.Parse("2023-08-01 13:13:35"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "That is where the wet and shining eyes.", Date = DateTime.Parse("2023-08-01 13:13:27"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "If thou speakest thus to me that it was most piteous, that last journey.",
                    Date = DateTime.Parse("2023-08-01 13:14:34"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "He laid an envelope which was luxurious to the back part of their coming.",
                    Date = DateTime.Parse("2023-08-01 13:13:58"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Still, there are two brave fellows! Ha, ha!", Date = DateTime.Parse("2023-08-01 13:13:51"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Well, Mr. Holmes, but glanced with some confidence, that the bed beside him.",
                    Date = DateTime.Parse("2023-08-01 13:13:18"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Every word I say to them ahead, yet with their fists and sticks.",
                    Date = DateTime.Parse("2023-08-01 13:13:39"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Like household dogs they came at last come for you.",
                    Date = DateTime.Parse("2023-08-01 13:14:16"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "To him it had done a great fish to swallow up the steel head of the cetacea.",
                    Date = DateTime.Parse("2023-08-01 13:17:10"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Thence he could towards me.", Date = DateTime.Parse("2023-08-01 13:13:23"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "There was still asleep, she slipped noiselessly from the shadow lay upon the one that he was pretty clear now.",
                    Date = DateTime.Parse("2023-08-01 13:14:14"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Of course, it instantly occurred to him, whom all thy creativeness mechanical.",
                    Date = DateTime.Parse("2023-08-01 13:13:25"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "And you''ll probably find some other English whalers I know nothing of my revolver.",
                    Date = DateTime.Parse("2023-08-01 13:15:09"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "His necessities supplied, Derick departed; but he rushed at the end of the previous night.",
                    Date = DateTime.Parse("2023-08-01 13:13:49"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "We will leave the metropolis at this point of view you will do good by stealth.",
                    Date = DateTime.Parse("2023-08-01 13:13:59"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "One young fellow in much the more intimate acquaintance.",
                    Date = DateTime.Parse("2023-08-01 13:15:23"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The shores of the middle of it, and you can imagine, it was probable, from the hall.",
                    Date = DateTime.Parse("2023-08-01 13:14:10"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I thought that you are bored to death in the other.",
                    Date = DateTime.Parse("2023-08-01 13:16:13"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "His initials were L. L. How do you think this steak is rather reserved, and your Krusenstern.",
                    Date = DateTime.Parse("2023-08-01 13:15:54"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "The day was just clear of all latitudes and longitudes, that unnearable spout was cast by one Garnery.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It sometimes ends in victory.", Date = DateTime.Parse("2023-08-01 13:13:27"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "The animal has been getting worse and worse at last I have been heard, it is possible that we were indeed his.",
                    Date = DateTime.Parse("2023-08-01 13:13:17"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "And it is true, only an absent-minded one who did not come here to the back of his general shape.",
                    Date = DateTime.Parse("2023-08-01 13:15:00"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "He was reminded of a former year been seen, for example, that a few minutes to nine when I kept the appointment.",
                    Date = DateTime.Parse("2023-08-01 13:14:02"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Was the other side.", Date = DateTime.Parse("2023-08-01 13:13:19"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "We feed him once or twice, when he has amassed a lot of things which were sucking him down.",
                    Date = DateTime.Parse("2023-08-01 13:13:27"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "He leaned back in Baker Street the detective was already bowed, and he put his hand a small and great, old and feeble.",
                    Date = DateTime.Parse("2023-08-01 13:13:50"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "And can''t I speak confidentially?", Date = DateTime.Parse("2023-08-01 13:16:08"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "At the same height.", Date = DateTime.Parse("2023-08-01 13:16:43"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "I thought it only means that little hell-hound Tonga who shot the slide a little, for a kindly voice at last.",
                    Date = DateTime.Parse("2023-08-01 13:15:05"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Mr. Holmes, the specialist and Dr. Mortimer, who had watched the whole of them, in such very affluent circumstances.",
                    Date = DateTime.Parse("2023-08-01 13:14:03"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "In some of the state of things here when he liked.",
                    Date = DateTime.Parse("2023-08-01 13:14:46"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The story of Hercules and the more extraordinary did my companion''s ironical comments.",
                    Date = DateTime.Parse("2023-08-01 13:16:04"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "You don''t mean to seriously suggest that you may fancy, for yourself, and you can reach us.",
                    Date = DateTime.Parse("2023-08-01 13:17:12"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Why, Holmes, it is certainly the last man with a frank, honest face and neck, till it boil.  _Sir William Davenant.",
                    Date = DateTime.Parse("2023-08-01 13:13:40"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It has been driven to use it.", Date = DateTime.Parse("2023-08-01 13:16:07"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "His frontispiece, boats attacking Sperm Whales, though no doubt as to give them a shilling of mine.",
                    Date = DateTime.Parse("2023-08-01 13:14:22"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Quiet, sir--a long mantle down to Aldershot to supplement the efforts of the victim, and dragged from my soul.",
                    Date = DateTime.Parse("2023-08-01 13:16:47"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "And in practice on very much upon the spot, nothing could ever wake me during the investigation.",
                    Date = DateTime.Parse("2023-08-01 13:16:09"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Their secret had been at it and led him aside gently, and yet where events are now over.",
                    Date = DateTime.Parse("2023-08-01 13:13:45"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Many a time when these things are queer, if I mistake not.",
                    Date = DateTime.Parse("2023-08-01 13:15:00"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It must, then, be the heads of their cigars might have been endowed?",
                    Date = DateTime.Parse("2023-08-01 13:16:33"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "For months my life or hers, for how could you know if I moved my things to talk above a hundred yards in front of us, Mr. Holmes?",
                    Date = DateTime.Parse("2023-08-01 13:13:47"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Wire me what has been buying things for the emblematical adornment of his overcoat on a showery and miry day.",
                    Date = DateTime.Parse("2023-08-01 13:13:56"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Soon it went down, with your sail set in a gang of thieves have secured the future, but as coming from Charles Street.",
                    Date = DateTime.Parse("2023-08-01 13:13:43"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "No, it''s no go.", Date = DateTime.Parse("2023-08-01 13:13:28"),
                    LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text = "I could not tell a Moriarty when I was in its meshes.",
                    Date = DateTime.Parse("2023-08-01 13:14:03"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It was only alive to wondrous depths, where strange shapes of the mess-table.",
                    Date = DateTime.Parse("2023-08-01 13:13:19"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Where have you not?", Date = DateTime.Parse("2023-08-01 13:13:39"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "McMurdo raised his left eyebrow.", Date = DateTime.Parse("2023-08-01 13:13:21"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "We must go home with me, and she raised one hand holding a mast''s lightning-rod in the world to solve our problem.",
                    Date = DateTime.Parse("2023-08-01 13:14:56"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "You have worked with Mr. James McCarthy, going the other evening felt.",
                    Date = DateTime.Parse("2023-08-01 13:13:49"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "When I heard thy cry; it was a vacant eye.", Date = DateTime.Parse("2023-08-01 13:15:01"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The youth moved in a month later on Portsmouth jetty, with my friend!",
                    Date = DateTime.Parse("2023-08-01 13:15:13"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Now, inclusive of the spare seat of his guilt.",
                    Date = DateTime.Parse("2023-08-01 13:14:15"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Yes, for strangers to the ground.", Date = DateTime.Parse("2023-08-01 13:14:40"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Because, owing to his own marks all over with patches of rushes.",
                    Date = DateTime.Parse("2023-08-01 13:13:27"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "In the morning of the wind, some few splintered planks, of what present avail to him.",
                    Date = DateTime.Parse("2023-08-01 13:16:57"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Hang it all, all the bulwarks; the mariners did run from the absolute urgency of this young gentleman''s father.",
                    Date = DateTime.Parse("2023-08-01 13:15:18"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Even when she found herself at Baker Street by the ghosts of these had to prop him up--me and Murcher between us.",
                    Date = DateTime.Parse("2023-08-01 13:14:33"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I had not taken things for children, you perceive.",
                    Date = DateTime.Parse("2023-08-01 13:14:53"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The porter had to be murdered.", Date = DateTime.Parse("2023-08-01 13:13:34"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "She saw Mr. Barker, I think I will recapitulate the facts before I am in mine, said he.",
                    Date = DateTime.Parse("2023-08-01 13:13:17"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Starbuck now is what we hear the worst.", Date = DateTime.Parse("2023-08-01 13:17:39"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Why, do ye yet again the little lower down was a poor creature if I neglected it.",
                    Date = DateTime.Parse("2023-08-01 13:14:50"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "As we approached it I heard some sounds downstairs.",
                    Date = DateTime.Parse("2023-08-01 13:13:45"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "The policeman and of the opinion that it is by going a very rich as well that he was right in on the bicycle.",
                    Date = DateTime.Parse("2023-08-01 13:15:48"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "How did you mean that it was better surely to face with a West-End practice.",
                    Date = DateTime.Parse("2023-08-01 13:13:17"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "You do what I was well that we went to the lawn.",
                    Date = DateTime.Parse("2023-08-01 13:13:40"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "I knew by experience that railway cases were scanty and the earth, accompanying Old Ahab in all the same.",
                    Date = DateTime.Parse("2023-08-01 13:13:52"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "But this time three years, but I never spent money better.",
                    Date = DateTime.Parse("2023-08-01 13:14:13"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Seat thyself sultanically among the nations in His own chosen people.",
                    Date = DateTime.Parse("2023-08-01 13:14:12"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Almost any one murder a husband, are they lying, and what are you acting, may I ask?",
                    Date = DateTime.Parse("2023-08-01 13:15:50"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "One is that to be a marriage with Miss Violet Smith did indeed get a broom and sweep down the stairs.",
                    Date = DateTime.Parse("2023-08-01 13:13:57"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Go to the main-top of his eyes that it came about.",
                    Date = DateTime.Parse("2023-08-01 13:13:28"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I''d never rest until I had thought.", Date = DateTime.Parse("2023-08-01 13:14:11"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It was empty on account of what she was saying to me with mischievous eyes.",
                    Date = DateTime.Parse("2023-08-01 13:16:31"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The selection of our finding something out.", Date = DateTime.Parse("2023-08-01 13:13:53"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It is he, then?", Date = DateTime.Parse("2023-08-01 13:13:50"),
                    LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text = "I wrote it rather fine, said Holmes, imperturbably.",
                    Date = DateTime.Parse("2023-08-01 13:16:35"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "You found out where my pipe when I explain, said he.",
                    Date = DateTime.Parse("2023-08-01 13:13:39"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I think of the furnace throughout the whole scene lay before me.",
                    Date = DateTime.Parse("2023-08-01 13:13:19"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "He staggered back with his landlady.", Date = DateTime.Parse("2023-08-01 13:15:16"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Collar and shirt bore the letters, of course.",
                    Date = DateTime.Parse("2023-08-01 13:15:56"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Someone seems to have continually had an example of the room, the harpooneer class of work to recover this immensely important paper.",
                    Date = DateTime.Parse("2023-08-01 13:14:53"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Why didn''t you tell me that it was from the boats, steadily pulling, or sailing, or paddling after the second was criticism.",
                    Date = DateTime.Parse("2023-08-01 13:14:19"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Then we lost them for the people at the back door, into a small paper packet.",
                    Date = DateTime.Parse("2023-08-01 13:14:09"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Mr. Stubb, said Ahab, that thou wouldst wad me that it is not mad.",
                    Date = DateTime.Parse("2023-08-01 13:16:12"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "I understood to be saying to my friend''s arm in frantic gestures, and hurling forth prophecies of speedy doom to the study.",
                    Date = DateTime.Parse("2023-08-01 13:13:41"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "In the Italian Quarter with you in ten minutes.",
                    Date = DateTime.Parse("2023-08-01 13:15:05"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "My friend insisted upon her just now.", Date = DateTime.Parse("2023-08-01 13:14:35"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "If it were suicide, then we must let me speak, said the voice, are you ramming home a cartridge there? Avast!",
                    Date = DateTime.Parse("2023-08-01 13:14:59"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Watson would tell him in the endless procession of the weather, in which, as an anchor in Blanket Bay.",
                    Date = DateTime.Parse("2023-08-01 13:13:40"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "He is not my commander''s vengeance.", Date = DateTime.Parse("2023-08-01 13:14:36"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The best defence that I am sure that I must be more convenient for all in at all.",
                    Date = DateTime.Parse("2023-08-01 13:13:18"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "I wonder if he''d give a very shiny top hat and my outstretched hand and countless subtleties, to which it contains.",
                    Date = DateTime.Parse("2023-08-01 13:17:34"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Then a long, heather-tufted curve, and we can get rid of it.",
                    Date = DateTime.Parse("2023-08-01 13:13:33"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Think of that, ye lawyers!", Date = DateTime.Parse("2023-08-01 13:14:57"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "My friend rubbed his long, thin finger was pointing up to a litre of water.",
                    Date = DateTime.Parse("2023-08-01 13:17:16"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Comparing the humped herds of wild wood lands.",
                    Date = DateTime.Parse("2023-08-01 13:13:27"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Is it not for attempted murder.", Date = DateTime.Parse("2023-08-01 13:13:29"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "What is it, too, that under the door.", Date = DateTime.Parse("2023-08-01 13:15:10"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Nothing, Sir; but I have a little pomp and ceremony now.",
                    Date = DateTime.Parse("2023-08-01 13:14:48"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "In the instance where three years I have just raised from a badly fitting cartridge happens to have a few days.",
                    Date = DateTime.Parse("2023-08-01 13:15:45"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "As you look at it once; why, the end of the human skull, beheld in the small parlour of the events at first we drew entirely blank.",
                    Date = DateTime.Parse("2023-08-01 13:13:27"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "It seems dreadful to listen to another thread which I happened to glance out of the past to have read all this.",
                    Date = DateTime.Parse("2023-08-01 13:13:54"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It is known of the photograph to his secret judges.",
                    Date = DateTime.Parse("2023-08-01 13:13:35"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "What do you make him let go his hold.", Date = DateTime.Parse("2023-08-01 13:13:23"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Thank you, I think the worse for a little.", Date = DateTime.Parse("2023-08-01 13:14:13"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It seemed as if he were stealing upon you so.",
                    Date = DateTime.Parse("2023-08-01 13:14:14"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Spurn the idol up very carefully to your house.",
                    Date = DateTime.Parse("2023-08-01 13:14:08"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "If you examine this scrap with attention to the bottom.",
                    Date = DateTime.Parse("2023-08-01 13:14:12"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I shouldn''t care to try him too deep for words.",
                    Date = DateTime.Parse("2023-08-01 13:13:38"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "You will remember, Lestrade, the sensation grew less keen as on the white whale principal, I will make a world, and then comes the spring!",
                    Date = DateTime.Parse("2023-08-01 13:13:53"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Exactly, said I, and had no part in it, sir.", Date = DateTime.Parse("2023-08-01 13:15:34"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Those buckskin legs and tingles at the same height.",
                    Date = DateTime.Parse("2023-08-01 13:14:19"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "You have probably never be seen.", Date = DateTime.Parse("2023-08-01 13:15:52"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Even after I had always been a distinct proof of it.",
                    Date = DateTime.Parse("2023-08-01 13:14:33"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "There was a middle-sized, strongly built figure as he was in this state of depression.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "My fears were set motionless with utter terror.",
                    Date = DateTime.Parse("2023-08-01 13:13:27"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "_Sure_, ye''ve been to Devonshire.", Date = DateTime.Parse("2023-08-01 13:14:48"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "He seized his outstretched hand.", Date = DateTime.Parse("2023-08-01 13:14:29"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Upon making known our desires for a very cheerful place, said Sir Henry Baskerville.",
                    Date = DateTime.Parse("2023-08-01 13:13:16"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "In either case the conspirators would have been whispered before.",
                    Date = DateTime.Parse("2023-08-01 13:13:25"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "No, he cared nothing for the set, cruel face of the village, perhaps, I suggested.",
                    Date = DateTime.Parse("2023-08-01 13:13:51"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "When you have said anything to stop his confidences.",
                    Date = DateTime.Parse("2023-08-01 13:13:23"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I glanced round suspiciously at the end of my harpoon-pole sticking in her.",
                    Date = DateTime.Parse("2023-08-01 13:13:35"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "But I thought so.", Date = DateTime.Parse("2023-08-01 13:15:02"),
                    LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text = "Then, this same Monday, very shortly to do.", Date = DateTime.Parse("2023-08-01 13:13:34"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Give me a few moments.", Date = DateTime.Parse("2023-08-01 13:13:29"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "They had never seen that morning, was further honoured by the fugitives without their meanings.",
                    Date = DateTime.Parse("2023-08-01 13:14:37"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Standing between the burglar had dragged from my nose and chin, and a lesson against the side next the stern sprang up without a word.",
                    Date = DateTime.Parse("2023-08-01 13:13:42"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Of course, we always had a brother in this world or the other, said Morris.",
                    Date = DateTime.Parse("2023-08-01 13:16:11"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Why not here, as well known in surgery.", Date = DateTime.Parse("2023-08-01 13:13:17"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "This ignorant, unconscious fearlessness of speech leaves a conviction of sincerity which a man of the book.",
                    Date = DateTime.Parse("2023-08-01 13:15:50"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "On the other side.", Date = DateTime.Parse("2023-08-01 13:13:31"),
                    LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text = "The message was as sensitive to flattery on the straight, said the voice.",
                    Date = DateTime.Parse("2023-08-01 13:14:17"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Within a week to do us all about it.", Date = DateTime.Parse("2023-08-01 13:15:17"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Holmes and I let my man knew he was a sturdy, middle-sized fellow, some thirty degrees of vision must involve them.",
                    Date = DateTime.Parse("2023-08-01 13:15:39"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "So, by the nape of his teeth; meanwhile repeating a string of abuse by a helping heave from the fiery hunt?",
                    Date = DateTime.Parse("2023-08-01 13:13:43"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The agent may be legible even when he is lodging at Hobson''s Patch.",
                    Date = DateTime.Parse("2023-08-01 13:13:36"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "But there were none.", Date = DateTime.Parse("2023-08-01 13:16:31"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I sat down at the moor-gate where he was.", Date = DateTime.Parse("2023-08-01 13:16:41"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "What a splendid night it is furnished with all their habits and cared little for evermore, the poor and to come in like that.",
                    Date = DateTime.Parse("2023-08-01 13:15:50"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I''ll swear it on the angle of the dead man.", Date = DateTime.Parse("2023-08-01 13:14:53"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "These submerged side blows are so shut up, belted about, every way were the principal members of his repeated visits?",
                    Date = DateTime.Parse("2023-08-01 13:14:16"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Delight is to work at your register? said Holmes.",
                    Date = DateTime.Parse("2023-08-01 13:13:28"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It puts him in Baker Street.", Date = DateTime.Parse("2023-08-01 13:14:29"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "No small number of days and such evidence.", Date = DateTime.Parse("2023-08-01 13:15:37"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "He had signed it in me an exercise in trigonometry, it always took the matter out.",
                    Date = DateTime.Parse("2023-08-01 13:14:07"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "We were engaged in reading pamphlets.", Date = DateTime.Parse("2023-08-01 13:14:38"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Never have I ever said or did.", Date = DateTime.Parse("2023-08-01 13:15:25"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Horrified by what he was now in that room.", Date = DateTime.Parse("2023-08-01 13:15:00"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Now, amid the cloud-scud.", Date = DateTime.Parse("2023-08-01 13:16:30"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Here, boy; Ahab''s cabin shall be happy until I knew.",
                    Date = DateTime.Parse("2023-08-01 13:13:34"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The German lay upon my face, opened a barred tail.",
                    Date = DateTime.Parse("2023-08-01 13:15:06"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "But heigh-ho! there are no side road for a good light from his Indian voyage.",
                    Date = DateTime.Parse("2023-08-01 13:14:00"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It was locked, but the rest with Colonel Ross.",
                    Date = DateTime.Parse("2023-08-01 13:15:33"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "An examination of the house, when a fourth keel, coming from that of my leaving it.",
                    Date = DateTime.Parse("2023-08-01 13:14:31"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "And Stapleton, where is the good work in repairing them.",
                    Date = DateTime.Parse("2023-08-01 13:13:40"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "She is to keep your confession, and if you describe Mr. Sherlock Holmes took a bottle of spirits standing in my breast.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I was particularly agitated.", Date = DateTime.Parse("2023-08-01 13:17:14"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Shall we argue about it which was naturally annoyed at not having the least promising commencement.",
                    Date = DateTime.Parse("2023-08-01 13:14:10"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I have described, we were all upon technical subjects.",
                    Date = DateTime.Parse("2023-08-01 13:16:39"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Then these are about two hundred and seventy-seventh!",
                    Date = DateTime.Parse("2023-08-01 13:14:56"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Such is the one; aye, man, it is called; this hooking up by a stealthy step passing my room.",
                    Date = DateTime.Parse("2023-08-01 13:13:35"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Any one of them.", Date = DateTime.Parse("2023-08-01 13:13:18"),
                    LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text = "And your name need not be darted at the word with you, led you safe to our needs.",
                    Date = DateTime.Parse("2023-08-01 13:15:59"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It was an upright beam, which had a remarkable degree, the power of stimulating it.",
                    Date = DateTime.Parse("2023-08-01 13:16:27"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "You appear, however, to prove it.", Date = DateTime.Parse("2023-08-01 13:15:39"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "So close did the whetstone which the schoolmaster whale betakes himself in his blubber is.",
                    Date = DateTime.Parse("2023-08-01 13:13:58"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Well, Mr. Holmes, have you got in.", Date = DateTime.Parse("2023-08-01 13:13:44"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Together we made our way to the ground.", Date = DateTime.Parse("2023-08-01 13:13:29"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Now, of course, I did was to use their sea bannisters.",
                    Date = DateTime.Parse("2023-08-01 13:14:35"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Until then I thought it was my companion''s quiet and didactic manner.",
                    Date = DateTime.Parse("2023-08-01 13:17:10"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Besides, if I remember right.", Date = DateTime.Parse("2023-08-01 13:14:12"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "They''ve got her, that they seemed abating their speed; gradually the ship must carry its cooper.",
                    Date = DateTime.Parse("2023-08-01 13:13:41"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "But there is any inference which is beyond the morass between us until this accursed affair began.",
                    Date = DateTime.Parse("2023-08-01 13:13:18"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "He then turned to run.", Date = DateTime.Parse("2023-08-01 13:15:04"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Starbuck''s body this night to him.", Date = DateTime.Parse("2023-08-01 13:13:40"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "As marching armies approaching an unfriendly defile in which to the far rush of the telegram.",
                    Date = DateTime.Parse("2023-08-01 13:14:44"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Yet so vast a being than the main road if a certain juncture of this poor fellow to my ears, clear, resonant, and unmistakable.",
                    Date = DateTime.Parse("2023-08-01 13:15:01"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "She stood with her indignation.", Date = DateTime.Parse("2023-08-01 13:14:58"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I did was to no one.", Date = DateTime.Parse("2023-08-01 13:13:21"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "He swaggered up a curtain, there stepped the man who called himself Stapleton was talking all the five dried pips.",
                    Date = DateTime.Parse("2023-08-01 13:14:42"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "It is a sight which met us by appointment outside the town, and that would whip electro-telegraphs.",
                    Date = DateTime.Parse("2023-08-01 13:16:13"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Yet in some inexplicable way to solve the mystery?",
                    Date = DateTime.Parse("2023-08-01 13:13:27"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "The worst man in the dry land;'' when the watches of the facts which are really islands cut off behind her.",
                    Date = DateTime.Parse("2023-08-01 13:14:34"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "But all these ran into the sea, as prairie cocks in the harpoon-line that he ever thought of it again after one the wiser.",
                    Date = DateTime.Parse("2023-08-01 13:14:40"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I am in the house lay before you went out a peddling, you see, I see! avast heaving there!",
                    Date = DateTime.Parse("2023-08-01 13:15:08"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "You don''t mean to the young and rich, and of the panels of the sun full upon old Ahab.",
                    Date = DateTime.Parse("2023-08-01 13:14:04"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "As to Miss Violet Smith.", Date = DateTime.Parse("2023-08-01 13:15:21"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "That must have come to you.", Date = DateTime.Parse("2023-08-01 13:17:23"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "On the third night after night, till he couldn''t drop from the house.",
                    Date = DateTime.Parse("2023-08-01 13:13:23"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I left the room.", Date = DateTime.Parse("2023-08-01 13:13:19"),
                    LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text =
                        "The train pulled up at his bereavement; but his eyes riveted upon that heart for ever; who ever conquered it?",
                    Date = DateTime.Parse("2023-08-01 13:17:36"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Your reverence need not warn you of the crime, and that the rascal had copied the paper down upon me.",
                    Date = DateTime.Parse("2023-08-01 13:13:49"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "I do not think he is my friend his only daughter, aged twenty, and two bold, dark eyes upon this absence of motive.",
                    Date = DateTime.Parse("2023-08-01 13:13:58"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Have I told my wife and my love went out into the mizentop for a moment?...",
                    Date = DateTime.Parse("2023-08-01 13:13:42"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Not so the whale''s slippery back, the after-oar reciprocating by rapping his knees drawn up, a woman''s dress.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The idea of what you say just now of observation and for a match?",
                    Date = DateTime.Parse("2023-08-01 13:13:25"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Pray sit down on the envelope, and it seemed the material for these gypsies.",
                    Date = DateTime.Parse("2023-08-01 13:14:35"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "What we did not withdraw it.", Date = DateTime.Parse("2023-08-01 13:13:21"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Riotous and disordered as the criminal who it may, answered the summons, a large, brass-bound safe.",
                    Date = DateTime.Parse("2023-08-01 13:13:28"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "You said you had made an utter island of Mauritius.",
                    Date = DateTime.Parse("2023-08-01 13:13:39"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Both are massive enough in his eyes.", Date = DateTime.Parse("2023-08-01 13:14:19"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "There, then, he sat, his very lips at the rudder, one to the door, and he took the New Forest or the other, said Morris.",
                    Date = DateTime.Parse("2023-08-01 13:13:25"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "His initials were L. L. Have you formed any explanation of Barrymore''s movements might be, it was stated that any one else saw it?",
                    Date = DateTime.Parse("2023-08-01 13:14:10"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "But I had examined everything with the soft wax.",
                    Date = DateTime.Parse("2023-08-01 13:14:43"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "While yet a slip would mean a confession of guilt.",
                    Date = DateTime.Parse("2023-08-01 13:14:22"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I picked them as they are so hopelessly lost to all his affairs.",
                    Date = DateTime.Parse("2023-08-01 13:14:19"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "What''s this? he asked.", Date = DateTime.Parse("2023-08-01 13:16:44"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The young hunter''s dark face grew tense with emotion and anticipation.",
                    Date = DateTime.Parse("2023-08-01 13:16:23"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "But if I can be perfectly frank.", Date = DateTime.Parse("2023-08-01 13:13:18"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "How cheerily, how hilariously, O my Captain, would we bowl on our starboard hand till we can drive where I stood firm.",
                    Date = DateTime.Parse("2023-08-01 13:13:56"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "As far as this conductor must descend to considerable accuracy by experts.",
                    Date = DateTime.Parse("2023-08-01 13:16:28"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It was on that important rope, he applied it with my employer.",
                    Date = DateTime.Parse("2023-08-01 13:13:37"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "No one saw it that this same humpbacked whale and the frail gunwales bent in, collapsed, and the disappearance of Silver Blaze?",
                    Date = DateTime.Parse("2023-08-01 13:14:23"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "God help me, Mr. Holmes, I can help you much.",
                    Date = DateTime.Parse("2023-08-01 13:13:17"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "These were all ready to dare anything rather than in life.",
                    Date = DateTime.Parse("2023-08-01 13:13:55"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Then we shall take them under.", Date = DateTime.Parse("2023-08-01 13:13:20"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "For years past the cottage, hurried the inmates out at a quarter of the largest of the second night he was an admirable screen.",
                    Date = DateTime.Parse("2023-08-01 13:14:24"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Yes, I have tried it, but I described to him who, in this room, and he drank it down.",
                    Date = DateTime.Parse("2023-08-01 13:14:36"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "You can''t tell what it was suggested by Sir Charles''s butler, is a hard blow for it, said Barker.",
                    Date = DateTime.Parse("2023-08-01 13:13:22"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The student had drawn the body of it was I?", Date = DateTime.Parse("2023-08-01 13:16:53"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "What a relief it was the place examined.", Date = DateTime.Parse("2023-08-01 13:16:30"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The stout gentleman with a little more reasonable.",
                    Date = DateTime.Parse("2023-08-01 13:17:17"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Once, I remember, to be a rock, but it is this Barrymore, anyhow?",
                    Date = DateTime.Parse("2023-08-01 13:17:26"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Even in his palm.", Date = DateTime.Parse("2023-08-01 13:14:00"),
                    LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text = "Well, we may take a premature lunch here, or how hope to read through them, went to bed.",
                    Date = DateTime.Parse("2023-08-01 13:14:01"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Set the pips upon the riveted gold coin there, he hasn''t a gill in his chair was mine.",
                    Date = DateTime.Parse("2023-08-01 13:15:56"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Already several fatalities had attended us, we can get a gleam of something unusual for your private eye.",
                    Date = DateTime.Parse("2023-08-01 13:13:26"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "This bureau consists of a great caravan upon its return journey.",
                    Date = DateTime.Parse("2023-08-01 13:15:32"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "No man burdens his mind in the morning.", Date = DateTime.Parse("2023-08-01 13:13:53"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Would you kindly step over to him.", Date = DateTime.Parse("2023-08-01 13:13:46"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "When he had been so anxious to hurry my work, for on the forecastle, till Ahab, troubledly pacing the deck, and we walked along the road.",
                    Date = DateTime.Parse("2023-08-01 13:13:26"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "You know my name?", Date = DateTime.Parse("2023-08-01 13:14:35"),
                    LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text =
                        "There was no money in my hand on the way, you plainly saw that he was in store for him, I should thoroughly understand it.",
                    Date = DateTime.Parse("2023-08-01 13:13:31"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Prick ears, and as my business affairs went wrong.",
                    Date = DateTime.Parse("2023-08-01 13:16:03"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "An opera hat was pushed to the French call him _Requin_.",
                    Date = DateTime.Parse("2023-08-01 13:14:18"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Well, said Lestrade, producing a small window between us.",
                    Date = DateTime.Parse("2023-08-01 13:15:22"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It was very sure would be seen.", Date = DateTime.Parse("2023-08-01 13:15:20"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I rose somewhat earlier than we may discriminate.",
                    Date = DateTime.Parse("2023-08-01 13:13:36"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Will you come to his feet on the trail so far convinced us that we had just discussed with him.",
                    Date = DateTime.Parse("2023-08-01 13:14:02"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Fournaye, who is an absolute darkness as I came back in his power.",
                    Date = DateTime.Parse("2023-08-01 13:14:21"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "What with the Freemen, the blacker were the principal person concerned is beyond our little ambush here.",
                    Date = DateTime.Parse("2023-08-01 13:14:09"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "When I approached, it vanished with a full, black beard.",
                    Date = DateTime.Parse("2023-08-01 13:13:18"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Now and then went downstairs, said a few drops of each with his life.",
                    Date = DateTime.Parse("2023-08-01 13:13:36"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "A peddler of heads too perhaps the heads of the vanishing cloth.",
                    Date = DateTime.Parse("2023-08-01 13:13:17"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Of course, when I would not call at four o''clock when we went down the passage, through the air, and making our way to Geneva.",
                    Date = DateTime.Parse("2023-08-01 13:14:18"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Unfortunately, the path and stooped behind the main-mast.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The table was littered.", Date = DateTime.Parse("2023-08-01 13:15:48"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It was our wretched captive, shivering and half shout.",
                    Date = DateTime.Parse("2023-08-01 13:15:03"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I watched his son be a castor of state.", Date = DateTime.Parse("2023-08-01 13:13:57"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Gone, too, was streaked with grime, and at the railway carriage, a capacity for self-restraint.",
                    Date = DateTime.Parse("2023-08-01 13:13:59"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "He found out that there can be ascertained in several companies and went up the level of the inverted compasses.",
                    Date = DateTime.Parse("2023-08-01 13:15:26"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It is only deterred from entering by the difficulty which faced them.",
                    Date = DateTime.Parse("2023-08-01 13:14:39"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Among our comrades of the carriage rattled past.",
                    Date = DateTime.Parse("2023-08-01 13:15:52"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "As for myself, but I had seen a man has got, and arrest him on eclipses.",
                    Date = DateTime.Parse("2023-08-01 13:13:31"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "And yet I dare say eh?", Date = DateTime.Parse("2023-08-01 13:15:31"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "But we had not been moved for many months or weeks as the fog-bank flowed onward we fell in love with her?",
                    Date = DateTime.Parse("2023-08-01 13:13:53"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Well, Watson, what do you think that your bag of blasting powder at the Hall.",
                    Date = DateTime.Parse("2023-08-01 13:14:54"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "There had been shot or interested in South America, establish his identity before the carriage rattled away.",
                    Date = DateTime.Parse("2023-08-01 13:13:49"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "And as if to yield to that clue.", Date = DateTime.Parse("2023-08-01 13:15:04"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "The more terrible, therefore, seemed that some of his feet.",
                    Date = DateTime.Parse("2023-08-01 13:13:35"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It was soothing to catch him and put away.", Date = DateTime.Parse("2023-08-01 13:16:38"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "He said nothing to prevent me from between swollen and puffy pouches.",
                    Date = DateTime.Parse("2023-08-01 13:14:29"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It is asking much of it in the world.", Date = DateTime.Parse("2023-08-01 13:16:50"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Have you no more.", Date = DateTime.Parse("2023-08-01 13:15:03"),
                    LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text = "She glanced at me.", Date = DateTime.Parse("2023-08-01 13:13:31"),
                    LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text = "Holmes examined it with admirable good-humour.",
                    Date = DateTime.Parse("2023-08-01 13:13:57"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I almost thought that Poncho would have warned our very formidable person.",
                    Date = DateTime.Parse("2023-08-01 13:15:22"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Well, good-bye, and let them know that her injuries were serious, but not necessarily fatal.",
                    Date = DateTime.Parse("2023-08-01 13:15:07"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Agents were suspected or even than your enemies from America.",
                    Date = DateTime.Parse("2023-08-01 13:13:49"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "At eleven there was movement in the teeth that he was in its niches.",
                    Date = DateTime.Parse("2023-08-01 13:15:47"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Those buckskin legs and fair ramping.", Date = DateTime.Parse("2023-08-01 13:14:00"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "You must put this horseshoe into my little woman, I would not have the warrant and can hold him back.",
                    Date = DateTime.Parse("2023-08-01 13:13:39"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It had been played by Mr. Barker?", Date = DateTime.Parse("2023-08-01 13:15:23"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Seems to me of Darmonodes'' elephant that so caused him to the kitchen door.",
                    Date = DateTime.Parse("2023-08-01 13:17:29"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Did this mad wife of either whale''s jaw, if you try to force this also.",
                    Date = DateTime.Parse("2023-08-01 13:14:45"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Then he certainly deserved it if any other person don''t believe it, but I confess that somehow anomalously did its duty.",
                    Date = DateTime.Parse("2023-08-01 13:14:21"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Have just been engaged by McGinty, they were regarded in the dining-room yet?",
                    Date = DateTime.Parse("2023-08-01 13:13:21"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "It was evident that the spirit of godly gamesomeness is not the wolf; Mr. Gregson of Scotland Yard, Mr. Holmes.",
                    Date = DateTime.Parse("2023-08-01 13:15:30"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "It was not yet finished his lunch, and certainly the records which he is well known to me to a finish.",
                    Date = DateTime.Parse("2023-08-01 13:15:28"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Still, in that wicker chair; it was he that I thought you might find herself in hot latitudes.",
                    Date = DateTime.Parse("2023-08-01 13:13:38"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "There we have to bustle about hither and thither before us; at a glance that something was moving in their place.",
                    Date = DateTime.Parse("2023-08-01 13:17:25"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Clap eye on the edge of the profession which has so shaken me most dreadfully.",
                    Date = DateTime.Parse("2023-08-01 13:14:07"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "People in Nantucket are carried about with him and tore him away from off his face.",
                    Date = DateTime.Parse("2023-08-01 13:13:28"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Well, not to spoil the hilarity of his own proper atmosphere.",
                    Date = DateTime.Parse("2023-08-01 13:15:17"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "What he sought was the landlord, placing the title Lord of the year!",
                    Date = DateTime.Parse("2023-08-01 13:13:33"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "You remember, Watson, that my sympathies in this room, absorbed in his breath and stood, livid and trembling, before us.",
                    Date = DateTime.Parse("2023-08-01 13:14:46"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "They are from a clump of buildings here is another man then?",
                    Date = DateTime.Parse("2023-08-01 13:13:19"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Well, well, you need not add imagination to your collection, and I to do?",
                    Date = DateTime.Parse("2023-08-01 13:13:42"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It is the reappearance of that sagacious saying in the whole truth.",
                    Date = DateTime.Parse("2023-08-01 13:13:23"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Oh, hush, Mr. McMurdo, may I forgive myself, but I thought you were going to be done.",
                    Date = DateTime.Parse("2023-08-01 13:14:31"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "He makes one in the air.", Date = DateTime.Parse("2023-08-01 13:16:03"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It was as close packed in its own controls it.",
                    Date = DateTime.Parse("2023-08-01 13:15:36"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It went through my field-glass.", Date = DateTime.Parse("2023-08-01 13:16:02"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Penetrating further and more unfolding its noiseless measureless leaves upon this gang.",
                    Date = DateTime.Parse("2023-08-01 13:17:26"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Our route was certainly no sane man would destroy us all.",
                    Date = DateTime.Parse("2023-08-01 13:13:18"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Only this: go down to Norfolk a wedded couple.",
                    Date = DateTime.Parse("2023-08-01 13:15:08"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "For two hours, and I know the incredible bulk he assigns it.",
                    Date = DateTime.Parse("2023-08-01 13:16:00"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Aye, he was still rubbing the towsy golden curls which covered the back part of the hut, and a dozen times before.",
                    Date = DateTime.Parse("2023-08-01 13:13:54"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "There he stands; two bones stuck into a study of the hut, walking as warily as Stapleton would have been aroused.",
                    Date = DateTime.Parse("2023-08-01 13:13:20"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Lestrade went after his wants.", Date = DateTime.Parse("2023-08-01 13:13:35"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Watson, I should certainly make every inquiry which can now be narrated brought his knife through the amazing thing happened.",
                    Date = DateTime.Parse("2023-08-01 13:14:23"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "It was he at last climbs up the paper is Sir Charles''s death, we had no very unusual affair.",
                    Date = DateTime.Parse("2023-08-01 13:14:11"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "All around farms were apportioned and allotted in proportion to the side; and then came back.",
                    Date = DateTime.Parse("2023-08-01 13:14:23"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "We are all really necessary for me to say that I failed to throw some light upon the Indian; so that I had his description of you.",
                    Date = DateTime.Parse("2023-08-01 13:13:37"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "He was a small sliding shutter, and, plunging in his chair and began once more at his skirts.",
                    Date = DateTime.Parse("2023-08-01 13:14:24"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Whether that mattress was stuffed in the bloodstained annals of the harem.",
                    Date = DateTime.Parse("2023-08-01 13:13:41"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "And has he done, then?", Date = DateTime.Parse("2023-08-01 13:13:21"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "How ever did you not, for the first dead American whale fishery, of which had just one way for the attempt.",
                    Date = DateTime.Parse("2023-08-01 13:13:29"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Now, while all these varied cases, however, I found him out.",
                    Date = DateTime.Parse("2023-08-01 13:15:17"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "On, on we flew; and our attention to this back-bone, for something or somebody upon the Temple, no Whale can pass it every consideration.",
                    Date = DateTime.Parse("2023-08-01 13:15:57"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "To me at all.", Date = DateTime.Parse("2023-08-01 13:13:58"), LikedBy = new List<string>(),
                    Author = a10
                },
                new Cheep
                {
                    Text = "As the gleam of light in his quick, firm tread.",
                    Date = DateTime.Parse("2023-08-01 13:13:26"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "There was no time; but I am myself an infinity of trouble.",
                    Date = DateTime.Parse("2023-08-01 13:14:26"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "I saved enough to do what in the clear moonlight, or starlight, as the needle-sleet of the inflexible jaw.",
                    Date = DateTime.Parse("2023-08-01 13:13:22"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Hullo, what is the question.", Date = DateTime.Parse("2023-08-01 13:14:13"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "But we won''t talk of my brown ones, and so dead to windward, then; the better classes of society.",
                    Date = DateTime.Parse("2023-08-01 13:13:26"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "The worst man in that gale, the but half fancy being committed this crime, what possible reason for not knowing what it was he.",
                    Date = DateTime.Parse("2023-08-01 13:15:23"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "They had a line of thought, resented anything which could give it.",
                    Date = DateTime.Parse("2023-08-01 13:14:24"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "A man entered and took up the whole universe, not excluding its suburbs.",
                    Date = DateTime.Parse("2023-08-01 13:14:46"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Presently, as we know, he wrote the history of the front pew at the next day''s sunshine dried upon it.",
                    Date = DateTime.Parse("2023-08-01 13:13:47"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "And when he had ever seen him.", Date = DateTime.Parse("2023-08-01 13:15:19"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Sometimes I think myself that it happened--August of that fine old Queen Anne house, which is not in my power.",
                    Date = DateTime.Parse("2023-08-01 13:17:13"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "In the dim light divers specimens of fin-backs and other nautical conveniences.",
                    Date = DateTime.Parse("2023-08-01 13:13:51"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "See here! he continued, taking a stroll along the cycloid, my soapstone for example, is there hope.",
                    Date = DateTime.Parse("2023-08-01 13:13:58"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "''Your best way is at the window.", Date = DateTime.Parse("2023-08-01 13:13:55"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Then all in high life, Watson, I should retain her secret--the more so than usual.",
                    Date = DateTime.Parse("2023-08-01 13:14:42"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "He who could tell whether, in case of razors--had been found sticking in near his light.",
                    Date = DateTime.Parse("2023-08-01 13:13:52"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "No doubt you thought arrange his affairs.", Date = DateTime.Parse("2023-08-01 13:14:29"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Holmes glanced over and almost danced with excitement and greed.",
                    Date = DateTime.Parse("2023-08-01 13:17:14"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I shall start off into the easy-chair and, sitting beside him, patted his hand in it.",
                    Date = DateTime.Parse("2023-08-01 13:15:37"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "We''d best put it on, to arrive ten to-morrow if I could not shoot him at last, with a gleam of his tail, Leviathan had run up the pathway.",
                    Date = DateTime.Parse("2023-08-01 13:13:39"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "It is all odds that he should see and understand.",
                    Date = DateTime.Parse("2023-08-01 13:15:01"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "She knocked without receiving any answer, and even solicitously cutting the lower part muffled round---- That will do.",
                    Date = DateTime.Parse("2023-08-01 13:15:39"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "More than one case our old Manxman the old hearse-driver, he must undress and get down to the Moss, the little table first.",
                    Date = DateTime.Parse("2023-08-01 13:13:39"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "I will endeavour to do with him.''", Date = DateTime.Parse("2023-08-01 13:14:36"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Morning to ye, Mr. Starbuck but it''s too springy to my knowledge of when to stop.",
                    Date = DateTime.Parse("2023-08-01 13:17:17"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Seen from the forehead seem now faded away.", Date = DateTime.Parse("2023-08-01 13:14:29"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Rain had fallen even darker over the document.",
                    Date = DateTime.Parse("2023-08-01 13:14:02"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "And why the word of honour--and I never mixed much with Morris.",
                    Date = DateTime.Parse("2023-08-01 13:16:01"), LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text = "Young man, said Holmes.", Date = DateTime.Parse("2023-08-01 13:13:27"),
                    LikedBy = new List<string>(), Author = a10
                },
                new Cheep
                {
                    Text =
                        "Of course, with a purely animal lust for the time stated I was surer than ever it occurred?",
                    Date = DateTime.Parse("2023-08-01 13:14:03"), LikedBy = new List<string>(), Author = a10
                },
            };
            _context.Cheeps.AddRange(cheeps);
        }

        var a11 = await _userManager.FindByEmailAsync("ropf@itu.dk");
        if (a11 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text = "Hello, BDSA students!", Date = DateTime.Parse("2023-08-01 12:16:48"),
                    LikedBy = new List<string>(), Author = a11
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }

        var a12 = await _userManager.FindByEmailAsync("adho@itu.dk");
        if (a12 != null)
        {
            var cheeps = new List<Cheep>()
            {
                new Cheep
                {
                    Text = "Hej, velkommen til kurset.", Date = DateTime.Parse("2023-08-01 13:08:28"),
                    LikedBy = new List<string>(), Author = a12
                }
            };
            _context.Cheeps.AddRange(cheeps);
        }

        await _context.SaveChangesAsync();
    }
}