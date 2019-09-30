class BIT {
    int n;
    long[] bit;
    public BIT(int n) {
        this.n = n;
        bit = new long[n];
    }
    public void add(int j, long w) {
        for (int i = j; i < n; i |= i + 1)
            bit[i] += w;
    }
    public long at(int j) => sum(j, j + 1);
    // [0, j)
    public long sum(int j) {
        long ret = 0;
        for (int i = j - 1; i >= 0; i = (i & i + 1) - 1) ret += bit[i];
        return ret;
    }
    // [j, k)
    public long sum(int j, int k) => sum(k) - sum(j);
}
