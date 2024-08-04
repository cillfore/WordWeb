using WordWeb.Classes;
using WordWeb.Helpers;

/*
 * WordWeb - 8/4/24
 * Cill Fore
 */

var webBuilder = new WebBuilder();
var wordWeb = webBuilder.Build(@"C:\Users\cillf\source\repos\WordWeb\WordWeb\Dictionaries\English.csv");

Console.WriteLine($"Words in Web: {wordWeb.Count:n0}");

foreach (var word in wordWeb.Values)
{
    var meanings = word.GetJoinedMeanings();
    var pause = true;
}

Console.ReadLine();