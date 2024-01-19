using System;
using System.Collections.Generic;

namespace Develop02
{
    public class PromptGenerator
    {
        private List<string> _prompts = new List<string>();
        private string darkGreenColor = "\u001b[32m";
        private string resetColor = "\u001b[0m";


        // Add Prompt ----------------------------------------------------------------
        public PromptGenerator()
        {
            // Initialize prompts
            _prompts.Add($"{darkGreenColor}Who was the most interesting person I interacted with today?{resetColor}");
            _prompts.Add($"{darkGreenColor}What was the best part of my day?{resetColor}");
            _prompts.Add($"{darkGreenColor}How did I see the hand of the Lord in my life today?{resetColor}");
            _prompts.Add($"{darkGreenColor}What was the strongest emotion I felt today?{resetColor}");
            _prompts.Add($"{darkGreenColor}If I had one thing I could do over today, what would it be?{resetColor}");

            _prompts.Add($"{darkGreenColor}Reflect on a moment from today that caught you off guard. Explain why it surprised you and how it impacted your perspective.{resetColor}");
            _prompts.Add($"{darkGreenColor}Identify the person who presented the most significant challenge to you today. Share what you learned from the experience and how it contributed to your personal growth.{resetColor}");
            _prompts.Add($"{darkGreenColor}Recount an act of kindness that you observed or participated in today. Explore the impact of this gesture and how it resonated with you.{resetColor}");
            _prompts.Add($"{darkGreenColor}f you could revisit a specific conversation from today, which one would it be? Delve into the details and discuss why that particular interaction holds significance for you.{resetColor}");
            _prompts.Add($"{darkGreenColor}Envision assembling a time capsule encapsulating your day. Outline five items you would include and elaborate on the reasons behind each choice.{resetColor}");
        }


        // Random Prompt Generator --------------------------------------------------
        public string GetRandomPrompt()
        {
            // Randomly select a prompt
            Random random = new Random();
            int randomIndex = random.Next(_prompts.Count);
            return _prompts[randomIndex];
        }
    }
}
