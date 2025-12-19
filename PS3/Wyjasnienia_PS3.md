# 📚 Wyjaśnienia zadań PS3 - Algorytmy

Ten dokument wyjaśnia w prosty sposób wszystkie algorytmy z PS3.

---

## 📋 Spis treści
1. [Problem 1 - Subkod genetyczny (LCS)](#1-problem-1---subkod-genetyczny-lcs)
2. [Problem 3 - Algorytm Kruskala (MST)](#2-problem-3---algorytm-kruskala-mst)

---

# 1. Problem 1 - Subkod genetyczny (LCS)

## 🧮 Co to jest matematycznie?

**LCS = Longest Common Subsequence = Najdłuższy Wspólny Podciąg**

Mamy dwa ciągi znaków i szukamy **najdłuższego ciągu**, który:
- Występuje w **obu** ciągach
- Zachowuje **kolejność** znaków (ale znaki nie muszą być obok siebie!)

### ⚠️ WAŻNE: Podciąg vs Podłańcuch

| Pojęcie | Definicja | Przykład dla "ABCDE" |
|---------|-----------|---------------------|
| **Podłańcuch** | Ciągłe znaki obok siebie | "BCD", "ABC", "DE" |
| **Podciąg** | Znaki w kolejności, ale niekoniecznie obok siebie | "ACE", "BD", "AE" |

### Przykład z zadania:

```
Ciąg 1: DDAF6AB34ADE
Ciąg 2: BD16A436BAF
```

Szukamy najdłuższego podciągu, który występuje w obu:

```
DDAF6AB34ADE
 ↓  ↓ ↓  ↓
 D  6 A  4     ... i tak dalej

BD16A436BAF
  ↓↓↓↓
  16A4         ... i tak dalej
```

**Wynik: `D6A4AF`** (długość 6) - te znaki występują w obu ciągach w tej samej kolejności!

## 💡 Dlaczego to działa? (Programowanie dynamiczne)

### Idea "podproblemów"

Zamiast rozwiązywać cały problem naraz, dzielimy go na mniejsze:

> "Jaki jest LCS dla **pierwszych i znaków** ciągu 1 i **pierwszych j znaków** ciągu 2?"

Budujemy tabelę `dp[i][j]` gdzie przechowujemy odpowiedzi na te mniejsze pytania.

### Reguły wypełniania tabeli:

```
Jeśli s1[i] == s2[j]:
    dp[i][j] = dp[i-1][j-1] + 1    ← znaki się zgadzają, dodaj 1 do poprzedniego LCS
    
Jeśli s1[i] != s2[j]:
    dp[i][j] = max(dp[i-1][j], dp[i][j-1])    ← weź lepszy wynik z pominięcia znaku
```

### Graficznie:

```
Jeśli znaki RÓWNE:           Jeśli znaki RÓŻNE:

   ↖                            ↑
    \                           |
     → dp[i][j] = +1          max(↑, ←) → dp[i][j]
```

## 🖥️ Przykład krok po kroku

Dla ciągów: `s1 = "ABCD"` i `s2 = "AEBD"`

### Krok 1: Tworzymy tabelę

```
        ""   A   E   B   D
    ""   0   0   0   0   0
    A    0   ?   ?   ?   ?
    B    0   ?   ?   ?   ?
    C    0   ?   ?   ?   ?
    D    0   ?   ?   ?   ?
```

### Krok 2: Wypełniamy tabelę

```
        ""   A   E   B   D
    ""   0   0   0   0   0
    A    0   1   1   1   1     ← A==A, więc 0+1=1
    B    0   1   1   2   2     ← B==B, więc 1+1=2
    C    0   1   1   2   2     ← C nie pasuje, max(2,2)=2
    D    0   1   1   2   3     ← D==D, więc 2+1=3
```

### Krok 3: Odczytujemy wynik

- Długość LCS = `dp[4][4]` = **3**
- Odtwarzamy LCS idąc "po przekątnej" gdy znaki się zgadzają

**LCS = "ABD"** ✅

## 🔄 Rekurencyjne odtwarzanie LCS (Backtracking)

```
OdtworzLCS(s1, s2, dp, i, j)
│
├── Czy i == 0 lub j == 0?
│   └── TAK → zwróć "" (pusty ciąg) ← WARUNEK BAZOWY
│
├── Czy s1[i-1] == s2[j-1]?  (znaki się zgadzają?)
│   └── TAK → zwróć OdtworzLCS(..., i-1, j-1) + s1[i-1]
│             ↑ idź po przekątnej i dodaj znak
│
└── NIE → idź w kierunku większej wartości:
    ├── dp[i-1][j] > dp[i][j-1]? → OdtworzLCS(..., i-1, j)
    └── inaczej                  → OdtworzLCS(..., i, j-1)
```

### Wizualnie na tabeli:

```
        ""   A   E   B   D
    ""   0   0   0   0   0
    A    0  [1]→ 1   1   1
    B    0   1   1  [2]→ 2
    C    0   1   1   2   2
    D    0   1   1   2  [3]  ← START
                         ↑
Idziemy: (4,4)→(3,3)→(2,2)→(1,1)→(0,0)
                ↓       ↓       ↓
              D=D     B=B     A=A
              
LCS = "A" + "B" + "D" = "ABD"
```

## 🔑 Kluczowe elementy w kodzie:

| Element | Co robi |
|---------|---------|
| `dp[i,j]` | Przechowuje długość LCS dla s1[0..i-1] i s2[0..j-1] |
| `dp[i-1,j-1] + 1` | Gdy znaki równe - rozszerzamy LCS |
| `Math.Max(dp[i-1,j], dp[i,j-1])` | Gdy różne - bierzemy lepszy wynik |
| Backtracking | Rekurencyjnie odtwarzamy sam ciąg LCS |

## ⚡ Złożoność:
- **Czasowa:** O(m × n) - wypełniamy tabelę m×n
- **Pamięciowa:** O(m × n) - przechowujemy tabelę

## 📝 Kontekst zadania (genetyka)

W zadaniu charakterystyki odmian tulipanów to ciągi znaków reprezentujące geny odpowiedzialne za kolor. **Subkod genetyczny** to najdłuższy wspólny podciąg - pokazuje, które geny są wspólne dla obu odmian i mogą być dziedziczone przy krzyżowaniu.

---

# 2. Problem 3 - Algorytm Kruskala (MST)

## 🧮 Co to jest matematycznie?

**MST = Minimum Spanning Tree = Minimalne Drzewo Rozpinające**

Mamy graf (punkty połączone liniami z wagami) i chcemy:
- Połączyć **wszystkie** punkty
- Użyć **jak najmniejszej** sumy wag
- Bez tworzenia **cykli** (kółek)

### Przykład:

```
    2
A ─────── B
│ \       │
│  \3     │1
│   \     │
5    \    │
│     \   │
│      \  │
C ─────── D
    4
```

**MST powinno mieć:**
- n-1 krawędzi (gdzie n = liczba wierzchołków)
- Minimalną sumę wag
- Łączyć wszystkie wierzchołki

**Rozwiązanie:** A-B (2), B-D (1), A-C (5) lub A-D (3)
Suma = 2 + 1 + 3 = **6**

## 💡 Jak działa algorytm Kruskala?

### Idea (zachłanna):

1. **Posortuj** wszystkie krawędzie według wag (od najmniejszej)
2. **Bierz** krawędzie po kolei
3. **Sprawdzaj** czy dodanie krawędzi tworzy cykl
4. Jeśli NIE tworzy cyklu → **dodaj** do drzewa
5. Jeśli tworzy cykl → **pomiń**
6. **Powtarzaj** aż masz n-1 krawędzi

### Wizualnie:

```
Krawędzie posortowane:
B-D: 1  ✅ dodaj (nie ma cyklu)
A-B: 2  ✅ dodaj (nie ma cyklu)  
A-D: 3  ✅ dodaj (nie ma cyklu)
C-D: 4  ❌ pomiń (tworzy cykl A-B-D-A)
A-C: 5  pomiń (już mamy n-1 krawędzi)

MST: {B-D, A-B, A-D}, suma = 6
```

## 🔄 Jak sprawdzić czy krawędź tworzy cykl?

### Struktura Union-Find (Find-Union / Disjoint Set Union)

To sprytna struktura danych, która:
- Grupuje wierzchołki w **zbiory**
- Szybko sprawdza czy dwa wierzchołki są w **tym samym zbiorze**
- Szybko **łączy** dwa zbiory

### Operacje:

| Operacja | Co robi |
|----------|---------|
| `Find(x)` | Znajdź "szefa" (korzeń) zbioru, do którego należy x |
| `Union(x, y)` | Połącz zbiory zawierające x i y |

### Jak to pomaga?

- Na początku: każdy wierzchołek jest w swoim własnym zbiorze
- Dodając krawędź (u, v):
  - Jeśli `Find(u) == Find(v)` → są w tym samym zbiorze → **CYKL!**
  - Jeśli `Find(u) != Find(v)` → różne zbiory → **OK, dodaj i połącz zbiory**

## 🖥️ Przykład Union-Find krok po kroku

```
Wierzchołki: 1, 2, 3, 4
Początek - każdy jest swoim szefem:

Zbiór 1: {1}   szef: 1
Zbiór 2: {2}   szef: 2
Zbiór 3: {3}   szef: 3
Zbiór 4: {4}   szef: 4
```

**Dodajemy krawędź 1-2:**
```
Find(1) = 1
Find(2) = 2
1 ≠ 2 → OK, dodaj krawędź
Union(1, 2) → łączymy zbiory

Zbiór {1, 2}   szef: 1
Zbiór {3}      szef: 3
Zbiór {4}      szef: 4
```

**Dodajemy krawędź 2-3:**
```
Find(2) = 1  (bo 2 należy do zbioru z szefem 1)
Find(3) = 3
1 ≠ 3 → OK, dodaj krawędź
Union(1, 3)

Zbiór {1, 2, 3}   szef: 1
Zbiór {4}         szef: 4
```

**Próbujemy dodać krawędź 1-3:**
```
Find(1) = 1
Find(3) = 1  (3 należy teraz do zbioru z szefem 1)
1 == 1 → CYKL! Nie dodawaj tej krawędzi!
```

## 🔄 Rekurencyjny Find z kompresją ścieżki

```
Find(rodzic[], x)
│
├── Czy rodzic[x] == x?  (czy x jest swoim własnym szefem?)
│   └── TAK → zwróć x ← WARUNEK BAZOWY
│
└── NIE → 
    rodzic[x] = Find(rodzic, rodzic[x])  ← kompresja ścieżki!
    zwróć rodzic[x]
```

### Co to jest kompresja ścieżki?

Bez kompresji struktura może wyglądać tak (długi łańcuch):
```
1 → 2 → 3 → 4 → 5
```
Każde `Find(1)` musi przejść całą drogę!

Z kompresją po pierwszym `Find(1)`:
```
    5
  / | \
 1  2  3
    |
    4
```
Wszystkie elementy wskazują bezpośrednio na szefa!

## 🔄 Sortowanie przez scalanie (Merge Sort)

W algorytmie Kruskala musimy posortować krawędzie. Używamy **Merge Sort** bo ma złożoność O(m log m).

### Idea (dziel i zwyciężaj):

1. **Podziel** listę na pół
2. **Rekurencyjnie** posortuj obie połowy
3. **Scal** dwie posortowane połowy

```
MergeSort(lista)
│
├── Czy lista ma ≤1 element?
│   └── TAK → zwróć listę ← WARUNEK BAZOWY
│
├── Podziel listę na pół: lewa, prawa
│
├── lewa = MergeSort(lewa)    ← rekurencja
├── prawa = MergeSort(prawa)  ← rekurencja
│
└── zwróć Merge(lewa, prawa)  ← scalanie
```

### Przykład:

```
[8, 3, 5, 1, 9, 2]
        ↓ dziel
   [8, 3, 5]    [1, 9, 2]
       ↓             ↓
  [8] [3,5]     [1] [9,2]
       ↓             ↓
  [8] [3][5]    [1] [9][2]
       ↓             ↓
  [8] [3,5]     [1] [2,9]
       ↓             ↓
   [3,5,8]      [1,2,9]
        ↓ scal
   [1, 2, 3, 5, 8, 9]
```

### Scalanie dwóch posortowanych list:

```
lewa:  [3, 5, 8]
prawa: [1, 2, 9]

Porównuj pierwsze elementy i bierz mniejszy:
1 < 3 → bierz 1 → [1]
2 < 3 → bierz 2 → [1, 2]
3 < 9 → bierz 3 → [1, 2, 3]
5 < 9 → bierz 5 → [1, 2, 3, 5]
8 < 9 → bierz 8 → [1, 2, 3, 5, 8]
zostaje 9      → [1, 2, 3, 5, 8, 9]
```

## 🖥️ Cały algorytm Kruskala - schemat

```
┌─────────────────────────────────────────────────────┐
│  WEJŚCIE: Graf G = (V, E) z wagami                 │
│  V = wierzchołki, E = krawędzie                    │
└─────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────┐
│  1. Utwórz listę wszystkich krawędzi               │
└─────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────┐
│  2. SORTUJ krawędzie wg wag (MergeSort)            │
│     O(m log m)                                     │
└─────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────┐
│  3. Inicjalizuj Union-Find                         │
│     Każdy wierzchołek w osobnym zbiorze            │
└─────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────┐
│  4. Dla każdej krawędzi (u, v, waga):              │
│                                                     │
│     p = Find(u)                                    │
│     q = Find(v)                                    │
│                                                     │
│     if p ≠ q:           ← nie ma cyklu            │
│        dodaj krawędź do MST                        │
│        Union(p, q)      ← połącz zbiory           │
│                                                     │
│     if MST ma n-1 krawędzi:                        │
│        KONIEC                                       │
└─────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────┐
│  WYJŚCIE: MST - lista krawędzi i suma wag          │
└─────────────────────────────────────────────────────┘
```

## 🔑 Kluczowe elementy w kodzie:

| Element | Co robi |
|---------|---------|
| `List<Krawedz>` | Lista krawędzi z wagą, początkiem i końcem |
| `MergeSort()` | Rekurencyjne sortowanie krawędzi wg wag |
| `Merge()` | Scalanie dwóch posortowanych list |
| `rodzic[]` | Tablica przechowująca "szefa" każdego wierzchołka |
| `ranga[]` | Tablica do optymalizacji Union (łączenie mniejszego do większego) |
| `Find()` | Rekurencyjne szukanie korzenia z kompresją ścieżki |
| `Union()` | Łączenie dwóch zbiorów z użyciem rangi |

## ⚡ Złożoność:
- **Sortowanie:** O(m log m) gdzie m = liczba krawędzi
- **Union-Find:** prawie O(1) dla każdej operacji (amortyzowane)
- **Całość:** O(m log m)

## ⚠️ Kiedy używać Kruskala?
- ✅ Gdy graf ma **mało krawędzi** (rzadki)
- ✅ Gdy krawędzie są już posortowane lub łatwo je posortować
- ❌ Dla gęstych grafów lepszy może być algorytm Prima

---

# 🔄 Podsumowanie technik

## Programowanie dynamiczne (Problem 1)

**Idea:** Rozwiąż mniejsze podproblemy, zapisz wyniki, użyj do rozwiązania większych.

```
Duży problem = f(mniejsze problemy)
```

**Kiedy używać:**
- Problem można podzielić na nakładające się podproblemy
- Optymalna struktura: rozwiązanie dużego problemu zależy od rozwiązań mniejszych

## Strategia zachłanna (Problem 3)

**Idea:** W każdym kroku wybierz **lokalnie najlepszą** opcję.

```
Zawsze bierz najmniejszą krawędź (jeśli nie tworzy cyklu)
```

**Kiedy używać:**
- Lokalne optima prowadzą do globalnego optimum
- Problem ma "właściwość zachłanną"

## Dziel i zwyciężaj (Merge Sort)

**Idea:** Podziel problem na mniejsze, rozwiąż rekurencyjnie, połącz wyniki.

```
1. Podziel
2. Zwyciężaj (rekurencyjnie)
3. Połącz
```

**Kiedy używać:**
- Problem można łatwo podzielić na niezależne części
- Łączenie wyników jest efektywne

---

# 📝 Słowniczek

| Termin | Znaczenie |
|--------|-----------|
| **LCS** | Longest Common Subsequence - najdłuższy wspólny podciąg |
| **Podciąg** | Znaki w kolejności, ale niekoniecznie obok siebie |
| **Programowanie dynamiczne** | Technika dzielenia problemu na podproblemy z zapamiętywaniem wyników |
| **Backtracking** | Odtwarzanie rozwiązania "wstecz" |
| **MST** | Minimum Spanning Tree - minimalne drzewo rozpinające |
| **Graf** | Struktura złożona z wierzchołków i krawędzi |
| **Drzewo rozpinające** | Podgraf łączący wszystkie wierzchołki bez cykli |
| **Cykl** | Ścieżka zaczynająca i kończąca się w tym samym wierzchołku |
| **Union-Find** | Struktura do zarządzania rozłącznymi zbiorami |
| **Kompresja ścieżki** | Optymalizacja Find - skracanie łańcuchów |
| **Merge Sort** | Sortowanie przez scalanie |
| **Strategia zachłanna** | Wybieranie lokalnie najlepszej opcji |
| **Dziel i zwyciężaj** | Podział problemu na mniejsze części |
| **Lista incydencji** | Sposób reprezentacji grafu - dla każdego wierzchołka lista sąsiadów |

---

# 📊 Porównanie złożoności

| Algorytm | Czasowa | Pamięciowa |
|----------|---------|------------|
| LCS (programowanie dynamiczne) | O(m × n) | O(m × n) |
| Merge Sort | O(n log n) | O(n) |
| Find (z kompresją) | ~O(1)* | O(n) |
| Union (z rangą) | ~O(1)* | O(n) |
| Kruskal | O(m log m) | O(n + m) |

*amortyzowana złożoność

---

*Dokument wygenerowany dla PS3 - Algorytmy SEM III*
