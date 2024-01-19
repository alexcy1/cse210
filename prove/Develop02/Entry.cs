using System;

namespace Develop02
{
    public class Entry
{
    private string _date;
    private string _promptText;
    private string _entryText;



    // Entry Method ------------------------------------------------------------------
    public Entry(string date, string promptText, string entryText)
    {
        _date = date;
        _promptText = promptText;
        _entryText = entryText;
    }

    public string Date => _date;
    public string PromptText => _promptText;
    public string EntryText => _entryText;



    // Display Method ------------------------------------------------------------------
    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_promptText}\n{_entryText}");
    }

    }
}
