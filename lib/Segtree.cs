using System;
using System.Collections.Generic;

class Segtree<T> {
    int n;
    T[] data;
    Func<T, T, T> f;
    T identity;

    public Segtree(int m, Func<T, T, T> f, T identity) {
        this.f = f;
        this.identity = identity;
        n = 1;
        while (n < m) n <<= 1;
        data = new T[(n << 1) - 1];
        for (int i = 0; i < data.Length; i++) data[i] = identity;
    }
    public Segtree(int m, IList<T> ini, Func<T, T, T> f, T identity) : this(m, f, identity) {
        for (int i = 0; i < m; ++i) assign(i, ini[i]);
        all_update();
    }
    public void assign(int j, T x) => data[j + n - 1] = x;
    public void update(int j, T x) {
        assign(j, x);
        update(j);
    }
    public void update(int j) {
        int i = j + n - 1;
        while (i > 0) {
            i = i - 1 >> 1;
            data[i] = f(data[i << 1 | 1], data[i + 1 << 1]);
        }
    }
    public void all_update() {
        for (int i = n - 2; i >= 0; i--)
            data[i] = f(data[i << 1 | 1], data[i + 1 << 1]);
    }
    public T look(int i) => data[i + n - 1];

    // [s, t)
    public T run(int s, int t) => run(s, t, 0, 0, n);
    T run(int s, int t, int k, int l, int r) => r <= s || t <= l ? identity : s <= l && r <= t ? data[k]
            : f(run(s, t, k << 1 | 1, l, l + r >> 1), run(s, t, k + 1 << 1, l + r >> 1, r));
}
