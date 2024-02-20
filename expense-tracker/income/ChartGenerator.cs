using System;
using System.Collections.Generic;
using System.Linq;

class ChartGenerator
{
    public static void GenerateConsolidatedChart(List<Expense> expenses, float totalIncome)
    {
        ColorUtility colorUtility = new ColorUtility();

        // Group expenses by category and calculate the total amount for each category
        var groupedExpenses = expenses
            .GroupBy(expense => expense.GetCategory())
            .Select(group => new
            {
                Category = group.Key,
                TotalAmount = group.Sum(expense => expense.GetAmount())
            });

        // Calculate total expenses, total income percentage, and highest expense percentage
        float totalExpenses = groupedExpenses.Sum(group => group.TotalAmount);
        float totalIncomePercent = (totalExpenses / totalIncome) * 100;
        float highestExpensePercent = groupedExpenses.Max(group => (group.TotalAmount / totalExpenses) * 100);

        // Display individual expense categories with their amounts and percentages
        foreach (var group in groupedExpenses)
        {
            float expensePercent = (group.TotalAmount / totalExpenses) * 100;
            float incomePercent = (group.TotalAmount / totalIncome) * 100;

            Console.WriteLine(colorUtility.ApplyDarkBlue($"{group.Category}\t\t${group.TotalAmount:N0}\t\tExpense %: {expensePercent:N2}%\t\tIncome %: {incomePercent:N2}%"));
        }

        // Display total expenses and total income percentage
        float totalExpensesPercent = (totalExpenses / totalIncome) * 100;
        Console.WriteLine(colorUtility.ApplyDarkPurple($"\nTotal Expenses\t${totalExpenses:N0}\t\tTotal Income Percent: {totalIncomePercent:N2}%"));

        // Identify and display the highest expense category or categories
        var highestExpenseGroups = groupedExpenses
            .Where(group => Math.Abs((group.TotalAmount / totalExpenses) * 100 - highestExpensePercent) < 0.01)
            .Select(group => new
            {
                Category = group.Category,
                ExpensePercent = (group.TotalAmount / totalExpenses) * 100,
                IncomePercent = (group.TotalAmount / totalIncome) * 100
            })
            .ToList();

        if (highestExpenseGroups.Any())
        {
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Highest Expense:".ToUpper());
            Console.WriteLine("----------------------------------------");

            foreach (var group in highestExpenseGroups)
            {
                Console.WriteLine(colorUtility.ApplyDarkPurple($"{group.Category}\t\tExpense %: {group.ExpensePercent:N2}%\t\tIncome %: {group.IncomePercent:N2}%"));
            }

            // Check if the highest income percentage is greater than 18%
            float highestIncomePercent = highestExpenseGroups.Max(group => group.IncomePercent);
            if (highestIncomePercent > 18)
            {
                float cutDownPercentage = highestIncomePercent - 18;

                // Display a warning if spending is too high
                if (highestExpenseGroups.Count() > 1)
                {
                    Console.WriteLine(colorUtility.ApplyRed($"\nWarning: You are spending too much on {string.Join(" and ", highestExpenseGroups.Select(g => g.Category))}.\nReduce your spending by {cutDownPercentage:N2}% each to increase savings."));
                }
                else
                {
                    Console.WriteLine(colorUtility.ApplyRed($"\nWarning: You are spending too much on {string.Join("", highestExpenseGroups.Select(g => g.Category))}.\nReduce your spending by {cutDownPercentage:N2}% to increase savings."));
                }
            }
            else
            {
                // Display congratulations if spending is within the recommended percentage
                Console.WriteLine(colorUtility.ApplyGreen($"\nCongratulations: You are spending within the\nrecommended percentage for this category."));
            }
        }
    }
}
