using System;
using System.Collections.Generic;

class MinCostFlow {
    class edge {
        public int to, cap, rev;
        public long cost;
        public edge(int to, int cap, long cost, int rev) {
            this.to = to;
            this.cap = cap;
            this.cost = cost;
            this.rev = rev;
        }
    }
    int n;
    List<edge>[] g;

    public MinCostFlow(int n) {
        this.n = n;
        g = new List<edge>[n];
        for (int i = 0; i < n; i++)
            g[i] = new List<edge>();
    }
    public void addEdge(int from, int to, int cap, long cost) {
        g[from].Add(new edge(to, cap, cost, g[to].Count));
        g[to].Add(new edge(from, 0, -cost, g[from].Count));
    }
    public long run(int s, int t, int f) {
        long res = 0;
        var h = new long[n];
        while (f > 0) {
            var q = new PriorityQueue<pair<long, int>>(){ rev = true };
            var dist = new long[n];
            var prevv = new int[n];
            var preve = new int[n];
            for (int i = 0; i < n; i++) dist[i] = util.LM;
            dist[s] = 0;
            q.Push(new pair<long, int>(0, s));
            while (q.Count > 0) {
                var p = q.Pop();
                int v = p.v2;
                if (dist[v] < p.v1) continue;
                for (int i = 0; i < g[v].Count; ++i) {
                    var e = g[v][i];
                    if (e.cap > 0 && dist[e.to] > dist[v] + e.cost + h[v] - h[e.to]) {
                        dist[e.to] = dist[v] + e.cost + h[v] - h[e.to];
                        prevv[e.to] = v;
                        preve[e.to] = i;
                        q.Push(new pair<long, int>(dist[e.to], e.to));
                    }
                }
            }
            if (dist[t] == util.LM) break;
            for (int v = 0; v < n; ++v) h[v] += dist[v];

            int d = f;
            for (int v = t; v != s; v = prevv[v]) d = Math.Min(d, g[prevv[v]][preve[v]].cap);

            f -= d;
            res += d * h[t];
            for (int v = t; v != s; v = prevv[v]) {
                var e = g[prevv[v]][preve[v]];
                e.cap -= d;
                g[v][e.rev].cap += d;
            }
        }
        return res;
    }
}
