using System;

public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text; // Accept a string parameter
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true; // On hide isHidden is true
    }

    public void Show()
    {
        _isHidden = false; // On show isHidden is false
    }

    public bool IsHidden
    {
        get { return _isHidden; } // Get isHidden
    }

    public string GetDisplayText()
    {
        return _isHidden ? "____ " : $"{_text} "; // If _isHidden is true, execute "____ " else execute _text
    }

    public string GetFinalDisplayText()
    {
        return _isHidden ? "____ " : $"{_text} "; // If _isHidden is true, execute "____ " else execute _text
    }
}
