using System;
using System.Collections.Generic;

class Scripture
{
    private Reference _reference;  // private field _reference of type Reference
    private List<Word> _words;


    public Scripture(Reference reference, string text) // Constructor
    {
        _reference = reference;
        InitializeWords(text);
    }


    // Split the string into individual text, loop through, and add them to the _words variables
    private void InitializeWords(string text)
    {
        _words = new List<Word>();
        string[] wordArray = text.Split(' ');

        foreach (string word in wordArray)
        {
            _words.Add(new Word(word));
        }
    }


    // Create a list of visible word, and hide the specified number of words
     // Randomly select words from the visibleWords list without modifying it during loop/iteration.
    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();

        List<Word> visibleWords = _words.FindAll(word => !word.IsHidden);
        Shuffle(visibleWords);

        for (int i = 0; i < Math.Min(numberToHide, visibleWords.Count); i++)
        {
            Word wordToHide = visibleWords[i];
            wordToHide.Hide();
        }
    }

    // Select words from the shuffled list for even distribution of hidden words.
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        Random random = new Random();
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }



    // Iterates over each Word object in the _words list.
    // concatenates the display text of the reference with the display text of each word in the scripture.
    public string GetDisplayText()
    {
        string displayText = $"{_reference.GetDisplayText()} ";
        foreach (Word word in _words)
        {
            displayText += word.GetDisplayText();
        }
        return displayText;
    }


    // Checks whether all words in the scripture are hidden
    public bool IsCompletelyHidden()
    {
        return _words.TrueForAll(word => word.IsHidden);

    }


    // Combining the display text of the reference with the final display text 
    // of each word in the scripture.
    public string GetFinalDisplayText()
    {
        string displayText = $"{_reference.GetDisplayText()} ";
        foreach (Word word in _words)
        {
            displayText += word.GetFinalDisplayText();
        }
        return displayText;
    }


}
