using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDSA_SemB
{
    public class Treap<K, V> where K : IComparable<K>
    {
        private static Random PriorityRandomGenerator = new Random();

        public int Count { get; private set; }

        private Node root;

        private class Node
        {
            public K Key { get; }
            public V Value { get; set; }
            public int Priority { get; }
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }
            public Node Parent { get; set; }

            public Node(K key, V value)
            {
                Key = key;
                Value = value;
                #if DEBUG
                Priority = PriorityRandomGenerator.Next(1, 51); // 1 až 50
                #else
                 Priority = PriorityRandomGenerator.Next();
                 #endif
                LeftChild = null;
                RightChild = null;
                Parent = null;
            }

            

        }

        public Treap()
        {
            root = null;
            Count = 0;
        }

        private bool IsLeftChild(Node node)
        {


            return node.Parent != null && node.Parent.LeftChild == node;
        }


        public bool Insert(K key, V value)
        {
            if (root == null) // strom je prázdný, vložíme nový uzel jako kořen
            {
                root = new Node(key, value);
                Count++;
                return true;
            }

            Node current = root; // ukazuje na uzel´, který právě procházíme

            Node parent = null; // bude ukazovat na rodiče nového uzlu, až ho najdeme

            int comparisonResult = 0; // uloží výslledek porovnání klíče, abychom věděli zda jdeme doleva nebo doprava

            while (current != null)
            {
                parent = current; // zapamatujeme aktuální uzel jako kandidáta na rodiče
                comparisonResult = key.CompareTo(current.Key);


                if (comparisonResult == 0) // klíč již existuje, aktualizujeme hodnotu a skončíme
                {
                    //nodeToRemove.Value = value; // pokud bych akorát chtěla aktualizovat hodnotu
                    return false;
                }

                if (comparisonResult < 0) 
                {
                    current = current.LeftChild;
                }
                else
                {
                    current = current.RightChild;
                }
            }

            Node newNode = new Node(key, value);

            newNode.Parent = parent; // nastavíme rodiče nového uzlu

            if (comparisonResult < 0) // vložíme nový uzel jako levého potomka
            {
                parent.LeftChild = newNode;
            }
            else // vložíme nový uzel jako pravého potomka
            {
                parent.RightChild = newNode;
            }

            Count++;

            // menší číslo priority = vyšší priorta
            // Dokud má uzel rodiče a jeho priorita je vyšší než priorita rodiče, provádíme rotace

            while (newNode.Parent != null && newNode.Priority < newNode.Parent.Priority) // otec musí mít vyšší prioritu
            {
                if (IsLeftChild(newNode))
                {
                    RotateRight(newNode.Parent);
                }
                else
                {
                    RotateLeft(newNode.Parent);
                }
            }
            return true;

        }



        private void RotateLeft(Node node) // rotuji rodiče 
        {
            Node rightChild = node.RightChild;

            if (rightChild == null) // nejde udělat rotaci pokud neexistuje pravý potomek
            {
                return;
            }

            Node parent = node.Parent;

            node.RightChild = rightChild.LeftChild; // Levý podstrom pravého potomka se po rotaci stane pravým podstromem původního uzlu

            if (rightChild.LeftChild != null)
            {
                rightChild.LeftChild.Parent = node; // Pokud se nějaký podstrom přesunul pod pivot, musíme mu opravit odkaz na rodiče.
            }

            rightChild.Parent = parent; // Pravý potomek se po rotaci posune nad pivot, takže jeho rodičem bude původní rodič pivotu.

            if (parent == null) // Pokud pivot byl kořen, po rotaci se kořenem stane pravý potomek.
            {
                root = rightChild;
            }
            else if (parent.LeftChild == node) // Pokud pivot byl levým potomkem svého rodiče, po rotaci se pravý potomek stane levým potomkem rodiče.
            {
                parent.LeftChild = rightChild;
            }
            else // Pokud pivot byl pravým potomkem svého rodiče, po rotaci se pravý potomek stane pravým potomkem rodiče.
            {
                parent.RightChild = rightChild;
            }

            rightChild.LeftChild = node; // Pivot se po rotaci stane levým potomkem svého původního pravého potomka.

            node.Parent = rightChild; // Oprava rodiče pivotu, který se po rotaci posune pod svého původního pravého potomka.

        }

        private void RotateRight(Node node)
        {
            Node leftChild = node.LeftChild;

            if (leftChild == null) // nejde udělat rotaci pokud neexistuje levý potomek
            {
                return;
            }

            Node parent = node.Parent;

            node.LeftChild = leftChild.RightChild; // Pravý podstrom levého potomka se po rotaci stane levým podstromem původního uzlu

            if (leftChild.RightChild != null)
            {
                leftChild.RightChild.Parent = node; // Pokud se nějaký podstrom přesunul pod pivot, musíme mu opravit odkaz na rodiče.
            }

            leftChild.Parent = parent; // Levý potomek se po rotaci posune nad pivot, takže jeho rodičem bude původní rodič pivotu.

            if (parent == null) // Pokud pivot byl kořen, po rotaci se kořenem stane levý potomek.
            {
                root = leftChild;
            }
            else if (parent.LeftChild == node) // Pokud pivot byl levým potomkem svého rodiče, po rotaci se levý potomek stane levým potomkem rodiče.
            {
                parent.LeftChild = leftChild;
            }
            else // Pokud pivot byl pravým potomkem svého rodiče, po rotaci se levý potomek stane pravým potomkem rodiče.
            {
                parent.RightChild = leftChild;
            }

            leftChild.RightChild = node; // Pivot se po rotaci stane pravým potomkem svého původního levého potomka.

            node.Parent = leftChild; // Oprava rodiče pivotu, který se po rotaci posune pod svého původního levého potomka.
        }

        public bool ContainsKey(K key)
        {
            return FindNode(key) != null;
        }

        public bool TryGetValue(K key, out V Value)
        {
            Node? foundNode = FindNode(key);
            if (foundNode != null)
            {
                Value = foundNode.Value; // klíč nalezen, vrátíme hodnotu
                return true;
            }
            else
            {
                Value = default(V);
                return false; // klíč nenalezen
            }


        }

        public bool Remove(K key)
        {
            Node nodeToRemove = FindNode(key);
            if (nodeToRemove == null)
            {
                return false; // klíč nenalezen, nic neodstraňujeme
            }




            // dokud má uzel oba potomky, rotacemi ho posouvám dolů
            while (nodeToRemove.LeftChild != null && nodeToRemove.RightChild != null)
            {
                // rotuju směrem k potomkovi s vyšší prioritou
                if (nodeToRemove.LeftChild.Priority < nodeToRemove.RightChild.Priority)
                {
                    RotateRight(nodeToRemove); // levý potomek má vyšší prioritu, posuneme ho nahoru
                }
                else
                {
                    RotateLeft(nodeToRemove);
                }
            }

            // nyní má uzel nejvýše jednoho potomka, můžeme ho jednoduše odstranit
            Node child;

            if (nodeToRemove.LeftChild != null)
            {
                child = nodeToRemove.LeftChild;
            }
            else
            {
                child = nodeToRemove.RightChild;
            }


            // pokud mazaný uzel není kořen, musíme upravit odkaz na jeho rodiče 
            if (nodeToRemove.Parent != null)
            {
                if (nodeToRemove.Parent.LeftChild == nodeToRemove)
                {
                    nodeToRemove.Parent.LeftChild = child;
                }
                else
                {
                    nodeToRemove.Parent.RightChild = child;
                }
            }
            else // pokud mazaný uzel je kořen, nový kořen bude jeho jediný potomek (nebo null pokud nemá žádné potomky)
            {
                root = child;
            }


            if (child != null)
            {
                child.Parent = nodeToRemove.Parent; // oprava rodiče jediného potomka mazaného uzlu
            }

            Count--;
            return true;
        }

        public List<KeyValuePair<K, V>> TraverseInOrder() // 
        {
            List<KeyValuePair<K, V>> result = new List<KeyValuePair<K, V>>();
            Stack<Node> stack = new Stack<Node>(); // Zásobník pro uložení uzlů během průchodu
            Node current = root;

            while (current != null || stack.Count > 0)
            {
                // Jdeme co nejvíce doleva a ukládáme uzly na zásobník
                while (current != null)
                {
                    stack.Push(current); // push znamená uložit uzel na zásobník
                    current = current.LeftChild;
                }

                // Nyní jsme dosáhli nejlevějšího uzlu, můžeme ho zpracovat
                current = stack.Pop(); // pop znamená vyjmout uzel ze zásobníku

                result.Add(new KeyValuePair<K, V>(current.Key, current.Value)); // zpracujeme aktuální uzel - přidáme ho do výsledku

                // pokračujeme do pravěho podstromu
                current = current.RightChild;

            }
            return result;
        }

        private Node? FindNode(K key)
        {
            Node current = root;
            while (current != null)
            {
                int comparisonResult = key.CompareTo(current.Key);
                if (comparisonResult == 0)
                {
                    return current; // klíč nalezen, vrátíme uzel
                }
                else if (comparisonResult < 0)
                {
                    current = current.LeftChild; // jdeme doleva
                }
                else
                {
                    current = current.RightChild; // jdeme doprava
                }
            }
            return null; // klíč nenalezen

        }

        public bool TryGetPredecessor(K key, out KeyValuePair<K, V> predecessor)
        {
            Node predecessorNode = null;
            Node current = root;

            while (current != null)
            {
                int comparisonResult = key.CompareTo(current.Key);
                if (comparisonResult <= 0)
                {
                    current = current.LeftChild; // Hledaný klíč je menší nebo stejný, takže případný předchůdce musí být vlevo
                }
                else
                {
                    predecessorNode = current; // Aktuální uzel má menší klíč než hledaný, takže je kandidátem na předchůdce

                    current = current.RightChild; //  zkusím najít ještě větší klíč, který je stále menší než hledaný v pravém podstromu
                }
            }

            if (predecessorNode == null)
            {
                predecessor = default(KeyValuePair<K, V>);
                return false; // nenalezen předchůdce, všechny klíče jsou větší nebo stejné jako hledaný
            }
            predecessor = new KeyValuePair<K, V>(predecessorNode.Key, predecessorNode.Value);
            return true;
        }


        public bool TryGetSuccessor(K key, out KeyValuePair<K, V> successor)
        {
            Node successorNode = null;
            Node current = root;

            while (current != null)
            {
                int comparisonResult = key.CompareTo(current.Key);
                if (comparisonResult >= 0)
                {
                    current = current.RightChild;
                }
                else
                {
                    successorNode = current;

                    current = current.LeftChild;
                }
            }

            if (successorNode == null)
            {
                successor = default(KeyValuePair<K, V>);
                return false;
            }
            successor = new KeyValuePair<K, V>(successorNode.Key, successorNode.Value);
            return true;
        }

        public void Clear()
        {
            root = null;
            Count = 0;
        }


    }
}
