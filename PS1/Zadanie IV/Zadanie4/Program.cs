using System;
using System.IO;
using System.Collections.Generic;

namespace Zadanie4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Odczyt z pliku In0104.txt
            string inputFile = "In0104.txt";
            string outputFile = "Out0104.txt";
            
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Plik {inputFile} nie istnieje!");
                return;
            }

            // Odczyt danych z pliku
            string[] lines = File.ReadAllLines(inputFile);
            
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                int lineIndex = 0;
                
                while (lineIndex < lines.Length)
                {
                    string line = lines[lineIndex].Trim();
                    
                    // Pomijamy puste linie i komentarze
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                    {
                        lineIndex++;
                        continue;
                    }
                    
                    // Odczyt n i m (n - szerokość, m - liczba prętów)
                    string[] parts = line.Split(new[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        if (int.TryParse(parts[0], out int n) && int.TryParse(parts[1], out int m))
                        {
                            writer.WriteLine($"{n} {m}");
                            lineIndex++;
                            
                            // Odczyt długości prętów
                            List<int> lengths = new List<int>();
                            while (lineIndex < lines.Length && lengths.Count < m)
                            {
                                string lengthLine = lines[lineIndex].Trim();
                                if (!string.IsNullOrWhiteSpace(lengthLine) && !lengthLine.StartsWith("//"))
                                {
                                    string[] lengthParts = lengthLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string part in lengthParts)
                                    {
                                        if (int.TryParse(part, out int length))
                                        {
                                            lengths.Add(length);
                                        }
                                    }
                                }
                                lineIndex++;
                            }
                            
                            // Znajdź wszystkie bezpieczne pasma
                            List<Tuple<int, int>> safeStrips = FindSafeStrips(n, lengths);
                            
                            // Wypisz pary (y1, y2) reprezentujące bezpieczne pasma
                            foreach (var strip in safeStrips)
                            {
                                writer.WriteLine($"{strip.Item1} {strip.Item2}");
                            }
                            
                            // Wypisz liczbę bezpiecznych pasm
                            writer.WriteLine($"liczba bezpiecznych pasm: {safeStrips.Count}");
                        }
                    }
                    else
                    {
                        lineIndex++;
                    }
                }
            }
            
            Console.WriteLine($"Wyniki zapisano do pliku {outputFile}");
        }

        /// <summary>
        /// Znajduje wszystkie bezpieczne pasma (przedziały bez prętów)
        /// Każdy pręt ma szerokość 3.5m z emiterem na pozycji x1
        /// Pręt blokuje przedział [x1, x1 + 3.5), czyli [x1, x1+3] w zaokrągleniu do całkowitych
        /// </summary>
        /// <param name="n">Szerokość korytarza (w metrach)</param>
        /// <param name="barPositions">Lista pozycji x1 gdzie umieszczone są emitery</param>
        /// <returns>Lista par (y1, y2) reprezentujących bezpieczne pasma</returns>
        static List<Tuple<int, int>> FindSafeStrips(int n, List<int> barPositions)
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            
            if (barPositions.Count == 0)
            {
                // Jeśli nie ma prętów, cały korytarz jest bezpieczny
                result.Add(Tuple.Create(0, n));
                return result;
            }
            
            // Tworzymy tablicę zajętości
            bool[] occupied = new bool[n + 1];
            
            // Każdy pręt blokuje tylko swoją pozycję x1
            foreach (int x1 in barPositions)
            {
                if (x1 >= 0 && x1 <= n)
                {
                    occupied[x1] = true;
                }
            }
            
            // Znajdujemy wszystkie przedziały niezajęte (bezpieczne pasma)
            int start = -1;
            for (int i = 0; i <= n; i++)
            {
                if (!occupied[i])
                {
                    if (start == -1)
                    {
                        start = i;
                    }
                }
                else
                {
                    if (start != -1)
                    {
                        result.Add(Tuple.Create(start, i));
                        start = -1;
                    }
                }
            }
            
            // Jeśli ostatnie pasmo sięga do końca
            if (start != -1)
            {
                result.Add(Tuple.Create(start, n + 1));
            }
            
            return result;
        }
    }
}

