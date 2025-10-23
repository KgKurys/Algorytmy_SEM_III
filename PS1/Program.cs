using System;
using System.IO;

namespace AlgorytmySEM3
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU GŁÓWNE ===");
                Console.WriteLine("1. Problem 1 - Symbol Newtona");
                Console.WriteLine("2. Problem 2 - Zbiory 2-elementowe");
                Console.WriteLine("4. Problem 4 - Sejf króla Bajtdocji");
                Console.WriteLine("0. Wyjście");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        MenuProblem1();
                        break;
                    case "2":
                        MenuProblem2();
                        break;
                    case "4":
                        MenuProblem4();
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
                Console.WriteLine("=== PROBLEM 1 - Symbol Newtona ===");
                Console.WriteLine("1. Algorytm I - Z definicji");
                Console.WriteLine("2. Algorytm V - Trójkąt Pascala (tablica 2D)");
                Console.WriteLine("3. Uruchom oba algorytmy");
                Console.WriteLine("0. Powrót");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        UruchomAlgorytm1();
                        break;
                    case "2":
                        UruchomAlgorytm5();
                        break;
                    case "3":
                        UruchomObaAlgorytmy();
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

        static void UruchomAlgorytm1()
        {
            Console.Clear();
            Console.WriteLine("=== Algorytm I - Symbol Newtona z definicji ===\n");

            var (n, k) = WczytajDane();
            var (wynik, operacje) = AlgorytmI_SymbolNewtona(n, k);

            Console.WriteLine($"\nWynik: C({n},{k}) = {wynik}");
            Console.WriteLine($"Liczba operacji elementarnych: {operacje}");

            // Zapis do pliku
            using (StreamWriter sw = new StreamWriter("Out0101.txt"))
            {
                sw.WriteLine($"n={n} k={k}");
                sw.WriteLine($"SN1 = {wynik}, liczba operacji = {operacje}");
            }

            Console.WriteLine("\nWyniki zapisano do pliku Out0101.txt");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void UruchomAlgorytm5()
        {
            Console.Clear();
            Console.WriteLine("=== Algorytm V - Symbol Newtona (Trójkąt Pascala) ===\n");

            var (n, k) = WczytajDane();
            var (wynik, operacje) = AlgorytmV_SymbolNewtona(n, k);

            Console.WriteLine($"\nWynik: C({n},{k}) = {wynik}");
            Console.WriteLine($"Liczba operacji elementarnych: {operacje}");

            // Zapis do pliku
            using (StreamWriter sw = new StreamWriter("Out0101.txt"))
            {
                sw.WriteLine($"n={n} k={k}");
                sw.WriteLine($"SN5 = {wynik}, liczba operacji = {operacje}");
            }

            Console.WriteLine("\nWyniki zapisano do pliku Out0101.txt");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void UruchomObaAlgorytmy()
        {
            Console.Clear();
            Console.WriteLine("=== Uruchomienie obu algorytmów ===\n");

            var (n, k) = WczytajDane();

            Console.WriteLine("\n--- Algorytm I (z definicji) ---");
            var (wynik1, operacje1) = AlgorytmI_SymbolNewtona(n, k);
            Console.WriteLine($"SN1 = {wynik1}, liczba operacji = {operacje1}");

            Console.WriteLine("\n--- Algorytm V (Trójkąt Pascala) ---");
            var (wynik5, operacje5) = AlgorytmV_SymbolNewtona(n, k);
            Console.WriteLine($"SN5 = {wynik5}, liczba operacji = {operacje5}");

            // Zapis do pliku wyjściowego
            ZapiszWyniki(n, k, wynik1, operacje1, wynik5, operacje5);

            Console.WriteLine("\nWyniki zapisano do pliku Out0101.txt");
            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static (int n, int k) WczytajDane()
        {
            Console.Write("Podaj ścieżkę do pliku wejściowego (lub Enter dla In0101.txt): ");
            string? sciezka = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(sciezka))
                sciezka = "In0101.txt";

            if (File.Exists(sciezka))
            {
                string linia = File.ReadAllText(sciezka).Trim();
                string[] dane = linia.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int n = int.Parse(dane[0]);
                int k = int.Parse(dane[1]);
                Console.WriteLine($"Wczytano z pliku: n = {n}, k = {k}");
                return (n, k);
            }
            else
            {
                Console.Write("Podaj n: ");
                int n = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Podaj k: ");
                int k = int.Parse(Console.ReadLine() ?? "0");
                return (n, k);
            }
        }

        static void ZapiszWyniki(int n, int k, long wynik1, int operacje1, long wynik5, int operacje5)
        {
            using (StreamWriter sw = new StreamWriter("Out0101.txt"))
            {
                sw.WriteLine($"n={n} k={k}");
                sw.WriteLine($"SN1 = {wynik1}, liczba operacji = {operacje1}");
                sw.WriteLine($"SN5 = {wynik5}, liczba operacji = {operacje5}");
            }
        }

        // ===== ALGORYTM I - Symbol Newtona z definicji =====
        // C(n,k) = n! / (k! * (n-k)!)
        // Operacja elementarna: mnożenie/dzielenie
        static (long wynik, int operacje) AlgorytmI_SymbolNewtona(int n, int k)
        {
            int operacje = 0;

            // Oblicz n!
            long nSilnia = 1;
            for (int i = 2; i <= n; i++)
            {
                nSilnia *= i;
                operacje++; // mnożenie
            }

            // Oblicz k!
            long kSilnia = 1;
            for (int i = 2; i <= k; i++)
            {
                kSilnia *= i;
                operacje++; // mnożenie
            }

            // Oblicz (n-k)!
            long nkSilnia = 1;
            for (int i = 2; i <= (n - k); i++)
            {
                nkSilnia *= i;
                operacje++; // mnożenie
            }

            // Oblicz wynik: n! / (k! * (n-k)!)
            operacje++; // mnożenie k! * (n-k)!
            operacje++; // dzielenie n! / wynik_mnozenia

            long wynik = nSilnia / (kSilnia * nkSilnia);

            return (wynik, operacje);
        }

        // ===== ALGORYTM V - Symbol Newtona przez Trójkąt Pascala (tablica 2D) - ZOPTYMALIZOWANY =====
        // Wykorzystuje własność: C(n,k) = C(n-1,k-1) + C(n-1,k)
        // Operacja elementarna: dodawanie
        // Optymalizacja: budujemy tylko do k-tej kolumny zamiast całego rzędu
        static (long wynik, int operacje) AlgorytmV_SymbolNewtona(int n, int k)
        {
            int operacje = 0;
            
            // Optymalizacja: wykorzystujemy symetrię C(n,k) = C(n, n-k)
            if (k > n - k)
                k = n - k;

            long[,] pascal = new long[n + 1, k + 1];

            // Inicjalizacja pierwszej kolumny (k=0): C(i,0) = 1
            for (int i = 0; i <= n; i++)
            {
                pascal[i, 0] = 1;
            }

            // Budowanie trójkąta Pascala (tylko do k-tej kolumny)
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= Math.Min(i, k); j++)
                {
                    pascal[i, j] = pascal[i - 1, j - 1] + pascal[i - 1, j];
                    operacje++; // dodawanie
                }
            }

            return (pascal[n, k], operacje);
        }

        // ==================== PROBLEM 2 - ZBIORY 2-ELEMENTOWE ====================

        static void MenuProblem2()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PROBLEM 2 - Zbiory 2-elementowe ===");
                Console.WriteLine("1. Algorytm I - Zachłanny (optymalny)");
                Console.WriteLine("2. Algorytm II - Naiwny");
                Console.WriteLine("3. Uruchom oba algorytmy");
                Console.WriteLine("0. Powrót");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        UruchomAlgorytmZbiorow1();
                        break;
                    case "2":
                        UruchomAlgorytmZbiorow2();
                        break;
                    case "3":
                        UruchomObaAlgorytmyZbiorow();
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

        static void UruchomAlgorytmZbiorow1()
        {
            Console.Clear();
            Console.WriteLine("=== Algorytm I - Zachłanny (optymalny) ===\n");

            var (n, k, liczby) = WczytajDaneZbiorow();
            var (zbiory, operacje) = AlgorytmI_Zbiory(n, k, liczby);

            Console.WriteLine($"\nLiczba zbiorów: {zbiory.Count}");
            Console.WriteLine($"Liczba operacji elementarnych: {operacje}");
            Console.WriteLine("\nWygenerowane zbiory:");
            foreach (var zbior in zbiory)
            {
                Console.WriteLine(string.Join(" ", zbior));
            }

            // Zapis do pliku
            ZapiszWynikiZbiorow(zbiory, "Out0102.txt");

            Console.WriteLine("\nWyniki zapisano do pliku Out0102.txt");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void UruchomAlgorytmZbiorow2()
        {
            Console.Clear();
            Console.WriteLine("=== Algorytm II - Naiwny ===\n");

            var (n, k, liczby) = WczytajDaneZbiorow();
            var (zbiory, operacje) = AlgorytmII_Zbiory(n, k, liczby);

            Console.WriteLine($"\nLiczba zbiorów: {zbiory.Count}");
            Console.WriteLine($"Liczba operacji elementarnych: {operacje}");
            Console.WriteLine("\nWygenerowane zbiory:");
            foreach (var zbior in zbiory)
            {
                Console.WriteLine(string.Join(" ", zbior));
            }

            // Zapis do pliku
            ZapiszWynikiZbiorow(zbiory, "Out0102.txt");

            Console.WriteLine("\nWyniki zapisano do pliku Out0102.txt");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void UruchomObaAlgorytmyZbiorow()
        {
            Console.Clear();
            Console.WriteLine("=== Uruchomienie obu algorytmów ===\n");

            var (n, k, liczby) = WczytajDaneZbiorow();

            Console.WriteLine("\n--- Algorytm I (Zachłanny - optymalny) ---");
            var (zbiory1, operacje1) = AlgorytmI_Zbiory(n, k, liczby.ToArray());
            Console.WriteLine($"Liczba zbiorów: {zbiory1.Count}");
            Console.WriteLine($"Liczba operacji: {operacje1}");

            Console.WriteLine("\n--- Algorytm II (Naiwny) ---");
            var (zbiory2, operacje2) = AlgorytmII_Zbiory(n, k, liczby.ToArray());
            Console.WriteLine($"Liczba zbiorów: {zbiory2.Count}");
            Console.WriteLine($"Liczba operacji: {operacje2}");

            // Zapis algorytmu optymalnego do pliku
            ZapiszWynikiZbiorow(zbiory1, "Out0102.txt");

            Console.WriteLine("\nWyniki zapisano do pliku Out0102.txt");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static (int n, int k, int[] liczby) WczytajDaneZbiorow()
        {
            Console.Write("Podaj ścieżkę do pliku wejściowego (lub Enter dla In0102.txt): ");
            string? sciezka = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(sciezka))
                sciezka = "In0102.txt";

            if (File.Exists(sciezka))
            {
                string[] linie = File.ReadAllLines(sciezka);
                string[] pierwszaLinia = linie[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int n = int.Parse(pierwszaLinia[0]);
                int k = int.Parse(pierwszaLinia[1]);

                int[] liczby = new int[n];
                for (int i = 0; i < n; i++)
                {
                    liczby[i] = int.Parse(linie[i + 1].Trim());
                }

                Console.WriteLine($"Wczytano z pliku: n = {n}, k = {k}");
                Console.WriteLine($"Liczby: {string.Join(", ", liczby)}");
                return (n, k, liczby);
            }
            else
            {
                Console.Write("Podaj n: ");
                int n = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Podaj k: ");
                int k = int.Parse(Console.ReadLine() ?? "0");

                int[] liczby = new int[n];
                for (int i = 0; i < n; i++)
                {
                    Console.Write($"Podaj liczbę {i + 1}: ");
                    liczby[i] = int.Parse(Console.ReadLine() ?? "0");
                }

                return (n, k, liczby);
            }
        }

        static void ZapiszWynikiZbiorow(List<List<int>> zbiory, string nazwaPliku)
        {
            using (StreamWriter sw = new StreamWriter(nazwaPliku))
            {
                foreach (var zbior in zbiory)
                {
                    sw.WriteLine(string.Join(" ", zbior));
                }
                sw.WriteLine(zbiory.Count);
            }
        }

        // ===== ALGORYTM I - Zachłanny (optymalny) =====
        // Sortowanie + Two Pointers
        // Operacja elementarna: porównanie
        // Złożoność: O(n log n) sortowanie + O(n) parowanie
        static (List<List<int>> zbiory, int operacje) AlgorytmI_Zbiory(int n, int k, int[] liczby)
        {
            int operacje = 0;
            List<List<int>> zbiory = new List<List<int>>();

            // Kopiujemy tablicę aby nie modyfikować oryginału
            int[] sorted = new int[n];
            Array.Copy(liczby, sorted, n);

            // Sortowanie (używamy wbudowanego - złożoność O(n log n))
            Array.Sort(sorted);
            // Operacje sortowania: n * log(n) porównań
            operacje += (int)(n * Math.Log(n, 2));

            bool[] uzyte = new bool[n];
            int left = 0;
            int right = n - 1;

            // Two pointers - łączenie najmniejszej z największą
            while (left <= right)
            {
                operacje++; // porównanie left <= right

                if (left == right)
                {
                    // Ostatni element - musi być sam
                    if (!uzyte[left])
                    {
                        zbiory.Add(new List<int> { sorted[left] });
                    }
                    break;
                }

                operacje++; // porównanie sumy z k
                if (sorted[left] + sorted[right] <= k)
                {
                    // Możemy utworzyć parę
                    zbiory.Add(new List<int> { sorted[left], sorted[right] });
                    uzyte[left] = true;
                    uzyte[right] = true;
                    left++;
                    right--;
                }
                else
                {
                    // Największa liczba nie może mieć pary - idzie sama
                    zbiory.Add(new List<int> { sorted[right] });
                    uzyte[right] = true;
                    right--;
                }
            }

            return (zbiory, operacje);
        }

        // ===== ALGORYTM II - Naiwny =====
        // Dla każdej liczby szuka pary liniowo
        // Operacja elementarna: porównanie
        // Złożoność: O(n²)
        static (List<List<int>> zbiory, int operacje) AlgorytmII_Zbiory(int n, int k, int[] liczby)
        {
            int operacje = 0;
            List<List<int>> zbiory = new List<List<int>>();
            bool[] uzyte = new bool[n];

            for (int i = 0; i < n; i++)
            {
                operacje++; // sprawdzenie czy użyte
                if (uzyte[i]) continue;

                bool znalezionoPare = false;

                // Szukaj pary dla liczby[i]
                for (int j = i + 1; j < n; j++)
                {
                    operacje++; // sprawdzenie czy użyte
                    if (uzyte[j]) continue;

                    operacje++; // porównanie sumy z k
                    if (liczby[i] + liczby[j] <= k)
                    {
                        // Znaleziono parę
                        zbiory.Add(new List<int> { liczby[i], liczby[j] });
                        uzyte[i] = true;
                        uzyte[j] = true;
                        znalezionoPare = true;
                        break;
                    }
                }

                if (!znalezionoPare && !uzyte[i])
                {
                    // Nie znaleziono pary - zbiór 1-elementowy
                    zbiory.Add(new List<int> { liczby[i] });
                    uzyte[i] = true;
                }
            }

            return (zbiory, operacje);
        }

        // ==================== PROBLEM 4 - SEJF KRÓLA BAJTDOCJI ====================

        static void MenuProblem4()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PROBLEM 4 - Sejf króla Bajtdocji ===");
                Console.WriteLine("1. Algorytm I - Scalanie przedziałów (optymalny)");
                Console.WriteLine("2. Algorytm II - Brute force");
                Console.WriteLine("3. Uruchom oba algorytmy");
                Console.WriteLine("0. Powrót");
                Console.Write("\nWybierz opcję: ");

                string? wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        UruchomAlgorytmSejfu1();
                        break;
                    case "2":
                        UruchomAlgorytmSejfu2();
                        break;
                    case "3":
                        UruchomObaAlgorytmySejfu();
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

        static void UruchomAlgorytmSejfu1()
        {
            Console.Clear();
            Console.WriteLine("=== Algorytm I - Scalanie przedziałów (optymalny) ===\n");

            var (n, prety) = WczytajDaneSejfu();
            var (pasma, operacje) = AlgorytmI_Sejf(n, prety);

            Console.WriteLine($"\nLiczba bezpiecznych pasm: {pasma.Count}");
            Console.WriteLine($"Liczba operacji elementarnych: {operacje}");
            Console.WriteLine("\nBezpieczne pasma:");
            foreach (var pasmo in pasma)
            {
                Console.WriteLine($"{pasmo.Item1} {pasmo.Item2}");
            }

            // Zapis do pliku
            ZapiszWynikiSejfu(pasma, "Out0104.txt");

            Console.WriteLine("\nWyniki zapisano do pliku Out0104.txt");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void UruchomAlgorytmSejfu2()
        {
            Console.Clear();
            Console.WriteLine("=== Algorytm II - Brute force ===\n");

            var (n, prety) = WczytajDaneSejfu();
            var (pasma, operacje) = AlgorytmII_Sejf(n, prety);

            Console.WriteLine($"\nLiczba bezpiecznych pasm: {pasma.Count}");
            Console.WriteLine($"Liczba operacji elementarnych: {operacje}");
            Console.WriteLine("\nBezpieczne pasma:");
            foreach (var pasmo in pasma)
            {
                Console.WriteLine($"{pasmo.Item1} {pasmo.Item2}");
            }

            // Zapis do pliku
            ZapiszWynikiSejfu(pasma, "Out0104.txt");

            Console.WriteLine("\nWyniki zapisano do pliku Out0104.txt");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void UruchomObaAlgorytmySejfu()
        {
            Console.Clear();
            Console.WriteLine("=== Uruchomienie obu algorytmów ===\n");

            var (n, prety) = WczytajDaneSejfu();

            Console.WriteLine("\n--- Algorytm I (Scalanie przedziałów - optymalny) ---");
            var (pasma1, operacje1) = AlgorytmI_Sejf(n, prety.ToArray());
            Console.WriteLine($"Liczba bezpiecznych pasm: {pasma1.Count}");
            Console.WriteLine($"Liczba operacji: {operacje1}");

            Console.WriteLine("\n--- Algorytm II (Brute force) ---");
            var (pasma2, operacje2) = AlgorytmII_Sejf(n, prety.ToArray());
            Console.WriteLine($"Liczba bezpiecznych pasm: {pasma2.Count}");
            Console.WriteLine($"Liczba operacji: {operacje2}");

            // Zapis algorytmu optymalnego do pliku
            ZapiszWynikiSejfu(pasma1, "Out0104.txt");

            Console.WriteLine("\nWyniki zapisano do pliku Out0104.txt");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static (int n, (int x1, int y1, int x2, int y2)[] prety) WczytajDaneSejfu()
        {
            Console.Write("Podaj ścieżkę do pliku wejściowego (lub Enter dla In0104.txt): ");
            string? sciezka = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(sciezka))
                sciezka = "In0104.txt";

            if (File.Exists(sciezka))
            {
                string[] linie = File.ReadAllLines(sciezka);
                string[] pierwszaLinia = linie[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int n = int.Parse(pierwszaLinia[0]);
                int m = int.Parse(pierwszaLinia[1]);

                var prety = new (int x1, int y1, int x2, int y2)[m];
                for (int i = 0; i < m; i++)
                {
                    string[] dane = linie[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    prety[i] = (
                        int.Parse(dane[0]),
                        int.Parse(dane[1]),
                        int.Parse(dane[2]),
                        int.Parse(dane[3])
                    );
                }

                Console.WriteLine($"Wczytano z pliku: n = {n}, m = {m}");
                return (n, prety);
            }
            else
            {
                Console.Write("Podaj n (szerokość korytarza): ");
                int n = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Podaj m (liczba prętów): ");
                int m = int.Parse(Console.ReadLine() ?? "0");

                var prety = new (int x1, int y1, int x2, int y2)[m];
                for (int i = 0; i < m; i++)
                {
                    Console.WriteLine($"Pręt {i + 1}:");
                    Console.Write("  x1: ");
                    int x1 = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("  y1: ");
                    int y1 = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("  x2: ");
                    int x2 = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("  y2: ");
                    int y2 = int.Parse(Console.ReadLine() ?? "0");
                    prety[i] = (x1, y1, x2, y2);
                }

                return (n, prety);
            }
        }

        static void ZapiszWynikiSejfu(List<(int y1, int y2)> pasma, string nazwaPliku)
        {
            using (StreamWriter sw = new StreamWriter(nazwaPliku))
            {
                foreach (var pasmo in pasma)
                {
                    sw.WriteLine($"{pasmo.y1} {pasmo.y2}");
                }
                sw.WriteLine($"liczba bezpiecznych pasm: {pasma.Count}");
            }
        }

        // ===== ALGORYTM I - Scalanie przedziałów (optymalny) =====
        // 1. Zbierz zajęte pasma (y1, y2) z wszystkich prętów
        // 2. Posortuj przedziały według punktu początkowego
        // 3. Scal nakładające się przedziały
        // 4. Znajdź luki między scalonymi przedziałami
        // Operacja elementarna: porównanie
        // Złożoność: O(m log m) sortowanie + O(m) scalanie + O(m) szukanie luk = O(m log m)
        static (List<(int y1, int y2)> pasma, int operacje) AlgorytmI_Sejf(int n, (int x1, int y1, int x2, int y2)[] prety)
        {
            int operacje = 0;
            List<(int y1, int y2)> bezpiecznePasma = new List<(int, int)>();

            // Krok 1: Zbierz zajęte przedziały (y1, y2)
            List<(int y1, int y2)> zajetePasma = new List<(int, int)>();
            for (int i = 0; i < prety.Length; i++)
            {
                zajetePasma.Add((prety[i].y1, prety[i].y2));
            }

            if (zajetePasma.Count == 0)
            {
                // Brak prętów - cały korytarz bezpieczny
                if (n > 0)
                    bezpiecznePasma.Add((0, n));
                return (bezpiecznePasma, operacje);
            }

            // Krok 2: Sortuj przedziały według punktu początkowego
            zajetePasma.Sort((a, b) => a.y1.CompareTo(b.y1));
            operacje += (int)(prety.Length * Math.Log(prety.Length, 2)); // operacje sortowania

            // Krok 3: Scal nakładające się przedziały
            List<(int y1, int y2)> scalone = new List<(int, int)>();
            var current = zajetePasma[0];

            for (int i = 1; i < zajetePasma.Count; i++)
            {
                operacje++; // porównanie czy przedziały się nakładają
                if (zajetePasma[i].y1 <= current.y2)
                {
                    // Nakładają się lub stykają - scal
                    operacje++; // porównanie max
                    current = (current.y1, Math.Max(current.y2, zajetePasma[i].y2));
                }
                else
                {
                    // Nie nakładają się - zapisz current i rozpocznij nowy
                    scalone.Add(current);
                    current = zajetePasma[i];
                }
            }
            scalone.Add(current); // dodaj ostatni

            // Krok 4: Znajdź luki (bezpieczne pasma)
            int pozycja = 0;

            foreach (var zajete in scalone)
            {
                operacje++; // porównanie
                if (pozycja < zajete.y1)
                {
                    // Jest luka przed zajętym pasmem
                    bezpiecznePasma.Add((pozycja, zajete.y1));
                }
                operacje++; // aktualizacja pozycji
                pozycja = Math.Max(pozycja, zajete.y2);
            }

            // Sprawdź czy jest luka na końcu
            operacje++;
            if (pozycja < n)
            {
                bezpiecznePasma.Add((pozycja, n));
            }

            return (bezpiecznePasma, operacje);
        }

        // ===== ALGORYTM II - Brute force =====
        // Dla każdego punktu y w przedziale [0, n] sprawdza czy jest bezpieczny
        // Operacja elementarna: porównanie
        // Złożoność: O(n * m) - dla każdego punktu sprawdzamy wszystkie pręty
        static (List<(int y1, int y2)> pasma, int operacje) AlgorytmII_Sejf(int n, (int x1, int y1, int x2, int y2)[] prety)
        {
            int operacje = 0;
            List<(int y1, int y2)> bezpiecznePasma = new List<(int, int)>();

            bool[] bezpieczny = new bool[n + 1];
            for (int i = 0; i <= n; i++)
            {
                bezpieczny[i] = true; // początkowo wszystkie punkty bezpieczne
            }

            // Dla każdego pręta oznacz zajęte punkty
            foreach (var pret in prety)
            {
                for (int y = pret.y1; y <= pret.y2; y++)
                {
                    operacje++; // sprawdzenie i oznaczenie
                    if (y <= n)
                        bezpieczny[y] = false;
                }
            }

            // Zbierz przedziały bezpiecznych punktów
            int poczatek = -1;
            for (int y = 0; y <= n; y++)
            {
                operacje++; // sprawdzenie
                if (bezpieczny[y])
                {
                    if (poczatek == -1)
                    {
                        poczatek = y;
                    }
                }
                else
                {
                    if (poczatek != -1)
                    {
                        bezpiecznePasma.Add((poczatek, y));
                        poczatek = -1;
                    }
                }
            }

            // Jeśli ostatnie punkty były bezpieczne
            if (poczatek != -1)
            {
                bezpiecznePasma.Add((poczatek, n));
            }

            return (bezpiecznePasma, operacje);
        }
    }
}
