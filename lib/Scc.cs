using System.Collections.Generic;
using System.Linq;

static class Scc {
    public static int[] scc(List<int>[] edge, out List<int>[] cmped_edge) {
        int n = edge.Length;
        var vis = new bool[n];
        var st = new Stack<int>();
        var cmp = new int[n];
        for (int i = 0; i < n; i++) if (!vis[i]) dfs(i, -1, vis, st, cmp, edge);

        vis = new bool[n];
        var rev = new List<int>[n];
        for (int i = 0; i < n; i++) rev[i] = new List<int>();
        for (int i = 0; i < n; i++) foreach (int j in edge[i]) rev[j].Add(i);
        int k = 0;
        while (st.Any()) {
            int i = st.Pop();
            if (!vis[i]) dfs(i, k++, vis, new Stack<int>(), cmp, rev);
        }
        var cmped_edge_set = new HashSet<int>[k];
        for (int i = 0; i < k; i++) cmped_edge_set[i] = new HashSet<int>();
        for (int i = 0; i < n; i++)
            foreach (var item in edge[i])
                if (cmp[i] != cmp[item])
                    cmped_edge_set[cmp[i]].Add(cmp[item]);

        cmped_edge = new List<int>[k];
        for (int i = 0; i < k; i++) cmped_edge[i] = cmped_edge_set[i].ToList();

        return cmp;
    }
    static void dfs(int v, int k, bool[] vis, Stack<int> st, int[] cmp, List<int>[] edge) {
        vis[v] = true;
        cmp[v] = k;
        foreach (int i in edge[v]) if (!vis[i]) dfs(i, k, vis, st, cmp, edge);
        st.Push(v);
    }
}
