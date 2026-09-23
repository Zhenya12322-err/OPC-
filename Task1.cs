using System;

namespace Pz1
{
    internal class Task1
    {
        public void Run()
        {
            double a = 3;
            double b = 4;
            double c = 5;

            if (!Method1(a, b, c))
            {
                Console.WriteLine("Сторони повинні бути додатними.");
                return;
            }

            if (!Method2(a, b, c))
            {
                Console.WriteLine("Трикутник не існує.");
                return;
            }

            Console.WriteLine("Трикутник існує.");

            Console.WriteLine("Периметр: " + Method3(a, b, c));
            Console.WriteLine("Площа: " + Method4(a, b, c));
            Console.WriteLine("Вид: " + Method5(a, b, c));
        }

        private bool Method1(double a, double b, double c)
        {
            return a > 0 && b > 0 && c > 0;
        }

        private bool Method2(double a, double b, double c)
        {
            return a + b > c &&
                   a + c > b &&
                   b + c > a;
        }

        private double Method3(double a, double b, double c)
        {
            return a + b + c;
        }

        private double Method4(double a, double b, double c)
        {
            double p = (a + b + c) / 2;

            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        private string Method5(double a, double b, double c)
        {
            if (a == b && b == c)
                return "рівносторонній";

            if (a == b || a == c || b == c)
                return "рівнобедрений";

            if (a * a + b * b == c * c ||
                a * a + c * c == b * b ||
                b * b + c * c == a * a)
                return "прямокутний";

            return "довільний";
        }
    }
}
