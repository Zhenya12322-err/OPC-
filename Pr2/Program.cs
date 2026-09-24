using System;

namespace PZ2
{
    class Program
    {
        static void Main()
        {
            int[][] groups =
            {
                new int[] { 10, 9, 8, 7, 10, 9, 8, 9, 7, 10 },
                new int[] { 8, 7, 9, 10, 6, 8, 9, 7, 10, 8 },
                new int[] { 9, 9, 10, 8, 7, 8, 9, 10, 9, 8 }
            };

            int totalSum = 0;
            int totalCount = 0;
            int totalMin = int.MaxValue;
            int totalMax = int.MinValue;

            for (int i = 0; i < groups.Length; i++)
            {
                int sum = 0;
                int min = groups[i][0];
                int max = groups[i][0];

                foreach (int grade in groups[i])
                {
                    sum += grade;

                    if (grade < min)
                        min = grade;

                    if (grade > max)
                        max = grade;
                }

                double average = (double)sum / groups[i].Length;

                Console.WriteLine($"Група {i + 1}:");
                Console.WriteLine($"Середній бал: {average:F2}");
                Console.WriteLine($"Мінімальна оцінка: {min}");
                Console.WriteLine($"Максимальна оцінка: {max}");
                Console.WriteLine();

                totalSum += sum;
                totalCount += groups[i].Length;

                if (min < totalMin)
                    totalMin = min;

                if (max > totalMax)
                    totalMax = max;
            }

            double totalAverage = (double)totalSum / totalCount;

            Console.WriteLine("Весь потік:");
            Console.WriteLine($"Середній бал: {totalAverage:F2}");
            Console.WriteLine($"Мінімальна оцінка: {totalMin}");
            Console.WriteLine($"Максимальна оцінка: {totalMax}");
        }
    }
}
