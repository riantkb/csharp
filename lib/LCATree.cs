using System;
using System.Collections.Generic;

class LCATree {
    int m;
    int[][] parent;
    long[][] dis;
    long[] distfrompar;
    int[] dep;
    public LCATree(List<pair<long, int>>[] g, int root) {
        int n = g.Length;
        parent = new int[n][];
        dis = new long[n][];
        dep = new int[n];
        distfrompar = new long[n];
        m = 1;
        while ((1 << m - 1) < n) ++m;
        for (int i = 0; i < n; i++) {
            parent[i] = new int[m];
            dis[i] = new long[m];
            dep[i] = n + 1;
            distfrompar[i]= 1L << 60;
            for (int j = 0; j < m; j++) {
                parent[i][j] = -1;
                dis[i][j] = 1L << 60;
            }
        }
        dep[root] = 0;
        distfrompar[root] = 0;
        var q = new Queue<int>();
        q.Enqueue(root);
        while (q.Count > 0) {
            var p = q.Dequeue();
            foreach (var item in g[p]) {
                if (dep[item.v2] > dep[p] + 1) {
                    dep[item.v2] = dep[p] + 1;
                    distfrompar[item.v2] = distfrompar[p] + item.v1;
                    parent[item.v2][0] = p;
                    dis[item.v2][0] = item.v1;
                    q.Enqueue(item.v2);
                }
            }
        }
        for (int i = 1; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (dep[j] >= (1 << i)) {
                    parent[j][i] = parent[parent[j][i - 1]][i - 1];
                    dis[j][i] = Math.Min(dis[j][i], dis[parent[j][i - 1]][i - 1] + dis[j][i - 1]);
                }
            }
        }
    }
    int climb(int a, int c, out long d) {
        d = 0;
        for (int i = m - 1; i >= 0 ; i--) {
            if (((c >> i) & 1) == 1) {
                d += dis[a][i];
                a = parent[a][i];
            }
        }
        return a;
    }
    public int getLCA(int a, int b) {
        if (dep[a] < dep[b]) {
            long d;
            b = climb(b, dep[b] - dep[a], out d);
        }
        else if (dep[a] > dep[b]) {
            long d;
            a = climb(a, dep[a] - dep[b], out d);
        }
        int p = m - 1;
        while (a != b && p >= 0) {
            if (parent[a][p] != parent[b][p]) {
                a = parent[a][p];
                b = parent[b][p];
            }
            else --p;
        }
        if (a != b) return parent[a][0];
        return a;
    }
    public long dist(int a, int b, int lca) => distfrompar[a] + distfrompar[b] - distfrompar[lca] * 2;
    public long dist(int a, int b) => dist(a, b, getLCA(a, b));
}
