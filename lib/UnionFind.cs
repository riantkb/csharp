class UnionFind {
    int num;
    int[] par, sz, rank;
    public UnionFind(int n) {
        num = n;
        par = new int[n];
        sz = new int[n];
        rank = new int[n];
        for (int i = 0; i < n; ++i) {
            par[i] = i;
            sz[i] = 1;
            rank[i] = 0;
        }
    }
    int find(int x) => par[x] == x ? x : (par[x] = find(par[x]));
    public bool same(int x, int y) => find(x) == find(y);
    public int unionsize(int x) => sz[find(x)];
    public int unioncount => num;
    public bool unite(int x, int y) {
        x = find(x);
        y = find(y);
        if (x == y) return false;
        --num;
        if (rank[x] < rank[y]) {
            par[x] = y;
            sz[y] += sz[x];
        }
        else {
            par[y] = x;
            sz[x] += sz[y];
            if (rank[x] == rank[y]) ++rank[x];
        }
        return true;
    }
    public bool ispar(int x) => x == find(x);
    public int getpar(int x) => find(x);
}
