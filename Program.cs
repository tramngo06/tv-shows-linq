using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

// name of TV show
//string show = "shows/buffy.json";
//string show = "shows/steven-universe.json";
//string show = "shows/mandalorian.json";
string show = "shows/mash.json";
//string show = "shows/stranger-things.json";

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

string text = File.ReadAllText(show);
var shows = JsonSerializer.Deserialize<List<Show>>(text, options);


shows.Select(show => show.Name)
    .Where(title => title.Contains("Pilot"))
    .ToList()
    .ForEach(Console.WriteLine);

// Q1: FINDING NUMBER OF EPS PER SEASON
var EpsSeason = shows
.GroupBy(show => show.Season)// grouping show with same number of season
.Select(group => new Season
{
    SeasonNum = group.Key,
    EpsCount = group.Count()
});
//now print the number of season out 
foreach (var print in EpsSeason)
{
    Console.WriteLine($"Season {print.SeasonNum}: {print.EpsCount} episodes");
}

//----------Q2: UNIQUE RUNTIME----------
var uniqueRuntimes = shows
.Where(show => show.Episodes != null)
.SelectMany(show => show.Episodes)// select ep from each show
.Select(episode => episode.Runtime)// now select runtime from each ep
.Distinct();

Console.WriteLine(@"The Unique Runtime is: ");
foreach (var runtime in uniqueRuntimes)
{
    Console.WriteLine(runtime);
}

//-------Q3: THE LENGHT OF LONGEST SUMMARY WORDS--------
var maxSummaryLength = shows
.Select(show => show.Summary.Split(' ').Length)
.Max();

Console.WriteLine($"The longest Summary length is: {maxSummaryLength} words");


// -----------Q4:
string interestingWord = "Adventure";

// Get total number of episodes
int totalEpisodes = shows.Sum(show => show.Episodes?.Count ?? 0);

// Get number of episodes containing the interesting word
int episodesWithInterestingWord = shows
    .SelectMany(show => show.Episodes ?? Enumerable.Empty<Episodes>())
    .Count(episode => episode.Name?.Contains(interestingWord, StringComparison.OrdinalIgnoreCase) ?? false);

// Calculate the percentage
double percentage = totalEpisodes > 0 ? (episodesWithInterestingWord / (double)totalEpisodes) * 100 : 0;

Console.WriteLine($"The word '{interestingWord}' appears in {percentage}% of episode names.");

//------------Q5: 
//let say we're interested in the word "adventure" in TV Shows called "Mash", we need to find num of season that contains at least one of that word

//sleect episode of chosen show
var mashEpisodes = shows.SingleOrDefault(show => show.Name == "M*A*S*H")?.Episodes;
if (mashEpisodes != null)
{
    // group them by season
    var seasonWithStalk = mashEpisodes
    .GroupBy(episode => episode.Season)
    .Where(seasonGroup => seasonGroup.Any(episode => episode.Name.Contains("Adventure", StringComparison.OrdinalIgnoreCase)))
    .Select(seasonGroup => seasonGroup.Key);

    int numOfSeasonWith = seasonWithStalk.Count();// return thr num of season contain word
    Console.WriteLine($"Number of season contains 'Adventure': {numOfSeasonWith}");
}
else
{
    Console.WriteLine("No show with name 'M*A*S*H' found.");
}

//---------- New episode name-------------
string newEps = "Shadows of Forgotten Lore";

// Prompt 1: Providing the list of existing episode names
var existingEps = new List<string>
{
    "Whispers of Betrayal" ,
"Dark Secrets Unveiled",
 "Intrigue in the Shadows" ,

};

// Prompt 2: Asking for a new name for an episode that doesn’t exist
string prompt = "Generate a new episode name in the style of the existing ones.";

// Generating a new episode name
string generatedEpisodeName = "Shadows of Forgotten Lore";

Dictionary<string, string> episodeSummaries = new Dictionary<string, string>
        {
            { "Whispers of Betrayal", "As tensions rise within the close-knit community, whispers of betrayal circulate through the shadows, testing loyalties and trust. Secrets long held threaten to unravel, leaving everyone on edge." },
            { "Dark Secrets Unveiled", "In this gripping episode, dark secrets that have haunted the town for generations are finally brought to light. As the truth emerges, characters must grapple with their newfound knowledge and navigate the consequences." },
            { "Intrigue in the Shadows", "The town becomes a hotbed of intrigue as mysterious occurrences cast long shadows over its streets. Suspicion and uncertainty grip the community, as they try to unravel the web of secrets and lies." }
        };

foreach (var episode in episodeSummaries)
{
    Console.WriteLine($"Episode: \"{episode.Key}\"");
    Console.WriteLine($"Summary: {episode.Value}\n");
}
