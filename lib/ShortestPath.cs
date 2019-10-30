using System;
using System.Collections.Generic;

using Number = System.Int64;

static class ShortestPath {
    public static void WarshallFloyd(Number[][] g) {
        int n = g.Length;
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < n; ++j)
                for (int k = 0; k < n; ++k)
                    g[j][k] = Math.Min(g[j][k], g[j][i] + g[i][k]);
    }

    class Heap {
        int n;
        Number inf;
        Number[] values;
        int[] keys, indices;
        public Heap(int n, int s, Number inf) {
            this.n = n;
            this.inf = inf;
            values = new Number[n];
            for (int i = 0; i < n; i++) values[i] = i == s ? 0 : inf;
            keys = new int[n];
            indices = new int[n];
            keys[0] = s;
            indices[s] = 0;
            for (int i = 1, j = 0; i < n; i++, j++) {
                if (s == j) ++j;
                keys[i] = j;
                indices[j] = i;
            }
        }

        void Update(int i, Number val) {
            values[i] = val;
            int p = indices[i];
            while (p > 0) {
                int par = p - 1 >> 1;
                if (values[keys[par]] > val) {
                    keys[p] = keys[par];
                    indices[keys[p]] = p;
                    p = par;
                }
                else break;
            }
            keys[p] = i;
            indices[i] = p;
        }
        int Pop() {
            --n;
            int ret = keys[0];
            // indices[ret] = -1;
            int i = keys[n];
            // keys[n] = -1;
            Number val = values[i];
            if (n == 0) return ret;
            int p = 0;
            while ((p << 1 | 1) < n) {
                int ch = p << 1 | 1;
                if (ch < n - 1 && values[keys[ch]] > values[keys[ch + 1]]) ++ch;
                if (val > values[keys[ch]]) {
                    keys[p] = keys[ch];
                    indices[keys[p]] = p;
                    p = ch;
                }
                else break;
            }
            keys[p] = i;
            indices[i] = p;
            return ret;
        }
        public Number[] Run(List<pair<Number, int>>[] edges) {
            while (n > 0 && values[keys[0]] < inf) {
                int p = Pop();
                foreach (var e in edges[p])
                    if (values[e.v2] > values[p] + e.v1)
                        Update(e.v2, values[p] + e.v1);
            }
            return values;
        }
    }

    public static Number[] Dijkstra(List<pair<Number, int>>[] edges, int s, Number inf) => new Heap(edges.Length, s, inf).Run(edges);

    // public static Number[] Dijkstra(List<pair<Number, int>>[] edges, int s, out long[] cnt, long Mod) {
    //     int n = edges.Length;
    //     var dist = new Number[n];
    //     var q = new PriorityQueue<pair<Number, int>>(){ rev = true };
    //     for (int i = 0; i < n; ++i) dist[i] = Inf;
    //     dist[s] = 0;
    //     q.Push(new pair<Number, int>(dist[s], s));
    //     cnt = new long[n];
    //     cnt[s] = 1;
    //     while (q.Count > 0) {
    //         var p = q.Pop();
    //         if (dist[p.v2] < p.v1) continue;
    //         foreach (var e in edges[p.v2]) {
    //             Number d = p.v1 + e.v1;
    //             if (dist[e.v2] > d) {
    //                 dist[e.v2] = d;
    //                 cnt[e.v2] = cnt[p.v2];
    //                 q.Push(new pair<Number, int>(d, e.v2));
    //             }
    //             else if (dist[e.v2] == d) cnt[e.v2] = (cnt[e.v2] + cnt[p.v2]) % Mod;
    //         }
    //     }
    //     return dist;
    // }
}
