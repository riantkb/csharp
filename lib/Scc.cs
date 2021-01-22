using System.Collections.Generic;

static class Scc {
    public static int[] scc(List<int>[] edge, out List<int>[] cmped_edge) {
        int n = edge.Length;
        var vis = new bool[n];
        var lis = new List<int>();
        var rev = new List<int>[n];
        var cmp = new int[n];
        for (int i = 0; i < n; i++) {
            if (!vis[i]) dfs1(i, vis, lis, edge);
            rev[i] = new List<int>();
            cmp[i] = -1;
        }
        for (int i = 0; i < n; i++)
            foreach (int j in edge[i]) rev[j].Add(i);

        int k = 0;
        for (int i = lis.Count - 1; i >= 0 ; i--)
            if (cmp[lis[i]] == -1) dfs2(lis[i], k++, cmp, rev);

        var cmped_edge_set = new HashSet<int>[k];
        for (int i = 0; i < k; i++)
            cmped_edge_set[i] = new HashSet<int>();

        for (int i = 0; i < n; i++)
            foreach (var item in edge[i])
                if (cmp[i] != cmp[item])
                    cmped_edge_set[cmp[i]].Add(cmp[item]);

        cmped_edge = new List<int>[k];
        for (int i = 0; i < k; i++)
            cmped_edge[i] = new List<int>(cmped_edge_set[i]);

        return cmp;
    }
    static void dfs1(int v, bool[] vis, List<int> lis, List<int>[] edge) {
        vis[v] = true;
        foreach (int i in edge[v]) if (!vis[i]) dfs1(i, vis, lis, edge);
        lis.Add(v);
    }
    static void dfs2(int v, int k, int[] cmp, List<int>[] edge) {
        cmp[v] = k;
        foreach (int i in edge[v])
            if (cmp[i] == -1) dfs2(i, k, cmp, edge);
    }
}
