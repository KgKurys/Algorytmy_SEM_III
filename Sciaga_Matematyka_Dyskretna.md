# ŚCIĄGA - MATEMATYKA DYSKRETNA
## Algorytmy i Struktury Danych - Pracownia Specjalistyczna

---

## SPIS TREŚCI
1. [Problem 1 - Symbol Newtona](#problem-1---symbol-newtona)
2. [Problem 2 - Zbiory 2-elementowe](#problem-2---zbiory-2-elementowe)
3. [Problem 4 - Sejf króla Bajtdocji](#problem-4---sejf-króla-bajtdocji)
4. [Złożoności czasowe - podsumowanie](#złożoności-czasowe---podsumowanie)

---

## PROBLEM 1 - SYMBOL NEWTONA

### TEORIA MATEMATYCZNA - PODSTAWY

#### Co to jest Symbol Newtona?

Symbol Newtona (nazywany też **współczynnikiem dwumianowym**) to liczba naturalna oznaczana jako $\binom{n}{k}$ (czytamy: "n nad k" lub "n po k").

**Definicja matematyczna:**
$$\binom{n}{k} = \frac{n!}{k! \cdot (n-k)!}$$

gdzie:
- $n$ - liczba całkowita nieujemna (liczba wszystkich elementów)
- $k$ - liczba całkowita nieujemna, gdzie $0 \leq k \leq n$ (liczba elementów do wyboru)
- $n!$ (n silnia) - iloczyn wszystkich liczb naturalnych od 1 do n

#### Czym jest silnia?

**Silnia** (oznaczana wykrzyknikiem "!") to iloczyn wszystkich liczb naturalnych od 1 do danej liczby:

$$n! = 1 \cdot 2 \cdot 3 \cdot 4 \cdot \ldots \cdot (n-1) \cdot n$$

**Przykłady:**
- $0! = 1$ (z definicji - to bardzo ważne!)
- $1! = 1$
- $2! = 1 \cdot 2 = 2$
- $3! = 1 \cdot 2 \cdot 3 = 6$
- $4! = 1 \cdot 2 \cdot 3 \cdot 4 = 24$
- $5! = 1 \cdot 2 \cdot 3 \cdot 4 \cdot 5 = 120$
- $8! = 1 \cdot 2 \cdot 3 \cdot 4 \cdot 5 \cdot 6 \cdot 7 \cdot 8 = 40320$

**Dlaczego 0! = 1?** To matematyczna konwencja, która sprawia, że wiele wzorów działa poprawnie. Można to rozumieć jako "liczbę sposobów ustawienia zera elementów" - jest dokładnie jeden sposób: nie robić nic.

#### Interpretacja kombinatoryczna (co to naprawdę oznacza?)

Symbol Newtona $\binom{n}{k}$ odpowiada na pytanie:

**"Na ile różnych sposobów mogę wybrać k elementów z n-elementowego zbioru?"**

przy czym:
- Kolejność wyboru **NIE MA ZNACZENIA** (wybór {A, B, C} to to samo co {C, A, B})
- Jeden element możemy wybrać **TYLKO RAZ** (nie ma powtórzeń)

**Przykład życiowy:**
Masz 8 piłkarzy i chcesz wybrać 3 do drużyny. Na ile sposobów możesz to zrobić?

$$\binom{8}{3} = \frac{8!}{3! \cdot 5!} = \frac{40320}{6 \cdot 120} = \frac{40320}{720} = 56$$

Odpowiedź: Na 56 różnych sposobów.

**Szczegółowe obliczenie:**
1. $8! = 40320$ - wszystkie możliwe ustawienia 8 osób
2. $3! = 6$ - wszystkie ustawienia 3 wybranych osób (dzielimy, bo kolejność nie ma znaczenia)
3. $5! = 120$ - wszystkie ustawienia 5 niewybranych osób (dzielimy, bo nie interesuje nas ich kolejność)
4. $\frac{40320}{6 \cdot 120} = \frac{40320}{720} = 56$

#### Podstawowe własności Symbolu Newtona

**Własność 1: Wybór 0 lub wszystkich elementów**
$$\binom{n}{0} = \binom{n}{n} = 1$$

**Wyjaśnienie:**
- $\binom{n}{0} = 1$ - jest dokładnie JEDEN sposób na wybranie zera elementów (nie wybierać nic)
- $\binom{n}{n} = 1$ - jest dokładnie JEDEN sposób na wybranie wszystkich elementów (wybrać wszystkie)

**Przykład:** $\binom{5}{0} = \frac{5!}{0! \cdot 5!} = \frac{120}{1 \cdot 120} = 1$

**Własność 2: Symetria**
$$\binom{n}{k} = \binom{n}{n-k}$$

**Wyjaśnienie:**
Wybranie k elementów to to samo co wybranie (n-k) elementów do NIE wybrania.

**Przykład:** 
- Wybranie 3 osób z 8 = Wybranie 5 osób do pominięcia z 8
- $\binom{8}{3} = \binom{8}{5} = 56$

Obliczenie: $\binom{8}{5} = \frac{8!}{5! \cdot 3!} = \frac{40320}{120 \cdot 6} = 56$ ✓

**Własność 3: Wzór rekurencyjny (rekurencja Pascala)**
$$\binom{n}{k} = \binom{n-1}{k-1} + \binom{n-1}{k}$$

**Wyjaśnienie intuicyjne:**
Wyobraź sobie, że masz n elementów i wybierasz k. Weź jeden konkretny element (nazwijmy go "X"):
- **ALBO** wybierasz X + wybierasz jeszcze (k-1) z pozostałych (n-1) elementów → $\binom{n-1}{k-1}$
- **ALBO** NIE wybierasz X + wybierasz k z pozostałych (n-1) elementów → $\binom{n-1}{k}$

Suma tych dwóch przypadków daje wszystkie możliwości.

**Przykład numeryczny:**
$$\binom{5}{2} = \binom{4}{1} + \binom{4}{2}$$
$$10 = 4 + 6$$
$$10 = 10$$

Sprawdzenie:
- $\binom{5}{2} = \frac{5!}{2! \cdot 3!} = \frac{120}{2 \cdot 6} = 10$
- $\binom{4}{1} = \frac{4!}{1! \cdot 3!} = \frac{24}{1 \cdot 6} = 4$
- $\binom{4}{2} = \frac{4!}{2! \cdot 2!} = \frac{24}{2 \cdot 2} = 6$

#### Trójkąt Pascala - wizualizacja wzoru rekurencyjnego

Trójkąt Pascala to układ liczb, gdzie każda liczba jest sumą dwóch liczb nad nią:

```
Wiersz 0:              1                    ← C(0,0)
Wiersz 1:            1   1                  ← C(1,0)  C(1,1)
Wiersz 2:          1   2   1                ← C(2,0)  C(2,1)  C(2,2)
Wiersz 3:        1   3   3   1              ← C(3,0)  C(3,1)  C(3,2)  C(3,3)
Wiersz 4:      1   4   6   4   1            ← C(4,0)  C(4,1)  C(4,2)  C(4,3)  C(4,4)
Wiersz 5:    1   5  10  10   5   1          ← C(5,0)  C(5,1)  C(5,2)  C(5,3)  C(5,4)  C(5,5)
```

**Jak czytać trójkąt:**
- Wiersz n zawiera wartości $\binom{n}{0}, \binom{n}{1}, \binom{n}{2}, \ldots, \binom{n}{n}$
- Każda liczba = suma dwóch liczb bezpośrednio nad nią

**Przykład:**
W wierszu 4, liczba 6 to:
$$\binom{4}{2} = \binom{3}{1} + \binom{3}{2} = 3 + 3 = 6$$

**Własności trójkąta:**
- Brzegi zawsze równe 1 (bo $\binom{n}{0} = \binom{n}{n} = 1$)
- Symetria względem środka (bo $\binom{n}{k} = \binom{n}{n-k}$)
- Suma elementów w wierszu n = $2^n$ (np. wiersz 3: $1+3+3+1 = 8 = 2^3$)

---

### ALGORYTM I - Z DEFINICJI

#### Idea algorytmu

Algorytm I oblicza symbol Newtona **bezpośrednio według definicji matematycznej**:
$$\binom{n}{k} = \frac{n!}{k! \cdot (n-k)!}$$

**Krok po kroku:**
1. Oblicz $n!$ (silnię z n)
2. Oblicz $k!$ (silnię z k)
3. Oblicz $(n-k)!$ (silnię z różnicy n-k)
4. Pomnóż $k!$ przez $(n-k)!$
5. Podziel $n!$ przez wynik z kroku 4

#### Szczegółowy przykład działania

Obliczmy $\binom{8}{3}$ krok po kroku:

**Dane wejściowe:** n = 8, k = 3

**Krok 1: Obliczenie 8!**
```
8! = 1 × 2 × 3 × 4 × 5 × 6 × 7 × 8

Działanie szczegółowe:
1 × 2 = 2          (operacja 1: mnożenie)
2 × 3 = 6          (operacja 2: mnożenie)
6 × 4 = 24         (operacja 3: mnożenie)
24 × 5 = 120       (operacja 4: mnożenie)
120 × 6 = 720      (operacja 5: mnożenie)
720 × 7 = 5040     (operacja 6: mnożenie)
5040 × 8 = 40320   (operacja 7: mnożenie)

Wynik: 8! = 40320
Liczba operacji: 7 mnożeń (czyli n-1 = 8-1 = 7)
```

**Krok 2: Obliczenie 3!**
```
3! = 1 × 2 × 3

Działanie szczegółowe:
1 × 2 = 2          (operacja 8: mnożenie)
2 × 3 = 6          (operacja 9: mnożenie)

Wynik: 3! = 6
Liczba operacji: 2 mnożenia (czyli k-1 = 3-1 = 2)
```

**Krok 3: Obliczenie (8-3)! = 5!**
```
5! = 1 × 2 × 3 × 4 × 5

Działanie szczegółowe:
1 × 2 = 2          (operacja 10: mnożenie)
2 × 3 = 6          (operacja 11: mnożenie)
6 × 4 = 24         (operacja 12: mnożenie)
24 × 5 = 120       (operacja 13: mnożenie)

Wynik: 5! = 120
Liczba operacji: 4 mnożenia (czyli (n-k)-1 = 5-1 = 4)
```

**Krok 4: Mnożenie k! × (n-k)!**
```
3! × 5! = 6 × 120 = 720    (operacja 14: mnożenie)
```

**Krok 5: Dzielenie n! przez wynik**
```
8! / (3! × 5!) = 40320 / 720 = 56    (operacja 15: dzielenie)
```

**WYNIK KOŃCOWY:**
$$\binom{8}{3} = 56$$

**Całkowita liczba operacji:** 7 + 2 + 4 + 1 + 1 = **15 operacji**

#### Wzór na liczbę operacji

Dla ogólnego przypadku $\binom{n}{k}$:

**Operacje mnożenia:**
- Obliczenie n!: **(n-1) mnożeń**
- Obliczenie k!: **(k-1) mnożeń**
- Obliczenie (n-k)!: **(n-k-1) mnożeń**
- Mnożenie k! × (n-k)!: **1 mnożenie**

**Operacje dzielenia:**
- Dzielenie n! przez (k! × (n-k)!): **1 dzielenie**

**SUMA:**
$$(n-1) + (k-1) + (n-k-1) + 1 + 1 = n + k \text{ operacji}$$

Uproszczenie:
- $(n-1) + (k-1) + (n-k-1) = n - 1 + k - 1 + n - k - 1 = 2n - 3$
- $2n - 3 + 2 = 2n - 1 \approx n + k$ (dla uproszczenia mówimy O(n))

Dokładniej: **(n-1) + (k-1) + (n-k-1) + 2 = 2n - 1**

Ale w implementacji liczymy trochę inaczej, wynik to około **n + k operacji**.

#### Pseudokod (algorytm krok po kroku)

```
ALGORYTM I - Symbol Newtona z definicji

WEJŚCIE: 
  n - liczba całkowita dodatnia (liczba wszystkich elementów)
  k - liczba całkowita, 0 ≤ k ≤ n (liczba elementów do wyboru)

WYJŚCIE:
  wynik - wartość C(n,k)
  licznik_operacji - liczba wykonanych operacji elementarnych

KROK 1: Inicjalizacja
  licznik_operacji ← 0
  
KROK 2: Obliczenie n!
  nSilnia ← 1
  dla i = 2 do n wykonuj:
      nSilnia ← nSilnia × i
      licznik_operacji ← licznik_operacji + 1  // liczymy mnożenie
  koniec dla

KROK 3: Obliczenie k!
  kSilnia ← 1
  dla i = 2 do k wykonuj:
      kSilnia ← kSilnia × i
      licznik_operacji ← licznik_operacji + 1  // liczymy mnożenie
  koniec dla

KROK 4: Obliczenie (n-k)!
  nkSilnia ← 1
  dla i = 2 do (n-k) wykonuj:
      nkSilnia ← nkSilnia × i
      licznik_operacji ← licznik_operacji + 1  // liczymy mnożenie
  koniec dla

KROK 5: Obliczenie mianownika
  mianownik ← kSilnia × nkSilnia
  licznik_operacji ← licznik_operacji + 1  // liczymy mnożenie

KROK 6: Obliczenie wyniku
  wynik ← nSilnia / mianownik
  licznik_operacji ← licznik_operacji + 1  // liczymy dzielenie

KROK 7: Zwróć wynik
  ZWRÓĆ (wynik, licznik_operacji)
```

#### Co to jest "operacja elementarna"?

**Operacja elementarna** to **podstawowa operacja arytmetyczna**, której czas wykonania jest stały (nie zależy od wielkości danych).

W Algorytmie I operacją elementarną jest:
- **Mnożenie** dwóch liczb
- **Dzielenie** dwóch liczb

**Dlaczego liczymy tylko mnożenie i dzielenie?**
- Przypisania (np. `nSilnia ← 1`) są bardzo szybkie - pomijamy
- Porównania w pętli (np. `i ≤ n`) są bardzo szybkie - pomijamy
- Inkrementacje (np. `i ← i + 1`) są bardzo szybkie - pomijamy
- **Mnożenie i dzielenie są wolniejsze** - to one dominują czas wykonania

#### Złożoność czasowa - szczegółowe wyjaśnienie

**Złożoność czasowa** to miara tego, jak szybko rośnie liczba operacji w zależności od wielkości danych wejściowych.

Dla Algorytmu I:
- Liczba operacji ≈ **n + k**
- W najgorszym przypadku (gdy k ≈ n/2): liczba operacji ≈ **2n**

**Notacja Big O:** **O(n)**

Co to znaczy O(n)?
- Czas wykonania rośnie **liniowo** z n
- Jeśli n się podwoi, czas wykonania też się mniej więcej podwoi
- Stałe współczynniki pomijamy (dlatego 2n to też O(n))

**Przykłady:**
- n = 10 → ~15 operacji
- n = 100 → ~150 operacji (10× większe n → 10× więcej operacji)
- n = 1000 → ~1500 operacji (100× większe n → 100× więcej operacji)

#### Zalety i wady Algorytmu I

**ZALETY:**
✓ **Bardzo prosty** - łatwo zrozumieć i zaimplementować
✓ **Bezpośrednio odpowiada definicji matematycznej** - intuicyjny
✓ **Mała złożoność pamięciowa** - O(1), tylko kilka zmiennych
✓ **Szybki dla małych wartości n**

**WADY:**
✗ **Przepełnienie** - silnie bardzo szybko rosną!
  - 20! = 2 432 902 008 176 640 000 (już nie mieści się w long)
  - 13! = 6 227 020 800 (to maksimum dla int w C#)
✗ **Nieefektywny dla wielu obliczeń** - jeśli chcemy policzyć kilka symboli, musimy liczyć wszystko od początku
✗ **Mnożenie jest wolniejsze** niż dodawanie (w praktyce)

#### Implementacja w C# - szczegółowo skomentowana

```csharp
// Funkcja obliczająca symbol Newtona C(n,k) metodą z definicji
// Zwraca krotkę: (wynik, liczba_operacji)
static (long wynik, int operacje) AlgorytmI_SymbolNewtona(int n, int k)
{
    // Licznik operacji elementarnych (mnożeń i dzieleń)
    int operacje = 0;

    // KROK 1: Obliczenie n! (silnia z n)
    long nSilnia = 1;  // Zaczynamy od 1 (element neutralny mnożenia)
    
    // Pętla: 1×2×3×4×...×n
    // Zaczynamy od i=2, bo 1×1 = 1 (nie zmienia wyniku)
    for (int i = 2; i <= n; i++)
    {
        nSilnia *= i;      // nSilnia = nSilnia × i
        operacje++;        // Zliczamy tę operację mnożenia
    }
    // Po pętli: nSilnia = n!
    // Wykonano (n-1) mnożeń (dla n=8: wykonano 7 mnożeń)

    // KROK 2: Obliczenie k! (silnia z k)
    long kSilnia = 1;
    
    for (int i = 2; i <= k; i++)
    {
        kSilnia *= i;
        operacje++;        // Zliczamy każde mnożenie
    }
    // Po pętli: kSilnia = k!
    // Wykonano (k-1) mnożeń (dla k=3: wykonano 2 mnożenia)

    // KROK 3: Obliczenie (n-k)! (silnia z różnicy)
    long nkSilnia = 1;
    
    for (int i = 2; i <= (n - k); i++)
    {
        nkSilnia *= i;
        operacje++;
    }
    // Po pętli: nkSilnia = (n-k)!
    // Wykonano (n-k-1) mnożeń (dla n=8, k=3: 5-1=4 mnożenia)

    // KROK 4: Obliczenie mianownika: k! × (n-k)!
    long mianownik = kSilnia * nkSilnia;
    operacje++;  // To jest jedno mnożenie
    
    // KROK 5: Obliczenie wyniku: n! / (k! × (n-k)!)
    long wynik = nSilnia / mianownik;
    operacje++;  // To jest jedno dzielenie

    // KROK 6: Zwróć wynik i liczbę operacji
    return (wynik, operacje);
}
```

**Ślad wykonania dla n=8, k=3:**
```
Wejście: n=8, k=3

operacje = 0

Pętla n!: i=2,3,4,5,6,7,8
  nSilnia: 1 → 2 → 6 → 24 → 120 → 720 → 5040 → 40320
  operacje: 0 → 1 → 2 → 3 → 4 → 5 → 6 → 7

Pętla k!: i=2,3
  kSilnia: 1 → 2 → 6
  operacje: 7 → 8 → 9

Pętla (n-k)!: i=2,3,4,5
  nkSilnia: 1 → 2 → 6 → 24 → 120
  operacje: 9 → 10 → 11 → 12 → 13

Mnożenie: 6 × 120 = 720
  operacje: 13 → 14

Dzielenie: 40320 / 720 = 56
  operacje: 14 → 15

Wynik: (56, 15)
```

---

### ALGORYTM V - TRÓJKĄT PASCALA (TABLICA 2D)

#### Idea algorytmu

Algorytm V oblicza symbol Newtona **wykorzystując wzór rekurencyjny** i budując **trójkąt Pascala** w pamięci komputera (w tablicy dwuwymiarowej).

**Podstawa matematyczna - wzór rekurencyjny:**
$$\binom{n}{k} = \binom{n-1}{k-1} + \binom{n-1}{k}$$

**Jak to działa intuicyjnie?**

Wyobraź sobie, że masz n osób i chcesz wybrać k osób do drużyny. Weź jedną konkretną osobę - nazwijmy ją "Anna":

1. **PRZYPADEK 1:** Anna jest w drużynie
   - Musimy wybrać jeszcze (k-1) osób z pozostałych (n-1) osób
   - Liczba możliwości: $\binom{n-1}{k-1}$

2. **PRZYPADEK 2:** Anny NIE MA w drużynie
   - Musimy wybrać k osób z pozostałych (n-1) osób (bez Anny)
   - Liczba możliwości: $\binom{n-1}{k}$

3. **SUMA:** Wszystkie możliwości = Przypadek 1 + Przypadek 2
   - $\binom{n}{k} = \binom{n-1}{k-1} + \binom{n-1}{k}$

#### Budowanie trójkąta Pascala krok po kroku

**Wizualizacja tablicy:**
```
        k=0  k=1  k=2  k=3  k=4
n=0      1
n=1      1    1
n=2      1    2    1
n=3      1    3    3    1
n=4      1    4    6    4    1
```

Każda komórka `pascal[n][k]` zawiera wartość $\binom{n}{k}$.

**Algorytm wypełniania:**

**KROK 1: Inicjalizacja pierwszej kolumny (k=0)**
- Dla każdego wiersza n: `pascal[n][0] = 1`
- Bo $\binom{n}{0} = 1$ (jeden sposób na wybranie 0 elementów)

```
        k=0  k=1  k=2  k=3
n=0      1    ?    ?    ?
n=1      1    ?    ?    ?
n=2      1    ?    ?    ?
n=3      1    ?    ?    ?
```

**KROK 2: Wypełnianie kolejnych komórek**
- Dla każdej komórki `pascal[i][j]`:
- `pascal[i][j] = pascal[i-1][j-1] + pascal[i-1][j]`
- (suma dwóch komórek nad aktualną)

**Szczegółowy przykład - obliczanie C(4,2):**

Chcemy obliczyć $\binom{4}{2}$, czyli `pascal[4][2]`.

**Wiersz 0:**
```
pascal[0][0] = 1  (definicja: C(0,0) = 1)
```

**Wiersz 1:**
```
pascal[1][0] = 1  (pierwsza kolumna)
pascal[1][1] = pascal[0][0] + pascal[0][1] 
             = 1 + 0 = 1
```
Uwaga: `pascal[0][1]` jest poza zakresem, traktujemy jako 0, lub uznajemy że `pascal[1][1] = 1` z definicji.

**Wiersz 2:**
```
pascal[2][0] = 1  (pierwsza kolumna)
pascal[2][1] = pascal[1][0] + pascal[1][1] = 1 + 1 = 2  ← DODAWANIE (operacja 1)
pascal[2][2] = pascal[1][1] + pascal[1][2] = 1 + 0 = 1
```

**Wiersz 3:**
```
pascal[3][0] = 1  (pierwsza kolumna)
pascal[3][1] = pascal[2][0] + pascal[2][1] = 1 + 2 = 3  ← DODAWANIE (operacja 2)
pascal[3][2] = pascal[2][1] + pascal[2][2] = 2 + 1 = 3  ← DODAWANIE (operacja 3)
pascal[3][3] = pascal[2][2] + pascal[2][3] = 1 + 0 = 1
```

**Wiersz 4:**
```
pascal[4][0] = 1  (pierwsza kolumna)
pascal[4][1] = pascal[3][0] + pascal[3][1] = 1 + 3 = 4  ← DODAWANIE (operacja 4)
pascal[4][2] = pascal[3][1] + pascal[3][2] = 3 + 3 = 6  ← DODAWANIE (operacja 5) ★ TO SZUKAMY!
```

**Wynik:** $\binom{4}{2} = 6$

**Trójkąt po wypełnieniu:**
```
        k=0  k=1  k=2  k=3  k=4
n=0      1
n=1      1    1
n=2      1    2    1
n=3      1    3    3    1
n=4      1    4    6    4    1
        ↑    ↑    ↑
     C(4,0) C(4,1) C(4,2)=6
```

#### Optymalizacja - wykorzystanie symetrii

**Obserwacja:** $\binom{n}{k} = \binom{n}{n-k}$ (symetria)

**Przykłady:**
- $\binom{10}{7} = \binom{10}{3}$ (oba = 120)
- $\binom{100}{99} = \binom{100}{1}$ (oba = 100)

**Optymalizacja:**
Jeśli k > n/2, możemy zamienić: oblicz $\binom{n}{n-k}$ zamiast $\binom{n}{k}$

```
if k > n - k:
    k = n - k  // Zamiana na mniejszą wartość
```

**Dlaczego to pomaga?**
- Musimy budować trójkąt tylko do kolumny k (zamiast do n)
- Dla $\binom{100}{99}$ zamieniamy na $\binom{100}{1}$ - oszczędność pamięci i czasu!

**Bez optymalizacji:**
- Tablica: `pascal[101][100]` = 10,100 komórek
- Dużo obliczeń

**Z optymalizacją:**
- Zamiana: k=99 → k=1
- Tablica: `pascal[101][2]` = 202 komórki
- Znacznie mniej obliczeń!

#### Pseudokod - szczegółowy

```
ALGORYTM V - Symbol Newtona przez trójkąt Pascala

WEJŚCIE:
  n - liczba całkowita dodatnia (wiersz trójkąta)
  k - liczba całkowita, 0 ≤ k ≤ n (kolumna trójkąta)

WYJŚCIE:
  wynik - wartość C(n,k)
  licznik_operacji - liczba wykonanych operacji elementarnych

KROK 1: Optymalizacja przez symetrię
  jeśli k > (n - k) to:
      k ← n - k  // Zamieniamy na mniejszą wartość
  koniec jeśli

KROK 2: Utworzenie tablicy
  pascal ← tablica[0..n][0..k]  // Tablica dwuwymiarowa (n+1) × (k+1)
  licznik_operacji ← 0

KROK 3: Inicjalizacja pierwszej kolumny (k=0)
  dla i = 0 do n wykonuj:
      pascal[i][0] ← 1  // C(i,0) = 1 dla każdego i
  koniec dla
  // Uwaga: to NIE są operacje elementarne (tylko przypisania)

KROK 4: Wypełnianie trójkąta (rekurencyjnie)
  dla i = 1 do n wykonuj:                    // Dla każdego wiersza
      dla j = 1 do min(i, k) wykonuj:        // Dla każdej kolumny (maksymalnie do k)
          
          // Wzór rekurencyjny: C(i,j) = C(i-1,j-1) + C(i-1,j)
          pascal[i][j] ← pascal[i-1][j-1] + pascal[i-1][j]
          
          licznik_operacji ← licznik_operacji + 1  // Zliczamy DODAWANIE
          
      koniec dla
  koniec dla

KROK 5: Zwróć wynik
  wynik ← pascal[n][k]
  ZWRÓĆ (wynik, licznik_operacji)
```

**Uwagi do pseudokodu:**
- `min(i, k)` - nie wypełniamy komórek poza zakresem
- Dla wiersza i=1 wypełniamy tylko kolumnę j=1
- Dla wiersza i=2 wypełniamy kolumny j=1,2
- Itd.

#### Liczba operacji - dokładna analiza

**Co liczymy?** Tylko operacje **DODAWANIA** (w linii: `pascal[i][j] = pascal[i-1][j-1] + pascal[i-1][j]`)

**Ile dodawań wykonujemy?**

Dla każdego wiersza i (od 1 do n):
- Wykonujemy min(i, k) dodawań

**Przypadek 1: k ≥ n** (budujemy pełny trójkąt do wiersza n)
```
Wiersz 1: 1 dodawanie       (j=1)
Wiersz 2: 2 dodawania       (j=1,2)
Wiersz 3: 3 dodawania       (j=1,2,3)
...
Wiersz n: n dodawań         (j=1,2,...,n)

SUMA = 1 + 2 + 3 + ... + n = n(n+1)/2
```

**Wzór:** $\sum_{i=1}^{n} i = \frac{n(n+1)}{2} = \frac{n^2 + n}{2}$

**Przykład:** n=10, k=10
- Liczba dodawań: $\frac{10 \cdot 11}{2} = \frac{110}{2} = 55$ operacji

**Przypadek 2: k < n** (budujemy tylko do k-tej kolumny)
```
Wiersz 1: 1 dodawanie       (j=1)
Wiersz 2: 2 dodawania       (j=1,2)
...
Wiersz k: k dodawań         (j=1,2,...,k)
Wiersz k+1: k dodawań       (j=1,2,...,k) - nie więcej niż k!
Wiersz k+2: k dodawań       (j=1,2,...,k)
...
Wiersz n: k dodawań         (j=1,2,...,k)

SUMA = 1 + 2 + ... + k + k + k + ... + k
     = (1 + 2 + ... + k) + k × (n - k)
     = k(k+1)/2 + k(n-k)
     = k(k+1)/2 + kn - k²
     = k²/2 + k/2 + kn - k²
     = kn - k²/2 + k/2
     = kn + k/2 - k²/2
     ≈ k·n (dla dużych wartości)
```

**Przykład:** n=10, k=3
- Suma dla wierszy 1,2,3: $\frac{3 \cdot 4}{2} = 6$
- Suma dla wierszy 4-10: $3 \cdot 7 = 21$
- Razem: $6 + 21 = 27$ operacji

**Wzór ogólny:**
$$\text{operacje} = \begin{cases}
\frac{n(n+1)}{2} & \text{gdy } k \geq n \\
\frac{k(k+1)}{2} + k(n-k) & \text{gdy } k < n
\end{cases}$$

#### Złożoność czasowa - Big O

**W najgorszym przypadku** (gdy k ≈ n):
- Liczba operacji ≈ $\frac{n^2}{2}$

**Notacja Big O:** **O(n²)** lub dokładniej **O(n·k)**

**Co to znaczy?**
- Jeśli n się podwoi, czas wykonania wzrośnie **4-krotnie** (kwadratowo)
- Dla k stałego i rosnącego n: czas rośnie liniowo z n
- Dla k ≈ n: czas rośnie kwadratowo

**Przykłady:**
- n=10, k=10 → ~55 operacji
- n=20, k=20 → ~210 operacji (2× większe → 4× więcej)
- n=100, k=100 → ~5050 operacji

**Złożoność pamięciowa:** **O(n·k)**
- Potrzebujemy tablicy `pascal[n+1][k+1]`
- Dla n=100, k=50: tablica 101 × 51 = 5151 komórek
- Każda komórka: 8 bajtów (long) → ~40 KB pamięci

#### Zalety i wady Algorytmu V

**ZALETY:**
✓ **Tylko dodawanie** - operacje dodawania są szybsze niż mnożenie
✓ **Brak dużych liczb pośrednich** - nie liczymy n! (mniejsze ryzyko przepełnienia)
✓ **Efektywny dla wielu obliczeń** - jeśli potrzebujemy wielu symboli Newtona, wystarczy raz zbudować trójkąt
✓ **Numerycznie stabilny** - dodawanie nie kumuluje błędów tak jak mnożenie
✓ **Można wykorzystać tablicę wielokrotnie** - raz obliczona tablica zawiera wiele wartości

**WADY:**
✗ **Wymaga dużo pamięci** - O(n·k) pamięci na tablicę
✗ **Wolniejszy dla pojedynczych obliczeń** - dla małych n Algorytm I jest szybszy
✗ **Złożoność O(n²)** - dla dużych n może być wolny

#### Implementacja w C# - szczegółowo skomentowana

```csharp
// Funkcja obliczająca symbol Newtona C(n,k) metodą trójkąta Pascala
// Zwraca krotkę: (wynik, liczba_operacji)
static (long wynik, int operacje) AlgorytmV_SymbolNewtona(int n, int k)
{
    int operacje = 0;  // Licznik operacji dodawania
    
    // OPTYMALIZACJA: Wykorzystanie symetrii C(n,k) = C(n,n-k)
    // Jeśli k > n/2, zamieniamy na mniejszą wartość
    if (k > n - k)
    {
        k = n - k;  // Teraz k ≤ n/2
    }
    // Przykład: C(100,99) → C(100,1) - oszczędność pamięci i czasu!

    // KROK 1: Utworzenie tablicy dwuwymiarowej
    // pascal[i][j] będzie przechowywać wartość C(i,j)
    long[,] pascal = new long[n + 1, k + 1];
    
    // Wymiary: (n+1) wierszy × (k+1) kolumn
    // +1 bo indeksujemy od 0 (potrzebujemy wiersz 0 i kolumnę 0)

    // KROK 2: Inicjalizacja pierwszej kolumny (j=0)
    // C(i,0) = 1 dla każdego i (jeden sposób na wybranie 0 elementów)
    for (int i = 0; i <= n; i++)
    {
        pascal[i, 0] = 1;
    }
    // Po tej pętli:
    // pascal[0][0] = 1
    // pascal[1][0] = 1
    // pascal[2][0] = 1
    // ...
    // pascal[n][0] = 1

    // KROK 3: Wypełnianie trójkąta rekurencyjnie
    // Przechodzimy wiersz po wierszu (od góry do dołu)
    for (int i = 1; i <= n; i++)  // Dla każdego wiersza (od 1, bo wiersz 0 już mamy)
    {
        // Dla każdej kolumny w tym wierszu
        // min(i, k) - nie wychodzimy poza zakres (nie możemy wybrać więcej niż mamy)
        for (int j = 1; j <= Math.Min(i, k); j++)
        {
            // WZÓR REKURENCYJNY PASCALA:
            // C(i,j) = C(i-1,j-1) + C(i-1,j)
            //
            // pascal[i-1][j-1] - wartość z lewej góry
            // pascal[i-1][j]   - wartość z prawej góry
            //
            // Wizualizacja:
            //       [i-1][j-1]  [i-1][j]
            //             ↘      ↙
            //              [i][j]
            
            pascal[i, j] = pascal[i - 1, j - 1] + pascal[i - 1, j];
            
            // OPERACJA ELEMENTARNA: DODAWANIE
            operacje++;  // Zliczamy każde dodawanie
        }
    }
    
    // Po zakończeniu pętli:
    // pascal[n][k] zawiera wynik C(n,k)
    
    // KROK 4: Zwróć wynik
    return (pascal[n, k], operacje);
}
```

#### Ślad wykonania dla n=4, k=2

```
Wejście: n=4, k=2

// Optymalizacja
k=2, n-k=2, k > n-k? NIE (2 nie jest > 2)
Pozostawiamy k=2

// Utworzenie tablicy pascal[5][3] (indeksy 0-4, 0-2)
operacje = 0

// Inicjalizacja pierwszej kolumny
pascal[0][0] = 1
pascal[1][0] = 1
pascal[2][0] = 1
pascal[3][0] = 1
pascal[4][0] = 1

Tablica po inicjalizacji:
     j=0  j=1  j=2
i=0   1    ?    ?
i=1   1    ?    ?
i=2   1    ?    ?
i=3   1    ?    ?
i=4   1    ?    ?

// Wypełnianie wiersz po wierszu

WIERSZ i=1:
  j=1: pascal[1][1] = pascal[0][0] + pascal[0][1]
                    = 1 + 0 = 1  (pascal[0][1] traktujemy jako 0 lub inicjalizujemy jako 0)
  operacje = 1
  
  Tablica:
       j=0  j=1  j=2
  i=0   1    0    0
  i=1   1    1    ?

WIERSZ i=2:
  j=1: pascal[2][1] = pascal[1][0] + pascal[1][1]
                    = 1 + 1 = 2
  operacje = 2
  
  j=2: pascal[2][2] = pascal[1][1] + pascal[1][2]
                    = 1 + 0 = 1
  operacje = 3
  
  Tablica:
       j=0  j=1  j=2
  i=0   1    0    0
  i=1   1    1    0
  i=2   1    2    1

WIERSZ i=3:
  j=1: pascal[3][1] = pascal[2][0] + pascal[2][1]
                    = 1 + 2 = 3
  operacje = 4
  
  j=2: pascal[3][2] = pascal[2][1] + pascal[2][2]
                    = 2 + 1 = 3
  operacje = 5
  
  Tablica:
       j=0  j=1  j=2
  i=0   1    0    0
  i=1   1    1    0
  i=2   1    2    1
  i=3   1    3    3

WIERSZ i=4:
  j=1: pascal[4][1] = pascal[3][0] + pascal[3][1]
                    = 1 + 3 = 4
  operacje = 6
  
  j=2: pascal[4][2] = pascal[3][1] + pascal[3][2]
                    = 3 + 3 = 6  ← TO JEST NASZ WYNIK!
  operacje = 7
  
  Tablica końcowa:
       j=0  j=1  j=2
  i=0   1    0    0
  i=1   1    1    0
  i=2   1    2    1
  i=3   1    3    3
  i=4   1    4    6   ← pascal[4][2] = 6

Wynik: (6, 7)
// C(4,2) = 6, wykonano 7 dodawań
```

---

### PORÓWNANIE ALGORYTMÓW (PROBLEM 1)

| Kryterium | Algorytm I (Definicja) | Algorytm V (Pascal) |
|-----------|------------------------|---------------------|
| Operacja elementarna | Mnożenie/dzielenie | Dodawanie |
| Liczba operacji | O(n + k) | O(n·k) lub O(n²) |
| Złożoność czasowa | O(n) | O(n·k) |
| Złożoność pamięciowa | O(1) | O(n·k) |
| Ryzyko przepełnienia | Wysokie (silnie!) | Niskie (dodawanie) |
| Wielokrotne użycie | Nieefektywne | Efektywne |

**Przykład (n=8, k=3):**
- Algorytm I: 8 + 3 = 11 operacji
- Algorytm V: ~14 operacji (budowanie części trójkąta)

---

## PROBLEM 2 - ZBIORY 2-ELEMENTOWE

### TEORIA MATEMATYCZNA - SZCZEGÓŁOWE WYJAŚNIENIE

#### Sformułowanie problemu

**DANE:**
- $n$ - liczba naturalnych liczb (ile liczb mamy)
- $k$ - liczba naturalna, limit sumy (maksymalna dopuszczalna suma)
- Zbiór n liczb: $\{a_1, a_2, a_3, \ldots, a_n\}$ gdzie każda $a_i \in [1, k]$ (każda liczba jest z przedziału od 1 do k)

**CEL:**
Znaleźć **minimalną** liczbę zbiorów postaci:
- Zbiór 2-elementowy: $\{x, y\}$ taki, że $x + y \leq k$
- Zbiór 1-elementowy: $\{x\}$ (gdy x nie może być sparowane z żadną inną liczbą)

**ZADANIE:**
Podziel wszystkie n liczb na takie zbiory, aby:
1. Każda liczba należy do dokładnie jednego zbioru
2. W zbiorze 2-elementowym suma nie przekracza k
3. **Liczba zbiorów jest jak najmniejsza** (to jest kluczowe!)

#### Przykład wprowadzający

**Dane:**
- n = 8 liczb
- k = 140 (limit sumy)
- Liczby: [60, 70, 80, 56, 67, 78, 81, 68]

**Pytanie:** Jak podzielić te 8 liczb na zbiory tak, aby było ich jak najmniej?

**Podejście naiwne (źle!):**
```
{60, 70} suma=130 ✓
{80}     suma=80  ✓ (nie może być sparowane z niczym innym)
{56}     suma=56  ✓
{67}     suma=67  ✓
{78}     suma=78  ✓
{81}     suma=81  ✓
{68}     suma=68  ✓

Liczba zbiorów: 7 (1 para + 6 pojedynczych)
```

**Podejście optymalne (dobrze!):**
Najpierw posortujmy: [56, 60, 67, 68, 70, 78, 80, 81]

```
{56, 81} suma=137 ✓  (najmniejsza + największa)
{60, 80} suma=140 ✓  (następna najmniejsza + następna największa)
{67, 70} suma=137 ✓  (łączymy środkowe)
{68}     suma=68  ✓  (zostało samo)
{78}     suma=78  ✓  (nie pasowało do niczego)

Liczba zbiorów: 5 (3 pary + 2 pojedyncze)
```

**Obserwacja:** Optymalne rozwiązanie ma 5 zbiorów, naiwne miało 7!

#### Matematyczna analiza problemu

**Własność 1: Zbiór 2-elementowy jest lepszy**
- 1 zbiór 2-elementowy "wykorzystuje" 2 liczby
- 2 zbiory 1-elementowe też "wykorzystują" 2 liczby
- Więc zawsze lepiej mieć pary niż pojedyncze liczby!

**Własność 2: Strategia zachłanna**
Jeśli najmniejsza liczba $x$ może być sparowana z jakąkolwiek liczbą, to najlepiej sparować ją z **największą możliwą** liczbą $y$.

**Dowód intuicyjny:**
- Mamy liczby: [1, 2, 3, 4, 5], k=6
- Najmniejsza to 1
- 1 może być sparowane z: 2, 3, 4, 5 (bo 1+2=3, 1+3=4, 1+4=5, 1+5=6, wszystkie ≤6)
- **Opcja A:** Sparuj 1 z 2 → zostają [3, 4, 5]
  - Teraz 3 może być sparowane z 3? NIE (3+3=6, ale nie mamy dwóch trójek)
  - 3 może z 4? NIE (3+4=7 > 6)
  - 3 musi iść samo
- **Opcja B:** Sparuj 1 z 5 → zostają [2, 3, 4]
  - Teraz 2 może być sparowane z 4? TAK (2+4=6 ≤6)
  - Zostaje {3} samo

Wynik:
- Opcja A: {1,2}, {3}, {4}, {5} = 4 zbiory
- Opcja B: {1,5}, {2,4}, {3} = 3 zbiory ← LEPIEJ!

**Własność 3: Sortowanie jest kluczowe**
Aby móc efektywnie łączyć najmniejsze z największymi, musimy najpierw **posortować** liczby.

#### Strategia zachłanna - szczegółowy opis

**ALGORYTM ZACHŁANNY (GREEDY):**

1. **Posortuj** liczby rosnąco
2. Użyj **dwóch wskaźników**:
   - `left` - wskazuje na najmniejszą nieprzydzieloną liczbę (start: 0)
   - `right` - wskazuje na największą nieprzydzieloną liczbę (start: n-1)
3. **Dopóki left ≤ right:**
   - **JEŚLI** `liczby[left] + liczby[right] ≤ k`:
     - Utwórz parę `{liczby[left], liczby[right]}`
     - Zwiększ `left` (przesuń w prawo)
     - Zmniejsz `right` (przesuń w lewo)
   - **W PRZECIWNYM RAZIE:**
     - `liczby[right]` jest za duża, żeby być w parze z kimkolwiek
     - Utwórz zbiór 1-elementowy `{liczby[right]}`
     - Zmniejsz `right`
4. **Koniec** - wszystkie liczby przydzielone

**Dlaczego to działa?**

**Lemat:** Jeśli najmniejsza liczba `x` NIE może być sparowana z największą `y` (bo x+y > k), to `y` NIE może być sparowane z ŻADNĄ inną liczbą.

**Dowód:**
- Wszystkie inne liczby są ≥ x (bo posortowaliśmy)
- Jeśli y + x > k, to y + (cokolwiek ≥ x) > k
- Więc y musi iść samo!

**Poprawność algorytmu zachłannego:**
1. Zawsze łączymy najmniejszą dostępną z największą dostępną (jeśli się da)
2. Jeśli się nie da, największa idzie sama (i tak musi)
3. Nigdy nie tracimy okazji do stworzenia pary
4. Wynik jest optymalny!

#### Szczegółowy przykład - krok po kroku

**Dane:**
- n = 8
- k = 140
- Liczby: [60, 70, 80, 56, 67, 78, 81, 68]

**KROK 1: Sortowanie**
```
Przed: [60, 70, 80, 56, 67, 78, 81, 68]
Po:    [56, 60, 67, 68, 70, 78, 80, 81]
       ↑                              ↑
     left=0                       right=7
```

**KROK 2: Parowanie - iteracja 1**
```
Sprawdzenie: liczby[0] + liczby[7] = 56 + 81 = 137
137 ≤ 140? TAK ✓

Akcja: Utwórz parę {56, 81}
left = 1, right = 6

Pozostałe: [60, 67, 68, 70, 78, 80]
            ↑                   ↑
          left=1            right=6
```

**KROK 3: Parowanie - iteracja 2**
```
Sprawdzenie: liczby[1] + liczby[6] = 60 + 80 = 140
140 ≤ 140? TAK ✓

Akcja: Utwórz parę {60, 80}
left = 2, right = 5

Pozostałe: [67, 68, 70, 78]
            ↑           ↑
          left=2    right=5
```

**KROK 4: Parowanie - iteracja 3**
```
Sprawdzenie: liczby[2] + liczby[5] = 67 + 78 = 145
145 ≤ 140? NIE ✗

Akcja: 78 jest za duże, musi iść samo → {78}
right = 4

Pozostałe: [67, 68, 70]
            ↑       ↑
          left=2 right=4
```

**KROK 5: Parowanie - iteracja 4**
```
Sprawdzenie: liczby[2] + liczby[4] = 67 + 70 = 137
137 ≤ 140? TAK ✓

Akcja: Utwórz parę {67, 70}
left = 3, right = 3

Pozostałe: [68]
            ↑
        left=right=3
```

**KROK 6: Parowanie - iteracja 5**
```
left = right = 3 (to samo)

Akcja: Ostatni element, musi iść sam → {68}
left = 4, right = 2

left > right → KONIEC
```

**WYNIK KOŃCOWY:**
```
Zbiory:
1. {56, 81} - para
2. {60, 80} - para
3. {78}     - pojedynczy
4. {67, 70} - para
5. {68}     - pojedynczy

Liczba zbiorów: 5
```

**Podsumowanie:**
- 3 pary (6 liczb)
- 2 pojedyncze (2 liczby)
- Razem: 5 zbiorów dla 8 liczb

To jest **OPTYMALNE** rozwiązanie - nie da się zrobić mniej niż 5 zbiorów!

---

### ALGORYTM I - ZACHŁANNY (OPTYMALNY)

**Idea:** Sortowanie + technika dwóch wskaźników (two pointers)

**Pseudokod:**
```
AlgorytmI_Zachłanny(n, k, liczby):
    posortuj liczby rosnąco
    zbiory = []
    left = 0
    right = n - 1
    
    while left <= right:
        if left == right:
            zbiory.dodaj({liczby[left]})
            break
        
        if liczby[left] + liczby[right] <= k:
            zbiory.dodaj({liczby[left], liczby[right]})
            left++
            right--
        else:
            zbiory.dodaj({liczby[right]})
            right--
    
    return zbiory
```

**Operacja elementarna:** Porównanie

**Liczba operacji:**
- Sortowanie: O(n log n) porównań (~n log₂ n)
- Parowanie: O(n) porównań (każdy element przetwarzany raz)
- **Razem:** ~n log₂ n + n

**Złożoność czasowa:** $O(n \log n)$ - zdominowane przez sortowanie

**Złożoność pamięciowa:** O(n) - kopia tablicy do sortowania

**Zalety:**
- Gwarantuje optymalną liczbę zbiorów
- Efektywny czasowo
- Prosty do implementacji

**Implementacja C#:**
```csharp
static (List<List<int>> zbiory, int operacje) AlgorytmI_Zbiory(int n, int k, int[] liczby)
{
    int operacje = 0;
    List<List<int>> zbiory = new List<List<int>>();
    
    int[] sorted = new int[n];
    Array.Copy(liczby, sorted, n);
    Array.Sort(sorted);
    operacje += (int)(n * Math.Log(n, 2)); // sortowanie
    
    int left = 0;
    int right = n - 1;
    
    while (left <= right)
    {
        operacje++; // porównanie left <= right
        
        if (left == right)
        {
            zbiory.Add(new List<int> { sorted[left] });
            break;
        }
        
        operacje++; // porównanie sumy
        if (sorted[left] + sorted[right] <= k)
        {
            zbiory.Add(new List<int> { sorted[left], sorted[right] });
            left++;
            right--;
        }
        else
        {
            zbiory.Add(new List<int> { sorted[right] });
            right--;
        }
    }
    
    return (zbiory, operacje);
}
```

**Przykład działania:**
```
Dane: n=8, k=140
Liczby: [60, 70, 80, 56, 67, 78, 81, 68]

Po sortowaniu: [56, 60, 67, 68, 70, 78, 80, 81]

Krok 1: left=56, right=81  → 56+81=137 ≤ 140 → {56, 81}
Krok 2: left=60, right=80  → 60+80=140 ≤ 140 → {60, 80}
Krok 3: left=67, right=78  → 67+78=145 > 140  → {78}
Krok 4: left=67, right=70  → 67+70=137 ≤ 140 → {67, 70}
Krok 5: left=68, right=68  → {68}

Wynik: 5 zbiorów
```

---

### ALGORYTM II - NAIWNY

**Idea:** Dla każdej liczby szukaj pierwszej możliwej pary liniowo

**Pseudokod:**
```
AlgorytmII_Naiwny(n, k, liczby):
    zbiory = []
    uzyte[1..n] = false
    
    for i = 1 to n:
        if uzyte[i]:
            continue
        
        znalezionoPare = false
        for j = i+1 to n:
            if uzyte[j]:
                continue
            
            if liczby[i] + liczby[j] <= k:
                zbiory.dodaj({liczby[i], liczby[j]})
                uzyte[i] = true
                uzyte[j] = true
                znalezionoPare = true
                break
        
        if not znalezionoPare:
            zbiory.dodaj({liczby[i]})
            uzyte[i] = true
    
    return zbiory
```

**Operacja elementarna:** Porównanie

**Liczba operacji:** O(n²) - w najgorszym przypadku dla każdej liczby sprawdzamy wszystkie pozostałe

**Złożoność czasowa:** $O(n^2)$

**Wady:**
- Nieefektywny
- Nie gwarantuje optymalnego rozwiązania
- Wynik zależy od kolejności liczb w wejściu

**Implementacja C#:**
```csharp
static (List<List<int>> zbiory, int operacje) AlgorytmII_Zbiory(int n, int k, int[] liczby)
{
    int operacje = 0;
    List<List<int>> zbiory = new List<List<int>>();
    bool[] uzyte = new bool[n];
    
    for (int i = 0; i < n; i++)
    {
        operacje++;
        if (uzyte[i]) continue;
        
        bool znalezionoPare = false;
        
        for (int j = i + 1; j < n; j++)
        {
            operacje++;
            if (uzyte[j]) continue;
            
            operacje++;
            if (liczby[i] + liczby[j] <= k)
            {
                zbiory.Add(new List<int> { liczby[i], liczby[j] });
                uzyte[i] = true;
                uzyte[j] = true;
                znalezionoPare = true;
                break;
            }
        }
        
        if (!znalezionoPare && !uzyte[i])
        {
            zbiory.Add(new List<int> { liczby[i] });
            uzyte[i] = true;
        }
    }
    
    return (zbiory, operacje);
}
```

---

### PORÓWNANIE ALGORYTMÓW (PROBLEM 2)

| Kryterium | Algorytm I (Zachłanny) | Algorytm II (Naiwny) |
|-----------|------------------------|----------------------|
| Złożoność czasowa | O(n log n) | O(n²) |
| Optymalizacja | TAK - minimalna liczba zbiorów | NIE |
| Zależność od kolejności | NIE (sortuje) | TAK |
| Liczba operacji (n=8) | ~27 | ~64 (w najgorszym) |
| Gwarancja poprawności | TAK | NIE |

---

## PROBLEM 4 - SEJF KRÓLA BAJTDOCJI

### TEORIA MATEMATYCZNA

**Problem:** Korytarz szerokości n metrów z m prętami laserowymi. Każdy pręt zajmuje przedział [y₁, y₂] wysokości. Znaleźć wszystkie bezpieczne pasma (gdzie nie ma prętów).

**Model matematyczny:**
- Korytarz: przedział [0, n]
- Pręt i: przedział [y₁ᵢ, y₂ᵢ]
- Bezpieczne pasmo: przedział [a, b] taki, że nie przecina się z żadnym prętem

**Własności:**
- Pręty mogą się nakładać lub dotykać
- Pręt może być zamocowany wzdłuż korytarza (0 ≤ x₁ ≤ x₂ ≤ n)
- Oś Y jest krytyczna (0Y - dół, S - góra)

**Kluczowa obserwacja:**
Jeśli scalimy wszystkie nakładające się przedziały zajęte, to bezpieczne pasma to luki między nimi.

---

### ALGORYTM I - SCALANIE PRZEDZIAŁÓW (OPTYMALNY)

**Idea:**
1. Zbierz wszystkie zajęte przedziały [y₁, y₂]
2. Posortuj według punktu początkowego
3. Scal nakładające się/stykające się przedziały
4. Znajdź luki między scalonymi przedziałami

**Matematyka scalania:**
- Dwa przedziały [a₁, b₁] i [a₂, b₂] (a₁ ≤ a₂) nakładają się, gdy a₂ ≤ b₁
- Scalony przedział: [a₁, max(b₁, b₂)]

**Pseudokod:**
```
AlgorytmI_Scalanie(n, prety):
    zajete = []
    for pret in prety:
        zajete.dodaj([pret.y1, pret.y2])
    
    posortuj zajete według punktu początkowego
    
    // Scalanie
    scalone = []
    current = zajete[0]
    for i = 1 to |zajete|-1:
        if zajete[i].y1 <= current.y2:
            current.y2 = max(current.y2, zajete[i].y2)
        else:
            scalone.dodaj(current)
            current = zajete[i]
    scalone.dodaj(current)
    
    // Szukanie luk
    bezpieczne = []
    pozycja = 0
    for zajety in scalone:
        if pozycja < zajety.y1:
            bezpieczne.dodaj([pozycja, zajety.y1])
        pozycja = max(pozycja, zajety.y2)
    
    if pozycja < n:
        bezpieczne.dodaj([pozycja, n])
    
    return bezpieczne
```

**Operacja elementarna:** Porównanie

**Liczba operacji:**
- Sortowanie: O(m log m) ≈ m log₂ m porównań
- Scalanie: O(m) porównań
- Szukanie luk: O(m) porównań
- **Razem:** ~m log₂ m + 2m

**Złożoność czasowa:** $O(m \log m)$ - zdominowane przez sortowanie

**Złożoność pamięciowa:** O(m) - listy przedziałów

**Implementacja C#:**
```csharp
static (List<(int y1, int y2)> pasma, int operacje) AlgorytmI_Sejf(
    int n, (int x1, int y1, int x2, int y2)[] prety)
{
    int operacje = 0;
    List<(int y1, int y2)> bezpieczne = new List<(int, int)>();
    
    // Zbierz zajęte przedziały
    List<(int y1, int y2)> zajete = new List<(int, int)>();
    for (int i = 0; i < prety.Length; i++)
    {
        zajete.Add((prety[i].y1, prety[i].y2));
    }
    
    if (zajete.Count == 0)
    {
        if (n > 0) bezpieczne.Add((0, n));
        return (bezpieczne, operacje);
    }
    
    // Sortowanie
    zajete.Sort((a, b) => a.y1.CompareTo(b.y1));
    operacje += (int)(prety.Length * Math.Log(prety.Length, 2));
    
    // Scalanie
    List<(int y1, int y2)> scalone = new List<(int, int)>();
    var current = zajete[0];
    
    for (int i = 1; i < zajete.Count; i++)
    {
        operacje++;
        if (zajete[i].y1 <= current.y2)
        {
            operacje++;
            current = (current.y1, Math.Max(current.y2, zajete[i].y2));
        }
        else
        {
            scalone.Add(current);
            current = zajete[i];
        }
    }
    scalone.Add(current);
    
    // Szukanie luk
    int pozycja = 0;
    foreach (var z in scalone)
    {
        operacje++;
        if (pozycja < z.y1)
        {
            bezpieczne.Add((pozycja, z.y1));
        }
        operacje++;
        pozycja = Math.Max(pozycja, z.y2);
    }
    
    operacje++;
    if (pozycja < n)
    {
        bezpieczne.Add((pozycja, n));
    }
    
    return (bezpieczne, operacje);
}
```

**Przykład działania:**
```
Dane: n=11, m=5
Pręty (y1, y2): [2,5], [2,2], [6,6], [4,1], [4,4], [10,10], [10,10], [1,6], [5,9]

Krok 1: Zbierz zajęte: [2,5], [2,2], [6,6], [1,4], [4,4], [10,10], [10,10], [1,6], [5,9]

Krok 2: Sortuj: [1,4], [1,6], [2,2], [2,5], [4,4], [5,9], [6,6], [10,10], [10,10]

Krok 3: Scalaj:
  [1,4] + [1,6] → [1,6]
  [1,6] + [2,2] → [1,6] (zawiera się)
  [1,6] + [2,5] → [1,6] (zawiera się)
  [1,6] + [4,4] → [1,6] (zawiera się)
  [1,6] + [5,9] → [1,9]
  [1,9] + [6,6] → [1,9] (zawiera się)
  [1,9] + [10,10] → [1,9], [10,10]
  [10,10] + [10,10] → [10,10]
  
Scalone: [1,9], [10,10]

Krok 4: Luki:
  0 < 1 → [0,1]
  9 < 10 → [9,10]
  10 < 11 → [10,11]

UWAGA: [10,11] jest błędne bo [10,10] zajęte!
Poprawnie: [0,1], [9,10], pasmo końcowe jeśli 10 < 11
```

---

### ALGORYTM II - BRUTE FORCE

**Idea:** Dla każdego punktu y ∈ [0, n] sprawdź czy jest zajęty przez jakiś pręt

**Pseudokod:**
```
AlgorytmII_BruteForce(n, prety):
    bezpieczny[0..n] = true
    
    for pret in prety:
        for y = pret.y1 to pret.y2:
            bezpieczny[y] = false
    
    // Zbierz przedziały
    pasma = []
    poczatek = -1
    for y = 0 to n:
        if bezpieczny[y]:
            if poczatek == -1:
                poczatek = y
        else:
            if poczatek != -1:
                pasma.dodaj([poczatek, y])
                poczatek = -1
    
    if poczatek != -1:
        pasma.dodaj([poczatek, n])
    
    return pasma
```

**Operacja elementarna:** Porównanie/przypisanie

**Liczba operacji:**
- Dla każdego pręta: (y₂ - y₁ + 1) operacji
- Razem dla wszystkich prętów: O(m · średnia_długość)
- Przejście po tablicy: O(n)
- **Razem:** O(m · długość_pręta + n) ≈ O(n · m) w najgorszym

**Złożoność czasowa:** $O(n \cdot m)$ lub $O(n + m \cdot L)$ gdzie L to średnia długość pręta

**Wady:**
- Nieefektywny dla dużych n
- Wymaga tablicy rozmiaru n (problem pamięciowy dla n=50000)

**Implementacja C#:**
```csharp
static (List<(int y1, int y2)> pasma, int operacje) AlgorytmII_Sejf(
    int n, (int x1, int y1, int x2, int y2)[] prety)
{
    int operacje = 0;
    List<(int y1, int y2)> bezpieczne = new List<(int, int)>();
    
    bool[] bezp = new bool[n + 1];
    for (int i = 0; i <= n; i++)
    {
        bezp[i] = true;
    }
    
    // Oznacz zajęte punkty
    foreach (var pret in prety)
    {
        for (int y = pret.y1; y <= pret.y2; y++)
        {
            operacje++;
            if (y <= n)
                bezp[y] = false;
        }
    }
    
    // Zbierz przedziały
    int poczatek = -1;
    for (int y = 0; y <= n; y++)
    {
        operacje++;
        if (bezp[y])
        {
            if (poczatek == -1)
                poczatek = y;
        }
        else
        {
            if (poczatek != -1)
            {
                bezpieczne.Add((poczatek, y));
                poczatek = -1;
            }
        }
    }
    
    if (poczatek != -1)
    {
        bezpieczne.Add((poczatek, n));
    }
    
    return (bezpieczne, operacje);
}
```

---

### PORÓWNANIE ALGORYTMÓW (PROBLEM 4)

| Kryterium | Algorytm I (Scalanie) | Algorytm II (Brute Force) |
|-----------|----------------------|---------------------------|
| Złożoność czasowa | O(m log m) | O(n·m) lub O(n + m·L) |
| Złożoność pamięciowa | O(m) | O(n) |
| Operacje | ~m log₂ m + 2m | ~suma_długości_prętów + n |
| Skalowalność | Doskonała | Słaba dla dużych n |
| Dla n=10000, m=50 | ~390 op. | ~500050 op. (worst) |

**Optymalizacja:** Algorytm I jest optymalny - O(m log m + n) przy wypisywaniu wyników.

---

## ZŁOŻONOŚCI CZASOWE - PODSUMOWANIE

### Notacja O (Big O)

**Definicja:** f(n) = O(g(n)) gdy istnieje c > 0 i n₀ takie, że dla wszystkich n ≥ n₀:
$$f(n) \leq c \cdot g(n)$$

**Hierarchia złożoności:**
$$O(1) < O(\log n) < O(\sqrt{n}) < O(n) < O(n \log n) < O(n^2) < O(n^3) < O(2^n) < O(n!)$$

### Porównanie wszystkich algorytmów

| Problem | Algorytm | Złożoność | Operacja | Typ |
|---------|----------|-----------|----------|-----|
| 1 | Algorytm I | O(n) | Mnożenie | Iteracyjny |
| 1 | Algorytm V | O(n·k) | Dodawanie | Programowanie dynamiczne |
| 2 | Algorytm I | O(n log n) | Porównanie | Zachłanny |
| 2 | Algorytm II | O(n²) | Porównanie | Naiwny |
| 4 | Algorytm I | O(m log m) | Porównanie | Zachłanny + sortowanie |
| 4 | Algorytm II | O(n·m) | Porównanie | Brute force |

### Przykładowe czasy dla n=1000

| Złożoność | Operacje | Czas (1GHz) |
|-----------|----------|-------------|
| O(n) | 1,000 | 1 μs |
| O(n log n) | ~10,000 | 10 μs |
| O(n²) | 1,000,000 | 1 ms |
| O(n³) | 1,000,000,000 | 1 s |
| O(2ⁿ) | 10³⁰⁰ | niemożliwe |

---

## TECHNIKI ALGORYTMICZNE

### 1. Programowanie dynamiczne (DP)
- **Idea:** Rozwiązuj mniejsze podproblemy i wykorzystuj ich wyniki
- **Przykład:** Trójkąt Pascala (Algorytm V)
- **Wzór rekurencyjny:** $\binom{n}{k} = \binom{n-1}{k-1} + \binom{n-1}{k}$
- **Kiedy stosować:** Optymalne podstruktury + nakładające się podproblemy

### 2. Algorytmy zachłanne (Greedy)
- **Idea:** Wybieraj lokalnie optymalne rozwiązanie w każdym kroku
- **Przykłady:** 
  - Problem 2: parowanie najmniejszej z największą
  - Problem 4: scalanie przedziałów
- **Kiedy stosować:** Własność zachłannego wyboru + optymalne podstruktury
- **Dowód poprawności:** Pokazać że zachłanny wybór prowadzi do optymalnego rozwiązania

### 3. Dziel i zwyciężaj (Divide and Conquer)
- **Idea:** Podziel problem na mniejsze, rozwiąż je, połącz wyniki
- **Przykład:** Sortowanie przez scalanie (Merge Sort) - O(n log n)
- **Wzór rekurencyjny:** T(n) = 2T(n/2) + O(n)

### 4. Two Pointers (Dwa wskaźniki)
- **Idea:** Dwa wskaźniki poruszają się po posortowanej strukturze
- **Przykład:** Problem 2 - left i right w posortowanej tablicy
- **Złożoność:** O(n) po sortowaniu
- **Kiedy stosować:** Pary elementów, przedziały, posortowane dane

---

## WSKAZÓWKI DO ODPOWIEDZI USTNEJ

### Jak odpowiadać na pytania o algorytmy:

1. **Przedstaw problem matematycznie**
   - Zdefiniuj notację (n, k, m)
   - Podaj wzory matematyczne
   - Wyjaśnij własności

2. **Opisz ideę algorytmu**
   - Jaka technika (greedy, DP, brute force)?
   - Dlaczego tak działa?
   - Kluczowe kroki

3. **Analiza złożoności**
   - Operacja elementarna
   - Liczba operacji (dokładna lub asymptotyczna)
   - Notacja O
   - Złożoność pamięciowa

4. **Przykład działania**
   - Małe dane wejściowe
   - Krok po kroku
   - Wynik

5. **Porównanie z innymi algorytmami**
   - Zalety i wady
   - Kiedy użyć którego

### Typowe pytania:

**Q: Dlaczego Algorytm V jest lepszy mimo większej złożoności?**
A: Używa tylko dodawania (vs mnożenie w Alg. I), co zmniejsza ryzyko przepełnienia. Dla wielokrotnych obliczeń symboli Newtona, tablica może być wykorzystana ponownie.

**Q: Jak działa strategia zachłanna w Problemie 2?**
A: Łączymy najmniejszą liczbę z największą. Jeśli się mieszczą w limicie k, tworzymy parę. W przeciwnym razie największa nie może mieć pary (bo jeśli nie pasuje do najmniejszej, nie pasuje do żadnej). Gwarantuje minimalną liczbę zbiorów.

**Q: Dlaczego sortujemy w Problemie 4?**
A: Aby efektywnie scalić nakładające się przedziały. Po sortowaniu wystarczy jedno przejście O(m), aby połączyć wszystkie nakładające się pręty.

**Q: Co to jest operacja elementarna?**
A: To podstawowa operacja, której liczbę chcemy minimalizować. Zależy od algorytmu: mnożenie/dzielenie (Alg. I Problem 1), dodawanie (Alg. V), porównanie (Problem 2, 4).

**Q: Różnica między O(n) a Θ(n)?**
A: 
- O(n) - górne ograniczenie (≤)
- Θ(n) - dokładne ograniczenie (=)
- Ω(n) - dolne ograniczenie (≥)

---

## WZORY I DEFINICJE DO ZAPAMIĘTANIA

### Symbol Newtona
$$\binom{n}{k} = \frac{n!}{k!(n-k)!} = \binom{n-1}{k-1} + \binom{n-1}{k}$$

### Własności
- $\binom{n}{0} = \binom{n}{n} = 1$
- $\binom{n}{k} = \binom{n}{n-k}$
- $\binom{n}{1} = n$

### Silnia
$$n! = 1 \cdot 2 \cdot 3 \cdots n$$
$$0! = 1$$

### Złożoności standardowych operacji
- Sortowanie (merge sort, heap sort): O(n log n)
- Sortowanie (quick sort średnio): O(n log n)
- Sortowanie (bubble sort): O(n²)
- Wyszukiwanie binarne: O(log n)
- Wyszukiwanie liniowe: O(n)

### Wzory sumy
$$\sum_{i=1}^{n} i = \frac{n(n+1)}{2} = O(n^2)$$
$$\sum_{i=1}^{n} i^2 = \frac{n(n+1)(2n+1)}{6} = O(n^3)$$

---

## STRUKTURA KODU - WYJAŚNIENIA

### Format plików wejściowych/wyjściowych

**Problem 1 (In0101.txt):**
```
8 3
```
Pierwsza linia: n=8, k=3 (gdzie k ≤ n)

**Problem 1 (Out0101.txt):**
```
n=8 k=3
SN1 = 56, liczba operacji = 14
SN5 = 56, liczba operacji = 4
```

**Problem 2 (In0102.txt):**
```
8 140
60
70
80
56
67
78
81
68
```
Pierwsza linia: n (liczba elementów), k (limit sumy)
Następne n linii: poszczególne liczby

**Problem 2 (Out0102.txt):**
```
56 81
60 80
78
67 70
68
5
```
Każda linia: zbiór (1 lub 2 elementy)
Ostatnia linia: liczba zbiorów

**Problem 4 (In0104.txt):**
```
11 5
2 5 2 6
4 1 4 4
4 10 10 10
1 6 5 9
3 8 7 9
```
Pierwsza linia: n (szerokość), m (liczba prętów)
Następne m linii: x₁ y₁ x₂ y₂ (współrzędne pręta)

**Problem 4 (Out0104.txt):**
```
0 1
4 5
9 10
10 11
liczba bezpiecznych pasm: 4
```
Każda linia: y₁ y₂ (bezpieczne pasmo)
Ostatnia linia: liczba pasm

---

## POWODZENIA NA EGZAMINIE!

**Pamiętaj:**
- Matematyka + implementacja = pełne zrozumienie
- Złożoność czasowa to klucz do oceny algorytmu
- Przykłady pomagają w wyjaśnieniu
- Zachłanne algorytmy wymagają dowodu poprawności
- Programowanie dynamiczne = rekurencja + zapamiętywanie

---

*Ściąga opracowana na podstawie implementacji w C# oraz teorii matematyki dyskretnej*
*Algorytmy i Struktury Danych - Pracownia Specjalistyczna*
