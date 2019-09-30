using System.Collections.Generic;

class MaxFlow {
    public class edge {
        public int to;
        public long cap;
        public int rev;
        public edge(int t, long c, int r) { to = t; cap = c; rev = r; }
    }
    int V;
    List<edge>[] G;
    int[] itr, lev;
    public List<edge>[] getEdges() => G;

    public MaxFlow(int v) {
        V = v; G = new List<edge>[v];
        for (int i = 0; i < v; ++i) G[i] = new List<edge>();
    }
    public void add_edge(int frm, int to, long c) => add_edge(frm, to, c, true);
    public void add_edge(int frm, int to, long c, bool dir) {
        G[frm].Add(new edge(to, c, G[to].Count));
        G[to].Add(new edge(frm, dir ? 0 : c, G[frm].Count - 1));
    }
    void bfs(int s) {
        lev = new int[V];
        for (int i = 0; i < V; ++i) lev[i] = -1;
        var q = new Queue<int>();
        lev[s] = 0;
        q.Enqueue(s);
        while (q.Count > 0) {
            int v = q.Dequeue();
            foreach (var e in G[v]) {
                if (e.cap > 0 && lev[e.to] < 0) {
                    lev[e.to] = lev[v] + 1;
                    q.Enqueue(e.to);
                }
            }
        }
    }
    long dfs(int v, int t, long f) {
        if (v == t) return f;
        for (; itr[v] < G[v].Count; ++itr[v]) {
            var e = G[v][itr[v]];
            if (e.cap > 0 && lev[v] < lev[e.to]) {
                long d = dfs(e.to, t, f < e.cap ? f : e.cap);
                if (d > 0) {
                    e.cap -= d;
                    G[e.to][e.rev].cap += d;
                    return d;
                }
            }
        }
        return 0;
    }

    public long run(int s, int t) {
        long ret = 0;
        bfs(s);
        while (lev[t] >= 0) {
            itr = new int[V];
            long f;
            while ((f = dfs(s, t, util.LM)) > 0) ret += f;
            bfs(s);
        }
        return ret;
    }
}
