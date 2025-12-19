using System;
using System.Collections.Generic;
using System.IO;

namespace PS3
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU PS3 ===");
                Console.WriteLine("1. Problem 1 - Subkod genetyczny (LCS)");
                Console.WriteLine("2. Problem 3 - Algorytm Kruskala (MST)");
                Console.WriteLine("0. Wyjście");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        WykonajProblem1();
                        break;
                    case "2":
                        WykonajProblem3();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ========== PROBLEM 1 - SUBKOD GENETYCZNY (LCS) ==========

        static void WykonajProblem1()
        {
            string nazwaWej = "In0301.txt";
            string nazwaWyj = "Out0301.txt";

            if (!File.Exists(nazwaWej))
                return;

            try
            {
                string[] linie = File.ReadAllLines(nazwaWej);
                
                if (linie.Length < 1)
                    return;

                int n = int.Parse(linie[0].Trim());

                List<string> wyniki = new List<string>();

                for (int i = 0; i < n; i++)
                {
                    string charakterystyka1 = linie[1 + i * 2].Trim();
                    string charakterystyka2 = linie[2 + i * 2].Trim();

                    // Znajdź najdłuższy wspólny podciąg (subkod genetyczny)
                    string lcs = ZnajdzNajdluzszyWspolnyPodciag(charakterystyka1, charakterystyka2);

                    wyniki.Add($"{lcs.Length} {lcs}");
                }

                // Zapisz wyniki
                File.WriteAllLines(nazwaWyj, wyniki);
            }
            catch (Exception)
            {
            }
        }

        static string ZnajdzNajdluzszyWspolnyPodciag(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;

            // Tablica do programowania dynamicznego
            // dp[i,j] = długość LCS dla s1[0..i-1] i s2[0..j-1]
            int[,] dp = new int[m + 1, n + 1];

            // Wypełnianie tablicy
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        // Znaki są równe - zwiększ długość LCS
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        // Znaki różne - weź maksimum z pominięcia znaku z s1 lub s2
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            // Odtworzenie LCS (backtracking)
            return OdtworzLCS(s1, s2, dp, m, n);
        }

        static string OdtworzLCS(string s1, string s2, int[,] dp, int i, int j)
        {
            // Warunek bazowy
            if (i == 0 || j == 0)
            {
                return "";
            }

            // Jeśli znaki są równe, dodaj znak do wyniku
            if (s1[i - 1] == s2[j - 1])
            {
                return OdtworzLCS(s1, s2, dp, i - 1, j - 1) + s1[i - 1];
            }

            // Idź w kierunku większej wartości
            if (dp[i - 1, j] > dp[i, j - 1])
            {
                return OdtworzLCS(s1, s2, dp, i - 1, j);
            }
            else
            {
                return OdtworzLCS(s1, s2, dp, i, j - 1);
            }
        }

        // ========== PROBLEM 3 - ALGORYTM KRUSKALA (MST) ==========

        // Struktura krawędzi
        class Krawedz
        {
            public int Waga { get; set; }
            public int Poczatek { get; set; }
            public int Koniec { get; set; }

            public Krawedz(int waga, int poczatek, int koniec)
            {
                Waga = waga;
                Poczatek = poczatek;
                Koniec = koniec;
            }
        }

        static void WykonajProblem3()
        {
            string nazwaWej = "In0303.txt";
            string nazwaWyj = "Out0303.txt";

            if (!File.Exists(nazwaWej))
                return;

            try
            {
                string[] linie = File.ReadAllLines(nazwaWej);
                
                if (linie.Length < 1)
                    return;

                int n = int.Parse(linie[0].Trim()); // liczba wierzchołków

                // Wczytaj listy incydencji i utwórz listę krawędzi
                List<Krawedz> krawedzie = new List<Krawedz>();
                HashSet<(int, int)> dodaneKrawedzie = new HashSet<(int, int)>();

                for (int i = 1; i <= n; i++)
                {
                    string[] dane = linie[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    
                    // Każda para (wierzchołek, waga)
                    for (int j = 0; j < dane.Length; j += 2)
                    {
                        int sasiad = int.Parse(dane[j]);
                        int waga = int.Parse(dane[j + 1]);

                        // Dodaj krawędź tylko raz (graf nieskierowany)
                        int min = Math.Min(i, sasiad);
                        int max = Math.Max(i, sasiad);

                        if (!dodaneKrawedzie.Contains((min, max)))
                        {
                            krawedzie.Add(new Krawedz(waga, i, sasiad));
                            dodaneKrawedzie.Add((min, max));
                        }
                    }
                }

                // Sortowanie krawędzi przez scalanie
                List<Krawedz> posortowaneKrawedzie = MergeSort(krawedzie);

                // Algorytm Kruskala
                List<Krawedz> mst = Kruskal(n, posortowaneKrawedzie);

                int sumaWag = 0;
                List<string> wynikiKrawedzi = new List<string>();

                foreach (var k in mst)
                {
                    wynikiKrawedzi.Add($"{k.Poczatek} {k.Koniec} [{k.Waga}]");
                    sumaWag += k.Waga;
                }

                // Zapisz wyniki
                List<string> wyniki = new List<string>();
                wyniki.Add(string.Join(", ", wynikiKrawedzi));
                wyniki.Add(sumaWag.ToString());
                File.WriteAllLines(nazwaWyj, wyniki);
            }
            catch (Exception)
            {
            }
        }

        static List<Krawedz> MergeSort(List<Krawedz> lista)
        {
            // Warunek bazowy
            if (lista.Count <= 1)
            {
                return lista;
            }

            // Podziel listę na pół
            int srodek = lista.Count / 2;
            List<Krawedz> lewa = lista.GetRange(0, srodek);
            List<Krawedz> prawa = lista.GetRange(srodek, lista.Count - srodek);

            // Rekurencyjnie posortuj obie połowy
            lewa = MergeSort(lewa);
            prawa = MergeSort(prawa);

            // Scal posortowane połowy
            return Merge(lewa, prawa);
        }

        static List<Krawedz> Merge(List<Krawedz> lewa, List<Krawedz> prawa)
        {
            List<Krawedz> wynik = new List<Krawedz>();
            int i = 0, j = 0;

            while (i < lewa.Count && j < prawa.Count)
            {
                if (lewa[i].Waga <= prawa[j].Waga)
                {
                    wynik.Add(lewa[i]);
                    i++;
                }
                else
                {
                    wynik.Add(prawa[j]);
                    j++;
                }
            }

            // Dodaj pozostałe elementy
            while (i < lewa.Count)
            {
                wynik.Add(lewa[i]);
                i++;
            }

            while (j < prawa.Count)
            {
                wynik.Add(prawa[j]);
                j++;
            }

            return wynik;
        }

        static List<Krawedz> Kruskal(int n, List<Krawedz> krawedzie)
        {
            List<Krawedz> mst = new List<Krawedz>();
            
            // Struktura Union-Find (Find-Union)
            int[] rodzic = new int[n + 1];
            int[] ranga = new int[n + 1];

            // Inicjalizacja - każdy wierzchołek jest swoim własnym rodzicem
            for (int i = 1; i <= n; i++)
            {
                rodzic[i] = i;
                ranga[i] = 0;
            }

            // Przetwarzaj krawędzie w kolejności rosnących wag
            foreach (var krawedz in krawedzie)
            {
                int korzenP = Find(rodzic, krawedz.Poczatek);
                int korzenK = Find(rodzic, krawedz.Koniec);

                // Jeśli wierzchołki są w różnych zbiorach, dodaj krawędź do MST
                if (korzenP != korzenK)
                {
                    mst.Add(krawedz);
                    Union(rodzic, ranga, korzenP, korzenK);

                    // MST ma dokładnie n-1 krawędzi
                    if (mst.Count == n - 1)
                    {
                        break;
                    }
                }
            }

            return mst;
        }

        static int Find(int[] rodzic, int x)
        {
            if (rodzic[x] != x)
            {
                rodzic[x] = Find(rodzic, rodzic[x]); // Kompresja ścieżki
            }
            return rodzic[x];
        }

        static void Union(int[] rodzic, int[] ranga, int x, int y)
        {
            if (ranga[x] < ranga[y])
            {
                rodzic[x] = y;
            }
            else if (ranga[x] > ranga[y])
            {
                rodzic[y] = x;
            }
            else
            {
                rodzic[y] = x;
                ranga[x]++;
            }
        }
    }
}
