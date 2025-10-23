using System;
using System.IO;
using System.Collections.Generic;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Odczyt z pliku In0102.txt
            string inputFile = "In0102.txt";
            string outputFile = "Out0102.txt";
            
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Plik {inputFile} nie istnieje!");
                return;
            }

            // Odczyt danych z pliku
            string[] lines = File.ReadAllLines(inputFile);
            
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                    {
                        // Przepisz komentarze i puste linie
                        writer.WriteLine(line);
                        continue;
                    }

                    string trimmedLine = line.Trim();
                    if (int.TryParse(trimmedLine, out int k))
                    {
                        // Generowanie wszystkich 2-elementowych podzbiorów zbioru {1, 2, ..., k}
                        List<int[]> subsets = Generate2ElementSubsets(k);
                        
                        writer.WriteLine(k);
                        
                        // Wypisanie wszystkich podzbiorów
                        foreach (var subset in subsets)
                        {
                            writer.WriteLine($"{subset[0]} {subset[1]}");
                        }
                    }
                }
            }
            
            Console.WriteLine($"Wyniki zapisano do pliku {outputFile}");
        }

        /// <summary>
        /// Generuje wszystkie 2-elementowe podzbiory zbioru {1, 2, ..., k}
        /// Algorytm O(k^2) - dla każdej pary (i,j) gdzie i < j
        /// </summary>
        static List<int[]> Generate2ElementSubsets(int k)
        {
            List<int[]> result = new List<int[]>();
            
            if (k < 2)
                return result;
            
            // Generowanie wszystkich par (i, j) gdzie 1 ≤ i < j ≤ k
            for (int i = 1; i <= k; i++)
            {
                for (int j = i + 1; j <= k; j++)
                {
                    result.Add(new int[] { i, j });
                }
            }
            
            return result;
        }
    }
}
