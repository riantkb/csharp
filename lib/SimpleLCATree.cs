using System.Collections.Generic;

class SimpleLCATree {
    int m;
    int[][] parents;
    int[] depth;

    public SimpleLCATree(List<int>[] g, int root = 0) {
        int n = g.Length;
        parents = new int[n][];
        depth = new int[n];
        m = 1;
        while ((1 << m) < n) ++m;
        for (int i = 0; i < n; i++) {
            parents[i] = new int[m];
            depth[i] = -1;
            for (int j = 0; j < m; j++) {
                parents[i][j] = -1;
            }
        }
        depth[root] = 0;
        var q = new Queue<int>();
        q.Enqueue(root);
        while (q.Count > 0) {
            var p = q.Dequeue();
            foreach (var item in g[p]) {
                if (depth[item] == -1) {
                    depth[item] = depth[p] + 1;
                    parents[item][0] = p;
                    for (int i = 1; i < m && parents[item][i - 1] != -1; i++) {
                        parents[item][i] = parents[parents[item][i - 1]][i - 1];
                    }
                    q.Enqueue(item);
                }
            }
        }
    }
    public int climb(int p, int cnt) {
        for (int i = m - 1; i >= 0 ; i--) {
            if (((cnt >> i) & 1) == 1) {
                p = parents[p][i];
            }
        }
        return p;
    }
    public int dist(int p, int q, int lca) => depth[p] + depth[q] - depth[lca] * 2;
    public int dist(int p, int q) => dist(p, q, lca(p, q));
    public int lca(int p, int q) {
        if (depth[p] > depth[q]) p = climb(p, depth[p] - depth[q]);
        if (depth[p] < depth[q]) q = climb(q, depth[q] - depth[p]);
        if (p == q) return p;

        for (int i = m - 1; i >= 0 ; i--) {
            if (parents[p][i] != parents[q][i]) {
                p = parents[p][i];
                q = parents[q][i];
            }
        }
        {
            p = parents[p][0];
            q = parents[q][0];
        }
        return p;
    }
}
