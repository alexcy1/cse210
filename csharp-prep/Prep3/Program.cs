using System;

class Program
{
    static void Main(string[] args)
    {
        string playAgain = "yes";
        while (playAgain.ToLower() == "yes")
        {
            Random randomizer = new Random();  // Generate Random Number
            int magicNumber = randomizer.Next(1, 101);
            int count = 0; 
            int guess = -1;
            
            // So long Guess Number is not found, keep prompting the user for the magic number
            while (guess != magicNumber) 
            {
                count++;
                Console.WriteLine("");

                // Prompt User for Margic Number
                Console.Write("What is the magic number? Take a guess (between 1 and 100) :::");
                guess = int.Parse(Console.ReadLine());

                if (guess < magicNumber) // Conditional statment to check for matching number
                {
                    Console.WriteLine("Too low! Try higher number.\n");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Too high! Try lower number.\n");
                }
                else
                {
                    Console.WriteLine($"You guessed it in {count} guesses!\n"); 
                }
            }

            Console.Write("Would you like to play again? (yes/no): "); // Play Again
            playAgain = Console.ReadLine();
        }

        Console.WriteLine("\nThanks for playing!\n");
    }
}
