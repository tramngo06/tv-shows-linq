using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

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
