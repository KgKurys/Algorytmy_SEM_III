using System;
using System.IO;

namespace zadanie_I_I
{
    class Program
    {
        static void Main(string[] args)
        {
            // Odczyt z pliku In0101.txt
            string inputFile = "In0101.txt";
            string outputFile1 = "Out0101.txt";
            
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Plik {inputFile} nie istnieje!");
                return;
            }

            // Odczyt danych z pliku
            string[] lines = File.ReadAllLines(inputFile);
            
            using (StreamWriter writer = new StreamWriter(outputFile1))
            {
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                    {
                        // Przepisz komentarze i puste linie
                        writer.WriteLine(line);
                        continue;
                    }

                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        if (int.TryParse(parts[0], out int n) && int.TryParse(parts[1], out int k))
                        {
                            long result1 = SN1(n, k);
                            long result5 = SN5(n, k);
                            
                            writer.WriteLine($"n={n} k={k}");
                            writer.WriteLine($"SN1 = {n}, kcz = {result1}");
                            writer.WriteLine($"SN2 = {n}, kcz = {result5}");
                        }
                    }
                }
            }
            
            Console.WriteLine($"Wyniki zapisano do pliku {outputFile1}");
        }

        /// <summary>
        /// Algorytm I: Wyznaczanie symbolu Newtona z definicji
        /// C(n,k) = n! / (k! * (n-k)!)
        /// </summary>
        static long SN1(int n, int k)
        {
            // Warunki brzegowe
            if (k < 0 || k > n)
                return 0;
            if (k == 0 || k == n)
                return 1;

            // Obliczanie z definicji
            long numerator = Factorial(n);
            long denominator = Factorial(k) * Factorial(n - k);
            
            return numerator / denominator;
        }

        /// <summary>
        /// Algorytm II (SN5): Wyznaczanie symbolu Newtona iteracyjnie z trójkąta Pascala
        /// Wykorzystuje tablicę 2-wymiarową do budowy trójkąta Pascala
        /// </summary>
        static long SN5(int n, int k)
        {
            // Warunki brzegowe
            if (k < 0 || k > n)
                return 0;
            if (k == 0 || k == n)
                return 1;

            // Tablica 2-wymiarowa dla trójkąta Pascala
            long[,] pascal = new long[n + 1, k + 1];

            // Wypełnianie trójkąta Pascala iteracyjnie
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= Math.Min(i, k); j++)
                {
                    // Warunki brzegowe: C(i,0) = 1 i C(i,i) = 1
                    if (j == 0 || j == i)
                    {
                        pascal[i, j] = 1;
                    }
                    else
                    {
                        // Własność trójkąta Pascala: C(i,j) = C(i-1,j-1) + C(i-1,j)
                        pascal[i, j] = pascal[i - 1, j - 1] + pascal[i - 1, j];
                    }
                }
            }

            return pascal[n, k];
        }

        /// <summary>
        /// Funkcja pomocnicza do obliczania silni
        /// </summary>
        static long Factorial(int n)
        {
            if (n <= 1)
                return 1;
            
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            
            return result;
        }
    }
}
