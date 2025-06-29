public class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; set; }
    public bool IsEndOfWord { get; set; }

    public TrieNode()
    {
        Children = new Dictionary<char, TrieNode>();
        IsEndOfWord = false;
    }
}

public class Trie
{
    private readonly TrieNode root;

    public Trie()
    {
        root = new TrieNode();
    }

    // Вставка слова в Trie
    public void Insert(string word)
    {
        TrieNode node = root;
        foreach (char c in word)
        {
            if (!node.Children.ContainsKey(c))
                node.Children[c] = new TrieNode();

            node = node.Children[c];
        }
        node.IsEndOfWord = true;
    }

    // Поиск полного слова
    public bool Search(string word)
    {
        TrieNode node = SearchNode(word);
        return node != null && node.IsEndOfWord;
    }

    // Проверка, начинается ли какое-либо слово с данного префикса
    public bool StartsWith(string prefix)
    {
        TrieNode node = SearchNode(prefix);
        return node != null;
    }

    // Вспомогательный метод для поиска узла, соответствующего строке
    private TrieNode SearchNode(string str)
    {
        TrieNode node = root;
        foreach (char c in str)
        {
            if (!node.Children.ContainsKey(c))
                return null;
            node = node.Children[c];
        }
        return node;
    }

    public bool Remove(string word)
    {
        return Remove(root, word, 0);
    }

    private bool Remove(TrieNode current, string word, int index)
    {
        if (index == word.Length)
        {
            if (!current.IsEndOfWord)
                return false;

            current.IsEndOfWord = false;
            return current.Children.Count == 0; // можно удалить этот узел
        }

        char ch = word[index];
        if (!current.Children.ContainsKey(ch))
            return false;

        bool shouldDeleteChild = Remove(current.Children[ch], word, index + 1);

        if (shouldDeleteChild)
        {
            current.Children.Remove(ch);
            return current.Children.Count == 0 && !current.IsEndOfWord;
        }

        return false;
    }

}
