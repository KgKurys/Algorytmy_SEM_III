using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PS4
{
    #region Część A - Drzewo AVL

    /// <summary>
    /// Węzeł drzewa AVL
    /// </summary>
    public class AVLNode
    {
        public int Key { get; set; }
        public int Balance { get; set; } // -1, 0, 1
        public AVLNode? Left { get; set; }
        public AVLNode? Right { get; set; }

        public AVLNode(int key)
        {
            Key = key;
            Balance = 0;
            Left = null;
            Right = null;
        }
    }

    /// <summary>
    /// Drzewo AVL z operacjami search, insert, delete, KLP
    /// </summary>
    public class AVLTree
    {
        public AVLNode? Root { get; private set; }

        public AVLTree()
        {
            Root = null;
        }

        #region Search
        public AVLNode? Search(int key)
        {
            return SearchRecursive(Root, key);
        }

        private AVLNode? SearchRecursive(AVLNode? node, int key)
        {
            if (node == null) return null;
            if (key == node.Key) return node;
            if (key < node.Key) return SearchRecursive(node.Left, key);
            return SearchRecursive(node.Right, key);
        }

        public (AVLNode? node, int level) SearchWithLevel(int key)
        {
            return SearchWithLevelRecursive(Root, key, 0);
        }

        private (AVLNode? node, int level) SearchWithLevelRecursive(AVLNode? node, int key, int level)
        {
            if (node == null) return (null, -1);
            if (key == node.Key) return (node, level);
            if (key < node.Key) return SearchWithLevelRecursive(node.Left, key, level + 1);
            return SearchWithLevelRecursive(node.Right, key, level + 1);
        }
        #endregion

        #region Insert
        public bool Insert(int key)
        {
            bool heightIncreased = false;
            bool inserted = false;
            Root = InsertRecursive(Root, key, ref heightIncreased, ref inserted);
            return inserted;
        }

        private AVLNode InsertRecursive(AVLNode? node, int key, ref bool heightIncreased, ref bool inserted)
        {
            if (node == null)
            {
                heightIncreased = true;
                inserted = true;
                return new AVLNode(key);
            }

            if (key == node.Key)
            {
                // Duplikat - nie wstawiamy
                heightIncreased = false;
                inserted = false;
                return node;
            }

            if (key < node.Key)
            {
                node.Left = InsertRecursive(node.Left, key, ref heightIncreased, ref inserted);
                if (heightIncreased)
                {
                    switch (node.Balance)
                    {
                        case 1: // było prawe cięższe, teraz wyważone
                            node.Balance = 0;
                            heightIncreased = false;
                            break;
                        case 0: // było wyważone, teraz lewe cięższe
                            node.Balance = -1;
                            break;
                        case -1: // było lewe cięższe, trzeba rotować
                            node = BalanceLeft(node);
                            heightIncreased = false;
                            break;
                    }
                }
            }
            else
            {
                node.Right = InsertRecursive(node.Right, key, ref heightIncreased, ref inserted);
                if (heightIncreased)
                {
                    switch (node.Balance)
                    {
                        case -1: // było lewe cięższe, teraz wyważone
                            node.Balance = 0;
                            heightIncreased = false;
                            break;
                        case 0: // było wyważone, teraz prawe cięższe
                            node.Balance = 1;
                            break;
                        case 1: // było prawe cięższe, trzeba rotować
                            node = BalanceRight(node);
                            heightIncreased = false;
                            break;
                    }
                }
            }

            return node;
        }
        #endregion

        #region Delete
        public bool Delete(int key)
        {
            bool heightDecreased = false;
            bool deleted = false;
            Root = DeleteRecursive(Root, key, ref heightDecreased, ref deleted);
            return deleted;
        }

        private AVLNode? DeleteRecursive(AVLNode? node, int key, ref bool heightDecreased, ref bool deleted)
        {
            if (node == null)
            {
                heightDecreased = false;
                deleted = false;
                return null;
            }

            if (key < node.Key)
            {
                node.Left = DeleteRecursive(node.Left, key, ref heightDecreased, ref deleted);
                if (heightDecreased)
                {
                    node = BalanceAfterLeftDeletion(node, ref heightDecreased);
                }
            }
            else if (key > node.Key)
            {
                node.Right = DeleteRecursive(node.Right, key, ref heightDecreased, ref deleted);
                if (heightDecreased)
                {
                    node = BalanceAfterRightDeletion(node, ref heightDecreased);
                }
            }
            else
            {
                // Znaleziono węzeł do usunięcia
                deleted = true;

                if (node.Left == null && node.Right == null)
                {
                    // Liść
                    heightDecreased = true;
                    return null;
                }
                else if (node.Left == null)
                {
                    // Tylko prawe poddrzewo
                    heightDecreased = true;
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    // Tylko lewe poddrzewo
                    heightDecreased = true;
                    return node.Left;
                }
                else
                {
                    // Dwa poddrzewa - znajdź największy element z lewego poddrzewa
                    AVLNode? maxNode = FindMax(node.Left);
                    node.Key = maxNode!.Key;
                    node.Left = DeleteRecursive(node.Left, maxNode.Key, ref heightDecreased, ref deleted);
                    if (heightDecreased)
                    {
                        node = BalanceAfterLeftDeletion(node, ref heightDecreased);
                    }
                }
            }

            return node;
        }

        private AVLNode? FindMax(AVLNode? node)
        {
            if (node == null) return null;
            while (node.Right != null)
            {
                node = node.Right;
            }
            return node;
        }

        private AVLNode BalanceAfterLeftDeletion(AVLNode node, ref bool heightDecreased)
        {
            switch (node.Balance)
            {
                case -1: // było lewe cięższe, teraz wyważone
                    node.Balance = 0;
                    break;
                case 0: // było wyważone, teraz prawe cięższe
                    node.Balance = 1;
                    heightDecreased = false;
                    break;
                case 1: // było prawe cięższe, trzeba rotować
                    int rightBalance = node.Right!.Balance;
                    node = BalanceRight(node);
                    if (rightBalance == 0)
                    {
                        heightDecreased = false;
                    }
                    break;
            }
            return node;
        }

        private AVLNode BalanceAfterRightDeletion(AVLNode node, ref bool heightDecreased)
        {
            switch (node.Balance)
            {
                case 1: // było prawe cięższe, teraz wyważone
                    node.Balance = 0;
                    break;
                case 0: // było wyważone, teraz lewe cięższe
                    node.Balance = -1;
                    heightDecreased = false;
                    break;
                case -1: // było lewe cięższe, trzeba rotować
                    int leftBalance = node.Left!.Balance;
                    node = BalanceLeft(node);
                    if (leftBalance == 0)
                    {
                        heightDecreased = false;
                    }
                    break;
            }
            return node;
        }
        #endregion

        #region Rotations
        private AVLNode RotateRight(AVLNode node)
        {
            AVLNode newRoot = node.Left!;
            node.Left = newRoot.Right;
            newRoot.Right = node;
            return newRoot;
        }

        private AVLNode RotateLeft(AVLNode node)
        {
            AVLNode newRoot = node.Right!;
            node.Right = newRoot.Left;
            newRoot.Left = node;
            return newRoot;
        }

        private AVLNode BalanceLeft(AVLNode node)
        {
            AVLNode left = node.Left!;

            if (left.Balance == -1)
            {
                // LL rotation
                node.Balance = 0;
                left.Balance = 0;
                return RotateRight(node);
            }
            else if (left.Balance == 1)
            {
                // LR rotation
                AVLNode leftRight = left.Right!;

                if (leftRight.Balance == -1)
                {
                    node.Balance = 1;
                    left.Balance = 0;
                }
                else if (leftRight.Balance == 0)
                {
                    node.Balance = 0;
                    left.Balance = 0;
                }
                else // leftRight.Balance == 1
                {
                    node.Balance = 0;
                    left.Balance = -1;
                }
                leftRight.Balance = 0;

                node.Left = RotateLeft(left);
                return RotateRight(node);
            }
            else // left.Balance == 0 (tylko przy usuwaniu)
            {
                node.Balance = -1;
                left.Balance = 1;
                return RotateRight(node);
            }
        }

        private AVLNode BalanceRight(AVLNode node)
        {
            AVLNode right = node.Right!;

            if (right.Balance == 1)
            {
                // RR rotation
                node.Balance = 0;
                right.Balance = 0;
                return RotateLeft(node);
            }
            else if (right.Balance == -1)
            {
                // RL rotation
                AVLNode rightLeft = right.Left!;

                if (rightLeft.Balance == 1)
                {
                    node.Balance = -1;
                    right.Balance = 0;
                }
                else if (rightLeft.Balance == 0)
                {
                    node.Balance = 0;
                    right.Balance = 0;
                }
                else // rightLeft.Balance == -1
                {
                    node.Balance = 0;
                    right.Balance = 1;
                }
                rightLeft.Balance = 0;

                node.Right = RotateRight(right);
                return RotateLeft(node);
            }
            else // right.Balance == 0 (tylko przy usuwaniu)
            {
                node.Balance = 1;
                right.Balance = -1;
                return RotateLeft(node);
            }
        }
        #endregion

        #region Traversals
        public List<(int key, int balance)> PreOrderKLP()
        {
            var result = new List<(int key, int balance)>();
            PreOrderRecursive(Root, result);
            return result;
        }

        private void PreOrderRecursive(AVLNode? node, List<(int key, int balance)> result)
        {
            if (node == null) return;
            result.Add((node.Key, node.Balance));
            PreOrderRecursive(node.Left, result);
            PreOrderRecursive(node.Right, result);
        }

        public List<List<int>> LevelOrder()
        {
            var result = new List<List<int>>();
            if (Root == null) return result;

            var queue = new Queue<AVLNode>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                var level = new List<int>();

                for (int i = 0; i < levelSize; i++)
                {
                    var node = queue.Dequeue();
                    level.Add(node.Key);

                    if (node.Left != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);
                }

                result.Add(level);
            }

            return result;
        }
        #endregion

        #region File Operations
        public void SaveToFileKLP(string filename)
        {
            var elements = PreOrderKLP();
            using var writer = new StreamWriter(filename);
            foreach (var (key, balance) in elements)
            {
                writer.Write($"{key}({balance}) ");
            }
        }

        public static AVLTree LoadFromFile(string filename)
        {
            var tree = new AVLTree();
            if (!File.Exists(filename))
            {
                Console.WriteLine($"Plik {filename} nie istnieje.");
                return tree;
            }

            string content = File.ReadAllText(filename);
            var numbers = content.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var numStr in numbers)
            {
                // Obsługa formatu z komentarzami //
                if (numStr.StartsWith("//")) break;

                if (int.TryParse(numStr, out int num))
                {
                    tree.Insert(num);
                }
            }

            return tree;
        }
        #endregion
    }

    #endregion

    #region Część B - Dwa drzewa AVL (PESEL <-> Telefon)

    /// <summary>
    /// Węzeł drzewa AVL dla numerów PESEL
    /// </summary>
    public class PeselNode
    {
        public long Pesel { get; set; }
        public int Balance { get; set; }
        public PeselNode? Left { get; set; }
        public PeselNode? Right { get; set; }
        public PhoneNode? PhoneLink { get; set; } // wskaźnik na węzeł z numerem telefonu

        public PeselNode(long pesel)
        {
            Pesel = pesel;
            Balance = 0;
        }
    }

    /// <summary>
    /// Węzeł drzewa AVL dla numerów telefonu
    /// </summary>
    public class PhoneNode
    {
        public long Phone { get; set; }
        public int Balance { get; set; }
        public PhoneNode? Left { get; set; }
        public PhoneNode? Right { get; set; }
        public PeselNode? PeselLink { get; set; } // wskaźnik na węzeł z numerem PESEL

        public PhoneNode(long phone)
        {
            Phone = phone;
            Balance = 0;
        }
    }

    /// <summary>
    /// Słownik dwóch drzew AVL połączonych wskaźnikowo
    /// </summary>
    public class PeselPhoneDictionary
    {
        private PeselNode? peselRoot;
        private PhoneNode? phoneRoot;

        #region Search
        public PeselNode? SearchByPesel(long pesel)
        {
            return SearchPeselRecursive(peselRoot, pesel);
        }

        private PeselNode? SearchPeselRecursive(PeselNode? node, long pesel)
        {
            if (node == null) return null;
            if (pesel == node.Pesel) return node;
            if (pesel < node.Pesel) return SearchPeselRecursive(node.Left, pesel);
            return SearchPeselRecursive(node.Right, pesel);
        }

        public PhoneNode? SearchByPhone(long phone)
        {
            return SearchPhoneRecursive(phoneRoot, phone);
        }

        private PhoneNode? SearchPhoneRecursive(PhoneNode? node, long phone)
        {
            if (node == null) return null;
            if (phone == node.Phone) return node;
            if (phone < node.Phone) return SearchPhoneRecursive(node.Left, phone);
            return SearchPhoneRecursive(node.Right, phone);
        }

        public long? FindPhoneByPesel(long pesel)
        {
            var peselNode = SearchByPesel(pesel);
            return peselNode?.PhoneLink?.Phone;
        }

        public long? FindPeselByPhone(long phone)
        {
            var phoneNode = SearchByPhone(phone);
            return phoneNode?.PeselLink?.Pesel;
        }
        #endregion

        #region Insert
        public bool Insert(long pesel, long phone)
        {
            // Sprawdź czy już istnieje
            if (SearchByPesel(pesel) != null)
            {
                Console.WriteLine($"Numer PESEL {pesel} już istnieje w bazie.");
                return false;
            }
            if (SearchByPhone(phone) != null)
            {
                Console.WriteLine($"Numer telefonu {phone} już istnieje w bazie.");
                return false;
            }

            // Utwórz nowe węzły
            var peselNode = new PeselNode(pesel);
            var phoneNode = new PhoneNode(phone);

            // Połącz wskaźnikami
            peselNode.PhoneLink = phoneNode;
            phoneNode.PeselLink = peselNode;

            // Wstaw do drzew
            bool heightIncreased = false;
            peselRoot = InsertPesel(peselRoot, peselNode, ref heightIncreased);
            heightIncreased = false;
            phoneRoot = InsertPhone(phoneRoot, phoneNode, ref heightIncreased);

            return true;
        }

        private PeselNode InsertPesel(PeselNode? root, PeselNode newNode, ref bool heightIncreased)
        {
            if (root == null)
            {
                heightIncreased = true;
                return newNode;
            }

            if (newNode.Pesel < root.Pesel)
            {
                root.Left = InsertPesel(root.Left, newNode, ref heightIncreased);
                if (heightIncreased)
                {
                    switch (root.Balance)
                    {
                        case 1:
                            root.Balance = 0;
                            heightIncreased = false;
                            break;
                        case 0:
                            root.Balance = -1;
                            break;
                        case -1:
                            root = BalancePeselLeft(root);
                            heightIncreased = false;
                            break;
                    }
                }
            }
            else
            {
                root.Right = InsertPesel(root.Right, newNode, ref heightIncreased);
                if (heightIncreased)
                {
                    switch (root.Balance)
                    {
                        case -1:
                            root.Balance = 0;
                            heightIncreased = false;
                            break;
                        case 0:
                            root.Balance = 1;
                            break;
                        case 1:
                            root = BalancePeselRight(root);
                            heightIncreased = false;
                            break;
                    }
                }
            }

            return root;
        }

        private PhoneNode InsertPhone(PhoneNode? root, PhoneNode newNode, ref bool heightIncreased)
        {
            if (root == null)
            {
                heightIncreased = true;
                return newNode;
            }

            if (newNode.Phone < root.Phone)
            {
                root.Left = InsertPhone(root.Left, newNode, ref heightIncreased);
                if (heightIncreased)
                {
                    switch (root.Balance)
                    {
                        case 1:
                            root.Balance = 0;
                            heightIncreased = false;
                            break;
                        case 0:
                            root.Balance = -1;
                            break;
                        case -1:
                            root = BalancePhoneLeft(root);
                            heightIncreased = false;
                            break;
                    }
                }
            }
            else
            {
                root.Right = InsertPhone(root.Right, newNode, ref heightIncreased);
                if (heightIncreased)
                {
                    switch (root.Balance)
                    {
                        case -1:
                            root.Balance = 0;
                            heightIncreased = false;
                            break;
                        case 0:
                            root.Balance = 1;
                            break;
                        case 1:
                            root = BalancePhoneRight(root);
                            heightIncreased = false;
                            break;
                    }
                }
            }

            return root;
        }
        #endregion

        #region Delete
        public bool DeleteByPesel(long pesel)
        {
            var peselNode = SearchByPesel(pesel);
            if (peselNode == null)
            {
                Console.WriteLine($"Numer PESEL {pesel} nie istnieje w bazie.");
                return false;
            }

            long phone = peselNode.PhoneLink!.Phone;

            bool heightDecreased = false;
            bool deleted = false;
            peselRoot = DeletePeselRecursive(peselRoot, pesel, ref heightDecreased, ref deleted);

            heightDecreased = false;
            deleted = false;
            phoneRoot = DeletePhoneRecursive(phoneRoot, phone, ref heightDecreased, ref deleted);

            return true;
        }

        public bool DeleteByPhone(long phone)
        {
            var phoneNode = SearchByPhone(phone);
            if (phoneNode == null)
            {
                Console.WriteLine($"Numer telefonu {phone} nie istnieje w bazie.");
                return false;
            }

            long pesel = phoneNode.PeselLink!.Pesel;

            bool heightDecreased = false;
            bool deleted = false;
            phoneRoot = DeletePhoneRecursive(phoneRoot, phone, ref heightDecreased, ref deleted);

            heightDecreased = false;
            deleted = false;
            peselRoot = DeletePeselRecursive(peselRoot, pesel, ref heightDecreased, ref deleted);

            return true;
        }

        private PeselNode? DeletePeselRecursive(PeselNode? node, long pesel, ref bool heightDecreased, ref bool deleted)
        {
            if (node == null)
            {
                heightDecreased = false;
                deleted = false;
                return null;
            }

            if (pesel < node.Pesel)
            {
                node.Left = DeletePeselRecursive(node.Left, pesel, ref heightDecreased, ref deleted);
                if (heightDecreased)
                {
                    node = BalancePeselAfterLeftDeletion(node, ref heightDecreased);
                }
            }
            else if (pesel > node.Pesel)
            {
                node.Right = DeletePeselRecursive(node.Right, pesel, ref heightDecreased, ref deleted);
                if (heightDecreased)
                {
                    node = BalancePeselAfterRightDeletion(node, ref heightDecreased);
                }
            }
            else
            {
                deleted = true;

                if (node.Left == null && node.Right == null)
                {
                    heightDecreased = true;
                    return null;
                }
                else if (node.Left == null)
                {
                    heightDecreased = true;
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    heightDecreased = true;
                    return node.Left;
                }
                else
                {
                    PeselNode? maxNode = FindMaxPesel(node.Left);
                    // Kopiujemy klucz i link
                    node.Pesel = maxNode!.Pesel;
                    node.PhoneLink = maxNode.PhoneLink;
                    // Aktualizujemy link w węźle telefonu
                    if (node.PhoneLink != null)
                    {
                        node.PhoneLink.PeselLink = node;
                    }
                    node.Left = DeletePeselRecursive(node.Left, maxNode.Pesel, ref heightDecreased, ref deleted);
                    if (heightDecreased)
                    {
                        node = BalancePeselAfterLeftDeletion(node, ref heightDecreased);
                    }
                }
            }

            return node;
        }

        private PhoneNode? DeletePhoneRecursive(PhoneNode? node, long phone, ref bool heightDecreased, ref bool deleted)
        {
            if (node == null)
            {
                heightDecreased = false;
                deleted = false;
                return null;
            }

            if (phone < node.Phone)
            {
                node.Left = DeletePhoneRecursive(node.Left, phone, ref heightDecreased, ref deleted);
                if (heightDecreased)
                {
                    node = BalancePhoneAfterLeftDeletion(node, ref heightDecreased);
                }
            }
            else if (phone > node.Phone)
            {
                node.Right = DeletePhoneRecursive(node.Right, phone, ref heightDecreased, ref deleted);
                if (heightDecreased)
                {
                    node = BalancePhoneAfterRightDeletion(node, ref heightDecreased);
                }
            }
            else
            {
                deleted = true;

                if (node.Left == null && node.Right == null)
                {
                    heightDecreased = true;
                    return null;
                }
                else if (node.Left == null)
                {
                    heightDecreased = true;
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    heightDecreased = true;
                    return node.Left;
                }
                else
                {
                    PhoneNode? maxNode = FindMaxPhone(node.Left);
                    node.Phone = maxNode!.Phone;
                    node.PeselLink = maxNode.PeselLink;
                    if (node.PeselLink != null)
                    {
                        node.PeselLink.PhoneLink = node;
                    }
                    node.Left = DeletePhoneRecursive(node.Left, maxNode.Phone, ref heightDecreased, ref deleted);
                    if (heightDecreased)
                    {
                        node = BalancePhoneAfterLeftDeletion(node, ref heightDecreased);
                    }
                }
            }

            return node;
        }

        private PeselNode? FindMaxPesel(PeselNode? node)
        {
            if (node == null) return null;
            while (node.Right != null) node = node.Right;
            return node;
        }

        private PhoneNode? FindMaxPhone(PhoneNode? node)
        {
            if (node == null) return null;
            while (node.Right != null) node = node.Right;
            return node;
        }
        #endregion

        #region Rotations - Pesel Tree
        private PeselNode RotatePeselRight(PeselNode node)
        {
            PeselNode newRoot = node.Left!;
            node.Left = newRoot.Right;
            newRoot.Right = node;
            return newRoot;
        }

        private PeselNode RotatePeselLeft(PeselNode node)
        {
            PeselNode newRoot = node.Right!;
            node.Right = newRoot.Left;
            newRoot.Left = node;
            return newRoot;
        }

        private PeselNode BalancePeselLeft(PeselNode node)
        {
            PeselNode left = node.Left!;

            if (left.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 0;
                return RotatePeselRight(node);
            }
            else if (left.Balance == 1)
            {
                PeselNode leftRight = left.Right!;

                if (leftRight.Balance == -1) { node.Balance = 1; left.Balance = 0; }
                else if (leftRight.Balance == 0) { node.Balance = 0; left.Balance = 0; }
                else { node.Balance = 0; left.Balance = -1; }
                leftRight.Balance = 0;

                node.Left = RotatePeselLeft(left);
                return RotatePeselRight(node);
            }
            else
            {
                node.Balance = -1;
                left.Balance = 1;
                return RotatePeselRight(node);
            }
        }

        private PeselNode BalancePeselRight(PeselNode node)
        {
            PeselNode right = node.Right!;

            if (right.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = 0;
                return RotatePeselLeft(node);
            }
            else if (right.Balance == -1)
            {
                PeselNode rightLeft = right.Left!;

                if (rightLeft.Balance == 1) { node.Balance = -1; right.Balance = 0; }
                else if (rightLeft.Balance == 0) { node.Balance = 0; right.Balance = 0; }
                else { node.Balance = 0; right.Balance = 1; }
                rightLeft.Balance = 0;

                node.Right = RotatePeselRight(right);
                return RotatePeselLeft(node);
            }
            else
            {
                node.Balance = 1;
                right.Balance = -1;
                return RotatePeselLeft(node);
            }
        }

        private PeselNode BalancePeselAfterLeftDeletion(PeselNode node, ref bool heightDecreased)
        {
            switch (node.Balance)
            {
                case -1: node.Balance = 0; break;
                case 0: node.Balance = 1; heightDecreased = false; break;
                case 1:
                    int rightBalance = node.Right!.Balance;
                    node = BalancePeselRight(node);
                    if (rightBalance == 0) heightDecreased = false;
                    break;
            }
            return node;
        }

        private PeselNode BalancePeselAfterRightDeletion(PeselNode node, ref bool heightDecreased)
        {
            switch (node.Balance)
            {
                case 1: node.Balance = 0; break;
                case 0: node.Balance = -1; heightDecreased = false; break;
                case -1:
                    int leftBalance = node.Left!.Balance;
                    node = BalancePeselLeft(node);
                    if (leftBalance == 0) heightDecreased = false;
                    break;
            }
            return node;
        }
        #endregion

        #region Rotations - Phone Tree
        private PhoneNode RotatePhoneRight(PhoneNode node)
        {
            PhoneNode newRoot = node.Left!;
            node.Left = newRoot.Right;
            newRoot.Right = node;
            return newRoot;
        }

        private PhoneNode RotatePhoneLeft(PhoneNode node)
        {
            PhoneNode newRoot = node.Right!;
            node.Right = newRoot.Left;
            newRoot.Left = node;
            return newRoot;
        }

        private PhoneNode BalancePhoneLeft(PhoneNode node)
        {
            PhoneNode left = node.Left!;

            if (left.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 0;
                return RotatePhoneRight(node);
            }
            else if (left.Balance == 1)
            {
                PhoneNode leftRight = left.Right!;

                if (leftRight.Balance == -1) { node.Balance = 1; left.Balance = 0; }
                else if (leftRight.Balance == 0) { node.Balance = 0; left.Balance = 0; }
                else { node.Balance = 0; left.Balance = -1; }
                leftRight.Balance = 0;

                node.Left = RotatePhoneLeft(left);
                return RotatePhoneRight(node);
            }
            else
            {
                node.Balance = -1;
                left.Balance = 1;
                return RotatePhoneRight(node);
            }
        }

        private PhoneNode BalancePhoneRight(PhoneNode node)
        {
            PhoneNode right = node.Right!;

            if (right.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = 0;
                return RotatePhoneLeft(node);
            }
            else if (right.Balance == -1)
            {
                PhoneNode rightLeft = right.Left!;

                if (rightLeft.Balance == 1) { node.Balance = -1; right.Balance = 0; }
                else if (rightLeft.Balance == 0) { node.Balance = 0; right.Balance = 0; }
                else { node.Balance = 0; right.Balance = 1; }
                rightLeft.Balance = 0;

                node.Right = RotatePhoneRight(right);
                return RotatePhoneLeft(node);
            }
            else
            {
                node.Balance = 1;
                right.Balance = -1;
                return RotatePhoneLeft(node);
            }
        }

        private PhoneNode BalancePhoneAfterLeftDeletion(PhoneNode node, ref bool heightDecreased)
        {
            switch (node.Balance)
            {
                case -1: node.Balance = 0; break;
                case 0: node.Balance = 1; heightDecreased = false; break;
                case 1:
                    int rightBalance = node.Right!.Balance;
                    node = BalancePhoneRight(node);
                    if (rightBalance == 0) heightDecreased = false;
                    break;
            }
            return node;
        }

        private PhoneNode BalancePhoneAfterRightDeletion(PhoneNode node, ref bool heightDecreased)
        {
            switch (node.Balance)
            {
                case 1: node.Balance = 0; break;
                case 0: node.Balance = -1; heightDecreased = false; break;
                case -1:
                    int leftBalance = node.Left!.Balance;
                    node = BalancePhoneLeft(node);
                    if (leftBalance == 0) heightDecreased = false;
                    break;
            }
            return node;
        }
        #endregion

        #region File Operations
        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"Plik {filename} nie istnieje.");
                return;
            }

            foreach (var line in File.ReadLines(filename))
            {
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    // Obsługa komentarzy //
                    if (parts[0].StartsWith("//")) continue;

                    if (long.TryParse(parts[0], out long pesel) && long.TryParse(parts[1], out long phone))
                    {
                        Insert(pesel, phone);
                    }
                }
            }
        }

        public void SaveToFile(string filename, bool byPesel = true)
        {
            using var writer = new StreamWriter(filename);

            if (byPesel)
            {
                SavePeselKLP(peselRoot, writer);
            }
            else
            {
                SavePhoneKLP(phoneRoot, writer);
            }
        }

        private void SavePeselKLP(PeselNode? node, StreamWriter writer)
        {
            if (node == null) return;
            writer.WriteLine($"{node.Pesel} {node.PhoneLink?.Phone}");
            SavePeselKLP(node.Left, writer);
            SavePeselKLP(node.Right, writer);
        }

        private void SavePhoneKLP(PhoneNode? node, StreamWriter writer)
        {
            if (node == null) return;
            writer.WriteLine($"{node.PeselLink?.Pesel} {node.Phone}");
            SavePhoneKLP(node.Left, writer);
            SavePhoneKLP(node.Right, writer);
        }

        public List<(long pesel, long phone)> GetAllKLPByPesel()
        {
            var result = new List<(long pesel, long phone)>();
            GetAllPeselKLP(peselRoot, result);
            return result;
        }

        private void GetAllPeselKLP(PeselNode? node, List<(long pesel, long phone)> result)
        {
            if (node == null) return;
            result.Add((node.Pesel, node.PhoneLink?.Phone ?? 0));
            GetAllPeselKLP(node.Left, result);
            GetAllPeselKLP(node.Right, result);
        }
        #endregion
    }

    #endregion

    #region Program główny

    class Program
    {
        static string basePath = AppContext.BaseDirectory.Contains("bin")
            ? Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."))
            : AppContext.BaseDirectory;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ZADANIE 4 - DRZEWO AVL ===");
                Console.WriteLine();
                Console.WriteLine("1. Część A - Testowanie drzewa AVL");
                Console.WriteLine("2. Część B - Słownik PESEL <-> Telefon");
                Console.WriteLine("0. Wyjście");
                Console.WriteLine();
                Console.Write("Wybór: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        MenuPartA();
                        break;
                    case "2":
                        MenuPartB();
                        break;
                    case "0":
                        return;
                }
            }
        }

        #region Menu Part A
        static void MenuPartA()
        {
            AVLTree tree = new AVLTree();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CZĘŚĆ A - DRZEWO AVL ===");
                Console.WriteLine();
                Console.WriteLine("1. Wczytaj elementy z pliku InTest1.txt");
                Console.WriteLine("2. Losuj elementy i wczytaj do drzewa");
                Console.WriteLine("3. Zapisz drzewo (KLP z wagami) do OutTest3.txt");
                Console.WriteLine("4. Podaj wagę i poziom elementu");
                Console.WriteLine("5. Dodaj element");
                Console.WriteLine("6. Usuń element");
                Console.WriteLine("7. Wypisz elementy poziomami");
                Console.WriteLine("8. Wypisz elementy KLP");
                Console.WriteLine("9. Wyszukaj element");
                Console.WriteLine("0. Powrót do menu głównego");
                Console.WriteLine();
                Console.Write("Wybór: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        LoadFromFileA(ref tree);
                        break;
                    case "2":
                        RandomInsertA(ref tree);
                        break;
                    case "3":
                        SaveToFileA(tree);
                        break;
                    case "4":
                        GetWeightAndLevel(tree);
                        break;
                    case "5":
                        InsertElementA(tree);
                        break;
                    case "6":
                        DeleteElementA(tree);
                        break;
                    case "7":
                        PrintLevelOrder(tree);
                        break;
                    case "8":
                        PrintKLP(tree);
                        break;
                    case "9":
                        SearchElementA(tree);
                        break;
                    case "0":
                        return;
                }
            }
        }

        static void LoadFromFileA(ref AVLTree tree)
        {
            Console.Clear();
            Console.WriteLine("=== WCZYTYWANIE Z PLIKU ===");
            Console.Write("Podaj ścieżkę do pliku (lub Enter dla InTest1.txt): ");
            string? path = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(path))
            {
                path = Path.Combine(basePath, "InTest1.txt");
            }

            tree = AVLTree.LoadFromFile(path);

            Console.WriteLine();
            Console.WriteLine("Wczytano elementy. Drzewo w kolejności KLP:");
            var elements = tree.PreOrderKLP();
            foreach (var (key, balance) in elements)
            {
                Console.Write($"{key}({balance}) ");
            }
            Console.WriteLine();
            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void RandomInsertA(ref AVLTree tree)
        {
            Console.Clear();
            Console.WriteLine("=== LOSOWANIE ELEMENTÓW ===");
            Console.Write("Podaj liczbę elementów do wylosowania: ");

            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.WriteLine("Nieprawidłowa liczba!");
                Console.ReadLine();
                return;
            }

            Console.Write("Podaj minimalną wartość zakresu: ");
            if (!int.TryParse(Console.ReadLine(), out int min))
            {
                Console.WriteLine("Nieprawidłowa wartość!");
                Console.ReadLine();
                return;
            }

            Console.Write("Podaj maksymalną wartość zakresu: ");
            if (!int.TryParse(Console.ReadLine(), out int max) || max < min)
            {
                Console.WriteLine("Nieprawidłowa wartość!");
                Console.ReadLine();
                return;
            }

            tree = new AVLTree();
            var random = new Random();
            var generated = new List<int>();

            for (int i = 0; i < count; i++)
            {
                int value = random.Next(min, max + 1);
                generated.Add(value);
                tree.Insert(value);
            }

            // Zapisz do pliku
            string outPath = Path.Combine(basePath, "OutTest2.txt");
            File.WriteAllText(outPath, string.Join(" ", generated));

            Console.WriteLine($"\nWylosowane elementy: {string.Join(" ", generated)}");
            Console.WriteLine($"Zapisano kolejność do: {outPath}");
            Console.WriteLine("\nDrzewo w kolejności KLP:");
            var elements = tree.PreOrderKLP();
            foreach (var (key, balance) in elements)
            {
                Console.Write($"{key}({balance}) ");
            }
            Console.WriteLine();
            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void SaveToFileA(AVLTree tree)
        {
            Console.Clear();
            string outPath = Path.Combine(basePath, "OutTest3.txt");
            tree.SaveToFileKLP(outPath);
            Console.WriteLine($"Zapisano drzewo do: {outPath}");
            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void GetWeightAndLevel(AVLTree tree)
        {
            Console.Clear();
            Console.WriteLine("=== WAGA I POZIOM ELEMENTU ===");
            Console.Write("Podaj klucz elementu: ");

            if (!int.TryParse(Console.ReadLine(), out int key))
            {
                Console.WriteLine("Nieprawidłowy klucz!");
                Console.ReadLine();
                return;
            }

            var (node, level) = tree.SearchWithLevel(key);

            if (node == null)
            {
                Console.WriteLine($"Element {key} nie został znaleziony w drzewie.");
            }
            else
            {
                Console.WriteLine($"Element: {node.Key}");
                Console.WriteLine($"Waga (balance): {node.Balance}");
                Console.WriteLine($"Poziom: {level}");
            }

            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void InsertElementA(AVLTree tree)
        {
            Console.Clear();
            Console.WriteLine("=== DODAWANIE ELEMENTU ===");
            Console.Write("Podaj wartość do dodania: ");

            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                Console.WriteLine("Nieprawidłowa wartość!");
                Console.ReadLine();
                return;
            }

            if (tree.Insert(value))
            {
                Console.WriteLine($"Dodano element {value}.");
            }
            else
            {
                Console.WriteLine($"Element {value} już istnieje w drzewie.");
            }

            Console.WriteLine("\nDrzewo po operacji (KLP):");
            var elements = tree.PreOrderKLP();
            foreach (var (key, balance) in elements)
            {
                Console.Write($"{key}({balance}) ");
            }
            Console.WriteLine();
            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void DeleteElementA(AVLTree tree)
        {
            Console.Clear();
            Console.WriteLine("=== USUWANIE ELEMENTU ===");
            Console.Write("Podaj wartość do usunięcia: ");

            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                Console.WriteLine("Nieprawidłowa wartość!");
                Console.ReadLine();
                return;
            }

            if (tree.Delete(value))
            {
                Console.WriteLine($"Usunięto element {value}.");
            }
            else
            {
                Console.WriteLine($"Element {value} nie istnieje w drzewie.");
            }

            Console.WriteLine("\nDrzewo po operacji (KLP):");
            var elements = tree.PreOrderKLP();
            foreach (var (key, balance) in elements)
            {
                Console.Write($"{key}({balance}) ");
            }
            Console.WriteLine();
            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void PrintLevelOrder(AVLTree tree)
        {
            Console.Clear();
            Console.WriteLine("=== ELEMENTY POZIOMAMI ===");

            var levels = tree.LevelOrder();

            for (int i = 0; i < levels.Count; i++)
            {
                Console.WriteLine($"Poziom {i}: {string.Join(" ", levels[i])}");
            }

            if (levels.Count == 0)
            {
                Console.WriteLine("Drzewo jest puste.");
            }

            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void PrintKLP(AVLTree tree)
        {
            Console.Clear();
            Console.WriteLine("=== ELEMENTY KLP (PREORDER) ===");

            var elements = tree.PreOrderKLP();

            if (elements.Count == 0)
            {
                Console.WriteLine("Drzewo jest puste.");
            }
            else
            {
                foreach (var (key, balance) in elements)
                {
                    Console.Write($"{key}({balance}) ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void SearchElementA(AVLTree tree)
        {
            Console.Clear();
            Console.WriteLine("=== WYSZUKIWANIE ELEMENTU ===");
            Console.Write("Podaj klucz do wyszukania: ");

            if (!int.TryParse(Console.ReadLine(), out int key))
            {
                Console.WriteLine("Nieprawidłowy klucz!");
                Console.ReadLine();
                return;
            }

            var node = tree.Search(key);

            if (node == null)
            {
                Console.WriteLine($"Element {key} nie został znaleziony.");
            }
            else
            {
                Console.WriteLine($"Znaleziono element: {node.Key} (balance: {node.Balance})");
            }

            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }
        #endregion

        #region Menu Part B
        static void MenuPartB()
        {
            var dictionary = new PeselPhoneDictionary();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CZĘŚĆ B - SŁOWNIK PESEL <-> TELEFON ===");
                Console.WriteLine();
                Console.WriteLine("Plik:");
                Console.WriteLine("  1. Wczytaj z pliku");
                Console.WriteLine("  2. Zapisz do pliku (wg PESEL KLP)");
                Console.WriteLine("  3. Zapisz do pliku (wg telefon KLP)");
                Console.WriteLine("Wstaw:");
                Console.WriteLine("  4. Wstaw nowy wpis (PESEL + telefon)");
                Console.WriteLine("Wyszukaj:");
                Console.WriteLine("  5. Znajdź telefon po PESEL");
                Console.WriteLine("  6. Znajdź PESEL po telefonie");
                Console.WriteLine("Usuń:");
                Console.WriteLine("  7. Usuń po PESEL");
                Console.WriteLine("  8. Usuń po telefonie");
                Console.WriteLine("Inne:");
                Console.WriteLine("  9. Wyświetl wszystkie wpisy");
                Console.WriteLine("  0. Powrót do menu głównego");
                Console.WriteLine();
                Console.Write("Wybór: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        LoadFromFileB(dictionary);
                        break;
                    case "2":
                        SaveToFileB(dictionary, true);
                        break;
                    case "3":
                        SaveToFileB(dictionary, false);
                        break;
                    case "4":
                        InsertB(dictionary);
                        break;
                    case "5":
                        SearchPhoneByPesel(dictionary);
                        break;
                    case "6":
                        SearchPeselByPhone(dictionary);
                        break;
                    case "7":
                        DeleteByPesel(dictionary);
                        break;
                    case "8":
                        DeleteByPhone(dictionary);
                        break;
                    case "9":
                        DisplayAll(dictionary);
                        break;
                    case "0":
                        return;
                }
            }
        }

        static void LoadFromFileB(PeselPhoneDictionary dictionary)
        {
            Console.Clear();
            Console.WriteLine("=== WCZYTYWANIE Z PLIKU ===");
            Console.Write("Podaj ścieżkę do pliku (lub Enter dla InTestB0404.txt): ");
            string? path = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(path))
            {
                path = Path.Combine(basePath, "InTestB0404.txt");
            }

            dictionary.LoadFromFile(path);
            Console.WriteLine("Wczytano dane z pliku.");
            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void SaveToFileB(PeselPhoneDictionary dictionary, bool byPesel)
        {
            Console.Clear();
            string outPath = Path.Combine(basePath, byPesel ? "OutTestB_Pesel.txt" : "OutTestB_Phone.txt");
            dictionary.SaveToFile(outPath, byPesel);
            Console.WriteLine($"Zapisano do: {outPath}");
            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void InsertB(PeselPhoneDictionary dictionary)
        {
            Console.Clear();
            Console.WriteLine("=== WSTAWIANIE NOWEGO WPISU ===");
            Console.Write("Podaj numer PESEL: ");

            if (!long.TryParse(Console.ReadLine(), out long pesel))
            {
                Console.WriteLine("Nieprawidłowy numer PESEL!");
                Console.ReadLine();
                return;
            }

            Console.Write("Podaj numer telefonu: ");
            if (!long.TryParse(Console.ReadLine(), out long phone))
            {
                Console.WriteLine("Nieprawidłowy numer telefonu!");
                Console.ReadLine();
                return;
            }

            if (dictionary.Insert(pesel, phone))
            {
                Console.WriteLine("Wpis został dodany.");
            }

            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void SearchPhoneByPesel(PeselPhoneDictionary dictionary)
        {
            Console.Clear();
            Console.WriteLine("=== WYSZUKIWANIE TELEFONU PO PESEL ===");
            Console.Write("Podaj numer PESEL: ");

            if (!long.TryParse(Console.ReadLine(), out long pesel))
            {
                Console.WriteLine("Nieprawidłowy numer PESEL!");
                Console.ReadLine();
                return;
            }

            var phone = dictionary.FindPhoneByPesel(pesel);

            if (phone.HasValue)
            {
                Console.WriteLine($"Numer telefonu: {phone.Value}");
            }
            else
            {
                Console.WriteLine($"Nie znaleziono numeru PESEL {pesel} w bazie.");
            }

            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void SearchPeselByPhone(PeselPhoneDictionary dictionary)
        {
            Console.Clear();
            Console.WriteLine("=== WYSZUKIWANIE PESEL PO TELEFONIE ===");
            Console.Write("Podaj numer telefonu: ");

            if (!long.TryParse(Console.ReadLine(), out long phone))
            {
                Console.WriteLine("Nieprawidłowy numer telefonu!");
                Console.ReadLine();
                return;
            }

            var pesel = dictionary.FindPeselByPhone(phone);

            if (pesel.HasValue)
            {
                Console.WriteLine($"Numer PESEL: {pesel.Value}");
            }
            else
            {
                Console.WriteLine($"Nie znaleziono numeru telefonu {phone} w bazie.");
            }

            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void DeleteByPesel(PeselPhoneDictionary dictionary)
        {
            Console.Clear();
            Console.WriteLine("=== USUWANIE PO PESEL ===");
            Console.Write("Podaj numer PESEL do usunięcia: ");

            if (!long.TryParse(Console.ReadLine(), out long pesel))
            {
                Console.WriteLine("Nieprawidłowy numer PESEL!");
                Console.ReadLine();
                return;
            }

            if (dictionary.DeleteByPesel(pesel))
            {
                Console.WriteLine("Wpis został usunięty.");
            }

            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void DeleteByPhone(PeselPhoneDictionary dictionary)
        {
            Console.Clear();
            Console.WriteLine("=== USUWANIE PO TELEFONIE ===");
            Console.Write("Podaj numer telefonu do usunięcia: ");

            if (!long.TryParse(Console.ReadLine(), out long phone))
            {
                Console.WriteLine("Nieprawidłowy numer telefonu!");
                Console.ReadLine();
                return;
            }

            if (dictionary.DeleteByPhone(phone))
            {
                Console.WriteLine("Wpis został usunięty.");
            }

            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }

        static void DisplayAll(PeselPhoneDictionary dictionary)
        {
            Console.Clear();
            Console.WriteLine("=== WSZYSTKIE WPISY (KLP wg PESEL) ===");

            var entries = dictionary.GetAllKLPByPesel();

            if (entries.Count == 0)
            {
                Console.WriteLine("Słownik jest pusty.");
            }
            else
            {
                Console.WriteLine($"{"PESEL",-15} {"TELEFON",-15}");
                Console.WriteLine(new string('-', 30));
                foreach (var (pesel, phone) in entries)
                {
                    Console.WriteLine($"{pesel,-15} {phone,-15}");
                }
            }

            Console.WriteLine("\nNaciśnij Enter...");
            Console.ReadLine();
        }
        #endregion
    }

    #endregion
}
