/*

1. I made the program work with a library of scriptures rather than a single one. 
   I let the user choose scriptures at random to present.

2. Randomly select from only those words that are not already hidden.
3. Allow user to enter the number of words to hide

*/

using System;
using System.CodeDom.Compiler;
using System.Xml.XPath;
using System.Collections.Generic;
using System.IO;

namespace Develop03
{
    class Program
    {
        private static Scripture selectedScripture;
    static void Main()
        {
            // Prompt user for number of words to hide
            Console.Write("\nEnter number of words to hide :::");
            int number = Convert.ToInt32(Console.ReadLine()!);

            List<Scripture> scriptures = new List<Scripture>
            {
                // Creating Scripture library with Scripure and Reference Instances
                new Scripture(new Reference("Psalm", 23, 1, 6), "The Lord is my shepherd; I shall not want. He maketh me to lie down in green pastures: he leadeth me beside the still waters. He restoreth my soul: he leadeth me in the paths of righteousness for his name's sake. Yea, though I walk through the valley of the shadow of death, I will fear no evil: for thou art with me; thy rod and thy staff they comfort me. Thou preparest a table before me in the presence of mine enemies: thou anointest my head with oil; my cup runneth over. Surely goodness and mercy shall follow me all the days of my life: and I will dwell in the house of the Lord forever."),
                new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart and lean not unto thine own understanding; in all thy ways acknowledge him, and he shall direct thy paths."),
                new Scripture(new Reference("Romans", 12, 2), "Do not conform any longer to the pattern of this world, but be transformed by the renewing of your mind. Then you will be able to test and approve what God’s will is—his good, pleasing and perfect will."),
                new Scripture(new Reference("Matthew", 28, 19, 1), "Therefore go and make disciples of all nations, baptizing them in the name of the Father and of the Son and of the Holy Spirit."),
                new Scripture(new Reference("Philippians", 4, 6, 1), "Do not be anxious about anything, but in everything, by prayer and petition, with thanksgiving, present your requests to God."),
                new Scripture(new Reference("Jeremiah", 29, 11), "For I know the plans I have for you,” declares the LORD, “plans to prosper you and not to harm you, plans to give you hope and a future."),
                new Scripture(new Reference("Genesis", 1, 1, 2), "In the beginning God created the heavens and the earth. 2 Now the earth was formless and empty, darkness was over the surface of the deep, and the Spirit of God was hovering over the waters."),
                new Scripture(new Reference("Luke", 12, 11, 12), "When you are brought before synagogues, rulers and authorities, do not worry about how you will defend yourselves or what you will say, 12 for the Holy Spirit will teach you at that time what you should say."),
                new Scripture(new Reference("Exodus", 5, 16), "Your servants are given no straw, yet we are told, ‘Make bricks!’ Your servants are being beaten, but the fault is with your own people."),
                new Scripture(new Reference("1 Kings", 7, 1, 2), "It took Solomon thirteen years, however, to complete the construction of his palace. 2 He built the Palace of the Forest of Lebanon a hundred cubits long, fifty wide and thirty high,[a] with four rows of cedar columns supporting trimmed cedar beams."),
            };

            if (selectedScripture == null) // Randonmly Select A scripture if not selected
            {
                Random random = new Random();
                selectedScripture = GetRandomScripture(scriptures, random);
            }

            while (!selectedScripture.IsCompletelyHidden())
            {
                Console.Clear(); // Clear console before showing the current display
                Console.WriteLine(selectedScripture.GetDisplayText());
                Console.Write("\nPress enter to continue or type 'quit' to finish ::: ");

                string userInput = Console.ReadLine();

                if (userInput.ToLower() == "quit")
                {
                    Console.WriteLine("You quit the program.\n");
                    break; // Quit the program
                }

                selectedScripture.HideRandomWords(number); // Number of words to hide
            }

            // Clear the console again to show the final display
            Console.Clear();
            Console.WriteLine(selectedScripture.GetFinalDisplayText());

            if (selectedScripture.IsCompletelyHidden())
            {
                Console.WriteLine("\nPress enter to continue or type 'quit' to finish ::: ");
            }
        }

        // Randomly select a scripture from a list of scriptures
        private static Scripture GetRandomScripture(List<Scripture> scriptures, Random random)
        {
            int index = random.Next(scriptures.Count);
            return scriptures[index];
        }

    }
}