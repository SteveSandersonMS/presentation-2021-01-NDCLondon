using System;
using System.Collections.Generic;

namespace CalendarApp.Data
{
    public class SeedData
    {
        private const int FromNumDaysInPast = 30;
        private const int ToNumDaysInFuture = 365;
        private const int MinEntriesPerDay = 2;
        private const int MaxEntriesPerDay = 16;
        private const int MinHour = 9;
        private const int MaxHour = 16;

        public static int RandomSeed = 19;

        public static void Initialize(ApplicationDbContext db)
        {
            var rooms = new[] {
                new Room { Name = "Studio 1" },
                new Room { Name = "Studio 2" },
            };
            db.Rooms.AddRange(rooms);
            db.SaveChanges();

            db.Bookings.AddRange(CreateSeedData(rooms));
            db.SaveChanges();
        }

        private static IEnumerable<Booking> CreateSeedData(Room[] rooms)
        {
            var rng = new Random(RandomSeed);
            var nextBookingId = 0;

            for (
                var date = DateTime.UtcNow.Date.AddDays(-1 * FromNumDaysInPast);
                date < DateTime.UtcNow.AddDays(ToNumDaysInFuture);
                date = date.AddDays(1))
            {
                var numEntriesRange = MaxEntriesPerDay - MinEntriesPerDay;
                var numEntries = MinEntriesPerDay + Math.Floor(numEntriesRange - Math.Sqrt(rng.Next(numEntriesRange*numEntriesRange)));
                var claimedSlots = new HashSet<(Room, int)>();
                for (var i = 0; i < numEntries; i++)
                {
                    nextBookingId++;
                    
                    int hour;
                    Room room;
                    do
                    {
                        hour = rng.Next(MinHour, MaxHour + 1);
                        room = rooms[rng.Next(rooms.Length)];
                    }
                    while (claimedSlots.Contains((room, hour)));

                    claimedSlots.Add((room, hour));

                    yield return new Booking
                    {
                        Id = nextBookingId,
                        StartTime = date.AddHours(hour),
                        RoomId = room.Id,
                        Name = Names[rng.Next(Names.Length)],
                        Notes = NotesChoices[rng.Next(NotesChoices.Length)],
                    };
                }
            }
        }

        private static readonly string[] NotesChoices = new[]
        {
            "Band practice",
            "Band practice",
            "Album recording",
            "Radio interview",
            "Voiceover recording",
            "Equipment setup",
            "Birthday party",
            null,
            "Podcast recording",
            "Post production",
        };

        private static readonly string[] Names = new[]
        {
            "Cain Roman",
            "Janis Joplin",
            "Jaime Blackburn",
            "Mufutau Elliott",
            "Damian Rasmussen",
            "Dante L. Davis",
            "Carissa Gamble",
            "Kevyn J. Potter",
            "Galena Brock",
            "Brynn Gay",
            "Hedley H. Trujillo",
            "Lunea X. Ayers",
            "Kirby Berg",
            "Jimi Hendrix",
            "Leigh C. Molina",
            "Jay-Z",
            "Price A. Shannon",
            "Rae Bonner",
            "Neve Nolan",
            "AC/DC",
            "Simon and Garfunkel",
            "The Beach Boys",
            "Queen",
            "Oliver J. Pate",
            "Pink Floyd",
            "Illiana Raymond",
            "Adena O. Daniels",
            "Howlin' Wolf",
            "The Who",
            "Tarik Randolph",
            "Juliet Morrison",
            "Prince",
            "Rhonda Hubbard",
            "Odysseus Myers",
            "Bob Dylan",
            "Eminem",
            "May Cabrera",
            "Gregory Santana",
            "Signe Farmer",
            "Zoe I. Wyatt",
            "Blythe Nicholson",
            "Upton Q. Mckenzie",
            "Jerry Workman",
            "Nirvana",
            "The Four Tops",
            "Neil Young",
            "James Y. Castaneda",
            "Macy J. Ashley",
            "Kameko Y. Williamson",
            "Tina Turner",
            "Marshall Y. Daniel",
            "Amaya Hoover",
            "Blair Cruz",
            "James Brown",
            "Run-DMC",
            "Perry Short",
            "Myles Clemons",
            "Public Enemy",
            "Belle Burnett",
            "Scott Daniels",
            "Valentine Pierce",
            "Warren Collier",
            "Melyssa Montgomery",
            "Roy Orbison",
            "The Police",
            "Tobias Coleman",
            "Ashely Lowe",
            "Hamish Chan",
            "The Temptations",
            "Beastie Boys",
            "Tatyana Gould",
            "Gwendolyn Richmond",
            "Fats Domino",
            "Hayfa Odom",
            "The Rolling Stones",
            "The Everly Brothers",
            "Hank Williams",
            "Ori Sharpe",
            "Alec Sargent",
            "Xenos Barton",
            "Buddy Holly",
            "Jenette Warren",
            "Elvis Costello",
            "Sly and the Family Stone",
            "Bruce Springsteen",
            "Deirdre Miller",
            "Holmes Kemp",
            "Lana Alston",
            "Michael Jackson",
            "Aphrodite V. Carter",
            "Adara Q. Hull",
            "Burke Slater",
            "Guns n' Roses",
            "Eric Clapton",
            "Ray Charles",
            "Kim R. Schneider",
            "Odessa Espinoza",
            "Booker T. and the MGs",
            "Gram Parsons",
            "May G. Sweet",
            "Cullen X. Pate",
            "Travis J. Delaney",
            "Brady F. Dudley",
            "The Kinks",
            "Raja Nichols",
            "Kareem L. Burris",
            "Katell Erickson",
            "Cheryl I. Dillon",
            "Muddy Waters",
            "Curtis Mayfield",
            "The Allman Brothers Band",
            "Uta Douglas",
            "Frank Zappa",
            "David Bowie",
            "Jane Buckley",
            "The Doors",
            "Nash Valencia",
            "Cathleen T. Griffin",
            "Brenden Ratliff",
            "Martha Cantrell",
            "Lucian X. Mills",
            "Lucas Townsend",
            "Chava Murphy",
            "Joel Cochran",
            "The Byrds",
            "The Velvet Underground",
            "Aristotle Shelton",
            "Dr. Dre",
            "Quinn Hooper",
            "Vincent Booth",
            "Velma Rivas",
            "Chava Morrow",
            "Jessica C. Bond",
            "Joni Mitchell",
            "Kristen Cooke",
            "Bo Diddley",
            "Jackie Wilson",
            "Asher Larsen",
            "Phillip Q. Burt",
            "Nola C. Hutchinson",
            "Ainsley Barber",
            "Radiohead",
            "Georgia D. Vinson",
            "Dominic P. Cooper",
            "Idona Clarke",
            "Sheila Oconnor",
            "Travis Mejia",
            "Aristotle C. Weiss",
            "Charlotte L. Goodman",
            "Bob Marley",
            "Chuck Berry",
            "Geoffrey R. Dean",
            "U2",
            "Aimee X. Santana",
            "Kristen O. Gamble",
            "Lynyrd Skynyrd",
            "Cullen H. Waters",
            "The Shirelles",
            "Marvin Gaye",
            "Little Richard",
            "Mufutau Q. Kaufman",
            "Wayne Bartlett",
            "Jocelyn B. Figueroa",
            "Hamilton Austin",
            "Black Sabbath",
            "Cassady Craig",
            "Aerosmith",
            "Nine Inch Nails",
            "Jessamine Daniels",
            "Germane Rice",
            "Smokey Robinson and the Miracles",
            "John Lennon",
            "Maite A. Randall",
            "Latifah Meyers",
            "Levi Moss",
            "Castor Rivera",
            "Jamal Maynard",
            "Zeph B. Hyde",
            "Roary Marshall",
            "Maxwell Pearson",
            "Laith Foley",
            "Adam Rose",
            "Cyrus Melendez",
            "Yeo Hatfield",
            "The Sex Pistols",
            "The Stooges",
            "Sam Cooke",
            "Grateful Dead",
            "Quinn Lane",
            "Denise U. Williamson",
            "Metallica",
            "Christine Petersen",
            "Eagles",
            "Jayme Dodson",
            "Creedence Clearwater Revival",
            "Debra Hayden",
            "Alice Oneal",
            "The Beatles",
            "Vielka Maynard",
            "James Taylor",
            "Otis Redding",
            "Breanna F. Harrington",
            "Dorothy Wall",
            "Halla Gentry",
            "Jerry Lee Lewis",
            "Kiona Cummings",
            "R.E.M.",
            "Kirsten Terrell",
            "Tom Petty",
            "Madonna",
            "The Clash",
            "Patti Smith",
            "The Drifters",
            "Astra Carrillo",
            "Aubrey Mccarty",
            "Cara Griffin",
            "Cream",
            "Parliament and Funkadelic",
            "Stevie Wonder",
            "Logan Gaines",
            "Lunea Curry",
            "Phil Spector",
            "Talking Heads",
            "Arsenio Bright",
            "Johnny Cash",
            "Diana Ross and the Supremes",
            "The Ramones",
            "Ayanna Branch",
            "Elton John",
            "Van Morrison",
            "Jana O. Dalton",
            "Isaac Hampton",
            "The Band",
            "Skyler Lynn",
            "Raja Randall",
            "Shaine Buchanan",
            "Aretha Franklin",
            "Led Zeppelin",
            "Idona V. Cotton",
            "Thomas Albert",
            "Carlos Santana",
            "Carl Perkins",
            "Elvis Presley",
            "Hiram C. Huber",
            "The Yardbirds",
            "Judah Mack",
            "Alma Carlson",
            "Lucy Hendrix",
            "Tupac Shakur",
            "Kelly Terrell",
            "Catherine Case",
            "Al Green",
            "Salvador Benjamin",
            "Ebony Simon",
            "Jackson Boone"
        };
    }
}
