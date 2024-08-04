using System;
namespace WordWeb.Classes;

internal class Word
{
    public string Name { get; set; } = "";

    public List<string> Meanings { get; set; } = [];

    public Word(string name, string meaning = "")
    {
        Name = name;
        Meanings.Add(meaning);
    }

    public void AddMeaning(string meaning)
    {
        Meanings.Add(meaning);
    }

    public string GetJoinedMeanings()
    {
        return String.Join("\n\n", Meanings);
    }
}
