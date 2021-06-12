class BinaryTrie {
    public int cnt, dep;
    BinaryTrie child0, child1;

    public BinaryTrie(int dep = 30) {
        cnt = 0;
        this.dep = dep;
    }

    public void insert(int s) {
        ++cnt;
        if (dep < 0) return;
        int c = (s >> dep) & 1;
        if (c == 0) {
            if (child0 == null) {
                child0 = new BinaryTrie(dep - 1);
            }
            child0.insert(s);
        }
        else {
            if (child1 == null) {
                child1 = new BinaryTrie(dep - 1);
            }
            child1.insert(s);
        }
    }
    public void calc() {
    }
}
