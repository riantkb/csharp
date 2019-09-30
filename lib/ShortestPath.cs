using System;
using System.Collections.Generic;

using Number = System.Int64;

static class ShortestPath {
    static Number Inf = util.LM;
    public static void WarshallFloyd(Number[][] g) {
        int n = g.Length;
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < n; ++j)
                for (int k = 0; k < n; ++k)
                    g[j][k] = Math.Min(g[j][k], g[j][i] + g[i][k]);
    }

    public static Number[] Dijkstra(List<pair<Number, int>>[] edges, int s) {
        int n = edges.Length;
        var dist = new Number[n];
        var q = new PriorityQueue<pair<Number, int>>(){ rev = true };
        for (int i = 0; i < n; ++i) dist[i] = Inf;
        dist[s] = 0;
        q.Push(new pair<Number, int>(dist[s], s));
        while (q.Count > 0) {
            var p = q.Pop();
            if (dist[p.v2] < p.v1) continue;
            foreach (var e in edges[p.v2]) {
                Number d = p.v1 + e.v1;
                if (dist[e.v2] > d) {
                    dist[e.v2] = d;
                    q.Push(new pair<Number, int>(d, e.v2));
                }
            }
        }
        return dist;
    }
    public static Number[] Dijkstra(List<pair<Number, int>>[] edges, Number[] dist) {
        int n = edges.Length;
        var q = new PriorityQueue<pair<Number, int>>(){ rev = true };
        for (int i = 0; i < n; ++i)
            if (dist[i] < Inf) q.Push(new pair<Number, int>(dist[i], i));

        while (q.Count > 0) {
            var p = q.Pop();
            if (dist[p.v2] < p.v1) continue;
            foreach (var e in edges[p.v2]) {
                Number d = p.v1 + e.v1;
                if (dist[e.v2] > d) {
                    dist[e.v2] = d;
                    q.Push(new pair<Number, int>(d, e.v2));
                }
            }
        }
        return dist;
    }
    public static Number[] Dijkstra(List<pair<Number, int>>[] edges, int s, out long[] cnt, long Mod) {
        int n = edges.Length;
        var dist = new Number[n];
        var q = new PriorityQueue<pair<Number, int>>(){ rev = true };
        for (int i = 0; i < n; ++i) dist[i] = Inf;
        dist[s] = 0;
        q.Push(new pair<Number, int>(dist[s], s));
        cnt = new long[n];
        cnt[s] = 1;
        while (q.Count > 0) {
            var p = q.Pop();
            if (dist[p.v2] < p.v1) continue;
            foreach (var e in edges[p.v2]) {
                Number d = p.v1 + e.v1;
                if (dist[e.v2] > d) {
                    dist[e.v2] = d;
                    cnt[e.v2] = cnt[p.v2];
                    q.Push(new pair<Number, int>(d, e.v2));
                }
                else if (dist[e.v2] == d) cnt[e.v2] = (cnt[e.v2] + cnt[p.v2]) % Mod;
            }
        }
        return dist;
    }
}
