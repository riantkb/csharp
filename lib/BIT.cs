using Number = System.Int64;

class BIT {
    int n;
    Number[] bit;
    public BIT(int n) {
        this.n = n;
        bit = new Number[n];
    }
    public void add(int j, Number w) {
        for (int i = j; i < n; i |= i + 1)
            bit[i] += w;
    }
    public Number at(int j) => sum(j, j + 1);
    // [0, j)
    public Number sum(int j) {
        Number ret = 0;
        for (int i = j - 1; i >= 0; i = (i & i + 1) - 1) ret += bit[i];
        return ret;
    }
    // [j, k)
    public Number sum(int j, int k) => sum(k) - sum(j);
}
