# 📚 Wyjaśnienia zadań PS2 - Algorytmy

Ten dokument wyjaśnia w prosty sposób wszystkie algorytmy z PS2.

---

## 📋 Spis treści
1. [Sortowanie przez zliczanie](#1-sortowanie-przez-zliczanie-counting-sort)
2. [Ciąg Fibonacciego](#2-ciąg-fibonacciego)
3. [Drzewo BST](#3-drzewo-binarne-bst)
4. [Metoda bisekcji](#4-metoda-bisekcji)

---

# 1. Sortowanie przez zliczanie (Counting Sort)

## 🧮 Co to jest matematycznie?

Sortowanie przez zliczanie to algorytm, który **nie porównuje elementów między sobą** (jak np. bubble sort). Zamiast tego:

1. Liczy ile razy każda liczba występuje
2. Na podstawie tych zliczeń odtwarza posortowaną tablicę

### Prosty przykład:

Mamy liczby: `[3, 1, 2, 1, 3, 1]`

**Krok 1: Zliczamy wystąpienia:**
- Liczba 1 → występuje 3 razy
- Liczba 2 → występuje 1 raz  
- Liczba 3 → występuje 2 razy

**Krok 2: Odtwarzamy tablicę:**
- Wypisujemy 1 trzy razy: `[1, 1, 1, ...]`
- Wypisujemy 2 jeden raz: `[1, 1, 1, 2, ...]`
- Wypisujemy 3 dwa razy: `[1, 1, 1, 2, 3, 3]`

**Wynik:** `[1, 1, 1, 2, 3, 3]` ✅

## 💡 Dlaczego to działa?

Wyobraź sobie, że masz pudełka ponumerowane od najmniejszej do największej liczby. Do każdego pudełka wrzucasz kulki - tyle ile razy dana liczba występuje. Potem po kolei wysypujesz kulki z pudełek - od najmniejszego numeru do największego. Automatycznie masz posortowane!

## 🖥️ Jak działa kod?

```
┌─────────────────────────────────────────────────────┐
│  WEJŚCIE: [3, 1, 2, 1, 3, 1]                       │
└─────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────┐
│  1. Znajdź MIN i MAX                               │
│     MIN = 1, MAX = 3                               │
│     Zakres = 3 - 1 + 1 = 3 pudełka                 │
└─────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────┐
│  2. Utwórz tablicę zliczającą (3 elementy)         │
│     zliczenia[0] = 0  (dla liczby 1)               │
│     zliczenia[1] = 0  (dla liczby 2)               │
│     zliczenia[2] = 0  (dla liczby 3)               │
└─────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────┐
│  3. Zlicz wystąpienia (rekurencyjnie)              │
│     zliczenia[0] = 3  (trzy jedynki)               │
│     zliczenia[1] = 1  (jedna dwójka)               │
│     zliczenia[2] = 2  (dwie trójki)                │
└─────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────┐
│  4. Zbuduj wynik (rekurencyjnie)                   │
│     [1, 1, 1, 2, 3, 3]                             │
└─────────────────────────────────────────────────────┘
```

## 🔑 Kluczowe elementy w kodzie:

| Element | Co robi |
|---------|---------|
| `min`, `max` | Określają zakres liczb |
| `zliczenia[]` | Tablica przechowująca ile razy występuje każda liczba |
| `zliczenia[tablica[i] - min]++` | Zwiększa licznik dla danej liczby (odejmujemy `min` bo tablica zaczyna się od 0) |
| Rekurencja | Każda operacja (szukanie min/max, zliczanie, budowanie) wywołuje samą siebie dla kolejnego elementu |

## ⚡ Złożoność:
- **Czasowa:** O(n + k) gdzie n = ilość elementów, k = zakres wartości
- **Pamięciowa:** O(k) - potrzebujemy tablicy wielkości zakresu

## ⚠️ Kiedy używać?
- ✅ Gdy znamy zakres liczb (np. oceny 1-6)
- ✅ Gdy zakres jest mały
- ❌ Nie nadaje się dla bardzo dużych zakresów (np. liczby od 1 do 1 000 000 000)

---

# 2. Ciąg Fibonacciego

## 🧮 Co to jest matematycznie?

Ciąg Fibonacciego to sekwencja liczb, gdzie **każda kolejna liczba jest sumą dwóch poprzednich**:

```
F(0) = 0
F(1) = 1
F(n) = F(n-1) + F(n-2)   dla n > 1
```

### Ciąg wygląda tak:
```
0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, ...
```

### Skąd się biorą te liczby?
```
0 + 1 = 1
1 + 1 = 2
1 + 2 = 3
2 + 3 = 5
3 + 5 = 8
5 + 8 = 13
...
```

## 💡 Dlaczego to działa?

Wyobraź sobie króliki 🐰:
- Miesiąc 0: 0 par (jeszcze nie ma królików)
- Miesiąc 1: 1 para (młode króliki)
- Miesiąc 2: 1 para (jeszcze nie mogą się rozmnażać)
- Miesiąc 3: 2 pary (stara para + nowa para)
- Miesiąc 4: 3 pary (2 stare + 1 nowa od najstarszej pary)
- I tak dalej...

Każda "dorosła" para rodzi nową parę co miesiąc!

## 🖥️ Jak działa rekurencja?

```
ObliczFibonacciRekurencyjnie(fib1, fib2, n, wynik)
│
├── Czy fib1 > n? 
│   └── TAK → STOP (koniec rekurencji)
│   └── NIE → kontynuuj ↓
│
├── Dodaj fib1 do wyniku
│
└── Wywołaj siebie z nowymi wartościami:
    └── ObliczFibonacciRekurencyjnie(fib2, fib1+fib2, n, wynik)
```

### Przykład dla n = 10:

| Wywołanie | fib1 | fib2 | Akcja |
|-----------|------|------|-------|
| 1 | 0 | 1 | Dodaj 0, wywołaj dalej |
| 2 | 1 | 1 | Dodaj 1, wywołaj dalej |
| 3 | 1 | 2 | Dodaj 1, wywołaj dalej |
| 4 | 2 | 3 | Dodaj 2, wywołaj dalej |
| 5 | 3 | 5 | Dodaj 3, wywołaj dalej |
| 6 | 5 | 8 | Dodaj 5, wywołaj dalej |
| 7 | 8 | 13 | Dodaj 8, wywołaj dalej |
| 8 | 13 | 21 | 13 > 10 → STOP |

**Wynik:** `[0, 1, 1, 2, 3, 5, 8]`

## 🔑 Kluczowe elementy w kodzie:

| Element | Co robi |
|---------|---------|
| `fib1`, `fib2` | Dwie poprzednie liczby Fibonacciego |
| `fib1 > n` | Warunek bazowy - kończymy gdy przekroczymy limit |
| `fib1 + fib2` | Obliczamy następną liczbę |
| Rekurencja ogonowa | Wywołujemy funkcję z już obliczonymi wartościami (efektywne!) |

## ⚡ Złożoność:
- **Czasowa:** O(n) - przechodzimy przez każdą liczbę raz
- **Pamięciowa:** O(n) - przechowujemy wynik

---

# 3. Drzewo binarne (BST)

## 🧮 Co to jest matematycznie?

**BST = Binary Search Tree = Binarne Drzewo Poszukiwań**

To struktura danych w kształcie drzewa, gdzie:
- Każdy węzeł ma **maksymalnie 2 dzieci** (lewe i prawe)
- **Lewa strona:** wszystkie wartości MNIEJSZE od rodzica
- **Prawa strona:** wszystkie wartości WIĘKSZE od rodzica

### Przykład:
Wstawiamy: `[5, 3, 7, 1, 4, 6, 8]`

```
        5           ← Korzeń (pierwszy element)
       / \
      3   7         ← 3 < 5 (lewo), 7 > 5 (prawo)
     / \ / \
    1  4 6  8       ← każdy idzie w odpowiednie miejsce
```

## 💡 Dlaczego to działa?

Wyobraź sobie grę "za dużo / za mało":
- Szukasz liczby 6
- Patrzysz na korzeń (5): 6 > 5 → idź w prawo
- Patrzysz na 7: 6 < 7 → idź w lewo  
- Znalazłeś 6! ✅

Dzięki tej strukturze **szukanie jest bardzo szybkie** - nie musisz sprawdzać wszystkich elementów!

## 🖥️ Jak działa wstawianie (rekurencyjnie)?

```
Insert(korzeń, wartość)
│
├── Czy korzeń jest pusty (null)?
│   └── TAK → Utwórz nowy węzeł z wartością → KONIEC
│   └── NIE → kontynuuj ↓
│
├── Czy wartość < korzeń.Wartość?
│   └── TAK → korzeń.Lewy = Insert(korzeń.Lewy, wartość)
│   └── NIE → korzeń.Prawy = Insert(korzeń.Prawy, wartość)
│
└── Zwróć korzeń
```

### Przykład wstawiania 4 do drzewa z korzeniem 5 i lewym dzieckiem 3:

```
Insert(węzeł[5], 4)
│
├── 4 < 5? TAK → idź w lewo
│
└── Insert(węzeł[3], 4)
    │
    ├── 4 < 3? NIE → idź w prawo
    │
    └── Insert(null, 4)
        │
        └── Utwórz nowy węzeł[4] → KONIEC
```

## 🖥️ Przegląd KLP (Preorder) - rekurencyjnie:

**KLP = Korzeń → Lewy → Prawy**

```
PrzeglądKLP(węzeł)
│
├── Czy węzeł jest null?
│   └── TAK → KONIEC
│   └── NIE → kontynuuj ↓
│
├── 1. Wypisz wartość węzła (Korzeń)
├── 2. PrzeglądKLP(węzeł.Lewy)    (Lewy)
└── 3. PrzeglądKLP(węzeł.Prawy)   (Prawy)
```

### Inne rodzaje przeglądów:
| Nazwa | Kolejność | Przykład dla drzewa wyżej |
|-------|-----------|---------------------------|
| **KLP (Preorder)** | Korzeń→Lewy→Prawy | 5, 3, 1, 4, 7, 6, 8 |
| **LKP (Inorder)** | Lewy→Korzeń→Prawy | 1, 3, 4, 5, 6, 7, 8 ← posortowane! |
| **LPK (Postorder)** | Lewy→Prawy→Korzeń | 1, 4, 3, 6, 8, 7, 5 |

## 🔑 Kluczowe elementy w kodzie:

| Element | Co robi |
|---------|---------|
| `WezelDrzewa` | Klasa reprezentująca jeden węzeł (ma wartość + wskaźniki na dzieci) |
| `korzen == null` | Warunek bazowy - znaleźliśmy miejsce na nowy węzeł |
| `wartosc < korzen.Wartosc` | Decyduje czy iść w lewo czy prawo |
| Rekurencja | Schodzimy w głąb drzewa aż znajdziemy wolne miejsce |

## ⚡ Złożoność:
- **Wstawianie:** O(log n) średnio, O(n) w najgorszym przypadku
- **Szukanie:** O(log n) średnio, O(n) w najgorszym przypadku

---

# 4. Metoda bisekcji

## 🧮 Co to jest matematycznie?

**Bisekcja = dzielenie na pół**

Jest to metoda znajdowania **miejsca zerowego funkcji** (czyli takiego x, że f(x) = 0).

### Warunek konieczny (Twierdzenie Darboux):
Funkcja musi mieć **różne znaki na końcach przedziału**:
- f(a) > 0 i f(b) < 0, lub
- f(a) < 0 i f(b) > 0

Jeśli tak jest, to gdzieś "po drodze" funkcja **musi** przeciąć oś X!

### Graficznie:
```
    f(x)
     │     *
     │    /
     │   /
─────┼──●───────── x    ← tu jest miejsce zerowe!
     │ /
     │/
    *│
     │
    a        b
```

## 💡 Dlaczego to działa?

Wyobraź sobie grę "ciepło-zimno" ale z liczbami:

1. Masz przedział [a, b] i wiesz, że odpowiedź jest gdzieś w środku
2. Sprawdzasz środek (c) przedziału
3. Patrzysz "po której stronie" jest odpowiedź
4. Odrzucasz połowę przedziału
5. Powtarzasz aż przedział jest bardzo mały

**Za każdym razem zmniejszasz obszar poszukiwań o połowę!**

## 🖥️ Jak działa rekurencja?

```
BisekcjaRekurencyjna(a, b, fa, epsilon, funkcja)
│
├── Czy |b - a| <= epsilon?  (przedział wystarczająco mały?)
│   └── TAK → Zwróć (a+b)/2 → KONIEC
│   └── NIE → kontynuuj ↓
│
├── Oblicz c = (a+b)/2  (środek przedziału)
├── Oblicz fc = f(c)
│
├── Czy |fc| < epsilon?  (czy już blisko zera?)
│   └── TAK → Zwróć c → KONIEC
│   └── NIE → kontynuuj ↓
│
└── Czy fa * fc < 0?  (czy zero jest między a i c?)
    └── TAK → BisekcjaRekurencyjna(a, c, fa, epsilon, funkcja)
    └── NIE → BisekcjaRekurencyjna(c, b, fc, epsilon, funkcja)
```

### Przykład: f(x) = x² - 2, przedział [0, 2], epsilon = 0.01

Szukamy √2 ≈ 1.414...

| Krok | a | b | c = (a+b)/2 | f(c) = c² - 2 | Decyzja |
|------|---|---|-------------|---------------|---------|
| 1 | 0 | 2 | 1 | -1 | f(0)=-2, f(1)=-1 → różne znaki? NIE → idź [c,b] |
| 2 | 1 | 2 | 1.5 | 0.25 | f(1)=-1, f(1.5)=0.25 → różne znaki? TAK → idź [a,c] |
| 3 | 1 | 1.5 | 1.25 | -0.4375 | różne znaki z 1.5 → idź [c,b] |
| 4 | 1.25 | 1.5 | 1.375 | -0.109 | różne znaki z 1.5 → idź [c,b] |
| 5 | 1.375 | 1.5 | 1.4375 | 0.066 | różne znaki z 1.375 → idź [a,c] |
| ... | ... | ... | ... | ... | ... |
| n | 1.414 | 1.415 | **1.4142** | ~0 | **KONIEC!** |

## 🔑 Kluczowe elementy w kodzie:

| Element | Co robi |
|---------|---------|
| `epsilon` | Dokładność - jak mały ma być przedział |
| `fa * fc < 0` | Sprawdza czy znaki są różne (iloczyn ujemny = różne znaki) |
| `(a + b) / 2.0` | Oblicza środek przedziału |
| `Math.Abs(b - a)` | Długość aktualnego przedziału |
| Rekurencja | Wywołuje się z nowym (mniejszym) przedziałem |

## ⚡ Złożoność:
- **Czasowa:** O(log₂((b-a)/epsilon)) - za każdym krokiem zmniejszamy przedział o połowę

## ⚠️ Kiedy używać?
- ✅ Gdy funkcja jest ciągła
- ✅ Gdy znasz przedział [a,b] gdzie funkcja zmienia znak
- ❌ Nie znajdzie pierwiastka jeśli funkcja nie przecina osi X w danym przedziale
- ❌ Nie znajdzie pierwiastka wielokrotnego (np. x² = 0 w punkcie x = 0)

---

# 🔄 Podsumowanie - Rekurencja

## Co to jest rekurencja?

**Rekurencja = funkcja wywołuje samą siebie**

Każda funkcja rekurencyjna musi mieć:
1. **Warunek bazowy** - kiedy przestać się wywoływać
2. **Krok rekurencyjny** - wywołanie siebie z "mniejszym" problemem

### Analogia:
Wyobraź sobie stos talerzy do umycia:
- Warunek bazowy: "Jeśli nie ma talerzy → SKOŃCZONE"
- Krok rekurencyjny: "Umyj jeden talerz, potem umyj resztę stosu"

```
UmyjTalerze(stos):
    jeśli stos jest pusty:
        KONIEC                      ← warunek bazowy
    inaczej:
        umyj pierwszy talerz
        UmyjTalerze(reszta stosu)   ← krok rekurencyjny
```

## Porównanie rekurencji vs iteracji:

| Aspekt | Rekurencja | Iteracja (pętle) |
|--------|------------|------------------|
| Czytelność | Często bardziej elegancka | Może być prostsza |
| Pamięć | Zużywa stos (może być problem dla głębokiej rekurencji) | Stała ilość pamięci |
| Szybkość | Może być wolniejsza (narzut wywołań) | Zazwyczaj szybsza |
| Naturalność | Lepsza dla drzew, dziel i zwyciężaj | Lepsza dla prostych pętli |

---

# 📝 Słowniczek

| Termin | Znaczenie |
|--------|-----------|
| **Rekurencja** | Funkcja wywołująca samą siebie |
| **Warunek bazowy** | Moment gdy rekurencja się kończy |
| **BST** | Binarne Drzewo Poszukiwań |
| **Korzeń** | Najwyższy węzeł drzewa |
| **Liść** | Węzeł bez dzieci |
| **Bisekcja** | Dzielenie przedziału na pół |
| **Miejsce zerowe** | Punkt gdzie f(x) = 0 |
| **Epsilon (ε)** | Dokładność obliczeń |
| **Twierdzenie Darboux** | Jeśli funkcja ciągła ma różne znaki na końcach przedziału, to gdzieś w środku jest zero |
| **Złożoność czasowa** | Ile operacji wykonuje algorytm |
| **O(n)** | Złożoność liniowa - czas rośnie proporcjonalnie do n |
| **O(log n)** | Złożoność logarytmiczna - bardzo szybka |

---

*Dokument wygenerowany dla PS2 - Algorytmy SEM III*
