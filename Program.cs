using WordWeb.Helpers;

/*
 * WordWeb - 8/4/24
 * Cill Fore
 */

var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
var englishDictionary = @"Dictionaries\English.csv";
var englishDictionaryPath = Path.Combine(baseDirectory, englishDictionary);

var webBuilder = new WebBuilder();
var wordWeb = webBuilder.Build(englishDictionaryPath);

var webWriter = new WebWriter(wordWeb);
webWriter.Write();

Console.WriteLine($"Words in Web: {wordWeb.Count:n0}");

Console.ReadLine();
