
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
                        MenuSortowanieZliczanie();
                        break;
                    case "2":
                        MenuProblem9();
                        break;
                    case "3":
                        MenuProblem3();
                        break;
                    case "4":
                        MenuProblem7();
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

        static void MenuProblem1()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PROBLEM 1 - SORTOWANIE ===");
                Console.WriteLine("a. Sortowanie przez zliczanie");
                Console.WriteLine("0. Powrót");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "a":
                        MenuSortowanieZliczanie();
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

        static void MenuProblem3()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PROBLEM 3 - CIĄG FIBONACCIEGO ===");
                Console.WriteLine("1. Uruchom z pliku");
                Console.WriteLine("2. Wprowadź dane ręcznie");
                Console.WriteLine("0. Powrót");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        FibonacciZPliku();
                        break;
                    case "2":
                        FibonacciRecznie();
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

        static void MenuProblem9()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PROBLEM 9 - METODA BISEKCJI ===");
                Console.WriteLine("1. Uruchom z pliku");
                Console.WriteLine("2. Wprowadź dane ręcznie");
                Console.WriteLine("0. Powrót");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        BisekcjaZPliku();
                        break;
                    case "2":
                        BisekcjaRecznie();
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

        static void MenuSortowanieZliczanie()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SORTOWANIE PRZEZ ZLICZANIE ===");
                Console.WriteLine("1. Uruchom z pliku");
                Console.WriteLine("2. Wprowadź dane ręcznie");
                Console.WriteLine("0. Powrót");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        SortowanieZliczanieZPliku();
                        break;
                    case "2":
                        SortowanieZliczanieRecznie();
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

        static void SortowanieZliczanieZPliku()
        {
            string nazwaWej = "In0201.txt";

            if (!File.Exists(nazwaWej))
            {
                Console.WriteLine($"Plik {nazwaWej} nie istnieje!");
                Console.ReadKey();
                return;
            }

            try
            {
                string[] linie = File.ReadAllLines(nazwaWej);
                
                if (linie.Length < 2)
                {
                    Console.WriteLine("Plik jest nieprawidłowy!");
                    Console.ReadKey();
                    return;
                }

                int n = int.Parse(linie[0].Trim());
                int[] liczby = Array.ConvertAll(linie[1].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

                Console.WriteLine($"\nLiczba elementów n: {n}");
                Console.WriteLine($"Dane wejściowe: {string.Join(" ", liczby)}");

                int[] posortowane = SortowanieZliczanie(liczby);

                Console.WriteLine($"Dane posortowane: {string.Join(" ", posortowane)}");

                // Zapisz wynik
                string nazwaWyj = nazwaWej.Replace("In", "Out");
                File.WriteAllText(nazwaWyj, string.Join(" ", posortowane));
                Console.WriteLine($"\nWynik zapisano do pliku: {nazwaWyj}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void SortowanieZliczanieRecznie()
        {
            Console.Write("Podaj ilość liczb: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Nieprawidłowa liczba!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Podaj {n} liczb oddzielonych spacją (zakres -10000...10000):");
            string? input = Console.ReadLine();
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Nie podano danych!");
                Console.ReadKey();
                return;
            }

            try
            {
                int[] liczby = Array.ConvertAll(input.Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

                if (liczby.Length != n)
                {
                    Console.WriteLine($"Podano {liczby.Length} liczb, a oczekiwano {n}!");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nDane wejściowe: {string.Join(" ", liczby)}");

                int[] posortowane = SortowanieZliczanie(liczby);

                Console.WriteLine($"Dane posortowane: {string.Join(" ", posortowane)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        /// 
        /// Sortowanie przez zliczanie (Counting Sort)
        /// Algorytm działa dla liczb z określonego zakresu
        /// 
        static int[] SortowanieZliczanie(int[] tablica)
        {
            if (tablica.Length == 0)
                return new int[0];

            // Znajdź wartości min i max
            int min = tablica[0];
            int max = tablica[0];
            
            for (int i = 1; i < tablica.Length; i++)
            {
                if (tablica[i] < min) min = tablica[i];
                if (tablica[i] > max) max = tablica[i];
            }

            // Rozmiar tablicy zliczającej
            int zakres = max - min + 1;
            int[] zliczenia = new int[zakres];

            // Zlicz wystąpienia każdej wartości
            for (int i = 0; i < tablica.Length; i++)
            {
                zliczenia[tablica[i] - min]++;
            }

            // Zbuduj posortowaną tablicę
            int[] wynik = new int[tablica.Length];
            int indeks = 0;

            for (int i = 0; i < zakres; i++)
            {
                for (int j = 0; j < zliczenia[i]; j++)
                {
                    wynik[indeks++] = i + min;
                }
            }

            return wynik;
        }

        // ========== PROBLEM 3 - CIĄG FIBONACCIEGO ==========

        static void FibonacciZPliku()
        {
            string nazwaWej = "In0203.txt";

            if (!File.Exists(nazwaWej))
            {
                Console.WriteLine($"Plik {nazwaWej} nie istnieje!");
                Console.ReadKey();
                return;
            }

            try
            {
                string[] linie = File.ReadAllLines(nazwaWej);
                
                if (linie.Length < 1)
                {
                    Console.WriteLine("Plik jest nieprawidłowy!");
                    Console.ReadKey();
                    return;
                }

                int n = int.Parse(linie[0].Trim());

                Console.WriteLine($"n = {n}");
                
                List<long> fibonacci = ObliczCiagFibonacciegoDoN(n);

                Console.WriteLine($"Ciąg Fibonacciego (liczby <= {n}):");
                Console.WriteLine(string.Join(", ", fibonacci));

                // Zapisz wynik
                string nazwaWyj = nazwaWej.Replace("In", "Out");
                File.WriteAllText(nazwaWyj, $"n={n}\n{string.Join(", ", fibonacci)}");
                Console.WriteLine($"\nWynik zapisano do pliku: {nazwaWyj}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void FibonacciRecznie()
        {
            try
            {
                Console.Write("Podaj n (maksymalna wartość liczby Fibonacciego, 1 <= n <= 10000): ");
                if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > 10000)
                {
                    Console.WriteLine("Nieprawidłowa wartość n!");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nn = {n}");
                
                List<long> fibonacci = ObliczCiagFibonacciegoDoN(n);

                Console.WriteLine($"Ciąg Fibonacciego (liczby <= {n}):");
                Console.WriteLine(string.Join(", ", fibonacci));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }


        static List<long> ObliczCiagFibonacciegoDoN(int n)
        {
            List<long> wynik = new List<long>();

            if (n < 0)
                return wynik;

            long fib1 = 0;
            long fib2 = 1;

            // Dodaj 0 jeśli n >= 0
            if (n >= 0)
            {
                wynik.Add(fib1);
            }

            // Dodaj kolejne liczby Fibonacciego dopóki nie przekroczą n
            while (fib2 <= n)
            {
                wynik.Add(fib2);
                long temp = fib1 + fib2;
                fib1 = fib2;
                fib2 = temp;
            }

            return wynik;
        }


        static List<long> ObliczCiagFibonacciego(int n)
        {
            List<long> wynik = new List<long>();

            if (n <= 0)
                return wynik;

            if (n >= 1)
                wynik.Add(0); // F(0) = 0

            if (n >= 2)
                wynik.Add(1); // F(1) = 1

            // Obliczamy kolejne liczby iteracyjnie
            for (int i = 2; i < n; i++)
            {
                long nastepna = wynik[i - 1] + wynik[i - 2];
                wynik.Add(nastepna);
            }

            return wynik;
        }

        // ========== PROBLEM 7 - DRZEWO BINARNE (BST) ==========

        // Klasa reprezentująca węzeł drzewa binarnego
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

        static void MenuProblem7()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PROBLEM 7 - DRZEWO BINARNE (BST) ===");
                Console.WriteLine("1. Uruchom z pliku");
                Console.WriteLine("2. Wprowadź dane ręcznie");
                Console.WriteLine("0. Powrót");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        DrzewoBinarneZPliku();
                        break;
                    case "2":
                        DrzewoBinarneRecznie();
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

        static void DrzewoBinarneZPliku()
        {
            string nazwaWej = "In0207.txt";

            if (!File.Exists(nazwaWej))
            {
                Console.WriteLine($"Plik {nazwaWej} nie istnieje!");
                Console.ReadKey();
                return;
            }

            try
            {
                string[] linie = File.ReadAllLines(nazwaWej);
                
                if (linie.Length < 1)
                {
                    Console.WriteLine("Plik jest nieprawidłowy!");
                    Console.ReadKey();
                    return;
                }

                int[] liczby = Array.ConvertAll(linie[0].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

                Console.WriteLine($"Dane wejściowe: {string.Join(" ", liczby)}");
                Console.WriteLine($"Wstawianie elementów do drzewa BST...\n");

                // Budowanie drzewa BST
                WezelDrzewa? korzen = null;
                foreach (int liczba in liczby)
                {
                    korzen = Insert(korzen, liczba);
                    Console.WriteLine($"Wstawiono: {liczba}");
                }

                Console.WriteLine("\n=== PRZEGLĄD DRZEWA ===");
                Console.WriteLine("Przegląd KLP (korzeń-lewy-prawy) - preorder:");
                List<int> wynik = new List<int>();
                PrzeglądKLP(korzen, wynik);
                Console.WriteLine(string.Join(", ", wynik));

                // Zapisz wynik
                string nazwaWyj = nazwaWej.Replace("In", "Out");
                File.WriteAllText(nazwaWyj, string.Join(", ", wynik));
                Console.WriteLine($"\nWynik zapisano do pliku: {nazwaWyj}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void DrzewoBinarneRecznie()
        {
            try
            {
                Console.WriteLine("Podaj liczby do wstawienia do drzewa BST (oddzielone spacją):");
                string? input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Nie podano danych!");
                    Console.ReadKey();
                    return;
                }

                int[] liczby = Array.ConvertAll(input.Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

                Console.WriteLine($"\nDane wejściowe: {string.Join(" ", liczby)}");
                Console.WriteLine($"Wstawianie elementów do drzewa BST...\n");

                // Budowanie drzewa BST
                WezelDrzewa? korzen = null;
                foreach (int liczba in liczby)
                {
                    korzen = Insert(korzen, liczba);
                    Console.WriteLine($"Wstawiono: {liczba}");
                }

                Console.WriteLine("\n=== PRZEGLĄD DRZEWA ===");
                Console.WriteLine("Przegląd KLP (korzeń-lewy-prawy) - preorder:");
                List<int> wynik = new List<int>();
                PrzeglądKLP(korzen, wynik);
                Console.WriteLine(string.Join(", ", wynik));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }


        static WezelDrzewa? Insert(WezelDrzewa? korzen, int wartosc)
        {
            // Jeśli drzewo jest puste, utwórz nowy węzeł
            if (korzen == null)
            {
                return new WezelDrzewa(wartosc);
            }

            // Rekurencyjnie wstaw do odpowiedniego poddrzewa
            if (wartosc < korzen.Wartosc)
            {
                korzen.Lewy = Insert(korzen.Lewy, wartosc);
            }
            else if (wartosc > korzen.Wartosc)
            {
                korzen.Prawy = Insert(korzen.Prawy, wartosc);
            }
            // Jeśli wartość już istnieje, nie wstawiamy duplikatu

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

        // ========== PROBLEM 9 - METODA BISEKCJI ==========

        static void BisekcjaZPliku()
        {
            string nazwaWej = "In029.txt";

            if (!File.Exists(nazwaWej))
            {
                Console.WriteLine($"Plik {nazwaWej} nie istnieje!");
                Console.ReadKey();
                return;
            }

            try
            {
                string[] linie = File.ReadAllLines(nazwaWej);
                
                if (linie.Length < 2)
                {
                    Console.WriteLine("Plik jest nieprawidłowy!");
                    Console.ReadKey();
                    return;
                }

                // Parsowanie danych wejściowych
                string[] dane = linie[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                double a = double.Parse(dane[0], System.Globalization.CultureInfo.InvariantCulture);
                double b = double.Parse(dane[1], System.Globalization.CultureInfo.InvariantCulture);
                double epsilon = double.Parse(dane[2], System.Globalization.CultureInfo.InvariantCulture);
                
                string funkcjaStr = linie[1].Trim();

                Console.WriteLine($"Przedział: [{a}, {b}]");
                Console.WriteLine($"Dokładność E: {epsilon}");
                Console.WriteLine($"Funkcja: f(x) = {funkcjaStr}");

                // Obliczanie pierwiastka metodą bisekcji
                double wynik = MetodaBisekcji(a, b, epsilon, funkcjaStr);

                Console.WriteLine($"\nPrzybliżony pierwiastek: {wynik}");
                Console.WriteLine($"f({wynik}) = {ObliczFunkcje(wynik, funkcjaStr)}");

                // Zapisz wynik
                string nazwaWyj = nazwaWej.Replace("In", "Out");
                File.WriteAllText(nazwaWyj, wynik.ToString("F4"));
                Console.WriteLine($"\nWynik zapisano do pliku: {nazwaWyj}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void BisekcjaRecznie()
        {
            try
            {
                Console.Write("Podaj początek przedziału a: ");
                double a = double.Parse(Console.ReadLine() ?? "0");

                Console.Write("Podaj koniec przedziału b: ");
                double b = double.Parse(Console.ReadLine() ?? "0");

                Console.Write("Podaj dokładność E: ");
                double epsilon = double.Parse(Console.ReadLine() ?? "0.01");

                Console.WriteLine("Podaj funkcję f(x) (np. x^2-2):");
                string funkcjaStr = Console.ReadLine()?.Trim() ?? "";

                Console.WriteLine($"\nPrzedział: [{a}, {b}]");
                Console.WriteLine($"Dokładność E: {epsilon}");
                Console.WriteLine($"Funkcja: f(x) = {funkcjaStr}");

                // Obliczanie pierwiastka metodą bisekcji
                double wynik = MetodaBisekcji(a, b, epsilon, funkcjaStr);

                Console.WriteLine($"\nPrzybliżony pierwiastek: {wynik}");
                Console.WriteLine($"f({wynik}) = {ObliczFunkcje(wynik, funkcjaStr)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static double MetodaBisekcji(double a, double b, double epsilon, string funkcjaStr)
        {
            double fa = ObliczFunkcje(a, funkcjaStr);
            double fb = ObliczFunkcje(b, funkcjaStr);

            // Sprawdzenie warunku Darboux (f(a)*f(b) < 0)
            if (fa * fb >= 0)
            {
                throw new Exception("Funkcja nie spełnia warunku Darboux: f(a) i f(b) muszą mieć różne znaki!");
            }

            double c = 0;
            int iteracja = 0;

            Console.WriteLine("\nIteracje:");
            Console.WriteLine("Nr\ta\t\tb\t\tc\t\tf(c)");
            Console.WriteLine(new string('-', 70));

            while (Math.Abs(b - a) > epsilon)
            {
                c = (a + b) / 2.0;
                double fc = ObliczFunkcje(c, funkcjaStr);

                iteracja++;
                Console.WriteLine($"{iteracja}\t{a:F4}\t\t{b:F4}\t\t{c:F4}\t\t{fc:F6}");

                // Jeśli f(c) jest wystarczająco bliskie 0, zwróć c
                if (Math.Abs(fc) < epsilon)
                {
                    return c;
                }

                // Sprawdź, w której połowie przedziału znajduje się pierwiastek
                if (fa * fc < 0)
                {
                    // Pierwiastek jest w przedziale [a, c]
                    b = c;
                    fb = fc;
                }
                else
                {
                    // Pierwiastek jest w przedziale [c, b]
                    a = c;
                    fa = fc;
                }
            }

            return c;
        }


        static double ObliczFunkcje(double x, string funkcja)
        {
            // Usunięcie spacji i zamiana f(x)= na pustą wartość
            funkcja = funkcja.Replace(" ", "").Replace("f(x)=", "").Replace("f(x)", "");

            // Zamiana x na wartość
            funkcja = funkcja.Replace("x", x.ToString(System.Globalization.CultureInfo.InvariantCulture));

            // Obsługa potęg (^ na Math.Pow)
            while (funkcja.Contains("^"))
            {
                int indeks = funkcja.IndexOf('^');
                
                // Znajdź podstawę (liczba przed ^)
                int poczatekPodstawy = indeks - 1;
                while (poczatekPodstawy > 0 && (char.IsDigit(funkcja[poczatekPodstawy - 1]) || funkcja[poczatekPodstawy - 1] == '.' || funkcja[poczatekPodstawy - 1] == '-'))
                {
                    poczatekPodstawy--;
                }
                string podstawa = funkcja.Substring(poczatekPodstawy, indeks - poczatekPodstawy);

                // Znajdź wykładnik (liczba po ^)
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

            // Użyj DataTable.Compute do obliczenia wyrażenia matematycznego
            var table = new System.Data.DataTable();
            var wynikKoncowy = table.Compute(funkcja, string.Empty);
            
            return Convert.ToDouble(wynikKoncowy);
        }
    }
}
