using System;

class ChartGenerator
{

// Generates a consolidated chart based on the provided expenses and total income.

public static void GenerateConsolidatedChart(List<Expense> expenses, float totalIncome)
{
    // Group expenses by category and calculate total amounts
    var groupedExpenses = expenses
        .GroupBy(expense => expense.GetCategory())
        .Select(group => new
        {
            Category = group.Key,
            TotalAmount = group.Sum(expense => expense.GetAmount())
        });

    // Calculate total expenses and percentages
    float totalExpenses = groupedExpenses.Sum(group => group.TotalAmount);
    float totalIncomePercent = (totalExpenses / totalIncome) * 100;
    float highestExpensePercent = groupedExpenses.Max(group => (group.TotalAmount / totalExpenses) * 100);

    // Display expenses breakdown
    foreach (var group in groupedExpenses)
    {
        float expensePercent = (group.TotalAmount / totalExpenses) * 100;
        float incomePercent = (group.TotalAmount / totalIncome) * 100;

        Console.WriteLine($"{group.Category}\t\t${group.TotalAmount:N0}\t\tExpense %: {expensePercent:N2}%\t\tIncome %: {incomePercent:N2}%");
    }

    // Display total expenses information
    float totalExpensesPercent = (totalExpenses / totalIncome) * 100;
    Console.WriteLine($"\nTotal Expenses\t${totalExpenses:N0}\t\tTotal Income Percent: {totalIncomePercent:N2}%");

    // Identify highest expense categories
    var highestExpenseGroups = groupedExpenses
        .Where(group => Math.Abs((group.TotalAmount / totalExpenses) * 100 - highestExpensePercent) < 0.01)
        .Select(group => new
        {
            Category = group.Category,
            ExpensePercent = (group.TotalAmount / totalExpenses) * 100,
            IncomePercent = (group.TotalAmount / totalIncome) * 100
        })
        .ToList();

    // Display information about highest expenses
    if (highestExpenseGroups.Any())
    {
        Console.WriteLine("\n----------------------------------------");
        Console.WriteLine("Highest Expense:".ToUpper());
        Console.WriteLine("----------------------------------------");

        foreach (var group in highestExpenseGroups)
        {
            Console.WriteLine($"{group.Category}\t\tExpense %: {group.ExpensePercent:N2}%\t\tIncome %: {group.IncomePercent:N2}%");
        }

        // Check for spending warnings or congratulations
        float highestIncomePercent = highestExpenseGroups.Max(group => group.IncomePercent);
        if (highestIncomePercent > 18)
        {
            float cutDownPercentage = highestIncomePercent - 18;
            
            if (highestExpenseGroups.Count() > 1)
            {
                Console.WriteLine($"\nWarning: You are spending too much on {string.Join(" and ", highestExpenseGroups.Select(g => g.Category))}.\nReduce your spending by {cutDownPercentage:N2}% each to increase savings.");
            }
            else
            {
                Console.WriteLine($"\nWarning: You are spending too much on {string.Join("", highestExpenseGroups.Select(g => g.Category))}.\nReduce your spending by {cutDownPercentage:N2}% to increase savings.");
            }
        }
        else
        {
            Console.WriteLine($"\nCongratulations: You are spending within the\nrecommended percentage for these categories.");
        }
    }
}
}