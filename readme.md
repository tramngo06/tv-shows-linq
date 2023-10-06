Shows
===
TV show JSON data is from tvmaze: https://api.tvmaze.com/shows/1615/episodes

Search https://www.tvmaze.com/ for your favorite show!

For example, if you search for your favorite show, like "Friends", you see this for Friends:

https://www.tvmaze.com/shows/431/friends

This means that "Friends" has show ID 431.

To pull all of the episodes, you would then download the JSON file at this URL:

https://api.tvmaze.com/shows/431/episodes

To make the JSON more readable, use a JSON "pretty printer", such as the one here:

https://jsonformatter.org/json-pretty-print

The C# code `Program.cs` and `Show.cs` will parse the JSON data into a list of shows, 
which you can then use `System.Linq` to process.

Some sample TV show JSON files are provided for you.