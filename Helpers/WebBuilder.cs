using System.Text.RegularExpressions;
using WordWeb.Classes;
using Humanizer;

namespace WordWeb.Helpers;

internal class WebBuilder
{
    private readonly string BAD_LINE = "BAD_LINE";

    public Dictionary<string, Word> Build(string pathToDictionary)
    {
        var words = new Dictionary<string, Word>();
        var currentLine = "";

        try
        {
            var stream = new StreamReader(pathToDictionary);
            currentLine = stream.ReadLine();

            while (currentLine != null)
            {
                var word = GetWord(currentLine);
                var meaning = GetMeaning(currentLine);

                if (meaning == BAD_LINE)
                {
                    currentLine = stream.ReadLine();
                    continue;
                }

                AddMeaning(words, word, meaning);
                currentLine = stream.ReadLine();
            }

            stream.Close();            
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        return words;
    }

    private string GetWord(string currentLine)
    {
        var wordSplits = currentLine.Split(",");
        
        return Strip(wordSplits[0]);
    }

    private string GetMeaning(string currentLine)
    {
        var firstQuote = currentLine.IndexOf('"');
        var lastQuote = currentLine.LastIndexOf('"');

        if (firstQuote == -1 || lastQuote == -1)
        {
            return BAD_LINE;
        }

        var length = lastQuote - firstQuote + 1;
        var definition = currentLine.Substring(firstQuote, length);
        
        return Strip(definition);
    }

    private void AddMeaning(Dictionary<string, Word> words, string word, string meaning)
    {
        if (words.ContainsKey(word))
        {
            words[word].AddMeaning(meaning);
        }
        else
        {
            words.Add(word, new Word(word, meaning));
        }
    }

    private string Strip(string definition)
    {
        var meaning = RemoveMostPunctuation(definition);       
        meaning = HumanizeNumbers(meaning);
        meaning = ReplaceDashesWithSpaces(meaning);
        meaning = EnsureSingleSpaceBetweenWords(meaning);

        return meaning.ToLower().Trim();
    }

    private string RemoveMostPunctuation(string meaning)
    {
        // Retains the single dash
        return Regex.Replace(meaning, @"[^\w\s-]|--", string.Empty);
    }

    private string HumanizeNumbers(string meaning)
    {
        try
        {
            return Regex.Replace(meaning, @"\d+", match => int.Parse(match.Value).ToWords());
        }
        catch
        {
            return meaning;
        }
    }

    private string EnsureSingleSpaceBetweenWords(string meaning)
    {
        return Regex.Replace(meaning, @"\s+", " ");
    }

    private string ReplaceDashesWithSpaces(string meaning)
    {
        return Regex.Replace(meaning, @"-", " ");
    }
}
