using System.Text.RegularExpressions;
using WordWeb.Classes;

namespace WordWeb.Helpers;

internal class WebWriter
{
    private readonly string VaultPath = @"Vault\WordWeb";
    private Dictionary<string, Word> WordWeb = new();
    
    public WebWriter(Dictionary<string, Word> wordWeb)
    {
        WordWeb = wordWeb;
    }

    public void Write()
    {
        try
        {
            var relativeVaultPath = GetRelativeVaultPath();
            EnsureVaultDirectoryExists(relativeVaultPath);

            foreach (var word in WordWeb.Values)
            {
                var meanings = word.GetJoinedMeanings();
                var bracketedMeanings = SurroundWordsWithBrackets(meanings);
                var filePath = Path.Combine(relativeVaultPath, $"{word.Name}.md");

                File.WriteAllText(filePath, bracketedMeanings);
                Console.WriteLine($"File written successfully for word: {word.Name}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    private string GetRelativeVaultPath()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        return Path.Combine(baseDirectory, VaultPath);
    }

    private void EnsureVaultDirectoryExists(string relativeVaultPath)
    {
        if (!Directory.Exists(relativeVaultPath))
        {
            Directory.CreateDirectory(relativeVaultPath);
        }
    }

    private string SurroundWordsWithBrackets(string meaning)
    {
        return Regex.Replace(meaning, @"\b(\w+)\b", m => $"[[{m.Value}]]");
    }
}
