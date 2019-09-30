class Trie {
    public int cnt, dep;
    private readonly char fir;
    private readonly int len;
    Trie[] childs;

    public Trie(char fir, int len) : this(fir, len, 0) {}
    Trie(char fir, int len, int dep) {
        cnt = 0;
        this.dep = dep;
        this.fir = fir;
        this.len = len;
        childs = new Trie[len];
    }

    // Count(prefix == s)
    public int getcnt(string s) {
        if (dep == s.Length) return cnt;
        if (childs[s[dep] - fir] == null) return 0;
        return childs[s[dep] - fir].getcnt(s);
    }
    public int get_longest_prefix(string s) {
        if (dep == s.Length) return dep;
        if (childs[s[dep] - fir] == null) return dep;
        return childs[s[dep] - fir].get_longest_prefix(s);
    }
    public void insert(string s) {
        ++cnt;
        if (dep == s.Length) return;
        if (childs[s[dep] - fir] == null) childs[s[dep] - fir] = new Trie(fir, len, dep + 1);
        childs[s[dep] - fir].insert(s);
    }
    public void calc() {}
}
