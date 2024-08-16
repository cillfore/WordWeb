using WordWeb.Helpers;

/*
 * WordWeb - 8/4/24
 * Cill Fore
 */

var userProvidedDictionary = "";

while (string.IsNullOrWhiteSpace(userProvidedDictionary))
{
    Console.WriteLine("Please provide the path to the dictionary CSV you would like graphed...");
    userProvidedDictionary = Console.ReadLine();
}

var webBuilder = new WebBuilder();
var wordWeb = webBuilder.Build(userProvidedDictionary);

var webWriter = new WebWriter(wordWeb);
webWriter.Write();

Console.WriteLine($"Words in Web: {wordWeb.Count:n0}");

Console.ReadLine();
