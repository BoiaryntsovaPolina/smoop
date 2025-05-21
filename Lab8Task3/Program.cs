internal class Program
{
    // Delegates for our lambda expressions
    public delegate int CountDelegate(int[] array);
    public delegate bool DateCheckDelegate(DateTime date);
    public delegate bool TextCheckDelegate(string text, string[] words);

    private static void Main(string[] args)
    {
        // Test data
        int[] numbers = { 7, 14, 3, 21, -5, 28, 6, 9, -14, 42, 8, 10 };

        // Lambda 1: Count numbers divisible by 7
        CountDelegate countDivisibleBySeven = arr => {
            int count = 0;
            foreach (int num in arr)
            {
                if (num % 7 == 0) count++;
            }
            return count;
        };

        // Lambda 2: Count positive numbers
        CountDelegate countPositive = arr => {
            int count = 0;
            foreach (int num in arr)
            {
                if (num > 0) count++;
            }
            return count;
        };

        // Lambda 3: Check if date is Programmer's Day (256th day of the year)
        DateCheckDelegate isProgrammersDay = date =>
            date.DayOfYear == 256;

        // Lambda 4: Check if text contains any of the specified words
        TextCheckDelegate containsWords = (text, words) => {
            if (text == null || words == null) return false;

            foreach (string word in words)
            {
                if (text.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
            }
            return false;
        };

        // Testing Lambda 1
        Console.WriteLine("Numbers divisible by 7: " + countDivisibleBySeven(numbers));

        // Testing Lambda 2
        Console.WriteLine("Positive numbers: " + countPositive(numbers));

        // Testing Lambda 3
        DateTime[] testDates = {
                new DateTime(2023, 9, 13), // Programmer's Day in non-leap year
                new DateTime(2024, 9, 12), // Programmer's Day in leap year
                new DateTime(2023, 9, 12),
                new DateTime(2024, 9, 13)
            };

        foreach (var date in testDates)
        {
            Console.WriteLine($"{date.ToShortDateString()} is programmer's day: {isProgrammersDay(date)}");
        }

        // Testing Lambda 4
        string sampleText = "C# delegates and lambda expressions are important concepts for programmers.";
        string[] wordsToFind1 = { "lambda", "important", "coding" };
        string[] wordsToFind2 = { "Java", "Python", "Ruby" };

        Console.WriteLine($"Text contains words from first array: {containsWords(sampleText, wordsToFind1)}");
        Console.WriteLine($"Text contains words from second array: {containsWords(sampleText, wordsToFind2)}");
    }
}