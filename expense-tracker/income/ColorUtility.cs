using System;

public class ColorUtility
{
    public string RedColor { get; } = "\u001b[31m";
    public string GreenColor { get; } = "\u001b[32m";
    public string DarkPurpleColor { get; } = "\u001b[35m";
    public string DarkBlueColor { get; } = "\u001b[34m";
    public string ResetColor { get; } = "\u001b[0m";

    public string ApplyRed(string text)
    {
        return $"{RedColor}{text}{ResetColor}";
    }

    public string ApplyGreen(string text)
    {
        return $"{GreenColor}{text}{ResetColor}";
    }

    public string ApplyDarkPurple(string text)
    {
        return $"{DarkPurpleColor}{text}{ResetColor}";
    }

    public string ApplyDarkBlue(string text)
    {
        return $"{DarkBlueColor}{text}{ResetColor}";
    }
}
