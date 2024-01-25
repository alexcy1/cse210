using System;

class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    // Initializing private fields
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
    }

    // Initializing private fields
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    // Display Format that conbines the two constructor
    public string GetDisplayText()
    {
        return $"{_book} {_chapter}:{_verse}" + (_endVerse > 0 ? $"-{_endVerse}" : "");
    }
}
