using System;
using System.Collections.Generic;

class LCATree<T> {
    int m;
    int[][] parents;
    T[][] values;
    Func<T, T, T> func;
    T identity;
    int[] depth;

    public LCATree(List<pair<T, int>>[] g, Func<T, T, T> func, T identity, int root = 0) {
        int n = g.Length;
        parents = new int[n][];
        values = new T[n][];
        depth = new int[n];
        this.func = func;
        this.identity = identity;
        m = 1;
        while ((1 << m - 1) < n) ++m;
        for (int i = 0; i < n; i++) {
            parents[i] = new int[m];
            values[i] = new T[m];
            depth[i] = -1;
            for (int j = 0; j < m; j++) {
                parents[i][j] = -1;
                values[i][j] = identity;
            }
        }
        depth[root] = 0;
        var q = new Queue<int>();
        q.Enqueue(root);
        while (q.Count > 0) {
            var p = q.Dequeue();
            foreach (var item in g[p]) {
                if (depth[item.v2] == -1) {
                    depth[item.v2] = depth[p] + 1;
                    parents[item.v2][0] = p;
                    values[item.v2][0] = item.v1;
                    for (int i = 1; i < m && parents[item.v2][i - 1] != -1; i++) {
                        parents[item.v2][i] = parents[parents[item.v2][i - 1]][i - 1];
                        values[item.v2][i] = func(values[item.v2][i - 1], values[parents[item.v2][i - 1]][i - 1]);
                    }
                    q.Enqueue(item.v2);
                }
            }
        }
    }
    T climb(ref int p, int cnt) {
        T val = identity;
        for (int i = m - 1; i >= 0 ; i--) {
            if (((cnt >> i) & 1) == 1) {
                val = func(val, values[p][i]);
                p = parents[p][i];
            }
        }
        return val;
    }
    public pair<T, int> lca(int p, int q) {
        T val = identity;
        if (depth[p] > depth[q]) val = climb(ref p, depth[p] - depth[q]);
        if (depth[p] < depth[q]) val = climb(ref q, depth[q] - depth[p]);
        if (p == q) return new pair<T, int>(val, p);

        for (int i = m - 1; i >= 0 ; i--) {
            if (parents[p][i] != parents[q][i]) {
                val = func(val, values[p][i]);
                val = func(val, values[q][i]);
                p = parents[p][i];
                q = parents[q][i];
            }
        }
        {
            val = func(val, values[p][0]);
            val = func(val, values[q][0]);
            p = parents[p][0];
            q = parents[q][0];
        }
        return new pair<T, int>(val, p);
    }
}
