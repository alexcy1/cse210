/*

1. I implemented a badge congratulatory message system to keep track of completed goals. 
  The message is displayed each time a goal is viewed or accessed.

2. Additionally, I introduced a completion message for each goal that is saved 
   to the file and marked as completed.

3. A "Show" message option that displays goal names.

*/

using System;
using System.Collections.Generic;

namespace Develop05
{
    class Program
    {
        static void Main()
        {
            Console.Clear();
            // Create an instance of the GoalManager
            GoalManager manager = new GoalManager();
            manager.Start();
            
        }
    }
}
