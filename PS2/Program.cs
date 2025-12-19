using System;
using System.Collections.Generic;
using System.IO;

namespace PS2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU GŁÓWNE PS2 ===");
                Console.WriteLine("1. Problem 1a - Sortowanie przez zliczanie");
                Console.WriteLine("2. Problem 9 - Metoda bisekcji");
                Console.WriteLine("3. Problem 3 - Ciąg Fibonacciego");
                Console.WriteLine("4. Problem 7 - Drzewo binarne (BST)");
                Console.WriteLine("0. Wyjście");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        SortowanieZliczanieZPliku();
                        break;
                    case "2":
                        BisekcjaZPliku();
                        break;
                    case "3":
                        FibonacciZPliku();
                        break;
                    case "4":
                        DrzewoBinarneZPliku();
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

        // ==================== ZADANIE 1a SORTOWANIE PRZEZ ZLICZANIE - START ====================
        static void SortowanieZliczanieZPliku()
        {
            string nazwaWej = "In0201.txt";

            if (!File.Exists(nazwaWej))
                return;

            try
            {
                string[] linie = File.ReadAllLines(nazwaWej);
                
                if (linie.Length < 2)
                    return;

                int n = int.Parse(linie[0].Trim());
                int[] liczby = Array.ConvertAll(linie[1].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

                int[] posortowane = SortowanieZliczanie(liczby);

                // Zapisz wynik
                string nazwaWyj = nazwaWej.Replace("In", "Out");
                File.WriteAllText(nazwaWyj, string.Join(" ", posortowane));
            }
            catch (Exception)
            {
            }
        }

        static int[] SortowanieZliczanie(int[] tablica)
        {
            if (tablica.Length == 0)
                return new int[0];

            // Znajdź min i max
            int min = ZnajdzMinRekurencyjnie(tablica, 0, tablica[0]);
            int max = ZnajdzMaxRekurencyjnie(tablica, 0, tablica[0]);

            // Utwórz tablicę zliczającą
            int zakres = max - min + 1;
            int[] zliczenia = new int[zakres];

            // Zlicz wystąpienia
            ZliczRekurencyjnie(tablica, zliczenia, min, 0);

            // Zbuduj wynik
            int[] wynik = new int[tablica.Length];
            BudujWynikRekurencyjnie(zliczenia, wynik, min, 0, 0);

            return wynik;
        }

        static int ZnajdzMinRekurencyjnie(int[] tablica, int indeks, int aktualneMin)
        {
            // Warunek bazowy
            if (indeks >= tablica.Length)
                return aktualneMin;

            // Aktualizuj minimum
            int noweMin = tablica[indeks] < aktualneMin ? tablica[indeks] : aktualneMin;
            return ZnajdzMinRekurencyjnie(tablica, indeks + 1, noweMin);
        }

        static int ZnajdzMaxRekurencyjnie(int[] tablica, int indeks, int aktualneMax)
        {
            // Warunek bazowy
            if (indeks >= tablica.Length)
                return aktualneMax;

            // Aktualizuj maksimum
            int noweMax = tablica[indeks] > aktualneMax ? tablica[indeks] : aktualneMax;
            return ZnajdzMaxRekurencyjnie(tablica, indeks + 1, noweMax);
        }

        static void ZliczRekurencyjnie(int[] tablica, int[] zliczenia, int min, int indeks)
        {
            // Warunek bazowy
            if (indeks >= tablica.Length)
                return;

            // Zlicz element
            zliczenia[tablica[indeks] - min]++;

            // Wywołanie rekurencyjne
            ZliczRekurencyjnie(tablica, zliczenia, min, indeks + 1);
        }

        static int BudujWynikRekurencyjnie(int[] zliczenia, int[] wynik, int min, int indeksZliczenia, int indeksWyniku)
        {
            // Warunek bazowy
            if (indeksZliczenia >= zliczenia.Length)
                return indeksWyniku;

            // Dodaj wartości do wyniku
            indeksWyniku = DodajWartosciRekurencyjnie(wynik, indeksZliczenia + min, zliczenia[indeksZliczenia], indeksWyniku);

            // Przejdź do następnej wartości
            return BudujWynikRekurencyjnie(zliczenia, wynik, min, indeksZliczenia + 1, indeksWyniku);
        }

        static int DodajWartosciRekurencyjnie(int[] wynik, int wartosc, int ile, int indeks)
        {
            // Warunek bazowy
            if (ile <= 0)
                return indeks;

            // Dodaj wartość
            wynik[indeks] = wartosc;

            // Wywołanie rekurencyjne
            return DodajWartosciRekurencyjnie(wynik, wartosc, ile - 1, indeks + 1);
        }
        // ==================== ZADANIE 1a - KONIEC ====================

        // ==================== ZADANIE 3  CIĄG FIBONACCIEGO - START ====================
        static void FibonacciZPliku()
        {
            string nazwaWej = "In0203.txt";

            if (!File.Exists(nazwaWej))
                return;

            try
            {
                string[] linie = File.ReadAllLines(nazwaWej);
                
                if (linie.Length < 1)
                    return;

                int n = int.Parse(linie[0].Trim());
                
                List<long> fibonacci = ObliczCiagFibonacciegoDoN(n);

                // Zapisz wynik
                string nazwaWyj = nazwaWej.Replace("In", "Out");
                File.WriteAllText(nazwaWyj, $"n={n}\n{string.Join(", ", fibonacci)}");
            }
            catch (Exception)
            {
            }
        }

        static List<long> ObliczCiagFibonacciegoDoN(int n)
        {
            List<long> wynik = new List<long>();

            if (n < 0)
                return wynik;

            ObliczFibonacciRekurencyjnie(0, 1, n, wynik);

            return wynik;
        }

        static void ObliczFibonacciRekurencyjnie(long fib1, long fib2, int n, List<long> wynik)
        {
            // Warunek bazowy
            if (fib1 > n)
                return;

            // Dodaj liczbę do wyniku
            wynik.Add(fib1);

            // Wywołanie rekurencyjne
            ObliczFibonacciRekurencyjnie(fib2, fib1 + fib2, n, wynik);
        }

        static List<long> ObliczCiagFibonacciego(int n)
        {
            List<long> wynik = new List<long>();

            if (n <= 0)
                return wynik;

            if (n >= 1)
                wynik.Add(0);

            if (n >= 2)
                wynik.Add(1);

            for (int i = 2; i < n; i++)
            {
                long nastepna = wynik[i - 1] + wynik[i - 2];
                wynik.Add(nastepna);
            }

            return wynik;
        }
        // ==================== ZADANIE 3 - KONIEC ====================

        // ==================== ZADANIE 7 DrzewoBinarne - START ====================
        class WezelDrzewa
        {
            public int Wartosc { get; set; }
            public WezelDrzewa? Lewy { get; set; }
            public WezelDrzewa? Prawy { get; set; }

            public WezelDrzewa(int wartosc)
            {
                Wartosc = wartosc;
                Lewy = null;
                Prawy = null;
            }
        }

        static void DrzewoBinarneZPliku()
        {
            string nazwaWej = "In0207.txt";

            if (!File.Exists(nazwaWej))
                return;

            try
            {
                string[] linie = File.ReadAllLines(nazwaWej);
                
                if (linie.Length < 1)
                    return;

                int[] liczby = Array.ConvertAll(linie[0].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

                // Budowanie drzewa BST
                WezelDrzewa? korzen = null;
                foreach (int liczba in liczby)
                {
                    korzen = Insert(korzen, liczba);
                }

                List<int> wynik = new List<int>();
                PrzeglądKLP(korzen, wynik);

                // Zapisz wynik
                string nazwaWyj = nazwaWej.Replace("In", "Out");
                File.WriteAllText(nazwaWyj, string.Join(", ", wynik));
            }
            catch (Exception)
            {
            }
        }

        static WezelDrzewa? Insert(WezelDrzewa? korzen, int wartosc)
        {
            // Utwórz nowy węzeł jeśli drzewo puste
            if (korzen == null)
            {
                return new WezelDrzewa(wartosc);
            }

            // Wstaw do lewego poddrzewa
            if (wartosc < korzen.Wartosc)
            {
                korzen.Lewy = Insert(korzen.Lewy, wartosc);
            }
            // Wstaw do prawego poddrzewa
            else if (wartosc > korzen.Wartosc)
            {
                korzen.Prawy = Insert(korzen.Prawy, wartosc);
            }

            return korzen;
        }

        static void PrzeglądKLP(WezelDrzewa? wezel, List<int> wynik)
        {
            if (wezel == null)
                return;

            // K - Korzeń
            wynik.Add(wezel.Wartosc);
            // L - Lewy
            PrzeglądKLP(wezel.Lewy, wynik);
            // P - Prawy
            PrzeglądKLP(wezel.Prawy, wynik);
        }
        // ==================== ZADANIE 7 - KONIEC ====================

        // ==================== ZADANIE 9 Bisekcja - START ====================
        static void BisekcjaZPliku()
        {
            string nazwaWej = "In029.txt";

            if (!File.Exists(nazwaWej))
                return;

            try
            {
                string[] linie = File.ReadAllLines(nazwaWej);
                
                if (linie.Length < 2)
                    return;

                // Parsowanie danych
                string[] dane = linie[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                double a = double.Parse(dane[0], System.Globalization.CultureInfo.InvariantCulture);
                double b = double.Parse(dane[1], System.Globalization.CultureInfo.InvariantCulture);
                double epsilon = double.Parse(dane[2], System.Globalization.CultureInfo.InvariantCulture);
                
                string funkcjaStr = linie[1].Trim();

                // Oblicz pierwiastek
                double wynik = MetodaBisekcji(a, b, epsilon, funkcjaStr);

                // Zapisz wynik
                string nazwaWyj = nazwaWej.Replace("In", "Out");
                File.WriteAllText(nazwaWyj, wynik.ToString("F4"));
            }
            catch (Exception)
            {
            }
        }

        static double MetodaBisekcji(double a, double b, double epsilon, string funkcjaStr)
        {
            double fa = ObliczFunkcje(a, funkcjaStr);
            double fb = ObliczFunkcje(b, funkcjaStr);

            // Sprawdź warunek Darboux
            if (fa * fb >= 0)
            {
                throw new Exception("Funkcja nie spełnia warunku Darboux: f(a) i f(b) muszą mieć różne znaki!");
            }

            return BisekcjaRekurencyjna(a, b, fa, epsilon, funkcjaStr);
        }

        static double BisekcjaRekurencyjna(double a, double b, double fa, double epsilon, string funkcjaStr)
        {
            // Warunek bazowy
            if (Math.Abs(b - a) <= epsilon)
            {
                return (a + b) / 2.0;
            }

            // Oblicz środek przedziału
            double c = (a + b) / 2.0;
            double fc = ObliczFunkcje(c, funkcjaStr);

            // Sprawdź czy znaleźliśmy pierwiastek
            if (Math.Abs(fc) < epsilon)
            {
                return c;
            }

            // Rekurencja dla odpowiedniego podprzedziału
            if (fa * fc < 0)
            {
                return BisekcjaRekurencyjna(a, c, fa, epsilon, funkcjaStr);
            }
            else
            {
                return BisekcjaRekurencyjna(c, b, fc, epsilon, funkcjaStr);
            }
        }

        static double ObliczFunkcje(double x, string funkcja)
        {
            // Usuń spacje i zamień f(x)=
            funkcja = funkcja.Replace(" ", "").Replace("f(x)=", "").Replace("f(x)", "");

            // Zamień x na wartość
            funkcja = funkcja.Replace("x", x.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // Obsługa potęg
            while (funkcja.Contains("^"))
            {
                int indeks = funkcja.IndexOf('^');
                
                // Znajdź podstawę
                int poczatekPodstawy = indeks - 1;
                while (poczatekPodstawy > 0 && (char.IsDigit(funkcja[poczatekPodstawy - 1]) || funkcja[poczatekPodstawy - 1] == '.' || funkcja[poczatekPodstawy - 1] == '-'))
                {
                    poczatekPodstawy--;
                }
                string podstawa = funkcja.Substring(poczatekPodstawy, indeks - poczatekPodstawy);

                // Znajdź wykładnik
                int koniecWykladnika = indeks + 1;
                if (funkcja[koniecWykladnika] == '-') koniecWykladnika++;
                while (koniecWykladnika < funkcja.Length && (char.IsDigit(funkcja[koniecWykladnika]) || funkcja[koniecWykladnika] == '.'))
                {
                    koniecWykladnika++;
                }
                string wykladnik = funkcja.Substring(indeks + 1, koniecWykladnika - indeks - 1);

                // Oblicz potęgę
                double wynik = Math.Pow(double.Parse(podstawa, System.Globalization.CultureInfo.InvariantCulture), 
                                       double.Parse(wykladnik, System.Globalization.CultureInfo.InvariantCulture));

                // Zamień wyrażenie na wynik
                funkcja = funkcja.Substring(0, poczatekPodstawy) + wynik.ToString(System.Globalization.CultureInfo.InvariantCulture) + funkcja.Substring(koniecWykladnika);
            }

            // Oblicz wyrażenie
            var table = new System.Data.DataTable();
            var wynikKoncowy = table.Compute(funkcja, string.Empty);
            
            return Convert.ToDouble(wynikKoncowy);
        }
        // ==================== ZADANIE 9 - KONIEC ====================
    }
}
