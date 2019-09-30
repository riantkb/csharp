using System;
using System.Collections.Generic;

class LazySegtree<T, U> {
    int n;
    T[] data;
    U[] lazy;
    bool[] is_lazy;
    Func<T, T, T> calc;
    Func<T, U, int, int, T> apply;
    Func<U, U, U> merge;
    T identity;

    public LazySegtree(int m, Func<T, T, T> calc, Func<T, U, int, int, T> apply, Func<U, U, U> merge, T identity) {
        this.calc = calc;
        this.apply = apply;
        this.merge = merge;
        this.identity = identity;
        n = 1;
        while (n < m) n <<= 1;
        data = new T[n * 2 - 1];
        lazy = new U[n * 2 - 1];
        is_lazy = new bool[n * 2 - 1];
        for (int i = 0; i < data.Length; i++) data[i] = identity;
    }
    public LazySegtree(int m, Func<T, T, T> calc, Func<T, U, int, int, T> apply, Func<U, U, U> merge, T identity, T ini) : this(m, calc, apply, merge, identity) {
        for (int i = 0; i < m; i++) data[i + n - 1] = ini;
        for (int i = n - 2; i >= 0; i--) data[i] = calc(data[i * 2 + 1], data[i * 2 + 2]);
    }
    public LazySegtree(int m, Func<T, T, T> calc, Func<T, U, int, int, T> apply, Func<U, U, U> merge, T identity, IList<T> ini) : this(m, calc, apply, merge, identity) {
        for (int i = 0; i < m; i++) data[i + n - 1] = ini[i];
        for (int i = n - 2; i >= 0; i--) data[i] = calc(data[i * 2 + 1], data[i * 2 + 2]);
    }
    void assign_lazy(int k, U x) {
        if (k >= lazy.Length) return;
        lazy[k] = is_lazy[k] ? merge(lazy[k], x) : x;
        is_lazy[k] = true;
    }
    void eval(int k, int l, int r){
        if (!is_lazy[k]) return;
        assign_lazy(k * 2 + 1, lazy[k]);
        assign_lazy(k * 2 + 2, lazy[k]);
        data[k] = apply(data[k], lazy[k], l, r);
        is_lazy[k] = false;
    }
    T update(int s, int t, U x, int k, int l, int r) {
        eval(k, l, r);
        if (r <= s || t <= l) return data[k];
        if (s <= l && r <= t) {
            assign_lazy(k, x);
            return apply(data[k], lazy[k], l, r);
        }
        return data[k] = calc(update(s, t, x, k * 2 + 1, l, (l + r) / 2),
                              update(s, t, x, k * 2 + 2, (l + r) / 2, r));
    }
    T run(int s, int t, int k, int l, int r) {
        eval(k, l, r);
        if (r <= s || t <= l) return identity;
        if (s <= l && r <= t) return data[k];
        return calc(run(s, t, k * 2 + 1, l, (l + r) / 2),
                    run(s, t, k * 2 + 2, (l + r) / 2, r));
    }

    // [s, t)
    public void update(int s, int t, U x) => update(s, t, x, 0, 0, n);
    // [s, t)
    public T run(int s, int t) => run(s, t, 0, 0, n);
}
