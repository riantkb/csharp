using System.Collections.Generic;

class LCATree {
    int m;
    int[][] par;
    long[] dis;
    int[] dep;
    public LCATree(List<pair<long, int>>[] g, int root = 0) {
        int n = g.Length;
        par = new int[n][];
        dep = new int[n];
        dis = new long[n];
        m = 1;
        while ((1 << m - 1) < n) ++m;
        for (int i = 0; i < n; i++) {
            dep[i] = -1;
            par[i] = new int[m];
            for (int j = 0; j < m; j++) par[i][j] = -1;
        }
        dep[root] = 0;
        var q = new Queue<int>();
        q.Enqueue(root);
        while (q.Count > 0) {
            var p = q.Dequeue();
            foreach (var item in g[p]) {
                if (dep[item.v2] == -1) {
                    dep[item.v2] = dep[p] + 1;
                    dis[item.v2] = dis[p] + item.v1;
                    par[item.v2][0] = p;
                    for (int i = 1; i < m && par[item.v2][i - 1] != -1; i++)
                        par[item.v2][i] = par[par[item.v2][i - 1]][i - 1];

                    q.Enqueue(item.v2);
                }
            }
        }
    }
    public LCATree(List<int>[] g, int root = 0) {
        int n = g.Length;
        par = new int[n][];
        dep = new int[n];
        dis = new long[n];
        m = 1;
        while ((1 << m - 1) < n) ++m;
        for (int i = 0; i < n; i++) {
            dep[i] = -1;
            par[i] = new int[m];
            for (int j = 0; j < m; j++) par[i][j] = -1;
        }
        dep[root] = 0;
        var q = new Queue<int>();
        q.Enqueue(root);
        while (q.Count > 0) {
            var p = q.Dequeue();
            foreach (var item in g[p]) {
                if (dep[item] == -1) {
                    dep[item] = dep[p] + 1;
                    dis[item] = dis[p] + 1;
                    par[item][0] = p;
                    for (int i = 1; i < m && par[item][i - 1] != -1; i++)
                        par[item][i] = par[par[item][i - 1]][i - 1];

                    q.Enqueue(item);
                }
            }
        }
    }
    int climb(int a, int d) {
        for (int i = 0; i < m; i++) if ((d >> i & 1) == 1) a = par[a][i];
        return a;
    }
    public int lca(int a, int b) {
        if (dep[a] > dep[b]) a = climb(a, dep[a] - dep[b]);
        if (dep[a] < dep[b]) b = climb(b, dep[b] - dep[a]);
        if (a == b) return a;
        for (int i = m - 1; i >= 0 ; i--) {
            if (par[a][i] != par[b][i]) {
                a = par[a][i];
                b = par[b][i];
            }
        }
        return par[a][0];
    }
    public long dist(int a, int b) => dis[a] + dis[b] - dis[lca(a, b)] * 2;
}
